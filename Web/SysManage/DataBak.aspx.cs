using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.SysManage
{
    public partial class DataBak : PPage
    {
        /// <summary>
        /// 数据库名称

        /// </summary>
        public string DataName
        {
            get
            {
                return (string)ViewState["DataName"];
            }
            set
            {
                ViewState["DataName"] = value;
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
        /// 登录成功页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.DataBakMenuId);

                this.HyperLink1.Visible = false;
                string str = BLL.Funs.ConnString;
                string[] group = str.Split(';');

                foreach (string s in group)
                {
                    if (s.Contains("Database"))
                    {
                        string[] bak = s.Split('=');
                        DataName = bak[1];
                    }
                }
            }
        }

        /// <summary>
        /// 数据库备份
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BtnDataBak_Click(object sender, EventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnDataBak) || this.CurrUser.UserId == BLL.Const.AdminId)
            {
                string newname = DataName + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + ".bak";
                string nepath = Server.MapPath("../App_Data/") + newname;

                string sql = "BACKUP DATABASE " + DataName + " to DISK ='" + nepath + "'";

                try
                {
                    BLL.SQLHelper.ExecutSql(sql);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('数据库备份失败,原因：" + ex.Message.ToString() + "')", true);
                }

                string path = "../App_Data/" + newname;
                HyperLink1.Text = "文件" + newname + "已成功备份到服务器, 请点击下载到本地！";
                HyperLink1.NavigateUrl = "DownLoad.aspx?path=" + path;
                HyperLink1.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }

    }
}