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
            this.perfiles = new System.Windows.Forms.Label();
            this.MedirPararButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.StartButton = new System.Windows.Forms.Button();
            this.VentanaHalconPrincipal = new HalconDotNet.HWindowControl();
            this.SuspendLayout();
            // 
            // perfiles
            // 
            this.perfiles.AutoSize = true;
            this.perfiles.Location = new System.Drawing.Point(281, 34);
            this.perfiles.Name = "perfiles";
            this.perfiles.Size = new System.Drawing.Size(40, 13);
            this.perfiles.TabIndex = 16;
            this.perfiles.Text = "perfiles";
            // 
            // MedirPararButton
            // 
            this.MedirPararButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.MedirPararButton.Enabled = false;
            this.MedirPararButton.Location = new System.Drawing.Point(158, 25);
            this.MedirPararButton.Name = "MedirPararButton";
            this.MedirPararButton.Size = new System.Drawing.Size(75, 23);
            this.MedirPararButton.TabIndex = 15;
            this.MedirPararButton.Text = "Medir";
            this.MedirPararButton.UseVisualStyleBackColor = false;
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
            this.StopButton.Location = new System.Drawing.Point(64, 12);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(48, 48);
            this.StopButton.TabIndex = 14;
            this.StopButton.UseVisualStyleBackColor = false;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
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
            this.StartButton.Location = new System.Drawing.Point(10, 12);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(48, 48);
            this.StartButton.TabIndex = 13;
            this.StartButton.UseVisualStyleBackColor = false;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // VentanaHalconPrincipal
            // 
            this.VentanaHalconPrincipal.BackColor = System.Drawing.Color.Black;
            this.VentanaHalconPrincipal.BorderColor = System.Drawing.Color.Black;
            this.VentanaHalconPrincipal.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.VentanaHalconPrincipal.Location = new System.Drawing.Point(9, 88);
            this.VentanaHalconPrincipal.Name = "VentanaHalconPrincipal";
            this.VentanaHalconPrincipal.Size = new System.Drawing.Size(792, 314);
            this.VentanaHalconPrincipal.TabIndex = 12;
            this.VentanaHalconPrincipal.WindowSize = new System.Drawing.Size(792, 314);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 433);
            this.Controls.Add(this.perfiles);
            this.Controls.Add(this.MedirPararButton);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.VentanaHalconPrincipal);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label perfiles;
        private System.Windows.Forms.Button MedirPararButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button StartButton;
        private HalconDotNet.HWindowControl VentanaHalconPrincipal;

    }
}

