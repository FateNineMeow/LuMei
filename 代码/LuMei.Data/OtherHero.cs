using System.Collections.Generic;
using LuMei.LoLModel;

namespace LuMei.Data
{
    public class OtherHero
    {
        public List<Champions> Heroes;
        public OtherHero()
        {
            Heroes = new List<Champions>();

            var hero = new Champions { DisplayName = "载入界面", Title = "载入界面", Name = "SRBackground" };
            Heroes.Add(hero);

            hero = new Champions { DisplayName = "眼位", Title = "眼位", Name = "Ward" };
            Heroes.Add(hero);

            hero = new Champions { DisplayName = "游戏界面", Title = "游戏界面", Name = "GameUI" };
            Heroes.Add(hero);

            hero = new Champions { DisplayName = "鼠标", Title = "鼠标", Name = "Cursors" };
            Heroes.Add(hero);

            hero = new Champions { DisplayName = "字体", Title = "字体", Name = "Fonts" };
            Heroes.Add(hero);

            hero = new Champions { DisplayName = "音效", Title = "音效", Name = "Sounds" };
            Heroes.Add(hero);

            hero = new Champions { DisplayName = "其他皮肤", Title = "其他皮肤", Name = "Other" };
            Heroes.Add(hero);
        }
    }

}
