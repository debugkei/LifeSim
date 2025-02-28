using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSim
{
  internal class UnitTester
  {
    public static void TestAll()
    {
      //Test dividing map vertically
      var tester1 = new DivideMapVerticallyCPUBoolPresenter2DTester();
      tester1.Test(2, 100, 50);
      tester1.Test(1, 100, 100);
      tester1.Test(3, 100, 34);
    }
  }
  internal class DivideMapVerticallyCPUBoolPresenter2DTester
  {
    private BitModel2D _model;
    public DivideMapVerticallyCPUBoolPresenter2DTester()
    {
      _model = new BitModel2D();
    }
    public void Test(int parts, int width, int expectedWidthOfFirstElem)
    {
      _model.Width = width;
      var partsArr = CPUBitMTPresenter2D.DivideMapIntoPartsVertically(parts, _model.Width, _model.Height);
      Debug.Assert(partsArr[0][0][1] == expectedWidthOfFirstElem);
    }
  }
}
