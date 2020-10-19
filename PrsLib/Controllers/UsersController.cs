using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using PrsLib.Models;

using Microsoft.Data.SqlClient;

namespace PrsLib.Controllers {
    
    public class UsersController {

        private readonly Connection connection = null;
        private static string SqlGetAll = "SELECT * from Users;";
        private static string SqlGetPk = "SELECT * from Users Where Id = @Id;";
        private static string SqlInsert = "INSERT Users " +
            " (Username, Password, Firstname, Lastname, Phone, Email, IsAdmin, IsReviewer) " +
            " VALUES (@Username, @Password, @Firstname, @Lastname, @Phone, @Email, @IsAdmin, @IsReviewer);";
        private static string SqlUpdate = "UPDATE Users Set " +
            " Username = @Username, Password = @Password, Firstname = @Firstname, Lastname = @Lastname, " +
            " Phone = @Phone, Email = @Email, IsAdmin = @IsAdmin, IsReviewer = @IsReviewer " +
            " WHERE Id = @Id; ";
        private static string SqlDelete = "DELETE Users where Id = @Id;";

        public UsersController(Connection connection) {
            this.connection = connection;
        }

        public IEnumerable<User> GetUsers() {
            var cmd = new SqlCommand(SqlGetAll, connection.sqlConnection);
            var users = new List<User>();
            var reader = cmd.ExecuteReader();
            while(reader.Read()) {
                var user = new User();
                users.Add(user);
                ReaderToUserInstance(reader, user);
            }
            reader.Close();
            return users;
        }

        public User GetUser(int id) {
            var cmd = new SqlCommand(SqlGetPk, connection.sqlConnection);
            cmd.Parameters.AddWithValue("@Id", id);
            var reader = cmd.ExecuteReader();
            if (!reader.HasRows) return null;
            reader.Read();
            var user = new User();
            ReaderToUserInstance(reader, user);
            reader.Close();
            return user;
        }

        public int Insert(User User) {
            var cmd = new SqlCommand(SqlInsert, connection.sqlConnection);
            UserInstanceToSqlParameters(User, cmd.Parameters);
            return cmd.ExecuteNonQuery();
        }

        public int Update(User User) {
            var cmd = new SqlCommand(SqlUpdate, connection.sqlConnection);
            UserInstanceToSqlParameters(User, cmd.Parameters);
            IdToSqlParameter(User.Id, cmd.Parameters);
            return cmd.ExecuteNonQuery();
        }

        public int Delete(int Id) {
            var cmd = new SqlCommand(SqlDelete, connection.sqlConnection);
            IdToSqlParameter(Id, cmd.Parameters);
            return cmd.ExecuteNonQuery();
        }

        public int GetLastGeneratedId() {
            var sql = "SELECT MAX(Id) From Users;";
            var cmd = new SqlCommand(sql, connection.sqlConnection);
            var lastId = cmd.ExecuteScalar();
            return Convert.ToInt32(lastId);
        }

        private void IdToSqlParameter(int Id, SqlParameterCollection Parameters) {
            Parameters.AddWithValue("@Id", Id);
        }

        private void UserInstanceToSqlParameters(User User, SqlParameterCollection Parameters) {
            Parameters.AddWithValue("@Username", User.Username);
            Parameters.AddWithValue("@Password", User.Password);
            Parameters.AddWithValue("@Firstname", User.Firstname);
            Parameters.AddWithValue("@Lastname", User.Lastname);
            Parameters.AddWithValue("@Phone", User.Phone);
            Parameters.AddWithValue("@Email", User.Email);
            Parameters.AddWithValue("@IsAdmin", User.IsAdmin);
            Parameters.AddWithValue("@IsReviewer", User.IsReviewer);
        }

        private void ReaderToUserInstance(SqlDataReader reader, User user) {
            user.Id = Convert.ToInt32(reader["Id"]);
            user.Username = Convert.ToString(reader["Username"]);
            user.Password = Convert.ToString(reader["Password"]);
            user.Firstname = Convert.ToString(reader["Firstname"]);
            user.Lastname = Convert.ToString(reader["Lastname"]);
            user.Phone = reader.IsDBNull("Phone") ? null : Convert.ToString(reader["Phone"]);
            user.Email = reader.IsDBNull("Email") ? null : Convert.ToString(reader["Email"]);
            user.IsAdmin = Convert.ToBoolean(reader["IsAdmin"]);
            user.IsReviewer = Convert.ToBoolean(reader["IsReviewer"]);
        }
    }
}
