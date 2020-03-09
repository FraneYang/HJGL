using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 介质综合分析
    /// </summary>
   public class SpTestPackagePipelineAnalysisItem
    {
       /// <summary>
       /// 项目ID
       /// </summary>
       public string ProjectId
       {
           get;
           set;
       }
       /// <summary>
       ///  管线ID
       /// </summary>
       public string ISO_Id
        {
           get;
           set;
       }
       /// <summary>
       /// 管线号
       /// </summary>
       public string ISO_IsoNo
        {
           get;
           set;
       }
       /// <summary>
       /// 施压包号
       /// </summary>
       public string PTP_TestPackageNo
        {
           get;
           set;
       }
       /// <summary>
       /// 最近焊期
       /// </summary>
       public DateTime? maxdate
        {
           get;
           set;
       }
       /// <summary>
       /// 焊口数
       /// </summary>
       public int? JotCounts
        {
           get;
           set;
       }
        /// <summary>
        /// 已完成数
        /// </summary>
        public int? JotCompletedCounts
        {
           get;
           set;
       }
        /// <summary>
        /// 完成比例
        /// </summary>
        public decimal? JotCompletedRatio
        {
           get;
           set;
       }
        /// <summary>
        /// 达因数量
        /// </summary>
        public decimal? DinCounts
        {
           get;
           set;
       }
        /// <summary>
        /// 已完成
        /// </summary>
        public decimal? DinCompletedCounts
        {
           get;
           set;
       }
        /// <summary>
        /// 完成率
        /// </summary>
        public decimal? DinCompletedRatio
        {
           get;
           set;
       }
        /// <summary>
        /// 对接接头总数
        /// </summary>
        public int? BWCounts
        {
           get;
           set;
       }
        /// <summary>
        /// 已焊接数
        /// </summary>
        public int? BWWeldedCounts
        {
           get;
           set;
       }
        /// <summary>
        /// 已检测数量
        /// </summary>
        public int? BWCheckedCounts
        {
           get;
           set;
       }
        /// <summary>
        /// 完成检测比例
        /// </summary>
        public decimal? BWCompletedCheckedRatio
        {
           get;
           set;
       }
        /// <summary>
        /// 固定口检测数量
        /// </summary>
        public int? BWFixedCheckedCounts
        {
           get;
           set;
       }
        /// <summary>
        /// 固定口检测比例
        /// </summary>
        public decimal? BWFixedCheckedRatio
        {
           get;
           set;
       }
        /// <summary>
        /// 焊工
        /// </summary>
        public string BWWelders
        {
           get;
           set;
       }
        /// <summary>
        /// 对接接头总数
        /// </summary>
        public int? SWCounts
        {
            get;
            set;
        }
        /// <summary>
        /// 已焊接数
        /// </summary>
        public int? SWWeldedCounts
        {
            get;
            set;
        }
        /// <summary>
        /// 已检测数量
        /// </summary>
        public int? SWCheckedCounts
        {
            get;
            set;
        }
        /// <summary>
        /// 完成检测比例
        /// </summary>
        public decimal? SWCompletedCheckedRatio
        {
            get;
            set;
        }
        /// <summary>
        /// 固定口检测数量
        /// </summary>
        public int? SWFixedCheckedCounts
        {
            get;
            set;
        }
        /// <summary>
        /// 固定口检测比例
        /// </summary>
        public decimal? SWFixedCheckedRatio
        {
            get;
            set;
        }
        /// <summary>
        /// 焊工
        /// </summary>
        public string SWWelders
        {
            get;
            set;
        }
        /// <summary>
        /// 对接接头总数
        /// </summary>
        public int? LETCounts
        {
            get;
            set;
        }
        /// <summary>
        /// 已焊接数
        /// </summary>
        public int? LETWeldedCounts
        {
            get;
            set;
        }
        /// <summary>
        /// 已检测数量
        /// </summary>
        public int? LETCheckedCounts
        {
            get;
            set;
        }
        /// <summary>
        /// 完成检测比例
        /// </summary>
        public decimal? LETCompletedCheckedRatio
        {
            get;
            set;
        }
        /// <summary>
        /// 焊工
        /// </summary>
        public string LETWelders
        {
            get;
            set;
        }
        /// <summary>
        /// 总拍片数
        /// </summary>
        public int? BWTotalFilm
        {
            get;
            set;
        }
        /// <summary>
        /// 合格片数
        /// </summary>
        public int? BWPassFilm
        {
            get;
            set;
        }
        /// <summary>
        /// 合格率
        /// </summary>
        public decimal? BWPassRatio
        {
            get;
            set;
        }
        /// <summary>
        /// 委托数
        /// </summary>
        public int? BWTrustCounts
        {
            get;
            set;
        }
        /// <summary>
        /// 已探口数
        /// </summary>
        public int? BWCheckedJotCounts
        {
            get;
            set;
        }
        /// <summary>
        /// 检测比例
        /// </summary>
        public decimal? BWsource_rate
        {
            get;
            set;
        }
        /// <summary>
        /// 委托比例
        /// </summary>
        public decimal? BWTrustRatio
        {
            get;
            set;
        }
        /// <summary>
        /// 已探比例
        /// </summary>
        public decimal? BWCheckedRatio
        {
            get;
            set;
        }
    }
}
