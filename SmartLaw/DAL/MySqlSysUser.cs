using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SmartLaw.DBUtility;
using MySql.Data.MySqlClient;//Please add references
namespace SmartLaw.DAL
{
	/// <summary>
	/// 数据访问类:SysUser
	/// </summary>
	public partial class MySqlSysUser
	{
        public MySqlSysUser()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string UserID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from SysUser");
			strSql.Append(" where UserID=@UserID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@UserID", MySqlDbType.VarChar,50)			};
			parameters[0].Value = UserID;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}

		/// <summary>
        /// 增加一条数据
		/// </summary>
		public bool Add(SmartLaw.Model.SysUser model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into SysUser(");
			strSql.Append("UserID,Password,EmployeeID,UserName,IsValid)");
			strSql.Append(" values (");
			strSql.Append("@UserID,@Password,@EmployeeID,@UserName,@IsValid)");
			MySqlParameter[] parameters = {
					new MySqlParameter("@UserID", MySqlDbType.VarChar,50),
					new MySqlParameter("@Password", MySqlDbType.VarChar,50),
					new MySqlParameter("@EmployeeID", MySqlDbType.VarChar,50),
					new MySqlParameter("@UserName", MySqlDbType.VarChar,50),
					new MySqlParameter("@IsValid", MySqlDbType.Bit,1)};
			parameters[0].Value = model.UserID;
			parameters[1].Value = model.Password;
			parameters[2].Value = model.EmployeeID;
			parameters[3].Value = model.UserName;
			parameters[4].Value = model.IsValid;

            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("insert into T_SYSCODEDETIAL(");
            strSql1.Append("SYSCodeID,SYSCodeDetialID,SYSCodeDetialContext,LastModifyTime,IsValid,Memo)");
            strSql1.Append(" values (");
            strSql1.Append("@SYSCodeID,@SYSCodeDetialID,@SYSCodeDetialContext,@LastModifyTime,@IsValid,@Memo)");
            MySqlParameter[] parameters1 = {
					new MySqlParameter("@SYSCodeID", MySqlDbType.VarChar,50),
					new MySqlParameter("@SYSCodeDetialID", MySqlDbType.VarChar,50),
					new MySqlParameter("@SYSCodeDetialContext", MySqlDbType.VarChar,50),
					new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime),
					new MySqlParameter("@IsValid", MySqlDbType.Bit,1),
					new MySqlParameter("@Memo", MySqlDbType.VarChar,50)};
            parameters1[0].Value = "SysUser";
            parameters1[1].Value = model.UserID;
            parameters1[2].Value = model.UserName;
            parameters1[3].Value = DateTime.Now;
            parameters1[4].Value = model.IsValid;
            parameters1[5].Value = "操作员";

            System.Collections.Hashtable sqlStringList=new System.Collections.Hashtable();
            sqlStringList.Add(strSql,parameters);
            sqlStringList.Add(strSql1,parameters1);

            try
            {
                DbHelperMySQL.ExecuteSqlTran(sqlStringList);
                return true;
            }
            catch
            {
                return false;
            }
		}

		/// <summary>
        /// 更新一条数据
		/// </summary>
		public bool Update(SmartLaw.Model.SysUser model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update SysUser set ");
			strSql.Append("Password=@Password,");
			strSql.Append("EmployeeID=@EmployeeID,");
			strSql.Append("UserName=@UserName,");
			strSql.Append("IsValid=@IsValid");
			strSql.Append(" where UserID=@UserID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@Password", MySqlDbType.VarChar,50),
					new MySqlParameter("@EmployeeID", MySqlDbType.VarChar,50),
					new MySqlParameter("@UserName", MySqlDbType.VarChar,50),
					new MySqlParameter("@IsValid", MySqlDbType.Bit,1),
					new MySqlParameter("@UserID", MySqlDbType.VarChar,50)};
			parameters[0].Value = model.Password;
			parameters[1].Value = model.EmployeeID;
			parameters[2].Value = model.UserName;
			parameters[3].Value = model.IsValid;
			parameters[4].Value = model.UserID;

            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("update T_SYSCODEDETIAL set ");
            strSql1.Append("SYSCodeID=@SYSCodeID,");
            strSql1.Append("SYSCodeDetialContext=@SYSCodeDetialContext,");
            strSql1.Append("LastModifyTime=@LastModifyTime,");
            strSql1.Append("IsValid=@IsValid,");
            strSql1.Append("Memo=@Memo");
            strSql1.Append(" where SYSCodeDetialID=@SYSCodeDetialID ");
            MySqlParameter[] parameters1 = {
					new MySqlParameter("@SYSCodeID", MySqlDbType.VarChar,50),
					new MySqlParameter("@SYSCodeDetialContext", MySqlDbType.VarChar,50),
					new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime),
					new MySqlParameter("@IsValid", MySqlDbType.Bit,1),
					new MySqlParameter("@Memo", MySqlDbType.VarChar,50),
					new MySqlParameter("@SYSCodeDetialID", MySqlDbType.VarChar,50)};
            parameters1[0].Value = "SysUser";
            parameters1[1].Value = model.UserName;
            parameters1[2].Value = DateTime.Now;
            parameters1[3].Value = model.IsValid;
            parameters1[4].Value = "操作员";
            parameters1[5].Value = model.UserID;

            System.Collections.Hashtable sqlStringList = new System.Collections.Hashtable();
            sqlStringList.Add(strSql, parameters);
            sqlStringList.Add(strSql1, parameters1);

            try
            {
                DbHelperMySQL.ExecuteSqlTran(sqlStringList);
                return true;
            }
            catch
            {
                return false;
            }
		}

		/// <summary>
        /// 删除一条数据
		/// </summary>
		public bool Delete(string UserID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from SysUser ");
			strSql.Append(" where UserID=@UserID ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@UserID", MySqlDbType.VarChar,50)			};
			parameters[0].Value = UserID;

            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from T_SYSCODEDETIAL ");
            strSql1.Append(" where SYSCodeDetialID=@SYSCodeDetialID ");
            MySqlParameter[] parameters1 = {
					new MySqlParameter("@SYSCodeDetialID", MySqlDbType.VarChar,50)			};
            parameters1[0].Value = UserID;

            System.Collections.Hashtable sqlStringList = new System.Collections.Hashtable();
            sqlStringList.Add(strSql, parameters);
            sqlStringList.Add(strSql1, parameters1);

            try
            {
                DbHelperMySQL.ExecuteSqlTran(sqlStringList);
                return true;
            }
            catch
            {
                return false;
            }
		}

		/// <summary>
        /// 批量删除数据
		/// </summary>
		public int DeleteList(string[] UserIDlist )
		{
			StringBuilder strSql=new StringBuilder();
            StringBuilder _UserIDlist = new StringBuilder();
            int i = 0;
            while (i < UserIDlist.Length)
            {
                _UserIDlist.Append("'"+UserIDlist[i]+"'").Append(",");
                i++;
            }
            _UserIDlist.Remove(_UserIDlist.Length - 1, 1);
			strSql.Append("delete from SysUser ");
            strSql.Append(" where UserID in (" + _UserIDlist + ")  ");

            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from T_SYSCODEDETIAL ");
            strSql1.Append(" where SYSCodeDetialID in (" + _UserIDlist + ")  ");

            System.Collections.Generic.List<string> sqlStringList = new System.Collections.Generic.List<string>();
            sqlStringList.Add(strSql.ToString());
            sqlStringList.Add(strSql1.ToString());
            int rows = DbHelperMySQL.ExecuteSqlTran(sqlStringList)/2;
            return rows;
		}

		/// <summary>
        /// 得到一条数据
		/// </summary>
		public SmartLaw.Model.SysUser GetModel(string UserID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select UserID,Password,EmployeeID,UserName,IsValid from SysUser ");
			strSql.Append(" where UserID=@UserID limit 1");
			MySqlParameter[] parameters = {
					new MySqlParameter("@UserID", MySqlDbType.VarChar,50)			};
			parameters[0].Value = UserID;

			SmartLaw.Model.SysUser model=new SmartLaw.Model.SysUser();
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
		public SmartLaw.Model.SysUser DataRowToModel(DataRow row)
		{
			SmartLaw.Model.SysUser model=new SmartLaw.Model.SysUser();
			if (row != null)
			{
				if(row["UserID"]!=null)
				{
					model.UserID=row["UserID"].ToString();
				}
				if(row["Password"]!=null)
				{
					model.Password=row["Password"].ToString();
				}
				if(row["EmployeeID"]!=null)
				{
					model.EmployeeID=row["EmployeeID"].ToString();
				}
				if(row["UserName"]!=null)
				{
					model.UserName=row["UserName"].ToString();
				}
				if(row["IsValid"]!=null && row["IsValid"].ToString()!="")
				{
					if((row["IsValid"].ToString()=="1")||(row["IsValid"].ToString().ToLower()=="true"))
					{
						model.IsValid=true;
					}
					else
					{
						model.IsValid=false;
					}
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select UserID,Password,EmployeeID,UserName,IsValid ");
			strSql.Append(" FROM SysUser ");
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
            strSql.Append("select UserID,Password,EmployeeID,UserName,IsValid ");
            strSql.Append(" FROM SysUser ");
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
			strSql.Append(" UserID,Password,EmployeeID,UserName,IsValid ");
			strSql.Append(" FROM SysUser ");
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
				strSql.Append("order by T.UserID desc");
			}
			strSql.Append(")AS Row, T.*  from SysUser T ");
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
        /// <param name="key">查询条件 0：UserID 1：Password 2：EmployeeID 3：UserName 4：ISValid 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value)
        {
            return GetList(key, value, -1, -1, false);
        }

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：UserID 1：Password 2：EmployeeID 3：UserName 4：ISValid 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：UserID 1：Password 2：EmployeeID 3：UserName 4：ISValid 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" UserID,Password,EmployeeID,UserName,IsValid ");
            strSql.Append(" FROM SysUser ");

            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "UserID = @UserID";
                    parameter = new MySqlParameter("@UserID", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "Password = @Password";
                    parameter = new MySqlParameter("@Password", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 2: strWhere = "EmployeeID = @EmployeeID";
                    parameter = new MySqlParameter("@EmployeeID", MySqlDbType.VarChar,50);
                    parameter.Value = value;
                    break;
                case 3: strWhere = "UserName like @UserName";
                    parameter = new MySqlParameter("@UserName", MySqlDbType.VarChar, 50);
                    parameter.Value = "%" + value + "%";
                    break;
                case 4: strWhere = "IsValid = @IsValid";
                    parameter = new MySqlParameter("@ISValid", MySqlDbType.Bit, 1);
                    parameter.Value = value;
                    break;
                default: break;
            }
            if (strWhere.Trim() != "")
                strSql.Append(" where " + strWhere);

            string strOrder = "";
            switch (filedOrder)
            {
                case 0: strOrder = "UserID"; break;
                case 1: strOrder = "Password"; break;
                case 2: strOrder = "EmployeeID"; break;
                case 3: strOrder = "UserName"; break;
                case 4: strOrder = "IsValid"; break;
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
            strSql.Append("select count(1) FROM SysUser ");
            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "UserID = @UserID";
                    parameter = new MySqlParameter("@UserID", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "Password = @Password";
                    parameter = new MySqlParameter("@Password", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 2: strWhere = "EmployeeID = @EmployeeID";
                    parameter = new MySqlParameter("@EmployeeID", MySqlDbType.VarChar,50);
                    parameter.Value = value;
                    break;
                case 3: strWhere = "UserName like @UserName";
                    parameter = new MySqlParameter("@UserName", MySqlDbType.VarChar, 50);
                    parameter.Value = "%" + value + "%";
                    break;
                case 4: strWhere = "IsValid = @IsValid";
                    parameter = new MySqlParameter("@ISValid", MySqlDbType.Bit, 1);
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
        /// <param name="key">查询条件 0：UserID 1：Password 2：EmployeeID 3：UserName 4：ISValid 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="filedOrder">排序字段 0：UserID 1：Password 2：EmployeeID 3：UserName 4：ISValid 其他:不排序</param>
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
                case 0: strOrder = "UserID"; break;
                case 1: strOrder = "Password"; break;
                case 2: strOrder = "EmployeeID"; break;
                case 3: strOrder = "UserName"; break;
                case 4: strOrder = "IsValid"; break;
                default: break;
            }

            if (!string.IsNullOrEmpty(strOrder.Trim()))
            {
                strSql.Append("order by T." + strOrder);
            }
            else
            {
                strSql.Append("order by T.UserID desc");
            }
            strSql.Append(")AS Row, T.*  from SysUser T ");


            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "UserID = @UserID";
                    parameter = new MySqlParameter("@UserID", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "Password = @Password";
                    parameter = new MySqlParameter("@Password", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 2: strWhere = "EmployeeID = @EmployeeID";
                    parameter = new MySqlParameter("@EmployeeID", MySqlDbType.VarChar,50);
                    parameter.Value = value;
                    break;
                case 3: strWhere = "UserName like @UserName";
                    parameter = new MySqlParameter("@UserName", MySqlDbType.VarChar, 50);
                    parameter.Value = "%" + value + "%";
                    break;
                case 4: strWhere = "IsValid = @IsValid";
                    parameter = new MySqlParameter("@ISValid", MySqlDbType.Bit, 1);
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

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

