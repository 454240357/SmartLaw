using System;
using System.Data;
using System.Text;
using SmartLaw.DBUtility;
using MySql.Data.MySqlClient;//Please add references
namespace SmartLaw.DAL
{
	/// <summary>
	/// 数据访问类:SYSCODE
	/// </summary>
    public partial class MySqlSysCode
	{
		public MySqlSysCode()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string SYSCodeID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_SYSCODE");
			strSql.Append(" where SYSCodeID=@SYSCodeID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@SYSCodeID", MySqlDbType.VarChar,50)			};
			parameters[0].Value = SYSCodeID;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(SmartLaw.Model.SysCode model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_SYSCODE(");
			strSql.Append("SYSCodeID,SYSCodeContext,LastModifytime,ISValid,Memo)");
			strSql.Append(" values (");
			strSql.Append("@SYSCodeID,@SYSCodeContext,@LastModifytime,@ISValid,@Memo)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@SYSCodeID", MySqlDbType.VarChar,50),
					new MySqlParameter("@SYSCodeContext", MySqlDbType.VarChar,50),
					new MySqlParameter("@LastModifytime", MySqlDbType.DateTime),
					new MySqlParameter("@ISValid", MySqlDbType.Bit,1),
					new MySqlParameter("@Memo", MySqlDbType.VarChar,50)};
			parameters[0].Value = model.SYSCodeID;
			parameters[1].Value = model.SYSCodeContext;
			parameters[2].Value = model.LastModifytime;
			parameters[3].Value = model.ISValid;
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
		public bool Update(SmartLaw.Model.SysCode model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_SYSCODE set ");
			strSql.Append("SYSCodeContext=@SYSCodeContext,");
			strSql.Append("LastModifytime=@LastModifytime,");
			strSql.Append("ISValid=@ISValid,");
			strSql.Append("Memo=@Memo");
			strSql.Append(" where SYSCodeID=@SYSCodeID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@SYSCodeContext", MySqlDbType.VarChar,50),
					new MySqlParameter("@LastModifytime", MySqlDbType.DateTime),
					new MySqlParameter("@ISValid", MySqlDbType.Bit,1),
					new MySqlParameter("@Memo", MySqlDbType.VarChar,50),
					new MySqlParameter("@SYSCodeID", MySqlDbType.VarChar,50)};
			parameters[0].Value = model.SYSCodeContext;
			parameters[1].Value = model.LastModifytime;
			parameters[2].Value = model.ISValid;
			parameters[3].Value = model.Memo;
			parameters[4].Value = model.SYSCodeID;

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
		public bool Delete(string SYSCodeID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_SYSCODE ");
			strSql.Append(" where SYSCodeID=@SYSCodeID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@SYSCodeID", MySqlDbType.VarChar,50)			};
			parameters[0].Value = SYSCodeID;

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
		public int DeleteList(string[] SYSCodeIDlist )
		{
			StringBuilder strSql=new StringBuilder();
            StringBuilder _SYSCodeIDlist = new StringBuilder();
            int i=0;
            while (i < SYSCodeIDlist.Length)
            {
                _SYSCodeIDlist.Append("'"+SYSCodeIDlist[i]+"'").Append(",");
                i++;
            }
            _SYSCodeIDlist.Remove(_SYSCodeIDlist.Length - 1, 1);
			strSql.Append("delete from T_SYSCODE ");
			strSql.Append(" where SYSCodeID in ("+_SYSCodeIDlist + ")  ");
			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString());
            return rows;
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SmartLaw.Model.SysCode GetModel(string SYSCodeID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select SYSCodeID,SYSCodeContext,LastModifytime,ISValid,Memo from T_SYSCODE");
			strSql.Append(" where SYSCodeID=@SYSCodeID limit 1");
			MySqlParameter[] parameters = {
					new MySqlParameter("@SYSCodeID", MySqlDbType.VarChar,50)			};
			parameters[0].Value = SYSCodeID;

			SmartLaw.Model.SysCode model=new SmartLaw.Model.SysCode();
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
		public SmartLaw.Model.SysCode DataRowToModel(DataRow row)
		{
			SmartLaw.Model.SysCode model=new SmartLaw.Model.SysCode();
			if (row != null)
			{
				if(row["SYSCodeID"]!=null)
				{
					model.SYSCodeID=row["SYSCodeID"].ToString();
				}
				if(row["SYSCodeContext"]!=null)
				{
					model.SYSCodeContext=row["SYSCodeContext"].ToString();
				}
				if(row["LastModifytime"]!=null && row["LastModifytime"].ToString()!="")
				{
					model.LastModifytime=DateTime.Parse(row["LastModifytime"].ToString());
				}
				if(row["ISValid"]!=null && row["ISValid"].ToString()!="")
				{
					if((row["ISValid"].ToString()=="1")||(row["ISValid"].ToString().ToLower()=="true"))
					{
						model.ISValid=true;
					}
					else
					{
						model.ISValid=false;
					}
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
			strSql.Append("select SYSCodeID,SYSCodeContext,LastModifytime,ISValid,Memo ");
			strSql.Append(" FROM T_SYSCODE ");
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
            strSql.Append("select SYSCodeID,SYSCodeContext,LastModifytime,ISValid,Memo ");
            strSql.Append(" FROM T_SYSCODE ");
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
			strSql.Append(" SYSCodeID,SYSCodeContext,LastModifytime,ISValid,Memo ");
			strSql.Append(" FROM T_SYSCODE ");
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
			parameters[0].Value = "T_SYSCODE";
			parameters[1].Value = "SYSCodeID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperMySQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/
        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：SYSCodeID 1：SYSCodeContext 2：LastModifytime 3：ISValid 4：Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value)
        {
            return GetList(key, value, -1, -1, false);
        }

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：SYSCodeID 1：SYSCodeContext 2：LastModifytime 3：ISValid 4：Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：SYSCodeID 1：SYSCodeContext 2：LastModifytime 3：ISValid 4：Memo 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" SYSCodeID,SYSCodeContext,LastModifytime,ISValid,Memo ");
            strSql.Append(" FROM T_SYSCODE ");

            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "SYSCodeID like @SYSCodeID";
                    parameter = new MySqlParameter("@SYSCodeID", MySqlDbType.VarChar, 50);
                    parameter.Value = "%" + value + "%";
                    break;
                case 1: strWhere = "SYSCodeContext like @SYSCodeContext";
                    parameter = new MySqlParameter("@SYSCodeContext", MySqlDbType.VarChar, 50);
                    parameter.Value = "%" + value + "%";
                    break;
                case 2: strWhere = "LastModifytime = @LastModifytime";
                    parameter = new MySqlParameter("@LastModifytime", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 3: strWhere = "ISValid = @ISValid";
                    parameter = new MySqlParameter("@ISValid", MySqlDbType.Bit, 1);
                    parameter.Value = value;
                    break;
                case 4: strWhere = "Memo like @Memo";
                    parameter = new MySqlParameter("@Memo", MySqlDbType.VarChar, 50);
                    parameter.Value = "%" + value + "%";
                    break;
                default: break;
            }
            if (strWhere.Trim() != "")
                strSql.Append(" where " + strWhere);

            string strOrder = "";
            switch (filedOrder)
            {
                case 0: strOrder = "SYSCodeID"; break;
                case 1: strOrder = "SYSCodeContext"; break;
                case 2: strOrder = "LastModifytime"; break;
                case 3: strOrder = "ISValid"; break;
                case 4: strOrder = "Memo"; break;
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
            strSql.Append("select count(1) FROM T_SYSCODE ");
            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "SYSCodeID like @SYSCodeID";
                    parameter = new MySqlParameter("@SYSCodeID", MySqlDbType.VarChar, 50);
                    parameter.Value = "%" + value + "%";
                    break;
                case 1: strWhere = "SYSCodeContext like @SYSCodeContext";
                    parameter = new MySqlParameter("@SYSCodeContext", MySqlDbType.VarChar, 50);
                    parameter.Value = "%" + value + "%";
                    break;
                case 2: strWhere = "LastModifytime = @LastModifytime";
                    parameter = new MySqlParameter("@LastModifytime", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 3: strWhere = "ISValid = @ISValid";
                    parameter = new MySqlParameter("@ISValid", MySqlDbType.Bit, 1);
                    parameter.Value = value;
                    break;
                case 4: strWhere = "Memo like @Memo";
                    parameter = new MySqlParameter("@Memo", MySqlDbType.VarChar, 50);
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
        /// <param name="key">查询条件 0：SYSCodeID 1：SYSCodeContext 2：LastModifytime 3：ISValid 4：Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="filedOrder">排序字段 0：SYSCodeID 1：SYSCodeContext 2：LastModifytime 3：ISValid 4：Memo 其他:不排序</param>
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
                case 0: strOrder = "SYSCodeID"; break;
                case 1: strOrder = "SYSCodeContext"; break;
                case 2: strOrder = "LastModifytime"; break;
                case 3: strOrder = "ISValid"; break;
                case 4: strOrder = "Memo"; break;
                default: break;
            }

            if (!string.IsNullOrEmpty(strOrder.Trim()))
            {
                strSql.Append("order by T." + strOrder);
            }
            else
            {
                strSql.Append("order by T.SYSCodeID desc");
            }
            strSql.Append(")AS Row, T.*  from T_SYSCODE T ");


            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "SYSCodeID like @SYSCodeID";
                    parameter = new MySqlParameter("@SYSCodeID", MySqlDbType.VarChar, 50);
                    parameter.Value = "%" + value + "%";
                    break;
                case 1: strWhere = "SYSCodeContext like @SYSCodeContext";
                    parameter = new MySqlParameter("@SYSCodeContext", MySqlDbType.VarChar, 50);
                    parameter.Value = "%" + value + "%";
                    break;
                case 2: strWhere = "LastModifytime = @LastModifytime";
                    parameter = new MySqlParameter("@LastModifytime", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 3: strWhere = "ISValid = @ISValid";
                    parameter = new MySqlParameter("@ISValid", MySqlDbType.Bit, 1);
                    parameter.Value = value;
                    break;
                case 4: strWhere = "Memo like @Memo";
                    parameter = new MySqlParameter("@Memo", MySqlDbType.VarChar, 50);
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

