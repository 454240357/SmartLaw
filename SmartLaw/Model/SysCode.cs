using System;
namespace SmartLaw.Model
{
	/// <summary>
	/// SYSCODE:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SysCode
	{
		public SysCode()
		{}
		#region Model
		private string _syscodeid;
		private string _syscodecontext;
		private DateTime _lastmodifytime= DateTime.Now;
		private bool _isvalid= true;
		private string _memo;
		/// <summary>
		/// 系统配置项代码名称
		/// </summary>
		public string SYSCodeID
		{
			set{ _syscodeid=value;}
			get{return _syscodeid;}
		}
		/// <summary>
		/// 系统配置项备注说明
		/// </summary>
		public string SYSCodeContext
		{
			set{ _syscodecontext=value;}
			get{return _syscodecontext;}
		}
		/// <summary>
		/// 创建日期
		/// </summary>
		public DateTime LastModifytime
		{
			set{ _lastmodifytime=value;}
			get{return _lastmodifytime;}
		}
		/// <summary>
		/// 状态
		/// </summary>
		public bool ISValid
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

