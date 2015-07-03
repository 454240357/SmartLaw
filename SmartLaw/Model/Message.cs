using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLaw.Model
{
    [Serializable]
    public partial class  Message
    {
        public Message()
        {
        }

        #region Model
        private long _autoid;
        private string _title;
        private string _contents;
        private int _orders;
        private string _messagetype;
        private string _disappeartype;
        private DateTime _availableTime;
        private DateTime _expiredTime; 
        private string _publisher;
        private DateTime _lastmodifytime = DateTime.Now;
        private bool _isvalid = true;
        private bool _andor;
        private string _memo;
        private string _datatype;
        /// <summary>
        /// 公告ID
        /// </summary>
        public long AutoID
        {
            set { _autoid = value; }
            get { return _autoid; }
        }

        public string Title
        {
            set { _title = value; }
            get { return _title; }

        }
        public string Contents
        {
            set { _contents = value; }
            get { return _contents; }
        }

        public int Orders
        {
            set { _orders = value; }
            get { return _orders; }
        }

        public string MessageType
        {
            set { _messagetype = value; }
            get { return _messagetype; }
        }
        /// <summary>
        /// 关闭 数字:滚动的次数 非数字：弹走方式的系统代码
        /// </summary>
        public string DisappearType
        {
            set { _disappeartype = value; }
            get { return _disappeartype; }
        }

        public DateTime AvailableTime
        {
            set { _availableTime = value; }
            get { return _availableTime; }
        }

        public DateTime ExpiredTime
        {
            set { _expiredTime = value; }
            get { return _expiredTime; }
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

        /// <summary>
        /// 关系 true:且 false：或
        /// </summary>
        public bool AndOr
        {
            set { _andor = value; }
            get { return _andor; } 
        }

        public string Memo
        {
            set { _memo = value; }
            get { return _memo; }
        }
        public string DataType
        {
            set { _datatype = value; }
            get { return _datatype; }
        }
        #endregion Model
    }
}
