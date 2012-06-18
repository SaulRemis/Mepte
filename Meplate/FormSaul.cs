using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpinPlatform_Controls;
using SpinPlatform.Data;
using SpinPlatform.Dispatcher;
using System.Dynamic;
using SpinPlatform.Sensors.Meplaca;

namespace Meplate
{
    delegate void delegatePintarresultadossaul(dynamic result);
    public partial class FormSaul : Form
    {
        Meplate _Meplate;
        delegatePintarresultadossaul d_PintarResultados;  //puntero a la funcion de pintar
       
        public FormSaul()
        {
            dynamic ConfigData = new ExpandoObject();
            InitializeComponent();
            _Meplate = new Meplate();
            _Meplate.Init(ConfigData);
            _Meplate.NewResultEvent += new ResultEventHandler(_Meplate_NewResultEvent);
            d_PintarResultados = new delegatePintarresultadossaul(PintarResultados);


            VentanaHalconPrincipal.HalconWindow.SetLineWidth(2);
            VentanaHalconPrincipal.HalconWindow.SetLut("temperature");

            _Meplate.Start();
            timerEstado.Enabled = true;
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

        void PintarResultados(dynamic datos)
        {
            if (datos.MEPErrors == "")
            {
                foreach (string data in datos.MEPReturnedData)
                {
                    switch (data)
                    {
                        case "Resultados":
                            #region resultados

                            Resultados resultados = datos.MEPResultados;
                            if (resultados.Perfiles > 0)
                            {
                                double[,] pixeles = resultados.Pixeles;
                                double[,] puntos = resultados.Puntos;
                                int ancho, alto;
                                resultados.Z.GetImageSize(out ancho, out alto);
                                // perfiles.Text = ancho.ToString() + " perfiles";

                                VentanaHalconPrincipal.HalconWindow.SetPart(0, 0, alto - 1, ancho - 1);

                                resultados.Z.SetGrayval(0, 0, resultados.Distancia_nominal - 50);  // para fijar la escala de colores
                                label_escala_min.Text = (resultados.Distancia_nominal - 50).ToString();

                                resultados.Z.SetGrayval(alto - 1, ancho - 1, resultados.Distancia_nominal + 50); // para fijar la escala de valores
                                label_escala_max.Text = (resultados.Distancia_nominal + 50).ToString() + " mm";
                                label_escala_inter.Text = ((resultados.Distancia_nominal + 50) / 2).ToString() + " mm";

                                VentanaHalconPrincipal.HalconWindow.DispObj(resultados.Z);

                                //pinto los defectos en el mapa

                                for (int i = 0; i < pixeles.Length / 5; i++)
                                {
                                    if (i < 5) VentanaHalconPrincipal.HalconWindow.SetColor("blue");
                                    else VentanaHalconPrincipal.HalconWindow.SetColor("green");
                                    VentanaHalconPrincipal.HalconWindow.DispLine(pixeles[i, 0], pixeles[i, 1], pixeles[i, 2], pixeles[i, 3]);
                                    VentanaHalconPrincipal.HalconWindow.SetColor("black");
                                    if (i < 5)
                                    {
                                        VentanaHalconPrincipal.HalconWindow.SetTposition((int)pixeles[i, 0], (int)pixeles[i, 1]);
                                        VentanaHalconPrincipal.HalconWindow.WriteString(pixeles[i, 4].ToString("F1"));
                                    }


                                }


                                // Añado los defectos al ListView

                                _listViewPuntos.Items.Clear();
                                for (int i = 0; i < puntos.Length / 7; i++)
                                {
                                    _listViewPuntos.Items.Add((i + 1).ToString(), i);
                                    if (i < 5) _listViewPuntos.Items[i].Group = _listViewPuntos.Groups[0];
                                    else _listViewPuntos.Items[i].Group = _listViewPuntos.Groups[1];
                                    for (int j = 0; j < 7; j++)
                                    {
                                        _listViewPuntos.Items[i].SubItems.Add(puntos[i, j].ToString("F0"));
                                    }
                                }

                                //Actualizo la informacion de la chapa
                                labelPlateId.MainText = resultados.ID;
                                labelLength.MainText = resultados.Longitud.ToString();
                                labelWidth.MainText = resultados.Ancho.ToString();
                                labelThickness.MainText = resultados.Thickness.ToString();
                                labelMinimum1.MainText = resultados.Tolerance1.ToString();
                                labelMinimum2.MainText = resultados.Tolerance2.ToString();
                                label1.MainText = resultados.NumDefects1.ToString();
                                label2.MainText = resultados.NumDefects2.ToString();

                                if (resultados.NumDefects1 + resultados.NumDefects2 == 0)
                                {
                                    spinBlackDataLabelControl5.PageStartColor = Color.DarkGreen;
                                    spinBlackDataLabelControl5.PageEndColor = Color.FromArgb(0, 192, 0);
                                    spinBlackDataLabelControl5.MainText = "OK";

                                }
                                else
                                {
                                    spinBlackDataLabelControl5.PageStartColor = Color.Maroon;
                                    spinBlackDataLabelControl5.PageEndColor = Color.Red;
                                    spinBlackDataLabelControl5.MainText = "NO";

                                }


                                //formPrincipal.VentanaHalconPrincipal.HalconWindow.DumpWindow("jpeg", "meplaca_lab"); 

                            }
                            #endregion
                            break;
                        case "Informacion":
                            #region informacion

                            Informacion informacion = datos.MEPInformacion;
                            labelLength.MainText = ((double)(informacion.Avance / 1000)).ToString("F02");
                            toolStripStatusframerate.Text = "FRAMERATE : " + informacion.Rate.ToString("F1") + " perfiles / sec";
                            #endregion

                            break;
                        default:
                            break;
                    }
                }
            }
        }

       
        void InicializarListView()
        {
            _listViewPuntos.Items.Clear();
            for (int i = 0; i < 5; i++)
            {
                _listViewPuntos.Items.Add((i + 1).ToString(), i);
                _listViewPuntos.Items[i].Group = _listViewPuntos.Groups[0];
                for (int j = 0; j < 7; j++)
                {
                    _listViewPuntos.Items[i].SubItems.Add("0");
                }
            }

        }

        private void timerEstado_Tick(object sender, EventArgs e)
        {
            if (_Meplate != null)
            {
                dynamic temp = new ExpandoObject();
                _Meplate.GetData(ref temp, "Estado");
                if (temp.MEPErrors == "")
                {
                    toolStripStatusSpeed.Text = "SPEED : " + (temp.MEPVelocidad / 100).ToString() + "  m/min";
                    if (temp.MEPMidiendo) toolStripStatusMidiendo.Text = "Measuring Plate";
                    else toolStripStatusMidiendo.Text = "Waiting for Plate";


                    if (temp.MEPOPConnected)
                    {
                        ToolStripConnectionPC.StatusConnection = spinConnectionStatus.connected;
                    }
                    else
                    {
                        ToolStripConnectionPC.StatusConnection = spinConnectionStatus.disconnected;

                    }
                    if (temp.MEPTarjetaConnected)
                    {
                        ToolStripConnectionSpeed.StatusConnection = spinConnectionStatus.connected;
                        connectToolStripMenuItem.Enabled = false;

                    }
                    else
                    {
                        ToolStripConnectionSpeed.StatusConnection = spinConnectionStatus.disconnected;
                        connectToolStripMenuItem.Enabled = true;

                    }


                }
            }
        }

        private void configuracionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dynamic temp = new ExpandoObject();
            _Meplate.GetData(ref temp, "Meplaca");
            Configuracion_Meplaca configuration = new Configuracion_Meplaca(temp.MEPMeplaca);
            configuration.ShowDialog();

        }

