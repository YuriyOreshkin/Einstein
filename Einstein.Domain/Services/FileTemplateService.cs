using Einstein.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace Einstein.Domain.Services
{
    public class FileTemplateService : ITemplateService
    {
        private string filename;

        public FileTemplateService(string _filename)
        {
            filename = _filename;
        }

        //Text
        public string GetTemplateText()
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

        private void GetLookups(PropertyInfo[] properties, object obj, string anchor, ref Dictionary<string, string> rules)
        {
            if (obj != null)
            {

                foreach (PropertyInfo property in properties)
                {

                    if (property.PropertyType.Namespace.Contains("System"))
                    {
                        if (property.GetValue(obj) != null)
                        {
                            rules.Add(anchor + "." + property.Name, property.GetValue(obj).ToString());
                        }
                        else
                        {
                            rules.Add(anchor + "." + property.Name, "null");
                        }

                    }
                    else
                    {

                        GetLookups(property.PropertyType.GetProperties(), property.GetValue(obj), anchor + "." + property.Name, ref rules);
                    }
                }
            }
        }



    /// <summary>
    /// 
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
        public string FullTemplate(ORDER order)
        {

            Dictionary<string, string> rules = new Dictionary<string, string>();
            //var objj = typeof(PriorityViewModel).GetProperty("priority").GetValue(notification);
            GetLookups(typeof(ORDER).GetProperties(), order, "order", ref rules);


            string result = GetTemplateText();
            foreach (KeyValuePair<string, string> rule in rules)
            {
                Regex regex = new Regex("{" + rule.Key + "}", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
                result = regex.Replace(result, rule.Value);
            }

            return result;
        }


        /// <summary>
        /// Save data to file
        /// </summary>
        public void SaveTemplate(string text)
         {
             string html = HttpUtility.HtmlDecode(text);

             using (StreamWriter file = new StreamWriter(filename, false))
             {
                 file.WriteLine(html);
             }
         }

        
    }
}