using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LifeSim {
  /// <summary>
  /// Grid interface, represents the grid (map), that can be accessed, changed, and read
  /// </summary>
  internal interface IGrid {
    /// <summary>
    /// Width of the grid
    /// </summary>
    public int Width { get; }

    /// <summary>
    /// Height of the grid
    /// </summary>
    public int Height { get; }

    /// <summary>
    /// Empty the whole grid, assign default values to every cell
    /// </summary>
    public void Empty();

    /// <summary>
    /// Randomly assigns values to the cells, with given density
    /// </summary>
    /// <param name="density"></param>
    public void Random(int density);

    /// <summary>
    /// Resets the whole grid with given values
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    public void Reset(int width, int height);

    ///<summary>
    /// Resizes the grid (Resets), and initializes with old values with offsets
    /// </summary>
    public void InitReset(int width, int height, int xOffset, int yOffset);

    /// <summary>
    /// Moves the grid by 2 offsets
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void Move(int x, int y);

    /// <summary>
    /// Moves the grid by 2 offsets in multitask mode
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void MoveMT(int x, int y, int nThreads);

    /// <summary>
    /// The indexer, allows indexed access to the grid
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public object this[int x, int y] {
      get; set;
    }

    /// <summary>
    /// The grid property itself, to set the grid itself
    /// </summary>
    public object Grid { get; set; }
  }
}
