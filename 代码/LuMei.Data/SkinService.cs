
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Reflection;
using LuMei.Helper;
using LuMei.Model;

namespace LuMei.Data
{
    public class SkinService
    {      //数据库连接
        private const string TableName = "Skin";

        private static bool ExecuteSqlNonquery(string sql)
        {
            try
            {
                SQLiteConnection dbConn = new SQLiteConnection(Soft.ConnectionString);
                dbConn.Open();
                var command = new SQLiteCommand(sql, dbConn);
                command.ExecuteNonQuery();
                dbConn.Close();
                return true;
            }
            catch (Exception ex)
            {
                Log.LogError("数据修改保存", ex);
                return false;
            }
        }
        /// <summary>
        /// 数据库重建
        /// </summary>
        /// <returns></returns>
        public string CreateNewDb()
        {
            //try
            //{
            //    if (!File.Exists(Soft.DbPath))
            //    {
            //        SQLiteConnection.CreateFile(Soft.DbPath);
            //        if (!File.Exists(Soft.DbPath))
            //        {
            //            const string sql =
            //                "CREATE TABLE AMSkins (ID  integer PRIMARY KEY autoincrement,Logo nvarchar(64),SkinName nvarchar(64),Author nvarchar(64),Hero  nvarchar(64),MountType nvarchar(64),CreateTime nvarchar(64),FileUrl nvarchar(64),Comment ntext);";
            //            ExecuteSqlNonquery(sql);
            //        }
            //    }
            //    return null;
            //}
            //catch (Exception ex)
            //{
            //     Log.LogInfo("创建数据库", ex.Message);
            return null;
            //}

        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="model"></param>
        public bool ChangeMountType(Skin model)
        {
            var sql = string.Format("Update {0} set MountType='{1}' where ID={2}", TableName, model.MountType, model.Id);
            try
            {
                return ExecuteSqlNonquery(sql);
            }
            catch (Exception ex)
            {
                Log.LogInfo("修改挂载状态", ex.Message);
                return false;
            }
        }
        public bool SaveOrEdit(Skin model)
        {
            return model.Id == 0 ? Add(model) : Update(model);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="model"></param>
        public bool Update(Skin model)
        {
            string sql = String.Format("Update {0} set SkinName='{1}',Hero='{2}',Author='{3}',FileName='{4}',MountType='{5}'," +
                   "LoadPic='{6}',Original='{7}',BackImage='{8}',CreateTime='{9}',Comment='{10}' where ID={11}",
                   TableName, model.SkinName, model.Hero, model.Author, model.FileName, model.MountType, model.LoadPic, model.Original, model.BackImage, model.CreateTime, model.Comment, model.Id);
            //sql =
            //   String.Format("Update {0} set LoadPic='{1}',SkinName='{2}',Author='{3}',Hero='{4}'," +
            //                 "MountType='{5}',CreateTime='{6}',Original='{7}',Comment='{8}', where ID='{9}'",
            //                 TableName, model.LoadPic, model.SkinName, model.Author, model.Hero,
            //                 model.MountType, model.CreateTime, model.Original, model.Comment, model.ID);
            try
            {
                return ExecuteSqlNonquery(sql);
            }
            catch (Exception ex)
            {
                Log.LogError("数据修改保存", ex);
                return false;
            }
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Skin GetSkin(string id)
        {
            var model = new Skin();
            string sql = string.Format("select * from {0} where id='{1}' order by CreateTime  desc", TableName, id);
            DataSet ds = new DataSet();
            try
            {
                ds = Query(sql);
                model.Id = int.Parse(ds.Tables[TableName].Rows[0]["ID"].ToString());
                model.SkinName = ds.Tables[TableName].Rows[0]["SkinName"].ToString();
                model.Hero = ds.Tables[TableName].Rows[0]["Hero"].ToString();
                model.Author = ds.Tables[TableName].Rows[0]["Author"].ToString();
                model.FileName = ds.Tables[TableName].Rows[0]["FileName"].ToString();
                model.MountType = ds.Tables[TableName].Rows[0]["MountType"].ToString();
                model.LoadPic = ds.Tables[TableName].Rows[0]["LoadPic"].ToString();
                model.Original = ds.Tables[TableName].Rows[0]["Original"].ToString();
                model.BackImage = ds.Tables[TableName].Rows[0]["BackImage"].ToString();
                model.Comment = ds.Tables[TableName].Rows[0]["Comment"].ToString();
                model.CreateTime = ds.Tables[TableName].Rows[0]["CreateTime"].ToString();
                return model;
            }
            catch (SQLiteException ex)
            {
                Log.LogInfo("获取实体出错", ex.Message);
                return null;
            }
        }
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="model"></param>
        public bool Add(Skin model)
        {
            if (!string.IsNullOrEmpty(model.Hero) && !string.IsNullOrEmpty(model.SkinName))
            {
                if (string.IsNullOrWhiteSpace(model.MountType))
                    model.MountType = "未挂载";
                if (string.IsNullOrWhiteSpace(model.CreateTime))
                    model.CreateTime = DateTime.Now.ToLongDateString();
                string sql = string.Format("insert into {0} (ID,SkinName,Hero,Author,FileName,MountType,LoadPic,Original,BackImage,CreateTime,Comment) values (null,'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}')", TableName, model.SkinName, model.Hero, model.Author, model.FileName, model.MountType, model.LoadPic, model.Original, model.BackImage, model.CreateTime, model.Comment);
                try
                {
                    return ExecuteSqlNonquery(sql);
                }
                catch (Exception ex)
                {
                    Log.LogInfo("数据添加", ex.Message);
                    return false;
                }
            }
            return false;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        public bool Delete(string id)
        {
            string sql = string.Format("DELETE FROM  {0} WHERE ID = {1};", TableName, id);
            try
            {
                return ExecuteSqlNonquery(sql);
            }
            catch (Exception ex)
            {
                Log.LogInfo("删除数据", ex.Message);
            }
            return false;
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="id"></param>
        public bool Delete(long id)
        {
            string sql = string.Format("DELETE FROM  {0} WHERE ID = {1};", TableName, id);
            try
            {
                return ExecuteSqlNonquery(sql);
            }
            catch (Exception ex)
            {
                Log.LogInfo("删除数据", ex.Message);
            }
            return false;
        }

        #region  执行查询语句，返回DataSet
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <returns>DataSet</returns>
        public DataSet RefreshDataSet()
        {
            string sql = string.Format("select * from {0} order by CreateTime  desc", TableName);
            DataSet ds = new DataSet();
            try
            {
                ds.Clear();
                ds = Query(sql);
            }
            catch (SQLiteException ex)
            {
                Log.LogInfo("插查询数据库出错", ex.Message);
            }
            return ds;
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sqlString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet Query(string sqlString, params SQLiteParameter[] cmdParms)
        {
            using (SQLiteConnection connection = new SQLiteConnection(Soft.ConnectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand();
                PrepareCommand(cmd, connection, null, sqlString, cmdParms);
                using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "Skin");
                        cmd.Parameters.Clear();
                    }
                    catch (SQLiteException ex)
                    {
                    }
                    return ds;
                }
            }
        }
        public static void PrepareCommand(SQLiteCommand cmd, SQLiteConnection conn, SQLiteTransaction trans, string cmdText, SQLiteParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SQLiteParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput
                        || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }
        #endregion

        #region 执行查询语句，返回DataTable

        public List<Skin> AllSkins()
        {
            var list = new List<Skin>();
            var datetable = GetDataTable();
            if (datetable != null)
                list = GetAllList(datetable);
            return list;
        }
        public List<Skin> HeroSkins(string heroname)
        {
            var datetable = GetDataTable(heroname);
            var list = GetAllList(datetable);
            return list;
        }
        public List<string> GetHeros()
        {
            var datetable = GetHeroTable();
            var list = GetHeroList(datetable);
            return list;
        }
        /// <summary>
        /// 执行查询语句，返回DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTable(string heroname)
        {
            string sql = "select * from " + TableName + " where hero='" + heroname + "' order by CreateTime  desc";
            DataSet ds = new DataSet();
            try
            {
                ds = Query(sql);
            }
            catch (SQLiteException ex)
            {
                throw new Exception(ex.Message);
            }
            return ds.Tables[TableName];
        }
        /// <summary>
        /// 执行查询语句，返回DataTable
        /// </summary>
        /// <returns></returns>
        public DataTable GetDataTable()
        {
            string sql = "select * from " + TableName + " order by CreateTime  desc";
            DataSet ds = new DataSet();
            try
            {
                ds = Query(sql);
            }
            catch (SQLiteException ex)
            {
                throw new Exception(ex.Message);
            }
            return ds.Tables[TableName];
        }
        public DataTable GetHeroTable()
        {
            string sql = "select Hero from " + TableName + " order by CreateTime  desc";
            DataSet ds = new DataSet();
            try
            {
                ds = Query(sql);
            }
            catch (SQLiteException ex)
            {
                throw new Exception(ex.Message);
            }
            return ds.Tables[TableName];
        }
        /// <summary>
        /// 将DateTable装换成泛型集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="table"></param>
        /// <returns></returns>
        public List<Skin> GetAllList(DataTable table)
        {
            List<Skin> list = new List<Skin>();
            Skin t = default(Skin);
            PropertyInfo[] propertypes = null;
            string tempName = string.Empty;
            foreach (DataRow row in table.Rows)
            {
                t = Activator.CreateInstance<Skin>();
                propertypes = t.GetType().GetProperties();
                foreach (PropertyInfo pro in propertypes)
                {
                    tempName = pro.Name;
                    if (table.Columns.Contains(tempName))
                    {
                        object value = row[tempName];
                        if (!value.ToString().Equals(""))
                        {
                            pro.SetValue(t, value, null);
                        }
                    }
                }
                list.Add(t);
            }
            return list.Count == 0 ? new List<Skin>() : list;
        }
        public List<string> GetHeroList(DataTable table)
        {
            var list = new List<string>();
            var t = "";
            PropertyInfo[] propertypes = null;
            string tempName = string.Empty;
            foreach (DataRow row in table.Rows)
            {
                t = "";
                propertypes = t.GetType().GetProperties();
                foreach (PropertyInfo pro in propertypes)
                {
                    tempName = pro.Name;
                    if (table.Columns.Contains(tempName))
                    {
                        object value = row[tempName];
                        if (!value.ToString().Equals(""))
                        {
                            pro.SetValue(t, value, null);
                        }
                    }
                }
                list.Add(t);
            }
            return list.Count == 0 ? null : list;
        }
        /// <summary>
        /// 将泛型集合转换成DateTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public DataSet ConvertToDataSet<T>(IList<T> list)
        {
            if (list == null || list.Count <= 0)
            {
                return null;
            }

            DataSet ds = new DataSet();
            DataTable dt = new DataTable(typeof(T).Name);
            DataColumn column;
            DataRow row;

            PropertyInfo[] myPropertyInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (T t in list)
            {
                if (t == null)
                {
                    continue;
                }

                row = dt.NewRow();

                for (int i = 0, j = myPropertyInfo.Length; i < j; i++)
                {
                    PropertyInfo pi = myPropertyInfo[i];

                    string name = pi.Name;

                    if (dt.Columns[name] == null)
                    {
                        column = new DataColumn(name, pi.PropertyType);
                        dt.Columns.Add(column);
                    }

                    row[name] = pi.GetValue(t, null);
                }

                dt.Rows.Add(row);
            }

            ds.Tables.Add(dt);

            return ds;
        }
        #endregion

        #region
        public bool CheckAmSkins()
        {
            string sql = "select * from AMSkins  order by CreateTime  desc";
            DataSet ds = new DataSet();
            try
            {
                ds = QueryAmSkins(sql);
                return ds.Tables["AMSkins"] != null;
            }
            catch (SQLiteException ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 获取所有的旧数据
        /// </summary>
        /// <returns></returns>
        public List<Skin> AmSkinList()
        {
            var list = new List<Skin>();
            string sql = "select * from AMSkins  order by CreateTime  desc";
            DataSet ds = new DataSet();
            try
            {
                ds = QueryAmSkins(sql);
                if (ds.Tables["AMSkins"] != null)
                    list = GetAllList(ds.Tables["AMSkins"]);
            }
            catch (SQLiteException ex)
            {
                throw new Exception(ex.Message);
            }
            return list;
        }
        public bool DeleAmSkin()
        {
            string sql = "DROP TABLE AMSkins";
            try
            {
                return ExecuteSqlNonquery(sql);
            }
            catch (SQLiteException ex)
            {
                return false;
            }

        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="sqlString">查询语句</param>
        /// <returns>DataSet</returns>
        public static DataSet QueryAmSkins(string sqlString, params SQLiteParameter[] cmdParms)
        {
            using (SQLiteConnection connection = new SQLiteConnection(Soft.ConnectionString))
            {
                SQLiteCommand cmd = new SQLiteCommand();
                PrepareCommand(cmd, connection, null, sqlString, cmdParms);
                using (SQLiteDataAdapter da = new SQLiteDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "AMSkins");
                        cmd.Parameters.Clear();
                    }
                    catch (SQLiteException ex)
                    {
                       
                    }
                    return ds;
                }
            }
        }
        #endregion
    }
}
