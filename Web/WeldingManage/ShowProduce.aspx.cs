using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BLL;
using System.Net;

namespace Web.WeldingManage
{
    public partial class ShowProduce : PPage
    {
        /// <summary>
        /// 照片名称
        /// </summary>
        public string FileName
        {
            get
            {
                return (string)ViewState["FileName"];
            }
            set
            {
                ViewState["FileName"] = value;
            }
        }

        /// <summary>
        /// 上传照的物理路径
        /// </summary>
        public string ImageUrl
        {
            get
            {
                return (string)ViewState["ImageUrl"];
            }
            set
            {
                ViewState["ImageUrl"] = value;
            }
        }

        public string WeldingProcedureJotId
        {
            get
            {
                return (string)ViewState["WeldingProcedureJotId"];
            }
            set
            {
                ViewState["WeldingProcedureJotId"] = value;
            }
        }

        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        //private string initPath = Const.ExcelUrl;
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                WeldingProcedureJotId = Request.QueryString["weldingProcedureJotId"].ToString();
                if (!string.IsNullOrEmpty(WeldingProcedureJotId))
                {
                    var pro = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.ProjectId);
                    if(pro != null)
                    {
                        this.txtProjectName.Text = pro.ProjectName; //项目名称           
                    }

                    var procedure = BLL.WeldingProcedureJotService.GetWeldingProcedureJOTByWeldingProcedureId(WeldingProcedureJotId);
                    if (procedure != null)
                    {
                        this.txtProduceCode.Text = procedure.ProcedureCode; //工艺评定编号
                        this.txtSpecimenThickness.Text = Convert.ToString(procedure.SpecimenThickness); //母材壁厚
                        if (!string.IsNullOrEmpty(this.txtSpecimenThickness.Text))
                        {


                            decimal thickness =(5500 / Convert.ToDecimal(this.txtSpecimenThickness.Text));//升温速度
                            if (thickness <= 55)
                            {
                                this.txtUpTemperatureSpeed.Text = "55";
                            }
                            else if (thickness >= 220)
                            {
                                this.txtUpTemperatureSpeed.Text = "220";
                            }
                            else
                            {
                                this.txtUpTemperatureSpeed.Text = thickness.ToString("0.00");
                            }

                            decimal dowmThickness = 7000 / Convert.ToDecimal(this.txtSpecimenThickness.Text.Trim());//降温速度
                            if (dowmThickness <= 55)
                            {
                                this.txtDownsTemperatureSpeed.Text = "55";
                            }
                            else if (dowmThickness >= 280)
                            {
                                this.txtDownsTemperatureSpeed.Text = "280";
                            }
                            else
                            {
                                this.txtDownsTemperatureSpeed.Text = dowmThickness.ToString("0.00"); 
                            }
                        }

                        this.labWWI.Text = procedure.WWI_Code;
                        this.txtMaterialGroup.Text = procedure.MaterialGroup;
                        this.txtWeldPosition.Text = procedure.WeldPositionCode;
                        this.txtJointsForm.Text = procedure.JointsForm;
                        this.txtWeldPosition.Text = procedure.WeldPositionCode;//焊接位置            
                        this.txtPostheatTemperature.Text = procedure.PWHT;//焊后热处理
                    
                        this.txtTungstenLiameter1.Text = procedure.PolarDiameter;
                        this.txtNozzlediameter1.Text = procedure.NozzleDiameter;
                        this.txtGasComposition.Text = procedure.GasComponent;
                        this.txtGasFlow.Text = procedure.GasFlow;
                        this.txtLayerTogether.Text = procedure.WeldLayer;
                        this.txtWME_ID.Text = procedure.WeldMethod;
                        this.txtElectricity.Text = procedure.ElectricCurrent;
                        this.txtVoltage.Text = procedure.Voltage;
                        this.txtKindsPolarity.Text = procedure.Polarity;
                        this.txtHeatInput.Text = procedure.LineCapacity;
                        this.txtPreheatTemperature.Text = procedure.PreheatingTemperature;
                        this.txtHeatingMeans.Text = procedure.HeatingMode;
                        this.txtCheckRT.Text = procedure.TestingRT;
                        this.txtCheckUT.Text = procedure.TestingUT;
                        this.txtCheckMT.Text = procedure.TestingMT;
                        this.txtCheckPT.Text = procedure.TestingPT;
                        this.txtOther.Text = procedure.TestingOther;
                        this.txtHotDisposeT.Text = procedure.HotTemperatures;
                        this.txtCoolingRate.Text = string.Format("{0:yyyy-MM-dd}", procedure.HoldingTime);
                        this.txtTechnicalMeasure.Text = procedure.TechnicalMeasures;
                        this.txtOtherDescription.Text = procedure.Description;
                        if (!string.IsNullOrEmpty(procedure.ImageId))
                        {
                            this.ImageUrl = BLL.ProcedureImageService.GetImageById(procedure.ImageId).AttachUrl;
                            this.imgPhoto.ImageUrl = "~/" + ImageUrl;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, ImageClickEventArgs e)
        {
            DateTime dt = DateTime.Now;
            string filename = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write("<meta http-equiv=Content-Type content=text/html;charset=UTF-8>");
            this.tr1.Visible = false;

            ////图片 导出要取绝对物理地址
            this.imgPhoto.ImageUrl = Server.MapPath("~/") +this.ImageUrl;
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("焊接工艺评定" + filename, System.Text.Encoding.UTF8) + ".doc");
            Response.ContentType = "application/ms-excel";
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            this.Table1.RenderControl(oHtmlTextWriter);
            Response.Write(oStringWriter.ToString());
            Response.Flush();
            Response.End();
        }

