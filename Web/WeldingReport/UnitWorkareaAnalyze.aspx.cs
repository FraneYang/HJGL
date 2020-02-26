using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.WeldingReport
{
    public partial class UnitWorkareaAnalyze :PPage
    {
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

         ///<summary>
         ///加载页面
         ///</summary>
         ///<param name="sender"></param>
         ///<param name="e"></param>
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
        /// 绑定GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvUnitAreaAnalyze_DataBound(object sender, EventArgs e)
        {
            if (this.gvUnitAreaAnalyze.BottomPagerRow == null)
            {
                return;
            }

            ((Web.Controls.GridNavgator)this.gvUnitAreaAnalyze.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvUnitAreaAnalyze;
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
            e.InputParameters["installationId"] = Convert.ToInt32(this.ddlInstalcode.SelectedValue);
            e.InputParameters["ste_steeltype"] = this.ddlSteType.SelectedValue;
            if (!String.IsNullOrEmpty(this.txtdate1.Value))
            {
                e.InputParameters["startTime"] = string.Format("{0:yyyy-MM-dd}", this.txtdate1.Value);
            }
            else
            {
                e.InputParameters["startTime"] = null;
            }

            if (!String.IsNullOrEmpty(this.txtdate2.Value))
            {
                e.InputParameters["endTime"] = string.Format("{0:yyyy-MM-dd}", this.txtdate2.Value);
            }
            else
            {
                e.InputParameters["endTime"] = null;
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
        /// 查找
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
            this.gvUnitAreaAnalyze.PageIndex = 0;
            this.gvUnitAreaAnalyze.DataBind();
            this.CalcFooter();
        }

        /// <summary>
        ///  单位项下拉框事件
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
            else   //现场
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
        /// 导出单位工区进度分析信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, ImageClickEventArgs e)
        {
            this.gvUnitAreaAnalyze.PageSize = 10000;
            this.gvUnitAreaAnalyze.PageIndex = 0;
            this.gvUnitAreaAnalyze.DataBind();
            this.gvUnitAreaAnalyze.BottomPagerRow.Visible = false;//导出到Excel表后，隐藏分页部分
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

            Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("单位工区进度分析表" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/ms-excel";
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            this.gvUnitAreaAnalyze.RenderControl(oHtmlTextWriter);
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
            var UnitBawAnalyze = BLL.UnitWorkareaAnalyzeService.GetUnitBawAnalyze();
            if (UnitBawAnalyze != null)
            {
                this.gvUnitAreaAnalyze.Columns[4].FooterText = UnitBawAnalyze.Sum(x => x.total_jot).ToString();
                this.gvUnitAreaAnalyze.Columns[5].FooterText = UnitBawAnalyze.Sum(x => x.total_sjot).ToString();
                this.gvUnitAreaAnalyze.Columns[6].FooterText = UnitBawAnalyze.Sum(x => x.total_fjot).ToString();
                this.gvUnitAreaAnalyze.Columns[7].FooterText = UnitBawAnalyze.Sum(x => x.cut_total_jot).ToString();
                this.gvUnitAreaAnalyze.Columns[8].FooterText = UnitBawAnalyze.Sum(x => x.total_din).ToString();
                this.gvUnitAreaAnalyze.Columns[9].FooterText = UnitBawAnalyze.Sum(x => x.total_Sdin).ToString();
                this.gvUnitAreaAnalyze.Columns[10].FooterText = UnitBawAnalyze.Sum(x => x.total_Fdin).ToString();
                this.gvUnitAreaAnalyze.Columns[11].FooterText = UnitBawAnalyze.Sum(x => x.finished_total_jot_bq).ToString();
                this.gvUnitAreaAnalyze.Columns[12].FooterText = UnitBawAnalyze.Sum(x => x.finished_total_sjot_bq).ToString();
                this.gvUnitAreaAnalyze.Columns[13].FooterText = UnitBawAnalyze.Sum(x => x.finished_total_fjot_bq).ToString();

                this.gvUnitAreaAnalyze.Columns[17].FooterText = UnitBawAnalyze.Sum(x => x.finished_total_din_bq).ToString();
                this.gvUnitAreaAnalyze.Columns[18].FooterText = UnitBawAnalyze.Sum(x => x.finished_total_Sdin_bq).ToString();
                this.gvUnitAreaAnalyze.Columns[19].FooterText = UnitBawAnalyze.Sum(x => x.finished_total_Fdin_bq).ToString();

                this.gvUnitAreaAnalyze.Columns[23].FooterText = UnitBawAnalyze.Sum(x => x.finished_total_jot).ToString();
                this.gvUnitAreaAnalyze.Columns[24].FooterText = UnitBawAnalyze.Sum(x => x.finished_total_sjot).ToString();
                this.gvUnitAreaAnalyze.Columns[25].FooterText = UnitBawAnalyze.Sum(x => x.finished_total_fjot).ToString();

                this.gvUnitAreaAnalyze.Columns[29].FooterText = UnitBawAnalyze.Sum(x => x.finished_total_din).ToString();
                this.gvUnitAreaAnalyze.Columns[30].FooterText = UnitBawAnalyze.Sum(x => x.finished_total_Sdin).ToString();
                this.gvUnitAreaAnalyze.Columns[31].FooterText = UnitBawAnalyze.Sum(x => x.finished_total_Fdin).ToString();
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