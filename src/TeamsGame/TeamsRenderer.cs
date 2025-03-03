using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSim {
  /// <summary>
  /// Teams Renderer for WinForms, renders via CPU
  /// </summary>
  internal class TeamsRenderer : IRendererWithMouse {
    private Graphics _graphics;
    private PictureBox _pbMap;
    private SolidBrush[] _cellBrushes;
    private RMDDTO _dto;

    //Ctor
    public TeamsRenderer(PictureBox pbMap, RMDDTO dto) {
      //Init values
      _pbMap = pbMap;
      _dto = dto;

      //Set up callbacks
      _dto.OnCellColorChanged += () => { _cellBrushes = GetCellBrushes(); };

      //Create map
      _pbMap.Image = new Bitmap(_pbMap.Width, _pbMap.Height);
      _graphics = Graphics.FromImage(_pbMap.Image);

      //Set variables
      _cellBrushes = GetCellBrushes();
    }

    /// <summary>
    /// Renders only the cells on the grid, not its job to clear
    /// </summary>
    /// <param name="grid"></param>
    public void RenderGrid(IInitResetable grid) {
      var nW = grid.Width - _dto.Offset;
      var nH = grid.Height - _dto.Offset;
      for (int i = 0; i + _dto.Offset < nW; i++) {
        for (int j = 0; j + _dto.Offset < nH; j++) {
          //Render the cells according to their colors
          if ((byte)grid[i + _dto.Offset, j + _dto.Offset] > 0) {
            var cellBrush = new SolidBrush(((Color[])_dto.CellColor)[(byte)grid[i + _dto.Offset, j + _dto.Offset] - 1]);
            _graphics.FillRectangle(cellBrush, i * _dto.Resolution, j * _dto.Resolution,
            _dto.CellWidth, _dto.CellWidth);
          }
        }
      }
    }

    /// <summary>
    /// Renders the mouse steps, color below cursor that shows where to draw, on the grid
    /// </summary>
    /// <param name="grid"></param>
    public void RenderMouseSteps(IInitResetable grid, IMouseHandler mouseLogic) {
      for (int i = 0; i < mouseLogic.BrushWidth; ++i) {
        for (int j = 0; j < mouseLogic.BrushHeight; ++j) {
          if (_dto.MouseStepsRects[i, j] != null) _graphics.FillRectangle(_dto.MouseStepsBrush, _dto.MouseStepsRects[i, j] ?? default);
        }
      }
    }

    /// <summary>
    /// Clears the map
    /// </summary>
    public void Clear() {
      _graphics.Clear(_dto.BackgroundColor);
    }

    /// <summary>
    /// Gets the cellBrushes
    /// </summary>
    /// <returns></returns>
    private SolidBrush[] GetCellBrushes() {
      Color[] colors = (Color[])_dto.CellColor;
      SolidBrush[] cellBrushes = new SolidBrush[colors.Length];

      for (var i = 0; i < colors.Length; ++i) {
        cellBrushes[i] = new SolidBrush(colors[i]);
      }

      return cellBrushes;
    }
  }
}