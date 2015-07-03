using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using SmartLaw.BLL;

namespace SmartLaw.Admin.NewsManage
{
    public class Help
    {
        Category cg = new Category();
        Regional rg = new Regional();
        SysCodeDetail scd = new SysCodeDetail();
        CategoryRegionalRelation crr = new CategoryRegionalRelation();

        /// <summary>
        /// 可获取到的分类
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isValid"></param>
        /// <returns></returns>
        public List<Model.Category> GetCategories(string userId, bool isValid)
        {
            DataSet scdDs = scd.GetListBySysCode(userId, "Region");
            String user_Regional = scdDs.Tables[0].Rows[0]["SYSCodeDetialID"].ToString();
            Model.Regional rgModel = rg.GetModel(user_Regional);
            List<Model.CategoryRegionalRelation> crrtempList = new List<Model.CategoryRegionalRelation>();
            List<Model.Category> cgList2 = new List<Model.Category>();
            List<Model.Category> cgList = new List<Model.Category>();
            //村级用户
            if (rgModel.RegionalLevel.Equals("RegionLevel_Community"))
            {
                crrtempList.AddRange(crr.GetModelList(2, rgModel.RegionalID, -1, 2, false));
                Model.Regional rgModel2 = rg.GetModel(rgModel.SubRegionalID);
                crrtempList.AddRange(crr.GetModelList(2, rgModel2.RegionalID, -1, 2, false));
                Model.Regional rgModel3 = rg.GetModel(rgModel2.SubRegionalID);
                crrtempList.AddRange(crr.GetModelList(2, rgModel3.RegionalID, -1, 2, false));
                foreach (Model.CategoryRegionalRelation crrM in crrtempList)
                {
                    cgList.Add(cg.GetModel(crrM.CategotyID));
                }
            }
            //街道级用户
            else if (rgModel.RegionalLevel.Equals("RegionLevel_Street"))
            {
                crrtempList.AddRange(crr.GetModelList(2, rgModel.RegionalID, -1, 2, false));
                Model.Regional rgModel2 = rg.GetModel(rgModel.SubRegionalID);
                crrtempList.AddRange(crr.GetModelList(2, rgModel2.RegionalID, -1, 2, false));
                foreach (Model.Regional rgM in rg.GetModelList(3, rgModel.RegionalID, -1, 8, false))
                {
                    crrtempList.AddRange(crr.GetModelList(2, rgM.RegionalID, -1, 2, false));
                }
                foreach (Model.CategoryRegionalRelation crrM in crrtempList)
                {
                    cgList.Add(cg.GetModel(crrM.CategotyID));
                }
            }
            //区级
            else
            {
                cgList = cg.GetModelList(-1, "", -1, 0, false);
            }
            cgList2 = cgList.GroupBy(rt => rt.AutoID).Select(p => new Model.Category
            {
                AutoID = p.Key,
                CategoryName = p.FirstOrDefault().CategoryName,
                ParentCategoryID = p.FirstOrDefault().ParentCategoryID,
                Orders = p.FirstOrDefault().Orders,
                LastModifyTime = p.FirstOrDefault().LastModifyTime,
                Memo = p.FirstOrDefault().Memo,
                IsValid = p.FirstOrDefault().IsValid
            }).ToList();
            if (isValid)
            {
                cgList2.RemoveAll(rt => rt.IsValid == false);
            }
            return cgList2.OrderBy(ct => ct.Orders).ToList();
        }

