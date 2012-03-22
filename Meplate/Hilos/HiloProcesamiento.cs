using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpinPlatform.Dispatcher;
using System.Diagnostics;
using SpinPlatform.Data;
using SpinPlatform.IO;

namespace Meplate
{
    class HiloProcesamiento: SpinThreadEvent
    {
        Meplate _Padre;
        CProcesamiento _Proc;

        double duracion;

        public HiloProcesamiento(Meplate padre, string name, dynamic parametros)
            : base(name)
        {
            _Proc = new CProcesamiento(parametros);
            _Padre = padre;
        }
        public override void FunctionToExecuteByThread()
        {
            List<CMedida> measurement = (List<CMedida>)((SharedData<List<CMedida>>)_SharedMemory["Chapas"]).Pop();

            if (measurement.Count > 0)
            {
                duracion = _Proc.ProcesamientoDatos(measurement);
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
             ((SharedData<Resultados>)SharedMemory["Resultados"]).Set(0, new Resultados(_Proc.Z, _Proc.columnas, _Proc.Pixeles,_Proc.Puntos, _Proc.numeroMedidas, _Proc.distancia_a_la_chapa));
             ((SharedData<double[]>)SharedMemory["Offset"]).Set(0, _Proc.offset);
            _Padre.PrepareEvent(_Name);
             PlateID temp=(PlateID) ((SharedData<PlateID>)SharedMemory["IDChapa"]).Get(0);

             if (temp != null)
             {
                 ((ComunicacionOP)((Meplate)_Padre)._DispatcherThreads["ComunicacionOP"]).SendMessageM5(temp.ID, _Proc.Puntos);
             }
             else
             {
                 DateTime now = DateTime.Now;
                 string hora = now.Hour.ToString() + "/" + now.Minute.ToString() + "/" + now.Second.ToString() + now.Hour.ToString() + "/" + now.Minute.ToString() + "/" + now.Second.ToString();
                 ((ComunicacionOP)((Meplate)_Padre)._DispatcherThreads["ComunicacionOP"]).SendMessageM5(hora, _Proc.Puntos);
             
             }

        } 
    }
}
