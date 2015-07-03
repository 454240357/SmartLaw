using System;
namespace SmartLaw.Model
{
	/// <summary>
	/// SYSCODEDETIAL:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SysCodeDetail
	{
		public SysCodeDetail()
		{}
		#region Model
		private string _syscodeid;
		private string _syscodedetialid;
		private string _syscodedetialcontext;
		private DateTime _lastmodifytime;
		private bool _isvalid= true;
		private string _memo;
		/// <summary>
		/// 对应的系统配置项代码ID
		/// </summary>
		public string SYSCodeID
		{
			set{ _syscodeid=value;}
			get{return _syscodeid;}
		}
		/// <summary>
		/// 详细配置项代码名称
		/// </summary>
		public string SYSCodeDetialID
		{
			set{ _syscodedetialid=value;}
			get{return _syscodedetialid;}
		}
		/// <summary>
		/// 系统详细配置项目说明
		/// </summary>
		public string SYSCodeDetialContext
		{
			set{ _syscodedetialcontext=value;}
			get{return _syscodedetialcontext;}
		}
		/// <summary>
		/// 最后修改日期
		/// </summary>
		public DateTime LastModifyTime
		{
			set{ _lastmodifytime=value;}
			get{return _lastmodifytime;}
		}
		/// <summary>
		/// 状态
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

