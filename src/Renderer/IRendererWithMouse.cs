using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSim {
  /// <summary>
  /// More specific interface, that is intended to use in GUI app and render via resolution
  /// </summary>
  internal interface IRendererWithMouse : IRenderer{
    /// <summary>
    /// Renders the mouse steps, color below cursor that shows where to draw, on the grid
    /// </summary>
    /// <param name="grid"></param>
    public void RenderMouseSteps(IGrid grid, IMouseLogic mouseLogic);
  }
}
