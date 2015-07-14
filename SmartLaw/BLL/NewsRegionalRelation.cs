using System;
using System.Data;
using System.Collections.Generic;
using SmartLaw.Common;
using SmartLaw.Model;
namespace SmartLaw.BLL
{
	/// <summary>
	/// NewsRegionalRelation
	/// </summary>
	public partial class NewsRegionalRelation
	{
        private readonly SmartLaw.DAL.MySqlNewsRegionalRelation dal = new SmartLaw.DAL.MySqlNewsRegionalRelation();
		public NewsRegionalRelation()
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
		/// 增加新闻区域关联
		/// </summary>
		public bool Add(SmartLaw.Model.NewsRegionalRelation model)
		{
			return dal.Add(model);
		}

		/// <summary>
        /// 更新新闻区域关联
		/// </summary>
		public bool Update(SmartLaw.Model.NewsRegionalRelation model)
		{
			return dal.Update(model);
		}

		/// <summary>
        /// 删除新闻区域关联
		/// </summary>
		public bool Delete(long AutoID)
		{
			
			return dal.Delete(AutoID);
		}
		/// <summary>
        /// 批量删除新闻区域关联
		/// </summary>
        public int DeleteList(string[] AutoIDlist)
		{
			return dal.DeleteList(AutoIDlist );
		}

		/// <summary>
        /// 得到新闻区域关联实体
		/// </summary>
		public SmartLaw.Model.NewsRegionalRelation GetModel(long AutoID)
		{
			
			return dal.GetModel(AutoID);
		}

		/// <summary>
        /// 得到新闻区域关联实体，从缓存中
		/// </summary>
		public SmartLaw.Model.NewsRegionalRelation GetModelByCache(long AutoID)
		{
			
			string CacheKey = "NewsRegionalRelationModel-" + AutoID;
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
				catch{}
			}
			return (SmartLaw.Model.NewsRegionalRelation)objModel;
		}

        /// <summary>
        /// 根据条件获取新闻区域关联列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1:NewsID 2：RegionalID 其他：全部</param>
        /// <param name="value">查询值</param>
        /// <returns>新闻区域关联集合</returns>
        public DataSet GetList(int key, string value)
        {
            return dal.GetList(key, value);
        }

        /// <summary>
        /// 根据条件获取新闻区域关联列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1:NewsID 2：RegionalID 其他：全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1:NewsID 2：RegionalID 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>新闻区域关联集合</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            return dal.GetList(key, value, Top, filedOrder, desc);
        }

        /// <summary>
        /// 根据条件获取新闻区域关联列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1:NewsID 2：RegionalID 其他：全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1:NewsID 2：RegionalID 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>新闻区域关联集合</returns>
        public List<SmartLaw.Model.NewsRegionalRelation> GetModelList(int key, string value, int Top, int filedOrder, bool desc)
        {
            DataSet ds = dal.GetList(key, value, Top, filedOrder, desc);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 将DateTable转成实体类集合
        /// </summary>
        public List<SmartLaw.Model.NewsRegionalRelation> DataTableToList(DataTable dt)
        {
            List<SmartLaw.Model.NewsRegionalRelation> modelList = new List<SmartLaw.Model.NewsRegionalRelation>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SmartLaw.Model.NewsRegionalRelation model;
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
        /// 获得所有新闻区域关联集合
        /// </summary>
        /// <returns>新闻区域关联集合</returns>
        public DataSet GetAllList()
        {
            return GetList(-1, "");
        }

        /// <summary>
        /// 根据条件获得新闻区域关联条目数
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1:NewsID 2：RegionalID 其他：全部</param>
        /// <param name="value">查询值</param>
        /// <returns>新闻区域关联集合条目数</returns>
        public int GetRecordCount(int key, string value)
        {
            return dal.GetRecordCount(key, value);
        }

        /// <summary>
        /// 分页获取新闻区域关联集合
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1:NewsID 2：RegionalID 其他：全部</param>
        /// <param name="value">查询值</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1:NewsID 2：RegionalID 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="endIndex">结束索引</param>
        /// <returns>新闻区域关联集合</returns>
        public DataSet GetListByPage(int key, string value, int filedOrder, bool desc, int startIndex, int endIndex)
        {
            return dal.GetListByPage(key, value, filedOrder, desc, startIndex, endIndex);
        }




        /// <summary>
        /// 根据条件查询每个村所对应菜单看到的新闻
        /// </summary>
        /// <param name="regionalId"></param>区域编码
        /// <param name="categoryId"></param>菜单编码
        /// <returns></returns>
        public DataSet getNewList(string regionalId, int categoryId,bool desc)
        {
            return dal.getNewsList(regionalId, categoryId, desc);
        }











		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

