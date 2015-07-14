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
    public partial class GetBusInfo
    {
        private readonly SmartLaw.DAL.MySqlBusLine dal = new SmartLaw.DAL.MySqlBusLine();
        private Encoding encoding = Encoding.GetEncoding( "utf-8");
        private string url = "http://bus.aibang.com/bus/ningbo/line_1.html";


        public bool DeleteAll()
        {
            return dal.DeleteAll();
        }



        public bool GetBusLine()
        {
            string htm = PostWebRequest(url,"", encoding, false);
            List<string> pageURLs=  GetPageURLs(htm);//获取每一页的url
            pageURLs.Add(url);
            string msg = "";
            for (int i = 0; i < pageURLs.Count; ++i)//对于每一页
            {
                string pageURL = "";
                if (i != pageURLs.Count - 1)
                {
                   pageURL= "http://bus.aibang.com" + pageURLs[i];
                }
                else
                {
                    pageURL = pageURLs[i];
                }
                string urls=PostWebRequest(pageURL,"",encoding,false);
               
                List<string> infoURLs = GetInfoURLs(urls);//获取每一页上公交线路信息的url
                for (int j = 0; j < infoURLs.Count; ++j)//对于每个线路
                {
                    

                    Model.BusLine model = new Model.BusLine();
                    
                    model = GetBusLineInfo(infoURLs[j]);
                    
                    try
                    {
                        dal.AddBusLine(model, out msg);
                    }
                    catch
                    {
                        return false;
                    }
                }
            }
            if (msg != "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        


        private List<string> GetPageURLs(string htm)
        {
            List<string> pageURLs = new List<string>();
            Regex qurl = new Regex("<p class=\"title0\"><strong>按数字查看：</strong>[\\s\\S]+?</p>");
            Regex qurl0 = new Regex("<a href=\".+?\" target=\"_self\" title=\"宁波公交路线\" onmousedown=");
            Regex qurl1 = new Regex("<a href=\"");
            Regex qurl2 = new Regex("\" target=\"_self\" title=\"宁波公交路线\" onmousedown=");
            string urls = qurl.Match(htm).Value;
            MatchCollection mCollection =qurl0.Matches(urls);
            foreach (Match m in mCollection)
            {
                string aurl = qurl1.Replace(m.ToString(), "");
                aurl = qurl2.Replace(aurl, "");
              pageURLs.Add(aurl);
            }
           
            return pageURLs;
        }


        private List<string> GetInfoURLs(string htm)
        {
            List<string> infoURLs=new List<string> ();
            Regex qinf = new Regex("<div class=\"hy\">[\\s\\S]+?</div>");
            Regex qinf0 = new Regex("<p><a href=\".+?\" target=");
            Regex qinf1 = new Regex("<p><a href=\"");
            Regex qinf2 = new Regex("\" target=");
            string infs = qinf.Match(htm).Value;
            MatchCollection mCollection = qinf0.Matches(infs);
            foreach (Match m in mCollection)
            {
                string ainf = qinf1.Replace(m.ToString(), "");
                ainf = qinf2.Replace(ainf, "");
               infoURLs.Add(ainf);
            }

            
            return infoURLs;
        }

        private Model.BusLine GetBusLineInfo(string infoURL)
        {
            Model.BusLine blModel = new Model.BusLine();

            string str = PostWebRequest(infoURL, "", encoding, false);


            Regex qzp = new Regex("<span class=\"red mr10\">.+?</span>站牌信息");
            Regex qzp0 = new Regex("<span class=\"red mr10\">");
            Regex qzp1 = new Regex("</span>站牌信息");
            string routName = qzp.Match(str).Value;
            routName= qzp0.Replace(routName, "");
            routName = qzp1.Replace(routName, "");
            blModel.RouteName = routName;


            Regex qrm = new Regex("<div class=\"line_info\">[\\s\\S]+?<div class=\"line_point\">");
            Regex qrm0 = new Regex("<p>.+?</p>");
            Regex qrm1 = new Regex("<p>");
            Regex qrm2 = new Regex("</p>");
            string remarks = "";
            remarks=    qrm.Match(str).Value;
            remarks = qrm0.Match(remarks).Value;
            remarks = qrm1.Replace(remarks, "");
            remarks = qrm2.Replace(remarks, "");

            Regex qrmk = new Regex("<div class=\"line_time\">[\\s\\S]+?<div class=\"line_point\">");
            Regex qrmk0 = new Regex("<p>.+?</p>");
            Regex qrmk1 = new Regex("<p>");
            Regex qrmk2 = new Regex("</p>");
            string remarks2 = "";
            remarks2 = qrmk.Match(str).Value;
            remarks2 = qrmk0.Match(remarks2).Value;
            remarks2 = qrmk1.Replace(remarks2, "");
            remarks2 = qrmk2.Replace(remarks2, "");

            remarks = remarks + remarks2;

            blModel.Remarks = remarks;





            List<string> stations = new List<string>();
            Regex qstt = new Regex("<a href=\"http://bus.aibang.com/ningbo/station-[\\s\\S]+? name=\".+?\"");
            Regex qstt0 = new Regex("<a href=\"http://bus.aibang.com/ningbo/station-[\\s\\S]+? name=\"");
            Regex qstt1 = new Regex("\"");
            MatchCollection mCollection = qstt.Matches(str);
            foreach (Match m in mCollection)
            {
                string astt = qstt0.Replace(m.ToString(), "");
                astt = qstt1.Replace(astt, "");
                stations.Add(astt);
            }
            blModel.Station = stations.ToArray();

            return blModel;
        }

        private string PostWebRequest(string postUrl, string paramData, Encoding dataEncode, bool hasCookie)
        {
            string ret = string.Empty;

            byte[] byteArray = dataEncode.GetBytes(paramData); //转化
            Uri uri = new Uri(postUrl);
            HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(uri);
            webReq.Method = "POST";
            webReq.ContentType = "application/x-www-form-urlencoded";

           
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
