using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpinPlatform.Data;
using SpinPlatform.IO;

namespace SpinPlatform.Sensors.Meplaca
{
   public class MeplacaData : ModuleData
    {
        //parametros de entrada

        public bool GetMedidas = false;
        public bool GetTensiones = false;
        public bool GetUltimaMedida = false;
        public bool GetUltimaTension = false;
        public bool EnviarOffsetsArchivo = false;
        public bool EnviarOffsets = false;



        //parametros de salida


        public List<double[]> Perfiles = null;
        public List<int[]> Tensiones = null;
        public int[] UltimaTension = null;
        public double[] UltimoPerfil = null;
        public double[] Offsets = null;

        public MeplacaData(bool getMedidas = false, bool getTensiones = false, bool getUltimaMedida = false, bool getUltimaTension = false, bool enviarOffsetsArchivo = false, bool enviarOffsets = false)
        {
            GetMedidas = getMedidas;
            GetTensiones = getTensiones;
            GetUltimaMedida = getUltimaMedida;
            GetUltimaTension = getUltimaTension;
            EnviarOffsetsArchivo = enviarOffsetsArchivo;
            EnviarOffsets = enviarOffsets;


            Perfiles = null;
            Tensiones = null;
            UltimaTension = null;
            UltimoPerfil = null;
            Offsets = null;
        }
        public void ResetData()
        {

            Perfiles = null;
            Tensiones = null;
            UltimaTension = null;
            UltimoPerfil = null;
            Offsets = null;
        }

    }
}
