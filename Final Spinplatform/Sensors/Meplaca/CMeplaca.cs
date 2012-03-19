﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading;
using SpinPlatform;
using SpinPlatform.IO;

namespace SpinPlatform.Sensors.Meplaca
{
    public class CMeplaca : ISpinPlatformInterface
    {
        CSerie serie;
        CModulosCal calibracion;
        int _NumeroModulos;
        double _MinimoAvanceParaMedir;
        double _Distancia_a_la_chapa;


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


        #region Miembros de ISpinPlatformInterface

        public void Start()
        {
            Inicializar();
        }

        public void Stop()
        {
            Cerrar();
        }

        public object GetData(object parameters)
        {
            if (parameters.GetType() == typeof(MeplacaData))
            {
                MeplacaData data = (MeplacaData)parameters;

                data.ResetData();
                if (data.GetMedidas)
                {
                    data.Perfiles = LeerMedidas();
                }
                if (data.GetUltimaMedida)
                {
                    data.UltimoPerfil = UltimaMedida();
                }
                if (data.GetTensiones)
                {
                    data.Tensiones = LeerTensiones();
                }
                if (data.GetUltimaTension)
                {
                    data.UltimaTension = UltimaTension();
                }
                return data;
            }
            else return null;
        }

        public void Init(dynamic parametros)
        {
            _MinimoAvanceParaMedir = double.Parse(parametros.Meplaca.MinimoAvanceParaMedir);
            _Distancia_a_la_chapa = double.Parse(parametros.Meplaca.distancia_nominal_trabajo);
            _NumeroModulos = int.Parse(parametros.Meplaca.numeroModulos);
            string puerto = parametros.Meplaca.puertoSerie;
            string[] pathDatosCalibracion = new string[_NumeroModulos];
            pathDatosCalibracion[0] = parametros.Calibracion.calibracionModulo1;
            pathDatosCalibracion[1] = parametros.Calibracion.calibracionModulo2;
            pathDatosCalibracion[2] = parametros.Calibracion.calibracionModulo3;
            pathDatosCalibracion[3] = parametros.Calibracion.calibracionModulo4;
            pathDatosCalibracion[4] = parametros.Calibracion.calibracionModulo5;
            pathDatosCalibracion[5] = parametros.Calibracion.calibracionModulo6;
            calibracion = new CModulosCal(_NumeroModulos, pathDatosCalibracion);
            serie = new CSerie(_NumeroModulos, puerto);
        }

        public void SetData(object parameters)
        {
            if (parameters.GetType() == typeof(MeplacaData))
            {
                MeplacaData data = (MeplacaData)parameters;

                if (data.EnviarOffsetsArchivo)
                {
                    enviarOffsetsArchivo();
                }
                if (data.EnviarOffsets)
                {
                    enviarOffsets(data.Offsets);
                }

            }
        }

        public event SpinPlatform.Dispatcher.ResultEventHandler NewResultEvent;

        #endregion
    }

}