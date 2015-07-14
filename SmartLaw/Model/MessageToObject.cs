using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLaw.Model
{
    [Serializable]
    public partial class MessageToObject
    {
        public MessageToObject()
        { }
        #region Model
        private long _autoid;
        private long _msgid;
        private string _objtype;
        private string _objvalue;
        /// <summary>
        /// id
        /// </summary>
        public long AutoID
        {
            set { _autoid = value; }
            get { return _autoid; }
        }
        /// <summary>
        /// ID
        /// </summary>
        public long MsgID
        {
            set { _msgid = value; }
            get { return _msgid; }
        }
        /// <summary>
        /// 类型 0:身份/区域 1：用户 2：不限
        /// </summary>
        public string ObjType
        {
            set { _objtype = value; }
            get { return _objtype; }
        }
        /// <summary>
        /// 值
        /// </summary>
        public string ObjValue
        {
            set { _objvalue = value; }
            get { return _objvalue; }
        }
        #endregion Model
    }
}
