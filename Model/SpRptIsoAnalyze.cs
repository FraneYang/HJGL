using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
   public class SpRptIsoAnalyze
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
       /// 管线ID
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
       /// 工区代号
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
       /// 最近焊期
       /// </summary>
       public DateTime? maxdate
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
       /// 预制总焊口
       /// </summary>
       public int? total_sjot
       {
           get;
           set;
       }
       /// <summary>
       /// 安装总焊口
       /// </summary>
       public int? total_fjot
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
       /// 预制完成总焊口数
       /// </summary>
       public int? finished_total_sjot
       {
           get;
           set;
       }
       /// <summary>
       /// 安装完成总焊口数
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
       /// 焊口总达因
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
       /// 完成总达因
       /// </summary>
       public decimal? finished_total_din
       {
           get;
           set;
       }
       /// <summary>
       /// 预制完成总达因
       /// </summary>
       public decimal? finished_total_Sdin
       {
           get;
           set;
       }
       /// <summary>
       /// 安装完成总达因
       /// </summary>
       public decimal? finished_total_Fdin
       {
           get;
           set;
       }
       /// <summary>
       /// 完成比例
       /// </summary>
       public decimal? finisedrate_din
       {
           get;
           set;
       }
       /// <summary>
       /// 预制完成比例
       /// </summary>
       public decimal? finisedrate_din_s
       {
           get;
           set;
       }
       /// <summary>
       /// 安装完成比例
       /// </summary>
       public decimal? finisedrate_din_f
       {
           get;
           set;
       }
       /// <summary>
       /// 总拍片数
       /// </summary>
       public int total_film
       {
           get;
           set;
       }
       /// <summary>
       /// 合格片数
       /// </summary>
       public int pass_film
       {
           get;
           set;
       }
       /// <summary>
       /// 合格率
       /// </summary>
       public decimal? passreate
       {
           get;
           set;
       }
       /// <summary>
       /// 扩透总片数
       /// </summary>
       public int ext_total_film
       {
           get;
           set;
       }
       /// <summary>
       /// 扩透合格片数
       /// </summary>
       public int ext_pass_film
       {
           get;
           set;
       }
       /// <summary>
       /// 扩透合格率
       /// </summary>
       public decimal? ext_passreate 
       {
           get;
           set; 
       }
       /// <summary>
       /// 点口总片数
       /// </summary>
       public int point_total_film
       {
           get;
           set;
       }
       /// <summary>
       /// 点口合格片数
       /// </summary>
       public int point_pass_film
       {
           get;
           set;
       }
       /// <summary>
       /// 点口合格率
       /// </summary>
       public decimal? point_passreate
       {
           get;
           set;
       }
       /// <summary>
       /// 切除总片数
       /// </summary>
       public int cut_total_film
       {
           get;
           set;
       }
       /// <summary>
       /// 切除合格片数
       /// </summary>
       public int cut_pass_film
       {
           get;
           set;
       }
      /// <summary>
      /// 扩透总数
      /// </summary>
       public int ext_jot
       {
           get;
           set;
       }
       /// <summary>
       /// 点口总数
       /// </summary>
       public int point_jot
       {
           get;
           set;
       }
       /// <summary>
       /// 委托总数
       /// </summary>
       public int trust_total_jot
       {
           get;
           set;
       }
       /// <summary>
       /// 总已探数
       /// </summary>
       public int check_total_jot
       {
           get;
           set;
       }
       /// <summary>
       /// 总返口数
       /// </summary>
       public int total_repairjot
       {
           get;
           set;
       }
       /// <summary>
       /// 要求比例
       /// </summary>
       public string source_rate
       {
           get;
           set;
       }
      
       /// <summary>
       /// 委托比例
       /// </summary>
       public decimal? trustrate
       {
           get;
           set;
       }
       /// <summary>
       /// 已探比例
       /// </summary>
       public decimal? checkrate
       {
           get;
           set;
       }

       /// <summary>
       /// 固定口检测比例
       /// </summary>
       public decimal? FixedCheckRate
       {
           get;
           set;
       }
    }
}
