using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using SpinPlatform.Dispatcher;
using System.Diagnostics;
using SpinPlatform.Data;
using  SpinPlatform.FTP;


namespace Meplate
{
    class HiloProcesamiento: SpinThreadEvent
    {
        Meplate _Padre;
        SpinFTP _FTP; 
        CProcesamiento _Proc;
        dynamic _AuxLogCom, _AuxLog, _AuxLogError, _AuxFTP;
        double duracion;

        public HiloProcesamiento(Meplate padre, string name, dynamic parametros)
            : base(name)
        {
            _Proc = new CProcesamiento(parametros);
            _Padre = padre;
            _AuxLogCom = parametros.LogComunicacion;
            _AuxLog = parametros.LogMeplate;
            _AuxLogError = parametros.LogErrores;
            _AuxFTP = parametros;
            if (_Proc._EnviarFTP)  _FTP = new SpinFTP();
           
        }
        public override void FunctionToExecuteByThread()
        {
            try
            {
                List<CMedida> measurement = (List<CMedida>)((SharedData<List<CMedida>>)_SharedMemory["Chapas"]).Pop();

                if (measurement.Count > 5)
                {
                    // si ya he recibido el ancho lo uso para procesar , si no pongo ancho =0 y proceso sin el
                    if (!((SharedData<PlateID>)SharedMemory["IDChapa"]).Vacio)
                    {
                        PlateID id = ((PlateID)((SharedData<PlateID>)SharedMemory["IDChapa"]).Get(0));
                        duracion = _Proc.ProcesamientoDatos(measurement, id.Width, id.Tolerance1, id.Tolerance2);
                    }
                    else
                    {
                        duracion = _Proc.ProcesamientoDatos(measurement, 900, 5, 5);
                    }

                    ActualizarResultados();
                }
            }
            catch (Exception e)
            {
                _AuxLogError.LOGTXTMessage = "PROCESAMIENTO : ERROR en el procesamiento : " + e.Message ;
                     _Padre.LogError.SetData(ref _AuxLogError, "Informacion");
               
            }

        }
        public override void Initializate()
        {
            _WakeUpThreadEvent = _Events["ChapaMedida"];

            if (_Proc._EnviarFTP) _FTP.Init(_AuxFTP);
            if (_Proc._EnviarFTP) _FTP.Start();
            Trace.WriteLine("ADRI:   Entrando en el HILO PROCESAMIENTO");
            duracion = 0; 
        }
        public override void Closing()
        {

            Trace.WriteLine("ADRI:   Saliendo en el HILO PROCESAMIENTO");
            _Proc.Dispose();

        }
         void ActualizarResultados()
        {
            string id,id_file;
             double ancho,largo, espesor, tol1, tol2;

             if (!((SharedData<PlateID>)SharedMemory["IDChapa"]).Vacio)
             { 
                   
                PlateID temp = (PlateID)((SharedData<PlateID>)SharedMemory["IDChapa"]).Pop();
          
                id=temp.ID;
                id_file = id;
                ancho=temp.Width;
                largo= temp.Length;
                tol1 = temp.Tolerance1;
                espesor = temp.Thickness;
                tol2 = temp.Tolerance2;
            }
            else
            {
                DateTime now = DateTime.Now;
                id = now.Hour.ToString() + "/" + now.Minute.ToString() + "/" + now.Second.ToString() + now.Hour.ToString() + "/" + now.Minute.ToString() + "/" + now.Second.ToString();
                id_file = now.Day.ToString() + "_" + now.Month.ToString() + "_" + now.Year.ToString() + "_" + now.Hour.ToString() + "_" + now.Minute.ToString() + "_" + now.Second.ToString();
                ancho=0;
                largo=0;
                espesor = 0;
                tol1 = 0;
                tol2 = 0;
                _AuxLogError.LOGTXTMessage = "Plate measured with default Parameters. No ID sent";
                _Padre.LogError.SetData(ref _AuxLogError, "Informacion");
               
            }

             //envio los resultados al ordenador de proceso
             ((ComunicacionOP)((Meplate)_Padre)._DispatcherThreads["ComunicacionOP"]).SendMessageM5(id, _Proc.Puntos, _Proc._Decision,_Proc._Puntuacion*10);


             //lanzo el evento para pintar los resultados en el form
             ((SharedData<Resultados>)SharedMemory["Resultados"]).Set(0, new Resultados(_Proc.Z, _Proc.columnas, _Proc.Pixeles,_Proc.Puntos, _Proc.numeroMedidas, _Proc.distancia_a_la_chapa,id,ancho,largo,espesor,tol1,tol2,_Proc._Defectos1m,_Proc._Defectos2m));
             _Padre.PrepareEvent(_Name);

            

             // guardo al log los resultados
            _AuxLog.LOGTXTMessage = "New Plate measured : " + id + " Decission : " + _Proc._Decision + " Score : " + _Proc._Puntuacion + " Number of defects 1m : " + _Proc._Defectos1m + " Number of defects 2m : " + _Proc._Defectos2m;
            _Padre.Log.SetData(ref _AuxLog, "Informacion");

             //Envio por ftpj
            if (_Proc._EnviarFTP)
            {
                if (!File.Exists(id_file + ".tif"))
                {
                    File.Move("ZCORREGIDA.tif", id_file + ".tif");
                    _AuxLogError.LOGTXTMessage = "File " + id_file + ".tif" + " already exists in Imeges directory";
                    _Padre.Log.SetData(ref _AuxLogError, "Informacion");
                }
                _AuxFTP.FTP.FTPNombreArchivo = id_file + ".tif";
                _FTP.SetData(ref _AuxFTP, "SubirArchivo");
                if (_AuxFTP.FTPErrors != "")
                {

                    _AuxLogError.LOGTXTMessage = "Error sending via FTP : " + _AuxFTP.FTPErrors;
                    _Padre.Log.SetData(ref _AuxLogError, "Informacion");
                }
                File.Delete(id_file + ".tif"); 
            }

            

        } 
    }
}
