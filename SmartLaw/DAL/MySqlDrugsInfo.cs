using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SmartLaw.DBUtility;
using MySql.Data.MySqlClient;

namespace SmartLaw.DAL
{
    /// <summary>
    /// 数据访问类:DrugsInfo
    /// </summary>
    public partial class MySqlDrugsInfo
    {
        public MySqlDrugsInfo()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long AutoID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from DrugsInfo");
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
        public long Add(SmartLaw.Model.DrugsInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into DrugsInfo(");
            strSql.Append("DrugName,Form,FormInfo,Spec,Unit,Price,ValidateTime,District,Company,Policy,Abbr,DataTime)");
            strSql.Append(" values (");
            strSql.Append("@DrugName,@Form,@FormInfo,@Spec,@Unit,@Price,@ValidateTime,@District,@Company,@Policy,@Abbr,@DataTime)");
            strSql.Append(";select @@IDENTITY");
            MySqlParameter[] parameters = {
					new MySqlParameter("@DrugName", MySqlDbType.VarChar,64),
					new MySqlParameter("@Form", MySqlDbType.VarChar,32),
					new MySqlParameter("@FormInfo", MySqlDbType.VarChar,32),
					new MySqlParameter("@Spec", MySqlDbType.VarChar,128),
					new MySqlParameter("@Unit", MySqlDbType.VarChar,16),
					new MySqlParameter("@Price", MySqlDbType.Float),
					new MySqlParameter("@ValidateTime", MySqlDbType.VarChar,16),
					new MySqlParameter("@District", MySqlDbType.VarChar,32),
					new MySqlParameter("@Company", MySqlDbType.VarChar,128),
					new MySqlParameter("@Policy", MySqlDbType.VarChar,64),
                    new MySqlParameter("@Abbr",MySqlDbType.VarChar,32),
                    new MySqlParameter("@DataTime",MySqlDbType.DateTime)};
            parameters[0].Value = model.DrugName;
            parameters[1].Value = model.Form;
            parameters[2].Value = model.FormInfo;
            parameters[3].Value = model.Spec;
            parameters[4].Value = model.Unit;
            parameters[5].Value = model.Price;
            parameters[6].Value = model.ValidateTime;
            parameters[7].Value = model.District;
            parameters[8].Value = model.Company;
            parameters[9].Value = model.Policy;
            parameters[10].Value = GetAbbr(model.DrugName);
            parameters[11].Value = model.DataTime;
            
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
        /// 得到一个对象实体
        /// </summary>
        public SmartLaw.Model.DrugsInfo GetModel(long AutoID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AutoID,DrugName,Form,FormInfo,Spec,Unit,Price,ValidateTime,District,Company,Policy,Abbr,DataTime from DrugsInfo");
            strSql.Append(" where AutoID=@AutoID limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@AutoID", MySqlDbType.Int64)
			};
            parameters[0].Value = AutoID;

            SmartLaw.Model.DrugsInfo model = new SmartLaw.Model.DrugsInfo();
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
        public SmartLaw.Model.DrugsInfo DataRowToModel(DataRow row)
        {
            SmartLaw.Model.DrugsInfo model = new SmartLaw.Model.DrugsInfo();
            if (row != null)
            {
                if (row["AutoID"] != null && row["AutoID"].ToString() != "")
                {
                    model.AutoID = long.Parse(row["AutoID"].ToString());
                }
                if (row["DrugName"] != null)
                {
                    model.DrugName = row["DrugName"].ToString();
                }
                if (row["Form"] != null && row["Form"].ToString() != "")
                {
                    model.Form = row["Form"].ToString();
                }
                if (row["FormInfo"] != null)
                {
                    model.FormInfo= row["FormInfo"].ToString();
                }
                if (row["Spec"] != null)
                {
                    model.Spec= row["Spec"].ToString();
                }
                if (row["Unit"] != null)
                {
                    model.Unit = row["Unit"].ToString();
                }
                if (row["Price"] != null)
                {
                    model.Price =float.Parse( row["Price"].ToString());
                }
                if (row["ValidateTime"] != null && row["ValidateTime"].ToString() != "")
                {
                    model.ValidateTime =row["ValidateTime"].ToString();
                }
                if (row["District"] != null && row["District"].ToString() != "")
                {
                    model.District = row["District"].ToString();
                }
                if (row["Company"] != null)
                {
                    model.Company = row["Company"].ToString();
                }
                if (row["Policy"] != null)
                {
                    model.Policy = row["Policy"].ToString();
                }
                if (row["Abbr"] != null)
                {
                    model.Abbr = row["Abbr"].ToString();
                }
                if (row["DataTime"] != null && row["DataTime"].ToString() != "")
                {
                    model.DataTime = DateTime.Parse(row["DataTime"].ToString());
                }
            }
            return model;
        }


        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1:DrugName,2:Form,3:FormInfo,4:Spec,5:Unit,6:Price,7:ValidateTime,8:District,9:Company,10:Policy,11:Abbr 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value)
        {
            return GetList(key, value, -1, -1, false);
        }

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1:DrugName,2:Form,3:FormInfo,4:Spec,5:Unit,6:Price,7:ValidateTime,8:District,9:Company,10:Policy,11:Abbr 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder"></param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" AutoID,DrugName,Form,FormInfo,Spec,Unit,Price,ValidateTime,District,Company,Policy,Abbr,DataTime ");
            strSql.Append(" FROM DrugsInfo ");

            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "DrugName like @DrugName";
                    parameter = new MySqlParameter("@DrugName", MySqlDbType.VarChar, 64);
                    parameter.Value = "%" + value + "%";
                    break;
                case 2: strWhere = "Form like @Form";
                    parameter = new MySqlParameter("@Form", MySqlDbType.VarChar, 32);
                    parameter.Value = "%" + value + "%";
                    break;
                case 3: strWhere = "FormInfo like @FormInfo";
                    parameter = new MySqlParameter("@FormInfo", MySqlDbType.VarChar, 32);
                    parameter.Value = "%" + value + "%";
                    break;
                case 4: strWhere = "Spec = @Spec";
                    parameter = new MySqlParameter("@Spec", MySqlDbType.VarChar, 128);
                    parameter.Value = value;
                    break;
                case 5: strWhere = "Unit = @Unit";
                    parameter = new MySqlParameter("@Unit", MySqlDbType.VarChar, 16);
                    parameter.Value = value;
                    break;
                case 6: strWhere = "Price = @Price";
                    parameter = new MySqlParameter("@Price", MySqlDbType.Float);
                    parameter.Value = value;
                    break;
                case 7: strWhere = "ValidateTime = @ValidateTime";
                    parameter = new MySqlParameter("@ValidateTime", MySqlDbType.VarChar, 16);
                    parameter.Value = value;
                    break;
                case 8: strWhere = "District = @District";
                    parameter = new MySqlParameter("@District", MySqlDbType.VarChar, 32);
                    parameter.Value = value;
                    break;
                case 9: strWhere = "Company = @Company";
                    parameter = new MySqlParameter("@Company", MySqlDbType.VarChar, 128);
                    parameter.Value = value;
                    break;
                case 10: strWhere = "Policy = @Policy";
                    parameter = new MySqlParameter("@Policy", MySqlDbType.VarChar, 64);
                    parameter.Value = value;
                    break;
                case 11: strWhere = "Abbr like @Abbr";
                    parameter = new MySqlParameter("@Abbr", MySqlDbType.VarChar, 32);
                    parameter.Value = "%" + value + "%";
                    break;
                default: break;
            }
            if (strWhere.Trim() != "")
                strSql.Append(" where " + strWhere);

