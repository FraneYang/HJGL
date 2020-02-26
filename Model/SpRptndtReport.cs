using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 探伤综合分析
    /// </summary>
    public class SpRptndtReport
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public string ProjectId
        {
            get;
            set;
        }
        /// <summary>
        /// 管线Id
        /// </summary>
        public string iso_id
        {
            get;
            set;
        }
        /// <summary>
        /// 管线号
        /// </summary>
        public string iso_isono
        {
            get;
            set;
        }
        /// <summary>
        /// 单位编号
        /// </summary>
        public string bsu_unitcode
        {
            get;
            set;
        }
        /// <summary>
        /// 单位名称
        /// </summary>
        public string bsu_unitname
        {
            get;
            set;
        }

        /// <summary>
        /// 区域
        /// </summary>
        public string WorkAreaCode
        {
            get;
            set;
        }

        /// <summary>
        /// 探伤比例
        /// </summary>
        public string source_rate
        {
            get;
            set;
        }
        /// <summary>
        /// 总焊口
        /// </summary>
        public int? total_jot
        {
            get;
            set;
        }
        /// <summary>
        /// 完成总焊口
        /// </summary>
        public int? finished_total_jot
        {
            get;
            set;
        }

        /// <summary>
        /// 委托口数
        /// </summary>
        public int? trust_total_jot
        {
            get;
            set;
        }

        /// <summary>
        /// 扩透口数
        /// </summary>
        public int? ext_jot
        {
            get;
            set;
        }
        /// <summary>
        /// 已探口数
        /// </summary>
        public int? check_total_jot
        {
            get;
            set;
        }
        /// <summary>
        /// 返修口数
        /// </summary>
        public int total_repairjot
        {
            get;
            set;
        }
        /// <summary>
        /// 切除口数
        /// </summary>
        public int? cut_jot
        {
            get;
            set;
        }
    }
}
