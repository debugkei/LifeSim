using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LifeSim {
  /// <summary>
  /// Init Resetable interface, class implementing has ability to InitReset
  /// </summary>
  internal interface IInitResetable {
    ///<summary>
    /// Resizes the grid (Resets), and initializes with old values with offsets
    /// </summary>
    public void InitReset(int width, int height, int xOffset, int yOffset);
  }
}
