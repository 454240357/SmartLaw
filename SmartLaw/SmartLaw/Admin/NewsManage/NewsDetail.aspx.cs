using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using SmartLaw.App_Code;
using SmartLaw.BLL;
using System.Data;
using System.Text;
using Telerik.Web.UI;
using System.Text.RegularExpressions; 
using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.Web.UI.Widgets;
using System.Drawing.Imaging;
using System.Net;
using System.IO;

namespace SmartLaw.Admin.NewsManage
{
    public partial class NewsDetail : BasePage
    {
        public long autoID; 
        private SessionUser user;
        News news = new News(); 
        Category cg = new Category();
        SysCodeDetail scd = new SysCodeDetail(); 
        Log log = new Log();
        SysUser su = new SysUser(); 
        public static int img_width;
        public static int img_height;
        protected void Page_Load(object sender, EventArgs e)
        {
            img_width = 5000;
            img_height = 5000;
            Model.SysCodeDetail widthModel = scd.GetModel("img_width");
            if (widthModel != null)
            {
                img_width = int.Parse(widthModel.SYSCodeDetialContext);
            }
            Model.SysCodeDetail heightModel = scd.GetModel("img_height");
            if (heightModel != null)
            {
                img_height = int.Parse(heightModel.SYSCodeDetialContext);
            }
            user = SessionUser.GetSession();
            user.ValidateAuthority("Auth_News_Retrieve");
            autoID = long.Parse(Request.QueryString["AutoID"]); 
            if (!IsPostBack)
            {
                contentEditor.EditModes = EditModes.Preview;
                List<Model.Category> cgList = cg.DataTableToList(cg.GetList(5, "1", -1, 0, false).Tables[0]);
                cgList.RemoveAll(CT => !CT.Memo.Contains("RW"));
                DataTable table = new DataTable();
                table.Columns.Add("AutoID");
                table.Columns.Add("ParentCategoryID");
                table.Columns.Add("CategoryName");
                foreach (Model.Category cgm in cgList)
                {
                    if (cgm.ParentCategoryID == -1)
                    {
                        table.Rows.Add(new String[] { cgm.AutoID.ToString(), null, cgm.CategoryName });
                    }
                    else
                    {
                        table.Rows.Add(new String[] { cgm.AutoID.ToString(), cgm.ParentCategoryID.ToString(), cgm.CategoryName });
                    }
                }
                RadDropDownTree2.DataFieldID = "AutoID";
                RadDropDownTree2.DataFieldParentID = "ParentCategoryID";
                RadDropDownTree2.DataValueField = "AutoID";
                RadDropDownTree2.DataTextField = "CategoryName";
                RadDropDownTree2.DataSource = table;
                RadDropDownTree2.DataBind();
                RadTreeView categoryTreeView = RadDropDownTree2.Controls[0] as RadTreeView;
                categoryTreeView.Nodes[0].Expanded = true;
                categoryTreeView.ShowLineImages = false; 
                if (!ReadValue())
                {
                    Panel1.Visible = false;
                } 
            } 
        }  

