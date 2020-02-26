using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI.WebControls;

namespace BLL
{
    public class WeldingProcedureService
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
        private static IQueryable<Model.PW_WeldingProcedure> qq = from x in db.PW_WeldingProcedure orderby x.WType, x.WeldingProcedureCode select x;

        /// <summary>
        /// 获取管线列表
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="searchValue"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static IEnumerable getListData(string weldingProcedureId, string wType, string ste_Id, string specification, string welding, string wRange, string jointsForm, string tubeDiameter, string specimenThickness, string weldMethod, string weldPositionCode, string weldPreheating, string pWHT, int startRowIndex, int maximumRows)
        {
            IQueryable<Model.PW_WeldingProcedure> q = qq;
            if (!string.IsNullOrEmpty(weldingProcedureId))
            {
                q = q.Where(e => e.WeldingProcedureCode.Contains(weldingProcedureId));
            }
            if (!string.IsNullOrEmpty(wType))
            {
                q = q.Where(e => e.WType.Contains(wType));
            }
            if (ste_Id != "0")
            {
                q = q.Where(e => e.STE_ID == ste_Id);
            }
            if (!string.IsNullOrEmpty(specification))
            {
                q = q.Where(e => e.Specification.Contains(specification));
            }
            if (!string.IsNullOrEmpty(welding))
            {
                q = q.Where(e => e.Welding.Contains(welding));
            }
            if (!string.IsNullOrEmpty(wRange))
            {
                q = q.Where(e => e.WRange.Contains(wRange));
            }
            if (!string.IsNullOrEmpty(jointsForm))
            {
                q = q.Where(e => e.JointsForm.Contains(jointsForm));
            }
            if (!string.IsNullOrEmpty(tubeDiameter))
            {
                q = q.Where(e => e.TubeDiameter.Contains(tubeDiameter));
            }
            if (!string.IsNullOrEmpty(specimenThickness))
            {
                q = q.Where(e => e.SpecimenThickness == Convert.ToDecimal(specimenThickness));
            }
            if (!string.IsNullOrEmpty(weldMethod))
            {
                q = q.Where(e => e.WeldMethod.Contains(weldMethod));
            }
            if (!string.IsNullOrEmpty(weldPositionCode))
            {
                q = q.Where(e => e.WeldPositionCode.Contains(weldPositionCode));
            }
            if (!string.IsNullOrEmpty(weldPreheating))
            {
                q = q.Where(e => e.WeldPreheating.Contains(weldPreheating));
            }
            if (!string.IsNullOrEmpty(pWHT))
            {
                q = q.Where(e => e.PWHT.Contains(pWHT));
            }           
            count = q.Count();
            if (count == 0)
            {
                return new object[] { "" };
            }
            return from x in q.Skip(startRowIndex).Take(maximumRows)
                   select new
                   {
                       x.WeldingProcedureId,
                       x.WeldingProcedureCode,
                       x.WType,
                       x.Material,
                       x.Specification,
                       x.Welding,
                       x.WRange,
                       x.Remark,
                       x.JointsForm,
                       x.TubeDiameter,
                       x.SpecimenThickness,
                       x.WeldMethod,
                       x.WeldPositionCode,
                       x.WeldPreheating,
                       x.PWHT,
                       STE_Name = (from y in db.BS_Steel where y.STE_ID == x.STE_ID select y.STE_Name).First(),
                       MaterialType = (from y in db.BS_Steel where y.STE_ID == x.STE_ID select y.MaterialType).First(),
                       MaterialGroups = (from y in db.BS_Steel where y.STE_ID == x.STE_ID select y.MaterialGroup).First()
                   };
        }

        /// <summary>
        /// 获取列表数
        /// </summary>
        /// <param name="searchItem"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int getListCount(string weldingProcedureId, string wType, string ste_Id, string specification, string welding, string wRange, string jointsForm, string tubeDiameter, string specimenThickness, string weldMethod, string weldPositionCode, string weldPreheating, string pWHT)
        {
            return count;
        }

