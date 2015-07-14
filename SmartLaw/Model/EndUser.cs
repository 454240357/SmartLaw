using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLaw.Model
{
    [Serializable]
    public partial class EndUser
    {
        public EndUser()
        {
        }
        #region Model
        private long _autoid;
        private string _endusername;
        private string _simcardno;
        private string _identities;
        private DateTime _lastmodifytime = DateTime.Now;
        private bool _isvalid = true;

        /// <summary>
        /// 终端用户ID
        /// </summary>
        public long AutoID
        {
            set { _autoid = value; }
            get { return _autoid; }
        }

        public string EndUserName
        {
            set { _endusername = value; }
            get { return _endusername; }
        }

        public string SimCardNo
        {
            set { _simcardno = value; }
            get { return _simcardno; }
        }

        public string Identities
        {
            set { _identities = value; }
            get { return _identities; }
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