        private bool ReadValue()
        { 
            Model.News newsMOdel = news.GetModel(autoID); 
            if (newsMOdel == null)
            {
                return false;
            }

            if (!cg.GetModel(newsMOdel.CategoryID).Memo.Contains("RW"))
            {
                btn_ReEdit.Enabled = false;//同步可改
            }
            else
            {
                if (!user.hasAuthority("Auth_News_Edit") || !newsMOdel.Publisher.Equals(user.UserInfo.UserID))
                {
                    btn_ReEdit.Enabled = false;
                }
                if (!canDo("Auth_News_Edit"))
                {
                    btn_ChangeValid.Enabled = false;
                }
                if (!canDo("Auth_News_Examine") || newsMOdel.Publisher.Equals(user.UserInfo.UserID))
                {
                    btn_Checked.Enabled = false;
                }
                btn_ReEdit.Enabled = true;
            }
            TB_DataSource.Text = scd.GetModel(newsMOdel.DataSource).SYSCodeDetialContext;
            TB_DataSource.ReadOnly = true;
            StringBuilder sb = new StringBuilder(); 
            TB_Category.Text = cg.GetModel(newsMOdel.CategoryID).CategoryName;
            string publiser = newsMOdel.Publisher;
            if( newsMOdel.DataSource.Equals("DataSource_Manual") ){
                if( su.GetModel(newsMOdel.Publisher)!=null){
                    publiser=su.GetModel(newsMOdel.Publisher).UserName+"[" + newsMOdel.Publisher+"]";
                }
                else{
                    publiser=newsMOdel.Publisher+"[已删除]";
                }
            }
            TB_Publisher.Text =publiser;
            RCB_Enable.SelectedValue = newsMOdel.IsValid ? "1" : "0";
            RCB_Checked.SelectedValue = newsMOdel.Checked.ToString();
            TB_CheckMemo.Text = newsMOdel.CheckMemo;
            TB_Checker.Text = newsMOdel.Checker == "" ? "" : (su.GetModel(newsMOdel.Checker) == null ? "" : su.GetModel(newsMOdel.Checker).UserName + "[" + newsMOdel.Checker + "]");
            Title1.Text = newsMOdel.Title; 
            if (newsMOdel.DataType == "DataType_Text")
            {
                contentText.Text = newsMOdel.Contents;
                contentEditor.Content = newsMOdel.Contents; 
                RadMultiPag1.SelectedIndex = 1;
                TabStrip1.SelectedIndex = 1;
            }
            else if (newsMOdel.DataType == "DataType_Html")
            {
                contentEditor.Content = newsMOdel.Contents; 
                RadMultiPag1.SelectedIndex = 0 ;
                TabStrip1.SelectedIndex = 0;
            }
            contentEditor.ImageManager.ContentProviderTypeName = typeof(ChangeImageSizeProvider).AssemblyQualifiedName;
            if (!user.hasAuthority("Auth_Image_Upload"))
            {
                string[] a = { "" };
                contentEditor.ImageManager.UploadPaths = a;
                contentEditor.ImageManager.DeletePaths = a;
                contentEditor.ImageManager.ViewPaths = a;
            } 
            return true;
        }

        private bool canDo(string operationItem)  //是否有权限
        {
            if (!user.hasAuthority(operationItem))
            {
                return false;
            }  
            return true; ;
        }

        protected void btn_ChangeValid_Click(object sender, EventArgs e)
        {
            bool NewsEnable = RCB_Enable.SelectedItem.Value == "1" ? true : false;
            Model.News newsModel = news.GetModel(autoID);
            newsModel.IsValid = NewsEnable; 
            bool isUpdate = false;
            Model.Log logModel = new Model.Log();
            try
            {
                logModel.OperationItem = "修改条目状态";
                logModel.Operator = user.UserInfo.UserID;
                logModel.OperationTime = DateTime.Now;
                logModel.OperationDetail = "条目编号："+autoID+" - 状态："+NewsEnable; 
                isUpdate = news.Update(newsModel);
                if (isUpdate)
                {
                    logModel.Memo = "成功";
                }
                else
                {
                    logModel.Memo = "失败！";
                }
            }
            catch (Exception ex)
            {
                logModel.Memo += "异常：" + ex.Message;
            }
            finally
            {
                log.Add(logModel);
                if (isUpdate)
                {
                    RadScriptManager.RegisterStartupScript(Page, GetType(), "c2", "OpenAlert('恭喜！状态修改成功！');", true);
                }
                else
                {
                    RadScriptManager.RegisterStartupScript(Page, GetType(), "c3", "OpenAlert('抱歉！状态修改失败！');", true);
                }
            }
        }

        protected void btn_ReEdit_Click(object sender, EventArgs e)
        { 
            if (btn_ReEdit.Visible)
            {
                btn_ReEdit.Visible = false;
                btn_CancelEdit.Visible = true;
                SetVisibles(true);
            }
            else
            {
                btn_ReEdit.Visible = true;
                btn_CancelEdit.Visible = false;
                SetVisibles(false);
            }
        }

