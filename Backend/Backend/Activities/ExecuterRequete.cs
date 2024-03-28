using System.Data.SqlClient;
using Elsa.Workflows;
using Elsa.Workflows.Memory;
using Python.Runtime;
using Backend.Models;
using Elsa.Workflows.Models;
using static Community.CsharpSqlite.Sqlite3;
using MySqlConnector;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Backend.Service;

namespace Backend.Activities
{
    public class ExecuterRequetek : Activity
    {
   
        public string ConnectionString { get; set; }
        public string Requete { get; set; }
        public string Type { get; set; }

        public ExecuterRequetek(Variable<string> req, Variable<string> connection, Variable<string> type)
        {
            ConnectionString = connection.Value.ToString()!;
            Requete = req.Value.ToString()!;
            Type = type.Value.ToString()!;
            TestRequet();
        }

        public void TestRequet()
        {
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string outputPath = Path.Combine(desktopPath, "output.txt");
            RequeteService requete=new RequeteService(ConnectionString,Requete,outputPath);
            if (Type == "MYSQL")
            {
                 requete.ExecuterRequetMysql();
            }
            else if(Type == "SQL SERVER")
            {
                requete.ExecuterRequetSqlServer();
            }

        }
        protected override void Execute(ActivityExecutionContext context)
        {
            TestRequet();
        }
    }
}
