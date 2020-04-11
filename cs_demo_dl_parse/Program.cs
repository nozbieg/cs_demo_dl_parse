using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using Ionic.Zip;
using Microsoft.VisualBasic.FileIO;

namespace cs_demo_dl_parse
{
    class Program
    {
        

        private static string[] GetFileNames(string path, string filter)
        {
            string[] files = Directory.GetFiles(path, filter);
            for (int i = 0; i < files.Length; i++)
                files[i] = Path.GetFileName(files[i]);
            return files;
        }



        static void Main(string[] args)
        {




            using (TextFieldParser parser = new TextFieldParser(@"csgo.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] fields = parser.ReadFields();
                   

                    //for (int i=0; i < 9; i++)
                    //{
                        
                    //    Console.Write(fields[i] + " " );
                    //}
                    //getting urls and file names
                    Console.WriteLine();
                    string url = fields[1];
                    string fileName = "demo" + fields[2] + ".rar";
                    string dirName = "demo" + fields[2];
                    string directory = "C:\\Users\\Norbert\\source\\repos\\cs_demo_dl_parse\\cs_demo_dl_parse\\bin\\Debug\\" + fileName;
                    Console.WriteLine(url + " " + fileName + " " + directory);

                    //downloading file
                    try
                    {
                        using (WebClient webClient = new WebClient())
                        {
                            webClient.DownloadFile(url, fileName);
                            Console.WriteLine("Download complete");
                        }
                    }
                    catch (WebException wex)
                    {
                        Console.WriteLine(wex);
                    }



                    //unpacking demo rar to folder
                    string SourceFile = @"C:\Users\Norbert\source\repos\cs_demo_dl_parse\cs_demo_dl_parse\bin\Debug\"+fileName;
                    string DestinationPath = @"C:\Users\Norbert\source\repos\cs_demo_dl_parse\cs_demo_dl_parse\bin\Debug\" + dirName+ @"\";
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo.FileName = @"F:\Program Files\WinRAR\WinRAR.exe";
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    process.EnableRaisingEvents = false;
                    process.StartInfo.Arguments = string.Format("x -o+ \"{0}\" \"{1}\"", SourceFile, DestinationPath);
                    process.Start();
                    process.WaitForExit();
                    Console.WriteLine("Unpack complete");
                    //deleting demo rar
                    File.Delete(SourceFile);

                    //getting names of files
                    try
                    {
                       

                        //string[] filesArray = Directory.GetFiles(DestinationPath);
                        //for (int i = 0; i < filesArray.Length; i++)
                        //{
                        //    Console.WriteLine(filesArray[i]);
                        //}
                        string[] demFiles = GetFileNames(DestinationPath, "*.dem");
                        for (int i = 0; i < demFiles.Length; i++)
                        {
                            Console.WriteLine(DestinationPath + demFiles[i]);

                            //ODPALAMY TUTAJ SKRYPT DLA KAŻDEGO PLIKU
                            Console.WriteLine("Press enter to run goScript");
                            Console.ReadLine();

                            //Delete dem file
                            Console.WriteLine("File " + demFiles[i] + " deleted");
                            File.Delete(DestinationPath + demFiles[i]);

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }


                    Console.WriteLine();
                    Console.ReadLine();
                }
            }



            Console.ReadLine();
        }
    }
}
