using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace BLL
{
    /// <summary>
    /// 焊口信息初始化
    /// </summary>
    public static class PW_JointInfoService
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
        /// 记录数2
        /// </summary>
        public static int count2
        {
            get;
            set;
        }

        /// <summary>
        /// 记录数3
        /// </summary>
        public static int count3
        {
            get;
            set;
        }
        /// <summary>
        /// 定义变量
        /// </summary>
        private static IQueryable<Model.View_JointInfo> qq = from x in db.View_JointInfo orderby x.JOT_JointNo orderby x.JOT_WeldDate descending select x;

        /// <summary>
        /// 获取焊口列表
        /// </summary>
        /// <param name="projectId">项目</param>
        /// <param name="jointNo">焊口号</param>
        /// <param name="iso_id">管线</param>
        /// <param name="wlo_Code">焊接区域（安装/预制）</param>
        /// <param name="jointDesc">焊口规格</param>
        /// <param name="joty_id">焊缝类型</param>
        /// <param name="wme_id">焊接方法</param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData(string projectId, string workAreaId, string jointNo, string iso_id, string wlo_Code, string jointDesc, string joty_id, string wme_id, string DReportID, string PW_PointID, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.View_JointInfo> q = qq;

            if (!string.IsNullOrEmpty(projectId))
            {
                q = q.Where(e => e.ProjectId == projectId);
            }

            if (!string.IsNullOrEmpty(workAreaId))
            {
                q = q.Where(e => e.BAW_ID == workAreaId);
            }

            if (!string.IsNullOrEmpty(jointNo))
            {
                q = q.Where(e => e.JOT_JointNo == jointNo);
            }
            if (!string.IsNullOrEmpty(iso_id))
            {
                q = q.Where(e => e.ISO_ID == iso_id);
            }
            if (!string.IsNullOrEmpty(wlo_Code))
            {
                q = q.Where(e => e.WLO_Code == wlo_Code);
            }
            if (!string.IsNullOrEmpty(jointDesc))
            {
                q = q.Where(e => e.JOT_JointDesc == jointDesc);
            }
            if (!string.IsNullOrEmpty(joty_id))
            {
                q = q.Where(e => e.JOTY_ID == joty_id);
            }
            if (!string.IsNullOrEmpty(wme_id))
            {
                q = q.Where(e => e.WME_ID == wme_id);
            }
            if (!string.IsNullOrEmpty(DReportID))
            {
                if (DReportID == "1")
                {
                    q = q.Where(e => e.JOT_DailyReportNo != null);
                }
                else
                {
                    q = q.Where(e => e.JOT_DailyReportNo == null);
                }
            }
            if (!string.IsNullOrEmpty(PW_PointID))
            {
                if (PW_PointID == "1")
                {
                    q = q.Where(e => e.PointNo != null);
                }
                else
                {
                    q = q.Where(e => e.PointNo == null);
                }
            }

            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }
            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.JOT_ID,
                       x.ProjectId,
                       x.JOT_JointNo,
                       x.JOT_DailyReportNo,
                       x.JOT_WeldDate,
                       x.ISO_ID,
                       x.ISO_IsoNo,
                       x.STE_Name1,
                       x.STE_Name2,
                       x.WED_Code1,
                       x.WED_Name1,
                       x.WED_Code2,
                       x.WED_Name2,
                       x.WLO_Code,
                       x.WLO_Name,
                       x.JOT_DoneDin,
                       x.FloorGroup,
                       x.CellGroup,
                       x.IS_Compute,
                       x.JOT_NDTResult,
                       x.Component1,
                       x.Component2,
                       x.JOT_HeartNo1,
                       x.JOT_HeartNo2,
                       x.WeldMat,
                       x.JointStatus,
                       x.JOT_FaceCheckResult,
                       x.JOT_FaceCheckDate,
                       x.JOT_PHWTDate,
                       x.JOT_PHWTReportNo,
                       x.JOT_PHWTResult,
                       x.JOT_FaceChecker,
                       x.JOT_Dia,
                       x.JOT_Size,
                       x.JOT_Sch,
                       x.JOT_FactSch,
                       x.JOT_JointFlag,
                       x.JOT_TrustFlag,
                       x.JOT_BecauseJointNo,
                       x.JOT_JointDesc,
                       x.WeldSilk,
                       x.JOTY_ID,
                       x.JOTY_Name,
                       x.JOT_CheckFlag,
                       x.JOT_RepairFlag,
                       x.WME_ID,
                       x.WME_Name,
                       x.JST_Name,
                       x.JOT_BelongPipe,
                       x.JOT_CheckResult,
                       x.JOT_Electricity,
                       x.JOT_Voltage,
                       IS_Proess = x.IS_Proess == "1" ? "是" : "否",
                       x.JOT_ProessDate,
                       x.JOT_HotRpt,
                       x.JOT_PrepareTemp,
                       x.JOT_CellTemp,
                       x.JOT_LastTemp,
                       x.JOT_JointAttribute,
                       x.JOT_CellWeldRules,
                       x.JOT_FloorWeldRules,
                       x.FloorWeld1,
                       x.CellWelder1,
                       x.FloorWeld2,
                       x.CellWelder2,
                       x.PointNo,
                       x.PointDate,
                       x.CH_TrustCode,
                       x.CH_TrustDate,
                       x.NDT_Name,
                       x.RePairNo1,
                       x.RePairDate1,
                       x.RePairNo2,
                       x.RePairDate2,
                       x.Fix1_date,
                       x.Fix2_date,
                       x.JOT_Remark,
                       x.WorkAreaCode,
                       x.NDTR_Name,
                       x.Is_hj,
                       x.If_dk
                   };
        }

        /// <summary>
        /// 获取焊口列表数
        /// </summary>
        /// <param name="projectId">项目</param>
        /// <param name="jointNo">焊口号</param>
        /// <param name="iso_id">管线</param>
        /// <param name="wlo_Code">焊接区域（安装/预制）</param>
        /// <param name="jointDesc">焊口规格</param>
        /// <param name="joty_id">焊缝类型</param>
        /// <param name="wme_id">焊接方法</param>
        /// <returns></returns>
        public static int GetListCount(string projectId, string workAreaId, string jointNo, string iso_id, string wlo_Code, string jointDesc, string joty_id, string wme_id, string DReportID, string PW_PointID)
        {
            return count;
        }

        ///// <summary>
        ///// 定义变量:焊接日报
        ///// </summary>
        //private static IQueryable<Model.PW_JointInfo> qq2 = from x in db.PW_JointInfo 
        //                                                    where x.DReportID == null orderby x.JOT_JointNo select x;

        public static IEnumerable GetListData2(string iso_id, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.View_JointInfoReportSearch> q2 = from x in db.View_JointInfoReportSearch 
                                                where x.ISO_ID == iso_id
                                                orderby x.JOT_JointNo
                                                select x;

            count2 = q2.Count();
            if (count2 == 0)
            {
                return new object[] { "" };
            }
            return from x in q2.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.JOT_ID,
                       x.ISO_ID,
                       x.JOT_JointNo,
                       x.ISO_IsoNo,
                       x.ISO_Sheel,
                       x.STE_Name1,
                       x.STE_Name2,
                       x.JOT_JointDesc
                   };
        }

        public static int GetListCount2(string iso_id)
        {
            return count2;
        }

        /// <summary>
        /// 定义变量：点口
        /// </summary>
        private static IQueryable<Model.PW_JointInfo> qq3 = from x in db.PW_JointInfo where x.PW_PointID == null && x.DReportID != null orderby x.JOT_JointNo select x;

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="iso_id"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable GetListData3(string iso_id, string dreportId, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.PW_JointInfo> q3 = qq3;
            if (!string.IsNullOrEmpty(iso_id))
            {
                q3 = q3.Where(e => e.ISO_ID == iso_id);
            }
            if (!string.IsNullOrEmpty(dreportId))
            {
                q3 = q3.Where(e => e.DReportID == dreportId);
            }
            count3 = q3.Count();
            if (count3 == 0)
            {
                return new object[] { "" };
            }
            return from x in q3.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.JOT_ID,
                       x.ISO_ID,
                       x.JOT_JointNo,
                       x.DReportID,
                       JOT_CellWelder = (from y in db.BS_Welder where x.JOT_CellWelder == y.WED_ID select y.WED_Code).First(),
                       JOT_FloorWelder = (from y in db.BS_Welder where x.JOT_FloorWelder == y.WED_ID select y.WED_Code).First(),
                       JointType = (from y in db.BS_JointType where x.JOTY_ID == y.JOTY_ID select y.JOTY_Name).First(),
                   };
        }
        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <param name="iso_id"></param>
        /// <returns></returns>
        public static int GetListCount3(string iso_id, string dreportId)
        {
            return count3;
        }

        /// <summary>
        /// 根据焊口Id获取焊口信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static Model.PW_JointInfo GetJointInfoByJotID(string jot_id)
        {
            return Funs.DB.PW_JointInfo.FirstOrDefault(e => e.JOT_ID == jot_id);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="jointInfo"></param>
        public static void AddJointInfo(Model.PW_JointInfo jointInfo)
        {
            Model.HJGLDB db = Funs.DB;
            string newKeyID = SQLHelper.GetNewID(typeof(Model.PW_JointInfo));
            Model.PW_JointInfo newJointInfo = new Model.PW_JointInfo();

            newJointInfo.JOT_ID = newKeyID;
            newJointInfo.ProjectId = jointInfo.ProjectId;
            newJointInfo.JOT_JointNo = jointInfo.JOT_JointNo;
            newJointInfo.DReportID = jointInfo.DReportID;
            newJointInfo.ISO_ID = jointInfo.ISO_ID;
            newJointInfo.STE_ID = jointInfo.STE_ID;
            newJointInfo.STE_ID2 = jointInfo.STE_ID2;
            newJointInfo.JOT_CellWelder = jointInfo.JOT_CellWelder;
            newJointInfo.JOT_FloorWelder = jointInfo.JOT_FloorWelder;
            newJointInfo.WLO_Code = jointInfo.WLO_Code;
            newJointInfo.JOT_DoneDin = jointInfo.JOT_DoneDin;
            newJointInfo.JOT_FloorGroup = jointInfo.JOT_FloorGroup;
            newJointInfo.JOT_CellGroup = jointInfo.JOT_CellGroup;
            newJointInfo.IS_Compute = jointInfo.IS_Compute;
            newJointInfo.JOT_NDTResult = jointInfo.JOT_NDTResult;
            newJointInfo.JOT_Component1 = jointInfo.JOT_Component1;
            newJointInfo.JOT_Component2 = jointInfo.JOT_Component2;
            newJointInfo.JOT_HeartNo1 = jointInfo.JOT_HeartNo1;
            newJointInfo.JOT_HeartNo2 = jointInfo.JOT_HeartNo2;
            newJointInfo.JOT_WeldMat = jointInfo.JOT_WeldMat;
            newJointInfo.JOT_JointStatus = "100";//jointInfo.JOT_JointStatus;
            newJointInfo.JOT_FaceCheckResult = jointInfo.JOT_FaceCheckResult;
            newJointInfo.JOT_FaceCheckDate = jointInfo.JOT_FaceCheckDate;
            newJointInfo.JOT_PHWTDate = jointInfo.JOT_PHWTDate;
            newJointInfo.JOT_PHWTReportNo = jointInfo.JOT_PHWTReportNo;
            newJointInfo.JOT_PHWTResult = jointInfo.JOT_PHWTResult;
            newJointInfo.JOT_FaceChecker = jointInfo.JOT_FaceChecker;
            newJointInfo.JOT_Dia = jointInfo.JOT_Dia;
            if (jointInfo.JOT_Size.HasValue)
            {
                newJointInfo.JOT_Size = jointInfo.JOT_Size;
            }
            else
            {
                newJointInfo.JOT_Size = 0;
            }
            
            newJointInfo.JOT_Sch = jointInfo.JOT_Sch;
            newJointInfo.JOT_FactSch = jointInfo.JOT_FactSch;
            newJointInfo.JOT_JointFlag = jointInfo.JOT_JointFlag;
            newJointInfo.JOT_TrustFlag = jointInfo.JOT_TrustFlag;
            newJointInfo.JOT_BecauseJointNo = jointInfo.JOT_BecauseJointNo;
            newJointInfo.JOT_JointDesc = jointInfo.JOT_JointDesc;
            newJointInfo.JOT_WeldSilk = jointInfo.JOT_WeldSilk;
            newJointInfo.JOTY_ID = jointInfo.JOTY_ID;
            newJointInfo.JOT_CheckFlag = jointInfo.JOT_CheckFlag;
            newJointInfo.JOT_RepairFlag = jointInfo.JOT_RepairFlag;
            newJointInfo.WME_ID = jointInfo.WME_ID;
            newJointInfo.JST_ID = jointInfo.JST_ID;
            newJointInfo.JOT_BelongPipe = jointInfo.JOT_BelongPipe;
            newJointInfo.JOT_CheckResult = jointInfo.JOT_CheckResult;
            newJointInfo.JOT_Electricity = jointInfo.JOT_Electricity;
            newJointInfo.JOT_Voltage = jointInfo.JOT_Voltage;
            newJointInfo.IS_Proess = jointInfo.IS_Proess;
            newJointInfo.JOT_ProessDate = jointInfo.JOT_ProessDate;
            newJointInfo.JOT_HotRpt = jointInfo.JOT_HotRpt;
            newJointInfo.JOT_PrepareTemp = jointInfo.JOT_PrepareTemp;
            newJointInfo.JOT_CellTemp = jointInfo.JOT_CellTemp;
            newJointInfo.JOT_LastTemp = jointInfo.JOT_LastTemp;
            newJointInfo.JOT_JointAttribute = jointInfo.JOT_JointAttribute;
            newJointInfo.JOT_CellWeldRules = jointInfo.JOT_CellWeldRules;
            newJointInfo.JOT_FloorWeldRules = jointInfo.JOT_FloorWeldRules;
            newJointInfo.Fix1_FloorWeld = jointInfo.Fix1_FloorWeld;
            newJointInfo.Fix1_CellWelder = jointInfo.Fix1_CellWelder;
            newJointInfo.Fix2_FloorWeld = jointInfo.Fix2_FloorWeld;
            newJointInfo.Fix2_CellWelder = jointInfo.Fix2_CellWelder;
            newJointInfo.PW_PointID = jointInfo.PW_PointID;
            newJointInfo.NDT_ID = jointInfo.NDT_ID;
            newJointInfo.CH_RepairID1 = jointInfo.CH_RepairID1;
            newJointInfo.CH_RepairID2 = jointInfo.CH_RepairID2;
            newJointInfo.Fix1_date = jointInfo.Fix1_date;
            newJointInfo.Fix2_date = jointInfo.Fix2_date;
            newJointInfo.JOT_Location = jointInfo.JOT_Location;
            newJointInfo.IS_Proess = jointInfo.IS_Proess;
            newJointInfo.JOT_Remark = jointInfo.JOT_Remark;
           
            db.PW_JointInfo.InsertOnSubmit(newJointInfo);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="jointInfo"></param>
        public static void UpdateJointInfo(Model.PW_JointInfo jointInfo)
        {
            Model.HJGLDB db = Funs.DB;
            Model.PW_JointInfo newJointInfo = db.PW_JointInfo.FirstOrDefault(e => e.JOT_ID == jointInfo.JOT_ID);

            newJointInfo.JOT_JointNo = jointInfo.JOT_JointNo;
            //newJointInfo.DReportID = jointInfo.DReportID;
            newJointInfo.ISO_ID = jointInfo.ISO_ID;
            newJointInfo.STE_ID = jointInfo.STE_ID;
            newJointInfo.STE_ID2 = jointInfo.STE_ID2;
            //newJointInfo.JOT_CellWelder = jointInfo.JOT_CellWelder;
            //newJointInfo.JOT_FloorWelder = jointInfo.JOT_FloorWelder;
            //newJointInfo.WLO_Code = jointInfo.WLO_Code;
            //newJointInfo.JOT_DoneDin = jointInfo.JOT_DoneDin;
            //newJointInfo.JOT_FloorGroup = jointInfo.JOT_FloorGroup;
            //newJointInfo.JOT_CellGroup = jointInfo.JOT_CellGroup;
            //newJointInfo.IS_Compute = jointInfo.IS_Compute;
            newJointInfo.JOT_NDTResult = jointInfo.JOT_NDTResult;
            newJointInfo.JOT_Component1 = jointInfo.JOT_Component1;
            newJointInfo.JOT_Component2 = jointInfo.JOT_Component2;
            newJointInfo.JOT_HeartNo1 = jointInfo.JOT_HeartNo1;
            newJointInfo.JOT_HeartNo2 = jointInfo.JOT_HeartNo2;
            newJointInfo.JOT_WeldMat = jointInfo.JOT_WeldMat;
            //newJointInfo.JOT_JointStatus = jointInfo.JOT_JointStatus;
            //newJointInfo.JOT_FaceCheckResult = jointInfo.JOT_FaceCheckResult;
            //newJointInfo.JOT_FaceCheckDate = jointInfo.JOT_FaceCheckDate;
            //newJointInfo.JOT_PHWTDate = jointInfo.JOT_PHWTDate;
            //newJointInfo.JOT_PHWTReportNo = jointInfo.JOT_PHWTReportNo;
            //newJointInfo.JOT_PHWTResult = jointInfo.JOT_PHWTResult;
            newJointInfo.JOT_FaceChecker = jointInfo.JOT_FaceChecker;
            newJointInfo.JOT_Dia = jointInfo.JOT_Dia;
            newJointInfo.JOT_Size = jointInfo.JOT_Size;
            newJointInfo.JOT_Sch = jointInfo.JOT_Sch;
            newJointInfo.JOT_FactSch = jointInfo.JOT_FactSch;
            //newJointInfo.JOT_JointFlag = jointInfo.JOT_JointFlag;
            //newJointInfo.JOT_TrustFlag = jointInfo.JOT_TrustFlag;
            newJointInfo.JOT_BecauseJointNo = jointInfo.JOT_BecauseJointNo;
            newJointInfo.JOT_JointDesc = jointInfo.JOT_JointDesc;
            newJointInfo.JOT_WeldSilk = jointInfo.JOT_WeldSilk;
            newJointInfo.JOTY_ID = jointInfo.JOTY_ID;
            //newJointInfo.JOT_CheckFlag = jointInfo.JOT_CheckFlag;
            //newJointInfo.JOT_RepairFlag = jointInfo.JOT_RepairFlag;
            newJointInfo.WME_ID = jointInfo.WME_ID;
            newJointInfo.JST_ID = jointInfo.JST_ID;
            newJointInfo.JOT_BelongPipe = jointInfo.JOT_BelongPipe;
            newJointInfo.JOT_CheckResult = jointInfo.JOT_CheckResult;
            newJointInfo.JOT_Electricity = jointInfo.JOT_Electricity;
            newJointInfo.JOT_Voltage = jointInfo.JOT_Voltage;
            newJointInfo.IS_Proess = jointInfo.IS_Proess;
            newJointInfo.JOT_ProessDate = jointInfo.JOT_ProessDate;
            newJointInfo.JOT_HotRpt = jointInfo.JOT_HotRpt;
            newJointInfo.JOT_PrepareTemp = jointInfo.JOT_PrepareTemp;
            newJointInfo.JOT_CellTemp = jointInfo.JOT_CellTemp;
            newJointInfo.JOT_LastTemp = jointInfo.JOT_LastTemp;
            //newJointInfo.JOT_JointAttribute = jointInfo.JOT_JointAttribute;
            newJointInfo.JOT_CellWeldRules = jointInfo.JOT_CellWeldRules;
            newJointInfo.JOT_FloorWeldRules = jointInfo.JOT_FloorWeldRules;
            //newJointInfo.Fix1_FloorWeld = jointInfo.Fix1_FloorWeld;
            //newJointInfo.Fix1_CellWelder = jointInfo.Fix1_CellWelder;
            //newJointInfo.Fix2_FloorWeld = jointInfo.Fix2_FloorWeld;
            //newJointInfo.Fix2_CellWelder = jointInfo.Fix2_CellWelder;
            //newJointInfo.PW_PointID = jointInfo.PW_PointID;
            newJointInfo.NDT_ID = jointInfo.NDT_ID;
            //newJointInfo.CH_RepairID1 = jointInfo.CH_RepairID1;
            //newJointInfo.CH_RepairID2 = jointInfo.CH_RepairID2;
            //newJointInfo.Fix1_date = jointInfo.Fix1_date;
            //newJointInfo.Fix2_date = jointInfo.Fix2_date;
            newJointInfo.IS_Proess = jointInfo.IS_Proess;
            newJointInfo.JOT_Remark = jointInfo.JOT_Remark;
            //newJointInfo.JOT_Location = jointInfo.JOT_Location;

            db.SubmitChanges();
        }

        /// <summary>
        /// 修改更新导入焊口
        /// </summary>
        /// <param name="jointInfo"></param>
        public static void UpdateExportJoint(Model.PW_JointInfo jointInfo)
        {
            Model.HJGLDB db = Funs.DB;
            Model.PW_JointInfo newJointInfo = db.PW_JointInfo.FirstOrDefault(e => e.JOT_ID == jointInfo.JOT_ID);

            newJointInfo.STE_ID = jointInfo.STE_ID;
            newJointInfo.STE_ID2 = jointInfo.STE_ID2;
            newJointInfo.JOTY_ID = jointInfo.JOTY_ID;
            newJointInfo.WLO_Code = jointInfo.WLO_Code;
            newJointInfo.JOT_JointAttribute = jointInfo.JOT_JointAttribute;
            newJointInfo.JOT_Size = jointInfo.JOT_Size;
            newJointInfo.JOT_JointDesc = jointInfo.JOT_JointDesc;
            newJointInfo.JOT_Sch = jointInfo.JOT_Sch;
            newJointInfo.WME_ID = jointInfo.WME_ID;
            newJointInfo.JOT_WeldMat = jointInfo.JOT_WeldMat;
            newJointInfo.JOT_WeldSilk = jointInfo.JOT_WeldSilk;
            newJointInfo.JST_ID = jointInfo.JST_ID;
            newJointInfo.JOT_Component1 = jointInfo.JOT_Component1;
            newJointInfo.JOT_Component2 = jointInfo.JOT_Component2;
            newJointInfo.JOT_HeartNo1 = jointInfo.JOT_HeartNo1;
            newJointInfo.JOT_HeartNo2 = jointInfo.JOT_HeartNo2;
            newJointInfo.JOT_BelongPipe = jointInfo.JOT_BelongPipe;
            newJointInfo.JOT_PrepareTemp = jointInfo.JOT_PrepareTemp;
            newJointInfo.IS_Proess = jointInfo.IS_Proess;
            newJointInfo.JOT_HotRpt = jointInfo.JOT_HotRpt;
            newJointInfo.JOT_Location = jointInfo.JOT_Location;
            newJointInfo.JOT_Dia = jointInfo.JOT_Dia;

            db.SubmitChanges();
        }

        /// <summary>
        /// 修改点口后焊接信息
        /// </summary>
        /// <param name="jointInfo"></param>
        public static void UpdateJointPoint(Model.PW_JointInfo jointInfo)
        {
            Model.HJGLDB db = Funs.DB;
            Model.PW_JointInfo newJointInfo = db.PW_JointInfo.FirstOrDefault(e => e.JOT_ID == jointInfo.JOT_ID);
            newJointInfo.JOT_JointStatus = jointInfo.JOT_JointStatus;
            newJointInfo.PW_PointID = jointInfo.PW_PointID;
            db.SubmitChanges();
        }

        /// <summary>
        /// 批量添加焊口信息
        /// </summary>
        /// <param name="jointInfo"></param>
        public static void AddJointInfoFatch(Model.PW_JointInfo jointInfo)
        {
            Model.HJGLDB db = Funs.DB;
            Model.PW_JointInfo newJointInfo = new Model.PW_JointInfo();
            string NewKeyID = SQLHelper.GetNewID(typeof(Model.PW_JointInfo));
            newJointInfo.JOT_ID = NewKeyID;
            newJointInfo.ProjectId = jointInfo.ProjectId;
            newJointInfo.ISO_ID = jointInfo.ISO_ID;
            newJointInfo.JOT_JointNo = jointInfo.JOT_JointNo;
            newJointInfo.WLO_Code = jointInfo.WLO_Code;
            newJointInfo.STE_ID = jointInfo.STE_ID;
            newJointInfo.STE_ID2 = jointInfo.STE_ID2;
            newJointInfo.JOT_JointDesc = jointInfo.JOT_JointDesc;
            newJointInfo.JOTY_ID = jointInfo.JOTY_ID;
            newJointInfo.JOT_Size = jointInfo.JOT_Size;
            newJointInfo.JOT_Dia = jointInfo.JOT_Dia;
            newJointInfo.JOT_JointAttribute = jointInfo.JOT_JointAttribute;
            newJointInfo.JOT_Sch = jointInfo.JOT_Sch;
            newJointInfo.JOT_TrustFlag = jointInfo.JOT_TrustFlag;
            newJointInfo.JOT_CheckFlag = jointInfo.JOT_CheckFlag;
            newJointInfo.JOT_JointStatus = jointInfo.JOT_JointStatus;
            newJointInfo.JOT_WeldMat = jointInfo.JOT_WeldMat;
            newJointInfo.JOT_WeldSilk = jointInfo.JOT_WeldSilk;
            newJointInfo.WME_ID = jointInfo.WME_ID;
            newJointInfo.IS_Proess = jointInfo.IS_Proess;

            db.PW_JointInfo.InsertOnSubmit(newJointInfo);
            db.SubmitChanges();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="jot_id"></param>
        public static void DeleteJointInfo(string jot_id)
        {
            Model.HJGLDB db = Funs.DB;
            Model.PW_JointInfo jointInfo = db.PW_JointInfo.FirstOrDefault(e => e.JOT_ID == jot_id);
            db.PW_JointInfo.DeleteOnSubmit(jointInfo);
            db.SubmitChanges();
        }

        /// <summary>
        /// 删除管线下焊口信息
        /// </summary>
        /// <param name="jot_id"></param>
        public static void DeleteJointInfoByIsoId(string iso_id)
        {
            Model.HJGLDB db = Funs.DB;
            var jointInfos = db.PW_JointInfo.Where(e => e.ISO_ID == iso_id);
            db.PW_JointInfo.DeleteAllOnSubmit(jointInfos);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="jointInfo"></param>
        public static void UpdateJointInfoByDReport(Model.PW_JointInfo jointInfo)
        {
            Model.HJGLDB db = Funs.DB;
            Model.PW_JointInfo newJointInfo = db.PW_JointInfo.FirstOrDefault(e => e.JOT_ID == jointInfo.JOT_ID);
            newJointInfo.JOT_JointNo = jointInfo.JOT_JointNo;
            newJointInfo.DReportID = jointInfo.DReportID;
            newJointInfo.JOT_CellWelder = jointInfo.JOT_CellWelder;
            newJointInfo.JOT_FloorWelder = jointInfo.JOT_FloorWelder;
            newJointInfo.WLO_Code = jointInfo.WLO_Code;
            newJointInfo.JOT_DoneDin = jointInfo.JOT_DoneDin;
            newJointInfo.JOT_JointAttribute = jointInfo.JOT_JointAttribute;
            newJointInfo.JOT_JointStatus = jointInfo.JOT_JointStatus;
            newJointInfo.JOT_Location = jointInfo.JOT_Location;

            db.SubmitChanges();
        }

        /// <summary>
        /// 更新焊口号 固定焊口号后 +G
        /// </summary>
        /// <param name="jotId">焊口id</param>
        /// <param name="jointAttribute">焊口属性</param>
        /// <param name="operateState">日报操作（增加、删除）</param>
        public static void UpdateJointNoAddG(string jotId ,string jointAttribute,string operateState)
        {
            Model.HJGLDB db = Funs.DB;
            if (operateState == Const.Delete || jointAttribute != "固定")
            {
                Model.PW_JointInfo deleteJointInfo = db.PW_JointInfo.FirstOrDefault(e => e.JOT_ID == jotId);
                if (deleteJointInfo.JOT_JointNo.Last() == 'G')
                {
                    deleteJointInfo.JOT_JointNo = deleteJointInfo.JOT_JointNo.Substring(0, deleteJointInfo.JOT_JointNo.Length - 1);
                    db.SubmitChanges();
                }
            }
            else
            {
                Model.PW_JointInfo addJointInfo = db.PW_JointInfo.FirstOrDefault(e => e.JOT_ID == jotId);
                if (addJointInfo.JOT_JointNo.Last() != 'G')
                {
                    addJointInfo.JOT_JointNo += "G";
                }
                
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 根据焊接日报ID获取焊接日期
        /// </summary>
        /// <param name="dReportID"></param>
        /// <returns></returns>
        public static DateTime GetReportDateByDReportID(string dReportID)
        {
            return (from y in db.BO_WeldReportMain where y.DReportID == dReportID select y.JOT_WeldDate).FirstOrDefault();
        }

        /// <summary>
        /// 根据日报告号获取焊口信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static List<Model.PW_JointInfo> GetJointInfosByDReportID(string DReportID)
        {
            return (from x in Funs.DB.PW_JointInfo where x.DReportID == DReportID select x).ToList();
        }

        /// <summary>
        /// 根据点口编号获取焊口信息
        /// </summary>
        /// <param name="PW_PointID"></param>
        /// <returns></returns>
        public static List<Model.PW_JointInfo> GetJointInfosByPointID(string PW_PointID)
        {
            return (from x in Funs.DB.PW_JointInfo where x.PW_PointID == PW_PointID select x).ToList();
        }

        /// <summary>
        /// 根据管线Id获取已焊接的焊口信息
        /// </summary>
        /// <param name="iso_id"></param>
        /// <returns></returns>
        public static string GetDeReportByJotID(string iso_id)
        {
            return (from x in Funs.DB.PW_JointInfo where x.ISO_ID == iso_id && x.DReportID != null select x.DReportID).FirstOrDefault();
        }

        /// <summary>
        /// 根据焊口号、管线号及区域号获取焊口信息
        /// </summary>
        /// <param name="jointNo">焊口号</param>
        /// <param name="isoNo"></param>
        /// <param name="bAW_ID"></param>
        /// <returns></returns>
        public static Model.PW_JointInfo GetJointInfoByJointNoAndIsoNoAndBAW_ID(string projectId, string bAW_ID, string isoNo, string jointNo)
        {
            return (from x in Funs.DB.PW_JointInfo join y in Funs.DB.PW_IsoInfo on x.ISO_ID equals y.ISO_ID where x.ProjectId == projectId && y.BAW_ID == bAW_ID && y.ISO_IsoNo == isoNo && x.JOT_JointNo == jointNo select x).FirstOrDefault();
        }

        /// <summary>
        /// 根据焊口信息查找焊口信息
        /// </summary>
        /// <param name="jot_no"></param>
        /// <returns></returns>
        public static string GetJointInfoByJOTNO(string iso_id, string jot_no)
        {
            return (from x in Funs.DB.PW_JointInfo where x.ISO_ID == iso_id && x.JOT_JointNo == jot_no select x.JOT_JointNo).FirstOrDefault();
        }

        /// <summary>
        /// 根据焊接日报获取焊口数
        /// </summary>
        /// <param name="dreportId"></param>
        /// <returns></returns>
        public static int GetJointInfoByDReportId(string dreportId)
        {
            var q = (from x in Funs.DB.PW_JointInfo where x.DReportID == dreportId select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据管线判断该管线下焊口是否已焊接
        /// </summary>
        /// <param name="iso_id"></param>
        /// <returns></returns>
        public static bool IsExistJointInfoWeld(string iso_id)
        {
            var q = from x in Funs.DB.PW_JointInfo where x.ISO_ID == iso_id && x.DReportID != null select x;
            if (q.Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据材质1获取焊口数
        /// </summary>
        /// <param name="dreportId"></param>
        /// <returns></returns>
        public static int GetJointInfoBySTEID(string steId)
        {
            var q = (from x in Funs.DB.PW_JointInfo where x.STE_ID == steId select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据材质2获取焊口数
        /// </summary>
        /// <param name="dreportId"></param>
        /// <returns></returns>
        public static int GetJointInfoBySTEID2(string steId)
        {
            var q = (from x in Funs.DB.PW_JointInfo where x.STE_ID2 == steId select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据盖面焊工获取焊口数
        /// </summary>
        /// <param name="cellWelder"></param>
        /// <returns></returns>
        public static int GetJointInfoByCellWelder(string cellWelder)
        {
            var q = (from x in Funs.DB.PW_JointInfo where x.JOT_CellWelder == cellWelder select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据打底焊工获取焊口数
        /// </summary>
        /// <param name="floorWelder"></param>
        /// <returns></returns>
        public static int GetJointInfoByFloorWelder(string floorWelder)
        {
            var q = (from x in Funs.DB.PW_JointInfo where x.JOT_FloorWelder == floorWelder select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据打底班组获取焊口数
        /// </summary>
        /// <param name="floorGroup"></param>
        /// <returns></returns>
        public static int GetJointInfoByFloorGroup(string floorGroup)
        {
            var q = (from x in Funs.DB.PW_JointInfo where x.JOT_FloorGroup == floorGroup select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据盖面班组获取焊口数
        /// </summary>
        /// <param name="cellGroup"></param>
        /// <returns></returns>
        public static int GetJointInfoByCellGroup(string cellGroup)
        {
            var q = (from x in Funs.DB.PW_JointInfo where x.JOT_CellGroup == cellGroup select x).ToList();
            return q.Count();
        }
        /// <summary>
        /// 根据安装组件1获取焊口数
        /// </summary>
        /// <param name="comId"></param>
        /// <returns></returns>
        public static int GetJointInfoByCom1(string comId)
        {
            var q = (from x in Funs.DB.PW_JointInfo where x.JOT_Component1 == comId select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据安装组件2获取焊口数
        /// </summary>
        /// <param name="comId"></param>
        /// <returns></returns>
        public static int GetJointInfoByCom2(string comId)
        {
            var q = (from x in Funs.DB.PW_JointInfo where x.JOT_Component2 == comId select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据焊条获取焊口数
        /// </summary>
        /// <param name="weldmat"></param>
        /// <returns></returns>
        public static int GetJointInfoByWeldMat(string weldmat)
        {
            var q = (from x in Funs.DB.PW_JointInfo where x.JOT_WeldMat == weldmat select x).ToList();
            return q.Count();
        }
        /// <summary>
        /// 根据焊丝获取焊口数
        /// </summary>
        /// <param name="weldsilk"></param>
        /// <returns></returns>
        public static int GetJointInfoByWeldSilk(string weldsilk)
        {
            var q = (from x in Funs.DB.PW_JointInfo where x.JOT_WeldSilk == weldsilk select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据焊缝类型获取焊口数
        /// </summary>
        /// <param name="jotyId"></param>
        /// <returns></returns>
        public static int GetJointInfoByJOTYID(string jotyId)
        {
            var q = (from x in Funs.DB.PW_JointInfo where x.JOTY_ID == jotyId select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据焊接方法获取焊口数
        /// </summary>
        /// <param name="wmeId"></param>
        /// <returns></returns>
        public static int GetJointInfoByWMEID(string wmeId)
        {
            var q = (from x in Funs.DB.PW_JointInfo where x.WME_ID == wmeId select x).ToList();
            return q.Count();
        }
        /// <summary>
        /// 根据焊接方法获取焊口数
        /// </summary>
        /// <param name="jstId"></param>
        /// <returns></returns>
        public static int GetJointInfoByJSTID(string jstId)
        {
            var q = (from x in Funs.DB.PW_JointInfo where x.JST_ID == jstId select x).ToList();
            return q.Count();
        }
        /// <summary>
        /// 根据打底工1获取焊口数
        /// </summary>
        /// <param name="floorWeld"></param>
        /// <returns></returns>
        public static int GetJointInfoByFloorWeld(string floorWeld)
        {
            var q = (from x in Funs.DB.PW_JointInfo where x.Fix1_FloorWeld == floorWeld select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据打底工2获取焊口数
        /// </summary>
        /// <param name="floorWeld"></param>
        /// <returns></returns>
        public static int GetJointInfoByFloorWeld2(string floorWeld)
        {
            var q = (from x in Funs.DB.PW_JointInfo where x.Fix2_FloorWeld == floorWeld select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据盖面工1获取焊口数
        /// </summary>
        /// <param name="cellWeld"></param>
        /// <returns></returns>
        public static int GetJointInfoByCellWeld(string cellWeld)
        {
            var q = (from x in Funs.DB.PW_JointInfo where x.Fix1_CellWelder == cellWeld select x).ToList();
            return q.Count();
        }
        /// <summary>
        /// 根据盖面工2获取焊口数
        /// </summary>
        /// <param name="cellWeld"></param>
        /// <returns></returns>
        public static int GetJointInfoByCellWeld2(string cellWeld)
        {
            var q = (from x in Funs.DB.PW_JointInfo where x.Fix2_CellWelder == cellWeld select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据点口获取焊口数
        /// </summary>
        /// <param name="pointId"></param>
        /// <returns></returns>
        public static int GetJointInfoByPointId(string pointId)
        {
            var q = (from x in Funs.DB.PW_JointInfo where x.PW_PointID == pointId select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据探伤类型获取焊口数
        /// </summary>
        /// <param name="ndtId"></param>
        /// <returns></returns>
        public static int GetJointInfoByNDTId(string ndtId)
        {
            var q = (from x in Funs.DB.PW_JointInfo where x.NDT_ID == ndtId select x).ToList();
            return q.Count();
        }
        /// <summary>
        /// 根据检测日委托获取焊口数
        /// </summary>
        /// <param name="ndtTId"></param>
        /// <returns></returns>
        public static int GetJointInfoByNDTTId(string ndtTId)
        {
            var q = (from x in Funs.DB.PW_JointInfo where x.NDTT_ID == ndtTId select x).ToList();
            return q.Count();
        }
        /// <summary>
        /// 根据返修获取焊口数
        /// </summary>
        /// <param name="RepairId"></param>
        /// <returns></returns>
        public static int GetJointInfoByRepairID1(string RepairId)
        {
            var q = (from x in Funs.DB.PW_JointInfo where x.CH_RepairID1 == RepairId select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据返修2获取焊口数
        /// </summary>
        /// <param name="RepairId"></param>
        /// <returns></returns>
        public static int GetJointInfoByRepairID2(string RepairId)
        {
            var q = (from x in Funs.DB.PW_JointInfo where x.CH_RepairID2 == RepairId select x).ToList();
            return q.Count();
        }
    }
}