        /// <summary>
        /// 可获取到的分类区域
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns> 
        public List<Model.Regional> GetRegions(string userId)
        {
            DataSet scdDs = scd.GetListBySysCode(userId, "Region");
            String user_Regional = scdDs.Tables[0].Rows[0]["SYSCodeDetialID"].ToString();
            Model.Regional rgModel = rg.GetModel(user_Regional);
            List<Model.Regional> rgList = new List<Model.Regional>();
            //村级用户 ----获取到本村 本村对应街道 本村对应区
            if (rgModel.RegionalLevel.Equals("RegionLevel_Community"))
            {
                Model.Regional rgModel2 = rg.GetModel(rgModel.SubRegionalID);
                Model.Regional rgModel3 = rg.GetModel(rgModel2.SubRegionalID);
                rgModel3.SubRegionalID = null;
                rgList.Add(rgModel);
                rgList.Add(rgModel2);
                rgList.Add(rgModel3);
            }
            //街道级用户 -----获取到本街道 本街道下属所有村 本街道对应区
            else if (rgModel.RegionalLevel.Equals("RegionLevel_Street"))
            { 
                rgList.Add(rgModel);
                Model.Regional rgModel2 = rg.GetModel(rgModel.SubRegionalID);
                rgModel2.SubRegionalID = null;
                rgList.Add(rgModel2);
                rgList.AddRange(rg.GetModelList(3, rgModel.RegionalID, -1, 8, false));
                 
            }
            //区级----获取到所有区域
            else
            {
                rgList = rg.GetModelList(6, "1", -1, 8, false);
                foreach (Model.Regional rgM in rgList)
                {
                    if (rgM.SubRegionalID.Equals("Root"))
                    {
                        rgM.SubRegionalID = null;
                    }
                }
            }
            rgList.RemoveAll(rt => rt.IsValid == false);
            return rgList.OrderBy(rt => rt.Orders).ToList();
        }

        /// <summary>
        /// 有权限进行操作的区域
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isValid"></param>
        /// <returns></returns> 
        public List<Model.Regional> GetRegions2(string userId,bool isValid)
        {
            DataSet scdDs = scd.GetListBySysCode(userId, "Region");
            String user_Regional = scdDs.Tables[0].Rows[0]["SYSCodeDetialID"].ToString();
            Model.Regional rgModel = rg.GetModel(user_Regional);
            List<Model.Regional> rgList = new List<Model.Regional>();
             //村级用户
            if (rgModel.RegionalLevel.Equals("RegionLevel_Community"))
            {
                rgList.Add(rgModel);
            }
            //街道级用户
            else if (rgModel.RegionalLevel.Equals("RegionLevel_Street"))
            {
                rgList.Add(rgModel);
                rgList.AddRange(rg.GetModelList(3, rgModel.RegionalID, -1, 8, false));
            }
            //区级
            else
            {
                rgList = rg.GetModelList(6, "1", -1, 8, false);
            }
            return rgList;
        }

        /// <summary>
        /// 获取分类的上下限分类的对应区域,做判断
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="IsParent"></param>
        /// <returns></returns>
        public List<Model.Regional> GetRgList(string categoryId,bool IsParent)
        { 
            if (IsParent)
            { 
                List<Model.CategoryRegionalRelation> crrList = crr.GetModelList(1, categoryId, -1, -1, false);
                List<Model.Regional> rgList = new List<Model.Regional>();
                foreach (Model.CategoryRegionalRelation crrM in crrList)
                {
                    if (rg.GetModel(crrM.RegionalID).RegionalLevel.Equals("RegionLevel_Area"))
                    {
                        rgList = rg.GetModelList(6, "1", -1, 8, false);
                        break;
                    }
                    if (rg.GetModel(crrM.RegionalID).RegionalLevel.Equals("RegionLevel_Street"))
                    {
                        rgList.Add(rg.GetModel(crrM.RegionalID));
                        rgList.AddRange(rg.GetModelList(3, crrM.RegionalID, -1, 8, false));
                    }
                    if (rg.GetModel(crrM.RegionalID).RegionalLevel.Equals("RegionLevel_Community"))
                    {
                        rgList.Add(rg.GetModel(crrM.RegionalID));
                    } 
                } 
                rgList.RemoveAll(rt => rt.IsValid == false);
                return rgList.Distinct().ToList().OrderBy(rt => rt.Orders).ToList();
            }
            else
            { 
                List<Model.Category> cgList = cg.GetModelList(2, categoryId, -1, -1, false);
                List<Model.CategoryRegionalRelation> crrList = new List<Model.CategoryRegionalRelation>();
                List<Model.Regional> rgList = new List<Model.Regional>();
                bool haveAll = false;
                foreach (Model.Category cgM in cgList)
                {
                    foreach (Model.CategoryRegionalRelation crrM in crr.GetModelList(1, cgM.AutoID.ToString(), -1, -1, false))
                    {
                        if (!rgList.Exists(rt => rt.RegionalID.Equals(crrM.RegionalID)))
                        {
                            Model.Regional rgM = rg.GetModel(crrM.RegionalID); 
                            if (rgM.RegionalLevel.Equals("RegionLevel_Area"))
                            {
                                rgList = rg.GetModelList(6, "1", -1, 8, false);
                                haveAll = true;
                                break;
                            }
                            if (rgM.RegionalLevel.Equals("RegionLevel_Street"))
                            {
                                rgList.Add(rgM);
                                rgList.AddRange(rg.GetModelList(3, crrM.RegionalID, -1, 8, false));
                            }
                            if (rgM.RegionalLevel.Equals("RegionLevel_Community"))
                            {
                                rgList.Add(rgM); 
                            }
                        }
                    }
                    if (haveAll)
                    {
                        break;
                    }
                }  
                rgList.RemoveAll(rt => rt.IsValid == false);
                return rgList.OrderBy(rt => rt.Orders).ToList(); 
            } 
        }

