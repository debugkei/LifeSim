using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeSim {
  //DTO == Data Transfer Object, object which is responsible for containing and/or transferring data

  /// <summary>
  /// DTO for renderer, mouse logic, and dispatcher, so those objects can pass the data to eachother without interception with interfaces.
  /// This also allows to get rid of repetition of code since the program has multiple implementations, yet it adds some difficulty in understanding the architecture.
  /// </summary>
  internal class RMDDTO {
    //Init
    public RMDDTO(PictureBox pbBox, Color backgroundColor, object cellColor, int offset, Color mouseStepsColor, byte mouseStepsTransparency, bool pixelOffBorder,
      int resolution, int minimumResolution, int maximumResolution) {
      BackgroundColor = backgroundColor;
      CellColor = cellColor;
      Offset = offset;
      MouseStepsColor = mouseStepsColor;
      MouseStepsTransparency = mouseStepsTransparency;
      PixelOffBorder = pixelOffBorder;
      Resolution = resolution;
      PBBox = pbBox;
      MinimumResolution = minimumResolution;
      MaximumResolution = maximumResolution;
    }

    public PictureBox PBBox { get; init; }
    public int MinimumResolution { get; init; }
    public int MaximumResolution { get; init; }

    /// <summary>
    /// Color of the background on which cells are rendered
    /// </summary>
    public Color BackgroundColor { get; set; }

    /// <summary>
    /// Color of cells on the background
    /// </summary>
    public object CellColor { get => CellColor; set { CellColor = value; } }
    public event Action OnCellColorChanged = delegate { };

    /// <summary>
    /// Offset at which the grid is rendered
    /// </summary>
    public int Offset { get; set; }
    /// <summary>
    /// Color of mouse steps, color below the mouse cursor
    /// </summary>
    public Color MouseStepsColor { get => MouseStepsColor; set { MouseStepsColor = value; MouseStepsBrush = GetMouseStepsBrush(); } }

    /// <summary>
    /// Transparency of color of mouse steps, color below the mouse cursor, 0-255, where 0 is invisible and 255 is fully visible
    /// </summary>
    public byte MouseStepsTransparency { get => MouseStepsTransparency; set { MouseStepsTransparency = value; MouseStepsBrush = GetMouseStepsBrush(); } }

    /// <summary>
    /// Should the cells have a little gap between them
    /// </summary>
    public bool PixelOffBorder { get => PixelOffBorder; set { PixelOffBorder = value; CellWidth = GetCellWidth(); } }

    /// <summary>
    /// Resolution of the map
    /// </summary>
    public int Resolution { get => Resolution; set { Resolution = value; CellWidth = GetCellWidth(); } }

    /// <summary>
    /// The mouse steps rectangles, mouse logic sets them, and renderer uses them
    /// </summary>
    public Rectangle?[,] MouseStepsRects { get; set; }

    /// <summary>
    /// Calculates the width of a single cell
    /// </summary>
    /// <returns></returns>
    private int GetCellWidth() {
      return Resolution - (PixelOffBorder ? 1 : 0);
    }

    ///<summary>
    /// Gets the width of a single cell
    /// </summary>
    public int CellWidth { get; set; }

    /// <summary>
    /// The brush for mouse steps
    /// </summary>
    public SolidBrush MouseStepsBrush { get; set; }

    /// <summary>
    /// Gets the mouse steps brush
    /// </summary>
    private SolidBrush GetMouseStepsBrush() {
      return new SolidBrush(Color.FromArgb(MouseStepsTransparency, MouseStepsColor));
    }
  }
}
