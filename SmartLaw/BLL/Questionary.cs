using System;
using System.Data;
using System.Collections.Generic;
using SmartLaw.Common;
using SmartLaw.Model;
namespace SmartLaw.BLL
{
    /// <summary>
    /// Questionary
    /// 
    /// </summary>
    public partial class Questionary
    {
        private readonly SmartLaw.DAL.MySqlQuestionary qnDal = new SmartLaw.DAL.MySqlQuestionary();
        private readonly SmartLaw.DAL.MySqlQuestion qsDal = new SmartLaw.DAL.MySqlQuestion();
        public Questionary()
        { }
        #region  BasicMethod 

        /// <summary>
        /// 是否存在该问卷
        /// </summary>
        public bool ExistQuestionary(long _id)
        {
            return qnDal.Exists(_id);
        }

        /// <summary>
        /// 指定问卷是否存在该问题
        /// </summary>
        public bool ExistQuestion(long _id)
        {
            return qsDal.Exists(_id);
        } 

        /// <summary>
        /// 新增不带问题的问卷
        /// </summary>
        public long AddEmptyQuestionary(SmartLaw.Model.Questionary model)
        {
            return qnDal.Add(model);
        }

        /// <summary>
        /// 为已存在的问卷新增问题
        /// </summary>
        public long AddQuestion(long _questionaryID,  SmartLaw.Model.Question model)
        {
            model.QuestionaryID = _questionaryID;
            return qsDal.Add(model);
        }
         


        /// <summary>
        /// 新增问卷及其包含的问题
        /// </summary>
        public long AddQuestionary(SmartLaw.Model.Questionary _qnModel,SmartLaw.Model.Question[] _qsModels )
        {
       
            long qnID = qnDal.Add(_qnModel);
            if (qnID >= 0)
            {
                for (int j = 0; j < _qsModels.Length; ++j)
                {
                    _qsModels[j].QuestionaryID = qnID;
                    qsDal.Add(_qsModels[j]);

                }
                return qnID;
            }
            else
            {
                return -1;
            }
            
        } 


        /// <summary>
        /// 更新问卷标题
        /// </summary>
        public bool UpdateQuestionary(SmartLaw.Model.Questionary model)
        {
            return qnDal.Update(model);
        }


        /// <summary>
        /// 更新某个问题
        /// </summary>
        public bool UpdateQuestion(SmartLaw.Model.Question model)
        {
            return qsDal.Update(model);
        } 

        /// <summary>
        /// 删除某个问题
        /// </summary>
        public bool DeleteQuestion(long _id )
        {

            return qsDal.Delete(_id);
        }

        /// <summary>
        /// 删除某个问卷
        /// </summary>
        public bool DeleteQuestionary(long _questionaryID)
        {

             qsDal.DeleteAll( _questionaryID);
             return qnDal.Delete(_questionaryID);
        }

        /// <summary>
        /// 删除某个问卷下所有问题
        /// </summary>
        public bool DeleteAllQuestionsInQn(long _questionaryID)
        {

            return qsDal.DeleteAll(_questionaryID);

        } 
 

        /// <summary>
        /// 得到问卷实体
        /// </summary>
        public SmartLaw.Model.Questionary GetQnModel(long _id)
        {

            return qnDal.GetModel(_id);
        }


        /// <summary>
        /// 得到问题实体
        /// </summary>
        public SmartLaw.Model.Question GetQsModel(long _id)
        {

            return qsDal.GetModel(_id);
        } 


        /// <summary>
        /// 根据条件获取问卷列表
        /// </summary>
        /// <param name="key">查询条件 0：ID, 1：Title, 2： IsValid 其他:全部 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>问卷集合</returns>
        public DataSet GetQuestionaryList(int key, string value)
        {
            return qnDal.GetList(key, value);
        }


        /// <summary>
        /// 根据条件获取问卷列表
        /// </summary>
        /// <param name="key">查询条件 0：ID, 1：Title, 2： IsValid 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>问卷集合</returns>
        public List<SmartLaw.Model.Questionary> GetQnModelList(int key, string value)
        {
           DataSet ds= qnDal.GetList(key, value);
            return DataTableToListForQn(ds.Tables[0]);
        }


        /// <summary>
        /// 根据条件获取问卷列表
        /// </summary>
        /// <param name="key">查询条件 0：ID, 1：Title, 2： IsValid 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段  0：ID, 1：Title, 2： IsValid 其他:全部其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>问卷集合</returns>
        public DataSet GetQuestionaryList(int key, string value, int Top, int filedOrder, bool desc)
        {
            return qnDal.GetList(key, value, Top, filedOrder, desc);
        }




