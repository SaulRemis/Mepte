using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpinPlatform.Data;
using SpinPlatform.Dispatcher;

namespace Meplate
{
    delegate void delegatePintarresultados(MeplateData result);
    public partial class Form1 : Form
    {
        Meplate meplate;
        delegatePintarresultados d_PintarResultados;  //puntero a la funcion de pintar

        public Form1()
        {
            meplate = new Meplate();


            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            meplate.Start();

            StartButton.Enabled = false;
            StopButton.Enabled = true;
            MedirPararButton.Enabled = true;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            meplate.Stop();

            MedirPararButton.Text = "Medir";
            StopButton.Enabled = false;
            StartButton.Enabled = true;
            MedirPararButton.Enabled = false;
        }
    }
}
