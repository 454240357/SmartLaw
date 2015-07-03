using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MongoDB;
using MongoDB.Configuration;
using System.Configuration;
using Interface.Entity.MongoDB;

namespace Interface
{
    /// <summary>
    /// MongoDBService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class MongoDBService : System.Web.Services.WebService
    {
        public string connectionString = ConfigurationManager.ConnectionStrings["mongodb"].ConnectionString;
        public string databaseName = "myDatabase";
        public string collectionName = "Enduser";

        //private Mongo mongo;
        //private MongoDatabase mongoDatabase;
        //private MongoCollection<Document> mongoCollection;



        //注意这里泛型类型为“Enduser”
        //private MongoCollection<Enduser> mongoCollection;


        //public MongoDBService()
        //{
        //    // mongo = GetMongo();
        //    mongo = new Mongo(connectionString);
        //    mongoDatabase = mongo.GetDatabase(databaseName) as MongoDatabase;
        //    mongoCollection = mongoDatabase.GetCollection<Document>(collectionName) as MongoCollection<Document>;
        //    mongo.Connect();
        //}

        //~MongoDBService()
        //{
        //    mongo.Disconnect();
        //}

        [WebMethod]
        public bool InsertEnduser(EndUser newUser, out string msg)
        {
            Mongo mongo = new Mongo(connectionString);
            MongoDatabase mongoDatabase = mongo.GetDatabase(databaseName) as MongoDatabase;
            MongoCollection<Document> mongoCollection = mongoDatabase.GetCollection<Document>(collectionName) as MongoCollection<Document>;

            try
            {
                mongo.Connect();
                IEnumerable<Document> doc = mongoCollection.Find(new Document { { "SimCardNo", newUser.SimCardNo } }).Documents;

                if (doc == null || doc.Count() == 0)
                {

                    Document enduser1 = new Document();
                    enduser1["AutoID"] = newUser.AutoID;
                    enduser1["EnduserName"] = newUser.EnduserName;
                    enduser1["SimCardNo"] = newUser.SimCardNo;
                    enduser1["Identities"] = getIdentitiesString(newUser.Identities);
                    enduser1["LastModifyTime"] = newUser.LastModifyTime;
                    enduser1["IsValid"] = newUser.IsValid;

                    mongoCollection.Save(enduser1);
                    msg = "插入成功";
                    mongo.Disconnect();
                    return true;
                }
                msg = "用户已存在";
                mongo.Disconnect();
                return false;

            }
            catch (Exception ex)
            {
                mongo.Disconnect();
                msg = ex.Message;
                return false;
            }
        }



        [WebMethod]
        public bool InsertEndusers(EndUser[] newUsers, out string msg)
        {
            Mongo mongo = new Mongo(connectionString);
            MongoDatabase mongoDatabase = mongo.GetDatabase(databaseName) as MongoDatabase;
            MongoCollection<Document> mongoCollection = mongoDatabase.GetCollection<Document>(collectionName) as MongoCollection<Document>;

            try
            {
                mongo.Connect();
                for (int i = 0; i < newUsers.Length; ++i)
                {
                    IEnumerable<Document> doc = mongoCollection.Find(new Document { { "SimCardNo", newUsers[i].SimCardNo } }).Documents;

                    if (doc == null || doc.Count() == 0)
                    {

                        Document enduser1 = new Document();
                        enduser1["AutoID"] = newUsers[i].AutoID;
                        enduser1["EnduserName"] = newUsers[i].EnduserName;
                        enduser1["SimCardNo"] = newUsers[i].SimCardNo;
                        enduser1["Identities"] = getIdentitiesString(newUsers[i].Identities);
                        enduser1["LastModifyTime"] = newUsers[i].LastModifyTime;
                        enduser1["IsValid"] = newUsers[i].IsValid;

                        mongoCollection.Save(enduser1);


                    }
                    else
                    {
                        msg = "用户已存在";
                        mongo.Disconnect();
                        return false;
                    }
                }
                msg = "插入成功";
                mongo.Disconnect();
                return true;

            }
            catch (Exception ex)
            {
                msg = ex.Message;
                mongo.Disconnect();
                return false;
            }
        }




        private string getIdentitiesString(string[] Identities)
        {
            string re = "";
            foreach (string i in Identities)
            {
                re += i + "|";
            }
            if (re.EndsWith("|"))
                re = re.Remove(re.Length - 1);
            return re;
        }

        /// 查询全部enduser
        /// </summary>
        [WebMethod]
        public EndUser[] SelectAll(out string msg)
        {
            Mongo mongo = new Mongo(connectionString);
            MongoDatabase mongoDatabase = mongo.GetDatabase(databaseName) as MongoDatabase;
            MongoCollection<Document> mongoCollection = mongoDatabase.GetCollection<Document>(collectionName) as MongoCollection<Document>;

            try
            {
                mongo.Connect();
                List<EndUser> userList = new List<EndUser>();
                IEnumerable<Document> doc = mongoCollection.FindAll().Documents;
                foreach (Document d in doc)
                {
                    EndUser user = new EndUser();
                    user.AutoID = d["AutoID"].ToString();
                    user.EnduserName = d["EnduserName"].ToString();
                    user.Identities = d["Identities"].ToString().Split('|');
                    user.IsValid = bool.Parse(d["IsValid"].ToString());
                    user.LastModifyTime = DateTime.Parse(d["LastModifyTime"].ToString());
                    user.SimCardNo = d["SimCardNo"].ToString();
                    userList.Add(user);
                }

                //Document[] docs=  doc.ToArray<Document>();
                msg = "查询成功";
                mongo.Disconnect();
                return userList.ToArray();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                mongo.Disconnect();
                return null;
            }

        }



        /// 查询全部enduser
        /// </summary>
        [WebMethod]
        public long CountAll(out string msg)
        {
            Mongo mongo = new Mongo(connectionString);
            MongoDatabase mongoDatabase = mongo.GetDatabase(databaseName) as MongoDatabase;
            MongoCollection<Document> mongoCollection = mongoDatabase.GetCollection<Document>(collectionName) as MongoCollection<Document>;

            try
            {
                mongo.Connect();
                List<EndUser> userList = new List<EndUser>();
                IEnumerable<Document> doc = mongoCollection.FindAll().Documents;
                foreach (Document d in doc)
                {
                    EndUser user = new EndUser();
                    user.AutoID = d["AutoID"].ToString();
                    user.EnduserName = d["EnduserName"].ToString();
                    user.Identities = d["Identities"].ToString().Split('|');
                    user.IsValid = bool.Parse(d["IsValid"].ToString());
                    user.LastModifyTime = DateTime.Parse(d["LastModifyTime"].ToString());
                    user.SimCardNo = d["SimCardNo"].ToString();
                    userList.Add(user);
                }

                //Document[] docs=  doc.ToArray<Document>();
                msg = "查询成功";
                mongo.Disconnect();
                return userList.Count;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                mongo.Disconnect();
                return 0;
            }

        }



        [WebMethod]
        /// 根据SimCardNo查询
        /// </summary>
        public EndUser SelectEnduserBySimCardNo(string iSimCardNo, out string msg)
        {
            Mongo mongo = new Mongo(connectionString);
            MongoDatabase mongoDatabase = mongo.GetDatabase(databaseName) as MongoDatabase;
            MongoCollection<Document> mongoCollection = mongoDatabase.GetCollection<Document>(collectionName) as MongoCollection<Document>;

            try
            {
                mongo.Connect();
                List<EndUser> userList = new List<EndUser>();
                IEnumerable<Document> doc = mongoCollection.Find(new Document { { "SimCardNo", iSimCardNo } }).Documents;
                foreach (Document d in doc)
                {
                    EndUser user = new EndUser();
                    user.AutoID = d["AutoID"].ToString();
                    user.EnduserName = d["EnduserName"].ToString();
                    user.Identities = d["Identities"].ToString().Split('|');
                    user.IsValid = bool.Parse(d["IsValid"].ToString());
                    user.LastModifyTime = DateTime.Parse(d["LastModifyTime"].ToString());
                    user.SimCardNo = d["SimCardNo"].ToString();
                    userList.Add(user);
                }
                msg = "查询成功";
                mongo.Disconnect();
                return (userList.ToArray())[0];
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                mongo.Disconnect();
                return null;
            }


        }


        [WebMethod]
        /// 根据AutoID查询
        /// </summary>
        public EndUser SelectEnduserByAutoID(string iAutoID, out string msg)
        {
            Mongo mongo = new Mongo(connectionString);
            MongoDatabase mongoDatabase = mongo.GetDatabase(databaseName) as MongoDatabase;
            MongoCollection<Document> mongoCollection = mongoDatabase.GetCollection<Document>(collectionName) as MongoCollection<Document>;

            try
            {
                mongo.Connect();
                List<EndUser> userList = new List<EndUser>();
                IEnumerable<Document> doc = mongoCollection.Find(new Document { { "AutoID", iAutoID } }).Documents;
                foreach (Document d in doc)
                {
                    EndUser user = new EndUser();
                    user.AutoID = d["AutoID"].ToString();
                    user.EnduserName = d["EnduserName"].ToString();
                    user.Identities = d["Identities"].ToString().Split('|');
                    user.IsValid = bool.Parse(d["IsValid"].ToString());
                    user.LastModifyTime = DateTime.Parse(d["LastModifyTime"].ToString());
                    user.SimCardNo = d["SimCardNo"].ToString();
                    userList.Add(user);
                }
                msg = "查询成功";
                mongo.Disconnect();
                return (userList.ToArray())[0];
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                mongo.Disconnect();
                return null;
            }
        }



        [WebMethod]
        /// 根据EnduserName模糊查询
        /// </summary>
        public EndUser[] SelectEnduserByName(string iEnduserName, out string msg)
        {
            Mongo mongo = new Mongo(connectionString);
            MongoDatabase mongoDatabase = mongo.GetDatabase(databaseName) as MongoDatabase;
            MongoCollection<Document> mongoCollection = mongoDatabase.GetCollection<Document>(collectionName) as MongoCollection<Document>;

            try
            {
                mongo.Connect();
                List<EndUser> userList = new List<EndUser>();
                MongoRegex reg = new MongoRegex(".*" + iEnduserName + ".*", MongoRegexOption.IgnoreCase);
                Document docEnduserName = new Document { { "name", reg } };
                IEnumerable<Document> doc = mongoCollection.Find(docEnduserName).Documents;
                foreach (Document d in doc)
                {
                    EndUser user = new EndUser();
                    user.AutoID = d["AutoID"].ToString();
                    user.EnduserName = d["EnduserName"].ToString();
                    user.Identities = d["Identities"].ToString().Split('|');
                    user.IsValid = bool.Parse(d["IsValid"].ToString());
                    user.LastModifyTime = DateTime.Parse(d["LastModifyTime"].ToString());
                    user.SimCardNo = d["SimCardNo"].ToString();
                    userList.Add(user);
                }
                msg = "查询成功";
                mongo.Disconnect();
                return userList.ToArray();
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                mongo.Disconnect();
                return null;
            }

        }




        [WebMethod]
        /// 删除所有enduser
        /// </summary>
        private bool DeleteAll()
        {
            Mongo mongo = new Mongo(connectionString);
            MongoDatabase mongoDatabase = mongo.GetDatabase(databaseName) as MongoDatabase;
            MongoCollection<Document> mongoCollection = mongoDatabase.GetCollection<Document>(collectionName) as MongoCollection<Document>;
            mongo.Connect();
            try
            {

                mongoCollection.Remove(new Document { });
                mongo.Disconnect();
                return true;
            }
            catch
            {
                mongo.Disconnect();
                return false;
            }


        }


        [WebMethod]
        /// 根据SimCardNo删除
        /// </summary>
        public bool DeleteEnduser(string iSimCardNo, out string msg)
        {
            Mongo mongo = new Mongo(connectionString);
            MongoDatabase mongoDatabase = mongo.GetDatabase(databaseName) as MongoDatabase;
            MongoCollection<Document> mongoCollection = mongoDatabase.GetCollection<Document>(collectionName) as MongoCollection<Document>;

            try
            {
                mongo.Connect();
                //List<EndUser> userList = new List<EndUser>();
                IEnumerable<Document> doc = mongoCollection.Find(new Document { { "SimCardNo", iSimCardNo } }).Documents;
                mongoCollection.Remove(new Document { { "SimCardNo", iSimCardNo } });
                //foreach (Document d in doc)
                //{
                //    EndUser user = new EndUser();
                //    user.AutoID = d["AutoID"].ToString();
                //    user.EnduserName = d["EnduserName"].ToString();
                //    user.Identities = d["Identities"].ToString().Split('|');
                //    user.IsValid = bool.Parse(d["IsValid"].ToString());
                //    user.LastModifyTime = DateTime.Parse(d["LastModifyTime"].ToString());
                //    user.SimCardNo = d["SimCardNo"].ToString();
                //    userList.Add(user);
                //}
                msg = "删除成功";
                mongo.Disconnect();
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                mongo.Disconnect();
                return false;
            }
        }





        // [WebMethod]
        /// 根据SimCardNo更新
        /// </summary>
        /* public IEnumerable<Document> UpdateEnduser( string iSimCardNo, string fieldName, string fieldValue)
         {
             try
             {


                 Document update = new Document { { fieldName, fieldValue } };
                 Document doc = new Document { { "SimCardNo", iSimCardNo} };
                 mongoCollection.Update(new Document("$set", update), doc);
                 IEnumerable<Document> doc2 = mongoCollection.Find(new Document { { "SimCardNo", iSimCardNo } }).Documents;
               
                 return doc2;
             }
             catch
             {
                 return null;
             }


         }*/


        // [WebMethod]
        /// 根据SimCardNo更新
        /// </summary>
        /*  public IEnumerable<Document> UpdateEnduser(string iSimCardNo, string fieldName, DateTime fieldValue)
          {
              try
              {


                  Document update = new Document { { fieldName, fieldValue } };
                  Document doc = new Document { { "SimCardNo", iSimCardNo } };
                  mongoCollection.Update(new Document("$set", update), doc);
                  IEnumerable<Document> doc2 = mongoCollection.Find(new Document { { "SimCardNo", iSimCardNo } }).Documents;

                  return doc2;
              }
              catch
              {
                  return null;
              }


          }*/


        // [WebMethod]
        /// 根据SimCardNo更新
        /// </summary>
        /*  public IEnumerable<Document> UpdateEnduser(string iSimCardNo, string fieldName, Int64 fieldValue)
           {
               try
               {


                   Document update = new Document { { fieldName, fieldValue } };
                   Document doc = new Document { { "SimCardNo", iSimCardNo } };
                   mongoCollection.Update(new Document("$set", update), doc);
                   IEnumerable<Document> doc2 = mongoCollection.Find(new Document { { "SimCardNo", iSimCardNo } }).Documents;

                   return doc2;
               }
               catch
               {
                   return null;
               }


           }*/


        [WebMethod]
        /// 根据SimCardNo更新
        /// </summary>
        public bool UpdateEnduser(EndUser newUser, out string msg)
        {
            Mongo mongo = new Mongo(connectionString);
            MongoDatabase mongoDatabase = mongo.GetDatabase(databaseName) as MongoDatabase;
            MongoCollection<Document> mongoCollection = mongoDatabase.GetCollection<Document>(collectionName) as MongoCollection<Document>;

            try
            {
                mongo.Connect();
                List<EndUser> userList = new List<EndUser>();

                Document enduser1 = new Document();
                enduser1["AutoID"] = newUser.AutoID;
                enduser1["EnduserName"] = newUser.EnduserName;
                enduser1["SimCardNo"] = newUser.SimCardNo;
                enduser1["Identities"] = getIdentitiesString(newUser.Identities);
                enduser1["LastModifyTime"] = newUser.LastModifyTime;
                enduser1["IsValid"] = newUser.IsValid;

                Document doc = new Document { { "SimCardNo", newUser.SimCardNo } };
                if (doc == null)
                {
                    msg = "不存在该SIM卡号";
                    mongo.Disconnect();
                    return false;
                }
                mongoCollection.Update(new Document("$set", enduser1), doc);
                //IEnumerable<Document> doc2 = mongoCollection.Find(new Document { { "SimCardNo", newUser.SimCardNo } }).Documents;
                //foreach (Document d in doc2)
                //{
                //    EndUser user = new EndUser();
                //    user.AutoID = d["AutoID"].ToString();
                //    user.EnduserName = d["EnduserName"].ToString();
                //    user.Identities = d["Identities"].ToString().Split('|');
                //    user.IsValid = bool.Parse(d["IsValid"].ToString());
                //    user.LastModifyTime = DateTime.Parse(d["LastModifyTime"].ToString());
                //    user.SimCardNo = d["SimCardNo"].ToString();
                //    userList.Add(user);
                //}
                msg = "更新成功";
                mongo.Disconnect();
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                mongo.Disconnect();
                return false;
            }
        }




        [WebMethod]
        /// 根据SimCardNo更新
        /// </summary>
        public bool UpdateEndusers(EndUser[] newUsers, out string msg)
        {
            Mongo mongo = new Mongo(connectionString);
            MongoDatabase mongoDatabase = mongo.GetDatabase(databaseName) as MongoDatabase;
            MongoCollection<Document> mongoCollection = mongoDatabase.GetCollection<Document>(collectionName) as MongoCollection<Document>;

            try
            {
                mongo.Connect();
                for (int i = 0; i < newUsers.Length; ++i)
                {
                    List<EndUser> userList = new List<EndUser>();

                    Document enduser1 = new Document();
                    enduser1["AutoID"] = newUsers[i].AutoID;
                    enduser1["EnduserName"] = newUsers[i].EnduserName;
                    enduser1["SimCardNo"] = newUsers[i].SimCardNo;
                    enduser1["Identities"] = getIdentitiesString(newUsers[i].Identities);
                    enduser1["LastModifyTime"] = newUsers[i].LastModifyTime;
                    enduser1["IsValid"] = newUsers[i].IsValid;

                    Document doc = new Document { { "SimCardNo", newUsers[i].SimCardNo } };
                    if (doc == null)
                    {
                        msg = "不存在该SIM卡号:" + newUsers[i].SimCardNo.ToString();
                        mongo.Disconnect();
                        return false;
                    }
                    mongoCollection.Update(new Document("$set", enduser1), doc);
                }
                msg = "更新成功";
                mongo.Disconnect();
                return true;
            }
            catch (Exception ex)
            {
                msg = ex.Message;
                mongo.Disconnect();
                return false;
            }
        }







        /* [WebMethod]
         /// 根据SimCardNo更新
         /// </summary>
         public IEnumerable<Document> UpdateEnduser(Int64 iAutoID, string iEnduserName, string iSimCardNo, string iIdentities, DateTime iLastModifyTime, string iIsValid)
         {
             try
             {

                 Document enduser1 = new Document();
                 enduser1["AutoID"] = iAutoID;
                 enduser1["EnduserName"] = iEnduserName;
                 enduser1["SimCardNo"] = iSimCardNo;
                 enduser1["Identities"] = iIdentities;
                 enduser1["LastModifyTime"] = iLastModifyTime;
                 enduser1["IsValid"] = iIsValid;

                 Document doc = new Document { { "SimCardNo", iSimCardNo } };
                 mongoCollection.Update(new Document("$set", enduser1), doc);
                 IEnumerable<Document> doc2 = mongoCollection.Find(new Document { { "SimCardNo", iSimCardNo } }).Documents;

                 return doc2;
             }
             catch
             {
                 return null;
             }


         }*/

    }
}
