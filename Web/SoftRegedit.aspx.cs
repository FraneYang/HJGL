using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web
{
    public partial class SoftRegedit : System.Web.UI.Page
    {
        /// <summary>
        /// 页面登录成功
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (BLL.SoftRegeditService.IsRegedit())
                {
                    this.NoRegedit.Visible = false;
                    this.Regedited.Visible = true;
                    this.btnSave.Visible = false;
                    this.txtRegeditedCode.Text = BLL.SoftRegeditService.GetRegeditCode();
                }
                else
                {
                    this.NoRegedit.Visible = true;
                    this.Regedited.Visible = false;
                    this.btnSave.Visible = true;
                    this.txtSerialId.Text = BLL.SoftRegeditService.SerialId();
                }
            }
        }

        /// <summary>
        /// 软件注册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                BLL.SoftRegeditService.WriteRegedit(this.txtSerialId.Text.ToString(), this.txtRegisterCode.Text.ToString());
                if (BLL.SoftRegeditService.IsRegedit())
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('注册成功！');location.href='Login.aspx'", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('注册失败，请输入正确的注册号！')", true);
                }
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('注册失败！+" + ex.ToString() + "')", true);
            }
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btncancel_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Login.aspx");
        }
    }
}