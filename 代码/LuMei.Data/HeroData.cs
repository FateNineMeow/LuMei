using System.Collections.Generic;
using System.Linq;
using LuMei.Model;

namespace LuMei.Data
{
    public class HeroData
    {
        public IList<Hero> Heroes;

        public List<string> HeroType;

        public HeroData()
        {
            Heroes = new List<Hero>();

            var hero = new Hero { Alias = "暗裔剑魔", ChName = "亚托克斯", EnName = "Aatrox", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "九尾妖狐", ChName = "阿狸", EnName = "Ahri", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "暗影之拳", ChName = "阿卡丽", EnName = "Akali", HeroType = "刺客" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "牛头酋长", ChName = "阿利斯塔", EnName = "Alistar", HeroType = "坦克" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "殇之木乃伊", ChName = "阿木木", EnName = "Amumu", HeroType = "坦克" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "冰晶凤凰", ChName = "艾尼维亚", EnName = "Anivia", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "黑暗之女", ChName = "安妮", EnName = "Annie", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "寒冰射手", ChName = "艾希", EnName = "Ashe", HeroType = "射手" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "沙漠皇帝", ChName = "阿兹尔", EnName = "Azir", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "蒸汽机器人", ChName = "布里茨", EnName = "Blitzcrank", HeroType = "辅助" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "复仇焰魂", ChName = "布兰德", EnName = "Brand", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "弗雷尔卓德之心", ChName = "布隆", EnName = "Braum", HeroType = "辅助" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "皮城女警", ChName = "凯特琳", EnName = "Caitlyn", HeroType = "射手" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "魔蛇之拥", ChName = "卡西奥佩娅", EnName = "Cassiopeia", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "虚空恐惧", ChName = "科加斯", EnName = "ChoGath", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "英勇投弹手", ChName = "库奇", EnName = "Corki", HeroType = "射手" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "诺克萨斯之手 ", ChName = "德莱厄斯", EnName = "Darius", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "皎月女神", ChName = "黛安娜", EnName = "Diana", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "祖安狂人", ChName = "蒙多", EnName = "DrMundo", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "荣耀行刑官", ChName = "德莱文", EnName = "Draven", HeroType = "射手" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "时间刺客", ChName = "艾克", EnName = "Ekko", HeroType = "刺客" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "蜘蛛女皇", ChName = "伊莉丝", EnName = "Elise", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "寡妇制造者", ChName = "伊芙琳", EnName = "Evelynn", HeroType = "刺客" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "探险家", ChName = "伊泽瑞尔", EnName = "Ezreal", HeroType = "射手" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "末日使者", ChName = "费德提克", EnName = "Fiddlesticks", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "无双剑姬", ChName = "菲奥娜", EnName = "Fiora", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "潮汐海灵", ChName = "菲兹", EnName = "Fizz", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "哨兵之殇", ChName = "加里奥", EnName = "Galio", HeroType = "坦克" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "海洋之灾", ChName = "普朗克", EnName = "Gangplank", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "德玛西亚之力", ChName = "盖伦", EnName = "Garen", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "迷失之牙", ChName = "纳尔", EnName = "Gnar", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "酒桶", ChName = "古拉加斯", EnName = "Gragas", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "法外狂徒", ChName = "格雷福斯", EnName = "Graves", HeroType = "射手" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "战争之影 ", ChName = "赫卡里姆", EnName = "Hecarim", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "大发明家", ChName = "黑默丁格", EnName = "Heimerdinger", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "刀锋意志", ChName = "艾瑞莉亚", EnName = "Irelia", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "风暴之怒", ChName = "迦娜", EnName = "Janna", HeroType = "辅助" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "德玛西亚皇子", ChName = "嘉文四世", EnName = "JarvanIV", HeroType = "坦克" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "武器大师", ChName = "贾克斯", EnName = "Jax", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "未来守护者", ChName = "杰斯", EnName = "Jayce", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "暴走萝莉", ChName = "金克丝", EnName = "Jinx", HeroType = "射手" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "复仇之矛", ChName = "卡莉丝塔", EnName = "Kalista", HeroType = "射手" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "天启者", ChName = "卡尔玛", EnName = "Karma", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "死亡颂唱者", ChName = "卡尔萨斯", EnName = "Karthus", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "虚空行者", ChName = "卡萨丁", EnName = "Kassadin", HeroType = "刺客" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "不祥之刃", ChName = "卡特琳娜", EnName = "Katarina", HeroType = "刺客" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "审判天使", ChName = "凯尔", EnName = "Kayle", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "狂暴之心", ChName = "凯南", EnName = "Kennen", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "虚空掠夺者", ChName = "卡兹克", EnName = "KhaZix", HeroType = "刺客" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "深渊巨口", ChName = "克格莫", EnName = "KogMaw", HeroType = "射手" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "诡术妖姬", ChName = "乐芙兰", EnName = "Leblanc", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "盲僧", ChName = "李青", EnName = "LeeSin", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "曙光女神", ChName = "蕾欧娜", EnName = "Leona", HeroType = "辅助" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "冰霜女巫", ChName = "丽桑卓", EnName = "Lissandra", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "圣枪游侠", ChName = "卢锡安", EnName = "Lucian", HeroType = "射手" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "仙灵女巫", ChName = "璐璐", EnName = "Lulu", HeroType = "辅助" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "光辉女郎", ChName = "拉克丝", EnName = "Lux", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "熔岩巨兽", ChName = "墨菲特", EnName = "Malphite", HeroType = "坦克" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "虚空先知", ChName = "玛尔扎哈", EnName = "Malzahar", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "扭曲树精", ChName = "茂凯", EnName = "Maokai", HeroType = "坦克" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "无极剑圣", ChName = "易", EnName = "MasterYi", HeroType = "刺客" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "赏金猎人", ChName = "厄运小姐", EnName = "MissFortune", HeroType = "射手" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "金属大师", ChName = "莫德凯撒", EnName = "Mordekaiser", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "堕落天使", ChName = "莫甘娜", EnName = "Morgana", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "唤潮鲛姬", ChName = "娜美", EnName = "Nami", HeroType = "辅助" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "沙漠死神", ChName = "内瑟斯", EnName = "Nasus", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "深海泰坦", ChName = "诺提勒斯", EnName = "Nautilus", HeroType = "坦克" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "狂野女猎手", ChName = "奈德丽", EnName = "Nidalee", HeroType = "刺客" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "永恒梦魇", ChName = "魔腾", EnName = "Nocturne", HeroType = "刺客" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "雪人骑士", ChName = "努努", EnName = "Nunu", HeroType = "辅助" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "狂战士", ChName = "奥拉夫", EnName = "Olaf", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "发条魔灵", ChName = "奥莉安娜", EnName = "Orianna", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "战争之王", ChName = "潘森", EnName = "Pantheon", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "钢铁大使", ChName = "波比", EnName = "Poppy", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "德玛西亚之翼", ChName = "奎因", EnName = "Quinn", HeroType = "射手" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "披甲龙龟", ChName = "拉莫斯", EnName = "Rammus", HeroType = "坦克" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "虚空掘地者", ChName = "雷克塞", EnName = "RekSai", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "荒漠屠夫", ChName = "雷克顿", EnName = "Renekton", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "傲之追猎者", ChName = "雷恩加尔", EnName = "Rengar", HeroType = "刺客" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "放逐之刃", ChName = "锐雯", EnName = "Riven", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "机械公敌", ChName = "兰博", EnName = "Rumble", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "流浪法师", ChName = "瑞兹", EnName = "Ryze", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "凛冬之怒", ChName = "瑟庄妮", EnName = "Sejuani", HeroType = "坦克" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "恶魔小丑", ChName = "萨科", EnName = "Shaco", HeroType = "刺客" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "暮光之眼", ChName = "慎", EnName = "Shen", HeroType = "坦克" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "龙血武姬", ChName = "希瓦娜", EnName = "Shyvana", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "炼金术士", ChName = "辛吉德", EnName = "Singed", HeroType = "坦克" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "亡灵勇士", ChName = "赛恩", EnName = "Sion", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "战争女神", ChName = "希维尔", EnName = "Sivir", HeroType = "射手" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "水晶先锋", ChName = "斯卡纳", EnName = "Skarner", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "琴瑟仙女", ChName = "娑娜", EnName = "Sona", HeroType = "辅助" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "众星之子", ChName = "索拉卡", EnName = "Soraka", HeroType = "辅助" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "策士统领", ChName = "斯维因", EnName = "Swain", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "暗黑元首", ChName = "辛德拉", EnName = "Syndra", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "刀锋之影", ChName = "泰隆", EnName = "Talon", HeroType = "刺客" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "宝石骑士", ChName = "塔里克", EnName = "Taric", HeroType = "辅助" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "迅捷斥候", ChName = "提莫", EnName = "Teemo", HeroType = "射手" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "魂锁典狱长", ChName = "锤石", EnName = "Thresh", HeroType = "辅助" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "麦林炮手", ChName = "崔丝塔娜", EnName = "Tristana", HeroType = "射手" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "诅咒巨魔", ChName = "特朗德尔", EnName = "Trundle", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "蛮族之王", ChName = "泰达米尔", EnName = "Tryndamere", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "卡牌大师", ChName = "崔斯特", EnName = "TwistedFate", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "瘟疫之源", ChName = "图奇", EnName = "Twitch", HeroType = "射手" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "兽灵行者", ChName = "乌迪尔", EnName = "Udyr", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "首领之傲", ChName = "厄加特", EnName = "Urgot", HeroType = "射手" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "惩戒之箭", ChName = "韦鲁斯", EnName = "Varus", HeroType = "射手" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "暗夜猎手", ChName = "薇恩", EnName = "Vayne", HeroType = "射手" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "邪恶小法师", ChName = "维迦", EnName = "Veigar", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "虚空之眼", ChName = "维克兹", EnName = "VelKoz", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "皮城执法官", ChName = "蔚", EnName = "Vi", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "机械先驱", ChName = "维克托", EnName = "Viktor", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "猩红收割者", ChName = "弗拉基米尔", EnName = "Vladimir", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "雷霆咆哮", ChName = "沃利贝尔", EnName = "Volibear", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "嗜血猎手", ChName = "沃里克", EnName = "Warwick", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "齐天大圣", ChName = "孙悟空", EnName = "MonkeyKing", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "远古巫灵", ChName = "泽拉斯", EnName = "Xerath", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "德邦总管", ChName = "赵信", EnName = "XinZhao", HeroType = "坦克" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "疾风剑豪", ChName = "亚索", EnName = "Yasuo", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "掘墓者", ChName = "约里克", EnName = "Yorick", HeroType = "战士" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "生化魔人", ChName = "扎克", EnName = "Zac", HeroType = "坦克" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "影流之主", ChName = "劫", EnName = "Zed", HeroType = "刺客" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "爆破鬼才", ChName = "吉格斯", EnName = "Ziggs", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "时光守护者", ChName = "基兰", EnName = "Zilean", HeroType = "辅助" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "荆棘之兴", ChName = "婕拉", EnName = "Zyra", HeroType = "法师" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "永猎双子", ChName = "千珏", EnName = "Kindred", HeroType = "射手" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "载入界面", ChName = "载入界面", EnName = "SRBackground", HeroType = "其他" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "眼位", ChName = "眼位", EnName = "Ward", HeroType = "其他" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "游戏界面", ChName = "游戏界面", EnName = "GameUI", HeroType = "其他" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "鼠标", ChName = "鼠标", EnName = "Cursors", HeroType = "其他" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "字体", ChName = "字体", EnName = "Fonts", HeroType = "其他" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "音效", ChName = "音效", EnName = "Sounds", HeroType = "其他" };
            Heroes.Add(hero);

            hero = new Hero { Alias = "其他皮肤", ChName = "其他皮肤", EnName = "Other", HeroType = "其他" };
            Heroes.Add(hero);

            HeroType = new List<string> { "刺客", "战士", "法师", "射手", "坦克", "辅助", "其他" };
            Heroes = Heroes.OrderBy(d => d.HeroType).ThenBy(d => d.EnName).ToList();
        }
    }

}
