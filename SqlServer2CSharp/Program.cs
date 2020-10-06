using System;

using SqlServer2CSharpLib;

namespace SqlServer2CSharp {

    class Program {

        static void Main(string[] args) {

            var ss = new SqlServer();
            var ok = ss.Connect("localhost", "sqlexpress", "EdDb");

            var students = ss.ExecuteQuery("SELECT * From Student;");

            ss.Disconnect();
        }
    }
}
