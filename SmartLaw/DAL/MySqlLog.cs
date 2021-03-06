﻿using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using SmartLaw.DBUtility;//Please add references
namespace SmartLaw.DAL
{
	/// <summary>
	/// 数据访问类:log
	/// </summary>
	public partial class MySqlLog
	{
		public MySqlLog()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long AutoID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from log");
			strSql.Append(" where AutoID=@AutoID");
			MySqlParameter[] parameters = {
					new MySqlParameter("@AutoID", MySqlDbType.Int64)
			};
			parameters[0].Value = AutoID;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(SmartLaw.Model.Log model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into log(");
			strSql.Append("OperationItem,OperationTime,OperationDetail,Operator,Memo)");
			strSql.Append(" values (");
			strSql.Append("@OperationItem,@OperationTime,@OperationDetail,@Operator,@Memo)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@OperationItem", MySqlDbType.VarChar,50),
					new MySqlParameter("@OperationTime", MySqlDbType.DateTime),
					new MySqlParameter("@OperationDetail", MySqlDbType.Text),
					new MySqlParameter("@Operator", MySqlDbType.VarChar,50),
					new MySqlParameter("@Memo", MySqlDbType.Text)};
			parameters[0].Value = model.OperationItem;
			parameters[1].Value = model.OperationTime;
			parameters[2].Value = model.OperationDetail;
			parameters[3].Value = model.Operator;
			parameters[4].Value = model.Memo;

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
		public bool Update(SmartLaw.Model.Log model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update log set ");
			strSql.Append("OperationItem=@OperationItem,");
			strSql.Append("OperationTime=@OperationTime,");
			strSql.Append("OperationDetail=@OperationDetail,");
			strSql.Append("Operator=@Operator,");
			strSql.Append("Memo=@Memo");
			strSql.Append(" where AutoID=@AutoID");
			MySqlParameter[] parameters = {
					new MySqlParameter("@OperationItem", MySqlDbType.VarChar,50),
					new MySqlParameter("@OperationTime", MySqlDbType.DateTime),
					new MySqlParameter("@OperationDetail", MySqlDbType.Text),
					new MySqlParameter("@Operator", MySqlDbType.VarChar,50),
					new MySqlParameter("@Memo", MySqlDbType.Text),
					new MySqlParameter("@AutoID", MySqlDbType.Int64,20)};
			parameters[0].Value = model.OperationItem;
			parameters[1].Value = model.OperationTime;
			parameters[2].Value = model.OperationDetail;
			parameters[3].Value = model.Operator;
			parameters[4].Value = model.Memo;
			parameters[5].Value = model.AutoID;

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
			strSql.Append("delete from log ");
			strSql.Append(" where AutoID=@AutoID");
			MySqlParameter[] parameters = {
					new MySqlParameter("@AutoID", MySqlDbType.Int64)
			};
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
		public int DeleteList(string[] AutoIDlist )
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
			strSql.Append("delete from log ");
            strSql.Append(" where AutoID in (" + _AutoIDlist + ")  ");
			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString());
            return rows;
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SmartLaw.Model.Log GetModel(long AutoID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select AutoID,OperationItem,OperationTime,OperationDetail,Operator,Memo from log ");
			strSql.Append(" where AutoID=@AutoID");
			MySqlParameter[] parameters = {
					new MySqlParameter("@AutoID", MySqlDbType.Int64)
			};
			parameters[0].Value = AutoID;

			SmartLaw.Model.Log model=new SmartLaw.Model.Log();
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
		public SmartLaw.Model.Log DataRowToModel(DataRow row)
		{
			SmartLaw.Model.Log model=new SmartLaw.Model.Log();
			if (row != null)
			{
				if(row["AutoID"]!=null && row["AutoID"].ToString()!="")
				{
					model.AutoID=long.Parse(row["AutoID"].ToString());
				}
				if(row["OperationItem"]!=null)
				{
					model.OperationItem=row["OperationItem"].ToString();
				}
				if(row["OperationTime"]!=null && row["OperationTime"].ToString()!="")
				{
					model.OperationTime=DateTime.Parse(row["OperationTime"].ToString());
				}
				if(row["OperationDetail"]!=null)
				{
					model.OperationDetail=row["OperationDetail"].ToString();
				}
				if(row["Operator"]!=null)
				{
					model.Operator=row["Operator"].ToString();
				}
				if(row["Memo"]!=null)
				{
					model.Memo=row["Memo"].ToString();
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
			strSql.Append("select AutoID,OperationItem,OperationTime,OperationDetail,Operator,Memo ");
			strSql.Append(" FROM log ");
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
            strSql.Append("select AutoID,OperationItem,OperationTime,OperationDetail,Operator,Memo ");
            strSql.Append(" FROM log ");
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
            strSql.Append(" AutoID,OperationItem,OperationTime,OperationDetail,Operator,Memo ");
            strSql.Append(" FROM log ");
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
        /// <param name="key">查询条件 0：AutoID 1：OperationItem 2：OperationTime 3：OperationDetail 4：Operator 5:Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value)
        {
            return GetList(key, value, -1, -1, false);
        }

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：OperationItem 2：OperationTime 3：OperationDetail 4：Operator 5:Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1：OperationItem 2：OperationTime 3：OperationDetail 4：Operator 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" AutoID,OperationItem,OperationTime,OperationDetail,Operator,Memo ");
            strSql.Append(" FROM log ");

            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "OperationItem = @OperationItem";
                    parameter = new MySqlParameter("@OperationItem", MySqlDbType.VarChar, 50);
                    parameter.Value =  value ;
                    break;
                case 3: strWhere = "OperationDetail like @OperationDetail";
                    parameter = new MySqlParameter("@OperationDetail", MySqlDbType.Text);
                    parameter.Value = "%" + value + "%";
                    break;
                case 4: strWhere = "Operator = @Operator";
                    parameter = new MySqlParameter("@Operator", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 5: strWhere = "Memo like @Memo";
                    parameter = new MySqlParameter("@Memo", MySqlDbType.Text);
                    parameter.Value = "%" + value + "%";
                    break;
                default: break;
            }
            if (key == 2)
            {
                strWhere = "OperationTime between '" + value + " 00:00:00' and '" + value + " 23:59:59'";
            }
            if (strWhere.Trim() != "")
                strSql.Append(" where " + strWhere);
            
            string strOrder = "";
            switch (filedOrder)
            {
                case 0: strOrder = "AutoID"; break;
                case 1: strOrder = "OperationItem"; break;
                case 2: strOrder = "OperationTime"; break;
                case 3: strOrder = "OperationDetail"; break;
                case 4: strOrder = "Operator"; break;
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
            strSql.Append("select count(1) FROM log ");
            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "OperationItem = @OperationItem";
                    parameter = new MySqlParameter("@OperationItem", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 2: strWhere = "OperationTime = @OperationTime";
                    parameter = new MySqlParameter("@OperationTime", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 3: strWhere = "OperationDetail like @OperationDetail";
                    parameter = new MySqlParameter("@OperationDetail", MySqlDbType.Text);
                    parameter.Value = "%" + value + "%";
                    break;
                case 4: strWhere = "Operator = @Operator";
                    parameter = new MySqlParameter("@Operator", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 5: strWhere = "Memo like @Memo";
                    parameter = new MySqlParameter("@Memo", MySqlDbType.Text);
                    parameter.Value = "%" + value + "%";
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
        /// <param name="key">查询条件 0：AutoID 1：CategoryName 2：ParentCategoryID 3：Orders 4：LastModifyTime 5:IsValid 6:Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="filedOrder">排序字段 0：Orders 1：AutoID 2：ParentCategoryID 3：CategoryName 4：LastModifyTime 5:IsValid 其他:不排序</param>
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
                case 1: strOrder = "OperationItem"; break;
                case 2: strOrder = "OperationTime"; break;
                case 3: strOrder = "OperationDetail"; break;
                case 4: strOrder = "Operator"; break;
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
            strSql.Append(")AS Row, T.*  from log T ");


            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "OperationItem = @OperationItem";
                    parameter = new MySqlParameter("@OperationItem", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 2: strWhere = "OperationTime = @OperationTime";
                    parameter = new MySqlParameter("@OperationTime", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 3: strWhere = "OperationDetail like @OperationDetail";
                    parameter = new MySqlParameter("@OperationDetail", MySqlDbType.Text);
                    parameter.Value = "%" + value + "%";
                    break;
                case 4: strWhere = "Operator = @Operator";
                    parameter = new MySqlParameter("@Operator", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 5: strWhere = "Memo like @Memo";
                    parameter = new MySqlParameter("@Memo", MySqlDbType.Text);
                    parameter.Value = "%" + value + "%";
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
		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

