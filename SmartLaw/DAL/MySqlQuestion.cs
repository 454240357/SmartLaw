using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SmartLaw.DBUtility;
using MySql.Data.MySqlClient;//Please add references
namespace SmartLaw.DAL
{
    /// <summary>
    /// 数据访问类:Question
    /// </summary>
    public partial class MySqlQuestion
    {
        public MySqlQuestion()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long ID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Question");
            strSql.Append(" where ID=@ID");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int64)    
			};
            parameters[0].Value = ID;
          

            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(SmartLaw.Model.Question model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Question(");
            strSql.Append("QuestionaryID,Content,Answer,Orders,IsSingle)");
            strSql.Append(" values (");
            strSql.Append("@QuestionaryID,@Content,@Answer,@Orders,@IsSingle)");
            strSql.Append(";select @@IDENTITY");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@QuestionaryID", MySqlDbType.VarChar,64),
					new MySqlParameter("@Content", MySqlDbType.VarChar,64),
					new MySqlParameter("@Answer", MySqlDbType.VarChar,256),
                    new MySqlParameter("@Orders", MySqlDbType.Int32),
					new MySqlParameter("@IsSingle", MySqlDbType.Bit)};

            parameters[0].Value = model.QuestionaryID;
            parameters[1].Value = model.Content;
            parameters[2].Value = getAnswersString(model.Answer);
            parameters[3].Value = model.Orders;
            parameters[4].Value = model.IsSingle;


            object obj = DbHelperMySQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }




        private string getAnswersString(string[] Answers)
        {
            string re = "";
            foreach (string i in Answers)
            {
                re += i + "|";
            }
            if (re.EndsWith("|"))
                re = re.Remove(re.Length - 1);
            return re;
        }




        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SmartLaw.Model.Question model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Question set ");
           // strSql.Append("QuestionaryID=@QuestionaryID,");
            strSql.Append("Content=@Content,");
            strSql.Append("Answer=@Answer,");
            strSql.Append("Orders=@Orders,");
            strSql.Append("IsSingle=@IsSingle ");
            strSql.Append(" where ID=@ID");
            MySqlParameter[] parameters = {
					new MySqlParameter("@Content", MySqlDbType.VarChar,64),
					new MySqlParameter("@Answer", MySqlDbType.VarChar,256),
					new MySqlParameter("@ID", MySqlDbType.Int64),
                    new MySqlParameter("@Orders", MySqlDbType.Int32),
					new MySqlParameter("@IsSingle", MySqlDbType.Bit)};

            parameters[0].Value = model.Content;
            parameters[1].Value = getAnswersString(model.Answer);
            parameters[2].Value = model.ID;
            parameters[3].Value = model.Orders;
            parameters[4].Value = model.IsSingle;
            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(long ID )
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Question ");
            strSql.Append(" where ID=@ID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int64),
			};
            parameters[0].Value = ID;
         
            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除某个问卷中所有问题
        /// </summary>
        public bool DeleteAll(long QuestionaryID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from Question ");
            strSql.Append(" where QuestionaryID=@QuestionaryID ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@QuestionaryID", MySqlDbType.Int64),
			};
            
            parameters[0].Value = QuestionaryID;

            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SmartLaw.Model.Question GetModel(long ID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,QuestionaryID,Content,Answer,Orders,IsSingle from Question");
            strSql.Append(" where ID=@ID limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@ID", MySqlDbType.Int64)
			};
            parameters[0].Value = ID;
          

            SmartLaw.Model.Question model = new SmartLaw.Model.Question();
            DataSet ds = DbHelperMySQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);

            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public SmartLaw.Model.Question DataRowToModel(DataRow row)
        {
            SmartLaw.Model.Question model = new SmartLaw.Model.Question();

            if (row != null)
            {
                if (row["ID"] != null && row["ID"].ToString() != "")
                {
                    model.ID = long.Parse(row["ID"].ToString());
                }

                if (row["QuestionaryID"] != null && row["QuestionaryID"].ToString() != "")
                {
                    model.QuestionaryID = long.Parse(row["QuestionaryID"].ToString());
                }
               
                
                if (row["Content"] != null)
                {
                    model.Content = row["Content"].ToString();
                }

                if (row["Answer"] != null)
                {
                    model.Answer = row["Answer"].ToString().Split('|');

                }

                if (row["Orders"] != null && row["Orders"].ToString() != "")
                {
                    model.Orders = int.Parse(row["Orders"].ToString());
                }


                if (row["IsSingle"] != null && row["IsSingle"].ToString() != "")
                {
                    if ((row["IsSingle"].ToString() == "1") || (row["IsSingle"].ToString().ToLower() == "true"))
                    {
                        model.IsSingle = true;
                    }
                    else
                    {
                        model.IsSingle = false;
                    }
                }
           }
               return model;
        }           
        

        /// <summary>
        /// 获得数据列表
        /// </summary>
        private DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,QuestionaryID,Content,Answer,Orders,IsSingle");
            strSql.Append(" FROM Question ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, params MySqlParameter[] cmdParms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,QuestionaryID,Content,Answer,Orders,IsSingle");
            strSql.Append(" FROM Question ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySQL.Query(strSql.ToString(), cmdParms);
        }


        /// <summary>
        /// 获得前几行数据
        /// </summary>
        private DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append("ID,QuestionaryID,Content,Answer,Orders,IsSingle");
            strSql.Append(" FROM Question ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            if (Top > 0)
            {
                strSql.Append(" limit " + Top.ToString());
            }
            return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：ID, 1：QuestionaryID,2：Content,3:Answer,4:Orders,5:IsSingle 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value)
        {
            return GetList(key, value, -1, -1, false);
        }

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件0：ID, 1：QuestionaryID,2：Content,3:Answer,4:Orders,5:IsSingle 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段0：ID, 1：QuestionaryID,2：Content,3:Answer,4:Orders,5:IsSingle其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append("ID,QuestionaryID,Content,Answer,Orders,IsSingle");
            strSql.Append(" FROM Question ");

            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "ID = @ID";
                    parameter = new MySqlParameter("@ID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;
               
                case 1: strWhere = "QuestionaryID = @QuestionaryID";
                    parameter = new MySqlParameter("@QuestionaryID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;

                case 2: strWhere = "Content like @Content";
                    parameter = new MySqlParameter("@Content", MySqlDbType.VarChar);
                    parameter.Value = "%" + value + "%";
                    break;

                case 3: strWhere = "Answer like @Answer";
                    parameter = new MySqlParameter("@Answer", MySqlDbType.VarChar);
                    parameter.Value = "%" + value + "%";
                    break;

                case 4: strWhere = "Orders = @Orders";
                    parameter = new MySqlParameter("@Orders", MySqlDbType.Int32);
                    parameter.Value = value;
                    break;

                case 5: strWhere = "IsSingle = @IsSingle";
                    parameter = new MySqlParameter("@IsSingle", MySqlDbType.Bit);
                    parameter.Value = value;
                    break;


                default: break;
            }
            if (strWhere.Trim() != "")
                strSql.Append(" where " + strWhere);

            string strOrder = "";
            switch (filedOrder)
            {
                case 0: strOrder = "ID"; break;
                case 1: strOrder = "QuestionaryID"; break;
                case 2: strOrder = "Content"; break;
                case 3: strOrder = "Answer"; break;
                case 4: strOrder = "Orders"; break;
                case 5: strOrder = "IsSingle"; break;
                
                default: break;
            }
            if (strOrder.Trim() != "")
            {
                strSql.Append(" order by " + strOrder);
                if (desc)
                    strSql.Append(" desc");
            }
            if (Top > 0)
            {
                strSql.Append(" limit " + Top.ToString());
            }
            if (parameter != null)
                return DbHelperMySQL.Query(strSql.ToString(), parameter);
            else
                return DbHelperMySQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(int key, string value)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Question ");
            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "ID = @ID";
                    parameter = new MySqlParameter("@ID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;

                case 1: strWhere = "QuestionaryID = @QuestionaryID";
                    parameter = new MySqlParameter("@QuestionaryID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;

                case 2: strWhere = "Content like @Content";
                    parameter = new MySqlParameter("@Content", MySqlDbType.VarChar);
                    parameter.Value = "%" + value + "%";
                    break;

                case 3: strWhere = "Answer like @Answer";
                    parameter = new MySqlParameter("@Answer", MySqlDbType.VarChar);
                    parameter.Value = "%" + value + "%";
                    break;

                case 4: strWhere = "Orders = @Orders";
                    parameter = new MySqlParameter("@Orders", MySqlDbType.Int32);
                    parameter.Value = value;
                    break;

                case 5: strWhere = "IsSingle = @IsSingle";
                    parameter = new MySqlParameter("@IsSingle", MySqlDbType.Bit);
                    parameter.Value = value;
                    break;
                default: break;
            }

            object obj = null;
            if (strWhere.Trim() != "")
                strSql.Append(" where " + strWhere);
            if (parameter != null)
                obj = DbHelperMySQL.GetSingle(strSql.ToString(), parameter);
            else
                obj = DbHelperMySQL.GetSingle(strSql.ToString());
            if (obj == null)
                return 0;
            else
                return Convert.ToInt32(obj);
        }  

        #endregion  BasicMethod 
    }
}