        protected void Btn_Modify_Click(object sender, EventArgs e)
        {  
            RadTreeView categoryTreeView = RadDropDownTree2.Controls[0] as RadTreeView;
            long caId = -1;
            string title = Title1.Text;
            string content = (TabStrip1.SelectedTab.Value == "2") ? contentText.Text : contentEditor.Content;
            string content2 = (TabStrip1.SelectedTab.Value == "2") ? "" : CheckStr(content);
            if (content.Contains("src=\"data:image"))
            {
                RadScriptManager.RegisterStartupScript(Page, GetType(), "c1", "OpenAlert('图片不能直接复制到编辑器，请使用工具栏的图片上传功能！');", true);
                return;
            }
            if (categoryTreeView.SelectedNode != null)
            {
                caId = long.Parse(categoryTreeView.SelectedNode.Value);
            }
            Model.News newsMOdel = news.GetModel(autoID);
            Model.Log logModel = new Model.Log();
            logModel.OperationItem = "修改条目";
            logModel.Operator = user.UserInfo.UserID;
            logModel.OperationTime = DateTime.Now;
            logModel.OperationDetail = "条目编号：" + newsMOdel.AutoID + " - 原发布人：" + newsMOdel.Publisher+" - 原审核人："+newsMOdel.Checker+" - 原审核备注："+newsMOdel.CheckMemo;
            newsMOdel.Title = title;
            newsMOdel.Contents = (TabStrip1.SelectedTab.Value == "2") ? content : RadButton1.Checked ? content2 : content;
            newsMOdel.DataType = (TabStrip1.SelectedTab.Value == "2") ? "DataType_Text" : "DataType_Html";
            newsMOdel.Checked = 0;
            newsMOdel.Checker = "";
            newsMOdel.CheckMemo = "";  
            newsMOdel.LastModifyTime = DateTime.Now; 
            newsMOdel.Publisher = user.UserInfo.UserID;
            if (caId != -1)
            {
                newsMOdel.CategoryID = caId;
            }
            if (newsMOdel.CategoryID== 200)
            {
                if (!imgHandle(newsMOdel.Contents))
                {
                    RadScriptManager.RegisterStartupScript(Page, GetType(), "c3", "OpenAlert('抱歉！添加条目失败！-缩略图生成失败');", true);
                    return;
                }
            }
            bool isUpdate = false; 
            try
            {  
                isUpdate = news.Update(newsMOdel);  
                if (isUpdate)
                {
                    logModel.Memo = "成功";
                }
                else
                {
                    logModel.Memo = "失败！";
                }

            }
            catch (Exception ex)
            {
                logModel.Memo = "异常：" + ex.Message;
            }
            finally
            {
                log.Add(logModel);
                if (isUpdate)
                { 
                    ReadValue();
                    btn_ReEdit_Click(null,null);
                    RadScriptManager.RegisterStartupScript(Page, GetType(), "c2", "OpenAlert('恭喜！修改成功！');", true);
                }
                else
                {
                    RadScriptManager.RegisterStartupScript(Page, GetType(), "c3", "OpenAlert('抱歉！修改失败！');", true);
                }
            }

        }

        private void SetVisibles(bool vb)
        { 
            RadDropDownTree2.Visible = vb;
            btn_ChangeValid.Visible = !vb;
            btn_Checked.Visible = !vb;
            TB_Publisher.Enabled = !vb;
            RCB_Checked.Enabled = !vb;
            TB_CheckMemo.Enabled = !vb;
            TB_Checker.Enabled = !vb;
            RCB_Enable.Enabled = !vb;
            Title1.ReadOnly = !vb;
            contentText.ReadOnly = !vb;
            RadButton1.Visible = vb;
            if (vb)
            {
                contentEditor.EditModes = EditModes.All;
                contentEditor.EditModes = contentEditor.EditModes ^ EditModes.Preview;
            }
            else
            {
                contentEditor.EditModes = EditModes.Preview;
            }
            Btn_Modify.Visible = vb;
        }

