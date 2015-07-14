using System;
namespace SmartLaw.Model
{
    /// <summary>
    /// Questionary:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Questionary
    {
        public Questionary()
        { }
        #region Model
        private long _id; 
        private string _title;
        private bool _isvalid;

        /// <summary>
        /// 问卷ID
        /// </summary>
        public long ID
        {
            set { _id = value; }
            get { return _id; }
        }
       
       
        /// <summary>
        /// 问卷标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public bool IsValid
        {
            set { _isvalid = value; }
            get { return _isvalid; }
        }

        #endregion Model

    }
}

