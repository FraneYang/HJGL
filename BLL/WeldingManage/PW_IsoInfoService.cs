using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public static class PW_IsoInfoService
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
        /// 定义变量
        /// </summary>
        private static IQueryable<Model.View_IsoInfo> qq = from x in db.View_IsoInfo orderby x.ISO_IsoNo select x;

        /// <summary>
        /// 获取管线列表
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="searchValue"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string projectId, string iSO_IsoNo, string sER_ID, string nDT_ID, string iSO_IsoNumber, string sTE_ID, string iSO_Specification, string workAreaId, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.View_IsoInfo> q = qq;
            //IQueryable<Model.PW_IsoInfo> q = from x in db.PW_IsoInfo orderby x.ISO_ID select x;
            if (!string.IsNullOrEmpty(projectId))
            {
                q = q.Where(e => e.ProjectId == projectId);
            }
            if (!string.IsNullOrEmpty(iSO_IsoNo))
            {
                q = q.Where(e => e.ISO_IsoNo.Contains(iSO_IsoNo));
            }
            if (!string.IsNullOrEmpty(sER_ID))
            {
                q = q.Where(e => e.SER_ID==sER_ID);
            }
            if (!string.IsNullOrEmpty(nDT_ID))
            {
                q = q.Where(e => e.NDT_ID == nDT_ID);
            }
            if (!string.IsNullOrEmpty(iSO_IsoNumber))
            {
                q = q.Where(e => e.ISO_IsoNumber.Contains(iSO_IsoNumber));
            }
            if (!string.IsNullOrEmpty(sTE_ID))
            {
                q = q.Where(e => e.STE_ID == sTE_ID);
            }
            if (!string.IsNullOrEmpty(iSO_Specification))
            {
                q = q.Where(e => e.ISO_Specification.Contains(iSO_Specification));
            }
            if (!string.IsNullOrEmpty(workAreaId))
            {
                q = q.Where(e => e.BAW_ID == workAreaId);
            }
            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }
            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.ISO_ID,
                       x.UnitName,
                       x.SERName,
                       x.NDTR_Name,
                       x.NDTName,
                       x.BAWName,
                       x.ISO_IsoNo,
                       x.ISO_SysNo,
                       x.ISO_FileName,
                       x.ISO_SubSysNo,
                       x.ISO_CwpNo,
                       x.ISO_IsoNumber,
                       x.ISO_Rev,
                       x.ISO_Sheet,
                       x.ISO_PipeQty,
                       x.ISO_TotalDin,
                       x.ISO_Paint,
                       x.ISO_Insulator,
                       x.STEName,
                       x.ISO_Executive,
                       x.ISO_Specification,
                       x.ISO_Modifier,
                       x.ISO_ModifyDate,
                       x.ISO_Creator,
                       x.ISO_CreateDate,
                       x.ISO_JointQty,
                       x.ISO_DesignPress,
                       x.ISO_DesignTemperature,
                       x.ISO_TestPress,
                       x.ISO_TestTemperature,
                       x.ISO_NDTClass,
                       x.ISO_PTRate,
                       x.IDName,
                       x.ISO_PTClass,
                       ISO_IfPickling = x.ISO_IfPickling == true ? "是" : "否",
                       ISO_IfChasing = x.ISO_IfChasing == true ? "是" : "否",
                       x.ISO_Remark,
                       x.PTP_TestPackageNo
                   };
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int getListCount(string projectId, string iSO_IsoNo, string sER_ID, string nDT_ID, string iSO_IsoNumber, string sTE_ID, string iSO_Specification, string workAreaId)
        {
            return count;
        }

        /// <summary>
        /// 根据管线ID获取管线信息
        /// </summary>
        /// <param name="isoInfoName"></param>
        /// <returns></returns>
        public static Model.PW_IsoInfo GetIsoInfoByIsoInfoId(string isoInfoId)
        {
            return Funs.DB.PW_IsoInfo.FirstOrDefault(e => e.ISO_ID == isoInfoId);
        }

        /// <summary>
        /// 根据管线号和工区号获取管线信息
        /// </summary>
        /// <param name="isoNo"></param>
        /// <returns></returns>
        public static bool IsExistIsoInfoCode(string isoNo,string area)
        {
            Model.HJGLDB db = Funs.DB;
            var q = from x in db.PW_IsoInfo where x.ISO_IsoNo == isoNo && x.BAW_ID==area select x;
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
        /// 添加作业管线
        /// </summary>
        /// <param name="isoInfo"></param>
        public static void AddIsoInfo(Model.PW_IsoInfo isoInfo)
        {
            Model.HJGLDB db = Funs.DB;
            Model.PW_IsoInfo newIsoInfo = new Model.PW_IsoInfo();

            newIsoInfo.ISO_ID = isoInfo.ISO_ID;
            newIsoInfo.ProjectId = isoInfo.ProjectId;
            newIsoInfo.BSU_ID = isoInfo.BSU_ID;
            newIsoInfo.SER_ID = isoInfo.SER_ID;
            newIsoInfo.NDTR_ID = isoInfo.NDTR_ID;
            newIsoInfo.NDT_ID = isoInfo.NDT_ID;
            newIsoInfo.BAW_ID = isoInfo.BAW_ID;
            newIsoInfo.ISO_IsoNo = isoInfo.ISO_IsoNo;
            newIsoInfo.ISO_SysNo = isoInfo.ISO_SysNo;
            newIsoInfo.ISO_FileName = isoInfo.ISO_FileName;
            newIsoInfo.ISO_SubSysNo = isoInfo.ISO_SubSysNo;
            newIsoInfo.ISO_CwpNo = isoInfo.ISO_CwpNo;
            newIsoInfo.ISO_IsoNumber = isoInfo.ISO_IsoNumber;
            newIsoInfo.ISO_Rev = isoInfo.ISO_Rev;
            newIsoInfo.ISO_Sheet = isoInfo.ISO_Sheet;
            newIsoInfo.ISO_PipeQty = isoInfo.ISO_PipeQty;
            newIsoInfo.ISO_TotalDin = isoInfo.ISO_TotalDin;
            newIsoInfo.ISO_Paint = isoInfo.ISO_Paint;
            newIsoInfo.ISO_Insulator = isoInfo.ISO_Insulator;
            newIsoInfo.STE_ID = isoInfo.STE_ID;
            newIsoInfo.ISO_HardnessRate = isoInfo.ISO_HardnessRate;
            newIsoInfo.ISO_Executive = isoInfo.ISO_Executive;
            newIsoInfo.ISO_Specification = isoInfo.ISO_Specification;
            newIsoInfo.ISO_Modifier = isoInfo.ISO_Modifier;
            newIsoInfo.ISO_ModifyDate = isoInfo.ISO_ModifyDate;
            newIsoInfo.ISO_Creator = isoInfo.ISO_Creator;
            newIsoInfo.ISO_CreateDate = isoInfo.ISO_CreateDate;
            newIsoInfo.ISO_JointQty = isoInfo.ISO_JointQty;
            newIsoInfo.ISO_DesignPress = isoInfo.ISO_DesignPress;
            newIsoInfo.ISO_DesignTemperature = isoInfo.ISO_DesignTemperature;
            newIsoInfo.ISO_TestPress = isoInfo.ISO_TestPress;
            newIsoInfo.ISO_TestTemperature = isoInfo.ISO_TestTemperature;
            newIsoInfo.ISO_NDTClass = isoInfo.ISO_NDTClass;
            newIsoInfo.ISO_PTRate = isoInfo.ISO_PTRate;
            newIsoInfo.ISC_ID = isoInfo.ISC_ID;
            newIsoInfo.ISO_PTClass = isoInfo.ISO_PTClass;
            newIsoInfo.ISO_IfPickling = isoInfo.ISO_IfPickling;
            newIsoInfo.ISO_IfChasing = isoInfo.ISO_IfChasing;
            newIsoInfo.ISO_Remark = isoInfo.ISO_Remark;
            newIsoInfo.IsBig = isoInfo.IsBig;
            newIsoInfo.PipeNumber = isoInfo.PipeNumber;
            db.PW_IsoInfo.InsertOnSubmit(newIsoInfo);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改作业管线
        /// </summary>
        /// <param name="isoInfo"></param>
        public static void UpdateIsoInfo(Model.PW_IsoInfo isoInfo)
        {
            Model.HJGLDB db = Funs.DB;
            Model.PW_IsoInfo newIsoInfo = db.PW_IsoInfo.First(e => e.ISO_ID == isoInfo.ISO_ID)
;
            newIsoInfo.BSU_ID = isoInfo.BSU_ID;
            newIsoInfo.SER_ID = isoInfo.SER_ID;
            newIsoInfo.NDTR_ID = isoInfo.NDTR_ID;
            newIsoInfo.NDT_ID = isoInfo.NDT_ID;
            newIsoInfo.BAW_ID = isoInfo.BAW_ID;
            newIsoInfo.ISO_IsoNo = isoInfo.ISO_IsoNo;
            newIsoInfo.ISO_SysNo = isoInfo.ISO_SysNo;
            newIsoInfo.ISO_FileName = isoInfo.ISO_FileName;
            newIsoInfo.ISO_SubSysNo = isoInfo.ISO_SubSysNo;
            newIsoInfo.ISO_CwpNo = isoInfo.ISO_CwpNo;
            newIsoInfo.ISO_IsoNumber = isoInfo.ISO_IsoNumber;
            newIsoInfo.ISO_Rev = isoInfo.ISO_Rev;
            newIsoInfo.ISO_Sheet = isoInfo.ISO_Sheet;
            newIsoInfo.ISO_PipeQty = isoInfo.ISO_PipeQty;
            newIsoInfo.ISO_TotalDin = isoInfo.ISO_TotalDin;
            newIsoInfo.ISO_HardnessRate = isoInfo.ISO_HardnessRate;
            newIsoInfo.ISO_Paint = isoInfo.ISO_Paint;
            newIsoInfo.ISO_Insulator = isoInfo.ISO_Insulator;
            newIsoInfo.STE_ID = isoInfo.STE_ID;
            newIsoInfo.ISO_Executive = isoInfo.ISO_Executive;
            newIsoInfo.ISO_Specification = isoInfo.ISO_Specification;
            newIsoInfo.ISO_Modifier = isoInfo.ISO_Modifier;
            newIsoInfo.ISO_ModifyDate = isoInfo.ISO_ModifyDate;
            newIsoInfo.ISO_Creator = isoInfo.ISO_Creator;
            newIsoInfo.ISO_CreateDate = isoInfo.ISO_CreateDate;
            newIsoInfo.ISO_JointQty = isoInfo.ISO_JointQty;
            newIsoInfo.ISO_DesignPress = isoInfo.ISO_DesignPress;
            newIsoInfo.ISO_DesignTemperature = isoInfo.ISO_DesignTemperature;
            newIsoInfo.ISO_TestPress = isoInfo.ISO_TestPress;
            newIsoInfo.ISO_TestTemperature = isoInfo.ISO_TestTemperature;
            newIsoInfo.ISO_NDTClass = isoInfo.ISO_NDTClass;
            newIsoInfo.ISO_PTRate = isoInfo.ISO_PTRate;
            newIsoInfo.ISC_ID = isoInfo.ISC_ID;
            newIsoInfo.ISO_PTClass = isoInfo.ISO_PTClass;
            newIsoInfo.ISO_IfPickling = isoInfo.ISO_IfPickling;
            newIsoInfo.ISO_IfChasing = isoInfo.ISO_IfChasing;
            newIsoInfo.ISO_Remark = isoInfo.ISO_Remark;
            newIsoInfo.IsBig = isoInfo.IsBig;
            newIsoInfo.PipeNumber = isoInfo.PipeNumber;
            db.SubmitChanges();

        }

        /// <summary>
        /// 修改作业管线
        /// </summary>
        /// <param name="isoInfo"></param>
        public static void UpdateExportIso(Model.PW_IsoInfo isoInfo)
        {
            Model.HJGLDB db = Funs.DB;
            Model.PW_IsoInfo newIsoInfo = db.PW_IsoInfo.First(e => e.ISO_ID == isoInfo.ISO_ID);
            newIsoInfo.STE_ID = isoInfo.STE_ID;
            newIsoInfo.NDTR_ID = isoInfo.NDTR_ID;
            newIsoInfo.ISO_Specification = isoInfo.ISO_Specification;
            newIsoInfo.ISO_TestPress = isoInfo.ISO_TestPress;
            newIsoInfo.SER_ID = isoInfo.SER_ID;
            newIsoInfo.ISO_IsoNumber = isoInfo.ISO_IsoNumber;
            newIsoInfo.ISO_DesignTemperature = isoInfo.ISO_DesignTemperature;
            newIsoInfo.ISO_DesignPress = isoInfo.ISO_DesignPress;
            newIsoInfo.ISC_ID = isoInfo.ISC_ID;
            newIsoInfo.ISO_HardnessRate = isoInfo.ISO_HardnessRate;

            db.SubmitChanges();

        }

        /// <summary>
        /// 根据作业管线Id删除一个作业管线信息
        /// </summary>
        /// <param name="isoInfoId"></param>
        public static void DeleteIsoInfo(string isoInfoId)
        {
            Model.HJGLDB db = Funs.DB;
            Model.PW_IsoInfo isoInfo = db.PW_IsoInfo.First(e => e.ISO_ID == isoInfoId);
            db.PW_IsoInfo.DeleteOnSubmit(isoInfo);
            db.SubmitChanges();
        }
        
        /// <summary>
        /// 根据单位Id获取管线数
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static int GetIsoInfoByUnitId(string unitId)
        {
            var q = (from x in Funs.DB.PW_IsoInfo where x.BSU_ID == unitId select x).ToList();
            return q.Count();
        }
        /// <summary>
        /// 根据介质Id获取管线数
        /// </summary>
        /// <param name="serId"></param>
        /// <returns></returns>
        public static int GetIsoInfoBySerID(string serId)
        {
            var q = (from x in Funs.DB.PW_IsoInfo where x.SER_ID == serId select x).ToList();
            return q.Count();
        }
        /// <summary>
        /// 根据探伤比例获取管线数
        /// </summary>
        /// <param name="ndtrId"></param>
        /// <returns></returns>
        public static int GetIsoInfoByNDTRID(string ndtrId)
        {
            var q = (from x in Funs.DB.PW_IsoInfo where x.NDTR_ID == ndtrId select x).ToList();
            return q.Count();
        }
        /// <summary>
        /// 根据探伤类型获取管线数
        /// </summary>
        /// <param name="ndtId"></param>
        /// <returns></returns>
        public static int GetIsoInfoByNDTID(string ndtId)
        {
            var q = (from x in Funs.DB.PW_IsoInfo where x.NDT_ID == ndtId select x).ToList();
            return q.Count();
        }
        /// <summary>
        /// 根据区域获取管线数
        /// </summary>
        /// <param name="bawId"></param>
        /// <returns></returns>
        public static int GetIsoInfoByBawId(string bawId)
        {
            var q = (from x in Funs.DB.PW_IsoInfo where x.BAW_ID == bawId select x).ToList();
            return q.Count();
        }
        /// <summary>
        /// 根据材质获取管线数
        /// </summary>
        /// <param name="steId"></param>
        /// <returns></returns>
        public static int GetIsoInfoBySteId(string steId)
        {
            var q = (from x in Funs.DB.PW_IsoInfo where x.STE_ID == steId select x).ToList();
            return q.Count();
        }
        /// <summary>
        /// 根据管道等级获取管线
        /// </summary>
        /// <param name="iscId"></param>
        /// <returns></returns>
        public static int GetIsoInfoByISCID(string iscId)
        {
            var q = (from x in Funs.DB.PW_IsoInfo where x.ISC_ID == iscId select x).ToList();
            return q.Count();
        }

        /// <summary>
        /// 根据单位Id获取管线信息
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static List<Model.View_IsoInfoReportSearch> GetIsoInfoViewListByUnitId(string projectId, string unitId, string workearId)
        {
            var iso = from x in Funs.DB.View_IsoInfoReportSearch
                      where x.BSU_ID == unitId && x.BAW_ID == workearId && x.ProjectId == projectId 
                      orderby x.ISO_IsoNo
                      select x;
            if (iso.Count() > 0)
            {
                return iso.ToList();
            }
            else
            {
                return null;
            }
        }
    }
}
