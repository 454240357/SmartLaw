using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SmartLaw.DBUtility;
using System.Collections.Generic;
using System.Collections;
using MySql.Data.MySqlClient;

namespace SmartLaw.DAL
{
    /// <summary>
    /// 数据访问类:UserPrize
    /// </summary>
    public partial class MySqlUserPrize
    {
        public MySqlUserPrize()
        { }
        #region  BasicMethod

        private readonly SmartLaw.DAL.MysqlIntegral dal = new SmartLaw.DAL.MysqlIntegral();
        private readonly SmartLaw.DAL.MySqlPrize dalPZ = new SmartLaw.DAL.MySqlPrize();




        ///// <summary>
        ///// 增加一条兑换信息
        ///// </summary>
        public bool Add(SmartLaw.Model.UserPrize model,out string msg)
        {

            //插入用户兑换奖品信息的sql
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UserPrize(");
            strSql.Append("SimCardNO,PrizeID,Amount,IsTaken,TakenTime,Remarks)");
            strSql.Append(" values (");
            strSql.Append("@SimCardNO,@PrizeID,@Amount,@IsTaken,@TakenTime,@Remarks)");
            strSql.Append(";select @@IDENTITY");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@SimCardNO", MySqlDbType.VarChar,50),
                    new MySqlParameter("@PrizeID", MySqlDbType.Int64),
                    new MySqlParameter("@Amount", MySqlDbType.Int32),
                    new MySqlParameter("@IsTaken", MySqlDbType.Bit),
                    new MySqlParameter("@TakenTime", MySqlDbType.DateTime),
                    new MySqlParameter("@Remarks", MySqlDbType.VarChar,255)};
            parameters[0].Value = model.SimCardNO;
            parameters[1].Value = model.PrizeID;
            parameters[2].Value = model.Amount;
            parameters[3].Value = model.IsTaken;
            parameters[4].Value = model.TakenTime;
            parameters[5].Value = model.Remarks;

            //查询兑换该奖品需要多少积分
            SmartLaw.Model.Prize prize = dalPZ.GetModel(model.PrizeID);
            int needpoints = (prize.Points) * (model.Amount);

            //检查奖品库存
            if (prize.Stock - model.Amount < 0)
            {
                msg = "库存不足";
                return false;
            }

            //修改用户积分信息
            DataSet ds = dal.GetList(1, model.SimCardNO, 1, 5, true);
            List<SmartLaw.Model.Integral> igList = DataTableToListForInt(ds.Tables[0]);
            StringBuilder strSql2 = new StringBuilder();

            //检查用户积分是否够兑换产品
            if (igList[0].TotalIntegral - needpoints < 0)
            {
                msg = "积分不足";
                return false;
            }

            //插入积分变化信息的sql
            strSql2.Append("insert into Integral(");
            strSql2.Append("SimCardNo,Items,IntegralAdded,TotalIntegral,LastModifyTime)");
            strSql2.Append(" values (");
            strSql2.Append("@SimCardNo,@Items,@IntegralAdded,@TotalIntegral,@LastModifyTime)");
            MySqlParameter[] parameters2 = {
					new MySqlParameter("@SimCardNo", MySqlDbType.VarChar,50),
					new MySqlParameter("@Items", MySqlDbType.VarChar,50),
					new MySqlParameter("@IntegralAdded", MySqlDbType.Int32),
					new MySqlParameter("@TotalIntegral", MySqlDbType.Int64,20),
					new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime)};
            parameters2[0].Value = igList[0] .SimCardNo;
            parameters2[1].Value = "兑换奖品";
            parameters2[2].Value =-needpoints;
            parameters2[3].Value = igList[0].TotalIntegral-needpoints;
            parameters2[4].Value = DateTime.Now;

            //减少产品库存的sql
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("update Prize set ");
            strSql3.Append("Stock=@Stock"); 
            strSql3.Append(" where AutoID=@AutoID");
            MySqlParameter[] parameters3 = {
					new MySqlParameter("@Stock", MySqlDbType.Int64),
					new MySqlParameter("@AutoID", MySqlDbType.Int64)};
            parameters3[0].Value =prize.Stock-model.Amount;
            parameters3[1].Value = prize.AutoID;



            Hashtable SQLStringList = new Hashtable();
            SQLStringList.Add(strSql.ToString(), parameters);
            SQLStringList.Add(strSql2.ToString(), parameters2);
            SQLStringList.Add(strSql3.ToString(), parameters3);


