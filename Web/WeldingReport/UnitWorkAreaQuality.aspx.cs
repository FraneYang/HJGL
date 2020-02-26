using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.WeldingReport
{
    public partial class UnitWorkAreaQuality : PPage
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
        /// 加载页面
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
                ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.UnitAreaQualityMenuId);
                               
                Funs.PleaseSelect(ddlInstalcode);
                Funs.PleaseSelect(ddlWorkarea);

                var unit = BLL.UnitService.GetUnit(this.CurrUser.UnitId);
                if (unit != null && unit.UnitType == "2")
                {
                    this.ddlUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                    this.ddlUnit.SelectedValue = this.CurrUser.UnitId;

                    this.ddlWorkarea.Items.AddRange(BLL.WorkAreaService.GetWorkAreaListByUnit(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                    this.ddlInstalcode.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId, this.CurrUser.UnitId));
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

                Flag = "0";
            }
        }
       /// <summary>
       /// 加载GridView
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        protected void gvUnitWorkQuality_DataBound(object sender, EventArgs e)
        {
            if (this.gvUnitWorkQuality.BottomPagerRow==null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvUnitWorkQuality.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvUnitWorkQuality;
        }

        /// <summary>
        /// 绑定参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["unitNo"] = this.ddlUnit.SelectedValue;
            e.InputParameters["areaNo"] = this.ddlWorkarea.SelectedValue;
            e.InputParameters["installationId"] =Convert.ToInt32(this.ddlInstalcode.SelectedValue);
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
            e.InputParameters["ste_steeltype"] = this.ddlSteType.SelectedValue;
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
            this.gvUnitWorkQuality.PageIndex = 0;
            this.gvUnitWorkQuality.DataBind();
            this.CalcFooter();
        }

        /// <summary>
        ///  单位下拉框联动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlWorkarea.Items.Clear();
            Funs.PleaseSelect(ddlWorkarea);

            this.ddlInstalcode.Items.Clear();
            Funs.PleaseSelect(ddlInstalcode);
            if (this.CurrUser.ProjectId == null)  //总部
            {
                this.ddlWorkarea.Items.AddRange(BLL.WorkAreaService.GetWorkAreaListByUnit(this.drpProject.SelectedValue, this.ddlUnit.SelectedValue));
                this.ddlInstalcode.Items.AddRange(BLL.InstallationService.GetInstallationList(this.drpProject.SelectedValue, this.ddlUnit.SelectedValue));
            }
            else  //现场
            {
                if (BLL.WorkAreaService.IsSupervisor(this.CurrUser.UnitId, this.CurrUser.ProjectId))
                {
                    this.ddlWorkarea.Items.AddRange(BLL.WorkAreaService.GetWorkAreaListBySupervisorUnit(this.CurrUser.ProjectId, this.ddlUnit.SelectedValue, this.CurrUser.UnitId));
                    this.ddlInstalcode.Items.AddRange(BLL.InstallationService.GetInstallationBySupervisorUnitIdList(this.CurrUser.ProjectId, this.ddlUnit.SelectedValue, this.CurrUser.UnitId));
                }
                else
                {
                    this.ddlWorkarea.Items.AddRange(BLL.WorkAreaService.GetWorkAreaListByUnit(this.CurrUser.ProjectId, this.ddlUnit.SelectedValue));
                    this.ddlInstalcode.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId, this.ddlUnit.SelectedValue));
                }
            }
        }

        /// <summary>
        /// 导出单位工区质量分析信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, ImageClickEventArgs e)
        {
            this.gvUnitWorkQuality.PageSize = 10000;
            this.gvUnitWorkQuality.PageIndex = 0;
            this.gvUnitWorkQuality.DataBind();
            this.gvUnitWorkQuality.BottomPagerRow.Visible = false;//导出到Excel表后，隐藏分页部分
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

            Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("单位工区质量分析表" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/ms-excel";
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            this.gvUnitWorkQuality.RenderControl(oHtmlTextWriter);
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

         /// <summary>
        /// 计算合计数量
        /// </summary>
        private void CalcFooter()
        {
            var rptBawZlfx = BLL.UnitWorkAreaQualityService.GetRptBawZlfx();
            if (rptBawZlfx != null)
            {
                this.gvUnitWorkQuality.Columns[4].FooterText = rptBawZlfx.Sum(x => x.total_jot).ToString();
                this.gvUnitWorkQuality.Columns[5].FooterText = rptBawZlfx.Sum(x => x.total_sjot).ToString();
                this.gvUnitWorkQuality.Columns[6].FooterText = rptBawZlfx.Sum(x => x.total_fjot).ToString();
                this.gvUnitWorkQuality.Columns[7].FooterText = rptBawZlfx.Sum(x => x.finished_total_jot).ToString();
                this.gvUnitWorkQuality.Columns[8].FooterText = rptBawZlfx.Sum(x => x.finished_total_sjot).ToString();
                this.gvUnitWorkQuality.Columns[9].FooterText = rptBawZlfx.Sum(x => x.finished_total_fjot).ToString();
                this.gvUnitWorkQuality.Columns[10].FooterText = rptBawZlfx.Sum(x => x.current_total_film).ToString();
                this.gvUnitWorkQuality.Columns[11].FooterText = rptBawZlfx.Sum(x => x.current_pass_film).ToString();

                this.gvUnitWorkQuality.Columns[13].FooterText = rptBawZlfx.Sum(x => x.current_point_total_film).ToString();
                this.gvUnitWorkQuality.Columns[14].FooterText = rptBawZlfx.Sum(x => x.current_point_pass_film).ToString();

                this.gvUnitWorkQuality.Columns[16].FooterText = rptBawZlfx.Sum(x => x.current_ext_total_film).ToString();
                this.gvUnitWorkQuality.Columns[17].FooterText = rptBawZlfx.Sum(x => x.current_ext_pass_film).ToString();
                this.gvUnitWorkQuality.Columns[19].FooterText = rptBawZlfx.Sum(x => x.current_trust_count_total).ToString();

                this.gvUnitWorkQuality.Columns[20].FooterText = rptBawZlfx.Sum(x => x.current_check_count_total).ToString();
                this.gvUnitWorkQuality.Columns[21].FooterText = rptBawZlfx.Sum(x => x.total_film).ToString();
                this.gvUnitWorkQuality.Columns[22].FooterText = rptBawZlfx.Sum(x => x.pass_film).ToString();

                this.gvUnitWorkQuality.Columns[24].FooterText = rptBawZlfx.Sum(x => x.point_total_film).ToString();
                this.gvUnitWorkQuality.Columns[25].FooterText = rptBawZlfx.Sum(x => x.point_pass_film).ToString();
                this.gvUnitWorkQuality.Columns[27].FooterText = rptBawZlfx.Sum(x => x.ext_total_film).ToString();
                this.gvUnitWorkQuality.Columns[28].FooterText = rptBawZlfx.Sum(x => x.ext_pass_film).ToString();

                this.gvUnitWorkQuality.Columns[30].FooterText = rptBawZlfx.Sum(x => x.trust_count_total).ToString();
                this.gvUnitWorkQuality.Columns[31].FooterText = rptBawZlfx.Sum(x => x.point_count_total).ToString();
                this.gvUnitWorkQuality.Columns[32].FooterText = rptBawZlfx.Sum(x => x.extend_count_total).ToString();
                this.gvUnitWorkQuality.Columns[33].FooterText = rptBawZlfx.Sum(x => x.repair_count_total).ToString();
                this.gvUnitWorkQuality.Columns[34].FooterText = rptBawZlfx.Sum(x => x.trust_check_total).ToString();
            }
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
    }
}