using System.Collections;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Numerics;
using System.Windows.Forms;

namespace lifegame
{
  public partial class View : Form
  {
    #region Issues
    //Potential issue lays in mouse logic, the way previous cordinates are calculated, potential bug: move doesnt work properly, Potentially fixed at the end of Apply()
    //Potential issue: after resolution change, the mouse still stores the old cordinates. Potential fix: update mouse pos right after resolution change
    //The mouse handling uses X and Y of the grid, which has an offset
    #endregion

    #region TODO
    //Make better GUI
    //Make all TODOs
    //Think about Funcs
    //Code review
    //Thread manipulations
    //Create docs
    #endregion

    private IGame _game;
    public int Resolution { get => (int)nudResolution.Value; private set { nudResolution.Value = value; } }
    public int MaximumResolution { get => (int)nudResolution.Maximum; }
    public int MinimumResolution { get => (int)nudResolution.Minimum; }
    public Image Image { get => PBBox.Image; set { PBBox.Image = value; } }
    public int GridWidth { get => PBBox.Width; set { PBBox.Width = value; } }
    public int GridHeight { get => PBBox.Height; set { PBBox.Width = value; } }

    public View()
    {
      InitializeComponent();

      _game = new ClassicGame(
        this, Color.Crimson, (int)nudOffset.Value, (int)nudResolution.Value, cbPixelOffBorder.Checked, Color.Black,
        PBBox.Width / (int)nudResolution.Value, PBBox.Height / (int)nudResolution.Value, (byte)nudTransparency.Value, Color.Gray,
        (int)nudBrushThickness.Value, (int)nudBrushThickness.Value, 0, 0
      );

      timerFPS.Interval = CalculateFPS();
      timerGPS.Interval = CalculateGPS();
      timerGPS.Start();
      timerFPS.Start();

      _game.Fill();
    }

