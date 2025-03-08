using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace lifegame {
  /// <summary>
  /// The core logic of the life-game, the generations computations, computes the grid's generation with teams
  /// </summary>
  internal class TeamsRules {
    private byte _teams;
    private Random _rand;
    public TeamsRules(byte teams) {
      _teams = teams;
      _rand = new Random();
    }
    /// <summary>
    /// Calculates the next generation on the grid, on the single thread, and assigns it to the grid
    /// </summary>
    /// <param name="grid"></param>
    public void Apply(TeamsGrid grid) {
      //Temp mapdata to store the new generation and then assign to grid
      var newMapData = new byte[grid.Width, grid.Height];

      //Calculate next gen into newMapData
      for (int i = 0; i < grid.Width; ++i) {
        for (int j = 0; j < grid.Height; ++j) {
          var lifeType = grid[i, j];
          var hasLife = lifeType != 0;

          if (hasLife) {
            var friendsCount = CountFriends(grid, i, j, lifeType);
            var enemiesCount = CountEnemies(grid, i, j, lifeType);

            //Fight
            if (enemiesCount > 0) {
              var wonFight = _rand.Next(0, 2) == 0;
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
          else {
            //Reproduce
            var candidatesForReproduction = new List<int>();

            for (var k = 1; k <= _teams; ++k) {
              var neighborsCountForKType = CountFriends(grid, i, j, k);
              if (neighborsCountForKType == 3) {
                candidatesForReproduction.Add(k);
              }
            }

            if (candidatesForReproduction.Count == 0) {
              newMapData[i, j] = 0;
            }
            else {
              newMapData[i, j] = (byte)candidatesForReproduction[_rand.Next(0, candidatesForReproduction.Count)];
            }
          }
        }
      }

      //Assign the grid with new Generation to the grid
      grid.Grid = newMapData;
    }

    /// <summary>
    /// Calculates the next generation, by using multitasking, and assigns it to the grid
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="nThreads"></param>
    public void ApplyMT(TeamsGrid grid, int nThreads) {
      //Declare list of tasks, and positions that tasks are responsible for
      List<Task> tasks = new List<Task>();
      var parts = Funcs.DivideGridIntoParts(nThreads, grid.Width, grid.Height);

      //Declare new mapData
      var newMapData = new byte[grid.Width, grid.Height];

      //Push all tasks on tasks list
      for (int i = 0; i < nThreads; ++i) {
        var task_i = i;
        //Create calculating task
        var task = Task.Run(() => {
          ThreadCalculateNextGen(grid, parts[task_i][0][0], parts[task_i][0][1], parts[task_i][1][0], parts[task_i][1][1], newMapData);
        });

        //Add the created task on list
        tasks.Add(task);
      }

      //Wait for all tasks
      foreach (var i in tasks) {
        i.Wait();
      }

      //Assign new mapData back to old
      grid.Grid = newMapData;
    }

    /// <summary>
    /// Counts the friends near a cell, only if lifeType != 0
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="lifeType"></param>
    /// <returns></returns>
    private int CountFriends(TeamsGrid grid, int x, int y, int lifeType) {
      int count = 0;

      for (int i = -1; i <= 1; ++i) {
        for (int j = -1; j <= 1; ++j) {
          var x_i = (x + i + grid.Width) % grid.Width;
          var y_j = (y + j + grid.Height) % grid.Height;

          if (x_i == x && y_j == y) continue;

          if (lifeType != 0 && grid[x_i, y_j] == lifeType) {
            ++count;
          }
        }
      }
      return count;
    }
    /// <summary>
    /// Counts the enemies around a cell, only if lifeType != 0
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="lifeType"></param>
    /// <returns></returns>
    private int CountEnemies(TeamsGrid grid, int x, int y, int lifeType) {
      int count = 0;

      for (int i = -1; i <= 1; ++i) {
        for (int j = -1; j <= 1; ++j) {
          var x_i = (x + i + grid.Width) % grid.Width;
          var y_j = (y + j + grid.Height) % grid.Height;

          if (x_i == x && y_j == y) continue;

          var xyCell = grid[x_i, y_j];

          if (xyCell != 0 && xyCell != lifeType) {
            ++count;
          }
        }
      }
      return count;
    }
    /// <summary>
    /// Calculation executed on single thread, that calculates part of the map
    /// </summary>
    /// <param name="grid"></param>
    /// <param name="startWidth"></param>
    /// <param name="endWidth"></param>
    /// <param name="startHeight"></param>
    /// <param name="endHeight"></param>
    /// <param name="newMapData"></param>
    private void ThreadCalculateNextGen(TeamsGrid grid, int startWidth, int endWidth, int startHeight, int endHeight, byte[,] newMapData) {
      //Calculate next gen into newMapData
      for (int i = startWidth; i < endWidth; ++i) {
        for (int j = startHeight; j < endHeight; ++j) {
          var lifeType = grid[i, j];
          var hasLife = lifeType != 0;

          if (hasLife) {
            var friendsCount = CountFriends(grid, i, j, lifeType);
            var enemiesCount = CountEnemies(grid, i, j, lifeType);

            //Fight
            if (enemiesCount > 0) {
              var wonFight = _rand.Next(0, 2) == 0;
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
          else {
            //Reproduce
            var candidatesForReproduction = new List<int>();

            for (var k = 1; k <= _teams; ++k) {
              var neighborsCountForKType = CountFriends(grid, i, j, k);
              if (neighborsCountForKType == 3) {
                candidatesForReproduction.Add(k);
              }
            }

            if (candidatesForReproduction.Count == 0) {
              newMapData[i, j] = 0;
            }
            else {
              newMapData[i, j] = (byte)candidatesForReproduction[_rand.Next(0, candidatesForReproduction.Count)];
            }
          }
        }
      }
    }
  }
}
