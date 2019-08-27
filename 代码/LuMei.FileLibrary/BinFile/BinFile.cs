using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LuMei.FileLibrary.BinFile
{
    public class BinFile
    {
        public uint Version;

        public string FilePath;

        public List<BinFileEntry> Entries = new List<BinFileEntry>();

        public BinFilePropertiesTable PropertiesTable = new BinFilePropertiesTable();
        public BinFile(string filepath)
        {
            using (BinaryReader binaryReader = new BinaryReader(File.Open(filepath, FileMode.Open)))
            {
                FilePath = filepath;
                var prop=new string(binaryReader.ReadChars(4));                
                if (prop.ToLower()!="prop")
                {
                    throw new Exception("此文件不是bin文件！");
                }
                Version=binaryReader.ReadUInt32();
                uint num = binaryReader.ReadUInt32();
                long num2 = (long)(unchecked((ulong)num) - 1uL);
					for (long num3 = 0L; num3 <= num2; num3 += 1L)
					{
						List<BinFileEntry> list = this.Entries;
						BinaryReader binaryReader2 = binaryReader;
						list.Add(new BinFileEntry(ref binaryReader2, ref this.PropertiesTable));
					}
					try
					{
						List<BinFileEntry>.Enumerator enumerator = this.Entries.GetEnumerator();
						while (enumerator.MoveNext())
						{
							BinFileEntry current = enumerator.Current;
							BinaryReader binaryReader2 = binaryReader;
							current.ReadEntry(ref binaryReader2);
						}
					}
					finally
					{
					}
            }
        }
        public List<object> EnumerateValues()
        {
            List<object> list = new List<object>();
            try
            {
                List<BinFileEntry>.Enumerator enumerator = this.Entries.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    BinFileEntry current = enumerator.Current;
                    list.Add(current);
                    try
                    {
                        List<BinFileValue>.Enumerator enumerator2 = current.Values.GetEnumerator();
                        while (enumerator2.MoveNext())
                        {
                            BinFileValue current2 = enumerator2.Current;
                            current2.GetValues(ref list);
                        }
                    }
                    finally
                    {
                        ((IDisposable)enumerator).Dispose();
                    }
                    list.Add(current);
                }
            }
            finally
            {
                ((IDisposable)list).Dispose();
            }
            return list;
        }

        public void UpdateSizes()
        {
            try
            {
                List<BinFileEntry>.Enumerator enumerator = this.Entries.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    BinFileEntry current = enumerator.Current;
                    current.UpdateSizes();
                }
            }
            finally
            {
            }
        }

        public void Save(string filepath)
        {
            this.UpdateSizes();
            using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(filepath, FileMode.Create)))
            {
                binaryWriter.Write(Encoding.ASCII.GetBytes("PROP"));
                binaryWriter.Write(this.Version);
                binaryWriter.Write(checked((uint)this.Entries.Count));
                try
                {
                    List<BinFileEntry>.Enumerator enumerator = this.Entries.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        BinFileEntry current = enumerator.Current;
                        binaryWriter.Write(current.Unknown);
                    }
                }
                finally
                {
                }
                try
                {
                    List<BinFileEntry>.Enumerator enumerator2 = this.Entries.GetEnumerator();
                    while (enumerator2.MoveNext())
                    {
                        BinFileEntry current2 = enumerator2.Current;
                        BinaryWriter binaryWriter2 = binaryWriter;
                        current2.Write(ref binaryWriter2);
                    }
                }
                finally
                {
                }
            }
        }
    }
}
