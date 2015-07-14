using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SmartLaw.BLL
{
    public partial class Drugs
    {
        private readonly SmartLaw.DAL.MySqlDrugs dal = new DAL.MySqlDrugs();
        public Drugs()
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
        /// 增加
        /// </summary>
        public long Add(SmartLaw.Model.Drugs model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public bool Update(SmartLaw.Model.Drugs model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public bool Delete(long AutoID)
        {

            return dal.Delete(AutoID);
        }
        /// <summary>
        /// 批量删除,返回成功删除的条目数
        /// </summary>
        public bool DeleteList(string[] AutoIDlist)
        {
            return dal.DeleteList(AutoIDlist);
        }

        /// <summary>
        /// 得到实体
        /// </summary>
        public SmartLaw.Model.Drugs GetModel(long AutoID)
        {

            return dal.GetModel(AutoID);
        }

        /// <summary>
        /// 得到实体，从缓存中
        /// </summary>
        public SmartLaw.Model.Drugs GetModelByCache(long AutoID)
        {

            string CacheKey = "DrugsModel-" + AutoID;
            object objModel = SmartLaw.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(AutoID);
                    if (objModel != null)
                    {
                        int ModelCache = SmartLaw.Common.ConfigHelper.GetConfigInt("ModelCache");
                        SmartLaw.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (SmartLaw.Model.Drugs)objModel;
        }

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件  0：AutoID 1：PZWH 2：CPMC 3: YWMC 4：SPM 5：JX 6：GG 7：SCDW 8：SCDZ 9：CPLB 10:YPZWH 11：PZRQ 12：BWM 13: BWMBZ 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value)
        {
            return dal.GetList(key, value);
        }

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件  0：AutoID 1：PZWH 2：CPMC 3: YWMC 4：SPM 5：JX 6：GG 7：SCDW 8：SCDZ 9：CPLB 10:YPZWH 11：PZRQ 12：BWM 13: BWMBZ 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1：PZWH 2：CPMC 3: YWMC 4：SPM 5：JX 6：GG 7：SCDW 8：SCDZ 9：CPLB 10:YPZWH 11：PZRQ 12：BWM 13: BWMBZ 其他:全部</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            return dal.GetList(key, value, Top, filedOrder, desc);
        }

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件  0：AutoID 1：PZWH 2：CPMC 3: YWMC 4：SPM 5：JX 6：GG 7：SCDW 8：SCDZ 9：CPLB 10:YPZWH 11：PZRQ 12：BWM 13: BWMBZ 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段  0：AutoID 1：PZWH 2：CPMC 3: YWMC 4：SPM 5：JX 6：GG 7：SCDW 8：SCDZ 9：CPLB 10:YPZWH 11：PZRQ 12：BWM 13: BWMBZ 其他:全部</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public List<SmartLaw.Model.Drugs> GetModelList(int key, string value, int Top, int filedOrder, bool desc)
        {
            DataSet ds = dal.GetList(key, value, Top, filedOrder, desc);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 将DateTable转成实体类集合
        /// </summary>
        public List<SmartLaw.Model.Drugs> DataTableToList(DataTable dt)
        {
            List<SmartLaw.Model.Drugs> modelList = new List<SmartLaw.Model.Drugs>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SmartLaw.Model.Drugs model;
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
        /// 获得所有集合
        /// </summary>
        /// <returns>集合</returns>
        public DataSet GetAllList()
        {
            return GetList(-1, "");
        }

        /// <summary>
        /// 根据条件获得公告条目数
        /// </summary> 
        public int GetRecordCount(int key, string value)
        {
            return dal.GetRecordCount(key, value);
        }

        /// <summary>
        /// 分页获取集合
        /// </summary> 
        public DataSet GetListByPage(int key, string value, int filedOrder, bool desc, int startIndex, int endIndex)
        {
            return dal.GetListByPage(key, value, filedOrder, desc, startIndex, endIndex);
        }

        #endregion  BasicMethod

        #region  ExtensionMethod
         
        #endregion  ExtensionMethod
    }
}
