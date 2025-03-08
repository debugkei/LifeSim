using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace lifegame {
  /// <summary>
  /// Classic Renderer for WinForms, renders via CPU
  /// </summary>
  internal class TeamsRenderer {
    private Graphics _graphics;
    private SolidBrush[] _cellBrushes;
    private View _view;
    public Color[] CellColors { set { _cellBrushes = GetCellBrushes(value); } }
    public int Teams { get => _cellBrushes.Length; }
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
    public TeamsRenderer(View view, Color[] cellColors, int offset, int resolution, bool pixelOffBorder, Color backgroundColor) {
      //Init
      _view = view;
      CellColors = cellColors;
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
    public void RenderGrid(TeamsGrid grid) {
      var nW = grid.Width - Offset;
      var nH = grid.Height - Offset;
      for (int i = Offset; i < nW; i++) {
        for (int j = Offset; j < nH; j++) {
          //Render the cells if its alive
          if (grid[i, j] == 0) continue; // Cell is not alive
          if (grid[i, j] + 1 >= _cellBrushes.Length) continue; //Skip unknown cell value
          _graphics.FillRectangle(_cellBrushes[grid[i, j] + 1], i * Resolution, j * Resolution,
          CellWidth, CellWidth);
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

    ///<summary>
    /// Gets the cell brushes
    /// </summary>
    private SolidBrush[] GetCellBrushes(Color[] colors) {
      var brushes = new SolidBrush[colors.Length];

      for (var i = 0; i < brushes.Length; ++i) {
        brushes[i] = new SolidBrush(colors[i]);
      }

      return brushes;
    }
  }
}