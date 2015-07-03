using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SmartLaw.DBUtility;
using MySql.Data.MySqlClient;//Please add references
namespace SmartLaw.DAL
{
    /// <summary>
    /// 数据访问类:Questionary
    /// </summary>
    public partial class MySqlQuestionary
    {
        public MySqlQuestionary()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Questionary");
            strSql.Append(" where ID=@ID");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int64)
			};
            parameters[0].Value = ID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(SmartLaw.Model.Questionary model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Questionary(");
            strSql.Append("Title,IsValid)");
            strSql.Append(" values (");
            strSql.Append("@Title,@IsValid)");
            strSql.Append(";select @@IDENTITY");
            MySqlParameter[] parameters = { 
					new MySqlParameter("@Title", MySqlDbType.VarChar,64),
                    new MySqlParameter("@IsValid", MySqlDbType.Bit,1)
                                          };
          
            parameters[0].Value = model.Title;
            parameters[1].Value = model.IsValid;
            try
            {
                object obj = DbHelperMySQL.GetSingle(strSql.ToString(), parameters);
                return Convert.ToInt64(obj);
            }
            catch (Exception ee)
            {
                return 0;
            } 
        }



        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SmartLaw.Model.Questionary model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Questionary set "); 
            strSql.Append("Title=@Title,");
            strSql.Append("IsValid=@IsValid ");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = { 
					new MySqlParameter("@ID", MySqlDbType.Int64),
					new MySqlParameter("@Title", MySqlDbType.VarChar,64),
                    new MySqlParameter("@IsValid", MySqlDbType.Bit,1)};

            parameters[0].Value = model.ID;
            parameters[1].Value = model.Title;
            parameters[2].Value = model.IsValid; 
            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Questionary ");
            strSql.Append(" where ID=@ID  ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int64) 
			};
            parameters[0].Value = ID; 
            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        } 


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SmartLaw.Model.Questionary GetModel(long ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Title,IsValid from Questionary");
            strSql.Append(" where ID=@ID limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int64),
                  
			};
            parameters[0].Value = ID; 
            SmartLaw.Model.Questionary model = new SmartLaw.Model.Questionary();
            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]); 
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SmartLaw.Model.Questionary DataRowToModel(DataRow row)
        {
            SmartLaw.Model.Questionary model = new SmartLaw.Model.Questionary();

            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = long.Parse(row["ID"].ToString());
                } 
                if (row["Title"] != null)
                {
                    model.Title = row["Title"].ToString();
                }  
                if (row["IsValid"] != null && row["IsValid"].ToString() != "")
                {
                    if ((row["IsValid"].ToString() == "1") || (row["IsValid"].ToString().ToLower() == "true"))
                    {
                        model.IsValid = true;
                    }
                    else
                    {
                        model.IsValid = false;
                    }
                }
            }
            return model;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        private DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Title,IsValid");
            strSql.Append(" FROM Questionary ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, params MySqlParameter[] cmdParms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,Title,IsValid ");
            strSql.Append(" FROM Questionary ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString(), cmdParms);
        }


        /// <summary>
        /// 获得前几行数据
        /// </summary>
        private DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append("ID,Title,IsValid ");
            strSql.Append(" FROM Questionary ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            if (Top > 0)
            {
                strSql.Append(" limit " + Top.ToString());
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：ID, 1：Title,2:IsValid 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value)
        {
            return GetList(key, value, -1, -1, false);
        }

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：Title 2：IsValid 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1：Title 2：IsValid  其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" ID,Title,IsValid ");
            strSql.Append(" FROM Questionary "); 
            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "ID = @ID";
                    parameter = new MySqlParameter("@ID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break; 
                case 1: strWhere = "Title like @Title";
                    parameter = new MySqlParameter("@Title", MySqlDbType.VarChar);
                    parameter.Value = "%" + value + "%";
                    break;
                case 2: strWhere = "IsValid = @IsValid";
                    parameter = new MySqlParameter("@IsValid", MySqlDbType.Bit, 1);
                    parameter.Value = value;
                    break; 
                default: break;
            }
            if (strWhere.Trim() != "")
                strSql.Append(" where " + strWhere); 
            string strOrder = "";
            switch (filedOrder)
            {
                case 0: strOrder = "ID"; break;
                case 1: strOrder = "Title"; break;
                case 2: strOrder = "IsValid"; break;
                default: break;
            }
            if (strOrder.Trim() != "")
            {
                strSql.Append(" order by " + strOrder);
                if (desc)
                    strSql.Append(" desc");
            }
            if (Top > 0)
            {
                strSql.Append(" limit " + Top.ToString());
            }
            if (parameter != null)
                return DbHelperMySQL.Query(strSql.ToString(), parameter);
            else
                return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(int key, string value)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Questionary ");
            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "ID = @ID";
                    parameter = new MySqlParameter("@ID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break; 
                case 1: strWhere = "Title like @Title";
                    parameter = new MySqlParameter("@Title", MySqlDbType.VarChar);
                    parameter.Value = "%" + value + "%";
                    break;
                case 2: strWhere = "IsValid = @IsValid";
                    parameter = new MySqlParameter("@IsValid", MySqlDbType.Bit, 1);
                    parameter.Value = value;
                    break; 
                default: break;
            } 
            object obj = null;
            if (strWhere.Trim() != "")
                strSql.Append(" where " + strWhere);
            if (parameter != null)
                obj = DbHelperMySQL.GetSingle(strSql.ToString(), parameter);
            else
                obj = DbHelperMySQL.GetSingle(strSql.ToString());
            if (obj == null)
                return 0;
            else
                return Convert.ToInt32(obj);
        } 

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：ID 1：Title 2：IsValid 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="filedOrder">排序字段 0：ID 1：Title 2：IsValid  其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="endIndex">结束索引</param>
        /// <returns>数据集</returns>
        public DataSet GetListByPage(int key, string value, int filedOrder, bool desc, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT");
            strSql.Append(" ID,Title ,IsValid");
            strSql.Append(" FROM Questionary ");
            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "ID = @ID";
                    parameter = new MySqlParameter("@ID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "Title like @Title";
                    parameter = new MySqlParameter("@Title", MySqlDbType.VarChar, 64);
                    parameter.Value = "%" + value + "%";
                    break;
                case 2: strWhere = "IsValid = @IsValid";
                    parameter = new MySqlParameter("@IsValid", MySqlDbType.Bit, 1);
                    parameter.Value = value;
                    break;
                default: break;
            }
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            } 

            string strOrder = "";
            switch (filedOrder)
            {
                case 0: strOrder = "ID"; break;
                case 1: strOrder = "Title"; break;
                case 2: strOrder = "IsValid"; break;
                default: break;
            } 
            if (!string.IsNullOrEmpty(strOrder.Trim()))
            {
                if (desc)
                {
                    strSql.Append(" order by" + strOrder + " desc");
                }
                else
                {
                    strSql.Append(" order by" + strOrder);
                }
            }
            else
            {
                strSql.Append(" order by ID desc");
            }

            strSql.AppendFormat(" limit {0} , {1}", startIndex, endIndex);

            if (parameter != null)
                return DbHelperMySQL.Query(strSql.ToString(), parameter);
            else
                return DbHelperMySQL.Query(strSql.ToString());
        } 
        #endregion  BasicMethod 
    }
}

