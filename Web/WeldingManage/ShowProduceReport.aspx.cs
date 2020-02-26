using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.WeldingManage
{
    public partial class ShowProduceReport : PPage
    {
        /// <summary>
        /// 主表键
        /// </summary>
        public string WeldingProcedureId
        {
            get
            {
                return (string)ViewState["WeldingProcedureId"];
            }
            set
            {
                ViewState["WeldingProcedureId"] = value;
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
                this.WeldingProcedureId = Request.Params["weldingProduceId"];
                Funs.PleaseSelect(this.drpImage);
                this.drpImage.Items.AddRange(BLL.ProcedureImageService.GetImageContentList());

                if (!string.IsNullOrEmpty(WeldingProcedureId))
                {
                    var procedure = BLL.WeldingProcedureService.GetWeldingProcedureByWeldingProcedureId(WeldingProcedureId);
                    this.txtProcedureCode.Text = procedure.WeldingProcedureCode;
                    if (procedure.ProcedureDate != null)
                    {
                        this.txtProcedureDate.Value = string.Format("{0:yyyy-MM-dd}", procedure.ProcedureDate);
                    }
                    this.txtWeldedJoints.Text = procedure.WeldedJoints;
                    this.txtGrooveForm.Text = procedure.GrooveForm;
                    this.txtMaterialCode.Text = procedure.MaterialCode;
                    this.txtThicknessRange.Text = procedure.ThicknessRange;
                    this.txtMaterialStandard.Text = procedure.MaterialStandard;
                    this.txtMaterialType.Text = procedure.MaterialType;
                    this.txtMaterialModel.Text = procedure.MaterialModel;
                    this.txtMaterialSpecification.Text = procedure.MaterialSpecification;
                    this.txtWeldingPosition.Text = procedure.WeldingPosition;
                    this.txtHotTemperatures.Text = procedure.HotTemperatures;
                    if (procedure.HoldingTime != null)
                    {
                        this.txtHoldingDate.Value = string.Format("{0:yyyy-MM-dd}", procedure.HoldingTime);
                    }
                    this.txtPreheatingTemperature.Text = procedure.PreheatingTemperature;
                    this.txtHeatingMode.Text = procedure.HeatingMode;
                    this.txtGasComponent.Text = procedure.GasComponent;
                    this.txtGasFlow.Text = procedure.GasFlow;
                    this.txtPolarDiameter.Text = procedure.PolarDiameter;
                    this.txtNozzleDiameter.Text = procedure.NozzleDiameter;
                    this.txtWeldLayer.Text = procedure.WeldLayer;
                    this.txtWeldMethod.Text = procedure.WeldMethod;
                    this.txtCardNum.Text = procedure.CardNum;
                    this.txtDiameter.Text = procedure.Diameter;
                    this.txtPolarity.Text = procedure.Polarity;
                    this.txtElectricCurrent.Text = procedure.ElectricCurrent;
                    this.txtVoltage.Text = procedure.Voltage;
                    this.txtSpeed.Text = procedure.Speed;
                    this.txtLineCapacity.Text = procedure.LineCapacity;
                    this.txtTestingRT.Text = procedure.TestingRT;
                    this.txtTestingPT.Text = procedure.TestingPT;
                    this.txtTestingMT.Text = procedure.TestingMT;
                    this.txtTestingUT.Text = procedure.TestingUT;
                    this.txtTestingOther.Text = procedure.TestingOther;
                    this.txtTechnicalMeasures.Text = procedure.TechnicalMeasures;
                    this.txtDescription.Text = procedure.Description;

                    if (!string.IsNullOrEmpty(procedure.ImageId))
                    {
                        this.drpImage.SelectedValue = procedure.ImageId;
                        string temporarySavePath = BLL.ProcedureImageService.GetImageById(procedure.ImageId).AttachUrl;
                        this.imgURL.ImageUrl = "~/" + temporarySavePath;
                    }
                }
            }
        }
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            var procedure = BLL.WeldingProcedureService.GetWeldingProcedureByWeldingProcedureId(WeldingProcedureId);           
            if (!string.IsNullOrEmpty(txtProcedureDate.Value))
            {
                procedure.ProcedureDate = Convert.ToDateTime(this.txtProcedureDate.Value);
            }
            procedure.WeldedJoints = this.txtWeldedJoints.Text.Trim();
            procedure.GrooveForm = this.txtGrooveForm.Text.Trim();
            procedure.MaterialCode = this.txtMaterialCode.Text.Trim();
            procedure.ThicknessRange = this.txtThicknessRange.Text.Trim();
            procedure.MaterialStandard = this.txtMaterialStandard.Text.Trim();
            procedure.MaterialType = this.txtMaterialType.Text.Trim();
            procedure.MaterialModel = this.txtMaterialModel.Text.Trim();
            procedure.MaterialSpecification = this.txtMaterialSpecification.Text.Trim();
            procedure.WeldingPosition = this.txtWeldingPosition.Text.Trim();
            procedure.HotTemperatures = this.txtHotTemperatures.Text.Trim();
            if (!string.IsNullOrEmpty(this.txtHoldingDate.Value))
            {
                procedure.HoldingTime = Convert.ToDateTime(this.txtHoldingDate.Value.Trim());
            }

            procedure.PreheatingTemperature = this.txtPreheatingTemperature.Text.Trim();
            procedure.HeatingMode = this.txtHeatingMode.Text.Trim();
            procedure.GasComponent = this.txtGasComponent.Text.Trim();
            procedure.GasFlow = this.txtGasFlow.Text.Trim();
            procedure.PolarDiameter = this.txtPolarDiameter.Text.Trim();
            procedure.NozzleDiameter = this.txtNozzleDiameter.Text.Trim();
            procedure.WeldLayer = this.txtWeldLayer.Text.Trim();
            procedure.WeldMethod = this.txtWeldMethod.Text.Trim();
            procedure.CardNum = this.txtCardNum.Text.Trim();
            procedure.Diameter = this.txtDiameter.Text.Trim();
            procedure.Polarity = this.txtPolarity.Text.Trim();
            procedure.ElectricCurrent = this.txtElectricCurrent.Text.Trim();
            procedure.Voltage = this.txtVoltage.Text.Trim();
            procedure.Speed = this.txtSpeed.Text.Trim();
            procedure.LineCapacity = this.txtLineCapacity.Text.Trim();
            procedure.TestingRT = this.txtTestingRT.Text.Trim();
            procedure.TestingPT = this.txtTestingPT.Text.Trim();
            procedure.TestingMT = this.txtTestingMT.Text.Trim();
            procedure.TestingUT = this.txtTestingUT.Text.Trim();
            procedure.TestingOther = this.txtTestingOther.Text.Trim();
            procedure.TechnicalMeasures = this.txtTechnicalMeasures.Text.Trim();
            procedure.Description = this.txtDescription.Text.Trim();
            if (this.drpImage.SelectedValue!="0")
            {
                procedure.ImageId = this.drpImage.SelectedValue;
            }

            if (!string.IsNullOrEmpty(WeldingProcedureId))
            {
                procedure.WeldingProcedureId = WeldingProcedureId;
                BLL.WeldingProcedureService.UpdateWeldProcedure(procedure);
                BLL.LogService.AddLog(this.CurrUser.UserId, "修改焊接工艺！");
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！');OnClientClick=window.close();", true);
            }
        }

        /// <summary>
        /// 选择工艺图片内容显示相应的图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpImage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpImage.SelectedValue != "0")
            {
                string imageId = this.drpImage.SelectedValue;
                string temporarySavePath = BLL.ProcedureImageService.GetImageById(imageId).AttachUrl;
                this.imgURL.ImageUrl = "~/" + temporarySavePath;
            }
        }
    }
}