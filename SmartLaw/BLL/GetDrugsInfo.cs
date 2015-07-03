using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
//using System.Web.Services;
using System.Text;
using System.Net;
using System.IO;
using SmartLaw.Model;
using System.Text.RegularExpressions;

namespace SmartLaw.BLL
{
    public partial class GetDrugsInfo
    {
        private readonly SmartLaw.DAL.MySqlDrugsInfo dal = new SmartLaw.DAL.MySqlDrugsInfo();
        private Encoding encoding = Encoding.UTF8;
        private string url = "http://data.baiduyy.com/yaojia/default10.aspx?page=1&subject=&company=&province_name=%E6%B5%99%E6%B1%9F";
        private string pageUrl1 = "http://data.baiduyy.com/yaojia/default10.aspx?page=";
        private string pageUrl2 = "&subject=&company=&province_name=浙江";
        private CookieContainer m_Cookie = new CookieContainer();


        public bool DeleteAll()
        {
            return dal.DeleteAll();
        }



        public bool GetDrugs(out string msg)
        {
            try
            {
                string htm = PostWebRequest(url, "", encoding, false);
                int pagenum = GetPageNum(htm);//获取每一页的url
                bool isDeleted = false;
                for (int i = 1; i<=200 ; ++i)//本来应该小于pagenum，但是网页限制只能取到200页
                {
                        string pageURL = pageUrl1 + i.ToString() + pageUrl2;
                        string htm2 = PostWebRequest(pageURL, "", encoding, true);

                        Regex q = new Regex("<tr  align=\"center\" bgcolor=\"#FFFFFF\" onmouseover=\"javascript:this.style.backgroundColor='#F2F4FB';\"  onmouseout=\"javascript:this.style.backgroundColor='#ffffff';\">[\\s\\S]+?</tr>");

                        MatchCollection mCollection = q.Matches(htm2);
                        if (mCollection.Count == 0)
                        {
                            msg = "网页已改版无法匹配信息";
                            return false;
                        }
                       
                        foreach (Match m in mCollection)
                        {
                            Model.DrugsInfo model = new Model.DrugsInfo();
                            model = GetModelInfo(m.ToString(),out msg);
                            if (model == null)
                            {
                                return false;
                            }
                            if (isDeleted == false)
                            {
                                isDeleted = true;
                                dal.DeleteAll();
                            }
                            dal.Add(model);
                        }
                }
                msg = "成功";
                return true;
            }
            catch(Exception ee)
            {
                msg = ee.ToString();
                return false;
            }
          
        }



        private int GetPageNum(string htm)//页数。根据页数计算每页的url
        {

            Regex qurl = new Regex("共.+?页");
            Regex qurl0 = new Regex("共");
            Regex qurl1 = new Regex("页");
           
            string spagenum = qurl.Match(htm).Value;

            spagenum = qurl0.Replace(spagenum, "");
            spagenum = qurl1.Replace(spagenum, "");

            return int.Parse(spagenum);
        }



