using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SmartLaw.DBUtility;
using MySql.Data.MySqlClient;

namespace SmartLaw.DAL
{
	/// <summary>
	/// 数据访问类:Prize
	/// </summary>
    public partial class MySqlPrize
    {
        public MySqlPrize()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long AutoID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Prize");
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
        public long Add(SmartLaw.Model.Prize model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Prize(");
            strSql.Append("PrizeName,PrizeUnit,Points,Registrant,Stock,Remarks,RegTime,Picture)");
            strSql.Append(" values (");
            strSql.Append("@PrizeName,@PrizeUnit,@Points,@Registrant,@Stock,@Remarks,@RegTime,@Picture)");
            strSql.Append(";select @@IDENTITY");
            MySqlParameter[] parameters = {
					new MySqlParameter("@PrizeName", MySqlDbType.VarChar,64),
					new MySqlParameter("@PrizeUnit", MySqlDbType.VarChar,16),
					new MySqlParameter("@Points", MySqlDbType.Int32),
					new MySqlParameter("@Registrant", MySqlDbType.VarChar,16),
					new MySqlParameter("@Stock", MySqlDbType.Int64),
					new MySqlParameter("@Remarks", MySqlDbType.VarChar,255),
					new MySqlParameter("@RegTime", MySqlDbType.DateTime),
					new MySqlParameter("@Picture", MySqlDbType.VarChar,128)};
            parameters[0].Value = model.PrizeName;
            parameters[1].Value = model.PrizeUnit;
            parameters[2].Value = model.Points;
            parameters[3].Value = model.Registrant;
            parameters[4].Value = model.Stock;
            parameters[5].Value = model.Remarks;
            parameters[6].Value = model.RegTime;
            parameters[7].Value = model.Picture;
           
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
        public bool Update(SmartLaw.Model.Prize model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Prize set ");
            strSql.Append("PrizeName=@PrizeName,");
            strSql.Append("PrizeUnit=@PrizeUnit,");
            strSql.Append("Points=@Points,");
            strSql.Append("Registrant=@Registrant,");
            strSql.Append("Stock=@Stock,");
            strSql.Append("Remarks=@Remarks,");
            strSql.Append("RegTime=@RegTime,");
            strSql.Append("Picture=@Picture");
            strSql.Append(" where AutoID=@AutoID");
            MySqlParameter[] parameters = {
					new MySqlParameter("@PrizeName", MySqlDbType.VarChar,64),
					new MySqlParameter("@PrizeUnit", MySqlDbType.VarChar,16),
					new MySqlParameter("@Points", MySqlDbType.Int32),
					new MySqlParameter("@Registrant", MySqlDbType.VarChar,16),
					new MySqlParameter("@Stock", MySqlDbType.Int64),
					new MySqlParameter("@Remarks", MySqlDbType.VarChar,255),
					new MySqlParameter("@RegTime", MySqlDbType.DateTime),
					new MySqlParameter("@Picture", MySqlDbType.VarChar,128),
					new MySqlParameter("@AutoID", MySqlDbType.Int64)};
            parameters[0].Value = model.PrizeName;
            parameters[1].Value = model.PrizeUnit;
            parameters[2].Value = model.Points;
            parameters[3].Value = model.Registrant;
            parameters[4].Value = model.Stock;
            parameters[5].Value = model.Remarks;
            parameters[6].Value = model.RegTime;
            parameters[7].Value = model.Picture;
            parameters[8].Value = model.AutoID;

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
            strSql.Append("delete from Prize ");
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
            strSql.Append("delete from Prize ");
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
        public SmartLaw.Model.Prize GetModel(long AutoID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AutoID,PrizeName,PrizeUnit,Points,Registrant,Stock,Remarks,RegTime,Picture from Prize");
            strSql.Append(" where AutoID=@AutoID limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@AutoID", MySqlDbType.Int64)
			};
            parameters[0].Value = AutoID;

            SmartLaw.Model.Prize model = new SmartLaw.Model.Prize();
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
        public SmartLaw.Model.Prize DataRowToModel(DataRow row)
        {
            SmartLaw.Model.Prize model = new SmartLaw.Model.Prize();
            if (row != null)
            {
                if (row["AutoID"] != null)
                {
                    model.AutoID = long.Parse(row["AutoID"].ToString());
                }
                if (row["PrizeName"] != null && row["PrizeName"].ToString() != "")
                {
                    model.PrizeName = row["PrizeName"].ToString();
                }
                if (row["PrizeUnit"] != null && row["PrizeUnit"].ToString() != "")
                {
                    model.PrizeUnit = row["PrizeUnit"].ToString();
                }
                if (row["Points"] != null)
                {
                    model.Points =int.Parse( row["Points"].ToString());
                }
                if (row["Registrant"] != null && row["Registrant"].ToString() != "")
                {
                    model.Registrant = row["Registrant"].ToString();
                }
                if (row["Stock"] != null)
                {
                    model.Stock = long.Parse( row["Stock"].ToString());
                }
                if (row["Remarks"] != null && row["Remarks"].ToString() != "")
                {
                    model.Remarks = row["Remarks"].ToString();
                }
                if (row["RegTime"] != null && row["RegTime"].ToString() != "")
                {
                    model.RegTime = DateTime.Parse(row["RegTime"].ToString());
                }
                if (row["Picture"] != null && row["Picture"].ToString() != "")
                {
                    model.Picture = row["Picture"].ToString();
                }
               
            }
            return model;
        }





        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：PrizeName,2:PrizeUnit,3:Points,4:Registrant,5:Stock,6:Remarks,7:RegTime,8:Picture 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value)
        {
            return GetList(key, value, -1, -1, false);
        }



        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件0：AutoID 1：PrizeName,2:PrizeUnit,3:Points,4:Registrant,5:Stock,6:Remarks,7:RegTime,8:Picture 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1：PrizeName,2:PrizeUnit,3:Points,4:Registrant,5:Stock,6:Remarks,7:RegTime,8:Picture 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" AutoID,PrizeName,PrizeUnit,Points,Registrant,Stock,Remarks,RegTime,Picture ");
            strSql.Append(" FROM Prize ");

            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "PrizeName like @PrizeName";
                    parameter = new MySqlParameter("@PrizeName", MySqlDbType.VarChar, 64);
                    parameter.Value = "%" + value + "%";
                    break;
                case 2: strWhere = "PrizeUnit=@PrizeUnit";
                    parameter = new MySqlParameter("@PrizeUnit", MySqlDbType.VarChar,16);
                    parameter.Value = value;
                    break;
                case 3: strWhere = "Points=@Points";
                    parameter = new MySqlParameter("@Points", MySqlDbType.Int32);
                    parameter.Value =  value;
                    break;
                case 4: strWhere = "Registrant like @Registrant";
                    parameter = new MySqlParameter("@Registrant", MySqlDbType.VarChar, 16);
                    parameter.Value = "%" + value + "%";
                    break;
                case 5: strWhere = "Stock=@Stock";
                    parameter = new MySqlParameter("@Stock", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 6: strWhere = "Remarks=@Remarks";
                    parameter = new MySqlParameter("@Remarks", MySqlDbType.VarChar, 255);
                    parameter.Value = value;
                    break;
                case 7: strWhere = "RegTime=@RegTime";
                    parameter = new MySqlParameter("@RegTime", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 8: strWhere = "Picture=@Picture";
                    parameter = new MySqlParameter("@Picture", MySqlDbType.VarChar, 128);
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
                case 1: strOrder = "PrizeName"; break;
                case 2: strOrder = "PrizeUnit"; break;
                case 3: strOrder = "Points"; break;
                case 4: strOrder = "Registrant"; break;
                case 5: strOrder = "Stock"; break;
                case 6: strOrder = "Remarks"; break;
                case 7: strOrder = "RegTime"; break;
                case 8: strOrder = "Picture"; break;
                
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




        #endregion  BasicMethod
    }
}
