using System;
using System.Collections.Generic;
using System.IO;
namespace LuMei.FileLibrary.SknVersion
{
	public class SknFile
	{
        public class SknMaterial
		{
			public char[] Name;
			public int StartVertex;
			public int NumVertices;
			public int StartIndex;
			public int NumIndices;
			public SknMaterial(ref BinaryReader br)
			{
				this.Name = br.ReadChars(64);
				checked
				{
					this.StartVertex = (int)br.ReadUInt32();
					this.NumVertices = (int)br.ReadUInt32();
					this.StartIndex = (int)br.ReadUInt32();
					this.NumIndices = (int)br.ReadUInt32();
				}
			}
		}
        public class SknData
		{
			public class Vertex
			{
				public float[] Position;
				public uint BoneIndex;
				public float[] Weights;
				public float[] Normal;
				public float[] UV;
				public Vertex(ref BinaryReader br)
				{
					this.Position = new float[3];
					this.Weights = new float[4];
					this.Normal = new float[3];
					this.UV = new float[2];
					int num = 0;
					checked
					{
						do
						{
							this.Position[num] = br.ReadSingle();
							num++;
						}
						while (num <= 2);
						this.BoneIndex = br.ReadUInt32();
						int num2 = 0;
						do
						{
							this.Weights[num2] = br.ReadSingle();
							num2++;
						}
						while (num2 <= 3);
						int num3 = 0;
						do
						{
							this.Normal[num3] = br.ReadSingle();
							num3++;
						}
						while (num3 <= 2);
						int num4 = 0;
						do
						{
							this.UV[num4] = br.ReadSingle();
							num4++;
						}
						while (num4 <= 1);
					}
				}
				public Vertex()
				{
					this.Position = new float[3];
					this.Weights = new float[4];
					this.Normal = new float[3];
					this.UV = new float[2];
				}
			}
			public uint Unknown;
			public byte[] UnknownBytes;
			public List<SknFile.SknData.Vertex> Vertices;
			public List<short> Indices;
			public SknData(ref BinaryReader br, ref SknFile SknFile)
			{
				this.Vertices = new List<SknFile.SknData.Vertex>();
				this.Indices = new List<short>();
				bool flag = SknFile.Version == 4;
				checked
				{
					if (flag)
					{
						this.Unknown = (uint)br.ReadInt32();
					}
					uint num = br.ReadUInt32();
					uint num2 = br.ReadUInt32();
					bool flag2 = SknFile.Version == 4;
					if (flag2)
					{
						this.UnknownBytes = br.ReadBytes(48);
					}
					long num3 = (long)(unchecked((ulong)num) - 1uL);
					for (long num4 = 0L; num4 <= num3; num4 += 1L)
					{
						this.Indices.Add(br.ReadInt16());
					}
					long num5 = (long)(unchecked((ulong)num2) - 1uL);
					for (long num6 = 0L; num6 <= num5; num6 += 1L)
					{
						this.Vertices.Add(new SknFile.SknData.Vertex(ref br));
					}
				}
			}
		}
		public int Magic;
		public short Version;
		public short Unknown;
        private List<SknFile.SknMaterial> Materials;
		public SknFile.SknData Data;
		public SknFile(string FilePath)
		{
			this.Materials = new List<SknFile.SknMaterial>();
			checked
			{
				using (BinaryReader binaryReader = new BinaryReader(File.Open(FilePath, FileMode.Open)))
				{
					this.Magic = binaryReader.ReadInt32();
					bool flag = this.Magic != 1122867;
					if (flag)
					{
						throw new Exception("This file is not a valid SKN file.");
					}
					this.Version = (short)binaryReader.ReadUInt16();
					this.Unknown = (short)binaryReader.ReadUInt16();
					long num = (long)(unchecked((ulong)binaryReader.ReadUInt32()) - 1uL);
					BinaryReader binaryReader2;
					for (long num2 = 0L; num2 <= num; num2 += 1L)
					{
						List<SknFile.SknMaterial> arg_8B_0 = this.Materials;
						binaryReader2 = binaryReader;
						SknFile.SknMaterial item = new SknFile.SknMaterial(ref binaryReader2);
						arg_8B_0.Add(item);
					}
					binaryReader2 = binaryReader;
					SknFile sknFile = this;
					SknFile.SknData data = new SknFile.SknData(ref binaryReader2, ref sknFile);
					this.Data = data;
				}
			}
		}
		public void SaveFile(string FilePath)
		{
			checked
			{
				using (BinaryWriter binaryWriter = new BinaryWriter(new FileStream(FilePath, FileMode.Create)))
				{
					binaryWriter.Write(this.Magic);
					binaryWriter.Write(this.Version);
					binaryWriter.Write(this.Unknown);
					binaryWriter.Write(this.Materials.Count);
					try
					{
						List<SknFile.SknMaterial>.Enumerator enumerator = this.Materials.GetEnumerator();
						while (enumerator.MoveNext())
						{
							SknFile.SknMaterial current = enumerator.Current;
							binaryWriter.Write(current.Name);
							binaryWriter.Write(current.StartVertex);
							binaryWriter.Write(current.NumVertices);
							binaryWriter.Write(current.StartIndex);
							binaryWriter.Write(current.NumIndices);
						}
					}
					finally
					{
					}
					bool flag = this.Version == 4;
					if (flag)
					{
						binaryWriter.Write(this.Data.Unknown);
					}
					binaryWriter.Write(this.Data.Indices.Count);
					binaryWriter.Write(this.Data.Vertices.Count);
					bool flag2 = this.Version == 4;
					if (flag2)
					{
						binaryWriter.Write(this.Data.UnknownBytes);
					}
					try
					{
						List<short>.Enumerator enumerator2 = this.Data.Indices.GetEnumerator();
						while (enumerator2.MoveNext())
						{
							short current2 = enumerator2.Current;
							binaryWriter.Write(current2);
						}
					}
					finally
					{
					}
					try
					{
						List<SknFile.SknData.Vertex>.Enumerator enumerator3 = this.Data.Vertices.GetEnumerator();
						while (enumerator3.MoveNext())
						{
							SknFile.SknData.Vertex current3 = enumerator3.Current;
							int num = 0;
							do
							{
								binaryWriter.Write(current3.Position[num]);
								num++;
							}
							while (num <= 2);
							binaryWriter.Write(current3.BoneIndex);
							int num2 = 0;
							do
							{
								binaryWriter.Write(current3.Weights[num2]);
								num2++;
							}
							while (num2 <= 3);
							int num3 = 0;
							do
							{
								binaryWriter.Write(current3.Normal[num3]);
								num3++;
							}
							while (num3 <= 2);
							int num4 = 0;
							do
							{
								binaryWriter.Write(current3.UV[num4]);
								num4++;
							}
							while (num4 <= 1);
						}
					}
					finally
					{
					}
					binaryWriter.Write(0);
					binaryWriter.Write(0);
					binaryWriter.Write(0);
				}
			}
		}
	}
}
