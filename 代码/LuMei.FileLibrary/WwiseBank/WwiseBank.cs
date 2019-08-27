using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Wooxy.LeagueOfLegends.FilesLibrary.WWiseBank
{
	public class WwiseBank
	{
		public enum SoundBankType
		{
			BNKSoundbank,
			WPKSoundbank
		}

		public class BNKBank
		{
			public class AudioFile
			{
				public uint FileDataOffset;

				public uint FileDataSize;

				public double DurationInSeconds;

				public string Name;

				public AudioFile(uint FileDataOffset, uint FileDataSize, uint DurationInSeconds, string Name)
				{
					this.FileDataOffset = FileDataOffset;
					this.FileDataSize = FileDataSize;
					this.DurationInSeconds = DurationInSeconds;
					this.Name = Name;
				}

				public void ExtractBytes(string FileLocation, string ExtractPath)
				{
					using (BinaryReader binaryReader = new BinaryReader(new FileStream(FileLocation, FileMode.Open)))
					{
						binaryReader.BaseStream.Seek((long)((ulong)this.FileDataOffset), SeekOrigin.Begin);
						File.WriteAllBytes(ExtractPath, binaryReader.ReadBytes(checked((int)this.FileDataSize)));
					}
				}
			}

			private uint uint_0;

			public List<WwiseBank.BNKBank.AudioFile> AudioFiles;

			public string FileLocation;

			public BNKBank(string FileLocation)
			{
				this.AudioFiles = new List<WwiseBank.BNKBank.AudioFile>();
				this.FileLocation = FileLocation;
				checked
				{
					using (BinaryReader binaryReader = new BinaryReader(new FileStream(FileLocation, FileMode.Open)))
					{
						int num = 1;
						while (binaryReader.BaseStream.Length - binaryReader.BaseStream.Position >= 1L)
						{
							string @string = Encoding.ASCII.GetString(binaryReader.ReadBytes(1));
							if (string.Compare(@string.ToString(), "R",false) == 0 && string.Compare(Encoding.ASCII.GetString(binaryReader.ReadBytes(3)), "IFF", false) == 0)
							{
								long num2 = binaryReader.BaseStream.Position - 4L;
								long num3 = (long)(unchecked((ulong)binaryReader.ReadUInt32()) + 8uL);
								binaryReader.ReadBytes(16);
								uint num4 = binaryReader.ReadUInt32();
								double num5 = binaryReader.ReadUInt32() / num4;
								this.AudioFiles.Add(new WwiseBank.BNKBank.AudioFile((uint)num2, (uint)num3, (uint)Math.Round((double)(num3 - 44L) / unchecked(num5 * num4)), "File_" + Convert.ToString(num)));
								num++;
								binaryReader.BaseStream.Seek(num2 + num3, SeekOrigin.Begin);
							}
						}
					}
				}
			}
		}

		public class WPKBank
		{
			public class AudioFile
			{
				public uint FileDataOffset;

				public uint FileDataSize;

				public uint FileOffset;

				public double DurationInSeconds;

				public string Name;

				public AudioFile(uint FileOffset)
				{
					this.FileOffset = FileOffset;
				}

				public void ExtractBytes(string FileLocation, string ExtractPath)
				{
					using (BinaryReader binaryReader = new BinaryReader(new FileStream(FileLocation, FileMode.Open)))
					{
						binaryReader.BaseStream.Seek((long)((ulong)this.FileDataOffset), SeekOrigin.Begin);
						File.WriteAllBytes(ExtractPath, binaryReader.ReadBytes(checked((int)this.FileDataSize)));
					}
				}
			}

			private uint uint_0;

			public List<WwiseBank.WPKBank.AudioFile> AudioFiles;

			public string FileLocation;

			public WPKBank(string FileLocation)
			{
				this.AudioFiles = new List<WwiseBank.WPKBank.AudioFile>();
				this.FileLocation = FileLocation;
				checked
				{
					using (BinaryReader binaryReader = new BinaryReader(new FileStream(FileLocation, FileMode.Open)))
					{
						binaryReader.ReadBytes(8);
						this.uint_0 = binaryReader.ReadUInt32();
						long num = (long)(unchecked((ulong)this.uint_0) - 1uL);
						for (long num2 = 0L; num2 <= num; num2 += 1L)
						{
							this.AudioFiles.Add(new WwiseBank.WPKBank.AudioFile(binaryReader.ReadUInt32()));
						}
						try
						{
							List<WwiseBank.WPKBank.AudioFile>.Enumerator enumerator = this.AudioFiles.GetEnumerator();
							while (enumerator.MoveNext())
							{
								WwiseBank.WPKBank.AudioFile current = enumerator.Current;
								binaryReader.BaseStream.Seek((long)(unchecked((ulong)current.FileOffset)), SeekOrigin.Begin);
								current.FileDataOffset = binaryReader.ReadUInt32();
								current.FileDataSize = binaryReader.ReadUInt32();
								uint num3 = binaryReader.ReadUInt32();
								string text = "";
								long num4 = (long)(unchecked((ulong)num3) - 1uL);
								for (long num5 = 0L; num5 <= num4; num5 += 1L)
								{
									text += Convert.ToString(Encoding.ASCII.GetString(binaryReader.ReadBytes(2))[0]);
								}
								current.Name = text;
								binaryReader.BaseStream.Seek((long)(unchecked((ulong)current.FileDataOffset) + 24uL), SeekOrigin.Begin);
								uint num6 = binaryReader.ReadUInt32();
								double num7 = binaryReader.ReadUInt32() / num6;
								current.DurationInSeconds = (double)(unchecked((ulong)current.FileDataSize) - 44uL) / unchecked(num7 * num6);
							}
						}
						finally
						{
						}
					}
				}
			}
		}

		public WwiseBank.SoundBankType SoundBankTypeInfo;

		public WwiseBank.WPKBank WPKSoundBank;

		public WwiseBank.BNKBank BNKSoundBank;

		public WwiseBank(string FileLocation)
		{
			string left = "";
			using (BinaryReader binaryReader = new BinaryReader(new FileStream(FileLocation, FileMode.Open)))
			{
				left = Encoding.ASCII.GetString(binaryReader.ReadBytes(4));
			}
			if (string.Compare(left, "r3d2", false) == 0)
			{
				this.WPKSoundBank = new WwiseBank.WPKBank(FileLocation);
				this.SoundBankTypeInfo = WwiseBank.SoundBankType.WPKSoundbank;
			}
			else
			{
				if (string.Compare(left, "BKHD", false) != 0)
				{
					throw new Exception("This is not a valid soundbank.");
				}
				this.BNKSoundBank = new WwiseBank.BNKBank(FileLocation);
				this.SoundBankTypeInfo = WwiseBank.SoundBankType.BNKSoundbank;
			}
		}
	}
}
