using System;
namespace SmartLaw.Model
{
    /// <summary>
    /// UserAnswer:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class UserAnswer
    {
        public UserAnswer()
        { }
        #region Model
        private long _userid;
        private long _questionid;
        private int _orders; 
        private string[] _answer;

        /// <summary>
        /// 用户ID
        /// </summary>
        public long UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
       
        /// <summary>
        /// 该用户选择的答案
        /// </summary>
        public string[] Answer
        {
            set { _answer = value; }
            get { return _answer; }
        }

        /// <summary>
        /// 答案所属问题的ID
        /// </summary>
        public long QuestionID
        {
            set { _questionid = value; }
            get { return _questionid; }
        }

        
        /// <summary>
        /// 排序标志，可为空
        /// </summary>
        public int Orders
        {
            set { _orders = value; }
            get { return _orders; }
        } 
        #endregion Model

    }
}

