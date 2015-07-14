using System;
using System.Data;
using System.Collections.Generic;
using SmartLaw.Common;
using SmartLaw.Model;
using System.Text.RegularExpressions;
namespace SmartLaw.BLL
{
    /// <summary>
    /// BusLine
    /// </summary>
    public partial class BusLine
    {
        private readonly SmartLaw.DAL.MySqlBusLine dal = new SmartLaw.DAL.MySqlBusLine();
        public BusLine()
        { }
        #region  BasicMethod
        /// <summary>
        /// 是否存在该公交线路
        /// </summary>
        public bool ExistsBusLine(long AutoID)
        {
            return dal.ExistsBusLine(AutoID);
        }


        /// <summary>
        /// 是否存在该公交站点
        /// </summary>
        public bool ExistsStation(string StationName)
        {
            return dal.ExistsStation(StationName);
        }


        /// <summary>
        /// 增加公交线路
        /// </summary>
        public long AddBusLine(SmartLaw.Model.BusLine model,out string msg)
        {
            return dal.AddBusLine(model,out msg);
        }

        /// <summary>
        /// 删除公交线路
        /// </summary>
        public bool DeleteBusLine(long AutoID)
        {

            return dal.Delete(AutoID);
        }


        /// <summary>
        /// 根据条件获取线路信息
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：RouteName 2：Station 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>数据集</returns>
        public DataSet GetBusLineList(int key, string value)
        {
            
            return dal.GetBusLineList(key, value);
        }




        /// <summary>
        /// 根据条件获取线路信息
        /// </summary>
        /// <param name="key">查询条件 0：AutoID 1：RouteName 2：Station 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>数据集</returns>
        public List<Model.BusLine> GetBusLineModelList(int key, string value)
        {
            DataSet ds= dal.GetBusLineList(key, value);
            DataTable dt = ds.Tables[0];
            if (key == 1)
            {
                return BusLineFilterByName(dt, value);
            }
            return DataTableToList(dt);

        }


        public List<SmartLaw.Model.BusLine> BusLineFilterByName(DataTable ds,string value)
        {
            Regex r0 = new Regex("^[^0-9]*?" + value + "[^0-9]*?");
            Regex r1=new Regex("[0-9]+?"+value);
            Regex r2 = new Regex(value + "[0-9]+?");
            List<Model.BusLine> models=new List<Model.BusLine>();
            //DataTable ds2 = new DataTable();
            for (int i = 0; i < ds.Rows.Count; ++i)
            {
                string s=r0.Match(ds.Rows[i]["RouteName"].ToString()).Value;
                if (s != null&& s != "")
                {
                    if (r1.Match(ds.Rows[i]["RouteName"].ToString()).Value == "" && r2.Match(ds.Rows[i]["RouteName"].ToString()).Value == "")
                    {
                        Model.BusLine amodel = new Model.BusLine();
                        amodel = dal.DataRowToModel(ds.Rows[i]);
                        models.Add(amodel);
                    }
                    
                  
                }
                
            }

            return models;
        }





        /// <summary>
        /// 根据拼音首字母缩写获取站点名称
        /// </summary>
        /// <param name="Abbr"></param>
        /// <returns></returns>
        public string[] getStationName(string Abbr)
        {
            return dal.getStationName(Abbr);
        }




        /// <summary>
        /// 将公交线路的DateTable转成实体类集合
        /// </summary>
        public List<SmartLaw.Model.BusLine> DataTableToList(DataTable dt)
        {
            List<SmartLaw.Model.BusLine> modelList = new List<SmartLaw.Model.BusLine>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SmartLaw.Model.BusLine model;
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




        #endregion  BasicMethod
    }
}