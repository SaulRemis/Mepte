using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpinPlatform.Dispatcher;
using SpinPlatform.Data;
using System.Threading;

namespace Meplate
{
    public class Meplate: SpinDispatcher
    {
            //Clases auxiliares
        CArchivos arch ;

        public Meplate()
        {
            _DispatcherThreads.Add("Adquisicion", new HiloAdquisicion("Adquisicion",arch));
            _DispatcherThreads.Add("Procesamiento", new HiloProcesamiento((Meplate)this, "Procesamiento",arch));


            ConnectMemory("Chapas", new SharedData<List<CMedida>>(20), "Adquisicion", "Procesamiento");
            ConnectMemory("Resultados", new SharedData<Resultados>(1), "Procesamiento");

            CreateEvent("ChapaMedida", new AutoResetEvent(false), "Adquisicion", "Procesamiento");
            CreateEvent("ComenzarMedida", new AutoResetEvent(false), "Adquisicion");
            CreateEvent("FinalizarMedida", new AutoResetEvent(false), "Adquisicion");

            //Clases auxiliares
            arch = new CArchivos("MeplateIni.xml");

         }

        public override object GetData(object parameters)
        {
            if (parameters.GetType()==typeof(MeplateData))
            {
                MeplateData data = (MeplateData)parameters;

                data.ResetData();
                if (data.GetResultados)
                {
                    Resultados temp = ( Resultados)((SharedData< Resultados>)_DispatcherSharedMemory["Resultados"]).Get(0);
                    data.Z=temp.Z;
                    data.Puntos= temp.Puntos;
                    data.Pixeles= temp.Pixeles;
                    data.Perfiles = temp.Perfiles;
                    data.Distancia_nominal = temp.Distancia_nominal;

                }
                return data;
            }
            else        return null;
        }
        public void PrepareEvent(string thread )
        {
          
            MeplateData temp = new MeplateData();

            switch (thread)
            {
                case "Consumidor":
                    temp.GetResultados = true;
                    break;
                default:
                    break;
            }

            object data = GetData(temp);
            DataEventArgs args = new DataEventArgs(data);
            if (Status == SpinDispatcherStatus.Running)  // Por si nadie escucha el evento o esta en proceso de parar
            {
                SetEvent(args);
            }
             
        }

    }
}
