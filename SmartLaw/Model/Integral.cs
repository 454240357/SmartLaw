using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLaw.Model
{
    [Serializable]
    public partial class Integral
    {
        public Integral()
        {
        }
        #region Model
        private long _autoid;
        private string _simcardno;
        private string _items;
        private int _integraladded;
        private long _totalintegral;
        private DateTime _lastmodifytime;
        /// <summary>
        /// id
        /// </summary>
        public long AutoID
        {
            set { _autoid = value; }
            get { return _autoid; }
        }
        /// <summary>
        /// SimCardNo
        /// </summary>
        public string SimCardNo
        {
            set { _simcardno = value; }
            get { return _simcardno; }
        }
        /// <summary>
        /// Items：名目
        /// </summary>
        public string Items
        {
            set { _items = value; }
            get { return _items; }
        }
        /// <summary>
        /// IntegralAdded：本次增加积分值
        /// </summary>
        public int IntegralAdded
        {
            set { _integraladded = value; }
            get { return _integraladded; }
        }

        /// <summary>
        /// TotalIntegral：当前总积分值
        /// </summary>
        public long TotalIntegral
        {
            set { _totalintegral = value; }
            get { return _totalintegral; }
        }
        public DateTime LastModifyTime
        {
            set { _lastmodifytime = value; }
            get { return _lastmodifytime; }
        } 

        #endregion Model
    }
}
