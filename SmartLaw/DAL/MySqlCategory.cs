using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SmartLaw.DBUtility;
using MySql.Data.MySqlClient;//Please add references
namespace SmartLaw.DAL
{
	/// <summary>
	/// 数据访问类:Category
	/// </summary>
	public partial class MySqlCategory
	{
        public MySqlCategory()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long AutoID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from Category");
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
		public long Add(SmartLaw.Model.Category model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into Category(");
			strSql.Append("CategoryName,ParentCategoryID,Orders,LastModifyTime,IsValid,Memo)");
			strSql.Append(" values (");
			strSql.Append("@CategoryName,@ParentCategoryID,@Orders,@LastModifyTime,@IsValid,@Memo)");
			strSql.Append(";select @@IDENTITY");
			MySqlParameter[] parameters = {
					new MySqlParameter("@CategoryName", MySqlDbType.VarChar,50),
					new MySqlParameter("@ParentCategoryID", MySqlDbType.Int64),
					new MySqlParameter("@Orders", MySqlDbType.Int32),
					new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime),
					new MySqlParameter("@IsValid", MySqlDbType.Bit,1),
					new MySqlParameter("@Memo", MySqlDbType.VarChar,50)};
			parameters[0].Value = model.CategoryName;
			parameters[1].Value = model.ParentCategoryID;
			parameters[2].Value = model.Orders;
			parameters[3].Value = model.LastModifyTime;
			parameters[4].Value = model.IsValid;
			parameters[5].Value = model.Memo;

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
		public bool Update(SmartLaw.Model.Category model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update Category set ");
			strSql.Append("CategoryName=@CategoryName,");
			strSql.Append("ParentCategoryID=@ParentCategoryID,");
			strSql.Append("Orders=@Orders,");
			strSql.Append("LastModifyTime=@LastModifyTime,");
			strSql.Append("IsValid=@IsValid,");
			strSql.Append("Memo=@Memo");
			strSql.Append(" where AutoID=@AutoID");
			MySqlParameter[] parameters = {
					new MySqlParameter("@CategoryName", MySqlDbType.VarChar,50),
					new MySqlParameter("@ParentCategoryID", MySqlDbType.Int64),
					new MySqlParameter("@Orders", MySqlDbType.Int32),
					new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime),
					new MySqlParameter("@IsValid", MySqlDbType.Bit,1),
					new MySqlParameter("@Memo", MySqlDbType.VarChar,50),
					new MySqlParameter("@AutoID", MySqlDbType.Int64)};
			parameters[0].Value = model.CategoryName;
			parameters[1].Value = model.ParentCategoryID;
			parameters[2].Value = model.Orders;
			parameters[3].Value = model.LastModifyTime;
			parameters[4].Value = model.IsValid;
			parameters[5].Value = model.Memo;
			parameters[6].Value = model.AutoID;

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
			strSql.Append("delete from Category ");
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
                _AutoIDlist.Append("'" + AutoIDlist[i] + "'").Append(",");
                i++;
            }
            _AutoIDlist.Remove(_AutoIDlist.Length - 1, 1);
			strSql.Append("delete from Category ");
            strSql.Append(" where AutoID in (" + _AutoIDlist + ")  ");
			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString());
            return rows;
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public SmartLaw.Model.Category GetModel(long AutoID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  AutoID,CategoryName,ParentCategoryID,Orders,LastModifyTime,IsValid,Memo from Category");
			strSql.Append(" where AutoID=@AutoID limit 1");
			MySqlParameter[] parameters = {
					new MySqlParameter("@AutoID", MySqlDbType.Int64)
			};
			parameters[0].Value = AutoID;

			SmartLaw.Model.Category model=new SmartLaw.Model.Category();
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
		public SmartLaw.Model.Category DataRowToModel(DataRow row)
		{
			SmartLaw.Model.Category model=new SmartLaw.Model.Category();
			if (row != null)
			{
				if(row["AutoID"]!=null && row["AutoID"].ToString()!="")
				{
					model.AutoID=long.Parse(row["AutoID"].ToString());
				}
				if(row["CategoryName"]!=null)
				{
					model.CategoryName=row["CategoryName"].ToString();
				}
				if(row["ParentCategoryID"]!=null && row["ParentCategoryID"].ToString()!="")
				{
					model.ParentCategoryID=long.Parse(row["ParentCategoryID"].ToString());
				}
				if(row["Orders"]!=null && row["Orders"].ToString()!="")
				{
					model.Orders=int.Parse(row["Orders"].ToString());
				}
				if(row["LastModifyTime"]!=null && row["LastModifyTime"].ToString()!="")
				{
					model.LastModifyTime=DateTime.Parse(row["LastModifyTime"].ToString());
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
			strSql.Append("select AutoID,CategoryName,ParentCategoryID,Orders,LastModifyTime,IsValid,Memo ");
			strSql.Append(" FROM Category ");
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
            strSql.Append("select AutoID,CategoryName,ParentCategoryID,Orders,LastModifyTime,IsValid,Memo ");
            strSql.Append(" FROM Category ");
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
			strSql.Append(" AutoID,CategoryName,ParentCategoryID,Orders,LastModifyTime,IsValid,Memo ");
			strSql.Append(" FROM Category ");
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
        /// <param name="key">查询条件 0：AutoID 1：CategoryName 2：ParentCategoryID 3：Orders 4：LastModifyTime 5:IsValid 6:Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value)
        {
            return GetList(key, value, -1, -1, false);
        }

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：CategoryName 2：ParentCategoryID 3：Orders 4：LastModifyTime 5:IsValid 6:Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：Orders 1：AutoID 2：ParentCategoryID 3：CategoryName 4：LastModifyTime 5:IsValid 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" AutoID,CategoryName,ParentCategoryID,Orders,LastModifyTime,IsValid,Memo ");
            strSql.Append(" FROM Category ");

            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                    parameter.Value = value ;
                    break;
                case 1: strWhere = "CategoryName like @CategoryName";
                    parameter = new MySqlParameter("@CategoryName", MySqlDbType.VarChar, 50);
                    parameter.Value = "%" + value + "%";
                    break;
                case 2: strWhere = "ParentCategoryID = @ParentCategoryID";
                    parameter = new MySqlParameter("@ParentCategoryID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 3: strWhere = "Orders = @Orders";
                    parameter = new MySqlParameter("@Orders", MySqlDbType.Int32);
                    parameter.Value = value;
                    break;
                case 4: strWhere = "LastModifyTime = @LastModifyTime";
                    parameter = new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime);
                    parameter.Value =value;
                    break;
                case 5: strWhere = "IsValid = @IsValid";
                    parameter = new MySqlParameter("@IsValid", MySqlDbType.Bit, 1);
                    parameter.Value = value;
                    break;
                case 6: strWhere = "Memo like @Memo";
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
                case 0: strOrder = "Orders"; break;
                case 1: strOrder = "AutoID"; break;
                case 2: strOrder = "ParentCategoryID"; break;
                case 3: strOrder = "CategoryName"; break;
                case 4: strOrder = "LastModifyTime"; break;
                case 5: strOrder = "IsValid"; break;
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
            strSql.Append("select count(1) FROM Category ");
            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "CategoryName like @CategoryName";
                    parameter = new MySqlParameter("@CategoryName", MySqlDbType.VarChar, 50);
                    parameter.Value = "%" + value + "%";
                    break;
                case 2: strWhere = "ParentCategoryID = @ParentCategoryID";
                    parameter = new MySqlParameter("@ParentCategoryID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 3: strWhere = "Orders = @Orders";
                    parameter = new MySqlParameter("@Orders", MySqlDbType.Int32);
                    parameter.Value = value;
                    break;
                case 4: strWhere = "LastModifyTime = @LastModifyTime";
                    parameter = new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 5: strWhere = "IsValid = @IsValid";
                    parameter = new MySqlParameter("@IsValid", MySqlDbType.Bit, 1);
                    parameter.Value = value;
                    break;
                case 6: strWhere = "Memo like @Memo";
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
                case 0: strOrder = "Orders"; break;
                case 1: strOrder = "AutoID"; break;
                case 2: strOrder = "ParentCategoryID"; break;
                case 3: strOrder = "CategoryName"; break;
                case 4: strOrder = "LastModifyTime"; break;
                case 5: strOrder = "IsValid"; break;
                default: break;
            }

            if (!string.IsNullOrEmpty(strOrder.Trim()))
            {
                strSql.Append("order by T." + strOrder);
            }
            else
            {
                strSql.Append("order by T.Orders desc");
            }
            strSql.Append(")AS Row, T.*  from Category T ");


            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "CategoryName like @CategoryName";
                    parameter = new MySqlParameter("@CategoryName", MySqlDbType.VarChar, 50);
                    parameter.Value = "%" + value + "%";
                    break;
                case 2: strWhere = "ParentCategoryID = @ParentCategoryID";
                    parameter = new MySqlParameter("@ParentCategoryID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 3: strWhere = "Orders = @Orders";
                    parameter = new MySqlParameter("@Orders", MySqlDbType.Int32);
                    parameter.Value = value;
                    break;
                case 4: strWhere = "LastModifyTime = @LastModifyTime";
                    parameter = new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime, 50);
                    parameter.Value = value;
                    break;
                case 5: strWhere = "IsValid = @IsValid";
                    parameter = new MySqlParameter("@IsValid", MySqlDbType.Bit, 1);
                    parameter.Value = value;
                    break;
                case 6: strWhere = "Memo like @Memo";
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

