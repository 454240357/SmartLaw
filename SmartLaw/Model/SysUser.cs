using System;
namespace SmartLaw.Model
{
	/// <summary>
	/// SysUser:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class SysUser
	{
		public SysUser()
		{}
		#region Model
		private string _userid;
		private string _password;
		private string _employeeid;
		private string _username;
		private bool _isvalid;
		/// <summary>
		/// 
		/// </summary>
		public string UserID
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EmployeeID
		{
			set{ _employeeid=value;}
			get{return _employeeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool IsValid
		{
			set{ _isvalid=value;}
			get{return _isvalid;}
		}
		#endregion Model

	}
}

