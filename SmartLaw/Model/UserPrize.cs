using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLaw.Model
{
    [Serializable]
    public partial class UserPrize
    {
        public UserPrize()
        { }
        #region Model
        private long _autoid;
        private long _prizeid;
        private string _simCardNO;
        private int _amount=1;
        private bool _isTaken=false;
        private DateTime _takenTime;
        private string _remarks;


        /// <summary>
        /// ID
        /// </summary>
        public long AutoID
        {
            set { _autoid = value; }
            get { return _autoid; }
        }
        /// <summary>
        /// 奖品ID
        /// </summary>
        public long PrizeID
        {
            set { _prizeid = value; }
            get { return _prizeid; }
        }
        /// <summary>
        /// SIM卡号，即用户ID
        /// </summary>
        public string SimCardNO
        {
            set { _simCardNO = value; }
            get { return _simCardNO; }
        }
        /// <summary>
        /// 兑换数量
        /// </summary>
        public int Amount
        {
            set { _amount = value; }
            get { return _amount; }
        }
        /// <summary>
        /// 是否已领取
        /// </summary>
        public bool IsTaken
        {
            set { _isTaken = value; }
            get { return _isTaken; }
        }
     
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            set { string _remarks = value; }
            get { return _remarks; }
        }
        /// <summary>
        /// 领取时间
        /// </summary>
        public DateTime TakenTime
        {
            set { _takenTime = value; }
            get { return _takenTime; }
        }
      
        #endregion Model

    }
}
