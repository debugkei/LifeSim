using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSim
{
  internal class ByteModel2D
  {
    public byte[,] MapData { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
  }
}