using System; 

namespace SmartLaw.Model
{
    [Serializable]
    public partial class Notice
    {
        public Notice()
        {
        }

        #region Model
        private long _autoid;
        private string _contents;
        private string _orders;
        private string _publisher;
        private DateTime _lastmodifytime = DateTime.Now;
        private bool _isvalid = true;

        /// <summary>
        /// 公告ID
        /// </summary>
        public long AutoID
        {
            set { _autoid = value; }
            get { return _autoid; }
        }

        public string Contents
        {
            set { _contents = value; }
            get { return _contents; }
        }

        public string Orders
        {
            set { _orders = value; }
            get { return _orders; }
        }

        public string Publisher
        {
            set { _publisher = value; }
            get { return _publisher; }
        }

        public DateTime LastModifyTime
        {
            set { _lastmodifytime = value; }
            get { return _lastmodifytime; }
        } 

        public bool IsValid
        {
            set { _isvalid = value; }
            get { return _isvalid; }
        }

        #endregion Model
    }
}
