using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SmartLaw.DBUtility;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
namespace SmartLaw.DAL
{
    /// <summary>
    /// 数据访问类:UserBehavior
    /// </summary>
    public partial class MySqlUserBehavior
    {
        public MySqlUserBehavior()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(SmartLaw.Model.UserBehavior model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into userbehavior(");
            strSql.Append("SimCardNO,CategoryID,IPAddr,ScanTime,Remarks,Behavior)");
            strSql.Append(" values (");
            strSql.Append("@SimCardNO,@CategoryID,@IPAddr,@ScanTime,@Remarks,@Behavior)");
            strSql.Append(";select @@IDENTITY");
            MySqlParameter[] parameters = {
					new MySqlParameter("@SimCardNO", MySqlDbType.VarChar,50),
					new MySqlParameter("@CategoryID", MySqlDbType.Int64),
					new MySqlParameter("@IPAddr", MySqlDbType.VarChar,15),
					new MySqlParameter("@ScanTime", MySqlDbType.DateTime),
                    new MySqlParameter("@Remarks", MySqlDbType.VarChar,128),
                    new MySqlParameter("@Behavior", MySqlDbType.VarChar,128)};
            parameters[0].Value = model.SimCardNO;
            parameters[1].Value = model.CategoryID;
            parameters[2].Value = model.IPAddr;
            parameters[3].Value = model.ScanTime;
            parameters[4].Value = model.Remarks;
            parameters[5].Value = model.Behavior;

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
        /// 删除一条数据
        /// </summary>
        public bool Delete(long AutoID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from userbehavior");
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
            strSql.Append("delete from userbehavior ");
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
        /// 按Behavior统计某个用户，某个IP，某个时间段，用户行为备注中存在某些情况的记录。
        /// </summary>
        /// <param name="SimCardNO">Sim卡号，不需要针对特定的sim卡号进行统计时可将参数设置为nuLL</param>
        /// <param name="IpAddr">ip地址，不需要针对特定的IP进行统计时可将参数设置为NULL</param>
        /// <param name="Remarks">备注，null</param>
        /// <param name="Behavior">Behavior,null</param>
        /// <param name="beginT">时间段的起始时间。不需要起始时间时可将参数设为DateTime.MinValue</param>
        /// <param name="endT">时间段的结束时间。不需要设置结束时间可将参数设为DateTime.MaxValue</param> 
        /// <returns></returns>
        public DataSet CountUserBh(string SimCardNO, string IpAddr,string Remarks, string Behavior, DateTime beginT, DateTime endT) 
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT Behavior, count(*) FROM userbehavior"); 
            StringBuilder strWhere = new StringBuilder();
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            int flag=0;
            if (SimCardNO!=null)
            {
                strWhere.Append(" where SimCardNO=@SimCardNO");
                MySqlParameter pr=new MySqlParameter("@SimCardNO",MySqlDbType.VarChar);
                pr.Value=SimCardNO;
                parameters.Add(pr); 
                flag = 1;
            } 
            if (IpAddr != null && IpAddr != "")
            {
                if (flag == 1)
                {
                    strWhere.Append(" and IpAddr=@IpAddr");
                }
                else
                {
                    strWhere.Append(" where IpAddr=@IpAddr");
                    flag=1;
                }
                MySqlParameter pr = new MySqlParameter("@IpAddr", MySqlDbType.VarChar);
                pr.Value = IpAddr;
                parameters.Add(pr);

            } 
            if (Remarks != null && Remarks != "")
            {
                if (flag == 1)
                {
                    strWhere.Append(" and Remarks like @Remarks");
                }
                else
                {
                    strWhere.Append(" where Remarks like @Remarks");
                    flag = 1;
                }
                MySqlParameter pr = new MySqlParameter("@Remarks", MySqlDbType.VarChar);
                pr.Value = "%" + Remarks + "%";
                parameters.Add(pr);

            }  
            if (Behavior != null && Behavior != "")
            {
                if (flag == 1)
                {
                    strWhere.Append(" and Behavior like @Behavior");
                }
                else
                {
                    strWhere.Append(" where Behavior like @Behavior");
                    flag = 1;
                }
                MySqlParameter pr = new MySqlParameter("@Behavior", MySqlDbType.VarChar);
                pr.Value = "%" + Behavior + "%";
                parameters.Add(pr); 
            } 
            if (flag == 1)
            {
                strWhere.Append(" and ScanTime>=@beginT and ScanTime<=@endT");
            }
            else
            {
                strWhere.Append(" where ScanTime>=@beginT and ScanTime<=@endT");
            }
            MySqlParameter prt1 = new MySqlParameter("@beginT", MySqlDbType.DateTime);
            prt1.Value = beginT;
            parameters.Add(prt1); 
            MySqlParameter prt2 = new MySqlParameter("@endT", MySqlDbType.DateTime);
            prt2.Value = endT;
            parameters.Add(prt2); 
            strWhere.Append(" GROUP BY Behavior");
            strSql.Append(strWhere.ToString()); 
            return DbHelperMySQL.Query(strSql.ToString(), parameters.ToArray()); 
        }


        /// <summary>
        /// 根据条件获取数据列表 
        /// </summary>
        /// <param name="SimCardNO">Sim卡号，不需要针对特定的sim卡号进行统计时可将参数设置为nuLL</param>
        /// <param name="IpAddr">ip地址，不需要针对特定的IP进行统计时可将参数设置为NULL</param>
        /// <param name="Remarks">备注，null</param>
        /// <param name="Behavior">Behavior,null</param>
        /// <param name="beginT">时间段的起始时间。不需要起始时间时可将参数设为DateTime.MinValue</param>
        /// <param name="endT">时间段的结束时间。不需要设置结束时间可将参数设为DateTime.MaxValue</param> 
        /// <returns></returns>
        public DataSet GetList(string SimCardNO, string IpAddr, string Remarks, string Behavior, DateTime beginT, DateTime endT) 
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM userbehavior"); 
            StringBuilder strWhere = new StringBuilder();
            List<MySqlParameter> parameters = new List<MySqlParameter>();
            int flag = 0;
            if (SimCardNO != null)
            {
                strWhere.Append(" where SimCardNO=@SimCardNO");
                MySqlParameter pr = new MySqlParameter("@SimCardNO", MySqlDbType.VarChar);
                pr.Value = SimCardNO;
                parameters.Add(pr); 
                flag = 1;
            }
            if (IpAddr != null && IpAddr != "")
            {
                if (flag == 1)
                {
                    strWhere.Append(" and IpAddr=@IpAddr");
                }
                else
                {
                    strWhere.Append(" where IpAddr=@IpAddr");
                    flag = 1;
                }
                MySqlParameter pr = new MySqlParameter("@IpAddr", MySqlDbType.VarChar);
                pr.Value = IpAddr;
                parameters.Add(pr);

            }
            if (Remarks != null && Remarks != "")
            {
                if (flag == 1)
                {
                    strWhere.Append(" and Remarks like @Remarks");
                }
                else
                {
                    strWhere.Append(" where Remarks like @Remarks");
                    flag = 1;
                }
                MySqlParameter pr = new MySqlParameter("@Remarks", MySqlDbType.VarChar);
                pr.Value = "%" + Remarks + "%";
                parameters.Add(pr);

            }

            if (Behavior != null && Behavior != "")
            {
                if (flag == 1)
                {
                    strWhere.Append(" and Behavior like @Behavior");
                }
                else
                {
                    strWhere.Append(" where Behavior like @Behavior");
                    flag = 1;
                }
                MySqlParameter pr = new MySqlParameter("@Behavior", MySqlDbType.VarChar);
                pr.Value = "%" + Behavior + "%";
                parameters.Add(pr);

            }
            if (flag == 1)
            {
                strWhere.Append(" and ScanTime>=@beginT and ScanTime<=@endT");
            }
            else
            {
                strWhere.Append(" where ScanTime>=@beginT and ScanTime<=@endT");
            }
            MySqlParameter prt1 = new MySqlParameter("@beginT", MySqlDbType.DateTime);
            prt1.Value = beginT;
            parameters.Add(prt1);
            MySqlParameter prt2 = new MySqlParameter("@endT", MySqlDbType.DateTime);
            prt2.Value = endT;
            parameters.Add(prt2);
            strWhere.Append(" ORDER BY ScanTime DESC");
            strSql.Append(strWhere.ToString());
            return DbHelperMySQL.Query(strSql.ToString(), parameters.ToArray()); 
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SmartLaw.Model.UserBehavior DataRowToModel(DataRow row)
        {
            SmartLaw.Model.UserBehavior model = new SmartLaw.Model.UserBehavior();
            if (row != null)
            {
                if (row["AutoID"] != null && row["AutoID"].ToString() != "")
                {
                    model.AutoID = long.Parse(row["AutoID"].ToString());
                }
                if (row["SimCardNO"] != null)
                {
                    model.SimCardNO = row["SimCardNO"].ToString();
                }
                if (row["CategoryID"] != null && row["CategoryID"].ToString() != "")
                {
                    model.CategoryID = long.Parse(row["CategoryID"].ToString());
                }
                if (row["Behavior"] != null && row["Behavior"].ToString() != "")
                {
                    model.Behavior = row["Behavior"].ToString();
                }
                if (row["ScanTime"] != null && row["ScanTime"].ToString() != "")
                {
                    model.ScanTime = DateTime.Parse(row["ScanTime"].ToString());
                }
                if (row["IPAddr"] != null && row["IPAddr"].ToString() != "")
                {
                    model.IPAddr = row["IPAddr"].ToString();
                }
                if (row["Remarks"] != null)
                {
                    model.Remarks = row["Remarks"].ToString();
                } 
            }
            return model;
        }


        #endregion  ExtensionMethod
    }
}

