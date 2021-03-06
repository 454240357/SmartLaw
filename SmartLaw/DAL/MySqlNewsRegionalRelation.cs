﻿using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SmartLaw.DBUtility;
using MySql.Data.MySqlClient;//Please add references
namespace SmartLaw.DAL
{
	/// <summary>
	/// 数据访问类:NewsRegionalRelation
	/// </summary>
	public partial class MySqlNewsRegionalRelation
	{
        public MySqlNewsRegionalRelation()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long AutoID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from NewsRegionalRelation");
			strSql.Append(" where AutoID=@AutoID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@AutoID", MySqlDbType.Int64)			};
			parameters[0].Value = AutoID;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(SmartLaw.Model.NewsRegionalRelation model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into NewsRegionalRelation(");
			strSql.Append("AutoID,NewsID,RegionalID)");
			strSql.Append(" values (");
			strSql.Append("@AutoID,@NewsID,@RegionalID)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@AutoID", MySqlDbType.Int64),
					new MySqlParameter("@NewsID", MySqlDbType.Int64),
					new MySqlParameter("@RegionalID", MySqlDbType.VarChar,50)};
			parameters[0].Value = model.AutoID;
			parameters[1].Value = model.NewsID;
			parameters[2].Value = model.RegionalID;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
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
		/// 更新一条数据
		/// </summary>
		public bool Update(SmartLaw.Model.NewsRegionalRelation model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update NewsRegionalRelation set ");
			strSql.Append("NewsID=@NewsID,");
			strSql.Append("RegionalID=@RegionalID");
			strSql.Append(" where AutoID=@AutoID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@NewsID", MySqlDbType.Int64),
					new MySqlParameter("@RegionalID", MySqlDbType.VarChar,50),
					new MySqlParameter("@AutoID", MySqlDbType.Int64)};
			parameters[0].Value = model.NewsID;
			parameters[1].Value = model.RegionalID;
			parameters[2].Value = model.AutoID;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
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
		public bool Delete(long AutoID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from NewsRegionalRelation ");
			strSql.Append(" where AutoID=@AutoID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@AutoID", MySqlDbType.Int64)			};
			parameters[0].Value = AutoID;

			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString(),parameters);
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
        /// 批量删除数据,返回成功删除的条目数
		/// </summary>
        public int DeleteList(string[] AutoIDlist)
		{
			StringBuilder strSql=new StringBuilder();
            StringBuilder _AutoIDlist = new StringBuilder();
            int i = 0;
            while (i < AutoIDlist.Length)
            {
                _AutoIDlist.Append("'" + AutoIDlist[i] + "'").Append(",");
                i++;
            }
            _AutoIDlist.Remove(_AutoIDlist.Length - 1, 1);
			strSql.Append("delete from NewsRegionalRelation ");
            strSql.Append(" where AutoID in (" + _AutoIDlist + ")  ");
			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString());
			return rows;
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SmartLaw.Model.NewsRegionalRelation GetModel(long AutoID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select AutoID,NewsID,RegionalID from NewsRegionalRelation");
			strSql.Append(" where AutoID=@AutoID limit 1");
			MySqlParameter[] parameters = {
					new MySqlParameter("@AutoID", MySqlDbType.Int64)			};
			parameters[0].Value = AutoID;

			SmartLaw.Model.NewsRegionalRelation model=new SmartLaw.Model.NewsRegionalRelation();
			DataSet ds=DbHelperMySQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
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
		public SmartLaw.Model.NewsRegionalRelation DataRowToModel(DataRow row)
		{
			SmartLaw.Model.NewsRegionalRelation model=new SmartLaw.Model.NewsRegionalRelation();
			if (row != null)
			{
				if(row["AutoID"]!=null && row["AutoID"].ToString()!="")
				{
					model.AutoID=long.Parse(row["AutoID"].ToString());
				}
				if(row["NewsID"]!=null && row["NewsID"].ToString()!="")
				{
					model.NewsID=long.Parse(row["NewsID"].ToString());
				}
				if(row["RegionalID"]!=null)
				{
					model.RegionalID=row["RegionalID"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
        private DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select AutoID,NewsID,RegionalID ");
			strSql.Append(" FROM NewsRegionalRelation ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperMySQL.Query(strSql.ToString());
		}


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, params MySqlParameter[] cmdParms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AutoID,NewsID,RegionalID ");
            strSql.Append(" FROM NewsRegionalRelation ");
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			strSql.Append(" AutoID,NewsID,RegionalID ");
			strSql.Append(" FROM NewsRegionalRelation ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
        /// <param name="key">查询条件 0：AutoID 1:NewsID 2：RegionalID 其他：全部</param>
        /// <param name="value">查询值</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value)
        {
            return GetList(key, value, -1, -1, false);
        }

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1:NewsID 2：RegionalID 其他：全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1:NewsID 2：RegionalID 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" AutoID,NewsID,RegionalID");
            strSql.Append(" FROM NewsRegionalRelation ");

            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "NewsID = @NewsID";
                    parameter = new MySqlParameter("@NewsID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 2: strWhere = "RegionalID = @RegionalID";
                    parameter = new MySqlParameter("@RegionalID", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                default: break;
            }
            if (strWhere.Trim() != "")
                strSql.Append(" where " + strWhere);

            string strOrder = "";
            switch (filedOrder)
            {
                case 0: strOrder = "AutoID"; break;
                case 1: strOrder = "NewsID"; break;
                case 2: strOrder = "RegionalID"; break;
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
            strSql.Append("select count(1) FROM NewsRegionalRelation ");
            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "NewsID = @NewsID";
                    parameter = new MySqlParameter("@NewsID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 2: strWhere = "RegionalID = @RegionalID";
                    parameter = new MySqlParameter("@RegionalID", MySqlDbType.VarChar, 50);
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
        /// <param name="key">查询条件 0：AutoID 1：NewsID 2：RegionalID 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1：NewsID 2：RegionalID 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="endIndex">结束索引</param>
        /// <returns>数据集</returns>
        public DataSet GetListByPage(int key, string value, int filedOrder, bool desc, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            string strOrder = "";
            switch (filedOrder)
            {
                case 0: strOrder = "AutoID"; break;
                case 1: strOrder = "NewsID"; break;
                case 2: strOrder = "RegionalID"; break;
                default: break;
            }

            if (!string.IsNullOrEmpty(strOrder.Trim()))
            {
                strSql.Append("order by T." + strOrder);
            }
            else
            {
                strSql.Append("order by T.AutoID desc");
            }
            strSql.Append(")AS Row, T.*  from NewsRegionalRelation T ");


            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "NewsID = @NewsID";
                    parameter = new MySqlParameter("@NewsID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 2: strWhere = "RegionalID = @RegionalID";
                    parameter = new MySqlParameter("@RegionalID", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                default: break;
            }
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperMySQL.Query(strSql.ToString());
        }



        /// <summary>
        /// 根据条件查询每个村所对应菜单看到的新闻
        /// </summary>
        /// <param name="regionalId"></param>区域编码
        /// <param name="categoryId"></param>菜单编码
        /// <returns></returns>
        public DataSet getNewsList(string regionalIds, int categoryId,bool desc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT A.NEWSID,B.LastModifyTime FROM  NEWSREGIONALRELATION A, NEWS B WHERE 1=1 ");
            strSql.Append("AND A.NEWSID=B.AUTOID ");
            strSql.Append("AND A.REGIONALID in ('" + regionalIds + "') ");
            strSql.Append("AND B.CATEGORYID=@categoryId ");
            strSql.Append("AND B.ISVALID=1 ");
            strSql.Append("AND B.CHECKED=1 ");
            strSql.Append("Order by B.LastModifyTime");
            if (desc)
            {
                strSql.Append(" desc");
            }
            MySqlParameter[] parameters = { 
					new MySqlParameter("@categoryId", MySqlDbType.Int64),
                                          };
             
            parameters[0].Value = categoryId;

            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            return ds;
        }







		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

