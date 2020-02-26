using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 探伤综合分析
    /// </summary>
    public class TrustCheckReport
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
        /// 装置名称
        /// </summary>
        public string devicename
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
        /// 已审核委托单数
        /// </summary>
        public int? trust_Audit_total
        {
            get;
            set;
        }
        /// <summary>
        /// 未审核委托单数
        /// </summary>
        public int? trust_NoAudit_total
        {
            get;
            set;
        }

        /// <summary>
        /// 已审核检测单数
        /// </summary>
        public int? check_Audit_total
        {
            get;
            set;
        }

        /// <summary>
        /// 未审核检测单数
        /// </summary>
        public int? check_NoAudit_total
        {
            get;
            set;
        }
     
    }
}
