using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SmartLaw.BLL
{
    public partial class Message
    {
        private readonly SmartLaw.DAL.MySqlMessage dal = new SmartLaw.DAL.MySqlMessage();
        public Message()
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
        public long Add(SmartLaw.Model.Message model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新公告
        /// </summary>
        public bool Update(SmartLaw.Model.Message model)
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
        public SmartLaw.Model.Message GetModel(long AutoID)
        {

            return dal.GetModel(AutoID);
        }

        /// <summary>
        /// 得到公告实体，从缓存中
        /// </summary>
        public SmartLaw.Model.Message GetModelByCache(long AutoID)
        {

            string CacheKey = "MessageModel-" + AutoID;
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
            return (SmartLaw.Model.Message)objModel;
        }

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：Title 2：Contents 3: Orders 4：MessageType 5：DisappearType 6：AvailableTime 7：ExpiredTime 8：LastModifyTime 9：Publisher 10:IsValid 11：AndOr 12：Memo 13: DataType其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value)
        {
            return dal.GetList(key, value);
        }

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：Title 2：Contents 3: Orders 4：MessageType 5：DisappearType 6：AvailableTime 7：ExpiredTime 8：LastModifyTime 9：Publisher 10:IsValid 11：AndOr 12：Memo 13: DataType其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1：Title 2：Contents 3: Orders 4：MessageType 5：DisappearType 6：AvailableTime 7：ExpiredTime 8：LastModifyTime 9：Publisher 10:IsValid 11：AndOr 12：Memo 13: DataType其他:全部</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            return dal.GetList(key, value, Top, filedOrder, desc);
        }

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：Title 2：Contents 3: Orders 4：MessageType 5：DisappearType 6：AvailableTime 7：ExpiredTime 8：LastModifyTime 9：Publisher 10:IsValid 11：AndOr 12：Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1：Title 2：Contents 3: Orders 4：MessageType 5：DisappearType 6：AvailableTime 7：ExpiredTime 8：LastModifyTime 9：Publisher 10:IsValid 11：AndOr 12：Memo 其他:全部</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public List<SmartLaw.Model.Message> GetModelList(int key, string value, int Top, int filedOrder, bool desc)
        {
            DataSet ds = dal.GetList(key, value, Top, filedOrder, desc);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 将DateTable转成实体类集合
        /// </summary>
        public List<SmartLaw.Model.Message> DataTableToList(DataTable dt)
        {
            List<SmartLaw.Model.Message> modelList = new List<SmartLaw.Model.Message>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SmartLaw.Model.Message model;
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
        /// 根据复合条件获取公告
        /// </summary>
        /// <param name="key">查询条件 0：AvailableTime下限 1：AvailableTime上限 2：ExpiredTime上限 3：ExpiredTime下限 4：Title 5:Content： 6：IsValid 7：Obj-Identity 8:Obj-Region 9:Obj-Sim 其他:全部</param>
        /// <param name="value">查询值</param> 
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1：Title 2：Contents 3: Orders 4：MessageType 5：DisappearType 6：AvailableTime 7：ExpiredTime 8：LastModifyTime 9：Publisher 10:IsValid 11：AndOr 12：Memo 其他:全部</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public List<SmartLaw.Model.Message> GetMessageEx(int[] keys, object[] values, int Top, int fieldOrder, bool desc)
        {
            DataSet ds = dal.GetMessageEx(keys, values, Top, fieldOrder,desc);
            return DataTableToList(ds.Tables[0]);
        }
        #endregion  ExtensionMethod

    }
}
