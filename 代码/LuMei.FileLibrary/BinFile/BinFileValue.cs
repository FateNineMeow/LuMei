using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace LuMei.FileLibrary.BinFile
{
    public class BinFileValue
    {
        public BinFileProperty Prop;

        public BinFileValueType ValueType;

        public object Value;

        private bool bool_0;

        public object Parent;

        public BinFilePropertiesTable PropertiesTable;

        public BinFileValue()
		{
		}

        public BinFileValue(ref BinaryReader br, BinFileValueType readType, ref object parent, ref BinFilePropertiesTable propertiesTable)
		{
			this.PropertiesTable = propertiesTable;
			this.Parent = RuntimeHelpers.GetObjectValue(parent);
			if (readType == (BinFileValueType)255)
			{
				this.bool_0 = true;
				this.Prop = this.PropertiesTable.GetProp(br.ReadUInt32());
				this.ValueType = (BinFileValueType)br.ReadByte();
			}
			else
			{
				this.bool_0 = false;
				this.ValueType = readType;
			}
			if (this.ValueType == BinFileValueType.StringValue)
			{
				ushort count = br.ReadUInt16();
				this.Value = new string(br.ReadChars((int)count));
			}
			else if (this.ValueType == BinFileValueType.SameTypeValuesList1)
			{
				this.Value = new BinFileValueList(ref br, this.ValueType, ref this.PropertiesTable);
			}
			else if (this.ValueType == BinFileValueType.SameTypeValuesList2)
			{
				this.Value = new BinFileValueList(ref br, this.ValueType, ref this.PropertiesTable);
			}
			else if (this.ValueType == BinFileValueType.ValuesList)
			{
				this.Value = new BinFileValueList(ref br, this.ValueType, ref this.PropertiesTable);
			}
			else if (this.ValueType == BinFileValueType.ValuesList2)
			{
				this.Value = new BinFileValueList(ref br, this.ValueType, ref this.PropertiesTable);
			}
			else if (this.ValueType == BinFileValueType.DoubleTypesValuesList)
			{
				this.Value = new BinFileValueList(ref br, this.ValueType, ref this.PropertiesTable);
			}
			else if (this.ValueType == BinFileValueType.FloatValue2)
			{
				this.Value = br.ReadSingle();
			}
			else if (this.ValueType == BinFileValueType.const_4)
			{
				this.Value = br.ReadUInt32();
			}
			else if (this.ValueType == BinFileValueType.const_3)
			{
				this.Value = br.ReadUInt32();
			}
			else if (this.ValueType == BinFileValueType.const_11)
			{
				this.Value = br.ReadUInt32();
			}
			else if (this.ValueType == BinFileValueType.BooleanValue)
			{
				this.Value = br.ReadBoolean();
			}
			else if (this.ValueType == BinFileValueType.ByteVector4_2)
			{
				this.Value = br.ReadBytes(4);
			}
			else if (this.ValueType == BinFileValueType.ByteVector4_1)
			{
				this.Value = br.ReadBytes(4);
			}
			else if (this.ValueType == BinFileValueType.ByteValue2)
			{
				this.Value = br.ReadByte();
			}
			else if (this.ValueType == BinFileValueType.FloatsVector2)
			{
				this.Value = new float[]
				{
					br.ReadSingle(),
					br.ReadSingle()
				};
			}
			else if (this.ValueType == BinFileValueType.FloatsVector4)
			{
				this.Value = new float[]
				{
					br.ReadSingle(),
					br.ReadSingle(),
					br.ReadSingle(),
					br.ReadSingle()
				};
			}
			else if (this.ValueType == BinFileValueType.FloatsVector3)
			{
				this.Value = new float[]
				{
					br.ReadSingle(),
					br.ReadSingle(),
					br.ReadSingle()
				};
			}
			else if (this.ValueType == BinFileValueType.UShortsVector3)
			{
				this.Value = new ushort[]
				{
					br.ReadUInt16(),
					br.ReadUInt16(),
					br.ReadUInt16()
				};
			}
			else
			{
				br = br;
			}
		}

        public void GetValues(ref List<object> listOfValues)
        {
            if (!(this.Value is BinFileValueList))
            {
                listOfValues.Add(this);
            }
            else
            {
                BinFileValueList binFileValueList = (BinFileValueList)this.Value;
                listOfValues.Add(binFileValueList);
                try
                {
                    List<BinFileValue>.Enumerator enumerator = binFileValueList.Entries.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        BinFileValue current = enumerator.Current;
                        current.GetValues(ref listOfValues);
                    }
                }
                finally
                {
                }
                listOfValues.Add(binFileValueList);
            }
        }

        public uint UpdateSizes()
        {
            uint num = 0u;
            if (this.bool_0)
            {
                num = 5u;
            }
            checked
            {
                uint result;
                if (this.ValueType == BinFileValueType.StringValue)
                {
                    string text = this.Value.ToString();
                    result = (uint)(unchecked((ulong)num) + (ulong)(unchecked((long)text.Length)) + 2uL);
                }
                else if (this.ValueType == BinFileValueType.SameTypeValuesList1)
                {
                    BinFileValueList binFileValueList = (BinFileValueList)this.Value;
                    result = Convert.ToUInt32(num+ Convert.ToUInt32(binFileValueList.UpdateSizes()));
                }
                else if (this.ValueType == BinFileValueType.SameTypeValuesList2)
                {
                    BinFileValueList binFileValueList2 = (BinFileValueList)this.Value;
                    result = Convert.ToUInt32(num+Convert.ToUInt32(binFileValueList2.UpdateSizes()));
                }
                else if (this.ValueType == BinFileValueType.ValuesList)
                {
                    BinFileValueList binFileValueList3 = (BinFileValueList)this.Value;
                    result = Convert.ToUInt32(num+ Convert.ToUInt32(binFileValueList3.UpdateSizes()));
                }
                else if (this.ValueType == BinFileValueType.ValuesList2)
                {
                    BinFileValueList binFileValueList4 = (BinFileValueList)this.Value;
                    result = Convert.ToUInt32(num+ Convert.ToUInt32(binFileValueList4.UpdateSizes()));
                }
                else if (this.ValueType == BinFileValueType.DoubleTypesValuesList)
                {
                    BinFileValueList binFileValueList5 = (BinFileValueList)this.Value;
                    result = Convert.ToUInt32(num+ Convert.ToUInt32(binFileValueList5.UpdateSizes()));
                }
                else if (this.ValueType == BinFileValueType.FloatValue2)
                {
                    result = (uint)(unchecked((ulong)num) + 4uL);
                }
                else if (this.ValueType == BinFileValueType.const_4)
                {
                    result = (uint)(unchecked((ulong)num) + 4uL);
                }
                else if (this.ValueType == BinFileValueType.const_3)
                {
                    result = (uint)(unchecked((ulong)num) + 4uL);
                }
                else if (this.ValueType == BinFileValueType.const_11)
                {
                    result = (uint)(unchecked((ulong)num) + 4uL);
                }
                else if (this.ValueType == BinFileValueType.BooleanValue)
                {
                    result = (uint)(unchecked((ulong)num) + 1uL);
                }
                else if (this.ValueType == BinFileValueType.ByteVector4_2)
                {
                    result = (uint)(unchecked((ulong)num) + 4uL);
                }
                else if (this.ValueType == BinFileValueType.ByteVector4_1)
                {
                    result = (uint)(unchecked((ulong)num) + 4uL);
                }
                else if (this.ValueType == BinFileValueType.ByteValue2)
                {
                    result = (uint)(unchecked((ulong)num) + 1uL);
                }
                else if (this.ValueType == BinFileValueType.FloatsVector2)
                {
                    result = (uint)(unchecked((ulong)num) + 8uL);
                }
                else if (this.ValueType == BinFileValueType.FloatsVector4)
                {
                    result = (uint)(unchecked((ulong)num) + 16uL);
                }
                else if (this.ValueType == BinFileValueType.FloatsVector3)
                {
                    result = (uint)(unchecked((ulong)num) + 12uL);
                }
                else if (this.ValueType == BinFileValueType.UShortsVector3)
                {
                    result = (uint)(unchecked((ulong)num) + 6uL);
                }
                else
                {
                    result = 0u;
                }
                return result;
            }
        }

        public void Write(ref BinaryWriter bw, bool writeType)
        {
            if (writeType)
            {
                bw.Write(this.Prop.Hash);
                bw.Write((byte)this.ValueType);
            }
            checked
            {
                if (this.ValueType == BinFileValueType.StringValue)
                {
                    string text = Convert.ToString(this.Value);
                    bw.Write((ushort)text.Length);
                    bw.Write(Encoding.ASCII.GetBytes(text));
                }
                else if (this.ValueType == BinFileValueType.SameTypeValuesList1)
                {
                    BinFileValueList binFileValueList = (BinFileValueList)this.Value;
                    binFileValueList.Write(ref bw);
                }
                else if (this.ValueType == BinFileValueType.SameTypeValuesList2)
                {
                    BinFileValueList binFileValueList2 = (BinFileValueList)this.Value;
                    binFileValueList2.Write(ref bw);
                }
                else if (this.ValueType == BinFileValueType.ValuesList)
                {
                    BinFileValueList binFileValueList3 = (BinFileValueList)this.Value;
                    binFileValueList3.Write(ref bw);
                }
                else if (this.ValueType == BinFileValueType.ValuesList2)
                {
                    BinFileValueList binFileValueList4 = (BinFileValueList)this.Value;
                    binFileValueList4.Write(ref bw);
                }
                else if (this.ValueType == BinFileValueType.DoubleTypesValuesList)
                {
                    BinFileValueList binFileValueList5 = (BinFileValueList)this.Value;
                    binFileValueList5.Write(ref bw);
                }
                else if (this.ValueType == BinFileValueType.FloatValue2)
                {
                    float value = Convert.ToSingle(this.Value);
                    bw.Write(value);
                }
                else if (this.ValueType == BinFileValueType.const_4)
                {
                    uint value2 = Convert.ToUInt32(this.Value);
                    bw.Write(value2);
                }
                else if (this.ValueType == BinFileValueType.const_3)
                {
                    uint value3 = Convert.ToUInt32(this.Value);
                    bw.Write(value3);
                }
                else if (this.ValueType == BinFileValueType.const_11)
                {
                    uint value4 = Convert.ToUInt32(this.Value);
                    
                    bw.Write(value4);
                }
                else if (this.ValueType == BinFileValueType.BooleanValue)
                {
                    bool value5 =Convert.ToBoolean(this.Value);
                    bw.Write(value5);
                }
                else if (this.ValueType == BinFileValueType.ByteVector4_2)
                {
                    byte[] buffer = (byte[])this.Value;
                    bw.Write(buffer);
                }
                else if (this.ValueType == BinFileValueType.ByteVector4_1)
                {
                    byte[] buffer2 = (byte[])this.Value;
                    bw.Write(buffer2);
                }
                else if (this.ValueType == BinFileValueType.ByteValue2)
                {
                    byte value6 = Convert.ToByte(this.Value);
                    bw.Write(value6);
                }
                else if (this.ValueType == BinFileValueType.FloatsVector2)
                {
                    float[] array = (float[])this.Value;
                    float[] array2 = array;
                    for (int i = 0; i < array2.Length; i++)
                    {
                        float value7 = array2[i];
                        bw.Write(value7);
                    }
                }
                else if (this.ValueType == BinFileValueType.FloatsVector4)
                {
                    float[] array3 = (float[])this.Value;
                    float[] array4 = array3;
                    for (int j = 0; j < array4.Length; j++)
                    {
                        float value8 = array4[j];
                        bw.Write(value8);
                    }
                }
                else if (this.ValueType == BinFileValueType.FloatsVector3)
                {
                    float[] array5 = (float[])this.Value;
                    float[] array6 = array5;
                    for (int k = 0; k < array6.Length; k++)
                    {
                        float value9 = array6[k];
                        bw.Write(value9);
                    }
                }
                else if (this.ValueType == BinFileValueType.UShortsVector3)
                {
                    ushort[] array7 = (ushort[])this.Value;
                    ushort[] array8 = array7;
                    for (int l = 0; l < array8.Length; l++)
                    {
                        ushort value10 = array8[l];
                        bw.Write(value10);
                    }
                }
            }
        }
    }
}
