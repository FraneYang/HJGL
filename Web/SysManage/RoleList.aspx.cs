using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.SysManage
{
    public partial class RoleList : PPage
    {
        /// <summary>
        /// 角色主键
        /// </summary>
        public string RoleId
        {
            get
            {
                return (string)ViewState["RoleId"];
            }
            set
            {
                ViewState["RoleId"] = value;
            }
        }

        /// <summary>
        /// 操作状态，是增加，修改，还是删除

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.btnSave.Enabled = false;
                this.btncancel.Enabled = false;
                this.TextIsReadOnly(true);

                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.RoleMenuId);
            }
        }


        /// <summary>
        /// 绑定参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["projectId"] = this.CurrUser.ProjectId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RoleGridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            RoleId = e.CommandArgument.ToString();

            if (e.CommandName == "RoleNameClick")
            {
                Model.Sys_Role role = BLL.RoleService.GetRole(e.CommandArgument.ToString());
                this.txtRoleName.Text = role.RoleName;
                this.txtSortIndex.Text = role.SortIndex != null ? role.SortIndex.Value.ToString() : "";
                this.txtDef.Text = role.Def;
            }

            if (e.CommandName == "RoleDelete")
            {
                if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    Model.Sys_Role role = BLL.RoleService.GetRole(e.CommandArgument.ToString());
                    if (role.RoleType == "1")
                    {
                        if (judgementDelete())
                        {
                            BLL.RoleService.DeleteRole(RoleId);
                            this.RoleGridView.DataBind();
                        }
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除角色");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('属于内置项，不能删除！')", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RoleGridView_DataBound(object sender, EventArgs e)
        {
            if (this.RoleGridView.BottomPagerRow == null)
            {
                return;
            }

            ((Web.Controls.GridNavgator)this.RoleGridView.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.RoleGridView;
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnAdd) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                RoleId = string.Empty;
                this.txtRoleName.Text = string.Empty;
                this.txtSortIndex.Text = string.Empty;
                this.txtDef.Text = string.Empty;

                this.OperateState = BLL.Const.Add;
                this.ButtonIsEnabled(false);
                this.TextIsReadOnly(false);
                this.RoleGridView.DataBind();
                this.txtRoleName.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnModify_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnModify) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                if (!string.IsNullOrEmpty(RoleId))
                {
                    Model.Sys_Role role = BLL.RoleService.GetRole(RoleId);
                    this.OperateState = BLL.Const.Modify;
                    this.ButtonIsEnabled(false);
                    if (role.RoleType == "1")
                    {
                        this.TextIsReadOnly(false);
                        this.txtRoleName.Focus();
                    }
                    else
                    {
                        this.txtDef.ReadOnly = false;
                        this.txtSortIndex.ReadOnly = false;
                    }
                    this.RoleGridView.DataBind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnSave) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                int? sortIndex = null;
                if (!String.IsNullOrEmpty(this.txtSortIndex.Text))
                {
                    sortIndex = Convert.ToInt32(this.txtSortIndex.Text.Trim());
                }

                if (OperateState == BLL.Const.Add)
                {
                    BLL.RoleService.AddRole(this.txtRoleName.Text, this.txtDef.Text, "1", sortIndex, this.CurrUser.ProjectId);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "添加角色");
                }

                if (OperateState == BLL.Const.Modify)
                {
                    BLL.RoleService.UpdateRole(RoleId, this.txtRoleName.Text, this.txtDef.Text, sortIndex);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改角色");
                }

                this.RoleGridView.DataBind();
                this.ButtonIsEnabled(true);
                this.TextIsReadOnly(true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }

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

        private void ButtonIsEnabled(bool enabled)
        {
            this.btnAdd.Enabled = enabled;
            this.btnModify.Enabled = enabled;
            this.btnSave.Enabled = !enabled;
            this.btncancel.Enabled = !enabled;
        }

        /// <summary>
        /// 文本框是否可编辑
        /// </summary>
        /// <param name="readOnly"></param>
        private void TextIsReadOnly(bool readOnly)
        {
            this.txtRoleName.ReadOnly = readOnly;
            this.txtDef.ReadOnly = readOnly;
            this.txtSortIndex.ReadOnly = readOnly;
        }

        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete()
        {
            string content = "";
            if (BLL.UserService.GetUserCountByRole(RoleId) > 0)
            {
                content = "不能删除，已有用户分配了此角色！";
            }
            if (BLL.RolePowerService.GetPostPowerCountByRoleId(RoleId) > 0)
            {
                content = "不能删除，角色权限中已经给此角色设置了权限！";
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
    }
}