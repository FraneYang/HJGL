using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.SysManage
{
    public partial class UpdatePassword : PPage
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
        /// 页面登录成功
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {
                if (this.CurrUser.ProjectId != null)
                {
                    this.drpName.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));
                }
                else
                {
                    this.drpName.Items.AddRange(BLL.UserService.GetGly());
                }
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.UpdatePasswordMenuId);

                if (this.CurrUser.Account != BLL.Const.AdminId)
                {
                    // 默认值设置
                    ListItem defaultName = this.drpName.Items.FindByText(this.CurrUser.UserName);
                    this.drpName.SelectedValue = defaultName.Value;
                    this.drpName.Enabled = false;
                    this.trEnabled.Visible = true;
                }
                else
                {
                    this.drpName.Enabled = true;
                    this.trEnabled.Visible = false;
                }
            }
        }

        /// <summary>
        /// 保存密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnSave) || this.CurrUser.Account == BLL.Const.AdminId || this.CurrUser.Account == BLL.Const.GLY)
            {
                if (this.txtNewPwd.Text == this.txtConfirm.Text)
                {
                    if (this.CurrUser.Account != BLL.Const.AdminId)
                    {
                        if (BLL.UserService.EncryptionPassword(this.txtOldPwd.Text.ToString()) == BLL.UserService.GetPasswordByUserId(this.CurrUser.UserId))
                        {
                            BLL.UserService.UpdatePassword(this.CurrUser.UserId, this.txtNewPwd.Text.ToString());

                            BLL.LogService.AddLog(this.CurrUser.UserId, "修改用户自身密码");
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('密码修改成功！')", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('密码不正确！')", true);
                        }
                    }
                    else
                    {
                        BLL.UserService.UpdatePassword(this.drpName.SelectedValue.ToString(), this.txtNewPwd.Text.ToString());

                        BLL.LogService.AddLog(this.CurrUser.UserId, "修改用户密码");
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('密码修改成功！')", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('密码未被确认，请确保密码与确认密码保持一致！')", true);
                }
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }

        /// <summary>
        /// 取消修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btncancel_Click(object sender, ImageClickEventArgs e)
        {
            this.txtNewPwd.Text = string.Empty;
            this.txtConfirm.Text = string.Empty;
        }
    }
}