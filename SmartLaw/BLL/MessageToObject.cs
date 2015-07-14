using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SmartLaw.BLL
{
    public partial class MessageToObject
    {
        private readonly SmartLaw.DAL.MySqlMessageToObject dal = new SmartLaw.DAL.MySqlMessageToObject();
        public MessageToObject()
		{}
        #region  BasicMethod
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long AutoID)
        {
            return dal.Exists(AutoID);
        }

        /// <summary>
        /// 增加公告对象关联
        /// </summary>
        public bool Add(SmartLaw.Model.MessageToObject model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新公告对象关联
        /// </summary>
        public bool Update(SmartLaw.Model.MessageToObject model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除公告对象关联
        /// </summary>
        public bool Delete(long AutoID)
        {

            return dal.Delete(AutoID);
        }
        /// <summary>
        /// 批量删除公告对象关联
        /// </summary>
        public int DeleteList(string[] AutoIDlist)
        {
            return dal.DeleteList(AutoIDlist);
        }

        /// <summary>
        /// 得到公告对象关联实体
        /// </summary>
        public SmartLaw.Model.MessageToObject GetModel(long AutoID)
        {

            return dal.GetModel(AutoID);
        }

        /// <summary>
        /// 得到公告对象关联实体，从缓存中
        /// </summary>
        public SmartLaw.Model.MessageToObject GetModelByCache(long AutoID)
        {

            string CacheKey = "MessageToObjectModel-" + AutoID;
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
            return (SmartLaw.Model.MessageToObject)objModel;
        }

        /// <summary>
        /// 根据条件获取公告对象关联列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1:MsgID 2：ObjType 3：ObjValue 其他：全部</param>
        /// <param name="value">查询值</param>
        /// <returns>公告对象关联集合</returns>
        public DataSet GetList(int key, string value)
        {
            return dal.GetList(key, value);
        }

        /// <summary>
        /// 根据条件获取公告对象关联列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1:MsgID 2：ObjType 3：ObjValue  其他：全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1:MsgID 2：ObjType 3：ObjValue  其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>公告对象关联集合</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            return dal.GetList(key, value, Top, filedOrder, desc);
        }

        /// <summary>
        /// 根据条件获取公告对象关联列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1:MsgID 2：ObjType 3：ObjValue 其他：全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段0：AutoID 1:MsgID 2：ObjType 3：ObjValue 其他：全部</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>公告对象关联集合</returns>
        public List<SmartLaw.Model.MessageToObject> GetModelList(int key, string value, int Top, int filedOrder, bool desc)
        {
            DataSet ds = dal.GetList(key, value, Top, filedOrder, desc);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 将DateTable转成实体类集合
        /// </summary>
        public List<SmartLaw.Model.MessageToObject> DataTableToList(DataTable dt)
        {
            List<SmartLaw.Model.MessageToObject> modelList = new List<SmartLaw.Model.MessageToObject>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SmartLaw.Model.MessageToObject model;
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
        /// 获得所有公告对象关联集合
        /// </summary>
        /// <returns>公告对象关联集合</returns>
        public DataSet GetAllList()
        {
            return GetList(-1, "");
        }

        /// <summary>
        /// 根据条件获得公告对象关联条目数
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1:MsgID 2：ObjType 3：ObjValue  其他：全部</param>
        /// <param name="value">查询值</param>
        /// <returns>公告对象关联集合条目数</returns>
        public int GetRecordCount(int key, string value)
        {
            return dal.GetRecordCount(key, value);
        }

        /// <summary>
        /// 分页获取公告对象关联集合
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1:MsgID 2：ObjType 3：ObjValue  其他：全部</param>
        /// <param name="value">查询值</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1:MsgID 2：ObjType 3：ObjValue  其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="endIndex">结束索引</param>
        /// <returns>公告对象关联集合</returns>
        public DataSet GetListByPage(int key, string value, int filedOrder, bool desc, int startIndex, int endIndex)
        {
            return dal.GetListByPage(key, value, filedOrder, desc, startIndex, endIndex);
        }



        #endregion  BasicMethod
    }
}
