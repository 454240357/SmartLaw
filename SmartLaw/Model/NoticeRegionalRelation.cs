using System; 

namespace SmartLaw.Model
{
    [Serializable]
    public partial class NoticeRegionalRelation
    {
        public NoticeRegionalRelation()
        { }
        #region Model
        private long _autoid;
        private long _noticeid;
        private string _regionalid;
        /// <summary>
        /// id
        /// </summary>
        public long AutoID
        {
            set { _autoid = value; }
            get { return _autoid; }
        }
        /// <summary>
        /// ID
        /// </summary>
        public long NoticeID
        {
            set { _noticeid = value; }
            get { return _noticeid; }
        }
        /// <summary>
        /// 区域代码
        /// </summary>
        public string RegionalID
        {
            set { _regionalid = value; }
            get { return _regionalid; }
        }
        #endregion Model
    }
}