            try
            {
                DbHelperMySQL.ExecuteSqlTran(SQLStringList);
                msg = "兑换成功";
                return true;
            }
            catch(Exception ee)
            {
                msg = ee.ToString();
                return false;
            }
        }


        /// <summary>
        /// 得到一条数据
        /// </summary>
        public SmartLaw.Model.UserPrize GetModel(long AutoID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select AutoID,SimCardNO,PrizeID,Amount,IsTaken,TakenTime,Remarks");
            strSql.Append(" FROM UserPrize ");
            strSql.Append(" where AutoID=@AutoID");
            MySqlParameter[] parameters = {
					new MySqlParameter("@AutoID", MySqlDbType.Int64)			};
            parameters[0].Value = AutoID;

            SmartLaw.Model.UserPrize model = new SmartLaw.Model.UserPrize();
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
        /// 取消一次兑换
        /// </summary>
        public bool Delete(SmartLaw.Model.UserPrize model, out string msg)
        {
            if (model.IsTaken == true)
            {
                msg = "该奖品已领取，不可取消";
                return false;
            }

            //取消用户兑换奖品信息的sql
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UserPrize where AutoID=@AutoID");
            MySqlParameter[] parameter = {new MySqlParameter("@AutoID", MySqlDbType.Int64)};
            parameter[0].Value = model.AutoID;



            //查询兑换该奖品需要多少积分
            SmartLaw.Model.Prize prize = dalPZ.GetModel(model.PrizeID);
            int needpoints = (prize.Points) * (model.Amount);

            //修改用户积分信息
            DataSet ds = dal.GetList(1, model.SimCardNO, 1, 5, true);
            List<SmartLaw.Model.Integral> igList = DataTableToListForInt(ds.Tables[0]);
            StringBuilder strSql2 = new StringBuilder();

            //插入积分变化信息的sql
            strSql2.Append("insert into Integral(");
            strSql2.Append("SimCardNo,Items,IntegralAdded,TotalIntegral,LastModifyTime)");
            strSql2.Append(" values (");
            strSql2.Append("@SimCardNo,@Items,@IntegralAdded,@TotalIntegral,@LastModifyTime)");
            MySqlParameter[] parameters2 = {
                    new MySqlParameter("@SimCardNo", MySqlDbType.VarChar,50),
                    new MySqlParameter("@Items", MySqlDbType.VarChar,50),
                    new MySqlParameter("@IntegralAdded", MySqlDbType.Int32),
                    new MySqlParameter("@TotalIntegral", MySqlDbType.Int64,20),
                    new MySqlParameter("@LastModifyTime", MySqlDbType.DateTime)};
            parameters2[0].Value = igList[0].SimCardNo;
            parameters2[1].Value = "取消兑换奖品";
            parameters2[2].Value = needpoints;
            parameters2[3].Value = igList[0].TotalIntegral +needpoints;
            parameters2[4].Value = DateTime.Now;



            //增加产品库存的sql
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("update Prize set ");
            strSql3.Append("Stock=@Stock");
            strSql3.Append(" where AutoID=@AutoID");
            MySqlParameter[] parameters3 = {
                    new MySqlParameter("@Stock", MySqlDbType.Int64),
                    new MySqlParameter("@AutoID", MySqlDbType.Int64)};
            parameters3[0].Value = prize.Stock + model.Amount;
            parameters3[1].Value = prize.AutoID;

            Hashtable SQLStringList = new Hashtable();
            SQLStringList.Add(strSql.ToString(), parameter);
            SQLStringList.Add(strSql2.ToString(), parameters2);
            SQLStringList.Add(strSql3.ToString(), parameters3);

            try
            {
                DbHelperMySQL.ExecuteSqlTran(SQLStringList);
                msg = "成功取消兑换";
                return true;
            }
            catch (Exception ee)
            {
                msg = ee.ToString();
                return false;
            }
        }



        /// <summary>
        /// 将积分的DateTable转成实体类集合
        /// </summary>
        public List<SmartLaw.Model.Integral> DataTableToListForInt(DataTable dt)
        {
            List<SmartLaw.Model.Integral> modelList = new List<SmartLaw.Model.Integral>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SmartLaw.Model.Integral model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }





 


     /// <summary>
     /// 将某userprize设置为已领取，领取时间可设置，若不设置默认为当前时间
     /// </summary>
     /// <param name="model"></param>
     /// <param name="msg"></param>
     /// <returns></returns>
        public bool PrizeTaken(SmartLaw.Model.UserPrize model,out string msg)
        { 
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserPrize set ");
            strSql.Append("IsTaken=TRUE,");
            strSql.Append("TakenTime=@TakenTime");
           
            strSql.Append(" where AutoID=@AutoID");
            MySqlParameter[] parameters = {
					new MySqlParameter("@TakenTime", MySqlDbType.DateTime),
					new MySqlParameter("@AutoID", MySqlDbType.Int64)};
            parameters[0].Value =(model.TakenTime==DateTime.MinValue? DateTime.Now:model.TakenTime);
            parameters[1].Value = model. AutoID;

            try
            {
                int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
                if (rows > 0)
                {
                    msg = "领取成功";
                    return true;
                }
                else
                {
                    msg = "不存在该记录";
                    return false;
                }
            }
            catch(Exception ee)
            {
                msg = ee.ToString();
                return false;
            }

        }




        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：PrizeID 2：Amount 3:IsTaken 4：TakenTime 5:Remarks 6:SimCardNO 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value)
        {
            return GetList(key, value, -1, -1, false);
        }

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：PrizeID 2：Amount 3:IsTaken 4：TakenTime 5:Remarks 6:SimCardNO 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">查询条件 0：AutoID 1：PrizeID 2：Amount 3:IsTaken 4：TakenTime 5:Remarks 6:SimCardNO 其他:全部</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" AutoID,PrizeID,Amount,IsTaken,TakenTime,Remarks,SimCardNO ");
            strSql.Append(" FROM UserPrize ");

            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "AutoID = @AutoID";
                    parameter = new MySqlParameter("@AutoID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
                case 1: strWhere = "PrizeID = @PrizeID";
                    parameter = new MySqlParameter("@PrizeID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break; 
                case 2: strWhere = "Amount = @Amount";
                    parameter = new MySqlParameter("@Amount", MySqlDbType.Int32);
                    parameter.Value = value;
                    break;
                case 3: strWhere = "IsTaken = @IsTaken";
                    parameter = new MySqlParameter("@IsTaken", MySqlDbType.Bit);
                    parameter.Value = value;
                    break;
                case 4: strWhere = "TakenTime = @TakenTime";
                    parameter = new MySqlParameter("@TakenTime", MySqlDbType.DateTime);
                    parameter.Value = value;
                    break;
                case 5: strWhere = "Remarks like @Remarks";
                    parameter = new MySqlParameter("@Remarks", MySqlDbType.VarChar,255);
                    parameter.Value = "%" + value + "%";
                    break;
                case 6: strWhere = "SimCardNO = @SimCardNO";
                    parameter = new MySqlParameter("@SimCardNO", MySqlDbType.VarChar, 50);
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
                case 1: strOrder = "PrizeID"; break;
                case 2: strOrder = "Amount"; break;
                case 3: strOrder = "IsTaken"; break;
                case 4: strOrder = "TakenTime"; break;
                case 5: strOrder = "Remarks"; break;
                case 6: strOrder = "SimCardNO"; break;
               
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
        /// 得到一个对象实体
        /// </summary>
        public SmartLaw.Model.UserPrize DataRowToModel(DataRow row)
        {
            SmartLaw.Model.UserPrize model = new SmartLaw.Model.UserPrize();
            if (row != null)
            {
                if (row["AutoID"] != null && row["AutoID"].ToString() != "")
                {
                    model.AutoID = long.Parse(row["AutoID"].ToString());
                }
                if (row["SimCardNO"] != null && row["SimCardNO"].ToString() != "")
                {
                    model.SimCardNO = row["SimCardNO"].ToString();
                }
                if (row["PrizeID"] != null && row["PrizeID"].ToString() != "")
                {
                    model.PrizeID= long.Parse(row["PrizeID"].ToString());
                }
                if (row["Amount"] != null)
                {
                    model.Amount=int.Parse( row["Amount"].ToString());
                }
                if (row["IsTaken"] != null && row["IsTaken"].ToString() != "")
                {
                    if ((row["IsTaken"].ToString() == "1") || (row["IsTaken"].ToString().ToLower() == "true"))
                    {
                        model.IsTaken = true;
                    }
                    else
                    {
                        model.IsTaken = false;
                    }
                }
                if (row["TakenTime"] != null)
                {
                    model.TakenTime = DateTime.Parse( row["TakenTime"].ToString());
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

