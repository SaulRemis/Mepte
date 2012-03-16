using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpinPlatform.Config;
using SpinPlatform.Errors;
using System.Data;
using System.Data.OleDb;
using System.Xml.Serialization;
//using MySql.Data.MySqlClient;

namespace SpinPlatform.DataBase
{
    internal class SpinDataBase : ISpinPlatformInterface
    {
        /// <summary>
        /// Class to connect and send queries to the database (configurable by the AppConfig.xml)
        /// </summary>

        #region Variables

        private String _query = null;
        private String _provider = null;
        private String _defaultTableName = null;
        private String _dbType = null;
        private String _connectionString = null;

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
            _provider = initData.DBProvider;
            _defaultTableName = initData.DBDefaultTableName;
            try
            {
                _dbType = initData.DBDbType;
            }
            catch
            {
                _dbType = "Unknow";
            }
            _connectionString = initData.DBConnectionString;
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
                _cnn.ConnectionString = _provider + _connectionString;
                _cnn.Open();
            }
            catch (OleDbException ex)
            {
                throw SpinException.GetException("SpinDatabase:: " + ex.Message, ex);
            }
        }

        public object GetData(dynamic obj)
        {
            /// <summary>
            /// Se encarga de realizar peticiones de consulta a la base de datos que esté abierta en ese momento
            /// Campos necesarios:
            /// (OBLIGATORIO)DBQuery: Petición preparada para ejecutar.
            /// (OPCIONAL)DBResponse: Es donde se guarda la respuesta de la petición. Si no existe, se crea y devuelve.
            /// </summary>
            
            obj = ExecuteGetQuery(obj);
            return obj;
        }

        public void SetData(dynamic obj)
        {
            /// <summary>
            /// Se encarga de realizar peticiones de consulta a la base de datos que esté abierta en ese momento
            /// Campos necesarios:
            /// (OBLIGATORIO)DBQuery: Petición preparada para ejecutar.
            /// </summary>
            
            ExecuteSetQuery(obj);
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

        internal void ExecuteSetQuery(dynamic obj)
        {
            try
            {
                cmd = new System.Data.OleDb.OleDbCommand(obj.DBQuery, _cnn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw SpinException.GetException("SpinDatabase:: " + ex.Message, ex);
            }
        }

        internal dynamic ExecuteGetQuery(dynamic obj)
        {
            if (_dbType != "MYSQL")
            {
                if (obj.DBQuery != null)
                {

                    try
                    {
                        cmd = new System.Data.OleDb.OleDbCommand(obj.DBQuery, _cnn);

                        DataSet myDataSet = new DataSet();
                        // Execute the query
                        OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(cmd);
                        int valor = myDataAdapter.Fill(myDataSet, _defaultTableName);

                        obj.DBResponse = myDataSet.Tables;
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
                /*
                    String conexion = _connectionString;
                    conexion = conexion.Replace("Password", "Pwd");
                    conexion = conexion.Replace("User ID", "Uid");
                    conexion = conexion.Replace("DATA SOURCE", "Database");
                    conexion = conexion.Replace("HOST", "Server");

                    MySqlConnection conn = new MySqlConnection(conexion);
                    conn.Open();
                    MySqlDataAdapter myDataAdapter = new MySqlDataAdapter(obj.DBQuery, conn);
                    DataSet myDataSet = new DataSet();
                    int valor = myDataAdapter.Fill(myDataSet,_defaultTableName);
                    obj.DBResponse = myDataSet.Tables;
                    conn.Close();
                    
                    return obj;*/
                    return null;
                }
        }

        #endregion
    }
}
