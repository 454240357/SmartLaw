/**  版本信息模板在安装目录下，可自行修改。
* log.cs
*
* 功 能： N/A
* 类 名： log
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/5/22 16:08:35   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace SmartLaw.Model
{
	/// <summary>
	/// log:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Log
	{
		public Log()
		{}
		#region Model
		private long _autoid;
		private string _operationitem;
        private DateTime _operationtime = DateTime.Now;
		private string _operationdetail;
		private string _operator;
		private string _memo;
		/// <summary>
		/// auto_increment
		/// </summary>
		public long AutoID
		{
			set{ _autoid=value;}
			get{return _autoid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperationItem
		{
			set{ _operationitem=value;}
			get{return _operationitem;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime OperationTime
		{
			set{ _operationtime=value;}
			get{return _operationtime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperationDetail
		{
			set{ _operationdetail=value;}
			get{return _operationdetail;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Operator
		{
			set{ _operator=value;}
			get{return _operator;}
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

