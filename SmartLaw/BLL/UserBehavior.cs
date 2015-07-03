using System;
using System.Data;
using System.Collections.Generic;
using SmartLaw.Common;
using SmartLaw.Model;

namespace SmartLaw.BLL
{
    public partial class UserBehavior
    {
      private readonly SmartLaw.DAL.MySqlUserBehavior dal=new SmartLaw.DAL.MySqlUserBehavior();
      public UserBehavior()
      {}

      public long Add(SmartLaw.Model.UserBehavior model)
      {
          return dal.Add(model);
      } 


      /// <summary>
      /// 按Behavior统计某个用户，某个IP，某个时间段，用户行为备注中存在某些情况的记录。
      /// </summary>
      /// <param name="SimCardNO">Sim卡号，不需要针对特定的sim卡号进行统计时可将参数设置为nuLL</param>
      /// <param name="IpAddr">ip地址，不需要针对特定的IP进行统计时可将参数设置为NULL</param>
      /// <param name="Remarks">备注，null</param>
      /// <param name="Behavior">Behavior,null</param>
      /// <param name="beginT">时间段的起始时间。不需要起始时间时可将参数设为DateTime.MinValue</param>
      /// <param name="endT">时间段的结束时间。不需要设置结束时间可将参数设为DateTime.MaxValue</param> 
      /// <returns></returns>
      public DataSet CountUserBh(string SimCardNO, string IpAddr, string Remarks, string Behavior, DateTime beginT, DateTime endT)
      {
          return dal.CountUserBh(SimCardNO, IpAddr, Remarks, Behavior, beginT, endT);
      }

      /// <summary>
      /// 根据条件获取数据列表
      /// </summary>
      /// <param name="SimCardNO">Sim卡号，不需要针对特定的sim卡号进行统计时可将参数设置为nuLL</param>
      /// <param name="IpAddr">ip地址，不需要针对特定的IP进行统计时可将参数设置为NULL</param>
      /// <param name="Remarks">备注，null</param>
      /// <param name="Behavior">Behavior,null</param>
      /// <param name="beginT">时间段的起始时间。不需要起始时间时可将参数设为DateTime.MinValue</param>
      /// <param name="endT">时间段的结束时间。不需要设置结束时间可将参数设为DateTime.MaxValue</param> 
      /// <returns></returns>
      public DataSet GetList(string SimCardNO, string IpAddr, string Remarks, string Behavior, DateTime beginT, DateTime endT)
      {
          return dal.GetList(SimCardNO, IpAddr, Remarks, Behavior, beginT, endT);
      }

      /// <summary>
      /// 根据条件获取数据列表
      /// </summary>
      /// <param name="SimCardNO">Sim卡号，不需要针对特定的sim卡号进行统计时可将参数设置为nuLL</param>
      /// <param name="IpAddr">ip地址，不需要针对特定的IP进行统计时可将参数设置为NULL</param>
      /// <param name="Remarks">备注，null</param>
      /// <param name="Behavior">Behavior,null</param>
      /// <param name="beginT">时间段的起始时间。不需要起始时间时可将参数设为DateTime.MinValue</param>
      /// <param name="endT">时间段的结束时间。不需要设置结束时间可将参数设为DateTime.MaxValue</param> 
      /// <returns></returns>
      public List<SmartLaw.Model.UserBehavior> GetModelList(string SimCardNO, string IpAddr, string Remarks, string Behavior, DateTime beginT, DateTime endT)
      {
          DataSet ds =  dal.GetList(SimCardNO, IpAddr, Remarks, Behavior, beginT, endT);
          return DataTableToList(ds.Tables[0]);
      }


      /// <summary>
      /// 将DateTable转成实体类集合
      /// </summary>
      public List<SmartLaw.Model.UserBehavior> DataTableToList(DataTable dt)
      {
          List<SmartLaw.Model.UserBehavior> modelList = new List<SmartLaw.Model.UserBehavior>();
          int rowsCount = dt.Rows.Count;
          if (rowsCount > 0)
          {
              SmartLaw.Model.UserBehavior model;
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
    }
}
