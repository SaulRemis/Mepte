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

namespace OPSaul
{
    delegate void delegatePintarresultados(Message result);

    public partial class Form1 : Form
    {
        OPSaul ejemplo;
        delegatePintarresultados d_PintarResultados;  //puntero a la funcion de pintar
        dynamic ConfigData;
        
        DataTable table = new DataTable();

        public Form1()
        {
            InitializeComponent();
            ConfigData = new ExpandoObject();
            ejemplo = new OPSaul(this);
            ejemplo.Init(ref ConfigData);
            d_PintarResultados = new delegatePintarresultados(PintarResultados);
            ejemplo.NewResultEvent += new ResultEventHandler(ejemplo_NewResultEvent);

            ejemplo.Start();

            table.Columns.Add("XMax", typeof(double));
            table.Columns.Add("YMax", typeof(double));
            table.Columns.Add("ZMax", typeof(double));
            table.Columns.Add("XMin", typeof(double));
            table.Columns.Add("YMin", typeof(double));
            table.Columns.Add("ZMin", typeof(double));
            table.Columns.Add("DIST", typeof(double));
        }

        void ejemplo_NewResultEvent(object sender, DataEventArgs res)
        {
            try
            {
                this.BeginInvoke(d_PintarResultados, ((dynamic)res.DataArgs));
            }
            catch (Exception)
            {
            }

        }
        void PintarResultados(Message resultados)
        {
            richTextBox1.Text += "\nMENSAJE::ID=" + resultados.Messageid;
            if (resultados.Messageid.Equals("M5"))
            {
                //relleno el datagrid
                dataGridView1.DataSource = getDatos(resultados);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                    ((ComunicacionMeplaca)ejemplo._DispatcherThreads["ComunicacionMeplaca"])._server.Start();
                    button2.Enabled = false;
                    label4.Text = "CONNECTED";
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                label4.Text = "COULDN´T CONNECT";
            }
        }

        private DataTable getDatos(Message datos)
        {
	        //
	        // Here we add five DataRows.
	        //
            table.Rows.Clear();
            for(int i=0;i<10;i++)
            {
	            table.Rows.Add(datos.Tabla1[i,0],datos.Tabla1[i,1],datos.Tabla1[i,2],datos.Tabla1[i,3],datos.Tabla1[i,4],datos.Tabla1[i,5],datos.Tabla1[i,6]);
            }
            return table;
        }

        public dynamic getDatos()
        {
            ConfigData.FORMPlate = textBox1.Text;
            ConfigData.FORMWidth = textBox2.Text;
            ConfigData.FORMLength = textBox3.Text;
            return ConfigData;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConfigData.FORMPlate = textBox1.Text;
            ConfigData.FORMWidth = textBox2.Text;
            ConfigData.FORMLength = textBox3.Text;
            ejemplo.SetData(ConfigData);
        }
    }
}
