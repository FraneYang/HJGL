using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 管线综合信息
    /// </summary>
    public class TestPackagePipelineAnalysisService
    {

        public static Model.HJGLDB db = Funs.DB;

        /// <summary>
        /// 记录数
        /// </summary>
        public static int count
        {
            get;
            set;
        }

        
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData( string projectId, string flag, int startRowIndex, int maximumRows)
        {
            if (flag != "1" || string.IsNullOrEmpty(projectId))
            {
                count = 0;
                return new object[] { "" };
            }
            else
            {
                IQueryable<Model.SpTestPackagePipelineAnalysisItem> q = GetTestPackagePipelineAnalysisList(projectId, flag).AsQueryable();
              count = q.Count();
                if (count == 0)
                {
                    return new object[] { "" };
                }
                return from x in q.Skip(startRowIndex).Take(maximumRows)
                       orderby x.PTP_TestPackageNo, x.BWsource_rate, x.ISO_IsoNo
                       select new
                       {
                           x.ProjectId,
                           x.ISO_Id,
                           x.ISO_IsoNo,
                           x.PTP_TestPackageNo,
                           x.maxdate,
                           x.JotCounts,
                           x.JotCompletedCounts,
                           x.JotCompletedRatio,
                           x.DinCounts,
                           x.DinCompletedCounts,
                           x.DinCompletedRatio,
                           BWCounts = x.BWCounts ?? 0,
                           BWWeldedCounts = x.BWWeldedCounts ?? 0,
                           BWCheckedCounts = x.BWCheckedCounts ?? 0,
                           BWCompletedCheckedRatio = x.BWCompletedCheckedRatio ?? 0,
                           BWFixedCheckedCounts = x.BWFixedCheckedCounts ?? 0,
                           BWFixedCheckedRatio = x.BWFixedCheckedRatio ?? 0,
                           x.BWWelders,
                           SWCounts = x.SWCounts ?? 0,
                           SWWeldedCounts = x.SWWeldedCounts ?? 0,
                           SWCheckedCounts = x.SWCheckedCounts ?? 0,
                           SWCompletedCheckedRatio = x.SWCompletedCheckedRatio ?? 0,
                           SWFixedCheckedCounts = x.SWFixedCheckedCounts ?? 0,
                           SWFixedCheckedRatio = x.SWFixedCheckedRatio ?? 0,
                           x.SWWelders,
                           LETCounts = x.LETCounts ?? 0,
                           LETWeldedCounts = x.LETWeldedCounts ?? 0,
                           LETCheckedCounts = x.LETCheckedCounts ?? 0,
                           LETCompletedCheckedRatio = x.LETCompletedCheckedRatio ?? 0,
                           x.LETWelders,
                           BWTotalFilm = x.BWTotalFilm ?? 0,
                           BWPassFilm = x.BWPassFilm ?? 0,
                           BWPassRatio = x.BWPassRatio ?? 0,
                           BWTrustCounts = x.BWTrustCounts ?? 0,
                           BWCheckedJotCounts = x.BWCheckedJotCounts ?? 0,
                           BWsource_rate = x.BWsource_rate ?? 0,
                           BWTrustRatio = x.BWTrustRatio ?? 0,
                           BWCheckedRatio = x.BWCheckedRatio ?? 0,
                       };
            }
        }
        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static int GetListCount(string projectId, string flag)
        {
            return count;
        }

        /// <summary>
        ///  获取分析方法
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public static List<Model.SpTestPackagePipelineAnalysisItem> GetTestPackagePipelineAnalysisList(string projectId, string flag)
        {
            List<Model.SpTestPackagePipelineAnalysisItem> getSpItemLists = new List<Model.SpTestPackagePipelineAnalysisItem>();
            var getTestPackages = from x in Funs.DB.TP_TestPackage
                                  where x.ProjectId == projectId
                                  //&& x.PTP_TestPackageNo == "3-OH-3522-2AHA"
                                  select x;
            var getISOs = from x in Funs.DB.PW_IsoInfo
                          where x.ProjectId == projectId
                          select x;
            var getWelders = Funs.DB.BS_Welder.Where(x => x.ProjectId == projectId).ToList();
            if (getTestPackages.Count() > 0)
            {
                foreach (var testItem in getTestPackages)
                {
                    List<string> getISO_IDLists = (from x in Funs.DB.TP_IsoList
                                                   where x.PTP_ID == testItem.PTP_ID
                                                   select x.ISO_ID).ToList();
                    if (getISO_IDLists.Count() > 0)
                    {
                        //// 焊接比例
                        var getBS_NDTRate = from x in Funs.DB.BS_NDTRate select x;
                        //// 对接
                        string BWJOTY_Id = string.Empty;
                        var getJointTypeBW = Funs.DB.BS_JointType.FirstOrDefault(x => x.JOTY_Code == "BW");
                        if (getJointTypeBW != null)
                        {
                            BWJOTY_Id = getJointTypeBW.JOTY_ID;
                        }
                        //// 支管
                        string LETJOTY_Id = string.Empty;
                        var getJointTypeLET = Funs.DB.BS_JointType.FirstOrDefault(x => x.JOTY_Code == "LET");
                        if (getJointTypeLET != null)
                        {
                            LETJOTY_Id = getJointTypeLET.JOTY_ID;
                        }
                        //// 角焊缝
                        string SWJOTY_Id = string.Empty;
                        var getJointTypeSW = Funs.DB.BS_JointType.FirstOrDefault(x => x.JOTY_Code == "SW");
                        if (getJointTypeSW != null)
                        {
                            SWJOTY_Id = getJointTypeSW.JOTY_ID;
                        }
                        //// 管线集合
                        var getIsoInfos = from x in getISOs where  getISO_IDLists.Contains(x.ISO_ID) select x;
                        foreach (var isoItem in getIsoInfos)
                        {
                            Model.SpTestPackagePipelineAnalysisItem newItem = new Model.SpTestPackagePipelineAnalysisItem
                            {
                                ProjectId = isoItem.ProjectId,
                                ISO_Id = isoItem.ISO_ID,
                                ISO_IsoNo = isoItem.ISO_IsoNo,
                                PTP_TestPackageNo = testItem.PTP_TestPackageNo,
                            };
                            var getndtrate = getBS_NDTRate.FirstOrDefault(x => x.NDTR_ID == isoItem.NDTR_ID);
                            if (getndtrate != null)
                            {
                                newItem.BWsource_rate = Funs.GetNewDecimal(getndtrate.NDTR_Rate);
                            }
                            //// 所以焊口集合
                            var getJots = from x in Funs.DB.PW_JointInfo where x.ISO_ID == isoItem.ISO_ID select x;
                            if (getJots.Count() > 0)
                            {
                                newItem.JotCounts = getJots.Count();
                                newItem.DinCounts = getJots.Sum(x => x.JOT_Size ?? 0);
                                //// 已焊接焊口集合
                                var getWeldJots = getJots.Where(x => x.DReportID != null).ToList();
                                newItem.JotCompletedCounts = getWeldJots.Count();
                                newItem.DinCompletedCounts = getWeldJots.Sum(x => x.JOT_Size ?? 0);

                                newItem.JotCompletedRatio = Convert.ToDecimal(Math.Round((newItem.JotCompletedCounts * 1.0 / (newItem.JotCounts * 1.0) * 100) ?? 0, 2));
                                newItem.DinCompletedRatio = Convert.ToDecimal(Math.Round((newItem.DinCompletedCounts / newItem.DinCounts * 100) ?? 0, 2));
                                var getDReportIDList = getWeldJots.Select(x => x.DReportID).ToList();
                                if (getWeldJots.Count() > 0)
                                {
                                    var getMaxDate = (from x in Funs.DB.BO_WeldReportMain
                                                      where getDReportIDList.Contains(x.DReportID)
                                                      select x.JOT_WeldDate).Max();
                                    newItem.maxdate = getMaxDate;
                                }
                                /// BW 焊口
                                var getBWJots = getJots.Where(x => x.JOTY_ID == BWJOTY_Id);
                                var getBWWeldJots = getWeldJots.Where(x => x.JOTY_ID == BWJOTY_Id);
                                var getBWWeldJotIdLists = getBWWeldJots.Select(x => x.JOT_ID).ToList();
                                newItem.BWCounts = getBWJots.Count();
                                if (newItem.BWCounts > 0)
                                {
                                    newItem.BWWeldedCounts = getBWWeldJots.Count();
                                    var getBWCheckJots = getBWJots.Where(x => x.JOT_CheckFlag != null && x.JOT_CheckFlag != "00");
                                    newItem.BWCheckedCounts = getBWCheckJots.Count();
                                    newItem.BWCompletedCheckedRatio = Convert.ToDecimal(Math.Round((newItem.BWCheckedCounts * 1.0 / newItem.BWCounts * 100) ?? 0, 2));

                                    var getBWGJots = getBWJots.Where(x => x.JOT_JointAttribute == "固定");
                                    if (getBWGJots.Count() > 0)
                                    {
                                        var getBWGCheckJots = getBWCheckJots.Where(x => x.JOT_JointAttribute == "固定");
                                        newItem.BWFixedCheckedCounts = getBWGCheckJots.Count();
                                        newItem.BWFixedCheckedRatio = Convert.ToDecimal(Math.Round((newItem.BWFixedCheckedCounts * 1.0 / getBWGJots.Count() * 100) ?? 0, 2));
                                    }
                                    newItem.BWWelders = getWelds(getBWJots.ToList(), projectId, getWelders);
                                }
                                if (getBWWeldJots.Count() > 0)
                                {
                                    var getCheckItem = Funs.DB.CH_CheckItem.Where(x => getBWWeldJotIdLists.Contains(x.JOT_ID));
                                    if (getCheckItem.Count() > 0)
                                    {
                                        newItem.BWTotalFilm = getCheckItem.Sum(x => x.CHT_TotalFilm ?? 0);
                                        newItem.BWPassFilm = getCheckItem.Sum(x => x.CHT_PassFilm ?? 0);
                                        if (newItem.BWTotalFilm > 0)
                                        {
                                            newItem.BWPassRatio = Convert.ToDecimal(Math.Round((newItem.BWPassFilm * 1.0 / newItem.BWTotalFilm * 100) ?? 0, 2));
                                        }
                                    }
                                    newItem.BWCheckedJotCounts = getCheckItem.Count();
                                    var getBWTrustJots = getBWWeldJots.Where(x => x.JOT_TrustFlag != null && x.JOT_TrustFlag != "00");
                                    newItem.BWTrustCounts = getBWTrustJots.Count();
                                    newItem.BWTrustRatio = Convert.ToDecimal(Math.Round((newItem.BWTrustCounts * 1.0 / newItem.BWCounts * 100) ?? 0, 2));
                                    newItem.BWCheckedRatio = Convert.ToDecimal(Math.Round((newItem.BWCheckedCounts * 1.0 / newItem.BWCounts * 100) ?? 0, 2));
                                }

                                /// SW 焊口
                                var getSWJots = getJots.Where(x => x.JOTY_ID == SWJOTY_Id);
                                var getSWWeldJots = getWeldJots.Where(x => x.JOTY_ID == SWJOTY_Id);
                                newItem.SWCounts = getSWJots.Count();
                                if (newItem.SWCounts > 0)
                                {
                                    newItem.SWWeldedCounts = getSWWeldJots.Count();
                                    var getSWCheckJots = getSWJots.Where(x => x.JOT_CheckFlag != null && x.JOT_CheckFlag != "00");
                                    newItem.SWCheckedCounts = getSWCheckJots.Count();
                                    newItem.SWCompletedCheckedRatio = Convert.ToDecimal(Math.Round((newItem.SWCheckedCounts * 1.0 / newItem.SWCounts * 100) ?? 0, 2));

                                    var getSWGJots = getSWJots.Where(x => x.JOT_JointAttribute == "固定");
                                    if (getSWGJots.Count() > 0)
                                    {
                                        var getSWGCheckJots = getSWCheckJots.Where(x => x.JOT_JointAttribute == "固定");
                                        newItem.SWFixedCheckedCounts = getSWGCheckJots.Count();
                                        newItem.SWFixedCheckedRatio = Convert.ToDecimal(Math.Round((newItem.SWFixedCheckedCounts * 1.0 / getSWGJots.Count() * 100) ?? 0, 2));
                                    }

                                    newItem.SWWelders = getWelds(getSWJots.ToList(), projectId, getWelders);
                                }
                                /// LET 焊口
                                var getLETJots = getJots.Where(x => x.JOTY_ID == LETJOTY_Id);
                                var getLETWeldJots = getWeldJots.Where(x => x.JOTY_ID == LETJOTY_Id);
                                newItem.LETCounts = getLETJots.Count();
                                if (newItem.LETCounts > 0)
                                {
                                    newItem.LETWeldedCounts = getLETWeldJots.Count();
                                    var getLETCheckJots = getLETJots.Where(x => x.JOT_CheckFlag != null && x.JOT_CheckFlag != "00");
                                    newItem.LETCheckedCounts = getLETCheckJots.Count();
                                    newItem.LETCompletedCheckedRatio = Convert.ToDecimal(Math.Round((newItem.LETCheckedCounts * 1.0 / newItem.LETCounts * 100) ?? 0, 2));
                                    newItem.LETWelders = getWelds(getLETJots.ToList(), projectId, getWelders);
                                }
                            }

                            getSpItemLists.Add(newItem);
                        }
                    }
                }
            }

            return getSpItemLists;
        }

        /// <summary>
        ///  获取项目ID
        /// </summary>
        /// <param name="getBWJots"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public static string getWelds(List<Model.PW_JointInfo> getBWJots, string projectId,List<Model.BS_Welder> getWelders)
        {
            string welders = string.Empty;           
            foreach (var item in getBWJots)
            {
                var getCellWelder = getWelders.FirstOrDefault(x => x.WED_ID == item.JOT_CellWelder);
                if (getCellWelder != null && !welders.Contains(getCellWelder.WED_Code))
                {
                    var getJotIdLists = getBWJots.FirstOrDefault(x =>x.PW_PointID != null && (x.JOT_CellWelder == getCellWelder.WED_ID || x.JOT_FloorWelder == getCellWelder.WED_ID));
                    if (getJotIdLists != null)
                    {
                        welders += getCellWelder.WED_Code +",";
                    }
                    else
                    {
                        welders += getCellWelder.WED_Code + "*,";
                    }
                }

                var getFloorWelder = getWelders.FirstOrDefault(x => x.WED_ID == item.JOT_FloorWelder);
                if (getFloorWelder != null && !welders.Contains(getFloorWelder.WED_Code))
                {
                    var getJotIdLists = getBWJots.FirstOrDefault(x => x.PW_PointID != null && (x.JOT_CellWelder == getFloorWelder.WED_ID || x.JOT_FloorWelder == getFloorWelder.WED_ID));
                    if (getJotIdLists != null)
                    {
                        welders += getFloorWelder.WED_Code + ",";
                    }
                    else
                    {
                        welders += getFloorWelder.WED_Code + "*,";
                    }
                }
            }
            return welders;
        }
    }
}
