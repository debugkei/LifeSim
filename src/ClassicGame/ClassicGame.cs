using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSim {
  /// <summary>
  /// The game interface implementation, game is managed from here.
  /// Specifically classic game implementation.
  /// </summary>
  internal class ClassicGame : IGame {
    private ClassicRenderer _renderer;
    private ClassicGrid _grid;
    private ClassicMouseHandler _mouseHanlder;
    private ClassicRules _rules;
    public ClassicGame(View view, Color cellColor, int offset, int resolution, bool pixelOffBorder, Color backgroundColor, int widthGrid, int heightGrid,
                       byte mouseShadeAlpha, Color mouseShadeColor, int brushHeight, int brushWidth, int xMouse, int yMouse) {
      //Renderer init
      _renderer = new(view, cellColor, offset, resolution, pixelOffBorder, backgroundColor);

      //Grid init
      _grid = new(widthGrid, heightGrid);

      //Rules init
      _rules = new();

      //MouseHandler init
      _mouseHanlder = new(brushWidth, brushHeight, xMouse, yMouse, _renderer, mouseShadeColor, mouseShadeAlpha, view);
    }
    //Setters
    public Color CellColor { set { if (_renderer != null) _renderer.CellColor = value; } }
    public int Offset { set { if (_renderer != null) _renderer.Offset = value; } }
    public Color BackgroundColor { set { if (_renderer != null) _renderer.BackgroundColor = value; } }
    public bool PixelOffBorder { set { if (_renderer != null) _renderer.PixelOffBorder = value; } }
    public int Resolution { set { if (_renderer != null) _renderer.Resolution = value; } }
    public byte MouseShadeAlpha { set { if (_mouseHanlder != null) _mouseHanlder.MouseShadeAlpha = value; } }
    public Color MouseShadeColor { set { if (_mouseHanlder != null) _mouseHanlder.MouseShadeColor = value; } }
    public int BrushWidth { set { if (_mouseHanlder != null) _mouseHanlder.BrushWidth = value; } }
    public int BrushHeight { set { if (_mouseHanlder != null) _mouseHanlder.BrushHeight = value; } }

    public void Empty() {
      _grid.Empty();
    }

    public void FixedUpdate() {
      _mouseHanlder.ApplyGridChanges(_grid);
      _rules.Apply(_grid);
    }

    public void MouseDown(MouseButtonType type) {
      _mouseHanlder.HandleMouseDown(type);
    }

    public void MouseMove(int x, int y) {
      _mouseHanlder.HandleMouseMove(x, y);
    }

    public void MouseUp(MouseButtonType type) {
      _mouseHanlder.HandleMouseUp(type);
    }

    public void MouseWheel(int delta) {
      _mouseHanlder.HandleMouseWheel(_grid, delta);
    }

    public void Fill() {
      _grid.Random(new Random().Next(2, 20));
    }

    public void Update() {
      _renderer.RenderGrid(_grid);
      _mouseHanlder.ApplyVisualChanges(_grid);
    }
  }
}
