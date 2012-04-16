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
          string _ID;  // Id del Chapon medido 
          double _Ancho; // Ancho del chapon medido
          double _Longitud; // Longitud del chapon medido
          double _Thickness;
          double _Tolerance1;
          double _Tolerance2;


        //descriptores de acceso
          public HImage Z { get { return _Z ; }}
          public int Perfiles { get { return _Perfiles; } }
          public double[,] Pixeles { get { return _Pixeles; } }
          public double[,] Puntos { get { return _Puntos; } }
          public double Distancia_nominal { get { return _Distancia_nominal; } }
          public double Ancho { get { return _Ancho; } }
          public double Longitud { get { return _Longitud; } }
          public string ID { get { return _ID; } }
          public double Thickness { get { return _Thickness; } }
          public double Tolerance1 { get { return _Tolerance1; } }
          public double Tolerance2 { get { return _Tolerance2; } }


        //metodos
          public Resultados(HImage imagen, int medidas, double[,] pixeles, double[,] puntos, int numMedidas, double distancia, string id, double ancho, double largo, double thickness, double tol1, double tol2)
            {
                _Z = imagen.CopyImage();
                _Perfiles = medidas;
                _Puntos = (double[,])puntos.Clone();
                _Pixeles = (double[,])pixeles.Clone();
                _NumMedidas=numMedidas;               
                _Distancia_nominal = distancia;
                _ID = id;
                _Ancho = ancho;
                _Longitud = largo;
                _Thickness = thickness;
                _Tolerance1 = tol1;
                _Tolerance2 = tol2;

            }
         

           
 
    }
}
