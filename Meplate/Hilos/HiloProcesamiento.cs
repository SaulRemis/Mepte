using System;
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
                // si ya he recibido el ancho lo uso para procesar , si no pongo ancho =0 y proceso sin el
                if  (!((SharedData<PlateID>)SharedMemory["IDChapa"]).Vacio) 
                    duracion = _Proc.ProcesamientoDatos(measurement,((PlateID)((SharedData<PlateID>)SharedMemory["IDChapa"]).Get(0)).Width);
                  
                else duracion = _Proc.ProcesamientoDatos(measurement, 900);
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
               
            }
             ((ComunicacionOP)((Meplate)_Padre)._DispatcherThreads["ComunicacionOP"]).SendMessageM5(id, _Proc.Puntos, _Proc._Decision,_Proc._Puntuacion);
             ((SharedData<Resultados>)SharedMemory["Resultados"]).Set(0, new Resultados(_Proc.Z, _Proc.columnas, _Proc.Pixeles,_Proc.Puntos, _Proc.numeroMedidas, _Proc.distancia_a_la_chapa,id,ancho,largo,espesor,tol1,tol2));
             ((SharedData<Offset>)SharedMemory["Offset"]).Set(0, new Offset(_Proc._ValoresMedios,_Proc._Referencias));
            _Padre.PrepareEvent(_Name);
            

        } 
    }
}
