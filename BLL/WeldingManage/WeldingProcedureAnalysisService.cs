using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
   public static class WeldingProcedureAnalysisService
    {
       public static Model.HJGLDB db = Funs.DB;

       /// <summary>
       /// 记录数
       /// </summary>
       private static int count
       {
           get;
           set;
       }
       /// <summary>
       /// 定义变量
       /// </summary>
       private static IQueryable<Model.View_WeldingProcedureAnalysis> qq = from x in db.View_WeldingProcedureAnalysis orderby x.WWI_Code select x;

       /// <summary>
       /// 获取列表
       /// </summary>
       /// <param name="iso_isoNo"></param>
       /// <param name="weldingProcedureId"></param>
       /// <param name="startRowIndex"></param>
       /// <param name="maximumRows"></param>
       /// <returns></returns>
       public static IEnumerable GetListData(string iso_isoNo, string weldingProcedureId, int startRowIndex, int maximumRows)
       {
           IQueryable<Model.View_WeldingProcedureAnalysis> q = qq;
           if (!string.IsNullOrEmpty(iso_isoNo))
           {
               q = q.Where(e => e.ISO_IsoNo.Contains(iso_isoNo));
           }
           if (!string.IsNullOrEmpty(weldingProcedureId))
           {
               q = q.Where(e => e.ProcedureCode.Contains(weldingProcedureId));
           }
           count = q.Count();
           if (count==0)
           {
               return new object[] { "" };
           }
           return from x in q.Skip(startRowIndex).Take(maximumRows)
                  select new
                  {
                      x.Numbers,
                      x.ProjectName,
                      x.UnitName,
                      x.JOT_JointNo,
                      x.ISO_IsoNo,
                      x.Specification,
                      x.WME_Name,
                      x.Welding,
                      x.PWHT,
                      x.ProcedureCode,
                      x.WeldPositionCode,
                      x.WWI_Code
                  };
       }
       /// <summary>
       /// 获取列表数
       /// </summary>
       /// <param name="iso_isoNo"></param>
       /// <param name="weldingProcedureId"></param>
       /// <returns></returns>
       public static int GetListCount(string iso_isoNo, string weldingProcedureId)
       {
           return count;
       }

    }
}
