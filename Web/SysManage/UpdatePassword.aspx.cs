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
        /// ��ťȨ���б�
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
        /// ҳ���¼�ɹ�
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
                    // Ĭ��ֵ����
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
        /// ��������
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

                            BLL.LogService.AddLog(this.CurrUser.UserId, "�޸��û���������");
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('�����޸ĳɹ���')", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('���벻��ȷ��')", true);
                        }
                    }
                    else
                    {
                        BLL.UserService.UpdatePassword(this.drpName.SelectedValue.ToString(), this.txtNewPwd.Text.ToString());

                        BLL.LogService.AddLog(this.CurrUser.UserId, "�޸��û�����");
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('�����޸ĳɹ���')", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('����δ��ȷ�ϣ���ȷ��������ȷ�����뱣��һ�£�')", true);
                }
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('��û�����Ȩ�ޣ��������Ա��ϵ��')", true);
            }
        }

        /// <summary>
        /// ȡ���޸�
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