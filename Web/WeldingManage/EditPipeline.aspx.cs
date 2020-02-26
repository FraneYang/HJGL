using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.WeldingManage
{
    public partial class EditPipeline : PPage
    {

        /// <summary>
        /// 按钮权限列表
        /// </summary>
        public string[] ButtonList
        {
            get
            {
                return (string[])ViewState["ButtonList"];
            }
            set
            {
                ViewState["ButtonList"] = value;
            }
        }

        /// <summary>
        /// 区域列表
        /// </summary>
        public string AreaWork
        {
            get
            {
                return (string)ViewState["AreaWork"];
            }
            set
            {
                ViewState["AreaWork"] = value;
            }
        }

        /// <summary>
        /// 单位列表
        /// </summary>
        public string UnitId
        {
            get
            {
                return (string)ViewState["UnitId"];
            }
            set
            {
                ViewState["UnitId"] = value;
            }
        }


        /// <summary>
        /// 管线号
        /// </summary>
        public string IsoNo
        {
            get
            {
                return (string)ViewState["IsoNo"];
            }
            set
            {
                ViewState["IsoNo"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.PipelineManageMenuId);

                Funs.PleaseSelect(this.drpSER);
                this.drpSER.Items.AddRange(BLL.MediumService.GetBSServiceList());

                Funs.PleaseSelect(this.drpNDTRate);
                this.drpNDTRate.Items.AddRange(DetectionService.GetNDTRateNameList());

                Funs.PleaseSelect(this.drpNDTName);
                this.drpNDTName.Items.AddRange(BLL.TestingService.GetNDTTypeNameList());

                Funs.PleaseSelect(this.drpSTEName);
                this.drpSTEName.Items.AddRange(BLL.MaterialService.GetSteelList());

                Funs.PleaseSelect(this.drpIDName);
                this.drpIDName.Items.AddRange(BLL.PipingClassService.GetIsoClassNameList());

                string iSO_ID = Request.Params["iSO_ID"];

                if (!string.IsNullOrEmpty(iSO_ID))
                {
                    this.lblTitle.Text = "修改管线信息";
                    Model.PW_IsoInfo iso = BLL.PW_IsoInfoService.GetIsoInfoByIsoInfoId(iSO_ID);

                    IsoNo = iso.ISO_IsoNo;
                    this.txtUnitName.Text = BLL.UnitService.GetUnit(iso.BSU_ID).UnitName;
                    this.txtWorkArea.Text = BLL.WorkAreaService.getWorkAreaByWorkAreaId(iso.BAW_ID).WorkAreaCode;
                    this.UnitId = iso.BSU_ID;
                    this.AreaWork = iso.BAW_ID;

                    if (!string.IsNullOrEmpty(iso.SER_ID))
                    {
                        this.drpSER.SelectedValue = iso.SER_ID;
                    }
                    if (!string.IsNullOrEmpty(iso.NDTR_ID))
                    {
                        this.drpNDTRate.SelectedValue = iso.NDTR_ID;
                    }
                    if (!string.IsNullOrEmpty(iso.NDT_ID))
                    {
                        this.drpNDTName.SelectedValue = iso.NDT_ID;
                    }
                    this.txtISO_IsoNo.Text = iso.ISO_IsoNo;
                    this.txtISO_SysNo.Text = iso.ISO_SysNo;
                    this.txtISO_SubSysNo.Text = iso.ISO_SubSysNo;
                    this.txtISO_CwpNo.Text = iso.ISO_CwpNo;
                    this.txtISO_IsoNumber.Text = iso.ISO_IsoNumber;
                    this.txtISO_Rev.Text = iso.ISO_Rev;
                    this.txtISO_Sheet.Text = iso.ISO_Sheet;
                    if (iso.ISO_PipeQty != null)
                    {
                        this.txtISO_PipeQty.Text = iso.ISO_PipeQty.ToString();
                    }
                    this.txtISO_Paint.Text = iso.ISO_Paint;
                    this.txtISO_Insulator.Text = iso.ISO_Insulator;
                    if (!string.IsNullOrEmpty(iso.STE_ID))
                    {
                        this.drpSTEName.SelectedValue = iso.STE_ID;
                    }
                    this.txtISO_Executive.Text = iso.ISO_Executive;
                    this.txtISO_Specification.Text = iso.ISO_Specification;
                    this.txtISO_Modifier.Text = iso.ISO_Modifier;
                    if (iso.ISO_ModifyDate != null)
                    {
                        this.txtISO_ModifyDate.Value = string.Format("{0:yyyy-MM-dd}", iso.ISO_ModifyDate);
                    }
                    this.txtISO_Creator.Text = iso.ISO_Creator;
                    if (iso.ISO_CreateDate != null)
                    {
                        this.txtISO_CreateDate.Value = string.Format("{0:yyyy-MM-dd}", iso.ISO_CreateDate);
                    }
                    if (iso.ISO_DesignPress != null)
                    {
                        this.txtISO_DesignPress.Text = iso.ISO_DesignPress.ToString();
                    }
                    if (iso.ISO_DesignTemperature != null)
                    {
                        this.txtISO_DesignTemperature.Text = iso.ISO_DesignTemperature.ToString();
                    }
                    if (iso.ISO_TestPress != null)
                    {
                        this.txtISO_TestPress.Text = iso.ISO_TestPress.ToString();
                    }
                    if (iso.ISO_TestTemperature != null)
                    {
                        this.txtISO_TestTemperature.Text = iso.ISO_TestTemperature.ToString();
                    }
                    if (iso.ISO_NDTClass != null)
                    {
                        this.drpNDTClass.SelectedValue = iso.ISO_NDTClass;
                    }
                    this.txtISO_PTRate.Text = iso.ISO_PTRate;
                    this.txtISO_PTClass.Text = iso.ISO_PTClass;
                    if (iso.ISO_IfPickling != null)
                    {
                        if (iso.ISO_IfPickling == true)
                        {
                            this.drpISO_IfPickling.SelectedValue = "true";
                        }
                        else
                        {
                            this.drpISO_IfPickling.SelectedValue = "false";
                        }
                    }
                    if (iso.ISO_IfChasing != null)
                    {
                        if (iso.ISO_IfChasing == true)
                        {
                            this.drpISO_IfChasing.SelectedValue = "true";
                        }
                        else
                        {
                            this.drpISO_IfChasing.SelectedValue = "false";
                        }
                    }
                    if (iso.ISC_ID != null)
                    {
                        this.drpIDName.SelectedValue = iso.ISC_ID;
                    }
                    this.txtISO_Remark.Text = iso.ISO_Remark;
                }
                else
                {
                    UnitId = Request.Params["unitId"];
                    this.txtUnitName.Text = BLL.UnitService.GetUnit(UnitId).UnitName;
                    this.AreaWork = Request.Params["workAreaId"];
                    this.txtWorkArea.Text = BLL.WorkAreaService.getWorkAreaByWorkAreaId(AreaWork).WorkAreaCode;

                    this.lblTitle.Text = "增加管线信息";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnSave) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                Model.PW_IsoInfo iso = new Model.PW_IsoInfo();
                iso.ProjectId = this.CurrUser.ProjectId;
                iso.BSU_ID = this.UnitId;

                if (this.drpSER.SelectedValue != "0")
                {
                    iso.SER_ID = this.drpSER.SelectedValue;
                }
                if (this.drpNDTRate.SelectedValue != "0")
                {
                    iso.NDTR_ID = this.drpNDTRate.SelectedValue;
                }
                if (this.drpNDTName.SelectedValue != "0")
                {
                    iso.NDT_ID = this.drpNDTName.SelectedValue;
                }
                iso.BAW_ID = this.AreaWork;
                iso.ISO_IsoNo = this.txtISO_IsoNo.Text.Trim();
                iso.ISO_SysNo = this.txtISO_SysNo.Text.Trim();
                iso.ISO_SubSysNo = this.txtISO_SubSysNo.Text.Trim();
                iso.ISO_CwpNo = this.txtISO_CwpNo.Text.Trim();
                iso.ISO_IsoNumber = this.txtISO_IsoNumber.Text.Trim();
                iso.ISO_Rev = this.txtISO_Rev.Text.Trim();
                iso.ISO_Sheet = this.txtISO_Sheet.Text.Trim();
                if (!string.IsNullOrEmpty(this.txtISO_PipeQty.Text.Trim()))
                {
                    iso.ISO_PipeQty = Convert.ToDecimal(this.txtISO_PipeQty.Text.Trim());
                }
                iso.ISO_Paint = this.txtISO_Paint.Text.Trim();
                iso.ISO_Insulator = this.txtISO_Insulator.Text.Trim();
                if (this.drpSTEName.SelectedValue != "0")
                {
                    iso.STE_ID = this.drpSTEName.SelectedValue;
                }
                iso.ISO_Executive = this.txtISO_Executive.Text.Trim();
                iso.ISO_Specification = this.txtISO_Specification.Text.Trim();
                iso.ISO_Modifier = this.txtISO_Modifier.Text.Trim();
                if (!string.IsNullOrEmpty(this.txtISO_ModifyDate.Value.Trim()))
                {
                    iso.ISO_ModifyDate = Convert.ToDateTime(this.txtISO_ModifyDate.Value.Trim());
                }
                iso.ISO_Creator = this.txtISO_Creator.Text.Trim();
                if (!string.IsNullOrEmpty(this.txtISO_CreateDate.Value.Trim()))
                {
                    iso.ISO_CreateDate = Convert.ToDateTime(this.txtISO_CreateDate.Value.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtISO_DesignPress.Text.Trim()))
                {
                    iso.ISO_DesignPress = Convert.ToDecimal(this.txtISO_DesignPress.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtISO_DesignTemperature.Text.Trim()))
                {
                    iso.ISO_DesignTemperature = Convert.ToDecimal(this.txtISO_DesignTemperature.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtISO_TestPress.Text.Trim()))
                {
                    iso.ISO_TestPress = Convert.ToDecimal(this.txtISO_TestPress.Text.Trim());
                }
                if (!string.IsNullOrEmpty(this.txtISO_TestTemperature.Text.Trim()))
                {
                    iso.ISO_TestTemperature = Convert.ToDecimal(this.txtISO_TestTemperature.Text.Trim());
                }
                if (this.drpNDTClass.SelectedValue != "0")
                {
                    iso.ISO_NDTClass = this.drpNDTClass.SelectedValue;
                }
                iso.ISO_PTRate = this.txtISO_PTRate.Text.Trim();
                iso.ISO_Sheet = this.txtISO_Sheet.Text.Trim();
                iso.ISO_PTClass = this.txtISO_PTClass.Text.Trim();
                if (this.drpISO_IfPickling.SelectedValue != "0")
                {
                    iso.ISO_IfPickling = Convert.ToBoolean(this.drpISO_IfPickling.SelectedValue);
                }
                if (this.drpISO_IfChasing.SelectedValue != "0")
                {
                    iso.ISO_IfChasing = Convert.ToBoolean(this.drpISO_IfChasing.SelectedValue);
                }
                if (this.drpIDName.SelectedValue != "0")
                {
                    iso.ISC_ID = this.drpIDName.SelectedValue;
                }
                iso.ISO_Remark = this.txtISO_Remark.Text.Trim();
                string iSO_ID = Request.Params["iSO_ID"];

                if (!string.IsNullOrEmpty(iSO_ID))
                {
                    iso.ISO_ID = iSO_ID;
                    if (this.txtISO_IsoNo.Text != IsoNo)
                    {
                        if (BLL.PW_IsoInfoService.IsExistIsoInfoCode(this.txtISO_IsoNo.Text.Trim(), this.AreaWork) == true)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('这个区域已存在这条管线，不能修改！')", true);
                            return;
                        }
                        else
                        {
                            BLL.PW_IsoInfoService.UpdateIsoInfo(iso);
                            BLL.LogService.AddLog(this.CurrUser.UserId, "修改管线信息");
                        }
                    }
                    else
                    {
                        BLL.PW_IsoInfoService.UpdateIsoInfo(iso);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "修改管线信息");
                    }
                   
                }
                else
                {
                    string newKeyID = SQLHelper.GetNewID(typeof(Model.PW_IsoInfo));
                    iso.ISO_ID = newKeyID;
                    if (BLL.PW_IsoInfoService.IsExistIsoInfoCode(this.txtISO_IsoNo.Text.Trim(), this.AreaWork) == true)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('这个区域已存在这条管线，不能增加！')", true);
                        return;
                    }
                    else
                    {
                        BLL.PW_IsoInfoService.AddIsoInfo(iso);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "增加管线信息");
                    }
                }
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "WindowClose('OK')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }
    }
}