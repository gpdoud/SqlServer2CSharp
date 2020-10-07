using System;

using PrsLib;
using PrsLib.User;

namespace SqlServer2CSharp {

    class Program {

        static void Main(string[] args) {

            var conn = new Connection("localhost", "PrsDb");
            conn.Connect();

            var usersController = new UsersController(conn);
            var user = new User {
                Id = 0, Username = "xxxx", Password = "xx", Firstname = "xx", Lastname = "xx",
                Phone = "xx", Email = "xx", IsAdmin = true, IsReviewer = true
            };
            var recsAffected = usersController.Insert(user);
            var id = usersController.GetLastGeneratedId();
            user = usersController.GetUser(id);
            user.Username += "x";
            recsAffected = usersController.Update(user);
            recsAffected = usersController.Delete(user.Id);
            var users = usersController.GetUsers();

            conn.Disconnect();

        }
    }
}
