using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using SpinPlatform.Data;

namespace Meplate
{
    class MeplateData: ModuleData
    {
        //parametros de entrada

        public bool GetResultados = false;
        public bool GetInformacion = false;
        public bool SetEventoEmpezarMedida = false;
        public bool SetEventoFinalizarMedida = false;


        //parametros de salida

        Resultados _Resultados = null; // estructura con los datos devueltos por el hilo procesamiento
        Informacion _Informacion = null;// estructura con los datos devueltos por el hilo Adquisicion

        public Resultados Resultados
        {
            get { return _Resultados; }
            set { _Resultados = value; }
        }
       
        public Informacion Informacion
        {
            get { return _Informacion; }
            set { _Informacion = value; }
        }

        public void ResetData()
        {
         _Resultados = null; // estructura con los datos devueltos por el hilo procesamiento
         _Informacion = null;// estructura con los datos devueltos por el hilo Adquisicion
        }
    }
}
