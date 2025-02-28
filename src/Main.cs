using System.Collections;
using System.Diagnostics;
using System.DirectoryServices.ActiveDirectory;
using System.Numerics;
using System.Windows.Forms;

namespace LifeSim
{
  /// <summary>
  /// This class is the center of the program.
  ///The whole purpose of it, is to dispatch operations, when it recieves them.
  /// </summary>
  public partial class Main : Form
  {
    #region REMEMBER
    //Potential issue lays in mouse logic, the way previous cordinates are calculated, potential bug: move doesnt work properly, Potentially fixed at the end of Apply()
    //Potential issue: after resoution change, the mouse still stores the old cordinates. Potential fix: update mouse pos right after resolution change
    #endregion

    #region TODO
    //Make better GUI
    //Finish design changes
    //Code review after uploading to github
    //Bring thread manipulations to DTO (Think about MT)
    //Funcs & DTO arent intuitive
    //Change methods' names in interfaces, instead of handle maybe be more straightforward
    //Create docs
    #endregion

    #region COMMITS
    //Finished the mouse logic, and change resolution dto.
    //To finish design changes:
      //Finish grid InitReset()
      //Create TeamsClassicMouseLogic
      //Remake dispatcher and delete all old stuff
    #endregion

    #region Fields
    private Graphics Graphics { get; set; }
    private Color BackgroundColor { get; set; }
    private Color CellColor { get; set; }
    private Color MouseStepsColor { get; set; }
    private bool PixelOffBorder { get => cbPixelOffBorder.Checked; }
    private bool IsRandomBrush { get => cbRandomBrush.Checked; }
    private bool _isChangingResolution;
    private int Resolution { get; set; }
    private int Offset { get; set; }
    private int Density { get => (int)nudDensity.Value; }
    private int BrushThickness { get => (int)nudBrushThickness.Value; }
    public int BrushTeam { get => (int)nudBrushTeam.Value; }
    private ICPUMTPresenter2D Presenter { get; set; }
    private IGUICPUMTMechanics2D _mechanics;
    #endregion

    //View
    public Main()
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
    private void ChangeResolution(int x, int y, int value)
    {
      if (!_isChangingResolution)
      {
        _isChangingResolution = true;
        if (x > 0 && y > 0 && x < pbMap.Width / Resolution && y < pbMap.Height / Resolution && value >= nudResolution.Minimum && value <= nudResolution.Maximum)
        {
          var oldResolution = Resolution;
          Resolution = value;
          nudResolution.Value = value;
          if (oldResolution > Resolution)
          {
            //downscale, map zoomed out
            var newWidth = pbMap.Width / Resolution;
            var oldWidth = pbMap.Width / oldResolution;
            var newHeight = pbMap.Height / Resolution;
            var oldHeight = pbMap.Height / oldResolution;
            _mechanics.ResetAndInitModel(pbMap,
              (int)((newWidth - oldWidth) * x / oldWidth),
              (int)((newHeight - oldHeight) * y / oldHeight),
              0, 0, Offset, Resolution, (int)nudThreads.Value);
            _mechanics.UpdatePreviousMousePos((newWidth - oldWidth) / 2, (newHeight - oldHeight) / 2);
          }
          else
          {
            //upscale, map zoomed in
            var newWidth = pbMap.Width / Resolution;
            var oldWidth = pbMap.Width / oldResolution;
            var newHeight = pbMap.Height / Resolution;
            var oldHeight = pbMap.Height / oldResolution;
            _mechanics.ResetAndInitModel(pbMap, 0, 0,
              (int)((oldWidth - newWidth) * x / oldWidth),
              (int)((oldHeight - newHeight) * y / oldHeight),
              Offset, Resolution, (int)nudThreads.Value);
            _mechanics.UpdatePreviousMousePos(-(oldWidth - newWidth) / 2, -(oldHeight - newHeight) / 2);
          }
        }
        _isChangingResolution = false;
      }
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

  }
}
