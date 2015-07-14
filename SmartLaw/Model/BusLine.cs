using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLaw.Model
{
    /// <summary>
    /// BusLine:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class BusLine
    {
        public BusLine()
        { }
        #region Model
        private long _autoid;
        private string _routeName;
        private string[] _station;
     
        private string _remarks;
        
        /// <summary>
        /// 线路ID
        /// </summary>
        public long AutoID
        {
            set { _autoid = value; }
            get { return _autoid; }
        }
        /// <summary>
        /// 线路名称
        /// </summary>
        public string RouteName
        {
            set { _routeName = value; }
            get { return _routeName; }
        }
        /// <summary>
        /// 线路途经站点
        /// </summary>
        public string[] Station
        {
            set { _station = value; }
            get { return _station; }
        }
        ///// <summary>
        ///// 首班车时间
        ///// </summary>
        //public string FirstTime
        //{
        //    set { _firstTime = value; }
        //    get { return _firstTime; }
        //}
        ///// <summary>
        ///// 末班车时间
        ///// </summary>
        //public string LastTime
        //{
        //    set { _lastTime = value; }
        //    get { return _lastTime; }
        //}
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks
        {
            set { _remarks = value; }
            get { return _remarks; }
        }
        #endregion Model

    }
}