        private Model.DrugsInfo GetModelInfo(string htm,out string msg)//获取具体信息
        {
            Model.DrugsInfo diModel = new Model.DrugsInfo();

            

            //药品名称
            Regex qzp = new Regex("<td width=\"110px\" align=\"center\" >.+?</td>");
            Regex qzp0 = new Regex("<td width=\"110px\" align=\"center\" >");
            Regex qzp1 = new Regex("</td>");
            string drugName= qzp.Match(htm).Value;
            drugName = qzp0.Replace(drugName, "");
            drugName = qzp1.Replace(drugName, "");
            if (drugName == "" || drugName == null)
            {
                msg = "网页已改版无法获取药品名称";
                return null;
            }
            diModel.DrugName = drugName;

            //剂型
            Regex qrm = new Regex("<td align=\"center\" width=\"85px\" >.+?</td>");
            Regex qrm0 = new Regex("<td align=\"center\" width=\"85px\" >");
            Regex qrm1 = new Regex("</td>");

            string form= qrm.Match(htm).Value;
            form = qrm0.Replace(form, "");
            form = qrm1.Replace(form, "");
            diModel.Form = form;


            ////剂型说明
            //Regex qfi = new Regex("剂型说明：</td>[\\s\\S]+?<td width=\"80%\" align=\"left\" bgcolor=\"f6f6f6\">.+?</td>");
            //Regex qfi0 = new Regex("剂型说明：</td>[\\s\\S]+?<td width=\"80%\" align=\"left\" bgcolor=\"f6f6f6\">");
            //Regex qfi1 = new Regex("</td>");
            //string formInfo = qfi.Match(str).Value;
            //formInfo = qfi0.Replace(formInfo, "");
            //formInfo = qfi1.Replace(formInfo, "");
            //diModel.FormInfo = formInfo;


            //规格
            Regex qsp = new Regex("<td width=\"120px\" >.+?</td>");
            Regex qsp0 = new Regex("<td width=\"120px\" >");
            Regex qsp1 = new Regex("</td>");

            string spec = qsp.Match(htm).Value;
            spec = qsp0.Replace(spec, "");
            spec = qsp1.Replace(spec, "");
            diModel.Spec = spec;


            //单位
            Regex qun = new Regex("<td width=\"80px\" >.+?</td>");
            Regex qun0 = new Regex("<td width=\"80px\" >");
            Regex qun1 = new Regex("</td>");

            string unit = qun.Match(htm).Value;
            unit = qun0.Replace(unit, "");
            unit = qun1.Replace(unit, "");
            diModel.Unit = unit;


            //参考价
            Regex qpr = new Regex("<td width=\"55px\" >.+?</td>");
            Regex qpr0 = new Regex("<td width=\"55px\" >");
            Regex qpr1 = new Regex("</td>");

            string price = qpr.Match(htm).Value;
            price = qpr0.Replace(price, "");
            price = qpr1.Replace(price, "");
            if (price == "" || price == null)
            {
                msg = "网页已改版无法获取药品参考价";
                return null;
            }
            diModel.Price = float.Parse( price);



            //地区
            Regex qdt = new Regex("<td width=\"50px\" >.+?</td>");
            Regex qdt0 = new Regex("<td width=\"50px\" >");
            Regex qdt1 = new Regex("</td>");
            
            string district = qdt.Match(htm).Value;
            district = qdt0.Replace(district, "");
            district = qdt1.Replace(district, "");
            
            diModel.District = district;


            //生效时间
            Regex qvlt = new Regex("<td width=\"70px\" > .+?</td>");
            Regex qvlt0 = new Regex("<td width=\"70px\" >");
            Regex qvlt1 = new Regex("</td>");

            string validateTime = qvlt.Match(htm).Value;
            validateTime = qvlt0.Replace(validateTime, "");
            validateTime = qvlt1.Replace(validateTime, "");
            diModel.ValidateTime = validateTime;


            //生产企业
            Regex qcmp = new Regex("<td width=\"150px\" >.+?</td>");
            Regex qcmp0 = new Regex("<td width=\"150px\" >");
            Regex qcmp1 = new Regex("</td>");

            string company = qcmp.Match(htm).Value;
            company = qcmp0.Replace(company, "");
            company = qcmp1.Replace(company, "");
            diModel.Company = company;


            ////政策依据
            //Regex qpl = new Regex("政策依据：</td>[\\s]+?<td align=\"left\" bgcolor=\"f6f6f6\">[\\s]+?.+?");
            //Regex qpl0 = new Regex("政策依据：</td>[\\s]+?<td align=\"left\" bgcolor=\"f6f6f6\">[\\s]+?");

            //string policy = qpl.Match(str).Value;
            //policy = qpl0.Replace(policy, "");
            //diModel.Policy = policy;


            msg = "成功";
            return diModel;
        }

        private string PostWebRequest(string postUrl, string paramData, Encoding dataEncode, bool hasCookie)
        {
            string ret = string.Empty;

            byte[] byteArray = dataEncode.GetBytes(paramData); //转化
            Uri uri = new Uri(postUrl);
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(uri);
            webReq.Method = "POST";
            webReq.ContentType = "application/x-www-form-urlencoded";
            if (hasCookie)
                webReq.CookieContainer = m_Cookie;

            //webReq.ContentLength = byteArray.Length;
            Stream newStream = webReq.GetRequestStream();
            newStream.Write(byteArray, 0, byteArray.Length);//写入参数
            newStream.Close();
            HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();



            StreamReader sr = new StreamReader(response.GetResponseStream(), dataEncode);
            ret = sr.ReadToEnd();
            sr.Close();
            response.Close();
            newStream.Close();
            return ret;
        }
    }
}
