using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 菜单项
    /// </summary>
    public class SpSysMenuItem
    {
        /// <summary>
        /// 系统ID
        /// </summary>
        public string MenuId
        {
            get;
            set;
        }
        /// <summary>
        /// 系统名称
        /// </summary>
        public string MenuName
        {
            get;
            set;
        }
        /// <summary>
        /// 路径
        /// </summary>
        public string Url
        {
            get;
            set;
        }
        /// <summary>
        /// 上级菜单
        /// </summary>
        public string SuperMenu
        {
            get;
            set;
        }
    }
}
