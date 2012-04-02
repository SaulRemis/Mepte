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

namespace Meplate
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
      
        }
    }
}
