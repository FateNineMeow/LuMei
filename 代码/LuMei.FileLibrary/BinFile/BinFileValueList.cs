using System;
using System.Collections.Generic;
using System.IO;

namespace LuMei.FileLibrary.BinFile
{
    public class BinFileValueList
    {
        public List<BinFileValue> Entries;

        public BinFileValueType ListType;

        public BinFileProperty Prop;

        public uint EntriesSize;

        public BinFileValueType EntriesType;

        public BinFileValueType[] EntriesTypes;

        public BinFilePropertiesTable PropertiesTable;

        public BinFileValueList()
		{
			this.Entries = new List<BinFileValue>();
		}

        public BinFileValueList(ref BinaryReader br, ValueType listType, ref BinFilePropertiesTable propertiesTable)
		{
			this.Entries = new List<BinFileValue>();
			this.PropertiesTable = propertiesTable;
			this.ListType = ((listType != null) ? ((BinFileValueType)listType) : ((BinFileValueType)0));
			checked
			{
				if (this.ListType == BinFileValueType.SameTypeValuesList1)
				{
					this.EntriesType = (BinFileValueType)br.ReadByte();
					this.EntriesSize = br.ReadUInt32();
					uint num = br.ReadUInt32();
					long num2 = (long)(unchecked((ulong)num) - 1uL);
					for (long num3 = 0L; num3 <= num2; num3 += 1L)
					{
						List<BinFileValue> arg_8E_0 = this.Entries;
						BinFileValueType arg_89_1 = this.EntriesType;
						object obj = this;
						arg_8E_0.Add(new BinFileValue(ref br, arg_89_1, ref obj, ref propertiesTable));
					}
				}
				else if (this.ListType == BinFileValueType.SameTypeValuesList2)
				{
					this.EntriesType = (BinFileValueType)br.ReadByte();
					byte b = br.ReadByte();
					int num4 = (int)(b - 1);
					for (int i = 0; i <= num4; i++)
					{
						List<BinFileValue> arg_EA_0 = this.Entries;
						BinFileValueType arg_E5_1 = this.EntriesType;
						object obj = this;
						arg_EA_0.Add(new BinFileValue(ref br, arg_E5_1, ref obj, ref propertiesTable));
					}
				}
				else if (this.ListType == BinFileValueType.ValuesList)
				{
					this.Prop = this.PropertiesTable.GetProp(br.ReadUInt32());
					this.EntriesSize = br.ReadUInt32();
					this.EntriesType = (BinFileValueType)255;
					ushort num5 = br.ReadUInt16();
					int num6 = (int)(num5 - 1);
					for (int j = 0; j <= num6; j++)
					{
						List<BinFileValue> arg_164_0 = this.Entries;
						BinFileValueType arg_15F_1 = this.EntriesType;
						object obj = this;
						arg_164_0.Add(new BinFileValue(ref br, arg_15F_1, ref obj, ref propertiesTable));
					}
				}
				else if (this.ListType == BinFileValueType.ValuesList2)
				{
					this.Prop = this.PropertiesTable.GetProp(br.ReadUInt32());
					this.EntriesSize = br.ReadUInt32();
					this.EntriesType = (BinFileValueType)255;
					ushort num7 = br.ReadUInt16();
					int num8 = (int)(num7 - 1);
					for (int k = 0; k <= num8; k++)
					{
						List<BinFileValue> arg_1DE_0 = this.Entries;
						BinFileValueType arg_1D9_1 = this.EntriesType;
						object obj = this;
						arg_1DE_0.Add(new BinFileValue(ref br, arg_1D9_1, ref obj, ref propertiesTable));
					}
				}
				else if (this.ListType == BinFileValueType.DoubleTypesValuesList)
				{
					this.EntriesTypes = new BinFileValueType[]
					{
						(BinFileValueType)br.ReadByte(),
						(BinFileValueType)br.ReadByte()
					};
					this.EntriesSize = br.ReadUInt32();
					uint num9 = br.ReadUInt32();
					long num10 = (long)(unchecked((ulong)num9) - 1uL);
					for (long num11 = 0L; num11 <= num10; num11 += 1L)
					{
						List<BinFileValue> arg_261_0 = this.Entries;
						BinFileValueType arg_25C_1 = this.EntriesTypes[0];
						object obj = this;
						arg_261_0.Add(new BinFileValue(ref br, arg_25C_1, ref obj, ref propertiesTable));
						List<BinFileValue> arg_280_0 = this.Entries;
						BinFileValueType arg_27B_1 = this.EntriesTypes[1];
						obj = this;
						arg_280_0.Add(new BinFileValue(ref br, arg_27B_1, ref obj, ref propertiesTable));
					}
				}
			}
		}

