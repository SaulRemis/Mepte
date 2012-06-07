using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpinPlatform.Dispatcher;
using SpinPlatform.Data;
using System.Threading;
using System.Dynamic;
using SpinPlatform.Config;
using SpinPlatform.Log;

namespace Meplate
{
    /// <summary>
    /// Modulo para realizar mediciones de planitud sobre chapones de acero usando el sensor MEPLATE
    /// </summary>
    public class Meplate: SpinDispatcher
    {
        //Objetos auxiliares
        dynamic configuracion;
       public  SpinLogFile Log = new SpinLogFile();
       public SpinLogFile LogCom = new SpinLogFile();
       public SpinLogFile LogError = new SpinLogFile();


        public Meplate()
        {
            configuracion = new ExpandoObject();
         }
        /// <summary>
        /// Inicializa el Modulo MEPLATE.
        /// </summary>
        public void Init(dynamic parameters)
        {
            SpinConfig con = new SpinConfig();
            configuracion.CONFFile = SpinConfigConstants.SPIN_CONFIG_XML_NAME;
            con.GetData(ref configuracion,"Parametros");

            
            // Hilos
            _DispatcherThreads.Add("Adquisicion", new HiloAdquisicion(this, "Adquisicion", configuracion));
            _DispatcherThreads.Add("Procesamiento", new HiloProcesamiento(this, "Procesamiento", configuracion));
            _DispatcherThreads.Add("ComunicacionTarjeta", new ComunicacionTarjeta(this, "ComunicacionTarjeta", configuracion));
            _DispatcherThreads.Add("ComunicacionOP", new ComunicacionOP(this, "ComunicacionOP", configuracion));
            

            //memorias Ompartidas
            ConnectMemory("Chapas", new SharedData<List<CMedida>>(20), "Adquisicion", "Procesamiento");
            ConnectMemory("Offset", new SharedData<Offset>(1), "Adquisicion", "Procesamiento");
            ConnectMemory("Informacion", new SharedData<Informacion>(1), "Adquisicion");
            ConnectMemory("Resultados", new SharedData<Resultados>(1), "Procesamiento");
            ConnectMemory("Velocidad", new SharedData<Tarjeta>(1), "Adquisicion", "ComunicacionTarjeta");
            ConnectMemory("IDChapa", new SharedData<PlateID>(1), "Procesamiento", "ComunicacionOP");


            //Eventos de sincronizacion
            CreateEvent("ChapaMedida", new AutoResetEvent(false), "Adquisicion", "Procesamiento");
            CreateEvent("ComenzarMedida", new AutoResetEvent(false), "Adquisicion", "ComunicacionTarjeta");
            CreateEvent("FinalizarMedida", new AutoResetEvent(false), "Adquisicion", "ComunicacionTarjeta");
            CreateEvent("AbortarMedida", new AutoResetEvent(false), "Adquisicion", "ComunicacionTarjeta");
            CreateEvent("IDChapa", new AutoResetEvent(false), "Procesamiento", "ComunicacionOP");



            //Inicio el Log
            Log.Init(configuracion.LogMeplate);
            LogCom.Init(configuracion.LogComunicacion);
            LogError.Init(configuracion.LogErrores);
            configuracion.LOGTXTMessage = "MEPLATE is Starting";
            Log.SetData(ref configuracion, "Informacion");
            



        }
        /// <summary>
        /// Obtiene datos del Modulo MEPLATE. 
        /// </summary>
        /// <param name="Data">
        /// Variable dinamica donde guardar los resultados: \n
        /// MEPResultados (Resultados) resultados del procesamiento \n
        /// MEPInformacion (Informacion) Informacion sobre el procesamiento \n
        /// MEPVelocidad (double) Velocidad de Avance de la chapa \n
        /// MEPOPConnected (bool) Estado de conexion del Ordenador de Proceso \n
        /// MEPTarjetaConnected (bool) Estado de conexion de la tarjeta de adquisicion \n
        /// </param>
        /// <param name="parameters">
        /// "Resultados" - Obtiene Los ultimos resultados medidos por el MEPLATE \n
        /// "Informacion"  - Obtiene la informacion de procesamiento del MEPLATE \n
        /// "Estado"  - Obtiene la informacion de conexiones y velocidad del MEPLATE \n
        ///  </param>
        public override void GetData(ref dynamic Data, params string[] parameters)
        {
            Data.MEPReturnedData = parameters;
            try
            {
                foreach (string parameter in parameters)
                {
                    switch (parameter)
                    {
                        case "Resultados":
                            Data.MEPResultados = (Resultados)((SharedData<Resultados>)_DispatcherSharedMemory["Resultados"]).Get(0);
                            break;

                        case "Estado":
                            Tarjeta tar = ((Tarjeta)((SharedData<Tarjeta>)_DispatcherSharedMemory["Velocidad"]).Get(0));

                            if (tar != null)
                                Data.MEPVelocidad = tar.Velocidad;
                            else Data.MEPVelocidad = 0.0;
                            ((ComunicacionOP)_DispatcherThreads["ComunicacionOP"])._server.GetData(ref Data,"EstadoSocket");
                            Data.MEPOPConnected = Data.COMSocketDatosConnected;
                            ((ComunicacionTarjeta)_DispatcherThreads["ComunicacionTarjeta"])._server.GetData(ref Data, "EstadoSocket");
                            Data.MEPTarjetaConnected = Data.COMSocketDatosConnected;
                            Data.MEPMidiendo = ((ComunicacionTarjeta)_DispatcherThreads["ComunicacionTarjeta"])._Midiendo;
                            break;
                        case "Informacion":
                            Data.MEPInformacion = (Informacion)((SharedData<Informacion>)_DispatcherSharedMemory["Informacion"]).Get(0);
                        break;
                       
                        case "Meplaca":
                        Data.MEPMeplaca = ((HiloAdquisicion)_DispatcherThreads["Adquisicion"])._Meplaca;
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

        public void PrepareEvent(string thread )
        {
       if (Status == SpinDispatcherStatus.Running)  // Por si nadie escucha el evento o esta en proceso de parar
            {
           dynamic temp = new ExpandoObject();

            switch (thread)
            {
                case "Procesamiento":
                    GetData(ref temp, "Resultados");
                    break;
                case "Adquisicion":
                    GetData(ref temp, "Informacion");
                    break;
                default:
                    break;
            }

            DataEventArgs args = new DataEventArgs(temp);

                SetEvent(args);
            }
             
        }

        /// <summary>
        /// Establece campos en el modulo Meplate \n
        /// </summary>
        /// <param name="data">
        /// Variable dinamica de donde obtener los datos a establecer \n
        /// </param>
        /// <param name="parameters">
        /// "EventoComenzarMedida" - lanza el evento de iniciar una nueva medicion \n
        /// "EventoFinalizarMedida" -lanza el evento de finalizar la medicion \n
        /// "ConectarTarjeta"  - Intenta reconectarse con la tarjeta de adquisicion de la velocidad \n
        /// </param>
        public override void SetData(ref dynamic Data, params string[] parameters)
        {
            try
            {
                foreach (string parameter in parameters)
                {
                    switch (parameter)
                    {
                        case "EventoComenzarMedida":
                            _Events["ComenzarMedida"].Set();
                            break;
                        case "EventoFinalizarMedida":
                            _Events["FinalizarMedida"].Set();
                            break;
                        case "ConectarTarjeta":                        
                            ((ComunicacionTarjeta)_DispatcherThreads["ComunicacionTarjeta"])._server.Start();
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

      

    }
}
