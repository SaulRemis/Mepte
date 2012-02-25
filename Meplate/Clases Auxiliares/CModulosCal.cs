using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Meplate
{
    class CModulosCal
    {
        public CModulo[] Modulos;
        public CModulosCal(int numeroModulos, string[] pathDatosCalibracion)
        {
            Modulos = new CModulo[numeroModulos];
            for (int i = 0; i < numeroModulos; i++)
            {
                Modulos[i] = new CModulo();
            }
            for (int i = 0; i < numeroModulos; i++)
            {
                string[] datosCalibracion = System.IO.File.ReadAllLines(pathDatosCalibracion[i]);
                for (int j = 0; j < Modulos[i].Sensores.Count(); j++)
                {
                    int indice = datosCalibracion[j * 4].LastIndexOf("=");
                    datosCalibracion[j * 4] = datosCalibracion[j * 4].Substring(indice + 1);
                    //datosCalibracion[j * 4] = datosCalibracion[j * 4].Replace(".", ",");
                    indice = datosCalibracion[j * 4 + 1].LastIndexOf("=");
                    datosCalibracion[j * 4 + 1] = datosCalibracion[j * 4 + 1].Substring(indice + 1);
                   // datosCalibracion[j * 4 + 1] = datosCalibracion[j * 4 + 1].Replace(".", ",");
                    indice = datosCalibracion[j * 4 + 2].LastIndexOf("=");
                    datosCalibracion[j * 4 + 2] = datosCalibracion[j * 4 + 2].Substring(indice + 1);
                    //datosCalibracion[j * 4 + 2] = datosCalibracion[j * 4 + 2].Replace(".", ",");
                    indice = datosCalibracion[j * 4 + 3].LastIndexOf("=");
                    datosCalibracion[j * 4 + 3] = datosCalibracion[j * 4 + 3].Substring(indice + 1);
                   // datosCalibracion[j * 4 + 3] = datosCalibracion[j * 4 + 3].Replace(".", ",");

                    Modulos[i].Sensores[j] = new CSensor(UInt16.Parse(datosCalibracion[j * 4]), 
                                                         double.Parse(datosCalibracion[j * 4 + 1]),
                                                         double.Parse(datosCalibracion[j * 4 + 2]),
                                                         double.Parse(datosCalibracion[j * 4 + 3]));
                }
            }

        }

    }
    class CModulo
    {
        public CSensor[] Sensores;
        public CModulo()
        {
            Sensores = new CSensor[6];
        }

    }
    class CSensor
    {
        public UInt16 off;
        public double a;
        public double b;
        public double c;
        public CSensor()
        {
        }
        public CSensor(UInt16 OFF, double A, double B, double C)
        {
            off = OFF;
            a = A;
            b = B;
            c = C;
        }
    }
}
