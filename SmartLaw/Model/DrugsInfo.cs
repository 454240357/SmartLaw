using System;
namespace SmartLaw.Model
{
    /// <summary>
    /// DrugsInfo:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class DrugsInfo
    {
        public DrugsInfo()
        { }
        #region Model
        private long _autoid;
        private string _drugName;
        private string _form;
        private string _formInfo;
        private string _spec;
        private string _unit;
        private float _price;
        private string _validateTime;
        private string _district;
        private string _company;
        private string _policy;
        private string _abbr;
        private DateTime _dataTime=DateTime.Now;
      
        /// <summary>
        /// 药品ID
        /// </summary>
        public long AutoID
        {
            set { _autoid = value; }
            get { return _autoid; }
        }
        /// <summary>
        /// 药品名称
        /// </summary>
        public string DrugName
        {
            set { _drugName = value; }
            get { return _drugName; }
        }
        /// <summary>
        /// 剂型
        /// </summary>
        public string Form
        {
            set { _form = value; }
            get { return _form; }
        }
        /// <summary>
        /// 剂型说明
        /// </summary>
        public string FormInfo
        {
            set { _formInfo = value; }
            get { return _formInfo; }
        }
        /// <summary>
        /// 规格
        /// </summary>
        public string Spec
        {
            set { _spec = value; }
            get { return _spec; }
        }
        /// <summary>
        ///单位
        /// </summary>
        public string Unit
        {
            set { _unit = value; }
            get { return _unit; }
        }
        /// <summary>
        /// 参考价
        /// </summary>
        public float Price
        {
            set { _price = value; }
            get { return _price; }
        }
        /// <summary>
        /// 生效时间
        /// </summary>
        public string ValidateTime
        {
            set { _validateTime = value; }
            get { return _validateTime; }
        }
        /// <summary>
        /// 执行地区
        /// </summary>
        public string District
        {
            set { _district = value; }
            get { return _district; }
        }
        /// <summary>
        /// 生产企业
        /// </summary>
        public string Company
        {
            set { _company = value; }
            get { return _company; }
        }
        /// <summary>
        /// 政策依据
        /// </summary>
        public string Policy
        {
            set { _policy = value; }
            get { return _policy; }
        }


        /// <summary>
        /// 缩写
        /// </summary>
        public string Abbr
        {
            set {_abbr = value; }
            get { return _abbr; }
        }




        /// <summary>
        /// 获取数据的日期
        /// </summary>
        public DateTime DataTime
        {
            set { _dataTime = value; }
            get { return _dataTime; }
        }
        #endregion Model

    }
}

