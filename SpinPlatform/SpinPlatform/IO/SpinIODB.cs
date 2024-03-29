﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpinPlatform.Errors;
using SpinPlatform.DataBase;
using System.Data;
using System.Dynamic;

namespace SpinPlatform
{
    namespace IO
    {
        public class SpinIODB : ISpinPlatformInterface
        {
            /// <summary>
            /// Módulo de entrada y salida de datos con BD
            /// Este módulo es el encargado de montar las consultas a la base de datos a laque accederáa través del módulo DataBase correspondiente
            /// NOTA: Para su uso es necesario realizar un "Init" para la configuración del módulo, un "Start" para abrir la conexión con la base de datos,
            /// Un Set o Get para recibir o enviar datos (a través de consultas genéricas) y finalizamos la conexión con un "Stop". 
            /// </summary>

            #region Variables
            private SpinDataBase _exeBaseDeDatos = null;
            private string _DBProvider;
            private string _DBType;
            private string _DBConnectionString;
            private string _DBDefaultTableName;
            private string _DBQueryType;
            #endregion

            #region Implementación de interface

            /// <summary>
            /// Inicializa el Modulo de Bases de Datos.
            /// </summary>
            /// <param name="obj">
            /// Campos Obligatorios:  \n
            /// DBProvider (string) \n
            /// DBType (string) \n
            /// DBConnectionString (string) \n
            /// DBDefaultTableName (string) \n
            /// DBQueryType (string) \n
            /// </param>
            public void Init(dynamic obj)
            {
                if ((obj as IDictionary<string, object>).ContainsKey("DataBase"))
                {
                    _DBProvider = obj.DataBase.DBProvider;
                    _DBType = obj.DataBase.DBType;
                    _DBConnectionString = obj.DataBase.DBConnectionString;
                    _DBDefaultTableName = obj.DataBase.DBDefaultTableName;
                    _DBQueryType = obj.DataBase.DBQueryType;
                }
                else
                {
                    _DBProvider = obj.DBProvider;
                    _DBType = obj.DBType;
                    _DBConnectionString = obj.DBConnectionString;
                    _DBDefaultTableName = obj.DBDefaultTableName;
                    _DBQueryType = obj.DBQueryType;
                }

                _exeBaseDeDatos = new SpinDataBase();
                _exeBaseDeDatos.Init(obj);
            }

            /// <summary>
            /// Arranca el Modulo de Basesde datos
            /// </summary>
            public void Start()
            {
                 _exeBaseDeDatos.Start();
            }

            /// <summary>
            /// Realiza lecturas en la base de datos \n
            /// </summary>
            /// <param name="obj">
            /// Variable dinamica donde guardar los resultados: \n
            /// Response (List(string)) Lista de mensajes obtenidos \n
            /// </param>
            /// <param name="parameters">
            /// "Get" - Obtiene los datos \n
            /// </param>
            public void GetData(ref dynamic obj, params string[] parameters)
            {
                obj.IODBReturnedData = parameters;
                try
                {
                    foreach (string valor in parameters)
                    {
                        switch (valor)
                        {
                            case "Get":
                                GetValues(ref obj);
                                break;
                            default:
                                obj.IODBErrors = "Wrong Query";
                                break;
                        }
                    }
                    obj.IODBErrors = "";
                }
                catch (Exception ex)
                {
                    obj.IODBErrors = ex.Message;
                }
            }

            /// <summary>
            /// Realiza escrituras en la base de datos preparando las queries \n
            /// </summary>
            /// <param name="obj">
            /// Variable dinamica de donde obtener los datos a establecer \n
            /// Message (List(string)) Lista de mensajes a guardar \n
            /// </param>
            /// <param name="parameters">
            /// "Set" - Guarda una nueva línea en la base de datos \n
            /// </param>
            public void SetData(ref dynamic obj, params string[] parameters)
            {
                try
                {
                    foreach (string valor in parameters)
                    {
                        switch (valor)
                        {
                            case "Set":
                                SetValues(ref obj);
                                break;
                            default:
                                obj.IODBErrors = "Wrong Query";
                                break;
                        }
                    } 
                    obj.IODBErrors = "";
                }
                catch (Exception ex)
                {
                    obj.IODBErrors = ex.Message;
                }
            }

