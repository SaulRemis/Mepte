using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading;
using SpinPlatform;

namespace Meplate
{
    class CMeplaca : ISpinPlatformInterface
    {
        CSerie serie;
        CModulosCal calibracion;
        int numeroModulos;
        double _MinimoAvanceParaMedir;

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

            double[] temp = new double[numeroModulos * 6];
            double valor = 0.0;
            List<int[]> tensiones = new List<int[]>();
            tensiones = serie.LeerTensiones();
            //Thread.Sleep(5);
            int numTensiones = tensiones.Count;
            for(int k=0;k<tensiones.Count;k++)
            {
                
                for (int i = 0; i < numeroModulos; i++)
                { 
                    for (int j = 0; j < 6; j++)
                    {
                        valor=calibracion.Modulos[i].Sensores[j].a/(tensiones[k][6*i+j]-calibracion.Modulos[i].Sensores[j].b)+calibracion.Modulos[i].Sensores[j].c;
                        if (valor>10) valor=10;
                        if (valor<2) valor =2;
                        temp[6 * i + j] = valor;
                    }
                
                }
                 for (int i = 1; i < ( numeroModulos * 6) - 1; i++)
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
            double[] temp = new double[numeroModulos * 6];
            int[] tensiones = serie.UltimaTension();
            double valor = 0.0;

            for (int i = 0; i < numeroModulos; i++)
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

            for (int i = 0; i < numeroModulos; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    serie.offset[i*6+j] =(UInt16)calibracion.Modulos[i].Sensores[j].off;
                }
            }
            serie.EnviarOffsets();
        
        }
        void enviarOffsets(UInt16[] offsets)
        {

            for (int i = 0; i < numeroModulos * 6; i++)
            {
                serie.offset[i] = offsets[i];
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

        public void Init(object parameters)
        {
            CArchivos arch = ((MeplacaData)parameters).File;

            _MinimoAvanceParaMedir = double.Parse(arch.LeerXML("MinimoAvanceParaMedir"));
            numeroModulos = int.Parse(arch.LeerXML("numeroModulos"));
            string puerto = arch.LeerXML("puertoSerie");
            string[] pathDatosCalibracion = new string[numeroModulos];
            for (int i = 0; i < numeroModulos; i++)
            {
                pathDatosCalibracion[i] = arch.LeerXML("calibracionModulo" + (i + 1).ToString());
            }
            calibracion = new CModulosCal(numeroModulos, pathDatosCalibracion);
            serie = new CSerie(numeroModulos, puerto);


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