        public object UpdateSizes()
        {
            checked
            {
                object result;
                if (this.ListType == BinFileValueType.SameTypeValuesList1)
                {
                    uint num = 0u;
                    try
                    {
                        List<BinFileValue>.Enumerator enumerator = this.Entries.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            BinFileValue current = enumerator.Current;
                            num += current.UpdateSizes();
                        }
                    }
                    finally
                    {
                    }
                    this.EntriesSize = (uint)(unchecked((ulong)num) + 4uL);
                    result = (long)(9uL + unchecked((ulong)num));
                }
                else if (this.ListType == BinFileValueType.SameTypeValuesList2)
                {
                    uint num2 = 0u;
                    try
                    {
                        List<BinFileValue>.Enumerator enumerator2 = this.Entries.GetEnumerator();
                        while (enumerator2.MoveNext())
                        {
                            BinFileValue current2 = enumerator2.Current;
                            num2 += current2.UpdateSizes();
                        }
                    }
                    finally
                    {
                    }
                    result = (long)(2uL + unchecked((ulong)num2));
                }
                else if (this.ListType == BinFileValueType.ValuesList)
                {
                    uint num3 = 0u;
                    try
                    {
                        List<BinFileValue>.Enumerator enumerator3 = this.Entries.GetEnumerator();
                        while (enumerator3.MoveNext())
                        {
                            BinFileValue current3 = enumerator3.Current;
                            num3 += current3.UpdateSizes();
                        }
                    }
                    finally
                    {
                    }
                    if (unchecked((ulong)this.EntriesSize) != unchecked((ulong)num3) + 2uL)
                    {
                        num3 = num3;
                    }
                    this.EntriesSize = (uint)(unchecked((ulong)num3) + 2uL);
                    result = (long)(10uL + unchecked((ulong)num3));
                }
                else if (this.ListType == BinFileValueType.ValuesList2)
                {
                    uint num4 = 0u;
                    try
                    {
                        List<BinFileValue>.Enumerator enumerator4 = this.Entries.GetEnumerator();
                        while (enumerator4.MoveNext())
                        {
                            BinFileValue current4 = enumerator4.Current;
                            num4 += current4.UpdateSizes();
                        }
                    }
                    finally
                    {
                    }
                    this.EntriesSize = (uint)(unchecked((ulong)num4) + 2uL);
                    result = (long)(10uL + unchecked((ulong)num4));
                }
                else if (this.ListType == BinFileValueType.DoubleTypesValuesList)
                {
                    uint num5 = 0u;
                    try
                    {
                        List<BinFileValue>.Enumerator enumerator5 = this.Entries.GetEnumerator();
                        while (enumerator5.MoveNext())
                        {
                            BinFileValue current5 = enumerator5.Current;
                            num5 += current5.UpdateSizes();
                        }
                    }
                    finally
                    {
                    }
                    this.EntriesSize = (uint)(unchecked((ulong)num5) + 4uL);
                    result = (long)(10uL + unchecked((ulong)num5));
                }
                else
                {
                    result = 0;
                }
                return result;
            }
        }
        
        public void Write(ref BinaryWriter bw)
        {
            checked
            {
                if (this.ListType == BinFileValueType.SameTypeValuesList1)
                {
                    bw.Write((byte)this.EntriesType);
                    bw.Write(this.EntriesSize);
                    bw.Write((uint)this.Entries.Count);
                    try
                    {
                        List<BinFileValue>.Enumerator enumerator = this.Entries.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            BinFileValue current = enumerator.Current;
                            current.Write(ref bw, false);
                        }
                        return;
                    }
                    finally
                    {
                    }
                }
                if (this.ListType == BinFileValueType.SameTypeValuesList2)
                {
                    bw.Write((byte)this.EntriesType);
                    bw.Write((byte)this.Entries.Count);
                    try
                    {
                        List<BinFileValue>.Enumerator enumerator2 = this.Entries.GetEnumerator();
                        while (enumerator2.MoveNext())
                        {
                            BinFileValue current2 = enumerator2.Current;
                            current2.Write(ref bw, false);
                        }
                        return;
                    }
                    finally
                    {
                    }
                }
                if (this.ListType == BinFileValueType.ValuesList)
                {
                    bw.Write(this.Prop.Hash);
                    bw.Write(this.EntriesSize);
                    bw.Write((ushort)this.Entries.Count);
                    try
                    {
                        List<BinFileValue>.Enumerator enumerator3 = this.Entries.GetEnumerator();
                        while (enumerator3.MoveNext())
                        {
                            BinFileValue current3 = enumerator3.Current;
                            current3.Write(ref bw, true);
                        }
                        return;
                    }
                    finally
                    {
                    }
                }
                if (this.ListType == BinFileValueType.ValuesList2)
                {
                    bw.Write(this.Prop.Hash);
                    bw.Write(this.EntriesSize);
                    bw.Write((ushort)this.Entries.Count);
                    try
                    {
                        List<BinFileValue>.Enumerator enumerator4 = this.Entries.GetEnumerator();
                        while (enumerator4.MoveNext())
                        {
                            BinFileValue current4 = enumerator4.Current;
                            current4.Write(ref bw, true);
                        }
                        return;
                    }
                    finally
                    {
                    }
                }
                if (this.ListType == BinFileValueType.DoubleTypesValuesList)
                {
                    bw.Write((byte)this.EntriesTypes[0]);
                    bw.Write((byte)this.EntriesTypes[1]);
                    bw.Write(this.EntriesSize);
                    bw.Write((uint)Math.Round((double)this.Entries.Count / 2.0));
                    try
                    {
                        List<BinFileValue>.Enumerator enumerator5 = this.Entries.GetEnumerator();
                        while (enumerator5.MoveNext())
                        {
                            BinFileValue current5 = enumerator5.Current;
                            current5.Write(ref bw, false);
                        }
                    }
                    finally
                    {
                    }
                }
            }
        }
    }
}
