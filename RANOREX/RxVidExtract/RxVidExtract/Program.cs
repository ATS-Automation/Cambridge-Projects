using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;

namespace RxVidExtract
{
    class Program
    {
        static void Main(string[] args)
        {
            var RanorexZipFileName = @args[0];
           //   var RanorexZipFileName = @"C:\Users\anpatel\Documents\2019\RanorexVideoFolder\TestforJenkinsBuild14.rxzlog";
            
            var UnzipFolder = RanorexZipFileName.TrimEnd(".rxzlog".ToCharArray());
            var VidFileName = UnzipFolder.Substring(RanorexZipFileName.LastIndexOf('\\') + 1);
            try
            {

               if (Directory.Exists(UnzipFolder))
                {
                    Directory.Delete(UnzipFolder, true);
                }
                ZipFile.ExtractToDirectory(RanorexZipFileName, UnzipFolder);
                var vidDirectory = Directory.GetDirectories(UnzipFolder, "videos*");
                Console.WriteLine(vidDirectory[0]);
                var dirInfo = new DirectoryInfo(vidDirectory[0]);
                var fileInfo = dirInfo.GetFiles("*.mkv");
                var filecount = fileInfo.Count();
                int i = 1;
                foreach (var file in fileInfo)
                {
                    File.Move(file.FullName, dirInfo.FullName + "\\" + VidFileName + "-" + i.ToString() + ".mkv");
                    i++;

                }
                    

                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred: " + ex.Message);
            }
            
        }
    }
}
