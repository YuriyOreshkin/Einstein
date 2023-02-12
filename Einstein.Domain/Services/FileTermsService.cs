using Einstein.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace Einstein.Domain.Services
{
    public class FileTermsService : ITermsService
    {
        private string filename;
        public FileTermsService(string _filename)
        {
            filename = _filename;
        }

        //Text
        public string GetTemplate()
        {
            
            return Read(filename);
        }

        /// <summary>
        /// Read data from file
        /// </summary>
        private string Read(string filename)
        {
            if (File.Exists(filename))
                return File.ReadAllText(filename);

            return String.Empty;
        }


        /// <summary>
        /// Save data to file
        /// </summary>
        public void SaveTemplate(string text)
         {
             string html =HttpUtility.HtmlDecode(text);

             using (StreamWriter file = new StreamWriter(filename, false))
             {
                 file.WriteLine(html);
             }
         }

    }
}