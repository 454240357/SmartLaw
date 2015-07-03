using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLaw.Model
{
    [Serializable]
    public partial class Prize
    {
        public Prize()
        { }
        #region Model
        private long _autoid;
        private string _prizeName;
        private string _prizeUnit;
        private int _points;
        private string _registrant;
        private long _stock;
        private string _remarks;
        private DateTime _regTime = DateTime.Now;
        private string _picture ;
        
       
        /// <summary>
        /// 奖品ID
        /// </summary>
        public long AutoID
        {
            set { _autoid = value; }
            get { return _autoid; }
        }
        /// <summary>
        /// 奖品名称
        /// </summary>
        public string PrizeName
        {
            set { _prizeName = value; }
            get { return _prizeName; }
        }
        /// <summary>
        /// 奖品单位
        /// </summary>
        public string PrizeUnit
        {
            set { _prizeUnit = value; }
            get { return _prizeUnit; }
        }
        /// <summary>
        /// 兑换该奖品所需积分
        /// </summary>
        public int Points
        {
            set { _points = value; }
            get { return   _points; }
        }
        /// <summary>
        /// 发布者
        /// </summary>
        public string Registrant
        {
            set { _registrant = value; }
            get { return _registrant; }
        }
        /// <summary>
        /// 库存
        /// </summary>
        public long Stock
        {
            set {  _stock = value; }
            get { return  _stock; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            set {  _remarks = value; }
            get { return  _remarks; }
        }
        /// <summary>
        /// 登记时间
        /// </summary>
        public DateTime RegTime
        {
            set { _regTime = value; }
            get { return _regTime; }
        }
        /// <summary>
        /// 图片
        /// </summary>
        public string Picture
        {
            set { _picture = value; }
            get { return _picture; }
        }
     
        #endregion Model

    }
}
