using System;
namespace SmartLaw.Model
{
	/// <summary>
	/// CategoryRegionalRelation:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class CategoryRegionalRelation
	{
		public CategoryRegionalRelation()
		{}
		#region Model
		private long _autoid;
		private long _categotyid;
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
		/// 分类ID
		/// </summary>
		public long CategotyID
		{
			set{ _categotyid=value;}
			get{return _categotyid;}
		}
		/// <summary>
		/// 区域代码
		/// </summary>
		public string RegionalID
		{
			set{ _regionalid=value;}
			get{return _regionalid;;}
		}
		#endregion Model

	}
}

