using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LifeSim {
  /// <summary>
  /// The core logic of the LifeSim, the generations computations, computes the classical way
  /// </summary>
  internal class ClassicCoreLogic : ICoreLogic {
    /// <summary>
    /// Calculates the next generation on single thread, and assigns it to the grid
    /// </summary>
    /// <param name="grid"></param>
    public void CalculateNextGen(IGrid grid) {
      //Create a bit array, as temp grid
      var newGrid = new BitArray[grid.Width];
      for (var i = 0; i < grid.Width; ++i) {
        newGrid[i] = new BitArray(grid.Height);
      }

      //Update the temp grid, as new generation of the grid
      for (var i = 0; i < grid.Width; ++i) {
        for (var j = 0; j < grid.Height; ++j) {
          var neighborCount = CountNeighbors(grid, i, j);
          var hasLife = (bool)grid[i,j];

          newGrid[i][j] = hasLife
            ? neighborCount == 2 || neighborCount == 3
            : neighborCount == 3;
        }
      }

      //Assign the grid to temp
      grid.Grid = newGrid;
    }

    /// <summary>
    /// Calculates the next generation with multitasking, and assigns it to the grid
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="nThreads"></param>
    public void CalculateNextGenMT(IGrid grid, int nThreads) {
      //Declare list of tasks, and positions that tasks are responsible for
      List<Task> tasks = new List<Task>();
      var parts = Funcs.DivideGridIntoParts(nThreads, grid.Width, grid.Height);

      //Declare temp grid for updating values
      var newGrid = new BitArray[grid.Width];
      for (var i = 0; i < grid.Width; ++i) {
        newGrid[i] = new BitArray(grid.Height);
      }

      //Push all tasks on tasks list
      for (int i = 0; i < nThreads; ++i) {
        var task_i = i;
        //Create calculating task
        var task = Task.Run(() => {
          ThreadCalculateNextGen(grid, parts[task_i][0][0], parts[task_i][0][1], parts[task_i][1][0], parts[task_i][1][1], newGrid);
        });

        //Add the created task on list
        tasks.Add(task);
      }

      //Wait for all tasks
      foreach (var i in tasks) {
        i.Wait();
      }

      //Assign new mapData back to old
      grid.Grid = newGrid;
    }

    /// <summary>
    /// Calculation executed on single thread, that calculates part of the grid
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="startWidth"></param>
    /// <param name="endWidth"></param>
    /// <param name="startHeight"></param>
    /// <param name="endHeight"></param>
    /// <param name="newGrid"></param>
    private void ThreadCalculateNextGen(IGrid grid, int startWidth, int endWidth, int startHeight, int endHeight, BitArray[] newGrid) {
      //Calculate next gen of the part of the map, and update the newGrid variable
      for (int j = startWidth; j < endWidth; ++j) {
        for (int k = startHeight; k < endHeight; ++k) {
          var neighborCount = CountNeighbors(grid, j, k);
          var hasLife = (bool)grid[j, k];

          newGrid[j][k] = hasLife
            ? neighborCount == 2 || neighborCount == 3
            : neighborCount == 3;
        }
      }
    }

    /// <summary>
    /// Counts neighbors around a cell on the grid
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    private int CountNeighbors(IGrid grid, int x, int y) {
      var count = 0;

      for (var i = -1; i <= 1; ++i) {
        for (var j = -1; j <= 1; ++j) {
          //Calculate the position
          var _x = (x + i + grid.Width) % grid.Width;
          var _y = (y + j + grid.Height) % grid.Height;

          //Dont self count
          if (x == _x && _y == y) continue;

          //Increment
          if ((bool)grid[_x, _y]) {
            ++count;
          }
        }
      }

      return count;
    }
  }
}
