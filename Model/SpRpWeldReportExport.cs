using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 管道焊接工作记录
    /// </summary>
    public class SpRpWeldReportExport
    {
        /// <summary>
        /// 焊工号
        /// </summary>
        public string WED_Code
        {
            get;
            set;
        }
        /// <summary>
        /// 管道编号
        /// </summary>
        public string ISO_IsoNo
        {
            get;
            set;
        }
        /// <summary>
        /// 焊口编号
        /// </summary>
        public string JOT_JointNo
        {
            get;
            set;
        }
        /// <summary>
        /// 规格(mm)
        /// </summary>
        public string JOT_JointDesc
        {
            get;
            set;
        }
        /// <summary>
        /// 材质
        /// </summary>
        public string STE_Name
        {
            get;
            set;
        }
        /// <summary>
        /// 单线图号
        /// </summary>
        public string ISO_IsoNumber
        {
            get;
            set;
        }
        /// <summary>
        /// 焊接位置
        /// </summary>
        public string weldLocal
        {
            get;
            set;
        }
        /// <summary>
        /// 焊接方法
        /// </summary>
        public string WME_Name
        {
            get;
            set;
        }
        /// <summary>
        /// 焊材牌号
        /// </summary>
        public string WMT_MatName
        {
            get;
            set;
        }
        /// <summary>
        /// 实际预热温度℃
        /// </summary>
        public decimal? JOT_PrepareTemp
        {
            get;
            set;
        }
        /// <summary>
        /// 月
        /// </summary>
        public int? ReportMonth
        {
            get;
            set;
        }
        /// <summary>
        /// 日
        /// </summary>
        public int? Reportday
        {
            get;
            set;
        }
        /// <summary>
        /// 无损检测报告编号
        /// </summary>
        public string NDTT_CheckCode
        {
            get;
            set;
        }
        /// <summary>
        /// 热处理报告编号
        /// </summary>
        public string JOT_HotRpt
        {
            get;
            set;
        }
        /// <summary>
        /// 管线id
        /// </summary>
        public string ISO_ID
        {
            get;
            set;
        }
    }
}
