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
using System.Dynamic;
using SpinPlatform.Sensors.Meplaca;

namespace Meplate
{
    delegate void delegatePintarresultados(dynamic result);
    public partial class Form1 : Form
    {
        Meplate _Meplate;
        delegatePintarresultados d_PintarResultados;  //puntero a la funcion de pintar
       

        public Form1()
        {
            dynamic ConfigData= new ExpandoObject() ;
            InitializeComponent();
            _Meplate = new Meplate();
            _Meplate.Init(ConfigData);
            _Meplate.NewResultEvent += new ResultEventHandler(_Meplate_NewResultEvent);
            d_PintarResultados = new delegatePintarresultados(PintarResultados);


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
                            label_escala_inter.Text = ((resultados.Distancia_nominal + 50)/2).ToString() + " mm";

                            VentanaHalconPrincipal.HalconWindow.DispObj(resultados.Z);

                            //pinto los defectos en el mapa

                            for (int i = 0; i < pixeles.Length / 5; i++)
                            {
                                if (i<5) VentanaHalconPrincipal.HalconWindow.SetColor("blue");
                                else VentanaHalconPrincipal.HalconWindow.SetColor("green");
                                VentanaHalconPrincipal.HalconWindow.DispLine(pixeles[i, 0], pixeles[i,1], pixeles[i,2], pixeles[i,3]);
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
                                if (i < 5)  _listViewPuntos.Items[i].Group = _listViewPuntos.Groups[0];
                                else _listViewPuntos.Items[i].Group = _listViewPuntos.Groups[1];
                                for (int j = 0; j < 7; j++)
                                {
                                    _listViewPuntos.Items[i].SubItems.Add(puntos[i,j].ToString("F0"));
                                }
                            }

                            //Actualizo la informacion de la chapa
                            label_ID.Text = resultados.ID;
                            label_Lenght.Text = resultados.Longitud.ToString();
                            label_Width.Text = resultados.Ancho.ToString();
                            label_THICKNESS.Text = resultados.Thickness.ToString();
                            labeL_defectominimo1.Text = resultados.Tolerance1.ToString();
                            labeL_defectominimo2.Text = resultados.Tolerance2.ToString();
                            label_numdefects1.Text = resultados.NumDefects1.ToString();
                            label_numdefect2.Text = resultados.NumDefects2.ToString();

                            if (resultados.NumDefects1 + resultados.NumDefects2 == 0)
                            {
                                button_Validacion.BackColor = Color.Green;
                                button_Validacion.Text = "OK";

                            }
                            else
                            {
                                button_Validacion.BackColor = Color.Red;
                                button_Validacion.Text = "NO";
                            
                            }


                            //formPrincipal.VentanaHalconPrincipal.HalconWindow.DumpWindow("jpeg", "meplaca_lab"); 

                        } 
                        #endregion
                            break;
                        case "Informacion":
                              #region informacion

                            Informacion informacion = datos.MEPInformacion;
                                label_Lenght.Text = ((double)(informacion.Avance/1000)).ToString("F02");
                                toolStripStatusframerate.Text = "FRAMERATE : "+ informacion.Rate.ToString("F1") + " perfiles / sec";
                            #endregion

                            break;
                        default:
                            break;
                    }
                }
            }
        }
       

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timerEstado.Enabled = false;
            _Meplate.Stop();
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

        private void timerEstado_Tick(object sender, EventArgs e)
        {
            if (_Meplate != null)
            {
                dynamic temp = new ExpandoObject();
                _Meplate.GetData(ref temp, "Estado");
                if (temp.MEPErrors=="")
                {
                    toolStripStatusSpeed.Text = "SPEED : "+ (temp.MEPVelocidad/100).ToString() +"  m/min";
                    if (temp.MEPMidiendo) toolStripStatusMidiendo.Text = "Measuring Plate";
                    else toolStripStatusMidiendo.Text = "Waiting for Plate";                   
                    
                    
                    if (temp.MEPOPConnected)
                    {
                        toolStripStatusProcessComputer.BackColor = Color.Green;
                        toolStripStatusProcessComputer.Text = "Process Computer :Connected";

                    }
                    else
                    {
                        toolStripStatusProcessComputer.BackColor = Color.Red;
                        toolStripStatusProcessComputer.Text = "Process Computer : Non Connected";
                    
                    }
                    if (temp.MEPTarjetaConnected)
                    {
                        toolStripStatusTarjeta.BackColor = Color.Green;
                        toolStripStatusTarjeta.Text = "Speed Reference :Connected";
                        connectToolStripMenuItem.Enabled = false;

                    }
                    else
                    {
                        toolStripStatusTarjeta.BackColor = Color.Red;
                        toolStripStatusTarjeta.Text = "Speed Reference : Non Connected";
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

               startToolStripMenuItem.Enabled= false;
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

       
    }
}
