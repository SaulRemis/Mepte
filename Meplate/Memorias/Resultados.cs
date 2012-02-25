using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Threading;

namespace Meplate
{
    [Serializable]
        public class Resultados
        {
        //mienbros privados
          HImage _Z; // imagen para pintar
          int _Perfiles; // numero de perfiles
          double[,] _Puntos; // las coordenadas (x,y,z) de los max y min para enviar y la distancia entre max y min
          double[,] _Pixeles; //las coordenadas (f,c) de los pixeles para pintar con la distancia entre max y min
          double _Distancia_nominal;  // distancia nominal para pintar todo a una altura a 
          int _NumMedidas; // numero de defectos que se quieren encontrar (normalmente 5 o 10)

        //descriptores de acceso
          public HImage Z { get { return _Z ; }}
          public int Perfiles { get { return _Perfiles; } }
          public double[,] Pixeles { get { return _Pixeles; } }
          public double[,] Puntos { get { return _Puntos; } }
          public double Distancia_nominal { get { return _Distancia_nominal; } }

        //metodos
          public Resultados(HImage imagen,int medidas,double[,] pixeles,int numMedidas, double distancia)
            {
                _Z = imagen.CopyImage();
                _Perfiles = medidas;
                //_Pixeles = new double[5, numMedidas];
                _Pixeles = pixeles;
                _NumMedidas=numMedidas;               
                _Distancia_nominal = distancia;

            }
         

           
 
    }
}
