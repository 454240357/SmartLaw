using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using SmartLaw.DBUtility;
using System.Data;

namespace SmartLaw.DAL
{
    public partial class MySqlDrugs
    {
        public MySqlDrugs()
		{}
        #region  BasicMethod


        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long AutoID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Drugs");
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
        public long Add(SmartLaw.Model.Drugs model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Drugs(");
            strSql.Append("PZWH,CPMC,YWMC,SPM,JX,GG,SCDW,SCDZ,CPLB,YPZWH,PZRQ,BWM,BWMBZ)");
            strSql.Append(" values (");
            strSql.Append("@PZWH,@CPMC,@YWMC,@SPM,@JX,@GG,@SCDW,@SCDZ,@CPLB,@YPZWH,@PZRQ,@BWM,@BWMBZ)");
            strSql.Append(";select @@IDENTITY");
            MySqlParameter[] parameters = { 
                    new MySqlParameter("@PZWH", MySqlDbType.VarChar,45), 
					new MySqlParameter("@CPMC", MySqlDbType.VarChar,45), 
                    new MySqlParameter("@YWMC", MySqlDbType.Text),
                    new MySqlParameter("@SPM", MySqlDbType.VarChar,45), 
                    new MySqlParameter("@JX", MySqlDbType.VarChar,45), 
                    new MySqlParameter("@GG", MySqlDbType.Text),
                    new MySqlParameter("@SCDW", MySqlDbType.VarChar,45), 
                    new MySqlParameter("@SCDZ", MySqlDbType.VarChar,45), 
					new MySqlParameter("@CPLB", MySqlDbType.VarChar,45), 
					new MySqlParameter("@YPZWH", MySqlDbType.VarChar,45), 
                    new MySqlParameter("@PZRQ", MySqlDbType.DateTime), 
                    new MySqlParameter("@BWM", MySqlDbType.Text),
                    new MySqlParameter("@BWMBZ", MySqlDbType.Text)};
            parameters[0].Value = model.PZWH;
            parameters[1].Value = model.CPMC;
            parameters[2].Value = model.YWMC;
            parameters[3].Value = model.SPM;
            parameters[4].Value = model.JX;
            parameters[5].Value = model.GG;
            parameters[6].Value = model.SCDW;
            parameters[7].Value = model.SCDZ;
            parameters[8].Value = model.CPLB;
            parameters[9].Value = model.YPZWH;
            parameters[10].Value = model.PZRQ;
            parameters[11].Value = model.BWM;
            parameters[12].Value = model.BWMBZ;
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
        public bool Update(SmartLaw.Model.Drugs model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Drugs set ");
            strSql.Append("PZWH=@PZWH,");
            strSql.Append("CPMC=@CPMC,");
            strSql.Append("YWMC=@YWMC,");
            strSql.Append("SPM=@SPM,");
            strSql.Append("JX=@JX,");
            strSql.Append("GG=@GG,");
            strSql.Append("SCDW=@SCDW,");
            strSql.Append("SCDZ=@SCDZ,");
            strSql.Append("CPLB=@CPLB,");
            strSql.Append("YPZWH=@YPZWH,");
            strSql.Append("PZRQ=@PZRQ,");
            strSql.Append("BWM=@BWM,");
            strSql.Append("BWMBZ=@BWMBZ");
            strSql.Append(" where AutoID=@AutoID");
            MySqlParameter[] parameters = { 
                    new MySqlParameter("@PZWH", MySqlDbType.VarChar,45), 
					new MySqlParameter("@CPMC", MySqlDbType.VarChar,45), 
                    new MySqlParameter("@YWMC", MySqlDbType.Text),
                    new MySqlParameter("@SPM", MySqlDbType.VarChar,45), 
                    new MySqlParameter("@JX", MySqlDbType.VarChar,45), 
                    new MySqlParameter("@GG", MySqlDbType.Text),
                    new MySqlParameter("@SCDW", MySqlDbType.VarChar,45), 
                    new MySqlParameter("@SCDZ", MySqlDbType.VarChar,45), 
					new MySqlParameter("@CPLB", MySqlDbType.VarChar,45), 
					new MySqlParameter("@YPZWH", MySqlDbType.VarChar,45), 
                    new MySqlParameter("@PZRQ", MySqlDbType.DateTime), 
                    new MySqlParameter("@BWM", MySqlDbType.Text),
                    new MySqlParameter("@BWMBZ", MySqlDbType.Text)};
            parameters[0].Value = model.PZWH;
            parameters[1].Value = model.CPMC;
            parameters[2].Value = model.YWMC;
            parameters[3].Value = model.SPM;
            parameters[4].Value = model.JX;
            parameters[5].Value = model.GG;
            parameters[6].Value = model.SCDW;
            parameters[7].Value = model.SCDZ;
            parameters[8].Value = model.CPLB;
            parameters[9].Value = model.YPZWH;
            parameters[10].Value = model.PZRQ;
            parameters[11].Value = model.BWM;
            parameters[12].Value = model.BWMBZ;
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
            strSql.Append("delete from Drugs ");
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
            strSql.Append("delete from Drugs ");
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
        public SmartLaw.Model.Drugs GetModel(long AutoID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AutoID,PZWH,CPMC,YWMC,SPM,JX,GG,SCDW,SCDZ,CPLB,YPZWH,PZRQ,BWM,BWMBZ from Drugs");
            strSql.Append(" where AutoID=@AutoID limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@AutoID", MySqlDbType.Int64)
			};
            parameters[0].Value = AutoID;

            SmartLaw.Model.Drugs model = new SmartLaw.Model.Drugs();
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
        public SmartLaw.Model.Drugs DataRowToModel(DataRow row)
        {
            SmartLaw.Model.Drugs model = new SmartLaw.Model.Drugs();
            if (row != null)
            {
                if (row["AutoID"] != null && row["AutoID"].ToString() != "")
                {
                    model.AutoID = long.Parse(row["AutoID"].ToString());
                }
                if (row["PZWH"] != null)
                {
                    model.PZWH = row["PZWH"].ToString();
                }
                if (row["CPMC"] != null)
                {
                    model.CPMC = row["CPMC"].ToString();
                }
                if (row["YWMC"] != null)
                {
                    model.YWMC = row["YWMC"].ToString();
                }
                if (row["SPM"] != null)
                {
                    model.SPM = row["SPM"].ToString();
                }
                if (row["JX"] != null)
                {
                    model.JX = row["JX"].ToString();
                }
                if (row["GG"] != null)
                {
                    model.GG = row["GG"].ToString();
                }
                if (row["SCDW"] != null)
                {
                    model.SCDW =row["SCDW"].ToString();
                }
                if (row["SCDZ"] != null)
                {
                    model.SCDZ = row["SCDZ"].ToString();
                }
                if (row["CPLB"] != null)
                {
                    model.CPLB = row["CPLB"].ToString();
                }
                if (row["YPZWH"] != null)
                {
                    model.YPZWH = row["YPZWH"].ToString();
                }
                if (row["PZRQ"] != null && row["PZRQ"].ToString() != "")
                {
                    model.PZRQ = DateTime.Parse(row["PZRQ"].ToString());
                }
                if (row["BWM"] != null)
                {
                    model.BWM = row["BWM"].ToString();
                }
                if (row["BWMBZ"] != null)
                {
                    model.BWMBZ = row["BWMBZ"].ToString();
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
            strSql.Append("select AutoID,PZWH,CPMC,YWMC,SPM,JX,GG,SCDW,SCDZ,CPLB,YPZWH,PZRQ,BWM,BWMBZ ");
            strSql.Append(" FROM Drugs ");
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
            strSql.Append("select AutoID,PZWH,CPMC,YWMC,SPM,JX,GG,SCDW,SCDZ,CPLB,YPZWH,PZRQ,BWM,BWMBZ ");
            strSql.Append(" FROM Drugs ");
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
            strSql.Append(" AutoID,PZWH,CPMC,YWMC,SPM,JX,GG,SCDW,SCDZ,CPLB,YPZWH,PZRQ,BWM,BWMBZ ");
            strSql.Append(" FROM Drugs ");
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
        /// <param name="key">查询条件 0：AutoID 1：PZWH 2：CPMC 3: YWMC 4：SPM 5：JX 6：GG 7：SCDW 8：SCDZ 9：CPLB 10:YPZWH 11：PZRQ 12：BWM 13: BWMBZ 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value)
        {
            return GetList(key, value, -1, -1, false);
        }

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：PZWH 2：CPMC 3: YWMC 4：SPM 5：JX 6：GG 7：SCDW 8：SCDZ 9：CPLB 10:YPZWH 11：PZRQ 12：BWM 13: BWMBZ 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1：PZWH 2：CPMC 3: YWMC 4：SPM 5：JX 6：GG 7：SCDW 8：SCDZ 9：CPLB 10:YPZWH 11：PZRQ 12：BWM  13: BWMBZ其他:全部</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" AutoID,PZWH,CPMC,YWMC,SPM,JX,GG,SCDW,SCDZ,CPLB,YPZWH,PZRQ,BWM,BWMBZ ");
            strSql.Append(" FROM Drugs ");

            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64, 20);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "PZWH = @PZWH";
                    parameter = new MySqlParameter("@PZWH", MySqlDbType.VarChar, 45);
                    parameter.Value = value;
                    break;
                case 2: strWhere = "CPMC like @CPMC";
                    parameter = new MySqlParameter("@CPMC", MySqlDbType.VarChar, 45);
                    parameter.Value = "%" + value + "%";
                    break;
                case 3: strWhere = "YWMC like @YWMC";
                    parameter = new MySqlParameter("@YWMC", MySqlDbType.Text);
                    parameter.Value = "%" + value + "%";
                    break;
                case 4: strWhere = "SPM like @SPM";
                    parameter = new MySqlParameter("@SPM", MySqlDbType.VarChar, 45);
                    parameter.Value = "%" + value + "%";
                    break;
                case 5: strWhere = "JX = @JX";
                    parameter = new MySqlParameter("@JX", MySqlDbType.VarChar, 45);
                    parameter.Value = value;
                    break;
                case 6: strWhere = "GG like @GG";
                    parameter = new MySqlParameter("@GG", MySqlDbType.Text);
                    parameter.Value = "%" + value + "%";
                    break;
                case 7: strWhere = "SCDW like @SCDW";
                    parameter = new MySqlParameter("@SCDW", MySqlDbType.VarChar, 45);
                    parameter.Value = "%" + value + "%";
                    break;
                case 8: strWhere = "SCDZ like @SCDZ";
                    parameter = new MySqlParameter("@SCDZ", MySqlDbType.VarChar, 45);
                    parameter.Value = "%" + value + "%";
                    break;
                case 9: strWhere = "CPLB = @CPLB";
                    parameter = new MySqlParameter("@CPLB", MySqlDbType.VarChar, 45);
                    parameter.Value = value;
                    break;
                case 10: strWhere = "YPZWH = @YPZWH";
                    parameter = new MySqlParameter("@YPZWH", MySqlDbType.VarChar, 45);
                    parameter.Value = value;
                    break;
                case 11: strWhere = "PZRQ = @PZRQ";
                    parameter = new MySqlParameter("@PZRQ", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 12: strWhere = "BWM like @BWM";
                    parameter = new MySqlParameter("@BWM", MySqlDbType.Text);
                    parameter.Value = "%" + value + "%";
                    break;
                case 13: strWhere = "BWMBZ = @BWMBZ";
                    parameter = new MySqlParameter("@BWMBZ", MySqlDbType.Text);
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
                case 1: strOrder = "PZWH"; break;
                case 2: strOrder = "CPMC"; break;
                case 3: strOrder = "YWMC"; break;
                case 4: strOrder = "SPM"; break;
                case 5: strOrder = "JX"; break;
                case 6: strOrder = "GG"; break;
                case 7: strOrder = "SCDW"; break;
                case 8: strOrder = "SCDZ"; break;
                case 9: strOrder = "CPLB"; break;
                case 10: strOrder = "YPZWH"; break;
                case 11: strOrder = "PZRQ"; break;
                case 12: strOrder = "BWM"; break;
                case 13: strOrder = "BWMBZ"; break;
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
            strSql.Append("select count(1) FROM Drugs ");
            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64, 20);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "PZWH = @PZWH";
                    parameter = new MySqlParameter("@PZWH", MySqlDbType.VarChar, 45);
                    parameter.Value = value;
                    break;
                case 2: strWhere = "CPMC like @CPMC";
                    parameter = new MySqlParameter("@CPMC", MySqlDbType.VarChar, 45);
                    parameter.Value = "%" + value + "%";
                    break;
                case 3: strWhere = "YWMC like @YWMC";
                    parameter = new MySqlParameter("@YWMC", MySqlDbType.Text);
                    parameter.Value = "%" + value + "%";
                    break;
                case 4: strWhere = "SPM like @SPM";
                    parameter = new MySqlParameter("@SPM", MySqlDbType.VarChar, 45);
                    parameter.Value = "%" + value + "%";
                    break;
                case 5: strWhere = "JX = @JX";
                    parameter = new MySqlParameter("@JX", MySqlDbType.VarChar, 45);
                    parameter.Value = value;
                    break;
                case 6: strWhere = "GG like @GG";
                    parameter = new MySqlParameter("@GG", MySqlDbType.Text);
                    parameter.Value = "%" + value + "%";
                    break;
                case 7: strWhere = "SCDW like @SCDW";
                    parameter = new MySqlParameter("@SCDW", MySqlDbType.VarChar, 45);
                    parameter.Value = "%" + value + "%";
                    break;
                case 8: strWhere = "SCDZ like @SCDZ";
                    parameter = new MySqlParameter("@SCDZ", MySqlDbType.VarChar, 45);
                    parameter.Value = "%" + value + "%";
                    break;
                case 9: strWhere = "CPLB = @CPLB";
                    parameter = new MySqlParameter("@CPLB", MySqlDbType.VarChar, 45);
                    parameter.Value = value;
                    break;
                case 10: strWhere = "YPZWH = @YPZWH";
                    parameter = new MySqlParameter("@YPZWH", MySqlDbType.VarChar, 45);
                    parameter.Value = value;
                    break;
                case 11: strWhere = "PZRQ = @PZRQ";
                    parameter = new MySqlParameter("@PZRQ", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 12: strWhere = "BWM = @BWM";
                    parameter = new MySqlParameter("@BWM", MySqlDbType.Text);
                    parameter.Value = value;
                    break;
                case 13: strWhere = "BWMBZ = @BWMBZ";
                    parameter = new MySqlParameter("@BWMBZ", MySqlDbType.Text);
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
        /// <param name="key">查询条件0：AutoID 1：PZWH 2：CPMC 3: YWMC 4：SPM 5：JX 6：GG 7：SCDW 8：SCDZ 9：CPLB 10:YPZWH 11：PZRQ 12：BWM 13: BWMBZ其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="filedOrder">排序字段0：AutoID 1：PZWH 2：CPMC 3: YWMC 4：SPM 5：JX 6：GG 7：SCDW 8：SCDZ 9：CPLB 10:YPZWH 11：PZRQ 12：BWM 13: BWMBZ其他:全部</param>
        /// <param name="desc">选用倒序</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="endIndex">结束索引</param>
        /// <returns>数据集</returns>
        public DataSet GetListByPage(int key, string value, int filedOrder, bool desc, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT");
            strSql.Append(" AutoID,PZWH,CPMC,YWMC,SPM,JX,GG,SCDW,SCDZ,CPLB,YPZWH,PZRQ,BWM,BWMBZ ");
            strSql.Append(" FROM Drugs AS D ");
            string strWhere = "";
            string strOrder = "";
            MySqlParameter parameter = null;
            
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64, 20);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "PZWH = @PZWH";
                    parameter = new MySqlParameter("@PZWH", MySqlDbType.VarChar, 45);
                    parameter.Value = value;
                    break;
                case 2: strWhere = "CPMC like @CPMC";
                    parameter = new MySqlParameter("@CPMC", MySqlDbType.VarChar, 45);
                    parameter.Value = "%" + value + "%";
                    break;
                case 3: strWhere = "YWMC like @YWMC";
                    parameter = new MySqlParameter("@YWMC", MySqlDbType.Text);
                    parameter.Value = "%" + value + "%";
                    break;
                case 4: strWhere = "SPM like @SPM";
                    parameter = new MySqlParameter("@SPM", MySqlDbType.VarChar, 45);
                    parameter.Value = "%" + value + "%";
                    break;
                case 5: strWhere = "JX = @JX";
                    parameter = new MySqlParameter("@JX", MySqlDbType.VarChar, 45);
                    parameter.Value = value;
                    break;
                case 6: strWhere = "GG like @GG";
                    parameter = new MySqlParameter("@GG", MySqlDbType.Text);
                    parameter.Value = "%" + value + "%";
                    break;
                case 7: strWhere = "SCDW like @SCDW";
                    parameter = new MySqlParameter("@SCDW", MySqlDbType.VarChar, 45);
                    parameter.Value = "%" + value + "%";
                    break;
                case 8: strWhere = "SCDZ like @SCDZ";
                    parameter = new MySqlParameter("@SCDZ", MySqlDbType.VarChar, 45);
                    parameter.Value = "%" + value + "%";
                    break;
                case 9: strWhere = "CPLB = @CPLB";
                    parameter = new MySqlParameter("@CPLB", MySqlDbType.VarChar, 45);
                    parameter.Value = value;
                    break;
                case 10: strWhere = "YPZWH = @YPZWH";
                    parameter = new MySqlParameter("@YPZWH", MySqlDbType.VarChar, 45);
                    parameter.Value = value;
                    break;
                case 11: strWhere = "PZRQ = @PZRQ";
                    parameter = new MySqlParameter("@PZRQ", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 12: strWhere = "BWM = @BWM";
                    parameter = new MySqlParameter("@BWM", MySqlDbType.Text);
                    parameter.Value = value;
                    break;
                case 13: strWhere = "BWMBZ = @BWMBZ";
                    parameter = new MySqlParameter("@BWMBZ", MySqlDbType.Text);
                    parameter.Value = value;
                    break;
                default: break;
            }
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" where " + strWhere);
            }

            switch (filedOrder)
            {
                case 0: strOrder = "AutoID"; break;
                case 1: strOrder = "PZWH"; break;
                case 2: strOrder = "CPMC"; break;
                case 3: strOrder = "YWMC"; break;
                case 4: strOrder = "SPM"; break;
                case 5: strOrder = "JX"; break;
                case 6: strOrder = "GG"; break;
                case 7: strOrder = "SCDW"; break;
                case 8: strOrder = "SCDZ"; break;
                case 9: strOrder = "CPLB"; break;
                case 10: strOrder = "YPZWH"; break;
                case 11: strOrder = "PZRQ"; break;
                case 12: strOrder = "BWM"; break;
                case 13: strOrder = "BWMBZ"; break;
                default: break;
            }

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
        #endregion  BasicMethod

    }
}
