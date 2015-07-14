using System;
namespace SmartLaw.Model
{
	/// <summary>
	/// Category:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Category
	{
		public Category()
		{}
		#region Model
		private long _autoid;
		private string _categoryname;
		private long _parentcategoryid;
		private int _orders;
		private DateTime _lastmodifytime= DateTime.Now;
		private bool _isvalid= true;
		private string _memo;
		/// <summary>
		/// 分类ID
		/// </summary>
		public long AutoID
		{
			set{ _autoid=value;}
			get{return _autoid;}
		}
		/// <summary>
		/// 分类名字
		/// </summary>
		public string CategoryName
		{
			set{ _categoryname=value;}
			get{return _categoryname;}
		}
		/// <summary>
		/// 父类别ID
		/// </summary>
		public long ParentCategoryID
		{
			set{ _parentcategoryid=value;}
			get{return _parentcategoryid;}
		}
		/// <summary>
		/// 排序标志
		/// </summary>
		public int Orders
		{
			set{ _orders=value;}
			get{return _orders;}
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
		/// 备注
		/// </summary>
		public string Memo
		{
			set{ _memo=value;}
			get{return _memo;}
		}
		#endregion Model

	}
}

