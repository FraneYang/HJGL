using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.BaseInfo
{
    public partial class WorkArea : PPage
    {
        #region 定义项
        /// <summary>
        /// 施工区域主键
        /// </summary>
        public string WorkAreaId
        {
            get
            {
                return (string)ViewState["WorkAreaId"];
            }
            set
            {
                ViewState["WorkAreaId"] = value;
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
        #endregion 

        #region 页面加载
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.WorkAreaMenuId);

                Funs.PleaseSelect(this.drpSearch);
                this.drpSearch.Items.AddRange(BLL.InstallationService.SearchList());

                Funs.PleaseSelect(this.drpUnit);
                this.drpUnit.Items.AddRange(BLL.UnitService.GetUnitNameList(this.CurrUser.ProjectId));

                Funs.PleaseSelect(this.drpSupervisorUnit);
                this.drpSupervisorUnit.Items.AddRange(BLL.UnitService.GetUnitNameByUnitTypeList(this.CurrUser.ProjectId,"4"));

                Funs.PleaseSelect(this.drpInstallation);
                this.drpInstallation.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId));

                this.btnSave.Enabled = false;
                this.btncancel.Enabled = false;
                this.TextIsReadOnly(true);
            }
        }
        #endregion 

        #region 绑定GridView
        /// <summary>
        /// 绑定GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvWorkArea_DataBound(object sender, EventArgs e)
        {
            if (this.gvWorkArea.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvWorkArea.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvWorkArea;
        }
        #endregion 

        #region GridView点击事件
        /// <summary>
        /// GridView基本操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvWorkArea_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            WorkAreaId = e.CommandArgument.ToString();
            if (e.CommandName == "workNameLink")
            {
                Model.Base_WorkArea workArea = BLL.WorkAreaService.getWorkAreaByWorkAreaId(WorkAreaId);
                this.drpUnit.SelectedValue = workArea.UnitId;
                this.txtWorkAreaCode.Text = workArea.WorkAreaCode;
                this.drpInstallation.SelectedValue = workArea.InstallationId.ToString();
                this.txtDef.Text = workArea.Def;
                if (!string.IsNullOrEmpty(workArea.SupervisorUnitId))
                {
                    this.drpSupervisorUnit.SelectedValue = workArea.SupervisorUnitId;
                }
            }

            if (e.CommandName == "DeleteWorkArea")
            {
                if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    if (judgementDelete())
                    {
                        BLL.WorkAreaService.DeleteWorkArea(WorkAreaId);
                        this.gvWorkArea.DataBind();
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除施工区域");
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
                }
            }
        }
        #endregion

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete()
        {
            string content = "";
            if (BLL.PW_IsoInfoService.GetIsoInfoByBawId(WorkAreaId)>0)
            {
                content = "管线中已经使用了该区域，不能删除!";
            }
            if (content == "")
            {
                return true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('" + content + "')", true);
                return false;
            }
        }
        #endregion

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            this.gvWorkArea.PageIndex = 0;
            this.gvWorkArea.DataBind();
        }
        #endregion

        #region 添加
        /// <summary>
        /// 添加装置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnAdd) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                this.EnterEmpty();
                this.OperateState = BLL.Const.Add;
                this.ButtonIsEnabled(false);
                this.TextIsReadOnly(false);
                this.drpUnit.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnModify_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnModify) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                this.OperateState = BLL.Const.Modify;
                this.ButtonIsEnabled(false);
                this.TextIsReadOnly(false);
                this.drpUnit.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
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
                Model.Base_WorkArea workArea = new Model.Base_WorkArea();

                workArea.ProjectId = this.CurrUser.ProjectId;
                if (this.drpUnit.SelectedValue != "0")
                {
                    workArea.UnitId = this.drpUnit.SelectedValue;
                }

                workArea.WorkAreaCode = this.txtWorkAreaCode.Text.Trim();
                if (this.drpInstallation.SelectedValue != "0")
                {
                    workArea.InstallationId = int.Parse(this.drpInstallation.SelectedValue);
                }
                if (this.drpSupervisorUnit.SelectedValue != "0")
                {
                    workArea.SupervisorUnitId = this.drpSupervisorUnit.SelectedValue;
                }
                workArea.Def = this.txtDef.Text;
                if (OperateState == BLL.Const.Add)
                {
                    if (BLL.WorkAreaService.IsExistWorkAreaCode(this.CurrUser.ProjectId, this.txtWorkAreaCode.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此施工区域编号已存在！')", true);
                        return;
                    }

                    BLL.WorkAreaService.AddWorkArea(workArea);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "增加施工区域");
                }
                if (OperateState == BLL.Const.Modify)
                {
                    string workAreaCode = BLL.WorkAreaService.getWorkAreaByWorkAreaId(this.WorkAreaId).WorkAreaCode;
                    if (workAreaCode != this.txtWorkAreaCode.Text.Trim())
                    {
                        if (BLL.WorkAreaService.IsExistWorkAreaCode(this.CurrUser.ProjectId, this.txtWorkAreaCode.Text.Trim()))
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此施工区域编号已存在！')", true);
                            return;
                        }
                    }

                    workArea.WorkAreaId = this.WorkAreaId;
                    BLL.WorkAreaService.updateWorkArea(workArea);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改施工区域");
                }


                this.gvWorkArea.DataBind();
                this.ButtonIsEnabled(true);
                this.TextIsReadOnly(true);
                this.EnterEmpty();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }
        #endregion

        #region 取消
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btncancel_Click(object sender, ImageClickEventArgs e)
        {
            this.ButtonIsEnabled(true);
            this.TextIsReadOnly(true);
        }
        #endregion

        #region 绑定参数
        /// <summary>
        /// 绑定参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["unitId"] = this.CurrUser.UnitId;
            e.InputParameters["searchItem"] = this.drpSearch.SelectedValue;
            e.InputParameters["searchValue"] = this.txtSearch.Text.ToString();
            e.InputParameters["projectId"] = this.CurrUser.ProjectId;
        }
        #endregion

        #region 文本框是否可编辑、按钮是否可用、清空文本框内容
        /// <summary>
        /// 文本框是否可编辑
        /// </summary>
        /// <param name="readOnly"></param>
        public void TextIsReadOnly(bool readOnly)
        {
            this.drpUnit.Enabled = !readOnly;
            this.drpSupervisorUnit.Enabled = !readOnly;
            this.txtWorkAreaCode.ReadOnly = readOnly;
            this.drpInstallation.Enabled = !readOnly;
            this.txtDef.ReadOnly = readOnly;
        }

        /// <summary>
        /// 按钮是否可选
        /// </summary>
        /// <param name="enabled"></param>
        public void ButtonIsEnabled(bool enabled)
        {
            this.btnAdd.Enabled = enabled;
            this.btnModify.Enabled = enabled;
            this.btnSave.Enabled = !enabled;
            this.btncancel.Enabled = !enabled;
        }

        /// <summary>
        ///  输入情空
        /// </summary>
        protected void EnterEmpty()
        {
            WorkAreaId = string.Empty;
            this.drpUnit.SelectedValue = "0";
            this.txtWorkAreaCode.Text = string.Empty;
            this.drpInstallation.SelectedValue = "0";
            this.drpSupervisorUnit.SelectedValue = "0";
            this.txtDef.Text = string.Empty;
        }
        #endregion

        #region GridView绑定下拉项
        /// <summary>
        ///  单位名称
        /// </summary>
        /// <param name="applyTypeId"></param>
        /// <returns></returns>
        protected string ConvertUnitName(object UnitId)
        {
            if (UnitId == null)
            {
                return null;
            }
            else
            {
                return BLL.UnitService.GetUnitNameList(this.CurrUser.ProjectId).FirstOrDefault(x => x.Value == UnitId.ToString()).Text;
            }
        }
        
        /// <summary>
        ///  装置
        /// </summary>
        /// <param name="applyTypeId"></param>
        /// <returns></returns>
        protected string ConvertInstallationName(object InstallationId)
        {
            if (InstallationId == null)
            {
                return null;
            }
            else
            {
                return BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId).FirstOrDefault(x => x.Value == InstallationId.ToString()).Text;
            }
        }
        #endregion
    }
}