            /// <summary>
            /// Para el Módulo de Bases de Datos
            /// </summary>
            public void Stop()
            {
                 _exeBaseDeDatos.Stop();
            }

            public event Dispatcher.ResultEventHandler NewResultEvent;

            #endregion

            #region Métodos

            internal void GetValues(ref dynamic obj)
            {
                getMessageToTable(ref obj);
                try
                {
                    DataTable table = obj.Response[_DBDefaultTableName];
                    obj.Response = new List<string>();
                    foreach (DataRow dr in table.Rows)
                    {
                        obj.Response.Add((string)dr[0]);
                    }
                }
                catch (Exception ex)
                {
                    throw SpinException.GetException("SpinIODB:: " + ex.Message, ex);
                }
            }

            internal void SetValues(ref dynamic obj)
            {
                addMessageToTable(obj);
            }

           internal void addMessageToTable(dynamic obj)
            {
                if ((obj as IDictionary<string, object>).ContainsKey("Message"))
                {
                    for (int i = 0; i < obj.Message.Count; i++)
                    {
                        try
                        {
                            //String columnas = "";
                            //if ((obj.DataBase as IDictionary<string, object>).ContainsKey("DBQueryColumns"))
                            //    columnas = "(" + obj.DataBase.DBQueryColumns[i] + ")";
                            //if (_data.DBQueryType == "INSERT")
                            //    obj.DataBase.DBQuery = _data.DBQueryType + " INTO " + _data.DBDefaultTableName + " " + columnas + " VALUES ('" + obj.Message[i] + "')";
                            //else if (_data.DBQueryType == "UPDATE")
                            //{
                            //    string valores = "";
                            //    string[] separators = new string[1];
                            //    separators[0] = ",";
                            //    string[] valo = ((String)obj.Message[i]).Split(separators, 1000, StringSplitOptions.RemoveEmptyEntries);
                            //    string[] name = ((String)obj.DataBase.DBQueryColumns[i]).Split(separators, 1000, StringSplitOptions.RemoveEmptyEntries);
                            //    for (int tam = 0; tam < valo.Length; tam++)
                            //    {
                            //        valores = name[tam] + "='" + valo[tam] + "', ";
                            //    }
                            //    obj.DataBase.DBQuery = _data.DBQueryType + " " + _data.DBDefaultTableName + " SET " + valores.Substring(0, valores.Length - 2) + " WHERE " + obj.DataBase.DBQueryUpdateRow[i];
                            //}
                            obj.DBQuery = _DBQueryType + " INTO " + _DBDefaultTableName + " VALUES ('" + obj.Message[i] + "')";
                            _exeBaseDeDatos.SetData(ref obj,"Set");
                        }
                        catch (Exception ex)
                        {
                            throw SpinException.GetException("SPINIODB:: " + ex.Message, ex);
                        }
                    }
                }
            }

            internal void getMessageToTable(ref dynamic obj)
            {
                String columns = " *";
                String parameters = "";
                try
                {

                    //if ((obj.DataBase as IDictionary<string, object>).ContainsKey("DBQueryColumns"))
                    //{
                    //    columns = " (";
                    //    foreach (String parame in obj.DataBase.DBQueryColumns)
                    //    {
                    //        columns += (String)parame + ", ";
                    //    }
                    //    columns = columns.Substring(0, columns.Length - 2) + ") ";
                    //}
                    //if ((obj.DataBase as IDictionary<string, object>).ContainsKey("DBQueryValues"))
                    //{
                    //    parameters = " WHERE ";
                    //    foreach (String parame in obj.DataBase.DBQueryValues)
                    //    {
                    //        parameters += parame;
                    //    }
                    //}

                    obj.DBQuery = _DBQueryType + columns + " FROM " + _DBDefaultTableName; //+ parameters;
                    _exeBaseDeDatos.GetData(ref obj,"Get");
                }
                catch (Exception ex)
                {
                    throw SpinException.GetException("SPINIODB:: " + ex.Message, ex);
                }
            }

            #endregion
        }
    }
}
