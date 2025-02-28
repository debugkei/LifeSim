using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSim {
  /// <summary>
  /// Renderer interface, its responsible for rendering the grid in any app
  /// </summary>
  internal interface IRenderer {
    /// <summary>
    /// Renders only the cells on the grid, not its job to clear
    /// </summary>
    /// <param name="grid"></param>
    public void RenderGrid(IGrid grid);

    /// <summary>
    /// Clears the map
    /// </summary>
    public void Clear();
  }
}