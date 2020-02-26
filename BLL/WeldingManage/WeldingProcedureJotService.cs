using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public static class WeldingProcedureJotService
    {
        public static Model.HJGLDB db = Funs.DB;

        public static void AddWeldingProdureJot(Model.PW_WeldingProcedureJOT weldingProcedureJot)
        {
            Model.HJGLDB db = Funs.DB;
            Model.PW_WeldingProcedureJOT newWeldingProcedureJot = new Model.PW_WeldingProcedureJOT();
            newWeldingProcedureJot.WeldingProcedureJotId = weldingProcedureJot.WeldingProcedureJotId;            
            newWeldingProcedureJot.ProcedureCode = weldingProcedureJot.ProcedureCode;
            newWeldingProcedureJot.ProcedureDate = weldingProcedureJot.ProcedureDate;
            newWeldingProcedureJot.WeldedJoints = weldingProcedureJot.WeldedJoints;
            newWeldingProcedureJot.GrooveForm = weldingProcedureJot.GrooveForm;
            newWeldingProcedureJot.MaterialCode = weldingProcedureJot.MaterialCode;
            newWeldingProcedureJot.ThicknessRange = weldingProcedureJot.ThicknessRange;
            newWeldingProcedureJot.MaterialStandard = weldingProcedureJot.MaterialStandard;
            newWeldingProcedureJot.MaterialType = weldingProcedureJot.MaterialType;
            newWeldingProcedureJot.MaterialModel = weldingProcedureJot.MaterialModel;
            newWeldingProcedureJot.MaterialSpecification = weldingProcedureJot.MaterialSpecification;
            newWeldingProcedureJot.WeldingPosition = weldingProcedureJot.WeldingPosition;
            newWeldingProcedureJot.HotTemperatures = weldingProcedureJot.HotTemperatures;
            newWeldingProcedureJot.HoldingTime = weldingProcedureJot.HoldingTime;
            newWeldingProcedureJot.PreheatingTemperature = weldingProcedureJot.PreheatingTemperature;
            newWeldingProcedureJot.HeatingMode = weldingProcedureJot.HeatingMode;
            newWeldingProcedureJot.GasComponent = weldingProcedureJot.GasComponent;
            newWeldingProcedureJot.GasFlow = weldingProcedureJot.GasFlow;
            newWeldingProcedureJot.PolarDiameter = weldingProcedureJot.PolarDiameter;
            newWeldingProcedureJot.NozzleDiameter = weldingProcedureJot.NozzleDiameter;
            newWeldingProcedureJot.WeldLayer = weldingProcedureJot.WeldLayer;
            newWeldingProcedureJot.WeldMethod = weldingProcedureJot.WeldMethod;
            newWeldingProcedureJot.CardNum = weldingProcedureJot.CardNum;
            newWeldingProcedureJot.Diameter = weldingProcedureJot.Diameter;
            newWeldingProcedureJot.Polarity = weldingProcedureJot.Polarity;
            newWeldingProcedureJot.ElectricCurrent = weldingProcedureJot.ElectricCurrent;
            newWeldingProcedureJot.Voltage = weldingProcedureJot.Voltage;
            newWeldingProcedureJot.Speed = weldingProcedureJot.Speed;
            newWeldingProcedureJot.LineCapacity = weldingProcedureJot.LineCapacity;
            newWeldingProcedureJot.TestingRT = weldingProcedureJot.TestingRT;
            newWeldingProcedureJot.TestingPT = weldingProcedureJot.TestingPT;
            newWeldingProcedureJot.TestingMT = weldingProcedureJot.TestingMT;
            newWeldingProcedureJot.TestingUT = weldingProcedureJot.TestingUT;
            newWeldingProcedureJot.TestingOther = weldingProcedureJot.TestingOther;
            newWeldingProcedureJot.TechnicalMeasures = weldingProcedureJot.TechnicalMeasures;
            newWeldingProcedureJot.Description = weldingProcedureJot.Description;
            newWeldingProcedureJot.ImageId = weldingProcedureJot.ImageId;
         
            newWeldingProcedureJot.WType = weldingProcedureJot.WType;
            newWeldingProcedureJot.Material = weldingProcedureJot.Material;
            newWeldingProcedureJot.Specification = weldingProcedureJot.Specification;
            newWeldingProcedureJot.Welding = weldingProcedureJot.Welding;
            newWeldingProcedureJot.WRange = weldingProcedureJot.WRange;
            newWeldingProcedureJot.Remark = weldingProcedureJot.Remark;
            newWeldingProcedureJot.MaterialGroup = weldingProcedureJot.MaterialGroup;
            newWeldingProcedureJot.JointsForm = weldingProcedureJot.JointsForm;
            newWeldingProcedureJot.TubeDiameter = weldingProcedureJot.TubeDiameter;
            newWeldingProcedureJot.SpecimenThickness = weldingProcedureJot.SpecimenThickness;
            newWeldingProcedureJot.WeldPositionCode = weldingProcedureJot.WeldPositionCode;
            newWeldingProcedureJot.WeldPreheating = weldingProcedureJot.WeldPreheating;
            newWeldingProcedureJot.PWHT = weldingProcedureJot.PWHT;
            newWeldingProcedureJot.STE_ID = weldingProcedureJot.STE_ID;
            newWeldingProcedureJot.WWI_Code = weldingProcedureJot.WWI_Code;
            db.PW_WeldingProcedureJOT.InsertOnSubmit(newWeldingProcedureJot);

            db.SubmitChanges();           
        }

        public static void UpdateWeldingProcedureJOT(Model.PW_WeldingProcedureJOT weldingProcedureJot)
        {
            Model.HJGLDB db = Funs.DB;
            Model.PW_WeldingProcedureJOT newWeldingProcedureJot = db.PW_WeldingProcedureJOT.FirstOrDefault(e => e.WeldingProcedureJotId == weldingProcedureJot.WeldingProcedureJotId);
            newWeldingProcedureJot.ProcedureCode = weldingProcedureJot.ProcedureCode;
            newWeldingProcedureJot.ProcedureDate = weldingProcedureJot.ProcedureDate;
            newWeldingProcedureJot.WeldedJoints = weldingProcedureJot.WeldedJoints;
            newWeldingProcedureJot.GrooveForm = weldingProcedureJot.GrooveForm;
            newWeldingProcedureJot.MaterialCode = weldingProcedureJot.MaterialCode;
            newWeldingProcedureJot.ThicknessRange = weldingProcedureJot.ThicknessRange;
            newWeldingProcedureJot.MaterialStandard = weldingProcedureJot.MaterialStandard;
            newWeldingProcedureJot.MaterialType = weldingProcedureJot.MaterialType;
            newWeldingProcedureJot.MaterialModel = weldingProcedureJot.MaterialModel;
            newWeldingProcedureJot.MaterialSpecification = weldingProcedureJot.MaterialSpecification;
            newWeldingProcedureJot.WeldingPosition = weldingProcedureJot.WeldingPosition;
            newWeldingProcedureJot.HotTemperatures = weldingProcedureJot.HotTemperatures;
            newWeldingProcedureJot.HoldingTime = weldingProcedureJot.HoldingTime;
            newWeldingProcedureJot.PreheatingTemperature = weldingProcedureJot.PreheatingTemperature;
            newWeldingProcedureJot.HeatingMode = weldingProcedureJot.HeatingMode;
            newWeldingProcedureJot.GasComponent = weldingProcedureJot.GasComponent;
            newWeldingProcedureJot.GasFlow = weldingProcedureJot.GasFlow;
            newWeldingProcedureJot.PolarDiameter = weldingProcedureJot.PolarDiameter;
            newWeldingProcedureJot.NozzleDiameter = weldingProcedureJot.NozzleDiameter;
            newWeldingProcedureJot.WeldLayer = weldingProcedureJot.WeldLayer;
            newWeldingProcedureJot.WeldMethod = weldingProcedureJot.WeldMethod;
            newWeldingProcedureJot.CardNum = weldingProcedureJot.CardNum;
            newWeldingProcedureJot.Diameter = weldingProcedureJot.Diameter;
            newWeldingProcedureJot.Polarity = weldingProcedureJot.Polarity;
            newWeldingProcedureJot.ElectricCurrent = weldingProcedureJot.ElectricCurrent;
            newWeldingProcedureJot.Voltage = weldingProcedureJot.Voltage;
            newWeldingProcedureJot.Speed = weldingProcedureJot.Speed;
            newWeldingProcedureJot.LineCapacity = weldingProcedureJot.LineCapacity;
            newWeldingProcedureJot.TestingRT = weldingProcedureJot.TestingRT;
            newWeldingProcedureJot.TestingPT = weldingProcedureJot.TestingPT;
            newWeldingProcedureJot.TestingMT = weldingProcedureJot.TestingMT;
            newWeldingProcedureJot.TestingUT = weldingProcedureJot.TestingUT;
            newWeldingProcedureJot.TestingOther = weldingProcedureJot.TestingOther;
            newWeldingProcedureJot.TechnicalMeasures = weldingProcedureJot.TechnicalMeasures;
            newWeldingProcedureJot.Description = weldingProcedureJot.Description;
            newWeldingProcedureJot.ImageId = weldingProcedureJot.ImageId;

            newWeldingProcedureJot.WType = weldingProcedureJot.WType;
            newWeldingProcedureJot.Material = weldingProcedureJot.Material;
            newWeldingProcedureJot.Specification = weldingProcedureJot.Specification;
            newWeldingProcedureJot.Welding = weldingProcedureJot.Welding;
            newWeldingProcedureJot.WRange = weldingProcedureJot.WRange;
            newWeldingProcedureJot.Remark = weldingProcedureJot.Remark;
            newWeldingProcedureJot.MaterialGroup = weldingProcedureJot.MaterialGroup;
            newWeldingProcedureJot.JointsForm = weldingProcedureJot.JointsForm;
            newWeldingProcedureJot.TubeDiameter = weldingProcedureJot.TubeDiameter;
            newWeldingProcedureJot.SpecimenThickness = weldingProcedureJot.SpecimenThickness;
            newWeldingProcedureJot.WeldPositionCode = weldingProcedureJot.WeldPositionCode;
            newWeldingProcedureJot.WeldPreheating = weldingProcedureJot.WeldPreheating;
            newWeldingProcedureJot.PWHT = weldingProcedureJot.PWHT;
            newWeldingProcedureJot.STE_ID = weldingProcedureJot.STE_ID;

            db.SubmitChanges();
        }

       
        /// <summary>
        /// 根据工艺评定ID获取工艺评定
        /// </summary>
        /// <param name="weldingProcedureId"></param>
        /// <returns></returns>
        public static Model.PW_WeldingProcedureJOT GetWeldingProcedureJOTByWeldingProcedureId(string weldingProcedureId)
        {
            return Funs.DB.PW_WeldingProcedureJOT.FirstOrDefault(e => e.WeldingProcedureJotId == weldingProcedureId);
        }
    }
}
