using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SpinPlatform.Dispatcher;
using SpinPlatform.Data;
using System.Threading;
using System.Dynamic;
using System.Diagnostics;

namespace CardSaul
{
    delegate void delegatePintarresultados(String result);

    public partial class Form1 : Form
    {
        CardSaul ejemplo;
        delegatePintarresultados d_PintarResultados;  //puntero a la funcion de pintar
        int messagecounter = 1000;
        dynamic ConfigData;
        public Form1()
        {
            ConfigData= new ExpandoObject();
            InitializeComponent();
            ejemplo = new CardSaul();
            ejemplo.Init(ref ConfigData);
            d_PintarResultados = new delegatePintarresultados(PintarResultados);
            ejemplo.NewResultEvent += new ResultEventHandler(ejemplo_NewResultEvent);

            ejemplo.Start();
        }

        void ejemplo_NewResultEvent(object sender, DataEventArgs res)
        {
            try
            {
                this.BeginInvoke(d_PintarResultados, ((dynamic)res.DataArgs).COMMessage);
            }
            catch (Exception)
            {
            }

        }
        void PintarResultados(String resultados)
        {
            
            label1.Text = "AVANCE: " + resultados;
            double temp = double.Parse(resultados);
            label2.Text = "VELOCIDAD: " + (temp/100).ToString();
        }

        private void Form1_FormClosing(object sender, EventArgs e)
        {
            ejemplo.Stop();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if(button1.Text.Equals("Entra Chapa"))
            {
                button1.Text = "Sale Chapa";
                if (trackBar1.Value >= 0)
                {
                    short id = 21;

                    byte[] rv = new byte[(Encoding.ASCII.GetBytes("$TARJETA0018" + messagecounter.ToString())).Length + (BitConverter.GetBytes(id)).Length] ;
                    System.Buffer.BlockCopy((Encoding.ASCII.GetBytes("$TARJETA0018" + messagecounter.ToString())), 0, rv, 0, (Encoding.ASCII.GetBytes("$TARJETA0018" + messagecounter.ToString())).Length);
                    System.Buffer.BlockCopy((BitConverter.GetBytes(id)), 0, rv, (Encoding.ASCII.GetBytes("$TARJETA0018" + messagecounter.ToString())).Length, (BitConverter.GetBytes(id)).Length);
                   
                    ((HiloServidor)((CardSaul)ejemplo)._DispatcherThreads["HiloServidor"]).SendMessage(rv);
                }

                else
                {
                    short id = 22;

                    byte[] rv = new byte[(Encoding.ASCII.GetBytes("$TARJETA0018" + messagecounter.ToString())).Length + (BitConverter.GetBytes(id)).Length] ;
                    System.Buffer.BlockCopy((Encoding.ASCII.GetBytes("$TARJETA0018" + messagecounter.ToString())), 0, rv, 0, (Encoding.ASCII.GetBytes("$TARJETA0018" + messagecounter.ToString())).Length);
                    System.Buffer.BlockCopy((BitConverter.GetBytes(id)), 0, rv, (Encoding.ASCII.GetBytes("$TARJETA0018" + messagecounter.ToString())).Length, (BitConverter.GetBytes(id)).Length);
                   
                    ((HiloServidor)((CardSaul)ejemplo)._DispatcherThreads["HiloServidor"]).SendMessage(rv);
                }

            }
            else
            {
                button1.Text = "Entra Chapa";
                if (trackBar1.Value >= 0)
                {
                     short id = 23;

                    byte[] rv = new byte[(Encoding.ASCII.GetBytes("$TARJETA0018" + messagecounter.ToString())).Length + (BitConverter.GetBytes(id)).Length] ;
                    System.Buffer.BlockCopy((Encoding.ASCII.GetBytes("$TARJETA0018" + messagecounter.ToString())), 0, rv, 0, (Encoding.ASCII.GetBytes("$TARJETA0018" + messagecounter.ToString())).Length);
                    System.Buffer.BlockCopy((BitConverter.GetBytes(id)), 0, rv, (Encoding.ASCII.GetBytes("$TARJETA0018" + messagecounter.ToString())).Length, (BitConverter.GetBytes(id)).Length);
                   
                    ((HiloServidor)((CardSaul)ejemplo)._DispatcherThreads["HiloServidor"]).SendMessage(rv);
                }
                else
                {
                     short id = 24;

                    byte[] rv = new byte[(Encoding.ASCII.GetBytes("$TARJETA0018" + messagecounter.ToString())).Length + (BitConverter.GetBytes(id)).Length] ;
                    System.Buffer.BlockCopy((Encoding.ASCII.GetBytes("$TARJETA0018" + messagecounter.ToString())), 0, rv, 0, (Encoding.ASCII.GetBytes("$TARJETA0018" + messagecounter.ToString())).Length);
                    System.Buffer.BlockCopy((BitConverter.GetBytes(id)), 0, rv, (Encoding.ASCII.GetBytes("$TARJETA0018" + messagecounter.ToString())).Length, (BitConverter.GetBytes(id)).Length);
                   
                    ((HiloServidor)((CardSaul)ejemplo)._DispatcherThreads["HiloServidor"]).SendMessage(rv);
                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            ConfigData.VALORSlider = trackBar1.Value;
            ejemplo.SetData(ref ConfigData,"");
        }

    }
}