        private void pararToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timerEstado.Enabled = false;
            _Meplate.Stop();
            iniciarToolStripMenuItem.Enabled = true;
            pararToolStripMenuItem.Enabled = false;
            forceMeasurementToolStripMenuItem1.Enabled = false;
            MenuConfiguracion.Enabled = false;
        }

        private void iniciarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Meplate.Start();
            timerEstado.Enabled = true;
            iniciarToolStripMenuItem.Enabled = false;
            pararToolStripMenuItem.Enabled = true;
            forceMeasurementToolStripMenuItem1.Enabled = true;
            MenuConfiguracion.Enabled = true;
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dynamic temp = new ExpandoObject();
            _Meplate.SetData(ref temp, "ConectarTarjeta");
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dynamic temp = new ExpandoObject();

            startToolStripMenuItem.Enabled = false;
            stopToolStripMenuItem.Enabled = true;
            configuracionOffsetToolStripMenuItem.Enabled = false;
            _Meplate.SetData(ref temp, "EventoComenzarMedida");

        }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dynamic temp = new ExpandoObject();

            startToolStripMenuItem.Enabled = true;
            stopToolStripMenuItem.Enabled = false;
            configuracionOffsetToolStripMenuItem.Enabled = true;
            _Meplate.SetData(ref temp, "EventoFinalizarMedida");

        }

        private void FormSaul_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerEstado.Enabled = false;
            _Meplate.Stop();
        }
    }
}
