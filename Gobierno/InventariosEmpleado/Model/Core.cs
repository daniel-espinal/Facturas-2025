using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.IO;
using System.Text;
using System.Security.Cryptography;

using System.Drawing;

using solg.lib.netframework;
using DevExpress.DataAccess.Sql;
using DevExpress.DataAccess.ConnectionParameters;

namespace FacturacionGobierno.Model
{
    class Core
    {
        public static string GetValueEncrypted(string key)
        {
            return Settings.GetInstance().GetAppSetting(key, true);
        }

        public static string GetValueDecrypted(string key)
        {
            return Settings.GetInstance().GetAppSetting(key, false);
        }


        public static SqlDataSource DataSourceReport(string key)
        {
            string connectionString = GetValueEncrypted(key);
            connectionString += "XpoProvider=MSSqlServer"; 
            CustomStringConnectionParameters connectionParameters = new CustomStringConnectionParameters(connectionString);
            return new SqlDataSource(connectionParameters);
        }



    }
}
