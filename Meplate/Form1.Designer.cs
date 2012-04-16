namespace Meplate
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("1 m RULER", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("2 m RULER", System.Windows.Forms.HorizontalAlignment.Center);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.VentanaHalconPrincipal = new HalconDotNet.HWindowControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_THICKNESS = new System.Windows.Forms.Label();
            this.label_Width = new System.Windows.Forms.Label();
            this.label_Lenght = new System.Windows.Forms.Label();
            this.label_ID = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this._listViewPuntos = new System.Windows.Forms.ListView();
            this.DEFECT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.X = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Y = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Z = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.X2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Y2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Z2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DIFF = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label_escala_min = new System.Windows.Forms.Label();
            this.label_escala_max = new System.Windows.Forms.Label();
            this.label_escala_inter = new System.Windows.Forms.Label();
            this._MeplatestatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusframerate = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusSpeed = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusTarjeta = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusProcessComputer = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerEstado = new System.Windows.Forms.Timer(this.components);
            this.Menu = new System.Windows.Forms.MenuStrip();
            this.MeplateMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.iniciarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pararToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.forceMeasurementToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuConfiguracion = new System.Windows.Forms.ToolStripMenuItem();
            this.configuracionOffsetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speedCardConnectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label_numdefects1 = new System.Windows.Forms.Label();
            this.labeL_defectominimo2 = new System.Windows.Forms.Label();
            this.labeL_defectominimo1 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_numdefect2 = new System.Windows.Forms.Label();
            this.button_Validacion = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this._MeplatestatusStrip1.SuspendLayout();
            this.Menu.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // VentanaHalconPrincipal
            // 
            this.VentanaHalconPrincipal.BackColor = System.Drawing.Color.Black;
            this.VentanaHalconPrincipal.BorderColor = System.Drawing.Color.Black;
            this.VentanaHalconPrincipal.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.VentanaHalconPrincipal.Location = new System.Drawing.Point(12, 265);
            this.VentanaHalconPrincipal.Name = "VentanaHalconPrincipal";
            this.VentanaHalconPrincipal.Size = new System.Drawing.Size(712, 314);
            this.VentanaHalconPrincipal.TabIndex = 12;
            this.VentanaHalconPrincipal.WindowSize = new System.Drawing.Size(712, 314);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_THICKNESS);
            this.groupBox1.Controls.Add(this.label_Width);
            this.groupBox1.Controls.Add(this.label_Lenght);
            this.groupBox1.Controls.Add(this.label_ID);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(261, 161);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "INFORMATION";
            // 
            // label_THICKNESS
            // 
            this.label_THICKNESS.AutoSize = true;
            this.label_THICKNESS.Location = new System.Drawing.Point(112, 118);
            this.label_THICKNESS.Name = "label_THICKNESS";
            this.label_THICKNESS.Size = new System.Drawing.Size(13, 13);
            this.label_THICKNESS.TabIndex = 20;
            this.label_THICKNESS.Text = "0";
            // 
            // label_Width
            // 
            this.label_Width.AutoSize = true;
            this.label_Width.Location = new System.Drawing.Point(112, 89);
            this.label_Width.Name = "label_Width";
            this.label_Width.Size = new System.Drawing.Size(13, 13);
            this.label_Width.TabIndex = 19;
            this.label_Width.Text = "0";
            // 
            // label_Lenght
            // 
            this.label_Lenght.AutoSize = true;
            this.label_Lenght.Location = new System.Drawing.Point(112, 61);
            this.label_Lenght.Name = "label_Lenght";
            this.label_Lenght.Size = new System.Drawing.Size(13, 13);
            this.label_Lenght.TabIndex = 19;
            this.label_Lenght.Text = "0";
            // 
            // label_ID
            // 
            this.label_ID.AutoSize = true;
            this.label_ID.Location = new System.Drawing.Point(112, 37);
            this.label_ID.Name = "label_ID";
            this.label_ID.Size = new System.Drawing.Size(13, 13);
            this.label_ID.TabIndex = 19;
            this.label_ID.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(179, 119);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(25, 13);
            this.label13.TabIndex = 18;
            this.label13.Text = "mm";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(7, 119);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "THICKNESS";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(179, 89);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "mm";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(7, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "WIDTH";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(179, 61);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(16, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "m";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "LENGHT";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "PLATE ID ";
            // 
            // _listViewPuntos
            // 
            this._listViewPuntos.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this._listViewPuntos.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.DEFECT,
            this.X,
            this.Y,
            this.Z,
            this.X2,
            this.Y2,
            this.Z2,
            this.DIFF});
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
            this._listViewPuntos.Location = new System.Drawing.Point(810, 265);
            this._listViewPuntos.Name = "_listViewPuntos";
            this._listViewPuntos.Size = new System.Drawing.Size(454, 305);
            this._listViewPuntos.TabIndex = 21;
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(29, 233);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 24);
            this.label4.TabIndex = 22;
            this.label4.Text = "DEFECTS :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(340, 233);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 24);
            this.label5.TabIndex = 22;
            this.label5.Text = "1m RULER";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(555, 233);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 24);
            this.label6.TabIndex = 22;
            this.label6.Text = "2m RULER";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel3.Location = new System.Drawing.Point(309, 233);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(25, 23);
            this.panel3.TabIndex = 23;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.GreenYellow;
            this.panel4.Location = new System.Drawing.Point(524, 233);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(25, 23);
            this.panel4.TabIndex = 23;
            // 
            // label_escala_min
            // 
            this.label_escala_min.AutoSize = true;
            this.label_escala_min.Location = new System.Drawing.Point(760, 566);
            this.label_escala_min.Name = "label_escala_min";
            this.label_escala_min.Size = new System.Drawing.Size(32, 13);
            this.label_escala_min.TabIndex = 25;
            this.label_escala_min.Text = "0 mm";
            // 
            // label_escala_max
            // 
            this.label_escala_max.AutoSize = true;
            this.label_escala_max.Location = new System.Drawing.Point(760, 263);
            this.label_escala_max.Name = "label_escala_max";
            this.label_escala_max.Size = new System.Drawing.Size(38, 13);
            this.label_escala_max.TabIndex = 25;
            this.label_escala_max.Text = "60 mm";
            // 
            // label_escala_inter
            // 
            this.label_escala_inter.AutoSize = true;
            this.label_escala_inter.Location = new System.Drawing.Point(760, 425);
            this.label_escala_inter.Name = "label_escala_inter";
            this.label_escala_inter.Size = new System.Drawing.Size(38, 13);
            this.label_escala_inter.TabIndex = 25;
            this.label_escala_inter.Text = "30 mm";
            // 
            // _MeplatestatusStrip1
            // 
            this._MeplatestatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusframerate,
            this.toolStripStatusSpeed,
            this.toolStripStatusTarjeta,
            this.toolStripStatusProcessComputer});
            this._MeplatestatusStrip1.Location = new System.Drawing.Point(0, 590);
            this._MeplatestatusStrip1.Name = "_MeplatestatusStrip1";
            this._MeplatestatusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this._MeplatestatusStrip1.Size = new System.Drawing.Size(1276, 24);
            this._MeplatestatusStrip1.TabIndex = 26;
            this._MeplatestatusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusframerate
            // 
            this.toolStripStatusframerate.Name = "toolStripStatusframerate";
            this.toolStripStatusframerate.Size = new System.Drawing.Size(315, 19);
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
            this.toolStripStatusSpeed.Size = new System.Drawing.Size(315, 19);
            this.toolStripStatusSpeed.Spring = true;
            this.toolStripStatusSpeed.Text = "SPEED : 0 m/min";
            // 
            // toolStripStatusTarjeta
            // 
            this.toolStripStatusTarjeta.BackColor = System.Drawing.Color.Red;
            this.toolStripStatusTarjeta.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusTarjeta.BorderStyle = System.Windows.Forms.Border3DStyle.Adjust;
            this.toolStripStatusTarjeta.Name = "toolStripStatusTarjeta";
            this.toolStripStatusTarjeta.Size = new System.Drawing.Size(315, 19);
            this.toolStripStatusTarjeta.Spring = true;
            this.toolStripStatusTarjeta.Text = "Speed Card : Non Connected";
            // 
            // toolStripStatusProcessComputer
            // 
            this.toolStripStatusProcessComputer.BackColor = System.Drawing.Color.Red;
            this.toolStripStatusProcessComputer.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusProcessComputer.BorderStyle = System.Windows.Forms.Border3DStyle.Adjust;
            this.toolStripStatusProcessComputer.Name = "toolStripStatusProcessComputer";
            this.toolStripStatusProcessComputer.Size = new System.Drawing.Size(315, 19);
            this.toolStripStatusProcessComputer.Spring = true;
            this.toolStripStatusProcessComputer.Text = "Process Computer : Non Connected";
            // 
            // timerEstado
            // 
            this.timerEstado.Interval = 500;
            this.timerEstado.Tick += new System.EventHandler(this.timerEstado_Tick);
            // 
            // Menu
            // 
            this.Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MeplateMenu,
            this.MenuConfiguracion});
            this.Menu.Location = new System.Drawing.Point(0, 0);
            this.Menu.Name = "Menu";
            this.Menu.Size = new System.Drawing.Size(1276, 24);
            this.Menu.TabIndex = 27;
            this.Menu.Text = "menuStrip1";
            // 
            // MeplateMenu
            // 
            this.MeplateMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iniciarToolStripMenuItem,
            this.pararToolStripMenuItem,
            this.toolStripSeparator1,
            this.forceMeasurementToolStripMenuItem1});
            this.MeplateMenu.Name = "MeplateMenu";
            this.MeplateMenu.Size = new System.Drawing.Size(62, 20);
            this.MeplateMenu.Text = "Meplate";
            // 
            // iniciarToolStripMenuItem
            // 
            this.iniciarToolStripMenuItem.Enabled = false;
            this.iniciarToolStripMenuItem.Name = "iniciarToolStripMenuItem";
            this.iniciarToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.iniciarToolStripMenuItem.Text = "Start";
            this.iniciarToolStripMenuItem.Click += new System.EventHandler(this.iniciarToolStripMenuItem_Click);
            // 
            // pararToolStripMenuItem
            // 
            this.pararToolStripMenuItem.Name = "pararToolStripMenuItem";
            this.pararToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.pararToolStripMenuItem.Text = "Stop";
            this.pararToolStripMenuItem.Click += new System.EventHandler(this.pararToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(176, 6);
            // 
            // forceMeasurementToolStripMenuItem1
            // 
            this.forceMeasurementToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.forceMeasurementToolStripMenuItem1.Name = "forceMeasurementToolStripMenuItem1";
            this.forceMeasurementToolStripMenuItem1.Size = new System.Drawing.Size(179, 22);
            this.forceMeasurementToolStripMenuItem1.Text = "Force Measurement";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.Enabled = false;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // MenuConfiguracion
            // 
            this.MenuConfiguracion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configuracionOffsetToolStripMenuItem,
            this.speedCardConnectionToolStripMenuItem});
            this.MenuConfiguracion.Name = "MenuConfiguracion";
            this.MenuConfiguracion.Size = new System.Drawing.Size(48, 20);
            this.MenuConfiguracion.Text = "Tools";
            // 
            // configuracionOffsetToolStripMenuItem
            // 
            this.configuracionOffsetToolStripMenuItem.Name = "configuracionOffsetToolStripMenuItem";
            this.configuracionOffsetToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.configuracionOffsetToolStripMenuItem.Text = "Offset Configuration";
            this.configuracionOffsetToolStripMenuItem.Click += new System.EventHandler(this.configuracionToolStripMenuItem_Click);
            // 
            // speedCardConnectionToolStripMenuItem
            // 
            this.speedCardConnectionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem});
            this.speedCardConnectionToolStripMenuItem.Name = "speedCardConnectionToolStripMenuItem";
            this.speedCardConnectionToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.speedCardConnectionToolStripMenuItem.Text = "SpeedCardConnection";
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_Validacion);
            this.groupBox2.Controls.Add(this.label_numdefect2);
            this.groupBox2.Controls.Add(this.label_numdefects1);
            this.groupBox2.Controls.Add(this.labeL_defectominimo2);
            this.groupBox2.Controls.Add(this.labeL_defectominimo1);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Location = new System.Drawing.Point(309, 38);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(415, 161);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "VALIDATION";
            // 
            // label_numdefects1
            // 
            this.label_numdefects1.AutoSize = true;
            this.label_numdefects1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_numdefects1.Location = new System.Drawing.Point(88, 118);
            this.label_numdefects1.Name = "label_numdefects1";
            this.label_numdefects1.Size = new System.Drawing.Size(14, 13);
            this.label_numdefects1.TabIndex = 20;
            this.label_numdefects1.Text = "0";
            // 
            // labeL_defectominimo2
            // 
            this.labeL_defectominimo2.AutoSize = true;
            this.labeL_defectominimo2.Location = new System.Drawing.Point(173, 61);
            this.labeL_defectominimo2.Name = "labeL_defectominimo2";
            this.labeL_defectominimo2.Size = new System.Drawing.Size(13, 13);
            this.labeL_defectominimo2.TabIndex = 19;
            this.labeL_defectominimo2.Text = "0";
            // 
            // labeL_defectominimo1
            // 
            this.labeL_defectominimo1.AutoSize = true;
            this.labeL_defectominimo1.Location = new System.Drawing.Point(69, 61);
            this.labeL_defectominimo1.Name = "labeL_defectominimo1";
            this.labeL_defectominimo1.Size = new System.Drawing.Size(13, 13);
            this.labeL_defectominimo1.TabIndex = 19;
            this.labeL_defectominimo1.Text = "0";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(9, 87);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(165, 13);
            this.label10.TabIndex = 18;
            this.label10.Text = " DEFECTS OVER MINIMUM";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Coral;
            this.label9.Location = new System.Drawing.Point(9, 31);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(184, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "MINIMUM DEFECT ACCEPTED";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(194, 61);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(25, 13);
            this.label14.TabIndex = 18;
            this.label14.Text = "mm";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(88, 61);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(25, 13);
            this.label12.TabIndex = 18;
            this.label12.Text = "mm";
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel5.BackgroundImage")));
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Location = new System.Drawing.Point(740, 265);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(14, 316);
            this.panel5.TabIndex = 24;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Meplate.Properties.Resources.logo4;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(804, 44);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(460, 126);
            this.panel1.TabIndex = 19;
            // 
            // label_numdefect2
            // 
            this.label_numdefect2.AutoSize = true;
            this.label_numdefect2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_numdefect2.Location = new System.Drawing.Point(194, 119);
            this.label_numdefect2.Name = "label_numdefect2";
            this.label_numdefect2.Size = new System.Drawing.Size(14, 13);
            this.label_numdefect2.TabIndex = 20;
            this.label_numdefect2.Text = "0";
            // 
            // button_Validacion
            // 
            this.button_Validacion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.button_Validacion.Enabled = false;
            this.button_Validacion.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button_Validacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_Validacion.Location = new System.Drawing.Point(262, 31);
            this.button_Validacion.Name = "button_Validacion";
            this.button_Validacion.Size = new System.Drawing.Size(129, 100);
            this.button_Validacion.TabIndex = 21;
            this.button_Validacion.Text = "OK";
            this.button_Validacion.UseVisualStyleBackColor = false;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(139, 61);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(39, 13);
            this.label15.TabIndex = 18;
            this.label15.Text = "2 m : ";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(24, 61);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(39, 13);
            this.label16.TabIndex = 18;
            this.label16.Text = "1 m : ";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(139, 119);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(39, 13);
            this.label17.TabIndex = 18;
            this.label17.Text = "2 m : ";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(24, 119);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(39, 13);
            this.label18.TabIndex = 18;
            this.label18.Text = "1 m : ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1276, 614);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this._MeplatestatusStrip1);
            this.Controls.Add(this.Menu);
            this.Controls.Add(this.label_escala_inter);
            this.Controls.Add(this.label_escala_max);
            this.Controls.Add(this.label_escala_min);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this._listViewPuntos);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.VentanaHalconPrincipal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.Menu;
            this.Name = "Form1";
            this.Text = "MEPLATE";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this._MeplatestatusStrip1.ResumeLayout(false);
            this._MeplatestatusStrip1.PerformLayout();
            this.Menu.ResumeLayout(false);
            this.Menu.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HalconDotNet.HWindowControl VentanaHalconPrincipal;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView _listViewPuntos;
        private System.Windows.Forms.ColumnHeader DEFECT;
        private System.Windows.Forms.ColumnHeader X;
        private System.Windows.Forms.ColumnHeader Y;
        private System.Windows.Forms.ColumnHeader Z;
        private System.Windows.Forms.ColumnHeader X2;
        private System.Windows.Forms.ColumnHeader Y2;
        private System.Windows.Forms.ColumnHeader Z2;
        private System.Windows.Forms.ColumnHeader DIFF;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label_escala_min;
        private System.Windows.Forms.Label label_escala_max;
        private System.Windows.Forms.Label label_escala_inter;
        private System.Windows.Forms.StatusStrip _MeplatestatusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusSpeed;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusTarjeta;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusProcessComputer;
        private System.Windows.Forms.Label label_Width;
        private System.Windows.Forms.Label label_Lenght;
        private System.Windows.Forms.Label label_ID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Timer timerEstado;
        private System.Windows.Forms.MenuStrip Menu;
        private System.Windows.Forms.ToolStripMenuItem MenuConfiguracion;
        private System.Windows.Forms.ToolStripMenuItem configuracionOffsetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MeplateMenu;
        private System.Windows.Forms.ToolStripMenuItem iniciarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pararToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speedCardConnectionToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusframerate;
        private System.Windows.Forms.Label label_numdefects1;
        private System.Windows.Forms.Label labeL_defectominimo1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem forceMeasurementToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.Label label_THICKNESS;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label labeL_defectominimo2;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label_numdefect2;
        private System.Windows.Forms.Button button_Validacion;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label15;

    }
}

