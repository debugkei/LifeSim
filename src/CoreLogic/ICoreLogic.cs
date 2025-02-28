using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSim {
  /// <summary>
  /// Core Logic Interface, responsible for computing the next generation
  /// </summary>
  internal interface ICoreLogic {
    /// <summary>
    /// Calculates the next generation on single thread, and assigns it to the grid
    /// </summary>
    /// <param name="grid"></param>
    public void CalculateNextGen(IGrid grid);

    /// <summary>
    /// Calculates the next generation with multitasking, and assigns it to the grid
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="nThreads"></param>
    public void CalculateNextGenMT(IGrid grid, int nThreads);
  }
}
