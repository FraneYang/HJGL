using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class TestPackageManageEditService
    {
        public static Model.HJGLDB db = Funs.DB;

        /// <summary>
        /// 根据试压Id获取用于试压信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static Model.TP_TestPackage GetTP_TestPackageByID(string PTP_ID)
        {
            Model.HJGLDB db = Funs.DB;
            var view = db.TP_TestPackage.FirstOrDefault(e => e.PTP_ID == PTP_ID);
            return view;
        }

        /// <summary>
        /// 根据试压Id获取用于管线明细信息
        /// </summary>
        /// <param name="jot_id"></param>
        /// <returns></returns>
        public static List<Model.PW_IsoInfo> GetIsoInfosByPTP_ID(string PTP_ID)
        {
            Model.HJGLDB db = Funs.DB;
            var view = from x in db.PW_IsoInfo
                       join y in db.TP_IsoList on x.ISO_ID equals y.ISO_ID
                       where y.PTP_ID == PTP_ID select x;
            return view.ToList();
        }

        /// <summary>
        /// 增加试压信息
        /// </summary>
        /// <param name="testPackage">试压实体</param>
        public static void AddTP_TestPackage(Model.TP_TestPackage testPackage)
        {
            Model.HJGLDB db = Funs.DB;
            Model.TP_TestPackage newTestPackage = new Model.TP_TestPackage();
            newTestPackage.PTP_ID = testPackage.PTP_ID;
            newTestPackage.BSU_ID = testPackage.BSU_ID;
            newTestPackage.PT_ID = testPackage.PT_ID;
            newTestPackage.PTP_TestPackageNo = testPackage.PTP_TestPackageNo;
            newTestPackage.PTP_TestPackageName = testPackage.PTP_TestPackageName;
            newTestPackage.PTP_TestHeat = testPackage.PTP_TestHeat;
            newTestPackage.PTP_TestService = testPackage.PTP_TestService;
            newTestPackage.PTP_TestType = testPackage.PTP_TestType;
            newTestPackage.PTP_Finisher = testPackage.PTP_Finisher;
            newTestPackage.PTP_FinishDate = testPackage.PTP_FinishDate;
            newTestPackage.PTP_Tabler = testPackage.PTP_Tabler;
            newTestPackage.PTP_TableDate = testPackage.PTP_TableDate;
            newTestPackage.PTP_Modifier = testPackage.PTP_Modifier;
            newTestPackage.PTP_ModifyDate = testPackage.PTP_ModifyDate;
            newTestPackage.PTP_Auditer = testPackage.PTP_Auditer;
            newTestPackage.PTP_AduditDate = testPackage.PTP_AduditDate;
            newTestPackage.PTP_Remark = testPackage.PTP_Remark;
            newTestPackage.PTP_TestPackageCode = testPackage.PTP_TestPackageCode;
            newTestPackage.PTP_TestAmbientTemp = testPackage.PTP_TestAmbientTemp;
            newTestPackage.PTP_TestMediumTemp = testPackage.PTP_TestMediumTemp;
            newTestPackage.PTP_TestPressure = testPackage.PTP_TestPressure;
            newTestPackage.PTP_TestPressureTemp = testPackage.PTP_TestPressureTemp;
            newTestPackage.PTP_TestPressureTime = testPackage.PTP_TestPressureTime;
            newTestPackage.PTP_TightnessTest = testPackage.PTP_TightnessTest;
            newTestPackage.PTP_TightnessTestTemp = testPackage.PTP_TightnessTestTemp;
            newTestPackage.PTP_TightnessTestTime = testPackage.PTP_TightnessTestTime;
            newTestPackage.PTP_LeakageTestService = testPackage.PTP_LeakageTestService;
            newTestPackage.PTP_LeakageTestPressure = testPackage.PTP_LeakageTestPressure;
            newTestPackage.PTP_VacuumTestService = testPackage.PTP_VacuumTestService;
            newTestPackage.PTP_VacuumTestPressure = testPackage.PTP_VacuumTestPressure;
            newTestPackage.PTP_OperationMedium = testPackage.PTP_OperationMedium;
            newTestPackage.PTP_PurgingMedium = testPackage.PTP_PurgingMedium;
            newTestPackage.PTP_CleaningMedium = testPackage.PTP_CleaningMedium;
            newTestPackage.PTP_AllowSeepage = testPackage.PTP_AllowSeepage;
            newTestPackage.PTP_FactSeepage = testPackage.PTP_FactSeepage;
            newTestPackage.ProjectId = testPackage.ProjectId;
            newTestPackage.InstallationId = testPackage.InstallationId;
            db.TP_TestPackage.InsertOnSubmit(newTestPackage);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改试压信息
        /// </summary>
        /// <param name="weldReport">试压实体</param>
        public static void UpdateTP_TestPackage(Model.TP_TestPackage testPackage)
        {
            Model.HJGLDB db = Funs.DB;
            Model.TP_TestPackage newTestPackage = db.TP_TestPackage.First(e => e.PTP_ID == testPackage.PTP_ID);
            newTestPackage.BSU_ID = testPackage.BSU_ID;
            newTestPackage.PT_ID = testPackage.PT_ID;
            newTestPackage.PTP_TestPackageNo = testPackage.PTP_TestPackageNo;
            newTestPackage.PTP_TestPackageName = testPackage.PTP_TestPackageName;
            newTestPackage.PTP_TestHeat = testPackage.PTP_TestHeat;
            newTestPackage.PTP_TestService = testPackage.PTP_TestService;
            newTestPackage.PTP_TestType = testPackage.PTP_TestType;
            newTestPackage.PTP_Finisher = testPackage.PTP_Finisher;
            newTestPackage.PTP_FinishDate = testPackage.PTP_FinishDate;
            newTestPackage.PTP_Tabler = testPackage.PTP_Tabler;
            newTestPackage.PTP_TableDate = testPackage.PTP_TableDate;
            newTestPackage.PTP_Modifier = testPackage.PTP_Modifier;
            newTestPackage.PTP_ModifyDate = testPackage.PTP_ModifyDate;
            newTestPackage.PTP_Auditer = testPackage.PTP_Auditer;
            newTestPackage.PTP_AduditDate = testPackage.PTP_AduditDate;
            newTestPackage.PTP_Remark = testPackage.PTP_Remark;
            newTestPackage.PTP_TestPackageCode = testPackage.PTP_TestPackageCode;
            newTestPackage.PTP_TestAmbientTemp = testPackage.PTP_TestAmbientTemp;
            newTestPackage.PTP_TestMediumTemp = testPackage.PTP_TestMediumTemp;
            newTestPackage.PTP_TestPressure = testPackage.PTP_TestPressure;
            newTestPackage.PTP_TestPressureTemp = testPackage.PTP_TestPressureTemp;
            newTestPackage.PTP_TestPressureTime = testPackage.PTP_TestPressureTime;
            newTestPackage.PTP_TightnessTest = testPackage.PTP_TightnessTest;
            newTestPackage.PTP_TightnessTestTemp = testPackage.PTP_TightnessTestTemp;
            newTestPackage.PTP_TightnessTestTime = testPackage.PTP_TightnessTestTime;
            newTestPackage.PTP_LeakageTestService = testPackage.PTP_LeakageTestService;
            newTestPackage.PTP_LeakageTestPressure = testPackage.PTP_LeakageTestPressure;
            newTestPackage.PTP_VacuumTestService = testPackage.PTP_VacuumTestService;
            newTestPackage.PTP_VacuumTestPressure = testPackage.PTP_VacuumTestPressure;
            newTestPackage.PTP_OperationMedium = testPackage.PTP_OperationMedium;
            newTestPackage.PTP_PurgingMedium = testPackage.PTP_PurgingMedium;
            newTestPackage.PTP_CleaningMedium = testPackage.PTP_CleaningMedium;
            newTestPackage.PTP_AllowSeepage = testPackage.PTP_AllowSeepage;
            newTestPackage.PTP_FactSeepage = testPackage.PTP_FactSeepage;
            newTestPackage.ProjectId = testPackage.ProjectId;
            newTestPackage.InstallationId = testPackage.InstallationId;
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除试压信息
        /// </summary>
        /// <param name="testPackageID">试压主键</param>
        public static void DeleteTP_TestPackageByTP_TestPackageID(string testPackageID)
        {
            Model.HJGLDB db = Funs.DB;
            Model.TP_TestPackage testPackage = db.TP_TestPackage.First(e => e.PTP_ID == testPackageID);
            db.TP_TestPackage.DeleteOnSubmit(testPackage);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据主键删除试压信息明细
        /// </summary>
        /// <param name="testPackageID">试压主键</param>
        public static void DeleteTP_IsoListByPTP_ID(string testPackageID)
        {
            Model.HJGLDB db = Funs.DB;
            var testPackage = from x in db.TP_IsoList where x.PTP_ID == testPackageID select x;
            if (testPackage != null)
            {
                db.TP_IsoList.DeleteAllOnSubmit(testPackage);
                db.SubmitChanges();
            }
        }

        /// <summary>
        /// 增加试压信息明细
        /// </summary>
        /// <param name="IsoList">试压明细实体</param>
        public static void AddTP_IsoList(Model.TP_IsoList IsoList)
        {
            Model.HJGLDB db = Funs.DB;
            Model.TP_IsoList newIsoList = new Model.TP_IsoList();

            newIsoList.PT_ID = SQLHelper.GetNewID(typeof(Model.TP_IsoList));
            newIsoList.PTP_ID = IsoList.PTP_ID;
            newIsoList.ISO_ID = IsoList.ISO_ID;
            newIsoList.PT_DataType = IsoList.PT_DataType;
            db.TP_IsoList.InsertOnSubmit(newIsoList);
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据单位获取试压
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public static int GetTestPackageByUnitId(string unitId)
        {
            var q = (from x in Funs.DB.TP_TestPackage where x.BSU_ID == unitId select x).ToList();
            return q.Count();
        }
        /// <summary>
        /// 根据装置获取试压
        /// </summary>
        /// <param name="installationId"></param>
        /// <returns></returns>
        public static int GetTestPackageByInstallationId(int installationId)
        {
            var q = (from x in Funs.DB.TP_TestPackage where x.InstallationId == installationId select x).ToList();
            return q.Count();
        }
    }
}
