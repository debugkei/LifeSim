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
  internal class ClassicRenderer {
    private Graphics _graphics;
    private SolidBrush _cellBrush;
    private View _view;
    public Color CellColor { set { _cellBrush = new SolidBrush(value); } }
    /// <summary>
    /// Offset of the map at which its rendered
    /// </summary>
    public int Offset { get; set; }
    public int Resolution { get; set; }
    //Cell width
    public int CellWidth { get; private set; }
    public bool PixelOffBorder { set { CellWidth = GetCellWidth(value); } }
    public Color BackgroundColor { get; set; }
    //Ctor
    public ClassicRenderer(View view, Color cellColor, int offset, int resolution, bool pixelOffBorder, Color backgroundColor) {
      //Init
      _view = view;
      CellColor = cellColor;
      Offset = offset;
      Resolution = resolution;
      PixelOffBorder = pixelOffBorder;
      BackgroundColor = backgroundColor;

      //Create map
      _view.Image = new Bitmap(_view.GridWidth, _view.GridHeight); // _view.Image is pbMap's image, same with Width and Height
      _graphics = Graphics.FromImage(_view.Image);
    }

    /// <summary>
    /// Renders only the cells on the grid, not its job to clear
    /// </summary>
    /// <param name="grid"></param>
    public void RenderGrid(ClassicGrid grid) {
      var nW = grid.Width - Offset;
      var nH = grid.Height - Offset;
      for (int i = Offset; i < nW; i++) {
        for (int j = Offset; j < nH; j++) {
          //Render the cell if its alive
          if (grid[i, j]) {
            _graphics.FillRectangle(_cellBrush, i * Resolution, j * Resolution,
            CellWidth, CellWidth);
          }
        }
      }
    }

    /// <summary>
    /// Renders a single rectangle
    /// </summary>
    /// <param name="grid"></param>
    public void RenderRect(SolidBrush brush, Rectangle rect) {
      _graphics.FillRectangle(brush, rect);
    }

    /// <summary>
    /// Clears the map
    /// </summary>
    public void Clear() {
      _graphics.Clear(BackgroundColor);
    }

    /// <summary>
    /// Calculates the width of a single cell
    /// </summary>
    /// <returns></returns>
    private int GetCellWidth(bool pixelOffBorder) {
      return Resolution - (pixelOffBorder ? 1 : 0);
    }
  }
}