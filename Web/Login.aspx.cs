using BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web
{
    public partial class Login : PPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                return;
            }
            else
            {                
                Funs.PleaseSelect(this.drpProject);
                this.drpProject.Items.AddRange(BLL.ProjectService.GetProjectList());
            }

            //this.LogOnHeaderPicture.ImageUrl = ResolveUrl("Images/222.jpg");

            if (Request.Cookies["UserInfo"] != null)
            {
                if (Request.Cookies["UserInfo"]["username"] != null)
                {
                    this.UserName.Value = Request.Cookies["UserInfo"]["username"].ToString();
                }
                if (Request.Cookies["UserInfo"]["password"] != null)
                {
                    this.Password.Text = Request.Cookies["UserInfo"]["password"].ToString();
                }
                if (Request.Cookies["UserInfo"]["projectId"] != null)
                {
                    this.drpProject.SelectedValue = Request.Cookies["UserInfo"]["projectId"].ToString();
                }
                this.savemessgae.Checked = true;
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            List<string> mainUserAccounts = (from x in Funs.DB.Sys_User where x.ProjectId == null select x.Account).ToList();
            //if(this.drpProject.SelectedValue==)
            string userName = this.UserName.Value;
            string paswword = this.Password.Text;
            string projectId = this.drpProject.SelectedValue;
            bool rememberMe = this.savemessgae.Checked;
            if (mainUserAccounts.Contains(userName) || (!mainUserAccounts.Contains(userName) && this.drpProject.SelectedValue != "0"))
            {
                if (BLL.UserService.UserLogOn(userName, paswword, projectId, rememberMe, this.Page))
                {
                    if (BLL.SoftRegeditService.IsRegedit())
                    {
                        BLL.LogService.AddLog(this.CurrUser.UserId, "登录成功！");
                        PPage.ZXRefresh(ResolveUrl("~/index.aspx"));
                    }
                    else
                    {
                        if (DateTime.Now.Date < Convert.ToDateTime("2020-12-30"))
                        {
                            BLL.LogService.AddLog(this.CurrUser.UserId, "登录成功！");
                            PPage.ZXRefresh(ResolveUrl("~/index.aspx"));
                        }
                        else
                        {
                            Response.Redirect("SoftRegedit.aspx");
                        }
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择项目！')", true);
            }
        }

        protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
        {
            this.UserName.Value = string.Empty;
            this.Password.Text = string.Empty;
            this.savemessgae.Checked = false;
        }

        protected void lbtnRegedit_Click(object sender, EventArgs e)
        {
            Response.Redirect("SoftRegedit.aspx");
        }

    }
}
