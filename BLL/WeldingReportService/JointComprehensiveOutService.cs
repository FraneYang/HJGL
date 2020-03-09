using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace BLL
{
    /// <summary>
    /// 焊口综合信息
    /// </summary>
    public static class JointComprehensiveOutService
    {
        /// <summary>
        /// 记录数
        /// </summary>
        public static int count
        {
            get;
            set;
        }

        /// <summary>
        /// 获取列表值
        /// </summary>
        /// <param name="values"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string values, int startRowIndex, int maximumRows)
        {
            List<string> listValues = Funs.GetStrListByStr(values, ',');
            if (listValues.Count < 5)
            {
                count = 0;
                return new object[] { "" };
            }
            else
            {
                if (listValues[0] == "0")
                {
                    listValues[0] = null;
                }
                if (listValues[1] == "0")
                {
                    listValues[1] = null;
                }
                if (string.IsNullOrEmpty(listValues[2]))
                {
                    listValues[2] = null;
                }
                if (string.IsNullOrEmpty(listValues[3]))
                {
                    listValues[3] = null;
                }
                if (string.IsNullOrEmpty(listValues[4]))
                {
                    listValues[4] = null;
                }
                //IEnumerable<Model.SpRptJointComprehensiveOutItem> qq = Funs.DB.SpJointComprehensiveOut(listValues[0], listValues[1], listValues[2], listValues[3], listValues[4]);
                var q = Funs.DB.SpJointComprehensiveOut(listValues[0], listValues[1], listValues[2], listValues[3], listValues[4]).ToList();
                count = q.Count();
                if (count == 0)
                {
                    return new object[] { "" };
                }
                return from x in q.Skip(startRowIndex).Take(maximumRows)
                       select new
                       {
                           x.ProjectId,
                           x.WorkAreaId,
                           x.SupervisorUnitId,
                           x.UnitCode,
                           x.WorkAreaCode,
                           x.ISO_IsoNo,
                           x.JOT_JointNo,
                           x.STE_Code,
                           x.STE_Code2,
                           x.NDTR_Code,
                           x.JOTY_Code,
                           x.WLO_CodeName,
                           x.JOT_JointAttribute,
                           x.JOT_Size,
                           x.JOT_JointDesc,
                           x.JOT_Sch,
                           x.WME_Code,
                           x.ISO_TestPress,
                           x.WMT_MatCode,
                           x.WMT_MatCode2,
                           x.SER_Code,
                           x.ISO_IsoNumber,
                           x.ISO_DesignPress,
                           x.ISO_DesignTemperature,
                           x.JST_Code,
                           x.ISC_IsoCode,
                           x.COM_Code,
                           x.COM_Code2,
                           x.JOT_HeartNo1,
                           x.JOT_HeartNo2,
                           x.JOT_BelongPipe,
                           x.JOT_PrepareTemp,
                           x.IS_ProessName,
                           x.JOT_HotRpt,
                           x.JOT_Location,
                           x.JOT_Dia,
                           x.ISO_HardnessRateName,
                           x.NDT_Code,
                           x.ISO_NDTClass,
                           x.IsBigName,
                           x.PipeNumber,
                           x.ISO_CwpNo,
                           x.JOT_DailyReportNo,
                           x.JOT_WeldDate,
                           x.WED_Code,
                           x.WED_Code2,
                           x.PW_PointNo,
                           x.PW_PointDate,
                           x.CH_TrustCode,
                           x.CH_TrustDate,
                           x.CHT_CheckCode,
                           x.CHT_CheckDate,
                           x.CHT_FilmDate,
                           x.CHT_ReportDate,
                           x.CHT_TotalFilm,
                           x.CHT_PassFilm,
                           x.CHT_CheckNo,
                           x.CHT_FilmSpecifications
                       };
            }
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static int GetListCount(string values)
        {
            return count;
        }
    }
}
