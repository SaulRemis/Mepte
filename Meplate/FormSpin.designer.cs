namespace Meplate
{
    partial class FormSpin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSpin));
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("1 m RULER", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("2 m RULER", System.Windows.Forms.HorizontalAlignment.Center);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this._MeplatestatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ToolStripConnectionPC = new SpinPlatform_Controls.SpinToolStripConnectionControl();
            this.ToolStripConnectionSpeed = new SpinPlatform_Controls.SpinToolStripConnectionControl();
            this.toolStripStatusframerate = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusSpeed = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusMidiendo = new System.Windows.Forms.ToolStripStatusLabel();
            this.label2 = new SpinPlatform_Controls.SpinBlackDataLabelControl();
            this.label1 = new SpinPlatform_Controls.SpinBlackDataLabelControl();
            this.labelThickness = new SpinPlatform_Controls.SpinBlackDataLabelControl();
            this.labelWidth = new SpinPlatform_Controls.SpinBlackDataLabelControl();
            this.labelMinimum2 = new SpinPlatform_Controls.SpinBlackDataLabelControl();
            this.labelMinimum1 = new SpinPlatform_Controls.SpinBlackDataLabelControl();
            this.spinBlackDataLabelControl5 = new SpinPlatform_Controls.SpinBlackDataLabelControl();
            this.spinTopMenuControl1 = new SpinPlatform_Controls.SpinTopMenuControl();
            this.MeplateMenu = new SpinPlatform_Controls.SpinToolStripMenuItemControl();
            this.iniciarToolStripMenuItem = new SpinPlatform_Controls.SpinToolStripMenuItemControl();
            this.pararToolStripMenuItem = new SpinPlatform_Controls.SpinToolStripMenuItemControl();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.forceMeasurementToolStripMenuItem1 = new SpinPlatform_Controls.SpinToolStripMenuItemControl();
            this.startToolStripMenuItem = new SpinPlatform_Controls.SpinToolStripMenuItemControl();
            this.stopToolStripMenuItem = new SpinPlatform_Controls.SpinToolStripMenuItemControl();
            this.MenuConfiguracion = new SpinPlatform_Controls.SpinToolStripMenuItemControl();
            this.configuracionOffsetToolStripMenuItem = new SpinPlatform_Controls.SpinToolStripMenuItemControl();
            this.speedCardConnectionToolStripMenuItem = new SpinPlatform_Controls.SpinToolStripMenuItemControl();
            this.connectToolStripMenuItem = new SpinPlatform_Controls.SpinToolStripMenuItemControl();
            this.labelPlateId = new SpinPlatform_Controls.SpinBlackDataLabelControl();
            this.labelLength = new SpinPlatform_Controls.SpinBlackDataLabelControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.spinControl1 = new SpinPlatform_Controls.SpinControl();
            this.panel3 = new System.Windows.Forms.Panel();
            this.VentanaHalconPrincipal = new HalconDotNet.HWindowControl();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label_escala_min = new System.Windows.Forms.Label();
            this.label_escala_inter = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label_escala_max = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.spinControl2 = new SpinPlatform_Controls.SpinControl();
            this._listViewPuntos = new System.Windows.Forms.ListView();
            this.DEFECT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.X = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Y = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Z = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.X2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Y2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Z2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DIFF = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timerEstado = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this._MeplatestatusStrip1.SuspendLayout();
            this.spinTopMenuControl1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.spinControl1.ContainerPanel.SuspendLayout();
            this.panel3.SuspendLayout();
            this.spinControl2.ContainerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Controls.Add(this._MeplatestatusStrip1, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelThickness, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelWidth, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelMinimum2, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelMinimum1, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.spinBlackDataLabelControl5, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.spinTopMenuControl1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelPlateId, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelLength, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 24F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1276, 614);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // _MeplatestatusStrip1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this._MeplatestatusStrip1, 5);
            this._MeplatestatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripConnectionPC,
            this.ToolStripConnectionSpeed,
            this.toolStripStatusframerate,
            this.toolStripStatusSpeed,
            this.toolStripStatusMidiendo});
            this._MeplatestatusStrip1.Location = new System.Drawing.Point(0, 590);
            this._MeplatestatusStrip1.Name = "_MeplatestatusStrip1";
            this._MeplatestatusStrip1.Size = new System.Drawing.Size(1276, 24);
            this._MeplatestatusStrip1.TabIndex = 27;
            this._MeplatestatusStrip1.Text = "statusStrip1";
            // 
            // ToolStripConnectionPC
            // 
            this.ToolStripConnectionPC.AutoSize = false;
            this.ToolStripConnectionPC.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripConnectionPC.FontColor = System.Drawing.Color.White;
            this.ToolStripConnectionPC.ForeColor = System.Drawing.Color.White;
            this.ToolStripConnectionPC.Name = "ToolStripConnectionPC";
            this.ToolStripConnectionPC.Size = new System.Drawing.Size(338, 19);
            this.ToolStripConnectionPC.StatusConnection = SpinPlatform_Controls.spinConnectionStatus.disconnected;
            this.ToolStripConnectionPC.Text = "Process Computer";
            // 
            // ToolStripConnectionSpeed
            // 
            this.ToolStripConnectionSpeed.AutoSize = false;
            this.ToolStripConnectionSpeed.BackColor = System.Drawing.Color.Transparent;
            this.ToolStripConnectionSpeed.FontColor = System.Drawing.Color.White;
            this.ToolStripConnectionSpeed.ForeColor = System.Drawing.Color.White;
            this.ToolStripConnectionSpeed.Name = "ToolStripConnectionSpeed";
            this.ToolStripConnectionSpeed.Size = new System.Drawing.Size(276, 19);
            this.ToolStripConnectionSpeed.Spring = true;
            this.ToolStripConnectionSpeed.StatusConnection = SpinPlatform_Controls.spinConnectionStatus.disconnected;
            this.ToolStripConnectionSpeed.Text = " Speed Card";
            // 
            // toolStripStatusframerate
            // 
            this.toolStripStatusframerate.Name = "toolStripStatusframerate";
            this.toolStripStatusframerate.Size = new System.Drawing.Size(276, 19);
            this.toolStripStatusframerate.Spring = true;
            this.toolStripStatusframerate.Text = "FRAMERATE : 0 ";
            // 
            // toolStripStatusSpeed
            // 
            this.toolStripStatusSpeed.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusSpeed.BorderStyle = System.Windows.Forms.Border3DStyle.Adjust;
            this.toolStripStatusSpeed.Name = "toolStripStatusSpeed";
            this.toolStripStatusSpeed.Size = new System.Drawing.Size(276, 19);
            this.toolStripStatusSpeed.Spring = true;
            this.toolStripStatusSpeed.Text = "SPEED : 0 m/min";
            // 
            // toolStripStatusMidiendo
            // 
            this.toolStripStatusMidiendo.Name = "toolStripStatusMidiendo";
            this.toolStripStatusMidiendo.Size = new System.Drawing.Size(95, 19);
            this.toolStripStatusMidiendo.Text = "Waiting for Plate";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("label2.BackgroundImage")));
            this.label2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.FontMainText = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.FontSubtitleText = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.FontTitleText = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.OrangeRed;
            this.label2.Location = new System.Drawing.Point(765, 110);
            this.label2.MainText = "0";
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(9, 0, 9, 8);
            this.label2.PageEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.label2.PageStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label2.Size = new System.Drawing.Size(255, 60);
            this.label2.SubtitleText = "2m Defects";
            this.label2.TabIndex = 16;
            this.label2.TitleColor = System.Drawing.Color.DarkOrange;
            this.label2.TitleText = "";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("label1.BackgroundImage")));
            this.label1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.FontMainText = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.FontSubtitleText = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.FontTitleText = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.OrangeRed;
            this.label1.Location = new System.Drawing.Point(510, 110);
            this.label1.MainText = "0";
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(9, 0, 9, 8);
            this.label1.PageEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.label1.PageStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Size = new System.Drawing.Size(255, 60);
            this.label1.SubtitleText = "1m Defects";
            this.label1.TabIndex = 15;
            this.label1.TitleColor = System.Drawing.Color.DarkOrange;
            this.label1.TitleText = "";
            // 
            // labelThickness
            // 
            this.labelThickness.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelThickness.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("labelThickness.BackgroundImage")));
            this.labelThickness.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.labelThickness.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelThickness.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelThickness.FontMainText = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelThickness.FontSubtitleText = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelThickness.FontTitleText = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelThickness.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelThickness.Location = new System.Drawing.Point(255, 110);
            this.labelThickness.MainText = "0";
            this.labelThickness.Margin = new System.Windows.Forms.Padding(0);
            this.labelThickness.Name = "labelThickness";
            this.labelThickness.Padding = new System.Windows.Forms.Padding(9, 0, 9, 8);
            this.labelThickness.PageEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.labelThickness.PageStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelThickness.Size = new System.Drawing.Size(255, 60);
            this.labelThickness.SubtitleText = "Thickness";
            this.labelThickness.TabIndex = 14;
            this.labelThickness.TitleColor = System.Drawing.Color.DarkOrange;
            this.labelThickness.TitleText = "";
            // 
            // labelWidth
            // 
            this.labelWidth.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelWidth.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("labelWidth.BackgroundImage")));
            this.labelWidth.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.labelWidth.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelWidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWidth.FontMainText = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWidth.FontSubtitleText = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWidth.FontTitleText = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWidth.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelWidth.Location = new System.Drawing.Point(0, 110);
            this.labelWidth.MainText = "0";
            this.labelWidth.Margin = new System.Windows.Forms.Padding(0);
            this.labelWidth.Name = "labelWidth";
            this.labelWidth.Padding = new System.Windows.Forms.Padding(9, 0, 9, 8);
            this.labelWidth.PageEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(124)))), ((int)(((byte)(124)))), ((int)(((byte)(124)))));
            this.labelWidth.PageStartColor = System.Drawing.Color.Black;
            this.labelWidth.Size = new System.Drawing.Size(255, 60);
            this.labelWidth.SubtitleText = "Width";
            this.labelWidth.TabIndex = 13;
            this.labelWidth.TitleColor = System.Drawing.Color.DarkOrange;
            this.labelWidth.TitleText = "";
            // 
            // labelMinimum2
            // 
            this.labelMinimum2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelMinimum2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("labelMinimum2.BackgroundImage")));
            this.labelMinimum2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.labelMinimum2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelMinimum2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMinimum2.FontMainText = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMinimum2.FontSubtitleText = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMinimum2.FontTitleText = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMinimum2.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelMinimum2.Location = new System.Drawing.Point(765, 50);
            this.labelMinimum2.MainText = "0";
            this.labelMinimum2.Margin = new System.Windows.Forms.Padding(0);
            this.labelMinimum2.Name = "labelMinimum2";
            this.labelMinimum2.Padding = new System.Windows.Forms.Padding(9, 4, 9, 8);
            this.labelMinimum2.PageEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelMinimum2.PageStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelMinimum2.Size = new System.Drawing.Size(255, 60);
            this.labelMinimum2.SubtitleText = "2m Minimum defect accepted";
            this.labelMinimum2.TabIndex = 12;
            this.labelMinimum2.TitleColor = System.Drawing.Color.DarkOrange;
            this.labelMinimum2.TitleText = "";
            // 
            // labelMinimum1
            // 
            this.labelMinimum1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelMinimum1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("labelMinimum1.BackgroundImage")));
            this.labelMinimum1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.labelMinimum1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelMinimum1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMinimum1.FontMainText = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMinimum1.FontSubtitleText = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMinimum1.FontTitleText = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMinimum1.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelMinimum1.Location = new System.Drawing.Point(510, 50);
            this.labelMinimum1.MainText = "0";
            this.labelMinimum1.Margin = new System.Windows.Forms.Padding(0);
            this.labelMinimum1.Name = "labelMinimum1";
            this.labelMinimum1.Padding = new System.Windows.Forms.Padding(9, 4, 9, 8);
            this.labelMinimum1.PageEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelMinimum1.PageStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelMinimum1.Size = new System.Drawing.Size(255, 60);
            this.labelMinimum1.SubtitleText = "1m Minimum defect accepted";
            this.labelMinimum1.TabIndex = 11;
            this.labelMinimum1.TitleColor = System.Drawing.Color.DarkOrange;
            this.labelMinimum1.TitleText = "Validation";
            // 
            // spinBlackDataLabelControl5
            // 
            this.spinBlackDataLabelControl5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.spinBlackDataLabelControl5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("spinBlackDataLabelControl5.BackgroundImage")));
            this.spinBlackDataLabelControl5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.spinBlackDataLabelControl5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spinBlackDataLabelControl5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spinBlackDataLabelControl5.FontMainText = new System.Drawing.Font("Microsoft Sans Serif", 46F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spinBlackDataLabelControl5.FontSubtitleText = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spinBlackDataLabelControl5.FontTitleText = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.spinBlackDataLabelControl5.ForeColor = System.Drawing.Color.OrangeRed;
            this.spinBlackDataLabelControl5.Location = new System.Drawing.Point(1020, 50);
            this.spinBlackDataLabelControl5.MainText = "OK";
            this.spinBlackDataLabelControl5.Margin = new System.Windows.Forms.Padding(0);
            this.spinBlackDataLabelControl5.Name = "spinBlackDataLabelControl5";
            this.spinBlackDataLabelControl5.Padding = new System.Windows.Forms.Padding(9, 8, 9, 8);
            this.spinBlackDataLabelControl5.PageEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.spinBlackDataLabelControl5.PageStartColor = System.Drawing.Color.DarkGreen;
            this.tableLayoutPanel1.SetRowSpan(this.spinBlackDataLabelControl5, 2);
            this.spinBlackDataLabelControl5.Size = new System.Drawing.Size(256, 120);
            this.spinBlackDataLabelControl5.SubtitleText = "Plate accepted";
            this.spinBlackDataLabelControl5.TabIndex = 5;
            this.spinBlackDataLabelControl5.TitleColor = System.Drawing.Color.White;
            this.spinBlackDataLabelControl5.TitleText = "Status";
            // 
            // spinTopMenuControl1
            // 
            this.spinTopMenuControl1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("spinTopMenuControl1.BackgroundImage")));
            this.tableLayoutPanel1.SetColumnSpan(this.spinTopMenuControl1, 5);
            this.spinTopMenuControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spinTopMenuControl1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MeplateMenu,
            this.MenuConfiguracion});
            this.spinTopMenuControl1.Location = new System.Drawing.Point(0, 0);
            this.spinTopMenuControl1.Name = "spinTopMenuControl1";
            this.spinTopMenuControl1.Size = new System.Drawing.Size(1276, 50);
            this.spinTopMenuControl1.TabIndex = 0;
            this.spinTopMenuControl1.Text = "spinTopMenuControl1";
            // 
            // MeplateMenu
            // 
            this.MeplateMenu.BackColor = System.Drawing.Color.Transparent;
            this.MeplateMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iniciarToolStripMenuItem,
            this.pararToolStripMenuItem,
            this.toolStripSeparator1,
            this.forceMeasurementToolStripMenuItem1});
            this.MeplateMenu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.MeplateMenu.Name = "MeplateMenu";
            this.MeplateMenu.Size = new System.Drawing.Size(65, 46);
            this.MeplateMenu.Text = "Meplate";
            // 
            // iniciarToolStripMenuItem
            // 
            this.iniciarToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.iniciarToolStripMenuItem.Enabled = false;
            this.iniciarToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.iniciarToolStripMenuItem.Name = "iniciarToolStripMenuItem";
            this.iniciarToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.iniciarToolStripMenuItem.Text = "Start";
            this.iniciarToolStripMenuItem.Click += new System.EventHandler(this.iniciarToolStripMenuItem_Click);
            // 
            // pararToolStripMenuItem
            // 
            this.pararToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.pararToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.pararToolStripMenuItem.Name = "pararToolStripMenuItem";
            this.pararToolStripMenuItem.Size = new System.Drawing.Size(186, 22);
            this.pararToolStripMenuItem.Text = "Stop";
            this.pararToolStripMenuItem.Click += new System.EventHandler(this.pararToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(183, 6);
            // 
            // forceMeasurementToolStripMenuItem1
            // 
            this.forceMeasurementToolStripMenuItem1.BackColor = System.Drawing.Color.Transparent;
            this.forceMeasurementToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.forceMeasurementToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.forceMeasurementToolStripMenuItem1.Name = "forceMeasurementToolStripMenuItem1";
            this.forceMeasurementToolStripMenuItem1.Size = new System.Drawing.Size(186, 22);
            this.forceMeasurementToolStripMenuItem1.Text = "Force Measurement";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.startToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.stopToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // MenuConfiguracion
            // 
            this.MenuConfiguracion.BackColor = System.Drawing.Color.Transparent;
            this.MenuConfiguracion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configuracionOffsetToolStripMenuItem,
            this.speedCardConnectionToolStripMenuItem});
            this.MenuConfiguracion.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.MenuConfiguracion.Name = "MenuConfiguracion";
            this.MenuConfiguracion.Size = new System.Drawing.Size(48, 46);
            this.MenuConfiguracion.Text = "Tools";
            // 
            // configuracionOffsetToolStripMenuItem
            // 
            this.configuracionOffsetToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.configuracionOffsetToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.configuracionOffsetToolStripMenuItem.Name = "configuracionOffsetToolStripMenuItem";
            this.configuracionOffsetToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.configuracionOffsetToolStripMenuItem.Text = "Offset Configuration";
            this.configuracionOffsetToolStripMenuItem.Click += new System.EventHandler(this.configuracionToolStripMenuItem_Click);
            // 
            // speedCardConnectionToolStripMenuItem
            // 
            this.speedCardConnectionToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.speedCardConnectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem});
            this.speedCardConnectionToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.speedCardConnectionToolStripMenuItem.Name = "speedCardConnectionToolStripMenuItem";
            this.speedCardConnectionToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.speedCardConnectionToolStripMenuItem.Text = "SpeedCardConnection";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.connectToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // labelPlateId
            // 
            this.labelPlateId.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelPlateId.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("labelPlateId.BackgroundImage")));
            this.labelPlateId.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.labelPlateId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPlateId.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlateId.FontMainText = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlateId.FontSubtitleText = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlateId.FontTitleText = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPlateId.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelPlateId.Location = new System.Drawing.Point(0, 50);
            this.labelPlateId.MainText = "0";
            this.labelPlateId.Margin = new System.Windows.Forms.Padding(0);
            this.labelPlateId.Name = "labelPlateId";
            this.labelPlateId.Padding = new System.Windows.Forms.Padding(9, 4, 9, 8);
            this.labelPlateId.PageEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelPlateId.PageStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelPlateId.Size = new System.Drawing.Size(255, 60);
            this.labelPlateId.SubtitleText = "Plate ID";
            this.labelPlateId.TabIndex = 1;
            this.labelPlateId.TitleColor = System.Drawing.Color.DarkOrange;
            this.labelPlateId.TitleText = "Information";
            // 
            // labelLength
            // 
            this.labelLength.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelLength.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("labelLength.BackgroundImage")));
            this.labelLength.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.labelLength.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLength.FontMainText = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLength.FontSubtitleText = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLength.FontTitleText = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelLength.ForeColor = System.Drawing.Color.OrangeRed;
            this.labelLength.Location = new System.Drawing.Point(255, 50);
            this.labelLength.MainText = "0";
            this.labelLength.Margin = new System.Windows.Forms.Padding(0);
            this.labelLength.Name = "labelLength";
            this.labelLength.Padding = new System.Windows.Forms.Padding(9, 4, 9, 8);
            this.labelLength.PageEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.labelLength.PageStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelLength.Size = new System.Drawing.Size(255, 60);
            this.labelLength.SubtitleText = "Length";
            this.labelLength.TabIndex = 10;
            this.labelLength.TitleColor = System.Drawing.Color.DarkOrange;
            this.labelLength.TitleText = "";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel1.SetColumnSpan(this.tableLayoutPanel2, 5);
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 850F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.spinControl1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.spinControl2, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 173);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1270, 414);
            this.tableLayoutPanel2.TabIndex = 28;
            // 
            // spinControl1
            // 
            this.spinControl1.BackColor = System.Drawing.Color.Transparent;
            this.spinControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            // 
            // spinControl1.panel2
            // 
            this.spinControl1.ContainerPanel.BackColor = System.Drawing.Color.White;
            this.spinControl1.ContainerPanel.Controls.Add(this.panel3);
            this.spinControl1.ContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spinControl1.ContainerPanel.Location = new System.Drawing.Point(2, 2);
            this.spinControl1.ContainerPanel.Name = "panel2";
            this.spinControl1.ContainerPanel.Padding = new System.Windows.Forms.Padding(3);
            this.spinControl1.ContainerPanel.Size = new System.Drawing.Size(838, 377);
            this.spinControl1.ContainerPanel.TabIndex = 1;
            this.spinControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spinControl1.FontBarColor = System.Drawing.Color.White;
            this.spinControl1.InfoText = null;
            this.spinControl1.Location = new System.Drawing.Point(0, 0);
            this.spinControl1.Margin = new System.Windows.Forms.Padding(0);
            this.spinControl1.Name = "spinControl1";
            this.spinControl1.Padding = new System.Windows.Forms.Padding(4);
            this.spinControl1.PageEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(124)))), ((int)(((byte)(2)))));
            this.spinControl1.PageStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(158)))), ((int)(((byte)(1)))));
            this.spinControl1.Size = new System.Drawing.Size(850, 414);
            this.spinControl1.TabIndex = 25;
            this.spinControl1.TitleText = "Plate Defects Map";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.VentanaHalconPrincipal);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label_escala_min);
            this.panel3.Controls.Add(this.label_escala_inter);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Controls.Add(this.label_escala_max);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel6);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(832, 371);
            this.panel3.TabIndex = 32;
            // 
            // VentanaHalconPrincipal
            // 
            this.VentanaHalconPrincipal.BackColor = System.Drawing.Color.Black;
            this.VentanaHalconPrincipal.BorderColor = System.Drawing.Color.Black;
            this.VentanaHalconPrincipal.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.VentanaHalconPrincipal.Location = new System.Drawing.Point(19, 45);
            this.VentanaHalconPrincipal.Name = "VentanaHalconPrincipal";
            this.VentanaHalconPrincipal.Size = new System.Drawing.Size(712, 314);
            this.VentanaHalconPrincipal.TabIndex = 12;
            this.VentanaHalconPrincipal.WindowSize = new System.Drawing.Size(712, 314);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(15, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 24);
            this.label4.TabIndex = 22;
            this.label4.Text = "DEFECTS :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(181, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 24);
            this.label5.TabIndex = 22;
            this.label5.Text = "1m RULER";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(336, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 24);
            this.label6.TabIndex = 22;
            this.label6.Text = "2m RULER";
            // 
            // label_escala_min
            // 
            this.label_escala_min.AutoSize = true;
            this.label_escala_min.Location = new System.Drawing.Point(760, 346);
            this.label_escala_min.Name = "label_escala_min";
            this.label_escala_min.Size = new System.Drawing.Size(32, 13);
            this.label_escala_min.TabIndex = 25;
            this.label_escala_min.Text = "0 mm";
            // 
            // label_escala_inter
            // 
            this.label_escala_inter.AutoSize = true;
            this.label_escala_inter.Location = new System.Drawing.Point(760, 207);
            this.label_escala_inter.Name = "label_escala_inter";
            this.label_escala_inter.Size = new System.Drawing.Size(38, 13);
            this.label_escala_inter.TabIndex = 25;
            this.label_escala_inter.Text = "30 mm";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel4.Location = new System.Drawing.Point(150, 11);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(25, 23);
            this.panel4.TabIndex = 23;
            // 
            // label_escala_max
            // 
            this.label_escala_max.AutoSize = true;
            this.label_escala_max.Location = new System.Drawing.Point(760, 45);
            this.label_escala_max.Name = "label_escala_max";
            this.label_escala_max.Size = new System.Drawing.Size(38, 13);
            this.label_escala_max.TabIndex = 25;
            this.label_escala_max.Text = "60 mm";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.GreenYellow;
            this.panel5.Location = new System.Drawing.Point(305, 11);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(25, 23);
            this.panel5.TabIndex = 23;
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel6.BackgroundImage")));
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Location = new System.Drawing.Point(740, 43);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(14, 316);
            this.panel6.TabIndex = 24;
            // 
            // spinControl2
            // 
            this.spinControl2.BackColor = System.Drawing.Color.Transparent;
            this.spinControl2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            // 
            // spinControl2.panel2
            // 
            this.spinControl2.ContainerPanel.BackColor = System.Drawing.Color.White;
            this.spinControl2.ContainerPanel.Controls.Add(this._listViewPuntos);
            this.spinControl2.ContainerPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spinControl2.ContainerPanel.Location = new System.Drawing.Point(2, 2);
            this.spinControl2.ContainerPanel.Margin = new System.Windows.Forms.Padding(0);
            this.spinControl2.ContainerPanel.Name = "panel2";
            this.spinControl2.ContainerPanel.Padding = new System.Windows.Forms.Padding(3);
            this.spinControl2.ContainerPanel.Size = new System.Drawing.Size(408, 377);
            this.spinControl2.ContainerPanel.TabIndex = 1;
            this.spinControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spinControl2.FontBarColor = System.Drawing.Color.White;
            this.spinControl2.InfoText = null;
            this.spinControl2.Location = new System.Drawing.Point(850, 0);
            this.spinControl2.Margin = new System.Windows.Forms.Padding(0);
            this.spinControl2.Name = "spinControl2";
            this.spinControl2.Padding = new System.Windows.Forms.Padding(4);
            this.spinControl2.PageEndColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(124)))), ((int)(((byte)(2)))));
            this.spinControl2.PageStartColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(158)))), ((int)(((byte)(1)))));
            this.spinControl2.Size = new System.Drawing.Size(420, 414);
            this.spinControl2.TabIndex = 26;
            this.spinControl2.TitleText = "Defect information";
            // 
            // _listViewPuntos
            // 
            this._listViewPuntos.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this._listViewPuntos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this._listViewPuntos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DEFECT,
            this.X,
            this.Y,
            this.Z,
            this.X2,
            this.Y2,
            this.Z2,
            this.DIFF});
            this._listViewPuntos.Dock = System.Windows.Forms.DockStyle.Fill;
            this._listViewPuntos.FullRowSelect = true;
            listViewGroup1.Header = "1 m RULER";
            listViewGroup1.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup1.Name = "listViewGroup1m";
            listViewGroup2.Header = "2 m RULER";
            listViewGroup2.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup2.Name = "listViewGroup2m";
            this._listViewPuntos.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this._listViewPuntos.Location = new System.Drawing.Point(3, 3);
            this._listViewPuntos.Margin = new System.Windows.Forms.Padding(0);
            this._listViewPuntos.Name = "_listViewPuntos";
            this._listViewPuntos.Size = new System.Drawing.Size(402, 371);
            this._listViewPuntos.TabIndex = 25;
            this._listViewPuntos.UseCompatibleStateImageBehavior = false;
            this._listViewPuntos.View = System.Windows.Forms.View.Details;
            // 
            // DEFECT
            // 
            this.DEFECT.Text = "DEFECT";
            this.DEFECT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // X
            // 
            this.X.Text = "X";
            this.X.Width = 50;
            // 
            // Y
            // 
            this.Y.Text = "Y";
            this.Y.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Z
            // 
            this.Z.Text = "Z";
            this.Z.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Z.Width = 50;
            // 
            // X2
            // 
            this.X2.Text = "X";
            this.X2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.X2.Width = 50;
            // 
            // Y2
            // 
            this.Y2.Text = "Y";
            this.Y2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Z2
            // 
            this.Z2.Text = "Z";
            this.Z2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.Z2.Width = 50;
            // 
            // DIFF
            // 
            this.DIFF.Text = "DIFF";
            this.DIFF.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DIFF.Width = 50;
            // 
            // timerEstado
            // 
            this.timerEstado.Tick += new System.EventHandler(this.timerEstado_Tick);
            // 
            // FormSaul
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1276, 614);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.spinTopMenuControl1;
            this.Name = "FormSaul";
            this.Text = "MEPLATE";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSaul_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this._MeplatestatusStrip1.ResumeLayout(false);
            this._MeplatestatusStrip1.PerformLayout();
            this.spinTopMenuControl1.ResumeLayout(false);
            this.spinTopMenuControl1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.spinControl1.ContainerPanel.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.spinControl2.ContainerPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private SpinPlatform_Controls.SpinBlackDataLabelControl spinBlackDataLabelControl5;
        private SpinPlatform_Controls.SpinTopMenuControl spinTopMenuControl1;
        private SpinPlatform_Controls.SpinBlackDataLabelControl labelPlateId;
        private SpinPlatform_Controls.SpinToolStripMenuItemControl MeplateMenu;
        private SpinPlatform_Controls.SpinToolStripMenuItemControl MenuConfiguracion;
        private SpinPlatform_Controls.SpinToolStripMenuItemControl iniciarToolStripMenuItem;
        private SpinPlatform_Controls.SpinToolStripMenuItemControl pararToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private SpinPlatform_Controls.SpinToolStripMenuItemControl forceMeasurementToolStripMenuItem1;
        private SpinPlatform_Controls.SpinToolStripMenuItemControl startToolStripMenuItem;
        private SpinPlatform_Controls.SpinToolStripMenuItemControl stopToolStripMenuItem;
        private SpinPlatform_Controls.SpinToolStripMenuItemControl configuracionOffsetToolStripMenuItem;
        private SpinPlatform_Controls.SpinToolStripMenuItemControl speedCardConnectionToolStripMenuItem;
        private SpinPlatform_Controls.SpinToolStripMenuItemControl connectToolStripMenuItem;
        private SpinPlatform_Controls.SpinBlackDataLabelControl label2;
        private SpinPlatform_Controls.SpinBlackDataLabelControl label1;
        private SpinPlatform_Controls.SpinBlackDataLabelControl labelThickness;
        private SpinPlatform_Controls.SpinBlackDataLabelControl labelWidth;
        private SpinPlatform_Controls.SpinBlackDataLabelControl labelMinimum2;
        private SpinPlatform_Controls.SpinBlackDataLabelControl labelMinimum1;
        private SpinPlatform_Controls.SpinBlackDataLabelControl labelLength;
        private System.Windows.Forms.Timer timerEstado;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private SpinPlatform_Controls.SpinControl spinControl1;
        private System.Windows.Forms.Panel panel3;
        private HalconDotNet.HWindowControl VentanaHalconPrincipal;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label_escala_min;
        private System.Windows.Forms.Label label_escala_inter;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label_escala_max;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private SpinPlatform_Controls.SpinControl spinControl2;
        private System.Windows.Forms.ListView _listViewPuntos;
        private System.Windows.Forms.ColumnHeader DEFECT;
        private System.Windows.Forms.ColumnHeader X;
        private System.Windows.Forms.ColumnHeader Y;
        private System.Windows.Forms.ColumnHeader Z;
        private System.Windows.Forms.ColumnHeader X2;
        private System.Windows.Forms.ColumnHeader Y2;
        private System.Windows.Forms.ColumnHeader Z2;
        private System.Windows.Forms.ColumnHeader DIFF;
        private System.Windows.Forms.StatusStrip _MeplatestatusStrip1;
        private SpinPlatform_Controls.SpinToolStripConnectionControl ToolStripConnectionPC;
        private SpinPlatform_Controls.SpinToolStripConnectionControl ToolStripConnectionSpeed;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusframerate;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusSpeed;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusMidiendo;
    }
}