using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Interface.Entity.MongoDB
{
    public class EndUser
    {
        private string _AutoID;
        /// <summary>
        ///
        /// </summary>
        public string AutoID
        {
            get { return _AutoID; }
            set { _AutoID = value; }
        }

        private string _EnduserName;
        /// <summary>
        ///
        /// </summary>
        public string EnduserName
        {
            get { return _EnduserName; }
            set { _EnduserName = value; }
        }

        private string _SimCardNo;
        /// <summary>
        ///
        /// </summary>
        public string SimCardNo
        {
            get { return _SimCardNo; }
            set { _SimCardNo = value; }
        }

        private string[] _Identities;
        /// <summary>
        ///
        /// </summary>
        public string[] Identities
        {
            get { return _Identities; }
            set { _Identities = value; }
        }

        private DateTime _LastModifyTime;
        /// <summary>
        ///
        /// </summary>
        public DateTime LastModifyTime
        {
            get { return _LastModifyTime; }
            set { _LastModifyTime = value; }
        }

        private bool _IsValid;
        /// <summary>
        ///
        /// </summary>
        public bool IsValid
        {
            get { return _IsValid; }
            set { _IsValid = value; }
        }
    }
}