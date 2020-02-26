using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 单位工区进度分析
    /// </summary>
    public class SpRptUnitBawAnalyze
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public string projectId
        {
            get;
            set;
        }

        /// <summary>
        /// 工区代码
        /// </summary>
        public string baw_areano
        {
            get;
            set;
        }
        /// <summary>
        /// 单位代码
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
        ///装置代号
        /// </summary>
        public string InstallationCode
        {
            get;
            set;
        }
        /// <summary>
        /// 装置名称
        /// </summary>
        public string InstallationName
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
        /// 预制总焊口数
        /// </summary>
        public int? total_sjot
        {
            get;
            set;
        }
        /// <summary>
        /// 安装总焊口数
        /// </summary>
        public int? total_fjot
        {
            get;
            set;
        }
        /// <summary>
        /// 总焊口
        /// </summary>
        public int? finished_total_jot
        {
            get;
            set;
        }
        /// <summary>
        /// 预制总焊口数
        /// </summary>
        public int? finished_total_sjot
        {
            get;
            set;
        }
        /// <summary>
        /// 安装总焊口数
        /// </summary>
        public int? finished_total_fjot
        {
            get;
            set;
        }
        /// <summary>
        /// 切除焊口
        /// </summary>
        public int? cut_total_jot
        {
            get;
            set;
        }
        /// <summary>
        /// 预制完成比例/焊口
        /// </summary>
        public decimal? finisedrate
        {
            get;
            set;
        }
        /// <summary>
        /// 预制完成比例/焊口
        /// </summary>
        public decimal? finisedrate_s
        {
            get;
            set;
        }
        /// <summary>
        ///   安装完成比例/焊口
        /// </summary>
        public decimal? finisedrate_f
        {
            get;
            set;
        }
        /// <summary>
        /// 总达因
        /// </summary>
        public decimal? total_din
        {
            get;
            set;
        }
        /// <summary>
        /// 预制总达因
        /// </summary>
        public decimal? total_Sdin
        {
            get;
            set;
        }
        /// <summary>
        /// 安装总达因
        /// </summary>
        public decimal? total_Fdin
        {
            get;
            set;
        }
        /// <summary>
        /// 总达因
        /// </summary>
        public decimal? finished_total_din
        {
            get;
            set;
        }
        /// <summary>
        /// 预制总达因
        /// </summary>
        public decimal? finished_total_Sdin
        {
            get;
            set;
        }
        /// <summary>
        /// 安装总达因
        /// </summary>
        public decimal? finished_total_Fdin
        {
            get;
            set;
        }
        /// <summary>
        /// 完成比例/达因
        /// </summary>
        public decimal? finisedrate_din
        {
            get;
            set;
        }
        /// <summary>
        /// 预制完成比例/达因
        /// </summary>
        public decimal? finisedrate_din_s
        {
            get;
            set;
        }
        /// <summary>
        /// 安装完成比例/达因
        /// </summary>
        public decimal? finisedrate_din_f
        {
            get;
            set;
        }
        /// <summary>
        /// 完成比例/焊口
        /// </summary>
        public decimal? finisedrate_bq
        {
            get;
            set;
        }
        /// <summary>
        /// 预制完成比例/焊口
        /// </summary>
        public decimal? finisedrate_s_bq
        {
            get;
            set;
        }
        /// <summary>
        /// 安装完成比例/焊口
        /// </summary>
        public decimal? finisedrate_f_bq
        {
            get;
            set;
        }
        /// <summary>
        /// 总达因
        /// </summary>
        public decimal? finished_total_din_bq
        {
            get;
            set;
        }
        /// <summary>
        /// 预制总达因
        /// </summary>
        public decimal? finished_total_Sdin_bq
        {
            get;
            set;
        }
        /// <summary>
        /// 安装总达因
        /// </summary>
        public decimal? finished_total_Fdin_bq
        {
            get;
            set;
        }
        /// <summary>
        /// 完成比例/达因
        /// </summary>
        public decimal? finisedrate_din_bq
        {
            get;
            set;
        }
        /// <summary>
        /// 预制完成比例/达因
        /// </summary>
        public decimal? finisedrate_din_s_bq
        {
            get;
            set;
        }
        /// <summary>
        /// 安装完成比例/达因
        /// </summary>
        public decimal? finisedrate_din_f_bq
        {
            get;
            set;
        }
        /// <summary>
        /// 本期总焊口
        /// </summary>
        public int? finished_total_jot_bq
        {
            get;
            set;
        }
        /// <summary>
        /// 本期预制总焊口数
        /// </summary>
        public int? finished_total_sjot_bq
        {
            get;
            set;
        }
        /// <summary>
        /// 本期安装总焊口数
        /// </summary>
        public int? finished_total_fjot_bq
        {
            get;
            set;
        }
    }
}
