using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.BaseInfo
{
    public partial class Installation : PPage
    {
        #region 定义项
        /// <summary>
        /// 装置主键
        /// </summary>
        public string InstallationId
        {
            get
            {
                return (string)ViewState["InstallationId"];
            }
            set
            {
                ViewState["InstallationId"] = value;
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
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.InstallationMenuId);

                Funs.PleaseSelect(this.drpSearch);
                this.drpSearch.Items.AddRange(BLL.InstallationService.SearchList());

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
            InstallationId = e.CommandArgument.ToString();
            if (e.CommandName == "InstallationName")
            {
                Model.Base_Installation installation = BLL.InstallationService.GetInstallationByInstallationId(e.CommandArgument.ToString());
                this.txtInstallationName.Text = installation.InstallationName;
                this.txtInstallationCode.Text = installation.InstallationCode;
                this.txtDef.Text = installation.Def;
            }
            if (e.CommandName == "DeleteInstallation")
            {
                if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    if (judgementDelete())
                    {
                        BLL.InstallationService.DeleteInstallation(InstallationId);
                        this.gvWorkArea.DataBind();
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除装置");
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
            if (BLL.WorkAreaService.GeWorkAreaCountByInstallationId(int.Parse(InstallationId)) > 0)
            {
                content = "施工区域已经使用了该设置，不能删除！";
            }
            if (BLL.WeldReportService.GetWeldReportByInstallationId(int.Parse(InstallationId)) > 0)
            {
                content = "焊接日报已经使用了该装置，不能删除！";
            }
            if (BLL.CheckManageService.GetCheckByInstallationId(int.Parse(InstallationId))>0)
            {
                content = "检测管理已经使用了该装置，不能删除！";
            }
            if (BLL.TrustManageEditService.GetTrustByInstallationId(int.Parse(InstallationId))>0)
            {
                content = "无损委托已经使用了该装置，不能删除！";
            }
            if (BLL.HotProessManageEditService.GetHotProessByInstallationId(int.Parse(InstallationId))>0)
            {
                content = "热处理已经使用了该装置，不能删除！";
            }
            if (BLL.TestPackageManageEditService.GetTestPackageByInstallationId(int.Parse(InstallationId))>0)
            {
                content = "试压包已经使用了该装置，不能删除！";
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
                InstallationId = string.Empty;
                this.txtInstallationName.Text = string.Empty;
                this.txtInstallationCode.Text = string.Empty;
                this.txtDef.Text = string.Empty;

                this.OperateState = BLL.Const.Add;
                this.ButtonIsEnabled(false);
                this.TextIsReadOnly(false);
                this.txtInstallationName.Focus();
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
                this.txtInstallationName.Focus();
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
                Model.Base_Installation install = new Model.Base_Installation();
                install.InstallationName = this.txtInstallationName.Text;
                install.ProjectId = this.CurrUser.ProjectId;
                install.InstallationCode = this.txtInstallationCode.Text.Trim();
                install.Def = this.txtDef.Text;
                install.IsUsed = true;

                if (OperateState == BLL.Const.Add)
                {
                    if (BLL.InstallationService.IsExistInstallationName(this.txtInstallationName.Text.Trim(), this.CurrUser.ProjectId))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此装置已存在！')", true);
                        return;
                    }
                    BLL.InstallationService.AddInstallation(install);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "增加装置");
                }
                if (OperateState == BLL.Const.Modify)
                {
                    install.InstallationId = Convert.ToInt32(InstallationId);
                    string installationName = BLL.InstallationService.GetInstallationByInstallationId(InstallationId).InstallationName;
                    if (installationName != this.txtInstallationName.Text.Trim())
                    {
                        if (BLL.InstallationService.IsExistInstallationName(this.txtInstallationName.Text.Trim(), this.CurrUser.ProjectId))
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此装置已存在！')", true);
                            return;
                        }
                    }

                    BLL.InstallationService.UpdateInstallation(install);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改装置");
                }
                this.gvWorkArea.DataBind();
                this.ButtonIsEnabled(true);
                this.TextIsReadOnly(true);
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
            e.InputParameters["searchItem"] = this.drpSearch.SelectedValue;
            e.InputParameters["searchValue"] = this.txtSearch.Text.ToString();
            e.InputParameters["projectId"] = this.CurrUser.ProjectId;
        }
        #endregion

        #region 文本框是否可编辑、按钮是否可用
        /// <summary>
        /// 文本框是否可编辑
        /// </summary>
        /// <param name="readOnly"></param>
        public void TextIsReadOnly(bool readOnly)
        {
            this.txtInstallationName.ReadOnly = readOnly;
            this.txtInstallationCode.ReadOnly = readOnly;
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
        #endregion
    }
}