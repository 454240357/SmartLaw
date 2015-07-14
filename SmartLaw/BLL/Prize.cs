using System;
using System.Data;
using System.Collections.Generic;
using SmartLaw.Common;
using SmartLaw.Model;
namespace SmartLaw.BLL
{
    /// <summary>
    /// Prize
    /// </summary>
    public partial class Prize
    {
        private readonly SmartLaw.DAL.MySqlPrize dal = new SmartLaw.DAL.MySqlPrize();
        public Prize()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long AutoID)
        {
            return dal.Exists(AutoID);
        }

        /// <summary>
        /// 增加奖品
        /// </summary>
        public long Add(SmartLaw.Model.Prize model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新奖品
        /// </summary>
        public bool Update(SmartLaw.Model.Prize model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除奖品
        /// </summary>
        public bool Delete(long AutoID)
        {

            return dal.Delete(AutoID);
        }
        /// <summary>
        /// 批量删除奖品,返回成功删除的条目数
        /// </summary>
        public bool DeleteList(string[] AutoIDlist)
        {
            return dal.DeleteList(AutoIDlist);
        }

        /// <summary>
        /// 得到奖品实体
        /// </summary>
        public SmartLaw.Model.Prize GetModel(long AutoID)
        {

            return dal.GetModel(AutoID);
        }




        /// <summary>
        /// 根据条件获取奖品列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：PrizeName,2:PrizeUnit,3:Points,4:Registrant,5:Stock,6:Remarks,7:RegTime,8:Picture 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>奖品集合</returns>
        public DataSet GetList(int key, string value)
        {
            return dal.GetList(key, value);
        }

        /// <summary>
        /// 根据条件获取奖品列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：PrizeName,2:PrizeUnit,3:Points,4:Registrant,5:Stock,6:Remarks,7:RegTime,8:Picture 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1：PrizeName,2:PrizeUnit,3:Points,4:Registrant,5:Stock,6:Remarks,7:RegTime,8:Picture 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>奖品集合</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            return dal.GetList(key, value, Top, filedOrder, desc);
        }

        /// <summary>
        /// 根据条件获取奖品列表(list)
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：PrizeName,2:PrizeUnit,3:Points,4:Registrant,5:Stock,6:Remarks,7:RegTime,8:Picture 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1：PrizeName,2:PrizeUnit,3:Points,4:Registrant,5:Stock,6:Remarks,7:RegTime,8:Picture其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>奖品集合</returns>
        public List<SmartLaw.Model.Prize> GetModelList(int key, string value, int Top, int filedOrder, bool desc)
        {
            DataSet ds = dal.GetList(key, value, Top, filedOrder, desc);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 将DateTable转成实体类集合
        /// </summary>
        public List<SmartLaw.Model.Prize> DataTableToList(DataTable dt)
        {
            List<SmartLaw.Model.Prize> modelList = new List<SmartLaw.Model.Prize>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SmartLaw.Model.Prize model;
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
        /// 获得所有奖品集合
        /// </summary>
        /// <returns>奖品集合</returns>
        public DataSet GetAllList()
        {
            return GetList(-1, "");
        }

        /// <summary>
        /// 获得所有奖品集合
        /// </summary>
        /// <returns>奖品集合</returns>
        public List<SmartLaw.Model.Prize> GetAllModelList()
        {
            DataSet ds = GetList(-1, "");
            return DataTableToList(ds.Tables[0]);
        }






        #endregion  BasicMethod
    }

}