using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace LifeSim
{
  internal class CPUByteMTPresenter2D : ICPUMTPresenter2D
  {
    public ByteModel2D Model { get; private set; }
    private int _teams;
    public CPUByteMTPresenter2D(int teams)
    {
      Model = new();
      _teams = teams;
    }
    public void EmptyMap()
    {
      for (var i = 0; i < Model.Width; ++i)
      {
        for (var j = 0; j < Model.Height; ++j)
        {
          Model.MapData[i,j] = 0;
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
          Model.MapData[i,j] = (byte)(rand.Next(-1, density) == 0 ? rand.Next(1, _teams + 1) : 0);
        }
      }
    }
    //Multi threaded version
    public void CalculateNextGen(int nThreads)
    {
      //Declare list of tasks, and positions that tasks are responsible for
      List<Task> tasks = new List<Task>();
      var parts = DivideMapIntoPartsVertically(nThreads, Model);

      //Declare new mapData
      var newMapData = new byte[Model.Width, Model.Height];

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
    private int CountFriends(int x, int y, int lifeType)
    {
      int count = 0;

      for (int i = -1; i <= 1; ++i)
      {
        for (int j = -1; j <= 1; ++j)
        {
          var _x = (x + i + Model.Width) % Model.Width;
          var _y = (y + j + Model.Height) % Model.Height;

          if (_x == x && _y == y) continue;

          if (lifeType != 0 && Model.MapData[_x, _y] == lifeType)
          {
            ++count;
          }
        }
      }
      return count;
    }
    private int CountEnemies(int x, int y, int lifeType)
    {
      int count = 0;

      for (int i = -1; i <= 1; ++i)
      {
        for (int j = -1; j <= 1; ++j)
        {
          var _x = (x + i + Model.Width) % Model.Width;
          var _y = (y + j + Model.Height) % Model.Height;

          if (_x == x && _y == y) continue;

          if (Model.MapData[_x, _y] != 0 && Model.MapData[_x, _y] != lifeType)
          {
            ++count;
          }
        }
      }
      return count;
    }
    //Calculation executed on single thread, that calculates part of the map
    private void ThreadCalculateNextGen(int startWidth, int endWidth, int startHeight, int endHeight, byte[,] newMapData)
    {
      var rand = new Random();
      //Calculate next gen into newMapData
      for (int i = startWidth; i < endWidth; ++i)
      {
        for (int j = startHeight; j < endHeight; ++j)
        {
          var lifeType = Model.MapData[i,j];
          var hasLife = lifeType != 0;

          if (hasLife)
          {
            var friendsCount = CountFriends(i, j, lifeType);
            var enemiesCount = CountEnemies(i, j, lifeType);

            //Fight
            if (enemiesCount > 0) {
              var wonFight = rand.Next(0, 2) == 0;
              newMapData[i, j] = (byte)(wonFight ? lifeType : 0);
            }
            else if (friendsCount > 1 && friendsCount < 4) {
              //Survival
              newMapData[i, j] = lifeType;
            }
            else {
              newMapData[i, j] = 0;
            }
          }
          else
          {
            //Reproduce
            var candidatesForReproduction = new List<int>();
            
            for (var k = 1; k <= _teams; ++k)
            {
              var neighborsCountForKType = CountFriends(i, j, k);
              if (neighborsCountForKType == 3)
              {
                candidatesForReproduction.Add(k);
              }
            }

            if (candidatesForReproduction.Count == 0)
            {
              newMapData[i, j] = 0;
            }
            else {
              newMapData[i, j] = (byte)candidatesForReproduction[rand.Next(0, candidatesForReproduction.Count)];
            }
          }
        }
      }
    }

    //Divides the map into i different parts, so threads can all have individual part, to compute
    //Returns a int[][][], where Length is amount of parts
    //At every index == (int[2][2], where [0][0] == Starting Width of the part, [0][1] == Ending Width of the part)
    //At every index == (int[2][2], where [1][0] == Starting Height of the part, [1][1] == Ending Height of the part)
    //Visualization of one index: | int[2][2](int[2](Start Width, End Width), int[2](Start Height, End Height)) |
    //Divides the Width of the map into parts
    public static int[][][] DivideMapIntoPartsVertically(int n, ByteModel2D model)
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
        parts[i][1][1] = model.Height;
      }

      //Properly divide the width
      var remainder = model.Width % n;
      var oneSectionLength = (model.Width / n);
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
