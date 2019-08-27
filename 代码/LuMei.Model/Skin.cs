
using System;

namespace LuMei.Model
{
    public class Skin
    {
        /// <summary>
        /// 编号
        /// </summary>
        public Int64 Id { get; set; }
        /// <summary>
        /// 皮肤名称
        /// </summary>
        public string SkinName { get; set; }
        /// <summary>
        /// 对应英雄
        /// </summary>
        public string Hero { get; set; }
        /// <summary>
        /// 皮肤作者
        /// </summary>
        public string Author { get; set; }
        /// <summary>
        /// 皮肤包名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 挂载类型
        /// </summary>
        public string MountType { get; set; }
        /// <summary>
        /// 载入图
        /// </summary>
        public string LoadPic { get; set; }
        /// <summary>
        /// 原画
        /// </summary>
        public string Original { get; set; }
        /// <summary>
        /// 预览图
        /// </summary>
        public string BackImage { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 皮肤版本
        /// </summary>
        public string SkinSkl { get; set; }
        /// <summary>
        /// 模型
        /// </summary>
        public string SkinSkn { get; set; }
        /// <summary>
        /// 皮肤贴图
        /// </summary>
        public string Skindds { get; set; }
    }
}
