using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSim {
  /// <summary>
  /// The game interface, game is managed from here.
  /// </summary>
  internal interface IGame {
    /// <summary>
    /// Offset at which map is rendered, allows map to exist beyond user's view.
    /// </summary>
    public int Offset { set; }
    public Color BackgroundColor { set; }
    /// <summary>
    /// Determines whether cells have pixel off border.
    /// </summary>
    public bool PixelOffBorder { set; }
    public int Resolution { set; }
    public byte MouseShadeAlpha { set; }
    public Color MouseShadeColor { set; }
    public int BrushWidth { set; }
    public int BrushHeight { set; }
    public void Update();
    public void FixedUpdate();
    public void Fill();
    public void Empty();
    public void MouseMove(int x, int y);
    public void MouseUp(MouseButtonType type);
    public void MouseDown(MouseButtonType type);
    public void MouseWheel(int delta);
  }
}
