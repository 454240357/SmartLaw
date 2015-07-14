using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmartLaw.DBUtility;
using MySql.Data.MySqlClient;//Please add references

namespace SmartLaw.DAL
{
	/// <summary>
	/// 数据访问类:BusLine
	/// </summary>
	public partial class MySqlBusLine
	{
        public MySqlBusLine()
		{}
		#region  BasicMethod

		/// <summary>
		/// 是否存在该公交线路
		/// </summary>
		public bool ExistsBusLine(long AutoID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from BusLine");
			strSql.Append(" where AutoID=@AutoID");
			MySqlParameter[] parameters = {
					new MySqlParameter("@AutoID", MySqlDbType.Int64)
			};
			parameters[0].Value = AutoID;

			return DbHelperMySQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 是否存在该站点
        /// </summary>
        /// <param name="StationName">站点名称</param>
        /// <returns></returns>
        public bool ExistsStation(string StationName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Station");
            strSql.Append(" where StationName=@StationName");
            MySqlParameter[] parameters = {
					new MySqlParameter("@StationName", MySqlDbType.VarChar,64)};
            parameters[0].Value =StationName;

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }





		/// <summary>
		/// 增加一条公交线路数据
		/// </summary>
		public long AddBusLine(SmartLaw.Model.BusLine model,out string msg)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into BusLine(");
            strSql.Append("RouteName,Station,Remarks)");
			strSql.Append(" values (");
            strSql.Append("@RouteName,@Station,@Remarks)");
			strSql.Append(";select @@IDENTITY");
			MySqlParameter[] parameters = {
					new MySqlParameter("@RouteName", MySqlDbType.VarChar,128),
					new MySqlParameter("@Station", MySqlDbType.VarChar,2048),
					new MySqlParameter("@Remarks", MySqlDbType.VarChar,128)};
			parameters[0].Value = model.RouteName;
			parameters[1].Value = getStationString(model.Station);
			parameters[2].Value = model.Remarks;
			
			object obj = DbHelperMySQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
                msg = "插入线路信息失败";
				return 0;
			}
			else
			{
                msg="";
                
                foreach (string s in model.Station)
                {
                    if (!ExistsStation(s))
                    {
                       bool bl= AddStation(s);
                       if (!bl)
                       {
                           msg += "添加站点" + s + "失败|";
                       }
                    }
                }



				return Convert.ToInt64(obj);
			}
		}



