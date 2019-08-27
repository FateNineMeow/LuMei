namespace LuMei.FileLibrary.BinFile
{
    public class BinFileProperty
    {
        public uint Hash;

        public string Prop;

        public BinFileProperty(string Prop, uint Hash)
        {
            this.Prop = Prop;
            this.Hash = Hash;
        }

        public BinFileProperty(uint Hash)
		{
			this.Hash = Hash;
		}
    }
}
