using System;
using System.Data;
using System.Collections.Generic;
using SmartLaw.Common;
using SmartLaw.Model;
namespace SmartLaw.BLL
{
	/// <summary>
	/// News
	/// </summary>
	public partial class News
	{
        private readonly SmartLaw.DAL.MySqlNews dal = new SmartLaw.DAL.MySqlNews();
		public News()
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
		/// 增加新闻
		/// </summary>
		public long Add(SmartLaw.Model.News model)
		{
			return dal.Add(model);
		}

		/// <summary>
        /// 更新新闻
		/// </summary>
		public bool Update(SmartLaw.Model.News model)
		{
			return dal.Update(model);
		}

		/// <summary>
        /// 删除新闻
		/// </summary>
		public bool Delete(long AutoID)
		{
			
			return dal.Delete(AutoID);
		}
		/// <summary>
        /// 批量删除新闻,返回成功删除的条目数
		/// </summary>
        public bool DeleteList(string[] AutoIDlist)
		{
			return dal.DeleteList(AutoIDlist );
		}

		/// <summary>
        /// 得到新闻实体
		/// </summary>
		public SmartLaw.Model.News GetModel(long AutoID)
		{
			
			return dal.GetModel(AutoID);
		}

		/// <summary>
        /// 得到新闻实体，从缓存中
		/// </summary>
		public SmartLaw.Model.News GetModelByCache(long AutoID)
		{
			
			string CacheKey = "NewsModel-" + AutoID;
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
			return (SmartLaw.Model.News)objModel;
		}

        /// <summary>
        /// 根据条件获取新闻列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：Title 2：CategoryID 3：Contents 4：Publisher 5:DataSource： 6：DataType 7：LastModifyTime 8:IsValid 9:Checked 10:Checker 11:CheckMemo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>新闻集合</returns>
        public DataSet GetList(int key, string value)
        {
            return dal.GetList(key, value);
        }

        /// <summary>
        /// 根据条件获取新闻列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：Title 2：CategoryID 3：Contents 4：Publisher 5:DataSource： 6：DataType 7：LastModifyTime 8:IsValid 9:Checked 10:Checker 11:CheckMemo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1：Title 2：CategoryID 3：Contents 4：Publisher 5:DataSource 6：DataType 7：LastModifyTime 8:IsValid 9:Checked 10:Checker 11:CheckMemo其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>新闻集合</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            return dal.GetList(key, value, Top, filedOrder, desc);
        }

        /// <summary>
        /// 根据条件获取新闻列表(list)
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：Title 2：CategoryID 3：Contents 4：Publisher 5:DataSource： 6：DataType 7：LastModifyTime 8:IsValid 9:Checked 10:Checker 11:CheckMemo其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1：Title 2：CategoryID 3：Contents 4：Publisher 5:DataSource 6：DataType 7：LastModifyTime 8:IsValid 9:Checked 10:Checker 11:CheckMemo其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>新闻集合</returns>
        public List<SmartLaw.Model.News> GetModelList(int key, string value, int Top, int filedOrder, bool desc)
        {
            DataSet ds = dal.GetList(key, value, Top, filedOrder, desc);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 将DateTable转成实体类集合
        /// </summary>
        public List<SmartLaw.Model.News> DataTableToList(DataTable dt)
        {
            List<SmartLaw.Model.News> modelList = new List<SmartLaw.Model.News>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SmartLaw.Model.News model;
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
        /// 获得所有新闻集合
        /// </summary>
        /// <returns>新闻集合</returns>
        public DataSet GetAllList()
        {
            return GetList(-1, "");
        }

        /// <summary>
        /// 根据条件获得新闻条目数
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：Title 2：CategoryID 3：Contents 4：Publisher 5:DataSource： 6：DataType 7：LastModifyTime 8:IsValid 9:Checked 10:Checker 11:CheckMemo其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>新闻集合条目数</returns>
        public int GetRecordCount(int key, string value)
        {
            return dal.GetRecordCount(key, value);
        }

        /// <summary>
        /// 分页获取新闻集合
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：Title 2：CategoryID 3：Contents 4：Publisher 5:DataSource： 6：DataType 7：LastModifyTime 8:IsValid 9:Checked 10:Checker 11:CheckMemo其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1：Title 2：CategoryID 3：Contents 4：Publisher 5:DataSource 6：DataType 7：LastModifyTime 8:IsValid 9:Checked 10:Checker 11:CheckMemo其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="endIndex">结束索引</param>
        /// <returns>新闻集合</returns>
        public DataSet GetListByPage(int key, string value, int filedOrder, bool desc, int startIndex, int endIndex)
        {
            return dal.GetListByPage(key, value, filedOrder, desc, startIndex, endIndex);
        }
		#endregion  BasicMethod
		#region  ExtensionMethod
        /// <summary>
        /// 根据条件获取新闻列表
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：Title 2：CategoryID 3：Contents 4：Publisher 5:DataSource： 6：DataType 7：LastModifyTime 8:IsValid 9:Checked 10:Checker 11:CheckMemo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：AutoID 1：Title 2：CategoryID 3：Contents 4：Publisher 5:DataSource 6：DataType 7：LastModifyTime 8:IsValid 9:Checked 10:Checker 11:CheckMemo其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="endIndex">结束索引</param>
        /// <returns>新闻集合</returns>
        public DataSet GetListEx(int[] keys, string[] values, int Top, int filedOrder, bool desc, int startIndex, int endIndex)
        {
            return dal.GetListEx(keys, values, Top, filedOrder, desc,startIndex,endIndex);
        }

        /// <summary>
        /// 根据条件获取新闻数目
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：Title 2：CategoryID 3：Contents 4：Publisher 5:DataSource： 6：DataType 7：LastModifyTime 8:IsValid 9:Checked 10:Checker 11:CheckMemo 其他:全部</param>
        /// <param name="value">查询值</param> 
        /// <returns>数目</returns>
        public int GetReCordEx(int[] keys, string[] values)
        {
            return dal.GetReCordEx(keys, values);
        }
        /// <summary>
        /// 根据条件获取新闻数目
        /// </summary>
        /// <param name="key">查询条件 2：CategoryID 12：regionalId </param>
        /// <param name="value">查询值</param> 
        /// <returns>数目</returns>
        public int GetReCordForRePort(int[] keys, string[] values,int checkSate, string startDate, string endDate)
        {
            return dal.GetRecordCountForRePort(keys, values,checkSate, startDate, endDate);
        }
		#endregion  ExtensionMethod
	}
}

