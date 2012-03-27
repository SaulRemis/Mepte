using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SpinPlatform.Dispatcher;
using SpinPlatform.Config;
using SpinPlatform.Data;
using SpinPlatform.Errors;
using SpinPlatform.Comunicaciones;
using System.Dynamic;
using SpinPlatform;
using System.Windows.Forms;

namespace OPSaul
{
    class OPSaul: SpinDispatcher
    {
        public dynamic data = new ExpandoObject();
        public Form _formulario;
        #region Funciones Publicas
        public OPSaul(Form1 formulario)
        {
            _formulario = formulario;
        }

        public void Init(ref dynamic parameters)
        {
            SpinConfig con = new SpinConfig();
            parameters.CONFFile = SpinConfigConstants.SPIN_CONFIG_XML_NAME;
            con.GetData(ref parameters, "Parametros");

          
            _DispatcherThreads.Add("ComunicacionMeplaca", new ComunicacionMeplaca(this, parameters, "ComunicacionMeplaca"));

            ConnectMemory("ResultadosUI", new SharedData<Message>(10), "ComunicacionMeplaca");

        }

        public override void SetData(ref dynamic obj, params string[] parameters)
        {
            data.FORMPlate = obj.FORMPlate;
            data.FORMWidth = obj.FORMWidth;
            data.FORMLength = obj.FORMLength;
            ((ComunicacionMeplaca)_DispatcherThreads["ComunicacionMeplaca"]).SendMessage("M9" + data.FORMPlate);
        }

        public override void GetData(ref dynamic Data, params string[] parameters)
        {
            Data.MEPReturnedData = parameters;
            try
            {
                foreach (string parameter in parameters)
                {
                    switch (parameter)
                    {
                        case "FORMGetData":
                            Data.Data = ((Form1)_formulario).getDatos();
                            break;

                        case "COMGetSocketLine":
                            Data.ResultadosUI = ((SharedData<Message>)_DispatcherSharedMemory["ResultadosUI"]).Pop();
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


        public void PrepareEvent(string thread)
        {
            dynamic data = new ExpandoObject();
            switch (thread)
            {
                case "ComunicacionMeplaca":
                    GetData(ref data, "COMGetSocketLine");
                    break;
                default:
                    break;
            }

            DataEventArgs args = new DataEventArgs(data.ResultadosUI);
            if (Status == SpinDispatcherStatus.Running)  // Por si nadie escucha el evento o esta en proceso de parar
            {
                SetEvent(args);
            }

        }

        #endregion
    }
}
