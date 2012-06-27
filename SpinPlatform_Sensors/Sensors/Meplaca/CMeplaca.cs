using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Threading;
using SpinPlatform;
using System.Dynamic;
using SpinPlatform.Dispatcher;
using SpinPlatform.Data;

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
        public int _NumeroModulos;
        double _MinimoAvanceParaMedir;
        double _Distancia_a_la_chapa;
       public string _Puerto;
        string[] _PathDatosCalibracion;
        bool _Meplate;
        public double _UmbralAltoDeteccionCabeza, _UmbralBajoDeteccionCabeza;
        public List<UInt16[]> _ListaOffset;


        public double MinimoAvanceParaMedir{get { return _MinimoAvanceParaMedir; }}
        public event SpinPlatform.Dispatcher.ResultEventHandler NewResultEvent;
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
         private UInt16[] LeerOffset()
         {
             return serie._Offset;
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
                        if (_Meplate)
                        {
                            valor = 4*calibracion.Modulos[i].Sensores[j].a / (tensiones[k][6 * i + j] - calibracion.Modulos[i].Sensores[j].b) +4* calibracion.Modulos[i].Sensores[j].c;
                            if (valor > 25) valor = 25;
                            if (valor < 5) valor = 5;
                            
                        }
                        else
                        {
                            valor = calibracion.Modulos[i].Sensores[j].a / (tensiones[k][6 * i + j] - calibracion.Modulos[i].Sensores[j].b) + calibracion.Modulos[i].Sensores[j].c;
                            if (valor > 10) valor = 10;
                            if (valor < 2) valor = 2; 
                        }
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

                    if (_Meplate)
                    {
                        valor =4* calibracion.Modulos[i].Sensores[j].a / (tensiones[6 * i + j] - calibracion.Modulos[i].Sensores[j].b) +4* calibracion.Modulos[i].Sensores[j].c;
                        if (valor > 25) valor = 25;
                        if (valor < 5) valor = 5;
                        
                    }
                    else
                    {
                        valor = calibracion.Modulos[i].Sensores[j].a / (tensiones[6 * i + j] - calibracion.Modulos[i].Sensores[j].b) + calibracion.Modulos[i].Sensores[j].c;
                        if (valor > 10) valor = 10;
                        if (valor < 2) valor = 2; 
                    }
                    temp[6 * i + j] = valor;
                }

            }

            return temp;
        
        }
        void VaciarBuffer()
        {
            serie.VaciarBuffer();
        
        }
         void enviarOffsetsArchivo()
        {

            for (int i = 0; i < _NumeroModulos; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    serie._Offset[i*6+j] =(UInt16)calibracion.Modulos[i].Sensores[j].off;
                }
            }
            serie.EnviarOffsets();
        
        }
        /// <summary>
        /// Envia un vector con nuevos offset al meplaca. Internamente los pasa de mm a tensiones
        /// </summary>
        /// <param name="offsets">offset en mm </param>
        void enviarOffsets(double[] valores, double[] referencias)
        {
            int dist, valor, diff;
            UInt16[] Offset = new UInt16[_NumeroModulos * 6];
                       
             for (int i = 0; i < _NumeroModulos; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        if (valores[6 * i + j] == 0)
                        {
                            serie._Offset[6 * i + j] = serie._Offset[6 * i + j];

                        }
                        else
                        {
                            if (_Meplate)
                            {
                                referencias[6 * i + j] = referencias[6 * i + j] / 4;
                                valores[6 * i + j] = valores[6 * i + j] / 4;
                            }
                           
                                dist = (int)Math.Round((calibracion.Modulos[i].Sensores[j].a / (referencias[6 * i + j] - calibracion.Modulos[i].Sensores[j].c)) + calibracion.Modulos[i].Sensores[j].b);
                                valor = (int)Math.Round((calibracion.Modulos[i].Sensores[j].a / (valores[6 * i + j] - calibracion.Modulos[i].Sensores[j].c)) + calibracion.Modulos[i].Sensores[j].b);
                                diff =(int)Math.Round((double)((valor - dist) / 8));
                        
                          //  serie._Offset[6 * i + j] = (UInt16)(serie._Offset[6 * i + j] + diff);
                            Offset[6 * i + j] = (UInt16)(serie._Offset[6 * i + j] + diff);
                        }
                    }
                
                }

             _ListaOffset.Add(Offset);
             double temp = 0;
             if (_ListaOffset.Count>9)
             {
                 for (int i = 0; i < _NumeroModulos*6;i++)
                 {
                     foreach (UInt16[] vector in _ListaOffset)
                     {
                         temp = temp + (double)vector[i];
                     }
                     temp = temp / _ListaOffset.Count;
                     serie._Offset[i] = (UInt16)Math.Round(temp);
                 }                
                 
                 serie.EnviarOffsets();
                 _ListaOffset.Clear();
             }
            
        }
        void ActualizaArchivoOffset()
        {
            for (int i = 0; i < _NumeroModulos; i++)
            {
                string[] datosCalibracion = System.IO.File.ReadAllLines(_PathDatosCalibracion[i]);
                for (int j = 0; j < 6; j++)
                {                   
                    datosCalibracion[j * 4] = "off" + (j + 1).ToString() + "= " + serie._Offset[6 * i + j].ToString();                   
                }
                System.IO.File.WriteAllLines(_PathDatosCalibracion[i], datosCalibracion);
            }
        
        
        
        }
        private void LeerOffsetsArchivo()
        {
            for (int i = 0; i < _NumeroModulos; i++)
            {
                string[] datosCalibracion = System.IO.File.ReadAllLines(_PathDatosCalibracion[i]);
                for (int j = 0; j < 6; j++)
                {
                    int indice = datosCalibracion[j * 4].LastIndexOf("=");
                    serie._Offset[6 * i + j] = UInt16.Parse( datosCalibracion[j * 4].Substring(indice + 1));
                }
               
            }
            serie.EnviarOffsets();
        }

        public void PrepareEvent(string msg, double media)
        {
            dynamic temp = new ExpandoObject();
            temp.MEPMessage = msg;
            temp.MEPVoltage = media;

                DataEventArgs args = new DataEventArgs(temp);

                if (NewResultEvent != null)  // Por si nadie escucha el evento o esta en proceso de parar
                {
                    NewResultEvent(this, args);
                }

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
                        case "NumeroModulos":
                            Data.MEPNumeroModulos = _NumeroModulos;
                            break;
                        case "Offset":
                            Data.MEPOffset = LeerOffset();
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
            _Meplate = bool.Parse(parametros.MEPMeplate);
            _PathDatosCalibracion = new string[_NumeroModulos];
            _PathDatosCalibracion[0] = parametros.MEPCalibracion.calibracionModulo1;
            _PathDatosCalibracion[1] = parametros.MEPCalibracion.calibracionModulo2;
            _PathDatosCalibracion[2] = parametros.MEPCalibracion.calibracionModulo3;
            _PathDatosCalibracion[3] = parametros.MEPCalibracion.calibracionModulo4;
            _PathDatosCalibracion[4] = parametros.MEPCalibracion.calibracionModulo5;
            _PathDatosCalibracion[5] = parametros.MEPCalibracion.calibracionModulo6;
            calibracion = new CModulosCal(_NumeroModulos, _PathDatosCalibracion);
            _ListaOffset = new List<UInt16[]>();
            _UmbralAltoDeteccionCabeza = double.Parse(parametros.MEPUmbralAltoDeteccionCabeza);
            _UmbralBajoDeteccionCabeza = double.Parse(parametros.MEPUmbralBajoDeteccionCabeza);
            serie = new CSerie( this);
            
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
                            enviarOffsets(data.MEPValores, data.MEPReferencias);
                            enviarOffsets(data.MEPValores, data.MEPReferencias);
                            enviarOffsets(data.MEPValores, data.MEPReferencias);
                            break;
                        case "EnviarOffsetsSensor":
                            serie.EnviarOffset((int)data.MEPModulo, (int)data.MEPSensor, (UInt16)data.MEPOffset);
                            break;
                        case "ActualizaArchivoOffset":
                            ActualizaArchivoOffset();
                            break;
                        case "LeerOffsetsArchivo":
                            LeerOffsetsArchivo();
                            break;
                        case "VaciarBuffer":
                            VaciarBuffer();
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

       

        

        #endregion
    }

}
