using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Collections;
using SpinPlatform.IO;

namespace Meplate
{
    class CProcesamiento
    {

        public int numeroModulos, numeroMedidas;
        public HImage X, Y, Z;
        public int filas, columnas;
        double distancia_entre_sensores ;
        public double distancia_a_la_chapa ;
        double sigma_bordes;
        double umbral_bordes;
        public double[] offset;
        public double BI, BD;
        public int borde_derecho, borde_izquierdo;
        public double[,] puntos;


        public CProcesamiento(CArchivos arch)
        {
            numeroModulos = int.Parse(arch.LeerXML("numeroModulos"));
            distancia_entre_sensores = double.Parse(arch.LeerXML("distancia_entre_sensores"));
            distancia_a_la_chapa = double.Parse(arch.LeerXML("distancia_nominal_trabajo"));
            numeroMedidas = int.Parse(arch.LeerXML("numeroMedidas"));
            sigma_bordes = double.Parse(arch.LeerXML("sigma_bordes"));
            umbral_bordes = double.Parse(arch.LeerXML("umbral_bordes"));
            filas = numeroModulos * 6;
            offset = new double[filas];
            puntos = new double[5, numeroMedidas];

        }
        public double ProcesamientoDatos(List<CMedida> measurement)
        {
            DateTime t1 = DateTime.Now;

            Inicializar(measurement);
            ObtenerImagenes(measurement);
            //proc.ObtenerBordes();
            ObtenerBordes(900);
            CorregirImagen();
            CalcularDefectos_1metro();


            DateTime t2 = DateTime.Now;
            TimeSpan elapsedtime = t2 - t1;

            return elapsedtime.Milliseconds;


        }
        public void Dispose()
        {

            if (Z != null) Z.Dispose();
            if (X != null) X.Dispose();
            if (Y != null) Y.Dispose();
        
        }

