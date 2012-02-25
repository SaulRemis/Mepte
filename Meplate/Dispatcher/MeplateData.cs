using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using SpinPlatform.Data;

namespace Meplate.Dispatcher
{
    class MeplateData: ModuleData
    {

        //parametros de salida

        HImage _Z= null; // imagen para pintar
        int _Perfiles = 0; // numero de perfiles
        double[,] _Puntos = null; // las coordenadas (x,y,z) de los max y min para enviar y la distancia entre max y min
        double[,] _Pixeles = null; //las coordenadas (f,c) de los pixeles para pintar con la distancia entre max y min
        double _Distancia_nominal = 0;  // distancia nominal para pintar todo a una altura a 
        int _NumMedidas = 0; // numero de defectos que se quieren encontrar (normalmente 5 o 10)

        public HImage Z { get { return _Z; } set { _Z = value; } }
        public int Perfiles { get { return _Perfiles; } set { _Perfiles = value; } }
        public double[,] Pixeles { get { return _Pixeles; } set { _Pixeles = value; } }
        public double[,] Puntos { get { return _Puntos; } set { _Puntos = value; } }
        public double Distancia_nominal { get { return _Distancia_nominal; } set { _Distancia_nominal = value; } }


        public void ResetData()
        {
          _Z = null; // imagen para pintar
           _Perfiles = 0; // numero de perfiles
           _Puntos = null; // las coordenadas (x,y,z) de los max y min para enviar y la distancia entre max y min
           _Pixeles = null; //las coordenadas (f,c) de los pixeles para pintar con la distancia entre max y min
         _Distancia_nominal = 0;  // distancia nominal para pintar todo a una altura a 
          _NumMedidas = 0; // numero de defectos que se quieren encontrar (normalmente 5 o 10)


        }
    }
}