        protected void btn_Checked_Click(object sender, EventArgs e)
        {
            Model.News newsModel = news.GetModel(autoID);
            newsModel.Checked = int.Parse(RCB_Checked.SelectedValue);
            newsModel.Checker = user.UserInfo.UserID;
            newsModel.CheckMemo = TB_CheckMemo.Text;
            bool isUpdate = false;
            Model.Log logModel = new Model.Log();
            try
            {
                logModel.OperationItem = "审核条目";
                logModel.Operator = user.UserInfo.UserID;
                logModel.OperationTime = DateTime.Now;
                logModel.OperationDetail = "条目编号：" + newsModel.AutoID + " - 审核状态：" + newsModel.Checked  + " - 审核备注：" + newsModel.CheckMemo;
                isUpdate = news.Update(newsModel);
                if (isUpdate)
                {
                    logModel.Memo = "成功";
                }
                else
                {
                    logModel.Memo = "失败！";
                }
            }
            catch (Exception ex)
            {
                logModel.Memo += "异常：" + ex.Message;
            }
            finally
            {
                log.Add(logModel);
                if (isUpdate)
                {
                    ReadValue(); 
                    RadScriptManager.RegisterStartupScript(Page, GetType(), "c2", "OpenAlert('恭喜！状态修改成功！');", true);
                }
                else
                {
                    RadScriptManager.RegisterStartupScript(Page, GetType(), "c3", "OpenAlert('抱歉！状态修改失败！');", true);
                }
            }
        }

