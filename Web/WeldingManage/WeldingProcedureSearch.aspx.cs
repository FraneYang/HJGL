using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.WeldingManage
{
    public partial class WeldingProcedureSearch : PPage
    {
        /// <summary>
        /// 主键
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
        /// 操作状态:增加、修改、删除
        /// </summary>
        public string OperateState
        {
            get
            {
                return (string)ViewState["State"];
            }
            set
            {
                ViewState["State"] = value;
            }
        }

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
        /// 页面加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.WeldProcedureMenuId);

                Funs.PleaseSelect(this.drpSte_Name);
                Funs.PleaseSelect(this.drpSte_NameS);
                this.drpSte_Name.Items.AddRange(BLL.MaterialService.GetSteelList());
                this.drpSte_NameS.Items.AddRange(BLL.MaterialService.GetSteelList());
                this.divSearch.Visible = false;
                this.btnSave.Enabled = false;
                this.btncancel.Enabled = false;
                this.TextIsReadOnly(true);
            }
        }

        /// <summary>
        /// 文本框是否可编辑
        /// </summary>
        /// <param name="readOnly"></param>
        public void TextIsReadOnly(bool readOnly)
        {
            this.txtWeldingProcedureId.Enabled = !readOnly;
            this.txtWtype.Enabled = !readOnly;
            this.drpSte_Name.Enabled = !readOnly;
            this.txtSpecification.Enabled = !readOnly;
            this.txtWelding.Enabled = !readOnly;
            this.txtWRange.Enabled = !readOnly;
            this.txtRemark.Enabled = !readOnly;
            this.txtJointsForm.Enabled = !readOnly;
            this.txtTubeDiameter.Enabled = !readOnly;
            this.txtSpecimenThickness.Enabled = !readOnly;
            this.txtWeldMethod.Enabled = !readOnly;
            this.txtWeldPositionCode.Enabled = !readOnly;
            this.txtWeldPreheating.Enabled = !readOnly;
            this.txtPWHT.Enabled = !readOnly;
        }

        /// <summary>
        /// 按钮是否可用
        /// </summary>
        /// <param name="enabled"></param>
        public void IsButtonEnable(bool enabled)
        {
            this.btnAdd.Enabled = enabled;
            this.btnModify.Enabled = enabled;
            this.btnSave.Enabled = !enabled;
            this.btncancel.Enabled = !enabled;
        }

        /// <summary>
        /// 清空文本框
        /// </summary>
        private void EnterEmpty()
        {
            this.WeldingProcedureId = string.Empty;
            this.txtWeldingProcedureId.Text = string.Empty;
            this.txtWtype.Text = string.Empty;
            this.drpSte_Name.SelectedValue = "0";
            this.txtSpecification.Text = string.Empty;
            this.txtWelding.Text = string.Empty;
            this.txtWRange.Text = string.Empty;
            this.txtRemark.Text = string.Empty;
            this.txtMaterialGroup.Text = string.Empty;
            this.txtMaterialType.Text = string.Empty;
            this.txtJointsForm.Text = string.Empty;
            this.txtTubeDiameter.Text = string.Empty;
            this.txtSpecimenThickness.Text = string.Empty;
            this.txtWeldMethod.Text = string.Empty;
            this.txtWeldPositionCode.Text = string.Empty;
            this.txtWeldPreheating.Text = string.Empty;
            this.txtPWHT.Text = string.Empty;
            this.txtProcedureId.Text = string.Empty;
        }

        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnAdd) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                WeldingProcedureId = string.Empty;
                this.EnterEmpty();
                this.OperateState = BLL.Const.Add;
                this.TextIsReadOnly(false);
                this.IsButtonEnable(false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
                return;
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnModify_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ButtonList.Contains(BLL.Const.BtnModify) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                this.TextIsReadOnly(false);
                this.IsButtonEnable(false);
                this.OperateState = BLL.Const.Modify;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
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
            if (this.ButtonList.Contains(BLL.Const.BtnSave) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                Model.PW_WeldingProcedure weldingProcedure = new Model.PW_WeldingProcedure();
                weldingProcedure.WeldingProcedureCode = this.txtWeldingProcedureId.Text.Trim();
                weldingProcedure.WType = this.txtWtype.Text.Trim();
                if (this.drpSte_Name.SelectedValue != "0")
                {
                    weldingProcedure.STE_ID = this.drpSte_Name.SelectedValue;
                }
                weldingProcedure.Specification = this.txtSpecification.Text.Trim();
                weldingProcedure.Welding = this.txtWelding.Text.Trim();
                weldingProcedure.WRange = this.txtWRange.Text.Trim();
                weldingProcedure.Remark = this.txtRemark.Text.Trim();
                weldingProcedure.MaterialGroup = this.txtMaterialGroup.Text.Trim();
                weldingProcedure.JointsForm = this.txtJointsForm.Text.Trim();
                weldingProcedure.TubeDiameter = this.txtTubeDiameter.Text.Trim();
                if (!string.IsNullOrEmpty(this.txtSpecimenThickness.Text))
                {
                    weldingProcedure.SpecimenThickness = Convert.ToDecimal(this.txtSpecimenThickness.Text.Trim());
                }
                weldingProcedure.WeldMethod = this.txtWeldMethod.Text.Trim();
                weldingProcedure.WeldPositionCode = this.txtWeldPositionCode.Text.Trim();
                weldingProcedure.WeldPreheating = this.txtWeldPreheating.Text.Trim();
                weldingProcedure.PWHT = this.txtPWHT.Text.Trim();

                if (OperateState == BLL.Const.Add)
                {
                    if (BLL.WeldingProcedureService.GetWeldingProcedureByWeldingProcedureId(this.txtWeldingProcedureId.Text.Trim()) != null)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此焊接工艺评定编号已经存在！')", true);
                        return;
                    }
                    weldingProcedure.IsAdd = true;
                    BLL.WeldingProcedureService.AddWeldProcedure(weldingProcedure);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "添加焊接工艺评定");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！')", true);
                }
                if (OperateState == BLL.Const.Modify)
                {
                    weldingProcedure.WeldingProcedureId = this.WeldingProcedureId;
                    BLL.WeldingProcedureService.UpdateWeldProcedure(weldingProcedure);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改焊接工艺评定");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！')", true);
                }
                this.gvPerson.DataBind();
                this.TextIsReadOnly(true);
                this.IsButtonEnable(true);
                this.EnterEmpty();               

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
                return;
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btncancel_Click(object sender, ImageClickEventArgs e)
        {
            this.IsButtonEnable(true);
            this.TextIsReadOnly(true);
        }

        /// <summary>
        /// 每次执行Select()和SelectCount前都会引发一次该事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["weldingProcedureId"] = this.txtWeldingProcedureIdS.Text.Trim();
            e.InputParameters["wType"] = this.txtSType.Text.Trim();
            e.InputParameters["ste_Id"] = this.drpSte_NameS.SelectedValue;
            e.InputParameters["specification"] = this.txtSpecificationS.Text.Trim();
            e.InputParameters["welding"] = this.txtWeldingS.Text.Trim();
            e.InputParameters["wRange"] = this.txtWRangeS.Text.Trim();            
            e.InputParameters["jointsForm"] = this.txtJointsFormS.Text.Trim();
            e.InputParameters["tubeDiameter"] = this.txtTubeDiameterS.Text.Trim();
            e.InputParameters["specimenThickness"] = this.txtSpecimenThicknessS.Text.Trim();
            e.InputParameters["weldMethod"] = this.txtWeldMethod.Text.Trim();
            e.InputParameters["weldPositionCode"] = this.txtWeldPositionCodeS.Text.Trim();
            e.InputParameters["weldPreheating"] = this.txtWeldPreheatingS.Text.Trim();
            e.InputParameters["pWHT"] = this.txtPWHTS.Text.Trim();
        }

        /// <summary>
        /// 在控件被绑定后激发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPerson_DataBound(object sender, EventArgs e)
        {
            if (this.gvPerson.BottomPagerRow == null)
            {
                return;
            }

            ((Web.Controls.GridNavgator)this.gvPerson.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvPerson;
        }

        /// <summary>
        /// GridView点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPerson_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            this.WeldingProcedureId = e.CommandArgument.ToString();
            if (e.CommandName == "click")
            {
                Model.PW_WeldingProcedure weldingProcedure = BLL.WeldingProcedureService.GetWeldingProcedureByWeldingProcedureId(WeldingProcedureId);
                this.txtWeldingProcedureId.Text = weldingProcedure.WeldingProcedureId;
                this.txtWeldingProcedureId.Text = weldingProcedure.WeldingProcedureCode ;
                this.txtWtype.Text = weldingProcedure.WType;
               
                if (!string.IsNullOrEmpty(weldingProcedure.STE_ID))
                {
                    this.drpSte_Name.SelectedValue = weldingProcedure.STE_ID;
                    this.txtMaterialType.Text = BLL.MaterialService.GetSteelBySteID(weldingProcedure.STE_ID).MaterialType;
                    this.txtMaterialGroup.Text = BLL.MaterialService.GetSteelBySteID(weldingProcedure.STE_ID).MaterialGroup;
                }
                this.txtSpecification.Text = weldingProcedure.Specification;
                this.txtWelding.Text = weldingProcedure.Welding;
                this.txtWRange.Text = weldingProcedure.WRange;
                this.txtRemark.Text = weldingProcedure.Remark;                      
                this.txtJointsForm.Text = weldingProcedure.JointsForm;
                this.txtTubeDiameter.Text = weldingProcedure.TubeDiameter;
                this.txtSpecimenThickness.Text = Convert.ToString(weldingProcedure.SpecimenThickness);
                this.txtWeldMethod.Text = weldingProcedure.WeldMethod;
                this.txtWeldPositionCode.Text = weldingProcedure.WeldPositionCode;
                this.txtWeldPreheating.Text = weldingProcedure.WeldPreheating;
                this.txtPWHT.Text = weldingProcedure.PWHT;
                this.divSearch.Visible = false;
                this.divEdit.Visible = true;
            }
            if (e.CommandName == "Del")
            {
                if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    Model.PW_WeldingProcedure weldingProcedure = BLL.WeldingProcedureService.GetWeldingProcedureByWeldingProcedureId(WeldingProcedureId);
                    if (weldingProcedure.IsAdd == true)
                    {                        
                        BLL.WeldingProcedureService.DeleteWeldingProcedure(WeldingProcedureId);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除焊接工艺评定");
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('删除成功')", true);
                        this.gvPerson.DataBind();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('对不起，初始化数据，无法删除！')", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('对不起，您没有这个权限，请联系管理员！')", true);
                    return;
                }
            }
            if (e.CommandName == "produce")
            {                
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>ShowProduce('" + WeldingProcedureId + "');</script>");
            }

        }

        /// <summary>
        /// 点击查询按钮，搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnConfirm_Click(object sender, ImageClickEventArgs e)
        {
            this.gvPerson.PageIndex = 0;
            this.gvPerson.DataBind();
        }

        protected void imgbtnShowSearch_Click(object sender, ImageClickEventArgs e)
        {
            this.divEdit.Visible = false;
            this.divSearch.Visible = true;
        }

        protected void imgbtncancel_Click(object sender, ImageClickEventArgs e)
        {
            this.divEdit.Visible = true;
            this.divSearch.Visible = false;
            this.txtWeldingProcedureIdS.Text = string.Empty;
            this.txtSType.Text = string.Empty;
            this.drpSte_NameS.SelectedValue = "0";
            this.txtSpecificationS.Text = string.Empty;
            this.txtWeldingS.Text = string.Empty;
            this.txtWRangeS.Text = string.Empty;
            this.txtMaterialType.Text = string.Empty;
            this.txtMaterialGroup.Text = string.Empty;
            this.txtJointsForm.Text = string.Empty;
            this.txtTubeDiameter.Text = string.Empty;
            this.txtSpecimenThickness.Text = string.Empty;
            this.txtWeldMethod.Text = string.Empty;
            this.txtWeldPositionCode.Text = string.Empty;
            this.txtWeldPreheating.Text = string.Empty;
            this.txtPWHT.Text = string.Empty;
            this.gvPerson.DataBind();
        }

        protected void drpSte_Name_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.drpSte_Name.SelectedValue!="0")
            {
                this.txtMaterialType.Text = BLL.MaterialService.GetSteelBySteID(this.drpSte_Name.SelectedValue).MaterialType;
                this.txtMaterialGroup.Text = BLL.MaterialService.GetSteelBySteID(this.drpSte_Name.SelectedValue).MaterialGroup;
            }
        }
    }
}