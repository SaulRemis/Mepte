﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpinPlatform.Dispatcher;
using System.Diagnostics;
using SpinPlatform.Data;


namespace Meplate
{
    class HiloProcesamiento: SpinThreadEvent
    {
        Meplate _Padre;
        CProcesamiento _Proc;
        dynamic _AuxLogCom, _AuxLog, _AuxLogError;

        double duracion;

        public HiloProcesamiento(Meplate padre, string name, dynamic parametros)
            : base(name)
        {
            _Proc = new CProcesamiento(parametros);
            _Padre = padre;
            _AuxLogCom = parametros.LogComunicacion;
            _AuxLog = parametros.LogMeplate;
            _AuxLogError = parametros.LogErrores;
        }
        public override void FunctionToExecuteByThread()
        {
            List<CMedida> measurement = (List<CMedida>)((SharedData<List<CMedida>>)_SharedMemory["Chapas"]).Pop();

            if (measurement.Count > 0)
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
        public override void Initializate()
        {
            _WakeUpThreadEvent = _Events["ChapaMedida"];
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
            string id;
             double ancho,largo, espesor, tol1, tol2;
            PlateID temp = (PlateID)((SharedData<PlateID>)SharedMemory["IDChapa"]).Get(0);

            if (temp != null)
            {
                id=temp.ID;
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
                ancho=0;
                largo=0;
                espesor = 0;
                tol1 = 0;
                tol2 = 0;
                _AuxLogError.LOGTXTMessage = "Plate measured with default Parameters. No ID sent";
                _Padre.Log.SetData(ref _AuxLogError, "Informacion");
               
            }
             ((ComunicacionOP)((Meplate)_Padre)._DispatcherThreads["ComunicacionOP"]).SendMessageM5(id, _Proc.Puntos, _Proc._Decision,_Proc._Puntuacion*10);
             ((SharedData<Resultados>)SharedMemory["Resultados"]).Set(0, new Resultados(_Proc.Z, _Proc.columnas, _Proc.Pixeles,_Proc.Puntos, _Proc.numeroMedidas, _Proc.distancia_a_la_chapa,id,ancho,largo,espesor,tol1,tol2,_Proc._Defectos1m,_Proc._Defectos2m));
             ((SharedData<Offset>)SharedMemory["Offset"]).Set(0, new Offset(_Proc._ValoresMedios,_Proc._Referencias));
            _Padre.PrepareEvent(_Name);
            _AuxLog.LOGTXTMessage = "New Plate measured : " + id + " Decission : " + _Proc._Decision + " Score : " + _Proc._Puntuacion + " Number of defects 1m : " + _Proc._Defectos1m + " Number of defects 2m : " + _Proc._Defectos2m;
            _Padre.Log.SetData(ref _AuxLog, "Informacion");
            

        } 
    }
}