        /// <summary>
        /// 增加焊接工艺评定信息
        /// </summary>
        /// <param name="weldProcedure">焊接工艺评定实体</param>
        public static void AddWeldProcedure(Model.PW_WeldingProcedure weldProcedure)
        {
            Model.HJGLDB db = Funs.DB;
            Model.PW_WeldingProcedure newWeldProcedure = new Model.PW_WeldingProcedure();
            newWeldProcedure.WeldingProcedureId = SQLHelper.GetNewID(typeof(Model.PW_WeldingProcedure));
            newWeldProcedure.WeldingProcedureCode = weldProcedure.WeldingProcedureCode;
            newWeldProcedure.WType = weldProcedure.WType;
            newWeldProcedure.Material = weldProcedure.Material;
            newWeldProcedure.Specification = weldProcedure.Specification;
            newWeldProcedure.Welding = weldProcedure.Welding;
            newWeldProcedure.WRange = weldProcedure.WRange;
            newWeldProcedure.Remark = weldProcedure.Remark;
            newWeldProcedure.IsAdd = weldProcedure.IsAdd;
            newWeldProcedure.MaterialGroup = weldProcedure.MaterialGroup;
            newWeldProcedure.JointsForm = weldProcedure.JointsForm;
            newWeldProcedure.TubeDiameter = weldProcedure.TubeDiameter;
            newWeldProcedure.SpecimenThickness = weldProcedure.SpecimenThickness;
            newWeldProcedure.WeldMethod = weldProcedure.WeldMethod;
            newWeldProcedure.WeldPositionCode = weldProcedure.WeldPositionCode;
            newWeldProcedure.WeldPreheating = weldProcedure.WeldPreheating;
            newWeldProcedure.PWHT = weldProcedure.PWHT;           
            newWeldProcedure.STE_ID = weldProcedure.STE_ID;
            newWeldProcedure.ProcedureDate = weldProcedure.ProcedureDate;
            newWeldProcedure.WeldedJoints = weldProcedure.WeldedJoints;
            newWeldProcedure.GrooveForm = weldProcedure.GrooveForm;
            newWeldProcedure.MaterialCode = weldProcedure.MaterialCode;
            newWeldProcedure.ThicknessRange = weldProcedure.ThicknessRange;
            newWeldProcedure.MaterialStandard = weldProcedure.MaterialStandard;
            newWeldProcedure.MaterialType = weldProcedure.MaterialType;
            newWeldProcedure.MaterialModel = weldProcedure.MaterialModel;
            newWeldProcedure.MaterialSpecification = weldProcedure.MaterialSpecification;
            newWeldProcedure.WeldingPosition = weldProcedure.WeldingPosition;
            newWeldProcedure.HotTemperatures = weldProcedure.HotTemperatures;
            newWeldProcedure.HoldingTime = weldProcedure.HoldingTime;
            newWeldProcedure.PreheatingTemperature = weldProcedure.PreheatingTemperature;
            newWeldProcedure.HeatingMode = weldProcedure.HeatingMode;
            newWeldProcedure.GasComponent = weldProcedure.GasComponent;
            newWeldProcedure.GasFlow = weldProcedure.GasFlow;
            newWeldProcedure.PolarDiameter = weldProcedure.PolarDiameter;
            newWeldProcedure.NozzleDiameter = weldProcedure.NozzleDiameter;
            newWeldProcedure.WeldLayer = weldProcedure.WeldLayer;
            newWeldProcedure.WeldMethod = weldProcedure.WeldMethod;
            newWeldProcedure.CardNum = weldProcedure.CardNum;
            newWeldProcedure.Diameter = weldProcedure.Diameter;
            newWeldProcedure.Polarity = weldProcedure.Polarity;
            newWeldProcedure.ElectricCurrent = weldProcedure.ElectricCurrent;
            newWeldProcedure.Voltage = weldProcedure.Voltage;
            newWeldProcedure.Speed = weldProcedure.Speed;
            newWeldProcedure.LineCapacity = weldProcedure.LineCapacity;
            newWeldProcedure.TestingRT = weldProcedure.TestingRT;
            newWeldProcedure.TestingPT = weldProcedure.TestingPT;
            newWeldProcedure.TestingMT = weldProcedure.TestingMT;
            newWeldProcedure.TestingUT = weldProcedure.TestingUT;
            newWeldProcedure.TestingOther = weldProcedure.TestingOther;
            newWeldProcedure.TechnicalMeasures = weldProcedure.TechnicalMeasures;
            newWeldProcedure.Description = weldProcedure.Description;
            newWeldProcedure.ImageId = weldProcedure.ImageId;

            db.PW_WeldingProcedure.InsertOnSubmit(newWeldProcedure);
            db.SubmitChanges();
        }

