using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmartLaw.Model
{
    /// <summary>
    /// Station:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Station
    {
        public Station()
        { }
        #region Model
        
        private string _stationName;
     
        private string _abbr;
 
        /// <summary>
        /// 站点名称
        /// </summary>
        public string StationName
        {
            set { _stationName = value; }
            get { return _stationName; }
        }
        /// <summary>
        /// 站点缩写：拼音首字母
        /// </summary>
        public string Abbr
        {
            set { _abbr = value; }
            get { return _abbr; }
        }
 
        #endregion Model

    }
}
