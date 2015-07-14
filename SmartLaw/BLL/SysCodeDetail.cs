using System;
using System.Data;
using System.Collections.Generic;
using SmartLaw.Common;
using SmartLaw.Model;
namespace SmartLaw.BLL
{
	/// <summary>
	/// SYSCODEDETIAL
	/// </summary>
	public partial class SysCodeDetail
	{
        private readonly SmartLaw.DAL.MySqlSysCodeDetail dal = new SmartLaw.DAL.MySqlSysCodeDetail();
		public SysCodeDetail()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该小类
		/// </summary>
		public bool Exists(string SYSCodeDetialID)
		{
			return dal.Exists(SYSCodeDetialID);
		}

		/// <summary>
		/// 增加小类
		/// </summary>
		public bool Add(SmartLaw.Model.SysCodeDetail model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新小类
		/// </summary>
		public bool Update(SmartLaw.Model.SysCodeDetail model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除小类
		/// </summary>
		public bool Delete(string SYSCodeDetialID)
		{
			
			return dal.Delete(SYSCodeDetialID);
		}
		/// <summary>
        /// 批量删除小类
		/// </summary>
		public int DeleteList(string[] SYSCodeDetialIDlist )
		{
			return dal.DeleteList(SYSCodeDetialIDlist );
		}

		/// <summary>
		/// 得到小类实体
		/// </summary>
		public SmartLaw.Model.SysCodeDetail GetModel(string SYSCodeDetialID)
		{
			
			return dal.GetModel(SYSCodeDetialID);
		}

		/// <summary>
        /// 得到小类实体，从缓存中
		/// </summary>
		public SmartLaw.Model.SysCodeDetail GetModelByCache(string SYSCodeDetialID)
		{
			
			string CacheKey = "SYSCODEDETIALModel-" + SYSCodeDetialID;
            object objModel = SmartLaw.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(SYSCodeDetialID);
					if (objModel != null)
					{
                        int ModelCache = SmartLaw.Common.ConfigHelper.GetConfigInt("ModelCache");
                        SmartLaw.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (SmartLaw.Model.SysCodeDetail)objModel;
		}

        /// <summary>
        /// 根据条件获取小类列表
        /// </summary>
        /// <param name="key">查询条件 0：SYSCodeID 1:SYSCodeDetialID 2：SYSCodeDetialContext 3：LastModifytime 4：ISValid 5：Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>小类集合</returns>
        public DataSet GetList(int key, string value)
        {
            return dal.GetList(key, value);
        }

        /// <summary>
        /// 根据条件获取小类列表
        /// </summary>
        /// <param name="key">查询条件 0：SYSCodeID 1:SYSCodeDetialID 2：SYSCodeDetialContext 3：LastModifytime 4：ISValid 5：Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：SYSCodeID 1:SYSCodeDetialID 2：SYSCodeDetialContext 3：LastModifytime 4：ISValid 5：Memo 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>小类集合</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            return dal.GetList(key, value, Top, filedOrder, desc);
        }

        /// <summary>
        /// 根据条件获取小类列表(list)
        /// </summary>
        /// <param name="key">查询条件 0：SYSCodeID 1:SYSCodeDetialID 2：SYSCodeDetialContext 3：LastModifytime 4：ISValid 5：Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：SYSCodeID 1:SYSCodeDetialID 2：SYSCodeDetialContext 3：LastModifytime 4：ISValid 5：Memo 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>小类集合</returns>
        public List<SmartLaw.Model.SysCodeDetail> GetModelList(int key, string value, int Top, int filedOrder, bool desc)
        {
            DataSet ds = dal.GetList(key, value, Top, filedOrder, desc);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 将DateTable转成实体类集合
        /// </summary>
        public List<SmartLaw.Model.SysCodeDetail> DataTableToList(DataTable dt)
        {
            List<SmartLaw.Model.SysCodeDetail> modelList = new List<SmartLaw.Model.SysCodeDetail>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SmartLaw.Model.SysCodeDetail model;
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
        /// 获得所有小类集合
        /// </summary>
        /// <returns>小类集合</returns>
        public DataSet GetAllList()
        {
            return GetList(-1, "");
        }

        /// <summary>
        /// 根据条件获得小类条目数
        /// </summary>
        /// <param name="key">查询条件 0：SYSCodeID 1:SYSCodeDetialID 2：SYSCodeDetialContext 3：LastModifytime 4：ISValid 5：Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>小类集合条目数</returns>
        public int GetRecordCount(int key, string value)
        {
            return dal.GetRecordCount(key, value);
        }

        /// <summary>
        /// 分页获取小类集合
        /// </summary>
        /// <param name="key">查询条件 0：SYSCodeID 1:SYSCodeDetialID 2：SYSCodeDetialContext 3：LastModifytime 4：ISValid 5：Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="filedOrder">排序字段 0：SYSCodeID 1:SYSCodeDetialID 2：SYSCodeDetialContext 3：LastModifytime 4：ISValid 5：Memo 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="endIndex">结束索引</param>
        /// <returns>小类集合</returns>
        public DataSet GetListByPage(int key, string value, int filedOrder, bool desc, int startIndex, int endIndex)
        {
            return dal.GetListByPage(key, value, filedOrder, desc, startIndex, endIndex);
        }

		#endregion  BasicMethod
		#region  ExtensionMethod
        /// <summary>
        /// 根据条件获取与特定大类关联的代码关联列表
        /// </summary>
        /// <param name="SysCodeDetialID">小类ID</param> 
        /// <param name="SysCodeID">大类ID</param> 
        /// <param name="SysCodeID">关联大类</param> 
        /// <returns>代码关联的子类集合</returns>
        public DataSet GetListBySysCode(string SysCodeDetialID, string SysCodeID)
        {
            SysCodeRelation scr = new SysCodeRelation();
            DataSet ds = GetList(0, SysCodeID);
            List<SmartLaw.Model.SysCodeRelation> syscodeRelationList = scr.GetModelList(1, SysCodeDetialID, -1, -1, false);
            syscodeRelationList.AddRange(scr.GetModelList(2, SysCodeDetialID, -1, -1, false));
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                SmartLaw.Model.SysCodeRelation scrModel = syscodeRelationList.Find(sr => (sr.SysCodeDetialIDEx.Equals(dr[1].ToString()) ||
                                                                                            sr.SysCodeDetialID.Equals(dr[1].ToString())));
                if (scrModel != null && dr[1].ToString() != SysCodeDetialID)
                {

                }
                else
                {
                    dr.Delete();
                }
            }
            ds.AcceptChanges();
            return ds;
        }
		#endregion  ExtensionMethod
	}
}

