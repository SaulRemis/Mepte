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
        Meplate _Meplate;
        delegatePintarresultados d_PintarResultados;  //puntero a la funcion de pintar

        public Form1()
        {
            InitializeComponent();
            _Meplate = new Meplate();
            _Meplate.NewResultEvent += new ResultEventHandler(_Meplate_NewResultEvent);
            d_PintarResultados = new delegatePintarresultados(PintarResultados);


            VentanaHalconPrincipal.HalconWindow.SetLineWidth(3);
            VentanaHalconPrincipal.HalconWindow.SetLut("temperature");


      
        }

        void _Meplate_NewResultEvent(object sender, DataEventArgs res)
        {
            try
            {
                this.Invoke(d_PintarResultados, res.DataArgs);
            }
            catch (Exception)
            {
                

            }
        }

        void PintarResultados(MeplateData resultados)
        {
            if (resultados.GetResultados)
            {
                if (resultados.Perfiles > 0)
                {
                    double[,] puntos = resultados.Pixeles;
                    int ancho, alto;
                    resultados.Z.GetImageSize(out ancho, out alto);


                    perfiles.Text = ancho.ToString() + " perfiles";

                    VentanaHalconPrincipal.HalconWindow.SetPart(0, 0, alto - 1, ancho - 1);

                    resultados.Z.SetGrayval(0, 0, resultados.Distancia_nominal - 100);  // para fijar la escala de colores
                    resultados.Z.SetGrayval(alto - 1, ancho - 1, resultados.Distancia_nominal + 10); // para fijar la escala de valores


                    VentanaHalconPrincipal.HalconWindow.DispObj(resultados.Z);



                    for (int i = 0; i < puntos.Length / 5; i++)
                    {
                        VentanaHalconPrincipal.HalconWindow.SetColor("blue");
                        VentanaHalconPrincipal.HalconWindow.DispLine(puntos[0, i], puntos[1, i], puntos[2, i], puntos[3, i]);
                        VentanaHalconPrincipal.HalconWindow.SetColor("black");
                        VentanaHalconPrincipal.HalconWindow.SetTposition((int)puntos[2, i], (int)puntos[3, i]);
                        VentanaHalconPrincipal.HalconWindow.WriteString(puntos[4, i].ToString("F1"));

                    }
                    //formPrincipal.VentanaHalconPrincipal.HalconWindow.DumpWindow("jpeg", "meplaca_lab");
                } 
            }
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            _Meplate.Start();

            StartButton.Enabled = false;
            StopButton.Enabled = true;
            MedirPararButton.Enabled = true;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            _Meplate.Stop();

            MedirPararButton.Text = "Medir";
            StopButton.Enabled = false;
            StartButton.Enabled = true;
            MedirPararButton.Enabled = false;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _Meplate.Stop();
        }

        private void MedirPararButton_Click(object sender, EventArgs e)
        {
            MeplateData temp = new MeplateData();

            if (MedirPararButton.Text == "Medir")
            {
                MedirPararButton.Text = "Parar";
                temp.SetEventoEmpezarMedida = true;

            }

            else
            {
                MedirPararButton.Text = "Medir";
                temp.SetEventoFinalizarMedida = true;
            }

            _Meplate.SetData(temp);
        }
    }
}
