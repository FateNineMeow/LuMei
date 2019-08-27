using System.Collections.Generic;
using System.Collections.ObjectModel;
using LuMei.LoLModel;
using LuMei.Model;

namespace LuMei.IO
{
    public interface ILuMeiClass
    {
        #region 初始化及配置文件相关
        /// <summary>
        /// 重建LuMei.txt文件
        /// </summary>
        bool CreateLuMeiTxt();
        /// <summary>
        /// 重新挂载已挂载但是文件不存在的皮肤
        /// </summary>
        void CheckMountSkinFile();
        /// <summary>
        /// 清理不正常挂载皮肤
        /// </summary>
        /// <returns></returns>
        bool ClearCheckSkin();
        #endregion

        #region 基础操作
        ObservableCollection<Hero> AllHero();
        List<Champions> AllHeroes();
        ObservableCollection<Hero> Hero(int typeid);
        ObservableCollection<Hero> Hero(string heroname);
        ObservableCollection<Skin> AllSkin();
        ObservableCollection<Skin> Skin(string heroname);
        ObservableCollection<Skin> Skin(string heroname, long skinid);
        /// <summary>
        /// 获取所有皮肤
        /// </summary>
        /// <returns></returns>
        List<Skin> AllSkins();
        /// <summary>
        /// 获取(某个英雄)的皮肤数量
        /// </summary>
        /// <param name="heroname"></param>
        /// <returns></returns>
        int SkinCount(string heroname);
        /// <summary>
        /// 依据英雄名称获取皮肤
        /// </summary>
        /// <param name="heroname"></param>
        /// <returns></returns>
        //     List<Skin> GetISkinsByHero(string heroname);
        /// <summary>
        /// 获取所有皮肤
        /// </summary>
        /// <returns></returns>
        List<Skin> GetSkinsByMountType(string mounttype);
        /// <summary>
        /// 依据英文名获取中文名
        /// </summary>
        /// <param name="enname"></param>
        /// <returns></returns>
        string EnNameToChName(string enname);
        Skin GetSkin(string id);
        /// <summary>
        /// 依据名字查询英雄（包括其他皮肤）
        /// </summary>
        /// <param name="enname"></param>
        /// <param name="type">是否包含其他皮肤</param>
        /// <returns></returns>
        Champions GetHero(string enname, bool type);
        /// <summary>
        /// 依据英雄类型获取英雄
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        List<Champions> GetHeroByType(int typeid);

        #endregion

        #region 保存皮肤文件

        /// <summary>
        /// 保存皮肤文件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Set SaveSkinModel(Skin model);
        #endregion 

        #region  皮肤操作（挂载、卸载、删除、导出）
        /// <summary>
        /// 挂载皮肤
        /// </summary>
        /// <param name="model"></param>
        /// <param name="addOrRemove"></param>
        /// <returns></returns>
        Skin SkinMount(Skin model);
        /// <summary>
        /// 卸载皮肤
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Skin SkinUnMount(Skin model);
        void SkinAllUninstall();
        void SkinReMount();
        /// <summary>
        /// 删除皮肤
        /// </summary>
        /// <param name="skinid"></param>
        /// <returns></returns>
        void SkinDelete(Skin model);
        /// <summary>
        /// 提取英雄皮肤
        /// </summary>
        /// <param name="heroname"></param>
        void HeroExtract(string heroname);
        /// <summary>
        /// 导出皮肤
        /// </summary>
        /// <param name="model"></param>
        void SkinExport(Skin model);

        #endregion

        #region ClientZipstxt操作
        /// <summary>
        /// 读写ClientZipstxt文件
        /// </summary>
        /// <param name="model"></param>
        /// <param name="type">0为写，1为读</param>
        /// <returns></returns>
        bool ReadOrWriteClientZipstxt(Skin model, bool type);
        /// <summary>
        /// 读写ClientZipstxt文件
        /// </summary>
        /// <param name="model"></param>
        /// <param name="type">0为写，1为读</param>
        /// <returns></returns>
        bool ReadOrWriteClientZipstxt(string filepath, bool type);
        #endregion

        #region 自动更新相关
        /// <summary>
        /// 检查更新状态、检查自动更新程序是否存在、判断程序是否正在运行
        /// </summary>
        /// <returns></returns>
        Set CheckUpdate();
        /// <summary>
        /// 开始升级
        /// </summary>
        /// <returns></returns>
        void StartUpdate();

        #endregion
        /// <summary>
        /// 获取所有文件
        /// </summary>
        /// <param name="sourepath"></param>
        /// <returns></returns>
        List<string> AllSkinZipFiles(string sourepath);
        /// <summary>
        /// 检查挂载皮肤
        /// </summary>
        /// <returns></returns>
        int CheckMountSkin();
        void SknConverter(string file);
    }
}
