using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 预制安装进度
    /// </summary>
    public class SpRptIsoYzazjd
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
        /// 管线号
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
        /// 施工区域
        /// </summary>
        public string baw_areano
        {
            get;
            set;
        }
        /// <summary>
        /// 材质代号
        /// </summary>
        public string ste_stecode
        {
            get;
            set;
        }
        /// <summary>
        /// 材质名称
        /// </summary>
        public string ste_stename
        {
            get;
            set;
        }
        /// <summary>
        /// 最大尺寸
        /// </summary>
        public decimal? max_din
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
        /// 完成总达因
        /// </summary>
        public decimal? finished_total_din
        {
            get;
            set;
        }
        /// <summary>
        /// 完成进度比例
        /// </summary>
        public decimal? finisedrate_din
        {
            get;
            set;
        }
        /// <summary>
        /// 预制总量
        /// </summary>
        public decimal? total_Sdin
        {
            get;
            set;
        }
        /// <summary>
        /// 预制完成
        /// </summary>
        public decimal? finished_total_Sdin
        {
            get;
            set;
        }
        /// <summary>
        /// 预制进度比例
        /// </summary>
        public decimal? finisedrate_din_s
        {
            get;
            set;
        }
        /// <summary>
        /// 安装总量
        /// </summary>
        public decimal? total_Fdin
        {
            get;
            set;
        }
        /// <summary>
        ///安装完成总量
        /// </summary>
        public decimal? finished_total_Fdin
        {
            get;
            set;
        }

        /// <summary>
        /// 安装进度比例
        /// </summary>
        public decimal? finisedrate_din_f
        {
            get;
            set;
        }
    }
}