        private void Inicializar(List<CMedida> medidas)
        {

            if (Z != null) Z.Dispose();
            if (X != null) X.Dispose();
            if (Y != null) Y.Dispose();

            offset.Initialize();
            columnas = medidas.Count;
            X = new HImage("real", columnas, filas);
            Y = new HImage("real", columnas, filas);
            Z = new HImage("real", columnas, filas);

            BI = 0;
            BD = 0;

        }
        private void ObtenerImagenes(List<CMedida> medidas)
        {

           
            if (medidas.Count > 0)
            {


                double distancia_inicial = medidas[0].distancia;

                for (int i = 0; i < columnas; i++)
                {
                    for (int j = 0; j < filas; j++)
                    {

                        X.SetGrayval(j, i, distancia_entre_sensores);
                        Y.SetGrayval(j, i, medidas[i].distancia - distancia_inicial);
                        Z.SetGrayval(j, i, medidas[i].perfil[j]);
                    }
                }
            }
            Z.WriteImage("tiff", 0, "Z.jpg");

        }
        private void CorregirImagen()
        {

            HImage media = Z.MeanImage(columnas - 1, 1);
            double valor, med;


            for (int i = 0; i < columnas; i++)
            {
                for (int j = 0; j < filas; j++)
                {
                    if (j < borde_izquierdo || j > borde_derecho)
                    {

                        valor = Z.GetGrayval(j, i);
                        med = media.GetGrayval(j, 0);
                        Z.SetGrayval(j, i, distancia_a_la_chapa + valor - med);
                    }
                }
            }

            media.Dispose();


        }
        public void ObtenerBordes(double ancho)
        {

            ObtenerBordesConAncho(ancho);
            if (borde_derecho == filas - 1 && borde_izquierdo == 0)
            {
                ObtenerBordesSinAncho();
            }

            // pongo a 0 todos los valores fuera de la chapa

            for (int i = 0; i < columnas; i++)
            {
                for (int j = 0; j < filas; j++)
                {

                    if (j < borde_izquierdo || j > borde_derecho)
                    {
                        Z.SetGrayval(j, i, 0);
                    }
                }
            }


            // HRegion Chapa = new HRegion((double)borde_izquierdo+1 , (double)0, (double)borde_derecho-1 , (double)columnas - 1);
            //HRegion imagen = new HRegion((double)0, (double)0, (double)filas - 1, (double)columnas - 1);
            //Z.ReduceDomain(Chapa);

            // Z.WriteImage("tiff", 0, "Z_sinbordes");


        }
        public void ObtenerBordesSinAncho()
        {
            HTuple filas_borde, columnas_borde, amplitude, distance, indices;

            // int ancho, alto;
            // Z.GetImageSize(out ancho, out alto);

            int anchura = (int)Math.Round((double)(columnas / 2.0));
            if (anchura > 100) anchura = 100;
            if (anchura > 5)
            {
                HMeasure bordes = new HMeasure((double)filas / 2 - 1, (double)columnas / 2 - 1, (double)Math.PI / 2.0, anchura, (int)Math.Round((double)(filas / 2.0)), columnas, filas, "nearest_neighbor");
                //HMeasure bordes = new HMeasure(20, 20, -(double)Math.PI / 2.0, 5,5, columnas , filas , "nearest_neighbor");
                bordes.MeasurePos(Z, sigma_bordes, umbral_bordes, "all", "all", out filas_borde, out columnas_borde, out amplitude, out distance);
                amplitude = amplitude.TupleAbs();
                indices = amplitude.TupleSortIndex();
                if (indices.Length > 1)
                {
                    BD = filas_borde[indices[0]];
                    BI = filas_borde[indices[1]];

                    if (BI > BD)
                    {
                        double temp = BD;
                        BD = BI;
                        BI = temp;
                    }
                    borde_derecho = (int)Math.Floor(BD) - 1;
                    borde_izquierdo = (int)Math.Ceiling(BI) + 1;
                    bordes.Dispose();
                }
                else
                {
                    borde_derecho = filas - 1;
                    borde_izquierdo = 0;
                }
            }
            else
            {
                borde_derecho = filas - 1;
                borde_izquierdo = 0;
            }
            // pongo a 0 todos los valores fuera de la chapa




        }
        public void ObtenerBordesConAncho(double ancho)
        {
            HTuple filas_borde, columnas_borde, amplitude, distance;

            double max = 0;
            double distancia = 0;

            // int modulos = (int)Math.Round(ancho / distancia_entre_sensores);
            int modulos = (int)Math.Round(ancho / 52);
            int anchura = (int)Math.Round((double)(columnas / 2.0));
            if (anchura > 100) anchura = 100;
            if (anchura > 5)
            {
                HMeasure bordes = new HMeasure((double)filas / 2 - 1, (double)columnas / 2 - 1, (double)Math.PI / 2.0, anchura, (int)Math.Round((double)(filas / 2.0)), columnas, filas, "nearest_neighbor");
                //HMeasure bordes = new HMeasure(20, 20, -(double)Math.PI / 2.0, 5,5, columnas , filas , "nearest_neighbor");
                bordes.MeasurePos(Z, sigma_bordes, umbral_bordes, "all", "all", out filas_borde, out columnas_borde, out amplitude, out distance);
                // bordes.MeasurePairs(Z, sigma_bordes, 0.01, "all", "all", out rowEdgeFirst, out columnEdgeFirst, out amplitudeFirst, out rowEdgeSecond, out columnEdgeSecond, out amplitudeSecond, out intraDistance, out interDistance);

                if (filas_borde.Length > 1)
                {
                    for (int i = 0; i < filas_borde.Length; i++)
                    {
                        for (int j = 0; j < filas_borde.Length; j++)
                        {
                            distancia = Math.Abs(filas_borde[i].D - filas_borde[j].D);
                            if (Math.Abs(distancia - modulos) < 4.0)
                            {
                                if (Math.Abs(amplitude[i].D - amplitude[j].D) > max)
                                {
                                    max = Math.Abs(amplitude[i].D - amplitude[j].D);
                                    BD = filas_borde[i];
                                    BI = filas_borde[j];


                                }
                            }
                        }

                    }
                    if (BI == 0 && BD == 0)
                    {
                        borde_derecho = filas - 1;
                        borde_izquierdo = 0;

                    }
                    else
                    {
                        if (BI > BD)
                        {
                            double temp2 = BD;
                            BD = BI;
                            BI = temp2;
                        }
                        borde_derecho = (int)Math.Floor(BD) - 1;
                        borde_izquierdo = (int)Math.Ceiling(BI) + 1;
                    }


                }
                else
                {
                    borde_derecho = filas - 1;
                    borde_izquierdo = 0;
                }

                bordes.Dispose();
            }
            else
            {
                borde_derecho = filas - 1;
                borde_izquierdo = 0;
            }




        }
        private void CalcularDefectos_1metro()
        {

            HTuple filas_max, columnas_max, filas_min, columnas_min, diff;


            // "fibras" o "circulos" 
            //Planitud_regla_1m(Z,"fibras", out filas_max, out columnas_max, out filas_min, out columnas_min, out diff);
            Planitud_regla_1m_cuadricula(Z, out filas_max, out columnas_max, out filas_min, out columnas_min, out diff);

            for (int i = 0; i <filas_max.Length; i++)
            {
                puntos[0, i] = filas_max.DArr[i];
                puntos[1, i] = columnas_max.DArr[i];
                puntos[2, i] = filas_min.DArr[i];
                puntos[3, i] = columnas_min.DArr[i];
                puntos[4, i] = diff.DArr[i];
            }



        }
        private void Planitud_regla_1m(HObject ho_Imagen, string modo, out HTuple hv_filasmaximos, out HTuple hv_columnasmaximos, out HTuple hv_filasminimos, out HTuple hv_columnasminimos, out HTuple hv_Diff)
        {


            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];
            long SP_O = 0;

