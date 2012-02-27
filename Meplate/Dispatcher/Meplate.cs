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
            //Clases auxiliares
            arch = new CArchivos("MeplateIni.xml");


            _DispatcherThreads.Add("Adquisicion", new HiloAdquisicion((Meplate)this, "Adquisicion", arch));
            _DispatcherThreads.Add("Procesamiento", new HiloProcesamiento((Meplate)this, "Procesamiento",arch));


            ConnectMemory("Chapas", new SharedData<List<CMedida>>(20), "Adquisicion", "Procesamiento");
            ConnectMemory("Informacion", new SharedData<Informacion>(1), "Adquisicion");
            ConnectMemory("Resultados", new SharedData<Resultados>(1), "Procesamiento");

            CreateEvent("ChapaMedida", new AutoResetEvent(false), "Adquisicion", "Procesamiento");
            CreateEvent("ComenzarMedida", new AutoResetEvent(false), "Adquisicion");
            CreateEvent("FinalizarMedida", new AutoResetEvent(false), "Adquisicion");



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
                    data.Resultados=temp;

                }
                if (data.GetInformacion)
                {
                    Informacion temp = (Informacion)((SharedData<Informacion>)_DispatcherSharedMemory["Informacion"]).Get(0);
                    data.Informacion = temp;


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
                case "Procesamiento":
                    temp.GetResultados = true;
                    break;
                case "Adquisicion":
                    temp.GetInformacion = true;
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
        public override void SetData(object parameters)
        {
            if (parameters.GetType() == typeof(MeplateData))
            {
                MeplateData data = (MeplateData)parameters;

                if (data.SetEventoEmpezarMedida)
                {
                    _Events["ComenzarMedida"].Set();
                }
                if (data.SetEventoFinalizarMedida)
                {
                    _Events["FinalizarMedida"].Set();
                
                }

            }

        }
    }
}
