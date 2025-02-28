using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSim
{
  public interface ICPUMTPresenter2D
  {
    public void EmptyMap();
    public void RandomMap(int density);
    public void CalculateNextGen(int nThreads);
  }
}
