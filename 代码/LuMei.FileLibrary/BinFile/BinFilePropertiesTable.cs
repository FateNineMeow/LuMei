using System.Collections.Generic;

namespace LuMei.FileLibrary.BinFile
{
    public class BinFilePropertiesTable
    {
        public List<BinFileProperty> Props;
        public BinFilePropertiesTable()
        {
            this.Props = new List<BinFileProperty>();
            this.NewProp("Animation time", 4142500092u);
            this.NewProp("Model scale", 2717386202u);
            this.NewProp("Skin ID", 2843210926u);
            this.NewProp("Skin Name", 762889000u);
            this.NewProp("Loading Screen", 2549553293u);
            this.NewProp("SKL File", 2974586734u);
            this.NewProp("SKN File", 3600813558u);
            this.NewProp("Texture File", 1013213428u);
        }
        public BinFileProperty NewProp(string Prop, uint Hash)
        {
            BinFileProperty binFileProperty = new BinFileProperty(Prop, Hash);
            this.Props.Add(binFileProperty);
            return binFileProperty;
        }
        public BinFileProperty GetProp(uint Hash)
        {
            BinFileProperty result;
            try
            {
                List<BinFileProperty>.Enumerator enumerator = this.Props.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    BinFileProperty current = enumerator.Current;
                    if (current.Hash == Hash)
                        return current;
                }
            }
            finally
            {
            }
            result = this.NewProp(null, Hash);
            return result;
        }
    }
}
