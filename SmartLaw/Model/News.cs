using System;
namespace SmartLaw.Model
{
	/// <summary>
	/// News:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class News
	{
		public News()
		{}
		#region Model
		private long _autoid;
		private string _title;
		private long _categoryid;
		private string _contents;
		private string _publisher;
		private string _datasource;
		private string _datatype;
		private DateTime _lastmodifytime= DateTime.Now;
		private bool _isvalid= true;
		private int _checked;
		private string _checker;
        private string _checkmemo;
		/// <summary>
		/// 新闻ID
		/// </summary>
		public long AutoID
		{
			set{ _autoid=value;}
			get{return _autoid;}
		}
		/// <summary>
		/// 标题
		/// </summary>
		public string Title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 分类
		/// </summary>
		public long CategoryID
		{
			set{ _categoryid=value;}
			get{return _categoryid;}
		}
		/// <summary>
		/// 内容
		/// </summary>
		public string Contents
		{
			set{ _contents=value;}
			get{return _contents;}
		}
		/// <summary>
		/// 发布者
		/// </summary>
		public string Publisher
		{
			set{ _publisher=value;}
			get{return _publisher;}
		}
		/// <summary>
		/// 发布途径 与系统配置项关联
		/// </summary>
		public string DataSource
		{
			set{ _datasource=value;}
			get{return _datasource;}
		}
		/// <summary>
		/// 新闻类型
		/// </summary>
		public string DataType
		{
			set{ _datatype=value;}
			get{return _datatype;}
		}
		/// <summary>
		/// 修改时间
		/// </summary>
		public DateTime LastModifyTime
		{
			set{ _lastmodifytime=value;}
			get{return _lastmodifytime;}
		}
		/// <summary>
		/// 状态 1:有效 0:无效
		/// </summary>
		public bool IsValid
		{
			set{ _isvalid=value;}
			get{return _isvalid;}
		}
		/// <summary>
		/// 审核状态 0:待审核 1:已审核 2：审核未通过
		/// </summary>
		public int Checked
		{
			set{ _checked=value;}
			get{return _checked;}
		}
		/// <summary>
		/// 审核者
		/// </summary>
		public string Checker
		{
			set{ _checker=value;}
			get{return _checker;}
		}
        /// <summary>
        /// 审核备注
        /// </summary>
        public string CheckMemo
        {
            set { _checkmemo = value; }
            get { return _checkmemo; }
        }
		#endregion Model

	}
}

