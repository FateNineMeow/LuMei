using System.Collections.Generic;
using System.IO;

namespace LuMei.FileLibrary.BinFile
{
    public class BinFileEntry
    {
        public uint Length;

        public uint Unknown;

        public BinFileProperty Prop;

        public List<BinFileValue> Values;

        public BinFilePropertiesTable PropertiesTable;

        public BinFileEntry(ref BinaryReader br, ref BinFilePropertiesTable propertiesTable)
		{
			this.Values = new List<BinFileValue>();
			this.PropertiesTable = propertiesTable;
			this.Unknown = br.ReadUInt32();
		}

        public void ReadEntry(ref BinaryReader br)
        {
            this.Length = br.ReadUInt32();
            this.Prop = this.PropertiesTable.GetProp(br.ReadUInt32());
            ushort num = br.ReadUInt16();
            checked
            {
                int num2 = (int)(num - 1);
                for (int i = 0; i <= num2; i++)
                {
                    List<BinFileValue> arg_50_0 = this.Values;
                    BinFileValueType arg_4B_1 = (BinFileValueType)255;
                    object obj = this;
                    arg_50_0.Add(new BinFileValue(ref br, arg_4B_1, ref obj, ref this.PropertiesTable));
                }
            }
        }

        public void UpdateSizes()
        {
            uint num = 0u;
            checked
            {
                try
                {
                    List<BinFileValue>.Enumerator enumerator = this.Values.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        BinFileValue current = enumerator.Current;
                        num += current.UpdateSizes();
                    }
                }
                finally
                {
                }
                this.Length = (uint)(unchecked((ulong)num) + 4uL + 2uL);
            }
        }

        public void Write(ref BinaryWriter bw)
        {
            bw.Write(this.Length);
            bw.Write(this.Prop.Hash);
            bw.Write(checked((ushort)this.Values.Count));
            try
            {
                List<BinFileValue>.Enumerator enumerator = this.Values.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    BinFileValue current = enumerator.Current;
                    current.Write(ref bw, true);
                }
            }
            finally
            {
            }
        }
    }
}
