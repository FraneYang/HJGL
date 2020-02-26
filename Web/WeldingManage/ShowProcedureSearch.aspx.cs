using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.WeldingManage
{
    public partial class ShowProcedureSearch : PPage
    {
        /// <summary>
        /// 按钮权限列表
        /// </summary>
        public string Jot_ID
        {
            get
            {
                return (string)ViewState["Jot_ID"];
            }
            set
            {
                ViewState["Jot_ID"] = value;
            }
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Jot_ID = Request.Params["jot_id"];
            }
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirm_Click(object sender, ImageClickEventArgs e)
        {
            IQueryable<Model.PW_WeldingProcedure> q = from x in Funs.DB.PW_WeldingProcedure select x;

            var jot = BLL.PW_JointInfoService.GetJointInfoByJotID(this.Jot_ID);
            if (jot != null)
            {

                var steel = BLL.MaterialService.GetSteelBySteID(jot.STE_ID);

                //母材类别
                if (this.cbMaterailType.Checked == true && steel != null)
                {
                    var mtType = BLL.MaterialService.GetSteelByMaterialType(steel.MaterialType);
                    if (mtType != null)
                    {
                        var sted = (from x in mtType select x.STE_ID).Distinct().ToList();
                        q = q.Where(x => sted.Contains(x.STE_ID));
                    }
                }

                if (this.cbWeldingMethod.Checked == true)//焊接方法
                {
                    var weldmethod = BLL.WeldingMethodService.GetWeldMethodByWMEID(jot.WME_ID);
                    if(weldmethod != null)
                    {
                        q = q.Where(x => x.WeldMethod == weldmethod.WME_Name);
                    }
                }

                if (this.cbIsHot.Checked == true)//是否热处理
                {
                    q = q.Where(x => x.PWHT != null);
                }
              
                //母材组别
                if (this.cbMaterailType.Checked == true && steel != null)
                {
                    var mtType = BLL.MaterialService.GetSteelByMaterialGroup(steel.MaterialGroup);
                    if (mtType != null)
                    {
                        var sted = (from x in mtType select x.STE_ID).Distinct().ToList();
                        q = q.Where(x => sted.Contains(x.STE_ID));
                    }                   
                }

                if (this.cbJointMaterial.Checked == true)//焊口材料
                {
                    var consumables = BLL.ConsumablesService.getConsumablesByConsumablesId(jot.JOT_WeldSilk);
                    if (consumables == null)
                    {
                        consumables = BLL.ConsumablesService.getConsumablesByConsumablesId(jot.JOT_WeldMat);
                    }

                    if (consumables != null)
                    {
                        q = q.Where(x => x.Welding == consumables.WMT_MatName);
                    }

                }
                if (this.cbSch.Checked == true)//壁厚
                {
                    if (!string.IsNullOrEmpty(jot.JOT_Sch))
                    {
                        q = q.Where(x => x.SpecimenThickness == Convert.ToDecimal(jot.JOT_Sch));
                    }
                }
                this.drpProcedure.Items.Clear();
                Funs.PleaseSelect(this.drpProcedure);

                if (q.ToList().Count() > 0)
                {
                    ListItem[] list = new ListItem[q.ToList().Count()];
                    for (int i = 0; i < q.ToList().Count(); i++)
                    {
                        list[i] = new ListItem(q.ToList()[i].WeldingProcedureCode ?? "", q.ToList()[i].WeldingProcedureId.ToString());
                    }
                    this.drpProcedure.Items.AddRange(list);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('没有相匹配的工艺评定！')", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('焊口信息不存在，请先保存焊口信息！')", true);
                return;
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            var jot = BLL.PW_JointInfoService.GetJointInfoByJotID(this.Jot_ID);
            if (jot != null)
            {
                Model.PW_WeldingProcedureJOT weldingProcedureJOT = new Model.PW_WeldingProcedureJOT();
                Model.PW_WeldingProcedure procedure = BLL.WeldingProcedureService.GetWeldingProcedureByWeldingProcedureId(this.drpProcedure.SelectedValue);
                weldingProcedureJOT.ProcedureCode = this.drpProcedure.SelectedValue;
                weldingProcedureJOT.ProcedureDate = procedure.ProcedureDate;
                weldingProcedureJOT.WeldedJoints = procedure.WeldedJoints;
                weldingProcedureJOT.GrooveForm = procedure.GrooveForm;
                weldingProcedureJOT.MaterialCode = procedure.MaterialCode;
                weldingProcedureJOT.ThicknessRange = procedure.ThicknessRange;
                weldingProcedureJOT.MaterialStandard = procedure.MaterialStandard;
                weldingProcedureJOT.MaterialType = procedure.MaterialType;
                weldingProcedureJOT.MaterialModel = procedure.MaterialModel;
                weldingProcedureJOT.MaterialSpecification = procedure.MaterialSpecification;
                weldingProcedureJOT.WeldingPosition = procedure.WeldingPosition;
                weldingProcedureJOT.HotTemperatures = procedure.HotTemperatures;
                weldingProcedureJOT.HoldingTime = procedure.HoldingTime;
                weldingProcedureJOT.PreheatingTemperature = procedure.PreheatingTemperature;
                weldingProcedureJOT.HeatingMode = procedure.HeatingMode;
                weldingProcedureJOT.GasComponent = procedure.GasComponent;
                weldingProcedureJOT.GasFlow = procedure.GasFlow;
                weldingProcedureJOT.PolarDiameter = procedure.PolarDiameter;
                weldingProcedureJOT.NozzleDiameter = procedure.NozzleDiameter;
                weldingProcedureJOT.WeldLayer = procedure.WeldLayer;
                weldingProcedureJOT.WeldMethod = procedure.WeldMethod;
                weldingProcedureJOT.CardNum = procedure.CardNum;
                weldingProcedureJOT.Diameter = procedure.Diameter;
                weldingProcedureJOT.Polarity = procedure.Polarity;
                weldingProcedureJOT.ElectricCurrent = procedure.ElectricCurrent;
                weldingProcedureJOT.Voltage = procedure.Voltage;
                weldingProcedureJOT.Speed = procedure.Speed;
                weldingProcedureJOT.LineCapacity = procedure.LineCapacity;
                weldingProcedureJOT.TestingRT = procedure.TestingRT;
                weldingProcedureJOT.TestingPT = procedure.TestingPT;
                weldingProcedureJOT.TestingMT = procedure.TestingMT;
                weldingProcedureJOT.TestingUT = procedure.TestingUT;
                weldingProcedureJOT.TestingOther = procedure.TestingOther;
                weldingProcedureJOT.TechnicalMeasures = procedure.TechnicalMeasures;
                weldingProcedureJOT.Description = procedure.Description;
                weldingProcedureJOT.ImageId = procedure.ImageId;
                weldingProcedureJOT.WType = procedure.WType;
                weldingProcedureJOT.Material = procedure.Material;
                weldingProcedureJOT.Specification = procedure.Specification;
                weldingProcedureJOT.Welding = procedure.Welding;
                weldingProcedureJOT.WRange = procedure.WRange;
                weldingProcedureJOT.Remark = procedure.Remark;
                weldingProcedureJOT.MaterialGroup = procedure.MaterialGroup;
                weldingProcedureJOT.JointsForm = procedure.JointsForm;
                weldingProcedureJOT.TubeDiameter = procedure.TubeDiameter;
                weldingProcedureJOT.SpecimenThickness = procedure.SpecimenThickness;
                weldingProcedureJOT.WeldPositionCode = procedure.WeldPositionCode;
                weldingProcedureJOT.WeldPreheating = procedure.WeldPreheating;
                weldingProcedureJOT.PWHT = procedure.PWHT;
                weldingProcedureJOT.STE_ID = procedure.STE_ID;

                if (string.IsNullOrEmpty(jot.WeldingProcedureJotId))
                {
                    weldingProcedureJOT.WWI_Code = BLL.SQLHelper.RunProcNewId("SpGetNewCodeNoProjectId", "dbo.PW_WeldingProcedureJOT", "WWI_Code", "WWI-");
                    weldingProcedureJOT.WeldingProcedureJotId = SQLHelper.GetNewID(typeof(Model.PW_WeldingProcedureJOT));                   
                    BLL.WeldingProcedureJotService.AddWeldingProdureJot(weldingProcedureJOT);
                    jot.WeldingProcedureJotId = weldingProcedureJOT.WeldingProcedureJotId;
                    BLL.PW_JointInfoService.UpdateJointInfo(jot);

                }
                else
                {
                    var weldingProcedureJOTUpdate = BLL.WeldingProcedureJotService.GetWeldingProcedureJOTByWeldingProcedureId(jot.WeldingProcedureJotId);
                    if (weldingProcedureJOTUpdate == null)
                    {
                        weldingProcedureJOTUpdate.WeldingProcedureJotId = jot.WeldingProcedureJotId;
                        BLL.WeldingProcedureJotService.UpdateWeldingProcedureJOT(weldingProcedureJOT);
                    }
                }

                ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>CloseWindows('" + this.drpProcedure.Text + "');</script>");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('焊口信息不存在,请先保存焊口信息！')", true);
                return;
            }
        }
    }
}