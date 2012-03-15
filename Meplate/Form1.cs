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
                this.BeginInvoke(d_PintarResultados, res.DataArgs);
            }
            catch (Exception)
            {
                

            }
        }

        void PintarResultados(MeplateData datos)
        {
            if (datos.GetResultados)
            {
                #region resultados

                Resultados resultados = datos.Resultados;
                if (resultados.Perfiles > 0)
                {
                    double[,] pixeles = resultados.Pixeles;
                    double[,] puntos = resultados.Puntos;
                    int ancho, alto;
                    resultados.Z.GetImageSize(out ancho, out alto);


                    perfiles.Text = ancho.ToString() + " perfiles";

                    VentanaHalconPrincipal.HalconWindow.SetPart(0, 0, alto - 1, ancho - 1);

                    resultados.Z.SetGrayval(0, 0, resultados.Distancia_nominal - 30);  // para fijar la escala de colores
                    resultados.Z.SetGrayval(alto - 1, ancho - 1, resultados.Distancia_nominal + 10); // para fijar la escala de valores


                    VentanaHalconPrincipal.HalconWindow.DispObj(resultados.Z);

                    //pinto los defectos en el mapa

                    for (int i = 0; i < pixeles.Length / 5; i++)
                    {
                        VentanaHalconPrincipal.HalconWindow.SetColor("blue");
                        VentanaHalconPrincipal.HalconWindow.DispLine(pixeles[i, 0], pixeles[i,1], pixeles[i,2], pixeles[i,3]);
                        VentanaHalconPrincipal.HalconWindow.SetColor("black");
                        VentanaHalconPrincipal.HalconWindow.SetTposition((int)pixeles[i,2], (int)pixeles[i,3]);
                        VentanaHalconPrincipal.HalconWindow.WriteString(pixeles[i,4].ToString("F1"));

                    }


                    // Añado los defectos al ListView

                    _listViewPuntos.Items.Clear();
                    for (int i = 0; i < puntos.Length / 7; i++)
                    {
                        _listViewPuntos.Items.Add((i + 1).ToString(), i);
                        _listViewPuntos.Items[i].Group = _listViewPuntos.Groups[0];
                        for (int j = 0; j < 7; j++)
                        {
                            _listViewPuntos.Items[i].SubItems.Add(puntos[i,j].ToString("F2"));
                        }
                    }



                    //formPrincipal.VentanaHalconPrincipal.HalconWindow.DumpWindow("jpeg", "meplaca_lab"); 

                } 
                #endregion
            }
            if (datos.GetInformacion)
            {
                #region informacion

                Informacion informacion = datos.Informacion;
                if (informacion.Perfiles > 0)
                {
                    perfiles.Text = informacion.Perfiles.ToString() + " perfiles";
                    _LabelFrameRate.Text = informacion.Rate.ToString("F1") + " perfiles / sec";

                }
                #endregion
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

        void InicializarListView()
        {
            _listViewPuntos.Items.Clear();
            for (int i = 0; i < 5; i++)
            {
                 _listViewPuntos.Items.Add((i+1).ToString(), i);
                 _listViewPuntos.Items[i].Group = _listViewPuntos.Groups[0];
                 for (int j = 0; j <7; j++)
                 {
                     _listViewPuntos.Items[i].SubItems.Add("0");
                 }
            }

        }
    }
}