        /// <summary>
        /// 根据条件获取问题列表
        /// </summary>
        /// <param name="key">查询条件 0：ID, 1：QuestionaryID,2：Content,3:Answer 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>问题集合</returns>
        public DataSet GetQuestionList(int key, string value)
        {
            return qsDal.GetList(key, value);
        }

        /// <summary>
        /// 根据条件获取问题列表
        /// </summary>
        /// <param name="key">查询条件 0：ID, 1：QuestionaryID,2：Content,3:Answer 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段  0：ID, 1：QuestionaryID,2：Content,3:Answer 其他:全部其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>问题集合</returns>
        public DataSet GetQuestionList(int key, string value, int Top, int filedOrder, bool desc)
        {
            return qsDal.GetList(key, value, Top, filedOrder, desc);
        }  


        /// <summary>
        /// 根据条件获取问卷列表(list)
        /// </summary>
        /// <param name="key">查询条件   0：ID, 1：Title,  2： IsValid 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段  0：ID, 1：Title, 2： IsValid 其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>问卷集合</returns>
        public List<SmartLaw.Model.Questionary> GetQnModelList(int key, string value, int Top, int filedOrder, bool desc)
        {
            DataSet ds = qnDal.GetList(key, value, Top, filedOrder, desc);
            
            return DataTableToListForQn(ds.Tables[0]);
        }


        /// <summary>
        /// 根据条件获取问题列表(list)
        /// </summary>
        /// <param name="key">查询条件   0：ID, 1：QuestionaryID,2：Content,3:Answer   其他:全部</param>
        /// <param name="value">查询值</param>
        /// <param name="Top">获取的条目数（小于0不限）</param>
        /// <param name="filedOrder">排序字段0：ID, 1：QuestionaryID,2：Content,3:Answer,4:Orders,5:IsSingle其他:不排序</param>
        /// <param name="desc">选用倒序</param>
        /// <returns>问题集合</returns>
        public List<SmartLaw.Model.Question> GetQsModelList(int key, string value, int Top, int filedOrder, bool desc)
        {
            DataSet ds = qsDal.GetList(key, value, Top, filedOrder, desc);

            return DataTableToListForQs(ds.Tables[0]);
        }

        /// <summary>
        /// 根据条件获取数据列表
        /// </summary>
        /// <param name="key">查询条件 0：ID, 1：QuestionaryID,2：Content,3:Answer,4:Orders,5:IsSingle 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>数据集</returns>
        public List<SmartLaw.Model.Question> GetQsModelList(int key, string value)
        {
            DataSet ds = qsDal.GetList(key, value);
            return DataTableToListForQs(ds.Tables[0]);
        }



        /// <summary>
        /// 将问卷的DateTable转成实体类集合
        /// </summary>
        public List<SmartLaw.Model.Questionary> DataTableToListForQn(DataTable dt)
        {
            List<SmartLaw.Model.Questionary> modelList = new List<SmartLaw.Model.Questionary>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SmartLaw.Model.Questionary model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = qnDal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }


        /// <summary>
        /// 将问题的DateTable转成实体类集合
        /// </summary>
        public List<SmartLaw.Model.Question> DataTableToListForQs(DataTable dt)
        {
            List<SmartLaw.Model.Question> modelList = new List<SmartLaw.Model.Question>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                SmartLaw.Model.Question model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = qsDal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        } 



        /// <summary>
        /// 获得所有问卷集合
        /// </summary>
        /// <returns>问卷集合</returns>
        public DataSet GetAllQuestionaryList()
        {
            return GetQuestionaryList(-1, "");
        } 


        /// <summary>
        /// 获得所有问题集合
        /// </summary>
        /// <returns>问题集合</returns>
        public DataSet GetAllQuestionList()
        {
            return GetQuestionList(-1, "");
        }

        /// <summary>
        /// 根据条件获得问卷条目数
        /// </summary>
        /// <param name="key">查询条件  0：ID, 1：Title,  其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>问卷集合条目数</returns>
        public int GetQuestionaryRecordCount(int key, string value)
        {
            return qnDal.GetRecordCount(key, value);
        }


        /// <summary>
        /// 根据条件获得问题条目数
        /// </summary>
        /// <param name="key">查询条件   0：ID, 1：QuestionaryID,2：Content,3:Answer,4:Orders,5:IsSingle 其他:全部</param>
        /// <param name="value">查询值</param>
        /// <returns>问题集合条目数</returns>
        public int GetQuestionRecordCount(int key, string value)
        {
            return qsDal.GetRecordCount(key, value);
        }




        public DataSet GetQnListByPage(int key, string value, int filedOrder, bool desc, int startIndex, int endIndex)
        {
            return qnDal.GetListByPage(key, value, filedOrder, desc, startIndex, endIndex);
        } 

        #endregion  BasicMethod 
    }
}

