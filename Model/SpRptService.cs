using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 介质综合分析
    /// </summary>
   public class SpRptService
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
       /// 单位ID
       /// </summary>
       public string bsu_id
       {
           get;
           set;
       }
       /// <summary>
       /// 工区ID
       /// </summary>
       public string baw_id
       {
           get;
           set;
       }
       /// <summary>
       /// 介质ID
       /// </summary>
       public string ser_id
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
       /// 工区代号
       /// </summary>
       public string baw_areano
       {
           get;
           set;
       }
       /// <summary>
       /// 介质代号
       /// </summary>
       public string ser_code
       {
           get;
           set;
       }
       /// <summary>
       /// 介质名称
       /// </summary>
       public string ser_name
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
       /// 预制焊口数
       /// </summary>
       public int? total_sjot
       {
           get;
           set;
       }
       /// <summary>
       /// 安装焊口数
       /// </summary>
       public int? total_fjot
       {
           get;
           set;
       }
       /// <summary>
       /// 完成焊口数
       /// </summary>
       public int? finished_jot
       {
           get;
           set;
       }
       /// <summary>
       /// 预制完成焊口数
       /// </summary>
       public int? finished_sjot
       {
           get;
           set;
       }
       /// <summary>
       /// 安装完成焊口数
       /// </summary>
       public int? finished_fjot
       {
           get;
           set;
       }
       /// <summary>
       /// 切除焊口数
       /// </summary>
       public int? cut_jot
       {
           get;
           set;
       }
       /// <summary>
       /// 完成比例
       /// </summary>
       public decimal? finisedrate
       {
           get;
           set;
       }
       /// <summary>
       /// 预制完成比例
       /// </summary>
       public decimal? finisedrate_s
       {
           get;
           set;
       }
       /// <summary>
       /// 安装完成比例
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
       /// 预制达因
       /// </summary>
       public decimal? total_sdin
       {
           get;
           set;
       }
       /// <summary>
       /// 安装达因
       /// </summary>
       public decimal? total_fdin
       {
           get;
           set;
       }
       /// <summary>
       /// 完成总达因
       /// </summary>
       public decimal? finished_din
       {
           get;
           set;
       }
       /// <summary>
       /// 预制完成总达因
       /// </summary>
       public decimal? finished_sdin
       {
           get;
           set;
       }
       /// <summary>
       /// 安装完成总达因
       /// </summary>
       public decimal? finished_fdin
       {
           get;
           set;
       }
       /// <summary>
       /// 达因完成比例
       /// </summary>
       public decimal? finishedrate_din
       {
           get;
           set;
       }
       /// <summary>
       /// 达因预制完成比例
       /// </summary>
       public decimal? finishedrate_sdin
       {
           get;
           set;
       }
       /// <summary>
       /// 达因安装完成比例
       /// </summary>
       public decimal? finishedrate_fdin
       {
           get;
           set;
       }
       /// <summary>
       /// 总拍片数
       /// </summary>
       public int? total_film
       {
           get;
           set;
       }
       /// <summary>
       /// 合格的片数
       /// </summary>
       public int? pass_film
       {
           get;
           set;
       }
       /// <summary>
       /// 合格率
       /// </summary>
       public decimal? passfilm_rate
       {
           get;
           set;
       }
       /// <summary>
       /// 扩透口数
       /// </summary>
       public int? ext_totalfilm
       {
           get;
           set;
       }
       /// <summary>
       /// 扩透合格口数
       /// </summary>
       public int? ext_passfilm
       {
           get;
           set;
       }
       /// <summary>
       /// 扩透合格率
       /// </summary>
       public decimal? ext_passrate
       {
           get;
           set;
       }
       /// <summary>
       /// 点口总数
       /// </summary>
       public int? point_totalfilm
       {
           get;
           set;
       }
       /// <summary>
       /// 点口合格总数
       /// </summary>
       public int? point_passfilm
       {
           get;
           set;
       }
       /// <summary>
       /// 点口合格率
       /// </summary>
       public decimal? point_passrate
       {
           get;
           set;
       }
       /// <summary>
       /// 切除总数
       /// </summary>
       public int? cut_totalfilm
       {
           get;
           set;
       }
       /// <summary>
       /// 切除合格总数
       /// </summary>
       public int? cut_passfilm
       {
           get;
           set;
       }
       /// <summary>
       /// 委托总数
       /// </summary>
       public int? trust_total_jot
       {
           get;
           set;
       }
       /// <summary>
       /// 委托扩透总数
       /// </summary>
       public int? trust_ext_total_jot
       {
           get;
           set;
       }
       /// <summary>
       ///  委托点口总数
       /// </summary>
       public int? trust_point_total_jot
       {
           get;
           set;
       }
       /// <summary>
       /// 已探口数
       /// </summary>
       public int? check_point_total_jot
       {
           get;
           set;
       }
       /// <summary>
       /// 返修口数
       /// </summary>
       public int? repair_jot
       {
           get;
           set;
       }      
    }
}
