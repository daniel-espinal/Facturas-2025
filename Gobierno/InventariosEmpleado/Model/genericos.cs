﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacturacionGobierno.Model
{
    public static class genericos
    {
        public static void Log(string vAplicacion, string vInfo, string vUsuario)
        {
            StreamWriter log;
            FileStream fileStream = null;
            DirectoryInfo logDirInfo = null;
            FileInfo logFileInfo;

            string logFilePath = @"C:\\log\\";
            logFilePath = logFilePath + "Log_FACTURACION_" + System.DateTime.Today.ToString("MM-dd-yyyy") + "." + "txt";
            logFileInfo = new FileInfo(logFilePath);
            logDirInfo = new DirectoryInfo(logFileInfo.DirectoryName);
            if (!logDirInfo.Exists) logDirInfo.Create();
            if (!logFileInfo.Exists)
            {
                fileStream = logFileInfo.Create();
            }
            else
            {
                fileStream = new FileStream(logFilePath, FileMode.Append);
            }
            log = new StreamWriter(fileStream);
            log.WriteLine(System.DateTime.Today.ToString("MM-dd-yyyy hh:mm:ss") + " - " + vAplicacion + " - " + vInfo + " - " + vUsuario);
            log.Close();
        }
    }
}
