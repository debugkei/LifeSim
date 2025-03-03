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
  internal class ClassicRenderer : IRenderer {
    private Graphics _graphics;
    private SolidBrush _cellBrush;
    public Color CellColor { get => CellColor; set { CellColor = value; _cellBrush = new SolidBrush(CellColor); } }
    public int Offset { get; set; }
    public int Resolution { get; set; }
    private int _cellWidth;
    public bool PixelOffBorder { get => PixelOffBorder; set { PixelOffBorder = value; _cellWidth = GetCellWidth(); } }
    //Ctor
    public ClassicRenderer() {
      //Create map
      _view.Image = new Bitmap(_view.Width, _view.Height); // _view.Image is pbMap's image, same with Width and Height
      _graphics = Graphics.FromImage(_view.Image);

      //Set variables
      _cellBrush = new SolidBrush(CellColor);
    }

    /// <summary>
    /// Renders only the cells on the grid, not its job to clear
    /// </summary>
    /// <param name="grid"></param>
    public void RenderGrid(ClassicGrid grid) {
      var nW = grid.Width - Offset;
      var nH = grid.Height - Offset;
      for (int i = 0; i + Offset < nW; i++) {
        for (int j = 0; j + Offset < nH; j++) {
          //Render the cell if its alive
          if ((bool)grid[i + Offset, j + Offset]) {
            _graphics.FillRectangle(_cellBrush, i * Resolution, j * Resolution,
            _cellWidth, _cellWidth);
          }
        }
      }
    }

    /// <summary>
    /// Renders the mouse steps, color below cursor that shows where to draw, on the grid
    /// </summary>
    /// <param name="grid"></param>
    public void RenderMouseSteps(ClassicGrid grid, IMouseHandler mouseLogic) {
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
    /// Calculates the width of a single cell
    /// </summary>
    /// <returns></returns>
    private int GetCellWidth() {
      return Resolution - (PixelOffBorder ? 1 : 0);
    }
  }
}