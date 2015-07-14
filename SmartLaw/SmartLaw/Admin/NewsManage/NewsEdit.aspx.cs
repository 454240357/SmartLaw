using System;
using System.Collections.Generic;
using System.Text;
using System.Web; 
using SmartLaw.BLL;
using System.Data;
using Telerik.Web.UI; 
using System.Text.RegularExpressions; 
using System.Drawing;
using System.Drawing.Drawing2D;
using Telerik.Web.UI.Widgets;
using Category = SmartLaw.Model.Category;
using SmartLaw.App_Code;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace SmartLaw.Admin.NewsManage
{
    public partial class NewsEdit : BasePage
    {  
        News news = new News(); 
        SysCodeDetail scd = new SysCodeDetail();
        BLL.Category cg = new BLL.Category();
        Log log = new Log();
        private SessionUser user; 
        public static  int img_width = 5000;
        public static  int img_height = 5000; 
        protected void Page_Load(object sender, EventArgs e)
        { 
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
            user.ValidateAuthority("Auth_News_Add");
            if (!user.hasAuthority("Auth_News_Retrieve"))
            {
                href1.Visible = false;
            } 
            if (!user.hasAuthority("Auth_News_Examine"))
            {
                A1.Visible = false;
            }
            contentEditor.EditModes = EditModes.All;
            contentEditor.EditModes = contentEditor.EditModes ^ EditModes.Preview;
            contentEditor.ImageManager.ContentProviderTypeName = typeof(ChangeImageSizeProvider).AssemblyQualifiedName; 
            if (!user.hasAuthority("Auth_Image_Upload"))
            {
                string[] a = { "" };
                contentEditor.ImageManager.UploadPaths = a;
                contentEditor.ImageManager.DeletePaths = a;
                contentEditor.ImageManager.ViewPaths = a;
            } 
            if (!IsPostBack)
            {
                List<Category> cgList = cg.DataTableToList(cg.GetList(5, "1", -1, 0, false).Tables[0]);
                cgList.RemoveAll(CT => !CT.Memo.Contains("RW"));
                DataTable table = new DataTable();
                table.Columns.Add("AutoID");
                table.Columns.Add("ParentCategoryID");
                table.Columns.Add("CategoryName");
                foreach (Category cgm in cgList)
                {
                    if (cgm.ParentCategoryID==-1)
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
            }  
        } 

        protected void rbtnSubmit_Click(object sender, EventArgs e)
        {  
            string TabV = TabStrip1.SelectedTab.Value;
            string titleString = "";
            string newsString = "";
            string newsString2 = "";
            string dataType = "";
            if (TabV == "1")
            {
                titleString = Title1.Text;
                newsString = contentText.Text; 
                dataType = "DataType_Text";
            }
            else if (TabV == "2")
            {
                titleString = Title2.Text;
                newsString = contentEditor.Content;
                if (newsString.Contains("src=\"data:image"))
                {
                    RadScriptManager.RegisterStartupScript(Page, GetType(), "c1", "OpenAlert('图片不能直接复制到编辑器，请使用工具栏的图片上传功能！');", true);
                    return;
                }
                newsString2 = CheckStr(newsString);
                dataType = "DataType_Html";
            }
            RadTreeView categoryTreeView = RadDropDownTree2.Controls[0] as RadTreeView;  
            if (titleString.Trim().Equals("") || newsString.Trim().Equals(""))
            {
                RadScriptManager.RegisterStartupScript(Page, GetType(), "c1", "OpenAlert('标题或内容不能为空！');", true);
                return;
            } 
            if (categoryTreeView.SelectedNode == null)
            {
                RadScriptManager.RegisterStartupScript(Page, GetType(), "c2", "OpenAlert('分类不能为空！');", true);
                return;
            }
            
            string cgid = categoryTreeView.SelectedNode.Value;
            Model.News newsModel = new Model.News();
            newsModel.CategoryID = long.Parse(cgid);
            newsModel.Checked = 0;
            newsModel.Checker = "";
            newsModel.CheckMemo = "";
            newsModel.Contents = RadButton1.Checked ? newsString2 : newsString;//newsString;
            newsModel.DataSource = "DataSource_Manual";
            newsModel.DataType = dataType;
            newsModel.IsValid = true;
            newsModel.LastModifyTime = DateTime.Now;
            newsModel.Publisher = user.UserInfo.UserID;
            newsModel.Title = titleString;
            long newsId = 0; 
            Model.Log logModel = new Model.Log();
            if (cgid.Equals("200"))
            {
                if (!imgHandle(newsModel.Contents))
                {
                    RadScriptManager.RegisterStartupScript(Page, GetType(), "c3", "OpenAlert('抱歉！添加条目失败！-缩略图生成失败');", true);
                    return;
                }
            }
            try
            {
                logModel.OperationItem = "添加条目";
                logModel.Operator = user.UserInfo.UserID;
                logModel.OperationTime = DateTime.Now;
                logModel.OperationDetail = "标题：" + titleString + " - 分类编号：" + cgid; 
                newsId = news.Add(newsModel); 
                if (newsId != 0)
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
                if (newsId != 0)
                { 
                    RadScriptManager.RegisterStartupScript(Page, GetType(), "c", "OpenAlert('恭喜！添加条目成功！');", true);
                }
                else
                {
                    RadScriptManager.RegisterStartupScript(Page, GetType(), "c3", "OpenAlert('抱歉！添加条目失败！');", true);
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
                Regex regfont = new Regex(@"<\/?(?:font)[^>]*>", RegexOptions.IgnoreCase| RegexOptions.Compiled);
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
                
                ViewState["tempContent"]  = contentEditor.Content;
                string tempStr = CheckStr(contentEditor.Content);
                contentEditor.Content = tempStr;
                contentEditor.EditModes = EditModes.Preview;
                saveEditBtn.Visible = true;
            }
            else
            {
                contentEditor.Content = ViewState["tempContent"] == null ? "" : ViewState["tempContent"].ToString();
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

        private static Bitmap ResizeImage(Image mg, Size newSize)
        {
            double ratio = 0d;
            double myThumbWidth = 0d;
            double myThumbHeight = 0d;
            Bitmap bp;
            if ((mg.Width / Convert.ToDouble(newSize.Width)) > (mg.Height / Convert.ToDouble(newSize.Height)))
            {
                ratio = Convert.ToDouble(mg.Width) / Convert.ToDouble(newSize.Width);
            }
            else
            {
                ratio = Convert.ToDouble(mg.Height) / Convert.ToDouble(newSize.Height);
            }
            myThumbHeight = Math.Ceiling(mg.Height / ratio);
            myThumbWidth = Math.Ceiling(mg.Width / ratio);
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
            g.DrawImage(mg, rect, 0, 0, mg.Width, mg.Height, GraphicsUnit.Pixel);
            return bp;
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
                    if (file.Name.Contains(fileName.Substring(fileName.LastIndexOf("/")+1)))
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