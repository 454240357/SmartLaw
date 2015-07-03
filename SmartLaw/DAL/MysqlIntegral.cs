using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmartLaw.DBUtility;
using MySql.Data.MySqlClient;
using System.Data;

namespace SmartLaw.DAL
{
    /// <summary>
    /// 数据访问类:Integral
    /// </summary>
    public partial class MysqlIntegral
    {
        public MysqlIntegral()
		{}

        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long AutoID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Integral");
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
        public bool Add(SmartLaw.Model.Integral model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Integral(");
            strSql.Append("SimCardNo,Items,IntegralAdded,TotalIntegral,LastModifyTime)");
            strSql.Append(" values (");
            strSql.Append("@SimCardNo,@Items,@IntegralAdded,@TotalIntegral,@LastModifyTime)");
            MySqlParameter[] parameters = {
					new MySqlParameter("@SimCardNo", MySqlDbType.VarChar,50),
					new MySqlParameter("@Items", MySqlDbType.VarChar,50),
					new MySqlParameter("@IntegralAdded", MySqlDbType.Int32),
					new MySqlParameter("@TotalIntegral", MySqlDbType.Int64,20),
					new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime)};
            parameters[0].Value = model.SimCardNo;
            parameters[1].Value = model.Items;
            parameters[2].Value = model.IntegralAdded;
            parameters[3].Value = model.TotalIntegral;
            parameters[4].Value = model.LastModifyTime;

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
        /// 更新一条数据
        /// </summary>
        public bool Update(SmartLaw.Model.Integral model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Integral set ");
            strSql.Append("SimCardNo=@SimCardNo,");
            strSql.Append("Items=@Items,");
            strSql.Append("IntegralAdded=@IntegralAdded,");
            strSql.Append("TotalIntegral=@TotalIntegral,");
            strSql.Append("LastModifyTime=@LastModifyTime");
            strSql.Append(" where AutoID=@AutoID");
            MySqlParameter[] parameters = {
					new MySqlParameter("@SimCardNo", MySqlDbType.VarChar,50),
					new MySqlParameter("@Items", MySqlDbType.VarChar,50),
					new MySqlParameter("@IntegralAdded", MySqlDbType.Int32),
					new MySqlParameter("@TotalIntegral", MySqlDbType.Int64,20),
					new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime),
					new MySqlParameter("@AutoID", MySqlDbType.Int64,20)};
            parameters[0].Value = model.SimCardNo;
            parameters[1].Value = model.Items;
            parameters[2].Value = model.IntegralAdded;
            parameters[3].Value = model.TotalIntegral;
            parameters[4].Value = model.LastModifyTime;
            parameters[5].Value = model.AutoID;

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
            strSql.Append("delete from Integral ");
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
        public int DeleteList(string[] AutoIDlist)
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
            strSql.Append("delete from Integral ");
            strSql.Append(" where AutoID in (" + _AutoIDlist + ")  ");
            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString());
            return rows;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SmartLaw.Model.Integral GetModel(long AutoID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AutoID,SimCardNo,Items,IntegralAdded,TotalIntegral,LastModifyTime from Integral ");
            strSql.Append(" where AutoID=@AutoID");
            MySqlParameter[] parameters = {
					new MySqlParameter("@AutoID", MySqlDbType.Int64)
			};
            parameters[0].Value = AutoID;

            SmartLaw.Model.Integral model = new SmartLaw.Model.Integral();
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
        public SmartLaw.Model.Integral DataRowToModel(DataRow row)
        {
            SmartLaw.Model.Integral model = new SmartLaw.Model.Integral();
            if (row != null)
            {
                if (row["AutoID"] != null && row["AutoID"].ToString() != "")
                {
                    model.AutoID = long.Parse(row["AutoID"].ToString());
                }
                if (row["SimCardNo"] != null)
                {
                    model.SimCardNo = row["SimCardNo"].ToString();
                }
                if (row["Items"] != null && row["Items"].ToString() != "")
                {
                    model.Items = row["Items"].ToString();
                }
                if (row["IntegralAdded"] != null)
                {
                    model.IntegralAdded = int.Parse(row["IntegralAdded"].ToString());
                }
                if (row["TotalIntegral"] != null)
                {
                    model.TotalIntegral = long.Parse(row["TotalIntegral"].ToString());
                }
                if (row["LastModifyTime"] != null)
                {
                    model.LastModifyTime = DateTime.Parse(row["LastModifyTime"].ToString());
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
            strSql.Append("select AutoID,SimCardNo,Items,IntegralAdded,TotalIntegral,LastModifyTime ");
            strSql.Append(" FROM Integral I ");
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
            strSql.Append("select AutoID,SimCardNo,Items,IntegralAdded,TotalIntegral,LastModifyTime ");
            strSql.Append(" FROM Integral I");
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
            strSql.Append(" AutoID,SimCardNo,Items,IntegralAdded,TotalIntegral,LastModifyTime ");
            strSql.Append(" FROM Integral ");
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
        /// <param name="key">查询条件 0：AutoID 1：SimCardNo 2：Items 3：IntegralAdded 4：TotalIntegral 5:LastModifyTime 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value)
        {
            return GetList(key, value, -1, -1, false);
        }

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：SimCardNo 2：Items 3：IntegralAdded 4：TotalIntegral 5:LastModifyTime 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1：SimCardNo 2：Items 3：IntegralAdded 4：TotalIntegral 5:LastModifyTime 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" AutoID,SimCardNo,Items,IntegralAdded,TotalIntegral,LastModifyTime ");
            strSql.Append(" FROM Integral ");

            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break; 
                case 1: strWhere = "SimCardNo = @SimCardNo";
                    parameter = new MySqlParameter("@SimCardNo", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 2: strWhere = "Items = @Items";
                    parameter = new MySqlParameter("@Items", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 3: strWhere = "IntegralAdded = @IntegralAdded";
                    parameter = new MySqlParameter("@IntegralAdded", MySqlDbType.Int32);
                    parameter.Value =  value;
                    break;
                case 4: strWhere = "TotalIntegral = @TotalIntegral";
                    parameter = new MySqlParameter("@TotalIntegral", MySqlDbType.Int64);
                    parameter.Value = value;
                    break; 
                default: break;
            }
            if (key == 5)
            {
                strWhere = "LastModifyTime between '" + value + " 00:00:00' and '" + value + " 23:59:59'";
            }
            if (strWhere.Trim() != "")
                strSql.Append(" where " + strWhere);

            string strOrder = "";
            switch (filedOrder)
            {
                case 0: strOrder = "AutoID"; break;
                case 1: strOrder = "SimCardNo"; break;
                case 2: strOrder = "Items"; break;
                case 3: strOrder = "IntegralAdded"; break;
                case 4: strOrder = "TotalIntegral"; break;
                case 5: strOrder = "LastModifyTime"; break;
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
            strSql.Append("select count(1) FROM Integral ");
            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "SimCardNo = @SimCardNo";
                    parameter = new MySqlParameter("@SimCardNo", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 2: strWhere = "Items = @Items";
                    parameter = new MySqlParameter("@Items", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 3: strWhere = "IntegralAdded = @IntegralAdded";
                    parameter = new MySqlParameter("@IntegralAdded", MySqlDbType.Int32);
                    parameter.Value = value;
                    break;
                case 4: strWhere = "TotalIntegral = @TotalIntegral";
                    parameter = new MySqlParameter("@TotalIntegral", MySqlDbType.Int64);
                    parameter.Value = value;
                    break; 
                default: break;
            }
            if (key == 5)
            {
                strWhere = "LastModifyTime between '" + value + " 00:00:00' and '" + value + " 23:59:59'";
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
        /// <param name="key">查询条件 0：AutoID 1：SimCardNo 2：Items 3：IntegralAdded 4：TotalIntegral 5:LastModifyTime 其他:全部</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1：SimCardNo 2：Items 3：IntegralAdded 4：TotalIntegral 5:LastModifyTime 其他:全部</param>
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
                case 1: strOrder = "SimCardNo"; break;
                case 2: strOrder = "Items"; break;
                case 3: strOrder = "IntegralAdded"; break;
                case 4: strOrder = "TotalIntegral"; break;
                case 5: strOrder = "LastModifyTime"; break;
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
            strSql.Append(")AS Row, T.*  from Integral T ");


            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "SimCardNo = @SimCardNo";
                    parameter = new MySqlParameter("@SimCardNo", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 2: strWhere = "Items = @Items";
                    parameter = new MySqlParameter("@Items", MySqlDbType.VarChar, 50);
                    parameter.Value = value;
                    break;
                case 3: strWhere = "IntegralAdded = @IntegralAdded";
                    parameter = new MySqlParameter("@IntegralAdded", MySqlDbType.Int32);
                    parameter.Value = value;
                    break;
                case 4: strWhere = "TotalIntegral = @TotalIntegral";
                    parameter = new MySqlParameter("@TotalIntegral", MySqlDbType.Int64);
                    parameter.Value = value;
                    break; 
                default: break;
            }
            if (key == 5)
            {
                strWhere = "LastModifyTime between '" + value + " 00:00:00' and '" + value + " 23:59:59'";
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
    }
}
