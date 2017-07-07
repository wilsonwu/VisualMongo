using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml.Linq;
using MongoDB.Driver;
using VisualMongo.Entity;

namespace VisualMongo.Service
{
    public static class ConnectionService
    {
        public static string appDataFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "VisualMongo");
        public static string appDataFilePath = Path.Combine(appDataFolderPath, "VisualMongo.xml");
       

        public static bool InitConfigurationFile(bool isOverWrite = false)
        {
            bool result = false;
            bool isNeedCreate = false;
            if (isOverWrite == false)
            {
                if (File.Exists(appDataFilePath) == false)
                {
                    isNeedCreate = true;
                }
                else
                {
                    isNeedCreate = false;
                }
            }
            else
            {
                isNeedCreate = true;
            }

            if (isNeedCreate == true)
            {
                //Create appdata file.
                try
                {
                    if (Directory.Exists(appDataFolderPath) == false)
                    {
                        Directory.CreateDirectory(appDataFolderPath);
                    }
                    XDocument document = new XDocument(
                        new XElement("VisualMongo",
                            new XElement("Connections"),
                            new XElement("Configurations"))
                            );
                    document.Save(appDataFilePath);
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }

            return result;
        }

        public static ConnectionEntity AddConnection(string connectionName, string serverName, int serverPort, string userName, string password, string databases)
        {
            bool isNeedToAdd = true;
            ConnectionEntity result = null;
            InitConfigurationFile();
            try
            {
                XDocument document = XDocument.Load(appDataFilePath);
                var getElements = document.Root.Element("Connections").Elements();
                if (getElements != null)
                {
                    if (getElements.Count() > 0)
                    {
                        foreach (var element in getElements)
                        {
                            if (element.Element("ConnectionName").Value == connectionName)
                            {
                                isNeedToAdd = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        isNeedToAdd = true;
                    }
                }
                else
                {
                    isNeedToAdd = true;
                }

                if (isNeedToAdd == true)
                {
                    document.Root.Element("Connections").Add(new XElement("Connection",
                        new XElement("ConnectionName", connectionName),
                        new XElement("ServerName", serverName),
                        new XElement("ServerPort", serverPort),
                        new XElement("UserName", userName),
                        new XElement("Password", password),
                        new XElement("Databases", databases)
                        ));
                    document.Save(appDataFilePath);
                    result = new ConnectionEntity
                    {
                        ConnectionName = connectionName,
                        ServerName = serverName,
                        ServerPort = serverPort,
                        UserName = userName,
                        Password = password,
                        Databases = databases,
                    };
                }
            }
            catch (Exception ex)
            {
                InitConfigurationFile(true);
            }

            return result;
        }

        public static ConnectionEntity UpdateConnection(string connectionNameOld, string connectionNameNew, string serverName, int serverPort, string userName, string password, string databases)
        {
            bool isCouldUpdate = true;
            ConnectionEntity result = null;
            XDocument document = XDocument.Load(appDataFilePath);
            if (connectionNameOld == connectionNameNew)
            {
                isCouldUpdate = true;
            }
            else
            {
                var getElements = document.Root.Element("Connections").Elements();
                if (getElements != null)
                {
                    if (getElements.Count() > 0)
                    {
                        if (getElements.Where(e => e.Element("ConnectionName").Value == connectionNameNew).Count() <= 0)
                        {
                            isCouldUpdate = true;
                        }
                        else
                        {
                            isCouldUpdate = false;
                        }
                    }
                    else
                    {
                        isCouldUpdate = false;
                    }
                }
                else
                {
                    isCouldUpdate = false;
                }
            }

            if (isCouldUpdate == true)
            {
                XElement getElement = document.Root.Element("Connections").Elements().Where(e => e.Element("ConnectionName").Value == connectionNameOld).FirstOrDefault();
                if (getElement != null)
                {
                    getElement.SetElementValue("ConnectionName", connectionNameNew);
                    getElement.SetElementValue("ServerName", serverName);
                    getElement.SetElementValue("ServerPort", serverPort);
                    getElement.SetElementValue("UserName", userName);
                    getElement.SetElementValue("Password", password);
                    getElement.SetElementValue("Databases", databases);
                    document.Save(appDataFilePath);
                    result = new ConnectionEntity
                    {
                        ConnectionName = connectionNameNew,
                        ServerName = serverName,
                        ServerPort = serverPort,
                        UserName = userName,
                        Password = password,
                        Databases = databases,
                    };
                }
            }
            return result;
        }

        public static bool DeleteConnection(string connectionName)
        {
            XDocument document = XDocument.Load(appDataFilePath);
            document.Root.Element("Connections").Elements().Where(e => e.Element("ConnectionName").Value == connectionName).FirstOrDefault().Remove();
            document.Save(appDataFilePath);
            return true;
        }

        public static ConnectionEntity GetConnection(string connectionName)
        {
            ConnectionEntity result = null;
            XDocument document = XDocument.Load(appDataFilePath);
            var getElements = document.Root.Element("Connections").Elements();
            if (getElements != null)
            {
                if (getElements.Count() > 0)
                {
                    foreach (var element in getElements)
                    {
                        if (element.Element("ConnectionName").Value == connectionName)
                        {
                            result = new ConnectionEntity
                            {
                                ConnectionName = element.Element("ConnectionName").Value,
                                ServerName = element.Element("ServerName").Value,
                                ServerPort = Convert.ToInt32(element.Element("ServerPort").Value),
                                UserName = element.Element("UserName").Value,
                                Password = element.Element("Password").Value,
                                Databases = element.Element("Databases").Value,
                            };
                            break;
                        }
                    }
                }
            }

            return result;
        }

        public static List<ConnectionEntity> GetAllConnections()
        {
            InitConfigurationFile();
            List<ConnectionEntity> result = new List<ConnectionEntity>();
            XDocument document = XDocument.Load(appDataFilePath);
            var getElements = document.Root.Element("Connections").Elements();
            if (getElements != null)
            {
                result = getElements.Select(e => new ConnectionEntity
                {
                    ConnectionName = e.Element("ConnectionName").Value,
                }).ToList();
            }
            return result;
        }

        public static bool Connect(ConnectionEntity connection)
        {
            bool result = false;
            if (connection != null)
            {
                string connectionString = GetConnectionString(connection);
                try
                {
                    VisualMongoDB.VMongoClient = new MongoClient(connectionString);
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                }

            }
            return result;
        }

        public static bool TryConnect(string serverName, int serverPort, string userName, string password, string databases)
        {
            bool result = false;
            if (string.IsNullOrEmpty(serverName) == false)
            {
                string connectionString = GetConnectionString(serverName, serverPort, userName, password, databases);
                try
                {
                    VisualMongoDB.VMongoClient = new MongoClient(connectionString);
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }
            return result;
        }

        private static string GetConnectionString(ConnectionEntity connection)
        {
            string connectionString = string.Empty;
            if (connection != null)
            {
                string user = (string.IsNullOrEmpty(connection.UserName) == false && string.IsNullOrEmpty(connection.Password) == false) ? connection.UserName + ":" + connection.Password + "@" : string.Empty;
                connectionString = "mongodb://" + user + connection.ServerName + ":" + connection.ServerPort;
            }
            return connectionString;
        }

        private static string GetConnectionString(string serverName, int serverPort, string userName, string password, string databases)
        {
            string connectionString = string.Empty;
            if (string.IsNullOrEmpty(serverName) == false)
            {
                string user = (string.IsNullOrEmpty(userName) == false && string.IsNullOrEmpty(password) == false) ? userName + ":" + password + "@" : string.Empty;
                connectionString = "mongodb://" + user + serverName + ":" + serverPort;
            }
            return connectionString;
        }
    }
}
