using System;
namespace SmartLaw.Model
{
    /// <summary>
    /// Question:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Question
    {
        public Question()
        { }
        #region Model
        private long _id;
        private long _questionaryid;
        private string _content;
        private string[] _answer;
        private int _orders;
        private bool _isSingle;
       
        /// <summary>
        /// 问题ID
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 问题所属的问卷ID
        /// </summary>
        public long QuestionaryID
        {
            set { _questionaryid = value; }
            get { return _questionaryid; }
        }
        /// <summary>
        /// 问题的所有备选答案
        /// </summary>
        public string[] Answer
        {
            set { _answer = value; }
            get { return _answer; }
        }
        /// <summary>
        /// 问题的题目
        /// </summary>
        public string Content
        {
            set { _content = value; }
            get { return _content; }
        }


        /// <summary>
        /// 问题显示顺序，即在问卷中是第几题
        /// </summary>
        public int Orders
        {
            set { _orders = value; }
            get { return _orders; }
        }


        /// <summary>
        /// 该问题是单选还是多选 1:单选 0:复选
        /// </summary>
        public bool IsSingle
        {
            set { _isSingle = value; }
            get { return _isSingle; }
        }

        #endregion Model

    }
}

