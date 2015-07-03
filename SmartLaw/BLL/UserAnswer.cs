using System;
using System.Data;
using System.Collections.Generic;
using SmartLaw.Common;
using SmartLaw.Model;
namespace SmartLaw.BLL
{
    /// <summary>
    /// News
    /// </summary>
    public partial class UserAnswer
    {
        private readonly SmartLaw.DAL.MySqlUserAnswer dal = new SmartLaw.DAL.MySqlUserAnswer();
        private readonly SmartLaw.DAL.MySqlQuestion qsDal = new SmartLaw.DAL.MySqlQuestion();
        public UserAnswer()
        { }
        #region  BasicMethod


        /// <summary>
        /// 是否存在某用户对于某问卷的某个问题的答案
        /// </summary>
        public bool Exist(long _questionID, long _userID)
        {
            return dal.Exists(_questionID, _userID);
        } 

        /// <summary>
        /// 新增用户对于某问卷下某个问题的答案
        /// </summary>
        public long Add(SmartLaw.Model.UserAnswer model)
        {
            return dal.Add(model);
        } 

        /// <summary>
        /// 更新某个用户对于某个问题的答案
        /// </summary>
        public bool Update(SmartLaw.Model.UserAnswer model)
        {
            return dal.Update(model);
        } 

        /// <summary>
        /// 删除某个用户对于某个问题的答案
        /// </summary>
        public bool Delete(long _questionID, long _userID)
        {

            return dal.Delete(_questionID, _userID);
        }

        /// <summary>
        /// 批量删除某个问卷的回答记录
        /// </summary>
        public bool DeleteList(string[] QuestionIDlist)
        {
            return dal.DeleteList(QuestionIDlist);
        }


        /// <summary>
        /// 得到答案实体
        /// </summary>
        public SmartLaw.Model.UserAnswer GetModel(long _questionID, long _userID)
        {

            return dal.GetModel(_questionID, _userID);
        }


        /// <summary>
        /// 根据条件获取答案列表
        /// </summary>
        /// <param name="key">查询条件 0：QuestionID,1：UserID,2:Orders,3:Answer 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>答案集合</returns>
        public DataSet GetList(int key, string value)
        {
            return dal.GetList(key, value);
        }

        /// <summary>
        /// 根据条件获取答案列表
        /// </summary>
        /// <param name="key">查询条件   0：QuestionID,1：UserID,2:Orders,3::Answer  其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段  0：QuestionID,1：UserID,2:Orders,3:Answer    其他:全部其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>答案集合</returns>
        public DataSet GetList(int key, string value, int Top, int filedOrder, bool desc)
        {
            return dal.GetList(key, value, Top, filedOrder, desc);
        } 

        /// <summary>
        /// 根据条件获取答案列表(list)
        /// </summary>
        /// <param name="key">查询条件  0：QuestionID,1：UserID,2:Orders,3:Answer  其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段 0：QuestionID,1：UserID,2:Orders,3:Answer    其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>问卷集合</returns>
        public List<SmartLaw.Model.UserAnswer> GetQnModelList(int key, string value, int Top, int filedOrder, bool desc)
        {
            DataSet ds = dal.GetList(key, value, Top, filedOrder, desc);

            return DataTableToList(ds.Tables[0]);
        }

        /// <summary>
        ///获得所有用户对某个问题的答案
        /// </summary>
        public DataSet GetListForQuestion(long _questionID)
        {
            return dal.GetListForQuestion(_questionID);
        }

        /// <summary>
        ///获得所有用户对某个问题的答案
        /// </summary>
        public List<SmartLaw.Model.UserAnswer> GetOnModelListForQuestion(long _questionID)
        {
            DataSet ds = dal.GetListForQuestion(_questionID);
            return DataTableToList(ds.Tables[0]);
        } 

        /// <summary>
        /// 将问卷的DateTable转成实体类集合
        /// </summary>
        public List<SmartLaw.Model.UserAnswer> DataTableToList(DataTable dt)
        {
            List<SmartLaw.Model.UserAnswer> modelList = new List<SmartLaw.Model.UserAnswer>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SmartLaw.Model.UserAnswer model;
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

        /// <summary>
        /// 获得所有答案
        /// </summary>
        /// <returns>答案集合</returns>
        public DataSet GetAllList()
        {
            return GetList(-1, "");
        } 

        /// <summary>
        /// 根据条件获得答案条目数
        /// </summary>
        /// <param name="key">查询条件  0：QuestionID,1：QuestionID,2：orders,3:Answer其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>问题集合条目数</returns>
        public int GetRecordCount(int key, string value)
        {
            return dal.GetRecordCount(key, value);
        }


        /// <summary>
        /// 对某个问题的答案进行统计,答案存在model中名为answer的string数组中，若用户所选答案编号为0,1,2，则存储为{"0","1","2"};
        /// </summary>
        /// 
        ///
        public int[] CountAnswers(long _questionID)
        {
            List<int> answerCount = new List<int>();

            List<SmartLaw.Model.UserAnswer> modelList = GetOnModelListForQuestion(_questionID);
            SmartLaw.Model.Question qs = qsDal.GetModel(_questionID);

            int maxLength = qs.Answer.Length;

            for (int k = 0; k < maxLength; ++k)
            {
                answerCount.Add(0);
            }

            for (int i = 0; i < modelList.Count; ++i)
            {
                if (modelList[i].Answer != null)
                {
                    for (int j = 0; j < modelList[i].Answer.Length; ++j)
                    {
                        int selectedNo = int.Parse(modelList[i].Answer[j]);
                        answerCount[selectedNo]++;
                    }
                }
            }
            return answerCount.ToArray();
        }

        #endregion  BasicMethod
    }
}

