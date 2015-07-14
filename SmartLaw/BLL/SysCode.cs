using System;
using System.Data;
using System.Collections.Generic;
using SmartLaw.Common;
using SmartLaw.Model;
using System.Data.SqlClient;
namespace SmartLaw.BLL
{
	/// <summary>
	/// SYSCODE
	/// </summary>
	public partial class SysCode
	{
        private readonly SmartLaw.DAL.MySqlSysCode dal = new SmartLaw.DAL.MySqlSysCode();
		public SysCode()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该大类
		/// </summary>
		public bool Exists(string SYSCodeID)
		{
			return dal.Exists(SYSCodeID);
		}

		/// <summary>
        /// 增加大类
		/// </summary>
		public bool Add(SmartLaw.Model.SysCode model)
		{
			return dal.Add(model);
		}

		/// <summary>
        /// 更新大类
		/// </summary>
		public bool Update(SmartLaw.Model.SysCode model)
		{
			return dal.Update(model);
		}

		/// <summary>
        /// 删除大类
		/// </summary>
		public bool Delete(string SYSCodeID)
		{
			return dal.Delete(SYSCodeID);
		}

		/// <summary>
        /// 批量删除大类,返回成功删除的条目数
		/// </summary>
		public int DeleteList(string[] SYSCodeIDlist )
		{
			return dal.DeleteList(SYSCodeIDlist);
		}

		/// <summary>
        /// 得到大类实体
		/// </summary>
		public SmartLaw.Model.SysCode GetModel(string SYSCodeID)
		{
			return dal.GetModel(SYSCodeID);
		}

		/// <summary>
        /// 得到大类实体，从缓存中
		/// </summary>
		public SmartLaw.Model.SysCode GetModelByCache(string SYSCodeID)
		{
			string CacheKey = "SYSCODEModel-" + SYSCodeID;
            object objModel = SmartLaw.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(SYSCodeID);
					if (objModel != null)
					{
                        int ModelCache = SmartLaw.Common.ConfigHelper.GetConfigInt("ModelCache");
                        SmartLaw.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (SmartLaw.Model.SysCode)objModel;
		}

        /// <summary>
        /// 根据条件获取大类列表
        /// </summary>
        /// <param name="key">查询条件 0：SYSCodeID 1：SYSCodeContext 2：LastModifytime 3：ISValid 4：Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>大类集合</returns>
        public DataSet GetList(int key, string value)
        {
            return dal.GetList(key, value);
        }

        /// <summary>
        /// 根据条件获取大类列表
        /// </summary>
        /// <param name="key">查询条件 0：SYSCodeID 1：SYSCodeContext 2：LastModifytime 3：ISValid 4：Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：SYSCodeID 1：SYSCodeContext 2：LastModifytime 3：ISValid 4：Memo 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>大类集合</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            return dal.GetList(key, value, Top, filedOrder, desc);
        }

        /// <summary>
        /// 根据条件获取大类列表(list)
        /// </summary>
        /// <param name="key">查询条件 0：SYSCodeID 1：SYSCodeContext 2：LastModifytime 3：ISValid 4：Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：SYSCodeID 1：SYSCodeContext 2：LastModifytime 3：ISValid 4：Memo 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>大类集合</returns>
        public List<SmartLaw.Model.SysCode> GetModelList(int key, string value, int Top, int filedOrder, bool desc)
        {
            DataSet ds = dal.GetList(key, value, Top, filedOrder, desc);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 将DateTable转成实体类集合
        /// </summary>
        public List<SmartLaw.Model.SysCode> DataTableToList(DataTable dt)
        {
            List<SmartLaw.Model.SysCode> modelList = new List<SmartLaw.Model.SysCode>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SmartLaw.Model.SysCode model;
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
        /// 获得所有大类集合
        /// </summary>
        /// <returns>大类集合</returns>
        public DataSet GetAllList()
        {
            return GetList(-1, "");
        }

        /// <summary>
        /// 根据条件获得大类条目数
        /// </summary>
        /// <param name="key">查询条件 0：SYSCodeID 1：SYSCodeContext 2：LastModifytime 3：ISValid 4：Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>大类集合条目数</returns>
        public int GetRecordCount(int key, string value)
        {
            return dal.GetRecordCount(key, value);
        }

        /// <summary>
        /// 分页获取大类集合
        /// </summary>
        /// <param name="key">查询条件 0：SYSCodeID 1：SYSCodeContext 2：LastModifytime 3：ISValid 4：Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="filedOrder">排序字段 0：SYSCodeID 1：SYSCodeContext 2：LastModifytime 3：ISValid 4：Memo 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="endIndex">结束索引</param>
        /// <returns>大类集合</returns>
        public DataSet GetListByPage(int key, string value, int filedOrder, bool desc, int startIndex, int endIndex)
        {
            return dal.GetListByPage(key, value, filedOrder, desc, startIndex, endIndex);
        }
        #endregion  BasicMethod

        #region  ExtensionMethod
		#endregion  ExtensionMethod
	}
}

