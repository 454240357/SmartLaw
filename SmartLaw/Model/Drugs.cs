using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLaw.Model
{
    /// <summary>
    /// Drugs:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Drugs
    {
        public Drugs()
		{ }
        #region Model
        private long _autoid;
        private string _pzwh;
        private string _cpmc;
        private string _ywmc;
        private string _spm;
        private string _jx;
        private string _gg;
        private string _scdw;
        private string _scdz;
        private string _cplb;
        private string _ypzwh; 
        private DateTime _pzrq;
        private string _bwm;
        private string _bwmbz;
        /// <summary>
        /// ID
        /// </summary>
        public long AutoID
        {
            set { _autoid = value; }
            get { return _autoid; }
        }
        /// <summary>
        /// 批准文号
        /// </summary>
        public string PZWH
        {
            set { _pzwh = value; }
            get { return _pzwh; }
        }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string CPMC
        {
            set { _cpmc = value; }
            get { return _cpmc; }
        }
        /// <summary>
        /// 英文名称
        /// </summary>
        public string YWMC
        {
            set { _ywmc = value; }
            get { return _ywmc; }
        }

        /// <summary>
        /// 商品名
        /// </summary>
        public string SPM
        {
            set { _spm = value; }
            get { return _spm; } 
        }
        /// <summary>
        /// 剂型
        /// </summary>
        public string JX
        {
            set { _jx = value; }
            get { return _jx; }
        }

        /// <summary>
        /// 规格
        /// </summary>
        public string GG
        {
            set { _gg = value; }
            get { return _gg; }
        }

        /// <summary>
        /// 生产单位
        /// </summary>
        public string SCDW
        {
            set { _scdw = value; }
            get { return _scdw; }
        }

        /// <summary>
        /// 生产地址
        /// </summary>
        public string SCDZ
        {
            set { _scdz = value; }
            get { return _scdz; }
        }

        /// <summary>
        /// 产品类别
        /// </summary>
        public string CPLB
        {
            set { _cplb = value; }
            get { return _cplb; }
        }

        /// <summary>
        /// 原批准文号
        /// </summary>
        public string YPZWH
        {
            set { _ypzwh = value; }
            get { return _ypzwh; }
        }
        /// <summary>
        /// 批准日期
        /// </summary>
        public DateTime PZRQ
        {
            set { _pzrq = value; }
            get { return _pzrq; }
        }
        /// <summary>
        /// 本位码
        /// </summary>
        public string BWM
        {
            set { _bwm = value; }
            get { return _bwm; }
        }
        /// <summary>
        /// 本位码备注	
        /// </summary>
        public string BWMBZ
        {
            set { _bwmbz = value; }
            get { return _bwmbz; }
        }
        #endregion model
    }
}
