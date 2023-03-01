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
    public class FileTemplateService : FileBaseTemplate, ITemplateService
    {
        private string filename;
        private  string anchor;
        public FileTemplateService(string _filename,string _anchor)
        {
            filename = _filename;
            anchor = _anchor;
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
                            if (property.PropertyType == typeof(DateTime))
                            {
                                rules.Add(anchor + "." + property.Name,((DateTime)property.GetValue(obj)).ToString("dd.MM.yyyy"));
                            }
                            else
                            {
                                rules.Add(anchor + "." + property.Name, property.GetValue(obj).ToString());
                            }
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
        private string FullTemplate(object order, string text)
        {

            Dictionary<string, string> rules = new Dictionary<string, string>();
            //var objj = typeof(PriorityViewModel).GetProperty("priority").GetValue(notification);
            GetLookups(order.GetType().GetProperties(), order, anchor, ref rules);


            string result = text;
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
        public void SaveTemplate(string subject, string text)
         {
            SaveTemplate(filename, subject, text);
            
         }

       

        public string GetTemplateBody()
        {
            var body = GetHtmlBody(filename);
            return body;
        }

        public string GetTemplateSubject()
        {

            var subject = GetHtmlSubject(filename);

            return subject;
        }

        public string GetTemplateBody(object order)
        {
            var body = GetTemplateBody();
            body = FullTemplate(order, body);

            return body;
        }

        public string GetTemplateSubject(object order)
        {
            var subject = GetTemplateSubject();
            subject = FullTemplate(order, subject);

            return subject;
        }


        public Dictionary<string, string> AvailableParameters(Type type)
        {
            var parameters = new Dictionary<string, string>();
            AvailableParameters(type, anchor, ref parameters);

            return parameters;
        }

        private void AvailableParameters(Type type,string anchor, ref Dictionary<string, string> result)
        {
            
            foreach (PropertyInfo property in type.GetProperties())
            {

                if (property.PropertyType.Namespace.Contains("System"))
                {

                    var attr = property.GetCustomAttribute(typeof(DisplayNameAttribute),false);
                        result.Add("{"+ anchor+"." +property.Name+"}", attr != null ? ((DisplayNameAttribute)attr).DisplayName :"");

                }
                else
                {

                      AvailableParameters(property.PropertyType, anchor+"."+property.Name, ref result);
                }
            }
        }
    }
}