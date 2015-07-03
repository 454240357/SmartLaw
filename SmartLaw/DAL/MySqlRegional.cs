using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using SmartLaw.DBUtility;//Please add references
namespace SmartLaw.DAL
{
	/// <summary>
	/// 数据访问类:regional
	/// </summary>
	public partial class MySqlRegional
	{
		public MySqlRegional()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string RegionalID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from regional");
            strSql.Append(" where RegionalID=@RegionalID");
			MySqlParameter[] parameters = {
					new MySqlParameter("@RegionalID", MySqlDbType.VarChar,50)
			};
            parameters[0].Value = RegionalID;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(SmartLaw.Model.Regional model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into regional(");
            strSql.Append("RegionalID,RegionalName,RegionalCode,SubRegionalID,RegionalLevel,Orders,LastModifyTime,IsValid,Memo)");
			strSql.Append(" values (");
            strSql.Append("@RegionalID,@RegionalName,@RegionalCode,@SubRegionalID,@RegionalLevel,@Orders,@LastModifyTime,@IsValid,@Memo)");
			MySqlParameter[] parameters = {
                    new MySqlParameter("@RegionalID", MySqlDbType.VarChar,50),
					new MySqlParameter("@RegionalName", MySqlDbType.VarChar,50),
					new MySqlParameter("@RegionalCode", MySqlDbType.VarChar,50),
					new MySqlParameter("@SubRegionalID", MySqlDbType.VarChar,50),
					new MySqlParameter("@RegionalLevel", MySqlDbType.VarChar,50),
					new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime),
					new MySqlParameter("@IsValid", MySqlDbType.Bit),
					new MySqlParameter("@Memo", MySqlDbType.VarChar,50),
                    new MySqlParameter("@Orders", MySqlDbType.Int32)};
            parameters[0].Value = model.RegionalID;
			parameters[1].Value = model.RegionalName;
			parameters[2].Value = model.RegionalCode;
			parameters[3].Value = model.SubRegionalID;
			parameters[4].Value = model.RegionalLevel;
			parameters[5].Value = model.LastModifyTime;
			parameters[6].Value = model.IsValid;
			parameters[7].Value = model.Memo;
            parameters[8].Value = model.Orders;

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
            parameters1[0].Value = "Region";
            parameters1[1].Value = model.RegionalID;
            parameters1[2].Value = model.RegionalName;
            parameters1[3].Value = DateTime.Now;
            parameters1[4].Value = model.IsValid;
            parameters1[5].Value = model.Memo;

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
		/// 更新一条数据
		/// </summary>
		public bool Update(SmartLaw.Model.Regional model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update regional set ");
			strSql.Append("RegionalName=@RegionalName,");
			strSql.Append("RegionalCode=@RegionalCode,");
			strSql.Append("SubRegionalID=@SubRegionalID,");
			strSql.Append("RegionalLevel=@RegionalLevel,");
            strSql.Append("Orders=@Orders,");
			strSql.Append("LastModifyTime=@LastModifyTime,");
			strSql.Append("IsValid=@IsValid,");
			strSql.Append("Memo=@Memo");
            strSql.Append(" where RegionalID=@RegionalID");
			MySqlParameter[] parameters = {
					new MySqlParameter("@RegionalName", MySqlDbType.VarChar,50),
					new MySqlParameter("@RegionalCode", MySqlDbType.VarChar,50),
					new MySqlParameter("@SubRegionalID", MySqlDbType.VarChar,50),
					new MySqlParameter("@RegionalLevel", MySqlDbType.VarChar,50),
					new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime),
					new MySqlParameter("@IsValid", MySqlDbType.Bit),
					new MySqlParameter("@Memo", MySqlDbType.VarChar,50),
					new MySqlParameter("@RegionalID", MySqlDbType.VarChar,50),
                    new MySqlParameter("@Orders", MySqlDbType.Int32)};
			parameters[0].Value = model.RegionalName;
			parameters[1].Value = model.RegionalCode;
			parameters[2].Value = model.SubRegionalID;
			parameters[3].Value = model.RegionalLevel;
			parameters[4].Value = model.LastModifyTime;
			parameters[5].Value = model.IsValid;
			parameters[6].Value = model.Memo;
            parameters[7].Value = model.RegionalID;
            parameters[8].Value = model.Orders;

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
            parameters1[0].Value = "Region";
            parameters1[1].Value = model.RegionalName;
            parameters1[2].Value = DateTime.Now;
            parameters1[3].Value = model.IsValid;
            parameters1[4].Value = model.Memo;
            parameters1[5].Value = model.RegionalID;

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
        public bool Delete(string RegionalID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from regional ");
            strSql.Append(" where RegionalID=@RegionalID");
			MySqlParameter[] parameters = {
					new MySqlParameter("@RegionalID", MySqlDbType.VarChar,50)
			};
            parameters[0].Value = RegionalID;

            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from T_SYSCODEDETIAL ");
            strSql1.Append(" where SYSCodeDetialID=@SYSCodeDetialID ");
            MySqlParameter[] parameters1 = {
					new MySqlParameter("@SYSCodeDetialID", MySqlDbType.VarChar,50)			};
            parameters1[0].Value = RegionalID;

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
        public int DeleteList(string[] RegionalIDlist)
		{
			StringBuilder strSql=new StringBuilder();
            StringBuilder _RegionalIDlist = new StringBuilder();
            int i = 0;
            while (i < RegionalIDlist.Length)
            {
                _RegionalIDlist.Append("'" + RegionalIDlist[i] + "'").Append(",");
                i++;
            }
            _RegionalIDlist.Remove(_RegionalIDlist.Length - 1, 1);
			strSql.Append("delete from regional ");
            strSql.Append(" where RegionalID in (" + _RegionalIDlist + ")  ");

            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from T_SYSCODEDETIAL ");
            strSql1.Append(" where SYSCodeDetialID in (" + _RegionalIDlist + ")  ");

            System.Collections.Generic.List<string> sqlStringList = new System.Collections.Generic.List<string>();
            sqlStringList.Add(strSql.ToString());
            sqlStringList.Add(strSql1.ToString());
            int rows = DbHelperMySQL.ExecuteSqlTran(sqlStringList) / 2;
            return rows;
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public SmartLaw.Model.Regional GetModel(string RegionalID)
		{
			
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select RegionalID,RegionalName,RegionalCode,SubRegionalID,RegionalLevel,Orders,LastModifyTime,IsValid,Memo from regional ");
            strSql.Append(" where RegionalID=@RegionalID limit 1");
			MySqlParameter[] parameters = {
					new MySqlParameter("@RegionalID", MySqlDbType.VarChar,50)
			};
            parameters[0].Value = RegionalID;

			SmartLaw.Model.Regional model=new SmartLaw.Model.Regional();
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
		public SmartLaw.Model.Regional DataRowToModel(DataRow row)
		{
			SmartLaw.Model.Regional model=new SmartLaw.Model.Regional();
			if (row != null)
			{
                if (row["RegionalID"] != null)
				{
                    model.RegionalID = row["RegionalID"].ToString();
				}
				if(row["RegionalName"]!=null)
				{
					model.RegionalName=row["RegionalName"].ToString();
				}
				if(row["RegionalCode"]!=null)
				{
					model.RegionalCode=row["RegionalCode"].ToString();
				}
				if(row["SubRegionalID"]!=null)
				{
					model.SubRegionalID=row["SubRegionalID"].ToString();
				}
				if(row["RegionalLevel"]!=null)
				{
					model.RegionalLevel=row["RegionalLevel"].ToString();
				}
                if (row["Orders"] != null)
				{
                    model.Orders = int.Parse(row["Orders"].ToString());
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
            strSql.Append("select RegionalID,RegionalName,RegionalCode,SubRegionalID,RegionalLevel,Orders,LastModifyTime,IsValid,Memo ");
			strSql.Append(" FROM regional ");
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
            strSql.Append("select RegionalID,RegionalName,RegionalCode,SubRegionalID,RegionalLevel,Orders,LastModifyTime,IsValid,Memo ");
            strSql.Append(" FROM regional ");
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
            strSql.Append(" RegionalID,RegionalName,RegionalCode,SubRegionalID,RegionalLevel,Orders,LastModifyTime,IsValid,Memo ");
            strSql.Append(" FROM regional ");
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
        /// <param name="key">查询条件 0：RegionalID 1：RegionalName 2：RegionalCode 3：SubRegionalID 4：RegionalLevel 5:LastModifyTime 6:IsValid 7:Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value)
        {
            return GetList(key, value, -1, -1, false);
        }

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：RegionalID 1：RegionalName 2：RegionalCode 3：SubRegionalID 4：RegionalLevel 5:LastModifyTime 6:IsValid 7:Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：RegionalID 1：RegionalName 2：RegionalCode 3：SubRegionalID 4：RegionalLevel 5:LastModifyTime 6:IsValid 7:Memo 8:Orders 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" RegionalID,RegionalName,RegionalCode,SubRegionalID,RegionalLevel,Orders,LastModifyTime,IsValid,Memo ");
            strSql.Append(" FROM regional ");

            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "RegionalID = @RegionalID";
                    parameter = new MySqlParameter("@RegionalID", MySqlDbType.VarChar,50);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "RegionalName like @RegionalName";
                    parameter = new MySqlParameter("@RegionalName", MySqlDbType.VarChar, 50);
                    parameter.Value = "%" + value + "%";
                    break;
                case 2: strWhere = "RegionalCode = @RegionalCode";
                    parameter = new MySqlParameter("@RegionalCode", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 3: strWhere = "SubRegionalID = @SubRegionalID";
                    parameter = new MySqlParameter("@SubRegionalID", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 4: strWhere = "RegionalLevel = @RegionalLevel";
                    parameter = new MySqlParameter("@RegionalLevel", MySqlDbType.VarChar, 50);
                    parameter.Value = value ;
                    break;
                case 5: strWhere = "LastModifyTime = @LastModifyTime";
                    parameter = new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 6: strWhere = "IsValid = @IsValid";
                    parameter = new MySqlParameter("@IsValid", MySqlDbType.Bit, 1);
                    parameter.Value = value;
                    break;
                case 7: strWhere = "Memo like @Memo";
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
                case 0: strOrder = "RegionalID"; break;
                case 1: strOrder = "RegionalName"; break;
                case 2: strOrder = "RegionalCode"; break;
                case 3: strOrder = "SubRegionalID"; break;
                case 4: strOrder = "RegionalLevel"; break;
                case 5: strOrder = "LastModifyTime"; break;
                case 6: strOrder = "IsValid"; break;
                case 7: strOrder = "Memo"; break;
                case 8: strOrder = "Orders"; break;
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
            strSql.Append("select count(1) FROM regional ");
            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "RegionalID = @RegionalID";
                    parameter = new MySqlParameter("@RegionalID", MySqlDbType.VarChar,50);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "RegionalName like @RegionalName";
                    parameter = new MySqlParameter("@RegionalName", MySqlDbType.VarChar, 50);
                    parameter.Value = "%" + value + "%";
                    break;
                case 2: strWhere = "RegionalCode = @RegionalCode";
                    parameter = new MySqlParameter("@RegionalCode", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 3: strWhere = "SubRegionalID = @SubRegionalID";
                    parameter = new MySqlParameter("@SubRegionalID", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 4: strWhere = "RegionalLevel = @RegionalLevel";
                    parameter = new MySqlParameter("@RegionalLevel", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 5: strWhere = "LastModifyTime = @LastModifyTime";
                    parameter = new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 6: strWhere = "IsValid = @IsValid";
                    parameter = new MySqlParameter("@IsValid", MySqlDbType.Bit, 1);
                    parameter.Value = value;
                    break;
                case 7: strWhere = "Memo like @Memo";
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
        /// <param name="key">查询条件 0：RegionalID 1：RegionalName 2：RegionalCode 3：SubRegionalID 4：RegionalLevel 5:LastModifyTime 6:IsValid 7:Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="filedOrder">排序字段 0：RegionalID 1：RegionalName 2：RegionalCode 3：SubRegionalID 4：RegionalLevel 5:LastModifyTime 6:IsValid 7:Memo 8:Orders 其他:不排序</param>
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
                case 0: strOrder = "RegionalID"; break;
                case 1: strOrder = "RegionalName"; break;
                case 2: strOrder = "RegionalCode"; break;
                case 3: strOrder = "SubRegionalID"; break;
                case 4: strOrder = "RegionalLevel"; break;
                case 5: strOrder = "LastModifyTime"; break;
                case 6: strOrder = "IsValid"; break;
                case 7: strOrder = "Memo"; break;
                case 8: strOrder = "Orders"; break;
                default: break;
            }

            if (!string.IsNullOrEmpty(strOrder.Trim()))
            {
                strSql.Append("order by T." + strOrder);
            }
            else
            {
                strSql.Append("order by T.RegionalID desc");
            }
            strSql.Append(")AS Row, T.*  from regional T ");


            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "RegionalID = @RegionalID";
                    parameter = new MySqlParameter("@RegionalID", MySqlDbType.VarChar,50);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "RegionalName like @RegionalName";
                    parameter = new MySqlParameter("@RegionalName", MySqlDbType.VarChar, 50);
                    parameter.Value = "%" + value + "%";
                    break;
                case 2: strWhere = "RegionalCode = @RegionalCode";
                    parameter = new MySqlParameter("@RegionalCode", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 3: strWhere = "SubRegionalID = @SubRegionalID";
                    parameter = new MySqlParameter("@SubRegionalID", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 4: strWhere = "RegionalLevel = @RegionalLevel";
                    parameter = new MySqlParameter("@RegionalLevel", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 5: strWhere = "LastModifyTime = @LastModifyTime";
                    parameter = new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 6: strWhere = "IsValid = @IsValid";
                    parameter = new MySqlParameter("@IsValid", MySqlDbType.Bit, 1);
                    parameter.Value = value;
                    break;
                case 7: strWhere = "Memo like @Memo";
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
        /// <summary>
        /// 根据条件获取有效数据列表
        /// </summary>
        /// <param name="key">查询条件 0：RegionalID 1：RegionalName 2：RegionalCode 3：SubRegionalID 4：RegionalLevel 5:LastModifyTime 6:IsValid 7:Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：RegionalID 1：RegionalName 2：RegionalCode 3：SubRegionalID 4：RegionalLevel 5:LastModifyTime 6:IsValid 7:Memo 8:Orders 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public DataSet GetListValid(int key, string value, int Top, int filedOrder, bool desc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" RegionalID,RegionalName,RegionalCode,SubRegionalID,RegionalLevel,Orders,LastModifyTime,IsValid,Memo ");
            strSql.Append(" FROM regional ");

            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "RegionalID = @RegionalID";
                    parameter = new MySqlParameter("@RegionalID", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "RegionalName like @RegionalName";
                    parameter = new MySqlParameter("@RegionalName", MySqlDbType.VarChar, 50);
                    parameter.Value = "%" + value + "%";
                    break;
                case 2: strWhere = "RegionalCode = @RegionalCode";
                    parameter = new MySqlParameter("@RegionalCode", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 3: strWhere = "SubRegionalID = @SubRegionalID";
                    parameter = new MySqlParameter("@SubRegionalID", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 4: strWhere = "RegionalLevel = @RegionalLevel";
                    parameter = new MySqlParameter("@RegionalLevel", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 5: strWhere = "LastModifyTime = @LastModifyTime";
                    parameter = new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 6: strWhere = "IsValid = @IsValid";
                    parameter = new MySqlParameter("@IsValid", MySqlDbType.Bit, 1);
                    parameter.Value = value;
                    break;
                case 7: strWhere = "Memo like @Memo";
                    parameter = new MySqlParameter("@Memo", MySqlDbType.VarChar, 50);
                    parameter.Value = "%" + value + "%";
                    break;
                default: break;
            }
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
                strSql.Append(" and  IsValid=1 ");
            }
            else
                strSql.Append(" where IsValid=1 ");
            string strOrder = "";
            switch (filedOrder)
            {
                case 0: strOrder = "RegionalID"; break;
                case 1: strOrder = "RegionalName"; break;
                case 2: strOrder = "RegionalCode"; break;
                case 3: strOrder = "SubRegionalID"; break;
                case 4: strOrder = "RegionalLevel"; break;
                case 5: strOrder = "LastModifyTime"; break;
                case 6: strOrder = "IsValid"; break;
                case 7: strOrder = "Memo"; break;
                case 8: strOrder = "Orders"; break;
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
		#endregion  ExtensionMethod
	}
}

