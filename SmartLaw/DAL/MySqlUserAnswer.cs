using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using SmartLaw.DBUtility;
using MySql.Data.MySqlClient;//Please add references
namespace SmartLaw.DAL
{
    /// <summary>
    /// 数据访问类:UserAnswer
    /// 
    /// </summary>
    public partial class MySqlUserAnswer
    {
        public MySqlUserAnswer()
        { }
        #region  BasicMethod

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(long QuestionID, long UserID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserAnswer");
            strSql.Append(" where QuestionID=@QuestionID and UserID=@UserID");
            MySqlParameter[] parameters = {
					new MySqlParameter("@QuestionID", MySqlDbType.Int64),
                    new MySqlParameter("@UserID", MySqlDbType.Int64)

			};
            parameters[0].Value = QuestionID;
         
            parameters[1].Value = UserID;


            return DbHelperMySQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(SmartLaw.Model.UserAnswer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into UserAnswer(");
            strSql.Append("QuestionID,UserID,Orders,Answer)");
            strSql.Append(" values (");
            strSql.Append("@QuestionID,@UserID,@Orders,@Answer)");
            strSql.Append(";select @@IDENTITY");
            MySqlParameter[] parameters = {
					
					new MySqlParameter("@QuestionID", MySqlDbType.Int64),
					new MySqlParameter("@UserID", MySqlDbType.Int64),
                    new MySqlParameter("@Orders", MySqlDbType.Int64),
					
					new MySqlParameter("@Answer", MySqlDbType.VarChar,64)};
            
          
            parameters[0].Value = model.QuestionID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.Orders;
            
            parameters[3].Value = getAnswersString(model.Answer);

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
            if (Answers.Length != 0)
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
            return null;
        }




        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(SmartLaw.Model.UserAnswer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update UserAnswer set ");
            
           
            strSql.Append("Answer=@Answer,Orders=@Orders");
            strSql.Append(" where QuestionID=@QuestionID and UserID=@UserID");
            MySqlParameter[] parameters = {
					new MySqlParameter("@QuestionID", MySqlDbType.Int64),
					new MySqlParameter("@UserID", MySqlDbType.Int64),
                    new MySqlParameter("@Orders", MySqlDbType.Int64),
					
					new MySqlParameter("@Answer", MySqlDbType.VarChar,64)};

            parameters[0].Value = model.QuestionID;
            parameters[1].Value = model.UserID;
            parameters[2].Value = model.Orders;
            
            parameters[3].Value = getAnswersString(model.Answer);

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
        public bool Delete(long QuestionID, long UserID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UserAnswer ");
            strSql.Append(" where QuestionID=@QuestionID and UserID=@UserID ");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@QuestionID", MySqlDbType.Int64),
                    new MySqlParameter("@UserID", MySqlDbType.Int64),
			}; 
            parameters[0].Value = QuestionID;
            parameters[1].Value = UserID;

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
        /// 批量删除数据,返回成功删除的条目数
        /// </summary>
        public bool DeleteList(string[] QuestionIDlist)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder _AutoIDlist = new StringBuilder();
            int i = 0;
            while (i < QuestionIDlist.Length)
            {
                _AutoIDlist.Append("'" + QuestionIDlist[i] + "'").Append(",");
                i++;
            }
            _AutoIDlist.Remove(_AutoIDlist.Length - 1, 1);
            strSql.Append("delete from UserAnswer ");
            strSql.Append(" where QuestionID in (" + _AutoIDlist + ")  ");
            int rows = DbHelperMySQL.ExecuteSql(strSql.ToString());
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
        public SmartLaw.Model.UserAnswer GetModel(long QuestionID, long UserID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select QuestionID,UserID,Orders,Answer from UserAnswer");
            strSql.Append(" where QuestionID=@QuestionID and UserID=@UserID limit 1");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@QuestionID", MySqlDbType.Int64),
                    new MySqlParameter("@UserID", MySqlDbType.Int64),
			};
            parameters[0].Value = QuestionID;
            parameters[1].Value = UserID;
           


            SmartLaw.Model.UserAnswer model = new SmartLaw.Model.UserAnswer();
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
        public SmartLaw.Model.UserAnswer DataRowToModel(DataRow row)
        {
            SmartLaw.Model.UserAnswer model = new SmartLaw.Model.UserAnswer();

            if (row != null)
            {

                if (row["QuestionID"] != null)
                {
                    model.QuestionID = long.Parse(row["QuestionID"].ToString());
                }


                if (row["UserID"] != null)
                {
                    model.UserID = long.Parse( row["UserID"].ToString());
                }


                if (row["Orders"] != null )
                {
                    model.Orders = 0;
                }

                if (row["Answer"] != null && row["Answer"].ToString() != "")
                {
                    model.Answer = row["Answer"].ToString().Split('|');

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
            strSql.Append("select ID,UserID,Orders,Answer");
            strSql.Append(" FROM UserAnswer ");
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
            strSql.Append("select QuestionID,UserID,Orders,Answer");
            strSql.Append(" FROM UserAnswer ");
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
            strSql.Append(" QuestionID,UserID,Orders,Answer");
            strSql.Append(" FROM UserAnswer ");
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
        /// <param name="key">查询条件 0：QuestionID,1：UserID,2:Orders,3:Answer 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value)
        {
            return GetList(key, value, -1, -1, false);
        }

        /// <summary>
        ///获得对于某个问题的所有用户答案
        /// </summary>
        public DataSet GetListForQuestion(long _questionID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append("QuestionID,UserID,Orders,Answer");
            strSql.Append(" FROM UserAnswer ");
            strSql.Append(" WHERE QuestionID=@QuestionID");
            MySqlParameter[] parameters = {			
					new MySqlParameter("@QuestionID", MySqlDbType.Int64)};
            parameters[0].Value = _questionID;
            return DbHelperMySQL.Query(strSql.ToString(), parameters);

        }


        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件  0：QuestionID,1：UserID,2:Orders,3:Answer 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段  0：QuestionID,1：UserID,2:Orders,3:Answer  其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>数据集</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append("QuestionID,UserID,Orders,Answer");
            strSql.Append(" FROM UserAnswer ");

            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {
                case 0: strWhere = "QuestionID = @QuestionID";
                    parameter = new MySqlParameter("@QuestionID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;

                case 1: strWhere = "UserID = @UserID";
                    parameter = new MySqlParameter("@UserID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;


                case 2: strWhere = "Orders = @Orders";
                    parameter = new MySqlParameter("@Orders", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;


                case 3: strWhere = "Answer like @Answer";
                    parameter = new MySqlParameter("@Answer", MySqlDbType.VarChar);
                    parameter.Value = "%" + value + "%";
                    break;

                default: break;
            }
            if (strWhere.Trim() != "")
                strSql.Append(" where " + strWhere);

            string strOrder = "";
            switch (filedOrder)
            {
                case 0: strOrder = "QuestionID"; break;
                case 1: strOrder = "UserID"; break;
                case 2: strOrder = "Orders"; break;
                
                case 3: strOrder = "Answer"; break;

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
            strSql.Append("select count(1) FROM UserAnswer ");
            string strWhere = "";
            MySqlParameter parameter = null;
            switch (key)
            {

                case 0: strWhere = "QuestionID = @QuestionID";
                    parameter = new MySqlParameter("@QuestionID", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;

                case 1: strWhere = "UserID = @UserID";
                    parameter = new MySqlParameter("@UserID", MySqlDbType.Int64);
                    parameter.Value =  value ;
                    break;


                case 2: strWhere = "Orders = @Orders";
                    parameter = new MySqlParameter("@Orders", MySqlDbType.Int64);
                    parameter.Value = value;
                    break;


                case 3: strWhere = "Answer like @Answer";
                    parameter = new MySqlParameter("@Answer", MySqlDbType.VarChar);
                    parameter.Value = "%" + value + "%";
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

