using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 焊工业绩分析
    /// </summary>
    public class SpRpWelderPerformance
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
        /// 焊工号
        /// </summary>
        public string wed_code
        {
            get;
            set;
        }
        /// <summary>
        /// 焊工名称
        /// </summary>
        public string wed_name
        {
            get;
            set;
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string WED_Sex
        {
            get;
            set;
        }
        /// <summary>
        /// 本期总达因值
        /// </summary>
        public decimal? nowtotal_din
        {
            get;
            set;
        }
        /// <summary>
        /// 本期总焊口数
        /// </summary>
        public int? nowtotal_jot
        {
            get;
            set;
        }
        /// <summary>
        /// 本期返口数
        /// </summary>
        public int? nowtotal_repairjot
        {
            get;
            set;
        }
        /// <summary>
        /// 本期返口率
        /// </summary>
        public decimal? nowrepairrate
        {
            get;
            set;
        }
        /// <summary>
        /// 本期成焊率
        /// </summary>
        public decimal? nowfinishedrate
        {
            get;
            set;
        }
        /// <summary>
        /// 本期拍片焊口数
        /// </summary>
        public int? current_count_film
        {
            get;
            set;
        }
        /// <summary>
        /// 本期拍片合格焊口数
        /// </summary>
        public int? current_pass_film
        {
            get;
            set;
        }

        /// <summary>
        ///本期拍片焊口合格率
        /// </summary>
        public decimal? current_passrate
        {
            get;
            set;
        }
        /// <summary>
        /// 本期拍片总数
        /// </summary>
        public int? nowtotalfilm
        {
            get;
            set;
        }
        /// <summary>
        /// 本期拍片合格数
        /// </summary>
        public int? nowpassfilm
        {
            get;
            set;
        }
        /// <summary>
        /// 本期拍片不合格数
        /// </summary>
        public int? nownotpassfilm
        {
            get;
            set;
        }
        /// <summary>
        /// 本期拍片合格率
        /// </summary>
        public decimal? nowpassrate
        {
            get;
            set;
        }
        /// <summary>
        /// 本期拍片不合格率
        /// </summary>
        public decimal? nowunpassrate
        {
            get;
            set;
        }
        /// <summary>
        /// 总达因值
        /// </summary>
        public decimal? totaldin
        {
            get;
            set;
        }
        /// <summary>
        /// 总焊口数
        /// </summary>
        public int? total_jot
        {
            get;
            set;
        }
        /// <summary>
        /// 总返口数
        /// </summary>
        public int? total_repairjot
        {
            get;
            set;
        }
        /// <summary>
        /// 反修率
        /// </summary>
        public decimal? repairrate
        {
            get;
            set;
        }
        /// <summary>
        /// 成焊率
        /// </summary>
        public decimal? finishedrate
        {
            get;
            set;
        }
        /// <summary>
        /// 总片数
        /// </summary>
        public int? totalfilm
        {
            get;
            set;
        }
        /// <summary>
        /// 合格片数
        /// </summary>
        public int? passfilm
        {
            get;
            set;
        }
        /// <summary>
        /// 不合格片数
        /// </summary>
        public int? notpassfilm
        {
            get;
            set;
        }
        /// <summary>
        /// 合格率
        /// </summary>
        public decimal? passrate
        {
            get;
            set;
        }
        /// <summary>
        /// 不合格率
        /// </summary>
        public decimal? unpassrate
        {
            get;
            set;
        }
       
        /// <summary>
        /// 所属班组
        /// </summary>
        public string education
        {
            get;
            set;
        }
        /// <summary>
        /// 在岗状态
        /// </summary>
        public bool? WED_IfOnGuard
        {
            get;
            set;
        }
    }
}
