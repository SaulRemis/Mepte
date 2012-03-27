using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpinPlatform.Config;
using SpinPlatform.Errors;
using System.Data;
using System.Data.OleDb;
using System.Xml.Serialization;
using System.Dynamic;
//using MySql.Data.MySqlClient;

namespace SpinPlatform.DataBase
{
    internal class SpinDataBase : ISpinPlatformInterface
    {
        /// <summary>
        /// Class to connect and send queries to the database (configurable by the AppConfig.xml)
        /// </summary>

        #region Variables

        private dynamic _data = new ExpandoObject();
        //private String _query = null;
        //private String _provider = null;
        //private String _defaultTableName = null;
        //private String _dbType = null;
        //private String _connectionString = null;

        private System.Data.OleDb.OleDbCommand cmd = null;
        private System.Data.OleDb.OleDbConnection _cnn = null;

        #endregion

        #region Implementación de interface

        public void Init(dynamic initData)
        {
            /// <summary>
            /// Se encarga de guardar en las variables internas de la clase los parámentros necesarios para laconexión con la base de datos
            /// En concreto el objetoinitData de entrada debe disponer de los siguientes campos:
            /// (OBLIGATORIO)DBProvider: nombre del componente software encargado de interactuar entre el OLEDB y la base de datos
            /// (OBLIGATORIO)DBDefaultTableName: nombre de la tabla sobre la que se realizarán consultas
            /// (OPCIONAL) DBDbType: tipo de base de datos a la que se accede (MYSql, SQL Server, ORACLE, DB2, SQLite o Access)
            /// (OBLIGATORIO)DBConnectionString: parámetros de conexión. nombre de la base de datos, localización, usuario y contraseña...
            /// </summary>
          
            //guardo parametros
            _data = initData.DataBase;

            if ((initData.DataBase as IDictionary<string, object>).ContainsKey("DBType"))
                _data.DBType = initData.DataBase.DBType;
            else
                _data.DBType = "Unknow";
            
        }

        public void Start()
        {
            /// <summary>
            /// Se encarga de abrir la conexión con la base de datos objetivo
            /// No necesita parámetros de entrada o salida
            /// </summary>
            
            try
            {
                this._cnn = new System.Data.OleDb.OleDbConnection();
                _cnn.ConnectionString = _data.DBProvider + _data.DBConnectionString;
                _cnn.Open();
            }
            catch (OleDbException ex)
            {
                throw SpinException.GetException("SpinDatabase:: " + ex.Message, ex);
            }
        }

        public void GetData(ref dynamic obj, params string[] parameters)
        {
            /// <summary>
            /// Se encarga de realizar peticiones de consulta a la base de datos que esté abierta en ese momento
            /// Campos necesarios:
            /// (OBLIGATORIO)DBQuery: Petición preparada para ejecutar.
            /// Si no lo tiene, no se ejecuta ninguna acción
            /// </summary>
            foreach (string valor in parameters)
            {
                switch (valor)
                {
                    case "Get":
                        GetValues(ref obj);
                        break;
                    default:
                        break;
                }
            }
           
        }

        public void SetData(ref dynamic obj, params string[] parameters)
        {
            /// <summary>
            /// Se encarga de realizar peticiones de consulta a la base de datos que esté abierta en ese momento
            /// Campos necesarios:
            /// (OBLIGATORIO)DBQuery: Petición preparada para ejecutar.
            /// Si no lo tiene, no se ejecuta ninguna acción
            /// </summary>
            foreach (string valor in parameters)
            {
                switch (valor)
                {
                    case "Set":
                        SetValues(ref obj);
                        break;
                    default:
                        break;
                }
            }

        }

        public void Stop()
        {
            /// <summary>
            /// Se encarga de cerrar la conexión con la base de datos objetivo
            /// No necesita parámetros de entrada o salida
            /// </summary>
            
            try
            {
                _cnn.Close();
                _cnn.Dispose();
                try
                {
                    cmd.Connection.Close();
                }
                catch
                {
                    Console.WriteLine("SpinDataBase::If you are working with MYSQL, this object isn´t necessary so it is null");
                }
            }
            catch (Exception ex)
            {
                throw SpinException.GetException("SpinDatabase:: " + ex.Message, ex);
            }
        }

        public event Dispatcher.ResultEventHandler NewResultEvent;

        #endregion

        #region Métodos

        internal void GetValues(ref dynamic data)
        {
            data = ExecuteGetQuery(data);
        }

        internal void SetValues(ref dynamic data)
        {
            ExecuteSetQuery(data);
        }


        internal void ExecuteSetQuery(dynamic obj)
        {
            try
            {
                if ((obj as IDictionary<string, object>).ContainsKey("DBQuery"))
                {
                    cmd = new System.Data.OleDb.OleDbCommand(obj.DBQuery, _cnn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw SpinException.GetException("SpinDatabase:: " + ex.Message, ex);
            }
        }

        internal dynamic ExecuteGetQuery(dynamic obj)
        {
            if (_data.DBType != "MYSQL")
            {
                if ((obj as IDictionary<string, object>).ContainsKey("DBQuery"))
                {

                    try
                    {
                        cmd = new System.Data.OleDb.OleDbCommand(obj.DBQuery, _cnn);

                        DataSet myDataSet = new DataSet();
                        // Execute the query
                        OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(cmd);
                        int valor = myDataAdapter.Fill(myDataSet, _data.DBDefaultTableName);

                        obj.Response = myDataSet.Tables;
                    }
                    catch (Exception ex)
                    {
                        throw SpinException.GetException("SpinDatabase:: " + ex.Message, ex);
                    }
                    return obj;

                }
                else
                {
                    return obj;
                }
            }
            else
                {
                    /* if ((obj.DataBase as IDictionary<string, object>).ContainsKey("DBQuery"))
                     {
                         String conexion = _data.DBConnectionString;
                        conexion = conexion.Replace("Password", "Pwd");
                        conexion = conexion.Replace("User ID", "Uid");
                        conexion = conexion.Replace("DATA SOURCE", "Database");
                        conexion = conexion.Replace("HOST", "Server");

                        MySqlConnection conn = new MySqlConnection(conexion);
                        conn.Open();
                        MySqlDataAdapter myDataAdapter = new MySqlDataAdapter(obj.DBQuery, conn);
                        DataSet myDataSet = new DataSet();
                        int valor = myDataAdapter.Fill(myDataSet, _data.DBDefaultTableName);
                        obj.Response = myDataSet.Tables;
                        conn.Close();
                    
                        return obj;
                     }*/
                    return null;
                }
        }

        #endregion
    }
}
