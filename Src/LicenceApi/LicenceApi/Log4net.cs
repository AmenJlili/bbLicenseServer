﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace LicenceApi
{
    public static class  Log4net
    {

        public static void createlog(string FunctionName, string Op)
        {
            string Filename = DateTime.Today.Date.Ticks.ToString();
            string fullSavePath = HttpContext.Current.Server.MapPath(string.Format("~/App_Data/" + Filename));

            if (!File.Exists(fullSavePath))
            {
                File.Create(fullSavePath);
            }
            else
            {

               // File.WriteAllText(fullSavePath, String.Empty);
            }
            

            // Create a new file     
            using (StreamWriter sw = File.AppendText(fullSavePath))
            {
                sw.WriteLine("New file created: {0}", DateTime.Now.ToString());
                sw.WriteLine(FunctionName);
                sw.WriteLine(Op);
                sw.Close();
            }

        }

    }
}