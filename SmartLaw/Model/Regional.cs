using System;
namespace SmartLaw.Model
{
	/// <summary>
	/// regional:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Regional
	{
		public Regional()
		{}
		#region Model
		private string _regionalid;
		private string _regionalname;
		private string _regionalcode;
        private string _subregionalid;
		private string _regionallevel;
        private int _orders;
        private DateTime _lastmodifytime = DateTime.Now;
		private bool _isvalid= false;
		private string _memo;
		/// <summary>
		/// auto_increment
		/// </summary>
		public string RegionalID
		{
            set { _regionalid = value; }
            get { return _regionalid; }
		}
		/// <summary>
		/// 
		/// </summary>
		public string RegionalName
		{
			set{ _regionalname=value;}
			get{return _regionalname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RegionalCode
		{
			set{ _regionalcode=value;}
			get{return _regionalcode;}
		}
		/// <summary>
		/// 
		/// </summary>
        public string SubRegionalID
		{
			set{ _subregionalid=value;}
			get{return _subregionalid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RegionalLevel
		{
			set{ _regionallevel=value;}
			get{return _regionallevel;}
		}
        /// <summary>
        /// 排序标志
        /// </summary>
        public int Orders
        {
            set { _orders = value; }
            get { return _orders; }
        }
		/// <summary>
		/// 
		/// </summary>
		public DateTime LastModifyTime
		{
			set{ _lastmodifytime=value;}
			get{return _lastmodifytime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsValid
		{
			set{ _isvalid=value;}
			get{return _isvalid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		#endregion Model

	}
}

