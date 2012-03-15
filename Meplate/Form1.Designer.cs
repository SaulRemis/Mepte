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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("1 m RULER", System.Windows.Forms.HorizontalAlignment.Center);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("2 m RULER", System.Windows.Forms.HorizontalAlignment.Left);
            this.perfiles = new System.Windows.Forms.Label();
            this.MedirPararButton = new System.Windows.Forms.Button();
            this.VentanaHalconPrincipal = new HalconDotNet.HWindowControl();
            this._LabelFrameRate = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.StartButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this._listViewPuntos = new System.Windows.Forms.ListView();
            this.X = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Y = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Z = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.X2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Y2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Z2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DIFF = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.DEFECT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // perfiles
            // 
            this.perfiles.AutoSize = true;
            this.perfiles.Location = new System.Drawing.Point(55, 111);
            this.perfiles.Name = "perfiles";
            this.perfiles.Size = new System.Drawing.Size(40, 13);
            this.perfiles.TabIndex = 16;
            this.perfiles.Text = "perfiles";
            // 
            // MedirPararButton
            // 
            this.MedirPararButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.MedirPararButton.Enabled = false;
            this.MedirPararButton.Location = new System.Drawing.Point(211, 32);
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
            this.VentanaHalconPrincipal.Location = new System.Drawing.Point(12, 343);
            this.VentanaHalconPrincipal.Name = "VentanaHalconPrincipal";
            this.VentanaHalconPrincipal.Size = new System.Drawing.Size(738, 314);
            this.VentanaHalconPrincipal.TabIndex = 12;
            this.VentanaHalconPrincipal.WindowSize = new System.Drawing.Size(738, 314);
            // 
            // _LabelFrameRate
            // 
            this._LabelFrameRate.AutoSize = true;
            this._LabelFrameRate.Location = new System.Drawing.Point(208, 111);
            this._LabelFrameRate.Name = "_LabelFrameRate";
            this._LabelFrameRate.Size = new System.Drawing.Size(62, 13);
            this._LabelFrameRate.TabIndex = 17;
            this._LabelFrameRate.Text = "perfiles/sec";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.StartButton);
            this.groupBox1.Controls.Add(this._LabelFrameRate);
            this.groupBox1.Controls.Add(this.StopButton);
            this.groupBox1.Controls.Add(this.perfiles);
            this.groupBox1.Controls.Add(this.MedirPararButton);
            this.groupBox1.Location = new System.Drawing.Point(12, 157);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(329, 161);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Control";
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
            // StartButton
            // 
            this.StartButton.BackColor = System.Drawing.Color.Transparent;
            this.StartButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StartButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.StartButton.FlatAppearance.BorderSize = 0;
            this.StartButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.StartButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.StartButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartButton.Image = global::Meplate.Properties.Resources.icon_play;
            this.StartButton.Location = new System.Drawing.Point(6, 19);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(48, 48);
            this.StartButton.TabIndex = 13;
            this.StartButton.UseVisualStyleBackColor = false;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.BackColor = System.Drawing.Color.Transparent;
            this.StopButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.StopButton.Enabled = false;
            this.StopButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.StopButton.FlatAppearance.BorderSize = 0;
            this.StopButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.StopButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.StopButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StopButton.Image = global::Meplate.Properties.Resources.icon_stop;
            this.StopButton.Location = new System.Drawing.Point(95, 19);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(48, 48);
            this.StopButton.TabIndex = 14;
            this.StopButton.UseVisualStyleBackColor = false;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
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
            // _listViewPuntos
            // 
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
            listViewGroup2.Name = "listViewGroup2m";
            this._listViewPuntos.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            this._listViewPuntos.Location = new System.Drawing.Point(379, 157);
            this._listViewPuntos.Name = "_listViewPuntos";
            this._listViewPuntos.Size = new System.Drawing.Size(435, 173);
            this._listViewPuntos.TabIndex = 21;
            this._listViewPuntos.UseCompatibleStateImageBehavior = false;
            this._listViewPuntos.View = System.Windows.Forms.View.Details;
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
            this.Y.Width = 50;
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
            this.Y2.Width = 50;
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
            // DEFECT
            // 
            this.DEFECT.Text = "DEFECT";
            this.DEFECT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DEFECT.Width = 70;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(826, 668);
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label perfiles;
        private System.Windows.Forms.Button MedirPararButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button StartButton;
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

    }
}

