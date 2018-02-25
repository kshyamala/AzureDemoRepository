using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using AzureProjectDemo.Models;
namespace AzureProjectDemo.Manager
{
    public class DBManager
    {
        private string _connectionstring = string.Empty;
        public DBManager()
        {
            var connectionstr = new SqlConnectionStringBuilder();
            connectionstr.DataSource = "azure-demo-dbserver.database.windows.net";
            connectionstr.UserID = "SqlAdmin";
            connectionstr.Password = "Password-1";
            connectionstr.InitialCatalog = "AzureDemoDB";
            _connectionstring = connectionstr.ConnectionString;
        }
        public User GetUser(int id)
        {

            User user = null;

            using (var sqlconnection = new SqlConnection(_connectionstring))
            {
                sqlconnection.Open();
                string tsql = "SELECT ID, FirstName, LastName, Age, EmailID FROM [tbl_UserDetails] WHERE ID = @Id";

                using (var command = new SqlCommand(tsql, sqlconnection))
                {

                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            user = new User();
                            if (reader["ID"] != DBNull.Value)
                                user.ID = Convert.ToInt32(reader["ID"]);
                            if (reader["FirstName"] != DBNull.Value)
                                user.FirstName = Convert.ToString(reader["FirstName"]);
                            if (reader["LastName"] != DBNull.Value)
                                user.LastName = Convert.ToString(reader["LastName"]);
                            if (reader["Age"] != DBNull.Value)
                                user.Age = Convert.ToInt32(reader["Age"]);
                            if (reader["EmailID"] != DBNull.Value)
                                user.EmailId = Convert.ToString(reader["EmailID"]);
                            // Get Image

                        }
                    }
                }
            }
            return user;
        }

        public List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            using (var sqlconnection = new SqlConnection(_connectionstring))
            {
                sqlconnection.Open();
                string tsql = "SELECT ID, FirstName, LastName, Age, EmailID FROM [tbl_UserDetails] order by ID asc";

                using (var command = new SqlCommand(tsql, sqlconnection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User();
                            if (reader["ID"] != DBNull.Value)
                                user.ID = Convert.ToInt32(reader["ID"]);
                            if (reader["FirstName"] != DBNull.Value)
                                user.FirstName = Convert.ToString(reader["FirstName"]);
                            if (reader["LastName"] != DBNull.Value)
                                user.LastName = Convert.ToString(reader["LastName"]);
                            if (reader["Age"] != DBNull.Value)
                                user.Age = Convert.ToInt32(reader["Age"]);
                            if (reader["EmailID"] != DBNull.Value)
                                user.EmailId = Convert.ToString(reader["EmailID"]);
                            users.Add(user);
                        }
                    }
                }
            }
            return users;
        }

        public int CreateUser(User user)
        {
            int numRowsAffected = 0;

            using (var sqlconnection = new SqlConnection(_connectionstring))
            {
                sqlconnection.Open();
                string tsql = "INSERT INTO [tbl_UserDetails] VALUES(@FirstName, @LastName, @Age, @EmailID, NULL);";

                using (var command = new SqlCommand(tsql, sqlconnection))
                {
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Age", user.Age);
                    command.Parameters.AddWithValue("@EmailID", user.EmailId);
                    numRowsAffected = command.ExecuteNonQuery();
                }
            }
            return numRowsAffected;
        }


        public int UpdateUser(User user)
        {
            int numRowsAffected = 0;

            using (var sqlconnection = new SqlConnection(_connectionstring))
            {
                sqlconnection.Open();
                string tsql = "UPDATE [tbl_UserDetails] SET FirstName = @FirstName, LastName = @LastName, Age = @Age, EmailID = @EmailID WHERE ID = @ID;";

                using (var command = new SqlCommand(tsql, sqlconnection))
                {
                    command.Parameters.AddWithValue("@ID", user.ID);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Age", user.Age);
                    command.Parameters.AddWithValue("@EmailID", user.EmailId);
                    numRowsAffected = command.ExecuteNonQuery();
                }
            }
            return numRowsAffected;
        }
    }
}
