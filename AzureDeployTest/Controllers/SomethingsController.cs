using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AzureDeployTest.Models;
using Microsoft.Azure;

namespace AzureDeployTest.Controllers
{
    public class SomethingsController : ApiController
    {
        [HttpPost]
        public void Create(Something something)
        {
            AddName(something.Name);
        }

        [HttpGet]
        public IEnumerable<string> GetAll()
        {
            return GetNames();
        }

        private void AddName(string name)
        {
            using (SqlConnection connection = new SqlConnection(CloudConfigurationManager.GetSetting("DbConnectionString")))
            {
                SqlCommand command = new SqlCommand(@"INSERT INTO Somethings(name) VALUES(@something_name)", connection);
                command.Parameters.AddWithValue("@something_name", name);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private IEnumerable<string> GetNames()
        {
            List<string> result = new List<string>();

            using (SqlConnection connection = new SqlConnection(CloudConfigurationManager.GetSetting("DbConnectionString")))
            {
                SqlCommand command = new SqlCommand(@"SELECT name FROM Somethings", connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    
                    while (reader.Read())
                    {
                        result.Add(reader.GetString(0));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }

            return result;
        }
    }
}
