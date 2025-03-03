using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSim {
  /// <summary>
  /// Teams mouse logic implementation
  /// </summary>
  class TeamsMouseLogic : IMouseHandler {
    private bool _isMoving;
    private bool _isDrawing;
    private bool _isErasing;
    private int _previousX;
    private int _previousY;
    private Point _brushCenter;
    private Rectangle?[,] _mouseStepsRects;
    public TeamsMouseLogic(RMDDTO dto, int brushWidth, int brushHeight, int x, int y) {
      //Init
      BrushWidth = brushWidth;
      BrushHeight = brushHeight;
      X = x;
      Y = y;
      _mouseStepsRects = new Rectangle?[BrushWidth, BrushHeight];
      _brushCenter = Funcs.GetBrushCenter(X, Y, BrushWidth, BrushHeight);
    }

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
    //Callback to update rectangles on change
    public int BrushWidth {
      get => BrushWidth; set {
        BrushWidth = value;
        _mouseStepsRects = new Rectangle?[BrushWidth, BrushHeight];
        _brushCenter = Funcs.GetBrushCenter(X, Y, BrushWidth, BrushHeight);
      }
    }
    /// <summary>
    /// Height of the brush (to paint on the grid)
    /// </summary>
    //Callback to update rectangles on change
    public int BrushHeight {
      get => BrushHeight; set {
        BrushHeight = value;
        _mouseStepsRects = new Rectangle?[BrushWidth, BrushHeight];
        _brushCenter = Funcs.GetBrushCenter(X, Y, BrushWidth, BrushHeight);
      }
    }

    /// <summary>
    /// Applies only all the computations that are made to the grid.
    /// Necessary to be called to draw or erase.
    /// </summary>
    public void ApplyGridChanges(IInitResetable grid) {
      //Draw
      if (_isDrawing) Draw(grid);
      //Erase
      else if (_isErasing) Erase(grid);
    }

    /// <summary>
    /// Applies only all the visual changes.
    /// Necessary to be called to move or zoom.
    /// </summary>
    public void ApplyVisualChanges(IInitResetable grid) {
      //Move
      if (_isMoving) grid.Move(_previousX - X, _previousY - Y);
      //Set the rectangles for renderer to draw as mouse steps
      if (_previousY != Y || _previousX != X) {
        for (var i = 0; i < BrushWidth; ++i) {
          for (var j = 0; j < BrushWidth; ++j) {
            //It makes a cursor (center) be in the middle, if the width or height is even if focuses rectangle to the left (or up)
            _mouseStepsRects[i, j] = new Rectangle((i - _brushCenter.X + X), (j - _brushCenter.Y + Y), _dto.CellWidth, _dto.CellWidth);
          }
        }
      }

      _previousX = X;
      _previousY = Y;
    }

    private void Draw(IInitResetable grid) {
      for (var i = 0; i < BrushWidth; ++i) {
        for (var j = 0; j < BrushWidth; ++j) {
          //Draw all elements including the brush thickness
          grid[i - _brushCenter.X + X, j - _brushCenter.Y + Y] = ; //Draw specific team
        }
      }
    }
    private void Erase(IInitResetable grid) {
      for (var i = 0; i < BrushWidth; ++i) {
        for (var j = 0; j < BrushWidth; ++j) {
          //Draw all elements including the brush thickness
          grid[i - _brushCenter.X + X, j - _brushCenter.Y + Y] = false;
        }
      }
    }
    /// <summary>
    /// Informs that specific mouse button was clicked
    /// </summary>
    /// <param name="grid"></param>
    public void HandleMouseDown(MouseButtonType type) {
      switch (type) {
        case MouseButtonType.Left:
          _isDrawing = true;
          _isErasing = false;
          break;
        case MouseButtonType.Right:
          _isErasing = true;
          _isDrawing = false;
          break;
        case MouseButtonType.Middle:
          _isMoving = true;
          break;
      }
    }

    /// <summary>
    /// Informs that specific mousebutton went up
    /// </summary>
    /// <param name="grid"></param>
    public void HandleMouseUp(MouseButtonType type) {
      switch (type) {
        case MouseButtonType.Left:
          _isDrawing = false;
          break;
        case MouseButtonType.Right:
          _isErasing = false;
          break;
        case MouseButtonType.Middle:
          _isMoving = false;
          break;
      }
    }

    /// <summary>
    /// Informs that mouse moved.
    /// </summary>
    /// <param name="grid"></param>
    public void HandleMouseMove(int x, int y) {
      _previousX = X;
      _previousY = Y;
      X = x;
      Y = y;
    }

    /// <summary>
    /// Changes the resolution, centered on cursor.
    /// </summary>
    /// <param name="grid"></param>
    public void HandleMouseWheel(IInitResetable grid, int delta) {
      //Check if the mouse is on the grid
      if (X >= 0 && Y >= 0 && X < grid.Width && Y < grid.Height) {
        //Change resolution, delta will be checked inside
        _view.ChangeResolution(grid, delta, X, Y);
      }
    }
  }
}