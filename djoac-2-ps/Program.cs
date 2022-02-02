using Microsoft.Extensions.Configuration;
using System;
using System.IO;




namespace djoac_2_ps
{
    class Program
    {
        private static IConfiguration _iconfiguration;
        static void Main(string[] args)
        {
            GetAppSettingFile();
            CreateFile();
            //PrintRecords();
        }

        static void GetAppSettingFile()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory
                .GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _iconfiguration = builder.Build();
        }

        static void CreateFile()
        {
            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;
            var rootpath = _iconfiguration.GetSection("Variables").GetSection("RootPath").Value;
            var filename = _iconfiguration.GetSection("Variables").GetSection("FileName").Value;
 
            try
            {
                ostrm = new FileStream(rootpath.ToString() + filename.ToString(), FileMode.Truncate, FileAccess.Write);
                writer = new StreamWriter(ostrm);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot open djoac.txt for writing");
                Console.WriteLine(ex.Message);
                return;
            }
            Console.SetOut(writer);
            //Console.WriteLine("This is a line of text");
            //Console.WriteLine("Everything written to Console.Write() or");
            //Console.WriteLine("Console.WriteLine() will be written to a file");
            DjoacDAL djoacDAL = new DjoacDAL(_iconfiguration);
            var listDjoacModel = djoacDAL.GetList();
            listDjoacModel.ForEach(item =>
            {
                var o1 = item.DtAgreed + item.EmpId;
                var o2 = item.PsCode + item.IdJid;                
                string sSpc1 = "";
                string sSpc2 = "";
                for (int i1 = o1.Length - 1; i1 < 21; i1++) 
                    {
                    sSpc1 += "\u0020";
                    }; ;
                for (int i2 = 0; i2 < 15; i2++)
                {
                    sSpc2 += "\u0020";
                }; ;
                string s = o1 + sSpc1 + o2 + sSpc2;
                 var oItem = s;
                Console.WriteLine(oItem);
            });
            Console.SetOut(oldOut);
            writer.Close();
            ostrm.Close();
            Console.WriteLine("Done");
            //Console.WriteLine("Press any key to stop.");
            //Console.ReadKey();

        }

        //static void PrintRecords()
        //{
        //    DjoacDAL djoacDAL = new DjoacDAL(_iconfiguration);
        //    var listDjoacModel = djoacDAL.GetList();
        //    listDjoacModel.ForEach(item =>
        //    {
        //        var oItem = item.DtAgreed + "," + item.EmpId + "," + item.PsCode + "," + item.IdJid;
        //        Console.WriteLine(oItem);
        //    });
        //    Console.WriteLine("Press any key to stop.");
        //    Console.ReadKey();

        //}
    }
}
