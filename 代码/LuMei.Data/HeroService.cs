
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Dos.ORM;
using LuMei.Helper;
using LuMei.LoLModel;

namespace LuMei.Data
{

    public class Db
    {
        public static readonly DbSession Context = new DbSession(DatabaseType.Sqlite3, Game.ConnectionString);
    }
    /// <summary>
    /// 英雄
    /// </summary>
    public class ChampionsService
    {
        readonly OtherHero _other = new OtherHero();
        /// <summary>
        /// 包括其他皮肤
        /// </summary>
        /// <returns></returns>
        public List<Champions> GetAll()
        {
            var list = new List<Champions>();
            try
            {
                list = Db.Context.From<Champions>().ToList();
                list.AddRange(_other.Heroes);
            }
            catch (Exception ex)
            {
                throw new Exception("客户端路径读取出错！");
            }
            return list;
        }
        /// <summary>
        /// 不包括其他皮肤
        /// </summary>
        /// <returns></returns>
        public List<Champions> AllHero()
        {
            return Db.Context.From<Champions>().ToList();
        }
        /// <summary>
        /// 其他皮肤
        /// </summary>
        /// <returns></returns>
        public List<Champions> OtherHero()
        {
            return _other.Heroes;
        }
        public List<Champions> GetList(Expression<Func<Champions, bool>> where)
        {
            var list = new List<Champions>();
            try
            {
                return Db.Context.From<Champions>().Where(where).ToList();
            }
            catch
            {
                throw new Exception("客户端路径读取出错！");
            }
            return list;
        }
        public Champions GetModel(string name)
        {
            var list = GetAll();
            var model = list.FirstOrDefault(d => d.Name == name);
            return model;
        }
        public Champions GetModel(Expression<Func<Champions, bool>> where)
        {
            var list = new Champions();
            try
            {
                return Db.Context.From<Champions>().Where(where).ToFirstDefault();
            }
            catch
            {
                throw new Exception("客户端路径读取出错！");
            }
            return list;

        }
        public Champions GetModel(int id)
        {
            var list = new Champions();
            try
            {
                return Db.Context.From<Champions>().Where(d => d.Id == id).ToFirstDefault();
            }
            catch
            {
                throw new Exception("客户端路径读取出错！");
            }
            return list;

        }
        public int Update(Champions model)
        {
            return Db.Context.Update(model);
        }

    }
    /// <summary>
    /// 英雄皮肤
    /// </summary>
    public class ChampionsSkinsService
    {
        public List<ChampionSkins> GetAll()
        {
            return Db.Context.From<ChampionSkins>().ToList();
        }
        public List<ChampionSkins> GetList(Expression<Func<ChampionSkins, bool>> where)
        {
            return Db.Context.From<ChampionSkins>().Where(where).ToList();
        }
        public ChampionSkins GetModel(Expression<Func<ChampionSkins, bool>> where)
        {
            return Db.Context.From<ChampionSkins>().Where(where).ToFirstDefault();
        }
        public ChampionSkins GetModel(int id)
        {
            return Db.Context.From<ChampionSkins>().Where(d => d.Id == id).ToFirstDefault();
        }
        public int Update(ChampionSkins model)
        {
            return Db.Context.Update(model);
        }
    }
    /// <summary>
    /// 英雄标签
    /// </summary>
    public class SearchTagsService
    {
        public List<SearchTags> GetAll()
        {
            return Db.Context.From<SearchTags>().ToList();
        }
        public List<SearchTags> GetList(Expression<Func<SearchTags, bool>> where)
        {
            return Db.Context.From<SearchTags>().Where(where).ToList();
        }
        public SearchTags GetModel(Expression<Func<SearchTags, bool>> where)
        {
            return Db.Context.From<SearchTags>().Where(where).ToFirstDefault();
        }
        public SearchTags GetModel(int id)
        {
            return Db.Context.From<SearchTags>().Where(d => d.Id == id).ToFirstDefault();
        }
        public int Update(SearchTags model)
        {
            return Db.Context.Update(model);
        }
    }
    /// <summary>
    /// 英雄标签
    /// </summary>
    public class ChampionSearchTagsService
    {
        public List<ChampionSearchTags> GetAll()
        {
            return Db.Context.From<ChampionSearchTags>().ToList();
        }
        public List<ChampionSearchTags> GetList(Expression<Func<ChampionSearchTags, bool>> where)
        {
            return Db.Context.From<ChampionSearchTags>().Where(where).ToList();
        }
        public ChampionSearchTags GetModel(Expression<Func<ChampionSearchTags, bool>> where)
        {
            return Db.Context.From<ChampionSearchTags>().Where(where).ToFirstDefault();
        }
        public ChampionSearchTags GetModel(int id)
        {
            return Db.Context.From<ChampionSearchTags>().Where(d => d.Id == id).ToFirstDefault();
        }
        public int Update(ChampionSearchTags model)
        {
            return Db.Context.Update(model);
        }
    }

}
