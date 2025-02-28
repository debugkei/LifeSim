namespace LifeSim
{
  partial class Main
  {
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
      scMap = new SplitContainer();
      splitContainer2 = new SplitContainer();
      nudBrushTeam = new NumericUpDown();
      nudTeams = new NumericUpDown();
      cbTeams = new CheckBox();
      butMouseStepsColor = new Button();
      butCellColor = new Button();
      butBackColor = new Button();
      label8 = new Label();
      nudTransparency = new NumericUpDown();
      label7 = new Label();
      nudThreads = new NumericUpDown();
      label3 = new Label();
      nudGPS = new NumericUpDown();
      label6 = new Label();
      nudFPS = new NumericUpDown();
      label5 = new Label();
      nudOffset = new NumericUpDown();
      cbPixelOffBorder = new CheckBox();
      cbRandomBrush = new CheckBox();
      label4 = new Label();
      nudBrushThickness = new NumericUpDown();
      label2 = new Label();
      nudDensity = new NumericUpDown();
      label1 = new Label();
      nudResolution = new NumericUpDown();
      butEmpty = new Button();
      butPause = new Button();
      butStart = new Button();
      pbMap = new PictureBox();
      timerFPS = new System.Windows.Forms.Timer(components);
      timerGPS = new System.Windows.Forms.Timer(components);
      ((System.ComponentModel.ISupportInitialize)scMap).BeginInit();
      scMap.Panel1.SuspendLayout();
      scMap.Panel2.SuspendLayout();
      scMap.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
      splitContainer2.Panel1.SuspendLayout();
      splitContainer2.Panel2.SuspendLayout();
      splitContainer2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)nudBrushTeam).BeginInit();
      ((System.ComponentModel.ISupportInitialize)nudTeams).BeginInit();
      ((System.ComponentModel.ISupportInitialize)nudTransparency).BeginInit();
      ((System.ComponentModel.ISupportInitialize)nudThreads).BeginInit();
      ((System.ComponentModel.ISupportInitialize)nudGPS).BeginInit();
      ((System.ComponentModel.ISupportInitialize)nudFPS).BeginInit();
      ((System.ComponentModel.ISupportInitialize)nudOffset).BeginInit();
      ((System.ComponentModel.ISupportInitialize)nudBrushThickness).BeginInit();
      ((System.ComponentModel.ISupportInitialize)nudDensity).BeginInit();
      ((System.ComponentModel.ISupportInitialize)nudResolution).BeginInit();
      ((System.ComponentModel.ISupportInitialize)pbMap).BeginInit();
      SuspendLayout();
      // 
      // scMap
      // 
      scMap.BackColor = SystemColors.ButtonShadow;
      scMap.BorderStyle = BorderStyle.FixedSingle;
      scMap.Dock = DockStyle.Fill;
      scMap.FixedPanel = FixedPanel.Panel1;
      scMap.IsSplitterFixed = true;
      scMap.Location = new Point(0, 0);
      scMap.Name = "scMap";
      // 
      // scMap.Panel1
      // 
      scMap.Panel1.Controls.Add(splitContainer2);
      // 
      // scMap.Panel2
      // 
      scMap.Panel2.Controls.Add(pbMap);
      scMap.Size = new Size(1146, 617);
      scMap.SplitterDistance = 167;
      scMap.TabIndex = 0;
      // 
      // splitContainer2
      // 
      splitContainer2.BorderStyle = BorderStyle.FixedSingle;
      splitContainer2.Dock = DockStyle.Fill;
      splitContainer2.FixedPanel = FixedPanel.Panel1;
      splitContainer2.IsSplitterFixed = true;
      splitContainer2.Location = new Point(0, 0);
      splitContainer2.Name = "splitContainer2";
      splitContainer2.Orientation = Orientation.Horizontal;
      // 
      // splitContainer2.Panel1
      // 
      splitContainer2.Panel1.BackColor = SystemColors.ControlDarkDark;
      splitContainer2.Panel1.Controls.Add(nudBrushTeam);
      splitContainer2.Panel1.Controls.Add(nudTeams);
      splitContainer2.Panel1.Controls.Add(cbTeams);
      splitContainer2.Panel1.Controls.Add(butMouseStepsColor);
      splitContainer2.Panel1.Controls.Add(butCellColor);
      splitContainer2.Panel1.Controls.Add(butBackColor);
      splitContainer2.Panel1.Controls.Add(label8);
      splitContainer2.Panel1.Controls.Add(nudTransparency);
      splitContainer2.Panel1.Controls.Add(label7);
      splitContainer2.Panel1.Controls.Add(nudThreads);
      splitContainer2.Panel1.Controls.Add(label3);
      splitContainer2.Panel1.Controls.Add(nudGPS);
      splitContainer2.Panel1.Controls.Add(label6);
      splitContainer2.Panel1.Controls.Add(nudFPS);
      splitContainer2.Panel1.Controls.Add(label5);
      splitContainer2.Panel1.Controls.Add(nudOffset);
      splitContainer2.Panel1.Controls.Add(cbPixelOffBorder);
      splitContainer2.Panel1.Controls.Add(cbRandomBrush);
      splitContainer2.Panel1.Controls.Add(label4);
      splitContainer2.Panel1.Controls.Add(nudBrushThickness);
      splitContainer2.Panel1.Controls.Add(label2);
      splitContainer2.Panel1.Controls.Add(nudDensity);
      splitContainer2.Panel1.Controls.Add(label1);
      splitContainer2.Panel1.Controls.Add(nudResolution);
      // 
      // splitContainer2.Panel2
      // 
      splitContainer2.Panel2.BackColor = SystemColors.ControlDark;
      splitContainer2.Panel2.Controls.Add(butEmpty);
      splitContainer2.Panel2.Controls.Add(butPause);
      splitContainer2.Panel2.Controls.Add(butStart);
      splitContainer2.Size = new Size(167, 617);
      splitContainer2.SplitterDistance = 489;
      splitContainer2.TabIndex = 0;
      // 
      // nudBrushTeam
      // 
      nudBrushTeam.BackColor = SystemColors.ScrollBar;
      nudBrushTeam.BorderStyle = BorderStyle.None;
      nudBrushTeam.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
      nudBrushTeam.Location = new Point(116, 464);
      nudBrushTeam.Maximum = new decimal(new int[] { 3, 0, 0, 0 });
      nudBrushTeam.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
      nudBrushTeam.Name = "nudBrushTeam";
      nudBrushTeam.Size = new Size(39, 19);
      nudBrushTeam.TabIndex = 28;
      nudBrushTeam.TextAlign = HorizontalAlignment.Center;
      nudBrushTeam.Value = new decimal(new int[] { 3, 0, 0, 0 });
      // 
      // nudTeams
      // 
      nudTeams.BackColor = SystemColors.ScrollBar;
      nudTeams.BorderStyle = BorderStyle.None;
      nudTeams.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
      nudTeams.Location = new Point(71, 464);
      nudTeams.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
      nudTeams.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
      nudTeams.Name = "nudTeams";
      nudTeams.Size = new Size(39, 19);
      nudTeams.TabIndex = 27;
      nudTeams.TextAlign = HorizontalAlignment.Center;
      nudTeams.Value = new decimal(new int[] { 3, 0, 0, 0 });
      nudTeams.ValueChanged += nudTeams_ValueChanged;
      // 
      // cbTeams
      // 
      cbTeams.AutoSize = true;
      cbTeams.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline);
      cbTeams.Location = new Point(4, 462);
      cbTeams.Name = "cbTeams";
      cbTeams.Size = new Size(61, 19);
      cbTeams.TabIndex = 26;
      cbTeams.Text = "Teams";
      cbTeams.UseVisualStyleBackColor = true;
      cbTeams.CheckedChanged += cbTeams_CheckedChanged;
      // 
      // butMouseStepsColor
      // 
      butMouseStepsColor.BackColor = SystemColors.ControlDark;
      butMouseStepsColor.Cursor = Cursors.Hand;
      butMouseStepsColor.FlatAppearance.BorderColor = Color.Black;
      butMouseStepsColor.FlatAppearance.BorderSize = 2;
      butMouseStepsColor.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline);
      butMouseStepsColor.Location = new Point(2, 395);
      butMouseStepsColor.Name = "butMouseStepsColor";
      butMouseStepsColor.Size = new Size(159, 24);
      butMouseStepsColor.TabIndex = 25;
      butMouseStepsColor.Text = "Mouse Steps Color";
      butMouseStepsColor.UseVisualStyleBackColor = false;
      butMouseStepsColor.Click += butMouseStepsColor_Click;
      // 
      // butCellColor
      // 
      butCellColor.BackColor = SystemColors.ControlDark;
      butCellColor.Cursor = Cursors.Hand;
      butCellColor.FlatAppearance.BorderColor = Color.Black;
      butCellColor.FlatAppearance.BorderSize = 2;
      butCellColor.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline);
      butCellColor.Location = new Point(2, 363);
      butCellColor.Name = "butCellColor";
      butCellColor.Size = new Size(159, 26);
      butCellColor.TabIndex = 24;
      butCellColor.Text = "Cell Color";
      butCellColor.UseVisualStyleBackColor = false;
      butCellColor.Click += butCellColor_Click;
      // 
      // butBackColor
      // 
      butBackColor.BackColor = SystemColors.ControlDark;
      butBackColor.Cursor = Cursors.Hand;
      butBackColor.FlatAppearance.BorderColor = Color.Black;
      butBackColor.FlatAppearance.BorderSize = 2;
      butBackColor.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline);
      butBackColor.Location = new Point(2, 331);
      butBackColor.Name = "butBackColor";
      butBackColor.Size = new Size(159, 26);
      butBackColor.TabIndex = 11;
      butBackColor.Text = "Background Color";
      butBackColor.UseVisualStyleBackColor = false;
      butBackColor.Click += butBackColor_Click;
      // 
      // label8
      // 
      label8.AutoSize = true;
      label8.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline);
      label8.Location = new Point(4, 287);
      label8.Name = "label8";
      label8.Size = new Size(154, 15);
      label8.TabIndex = 23;
      label8.Text = "Mouse Steps Transparency";
      // 
      // nudTransparency
      // 
      nudTransparency.BackColor = SystemColors.ScrollBar;
      nudTransparency.BorderStyle = BorderStyle.None;
      nudTransparency.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
      nudTransparency.Increment = new decimal(new int[] { 50, 0, 0, 0 });
      nudTransparency.Location = new Point(2, 306);
      nudTransparency.Maximum = new decimal(new int[] { 255, 0, 0, 0 });
      nudTransparency.Name = "nudTransparency";
      nudTransparency.Size = new Size(159, 19);
      nudTransparency.TabIndex = 22;
      nudTransparency.Value = new decimal(new int[] { 50, 0, 0, 0 });
      // 
      // label7
      // 
      label7.AutoSize = true;
      label7.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline);
      label7.Location = new Point(43, 245);
      label7.Name = "label7";
      label7.Size = new Size(74, 15);
      label7.TabIndex = 21;
      label7.Text = "CPU Priority";
      // 
      // nudThreads
      // 
      nudThreads.BackColor = SystemColors.ScrollBar;
      nudThreads.BorderStyle = BorderStyle.None;
      nudThreads.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
      nudThreads.Location = new Point(3, 263);
      nudThreads.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
      nudThreads.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
      nudThreads.Name = "nudThreads";
      nudThreads.Size = new Size(159, 19);
      nudThreads.TabIndex = 20;
      nudThreads.Value = new decimal(new int[] { 8, 0, 0, 0 });
      // 
      // label3
      // 
      label3.AutoSize = true;
      label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline);
      label3.Location = new Point(11, 207);
      label3.Name = "label3";
      label3.Size = new Size(141, 15);
      label3.TabIndex = 19;
      label3.Text = "Generations Per Second";
      // 
      // nudGPS
      // 
      nudGPS.BackColor = SystemColors.ScrollBar;
      nudGPS.BorderStyle = BorderStyle.None;
      nudGPS.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
      nudGPS.Increment = new decimal(new int[] { 20, 0, 0, 0 });
      nudGPS.Location = new Point(3, 225);
      nudGPS.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
      nudGPS.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
      nudGPS.Name = "nudGPS";
      nudGPS.Size = new Size(159, 19);
      nudGPS.TabIndex = 18;
      nudGPS.Value = new decimal(new int[] { 15, 0, 0, 0 });
      nudGPS.ValueChanged += nudGPS_ValueChanged;
      // 
      // label6
      // 
      label6.AutoSize = true;
      label6.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline);
      label6.Location = new Point(25, 168);
      label6.Name = "label6";
      label6.Size = new Size(113, 15);
      label6.TabIndex = 17;
      label6.Text = "Frames Per Second";
      // 
      // nudFPS
      // 
      nudFPS.BackColor = SystemColors.ScrollBar;
      nudFPS.BorderStyle = BorderStyle.None;
      nudFPS.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
      nudFPS.Increment = new decimal(new int[] { 20, 0, 0, 0 });
      nudFPS.Location = new Point(4, 185);
      nudFPS.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
      nudFPS.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
      nudFPS.Name = "nudFPS";
      nudFPS.Size = new Size(159, 19);
      nudFPS.TabIndex = 16;
      nudFPS.Value = new decimal(new int[] { 40, 0, 0, 0 });
      nudFPS.ValueChanged += nudFPS_ValueChanged;
      // 
      // label5
      // 
      label5.AutoSize = true;
      label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline);
      label5.Location = new Point(12, 125);
      label5.Name = "label5";
      label5.Size = new Size(146, 15);
      label5.TabIndex = 15;
      label5.Text = "Map Cells Beyond Screen";
      // 
      // nudOffset
      // 
      nudOffset.BackColor = SystemColors.ScrollBar;
      nudOffset.BorderStyle = BorderStyle.None;
      nudOffset.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
      nudOffset.Increment = new decimal(new int[] { 50, 0, 0, 0 });
      nudOffset.Location = new Point(4, 143);
      nudOffset.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
      nudOffset.Name = "nudOffset";
      nudOffset.Size = new Size(159, 19);
      nudOffset.TabIndex = 14;
      nudOffset.Value = new decimal(new int[] { 100, 0, 0, 0 });
      nudOffset.ValueChanged += nudOffset_ValueChanged;
      // 
      // cbPixelOffBorder
      // 
      cbPixelOffBorder.AutoSize = true;
      cbPixelOffBorder.Checked = true;
      cbPixelOffBorder.CheckState = CheckState.Checked;
      cbPixelOffBorder.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline);
      cbPixelOffBorder.Location = new Point(3, 425);
      cbPixelOffBorder.Name = "cbPixelOffBorder";
      cbPixelOffBorder.Size = new Size(117, 19);
      cbPixelOffBorder.TabIndex = 13;
      cbPixelOffBorder.Text = "Pixel Off Border";
      cbPixelOffBorder.UseVisualStyleBackColor = true;
      // 
      // cbRandomBrush
      // 
      cbRandomBrush.AutoSize = true;
      cbRandomBrush.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline);
      cbRandomBrush.Location = new Point(3, 442);
      cbRandomBrush.Name = "cbRandomBrush";
      cbRandomBrush.Size = new Size(107, 19);
      cbRandomBrush.TabIndex = 12;
      cbRandomBrush.Text = "Random Brush";
      cbRandomBrush.UseVisualStyleBackColor = true;
      // 
      // label4
      // 
      label4.AutoSize = true;
      label4.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline);
      label4.Location = new Point(33, 85);
      label4.Name = "label4";
      label4.Size = new Size(96, 15);
      label4.TabIndex = 11;
      label4.Text = "Brush Thickness";
      // 
      // nudBrushThickness
      // 
      nudBrushThickness.BackColor = SystemColors.ScrollBar;
      nudBrushThickness.BorderStyle = BorderStyle.None;
      nudBrushThickness.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
      nudBrushThickness.Increment = new decimal(new int[] { 10, 0, 0, 0 });
      nudBrushThickness.Location = new Point(3, 103);
      nudBrushThickness.Maximum = new decimal(new int[] { 300, 0, 0, 0 });
      nudBrushThickness.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
      nudBrushThickness.Name = "nudBrushThickness";
      nudBrushThickness.Size = new Size(159, 19);
      nudBrushThickness.TabIndex = 10;
      nudBrushThickness.Value = new decimal(new int[] { 5, 0, 0, 0 });
      // 
      // label2
      // 
      label2.AutoSize = true;
      label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline);
      label2.Location = new Point(12, 43);
      label2.Name = "label2";
      label2.Size = new Size(134, 15);
      label2.TabIndex = 7;
      label2.Text = "Starting/Brush Density";
      // 
      // nudDensity
      // 
      nudDensity.BackColor = SystemColors.ScrollBar;
      nudDensity.BorderStyle = BorderStyle.None;
      nudDensity.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
      nudDensity.Increment = new decimal(new int[] { 20, 0, 0, 0 });
      nudDensity.Location = new Point(4, 61);
      nudDensity.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
      nudDensity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
      nudDensity.Name = "nudDensity";
      nudDensity.Size = new Size(159, 19);
      nudDensity.TabIndex = 6;
      nudDensity.Value = new decimal(new int[] { 5, 0, 0, 0 });
      // 
      // label1
      // 
      label1.AutoSize = true;
      label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline);
      label1.Location = new Point(51, 2);
      label1.Name = "label1";
      label1.Size = new Size(66, 15);
      label1.TabIndex = 5;
      label1.Text = "Resolution";
      // 
      // nudResolution
      // 
      nudResolution.BackColor = SystemColors.ScrollBar;
      nudResolution.BorderStyle = BorderStyle.None;
      nudResolution.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
      nudResolution.Increment = new decimal(new int[] { 2, 0, 0, 0 });
      nudResolution.Location = new Point(3, 20);
      nudResolution.Maximum = new decimal(new int[] { 25, 0, 0, 0 });
      nudResolution.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
      nudResolution.Name = "nudResolution";
      nudResolution.Size = new Size(159, 19);
      nudResolution.TabIndex = 0;
      nudResolution.Value = new decimal(new int[] { 5, 0, 0, 0 });
      nudResolution.ValueChanged += nudResolution_ValueChanged;
      // 
      // butEmpty
      // 
      butEmpty.BackColor = SystemColors.ControlDark;
      butEmpty.Cursor = Cursors.Hand;
      butEmpty.FlatAppearance.BorderColor = Color.Black;
      butEmpty.FlatAppearance.BorderSize = 2;
      butEmpty.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline);
      butEmpty.Location = new Point(2, 83);
      butEmpty.Name = "butEmpty";
      butEmpty.Size = new Size(159, 34);
      butEmpty.TabIndex = 10;
      butEmpty.Text = "Empty";
      butEmpty.UseVisualStyleBackColor = false;
      butEmpty.Click += butEmpty_Click;
      // 
      // butPause
      // 
      butPause.BackColor = SystemColors.ControlDark;
      butPause.Cursor = Cursors.Hand;
      butPause.FlatAppearance.BorderColor = Color.Black;
      butPause.FlatAppearance.BorderSize = 2;
      butPause.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline);
      butPause.Location = new Point(2, 43);
      butPause.Name = "butPause";
      butPause.Size = new Size(159, 34);
      butPause.TabIndex = 9;
      butPause.Text = "Pause/Unpause";
      butPause.UseVisualStyleBackColor = false;
      butPause.Click += butPause_Click;
      // 
      // butStart
      // 
      butStart.BackColor = SystemColors.ControlDark;
      butStart.Cursor = Cursors.Hand;
      butStart.FlatAppearance.BorderColor = Color.Black;
      butStart.FlatAppearance.BorderSize = 2;
      butStart.Font = new Font("Segoe UI", 9F, FontStyle.Bold | FontStyle.Underline);
      butStart.Location = new Point(2, 3);
      butStart.Name = "butStart";
      butStart.Size = new Size(159, 34);
      butStart.TabIndex = 8;
      butStart.Text = "Restart";
      butStart.UseVisualStyleBackColor = false;
      butStart.Click += butStart_Click;
      // 
      // pbMap
      // 
      pbMap.Dock = DockStyle.Fill;
      pbMap.Location = new Point(0, 0);
      pbMap.Name = "pbMap";
      pbMap.Size = new Size(973, 615);
      pbMap.TabIndex = 0;
      pbMap.TabStop = false;
      pbMap.MouseDown += pbMap_MouseDown;
      pbMap.MouseMove += pbMap_MouseMove;
      pbMap.MouseWheel += pbMap_MouseWheel;
      // 
      // timerFPS
      // 
      timerFPS.Interval = 16;
      timerFPS.Tick += timerFPS_Tick;
      // 
      // timerGPS
      // 
      timerGPS.Tick += timerGPS_Tick;
      // 
      // Form1
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(1146, 617);
      Controls.Add(scMap);
      Icon = (Icon)resources.GetObject("$this.Icon");
      MinimumSize = new Size(352, 641);
      Name = "Form1";
      Text = "Life Simulator";
      Resize += Form1_Resize;
      scMap.Panel1.ResumeLayout(false);
      scMap.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)scMap).EndInit();
      scMap.ResumeLayout(false);
      splitContainer2.Panel1.ResumeLayout(false);
      splitContainer2.Panel1.PerformLayout();
      splitContainer2.Panel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
      splitContainer2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)nudBrushTeam).EndInit();
      ((System.ComponentModel.ISupportInitialize)nudTeams).EndInit();
      ((System.ComponentModel.ISupportInitialize)nudTransparency).EndInit();
      ((System.ComponentModel.ISupportInitialize)nudThreads).EndInit();
      ((System.ComponentModel.ISupportInitialize)nudGPS).EndInit();
      ((System.ComponentModel.ISupportInitialize)nudFPS).EndInit();
      ((System.ComponentModel.ISupportInitialize)nudOffset).EndInit();
      ((System.ComponentModel.ISupportInitialize)nudBrushThickness).EndInit();
      ((System.ComponentModel.ISupportInitialize)nudDensity).EndInit();
      ((System.ComponentModel.ISupportInitialize)nudResolution).EndInit();
      ((System.ComponentModel.ISupportInitialize)pbMap).EndInit();
      ResumeLayout(false);
    }

    #endregion

    private SplitContainer scMap;
    private SplitContainer splitContainer2;
    private NumericUpDown nudResolution;
    private PictureBox pbMap;
    private Label label1;
    private Label label2;
    private NumericUpDown nudDensity;
    private Button butStart;
    private Button butEmpty;
    private Button butPause;
    private System.Windows.Forms.Timer timerFPS;
    private Label label4;
    private NumericUpDown nudBrushThickness;
    private CheckBox cbRandomBrush;
    private CheckBox cbPixelOffBorder;
    private Label label5;
    private NumericUpDown nudOffset;
    private Label label6;
    private NumericUpDown nudFPS;
    private Label label3;
    private NumericUpDown nudGPS;
    private System.Windows.Forms.Timer timerGPS;
    private Label label7;
    private NumericUpDown nudThreads;
    private Label label8;
    private NumericUpDown nudTransparency;
    private Button butBackColor;
    private Button butCellColor;
    private Button butMouseStepsColor;
    private CheckBox cbTeams;
    private NumericUpDown nudTeams;
    private NumericUpDown nudBrushTeam;
  }
}