            // Local iconic variables 

            HObject ho_ImagenOut, ho_LocalMaxima, ho_ConnectedRegions;
            HObject ho_Circle = null, ho_ImageReduced1 = null, ho_Circle2 = null;


            // Local control variables 

            HTuple hv_Area, hv_Row1, hv_Column1, hv_filas;
            HTuple hv_columnas, hv_Grayval, hv_Indices, hv_Selected_indices;
            HTuple hv_valoresmaximos, hv_Index, hv_Min = new HTuple();
            HTuple hv_Max = new HTuple(), hv_Range = new HTuple(), hv_Area1 = new HTuple();
            HTuple hv_Row2 = new HTuple(), hv_Column2 = new HTuple(), hv_valoresminimos;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ImagenOut);
            HOperatorSet.GenEmptyObj(out ho_LocalMaxima);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_Circle);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
            HOperatorSet.GenEmptyObj(out ho_Circle2);

            ho_ImagenOut.Dispose();
            HOperatorSet.CopyObj(ho_Imagen, out ho_ImagenOut, 1, -1);
            OTemp[SP_O] = ho_ImagenOut.CopyObj(1, -1);
            SP_O++;
            ho_ImagenOut.Dispose();
            HOperatorSet.GaussImage(OTemp[SP_O - 1], out ho_ImagenOut, 5);
            OTemp[SP_O - 1].Dispose();
            SP_O = 0;

            ho_LocalMaxima.Dispose();
            HOperatorSet.LocalMax(ho_ImagenOut, out ho_LocalMaxima);

            ho_ConnectedRegions.Dispose();
            HOperatorSet.Connection(ho_LocalMaxima, out ho_ConnectedRegions);
            HOperatorSet.AreaCenter(ho_ConnectedRegions, out hv_Area, out hv_Row1, out hv_Column1);
            HOperatorSet.TupleRound(hv_Row1, out hv_filas);
            HOperatorSet.TupleRound(hv_Column1, out hv_columnas);
            HOperatorSet.GetGrayval(ho_ImagenOut, hv_filas, hv_columnas, out hv_Grayval);
            HOperatorSet.TupleSortIndex(hv_Grayval, out hv_Indices);
            HOperatorSet.TupleSort(hv_Grayval, out hv_Grayval);


            if (hv_Indices.TupleLength() > numeroMedidas)
            {
                HOperatorSet.TupleLastN(hv_Indices, (new HTuple(hv_Indices.TupleLength())) - numeroMedidas,
               out hv_Selected_indices);
                HOperatorSet.TupleLastN(hv_Grayval, (new HTuple(hv_Grayval.TupleLength())) - numeroMedidas,
                    out hv_valoresmaximos);

            }
            else
            {

                hv_Selected_indices = hv_Indices;
                hv_valoresmaximos = hv_Grayval;
            }


            HOperatorSet.TupleSelect(hv_filas, hv_Selected_indices, out hv_filasmaximos);
            HOperatorSet.TupleSelect(hv_columnas, hv_Selected_indices, out hv_columnasmaximos);
            hv_filasminimos = new HTuple();
            hv_columnasminimos = new HTuple();
            for (hv_Index = 0; (int)hv_Index <= (int)((new HTuple(hv_columnasmaximos.TupleLength()
                )) - 1); hv_Index = (int)hv_Index + 1)
            {
                //depende si quiero medir en la fibra o en el circulo , creo una region o otra

                ho_Circle.Dispose();

                if (modo=="fibras")

                HOperatorSet.GenRegionLine(out ho_Circle, hv_filasmaximos.TupleSelect(
hv_Index), (hv_columnasmaximos.TupleSelect(hv_Index)) - 5, hv_filasmaximos.TupleSelect(
hv_Index), (hv_columnasmaximos.TupleSelect(hv_Index)) + 5);

                else
                HOperatorSet.GenCircle(out ho_Circle, hv_filasmaximos.TupleSelect(hv_Index),
                    hv_columnasmaximos.TupleSelect(hv_Index), 5);




                HOperatorSet.MinMaxGray(ho_Circle, ho_ImagenOut, 0, out hv_Min, out hv_Max,
                    out hv_Range);
                ho_ImageReduced1.Dispose();
                HOperatorSet.ReduceDomain(ho_ImagenOut, ho_Circle, out ho_ImageReduced1);
                ho_Circle2.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced1, out ho_Circle2, 0, hv_Min);

                HOperatorSet.AreaCenter(ho_Circle2, out hv_Area1, out hv_Row2, out hv_Column2);
                hv_filasminimos = hv_filasminimos.TupleConcat(hv_Row2);
                hv_columnasminimos = hv_columnasminimos.TupleConcat(hv_Column2);
            }

            HOperatorSet.TupleRound(hv_filasminimos, out hv_filasminimos);
            HOperatorSet.TupleRound(hv_columnasminimos, out hv_columnasminimos);
            HOperatorSet.GetGrayval(ho_ImagenOut, hv_filasminimos, hv_columnasminimos, out hv_valoresminimos);
            HOperatorSet.TupleSub(hv_valoresmaximos, hv_valoresminimos, out hv_Diff);
            HOperatorSet.TupleMult(hv_Diff, 10, out hv_Diff);
            ho_ImagenOut.Dispose();
            ho_LocalMaxima.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_Circle.Dispose();
            ho_ImageReduced1.Dispose();
            ho_Circle2.Dispose();

            return;
        }
        private void Planitud_regla_1m_cuadricula(HObject ho_Imagen, out HTuple hv_filasmaximos, out HTuple hv_columnasmaximos, out HTuple hv_filasminimos, out HTuple hv_columnasminimos, out HTuple hv_Diff)
        {


            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];
            long SP_O = 0;

            // Local iconic variables 

            HObject ho_ImagenOut, ho_Region, ho_Partitioned;
            HObject ho_regionseleccionada = null, ho_ImageReduced = null;
            HObject ho_maximo = null, ho_minimo = null;


            // Local control variables 

            HTuple hv_Width, hv_Height, hv_ancho_cuadro;
            HTuple hv_Min1, hv_Max1, hv_Range1, hv_Diff1, hv_Indices1;
            HTuple hv_DiffSorted,  hv_j, hv_indice = new HTuple();
            HTuple hv_Min3 = new HTuple(), hv_Max3 = new HTuple(), hv_Range3 = new HTuple();
            HTuple hv_Area2 = new HTuple(), hv_Row = new HTuple(), hv_Column = new HTuple();
            HTuple hv_Area = new HTuple(), hv_Row1 = new HTuple(), hv_Column1 = new HTuple();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_ImagenOut);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_Partitioned);
            HOperatorSet.GenEmptyObj(out ho_regionseleccionada);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced);
            HOperatorSet.GenEmptyObj(out ho_maximo);
            HOperatorSet.GenEmptyObj(out ho_minimo);

            ho_ImagenOut.Dispose();
            HOperatorSet.CopyObj(ho_Imagen, out ho_ImagenOut, 1, -1);
            OTemp[SP_O] = ho_ImagenOut.CopyObj(1, -1);
            SP_O++;
            ho_ImagenOut.Dispose();
            HOperatorSet.GaussImage(OTemp[SP_O - 1], out ho_ImagenOut, 5);
            OTemp[SP_O - 1].Dispose();
            SP_O = 0;
            ho_Region.Dispose();
          
           // HOperatorSet.GetDomain(ho_Imagen, out ho_Region);


            HOperatorSet.GetImageSize(ho_ImagenOut, out hv_Width, out hv_Height);
            HOperatorSet.GenRectangle1(out ho_Region, borde_izquierdo, 0, borde_derecho, hv_Width);
            //ancho_cuadro := Width/10
            hv_ancho_cuadro = 10;
            HOperatorSet.TupleRound(hv_ancho_cuadro, out hv_ancho_cuadro);
            ho_Partitioned.Dispose();
            HOperatorSet.PartitionRectangle(ho_Region, out ho_Partitioned, hv_ancho_cuadro,
                5);
            HOperatorSet.MinMaxGray(ho_Partitioned, ho_ImagenOut, 0, out hv_Min1, out hv_Max1,
                out hv_Range1);
            HOperatorSet.TupleSub(hv_Max1, hv_Min1, out hv_Diff1);
            HOperatorSet.TupleSortIndex(hv_Diff1, out hv_Indices1);
            HOperatorSet.TupleSort(hv_Diff1, out hv_DiffSorted);

            if (hv_Indices1.TupleLength() > numeroMedidas)
            {

                HOperatorSet.TupleLastN(hv_Indices1, (new HTuple(hv_Indices1.TupleLength())) - numeroMedidas,
                    out hv_Indices1);
               // HOperatorSet.TupleLastN(hv_DiffSorted, (new HTuple(hv_DiffSorted.TupleLength())) - 10, out hv_Diff);
            }
            hv_filasmaximos = new HTuple();
            hv_columnasmaximos = new HTuple();
            hv_filasminimos = new HTuple();
            hv_columnasminimos = new HTuple();
            hv_Diff = new HTuple();
            for (hv_j = 0; (int)hv_j <= hv_Indices1.TupleLength()-1; hv_j = (int)hv_j + 1)
            {

                hv_indice = hv_Indices1.TupleSelect(hv_j);
                ho_regionseleccionada.Dispose();
                HOperatorSet.SelectObj(ho_Partitioned, out ho_regionseleccionada, hv_indice + 1);
                HOperatorSet.MinMaxGray(ho_regionseleccionada, ho_ImagenOut, 0, out hv_Min3,
                    out hv_Max3, out hv_Range3);
                ho_ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(ho_ImagenOut, ho_regionseleccionada, out ho_ImageReduced
                    );
                ho_maximo.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_maximo, hv_Max3, 10000);
                HOperatorSet.AreaCenter(ho_maximo, out hv_Area2, out hv_Row, out hv_Column);

                hv_filasmaximos = hv_filasmaximos.TupleConcat(hv_Row);
                hv_columnasmaximos = hv_columnasmaximos.TupleConcat(hv_Column);
                ho_minimo.Dispose();
                HOperatorSet.Threshold(ho_ImageReduced, out ho_minimo, 0, hv_Min3);
                HOperatorSet.AreaCenter(ho_minimo, out hv_Area, out hv_Row1, out hv_Column1);


                hv_filasminimos = hv_filasminimos.TupleConcat(hv_Row1);
                hv_columnasminimos = hv_columnasminimos.TupleConcat(hv_Column1);

                hv_Diff = hv_Diff.TupleConcat(hv_Range3 * 10);
            }





        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    