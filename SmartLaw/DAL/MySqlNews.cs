using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SmartLaw.DBUtility;
using MySql.Data.MySqlClient;//Please add references
namespace SmartLaw.DAL
{
	/// <summary>
	/// 数据访问类:News
	/// </summary>
	public partial class MySqlNews
	{
        public MySqlNews()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(long AutoID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from News");
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
		public long Add(SmartLaw.Model.News model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into News(");
            strSql.Append("Title,CategoryID,Contents,Publisher,DataSource,DataType,LastModifyTime,IsValid,Checked,Checker,CheckMemo)");
			strSql.Append(" values (");
			strSql.Append("@Title,@CategoryID,@Contents,@Publisher,@DataSource,@DataType,@LastModifyTime,@IsValid,@Checked,@Checker,@CheckMemo)");
			strSql.Append(";select @@IDENTITY");
			MySqlParameter[] parameters = {
					new MySqlParameter("@Title", MySqlDbType.VarChar,50),
					new MySqlParameter("@CategoryID", MySqlDbType.Int64),
					new MySqlParameter("@Contents", MySqlDbType.Text),
					new MySqlParameter("@Publisher", MySqlDbType.VarChar,50),
					new MySqlParameter("@DataSource", MySqlDbType.VarChar,50),
					new MySqlParameter("@DataType", MySqlDbType.VarChar,50),
					new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime),
					new MySqlParameter("@IsValid", MySqlDbType.Bit,1),
					new MySqlParameter("@Checked", MySqlDbType.Int32),
					new MySqlParameter("@Checker", MySqlDbType.VarChar,50),
                    new MySqlParameter("@CheckMemo",MySqlDbType.VarChar,50)};
			parameters[0].Value = model.Title;
			parameters[1].Value = model.CategoryID;
			parameters[2].Value = model.Contents;
			parameters[3].Value = model.Publisher;
			parameters[4].Value = model.DataSource;
			parameters[5].Value = model.DataType;
			parameters[6].Value = model.LastModifyTime;
			parameters[7].Value = model.IsValid;
			parameters[8].Value = model.Checked;
			parameters[9].Value = model.Checker;
            parameters[10].Value = model.CheckMemo;
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
		public bool Update(SmartLaw.Model.News model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update News set ");
			strSql.Append("Title=@Title,");
			strSql.Append("CategoryID=@CategoryID,");
			strSql.Append("Contents=@Contents,");
			strSql.Append("Publisher=@Publisher,");
			strSql.Append("DataSource=@DataSource,");
			strSql.Append("DataType=@DataType,");
			strSql.Append("LastModifyTime=@LastModifyTime,");
			strSql.Append("IsValid=@IsValid,");
			strSql.Append("Checked=@Checked,");
			strSql.Append("Checker=@Checker,");
            strSql.Append("CheckMemo=@CheckMemo");
			strSql.Append(" where AutoID=@AutoID");
			MySqlParameter[] parameters = {
					new MySqlParameter("@Title", MySqlDbType.VarChar,50),
					new MySqlParameter("@CategoryID", MySqlDbType.Int64),
					new MySqlParameter("@Contents", MySqlDbType.Text),
					new MySqlParameter("@Publisher", MySqlDbType.VarChar,50),
					new MySqlParameter("@DataSource", MySqlDbType.VarChar,50),
					new MySqlParameter("@DataType", MySqlDbType.VarChar,50),
					new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime),
					new MySqlParameter("@IsValid", MySqlDbType.Bit,1),
					new MySqlParameter("@Checked", MySqlDbType.Int32),
					new MySqlParameter("@Checker", MySqlDbType.VarChar,50),
                    new MySqlParameter("@CheckMemo", MySqlDbType.VarChar,50),
					new MySqlParameter("@AutoID", MySqlDbType.Int64)};
			parameters[0].Value = model.Title;
			parameters[1].Value = model.CategoryID;
			parameters[2].Value = model.Contents;
			parameters[3].Value = model.Publisher;
			parameters[4].Value = model.DataSource;
			parameters[5].Value = model.DataType;
			parameters[6].Value = model.LastModifyTime;
			parameters[7].Value = model.IsValid;
			parameters[8].Value = model.Checked;
			parameters[9].Value = model.Checker;
            parameters[10].Value = model.CheckMemo;
			parameters[11].Value = model.AutoID;

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
			strSql.Append("delete from News ");
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
        public bool DeleteList(string[] AutoIDlist)
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
			strSql.Append("delete from News ");
            strSql.Append(" where AutoID in (" + _AutoIDlist + ")  ");
			int rows=DbHelperMySQL.ExecuteSql(strSql.ToString());
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
		public SmartLaw.Model.News GetModel(long AutoID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select AutoID,Title,CategoryID,Contents,Publisher,DataSource,DataType,LastModifyTime,IsValid,Checked,Checker,CheckMemo from News");
            strSql.Append(" where AutoID=@AutoID limit 1");
			MySqlParameter[] parameters = {
					new MySqlParameter("@AutoID", MySqlDbType.Int64)
			};
			parameters[0].Value = AutoID;

			SmartLaw.Model.News model=new SmartLaw.Model.News();
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
		public SmartLaw.Model.News DataRowToModel(DataRow row)
		{
			SmartLaw.Model.News model=new SmartLaw.Model.News();
			if (row != null)
			{
				if(row["AutoID"]!=null && row["AutoID"].ToString()!="")
				{
					model.AutoID=long.Parse(row["AutoID"].ToString());
				}
				if(row["Title"]!=null)
				{
					model.Title=row["Title"].ToString();
				}
				if(row["CategoryID"]!=null && row["CategoryID"].ToString()!="")
				{
					model.CategoryID=long.Parse(row["CategoryID"].ToString());
				}
				if(row["Contents"]!=null)
				{
					model.Contents=row["Contents"].ToString();
				}
				if(row["Publisher"]!=null)
				{
					model.Publisher=row["Publisher"].ToString();
				}
				if(row["DataSource"]!=null)
				{
					model.DataSource=row["DataSource"].ToString();
				}
				if(row["DataType"]!=null)
				{
					model.DataType=row["DataType"].ToString();
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
                if (row["Checked"] != null)
                { 
                    model.Checked = int.Parse(row["Checked"].ToString());
                }
				if(row["Checker"]!=null)
				{
					model.Checker=row["Checker"].ToString();
				}
                if (row["CheckMemo"] != null)
                {
                    model.CheckMemo = row["CheckMemo"].ToString();
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
			strSql.Append("select AutoID,Title,CategoryID,Contents,Publisher,DataSource,DataType,LastModifyTime,IsValid,Checked,Checker,CheckMemo ");
			strSql.Append(" FROM News ");
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
            strSql.Append("select AutoID,Title,CategoryID,Contents,Publisher,DataSource,DataType,LastModifyTime,IsValid,Checked,Checker,CheckMemo ");
            strSql.Append(" FROM News ");
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
            strSql.Append(" AutoID,Title,CategoryID,Contents,Publisher,DataSource,DataType,LastModifyTime,IsValid,Checked,Checker,CheckMemo ");
			strSql.Append(" FROM News ");
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
        /// <param name="key">查询条件 0：AutoID 1：Title 2：CategoryID 3：Contents 4：Publisher 5:DataSource： 6：DataType 7：LastModifyTime 8:IsValid 9:Checked 10:Checker 11:CheckMemo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value)
        {
            return GetList(key, value, -1, -1, false);
        }

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：Title 2：CategoryID 3：Contents 4：Publisher 5:DataSource 6：DataType 7：LastModifyTime 8:IsValid 9:Checked 10:Checker 11:CheckMemo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1：Title 2：CategoryID 3：Contents 4：Publisher 5:DataSource 6：DataType 7：LastModifyTime 8:IsValid 9:Checked 10:Checker 11:CheckMemo 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" AutoID,Title,CategoryID,Contents,Publisher,DataSource,DataType,LastModifyTime,IsValid,Checked,Checker,CheckMemo ");
            strSql.Append(" FROM News ");

            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "Title like @Title";
                    parameter = new MySqlParameter("@Title", MySqlDbType.VarChar, 50);
                    parameter.Value = "%" + value + "%";
                    break;
                case 2: strWhere = "CategoryID = @CategoryID";
                    parameter = new MySqlParameter("@CategoryID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 3: strWhere = "Contents like @Contents";
                    parameter = new MySqlParameter("@Contents", MySqlDbType.Text);
                    parameter.Value = "%" + value + "%";
                    break;
                case 4: strWhere = "Publisher = @Publisher";
                    parameter = new MySqlParameter("@Publisher", MySqlDbType.VarChar, 50);
                    parameter.Value =  value;
                    break;
                case 5: strWhere = "DataSource = @DataSource";
                    parameter = new MySqlParameter("@DataSource", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 6: strWhere = "DataType = @DataType";
                    parameter = new MySqlParameter("@DataType", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 7: strWhere = "LastModifyTime = @LastModifyTime";
                    parameter = new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 8: strWhere = "IsValid = @IsValid";
                    parameter = new MySqlParameter("@IsValid", MySqlDbType.Bit, 1);
                    parameter.Value = value;
                    break;
                case 9: strWhere = "Checked = @Checked";
                    parameter = new MySqlParameter("@Checked", MySqlDbType.Int32);
                    parameter.Value = value;
                    break;
                case 10: strWhere = "Checker = @Checker";
                    parameter = new MySqlParameter("@Checker", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 11: strWhere = "CheckMemo = @CheckMemo";
                    parameter = new MySqlParameter("@CheckMemo", MySqlDbType.VarChar, 50);
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
                case 1: strOrder = "Title"; break;
                case 2: strOrder = "CategoryID"; break;
                case 3: strOrder = "Contents"; break;
                case 4: strOrder = "Publisher"; break;
                case 5: strOrder = "DataSource"; break;
                case 6: strOrder = "DataType"; break;
                case 7: strOrder = "LastModifyTime"; break;
                case 8: strOrder = "IsValid"; break;
                case 9: strOrder = "Checked"; break;
                case 10: strOrder = "Checker"; break;
                case 11: strOrder = "CheckMemo"; break;
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
            strSql.Append("select count(1) FROM News ");
            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "Title like @Title";
                    parameter = new MySqlParameter("@Title", MySqlDbType.VarChar, 50);
                    parameter.Value = "%" + value + "%";
                    break;
                case 2: strWhere = "CategoryID = @CategoryID";
                    parameter = new MySqlParameter("@CategoryID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 3: strWhere = "Contents like @Contents";
                    parameter = new MySqlParameter("@Contents", MySqlDbType.Text);
                    parameter.Value = "%" + value + "%";
                    break;
                case 4: strWhere = "Publisher = @Publisher";
                    parameter = new MySqlParameter("@Publisher", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 5: strWhere = "DataSource = @DataSource";
                    parameter = new MySqlParameter("@DataSource", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 6: strWhere = "DataType = @DataType";
                    parameter = new MySqlParameter("@DataType", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 7: strWhere = "LastModifyTime = @LastModifyTime";
                    parameter = new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 8: strWhere = "IsValid = @IsValid";
                    parameter = new MySqlParameter("@IsValid", MySqlDbType.Bit, 1);
                    parameter.Value = value;
                    break;
                case 9: strWhere = "Checked = @Checked";
                    parameter = new MySqlParameter("@Checked", MySqlDbType.Int32);
                    parameter.Value = value;
                    break;
                case 10: strWhere = "Checker = @Checker";
                    parameter = new MySqlParameter("@Checker", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 11: strWhere = "CheckMemo = @CheckMemo";
                    parameter = new MySqlParameter("@CheckMemo", MySqlDbType.VarChar, 50);
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
            strSql.Append("SELECT");
            strSql.Append(" AutoID,Title,CategoryID,LastModifyTime,IsValid,Checked ");
            strSql.Append(" FROM NEWS AS T "); 
            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "Title like @Title";
                    parameter = new MySqlParameter("@Title", MySqlDbType.VarChar, 50);
                    parameter.Value = "%" + value + "%";
                    break;
                case 2: strWhere = "CategoryID = @CategoryID";
                    parameter = new MySqlParameter("@CategoryID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 3: strWhere = "Contents like @Contents";
                    parameter = new MySqlParameter("@Contents", MySqlDbType.Text);
                    parameter.Value = "%" + value + "%";
                    break;
                case 4: strWhere = "Publisher = @Publisher";
                    parameter = new MySqlParameter("@Publisher", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 5: strWhere = "DataSource = @DataSource";
                    parameter = new MySqlParameter("@DataSource", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 6: strWhere = "DataType = @DataType";
                    parameter = new MySqlParameter("@DataType", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 7: strWhere = "LastModifyTime = @LastModifyTime";
                    parameter = new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 8: strWhere = "IsValid = @IsValid";
                    parameter = new MySqlParameter("@IsValid", MySqlDbType.Bit, 1);
                    parameter.Value = value;
                    break;
                case 9: strWhere = "Checked = @Checked";
                    parameter = new MySqlParameter("@Checked", MySqlDbType.Int32);
                    parameter.Value = value;
                    break;
                case 10: strWhere = "Checker = @Checker";
                    parameter = new MySqlParameter("@Checker", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 11: strWhere = "CheckMemo = @CheckMemo";
                    parameter = new MySqlParameter("@CheckMemo", MySqlDbType.VarChar, 50);
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
                case 0: strOrder = "AutoID"; break;
                case 1: strOrder = "Title"; break;
                case 2: strOrder = "CategoryID"; break;
                case 3: strOrder = "Contents"; break;
                case 4: strOrder = "Publisher"; break;
                case 5: strOrder = "DataSource"; break;
                case 6: strOrder = "DataType"; break;
                case 7: strOrder = "LastModifyTime"; break;
                case 8: strOrder = "IsValid"; break;
                case 9: strOrder = "Checked"; break;
                case 10: strOrder = "Checker"; break;
                case 11: strOrder = "CheckMemo"; break;
                default: break;
            }

            if (!string.IsNullOrEmpty(strOrder.Trim()))
            {
                if (desc)
                {
                    strSql.Append(" order by T." + strOrder + " desc");
                }
                else
                {
                    strSql.Append(" order by T." + strOrder);
                }
            }
            else
            {
                strSql.Append(" order by T.AutoID desc");
            }

            strSql.AppendFormat(" limit {0} , {1}", startIndex, endIndex);

            if (parameter != null)
                return DbHelperMySQL.Query(strSql.ToString(), parameter);
            else
                return DbHelperMySQL.Query(strSql.ToString());
        }
		#endregion  BasicMethod
		#region  ExtensionMethod
        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：Title 2：CategoryID 3：Contents 4：Publisher 5:DataSource 6：DataType 7：LastModifyTime 8:IsValid 9:Checked 10:Checker 11:CheckMemo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1：Title 2：CategoryID 3：Contents 4：Publisher 5:DataSource 6：DataType 7：LastModifyTime 8:IsValid 9:Checked 10:Checker 11:CheckMemo 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public DataSet GetListEx(int[] keys, string[] values, int Top, int filedOrder, bool desc,int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT");
            strSql.Append(" N.AutoID,N.Title,N.CategoryID,N.LastModifyTime,N.IsValid,N.Checked, N.Publisher ");
            bool rgCheck = false;
            int otherParam = 0; 
            StringBuilder strWhere = new StringBuilder();
            for (int i = keys.Length - 1; i >= 0; i--)
            {
                if (keys[i] == 12)
                {
                    strSql.Append(" FROM NEWS N,NEWSREGIONALRELATION NRR"); 
                    rgCheck = true;
                    otherParam++;
                }
                if (keys[i] == 2)
                {
                    otherParam++;
                }
            }
            if (!rgCheck)
            {
                strSql.Append(" FROM NEWS N");
            }


            MySqlParameter[] parameter = new MySqlParameter[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                if (i != 0)
                {
                    strWhere.Append(" and ");
                }
                switch (keys[i])
                {
                    case 0: strWhere.Append(" N.AutoID = @AutoID");
                        parameter[i] = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                        parameter[i].Value = values[i];
                        break;
                    case 1: strWhere.Append(" N.Title like @Title");
                        parameter[i] = new MySqlParameter("@Title", MySqlDbType.VarChar, 50);
                        parameter[i].Value = "%" + values[i] + "%";
                        break;
                    case 2: strWhere.Append(" N.CategoryID in  ( " + values[i] + " ) and 1 = @CategoryID");
                        parameter[i] = new MySqlParameter("@CategoryID", MySqlDbType.Int32);
                        parameter[i].Value = 1;
                        break;
                    case 3: strWhere.Append(" N.Contents like @Contents");
                        parameter[i] = new MySqlParameter("@Contents", MySqlDbType.Text);
                        parameter[i].Value = "%" + values[i] + "%";
                        break;
                    case 4: strWhere.Append(" N.Publisher = @Publisher");
                        parameter[i] = new MySqlParameter("@Publisher", MySqlDbType.VarChar, 50);
                        parameter[i].Value = values[i];
                        break;
                    case 5: strWhere.Append(" N.DataSource = @DataSource");
                        parameter[i] = new MySqlParameter("@DataSource", MySqlDbType.VarChar, 50);
                        parameter[i].Value = values[i];
                        break;
                    case 6: strWhere.Append(" N.DataType = @DataType");
                        parameter[i] = new MySqlParameter("@DataType", MySqlDbType.VarChar, 50);
                        parameter[i].Value = values[i];
                        break;
                    case 7: strWhere.Append(" N.LastModifyTime = @LastModifyTime");
                        parameter[i] = new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime);
                        parameter[i].Value = values[i];
                        break;
                    case 8: strWhere.Append(" N.IsValid = @IsValid");
                        parameter[i] = new MySqlParameter("@IsValid", MySqlDbType.Bit, 1);
                        parameter[i].Value = values[i];
                        break;
                    case 9: strWhere.Append(" N.Checked = @Checked");
                        parameter[i] = new MySqlParameter("@Checked", MySqlDbType.Int32);
                        parameter[i].Value = values[i];
                        break;
                    case 10: strWhere.Append(" N.Checker = @Checker");
                        parameter[i] = new MySqlParameter("@Checker", MySqlDbType.VarChar, 50);
                        parameter[i].Value = values[i];
                        break;
                    case 11: strWhere.Append(" N.CheckMemo = @CheckMemo");
                        parameter[i] = new MySqlParameter("@CheckMemo", MySqlDbType.VarChar, 50);
                        parameter[i].Value = values[i];
                        break;
                    case 12: strWhere.Append(" NRR.RegionalID in ( " + values[i] + " ) and 1 = @RegionalID");
                        parameter[i] = new MySqlParameter("@RegionalID", MySqlDbType.Int32);
                        parameter[i].Value = 1;
                        break;
                    default: break;
                }
            }
            if (strWhere.ToString().Trim() != "")
                strSql.Append(" where "+ (rgCheck ? " N.AutoID = NRR.NewsID AND " :"") + strWhere.ToString());

            string strOrder = "";
            switch (filedOrder)
            {
                case 0: strOrder = "AutoID"; break;
                case 1: strOrder = "Title"; break;
                case 2: strOrder = "CategoryID"; break;
                case 3: strOrder = "Contents"; break;
                case 4: strOrder = "Publisher"; break;
                case 5: strOrder = "DataSource"; break;
                case 6: strOrder = "DataType"; break;
                case 7: strOrder = "LastModifyTime"; break;
                case 8: strOrder = "IsValid"; break;
                case 9: strOrder = "Checked"; break;
                case 10: strOrder = "Checker"; break;
                case 11: strOrder = "CheckMemo"; break;
                default: break;
            }
            strSql.Append(" GROUP BY  N.AutoID,N.Title,N.CategoryID,N.LastModifyTime,N.IsValid,N.Checked  ");
            if (!string.IsNullOrEmpty(strOrder.Trim()))
            {
                if (desc)
                {
                    strSql.Append(" order by N." + strOrder + " desc");
                }
                else
                {
                    strSql.Append(" order by N." + strOrder);
                }
            }
            else
            {
                strSql.Append(" order by N.AutoID desc");
            }
            if (startIndex != -1 && endIndex != -1)
            {
                strSql.AppendFormat(" limit {0} , {1}", startIndex, endIndex);
            }
            return DbHelperMySQL.Query(strSql.ToString(), parameter); 
        }

        /// <summary>
        /// 根据条件获取数据数量
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：Title 2：CategoryID 3：Contents 4：Publisher 5:DataSource 6：DataType 7：LastModifyTime 8:IsValid 9:Checked 10:Checker 11:CheckMemo 其他:全部</param>
        /// <param name="value">查询值</param>  
        /// <returns>个数</returns>
        public int GetReCordEx(int[] keys, string[] values)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT count(*) from (");
            strSql.Append("SELECT N.AutoID");
            bool rgCheck = false;
            int otherparam = 0;
            for (int i = keys.Length-1; i >=0; i--)
            {
                if (keys[i] == 12)
                {
                    strSql.Append(" FROM NEWS N,NEWSREGIONALRELATION NRR");
                    rgCheck = true;
                    otherparam++;
                }
                if (keys[i] == 2)
                {
                    otherparam++;
                }
            }
            if (!rgCheck)
            {
                strSql.Append(" FROM NEWS N"); 
            }
            StringBuilder strWhere = new StringBuilder();
            MySqlParameter[] parameter = new MySqlParameter[keys.Length];
            for (int i = 0; i < keys.Length; i++)
            {
                if (i != 0)
                {
                    strWhere.Append(" and ");
                }
                switch (keys[i])
                {
                    case 0: strWhere.Append(" N.AutoID = @AutoID");
                        parameter[i] = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                        parameter[i].Value = values[i];
                        break;
                    case 1: strWhere.Append(" N.Title like @Title");
                        parameter[i] = new MySqlParameter("@Title", MySqlDbType.VarChar, 50);
                        parameter[i].Value = "%" + values[i] + "%";
                        break;
                    case 2: strWhere.Append(" N.CategoryID in  ( " + values[i] + " ) and 1 = @CategoryID");
                        parameter[i] = new MySqlParameter("@CategoryID", MySqlDbType.Int32);
                        parameter[i].Value = 1;
                        break;
                    case 3: strWhere.Append(" Contents like @Contents");
                        parameter[i] = new MySqlParameter("@Contents", MySqlDbType.Text);
                        parameter[i].Value = "%" + values[i] + "%";
                        break;
                    case 4: strWhere.Append(" N.Publisher = @Publisher");
                        parameter[i] = new MySqlParameter("@Publisher", MySqlDbType.VarChar, 50);
                        parameter[i].Value = values[i];
                        break;
                    case 5: strWhere.Append(" N.DataSource = @DataSource");
                        parameter[i] = new MySqlParameter("@DataSource", MySqlDbType.VarChar, 50);
                        parameter[i].Value = values[i];
                        break;
                    case 6: strWhere.Append(" N.DataType = @DataType");
                        parameter[i] = new MySqlParameter("@DataType", MySqlDbType.VarChar, 50);
                        parameter[i].Value = values[i];
                        break;
                    case 7: strWhere.Append(" N.LastModifyTime = @LastModifyTime");
                        parameter[i] = new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime);
                        parameter[i].Value = values[i];
                        break;
                    case 8: strWhere.Append(" N.IsValid = @IsValid");
                        parameter[i] = new MySqlParameter("@IsValid", MySqlDbType.Bit, 1);
                        parameter[i].Value = values[i];
                        break;
                    case 9: strWhere.Append(" N.Checked = @Checked");
                        parameter[i] = new MySqlParameter("@Checked", MySqlDbType.Int32);
                        parameter[i].Value = values[i];
                        break;
                    case 10: strWhere.Append(" N.Checker = @Checker");
                        parameter[i] = new MySqlParameter("@Checker", MySqlDbType.VarChar, 50);
                        parameter[i].Value = values[i];
                        break;
                    case 11: strWhere.Append(" N.CheckMemo = @CheckMemo");
                        parameter[i] = new MySqlParameter("@CheckMemo", MySqlDbType.VarChar, 50);
                        parameter[i].Value = values[i];
                        break;
                    case 12: strWhere.Append(" NRR.RegionalID in ( " + values[i] + " ) and 1 = @RegionalID");
                        parameter[i] = new MySqlParameter("@RegionalID", MySqlDbType.Int32);
                        parameter[i].Value = 1;
                        break;
                    default: break;
                }
            }
            if (strWhere.ToString().Trim() != "")
                strSql.Append(" where " + (rgCheck ? " N.AutoID = NRR.NewsID AND " : "") + strWhere.ToString());
            strSql.Append(" GROUP BY N.AutoID ) T");
            Object obj = null; 
            obj = DbHelperMySQL.GetSingle(strSql.ToString(), parameter); 
            if (obj == null)
                return 0;
            else
                return Convert.ToInt32(obj);
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCountForRePort(int[] keys, string[] values,int checkState, string startDate,string endDate)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT count(*) from (");
            strSql.Append("SELECT N.AutoID"); 
            strSql.Append(" FROM NEWS N,NEWSREGIONALRELATION NRR"); 
            StringBuilder strWhere = new StringBuilder();
            int pNo = 2;
            if (!startDate.Equals("") && !endDate.Equals(""))
            {
                pNo += 2;
            }
            if (checkState != -1)
            {
                pNo += 1;
            } 
            MySqlParameter[] parameter = new MySqlParameter[pNo];
            for (int i = 0; i < keys.Length; i++)
            {
                if (i != 0)
                {
                    strWhere.Append(" and ");
                }
                switch (keys[i])
                {
                    case 2: strWhere.Append("CategoryID  in ( " + values[i] + " ) and 1 = @CategoryID ");
                        parameter[i] = new MySqlParameter("@CategoryID", MySqlDbType.Int32);
                        parameter[i].Value = 1;
                        break;
                    case 12: strWhere.Append(" NRR.RegionalID  in ( " + values[i] + " ) and 1 = @RegionalID ");
                        parameter[i] = new MySqlParameter("@RegionalID", MySqlDbType.Int32);
                        parameter[i].Value = 1;
                        break;
                    default: break;
                }
            }
            if (!startDate.Equals("") && !endDate.Equals(""))
            {
                strWhere.Append("AND LastModifyTime between @startDate and @endDate ");
                parameter[2] = new MySqlParameter("@startDate", MySqlDbType.DateTime);
                parameter[2].Value = startDate;
                parameter[3] = new MySqlParameter("@endDate", MySqlDbType.DateTime);
                parameter[3].Value = endDate;
            }
            if (checkState != -1)
            {
                strWhere.Append("AND N.Checked = @Checked ");
                parameter[pNo-1] = new MySqlParameter("@Checked", MySqlDbType.Int32);
                parameter[pNo-1].Value = checkState;
            }
            object obj = null;
            if (strWhere.ToString().Trim() != "")
                strSql.Append(" where N.AutoID = NRR.NewsID AND " + strWhere.ToString());
            strSql.Append(" GROUP BY N.AutoID ) T");
            obj = DbHelperMySQL.GetSingle(strSql.ToString(), parameter); 
            if (obj == null)
                return 0;
            else
                return Convert.ToInt32(obj);
        }
		#endregion  ExtensionMethod
	}
}

