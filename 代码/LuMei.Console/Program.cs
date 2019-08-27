using LuMei.Data;
using System.IO;
using LuMei.Helper;

namespace LuMei.Console
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    SkinImport import = new SkinImport();
        //    ChampionsService champions = new ChampionsService();
        //    _import.CreateLoadZip(@"F:\Work Out\360\Sync\Other\Model\MMD\Christmas\Christmas pack part 1 (Crypton).png");
        //    var zip = @"D:\Haibara\Desktop\雷霆剑圣.zip";
        //    _import.SkinList(zip);
        //    var str = Zip.GetZipNote(zip);
        //    System.Console.WriteLine(str.SkinName + str.HeroName + str.Author + str.GameVersion);
        //    System.Console.WriteLine(zip);
        //    System.Console.ReadLine();
        //    Zip.SetZipInfo(zip, "重写注释！\r\n" + str);
        //    System.Console.WriteLine(Zip.GetZipInfo(zip));
        //    System.Console.ReadLine();
        //}
        static void Main(string[] args)
        {
            var file = File.ReadAllText(Soft.SoftPath + "\\MasterYi.bin");
            var bin = new BinFile();
            bin.id = file.Substring(0, 4);
            bin.version = file;
        }
    }
    public class BinFile
    {
        public string id { get; set; }
        public string version { get; set; }
        public string entriesCounter { get; set; }
        public string types { get; set; }
        public string properties { get; set; }
    }
}
