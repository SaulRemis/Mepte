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
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("1 m RULER", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("2 m RULER", System.Windows.Forms.HorizontalAlignment.Center);
            this.perfiles = new System.Windows.Forms.Label();
            this.MedirPararButton = new System.Windows.Forms.Button();
            this.VentanaHalconPrincipal = new HalconDotNet.HWindowControl();
            this._LabelFrameRate = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label_Width = new System.Windows.Forms.Label();
            this.label_Lenght = new System.Windows.Forms.Label();
            this.label_ID = new System.Windows.Forms.Label();
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
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label_escala_min = new System.Windows.Forms.Label();
            this.label_escala_max = new System.Windows.Forms.Label();
            this.label_escala_inter = new System.Windows.Forms.Label();
            this._MeplatestatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusSpeed = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusTarjeta = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusProcessComputer = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerEstado = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this._MeplatestatusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // perfiles
            // 
            this.perfiles.AutoSize = true;
            this.perfiles.Location = new System.Drawing.Point(34, 138);
            this.perfiles.Name = "perfiles";
            this.perfiles.Size = new System.Drawing.Size(40, 13);
            this.perfiles.TabIndex = 16;
            this.perfiles.Text = "perfiles";
            // 
            // MedirPararButton
            // 
            this.MedirPararButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.MedirPararButton.Location = new System.Drawing.Point(254, 138);
            this.MedirPararButton.Name = "MedirPararButton";
            this.MedirPararButton.Size = new System.Drawing.Size(75, 23);
            this.MedirPararButton.TabIndex = 15;
            this.MedirPararButton.Text = "Medir";
            this.MedirPararButton.UseVisualStyleBackColor = false;
            this.MedirPararButton.Click += new System.EventHandler(this.MedirPararButton_Click);
            // 
            // VentanaHalconPrincipal
            // 
            this.VentanaHalconPrincipal.BackColor = System.Drawing.Color.Black;
            this.VentanaHalconPrincipal.BorderColor = System.Drawing.Color.Black;
            this.VentanaHalconPrincipal.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.VentanaHalconPrincipal.Location = new System.Drawing.Point(12, 372);
            this.VentanaHalconPrincipal.Name = "VentanaHalconPrincipal";
            this.VentanaHalconPrincipal.Size = new System.Drawing.Size(738, 314);
            this.VentanaHalconPrincipal.TabIndex = 12;
            this.VentanaHalconPrincipal.WindowSize = new System.Drawing.Size(738, 314);
            // 
            // _LabelFrameRate
            // 
            this._LabelFrameRate.AutoSize = true;
            this._LabelFrameRate.Location = new System.Drawing.Point(133, 138);
            this._LabelFrameRate.Name = "_LabelFrameRate";
            this._LabelFrameRate.Size = new System.Drawing.Size(62, 13);
            this._LabelFrameRate.TabIndex = 17;
            this._LabelFrameRate.Text = "perfiles/sec";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_Width);
            this.groupBox1.Controls.Add(this.label_Lenght);
            this.groupBox1.Controls.Add(this.label_ID);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this._LabelFrameRate);
            this.groupBox1.Controls.Add(this.perfiles);
            this.groupBox1.Controls.Add(this.MedirPararButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 157);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(329, 161);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "INFORMATION";
            // 
            // label_Width
            // 
            this.label_Width.AutoSize = true;
            this.label_Width.Location = new System.Drawing.Point(91, 89);
            this.label_Width.Name = "label_Width";
            this.label_Width.Size = new System.Drawing.Size(13, 13);
            this.label_Width.TabIndex = 19;
            this.label_Width.Text = "0";
            // 
            // label_Lenght
            // 
            this.label_Lenght.AutoSize = true;
            this.label_Lenght.Location = new System.Drawing.Point(91, 61);
            this.label_Lenght.Name = "label_Lenght";
            this.label_Lenght.Size = new System.Drawing.Size(13, 13);
            this.label_Lenght.TabIndex = 19;
            this.label_Lenght.Text = "0";
            // 
            // label_ID
            // 
            this.label_ID.AutoSize = true;
            this.label_ID.Location = new System.Drawing.Point(91, 37);
            this.label_ID.Name = "label_ID";
            this.label_ID.Size = new System.Drawing.Size(13, 13);
            this.label_ID.TabIndex = 19;
            this.label_ID.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(152, 89);
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
            this.label7.Location = new System.Drawing.Point(152, 61);
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
            listViewGroup3.Header = "1 m RULER";
            listViewGroup3.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup3.Name = "listViewGroup1m";
            listViewGroup4.Header = "2 m RULER";
            listViewGroup4.HeaderAlignment = System.Windows.Forms.HorizontalAlignment.Center;
            listViewGroup4.Name = "listViewGroup2m";
            this._listViewPuntos.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup3,
            listViewGroup4});
            this._listViewPuntos.Location = new System.Drawing.Point(360, 174);
            this._listViewPuntos.Name = "_listViewPuntos";
            this._listViewPuntos.Size = new System.Drawing.Size(454, 144);
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
            this.label4.Location = new System.Drawing.Point(29, 336);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(117, 24);
            this.label4.TabIndex = 22;
            this.label4.Text = "DEFECTS :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(340, 336);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(111, 24);
            this.label5.TabIndex = 22;
            this.label5.Text = "1m RULER";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(555, 336);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(111, 24);
            this.label6.TabIndex = 22;
            this.label6.Text = "2m RULER";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.RoyalBlue;
            this.panel3.Location = new System.Drawing.Point(309, 336);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(25, 23);
            this.panel3.TabIndex = 23;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.GreenYellow;
            this.panel4.Location = new System.Drawing.Point(524, 336);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(25, 23);
            this.panel4.TabIndex = 23;
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = global::Meplate.Properties.Resources.escale1;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Location = new System.Drawing.Point(756, 370);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(14, 316);
            this.panel5.TabIndex = 24;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::Meplate.Properties.Resources.Diapositiva2;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Location = new System.Drawing.Point(607, 13);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(214, 126);
            this.panel2.TabIndex = 20;
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::Meplate.Properties.Resources.meplatelogo;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(18, 13);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(582, 126);
            this.panel1.TabIndex = 19;
            // 
            // label_escala_min
            // 
            this.label_escala_min.AutoSize = true;
            this.label_escala_min.Location = new System.Drawing.Point(776, 673);
            this.label_escala_min.Name = "label_escala_min";
            this.label_escala_min.Size = new System.Drawing.Size(32, 13);
            this.label_escala_min.TabIndex = 25;
            this.label_escala_min.Text = "0 mm";
            // 
            // label_escala_max
            // 
            this.label_escala_max.AutoSize = true;
            this.label_escala_max.Location = new System.Drawing.Point(776, 370);
            this.label_escala_max.Name = "label_escala_max";
            this.label_escala_max.Size = new System.Drawing.Size(38, 13);
            this.label_escala_max.TabIndex = 25;
            this.label_escala_max.Text = "60 mm";
            // 
            // label_escala_inter
            // 
            this.label_escala_inter.AutoSize = true;
            this.label_escala_inter.Location = new System.Drawing.Point(776, 532);
            this.label_escala_inter.Name = "label_escala_inter";
            this.label_escala_inter.Size = new System.Drawing.Size(38, 13);
            this.label_escala_inter.TabIndex = 25;
            this.label_escala_inter.Text = "30 mm";
            // 
            // _MeplatestatusStrip1
            // 
            this._MeplatestatusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusSpeed,
            this.toolStripStatusTarjeta,
            this.toolStripStatusProcessComputer});
            this._MeplatestatusStrip1.Location = new System.Drawing.Point(0, 674);
            this._MeplatestatusStrip1.Name = "_MeplatestatusStrip1";
            this._MeplatestatusStrip1.Size = new System.Drawing.Size(826, 24);
            this._MeplatestatusStrip1.TabIndex = 26;
            this._MeplatestatusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusSpeed
            // 
            this.toolStripStatusSpeed.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusSpeed.Name = "toolStripStatusSpeed";
            this.toolStripStatusSpeed.Size = new System.Drawing.Size(98, 19);
            this.toolStripStatusSpeed.Text = "Speed : 0 m/min";
            // 
            // toolStripStatusTarjeta
            // 
            this.toolStripStatusTarjeta.BackColor = System.Drawing.Color.Red;
            this.toolStripStatusTarjeta.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)
                        | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusTarjeta.BorderStyle = System.Windows.Forms.Border3DStyle.Adjust;
            this.toolStripStatusTarjeta.Name = "toolStripStatusTarjeta";
            this.toolStripStatusTarjeta.Size = new System.Drawing.Size(164, 19);
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
            this.toolStripStatusProcessComputer.Size = new System.Drawing.Size(201, 19);
            this.toolStripStatusProcessComputer.Text = "Process Computer : Non Connected";
            // 
            // timerEstado
            // 
            this.timerEstado.Interval = 500;
            this.timerEstado.Tick += new System.EventHandler(this.timerEstado_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(826, 698);
            this.Controls.Add(this._MeplatestatusStrip1);
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
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.VentanaHalconPrincipal);
            this.Name = "Form1";
            this.Text = "MEPLATE";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this._MeplatestatusStrip1.ResumeLayout(false);
            this._MeplatestatusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label perfiles;
        private System.Windows.Forms.Button MedirPararButton;
        private HalconDotNet.HWindowControl VentanaHalconPrincipal;
        private System.Windows.Forms.Label _LabelFrameRate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
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

    }
}

