using LifeSim;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSim {
  /// <summary>
  /// Classic Renderer for WinForms, renders via CPU
  /// </summary>
  internal class ClassicRenderer : IRendererWithMouse {
    private Graphics _graphics;
    private PictureBox _pbMap;
    private SolidBrush _cellBrush;
    private RMDDTO _dto;

    //Ctor
    public ClassicRenderer(PictureBox pbMap, RMDDTO dto) {
      //Init values
      _pbMap = pbMap;
      _dto = dto;

      //Set up callbacks
      _dto.OnCellColorChanged += () => { _cellBrush = new SolidBrush((Color)_dto.CellColor); };

      //Create map
      _pbMap.Image = new Bitmap(_pbMap.Width, _pbMap.Height);
      _graphics = Graphics.FromImage(_pbMap.Image);

      //Set variables
      _cellBrush = new SolidBrush((Color)_dto.CellColor);
    }

    /// <summary>
    /// Renders only the cells on the grid, not its job to clear
    /// </summary>
    /// <param name="grid"></param>
    public void RenderGrid(IGrid grid) {
      var nW = grid.Width - _dto.Offset;
      var nH = grid.Height - _dto.Offset;
      for (int i = 0; i + _dto.Offset < nW; i++) {
        for (int j = 0; j + _dto.Offset < nH; j++) {
          //Render the cell if its alive
          if ((bool)grid[i + _dto.Offset, j + _dto.Offset]) {
            _graphics.FillRectangle(_cellBrush, i * _dto.Resolution, j * _dto.Resolution,
            _dto.CellWidth, _dto.CellWidth);
          }
        }
      }
    }

    /// <summary>
    /// Renders the mouse steps, color below cursor that shows where to draw, on the grid
    /// </summary>
    /// <param name="grid"></param>
    public void RenderMouseSteps(IGrid grid, IMouseLogic mouseLogic) {
      for (int i = 0; i < mouseLogic.BrushWidth; ++i) {
        for (int j = 0; j < mouseLogic.BrushHeight; ++j) {
          _graphics.FillRectangle(_dto.MouseStepsBrush, _dto.MouseStepsRects[i, j]);
        }
      }
    }

    /// <summary>
    /// Clears the map
    /// </summary>
    public void Clear() {
      _graphics.Clear(_dto.BackgroundColor);
    }
  }
}