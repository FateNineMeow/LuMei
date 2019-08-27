using System.Windows.Media;

namespace LuMei.IO
{
    public class AccentColorMenuData
    {
        public string Name { get; set; }
        public Brush BorderColorBrush { get; set; }
        public Brush ColorBrush { get; set; }

        protected virtual void DoChangeTheme(object sender)
        {
           
        }
    }

    public class AppThemeMenuData : AccentColorMenuData
    {
        protected override void DoChangeTheme(object sender)
        {
           
        }
    }
}