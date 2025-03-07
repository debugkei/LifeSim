using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSim {
  /// <summary>
  /// Teams mouse logic implementation
  /// </summary>
  class TeamsMouseHandler {
    private bool _isMoving;
    private bool _isDrawing;
    private bool _isErasing;
    private int _previousX;
    private int _previousY;
    private Point _brushCenter;
    private Rectangle[,] _mouseShadeRects;
    private TeamsRenderer _renderer;
    //Mouse steps "Preview of draw. Color below cursor."
    private SolidBrush _mouseShadeBrush;
    public Color MouseShadeColor { set { _mouseShadeBrush = GetMouseStepsBrush(_mouseShadeBrush.Color.A, value); } }
    public byte MouseShadeAlpha { set { _mouseShadeBrush = GetMouseStepsBrush(value, _mouseShadeBrush.Color); } }
    private View _view;
    /// <summary>
    /// Index of the team the client wants to draw, it can be 0-indexed.
    /// </summary>
    public byte TeamToDraw { get; set; }
    public TeamsMouseHandler(int brushWidth, int brushHeight, int x, int y, TeamsRenderer renderer, Color mouseShadeColor, byte mouseShadeAlpha, View view, byte teamToDraw) {
      //Init
      X = x;
      Y = y;
      _renderer = renderer;
      _mouseShadeBrush = GetMouseStepsBrush(mouseShadeAlpha, mouseShadeColor);
      _mouseShadeRects = new Rectangle[brushWidth, brushHeight];
      _brushCenter = Funcs.GetBrushCenter(X, Y, brushWidth, brushHeight);
      _view = view;
      TeamToDraw = teamToDraw;
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
    public int BrushWidth { get => _mouseShadeRects.GetLength(0); set {
        _mouseShadeRects = new Rectangle[value, _mouseShadeRects.GetLength(1)];
        _brushCenter = Funcs.GetBrushCenter(X, Y, value, _mouseShadeRects.GetLength(1));
      }
    }
    /// <summary>
    /// Height of the brush (to paint on the grid)
    /// </summary>
    //Callback to update rectangles on change
    public int BrushHeight { get => _mouseShadeRects.GetLength(1);  set {
        _mouseShadeRects = new Rectangle[_mouseShadeRects.GetLength(0), value];
        _brushCenter = Funcs.GetBrushCenter(X, Y, _mouseShadeRects.GetLength(0), value);
      }
    }

    /// <summary>
    /// Applies only all the computations that are made to the grid.
    /// Necessary to be called to draw or erase.
    /// </summary>
    public void ApplyGridChanges(TeamsGrid grid) {
      //Draw
      if (_isDrawing) Draw(grid);
      //Erase
      else if (_isErasing) Erase(grid);
    }

    /// <summary>
    /// Applies only all the visual changes.
    /// Necessary to be called to move or zoom.
    /// </summary>
    public void ApplyVisualChanges(TeamsGrid grid) {
      //Move
      if (_isMoving) grid.Move(_previousX - X, _previousY - Y);
      //Set the rectangles for renderer to draw as mouse steps
      if (_previousY != Y || _previousX != X) {
        for (var i = 0; i < BrushWidth; ++i) {
          for (var j = 0; j < BrushWidth; ++j) {
            //It makes a cursor (center) be in the middle, if the width or height is even if focuses rectangle to the left (or up)
            _mouseShadeRects[i, j] = new Rectangle((i - _brushCenter.X + X), (j - _brushCenter.Y + Y), _renderer.CellWidth, _renderer.CellWidth);
          }
        }
      }
      //Ask renderer to render mouse steps
      for (var i = 0; i < BrushWidth; ++i) {
        for (var j = 0; j < BrushWidth; ++j) {
          var x = _mouseShadeRects[i, j].X / _renderer.Resolution;
          var y = _mouseShadeRects[i, j].Y / _renderer.Resolution;
          if (x >= 0 && y >= 0 && x < grid.Width && y < grid.Height) _renderer.RenderRect(_mouseShadeBrush, _mouseShadeRects[i, j]);
        }
      }

      _previousX = X;
      _previousY = Y;
    }

    private void Draw(TeamsGrid grid) {
      if (_renderer.Teams <= TeamToDraw + 1) return; //Return if Team to draw is greater than possible teams availible
      for (var i = 0; i < BrushWidth; ++i) {
        for (var j = 0; j < BrushWidth; ++j) {
          //Draw all elements including the brush thickness
          grid[i - _brushCenter.X + X, j - _brushCenter.Y + Y] = (byte)(TeamToDraw + 1);
        }
      }
    }
    private void Erase(TeamsGrid grid) {
      for (var i = 0; i < BrushWidth; ++i) {
        for (var j = 0; j < BrushWidth; ++j) {
          //Draw all elements including the brush thickness
          grid[i - _brushCenter.X + X, j - _brushCenter.Y + Y] = 0;
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
    public void HandleMouseWheel(TeamsGrid grid, int delta) {
      //Check if the mouse is on the grid
      if (X >= 0 && Y >= 0 && X < grid.Width && Y < grid.Height) {
        //Change resolution, delta will be checked inside
        _view.ChangeResolution(grid, delta, X, Y);
      }
    }

    /// <summary>
    /// Gets the mouse steps brush
    /// </summary>
    private SolidBrush GetMouseStepsBrush(byte alpha, Color color) {
      return new SolidBrush(Color.FromArgb(alpha, color));
    }
  }
}