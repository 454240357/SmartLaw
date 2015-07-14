using System;
using System.Data;
using System.Collections.Generic;
using SmartLaw.Common;
using SmartLaw.Model;

namespace SmartLaw.BLL
{
    public partial class Notice
    {
        private readonly SmartLaw.DAL.MySqlNotice dal = new SmartLaw.DAL.MySqlNotice();
        public Notice()
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
        /// 增加公告
        /// </summary>
        public long Add(SmartLaw.Model.Notice model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新公告
        /// </summary>
        public bool Update(SmartLaw.Model.Notice model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除公告
        /// </summary>
        public bool Delete(long AutoID)
        {

            return dal.Delete(AutoID);
        }
        /// <summary>
        /// 批量删除公告,返回成功删除的条目数
        /// </summary>
        public bool DeleteList(string[] AutoIDlist)
        {
            return dal.DeleteList(AutoIDlist);
        }

        /// <summary>
        /// 得到公告实体
        /// </summary>
        public SmartLaw.Model.Notice GetModel(long AutoID)
        {

            return dal.GetModel(AutoID);
        }

        /// <summary>
        /// 得到公告实体，从缓存中
        /// </summary>
        public SmartLaw.Model.Notice GetModelByCache(long AutoID)
        {

            string CacheKey = "NoticeModel-" + AutoID;
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
            return (SmartLaw.Model.Notice)objModel;
        }

        /// <summary>
        /// 根据条件获取公告列表
        /// </summary> 
        public DataSet GetList(int key, string value)
        {
            return dal.GetList(key, value);
        }

        /// <summary>
        /// 根据条件获取公告列表
        /// </summary> 
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            return dal.GetList(key, value, Top, filedOrder, desc);
        }

        /// <summary>
        /// 根据条件获取公告列表(list)
        /// </summary> 
        public List<SmartLaw.Model.Notice> GetModelList(int key, string value, int Top, int filedOrder, bool desc)
        {
            DataSet ds = dal.GetList(key, value, Top, filedOrder, desc);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 将DateTable转成实体类集合
        /// </summary>
        public List<SmartLaw.Model.Notice> DataTableToList(DataTable dt)
        {
            List<SmartLaw.Model.Notice> modelList = new List<SmartLaw.Model.Notice>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SmartLaw.Model.Notice model;
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
        /// 获得所有公告集合
        /// </summary>
        /// <returns>公告集合</returns>
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
        /// 分页获取公告集合
        /// </summary> 
        public DataSet GetListByPage(int key, string value, int filedOrder, bool desc, int startIndex, int endIndex)
        {
            return dal.GetListByPage(key, value, filedOrder, desc, startIndex, endIndex);
        }
         
        #endregion  BasicMethod

        #region  ExtensionMethod

        /// <summary>
        /// 查询某区域所能看到的公告列表
        /// </summary>
        /// <param name="regionalId"></param>区域ID
        /// <returns>该区域所能看到所有公告</returns>
        public List<Model.Notice> getNoticeList(string regionalId)
        {
            List<string> regionids = new List<string>();
            BLL.Regional bll = new BLL.Regional();
            string cRegionId = regionalId;
            while (cRegionId != "Root")
            {
                regionids.Add(cRegionId);
                List<Model.Regional> regionallist = bll.GetModelList(0, cRegionId, -1, -1, false);
                if (regionallist.Count < 1)
                    break;
                cRegionId = regionallist[0].SubRegionalID;
            }
            return DataTableToList(dal.getNoticeList(regionids.ToArray()).Tables[0]);
        }
        #endregion  ExtensionMethod
    }
}