        /// <summary>
        /// 重载VerifyRenderingInServerForm方法，否则运行的时候会出现如下错误提示：“类型“GridView”的控件“GridView1”必须放在具有 runat=server 的窗体标记内”
        /// </summary>
        /// <param name="control"></param>
        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(WeldingProcedureJotId))
            {
                Model.PW_WeldingProcedureJOT wpj = BLL.WeldingProcedureJotService.GetWeldingProcedureJOTByWeldingProcedureId(WeldingProcedureJotId);
                if (wpj != null)
                {
                    wpj.ProcedureCode = this.txtProduceCode.Text.Trim();                                      
                    wpj.PolarDiameter = this.txtTungstenLiameter1.Text.Trim();
                    wpj.NozzleDiameter = this.txtNozzlediameter1.Text.Trim();
                    wpj.GasComponent = this.txtGasComposition.Text.Trim();
                    wpj.GasFlow = this.txtGasFlow.Text.Trim();
                    wpj.WeldLayer = this.txtLayerTogether.Text.Trim();
                    wpj.WeldMethod = this.txtWME_ID.Text.Trim();
                    wpj.ElectricCurrent = this.txtElectricity.Text.Trim();
                    wpj.Voltage = this.txtVoltage.Text.Trim();
                    wpj.Polarity = this.txtKindsPolarity.Text.Trim();
                    wpj.LineCapacity = this.txtHeatInput.Text.Trim();
                    wpj.PreheatingTemperature = this.txtPreheatTemperature.Text.Trim();
                    wpj.HeatingMode = this.txtHeatingMeans.Text.Trim();
                    wpj.TestingRT = this.txtCheckRT.Text.Trim();
                    wpj.TestingUT = this.txtCheckUT.Text.Trim();
                    wpj.TestingMT = this.txtCheckMT.Text.Trim();
                    wpj.TestingPT = this.txtCheckPT.Text.Trim();
                    wpj.TestingOther = this.txtOther.Text.Trim();
                    wpj.HotTemperatures = this.txtHotDisposeT.Text.Trim();
                    if (!string.IsNullOrEmpty(this.txtCoolingRate.Text.Trim()))
                    {
                        wpj.HoldingTime = Convert.ToDateTime(this.txtCoolingRate.Text.Trim());
                    }                   
                    wpj.TechnicalMeasures = this.txtTechnicalMeasure.Text.Trim();
                    wpj.Description = this.txtOtherDescription.Text.Trim();

                    BLL.WeldingProcedureJotService.UpdateWeldingProcedureJOT(wpj);
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！');OnClientClick=window.close();", true);
                }
            }
        }
    }
}