        /// <summary>
        /// 修改工艺评定
        /// </summary>
        /// <param name="weldReport">焊接日报实体</param>
        public static void UpdateWeldProcedure(Model.PW_WeldingProcedure weldProcedure)
        {
            Model.HJGLDB db = Funs.DB;
            Model.PW_WeldingProcedure newWeldProcedure = db.PW_WeldingProcedure.First(e => e.WeldingProcedureId == weldProcedure.WeldingProcedureId);
            newWeldProcedure.WeldingProcedureCode = weldProcedure.WeldingProcedureCode;
            newWeldProcedure.WType = weldProcedure.WType;
            newWeldProcedure.Material = weldProcedure.Material;
            newWeldProcedure.Specification = weldProcedure.Specification;
            newWeldProcedure.Welding = weldProcedure.Welding;
            newWeldProcedure.WRange = weldProcedure.WRange;
            newWeldProcedure.Remark = weldProcedure.Remark;
            newWeldProcedure.MaterialGroup = weldProcedure.MaterialGroup;
            newWeldProcedure.JointsForm = weldProcedure.JointsForm;
            newWeldProcedure.TubeDiameter = weldProcedure.TubeDiameter;
            newWeldProcedure.SpecimenThickness = weldProcedure.SpecimenThickness;
            newWeldProcedure.WeldMethod = weldProcedure.WeldMethod;
            newWeldProcedure.WeldPositionCode = weldProcedure.WeldPositionCode;
            newWeldProcedure.WeldPreheating = weldProcedure.WeldPreheating;
            newWeldProcedure.PWHT = weldProcedure.PWHT;
            newWeldProcedure.STE_ID = weldProcedure.STE_ID;
            newWeldProcedure.ProcedureDate = weldProcedure.ProcedureDate;
            newWeldProcedure.WeldedJoints = weldProcedure.WeldedJoints;
            newWeldProcedure.GrooveForm = weldProcedure.GrooveForm;
            newWeldProcedure.MaterialCode = weldProcedure.MaterialCode;
            newWeldProcedure.ThicknessRange = weldProcedure.ThicknessRange;
            newWeldProcedure.MaterialStandard = weldProcedure.MaterialStandard;
            newWeldProcedure.MaterialType = weldProcedure.MaterialType;
            newWeldProcedure.MaterialModel = weldProcedure.MaterialModel;
            newWeldProcedure.MaterialSpecification = weldProcedure.MaterialSpecification;
            newWeldProcedure.WeldingPosition = weldProcedure.WeldingPosition;
            newWeldProcedure.HotTemperatures = weldProcedure.HotTemperatures;
            newWeldProcedure.HoldingTime = weldProcedure.HoldingTime;
            newWeldProcedure.PreheatingTemperature = weldProcedure.PreheatingTemperature;
            newWeldProcedure.HeatingMode = weldProcedure.HeatingMode;
            newWeldProcedure.GasComponent = weldProcedure.GasComponent;
            newWeldProcedure.GasFlow = weldProcedure.GasFlow;
            newWeldProcedure.PolarDiameter = weldProcedure.PolarDiameter;
            newWeldProcedure.NozzleDiameter = weldProcedure.NozzleDiameter;
            newWeldProcedure.WeldLayer = weldProcedure.WeldLayer;
            newWeldProcedure.WeldMethod = weldProcedure.WeldMethod;
            newWeldProcedure.CardNum = weldProcedure.CardNum;
            newWeldProcedure.Diameter = weldProcedure.Diameter;
            newWeldProcedure.Polarity = weldProcedure.Polarity;
            newWeldProcedure.ElectricCurrent = weldProcedure.ElectricCurrent;
            newWeldProcedure.Voltage = weldProcedure.Voltage;
            newWeldProcedure.Speed = weldProcedure.Speed;
            newWeldProcedure.LineCapacity = weldProcedure.LineCapacity;
            newWeldProcedure.TestingRT = weldProcedure.TestingRT;
            newWeldProcedure.TestingPT = weldProcedure.TestingPT;
            newWeldProcedure.TestingMT = weldProcedure.TestingMT;
            newWeldProcedure.TestingUT = weldProcedure.TestingUT;
            newWeldProcedure.TestingOther = weldProcedure.TestingOther;
            newWeldProcedure.TechnicalMeasures = weldProcedure.TechnicalMeasures;
            newWeldProcedure.Description = weldProcedure.Description;
            newWeldProcedure.ImageId = weldProcedure.ImageId;
            db.SubmitChanges();
        }

        /// <summary>
        /// 根据焊接工艺评定ID获取焊接工艺评定信息
        /// </summary>
        /// <param name="WeldingProcedureName"></param>
        /// <returns></returns>
        public static Model.PW_WeldingProcedure GetWeldingProcedureByWeldingProcedureId(string WeldingProcedureId)
        {
            return Funs.DB.PW_WeldingProcedure.FirstOrDefault(e => e.WeldingProcedureId == WeldingProcedureId);
        }

        /// <summary>
        /// 根据焊接工艺评定Id删除一个焊接工艺评定信息
        /// </summary>
        /// <param name="WeldingProcedureId"></param>
        public static void DeleteWeldingProcedure(string WeldingProcedureId)
        {
            Model.HJGLDB db = Funs.DB;
            Model.PW_WeldingProcedure WeldingProcedure = db.PW_WeldingProcedure.First(e => e.WeldingProcedureId == WeldingProcedureId);
            db.PW_WeldingProcedure.DeleteOnSubmit(WeldingProcedure);
            db.SubmitChanges();
        }

    }
}
