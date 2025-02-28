using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace LifeSim
{
  internal interface IGUICPUMTMechanics2D
  {
    public void ResetModel(PictureBox pbMap, int offset, int resolution);
    public void ResetAndInitModel(PictureBox pbMap, int offset, int resolution);
    public void ResetAndInitModel(PictureBox pbMap, int xOffsetNew, int yOffsetNew, int xOffsetOld, int yOffsetOld, int offset, int resolution, int nThreads);
    public void HandleMouse(MouseEventArgs e, PictureBox pbMap, int offset, int resolution, int brushThickness, bool isRandomBrush, int density, int nThreads);
    public void DrawWholeMap(int offset, bool pixelOffBorder, Graphics graphics, int resolution, Color cellColor);
    public void DrawMouseSteps(Graphics graphics, int resolution, bool pixelOffBorder, int mouseStepsTransparency, Color mouseStepsColor);
    public void UpdatePreviousMousePos(int xOffset, int yOffset);
  }
}
