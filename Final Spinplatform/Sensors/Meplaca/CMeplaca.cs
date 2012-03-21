﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading;
using SpinPlatform;
using SpinPlatform.IO;
using System.Dynamic;

namespace SpinPlatform.Sensors.Meplaca
{
    public class CMeplaca : ISpinPlatformInterface2
       //  public class CMeplaca : ISpinPlatformInterface
    {
        CSerie serie;
        CModulosCal calibracion;
        int _NumeroModulos;
        double _MinimoAvanceParaMedir;
        double _Distancia_a_la_chapa;
        string _Puerto;
        string[] _PathDatosCalibracion;


        public double MinimoAvanceParaMedir{get { return _MinimoAvanceParaMedir; }}
        
        public CMeplaca()
        {
           
        }
        void Inicializar()
        {
            serie.AbrirPuerto();
            enviarOffsetsArchivo();
        
        }
        void Cerrar()
        {
            
            serie.CerrarPuerto();
        

        }
         int[] UltimaTension()
        {
            return serie.UltimaTension();
        
        }
        List<double []> LeerMedidas()
        {
            List<double[]> distancias = new List<double[]>();

            double[] temp = new double[_NumeroModulos * 6];
            double valor = 0.0;
            List<int[]> tensiones = new List<int[]>();
            tensiones = serie.LeerTensiones();
            //Thread.Sleep(5);
            int numTensiones = tensiones.Count;
            for(int k=0;k<tensiones.Count;k++)
            {
                
                for (int i = 0; i < _NumeroModulos; i++)
                { 
                    for (int j = 0; j < 6; j++)
                    {
                        valor=calibracion.Modulos[i].Sensores[j].a/(tensiones[k][6*i+j]-calibracion.Modulos[i].Sensores[j].b)+calibracion.Modulos[i].Sensores[j].c;
                        if (valor>10) valor=10;
                        if (valor<2) valor =2;
                        temp[6 * i + j] = valor;
                    }
                
                }
                 for (int i = 1; i < ( _NumeroModulos * 6) - 1; i++)
               {
                  if (Math.Abs(temp[i]-temp[i-1])>3&&Math.Abs(temp[i]-temp[i+1])>3)

                      temp[i]=(temp[i-1]+temp[i-1])/2;
	

               }

               
                    distancias.Add(temp);
            } 
            return distancias;

        }
         List<int[]> LeerTensiones()
        {
            return serie.LeerTensiones();
        
        }
        double[] UltimaMedida()
        {
            double[] temp = new double[_NumeroModulos * 6];
            int[] tensiones = serie.UltimaTension();
            double valor = 0.0;

            for (int i = 0; i < _NumeroModulos; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    valor = calibracion.Modulos[i].Sensores[j].a / (tensiones[6 * i + j] - calibracion.Modulos[i].Sensores[j].b) + calibracion.Modulos[i].Sensores[j].c;
                    if (valor > 10) valor = 10;
                    if (valor < 2) valor = 2;
                    temp[6 * i + j] = valor;
                }

            }

            return temp;
        
        }
         void enviarOffsetsArchivo()
        {

            for (int i = 0; i < _NumeroModulos; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    serie.offset[i*6+j] =(UInt16)calibracion.Modulos[i].Sensores[j].off;
                }
            }
            serie.EnviarOffsets();
        
        }

        /// <summary>
        /// Envia un vector con nuevos offset al meplaca. Internamente los pasa de mm a tensiones
        /// </summary>
        /// <param name="offsets">offset en mm </param>
        void enviarOffsets(double[] offsets)
        {
            UInt16 dist, valor;
                       
             for (int i = 0; i < _NumeroModulos; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (offsets[6 * i + j] == 0)
                        {
                            serie.offset[6 * i + j] = serie.offset[6 * i + j];

                        }
                        else
                        {
                             dist=(UInt16)Math.Round((calibracion.Modulos[i].Sensores[j].a / (_Distancia_a_la_chapa - calibracion.Modulos[i].Sensores[j].c)) + calibracion.Modulos[i].Sensores[j].b);
                             valor=(UInt16)Math.Round((calibracion.Modulos[i].Sensores[j].a / (offsets[6 * i + j] - calibracion.Modulos[i].Sensores[j].c)) + calibracion.Modulos[i].Sensores[j].b);
                             serie.offset[6 * i + j] = (UInt16)(serie.offset[6 * i + j]+ valor - dist);
                        }
                    }
                
                }
            serie.EnviarOffsets();
        }


        #region Miembros de ISpinPlatformInterface2

        public void Start()
        {
            Inicializar();
        }

        public void Stop()
        {
            Cerrar();
        }

        public void GetData(ref dynamic Data, params string[] parameters)
        {
           // dynamic Data = new ExpandoObject();
            foreach (string parameter in parameters)
            {
                switch (parameter)
                {
                    case "Medidas":
                        Data.Medidas = LeerMedidas();
                        break;
                    case "UltimaMedida":
                        Data.UltimoPerfil = UltimaMedida();
                        break;
                    case "Tensiones":
                        Data.Tensiones = LeerTensiones();
                        break;
                    case "UltimaTension":
                        Data.UltimaTension = UltimaTension();
                        break;

                    default:
                        break;
                }
            }

        }

        public void Init(dynamic parametros)
        {
            _MinimoAvanceParaMedir = double.Parse(parametros.MEPMinimoAvanceParaMedir);
            _Distancia_a_la_chapa = double.Parse(parametros.MEPDistancia_nominal_trabajo);
            _NumeroModulos = int.Parse(parametros.MEPNumeroModulos);
            _Puerto = parametros.MEPPuertoSerie;
            _PathDatosCalibracion = new string[_NumeroModulos];
            _PathDatosCalibracion[0] = parametros.MEPCalibracion.calibracionModulo1;
            _PathDatosCalibracion[1] = parametros.MEPCalibracion.calibracionModulo2;
            _PathDatosCalibracion[2] = parametros.MEPCalibracion.calibracionModulo3;
            _PathDatosCalibracion[3] = parametros.MEPCalibracion.calibracionModulo4;
            _PathDatosCalibracion[4] = parametros.MEPCalibracion.calibracionModulo5;
            _PathDatosCalibracion[5] = parametros.MEPCalibracion.calibracionModulo6;
            calibracion = new CModulosCal(_NumeroModulos, _PathDatosCalibracion);
            serie = new CSerie(_NumeroModulos, _Puerto);
        }

        public void SetData(dynamic data, params string[] parameters)
        {
            foreach (string parameter in parameters)
            {
                switch (parameter)
                {
                    case "EnviarOffsetsArchivo":
                        enviarOffsetsArchivo();
                        break;
                    case "EnviarOffsets":
                        enviarOffsets(data.Offsets);
                        break;                  

                    default:
                        break;
                }
            }
           
        }

        public event SpinPlatform.Dispatcher.ResultEventHandler NewResultEvent;

        #endregion
    }

}
