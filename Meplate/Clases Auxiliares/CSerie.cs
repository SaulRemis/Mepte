using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Meplate
{
    class CSerie
    {
        byte[] bufferlocal;
        int modulos=0;
        int marcador=0;
        List<byte[]> tramas;
        int longitudtrama=0;
        List<int[]> tensiones;
        System.IO.Ports.SerialPort PuertoSerie;

        Mutex tensionesMutex;
        public UInt16[] offset;

       public CSerie(int mod,string puerto)
        {
            modulos = mod;
            longitudtrama = mod * 12 + mod + 1;
            bufferlocal = new byte[100000];
            tramas= new List<byte[]>();
            tensiones= new List<int[]>();
            PuertoSerie = new System.IO.Ports.SerialPort(puerto, 250000);
            PuertoSerie.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(PuertoSerie_DataReceived);
            tensionesMutex = new Mutex();
            offset = new UInt16[modulos * 6];

        }

       void PuertoSerie_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
       {
           int NumBytes = PuertoSerie.BytesToRead;

           PuertoSerie.Read(bufferlocal, marcador, NumBytes);
           marcador = marcador + NumBytes;
           if (Buscar_Final())     ProcesarTramas();
       }


       public  int Buscar_Inicio(byte[] buffer)
        {

            for (int i = 0; i < buffer.Length-1; i++)
            {
                if (buffer[i] == 255)
                {
                    if (buffer[i+1]==255) return i+1;
                }
            }
            return -1;
        
        }
       public bool Buscar_Final()
       {
           byte temp;
           int modulo;
           int indice_ultima_trama = 0;
           byte[] trama = new byte[longitudtrama];
           byte[] temporal= new byte[bufferlocal.Length] ;

           for (int i = 2; i < marcador; i++)
           {

               if (bufferlocal[i] == 255)
               {
                   temp = bufferlocal[i - 2];
                   modulo = (int)temp;
                   modulo = modulo >> 4;
                   if ((modulo == modulos - 1) && (i > longitudtrama))
                   {

                       if ((bufferlocal[i - longitudtrama + 1]) == 255)
                       {
                           indice_ultima_trama = i;
                           Array.Copy(bufferlocal, i - longitudtrama + 1, trama, 0, longitudtrama);
                           tramas.Add(trama);
                       }

                   }
               }
           }
           if (indice_ultima_trama > 0)
           {
               Array.Copy(bufferlocal, indice_ultima_trama + 1, temporal, 0, marcador - indice_ultima_trama - 1);
               bufferlocal.Initialize();
               Array.Copy(temporal, bufferlocal, bufferlocal.Length);
               marcador = marcador - indice_ultima_trama - 1;
               if (marcador < 0) 
                   marcador = 0;
               return true;
           }

           else return false;
       }
       public void ProcesarTramas()
       {
           int temp=0;
          int[] vector = new int[modulos*6];
        
      
           byte[] sensor= new byte [2];
           foreach (byte[] trama in tramas)
           {
               for (int i = 0; i < modulos; i++)
               {
                   vector.Initialize();
                   for (int j = 1; j < 13; j = j + 2)
                   { 
                    temp=(int)trama[13*i+j];
                    temp = temp & 15;  //mascara para quedarme con la parte baja del byte
                    sensor[0] = trama[13 * i + j + 1];
                    sensor[1] = (byte)temp;
                    temp = BitConverter.ToUInt16(sensor, 0);
                    vector[6 * i + (j-1)/2] = temp;
                   }
                  
               
               }

               tensionesMutex.WaitOne(); //Inicio seccion critica
               tensiones.Add(vector);
               tensionesMutex.ReleaseMutex();//Fin seccion critica
           }
           tramas.Clear();
       
       }
       public void AbrirPuerto()
       {
           try
           {
               PuertoSerie.Open();
           }
           catch (Exception)
           {

           }
       }
       public void CerrarPuerto()
       {
           PuertoSerie.Close();
           bufferlocal.Initialize();
           tramas.Clear();
           tensiones.Clear();
           marcador = 0;
       }
       public List<int[]> LeerTensiones()
       {
           tensionesMutex.WaitOne(); //Inicio seccion critica
           List<int[]> temp = new List<int[]>(tensiones);
           tensiones.Clear();
           tensionesMutex.ReleaseMutex();//Fin seccion critica
           return temp;

       }
       public int[] UltimaTension()
       {
           int [] temp= new int[modulos*6];
           if (tensiones.Count>0)
               tensionesMutex.WaitOne(); //Inicio seccion critica
           temp = tensiones[tensiones.Count - 1];
           tensiones.Clear();
           tensionesMutex.ReleaseMutex();//Fin seccion critica

            return temp;
            

       }
       public int[][] ObtenerMatriz()
       {
           int[][] matriz= new int[tensiones.Count][];
           tensiones.CopyTo(matriz);
       return matriz;
       
       }
       public void EnviarOffsets()
        {
            byte[] temp = new byte[2];
            byte[] buff = new byte[6];

            if (PuertoSerie.IsOpen)
            {

                    for (int i = 0; i < modulos; i++)
                    {

                        for (int j = 0; j < 6; j++)
                        {
                            
                            buff.Initialize();

                            buff[0] = 255;
                            buff[1] = (byte)i;//Número de celda/nºsensores (CELDA)
                            buff[2] = (byte)j; //Sensor al que corresponde el offset (CANAL)
                            temp=BitConverter.GetBytes(offset[6*i+j]);
                            buff[3] = temp[1];
                            if (temp[0] == 255) 
                                buff[4] = 254;
                            else buff[4] = temp[0];  //byte bajo (VALORV)r
                           // buff[3] = (byte) (cal.Modulos[i].Sensores[j].off/256);
                            //buff[4] = (byte)(cal.Modulos[i].Sensores[j].off % 256);


                            buff[5] = (byte)(((buff[0] + buff[1] + buff[2] + buff[3] + buff[4]) % 256));

                            PuertoSerie.Write(buff, 0, 6);
                            System.Threading.Thread.Sleep(50);
                            
                        }
                    }

                }

       }
       public void EnviarOffset(int modulo, int sensor, UInt16 offset)
       {
           byte[] temp = new byte[2];
           byte[] buff = new byte[6];

           if (PuertoSerie.IsOpen)
           {
                       buff[0] = 255;
                       buff[1] = (byte)modulo;//Número de celda/nºsensores (CELDA)
                       buff[2] = (byte)sensor; //Sensor al que corresponde el offset (CANAL)
                       temp = BitConverter.GetBytes(offset);
                       buff[3] = temp[1];
                       if (temp[0] == 255)
                           buff[4] = 254;
                       else buff[4] = temp[0];  //byte bajo (VALORV)r
                       // buff[3] = (byte) (cal.Modulos[i].Sensores[j].off/256);
                       //buff[4] = (byte)(cal.Modulos[i].Sensores[j].off % 256);


                       buff[5] = (byte)(((buff[0] + buff[1] + buff[2] + buff[3] + buff[4]) % 256));

                       PuertoSerie.Write(buff, 0, 6);
                       System.Threading.Thread.Sleep(50);
           }
       
       }

       //Corregir la influencia de la banda qu eno esta directamente debajo del sensor  si no al lado 
       public void CorregirInfluenciaLateral()
       {
           tensionesMutex.WaitOne(); //Inicio seccion critica
          
           int valor_2, valor_1, valor, valor1, valor2, val;

           for (int k = 0; k < tensiones.Count; k++)
           {

               for (int i = 2; i < (modulos*6)-2; i++)
               {
                   valor_2 = tensiones[k][i - 2];
                   valor_1 = tensiones[k][i - 1];
                   valor = tensiones[k][i ];
                   valor1 = tensiones[k][i +1];
                   valor2 = tensiones[k][i + 2];
                   val =(int) Math.Round(valor - 0.7 * valor_2 * .05 - 0.7 * valor2 * 0.05 - 0.7 * valor_1 * 0.1 - 0.7 * valor1 * 0.1);

                   tensiones[k][i] = val;


               }
               
           }

           tensionesMutex.ReleaseMutex();//Fin seccion critica
       
       }
    }
}
