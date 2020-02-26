using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.SysManage
{
    public partial class SysSet : PPage
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {
                Show();
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.SysSetMenuId);
            }
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnSave) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                Model.HJGLDB db = BLL.Funs.DB;
                string projectId = this.CurrUser.ProjectId;
                Model.Sys_Set dayReport = db.Sys_Set.First(x => x.SetId == 1 && x.ProjectId == projectId);
                Model.Sys_Set point = db.Sys_Set.First(x => x.SetId == 2 && x.ProjectId == projectId);
                Model.Sys_Set trust = db.Sys_Set.First(x => x.SetId == 3 && x.ProjectId == projectId);
                Model.Sys_Set supervisor = db.Sys_Set.First(x => x.SetId == 4 && x.ProjectId == projectId);
                Model.Sys_Set filmNum = db.Sys_Set.First(x => x.SetId == 5 && x.ProjectId == projectId);
                Model.Sys_Set dayReportDataIn = db.Sys_Set.First(x => x.SetId == 6 && x.ProjectId == projectId);
                Model.Sys_Set trustToIso = db.Sys_Set.First(x => x.SetId == 7 && x.ProjectId == projectId);
                if (ckbDayReport.Checked)
                {
                    dayReport.IsAuto = true;
                }
                else
                {
                    dayReport.IsAuto = false;
                }

                if (ckbDayReportDataIn.Checked)
                {
                    dayReportDataIn.IsAuto = true;
                }
                else
                {
                    dayReportDataIn.IsAuto = false;
                }

                if (ckbPoint.Checked)
                {
                    point.IsAuto = true;
                }
                else
                {
                    point.IsAuto = false;
                }

                if (robStandard.SelectedValue == "1")
                {
                    trust.IsAuto = true;
                    trust.SetValue = null;
                }
                else if (robStandard.SelectedValue == "2")
                {
                    trust.IsAuto = false;
                    trust.SetValue = null;
                }
                else
                {
                    trust.IsAuto = null;
                    trust.SetValue = robStandard.SelectedValue;
                }

                if (ckbSupervisor.Checked)
                {
                    supervisor.IsAuto = true;
                }
                else
                {
                    supervisor.IsAuto = false;
                }

                if (this.ckbFilmNum.Checked)
                {
                    filmNum.IsAuto = true;
                }
                else
                {
                    filmNum.IsAuto = false;
                }

                if (this.ckbTrust.Checked)
                {
                    trustToIso.IsAuto = true;
                }
                else
                {
                    trustToIso.IsAuto = false;
                }

                db.SubmitChanges();
                Show();
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }

        private void Show()
        {
            var q = from x in BLL.Funs.DB.Sys_Set where x.ProjectId == this.CurrUser.ProjectId select x;
            foreach (var s in q)
            {
                if (s.SetId == 1)
                {
                    if (s.IsAuto == true)
                    {
                        this.ckbDayReport.Checked = true;
                    }
                    else
                    {
                        this.ckbDayReport.Checked = false;
                    }
                }

                if (s.SetId == 2)
                {
                    if (s.IsAuto == true)
                    {
                        this.ckbPoint.Checked = true;
                    }
                    else
                    {
                        this.ckbPoint.Checked = false;
                    }
                }

                if (s.SetId == 3)
                {
                    if (s.IsAuto == true)
                    {
                        this.robStandard.SelectedValue = "1";
                    }
                    if (s.IsAuto == false)
                    {
                        this.robStandard.SelectedValue = "2";
                    }
                    if (s.SetValue == "3")
                    {
                        this.robStandard.SelectedValue = "3";
                    }
                    if (s.SetValue == "4")
                    {
                        this.robStandard.SelectedValue = "4";
                    }
                }

                if (s.SetId == 4)
                {
                    if (s.IsAuto == true)
                    {
                        this.ckbSupervisor.Checked = true;
                    }
                    else
                    {
                        this.ckbSupervisor.Checked = false;
                    }
                }

                if (s.SetId == 5)
                {
                    if (s.IsAuto == true)
                    {
                        this.ckbFilmNum.Checked = true;
                    }
                    else
                    {
                        this.ckbFilmNum.Checked = false;
                    }
                }

                if (s.SetId == 6)
                {
                    if (s.IsAuto == true)
                    {
                        this.ckbDayReportDataIn.Checked = true;
                    }
                    else
                    {
                        this.ckbDayReportDataIn.Checked = false;
                    }
                }

                if (s.SetId == 7)
                {
                    if (s.IsAuto == true)
                    {
                        this.ckbTrust.Checked = true;
                    }
                    else
                    {
                        this.ckbTrust.Checked = false;
                    }
                }
            }
        }
    }
}