using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using NPOI.SS.UserModel;
using ICSharpCode.SharpZipLib.Zip.Compression;

namespace Einstein.Domain.Services
{
    internal class FastZipPack : IPack
    {
        public void Pack(string filename,string zipname)
        {
            if (File.Exists(filename))
            {
                
                FastZip zip = new FastZip();
            
                zip.CreateZip(zipname, filename, true, null);
                //using (ZipOutputStream stream = new ZipOutputStream(File.Create(Path.GetFileNameWithoutExtension(filename) + ".zip")))
                //{
                //    var entry = new ZipEntry(filename);
                //    stream.SetLevel(5);
                //    byte[] buffer = new byte[4096];
                //    stream.PutNextEntry(entry);
                //    var bytes = File.ReadAllBytes(filename);
                //    stream.Write(bytes, 0, bytes.Length);

                //    stream.Finish();
                //    stream.Close();
                //}

            }
        }

        public void UnPack(string filename)
        {
            FastZip zip = new FastZip();

            zip.ExtractZip(filename, Path.GetDirectoryName(filename), null);
        }
    }
}
