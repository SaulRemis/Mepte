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

        public HiloProcesamiento(Meplate padre, string name, CArchivos arch)
            : base(name)
        {
            _Proc = new CProcesamiento(arch);
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
             ((SharedData<Resultados>)SharedMemory["Resultados"]).Set(0, new Resultados(_Proc.Z, _Proc.columnas, _Proc.puntos, _Proc.numeroMedidas, _Proc.distancia_a_la_chapa));
            _Padre.PrepareEvent(_Name);

        } 
    }
}
