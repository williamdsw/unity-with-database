
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using UnityEngine;

namespace DAO.Utils
{
    /// <summary>
    /// Factory for Database
    /// </summary>
    /// <typeparam name="T"> Generic Type </typeparam>
    public static class Factory<T>
    {
        /// <summary>
        /// Create instance of given type
        /// </summary>
        /// <param name="reader"> Reader with data </param>
        /// <returns> New Instance </returns>
        public static T CreateOne(IDataReader reader)
        {
            T instance = (T)Activator.CreateInstance(typeof(T));

            if (reader.FieldCount > 0)
            {
                int index = 0;
                foreach (PropertyInfo prop in instance.GetType().GetProperties())
                {
                    try
                    {
                        if (index < reader.FieldCount)
                        {
                            prop.SetValue(instance, (prop.Name != "Status") ? (reader[prop.Name] != DBNull.Value ? (prop.PropertyType == typeof(string) ? (string)reader[prop.Name] : reader[prop.Name]) : null) : ((long)reader[prop.Name] == 1));
                        }

                        index++;
                    }
                    catch (Exception ex)
                    {
                        Debug.LogErrorFormat("{0}===={1}", prop.Name, ex.Message);
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// Create many instances of given type
        /// </summary>
        /// <param name="reader"> Reader with data </param>
        /// <returns> List with instances </returns>
        public static List<T> CreateMany(IDataReader reader)
        {
            List<T> instances = new List<T>();

            if (reader.FieldCount > 0)
            {
                while (reader.Read())
                {
                    instances.Add(CreateOne(reader));
                }
            }

            return instances;
        }
    }
}