using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.WeldingReport
{
    public partial class WelderPerformance : PPage
    {
        /// <summary>
        /// 按钮权限
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

        public string Flag
        {
            get
            {
                return (string)ViewState["Flag"];
            }
            set
            {
                ViewState["Flag"] = value;
            }
        }

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {
                if (this.CurrUser.ProjectId == null)
                {
                    this.trProject.Visible = true;
                    Funs.PleaseSelect(this.drpProject);
                    this.drpProject.Items.AddRange(BLL.ProjectService.GetProjectList());
                }
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.WelderPerformanceMenuId);


                var unit = BLL.UnitService.GetUnit(this.CurrUser.UnitId);
                if (unit != null && unit.UnitType == "2")
                {
                    this.ddlUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                    this.ddlUnit.SelectedValue = this.CurrUser.UnitId;
                    this.ddlWorkarea.Items.AddRange(BLL.WorkAreaService.GetWorkAreaListByUnit(this.CurrUser.ProjectId, this.CurrUser.UnitId));

                    Funs.PleaseSelect(ddlWloName);
                    this.ddlWloName.Items.AddRange(BLL.PersonManageService.GetWelderListByUnit(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                }
                else
                {
                    Funs.PleaseSelect(ddlUnit);
                    if (BLL.WorkAreaService.IsSupervisor(this.CurrUser.UnitId, this.CurrUser.ProjectId))
                    {
                        this.ddlUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameBySupervisorUnitIdList(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                    }
                    else
                    {
                        this.ddlUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId));
                    }
                }

                Funs.PleaseSelect(ddlWorkarea);
                Funs.PleaseSelect(drpTeamGroup);
                Funs.PleaseSelect(ddlSteel);
                this.ddlSteel.Items.AddRange(BLL.MaterialService.GetSteelList());

                Funs.PleaseSelect(ddlWloName);

                Flag = "0";
            }
        }

        /// <summary>
        /// 绑定GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvWelderPerformance_DataBound(object sender, EventArgs e)
        {
            if (this.gvWelderPerformance.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvWelderPerformance.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvWelderPerformance;
        }

        /// <summary>
        /// 绑定参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["unitcode"] = this.ddlUnit.SelectedValue;
            e.InputParameters["workareacode"] = this.ddlWorkarea.SelectedValue;
            e.InputParameters["steel"] = this.ddlSteel.SelectedValue;
            e.InputParameters["wloName"] = this.ddlWloName.SelectedValue;
            if (!String.IsNullOrEmpty(this.txtdate1.Value))
            {
                e.InputParameters["date1"] = string.Format("{0:yyyy-MM-dd}", this.txtdate1.Value);
            }
            else
            {
                e.InputParameters["date1"] = null;
            }

            if (!String.IsNullOrEmpty(this.txtdate2.Value))
            {
                e.InputParameters["date2"] = string.Format("{0:yyyy-MM-dd}", this.txtdate2.Value);
            }
            else
            {
                e.InputParameters["date2"] = null;
            }
            if (this.CurrUser.ProjectId == null)
            {
                e.InputParameters["projectId"] = this.drpProject.SelectedValue;
            }
            else
            {
                e.InputParameters["projectId"] = this.CurrUser.ProjectId;
            }
            e.InputParameters["flag"] = Flag;
            if (BLL.WorkAreaService.IsSupervisor(this.CurrUser.UnitId, this.CurrUser.ProjectId))
            {
                e.InputParameters["supervisorUnitId"] = this.CurrUser.UnitId;
            }
            else
            {
                e.InputParameters["supervisorUnitId"] = null;
            }
        }
        /// <summary>
        /// 查找按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFind_Click(object sender, ImageClickEventArgs e)
        {
            if (this.CurrUser.ProjectId == null)
            {
                if (this.drpProject.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择项目！')", true);
                    return;
                }
            }
            Flag = "1";
            this.gvWelderPerformance.PageIndex = 0;
            this.gvWelderPerformance.DataBind();
        }

        /// <summary>
        ///  单位下拉框联动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlWorkarea.Items.Clear();
            this.drpTeamGroup.Items.Clear();
            Funs.PleaseSelect(ddlWorkarea);
            if (this.CurrUser.ProjectId == null)  //总部
            {
                this.ddlWorkarea.Items.AddRange(BLL.WorkAreaService.GetWorkAreaListByUnit(this.drpProject.SelectedValue, this.ddlUnit.SelectedValue));
            }
            else  //现场
            {
                if (BLL.WorkAreaService.IsSupervisor(this.CurrUser.UnitId, this.CurrUser.ProjectId))
                {
                    this.ddlWorkarea.Items.AddRange(BLL.WorkAreaService.GetWorkAreaListBySupervisorUnit(this.CurrUser.ProjectId, this.ddlUnit.SelectedValue, this.CurrUser.UnitId));
                }
                else
                {
                    this.ddlWorkarea.Items.AddRange(BLL.WorkAreaService.GetWorkAreaListByUnit(this.CurrUser.ProjectId, this.ddlUnit.SelectedValue));
                }
            }

            this.ddlWloName.Items.Clear();
            Funs.PleaseSelect(ddlWloName);
            Funs.PleaseSelect(drpTeamGroup);
            if (this.CurrUser.ProjectId == null)  //总部
            {
                this.drpTeamGroup.Items.AddRange(BLL.TeamGroupService.GetEducationListByUnitId(this.drpProject.SelectedValue, this.ddlUnit.SelectedValue));
                this.ddlWloName.Items.AddRange(BLL.PersonManageService.GetWelderListByUnit(this.drpProject.SelectedValue, this.ddlUnit.SelectedValue));
            }
            else  //现场
            {
                this.drpTeamGroup.Items.AddRange(BLL.TeamGroupService.GetEducationListByUnitId(this.CurrUser.ProjectId, this.ddlUnit.SelectedValue));
                this.ddlWloName.Items.AddRange(BLL.PersonManageService.GetWelderListByUnit(this.CurrUser.ProjectId, this.ddlUnit.SelectedValue));
            }
        }

        /// <summary>
        /// 导出单位工区质量分析信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, ImageClickEventArgs e)
        {
            this.gvWelderPerformance.PageSize = 10000;
            this.gvWelderPerformance.PageIndex = 0;
            this.gvWelderPerformance.DataBind();
            this.gvWelderPerformance.BottomPagerRow.Visible = false;//导出到Excel表后，隐藏分页部分
            //int num = this.gvUnitAreaAnalyze.Columns.Count;
            //this.gvUnitAreaAnalyze.Columns[num - 1].Visible = false;
            //foreach (GridViewRow dg in this.gvJointInfo.Rows)
            //{
            //    dg.Cells[0].Attributes.Add("style", "vnd.ms-excel.numberformat: @;");
            //    dg.Cells[7].Attributes.Add("style", "vnd.ms-excel.numberformat: @;");
            //}
            DateTime dt = DateTime.Now;
            string filename = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write("<meta http-equiv=Content-Type content=text/html;charset=UTF-8>");

            Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("焊工业绩分析表" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/ms-excel";
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            this.gvWelderPerformance.RenderControl(oHtmlTextWriter);
            Response.Write(oStringWriter.ToString());
            Response.Flush();
            Response.End();
        }

        /// <summary>
        /// 重载VerifyRenderingInServerForm方法，否则运行的时候会出现如下错误提示：“类型“GridView”的控件“GridView1”必须放在具有 runat=server 的窗体标记内”
        /// </summary>
        /// <param name="control"></param>
        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void drpProject_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlUnit.Items.Clear();
            Funs.PleaseSelect(this.ddlUnit);
            if (this.drpProject.SelectedValue != "0")
            {
                this.ddlUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.drpProject.SelectedValue));
            }
        }

        protected void drpTeamGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlWloName.Items.Clear();
            Funs.PleaseSelect(ddlWloName);
            if (this.drpTeamGroup.SelectedValue != "0")
            {
                if (this.CurrUser.ProjectId == null)  //总部
                {
                    this.ddlWloName.Items.AddRange(BLL.PersonManageService.GetWelderListByEduId(this.drpProject.SelectedValue, this.drpTeamGroup.SelectedValue));
                }
                else   //现场
                {
                    this.ddlWloName.Items.AddRange(BLL.PersonManageService.GetWelderListByEduId(this.CurrUser.ProjectId, this.drpTeamGroup.SelectedValue));
                }
            }
            else
            {
                if (this.CurrUser.ProjectId == null)  //总部
                {
                    this.ddlWloName.Items.AddRange(BLL.PersonManageService.GetWelderListByUnit(this.drpProject.SelectedValue, this.ddlUnit.SelectedValue));
                }
                else  //现场
                {
                    this.ddlWloName.Items.AddRange(BLL.PersonManageService.GetWelderListByUnit(this.CurrUser.ProjectId, this.ddlUnit.SelectedValue));
                }
            }
        }
    }
}