using System.Collections;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Numerics;
using System.Windows.Forms;

namespace LifeSim
{
  public partial class View : Form
  {
    #region REMEMBER
    //Potential issue lays in mouse logic, the way previous cordinates are calculated, potential bug: move doesnt work properly, Potentially fixed at the end of Apply()
    //Potential issue: after resolution change, the mouse still stores the old cordinates. Potential fix: update mouse pos right after resolution change
    #endregion

    #region TODO
    //Make better GUI
    //Finish design changes
      //Finish creating IGame, its implementations, View
      //Think about Funcs
    //Code review
    //Thread manipulations
    //Create docs
    #endregion

    public View()
    {
      InitializeComponent();

      //Call Tester
      UnitTester.TestAll();

      #region Init Actions
      //Set colors && Style
      BackgroundColor = Color.Black;
      CellColor = Color.Crimson;
      MouseStepsColor = Color.Gray;

      pbMap.Image = new Bitmap(pbMap.Width, pbMap.Height);
      Graphics = Graphics.FromImage(pbMap.Image);
      Resolution = (int)nudResolution.Value;
      Offset = (int)nudOffset.Value;
      timerFPS.Interval = CalculateFPS();
      timerGPS.Interval = CalculateGPS();
      Reset();
      Presenter.EmptyMap();
      timerGPS.Start();
      timerFPS.Start();
      #endregion
    }

    #region Forms Callbacks
    //Timers
    private void timerFPS_Tick(object sender, EventArgs e)
    {
      Graphics.Clear(BackgroundColor);
      _mechanics.DrawWholeMap(Offset, PixelOffBorder, Graphics, Resolution, CellColor);
      _mechanics.DrawMouseSteps(Graphics, Resolution, PixelOffBorder, (int)nudTransparency.Value, MouseStepsColor);
      pbMap.Refresh();
    }
    private void timerGPS_Tick(object sender, EventArgs e)
    {
      Presenter.CalculateNextGen((int)nudThreads.Value);
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
      Reset();
      Presenter.RandomMap(Density);
      timerFPS.Start();
    }
    //Pause
    private void butPause_Click(object sender, EventArgs e)
    {
      if (timerGPS.Enabled)
      {
        Pause();
      }
      else
      {
        Unpause();
      }
    }
    //Empty
    private void butEmpty_Click(object sender, EventArgs e)
    {
      Presenter.EmptyMap();
    }
    //Mouse Handle
    private void pbMap_MouseMove(object sender, MouseEventArgs e)
    {
      _mechanics.HandleMouse(e, pbMap, Offset, Resolution, BrushThickness, IsRandomBrush, Density, (int)nudThreads.Value);
    }
    private void pbMap_MouseDown(object sender, MouseEventArgs e)
    {
      pbMap_MouseMove(sender, e);
    }
    private void pbMap_MouseWheel(object sender, MouseEventArgs e)
    {
      pbMap_MouseMove(sender, e);
      ChangeResolution(e.X / Resolution, e.Y / Resolution, Resolution + MathF.Sign(e.Delta));
    }
    //Resolution
    private void nudResolution_ValueChanged(object sender, EventArgs e)
    {
      ChangeResolution(pbMap.Width / Resolution / 2, pbMap.Height / Resolution / 2, (int)nudResolution.Value);
    }
    //Offset
    private void nudOffset_ValueChanged(object sender, EventArgs e)
    {
      var oldOffset = Offset;
      Offset = (int)nudOffset.Value;
      if (oldOffset > Offset)
      {
        //Decrease
        _mechanics.ResetAndInitModel(pbMap, 0, 0, oldOffset - Offset, oldOffset - Offset, Offset, Resolution, (int)nudThreads.Value);
      }
      else
      {
        //Increase
        _mechanics.ResetAndInitModel(pbMap, Offset - oldOffset, Offset - oldOffset, 0, 0, Offset, Resolution, (int)nudThreads.Value);
      }
    }
    //Support for Resizing
    private void Form1_Resize(object sender, EventArgs e)
    {
      pbMap.Width = scMap.Panel2.Width;
      pbMap.Height = scMap.Panel2.Height;

      pbMap.Image = new Bitmap(scMap.Panel2.Width, scMap.Panel2.Height);

      Graphics = Graphics.FromImage(pbMap.Image);
      _mechanics.ResetAndInitModel(pbMap, Offset, Resolution);
      timerFPS_Tick(sender, e);
    }
    //Color buttons
    private void butBackColor_Click(object sender, EventArgs e) {
      BackgroundColor = RequestColor();
    }
    private void butCellColor_Click(object sender, EventArgs e) {
      CellColor = RequestColor();
    }
    private void butMouseStepsColor_Click(object sender, EventArgs e) {
      MouseStepsColor = RequestColor();
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
      butStart_Click(sender, e);
    }
    private void nudTeams_ValueChanged(object sender, EventArgs e) {
      if (cbTeams.Checked) {
        butStart_Click(sender, e);
      }
      nudBrushTeam.Maximum = nudTeams.Value;
    }
    #endregion

    #region Methods
    private void Reset()
    {
      //Set main components
      if (cbTeams.Checked)
      {
        var rand = new Random();
        var teams = (int)nudTeams.Value;
        var colors = new Color[teams];

        for (var i = 0; i < teams; ++i)
        {
          colors[i] = Color.FromArgb(255, rand.Next(256), rand.Next(256), rand.Next(256));
        }

        Presenter = new CPUByteMTPresenter2D(teams);
        _mechanics = new GUICPUByteMTMechanics2D(this, Presenter as CPUByteMTPresenter2D, colors);
      }
      else
      {
        Presenter = new CPUBitMTPresenter2D();
        _mechanics = new GUICPUBitMTMechanics2D(this, Presenter as CPUBitMTPresenter2D);
      }
      //ResetModelValues
      _mechanics.ResetModel(pbMap, Offset, Resolution);
      Graphics.Clear(BackgroundColor);
    }
    private void Pause()
    {
      timerGPS.Stop();
    }
    private void Unpause()
    {
      timerGPS.Start();
    }
    #endregion


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