        /// <summary>
        /// 增加一个站点信息
        /// </summary>
        public bool AddStation(string StationName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Station(");
            strSql.Append("StationName,Abbr)");
            strSql.Append(" values (");
            strSql.Append("@StationName,@Abbr)");
            
            MySqlParameter[] parameters = {
                    new MySqlParameter("@StationName", MySqlDbType.VarChar,64),
					new MySqlParameter("@Abbr", MySqlDbType.VarChar,32)};

            parameters[0].Value = StationName;
            parameters[1].Value = GetAbbr(StationName);
           


            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows>0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// 获得站点缩写
        /// </summary>
        /// <param name="StationName"></param>
        /// <returns></returns>
        private string GetAbbr(string StationName)
        {
            char[] ch = StationName.ToArray();
            StringBuilder rt=new StringBuilder();
            for (int i = 0; i < ch.Length; ++i)
            {
                rt.Append(GetCharSpellCode(ch[i].ToString()));
            }
            return rt.ToString();
        }

        /// <summary>
        /// 获得每个汉字的拼音首字母
        /// </summary>
        /// <param name="CnChar"></param>
        /// <returns></returns>
        private static string GetCharSpellCode(string CnChar)
        {
            long iCnChar;
            byte[] ZW = System.Text.Encoding.Default.GetBytes(CnChar);
            //如果是字母，则直接返回 
            if (ZW.Length == 1)
            {
                return CnChar.ToUpper();
            }
            else
            {
                //   get   the     array   of   byte   from   the   single   char    
                int i1 = (short)(ZW[0]);
                int i2 = (short)(ZW[1]);
                iCnChar = i1 * 256 + i2;
            }
            #region table   of   the   constant   list
            //expresstion 
            //table   of   the   constant   list 
            // 'A';           //45217..45252 
            // 'B';           //45253..45760 
            // 'C';           //45761..46317 
            // 'D';           //46318..46825 
            // 'E';           //46826..47009 
            // 'F';           //47010..47296 
            // 'G';           //47297..47613
            // 'H';           //47614..48118 
            // 'J';           //48119..49061 
            // 'K';           //49062..49323 
            // 'L';           //49324..49895 
            // 'M';           //49896..50370 
            // 'N';           //50371..50613 
            // 'O';           //50614..50621 
            // 'P';           //50622..50905 
            // 'Q';           //50906..51386
            // 'R';           //51387..51445 
            // 'S';           //51446..52217 
            // 'T';           //52218..52697 
            //没有U,V 
            // 'W';           //52698..52979 
            // 'X';           //52980..53640 
            // 'Y';           //53689..54480 
            // 'Z';           //54481..55289 
            #endregion
            //   iCnChar match     the   constant 
            if ((iCnChar >= 45217) && (iCnChar <= 45252))
            {
                return "A";
            }
            else if ((iCnChar >= 45253) && (iCnChar <= 45760))
            {
                return "B";
            }
            else if ((iCnChar >= 45761) && (iCnChar <= 46317))
            {
                return "C";
            }
            else if ((iCnChar >= 46318) && (iCnChar <= 46825))
            {
                return "D";
            }
            else if ((iCnChar >= 46826) && (iCnChar <= 47009))
            {
                return "E";
            }
            else if ((iCnChar >= 47010) && (iCnChar <= 47296))
            {
                return "F";
            }
            else if ((iCnChar >= 47297) && (iCnChar <= 47613))
            {
                return "G";
            }
            else if ((iCnChar >= 47614) && (iCnChar <= 48118))
            {
                return "H";
            }
            else if ((iCnChar >= 48119) && (iCnChar <= 49061))
            {
                return "J";
            }
            else if ((iCnChar >= 49062) && (iCnChar <= 49323))
            {
                return "K";
            }
            else if ((iCnChar >= 49324) && (iCnChar <= 49895))
            {
                return "L";
            }
            else if ((iCnChar >= 49896) && (iCnChar <= 50370))
            {
                return "M";
            }
            else if ((iCnChar >= 50371) && (iCnChar <= 50613))
            {
                return "N";
            }
            else if ((iCnChar >= 50614) && (iCnChar <= 50621))
            {
                return "O";
            }
            else if ((iCnChar >= 50622) && (iCnChar <= 50905))
            {
                return "P";
            }
            else if ((iCnChar >= 50906) && (iCnChar <= 51386))
            {
                return "Q";
            }
            else if ((iCnChar >= 51387) && (iCnChar <= 51445))
            {
                return "R";
            }
            else if ((iCnChar >= 51446) && (iCnChar <= 52217))
            {
                return "S";
            }
            else if ((iCnChar >= 52218) && (iCnChar <= 52697))
            {
                return "T";
            }
            else if ((iCnChar >= 52698) && (iCnChar <= 52979))
            {
                return "W";
            }
            else if ((iCnChar >= 52980) && (iCnChar <= 53688))
            {
                return "X";
            }
            else if ((iCnChar >= 53689) && (iCnChar <= 54480))
            {
                return "Y";
            }
            else if ((iCnChar >= 54481) && (iCnChar <= 55289))
            {
                return "Z";
            }
            else return ("?");
        }




        private string getStationString(string[] Answers)
        {
            string re = "";
            foreach (string i in Answers)
            {
                re += i + "|";
            }
            if (re.EndsWith("|"))
                re = re.Remove(re.Length - 1);
            return re;
        }

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(long AutoID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from BusLine ");
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
        /// 删除所有数据，包括公交线路和站点信息
        /// </summary>
        public bool DeleteAll()
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("Truncate BusLine ");
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("Truncate Station ");
            try
            {

                DbHelperMySQL.ExecuteSql(strSql.ToString());


                DbHelperMySQL.ExecuteSql(strSql2.ToString());
            }
            catch
            {
                return false;
            }
            return true;
          
        }


        /// <summary>
        /// 根据条件获取线路信息
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：RouteName 2：Station 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>数据集</returns>
        public DataSet GetBusLineList(int key, string value)
        {
            return GetBusLineList(key, value, -1, -1, false);
        }




        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件  0：AutoID 1：RouteName 2：Station 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段  0：AutoID 1：RouteName 2：Station 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public DataSet GetBusLineList(int key, string value, int Top, int filedOrder, bool desc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" DISTINCT(RouteName),AutoID,Station,Remarks");
            strSql.Append(" FROM BusLine ");

            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "RouteName like @RouteName";
                    parameter = new MySqlParameter("@RouteName", MySqlDbType.VarChar, 128);
                    parameter.Value = "%" + value + "%";
                    break;
                case 2: strWhere = "Station like @Station";
                    parameter = new MySqlParameter("@Station", MySqlDbType.VarChar);
                    parameter.Value = "%" + value + "%";
                    break; 
                default: break;
            }
            if (strWhere.Trim() != "")
                strSql.Append(" where " + strWhere);

            string strOrder = "";
            switch (filedOrder)
            {
                case 0: strOrder = "AutoID"; break;
                case 1: strOrder = "RouteName"; break;
                case 2: strOrder = "Station"; break; 
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

        public string[] getStationName(string Abbr)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select StationName FROM Station where Abbr like @Abbr");
            MySqlParameter parameter = null;
            parameter = new MySqlParameter("@Abbr", MySqlDbType.VarChar);
            parameter.Value = "%" + Abbr + "%";
            DataSet ds= DbHelperMySQL.Query(strSql.ToString(), parameter);
            List<string> stNames=new List<string>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; ++i)
                {
                    stNames.Add(ds.Tables[0].Rows[i]["StationName"].ToString());
                }
                return stNames.ToArray();
            }
            return null;
        }







        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SmartLaw.Model.BusLine DataRowToModel(DataRow row)
        {
            SmartLaw.Model.BusLine model = new SmartLaw.Model.BusLine();
            if (row != null)
            {
                if (row["AutoID"] != null && row["AutoID"].ToString() != "")
                {
                    model.AutoID = long.Parse(row["AutoID"].ToString());
                }
                if (row["RouteName"] != null)
                {
                    model.RouteName = row["RouteName"].ToString();
                }
                if (row["Station"] != null && row["Station"].ToString() != "")
                {
                    
                    model.Station = row["Station"].ToString().Split('|');
                }
                if (row["Remarks"] != null)
                {
                    model.Remarks = row["Remarks"].ToString();
                }          
            }
            return model;
        }


    #endregion  BasicMethod
    }
}
