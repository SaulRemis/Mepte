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
        /// Módulo para conexión con bases de datos a bajo nivel
        /// Éste módulo realiza laconexión con una base de datos según los parámetros facilitados. Soporta los siguientes tipos de bases de datos:
        /// Access, MySQL, ORACLE, SQLite, DB2 y SQL Server. 
        /// Para su uso es necesario realizar un "Init" para la configuración del módulo, un "Start" para abrir la conexión con la base de datos,
        /// Un Set o Get para recibir o enviar datos (a través de consultas preparadas anteriormente) y finalizamos la conexión con un "Stop". 
        /// </summary>

        #region Variables
        private string _DBProvider;
        private string _DBType = "Unknow";
        private string _DBConnectionString;
        private string _DBDefaultTableName;
        private System.Data.OleDb.OleDbCommand _cmd = null;
        private System.Data.OleDb.OleDbConnection _cnn = null;

        #endregion

        #region Implementación de interface

        /// <summary>
        /// Se encarga de guardar en las variables internas de la clase los parámentros necesarios para laconexión con la base de datos
        /// En concreto el objetoinitData de entrada debe disponer de los siguientes campos:
        /// (OBLIGATORIO)DBProvider: nombre del componente software encargado de interactuar entre el OLEDB y la base de datos
        /// (OBLIGATORIO)DBDefaultTableName: nombre de la tabla sobre la que se realizarán consultas
        /// (OPCIONAL) DBDbType: tipo de base de datos a la que se accede (MYSql, SQL Server, ORACLE, DB2, SQLite o Access)
        /// (OBLIGATORIO)DBConnectionString: parámetros de conexión. nombre de la base de datos, localización, usuario y contraseña...
        /// </summary>
        public void Init(dynamic initData)
        {
            if ((initData as IDictionary<string, object>).ContainsKey("DataBase"))
            {
                _DBProvider = initData.DataBase.DBProvider;
                if ((initData.DataBase as IDictionary<string, object>).ContainsKey("DBType"))
                     _DBType = initData.DataBase.DBType;
                _DBConnectionString = initData.DataBase.DBConnectionString;
                _DBDefaultTableName = initData.DataBase.DBDefaultTableName;
            }
            else
            {
                _DBProvider = initData.DBProvider;
                if ((initData as IDictionary<string, object>).ContainsKey("DBType"))
                         _DBType = initData.DBType;
                _DBConnectionString = initData.DBConnectionString;
                _DBDefaultTableName = initData.DBDefaultTableName;
            } 
        }


        /// <summary>
        /// Se encarga de abrir la conexión con la base de datos objetivo
        /// No necesita parámetros de entrada o salida
        /// </summary>
        public void Start()
        {
            
            try
            {
                this._cnn = new System.Data.OleDb.OleDbConnection();
                _cnn.ConnectionString = _DBProvider + _DBConnectionString;
                _cnn.Open();
            }
            catch (OleDbException ex)
            {
                throw SpinException.GetException("SpinDatabase:: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Se encarga de realizar peticiones de consulta a la base de datos que esté abierta en ese momento
        /// Campos necesarios:
        /// (OBLIGATORIO)DBQuery: Petición preparada para ejecutar.
        /// Si no lo tiene, no se ejecuta ninguna acción
        /// </summary>
        public void GetData(ref dynamic obj, params string[] parameters)
        {
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


        /// <summary>
        /// Se encarga de realizar peticiones de consulta a la base de datos que esté abierta en ese momento
        /// Campos necesarios:
        /// (OBLIGATORIO)DBQuery: Petición preparada para ejecutar.
        /// Si no lo tiene, no se ejecuta ninguna acción
        /// </summary>
        public void SetData(ref dynamic obj, params string[] parameters)
        {
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

        /// <summary>
        /// Se encarga de cerrar la conexión con la base de datos objetivo
        /// No necesita parámetros de entrada o salida
        /// </summary>
        public void Stop()
        {
            
            
            try
            {
                _cnn.Close();
                _cnn.Dispose();
                try
                {
                    _cmd.Connection.Close();
                }
                catch
                {
                    Console.WriteLine("SpinDataBase::If you are working with MYSQL, this object isn´t necessary or it is null");
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
                    _cmd = new System.Data.OleDb.OleDbCommand(obj.DBQuery, _cnn);
                    _cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw SpinException.GetException("SpinDatabase:: " + ex.Message, ex);
            }
        }

        internal dynamic ExecuteGetQuery(dynamic obj)
        {
            if (_DBType != "MYSQL")
            {
                if ((obj as IDictionary<string, object>).ContainsKey("DBQuery"))
                {

                    try
                    {
                        _cmd = new System.Data.OleDb.OleDbCommand(obj.DBQuery, _cnn);

                        DataSet myDataSet = new DataSet();
                        // Execute the query
                        OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(_cmd);
                        int valor = myDataAdapter.Fill(myDataSet, _DBDefaultTableName);

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
                     if ((obj.DataBase as IDictionary<string, object>).ContainsKey("DBQuery"))
                     {
                         String conexion = _DBConnectionString;
                        conexion = conexion.Replace("Password", "Pwd");
                        conexion = conexion.Replace("User ID", "Uid");
                        conexion = conexion.Replace("DATA SOURCE", "Database");
                        conexion = conexion.Replace("HOST", "Server");

                      //  MySqlConnection conn = new MySqlConnection(conexion);
                        //conn.Open();
                        //MySqlDataAdapter myDataAdapter = new MySqlDataAdapter(obj.DBQuery, conn);
                        DataSet myDataSet = new DataSet();
                        //int valor = myDataAdapter.Fill(myDataSet, _DBDefaultTableName);
                        obj.Response = myDataSet.Tables;
                        //conn.Close();
                    
                        return obj;
                     }
                    return null;
                }
        }

        #endregion
    }
}