    //Timers
    private void timerFPS_Tick(object sender, EventArgs e)
    {
      _game.Update();
    }
    private void timerGPS_Tick(object sender, EventArgs e)
    {
      _game.FixedUpdate();
    }
    //Intervals
    private void nudFPS_ValueChanged(object sender, EventArgs e)
    {
      timerFPS.Interval = CalculateFPS();
    }
    private void nudGPS_ValueChanged(object sender, EventArgs e)
    {
      timerGPS.Interval = CalculateGPS();
    }
    private int CalculateFPS()
    {
      return 1000 / (int)nudFPS.Value;
    }
    private int CalculateGPS()
    {
      return 1000 / (int)nudGPS.Value;
    }
    //Start
    private void butStart_Click(object sender, EventArgs e)
    {
      _game.Fill();
    }
    //Pause
    private void butPause_Click(object sender, EventArgs e)
    {
      if (timerGPS.Enabled)
      {
        timerGPS.Stop();
      }
      else
      {
        timerGPS.Start();
      }
    }
    //Empty
    private void butEmpty_Click(object sender, EventArgs e)
    {
      _game.Empty();
    }
    //Mouse Handle
    private void pbMap_MouseMove(object sender, MouseEventArgs e)
    {
      _game.MouseMove(e.X / (int)nudResolution.Value, e.Y / (int)nudResolution.Value);
    }
    private void pbMap_MouseDown(object sender, MouseEventArgs e)
    {
      switch (e.Button) {
        case MouseButtons.Left:
          _game.MouseDown(MouseButtonType.Left);
          break;
        case MouseButtons.Right:
          _game.MouseDown(MouseButtonType.Right);
          break;
        case MouseButtons.Middle:
          _game.MouseDown(MouseButtonType.Middle);
          break;
        default:
          break;
      }
    }
    private void pbMap_MouseWheel(object sender, MouseEventArgs e)
    {
      _game.MouseWheel(e.Delta);
    }
    //Resolution
    private void nudResolution_ValueChanged(object sender, EventArgs e)
    {
      //TODO: wait for GUI chantes and then configure resoution
    }
    //Offset
    private void nudOffset_ValueChanged(object sender, EventArgs e)
    {
      _game.Offset = (int)nudOffset.Value;
    }
    //Support for Resizing
    private void Form1_Resize(object sender, EventArgs e)
    {
      //TODO: implement resizing
      //pbMap.Width = scMap.Panel2.Width;
      //pbMap.Height = scMap.Panel2.Height;

      //pbMap.Image = new Bitmap(scMap.Panel2.Width, scMap.Panel2.Height);

      //Graphics = Graphics.FromImage(pbMap.Image);
      //_mechanics.ResetAndInitModel(pbMap, Offset, Resolution);
      //timerFPS_Tick(sender, e);
    }
    //Color buttons
    private void butBackColor_Click(object sender, EventArgs e) {
      _game.BackgroundColor = RequestColor();
    }
    private void butCellColor_Click(object sender, EventArgs e) {
      if (_game is ClassicGame gameClassic) {
        gameClassic.CellColor = RequestColor();
      }
      else if (_game is TeamsGame gameTeams) {
        //TODO: implement game teams support for changing colors
      }
    }
    private void butMouseStepsColor_Click(object sender, EventArgs e) {
      _game.MouseShadeColor = RequestColor();
    }
    private Color RequestColor() {
      var cd = new ColorDialog();
      cd.AnyColor = true;
      cd.FullOpen = true;
      if (cd.ShowDialog() == DialogResult.OK) {
        return cd.Color;
      }
      return Color.Black;
    }
    //Restart map when teams mode is selected && changed
    private void cbTeams_CheckedChanged(object sender, EventArgs e) {
      if (_game is ClassicGame) {
        _game = new ClassicGame(
        this, Color.Crimson, (int)nudOffset.Value, (int)nudResolution.Value, cbPixelOffBorder.Checked, Color.Black,
        PBBox.Width / (int)nudResolution.Value, PBBox.Height / (int)nudResolution.Value, (byte)nudTransparency.Value, Color.Gray,
        (int)nudBrushThickness.Value, (int)nudBrushThickness.Value, 0, 0
      );
      }
      else if (_game is TeamsGame) {
        //TODO: add support for changing teams
        //_game = new TeamsGame(
        //  this, Color.Crimson, (int)nudOffset.Value, (int)nudResolution.Value, cbPixelOffBorder.Checked, Color.Black,
        //  pbMap.Width / (int)nudResolution.Value, pbMap.Height / (int)nudResolution.Value, (byte)nudTransparency.Value, Color.Gray,
        //  (int)nudBrushThickness.Value, (int)nudBrushThickness.Value, 0, 0, 0
        //);
      }
    }
    private void nudTeams_ValueChanged(object sender, EventArgs e) {
      if (_game is TeamsGame game) {
        //TODO: support for changing amount of teams wihtout allocating new TeamsGame
      }
      nudBrushTeam.Maximum = nudTeams.Value;
    }

    ///<summary>
    /// Changes the resolution including the x and y centers.
    /// This method isnt responsible for validation of xCenter and yCenter.
    /// But indeed responsible for delta.
    /// </summary>
    public void ChangeResolution(IInitResetable grid, int delta, int xCenter, int yCenter) {
      //Whether resolution is in limits
      if (Resolution + delta >= MinimumResolution && Resolution + delta < MaximumResolution) {
        //Cache old resolution and init new
        var oldResolution = Resolution;
        Resolution += delta;

        //Calculate the new grid width and height
        var newWidth = PBBox.Width / Resolution;
        var oldWidth = PBBox.Width / oldResolution;
        var newHeight = PBBox.Height / Resolution;
        var oldHeight = PBBox.Height / oldResolution;

        //downscale, map zoomed out
        if (oldResolution > Resolution) {
          //Inits the new map with old values at specific offset, offset here includes the center
          grid.InitReset(newWidth, newHeight, (int)((newWidth - oldWidth) * xCenter / oldWidth), (int)((newHeight - oldHeight) * yCenter / oldHeight));
        }
        //upscale, map zoomed in
        else if (Resolution > oldResolution) {
          //Inits the new map with old values at specific offset, offset here includes the center
          grid.InitReset(newWidth, newHeight, -(int)((oldWidth - newWidth) * xCenter / oldWidth), -(int)((oldHeight - newHeight) * yCenter / oldHeight));
        }
      }
    }
  }
}
