using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 单位工区质量分析
    /// </summary>
  public class SpRptBawZlfx
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
      /// 装置代码
      /// </summary>
      public string devicecode
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
      /// 工区代码
      /// </summary>
      public string baw_areano
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
      /// 完成预制总焊口
      /// </summary>
      public int? finished_total_sjot
      {
          get;
          set;
      }
      /// <summary>
      /// 完成安装总焊口
      /// </summary>
      public int? finished_total_fjot
      {
          get;
          set;
      }
      /// <summary>
      /// 本期总拍片数
      /// </summary>
      public int? current_total_film
      {
          get;
          set;
      }
      /// <summary>
      /// 本期合格片数
      /// </summary>
      public int? current_pass_film
      {
          get;
          set;
      }
      /// <summary>
      /// 本期合格率
      /// </summary>
      public decimal? current_passreate
      {
          get;
          set;
      }
      /// <summary>
      /// 本期点口片数
      /// </summary>
      public int? current_point_total_film
      {
          get;
          set;
      }
      /// <summary>
      /// 本期点口合格片数
      /// </summary>
      public int? current_point_pass_film
      {
          get;
          set;
      }
      /// <summary>
      /// 本期点口合格率
      /// </summary>
      public decimal? cuurent_point_passreate
      {
          get;
          set;
      }
      /// <summary>
      /// 本期扩透总片数
      /// </summary>
      public int? current_ext_total_film
      {
          get;
          set;
      }
      /// <summary>
      /// 本期扩透合格片数
      /// </summary>
      public int? current_ext_pass_film
      {
          get;
          set;
      }
      /// <summary>
      /// 本期扩透合格率
      /// </summary>
      public decimal? current_ext_passreate
      {
          get;
          set;
      }
      /// <summary>
      /// 本期总委托数
      /// </summary>
      public int? current_trust_count_total
      {
          get;
          set;
      }
      /// <summary>
      /// 本期总检测数
      /// </summary>
      public int? current_check_count_total
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
      /// 合格片数
      /// </summary>
      public int? pass_film
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
      /// 点口总片数
      /// </summary>
      public int? point_total_film
      {
          get;
          set;
      }
      /// <summary>
      /// 点口合格片数
      /// </summary>
      public int? point_pass_film
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
      /// 扩透总片数
      /// </summary>
      public int? ext_total_film
      {
          get;
          set;
      }
      /// <summary>
      /// 扩透合格片数
      /// </summary>
      public int? ext_pass_film
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
      /// 委托总数
      /// </summary>
      public int? trust_count_total
      {
          get;
          set;
      }
      /// <summary>
      /// 点口总焊口数
      /// </summary>
      public int? point_count_total
      {
          get;
          set;
      }
      /// <summary>
      /// 扩透总焊口数
      /// </summary>
      public int? extend_count_total
      {
          get;
          set;
      }
      /// <summary>
      /// 返修总焊口数
      /// </summary>
      public int? repair_count_total
      {
          get;
          set;
      }
      /// <summary>
      /// 检测口数
      /// </summary>
      public int? trust_check_total
      {
          get;
          set;
      }
    }
}
