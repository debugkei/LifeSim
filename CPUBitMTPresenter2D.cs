using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSim
{
  internal class CPUBitMTPresenter2D : ICPUMTPresenter2D
  {
    public BitModel2D Model { get; private set; }
    public CPUBitMTPresenter2D()
    {
      Model = new();
    }
    public void EmptyMap()
    {
      for (var i = 0; i < Model.Width; ++i)
      {
        for (var j = 0; j < Model.Height; ++j)
        {
          Model.MapData[i][j] = false;
        }
      }
    }
    public void RandomMap(int density)
    {
      Random rand = new();
      for (int i = 0; i < Model.Width; ++i)
      {
        for (int j = 0; j < Model.Height; ++j)
        {
          Model.MapData[i][j] = (rand.Next(-1, density) == 0);
        }
      }
    }
    //Single threaded version
    private void CalculateNextGen()
    {
      var newMapData = new BitArray[Model.Width];
      for (var i = 0; i < Model.Width; ++i)
      {
        newMapData[i] = new BitArray(Model.Height);
      }
      for (int i = 0; i < Model.Width; ++i)
      {
        for (int j = 0; j < Model.Height; ++j)
        {
          var neighborCount = CountNeighbors(i, j);
          var hasLife = Model.MapData[i][j];

          newMapData[i][j] = hasLife
            ? neighborCount == 2 || neighborCount == 3
            : neighborCount == 3;
        }
      }
      Model.MapData = newMapData;
    }
    //Multi threaded version
    public void CalculateNextGen(int nThreads)
    {
      //Declare list of tasks, and positions that tasks are responsible for
      List<Task> tasks = new List<Task>();
      var parts = DivideMapIntoPartsVertically(nThreads, Model.Width, Model.Height);

      //Declare new mapData
      var newMapData = new BitArray[Model.Width];
      for (var i = 0; i < Model.Width; ++i)
      {
        newMapData[i] = new BitArray(Model.Height);
      }

      //Push all tasks on tasks list
      for (int i = 0; i < nThreads; ++i)
      {
        var task_i = i;
        //Create calculating task
        var task = Task.Run(() => {
          ThreadCalculateNextGen(parts[task_i][0][0], parts[task_i][0][1], parts[task_i][1][0], parts[task_i][1][1], newMapData);
        });

        //Add the created task on list
        tasks.Add(task);
      }

      //Wait for all tasks
      foreach (var i in tasks)
      {
        i.Wait();
      }

      //Assign new mapData back to old
      Model.MapData = newMapData;
    }
    private int CountNeighbors(int x, int y)
    {
      int count = 0;

      for (int i = -1; i <= 1; ++i)
      {
        for (int j = -1; j <= 1; ++j)
        {
          var _x = (x + i + Model.Width) % Model.Width;
          var _y = (y + j + Model.Height) % Model.Height;
          if (Model.MapData[_x][_y])
          {
            ++count;
          }
        }
      }
      if (Model.MapData[x][y])
      {
        --count;
      }
      return count;
    }
    //Calculation executed on single thread, that calculates part of the map
    private void ThreadCalculateNextGen(int startWidth, int endWidth, int startHeight, int endHeight, BitArray[] newMapData)
    {
      //Calculate next gen into newMapData
      for (int j = startWidth; j < endWidth; ++j)
      {
        for (int k = startHeight; k < endHeight; ++k)
        {
          var neighborCount = CountNeighbors(j, k);
          var hasLife = Model.MapData[j][k];

          newMapData[j][k] = hasLife
            ? neighborCount == 2 || neighborCount == 3
            : neighborCount == 3;
        }
      }
    }

    //Divides the map into i different parts, so threads can all have individual part, to compute
    //Returns a int[][][], where Length is amount of parts
    //At every index == (int[2][2], where [0][0] == Starting Width of the part, [0][1] == Ending Width of the part)
    //At every index == (int[2][2], where [1][0] == Starting Height of the part, [1][1] == Ending Height of the part)
    //Visualization of one index: | int[2][2](int[2](Start Width, End Width), int[2](Start Height, End Height)) |
    //Divides the Width of the map into parts
    public static int[][][] DivideMapIntoPartsVertically(int n, int modelWidth, int modelHeight)
    {
      var parts = new int[n][][];
      for (var i = 0; i < n; ++i)
      {
        parts[i] = new int[2][];
        parts[i][0] = new int[2];
        parts[i][1] = new int[2];
      }

      for (var i = 0; i < n; ++i)
      {
        //Set all heights to proper values
        parts[i][1][0] = 0;
        parts[i][1][1] = modelHeight;
      }

      //Properly divide the width
      var remainder = modelWidth % n;
      var oneSectionLength = (modelWidth / n);
      var startValue = 0;
      
      for (var i = 0; i < n; ++i)
      {
        parts[i][0][0] = startValue;
        startValue += oneSectionLength + (i == 0 ? remainder : 0);
        parts[i][0][1] = startValue;
      }

      return parts;
    }
  }
}
