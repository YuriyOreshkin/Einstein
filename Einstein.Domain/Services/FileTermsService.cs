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
    public class FileTermsService : FileBaseTemplate, ITermsService
    {
        private string filename;
        public FileTermsService(string _filename)
        {
            filename = _filename;
        }

        //Text
        public string GetTemplate()
        {
            
            return GetTemplateText(filename);
        }



        /// <summary>
        /// Save data to file
        /// </summary>
        public void SaveTemplate(string text)
         {
            SaveTemplate(filename,null, text);
         }

    }
}