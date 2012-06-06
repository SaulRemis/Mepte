using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Dynamic;
using SpinPlatform.Sensors.Meplaca;
using System.Windows.Forms.DataVisualization.Charting;

namespace SpinPlatform.Sensors.Meplaca
{
    public partial class Configuracion_Meplaca : Form
    {
        CMeplaca _Meplaca;
        dynamic Aux_meplaca;
        int _NumeroModulos;
        double[] distancias;
        int[] tensiones;
        UInt16[] offset;



        public Configuracion_Meplaca(CMeplaca meplaca)
        {
            InitializeComponent();
            _Meplaca = meplaca;
            Aux_meplaca = new ExpandoObject();
            _Meplaca.GetData(ref Aux_meplaca, "NumeroModulos");
            _NumeroModulos = Aux_meplaca.MEPNumeroModulos;
            trackBar_sensores.Maximum = _NumeroModulos*6-1;
            label_numsensores.Text =(_NumeroModulos*6-1).ToString();
            label_medionumsensores.Text = ((int)Math.Round((decimal)(_NumeroModulos*6/2))).ToString();
        }

        private void timer_meplaca_Tick(object sender, EventArgs e)
        {
            _Meplaca.GetData(ref Aux_meplaca, "UltimaMedida");
            _Meplaca.GetData(ref Aux_meplaca, "UltimaTension");
            _Meplaca.GetData(ref Aux_meplaca, "Offset");

            distancias = Aux_meplaca.MEPUltimoPerfil;
            tensiones = Aux_meplaca.MEPUltimaTension;
            offset = Aux_meplaca.MEPOffset;

            if (radioButton_distances.Checked)
            {
                chart_meplaca.Series[0].Points.DataBindY(distancias);
            }
            else
            {
                chart_meplaca.Series[0].Points.DataBindY(tensiones);            
            }

            label_sensor.Text = trackBar_sensores.Value.ToString();
            label_voltage.Text = tensiones[trackBar_sensores.Value].ToString();
            label_distance.Text = distancias[trackBar_sensores.Value].ToString("F02");
            label_offset.Text = offset[trackBar_sensores.Value].ToString();
            chart_meplaca.Series[0].Points[trackBar_sensores.Value].Color = Color.Red;
           // chart_meplaca.Series[1].Points.AddXY(0,double.Parse(textBox_reference.Text));
            //chart_meplaca.Series[1].Points.AddXY(_NumeroModulos * 6 - 1, double.Parse(textBox_reference.Text));
      
        }

        private void button_mas5_Click(object sender, EventArgs e)
        {
            Aux_meplaca.MEPModulo =Math.Truncate((decimal)trackBar_sensores.Value / 6);
            Aux_meplaca.MEPSensor = trackBar_sensores.Value % 6;
            Aux_meplaca.MEPOffset = offset[trackBar_sensores.Value] + 5;
            _Meplaca.SetData(ref Aux_meplaca, "EnviarOffsetsSensor");
        }

        private void button_menos5_Click(object sender, EventArgs e)
        {
            Aux_meplaca.MEPModulo = Math.Truncate((decimal)trackBar_sensores.Value / 6);
            Aux_meplaca.MEPSensor = trackBar_sensores.Value % 6;
            Aux_meplaca.MEPOffset = offset[trackBar_sensores.Value] - 5;
            _Meplaca.SetData(ref Aux_meplaca, "EnviarOffsetsSensor");
        }

        private void chart_meplaca_MouseDown(object sender, MouseEventArgs e)
        {
            HitTestResult result = chart_meplaca.HitTest(e.X, e.Y);
            if (result.PointIndex>=trackBar_sensores.Minimum&&result.PointIndex<=trackBar_sensores.Maximum)
            trackBar_sensores.Value = result.PointIndex;

        }

        private void button_newoffset_Click(object sender, EventArgs e)
        {
            Aux_meplaca.MEPModulo = Math.Truncate((decimal)trackBar_sensores.Value / 6);
            Aux_meplaca.MEPSensor = trackBar_sensores.Value % 6;
            Aux_meplaca.MEPOffset = UInt16.Parse(textBox_newoffset.Text);
            _Meplaca.SetData(ref Aux_meplaca, "EnviarOffsetsSensor");
        }

        private void button_sobreescribir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to overwrite the original offsets?","WARNING",MessageBoxButtons.YesNo,MessageBoxIcon.Question)  ==DialogResult.Yes)     _Meplaca.SetData(ref Aux_meplaca, "ActualizaArchivoOffset");
        }

        private void button_leerarchivo_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure to go back to the original offsets?", "WARNING", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) _Meplaca.SetData(ref Aux_meplaca, "LeerOffsetsArchivo");
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Auto_Click(object sender, EventArgs e)
        {
            double[] Referencias = new double[_NumeroModulos * 6];
            double[] medidas = new double[_NumeroModulos * 6];
            medidas=(double[])Aux_meplaca.MEPUltimoPerfil; 

            double valor = double.Parse(textBox_value.Text);
            double limder = double.Parse(textBox_limder.Text);
            double limizq = double.Parse(textBox_limizq.Text);

            _Meplaca.GetData(ref Aux_meplaca, "UltimaMedida");

            for (int i = 0; i < Referencias.Length; i++)
            {
                if ((i >= limizq) && (i <= limder))
                {
                    Referencias[i] = valor;
                }
                else
                {
                    medidas[i] = 0;
                    Referencias[i] = 0;
                }
            }


            Aux_meplaca.MEPOffsets = medidas; 
            Aux_meplaca.MEPReferencias = Referencias;
            _Meplaca.SetData(ref Aux_meplaca, "EnviarOffsets");
        }
    }
}
