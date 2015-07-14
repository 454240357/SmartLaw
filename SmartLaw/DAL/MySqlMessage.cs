using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using SmartLaw.DBUtility;
using System.Data;

namespace SmartLaw.DAL
{
    public partial class MySqlMessage
    {
        public MySqlMessage()
		{ }
        #region  BasicMethod


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long AutoID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Message");
            strSql.Append(" where AutoID=@AutoID");
            MySqlParameter[] parameters = {
					new MySqlParameter("@AutoID", MySqlDbType.Int64)
			};
            parameters[0].Value = AutoID;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(SmartLaw.Model.Message model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Message(");
            strSql.Append("Title,Contents,Orders,MessageType,DisappearType,AvailableTime,ExpiredTime,LastModifyTime,Publisher,IsValid,AndOr,Memo,DataType)");
            strSql.Append(" values (");
            strSql.Append("@Title,@Contents,@Orders,@MessageType,@DisappearType,@AvailableTime,@ExpiredTime,@LastModifyTime,@Publisher,@IsValid,@AndOr,@Memo,@DataType)");
            strSql.Append(";select @@IDENTITY");
            MySqlParameter[] parameters = { 
                    new MySqlParameter("@Title", MySqlDbType.VarChar,50), 
					new MySqlParameter("@Contents", MySqlDbType.Text),
                    new MySqlParameter("@Orders", MySqlDbType.Int32),
                    new MySqlParameter("@MessageType", MySqlDbType.VarChar,50), 
                    new MySqlParameter("@DisappearType", MySqlDbType.VarChar,50), 
                    new MySqlParameter("@AvailableTime", MySqlDbType.DateTime),
                    new MySqlParameter("@ExpiredTime", MySqlDbType.DateTime),
                    new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime), 
					new MySqlParameter("@Publisher", MySqlDbType.VarChar,50), 
					new MySqlParameter("@IsValid", MySqlDbType.Bit,1),
                    new MySqlParameter("@AndOr", MySqlDbType.Bit,1), 
                    new MySqlParameter("@Memo", MySqlDbType.VarChar,50),
                    new MySqlParameter("@DataType", MySqlDbType.VarChar,50)};
            parameters[0].Value = model.Title;
            parameters[1].Value = model.Contents;
            parameters[2].Value = model.Orders;
            parameters[3].Value = model.MessageType;
            parameters[4].Value = model.DisappearType;
            parameters[5].Value = model.AvailableTime;
            parameters[6].Value = model.ExpiredTime;
            parameters[7].Value = model.LastModifyTime; 
            parameters[8].Value = model.Publisher; 
            parameters[9].Value = model.IsValid;
            parameters[10].Value = model.AndOr;
            parameters[11].Value = model.Memo;
            parameters[12].Value = model.DataType;
            object obj = DbHelperMySQL.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(SmartLaw.Model.Message model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Message set ");
            strSql.Append("Title=@Title,");
            strSql.Append("Contents=@Contents,");
            strSql.Append("Orders=@Orders,");
            strSql.Append("MessageType=@MessageType,");
            strSql.Append("DisappearType=@DisappearType,");
            strSql.Append("AvailableTime=@AvailableTime,");
            strSql.Append("ExpiredTime=@ExpiredTime,");
            strSql.Append("LastModifyTime=@LastModifyTime,"); 
            strSql.Append("Publisher=@Publisher,");
            strSql.Append("IsValid=@IsValid,");
            strSql.Append("AndOr=@AndOr,");
            strSql.Append("Memo=@Memo,");
            strSql.Append("DataType=@DataType");
            strSql.Append(" where AutoID=@AutoID");
            MySqlParameter[] parameters = { 
                    new MySqlParameter("@Title", MySqlDbType.VarChar,50), 
					new MySqlParameter("@Contents", MySqlDbType.Text),
                    new MySqlParameter("@Orders", MySqlDbType.Int32),
                    new MySqlParameter("@MessageType", MySqlDbType.VarChar,50), 
                    new MySqlParameter("@DisappearType", MySqlDbType.VarChar,50), 
                    new MySqlParameter("@AvailableTime", MySqlDbType.DateTime),
                    new MySqlParameter("@ExpiredTime", MySqlDbType.DateTime),
                    new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime), 
					new MySqlParameter("@Publisher", MySqlDbType.VarChar,50), 
					new MySqlParameter("@IsValid", MySqlDbType.Bit,1),
                    new MySqlParameter("@AndOr", MySqlDbType.Bit,1), 
                    new MySqlParameter("@Memo", MySqlDbType.VarChar,50),
                    new MySqlParameter("@DataType", MySqlDbType.VarChar,50),
                    new MySqlParameter("@AutoID", MySqlDbType.Int64,20)    };
            parameters[0].Value = model.Title;
            parameters[1].Value = model.Contents;
            parameters[2].Value = model.Orders;
            parameters[3].Value = model.MessageType;
            parameters[4].Value = model.DisappearType;
            parameters[5].Value = model.AvailableTime;
            parameters[6].Value = model.ExpiredTime;
            parameters[7].Value = model.LastModifyTime;
            parameters[8].Value = model.Publisher;
            parameters[9].Value = model.IsValid;
            parameters[10].Value = model.AndOr;
            parameters[11].Value = model.Memo;
            parameters[12].Value = model.DataType;
            parameters[13].Value = model.AutoID;
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
        public bool Delete(long AutoID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Message ");
            strSql.Append(" where AutoID=@AutoID");
            MySqlParameter[] parameters = {
					new MySqlParameter("@AutoID", MySqlDbType.Int64)
			};
            parameters[0].Value = AutoID;

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
        /// 批量删除数据,返回成功删除的条目数
        /// </summary>
        public bool DeleteList(string[] AutoIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder _AutoIDlist = new StringBuilder();
            int i = 0;
            while (i < AutoIDlist.Length)
            {
                _AutoIDlist.Append("'" + AutoIDlist[i] + "'").Append(",");
                i++;
            }
            _AutoIDlist.Remove(_AutoIDlist.Length - 1, 1);
            strSql.Append("delete from Message ");
            strSql.Append(" where AutoID in (" + _AutoIDlist + ")  ");
            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString());
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
        public SmartLaw.Model.Message GetModel(long AutoID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AutoID,Title,Contents,Orders,MessageType,DisappearType,AvailableTime,ExpiredTime,LastModifyTime,Publisher,IsValid,AndOr,Memo,DataType from Message");
            strSql.Append(" where AutoID=@AutoID limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@AutoID", MySqlDbType.Int64)
			};
            parameters[0].Value = AutoID;

            SmartLaw.Model.Message model = new SmartLaw.Model.Message();
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
        public SmartLaw.Model.Message DataRowToModel(DataRow row)
        {
            SmartLaw.Model.Message model = new SmartLaw.Model.Message();
            if (row != null)
            {
                if (row["AutoID"] != null && row["AutoID"].ToString() != "")
                {
                    model.AutoID = long.Parse(row["AutoID"].ToString());
                }
                if (row["Title"] != null)
                {
                    model.Title = row["Title"].ToString();
                } 
                if (row["Contents"] != null)
                {
                    model.Contents = row["Contents"].ToString();
                }
                if (row["Orders"] != null)
                {
                    model.Orders =Int32.Parse( row["Orders"].ToString());
                }
                if (row["MessageType"] != null)
                {
                    model.MessageType = row["MessageType"].ToString(); 
                }
                if (row["DisappearType"] != null)
                {
                    model.DisappearType = row["DisappearType"].ToString();
                }
                if (row["AvailableTime"] != null && row["AvailableTime"].ToString() != "")
                {
                    model.AvailableTime = DateTime.Parse(row["AvailableTime"].ToString());
                }
                if (row["ExpiredTime"] != null && row["ExpiredTime"].ToString() != "")
                {
                    model.ExpiredTime = DateTime.Parse(row["ExpiredTime"].ToString());
                }
                if (row["LastModifyTime"] != null && row["LastModifyTime"].ToString() != "")
                {
                    model.LastModifyTime = DateTime.Parse(row["LastModifyTime"].ToString());
                }
                if (row["Publisher"] != null)
                {
                    model.Publisher = row["Publisher"].ToString();
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
                if (row["AndOr"] != null && row["AndOr"].ToString() != "")
                {
                    if ((row["AndOr"].ToString() == "1") || (row["AndOr"].ToString().ToLower() == "true"))
                    {
                        model.AndOr = true;
                    }
                    else
                    {
                        model.AndOr = false;
                    }
                }
                if (row["Memo"] != null)
                {
                    model.Memo = row["Memo"].ToString();
                }
                if (row["DataType"] != null)
                {
                    model.DataType = row["DataType"].ToString();
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
            strSql.Append("select AutoID,Title,Contents,Orders,MessageType,DisappearType,AvailableTime,ExpiredTime,LastModifyTime,Publisher,IsValid,AndOr,Memo,DataType ");
            strSql.Append(" FROM Message ");
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
            strSql.Append("select AutoID,Title,Contents,Orders,MessageType,DisappearType,AvailableTime,ExpiredTime,LastModifyTime,Publisher,IsValid,AndOr,Memo,DataType ");
            strSql.Append(" FROM Message ");
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
            strSql.Append(" AutoID,Title,Contents,Orders,MessageType,DisappearType,AvailableTime,ExpiredTime,LastModifyTime,Publisher,IsValid,AndOr,Memo,DataType ");
            strSql.Append(" FROM Message ");
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
        /// <param name="key">查询条件 0：AutoID 1：Title 2：Contents 3: Orders 4：MessageType 5：DisappearType 6：AvailableTime 7：ExpiredTime 8：LastModifyTime 9：Publisher 10:IsValid 11：AndOr 12：Memo 13: DataType 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value)
        {
            return GetList(key, value, -1, -1, false);
        }

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：Title 2：Contents 3: Orders 4：MessageType 5：DisappearType 6：AvailableTime 7：ExpiredTime 8：LastModifyTime 9：Publisher 10:IsValid 11：AndOr 12：Memo 13: DataType 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1：Title 2：Contents 3: Orders 4：MessageType 5：DisappearType 6：AvailableTime 7：ExpiredTime 8：LastModifyTime 9：Publisher 10:IsValid 11：AndOr 12：Memo  13: DataType其他:全部</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" AutoID,Title,Contents,Orders,MessageType,DisappearType,AvailableTime,ExpiredTime,LastModifyTime,Publisher,IsValid,AndOr,Memo,DataType ");
            strSql.Append(" FROM Message ");

            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64,20);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "Title = @Title";
                    parameter = new MySqlParameter("@Title", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 2: strWhere = "Contents like @Contents";
                    parameter = new MySqlParameter("@Contents", MySqlDbType.Text);
                    parameter.Value = "%" + value + "%";
                    break;
                case 3: strWhere = "Orders = @Orders";
                    parameter = new MySqlParameter("@Orders", MySqlDbType.Int32);
                    parameter.Value =  value ;
                    break;
                case 4: strWhere = "MessageType = @MessageType";
                    parameter = new MySqlParameter("@MessageType", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 5: strWhere = "DisappearType = @DisappearType";
                    parameter = new MySqlParameter("@DisappearType", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 6: strWhere = "AvailableTime = @AvailableTime";
                    parameter = new MySqlParameter("@AvailableTime", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 7: strWhere = "ExpiredTime = @ExpiredTime";
                    parameter = new MySqlParameter("@ExpiredTime", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 8: strWhere = "LastModifyTime = @LastModifyTime";
                    parameter = new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;  
                case 9: strWhere = "Publisher = @Publisher";
                    parameter = new MySqlParameter("@Publisher", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break; 
                case 10: strWhere = "IsValid = @IsValid";
                    parameter = new MySqlParameter("@IsValid", MySqlDbType.Bit, 1);
                    parameter.Value = value;
                    break;
                case 11: strWhere = "AndOr = @AndOr";
                    parameter = new MySqlParameter("@AndOr", MySqlDbType.Bit, 1);
                    parameter.Value = value;
                    break;
                case 12: strWhere = "Memo = @Memo";
                    parameter = new MySqlParameter("@Memo", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 13: strWhere = "DataType = @DataType";
                    parameter = new MySqlParameter("@DataType", MySqlDbType.VarChar, 50);
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
                case 2: strOrder = "Contents"; break;
                case 3: strOrder = "Orders"; break;
                case 4: strOrder = "MessageType"; break;
                case 5: strOrder = "DisappearType"; break;
                case 6: strOrder = "AvailableTime"; break;
                case 7: strOrder = "ExpiredTime"; break;
                case 8: strOrder = "LastModifyTime"; break; 
                case 9: strOrder = "Publisher"; break; 
                case 10: strOrder = "IsValid"; break;
                case 11: strOrder = "AndOr"; break;
                case 12: strOrder = "Memo"; break;
                case 13: strOrder = "DataType"; break; 
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
            strSql.Append("select count(1) FROM Message ");
            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64,20);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "Title = @Title";
                    parameter = new MySqlParameter("@Title", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 2: strWhere = "Contents like @Contents";
                    parameter = new MySqlParameter("@Contents", MySqlDbType.Text);
                    parameter.Value = "%" + value + "%";
                    break;
                case 3: strWhere = "Orders like @Orders";
                    parameter = new MySqlParameter("@Orders", MySqlDbType.Int32);
                    parameter.Value = value;
                    break;
                case 4: strWhere = "MessageType = @MessageType";
                    parameter = new MySqlParameter("@MessageType", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 5: strWhere = "DisappearType = @DisappearType";
                    parameter = new MySqlParameter("@DisappearType", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 6: strWhere = "AvailableTime = @AvailableTime";
                    parameter = new MySqlParameter("@AvailableTime", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 7: strWhere = "ExpiredTime = @ExpiredTime";
                    parameter = new MySqlParameter("@ExpiredTime", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 8: strWhere = "LastModifyTime = @LastModifyTime";
                    parameter = new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 9: strWhere = "Publisher = @Publisher";
                    parameter = new MySqlParameter("@Publisher", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 10: strWhere = "IsValid = @IsValid";
                    parameter = new MySqlParameter("@IsValid", MySqlDbType.Bit, 1);
                    parameter.Value = value;
                    break;
                case 11: strWhere = "AndOr = @AndOr";
                    parameter = new MySqlParameter("@AndOr", MySqlDbType.Bit, 1);
                    parameter.Value = value;
                    break;
                case 12: strWhere = "Memo = @Memo";
                    parameter = new MySqlParameter("@Memo", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 13: strWhere = "DataType = @DataType";
                    parameter = new MySqlParameter("@DataType", MySqlDbType.VarChar, 50);
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
        /// <param name="key">查询条件0：AutoID 1：Title 2：Contents 3: Orders 4：MessageType 5：DisappearType 6：AvailableTime 7：ExpiredTime 8：LastModifyTime 9：Publisher 10:IsValid 11：AndOr 12：Memo 13: DataType其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="filedOrder">排序字段0：AutoID 1：Title 2：Contents 3: Orders 4：MessageType 5：DisappearType 6：AvailableTime 7：ExpiredTime 8：LastModifyTime 9：Publisher 10:IsValid 11：AndOr 12：Memo 13: DataType其他:全部</param>
        /// <param name="desc">选用倒序</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="endIndex">结束索引</param>
        /// <returns>数据集</returns>
        public DataSet GetListByPage(int key, string value, int filedOrder, bool desc, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from ( ");
            strSql.Append(" select row_number() over(");
            string strOrder = "";
            switch (filedOrder)
            {
                case 0: strOrder = "AutoID"; break;
                case 1: strOrder = "Title"; break;
                case 2: strOrder = "Contents"; break;
                case 3: strOrder = "Orders"; break;
                case 4: strOrder = "MessageType"; break;
                case 5: strOrder = "DisappearType"; break;
                case 6: strOrder = "AvailableTime"; break;
                case 7: strOrder = "ExpiredTime"; break;
                case 8: strOrder = "LastModifyTime"; break;
                case 9: strOrder = "Publisher"; break;
                case 10: strOrder = "IsValid"; break;
                case 11: strOrder = "AndOr"; break;
                case 12: strOrder = "Memo"; break;
                case 13: strOrder = "DataType"; break; 
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
            strSql.Append(")AS Row, T.*  from Message T ");


            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64,20);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "Title = @Title";
                    parameter = new MySqlParameter("@Title", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 2: strWhere = "Contents like @Contents";
                    parameter = new MySqlParameter("@Contents", MySqlDbType.Text);
                    parameter.Value = "%" + value + "%";
                    break;
                case 3: strWhere = "Orders like @Orders";
                    parameter = new MySqlParameter("@Orders", MySqlDbType.Int32);
                    parameter.Value = value;
                    break;
                case 4: strWhere = "MessageType = @MessageType";
                    parameter = new MySqlParameter("@MessageType", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 5: strWhere = "DisappearType = @DisappearType";
                    parameter = new MySqlParameter("@DisappearType", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 6: strWhere = "AvailableTime = @AvailableTime";
                    parameter = new MySqlParameter("@AvailableTime", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 7: strWhere = "ExpiredTime = @ExpiredTime";
                    parameter = new MySqlParameter("@ExpiredTime", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 8: strWhere = "LastModifyTime = @LastModifyTime";
                    parameter = new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 9: strWhere = "Publisher = @Publisher";
                    parameter = new MySqlParameter("@Publisher", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 10: strWhere = "IsValid = @IsValid";
                    parameter = new MySqlParameter("@IsValid", MySqlDbType.Bit, 1);
                    parameter.Value = value;
                    break;
                case 11: strWhere = "AndOr = @AndOr";
                    parameter = new MySqlParameter("@AndOr", MySqlDbType.Bit, 1);
                    parameter.Value = value;
                    break;
                case 12: strWhere = "Memo = @Memo";
                    parameter = new MySqlParameter("@Memo", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 13: strWhere = "DataType = @DataType";
                    parameter = new MySqlParameter("@DataType", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                default: break;
            }
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" where TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperMySQL.Query(strSql.ToString());
        }
        #endregion  BasicMethod

       #region  ExtensionMethod
        /// <summary>
        /// 根据复合条件获取公告
        /// </summary>
        /// <param name="key">查询条件 0：AvailableTime下限 1：AvailableTime上限 2：ExpiredTime上限 3：ExpiredTime下限 4：Title 5:Content： 6：IsValid 7：Obj-Identity 8:Obj-Region 9:Obj-Sim 其他:全部</param>
        /// <param name="value">查询值</param> 
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1：Title 2：Contents 3: Orders 4：MessageType 5：DisappearType 6：AvailableTime 7：ExpiredTime 8：LastModifyTime 9：Publisher 10:IsValid 11：AndOr 12：Memo 其他:全部</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public DataSet GetMessageEx(int[] keys, object[] values, int Top, int filedOrder, bool desc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT");
            strSql.Append(" M.AutoID,M.Title,M.Contents,M.Orders,M.MessageType,M.DisappearType,M.AvailableTime,M.ExpiredTime,M.LastModifyTime,M.Publisher,M.IsValid,M.AndOr,M.Memo,M.DataType ");
            StringBuilder strWhere = new StringBuilder(); 
            strSql.Append(" FROM Message M"); 
            MySqlParameter[] parameter = new MySqlParameter[keys.Length];
            bool obj  = false;
            for (int i = keys.Length - 1; i >=0; i--)
            {
                if (i != keys.Length - 1)
                {  
                    strWhere.Append(" and "); 
                } 
                switch (keys[i])
                {
                    case 0: strWhere.Append(" M.AvailableTime >= @AvailableTime");
                        parameter[i] = new MySqlParameter("@AvailableTime", MySqlDbType.DateTime);
                        parameter[i].Value = values[i];
                        break;
                    case 1: strWhere.Append(" M.AvailableTime <= @AvailableTime");
                        parameter[i] = new MySqlParameter("@AvailableTime", MySqlDbType.DateTime);
                        parameter[i].Value = values[i];
                        break;
                    case 2: strWhere.Append(" M.ExpiredTime >= @ExpiredTime");
                        parameter[i] = new MySqlParameter("@ExpiredTime", MySqlDbType.DateTime);
                        parameter[i].Value = values[i];
                        break;
                    case 3: strWhere.Append(" M.ExpiredTime <= @ExpiredTime");
                        parameter[i] = new MySqlParameter("@ExpiredTime", MySqlDbType.DateTime);
                        parameter[i].Value = values[i];
                        break;
                    case 4: strWhere.Append(" M.Title like @Title");
                        parameter[i] = new MySqlParameter("@Title", MySqlDbType.VarChar, 50);
                        parameter[i].Value = "%" + values[i] + "%";
                        break;
                    case 5: strWhere.Append(" M.Content like @Content");
                        parameter[i] = new MySqlParameter("@Content", MySqlDbType.Text);
                        parameter[i].Value = "%" + values[i] + "%";
                        break;
                    case 6: strWhere.Append(" M.IsValid = @IsValid");
                        parameter[i] = new MySqlParameter("@IsValid", MySqlDbType.Bit,1);
                        parameter[i].Value = values[i];
                        break;  
                    default: break;
                }
            }
            if (obj)
            {
                strWhere.Append(" )");
            }
            if (strWhere.ToString().Trim() != "")
                strSql.Append(" where " + strWhere.ToString());

            string strOrder = "";
            switch (filedOrder)
            {
                case 0: strOrder = "AutoID"; break;
                case 1: strOrder = "Title"; break;
                case 2: strOrder = "Contents"; break;
                case 3: strOrder = "Orders"; break;
                case 4: strOrder = "MessageType"; break;
                case 5: strOrder = "DisappearType"; break;
                case 6: strOrder = "AvailableTime"; break;
                case 7: strOrder = "ExpiredTime"; break;
                case 8: strOrder = "LastModifyTime"; break;
                case 9: strOrder = "Publisher"; break;
                case 10: strOrder = "IsValid"; break;
                case 11: strOrder = "AndOr"; break;
                case 12: strOrder = "Memo"; break;
                case 13: strOrder = "DataType"; break;
                default: break;
            } 
            if (!string.IsNullOrEmpty(strOrder.Trim()))
            {
                if (desc)
                {
                    strSql.Append(" order by M." + strOrder + " desc");
                }
                else
                {
                    strSql.Append(" order by M." + strOrder);
                }
            }
            else
            {
                strSql.Append(" order by M.AutoID desc");
            }

            return DbHelperMySQL.Query(strSql.ToString(), parameter); 
        }
        
        #endregion  ExtensionMethod
    }
}
