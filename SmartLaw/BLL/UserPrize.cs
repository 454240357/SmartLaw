using System;
using System.Data;
using System.Collections.Generic;
using SmartLaw.Common;
using SmartLaw.Model;
namespace SmartLaw.BLL
{
    /// <summary>
    /// UserPrize
    /// </summary>
    public partial class UserPrize
    {
        private readonly SmartLaw.DAL.MySqlUserPrize dal = new SmartLaw.DAL.MySqlUserPrize();
        public UserPrize()
        { }
        #region  BasicMethod

        /// <summary>
        /// 兑换奖品
        /// </summary>
        public void Add(SmartLaw.Model.UserPrize model,out string msg)
        {
            dal.Add(model,out msg);
        }

        /// <summary>
        /// 取消兑换
        /// </summary>
        /// <param name="model"></param>
        /// <param name="msg"></param>
        public void Delete(SmartLaw.Model.UserPrize model, out string msg)
        {
            dal.Delete(model, out msg);
        }





        /// <summary>
        /// 将userprize设置为已领取
        /// </summary>
        /// <param name="model">userprize</param>
        /// <param name="msg">返回信息</param>
        /// <returns></returns>
        public bool PrizeTaken(SmartLaw.Model.UserPrize model, out string msg)
        {
            return dal.PrizeTaken(model, out msg);
        }





        /// <summary>
        /// 根据条件获取UserPrize列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：PrizeID 2：Amount 3:IsTaken 4：TakenTime 5:Remarks 6:SimCardNO 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>UserPrize集合</returns>
        public DataSet GetList(int key, string value)
        {
            return dal.GetList(key, value);
        }

        /// <summary>
        /// 得到兑换记录
        /// </summary>
        public SmartLaw.Model.UserPrize GetModel(long AutoID)
        {

            return dal.GetModel(AutoID);
        }

        /// <summary>
        /// 根据条件获取UserPrize列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：PrizeID 2：Amount 3:IsTaken 4：TakenTime 5:Remarks 6:SimCardNO 其他:全部/param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段  0：AutoID 1：PrizeID 2：Amount 3:IsTaken 4：TakenTime 5:Remarks 6:SimCardNO 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>UserPrize集合</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            return dal.GetList(key, value, Top, filedOrder, desc);
        }



        /// <summary>
        /// 根据条件获取UserPrize列表(list)
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：PrizeID 2：Amount 3:IsTaken 4：TakenTime 5:Remarks 6:SimCardNO 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段  0：AutoID 1：PrizeID 2：Amount 3:IsTaken 4：TakenTime 5:Remarks 6:SimCardNO 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>UserPrize集合</returns>
        public List<SmartLaw.Model.UserPrize> GetModelList(int key, string value, int Top, int filedOrder, bool desc)
        {
            DataSet ds = dal.GetList(key, value, Top, filedOrder, desc);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 将DateTable转成实体类集合
        /// </summary>
        public List<SmartLaw.Model.UserPrize> DataTableToList(DataTable dt)
        {
            List<SmartLaw.Model.UserPrize> modelList = new List<SmartLaw.Model.UserPrize>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SmartLaw.Model.UserPrize model;
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
        /// 获得所有UserPrize集合
        /// </summary>
        /// <returns>UserPrize集合</returns>
        public DataSet GetAllList()
        {
            return GetList(-1, "");
        }


        #endregion BasicMethod

    }
}