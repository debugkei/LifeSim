using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSim {
  /// <summary>
  /// Mouse logic, responible for handling the mouse
  /// </summary>
  internal interface IMouseHandler {
    /// <summary>
    /// Cursor X cordinate on the grid
    /// </summary>
    public int X { get; set; }
    /// <summary>
    /// Cursor Y cordinate on the grid
    /// </summary>
    public int Y { get; set; }
    /// <summary>
    /// Width of the brush (to paint on the grid)
    /// </summary>
    public int BrushWidth { get; set; }
    /// <summary>
    /// Height of the brush (to paint on the grid)
    /// </summary>
    public int BrushHeight { get; set; }

    //2 methods below are split into 2, because client has to be able to render and compute at different points of time

    /// <summary>
    /// Applies only all the computations that are made to the grid.
    /// Necessary to be called to draw or erase.
    /// </summary>
    public void ApplyGridChanges(IInitResetable grid);

    /// <summary>
    /// Applies only all the visual changes.
    /// Necessary to be called to move or zoom.
    /// </summary>
    public void ApplyVisualChanges(IInitResetable grid);

    /// <summary>
    /// Informs that specific mouse button was clicked
    /// </summary>
    /// <param name="grid"></param>
    public void HandleMouseDown(MouseButtonType type);

    /// <summary>
    /// Informs that specific mousebutton went up
    /// </summary>
    /// <param name="grid"></param>
    public void HandleMouseUp(MouseButtonType type);

    /// <summary>
    /// Informes that mouse moved.
    /// </summary>
    /// <param name="grid"></param>
    public void HandleMouseMove(int x, int y);

    /// <summary>
    /// Informs that mouse wheel was used.
    /// </summary>
    /// <param name="grid"></param>
    public void HandleMouseWheel(IInitResetable grid, int delta);
  }

  enum MouseButtonType {
    Left,
    Right,
    Middle
  }
}