using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading;
using SpinPlatform;
using System.Dynamic;

namespace SpinPlatform.Sensors.Meplaca
{
    /// <summary>
    /// Modulo para interactuar con el sensor de planitud MEPLACA 
    /// </summary>
    public class CMeplaca : ISpinPlatformInterface
       //  public class CMeplaca : ISpinPlatformInterface
    {

        #region Definicion de variables
        CSerie serie;
        CModulosCal calibracion;
        int _NumeroModulos;
        double _MinimoAvanceParaMedir;
        double _Distancia_a_la_chapa;
        string _Puerto;
        string[] _PathDatosCalibracion;


        public double MinimoAvanceParaMedir{get { return _MinimoAvanceParaMedir; }}

            # endregion

        #region Metodos Privados
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
        #endregion

        #region Miembros de ISpinPlatformInterface2
        /// <summary>
        /// Arranca el Modulo MEPLACA
        /// </summary>
        public void Start()
        {
            Inicializar();
        }
        /// <summary>
        /// Para el MOdulo MEPLACA
        /// </summary>
        public void Stop()
        {
            Cerrar();
        }
        /// <summary>
        /// Obtiene datos del Modulo Meplaca. 
        /// </summary>
        /// <param name="Data">
        /// Variable dinamica donde guardar los resultados: \n
        /// MEPMedidas (List(double [])) resultados de "Medidas" \n
        /// MEPUltimoPerfil (double []) resultados de "UltimaMedida" \n
        /// MEPTensiones (List(int[])) resultados de "Tensiones" \n
        /// MEPUltimaTension  (int[]) resulatdos de "UltimaTension" \n
        /// </param>
        /// <param name="parameters">
        /// "Medidas" - Obtiene todos los perfiles almacenados en el buffer del meplaca \n
        /// "UltimaMedida"  - Obtiene el ultimo perfil medido por el sensor \n
        /// "Tensiones"  - Obtiene todos losperfiles en tensiones almacenados en el buffer del meplaca \n
        /// "UltimaTension"  - Obtiene el ultimo array de tensiones (un valor por cada sensor) \n
        /// </param>
        public void GetData(ref dynamic Data, params string[] parameters)
        {
            
            Data.MEPReturnedData= parameters;
            try
            {
                foreach (string parameter in parameters)
                {
                    switch (parameter)
                    {
                        case "Medidas":
                            Data.MEPMedidas = LeerMedidas();
                            break;
                        case "UltimaMedida":
                            Data.MEPUltimoPerfil = UltimaMedida();
                            break;
                        case "Tensiones":
                            Data.MEPTensiones = LeerTensiones();
                            break;
                        case "UltimaTension":
                            Data.MEPUltimaTension = UltimaTension();
                            break;

                        default:
                            Data.MEPErrors = "Wrong Query";
                            break;
                    }
                }
                Data.MEPErrors = "";
            }
            catch (Exception ex)
            {

                Data.MEPErrors = ex.Message;
                //Ademas se lanzaria la excepcion oportuna
            }

        }

        /// <summary>
        /// Inicializa el Modulo MEPLACA.
  /// </summary>
  /// <param name="parametros">
        /// Campos Obligatorios:  \n
        /// MEPMinimoAvanceParaMedir (double) \n
        /// MEPDistancia_nominal_trabajo (double) \n
        /// MEPNumeroModulos (int) \n
        /// MEPPuertoSerie (string) \n
        /// MEPCalibracion (string []) \n
  /// </param>
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

        /// <summary>
        /// Establece campos en el modulo Meplaca \n
        /// </summary>
        /// <param name="data">
        /// Variable dinamica de donde obtener los datos a establecer \n
        /// MEPOffsets (double[]) offset a enviar atraves de"EnviarOffsets" \n
        /// </param>
        /// <param name="parameters">
        /// "EnviarOffsetsArchivo" - Envia al meplaca los offset guardados en el archivo de calibracion \n
        /// "EnviarOffsets" - Envia al meplaca los offset pasados como parametro en MEPOffsets \n
        /// </param>
        public void SetData(ref dynamic data, params string[] parameters)
        {
            try
            {
                foreach (string parameter in parameters)
                {
                    switch (parameter)
                    {
                        case "EnviarOffsetsArchivo":
                            enviarOffsetsArchivo();
                            break;
                        case "EnviarOffsets":
                            enviarOffsets(data.MEPOffsets);
                            break;

                        default:
                               data.MEPErrors = "Wrong Query";
                            break;
                    }
                }
                data.MEPErrors = "";
            }
            catch (Exception ex)
            {

                data.MEPErrors = ex.Message;
                //Ademas se lanzaria la excepcion oportuna
            }
           
        }

        public event SpinPlatform.Dispatcher.ResultEventHandler NewResultEvent;

        #endregion
    }

}
