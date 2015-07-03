using System;
namespace SmartLaw.Model
{
	/// <summary>
	/// SysCodeRelation:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SysCodeRelation
	{
		public SysCodeRelation()
		{}
		#region Model
		private long _autoid;
		private string _syscodedetialid;
		private string _syscodedetialidex;
		/// <summary>
		/// 
		/// </summary>
		public long AutoID
		{
			set{ _autoid=value;}
			get{return _autoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SysCodeDetialID
		{
			set{ _syscodedetialid=value;}
			get{return _syscodedetialid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SysCodeDetialIDEx
		{
			set{ _syscodedetialidex=value;}
			get{return _syscodedetialidex;}
		}
		#endregion Model

	}
}

