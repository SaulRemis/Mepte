namespace SpinPlatform.Sensors.Meplaca
{
    partial class Configuracion_Meplaca
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Configuracion_Meplaca));
            this.chart_meplaca = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timer_meplaca = new System.Windows.Forms.Timer(this.components);
            this.radioButton_distances = new System.Windows.Forms.RadioButton();
            this.radioButton_voltages = new System.Windows.Forms.RadioButton();
            this.groupBox_Source = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button_newoffset = new System.Windows.Forms.Button();
            this.textBox_newoffset = new System.Windows.Forms.TextBox();
            this.button_menos5 = new System.Windows.Forms.Button();
            this.button_mas5 = new System.Windows.Forms.Button();
            this.label_offset = new System.Windows.Forms.Label();
            this.label_distance = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label_voltage = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_medionumsensores = new System.Windows.Forms.Label();
            this.label_numsensores = new System.Windows.Forms.Label();
            this.label_sensor = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.trackBar_sensores = new System.Windows.Forms.TrackBar();
            this.button_sobreescribir = new System.Windows.Forms.Button();
            this.button_leerarchivo = new System.Windows.Forms.Button();
            this.button_ok = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chart_meplaca)).BeginInit();
            this.groupBox_Source.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_sensores)).BeginInit();
            this.SuspendLayout();
            // 
            // chart_meplaca
            // 
            chartArea1.Name = "ChartArea1";
            this.chart_meplaca.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chart_meplaca.Legends.Add(legend1);
            this.chart_meplaca.Location = new System.Drawing.Point(3, 43);
            this.chart_meplaca.Name = "chart_meplaca";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart_meplaca.Series.Add(series1);
            this.chart_meplaca.Size = new System.Drawing.Size(273, 227);
            this.chart_meplaca.TabIndex = 0;
            this.chart_meplaca.Text = "chart1";
            this.chart_meplaca.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart_meplaca_MouseDown);
            // 
            // timer_meplaca
            // 
            this.timer_meplaca.Enabled = true;
            this.timer_meplaca.Interval = 200;
            this.timer_meplaca.Tick += new System.EventHandler(this.timer_meplaca_Tick);
            // 
            // radioButton_distances
            // 
            this.radioButton_distances.AutoSize = true;
            this.radioButton_distances.Checked = true;
            this.radioButton_distances.Location = new System.Drawing.Point(24, 16);
            this.radioButton_distances.Name = "radioButton_distances";
            this.radioButton_distances.Size = new System.Drawing.Size(72, 17);
            this.radioButton_distances.TabIndex = 1;
            this.radioButton_distances.TabStop = true;
            this.radioButton_distances.Text = "Distances";
            this.radioButton_distances.UseVisualStyleBackColor = true;
            // 
            // radioButton_voltages
            // 
            this.radioButton_voltages.AutoSize = true;
            this.radioButton_voltages.Location = new System.Drawing.Point(119, 16);
            this.radioButton_voltages.Name = "radioButton_voltages";
            this.radioButton_voltages.Size = new System.Drawing.Size(66, 17);
            this.radioButton_voltages.TabIndex = 1;
            this.radioButton_voltages.Text = "Voltages";
            this.radioButton_voltages.UseVisualStyleBackColor = true;
            // 
            // groupBox_Source
            // 
            this.groupBox_Source.Controls.Add(this.radioButton_distances);
            this.groupBox_Source.Controls.Add(this.radioButton_voltages);
            this.groupBox_Source.Location = new System.Drawing.Point(47, 3);
            this.groupBox_Source.Name = "groupBox_Source";
            this.groupBox_Source.Size = new System.Drawing.Size(215, 43);
            this.groupBox_Source.TabIndex = 2;
            this.groupBox_Source.TabStop = false;
            this.groupBox_Source.Text = "Source";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.groupBox_Source);
            this.panel1.Controls.Add(this.chart_meplaca);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(294, 273);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.button_newoffset);
            this.panel2.Controls.Add(this.textBox_newoffset);
            this.panel2.Controls.Add(this.button_menos5);
            this.panel2.Controls.Add(this.button_mas5);
            this.panel2.Controls.Add(this.label_offset);
            this.panel2.Controls.Add(this.label_distance);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label_voltage);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label_medionumsensores);
            this.panel2.Controls.Add(this.label_numsensores);
            this.panel2.Controls.Add(this.label_sensor);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.trackBar_sensores);
            this.panel2.Location = new System.Drawing.Point(339, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(275, 273);
            this.panel2.TabIndex = 4;
            // 
            // button_newoffset
            // 
            this.button_newoffset.Location = new System.Drawing.Point(193, 224);
            this.button_newoffset.Name = "button_newoffset";
            this.button_newoffset.Size = new System.Drawing.Size(72, 23);
            this.button_newoffset.TabIndex = 4;
            this.button_newoffset.Text = "New Offset";
            this.button_newoffset.UseVisualStyleBackColor = true;
            this.button_newoffset.Click += new System.EventHandler(this.button_newoffset_Click);
            // 
            // textBox_newoffset
            // 
            this.textBox_newoffset.AcceptsReturn = true;
            this.textBox_newoffset.Location = new System.Drawing.Point(118, 227);
            this.textBox_newoffset.Name = "textBox_newoffset";
            this.textBox_newoffset.Size = new System.Drawing.Size(56, 20);
            this.textBox_newoffset.TabIndex = 3;
            this.textBox_newoffset.Text = "0";
            // 
            // button_menos5
            // 
            this.button_menos5.Location = new System.Drawing.Point(230, 195);
            this.button_menos5.Name = "button_menos5";
            this.button_menos5.Size = new System.Drawing.Size(35, 23);
            this.button_menos5.TabIndex = 2;
            this.button_menos5.Text = "-5";
            this.button_menos5.UseVisualStyleBackColor = true;
            this.button_menos5.Click += new System.EventHandler(this.button_menos5_Click);
            // 
            // button_mas5
            // 
            this.button_mas5.Location = new System.Drawing.Point(178, 195);
            this.button_mas5.Name = "button_mas5";
            this.button_mas5.Size = new System.Drawing.Size(35, 23);
            this.button_mas5.TabIndex = 2;
            this.button_mas5.Text = "+5";
            this.button_mas5.UseVisualStyleBackColor = true;
            this.button_mas5.Click += new System.EventHandler(this.button_mas5_Click);
            // 
            // label_offset
            // 
            this.label_offset.AutoSize = true;
            this.label_offset.Location = new System.Drawing.Point(139, 200);
            this.label_offset.Name = "label_offset";
            this.label_offset.Size = new System.Drawing.Size(13, 13);
            this.label_offset.TabIndex = 1;
            this.label_offset.Text = "0";
            // 
            // label_distance
            // 
            this.label_distance.AutoSize = true;
            this.label_distance.Location = new System.Drawing.Point(139, 161);
            this.label_distance.Name = "label_distance";
            this.label_distance.Size = new System.Drawing.Size(13, 13);
            this.label_distance.TabIndex = 1;
            this.label_distance.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 235);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(66, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "New Offset :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 197);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Offset :";
            // 
            // label_voltage
            // 
            this.label_voltage.AutoSize = true;
            this.label_voltage.Location = new System.Drawing.Point(139, 122);
            this.label_voltage.Name = "label_voltage";
            this.label_voltage.Size = new System.Drawing.Size(13, 13);
            this.label_voltage.TabIndex = 1;
            this.label_voltage.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(33, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Distance :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Voltage :";
            // 
            // label_medionumsensores
            // 
            this.label_medionumsensores.AutoSize = true;
            this.label_medionumsensores.Location = new System.Drawing.Point(128, 51);
            this.label_medionumsensores.Name = "label_medionumsensores";
            this.label_medionumsensores.Size = new System.Drawing.Size(19, 13);
            this.label_medionumsensores.TabIndex = 1;
            this.label_medionumsensores.Text = "18";
            // 
            // label_numsensores
            // 
            this.label_numsensores.AutoSize = true;
            this.label_numsensores.Location = new System.Drawing.Point(223, 51);
            this.label_numsensores.Name = "label_numsensores";
            this.label_numsensores.Size = new System.Drawing.Size(19, 13);
            this.label_numsensores.TabIndex = 1;
            this.label_numsensores.Text = "36";
            // 
            // label_sensor
            // 
            this.label_sensor.AutoSize = true;
            this.label_sensor.Location = new System.Drawing.Point(139, 83);
            this.label_sensor.Name = "label_sensor";
            this.label_sensor.Size = new System.Drawing.Size(13, 13);
            this.label_sensor.TabIndex = 1;
            this.label_sensor.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(13, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(33, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sensor :";
            // 
            // trackBar_sensores
            // 
            this.trackBar_sensores.LargeChange = 1;
            this.trackBar_sensores.Location = new System.Drawing.Point(36, 19);
            this.trackBar_sensores.Maximum = 36;
            this.trackBar_sensores.Name = "trackBar_sensores";
            this.trackBar_sensores.Size = new System.Drawing.Size(209, 45);
            this.trackBar_sensores.TabIndex = 0;
            // 
            // button_sobreescribir
            // 
            this.button_sobreescribir.Location = new System.Drawing.Point(200, 305);
            this.button_sobreescribir.Name = "button_sobreescribir";
            this.button_sobreescribir.Size = new System.Drawing.Size(157, 23);
            this.button_sobreescribir.TabIndex = 5;
            this.button_sobreescribir.Text = "WRITE OFFSETS TO FILE";
            this.button_sobreescribir.UseVisualStyleBackColor = true;
            this.button_sobreescribir.Click += new System.EventHandler(this.button_sobreescribir_Click);
            // 
            // button_leerarchivo
            // 
            this.button_leerarchivo.Location = new System.Drawing.Point(15, 305);
            this.button_leerarchivo.Name = "button_leerarchivo";
            this.button_leerarchivo.Size = new System.Drawing.Size(166, 23);
            this.button_leerarchivo.TabIndex = 6;
            this.button_leerarchivo.Text = "READ OFFSETS FROM FILE";
            this.button_leerarchivo.UseVisualStyleBackColor = true;
            this.button_leerarchivo.Click += new System.EventHandler(this.button_leerarchivo_Click);
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(532, 305);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 7;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // Configuracion_Meplaca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(643, 340);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.button_leerarchivo);
            this.Controls.Add(this.button_sobreescribir);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Configuracion_Meplaca";
            this.Text = "Sensor Offset Configuration";
            ((System.ComponentModel.ISupportInitialize)(this.chart_meplaca)).EndInit();
            this.groupBox_Source.ResumeLayout(false);
            this.groupBox_Source.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_sensores)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart_meplaca;
        private System.Windows.Forms.Timer timer_meplaca;
        private System.Windows.Forms.RadioButton radioButton_distances;
        private System.Windows.Forms.RadioButton radioButton_voltages;
        private System.Windows.Forms.GroupBox groupBox_Source;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trackBar_sensores;
        private System.Windows.Forms.Label label_numsensores;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_medionumsensores;
        private System.Windows.Forms.Label label_offset;
        private System.Windows.Forms.Label label_distance;
        private System.Windows.Forms.Label label_voltage;
        private System.Windows.Forms.Label label_sensor;
        private System.Windows.Forms.Button button_menos5;
        private System.Windows.Forms.Button button_mas5;
        private System.Windows.Forms.Button button_newoffset;
        private System.Windows.Forms.TextBox textBox_newoffset;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button_sobreescribir;
        private System.Windows.Forms.Button button_leerarchivo;
        private System.Windows.Forms.Button button_ok;
    }
}