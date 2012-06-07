﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HalconDotNet;
using System.Collections;

namespace Meplate
{
    class CProcesamiento
    {

        public int numeroModulos, numeroMedidas, _Defectos1m=0,_Defectos2m=0;
        public HImage X, Y, Z;
        public int filas, columnas;
        double distancia_entre_sensores ;
        public double distancia_a_la_chapa ;
        double sigma_bordes;
        double sigma_cabeza;
        double umbral_bordes;
        double umbral_cabeza;
        public double[] _ValoresMedios;
        public double[] _Referencias;
        public double BI, BD;
        public int borde_derecho, borde_izquierdo;
        public double[,] Pixeles;
        public double[,] Puntos;
        public string _Decision = "Y";
        public double _Puntuacion = 10;
        public bool _Incluir_Parcialmente_Cubiertos;
        public bool _Guardar_Imagenes_Parciales;
        public bool _EnviarFTP;



        public CProcesamiento(dynamic parametros)
        {
            numeroModulos = int.Parse(parametros.Meplaca.MEPNumeroModulos);
            distancia_entre_sensores = double.Parse(parametros.Meplaca.MEPDistancia_entre_sensores);
            distancia_a_la_chapa = double.Parse(parametros.Meplaca.MEPDistancia_nominal_trabajo);
            numeroMedidas = int.Parse(parametros.Procesamiento.numeroMedidas);
            sigma_bordes = double.Parse(parametros.Procesamiento.sigma_bordes);
            sigma_cabeza = double.Parse(parametros.Procesamiento.sigma_cabeza);
            umbral_bordes = double.Parse(parametros.Procesamiento.umbral_bordes);
            umbral_cabeza = double.Parse(parametros.Procesamiento.umbral_cabeza);
            filas = numeroModulos * 6;
            _ValoresMedios = new double[filas];
            _Referencias = new double[filas];
            Pixeles = new double[ numeroMedidas*2,5];
            Puntos = new double[ numeroMedidas*2,7];
            _Incluir_Parcialmente_Cubiertos = bool.Parse(parametros.Procesamiento.PROincluirparcialmentecubiertos);
            _Guardar_Imagenes_Parciales = bool.Parse(parametros.Procesamiento.PROGuardarImagenesParciales);
            _EnviarFTP = bool.Parse(parametros.Procesamiento.PROEnviarFtp);

            //Pixeles2 = new double[numeroMedidas, 5];
            //Puntos2 = new double[numeroMedidas, 7];

        }
        public double ProcesamientoDatos(List<CMedida> measurement, double ancho,double tol1,double tol2)
        {
            DateTime t1 = DateTime.Now;

            Inicializar(measurement);
            ObtenerImagenes(measurement);
            //proc.ObtenerBordes();
            ObtenerBordes(ancho);
            CorregirImagen();
            CalcularDefectos_1metro(tol1);
            CalcularDefectos_2metro(tol2);


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

            _ValoresMedios.Initialize();
            _Referencias.Initialize();
            columnas = medidas.Count;
            X = new HImage("real", columnas, filas);
            Y = new HImage("real", columnas, filas);
            Z = new HImage("real", columnas, filas);

            BI = 0;
            BD = 0;
            _Defectos1m = 0;
            _Defectos2m=0;
            _Decision = "Y";
            _Puntuacion = 10;

        }
        private void ObtenerImagenes(List<CMedida> medidas)
        {
            HTuple filas_borde, columnas_borde, amplitude, distance, indices;
            int cabeza=0;
            // la X la creo cuando conozca los bordes
            if (medidas.Count > 0)
            {
                
                //creo la imagen Z
                for (int i = 0; i < columnas; i++)
                {
                    for (int j = 0; j < filas; j++)
                    {
                       if ( medidas[i].perfil[j]>0)      Z.SetGrayval(j, i, medidas[i].perfil[j] * 10);  // lo paso a mm
                        else Z.SetGrayval(j, i, 0); 
                     
                    }
                }

                if (_Guardar_Imagenes_Parciales)
                {
                    Z.WriteImage("tiff", 0, "ZRAW" + (string)DateTime.Now.Minute.ToString() + (string)DateTime.Now.Second.ToString());
                     }

               // corto La cabeza donde no hay chapa
                HMeasure bordes = new HMeasure((double)6, (double)columnas / 2 - 1, (double)0, (int)Math.Round((double)(columnas / 2.0)-2), 5, columnas, filas, "nearest_neighbor");
                //HMeasure bordes = new HMeasure(20, 20, -(double)Math.PI / 2.0, 5,5, columnas , filas , "nearest_neighbor");
                bordes.MeasurePos(Z, sigma_cabeza, umbral_bordes, "all", "all", out filas_borde, out columnas_borde, out amplitude, out distance);
                amplitude = amplitude.TupleAbs();
                indices = amplitude.TupleSortIndex();
                if (indices.Length > 1)
                {
                    double temp = columnas_borde.DArr[indices[indices.Length-1]];
                    cabeza = (int)Math.Ceiling(temp);
                   
                }
                else
                {
                    cabeza = 0;
                }
                if (cabeza + 3 < columnas - 1) Z.CropRectangle1 (0, cabeza, filas - 1, columnas - 1);
                else Z.CropRectangle1(0, cabeza, filas - 1, columnas - 1);
            
                //creo la imagen Y, aunque solo relleno la parte con chpaa, queda espacio sin rellenar
                Z.GetImageSize(out columnas, out filas);
               
                X = new HImage("real", columnas, filas);
                Y = new HImage("real", columnas, filas);

                double distancia_inicial = medidas[cabeza].distancia;
                for (int i = cabeza; i < columnas ; i++)
                {
                    for (int j = 0; j < filas; j++)
                    {
                        Y.SetGrayval(j, i-cabeza, medidas[i].distancia - distancia_inicial);
                       

                    }
                }


                if (_Guardar_Imagenes_Parciales)
                {
                    Z.WriteImage("tiff", 0, "Z_Cortada" + (string)DateTime.Now.Minute.ToString() + (string)DateTime.Now.Second.ToString());
                    Y.WriteImage("tiff", 0, "YRAW" + (string)DateTime.Now.Minute.ToString() + (string)DateTime.Now.Second.ToString());
                }
           

            }






        }
        private void CorregirImagen()
        {

            HImage media = Z.MeanImage(columnas - 1, 1);
            double valor, med;
            
            for (int i = 0; i < columnas; i++)
            {
                for (int j = 0; j < filas; j++)
                {
                   
                   
                    if (j >= borde_izquierdo && j <= borde_derecho)
                    {
                       
                        med = media.GetGrayval(j, 0);
                        _ValoresMedios[j] = med/10; // para pasarlo a cm que es como trabaja el meplaca
                        _Referencias[j] = distancia_a_la_chapa/10;  //para pasarlo a cm que es como trabaja el meplaca
     
                        valor = Z.GetGrayval(j, i);
                        if (distancia_a_la_chapa + valor - med>0)     Z.SetGrayval(j, i, distancia_a_la_chapa + valor - med);
                        else Z.SetGrayval(j, i, 0); 

                    }
                }
            }

            media.Dispose();

            if (_Guardar_Imagenes_Parciales || _EnviarFTP) Z.WriteImage("tiff", 0, "ZCORREGIDA" + (string)DateTime.Now.Minute.ToString() + (string)DateTime.Now.Second.ToString());

           

        }
        public void ObtenerBordes(double ancho)
        {
            if (ancho > 0)
            {
                ObtenerBordesConAncho3(ancho);
                if (borde_derecho == filas - 1 && borde_izquierdo == 0)
                {
                    ObtenerBordesSinAncho();
                }
            }
            else ObtenerBordesSinAncho();

           
            //si quiero incluir los parcialmente cubiertos
            if (_Incluir_Parcialmente_Cubiertos)
            {
                if (borde_izquierdo > 0) borde_izquierdo = borde_izquierdo - 1;
                if (borde_izquierdo < filas - 1) borde_derecho = borde_derecho + 1;
            }



            // pongo a 0 todos los valores fuera de la chapa y creo la imgen de X

            for (int i = 0; i < columnas; i++)
            {
                for (int j = 0; j < filas; j++)
                {

                    if (j < borde_izquierdo || j > borde_derecho)
                    {
                        Z.SetGrayval(j, i, 0);
                        X.SetGrayval(j, i, 0);                       
                    }
                    //considero como punto la distancia media entre 2 sensores , por lo que empizo en 100 (distancia_entre_sensores / 2). 
                    //Empiezo a conta en el borde por lo que le resto el borde izq a la j 
                    else X.SetGrayval(j, i, (((j - borde_izquierdo) + 1) * distancia_entre_sensores) - (distancia_entre_sensores / 2));
                       
                }
            }


            // HRegion Chapa = new HRegion((double)borde_izquierdo+1 , (double)0, (double)borde_derecho-1 , (double)columnas - 1);
            //HRegion imagen = new HRegion((double)0, (double)0, (double)filas - 1, (double)columnas - 1);
            //Z.ReduceDomain(Chapa);

            if (_Guardar_Imagenes_Parciales)
            {
                Z.WriteImage("tiff", 0, "Z_sinbordes" + (string)DateTime.Now.Minute.ToString() + (string)DateTime.Now.Second.ToString());
                X.WriteImage("tiff", 0, "XRAW.jpg" + (string)DateTime.Now.Minute.ToString() + (string)DateTime.Now.Second.ToString());
            }
            //


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
            HTuple filas_borde, columnas_borde, amplitude, distance,width,lenght;

            double max = 0;
            double distancia = 0;

            Z.GetImageSize(out width, out lenght);
            // int modulos = (int)Math.Round(ancho / distancia_entre_sensores);
            int modulos = (int)Math.Round(ancho / 52);
            int anchura = (int)Math.Round((double)(columnas / 2.0)-1);
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

        //igual que el anterior pero pone el borde izq en 0
        public void ObtenerBordesConAncho2(double ancho)
        {
            HTuple filas_borde, columnas_borde, amplitude, distance, width, lenght;

            double max = 0;
            double distancia = 0;


             int modulos = (int)Math.Round(ancho / distancia_entre_sensores);
           // int modulos = (int)Math.Round(ancho / 52);
            int anchura = (int)Math.Round((double)(columnas / 2.0) - 1);
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
                       
                            distancia = Math.Abs(filas_borde[i].D );
                            if (Math.Abs(distancia - modulos) < 4.0)
                            {
                                if (Math.Abs(amplitude[i].D ) > max)
                                {
                                    max = Math.Abs(amplitude[i].D - 1);
                                    BD = filas_borde[i];
                                    BI = 0;


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
                        borde_derecho = (int)Math.Floor(BD) - 2;
                        borde_izquierdo = 0;
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
        //pone el borde izq en 0 y el derecho en donde coincida el ancho 
        public void ObtenerBordesConAncho3(double ancho)
        {
           

            int modulos = (int)Math.Round(ancho / distancia_entre_sensores);
           
                        borde_derecho = modulos -1;
                        borde_izquierdo = 0;



        }
        private void CalcularDefectos_1metro(double tol)
        {

            HTuple filas_max, columnas_max, filas_min, columnas_min, diff;


            // "fibras" o "circulos" 
            //Planitud_regla_1m(Z,"fibras", out filas_max, out columnas_max, out filas_min, out columnas_min, out diff);
            //Planitud_max_min(1, Z, out filas_max, out columnas_max, out filas_min, out columnas_min, out diff);
            Planitud_regla(1, Z, out filas_max, out columnas_max, out filas_min, out columnas_min, out diff);

            for (int i = 0; i <filas_max.Length; i++)
            {
                Pixeles[i,0] = filas_max.DArr[i];
                Pixeles[i,1] = columnas_max.DArr[i];
                Pixeles[i,2] = filas_min.DArr[i];
                Pixeles[i,3] = columnas_min.DArr[i];
               // Pixeles[i,4] = diff.DArr[i];


                Puntos[i, 0] = X.GetGrayval((int)Pixeles[i, 0], (int) Pixeles[i,1]);
                Puntos[i, 1] = Y.GetGrayval((int)Pixeles[i, 0], (int)Pixeles[i, 1]);
                Puntos[i, 2] = Z.GetGrayval((int)Pixeles[i, 0], (int)Pixeles[i, 1]);
                Puntos[i, 3] = X.GetGrayval((int)Pixeles[i, 2], (int)Pixeles[i, 3]);
                Puntos[i, 4] = Y.GetGrayval((int)Pixeles[i, 2], (int)Pixeles[i, 3]);
                Puntos[i, 5] = Z.GetGrayval((int)Pixeles[i, 2], (int)Pixeles[i, 3]);
                Pixeles[i, 4] = Puntos[i, 2] - Puntos[i, 5]; // la diferencia entre el maximo y el minimo
                Puntos[i,6] = Pixeles[i,4];
                if (Math.Abs(Puntos[i, 6]) > tol) _Defectos1m++;
                _Puntuacion = _Puntuacion - CalcularPenalizacion(Puntos[i, 6], tol);
            }
            if (_Defectos1m > 0) _Decision = "N";
            if (_Puntuacion < 0) _Puntuacion = 0;

        }
        private void CalcularDefectos_2metro(double tol)
        {

            HTuple filas_max, columnas_max, filas_min, columnas_min, diff;


            // "fibras" o "circulos" 
            //Planitud_regla_1m(Z,"fibras", out filas_max, out columnas_max, out filas_min, out columnas_min, out diff);
           // Planitud_max_min(2, Z, out filas_max, out columnas_max, out filas_min, out columnas_min, out diff);
            Planitud_regla(2, Z, out filas_max, out columnas_max, out filas_min, out columnas_min, out diff);

            for (int i = numeroMedidas; i < numeroMedidas+filas_max.Length; i++)
            {
                Pixeles[i, 0] = filas_max.DArr[i-numeroMedidas];
                Pixeles[i, 1] = columnas_max.DArr[i-numeroMedidas];
                Pixeles[i, 2] = filas_min.DArr[i-numeroMedidas];
                Pixeles[i, 3] = columnas_min.DArr[i-numeroMedidas];
                Pixeles[i, 4] = diff.DArr[i-numeroMedidas];


                Puntos[i, 0] = X.GetGrayval((int)Pixeles[i, 0], (int)Pixeles[i, 1]);
                Puntos[i, 1] = Y.GetGrayval((int)Pixeles[i, 0], (int)Pixeles[i, 1]);
                Puntos[i, 2] = Z.GetGrayval((int)Pixeles[i, 0], (int)Pixeles[i, 1]);
                Puntos[i, 3] = X.GetGrayval((int)Pixeles[i, 2], (int)Pixeles[i, 3]);
                Puntos[i, 4] = Y.GetGrayval((int)Pixeles[i, 2], (int)Pixeles[i, 3]);
                Puntos[i, 5] = Z.GetGrayval((int)Pixeles[i, 2], (int)Pixeles[i, 3]);
                Pixeles[i, 4] = Puntos[i, 2] - Puntos[i, 5];
                Puntos[i, 6] = Pixeles[i, 4];
                if (Math.Abs(Puntos[i, 6]) > tol) _Defectos2m++;
                _Puntuacion = _Puntuacion - CalcularPenalizacion(Puntos[i, 6], tol);
            }

            if (_Defectos2m > 0) _Decision = "N";
            if (_Puntuacion < 0) _Puntuacion = 0;

        }
        private void Planitud_max_min(HTuple regla, HObject Imagen, out HTuple hv_filasmaximos, out HTuple hv_columnasmaximos, out HTuple hv_filasminimos, out HTuple hv_columnasminimos, out HTuple hv_Diff)
        {


            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];
            long SP_O = 0;

            // Local iconic variables 

            HObject ImagenOut, Region, Partitioned, ellipse;
            HObject regionseleccionada = null, ImageReduced = null;
            HObject maximo = null, minimo = null;


            // Local control variables 

            HTuple hv_Width, hv_Height, hv_anccuadro, hv_radio1, hv_radio2;
            HTuple hv_Min1, hv_Max1, hv_Range1, hv_Diff1, hv_Indices1;
            HTuple hv_DiffSorted, hv_j, hv_indice = new HTuple();
            HTuple hv_Min3 = new HTuple(), hv_Max3 = new HTuple(), hv_Range3 = new HTuple();
            HTuple hv_Min4 = new HTuple(), hv_Max4 = new HTuple(), hv_Range4 = new HTuple();
            HTuple hv_Area2 = new HTuple(), hv_Row = new HTuple(), hv_Column = new HTuple();
            HTuple hv_Area = new HTuple(), hv_Row1 = new HTuple(), hv_Column1 = new HTuple();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ImagenOut);
            HOperatorSet.GenEmptyObj(out Region);
            HOperatorSet.GenEmptyObj(out Partitioned);
            HOperatorSet.GenEmptyObj(out regionseleccionada);
            HOperatorSet.GenEmptyObj(out ImageReduced);
            HOperatorSet.GenEmptyObj(out maximo);
            HOperatorSet.GenEmptyObj(out minimo);
            HOperatorSet.GenEmptyObj(out ellipse);

            ImagenOut.Dispose();
            HOperatorSet.CopyObj(Imagen, out ImagenOut, 1, -1);
            OTemp[SP_O] = ImagenOut.CopyObj(1, -1);
            SP_O++;
            ImagenOut.Dispose();
            HOperatorSet.GaussImage(OTemp[SP_O - 1], out ImagenOut, 5);
            OTemp[SP_O - 1].Dispose();
            SP_O = 0;
            Region.Dispose();

            // HOperatorSet.GetDomain(Imagen, out Region);


            HOperatorSet.GetImageSize(ImagenOut, out hv_Width, out hv_Height);
            HOperatorSet.GenRectangle1(out Region, borde_izquierdo, 0, borde_derecho, hv_Width);
            // hv_anccuadro = hv_Width / 10;
            hv_anccuadro = 50;
            hv_anccuadro.TupleFloor();
            //hv_anccuadro = 10;
            HOperatorSet.TupleRound(hv_anccuadro, out hv_anccuadro);
            Partitioned.Dispose();
            HOperatorSet.PartitionRectangle(Region, out Partitioned, hv_anccuadro,
                5);
            HOperatorSet.MinMaxGray(Partitioned, ImagenOut, 0, out hv_Min1, out hv_Max1,
                out hv_Range1);

            HOperatorSet.TupleSortIndex(hv_Max1, out hv_Indices1);
            HOperatorSet.TupleSort(hv_Max1, out hv_DiffSorted);

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
            for (hv_j = 0; (int)hv_j <= hv_Indices1.TupleLength() - 1; hv_j = (int)hv_j + 1)
            {
                //Calculo el maximo de la region
                hv_indice = hv_Indices1.TupleSelect(hv_j);
                regionseleccionada.Dispose();
                HOperatorSet.SelectObj(Partitioned, out regionseleccionada, hv_indice + 1);
                HOperatorSet.MinMaxGray(regionseleccionada, ImagenOut, 0, out hv_Min3,
                    out hv_Max3, out hv_Range3);
                ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(ImagenOut, regionseleccionada, out ImageReduced
                    );
                maximo.Dispose();
                HOperatorSet.Threshold(ImageReduced, out maximo, hv_Max3, 10000);
                HOperatorSet.AreaCenter(maximo, out hv_Area2, out hv_Row, out hv_Column);



                hv_filasmaximos = hv_filasmaximos.TupleConcat(hv_Row);
                hv_columnasmaximos = hv_columnasmaximos.TupleConcat(hv_Column);
                minimo.Dispose();


                //Calculo un circulo de 1m de radio

                hv_radio1 = CalcularRadio(hv_Column, regla * 1000);
                hv_radio2 = ((regla * 1000) / distancia_entre_sensores);
                hv_radio2.TupleFloor();
                HOperatorSet.GenEllipse(out ellipse, hv_Row, hv_Column, 0, hv_radio1, hv_radio2);
                HOperatorSet.Intersection(Region, ellipse, out ellipse);
                HOperatorSet.MinMaxGray(ellipse, ImagenOut, 0, out hv_Min4,
                   out hv_Max4, out hv_Range4);
                HOperatorSet.ReduceDomain(ImagenOut, ellipse, out ImageReduced);
                HOperatorSet.Threshold(ImageReduced, out minimo, 0, hv_Min4);
                HOperatorSet.AreaCenter(minimo, out hv_Area, out hv_Row1, out hv_Column1);


                hv_filasminimos = hv_filasminimos.TupleConcat(hv_Row1);
                hv_columnasminimos = hv_columnasminimos.TupleConcat(hv_Column1);

                hv_Diff = hv_Diff.TupleConcat(hv_Range3);
            }


        }
        private void Planitud_regla(HTuple regla, HObject Imagen, out HTuple hv_filasmaximos, out HTuple hv_columnasmaximos, out HTuple hv_filasminimos, out HTuple hv_columnasminimos, out HTuple hv_Diff)
        {

 
             #region Inicializar
            // Stack for temporary objects 
            HObject[] OTemp = new HObject[20];
            long SP_O = 0;

            // Local iconic variables 

            HObject ImagenOut, Region, Partitioned, ellipse, localmaxima, connectedRegions, contour, region3, Imagetemp, regiontemp;
            HObject regionseleccionada = null, ImageReduced = null;
            HObject maximo = null, minimo = null;


            // Local control variables 

            HTuple hv_Width, hv_Height, hv_anccuadro, hv_radio1, hv_radio2;
            HTuple hv_Min1, hv_Max1, hv_Range1, hv_Diff1, hv_Indices1, hv_Max, hmax,rowmin,colmin,h;
            HTuple hv_MaxSorted, hv_j, hv_indice = new HTuple();
            HTuple hv_Min3 = new HTuple(), hv_Max3 = new HTuple(), hv_Range3 = new HTuple();
            HTuple hv_Min4 = new HTuple(), hv_Max4 = new HTuple(), hv_Range4 = new HTuple();
            HTuple hv_Min5 = new HTuple(), hv_Max5 = new HTuple(), hv_Range5 = new HTuple();
            HTuple hv_Area2 = new HTuple(), hv_Row = new HTuple(), hv_Column = new HTuple();
            HTuple hv_Area3 = new HTuple(), hv_Row3 = new HTuple(), hv_Column3 = new HTuple();
            HTuple hv_Area = new HTuple(), hv_Row1 = new HTuple(), hv_Column1 = new HTuple();
            HTuple hv_Areatemp = new HTuple(), hv_Rowtemp = new HTuple(), hv_Columntemp = new HTuple();

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ImagenOut);
            HOperatorSet.GenEmptyObj(out Region);
            HOperatorSet.GenEmptyObj(out Partitioned);
            HOperatorSet.GenEmptyObj(out regionseleccionada);
            HOperatorSet.GenEmptyObj(out ImageReduced);
            HOperatorSet.GenEmptyObj(out localmaxima);
            HOperatorSet.GenEmptyObj(out connectedRegions);
            HOperatorSet.GenEmptyObj(out maximo);
            HOperatorSet.GenEmptyObj(out minimo);
            HOperatorSet.GenEmptyObj(out ellipse); 
            HOperatorSet.GenEmptyObj(out contour);
            HOperatorSet.GenEmptyObj(out region3);
            HOperatorSet.GenEmptyObj(out Imagetemp);
            HOperatorSet.GenEmptyObj(out regiontemp); 

            #endregion

            ///<>  Filtro la imagen . Divido la imagen en rectangulos y busco las maximas alturas           
             #region BuscaMaximos
            ImagenOut.Dispose();
            HOperatorSet.CopyObj(Imagen, out ImagenOut, 1, -1);
            OTemp[SP_O] = ImagenOut.CopyObj(1, -1);
            SP_O++;
            ImagenOut.Dispose();
            HOperatorSet.GaussImage(OTemp[SP_O - 1], out ImagenOut, 5);
            OTemp[SP_O - 1].Dispose();
            SP_O = 0;
            Region.Dispose();

            // HOperatorSet.GetDomain(Imagen, out Region);

            
         
            HOperatorSet.GetImageSize(ImagenOut, out hv_Width, out hv_Height);
            HOperatorSet.GenRectangle1(out Region, borde_izquierdo, 0, borde_derecho, hv_Width);
            if (hv_Width > 20) hv_anccuadro = hv_Width / 10;
            else hv_anccuadro = 1;
           // hv_anccuadro = 50;
            hv_anccuadro.TupleFloor();
            //hv_anccuadro = 10;
            HOperatorSet.TupleRound(hv_anccuadro, out hv_anccuadro);
            Partitioned.Dispose();
            HOperatorSet.PartitionRectangle(Region, out Partitioned, hv_anccuadro,
                5);
            HOperatorSet.MinMaxGray(Partitioned, ImagenOut, 0, out hv_Min1, out hv_Max1,
                out hv_Range1);

            HOperatorSet.TupleSortIndex(hv_Max1, out hv_Indices1);
            HOperatorSet.TupleSort(hv_Max1, out hv_MaxSorted);

            if (hv_Indices1.TupleLength() > numeroMedidas)
            {

                HOperatorSet.TupleLastN(hv_Indices1, (new HTuple(hv_Indices1.TupleLength())) - numeroMedidas, out hv_Indices1);


            } 
            #endregion


            /// <Comment>analizo las 10 regiones con los puntos mas altos. Busco los maximos y calculo un circulo de 1 o 2 metros alrededor.
            /// En ese circulo busco los maximos relativos y probandolos todos veo en cual deberia apoyar la regla
            /// para que me de el maximo defectos. Los resultados los guardo en las variables de salida
            #region Buscar defectos

            hv_filasmaximos = new HTuple();
            hv_columnasmaximos = new HTuple();
            hv_filasminimos = new HTuple();
            hv_columnasminimos = new HTuple();
            hv_Diff = new HTuple();

            for (hv_j = 0; (int)hv_j <= hv_Indices1.TupleLength() - 1; hv_j = (int)hv_j + 1)
            {   

                ///<code>Calculo el maximo de la region
                hv_indice = hv_Indices1.TupleSelect(hv_j);
                regionseleccionada.Dispose();
                HOperatorSet.SelectObj(Partitioned, out regionseleccionada, hv_indice + 1);
                HOperatorSet.MinMaxGray(regionseleccionada, ImagenOut, 0, out hv_Min3, out hv_Max3, out hv_Range3);
                ImageReduced.Dispose();
                HOperatorSet.ReduceDomain(ImagenOut, regionseleccionada, out ImageReduced);
                maximo.Dispose();
                HOperatorSet.Threshold(ImageReduced, out maximo, hv_Max3, 10000);
                HOperatorSet.AreaCenter(maximo, out hv_Area2, out hv_Row, out hv_Column);
                hv_filasmaximos = hv_filasmaximos.TupleConcat(hv_Row);
                hv_columnasmaximos = hv_columnasmaximos.TupleConcat(hv_Column);
                minimo.Dispose();


                ///<code>Calculo un circulo (elipse) de 1m o 2m de radio

                hv_radio1 = CalcularRadio(hv_Column, regla * 1000);
                hv_radio2 = ((regla * 1000) / distancia_entre_sensores);
                hv_radio2.TupleFloor();
                HOperatorSet.GenEllipse(out ellipse, hv_Row, hv_Column, 0, hv_radio1, hv_radio2);
                HOperatorSet.Intersection(Region, ellipse, out ellipse);

                ///<Busco los maximos locales
                HOperatorSet.ReduceDomain(ImagenOut, ellipse, out ImageReduced);
                HOperatorSet.LocalMax(ImageReduced, out localmaxima);
                HOperatorSet.Connection(localmaxima, out connectedRegions);
                HOperatorSet.AreaCenter(connectedRegions, out hv_Area, out hv_Row1, out hv_Column1);

                ///<code>  Si hay alguun maximos local analizo todos los maximos locales y el defecto que me darian al apoyar la regla
                /// Si no cojo el puntyo minimo y apoyo en el la regla
                if (hv_Row1.Length>0)
                {
                                    

                    hmax = 0;
                    rowmin = 0;
                    colmin = 0;



                    for (int i = 0; i < hv_Row1.Length - 1; i++)
                    {
      
                     //   HOperatorSet.GenContourPolygonXld(out contour, new HTuple((double)hv_Row, (double)hv_Row1[i]), new HTuple((double)hv_Column, (double)hv_Column1[i]));
                        HOperatorSet.GenContourPolygonXld(out contour, hv_Row.TupleConcat(hv_Row1[i]), hv_Column.TupleConcat(hv_Column1[i]));
                        HOperatorSet.GenRegionContourXld(contour, out region3, "filled");
                        HOperatorSet.AreaCenter(connectedRegions, out hv_Area3, out hv_Row3, out hv_Column3);
                        if (hv_Area3.Length > 20)
                        {
                            HOperatorSet.Difference(region3, maximo, out region3);
                            HOperatorSet.MinMaxGray(region3, ImagenOut, 0, out hv_Min5, out hv_Max5, out hv_Range5);
                            if (hv_Min5>0)
                            {
                                HOperatorSet.ReduceDomain(ImagenOut, region3, out Imagetemp);
                                HOperatorSet.Threshold(Imagetemp, out regiontemp, 0, hv_Min5);
                                HOperatorSet.AreaCenter(regiontemp, out hv_Areatemp, out hv_Rowtemp, out hv_Columntemp);
                                if (hv_Areatemp > 1)
                                {
                                    hv_Rowtemp = hv_Rowtemp[0];
                                    hv_Columntemp = hv_Columntemp[0];

                                }
                                h = CalcularAltura(
                                    X.GetGrayval((int)hv_Row.D, (int)hv_Column.D), Y.GetGrayval((int)hv_Row.D, (int)hv_Column.D), Z.GetGrayval((int)hv_Row.D, (int)hv_Column.D),
                                    X.GetGrayval((int)hv_Row1[i].D, (int)hv_Column1[i].D), Y.GetGrayval((int)hv_Row1[i].D, (int)hv_Column1[i].D), Z.GetGrayval((int)hv_Row1[i].D, (int)hv_Column1[i].D),
                                    X.GetGrayval((int)hv_Rowtemp.D, (int)hv_Columntemp.D), Y.GetGrayval((int)hv_Rowtemp.D, (int)hv_Columntemp.D), Z.GetGrayval((int)hv_Rowtemp.D, (int)hv_Columntemp.D));
                                if (h > hmax)
                                {
                                    hmax = h;
                                    rowmin = hv_Rowtemp;
                                    colmin = hv_Columntemp;

                                } 
                            }

                        }
                      


                    }
                    if (rowmin.D==0)
                    {
                        HOperatorSet.MinMaxGray(ellipse, ImagenOut, 0, out hv_Min4, out hv_Max4, out hv_Range4);
                        HOperatorSet.ReduceDomain(ImagenOut, ellipse, out ImageReduced);
                        HOperatorSet.Threshold(ImageReduced, out minimo, 0, hv_Min4);
                        HOperatorSet.AreaCenter(minimo, out hv_Area, out hv_Row1, out hv_Column1);


                        hv_filasminimos = hv_filasminimos.TupleConcat(hv_Row1);
                        hv_columnasminimos = hv_columnasminimos.TupleConcat(hv_Column1);
                        hv_Diff = hv_Diff.TupleConcat(hv_Range3);

                    
                    }
                    else
                    {
                        hv_filasminimos = hv_filasminimos.TupleConcat(rowmin);
                        hv_columnasminimos = hv_columnasminimos.TupleConcat(colmin);
                        hv_Diff = hv_Diff.TupleConcat(hmax);
                    }
                  
                    
                }

                else
                {


                    HOperatorSet.MinMaxGray(ellipse, ImagenOut, 0, out hv_Min4, out hv_Max4, out hv_Range4);
                    HOperatorSet.ReduceDomain(ImagenOut, ellipse, out ImageReduced);
                    HOperatorSet.Threshold(ImageReduced, out minimo, 0, hv_Min4);
                    HOperatorSet.AreaCenter(minimo, out hv_Area, out hv_Row1, out hv_Column1);


                    hv_filasminimos = hv_filasminimos.TupleConcat(hv_Row1);
                    hv_columnasminimos = hv_columnasminimos.TupleConcat(hv_Column1);
                    hv_Diff = hv_Diff.TupleConcat(hv_Range3);

                }

             
            }


            localmaxima.Dispose();
            connectedRegions.Dispose();
            ellipse.Dispose();
            contour.Dispose();
            region3.Dispose();
            Imagetemp.Dispose();
            regiontemp.Dispose();
           
            
            
            #endregion

           
        }       
        private HTuple CalcularRadio(HTuple column, int distancia)
        {
            HTuple hv_Width, hv_Height;
            double temp;
            HOperatorSet.GetImageSize(Y, out hv_Width, out hv_Height);
            int col = (int)column.D;
           double  valor_inicial = Y.GetGrayval(0,col);
           double valor_ultimo = Y.GetGrayval(0, (int)(hv_Width.D-1));
           if (valor_inicial + distancia < valor_ultimo)
           {
               while (col < hv_Width.D)
               {
                   temp = Y.GetGrayval(0, col);
                   if (temp > valor_inicial + distancia) break;
                   col++;


               }
           }
           else
           {
               col = 0;
               while (col < hv_Width.D)
               {
                   temp = Y.GetGrayval(0, col);
                   if (temp > (valor_inicial - distancia)) break;
                   col++;


               }
           
           }
            HTuple dif= col-column;
            return  dif.TupleAbs();

        
        
        
        }
        private HTuple CalcularAltura(double x1, double y1, double z1, double x2, double y2, double z2, double x3, double y3, double z3)
        {
            if (z3==0)
            {
                return 0;
            }
            HTuple h,a,b,c,S,AA;


            //recalculo x3 para asegurarme que esta en la recta
            
            x3 = ((y3 - y1) * (x2 - x1) / (y2 - y1)) + x1;
            a=Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2) + (z1 - z2) * (z1 - z2));
            b = Math.Sqrt((x1 - x3) * (x1 - x3) + (y1 - y3) * (y1 - y3) + (z1 - z3) * (z1 - z3));
            c = Math.Sqrt((x2 - x3) * (x2 - x3) + (y2 - y3) * (y2 - y3) + (z2 - z3) * (z2 - z3));
            S = 0.5 * (a + b + c);
            AA = Math.Sqrt(S * (S - a) * (S - b) * (S - c));
            
            h = 2 * AA / a;
            return h;
        }
        private double CalcularPenalizacion(double valor, double tolerancia)
        {
            double penalizacion = 0;

            penalizacion = (5 * Math.Abs(valor) - tolerancia) / (0.8 * tolerancia);
            if (penalizacion > 5) penalizacion = 5;
            if (penalizacion < 0) penalizacion = 0;


            return penalizacion;
        
        
        }
    }
}
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             