using System;
namespace SmartLaw.Model
{
    /// <summary>
    /// UserBehavior:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class UserBehavior
    {//
        public UserBehavior()
        { }
        #region Model

        private long _autoid;
        private string _simCardNo;
        private long _categoryid;
        private string _ipAddr;
        private string _remarks=null;
        private DateTime _scanTime = DateTime.Now;
        private string _behavior;
       
        /// <summary>
        /// 用户行为ID
        /// </summary>
        public long AutoID
        {
            set { _autoid = value; }
            get { return _autoid; }
        }
        /// <summary>
        ///SIM卡号
        /// </summary>
        public string SimCardNO
        {
            set { _simCardNo = value; }
            get { return _simCardNo; }
        }
        /// <summary>
        /// 分类
        /// </summary>
        public long CategoryID
        {
            set { _categoryid = value; }
            get { return _categoryid; }
        }
        /// <summary>
        /// IP
        /// </summary>
        public string IPAddr
        {
            set { _ipAddr = value; }
            get { return _ipAddr; }
        }

        /// <summary>
        ///浏览时间
        /// </summary>
        public DateTime ScanTime
        {
            set { _scanTime = value; }
            get { return _scanTime; }
        }


        /// <summary>
        ///备注
        /// </summary>
        public string Remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }


        /// <summary>
        ///用户行为：完成问卷调查/观看党员视频
        /// </summary>
        public string Behavior
        {
            set { _behavior = value; }
            get { return _behavior; }
        }


        #endregion Model

    }
}

