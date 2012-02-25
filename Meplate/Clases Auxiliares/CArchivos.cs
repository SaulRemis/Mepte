using System;
using System.IO;
using System.Text;

using System.Threading;
using System.Xml;
using System.Runtime.InteropServices;



namespace Meplate
{
    public class CArchivos
    {
        //Elementos del archivo de configuracion
        XmlDocument archivoConfig;
        string nombreArchivo;

        /*public Archivos() //Constructor
        {
            archivoConfig = new XmlDocument();
            archivoConfig.Load("./VighumIni.xml");
            if (LeerXML("ARRANQUEAUTOMATICO") == "1")//Asignar archivo config automaticamente
            {
                string nombreArchivoConfig = LeerXML("NOMBREARCHIVOCONFIG");
                archivoConfig.Load(nombreArchivoConfig);
                nombreArchivo = new System.Windows.Forms.OpenFileDialog();
                nombreArchivo.FileName = nombreArchivoConfig;
            }
            else//Pedir archivo
            {
                archivoConfig = new XmlDocument();
                try
                {
                    nombreArchivo = new System.Windows.Forms.OpenFileDialog();
                    nombreArchivo.Title = "Selecciona archivo de configuracion";
                    nombreArchivo.InitialDirectory = "C:\\";
                    nombreArchivo.Filter = "(*.xml)|*.xml";
                    nombreArchivo.ShowDialog();
                    if (nombreArchivo.FileName == "")
                        System.Windows.Forms.MessageBox.Show("Error lectura archivo configuracion. El programa no funcionara correctamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        archivoConfig.Load(nombreArchivo.FileName);
                    //archivoConfig.Load("./VighumIni.xml");

                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show(e.Message);
                }
            }
        }*/
        public CArchivos(string nA)
        {
            nombreArchivo = nA;
            archivoConfig = new XmlDocument();
            try
            {
                archivoConfig.Load(nombreArchivo);
            }
            catch (Exception e)
            {
               
            }
        }
        public void InicializarArchivos()
        {
            //Crear SableLog.txt
            if (System.IO.File.Exists("C:\\Sable\\SableLog.txt"))
            {
               /* for (int i = 1; i < 100; i++)
                {
                    if (!System.IO.File.Exists("C:\\Sable\\SableLog(" + i.ToString() + ").txt"))
                    {
                        System.IO.File.Copy("C:\\Sable\\SableLog.txt", "C:\\Sable\\SableLog(" + i.ToString() + ").txt");
                        System.IO.File.Delete("C:\\Sable\\SableLog.txt");
                        System.IO.File.Create("C:\\Sable\\SableLog.txt");
                        break;
                    }
                }*/
              }
            else { System.IO.File.Create("C:\\Sable\\SableLog.txt"); }

            //Crear SableError.txt
            if (System.IO.File.Exists("C:\\Sable\\SableError.txt"))
            {
                /*for (int i = 1; i < 100; i++)
                {
                    if (!System.IO.File.Exists("C:\\Sable\\SableError(" + i.ToString() + ").txt"))
                    {
                        System.IO.File.Copy("C:\\Sable\\SableError.txt", "C:\\Sable\\SableError(" + i.ToString() + ").txt");
                        System.IO.File.Delete("C:\\Sable\\SableError.txt");
                        System.IO.File.Create("C:\\Sable\\SableError.txt");
                        break;
                    }
                }*/

            }
            else { System.IO.File.Create("C:\\Sable\\SableError.txt"); }

            //Crear UltimosSables.txt
            if (System.IO.File.Exists("C:\\Sable\\UltimosSables.txt"))
            {
                /*for (int i = 1; i < 100; i++)
                {
                    if (!System.IO.File.Exists("./UltimosSables(" + i.ToString() + ").txt"))
                    {
                        System.IO.File.Copy("./UltimosSables.txt", "./UltimosSables(" + i.ToString() + ").txt");
                        System.IO.File.Delete("./UltimosSables.txt");
                        System.IO.File.Create("./UltimosSables.txt");
                        break;
                    }
                }*/
            }
            else { System.IO.File.Create("C:\\Sable\\UltimosSables.txt"); }
        }//Funcion que inicializa archivos
        public void EscribirLinea(string Path, string Linea)
        {
            if (System.IO.File.Exists(Path))
            {
                try
                {
                    StreamWriter escritor = new StreamWriter(Path, true);
                    escritor.WriteLine(Linea);
                    escritor.Flush();
                    escritor.Close();
                }
                catch (Exception e1)
                {
                    //System.Windows.Forms.MessageBox.Show("Error escritura archivo configuracion(variable '" + Linea + "' inexistente).");
                }
            }
        }//Escribe una linea nueva conservando las anteriores.
        public string fechaActual()
        {
            int Dia = DateTime.Now.Day;
            int Mes = DateTime.Now.Month;
            int Ano = DateTime.Now.Year;
            int Hora = DateTime.Now.Hour;
            int Minuto = DateTime.Now.Minute;
            int Segundo = DateTime.Now.Second;
            string linea = "<" + estandarizar(Dia) + "-" + estandarizar(Mes) + "-" + estandarizar(Ano) + " " + estandarizar(Hora) + ":" + estandarizar(Minuto) + ":" + estandarizar(Segundo) + "> ";
            return linea;
        }//Devuelve un string con la fecha y hora acutal        
        private string estandarizar(int num)
            {
                string cadena;
                if (num < 10) { cadena = "0" + num.ToString(); } else { cadena = num.ToString(); }
                return cadena;
            }//Si el numero es menor que 10 le añade un "0" a la izquierda y lo devuelve como string.
        public string LeerXML(string nombreVariable)
        {
            try
            {
                XmlNodeList Nodo = archivoConfig.GetElementsByTagName(nombreVariable);
                return Nodo[0].InnerText;
            }
            catch (Exception e1)
            {
                EscribirLinea("C://Sable//SableError.txt", fechaActual() + "Error lectura archivo configuracion(variable '"+nombreVariable+ "' inexistente).");
                
                return "-1";
            }
        }//Leemos la variable de configuracion.
        public void EscribirXML(string nombreVariable, string valor,bool guardar)
        {

            XmlNodeList ListaNodos = archivoConfig.GetElementsByTagName(nombreVariable);
            ListaNodos[0].InnerText = valor;
            //Save
            if (guardar)
            {
                archivoConfig.Save(nombreArchivo);
            }

             
        }//Escribimos la variable de configuracion
        public void EscribirXML(string[] nombreVariables, string[] valores)
        {
            for (int i = 0; i < nombreVariables.Length; i++)
            {
                XmlNodeList ListaNodos = archivoConfig.GetElementsByTagName(nombreVariables[i]);
                ListaNodos[0].InnerText = valores[i];
            }
            archivoConfig.Save(nombreArchivo);

        }
    }

}