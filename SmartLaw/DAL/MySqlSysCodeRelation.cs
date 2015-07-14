using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SmartLaw.DBUtility;
using MySql.Data.MySqlClient;//Please add references
namespace SmartLaw.DAL
{
	/// <summary>
	/// 数据访问类:SysCodeRelation
	/// </summary>
	public partial class MySqlSysCodeRelation
	{
        public MySqlSysCodeRelation()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long AutoID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SysCodeRelation");
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
		public long Add(SmartLaw.Model.SysCodeRelation model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SysCodeRelation(");
			strSql.Append("SysCodeDetialID,SysCodeDetialIDEx)");
			strSql.Append(" values (");
			strSql.Append("@SysCodeDetialID,@SysCodeDetialIDEx)");
			strSql.Append(";select @@IDENTITY");
			MySqlParameter[] parameters = {
					new MySqlParameter("@SysCodeDetialID", MySqlDbType.VarChar,50),
					new MySqlParameter("@SysCodeDetialIDEx", MySqlDbType.VarChar,50)};
			parameters[0].Value = model.SysCodeDetialID;
			parameters[1].Value = model.SysCodeDetialIDEx;

			object obj = DbHelperMySQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt64(obj);
			}
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(SmartLaw.Model.SysCodeRelation model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SysCodeRelation set ");
			strSql.Append("SysCodeDetialID=@SysCodeDetialID,");
			strSql.Append("SysCodeDetialIDEx=@SysCodeDetialIDEx");
			strSql.Append(" where AutoID=@AutoID");
			MySqlParameter[] parameters = {
					new MySqlParameter("@SysCodeDetialID", MySqlDbType.VarChar,50),
					new MySqlParameter("@SysCodeDetialIDEx", MySqlDbType.VarChar,50),
					new MySqlParameter("@AutoID", MySqlDbType.Int64)};
			parameters[0].Value = model.SysCodeDetialID;
			parameters[1].Value = model.SysCodeDetialIDEx;
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
			strSql.Append("delete from SysCodeRelation ");
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
        public int DeleteList(string[] AutoIDlist)
		{
			StringBuilder strSql=new StringBuilder();
            StringBuilder _AutoIDlist = new StringBuilder();
            int i = 0;
            while (i < AutoIDlist.Length)
            {
                _AutoIDlist.Append(AutoIDlist[i]).Append(",");
                i++;
            }
            _AutoIDlist.Remove(_AutoIDlist.Length - 1, 1); 
            strSql.Append("delete from SysCodeRelation ");
            strSql.Append(" where AutoID in (" + _AutoIDlist + ")  ");
			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString());
            return rows;
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SmartLaw.Model.SysCodeRelation GetModel(long AutoID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select AutoID,SysCodeDetialID,SysCodeDetialIDEx from SysCodeRelation");
			strSql.Append(" where AutoID=@AutoID limit 1");
			MySqlParameter[] parameters = {
					new MySqlParameter("@AutoID", MySqlDbType.Int64)
			};
			parameters[0].Value = AutoID;

			SmartLaw.Model.SysCodeRelation model=new SmartLaw.Model.SysCodeRelation();
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
		public SmartLaw.Model.SysCodeRelation DataRowToModel(DataRow row)
		{
			SmartLaw.Model.SysCodeRelation model=new SmartLaw.Model.SysCodeRelation();
			if (row != null)
			{
				if(row["AutoID"]!=null && row["AutoID"].ToString()!="")
				{
					model.AutoID=long.Parse(row["AutoID"].ToString());
				}
				if(row["SysCodeDetialID"]!=null)
				{
					model.SysCodeDetialID=row["SysCodeDetialID"].ToString();
				}
				if(row["SysCodeDetialIDEx"]!=null)
				{
					model.SysCodeDetialIDEx=row["SysCodeDetialIDEx"].ToString();
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
			strSql.Append("select AutoID,SysCodeDetialID,SysCodeDetialIDEx ");
			strSql.Append(" FROM SysCodeRelation ");
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
            strSql.Append("select AutoID,SysCodeDetialID,SysCodeDetialIDEx ");
            strSql.Append(" FROM SysCodeRelation ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString(), cmdParms);
        }

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		private DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			strSql.Append(" AutoID,SysCodeDetialID,SysCodeDetialIDEx ");
			strSql.Append(" FROM SysCodeRelation ");
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
		/// 获取记录总数
		/// </summary>
		private int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM SysCodeRelation ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperMySQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
        private DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.AutoID desc");
			}
			strSql.Append(")AS Row, T.*  from SysCodeRelation T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperMySQL.Query(strSql.ToString());
		}

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1:SysCodeDetialID 2：SysCodeDetialIDEx 其他：全部</param>
        /// <param name="value">查询值</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value)
        {
            return GetList(key, value, -1, -1, false);
        }

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1:SysCodeDetialID 2：SysCodeDetialIDEx 其他：全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1:SysCodeDetialID 2：SysCodeDetialIDEx 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" AutoID,SysCodeDetialID,SysCodeDetialIDEx");
            strSql.Append(" FROM SysCodeRelation ");

            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "SysCodeDetialID = @SysCodeDetialID";
                    parameter = new MySqlParameter("@SysCodeDetialID", MySqlDbType.VarChar, 50);
                    parameter.Value =  value ;
                    break;
                case 2: strWhere = "SysCodeDetialIDEx = @SysCodeDetialIDEx";
                    parameter = new MySqlParameter("@SysCodeDetialIDEx", MySqlDbType.VarChar, 50);
                    parameter.Value =  value ;
                    break;
                default: break;
            }
            if (strWhere.Trim() != "")
                strSql.Append(" where " + strWhere);

            string strOrder = "";
            switch (filedOrder)
            {
                case 0: strOrder = "AutoID"; break;
                case 1: strOrder = "SysCodeDetialID"; break;
                case 2: strOrder = "SysCodeDetialIDEx"; break;
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
            strSql.Append("select count(1) FROM SysCodeRelation ");
            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "SysCodeDetialID = @SysCodeDetialID";
                    parameter = new MySqlParameter("@SysCodeDetialID", MySqlDbType.VarChar, 50);
                    parameter.Value =  value;
                    break;
                case 2: strWhere = "SysCodeDetialIDEx = @SysCodeDetialIDEx";
                    parameter = new MySqlParameter("@SysCodeDetialIDEx", MySqlDbType.VarChar, 50);
                    parameter.Value =  value;
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
        /// <param name="key">查询条件 0：AutoID 1：SysCodeDetialID 2：SysCodeDetialIDEx 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1：SysCodeDetialID 2：SysCodeDetialIDEx 其他:不排序</param>
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
                case 1: strOrder = "SysCodeDetialID"; break;
                case 2: strOrder = "SysCodeDetialIDEx"; break;
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
            strSql.Append(")AS Row, T.*  from SysCodeRelation T ");


            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "SysCodeDetialID = @SysCodeDetialID";
                    parameter = new MySqlParameter("@SysCodeDetialID", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 2: strWhere = "SysCodeDetialIDEx = @SysCodeDetialIDEx";
                    parameter = new MySqlParameter("@SysCodeDetialIDEx", MySqlDbType.VarChar, 50);
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
		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			MySqlParameter[] parameters = {
					new MySqlParameter("@tblName", MySqlDbType.VarChar, 255),
					new MySqlParameter("@fldName", MySqlDbType.VarChar, 255),
					new MySqlParameter("@PageSize", MySqlDbType.Int32),
					new MySqlParameter("@PageIndex", MySqlDbType.Int32),
					new MySqlParameter("@IsReCount", MySqlDbType.Bit),
					new MySqlParameter("@OrderType", MySqlDbType.Bit),
					new MySqlParameter("@strWhere", MySqlDbType.VarChar,1000),
					};
			parameters[0].Value = "SysCodeRelation";
			parameters[1].Value = "AutoID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperMySQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod
        /// <summary>
        /// 得到代码关联实体(顺序无关)
        /// </summary>
        /// <param name="SysCodeDetialID">小类编号1</param>
        /// <param name="SysCodeDetialIDEx">小类编号2</param>
        public SmartLaw.Model.SysCodeRelation GetModel(string SysCodeDetialID, string SysCodeDetialIDEx)
        { 
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AutoID,SysCodeDetialID,SysCodeDetialIDEx from SysCodeRelation");
            strSql.Append(" where (SysCodeDetialID=@SysCodeDetialID and SysCodeDetialIDEx=@SysCodeDetialIDEx) or " +
            " (SysCodeDetialID=@SysCodeDetialIDEx  and SysCodeDetialIDEx=@SysCodeDetialID) limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@SysCodeDetialID", MySqlDbType.VarChar, 50),
                    new MySqlParameter("@SysCodeDetialIDEx", MySqlDbType.VarChar, 50)
			};
            parameters[0].Value = SysCodeDetialID;
            parameters[1].Value = SysCodeDetialIDEx;
            SmartLaw.Model.SysCodeRelation model = new SmartLaw.Model.SysCodeRelation();
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
		#endregion  ExtensionMethod
	}
}

