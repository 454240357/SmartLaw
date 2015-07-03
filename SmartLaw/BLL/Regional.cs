using System;
using System.Data;
using System.Collections.Generic;
using SmartLaw.Common;
using SmartLaw.Model;
namespace SmartLaw.BLL
{
	/// <summary>
	/// regional
	/// </summary>
	public partial class Regional
	{
		private readonly SmartLaw.DAL.MySqlRegional dal=new SmartLaw.DAL.MySqlRegional();
		public Regional()
		{}
		#region  BasicMethod
		/// <summary>
		/// 是否存在该记录
		/// </summary>
        public bool Exists(string RegionalID)
		{
            return dal.Exists(RegionalID);
		}

		/// <summary>
		/// 增加区域
		/// </summary>
		public bool Add(SmartLaw.Model.Regional model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新区域
		/// </summary>
		public bool Update(SmartLaw.Model.Regional model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除区域
		/// </summary>
        public bool Delete(string RegionalID)
		{

            return dal.Delete(RegionalID);
		}
		/// <summary>
		/// 批量删除区域
		/// </summary>
		public int DeleteList(string[] RegionalIDlist )
		{
            return dal.DeleteList(RegionalIDlist);
		}

		/// <summary>
		/// 得到区域实体
		/// </summary>
        public SmartLaw.Model.Regional GetModel(string RegionalID)
		{

            return dal.GetModel(RegionalID);
		}

		/// <summary>
		/// 得到区域对象实体，从缓存中
		/// </summary>
        public SmartLaw.Model.Regional GetModelByCache(string RegionalID)
		{

            string CacheKey = "regionalModel-" + RegionalID;
			object objModel = SmartLaw.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
                    objModel = dal.GetModel(RegionalID);
					if (objModel != null)
					{
                        int ModelCache = SmartLaw.Common.ConfigHelper.GetConfigInt("ModelCache");
                        SmartLaw.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (SmartLaw.Model.Regional)objModel;
		}

        /// <summary>
        /// 根据条件获取区域列表
        /// </summary>
        /// <param name="key">查询条件 0：RegionalID 1：RegionalName 2：RegionalCode 3：SubRegionalID 4：RegionalLevel 5:LastModifyTime 6:IsValid 7:Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>区域集合</returns>
        public DataSet GetList(int key, string value)
        {
            return dal.GetList(key, value);
        }

        /// <summary>
        /// 根据条件获取区域列表
        /// </summary>
        /// <param name="key">查询条件 0：RegionalID 1：RegionalName 2：RegionalCode 3：SubRegionalID 4：RegionalLevel 5:LastModifyTime 6:IsValid 7:Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：RegionalID 1：RegionalName 2：RegionalCode 3：SubRegionalID 4：RegionalLevel 5:LastModifyTime 6:IsValid 7:Memo 8:Orders 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>区域集合</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            return dal.GetList(key, value, Top, filedOrder, desc);
        }

        /// <summary>
        /// 根据条件获取区域列表(list)
        /// </summary>
        /// <param name="key">查询条件 0：RegionalID 1：RegionalName 2：RegionalCode 3：SubRegionalID 4：RegionalLevel 5:LastModifyTime 6:IsValid 7:Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：RegionalID 1：RegionalName 2：RegionalCode 3：SubRegionalID 4：RegionalLevel 5:LastModifyTime 6:IsValid 7:Memo 8:Orders 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>区域集合</returns>
        public List<SmartLaw.Model.Regional> GetModelList(int key, string value, int Top, int filedOrder, bool desc)
        {
            DataSet ds = dal.GetList(key, value, Top, filedOrder, desc);
            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        /// 将DateTable转成实体类集合
        /// </summary>
        public List<SmartLaw.Model.Regional> DataTableToList(DataTable dt)
        {
            List<SmartLaw.Model.Regional> modelList = new List<SmartLaw.Model.Regional>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SmartLaw.Model.Regional model;
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
        /// 获得所有区域集合
        /// </summary>
        /// <returns>区域集合</returns>
        public DataSet GetAllList()
        {
            return GetList(-1, "");
        }

        /// <summary>
        /// 根据条件获得区域条目数
        /// </summary>
        /// <param name="key">查询条件 0：RegionalID 1：RegionalName 2：RegionalCode 3：SubRegionalID 4：RegionalLevel 5:LastModifyTime 6:IsValid 7:Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>区域集合条目数</returns>
        public int GetRecordCount(int key, string value)
        {
            return dal.GetRecordCount(key, value);
        }

        /// <summary>
        /// 分页获取区域集合
        /// </summary>
        /// <param name="key">查询条件 0：RegionalID 1：RegionalName 2：RegionalCode 3：SubRegionalID 4：RegionalLevel 5:LastModifyTime 6:IsValid 7:Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="filedOrder">排序字段 0：RegionalID 1：RegionalName 2：RegionalCode 3：SubRegionalID 4：RegionalLevel 5:LastModifyTime 6:IsValid 7:Memo 8:Orders 其他:全部</param>
        /// <param name="desc">选用倒序</param>
        /// <param name="startIndex">起始索引</param>
        /// <param name="endIndex">结束索引</param>
        /// <returns>区域集合</returns>
        public DataSet GetListByPage(int key, string value, int filedOrder, bool desc, int startIndex, int endIndex)
        {
            return dal.GetListByPage(key, value, filedOrder, desc, startIndex, endIndex);
        }
		#endregion  BasicMethod

		#region  ExtensionMethod
        /// <summary>
        /// 根据条件获取有效的区域列表(list)
        /// </summary>
        /// <param name="key">查询条件 0：RegionalID 1：RegionalName 2：RegionalCode 3：SubRegionalID 4：RegionalLevel 5:LastModifyTime 6:IsValid 7:Memo 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：RegionalID 1：RegionalName 2：RegionalCode 3：SubRegionalID 4：RegionalLevel 5:LastModifyTime 6:IsValid 7:Memo 8:Orders 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>区域集合</returns>
        public List<SmartLaw.Model.Regional> GetModelListValid(int key, string value, int Top, int filedOrder, bool desc)
        {
            DataSet ds = dal.GetListValid(key, value, Top, filedOrder, desc);
            return DataTableToList(ds.Tables[0]);
        }
		#endregion  ExtensionMethod
	}
}

