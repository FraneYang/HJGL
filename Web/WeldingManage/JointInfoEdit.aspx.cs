using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.WeldingManage
{
    public partial class JointInfoEdit : PPage
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

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.JointInfoMenuId);

                string iso_id = Request.Params["iso_id"];
                this.txtISONO.Text = BLL.PW_IsoInfoService.GetIsoInfoByIsoInfoId(iso_id).ISO_IsoNo;
                string workareaid = Request.Params["workArea"];
                this.txtWorkAreaId.Text = BLL.WorkAreaService.getWorkAreaByWorkAreaId(workareaid).WorkAreaCode;

                LoadDropDownList(); ///加载下拉列表值
                this.txtReport.Text = "未焊接";
                this.txtPoint.Text = "未点口";
                voluation();  //赋值
                if (this.txtReport.Text.Trim()=="已焊接")
                {
                    TextIsReadOnly(true);
                }
                else if (this.txtPoint.Text.Trim()=="已点口")
                {
                    TextIsReadOnly(true);
                }
               
            }
        }
        #endregion

        #region 加载下了列表值
        /// <summary>
        /// 加载下拉列表值
        /// </summary>
        private void LoadDropDownList()
        {
            Funs.PleaseSelect(ddlSTE1);
            this.ddlSTE1.Items.AddRange(BLL.MaterialService.GetSteelList());
            Funs.PleaseSelect(ddlSTE2);
            this.ddlSTE2.Items.AddRange(BLL.MaterialService.GetSteelList());

            Funs.PleaseSelect(ddlComponent1);
            this.ddlComponent1.Items.AddRange(BLL.ComponentsService.GetComponentNameList());
            Funs.PleaseSelect(ddlComponent2);
            this.ddlComponent2.Items.AddRange(BLL.ComponentsService.GetComponentNameList());

            Funs.PleaseSelect(ddlJOTY_ID);
            this.ddlJOTY_ID.Items.AddRange(BLL.WeldService.GetJointTypeNameList());

            Funs.PleaseSelect(ddlJST_ID);
            this.ddlJST_ID.Items.AddRange(BLL.GrooveService.GetSlopeTypeNameList());

            Funs.PleaseSelect(ddlWME_ID);
            this.ddlWME_ID.Items.AddRange(BLL.WeldingMethodService.GetWeldMethodNameList());

            Funs.PleaseSelect(ddlWeldSilk);
            this.ddlWeldSilk.Items.AddRange(BLL.ConsumablesService.GetMaterialSilkList());

            Funs.PleaseSelect(ddlWeldMat);
            this.ddlWeldMat.Items.AddRange(BLL.ConsumablesService.GetMaterialWeldMatList());

            Funs.PleaseSelect(ddlReportCode);
            this.ddlReportCode.Items.AddRange(BLL.WeldReportService.GetWeldReportList());
        }
        #endregion

        #region 文本框是否可编辑
        /// <summary>
        /// 文本框是否可编辑
        /// </summary>
        /// <param name="readOnly"></param>
        private void TextIsReadOnly(bool readOnly)
        {
            this.txtJointNo.Enabled = !readOnly;
            this.ddlWLO_CODE.Enabled = !readOnly;
            //this.ddlSTE1.Enabled = !readOnly;
            //this.ddlSTE2.Enabled = !readOnly;
            this.txtBelongPipe.Enabled = !readOnly;
            //this.ddlComponent1.Enabled = !readOnly;
            //this.ddlComponent2.Enabled = !readOnly;
            //this.txtJointDesc.Enabled = !readOnly;
            //this.txtHeartNo1.Enabled = !readOnly;
            //this.txtHeartNo2.Enabled = !readOnly;
            //this.ddlJOTY_ID.Enabled = !readOnly;            
            if (this.CurrUser.Account != Const.AdminId)
            {
                this.txtSize.Enabled = !readOnly; //兰化项目寸径存在空的情况,需要修改为可修改。
            }
            //this.txtDia.Enabled = !readOnly;
            //this.ddlJST_ID.Enabled = !readOnly;
            //this.txtSch.Enabled = !readOnly;
            //this.txtFactSch.Enabled = !readOnly;
            //this.txtLastTemp.Enabled = !readOnly;
            //this.txtCellTemp.Enabled = !readOnly;
            //this.txtPrepareTemp.Enabled = !readOnly;
            this.ddlJointAttribute.Enabled = !readOnly;
            //this.ddlWME_ID.Enabled = !readOnly;
            //this.ddlWeldSilk.Enabled = !readOnly;
            //this.ddlWeldMat.Enabled = !readOnly;
            //this.txtElectricity.Enabled = !readOnly;
            //this.txtVoltage.Enabled = !readOnly;
            //this.txtRemark.Enabled = !readOnly;
        }
        #endregion

        #region 赋值
        /// <summary>
        /// 赋值
        /// </summary>
        private void voluation()
        {
            string jot_id = Request.Params["jot_id"];
            if (!string.IsNullOrEmpty(jot_id))
            {
                this.lblTitle.Text = "修改焊口信息";
                Model.PW_JointInfo jointInfo = BLL.PW_JointInfoService.GetJointInfoByJotID(jot_id);
                this.txtJointNo.Text = jointInfo.JOT_JointNo;
                if (!string.IsNullOrEmpty(jointInfo.WLO_Code))
                {
                    this.ddlWLO_CODE.SelectedValue = jointInfo.WLO_Code;
                }
               
                if (!string.IsNullOrEmpty(jointInfo.STE_ID))
                {
                    this.ddlSTE1.SelectedValue = jointInfo.STE_ID;
                }
                else
                {
                    this.ddlSTE1.SelectedValue = "0";
                }
                if (!string.IsNullOrEmpty(jointInfo.STE_ID2))
                {
                    this.ddlSTE2.SelectedValue = jointInfo.STE_ID2;
                }
                else
                {
                    this.ddlSTE2.SelectedValue = "0";
                }

                this.txtBelongPipe.Text = jointInfo.JOT_BelongPipe;

                if (!string.IsNullOrEmpty(jointInfo.JOT_Component1))
                {
                    this.ddlComponent1.SelectedValue = jointInfo.JOT_Component1;
                }
                else
                {
                    this.ddlComponent1.SelectedValue = "0";
                }
                if (!string.IsNullOrEmpty(jointInfo.JOT_Component2))
                {
                    this.ddlComponent2.SelectedValue = jointInfo.JOT_Component2;
                }
                else
                {
                    this.ddlComponent2.SelectedValue = "0";
                }
                this.txtJointDesc.Text = jointInfo.JOT_JointDesc;
                this.txtHeartNo1.Text = jointInfo.JOT_HeartNo1;
                this.txtHeartNo2.Text = jointInfo.JOT_HeartNo2;

                if (!string.IsNullOrEmpty(jointInfo.JOTY_ID))
                {
                    this.ddlJOTY_ID.SelectedValue = jointInfo.JOTY_ID;
                }
                else
                {
                    this.ddlJOTY_ID.SelectedValue = "0";
                }
                if (jointInfo.JOT_Size != 0)
                {
                    this.txtSize.Text = Convert.ToString(jointInfo.JOT_Size);
                }
                else
                {
                    this.txtDia.Text = Convert.ToString(0);
                }
                if (jointInfo.JOT_Dia != 0)
                {
                    this.txtDia.Text = Convert.ToString(jointInfo.JOT_Dia);
                }
                else
                {
                    this.txtDia.Text = Convert.ToString(0);
                }
                if (!string.IsNullOrEmpty(jointInfo.JST_ID))
                {
                    this.ddlJST_ID.SelectedValue = jointInfo.JST_ID;
                }
                else
                {
                    this.ddlJST_ID.SelectedValue = "0";
                }
                this.txtSch.Text = jointInfo.JOT_Sch;
                if (jointInfo.JOT_FactSch != 0)
                {
                    this.txtFactSch.Text = Convert.ToString(jointInfo.JOT_FactSch);
                }
                else
                {
                    this.txtFactSch.Text = Convert.ToString(0);
                }
                if (jointInfo.JOT_LastTemp != 0)
                {
                    this.txtLastTemp.Text = Convert.ToString(jointInfo.JOT_LastTemp);
                }
                else
                {
                    this.txtLastTemp.Text = Convert.ToString(0);
                }
                if (jointInfo.JOT_CellTemp != 0)
                {
                    this.txtCellTemp.Text = Convert.ToString(jointInfo.JOT_CellTemp);
                }
                else
                {
                    this.txtCellTemp.Text = Convert.ToString(0);
                }
                if (jointInfo.JOT_PrepareTemp != 0)
                {
                    this.txtPrepareTemp.Text = Convert.ToString(jointInfo.JOT_PrepareTemp);
                }
                else
                {
                    this.txtPrepareTemp.Text = Convert.ToString(0);
                }
                if (!string.IsNullOrEmpty(jointInfo.JOT_JointAttribute))
                {
                    this.ddlJointAttribute.SelectedValue = jointInfo.JOT_JointAttribute;
                }
                else
                {
                    this.ddlJointAttribute.SelectedValue = "0";
                }
                if (!string.IsNullOrEmpty(jointInfo.WME_ID))
                {
                    this.ddlWME_ID.SelectedValue = jointInfo.WME_ID;
                }
                else
                {
                    this.ddlWME_ID.SelectedValue = "0";
                }
                if (!string.IsNullOrEmpty(jointInfo.JOT_WeldSilk))
                {
                    this.ddlWeldSilk.SelectedValue = jointInfo.JOT_WeldSilk;
                }
                else
                {
                    this.ddlWeldSilk.SelectedValue = "0";
                }
                if (!string.IsNullOrEmpty(jointInfo.JOT_WeldMat))
                {
                    this.ddlWeldMat.SelectedValue = jointInfo.JOT_WeldMat;
                }
                else
                {
                    this.ddlWeldMat.SelectedValue = "0";
                }
                this.txtElectricity.Text = jointInfo.JOT_Electricity;
                this.txtVoltage.Text = jointInfo.JOT_Voltage;
                if (!string.IsNullOrEmpty(jointInfo.IS_Proess))
                {
                    this.drpIS_Proess.SelectedValue = jointInfo.IS_Proess;
                }
                this.txtRemark.Text = jointInfo.JOT_Remark;

                ////////////////////////////////////////////////////
                if (!string.IsNullOrEmpty(jointInfo.DReportID))
                {
                    this.ddlReportCode.SelectedValue = jointInfo.DReportID;
                    this.txtReportDate.Value = Convert.ToString(BLL.PW_JointInfoService.GetReportDateByDReportID(jointInfo.DReportID));
                    this.txtReport.Text = "已焊接";
                }
                else
                {
                    this.txtReport.Text = "未焊接";
                    this.ddlReportCode.SelectedValue = "0";
                }
                //日报日期
                if (!String.IsNullOrEmpty(jointInfo.JOT_FloorWelder))
                {
                    var floorWelder = from x in BLL.Funs.DB.BS_Welder where x.WED_ID == jointInfo.JOT_FloorWelder select x;
                    this.txtFloorWelder.Text = floorWelder.FirstOrDefault().WED_Code;
                    this.txtFloorWelderName.Text = floorWelder.FirstOrDefault().WED_Name;
                }
                if (!String.IsNullOrEmpty(jointInfo.JOT_CellWelder))
                {
                    var cellWelder = from x in BLL.Funs.DB.BS_Welder where x.WED_ID == jointInfo.JOT_CellWelder select x;
                    this.txtCellWelder.Text = cellWelder.FirstOrDefault().WED_Code;
                    this.txtCellWelderName.Text = cellWelder.FirstOrDefault().WED_Name;
                }
                if (jointInfo.JOT_DoneDin!=0)
                {
                    this.txtDoneDia.Text = Convert.ToString(jointInfo.JOT_DoneDin);
                }

                if (!string.IsNullOrEmpty(jointInfo.PW_PointID))
                {
                    this.txtPoint.Text = "已点口";
                }
                else
                {
                    this.txtPoint.Text = "未点口";
                } 
                if (!string.IsNullOrEmpty(jointInfo.JOT_TrustFlag))
                {
                    this.ddlTrustFlag.SelectedValue = jointInfo.JOT_TrustFlag;
                }
                else
                {
                    this.ddlTrustFlag.SelectedValue = "00";
                }
                if (!string.IsNullOrEmpty(jointInfo.JOT_CheckFlag))
                {
                    this.ddlCheckFlag.SelectedValue = jointInfo.JOT_CheckFlag;
                }
                else
                {
                    this.ddlCheckFlag.SelectedValue = "00";
                }
                if (!string.IsNullOrEmpty(jointInfo.JOT_JointStatus))
                {
                    this.ddlJointStatus.SelectedValue = jointInfo.JOT_JointStatus;
                }             
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnSave) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                Model.PW_JointInfo jointInfo = new Model.PW_JointInfo();
                jointInfo.ProjectId = this.CurrUser.ProjectId;
                jointInfo.JOT_JointNo = this.txtJointNo.Text.Trim();

                if (this.ddlReportCode.SelectedValue != "0")
                {
                    jointInfo.DReportID = this.ddlReportCode.SelectedValue.Trim();
                }
                else
                {
                    jointInfo.DReportID = null;
                }

                if (!string.IsNullOrEmpty(this.txtISONO.Text.Trim()))
                {
                    jointInfo.ISO_ID = Request.Params["iso_id"];
                }
                else
                {
                    jointInfo.ISO_ID = null;
                }
                if (this.ddlSTE1.SelectedValue != "0")
                {
                    jointInfo.STE_ID = this.ddlSTE1.SelectedValue.ToString();
                }
                else
                {
                    jointInfo.STE_ID = null;
                }
                if (this.ddlSTE2.SelectedValue != "0")
                {
                    jointInfo.STE_ID2 = this.ddlSTE2.SelectedValue.ToString();
                }
                else
                {
                    jointInfo.STE_ID2 = null;
                }

                jointInfo.WLO_Code = this.ddlWLO_CODE.SelectedValue;
                //jointInfo.JOT_CellWelder = null;
                //jointInfo.JOT_FloorWelder = null;
                //jointInfo.JOT_DoneDin = 0;
                //jointInfo.JOT_FloorGroup = null;
                //jointInfo.JOT_CellGroup = null;
                //jointInfo.IS_Compute = null;
                //jointInfo.JOT_NDTResult = null;
                if (this.ddlComponent1.SelectedValue != "0")
                {
                    jointInfo.JOT_Component1 = this.ddlComponent1.SelectedValue;
                }
                else
                {
                    jointInfo.JOT_Component1 = null;
                }
                if (this.ddlComponent2.SelectedValue != "0")
                {
                    jointInfo.JOT_Component2 = this.ddlComponent2.SelectedValue;
                }
                else
                {
                    jointInfo.JOT_Component2 = null;
                }
                jointInfo.JOT_HeartNo1 = this.txtHeartNo1.Text.Trim();
                jointInfo.JOT_HeartNo2 = this.txtHeartNo2.Text.Trim();
                if (this.ddlWeldMat.SelectedValue != "0")
                {
                    jointInfo.JOT_WeldMat = this.ddlWeldMat.SelectedValue;
                }
                else
                {
                    jointInfo.JOT_WeldMat = null;
                }

                if (!string.IsNullOrEmpty(this.txtDia.Text.Trim()))
                {
                    jointInfo.JOT_Dia = Convert.ToDecimal(this.txtDia.Text.Trim());
                }
                else
                {
                    jointInfo.JOT_Dia = 0;
                }
                if (!string.IsNullOrEmpty(this.txtSize.Text.Trim()))
                {
                    jointInfo.JOT_Size = Convert.ToDecimal(this.txtSize.Text.Trim());
                }
                else
                {
                    jointInfo.JOT_Size = 0;
                }
                jointInfo.JOT_Sch = this.txtSch.Text.Trim();
                if (!string.IsNullOrEmpty(this.txtFactSch.Text.Trim()))
                {
                    jointInfo.JOT_FactSch = Convert.ToDecimal(this.txtFactSch.Text.Trim());
                }
                else
                {
                    jointInfo.JOT_FactSch = 0;
                }
               
                jointInfo.JOT_JointDesc = this.txtJointDesc.Text.Trim();
                if (this.ddlWeldSilk.SelectedValue != "0")
                {
                    jointInfo.JOT_WeldSilk = this.ddlWeldSilk.SelectedValue.ToString();
                }
                else
                {
                    jointInfo.JOT_WeldSilk = null;
                }
                if (this.ddlJOTY_ID.SelectedValue != "0")
                {
                    jointInfo.JOTY_ID = this.ddlJOTY_ID.SelectedValue.ToString();
                }
                else
                {
                    jointInfo.JOTY_ID = null;
                }

                jointInfo.JOT_RepairFlag = null;
                if (ddlWME_ID.SelectedValue != "0")
                {
                    jointInfo.WME_ID = this.ddlWME_ID.SelectedValue;
                }
                else
                {
                    jointInfo.WME_ID = null;
                }
                if (ddlJST_ID.SelectedValue != "0")
                {
                    jointInfo.JST_ID = this.ddlJST_ID.SelectedValue;
                }
                else
                {
                    jointInfo.JST_ID = null;
                }
               
                if (!string.IsNullOrEmpty(this.txtPrepareTemp.Text.Trim()))
                {
                    jointInfo.JOT_PrepareTemp = Convert.ToDecimal(this.txtPrepareTemp.Text.Trim());
                }
              
                if (!string.IsNullOrEmpty(this.txtCellTemp.Text.Trim()))
                {
                    jointInfo.JOT_CellTemp = Convert.ToDecimal(this.txtCellTemp.Text.Trim());
                }
               
                if (!string.IsNullOrEmpty(this.txtLastTemp.Text.Trim()))
                {
                    jointInfo.JOT_LastTemp = Convert.ToDecimal(this.txtLastTemp.Text.Trim());
                }
               
                jointInfo.JOT_JointAttribute = this.ddlJointAttribute.SelectedValue;
                jointInfo.IS_Proess = this.drpIS_Proess.SelectedValue;
                jointInfo.JOT_Remark = this.txtRemark.Text.Trim();

                string jot_id = Request.Params["JOT_ID"];
                if (!string.IsNullOrEmpty(jot_id))
                {
                    jointInfo.JOT_ID = jot_id;
                    
                    BLL.PW_JointInfoService.UpdateJointInfo(jointInfo);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改焊口信息！");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功')", true);
                }
                else
                {
                    BLL.PW_JointInfoService.AddJointInfo(jointInfo);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "添加焊口信息！");
                }
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "WindowClose('OK')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }
        #endregion

    }
}