            string strOrder = "";
            //switch (filedOrder)
            //{
            //    case 0: strOrder = "AutoID"; break;
            //    case 1: strOrder = "Title"; break;
            //    case 2: strOrder = "CategoryID"; break;
            //    case 3: strOrder = "Contents"; break;
            //    case 4: strOrder = "Publisher"; break;
            //    case 5: strOrder = "DataSource"; break;
            //    case 6: strOrder = "DataType"; break;
            //    case 7: strOrder = "LastModifyTime"; break;
            //    case 8: strOrder = "IsValid"; break;
            //    case 9: strOrder = "Checked"; break;
            //    case 10: strOrder = "Checker"; break;
            //    case 11: strOrder = "CheckMemo"; break;
            //    default: break;
            //}
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
        /// 删除所有数据，包括公交线路和站点信息
        /// </summary>
        public bool DeleteAll()
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("Truncate DrugsInfo ");
            
            try
            {

                DbHelperMySQL.ExecuteSql(strSql.ToString());
            }
            catch
            {
                return false;
            }
            return true;

        }





        /// <summary>
        /// 
        /// 获得药品名称缩写
        /// </summary>
        /// <param name="DrugName"></param>
        /// <returns></returns>
        private string GetAbbr(string DrugName)
        {
            char[] ch = DrugName.ToArray();
            StringBuilder rt = new StringBuilder();
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






        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1:DrugName,2:Form,3:FormInfo,4:Spec,5:Unit,6:Price,7:ValidateTime,8:District,9:Company,10:Policy,11:Abbr 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="filedOrder">排序字段</param>
        /// <param name="desc">选用倒序</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="endIndex">结束索引</param>
        /// <returns>数据集</returns>
        public DataSet GetListByPage(int key, string value, int filedOrder, bool desc, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT");
            strSql.Append(" AutoID,DrugName,Form,FormInfo,Spec,Unit,Price,ValidateTime,District,Company,Policy,Abbr,DataTime");
            strSql.Append(" FROM DrugsInfo AS D ");
            string strWhere = "";
            string strOrder = "";
            MySqlParameter parameter = null;

            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "DrugName like @DrugName";
                    parameter = new MySqlParameter("@DrugName", MySqlDbType.VarChar, 64);
                    parameter.Value = "%" + value + "%";
                    break;
                case 2: strWhere = "Form like @Form";
                    parameter = new MySqlParameter("@Form", MySqlDbType.VarChar, 32);
                    parameter.Value = "%" + value + "%";
                    break;
                case 3: strWhere = "FormInfo like @FormInfo";
                    parameter = new MySqlParameter("@FormInfo", MySqlDbType.VarChar, 32);
                    parameter.Value = "%" + value + "%";
                    break;
                case 4: strWhere = "Spec = @Spec";
                    parameter = new MySqlParameter("@Spec", MySqlDbType.VarChar,128);
                    parameter.Value = value;
                    break;
                case 5: strWhere = "Unit = @Unit";
                    parameter = new MySqlParameter("@Unit", MySqlDbType.VarChar, 16);
                    parameter.Value = value;
                    break;
                case 6: strWhere = "Price = @Price";
                    parameter = new MySqlParameter("@Price", MySqlDbType.Float);
                    parameter.Value = value;
                    break;
                case 7: strWhere = "ValidateTime = @ValidateTime";
                    parameter = new MySqlParameter("@ValidateTime", MySqlDbType.VarChar, 16);
                    parameter.Value = value;
                    break;
                case 8: strWhere = "District = @District";
                    parameter = new MySqlParameter("@District", MySqlDbType.VarChar, 32);
                    parameter.Value = value;
                    break;
                case 9: strWhere = "Company = @Company";
                    parameter = new MySqlParameter("@Company", MySqlDbType.VarChar, 64);
                    parameter.Value = value;
                    break;
                case 10: strWhere = "Policy = @Policy";
                    parameter = new MySqlParameter("@Policy", MySqlDbType.VarChar, 64);
                    parameter.Value = value;
                    break;
                case 11: strWhere = "Abbr like @Abbr";
                    parameter = new MySqlParameter("@Abbr", MySqlDbType.VarChar, 32);
                    parameter.Value = "%" + value + "%";
                    break;
                default: break;
            }
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" where " + strWhere);
            }

            //switch (filedOrder)
            //{
            //    case 0: strOrder = "AutoID"; break;
            //    case 1: strOrder = "PZWH"; break;
            //    case 2: strOrder = "CPMC"; break;
            //    case 3: strOrder = "YWMC"; break;
            //    case 4: strOrder = "SPM"; break;
            //    case 5: strOrder = "JX"; break;
            //    case 6: strOrder = "GG"; break;
            //    case 7: strOrder = "SCDW"; break;
            //    case 8: strOrder = "SCDZ"; break;
            //    case 9: strOrder = "CPLB"; break;
            //    case 10: strOrder = "YPZWH"; break;
            //    case 11: strOrder = "PZRQ"; break;
            //    case 12: strOrder = "BWM"; break;
            //    case 13: strOrder = "BWMBZ"; break;
            //    default: break;
            //}

            if (!string.IsNullOrEmpty(strOrder.Trim()))
            {
                if (desc)
                {
                    strSql.Append(" order by D." + strOrder + " desc");
                }
                else
                {
                    strSql.Append(" order by D." + strOrder);
                }
            }
            else
            {
                strSql.Append(" order by D.AutoID desc");
            }


            strSql.AppendFormat(" limit {0} , {1}", startIndex, endIndex);

            if (parameter != null)
                return DbHelperMySQL.Query(strSql.ToString(), parameter);
            else
                return DbHelperMySQL.Query(strSql.ToString());
        }





        /// <summary>
        /// 获取记录总数
        /// 查询条件 0：AutoID 1:DrugName,2:Form,3:FormInfo,4:Spec,5:Unit,6:Price,7:ValidateTime,8:District,9:Company,10:Policy,11:Abbr 其他:全部
        /// </summary>
        public int GetRecordCount(int key, string value)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM DrugsInfo ");
            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "DrugName like @DrugName";
                    parameter = new MySqlParameter("@DrugName", MySqlDbType.VarChar, 64);
                    parameter.Value = "%" + value + "%";
                    break;
                case 2: strWhere = "Form like @Form";
                    parameter = new MySqlParameter("@Form", MySqlDbType.VarChar, 32);
                    parameter.Value = "%" + value + "%";
                    break;
                case 3: strWhere = "FormInfo like @FormInfo";
                    parameter = new MySqlParameter("@FormInfo", MySqlDbType.VarChar, 32);
                    parameter.Value = "%" + value + "%";
                    break;
                case 4: strWhere = "Spec = @Spec";
                    parameter = new MySqlParameter("@Spec", MySqlDbType.VarChar, 128);
                    parameter.Value = value;
                    break;
                case 5: strWhere = "Unit = @Unit";
                    parameter = new MySqlParameter("@Unit", MySqlDbType.VarChar, 16);
                    parameter.Value = value;
                    break;
                case 6: strWhere = "Price = @Price";
                    parameter = new MySqlParameter("@Price", MySqlDbType.Float);
                    parameter.Value = value;
                    break;
                case 7: strWhere = "ValidateTime = @ValidateTime";
                    parameter = new MySqlParameter("@ValidateTime", MySqlDbType.VarChar, 16);
                    parameter.Value = value;
                    break;
                case 8: strWhere = "District = @District";
                    parameter = new MySqlParameter("@District", MySqlDbType.VarChar, 32);
                    parameter.Value = value;
                    break;
                case 9: strWhere = "Company = @Company";
                    parameter = new MySqlParameter("@Company", MySqlDbType.VarChar, 64);
                    parameter.Value = value;
                    break;
                case 10: strWhere = "Policy = @Policy";
                    parameter = new MySqlParameter("@Policy", MySqlDbType.VarChar, 64);
                    parameter.Value = value;
                    break;
                case 11: strWhere = "Abbr like @Abbr";
                    parameter = new MySqlParameter("@Abbr", MySqlDbType.VarChar, 32);
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

        #endregion BasicMethod
    }
}