        /// <summary>
        /// 与分类关联的区域
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="isParent"></param>
        /// <returns></returns>
        public List<Model.Regional> GetRegions3(string categoryId,bool isParent)
        { 
            List<Model.CategoryRegionalRelation> crrList = new List<Model.CategoryRegionalRelation>(); 
            List<Model.Regional> rgList = new List<Model.Regional>();
            if (isParent)
            {
                crrList.AddRange(crr.GetModelList(1, categoryId, -1, -1, false));
                foreach (Model.CategoryRegionalRelation crrM in crrList)
                {
                    if (rg.GetModel(crrM.RegionalID).RegionalLevel.Equals("RegionLevel_Area"))
                    {
                        rgList.Clear();
                        rgList = rg.GetModelList(6, "1", -1, -1, false);
                        break;
                    }
                    if (rg.GetModel(crrM.RegionalID).RegionalLevel.Equals("RegionLevel_Street"))
                    {
                        rgList.Add(rg.GetModel(crrM.RegionalID));
                        rgList.AddRange(rg.GetModelList(3, crrM.RegionalID, -1, -1, false));
                    }
                    else if (rg.GetModel(crrM.RegionalID).RegionalLevel.Equals("RegionLevel_Community"))
                    {
                        rgList.Add(rg.GetModel(crrM.RegionalID));
                    }
                }
            }
            else
            {
                List<Model.Regional> rgtempList = new List<Model.Regional>();
                List<Model.Category> cgList = cg.GetModelList(2, categoryId, -1, -1, false);
                foreach (Model.Category cgM in cgList)
                {
                    crrList.AddRange(crr.GetModelList(1, cgM.AutoID.ToString(), -1, -1, false));
                }
                foreach (Model.CategoryRegionalRelation crrM in crrList)
                {
                    rgtempList.Add(rg.GetModel(crrM.RegionalID));
                }
                rgList = rgtempList.GroupBy(rt => rt.RegionalID).Select(p => new Model.Regional
                {
                    RegionalID = p.Key,
                    RegionalName = p.FirstOrDefault().RegionalName,
                    RegionalLevel = p.FirstOrDefault().RegionalLevel,
                    RegionalCode = p.FirstOrDefault().RegionalCode,
                    SubRegionalID = p.FirstOrDefault().SubRegionalID,
                    Orders = p.FirstOrDefault().Orders,
                    LastModifyTime = p.FirstOrDefault().LastModifyTime,
                    Memo = p.FirstOrDefault().Memo,
                    IsValid = p.FirstOrDefault().IsValid
                }).ToList();
            
            }
            return rgList;
        }
    }
}