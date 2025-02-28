using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LifeSim
{
  internal class GUICPUByteMTMechanics2D : IGUICPUMTMechanics2D
  {
    private Main _view;
    private readonly ByteModel2D _model;
    private int _previousX;
    private int _previousY;
    private int _previousHalfBrushThickness;
    private int _previousNHalfBrushThickness;
    private Color[] _teamsColors;
    public GUICPUByteMTMechanics2D(Main view, CPUByteMTPresenter2D presenter, Color[] teamsColors)
    {
      _view = view;
      _model = presenter.Model;
      _teamsColors = teamsColors;
    }
    public void DrawWholeMap(int offset, bool pixelOffBorder, Graphics graphics, int resolution, Color cellColor)
    {
      var cellWidth = resolution - (pixelOffBorder ? 1 : 0);
      for (int i = 0; i + offset < _model.Width - offset; i++)
      {
        for (int j = 0; j + offset < _model.Height - offset; j++)
        {
          if (_model.MapData[i + offset, j + offset] > 0)
          {
            var cellBrush = new SolidBrush(_teamsColors[_model.MapData[i + offset, j + offset] - 1]);
            graphics.FillRectangle(cellBrush, i * resolution, j * resolution,
            cellWidth, cellWidth);
          }
        }
      }
    }
    public void DrawMouseSteps(Graphics graphics, int resolution, bool pixelOffBorder, int mouseStepsTransparency, Color mouseStepsColor)
    {
      var cellWidth = resolution - (pixelOffBorder ? 1 : 0);
      var mouseStepsBrush = new SolidBrush(Color.FromArgb(mouseStepsTransparency, mouseStepsColor));
      if (_previousX > 0 && _previousY > 0 && _previousX < _model.Width && _previousY < _model.Height)
      {
        for (int i = _previousNHalfBrushThickness; i < _previousHalfBrushThickness; ++i)
        {
          for (int j = _previousNHalfBrushThickness; j < _previousHalfBrushThickness; ++j)
          {
            var xMap = _previousX + i;
            var yMap = _previousY + j;
            if (xMap >= 0 && xMap < _model.Width && yMap >= 0 && yMap < _model.Height)
            {
              graphics.FillRectangle(mouseStepsBrush, xMap * resolution, yMap * resolution,
                cellWidth, cellWidth);
            }
          }
        }
      }
    }
    public void HandleMouse(MouseEventArgs e, PictureBox pbMap, int offset, int resolution, int brushThickness, bool isRandomBrush, int density, int nThreads)
    {
      var x = e.X / resolution;
      var y = e.Y / resolution;
      int nHalfBrushThickness = -brushThickness / 2;
      int halfBrushThickness = brushThickness / 2 + 1;
      if (e.X > 0 && e.Y > 0 && e.X < pbMap.Width && e.Y < pbMap.Height)
      {
        if (e.Button == MouseButtons.Left)
        {
          HandleMouseLeft(x, y, offset, brushThickness, isRandomBrush, density, halfBrushThickness, nHalfBrushThickness);
        }
        else if (e.Button == MouseButtons.Right)
        {
          HandleMouseRight(x, y, offset, brushThickness, halfBrushThickness, nHalfBrushThickness);
        }
        else if (e.Button == MouseButtons.Middle)
        {
          HandleMouseMiddle(x, y, pbMap, offset, resolution, nThreads);
        }
      }
      _previousX = x;
      _previousY = y;
      _previousHalfBrushThickness = halfBrushThickness;
      _previousNHalfBrushThickness = nHalfBrushThickness;
    }
    public void UpdatePreviousMousePos(int xOffset, int yOffset)
    {
      _previousX += xOffset;
      _previousY += yOffset;
    }
    public void HandleMouseLeft(int x, int y, int offset, int brushThickness, bool isRandomBrush, int density, int halfBrushThickness, int nHalfBrushThickness)
    {
      Random rand = new();
      Action<int, int> setFunc;
      if (isRandomBrush)
      {
        setFunc = (int x, int y) =>
        {
          if (rand.Next(-1, density) == 0)
          {
            _model.MapData[x, y] = (byte)rand.Next(1, _teamsColors.Length + 1);
          }
        };
      }
      else
      {
        setFunc = (int x, int y) =>
        {
          _model.MapData[x, y] = (byte)_view.BrushTeam;
        };
      }
      for (int i = nHalfBrushThickness; i < halfBrushThickness; ++i)
      {
        for (int j = nHalfBrushThickness; j < halfBrushThickness; ++j)
        {
          var xMap = x + i + offset;
          var yMap = y + j + offset;
          if (xMap >= 0 && xMap < _model.Width && yMap >= 0 && yMap < _model.Height)
          {
            setFunc(xMap, yMap);
          }
        }
      }
    }
    public void HandleMouseRight(int x, int y, int offset, int brushThickness, int halfBrushThickness, int nHalfBrushThickness)
    {
      for (int i = nHalfBrushThickness; i < halfBrushThickness; ++i)
      {
        for (int j = nHalfBrushThickness; j < halfBrushThickness; ++j)
        {
          var xMap = x + i + offset;
          var yMap = y + j + offset;
          if (xMap >= 0 && xMap < _model.Width && yMap >= 0 && yMap < _model.Height)
          {
            _model.MapData[xMap, yMap] = 0;
          }
        }
      }
    }
    public void HandleMouseMiddle(int x, int y, PictureBox pbMap, int offset, int resolution, int nThreads)
    {
      ResetAndInitModel(pbMap,
        (_previousX < x ? x - _previousX : 0),
        (_previousY < y ? y - _previousY : 0),
        (_previousX > x ? _previousX - x: 0),
        (_previousY > y ? _previousY - y: 0),
      offset, resolution, nThreads);
    }
    public void ResetModel(PictureBox pbMap, int offset, int resolution)
    {
      _model.Width = pbMap.Width / resolution + offset * 2;
      _model.Height = pbMap.Height / resolution + offset * 2;
      _model.MapData = new byte[_model.Width, _model.Height];
    }
    public void ResetAndInitModel(PictureBox pbMap, int offset, int resolution)
    {
      var initArr = _model.MapData;
      ResetModel(pbMap, offset, resolution);
      for (var x = 0; x < initArr.GetLength(0) && _model.MapData.GetLength(0) > x; ++x)
      {
        for (var y = 0; y < initArr.GetLength(1) && _model.MapData.GetLength(1) > y; ++y)
        {
          _model.MapData[x,y] = initArr[x,y];
        }
      }
    }
    public void ResetAndInitModel(PictureBox pbMap, int xOffsetNew, int yOffsetNew, int xOffsetOld, int yOffsetOld, int offset, int resolution, int nThreads)
    {
      //Declare list of tasks, and positions that tasks are responsible for
      List<Task> tasks = new List<Task>();
      var initArr = _model.MapData;

      //Vertical Find parts
      for (var i = nThreads; i > 0; --i)
      {
        if (_model.Width % i == 0) nThreads = i;
      }
      var oldParts = CPUBitMTPresenter2D.DivideMapIntoPartsVertically(nThreads, _model.Width, _model.Height);
      //Reset the model's size
      ResetModel(pbMap, offset, resolution);
      var newParts = CPUBitMTPresenter2D.DivideMapIntoPartsVertically(nThreads, _model.Width, _model.Height);


      //Push all tasks on tasks list
      for (int i = 0; i < nThreads; ++i)
      {
        var task_i = i;
        //Create calculating task
        var task = Task.Run(() =>
        {
          ThreadResetAndInitModel(
            newParts[task_i][0][0], newParts[task_i][0][1], newParts[task_i][1][0], newParts[task_i][1][1],
            oldParts[task_i][0][0], oldParts[task_i][0][1], oldParts[task_i][1][0], oldParts[task_i][1][1],
            initArr, xOffsetNew, yOffsetNew, xOffsetOld, yOffsetOld);
        });

        //Add the created task on list
        tasks.Add(task);
      }

      //Wait for all tasks
      foreach (var i in tasks)
      {
        i.Wait();
      }
    }
    private void ThreadResetAndInitModel(
      int newStartWidth, int newEndWidth, int newStartHeight, int newEndHeight,
      int oldStartWidth, int oldEndWidth, int oldStartHeight, int oldEndHeight,
      byte[,] initArr, int xOffsetNew, int yOffsetNew, int xOffsetOld, int yOffsetOld)
    {
      //Backup of old ResetModel
      //for (var x = 0; (x + xOffsetOld) < initArr.GetLength(0) && _model.MapData.GetLength(0) > (x + xOffsetNew); ++x)
      //{
      //  for (var y = 0; (y + yOffsetOld) < initArr.GetLength(1) && _model.MapData.GetLength(1) > (y + yOffsetNew); ++y)
      //  {
      //    _model.MapData[(x + xOffsetNew), (y + yOffsetNew)] = initArr[(x + xOffsetOld), (y + yOffsetOld)];
      //  }
      //}
      for (int xNew = newStartWidth, xOld = oldStartWidth; (xOld + xOffsetOld) < oldEndWidth && newEndWidth > (xNew + xOffsetNew); ++xNew, ++xOld)
      {
        for (int yNew = newStartHeight, yOld = oldStartHeight; (yOld + yOffsetOld) < oldEndHeight && newEndHeight > (yNew + yOffsetNew); ++yNew, ++yOld)
        {
          _model.MapData[(xNew + xOffsetNew), (yNew + yOffsetNew)] = initArr[(xOld + xOffsetOld), (yOld + yOffsetOld)];
        }
      }
    }
  }
}
