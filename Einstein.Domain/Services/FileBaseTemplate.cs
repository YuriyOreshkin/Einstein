using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Einstein.Domain.Services
{
    public class FileBaseTemplate
    {
        public string GetTemplateText(string filename)
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
        public void SaveTemplate(string filename,string subject,string text)
        {
            string html = subject==null ? HttpUtility.HtmlDecode(text) : "<subject>" + subject + "</subject>" + "<body>" + HttpUtility.HtmlDecode(text) + "</body>";
           
            using (StreamWriter file = new StreamWriter(filename, false))
            {
                file.WriteLine(html);
            }
        }


        private string TextInTag(string tag, string text)
        {
            string pattern = "(<" + tag + ">)(.*)(</" + tag + ">)";
            Regex regex = new Regex(pattern);

            var match = regex.Match(text);

            return match.Groups[2].Value;
        }

        public string GetHtmlBody(string filename)
        {
            var text = GetTemplateText(filename);
            var body = TextInTag("body", text);
            return body;

        }

        public string GetHtmlSubject(string filename)
        {
            var text = GetTemplateText(filename);
            var subject = TextInTag("subject", text);

            return subject;
        }
    }
}
