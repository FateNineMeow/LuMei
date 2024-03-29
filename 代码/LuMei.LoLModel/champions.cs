﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//     Support: http://www.cnblogs.com/huxj
//     Website: http://ITdos.com/Dos/ORM/Index.html
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using Dos.ORM;

namespace LuMei.LoLModel
{

    /// <summary>
    /// 英雄
    /// 实体类Champions 。(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Champions : Entity
    {
        public Champions() : base("champions") { }

        #region Model
        private int _Id;
        private string _Name;
        private string _DisplayName;
        private string _Title;
        private string _IconPath;
        private string _PortraitPath;
        private string _SplashPath;
        private string _DanceVideoPath;
        private string _Tags;
        private string _Description;
        private string _Quote;
        private string _QuoteAuthor;
        private decimal? _Range;
        private decimal? _MoveSpeed;
        private decimal? _ArmorBase;
        private decimal? _ArmorLevel;
        private decimal? _ManaBase;
        private decimal? _ManaLevel;
        private decimal? _CriticalChanceBase;
        private decimal? _CriticalChanceLevel;
        private decimal? _ManaRegenBase;
        private decimal? _ManaRegenLevel;
        private decimal? _HealthRegenBase;
        private decimal? _HealthRegenLevel;
        private decimal? _MagicResistBase;
        private decimal? _MagicResistLevel;
        private decimal? _HealthBase;
        private decimal? _HealthLevel;
        private decimal? _AttackBase;
        private decimal? _AttackLevel;
        private int? _RatingDefense;
        private int? _RatingMagic;
        private int? _RatingDifficulty;
        private int? _RatingAttack;
        private string _Tips;
        private string _OpponentTips;
        private string _SelectSoundPath;
        /// <summary>
        /// ID
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set
            {
                this.OnPropertyValueChange(_.Id, _Id, value);
                this._Id = value;
            }
        }
        /// <summary>
        /// 英文名
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set
            {
                this.OnPropertyValueChange(_.Name, _Name, value);
                this._Name = value;
            }
        }
        /// <summary>
        /// 称号
        /// </summary>
        public string DisplayName
        {
            get { return _DisplayName; }
            set
            {
                this.OnPropertyValueChange(_.DisplayName, _DisplayName, value);
                this._DisplayName = value;
            }
        }
        /// <summary>
        /// 中文名
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set
            {
                this.OnPropertyValueChange(_.Title, _Title, value);
                this._Title = value;
            }
        }
        /// <summary>
        /// 头像
        /// </summary>
        public string IconPath
        {
            get { return _IconPath; }
            set
            {
                this.OnPropertyValueChange(_.IconPath, _IconPath, value);
                this._IconPath = value;
            }
        }
        /// <summary>
        /// 载入图
        /// </summary>
        public string PortraitPath
        {
            get { return _PortraitPath; }
            set
            {
                this.OnPropertyValueChange(_.PortraitPath, _PortraitPath, value);
                this._PortraitPath = value;
            }
        }
        /// <summary>
        /// 原画
        /// </summary>
        public string SplashPath
        {
            get { return _SplashPath; }
            set
            {
                this.OnPropertyValueChange(_.SplashPath, _SplashPath, value);
                this._SplashPath = value;
            }
        }
        /// <summary>
        /// 跳舞视频
        /// </summary>
        public string DanceVideoPath
        {
            get { return _DanceVideoPath; }
            set
            {
                this.OnPropertyValueChange(_.DanceVideoPath, _DanceVideoPath, value);
                this._DanceVideoPath = value;
            }
        }
        /// <summary>
        /// 类型
        /// </summary>
        public string Tags
        {
            get { return _Tags; }
            set
            {
                this.OnPropertyValueChange(_.Tags, _Tags, value);
                this._Tags = value;
            }
        }
        /// <summary>
        /// 背景说明
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set
            {
                this.OnPropertyValueChange(_.Description, _Description, value);
                this._Description = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Quote
        {
            get { return _Quote; }
            set
            {
                this.OnPropertyValueChange(_.Quote, _Quote, value);
                this._Quote = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string QuoteAuthor
        {
            get { return _QuoteAuthor; }
            set
            {
                this.OnPropertyValueChange(_.QuoteAuthor, _QuoteAuthor, value);
                this._QuoteAuthor = value;
            }
        }
        /// <summary>
        /// 攻击范围
        /// </summary>
        public decimal? Range
        {
            get { return _Range; }
            set
            {
                this.OnPropertyValueChange(_.Range, _Range, value);
                this._Range = value;
            }
        }
        /// <summary>
        /// 移速
        /// </summary>
        public decimal? MoveSpeed
        {
            get { return _MoveSpeed; }
            set
            {
                this.OnPropertyValueChange(_.MoveSpeed, _MoveSpeed, value);
                this._MoveSpeed = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? ArmorBase
        {
            get { return _ArmorBase; }
            set
            {
                this.OnPropertyValueChange(_.ArmorBase, _ArmorBase, value);
                this._ArmorBase = value;
            }
        }
        /// <summary>
        /// 基础护甲
        /// </summary>
        public decimal? ArmorLevel
        {
            get { return _ArmorLevel; }
            set
            {
                this.OnPropertyValueChange(_.ArmorLevel, _ArmorLevel, value);
                this._ArmorLevel = value;
            }
        }
        /// <summary>
        /// 护甲等级加成
        /// </summary>
        public decimal? ManaBase
        {
            get { return _ManaBase; }
            set
            {
                this.OnPropertyValueChange(_.ManaBase, _ManaBase, value);
                this._ManaBase = value;
            }
        }
        /// <summary>
        /// 基础魔法
        /// </summary>
        public decimal? ManaLevel
        {
            get { return _ManaLevel; }
            set
            {
                this.OnPropertyValueChange(_.ManaLevel, _ManaLevel, value);
                this._ManaLevel = value;
            }
        }
        /// <summary>
        /// 魔法等级加成
        /// </summary>
        public decimal? CriticalChanceBase
        {
            get { return _CriticalChanceBase; }
            set
            {
                this.OnPropertyValueChange(_.CriticalChanceBase, _CriticalChanceBase, value);
                this._CriticalChanceBase = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal? CriticalChanceLevel
        {
            get { return _CriticalChanceLevel; }
            set
            {
                this.OnPropertyValueChange(_.CriticalChanceLevel, _CriticalChanceLevel, value);
                this._CriticalChanceLevel = value;
            }
        }
        /// <summary>
        /// 基础法力回复
        /// </summary>
        public decimal? ManaRegenBase
        {
            get { return _ManaRegenBase; }
            set
            {
                this.OnPropertyValueChange(_.ManaRegenBase, _ManaRegenBase, value);
                this._ManaRegenBase = value;
            }
        }
        /// <summary>
        /// 法力回复等级加成
        /// </summary>
        public decimal? ManaRegenLevel
        {
            get { return _ManaRegenLevel; }
            set
            {
                this.OnPropertyValueChange(_.ManaRegenLevel, _ManaRegenLevel, value);
                this._ManaRegenLevel = value;
            }
        }
        /// <summary>
        /// 基础生命回复
        /// </summary>
        public decimal? HealthRegenBase
        {
            get { return _HealthRegenBase; }
            set
            {
                this.OnPropertyValueChange(_.HealthRegenBase, _HealthRegenBase, value);
                this._HealthRegenBase = value;
            }
        }
        /// <summary>
        /// 生命回复等级加成
        /// </summary>
        public decimal? HealthRegenLevel
        {
            get { return _HealthRegenLevel; }
            set
            {
                this.OnPropertyValueChange(_.HealthRegenLevel, _HealthRegenLevel, value);
                this._HealthRegenLevel = value;
            }
        }
        /// <summary>
        /// 基础魔抗
        /// </summary>
        public decimal? MagicResistBase
        {
            get { return _MagicResistBase; }
            set
            {
                this.OnPropertyValueChange(_.MagicResistBase, _MagicResistBase, value);
                this._MagicResistBase = value;
            }
        }
        /// <summary>
        /// 魔抗加成
        /// </summary>
        public decimal? MagicResistLevel
        {
            get { return _MagicResistLevel; }
            set
            {
                this.OnPropertyValueChange(_.MagicResistLevel, _MagicResistLevel, value);
                this._MagicResistLevel = value;
            }
        }
        /// <summary>
        /// 基础生命
        /// </summary>
        public decimal? HealthBase
        {
            get { return _HealthBase; }
            set
            {
                this.OnPropertyValueChange(_.HealthBase, _HealthBase, value);
                this._HealthBase = value;
            }
        }
        /// <summary>
        /// 生命加成
        /// </summary>
        public decimal? HealthLevel
        {
            get { return _HealthLevel; }
            set
            {
                this.OnPropertyValueChange(_.HealthLevel, _HealthLevel, value);
                this._HealthLevel = value;
            }
        }
        /// <summary>
        /// 基础攻击
        /// </summary>
        public decimal? AttackBase
        {
            get { return _AttackBase; }
            set
            {
                this.OnPropertyValueChange(_.AttackBase, _AttackBase, value);
                this._AttackBase = value;
            }
        }
        /// <summary>
        /// 攻击加成
        /// </summary>
        public decimal? AttackLevel
        {
            get { return _AttackLevel; }
            set
            {
                this.OnPropertyValueChange(_.AttackLevel, _AttackLevel, value);
                this._AttackLevel = value;
            }
        }
        /// <summary>
        /// 防御等级
        /// </summary>
        public int? RatingDefense
        {
            get { return _RatingDefense; }
            set
            {
                this.OnPropertyValueChange(_.RatingDefense, _RatingDefense, value);
                this._RatingDefense = value;
            }
        }
        /// <summary>
        /// 魔抗
        /// </summary>
        public int? RatingMagic
        {
            get { return _RatingMagic; }
            set
            {
                this.OnPropertyValueChange(_.RatingMagic, _RatingMagic, value);
                this._RatingMagic = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RatingDifficulty
        {
            get { return _RatingDifficulty; }
            set
            {
                this.OnPropertyValueChange(_.RatingDifficulty, _RatingDifficulty, value);
                this._RatingDifficulty = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? RatingAttack
        {
            get { return _RatingAttack; }
            set
            {
                this.OnPropertyValueChange(_.RatingAttack, _RatingAttack, value);
                this._RatingAttack = value;
            }
        }
        /// <summary>
        /// 说明
        /// </summary>
        public string Tips
        {
            get { return _Tips; }
            set
            {
                this.OnPropertyValueChange(_.Tips, _Tips, value);
                this._Tips = value;
            }
        }
        /// <summary>
        /// 对局提示
        /// </summary>
        public string OpponentTips
        {
            get { return _OpponentTips; }
            set
            {
                this.OnPropertyValueChange(_.OpponentTips, _OpponentTips, value);
                this._OpponentTips = value;
            }
        }
        /// <summary>
        /// 选人音效
        /// </summary>
        public string SelectSoundPath
        {
            get { return _SelectSoundPath; }
            set
            {
                this.OnPropertyValueChange(_.SelectSoundPath, _SelectSoundPath, value);
                this._SelectSoundPath = value;
            }
        }
        #endregion

        #region Method
        /// <summary>
        /// 获取实体中的标识列
        /// </summary>
        public override Field GetIdentityField()
        {
            return _.Id;
        }
        /// <summary>
        /// 获取实体中的主键列
        /// </summary>
        public override Field[] GetPrimaryKeyFields()
        {
            return new Field[] {
				_.Id};
        }
        /// <summary>
        /// 获取列信息
        /// </summary>
        public override Field[] GetFields()
        {
            return new Field[] {
				_.Id,
				_.Name,
				_.DisplayName,
				_.Title,
				_.IconPath,
				_.PortraitPath,
				_.SplashPath,
				_.DanceVideoPath,
				_.Tags,
				_.Description,
				_.Quote,
				_.QuoteAuthor,
				_.Range,
				_.MoveSpeed,
				_.ArmorBase,
				_.ArmorLevel,
				_.ManaBase,
				_.ManaLevel,
				_.CriticalChanceBase,
				_.CriticalChanceLevel,
				_.ManaRegenBase,
				_.ManaRegenLevel,
				_.HealthRegenBase,
				_.HealthRegenLevel,
				_.MagicResistBase,
				_.MagicResistLevel,
				_.HealthBase,
				_.HealthLevel,
				_.AttackBase,
				_.AttackLevel,
				_.RatingDefense,
				_.RatingMagic,
				_.RatingDifficulty,
				_.RatingAttack,
				_.Tips,
				_.OpponentTips,
				_.SelectSoundPath};
        }
        /// <summary>
        /// 获取值信息
        /// </summary>
        public override object[] GetValues()
        {
            return new object[] {
				this._Id,
				this._Name,
				this._DisplayName,
				this._Title,
				this._IconPath,
				this._PortraitPath,
				this._SplashPath,
				this._DanceVideoPath,
				this._Tags,
				this._Description,
				this._Quote,
				this._QuoteAuthor,
				this._Range,
				this._MoveSpeed,
				this._ArmorBase,
				this._ArmorLevel,
				this._ManaBase,
				this._ManaLevel,
				this._CriticalChanceBase,
				this._CriticalChanceLevel,
				this._ManaRegenBase,
				this._ManaRegenLevel,
				this._HealthRegenBase,
				this._HealthRegenLevel,
				this._MagicResistBase,
				this._MagicResistLevel,
				this._HealthBase,
				this._HealthLevel,
				this._AttackBase,
				this._AttackLevel,
				this._RatingDefense,
				this._RatingMagic,
				this._RatingDifficulty,
				this._RatingAttack,
				this._Tips,
				this._OpponentTips,
				this._SelectSoundPath};
        }
        #endregion

        #region _Field
        /// <summary>
        /// 字段信息
        /// </summary>
        public class _
        {
            /// <summary>
            /// * 
            /// </summary>
            public readonly static Field All = new Field("*", "champions");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Id = new Field("id", "champions", "id");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Name = new Field("name", "champions", "name");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field DisplayName = new Field("displayName", "champions", "displayName");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Title = new Field("title", "champions", "title");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field IconPath = new Field("iconPath", "champions", "iconPath");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field PortraitPath = new Field("portraitPath", "champions", "portraitPath");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field SplashPath = new Field("splashPath", "champions", "splashPath");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field DanceVideoPath = new Field("danceVideoPath", "champions", "danceVideoPath");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Tags = new Field("tags", "champions", "tags");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Description = new Field("description", "champions", "description");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Quote = new Field("quote", "champions", "quote");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field QuoteAuthor = new Field("quoteAuthor", "champions", "quoteAuthor");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Range = new Field("range", "champions", "range");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field MoveSpeed = new Field("moveSpeed", "champions", "moveSpeed");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ArmorBase = new Field("armorBase", "champions", "armorBase");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ArmorLevel = new Field("armorLevel", "champions", "armorLevel");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ManaBase = new Field("manaBase", "champions", "manaBase");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ManaLevel = new Field("manaLevel", "champions", "manaLevel");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field CriticalChanceBase = new Field("criticalChanceBase", "champions", "criticalChanceBase");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field CriticalChanceLevel = new Field("criticalChanceLevel", "champions", "criticalChanceLevel");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ManaRegenBase = new Field("manaRegenBase", "champions", "manaRegenBase");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field ManaRegenLevel = new Field("manaRegenLevel", "champions", "manaRegenLevel");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field HealthRegenBase = new Field("healthRegenBase", "champions", "healthRegenBase");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field HealthRegenLevel = new Field("healthRegenLevel", "champions", "healthRegenLevel");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field MagicResistBase = new Field("magicResistBase", "champions", "magicResistBase");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field MagicResistLevel = new Field("magicResistLevel", "champions", "magicResistLevel");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field HealthBase = new Field("healthBase", "champions", "healthBase");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field HealthLevel = new Field("healthLevel", "champions", "healthLevel");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field AttackBase = new Field("attackBase", "champions", "attackBase");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field AttackLevel = new Field("attackLevel", "champions", "attackLevel");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field RatingDefense = new Field("ratingDefense", "champions", "ratingDefense");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field RatingMagic = new Field("ratingMagic", "champions", "ratingMagic");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field RatingDifficulty = new Field("ratingDifficulty", "champions", "ratingDifficulty");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field RatingAttack = new Field("ratingAttack", "champions", "ratingAttack");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field Tips = new Field("tips", "champions", "tips");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field OpponentTips = new Field("opponentTips", "champions", "opponentTips");
            /// <summary>
            /// 
            /// </summary>
            public readonly static Field SelectSoundPath = new Field("selectSoundPath", "champions", "selectSoundPath");
        }
        #endregion


    }
}

