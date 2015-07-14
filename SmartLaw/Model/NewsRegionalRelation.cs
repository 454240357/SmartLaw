using System;
namespace SmartLaw.Model
{
	/// <summary>
	/// NewsRegionalRelation:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class NewsRegionalRelation
	{
		public NewsRegionalRelation()
		{}
		#region Model
		private long _autoid;
		private long _newsid;
		private string _regionalid;
		/// <summary>
		/// id
		/// </summary>
		public long AutoID
		{
			set{ _autoid=value;}
			get{return _autoid;}
		}
		/// <summary>
		/// 新闻ID
		/// </summary>
		public long NewsID
		{
			set{ _newsid=value;}
			get{return _newsid;}
		}
		/// <summary>
		/// 区域代码
		/// </summary>
		public string RegionalID
		{
            set { _regionalid = value; }
            get { return _regionalid; }
		}
		#endregion Model

	}
}

