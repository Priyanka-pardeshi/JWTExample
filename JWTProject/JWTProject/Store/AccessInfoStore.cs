using JWTProject.Models;
using JWTProject.Store.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace JWTProject.Store
{
    public class AccessInfoStore: IAccessInfoStore
    {
        private readonly IOptions<appsettings> _options;
        public AccessInfoStore(IOptions<appsettings> options)
        {
                _options = options;
        }
        public List<UserAccess> AccessForuser()
        {
            var userRecords = new List<UserAccess>();
            try
            {
               

                using (var connection = new SqlConnection(_options.Value.ConnectionStrings))
                {
                    connection.Open();

                    var query = "SELECT * FROM UserGuidelines";

                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var userAccess = new UserAccess
                                {
                                    UserGuideline1 = reader.GetString(reader.GetOrdinal("UserGuideline1")),
                                    UserGuideline2 = reader.GetString(reader.GetOrdinal("UserGuideline2")),
                                    // Add other properties as needed
                                };
                                userRecords.Add(userAccess);
                            }
                        }
                    }
                }
            }catch (Exception ex)
            {

            }
            return userRecords;
        }
        public List<AdminAccess> AccessForAdmin()
        {
            var adminRecords = new List<AdminAccess>();
            try
            {
                using (var connection = new SqlConnection(_options.Value.ConnectionStrings))
                {
                    connection.Open();

                    var query = "SELECT * FROM AdminGuidelines";

                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var adminAccess = new AdminAccess
                                {
                                    AdminGuideline1 = reader.GetString(reader.GetOrdinal("AdminGuideline1")),
                                    AdminGuideline2 = reader.GetString(reader.GetOrdinal("AdminGuideline2")),
                                    // Add other properties as needed
                                };
                                adminRecords.Add(adminAccess);
                            }
                        }
                    }
                }
            }catch(Exception ex) { }
            return adminRecords;
        }
    }
}