        public static string CheckStr(string html)
        { 
            try
            {
                //过滤style
                // Regex reg = new Regex(@"\sstyle=""(?<style>([^"";]+;?)+)""", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                //过滤class
                Regex reg2 = new Regex(@"\sclass=""(?<class>([^"";]+;?)+)""", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                //过滤color
                Regex reg3 = new Regex(@"\scolor=""(?<color>([^"";]+;?)+)""", RegexOptions.IgnoreCase | RegexOptions.Compiled);
               //过滤width
                Regex reg4 = new Regex(@"\swidth=""(?<width>([^"";]+;?)+)""", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                Regex reg4x = new Regex(@"width=\d*", RegexOptions.IgnoreCase | RegexOptions.Compiled); 
                //过滤height
                Regex reg5 = new Regex(@"\sheight=""(?<height>([^"";]+;?)+)""", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                Regex reg5x = new Regex(@"height=\d*", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                Regex regfont = new Regex(@"<\/?(?:font)[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                Regex rega = new Regex(@"<a.*?>|</a>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                //html = reg.Replace(html, "");
                html = reg2.Replace(html, "");
                html = reg3.Replace(html, "");
                html = reg4.Replace(html, "");
                html = reg5.Replace(html, "");
                html = reg4x.Replace(html, "");
                html = reg5x.Replace(html, "");
                html = regfont.Replace(html, "");
                html = rega.Replace(html, "");
                //Regex reg0 = new Regex(@"\sstyle=(""')(?<style>([^""';]+;?)+)""", RegexOptions.IgnoreCase);
                Regex reg0 = new Regex(@"style\s*=("")[^""]*("")", RegexOptions.IgnoreCase);  //双引号的style
                Regex reg1 = new Regex(@"style\s*=(')[^']*(')", RegexOptions.IgnoreCase);     //单引号的 style
                MatchCollection matches = reg0.Matches(html); 
                if (matches.Count > 0)
                {
                    foreach (Match mc in matches)
                    {
                        if (mc.Value.ToLower().Contains("text-align"))
                        {
                            Regex rgx = new Regex(@"[^""]*(text-align:[^;]*)[^""]*", RegexOptions.IgnoreCase);
                            string str = rgx.Replace(mc.Value, "$1");
                            html = html.Replace(mc.Value, str);
                        }
                        else
                        {
                            html = html.Replace(mc.Value, "");
                        }
                    }
                }
                MatchCollection matches2 = reg1.Matches(html);
                if (matches2.Count > 0)
                {
                    foreach (Match mc in matches2)
                    {
                        if (mc.Value.ToLower().Contains("text-align"))
                        {
                            Regex rgx = new Regex(@"[^']*(text-align:[^;]*)[^']*", RegexOptions.IgnoreCase);
                            string str = rgx.Replace(mc.Value, "$1");
                            html = html.Replace(mc.Value, str);
                        }
                        else
                        {
                            html = html.Replace(mc.Value, "");
                        }
                    }
                }

            }
            catch
            { }
            return html;
        }

        protected void saveEditBtn_Click(object sender, EventArgs e)
        {

            contentEditor.EditModes = EditModes.All;
            contentEditor.EditModes = contentEditor.EditModes ^ EditModes.Preview;
            saveEditBtn.Visible = false;
            RadButton1.Checked = false;
        }

        protected void RadButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (RadButton1.Checked)
            {

                ViewState["tempContent"] = contentEditor.Content;
                string tempStr = CheckStr(contentEditor.Content);
                contentEditor.Content = tempStr;
                contentEditor.EditModes = EditModes.Preview; 
                saveEditBtn.Visible = true;
            }
            else
            {
                contentEditor.Content = ViewState["tempContent"].ToString();
                contentEditor.EditModes = EditModes.All;
                contentEditor.EditModes = contentEditor.EditModes ^ EditModes.Preview;
                RadButton1.Checked = false;
                saveEditBtn.Visible = false;
            }
        }

        public class ChangeImageSizeProvider : FileSystemContentProvider
        {

            public ChangeImageSizeProvider(HttpContext context, string[] searchPatterns, string[] viewPaths, string[] uploadPaths, string[] deletePaths, string selectedUrl, string selectedItemTag)
                : base(context, searchPatterns, viewPaths, uploadPaths, deletePaths, selectedUrl, selectedItemTag)
            {
            }

            public override string StoreFile(UploadedFile file, string path, string name, params string[] arguments)
            {
                Image image = Image.FromStream(file.InputStream);
                string physicalPath = Context.Server.MapPath(path);
                int newWidth = image.Width;
                int newHeight = image.Height;
                if (newWidth > img_width)
                {
                    newWidth = img_width;// Fixed width 
                }
                if (newHeight > img_height)
                {
                    newHeight = img_height;// Fixed height
                }
                Bitmap resultImage = ResizeImage(image, new Size(newWidth, newHeight));
                KiSaveAsJPEG(resultImage, physicalPath + name, 80); 
                string result = path + name;
                return result;
            } 
        }
        private static bool KiSaveAsJPEG(Bitmap bmp, string FileName, int Qty)
        {
            try
            {
                EncoderParameter p;
                EncoderParameters ps;
                ps = new EncoderParameters(1);
                p = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, Qty);
                ps.Param[0] = p;
                bmp.Save(FileName, GetCodecInfo("image/jpeg"), ps);
                return true;
            }
            catch
            {
                return false;
            }

        }

        private static ImageCodecInfo GetCodecInfo(string mimeType)
        {
            ImageCodecInfo[] CodecInfo = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo ici in CodecInfo)
            {
                if (ici.MimeType == mimeType) return ici;
            }
            return null;
        }


        private static Bitmap ResizeImage(Image sourceImage, Size newSize)
        {
            double ratio = 0d;
            double myThumbWidth = 0d;
            double myThumbHeight = 0d;
            Bitmap bp;
            if ((sourceImage.Width / Convert.ToDouble(newSize.Width)) > (sourceImage.Height / Convert.ToDouble(newSize.Height)))
            {
                ratio = Convert.ToDouble(sourceImage.Width) / Convert.ToDouble(newSize.Width);
            }
            else
            {
                ratio = Convert.ToDouble(sourceImage.Height) / Convert.ToDouble(newSize.Height);
            }
            myThumbHeight = Math.Ceiling(sourceImage.Height / ratio);
            myThumbWidth = Math.Ceiling(sourceImage.Width / ratio);
            Size thumbSize = new Size((int)myThumbWidth, (int)myThumbHeight);
            bp = new Bitmap(thumbSize.Width, thumbSize.Height);
            //x = (newSize.Width - thumbSize.Width) / 2;
            //y = (newSize.Height - thumbSize.Height);
            System.Drawing.Graphics g = Graphics.FromImage(bp);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            Rectangle rect = new Rectangle(0, 0, thumbSize.Width, thumbSize.Height);
            g.Clear(System.Drawing.Color.Transparent);
            g.DrawImage(sourceImage, rect, 0, 0, sourceImage.Width, sourceImage.Height, GraphicsUnit.Pixel);
            return bp;
        }

        public bool imgHandle(string input)
        {
            string inputHandled = input;
            Regex regImgX = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
            Regex regImg = new Regex(@"<img[^>]*src=(['""]?)(?<img>[^'""\s>]*)\1[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Compiled);
            MatchCollection matches3 = regImg.Matches(input);
            if (matches3.Count > 0)
            {
                try
                {
                    string imgBasePath = Server.MapPath(scd.GetModel("SImagesPath").SYSCodeDetialContext); 
                    DirectoryInfo sDir = new DirectoryInfo(imgBasePath);
                    sDir.Attributes = FileAttributes.Normal & FileAttributes.Directory; //去除只读属性
                    FileInfo[] fileArray = sDir.GetFiles();
                    List<string> filenameList = new List<string>(); 
                    foreach (FileInfo file in fileArray)
                    {
                        filenameList.Add(file.Name);
                    }
                    foreach (Match mc in matches3)
                    {
                        string imgSrc = mc.Groups["img"].Value;
                        string url = Request.Url.AbsoluteUri.Remove(Request.Url.AbsoluteUri.ToLower().IndexOf("/admin"));
                        imgSrc = url + imgSrc;
                        string imgName = imgSrc.Substring(imgSrc.LastIndexOf("/") + 1); 
                        string[] imgPathForView = imgBasePath.Split('\\');
                        if (!filenameList.Contains("s" + HttpUtility.UrlDecode(imgName)))
                        { 
                            string newImgNames = "s" + HttpUtility.UrlDecode(imgName);
                            Bitmap bi = Get_img(imgSrc);
                            //缩略图
                            Size szs = new System.Drawing.Size(146, 146);
                            Bitmap newBis = ResizeImage(bi, szs);
                            KiSaveAsJPEG(newBis, imgBasePath + "\\" + newImgNames, 80);
                        }
                    }
                }
                catch (Exception ex)
                { 
                    return false;
                }
            }
            return true;
        }


        private Bitmap Get_img(string imgSrc)
        {
            Bitmap img = null;
            HttpWebRequest req;
            HttpWebResponse res = null;
            try
            {
                System.Uri httpUrl = new System.Uri(imgSrc);
                req = (HttpWebRequest)(WebRequest.Create(httpUrl));
                req.Timeout = 10000; //设置超时值10秒 
                req.Method = "GET";
                res = (HttpWebResponse)(req.GetResponse());
                img = new Bitmap(res.GetResponseStream());//获取图片流  
            }

            catch (Exception ex)
            {
                string aa = ex.Message;
            }
            finally
            {
                res.Close();
            }
            return img;
        }

        public bool contentEditor_FileDelete(object sender, string fileName)
        {
            try
            {
                string imgBasePath = Server.MapPath(scd.GetModel("SImagesPath").SYSCodeDetialContext);
                DirectoryInfo sDir = new DirectoryInfo(imgBasePath);
                sDir.Attributes = FileAttributes.Normal & FileAttributes.Directory; //去除只读属性
                FileInfo[] fileArray = sDir.GetFiles();
                foreach (FileInfo file in fileArray)
                {
                    if (file.Name.Contains(fileName.Substring(fileName.LastIndexOf("/") + 1)))
                    {
                        File.Delete(imgBasePath + @"\" + file.Name);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        } 
    }
}