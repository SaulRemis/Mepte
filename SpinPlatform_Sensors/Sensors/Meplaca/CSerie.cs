using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace SpinPlatform.Sensors.Meplaca
{
    class CSerie
    {
        byte[] bufferlocal,ultimatrama;
        int modulos=0;
        int error = 0;
        int marcador=0;
        List<byte[]> tramas;
        int longitudtrama=0;
        List<int[]> tensiones;
        System.IO.Ports.SerialPort PuertoSerie;
        double _UmbralAltoCabeza,_UmbralBajoCabeza;
        CMeplaca _Padre;
        bool chapa = false;

        readonly object _locker;

        public UInt16[] _Offset;

       public CSerie( CMeplaca padre)
        {
            _Padre = padre;
            modulos = _Padre._NumeroModulos;
            longitudtrama = modulos * 12 + modulos + 1;
            bufferlocal = new byte[100000];
            ultimatrama = new byte[longitudtrama];
            tramas= new List<byte[]>();
            tramas.Capacity = 100;
            tensiones= new List<int[]>();
            PuertoSerie = new System.IO.Ports.SerialPort(_Padre._Puerto, 250000);
            PuertoSerie.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(PuertoSerie_DataReceived);
            _locker = new object();
            _Offset = new UInt16[modulos * 6];
            _UmbralBajoCabeza =_Padre._UmbralBajoDeteccionCabeza ;
            _UmbralAltoCabeza = _Padre._UmbralAltoDeteccionCabeza;

        }

       void PuertoSerie_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
       {
           if (PuertoSerie.IsOpen)
           {
                int NumBytes = PuertoSerie.BytesToRead;
                PuertoSerie.Read(bufferlocal, marcador, NumBytes);
                marcador = marcador + NumBytes;
                if (Buscar_Inicio2())
                {
                    ProcesarTramas();
                    DetectarCabeza();
                }
            
           }
       }

       public void DetectarCabeza()
       {
           int[] trama = UltimaTension();
           float media=0;
           for (int i = 1; i < 8; i++)
           {
               media = media + trama[i];
           }
           media = media / 6;
           if (media > _UmbralAltoCabeza && !chapa) { 
               
               chapa = true;
               //Debug
               //_UmbralCabeza = media - 100;
               
               _Padre.PrepareEvent("InicioChapa",media); }

           if (media < _UmbralBajoCabeza && chapa) {
               
               chapa = false;
               //Debug
               //_UmbralCabeza = media + 100;
               _Padre.PrepareEvent("FinChapa",media);
           
           }          

       }
       public bool Buscar_Inicio2()
       {

           int indice_ultima_trama = 0;

           byte[] trama = new byte[longitudtrama];
           byte[] temporal = new byte[bufferlocal.Length];

           for (int i = 0; i < marcador - 1; i++)
           {

               if (bufferlocal[i] == 255 && bufferlocal[i + 1] == 255)
               {
                   if (i + longitudtrama+1 < marcador)
                   {
                       for (int j = i + longitudtrama - 3; j < i + longitudtrama+1; j++)
                       {
                           if (bufferlocal[j] == 255 && bufferlocal[j + 1] == 255)
                           {
                               if (j - i == longitudtrama )
                               {
                                   indice_ultima_trama = i + longitudtrama - 2;
                                   Array.Copy(bufferlocal, i + 1, trama, 0, longitudtrama);
                                   trama.CopyTo(ultimatrama,0);
                                   tramas.Add(trama);
                                   error = 0;
                               }
                               else
                               {
                                   if (error==0)
                                   {
                                       Array.Copy(bufferlocal, i + 1, trama, 0, longitudtrama);
                                       CorregirTrama(trama, longitudtrama - j + i);
                                       error = 1;
                                   }
                               }

                           }

                       }
                   }
               }


           }
           if (indice_ultima_trama > 2)
           {
               Array.Copy(bufferlocal, indice_ultima_trama , temporal, 0, marcador - indice_ultima_trama + 3);
               Array.Copy(temporal, 0, bufferlocal, 0, bufferlocal.Length);
               marcador = marcador - indice_ultima_trama - 1;
               if (marcador < 0)
                   marcador = 0;
               return true;
           }

           else return false;

       }

       private void CorregirTrama(byte[] trama, int p)
       {
           int modulo=0;
           byte[] trama_temp = new byte[longitudtrama];
          
           for (int i = 0; i < trama.Length-13; i++)
           {
              if (trama[i] == 255 && trama[i +13-1] == 255)
              {
                 modulo =(int) Math.Truncate((double)i/13);
                 if (modulo > 0) Array.Copy(trama, 0, trama_temp, 0, modulo * 13);
                 Array.Copy(ultimatrama, modulo * 13, trama_temp, modulo * 13, 13);
                 if (modulo < modulos - 1) Array.Copy(trama, ((modulo + 1) * 13) - p, trama_temp, (modulo + 1) * 13, ((modulos - modulo - 1) * 13) + 1);
      
                   
              }
              if (trama[i] == 255 && trama[i + 13 - 2] == 255)
              {
                  modulo = (int)Math.Truncate((double)i / 13);
                  if (modulo > 0) Array.Copy(trama, 0, trama_temp, 0, modulo * 13);
                  Array.Copy(ultimatrama, modulo * 13, trama_temp, modulo * 13, 13);
                  if (modulo < modulos - 1) Array.Copy(trama, ((modulo + 1) * 13) - p, trama_temp, (modulo + 1) * 13, ((modulos - modulo - 1) * 13) + 1);

              }
           }
               tramas.Add(trama);
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
               Array.Copy(bufferlocal, indice_ultima_trama, temporal, 0, marcador - indice_ultima_trama );
               bufferlocal.Initialize();
               Array.Copy(temporal, bufferlocal, bufferlocal.Length);
               marcador = marcador - indice_ultima_trama ;
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

              //Inicio seccion critica
               lock (_locker)
               {
                   tensiones.Add(vector); 
               }
               //Fin seccion critica
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
       public void VaciarBuffer()
       {
           lock (_locker)
           {
               
               tensiones.Clear();
           }
       
       }
       public void CerrarPuerto()
       {
           if (PuertoSerie.IsOpen) PuertoSerie.Close();
           bufferlocal.Initialize();
           tramas.Clear();
           tensiones.Clear();
           marcador = 0;
       }
       public List<int[]> LeerTensiones()
       {
           List<int[]> temp;
 //Inicio seccion critica
           lock (_locker)
           {
               temp = new List<int[]>(tensiones);
               tensiones.Clear(); 
           }
//Fin seccion critica
           return temp;

       }
       public int[] UltimaTension()
       {
           int [] temp= new int[modulos*6];
          
 //Inicio seccion critica
           lock (_locker)
           { if (tensiones.Count>0)
            tensiones[tensiones.Count - 1].CopyTo((Array)temp,0);
              // tensiones.Clear(); 
           }
//Fin seccion critica

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
                            temp=BitConverter.GetBytes(_Offset[6*i+j]);
                            buff[3] = temp[1];
                            if (temp[0] == 255) 
                                buff[4] = 254;
                            else buff[4] = temp[0];  //byte bajo (VALORV)r
                           // buff[3] = (byte) (cal.Modulos[i].Sensores[j].off/256);
                            //buff[4] = (byte)(cal.Modulos[i].Sensores[j].off % 256);


                            buff[5] = (byte)(((buff[0] + buff[1] + buff[2] + buff[3] + buff[4]) % 256));

                            PuertoSerie.Write(buff, 0, 6);
                            System.Threading.Thread.Sleep(60);
                            
                        }
                    }

                }

       }
      
       public void EnviarOffset(int modulo, int sensor, UInt16 offset)
       {
           byte[] temp = new byte[2];
           byte[] buff = new byte[6];
           _Offset[modulo * 6 + sensor] = offset;

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
           }
       
       }

       //Corregir la influencia de la banda qu eno esta directamente debajo del sensor  si no al lado 
       public void CorregirInfluenciaLateral()
       {
 //Inicio seccion critica

           lock (_locker)
           {
               int valor_2, valor_1, valor, valor1, valor2, val;

               for (int k = 0; k < tensiones.Count; k++)
               {

                   for (int i = 2; i < (modulos * 6) - 2; i++)
                   {
                       valor_2 = tensiones[k][i - 2];
                       valor_1 = tensiones[k][i - 1];
                       valor = tensiones[k][i];
                       valor1 = tensiones[k][i + 1];
                       valor2 = tensiones[k][i + 2];
                       val = (int)Math.Round(valor - 0.7 * valor_2 * .05 - 0.7 * valor2 * 0.05 - 0.7 * valor_1 * 0.1 - 0.7 * valor1 * 0.1);

                       tensiones[k][i] = val;


                   }

               } 
           }

//Fin seccion critica
       
       }
    }
}
