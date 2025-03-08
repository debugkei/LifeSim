using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lifegame {
  /// <summary>
  /// This class exists to represent functions, its purpose is to avoid repeating code:
  /// since there are 2 implementations for every game mode the private functions that can reuse code only available in interfaces, which dont support protected
  /// therefore I decided to bring the functions here because they aren't for Encapsulation and its the most simple design.
  /// </summary>
  static class Funcs {
    /// <summary>
    /// Gets the center of the brush
    /// </summary>
    public static Point GetBrushCenter(int x, int y, int width, int height) {
      //The formula for both of measurements: if width (or height) is even then return width (or height) / 2 - 1, else if odd then return width (or height) / 2
      return new Point((width % 2 == 0 ? width / 2 - 1 : width / 2), (height % 2 == 0 ? height / 2 - 1 : height / 2));
    }

    /// <summary>
    /// Divides the map into n different parts, so threads can all have individual part, to compute.
    ///Returns a int[][][], where Length is amount of parts.
    ///At every index == (int[2][2], where [0][0] == Starting Width of the part, [0][1] == Ending Width of the part).
    ///At every index == (int[2][2], where [1][0] == Starting Height of the part, [1][1] == Ending Height of the part).
    ///Visualization of one index: | int[2][2](int[2](Start Width, End Width), int[2](Start Height, End Height)) |.
    ///Divides the Width of the map into parts.
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int[][][] DivideGridIntoParts(int n, int width, int height) {
      var parts = new int[n][][];
      for (var i = 0; i < n; ++i) {
        parts[i] = new int[2][];
        parts[i][0] = new int[2];
        parts[i][1] = new int[2];
      }

      for (var i = 0; i < n; ++i) {
        //Set all heights to proper values
        parts[i][1][0] = 0;
        parts[i][1][1] = height;
      }

      //Properly divide the width
      var remainder = width % n;
      var oneSectionLength = (width / n);
      var startValue = 0;

      for (var i = 0; i < n; ++i) {
        parts[i][0][0] = startValue;
        startValue += oneSectionLength + (i == 0 ? remainder : 0);
        parts[i][0][1] = startValue;
      }

      return parts;
    }
  }
}
