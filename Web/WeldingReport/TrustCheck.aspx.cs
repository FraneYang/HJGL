using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.WeldingReport
{
    public partial class TrustCheck : PPage
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
                ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.DetectionAnalyzeMenuId);

                Funs.PleaseSelect(ddlWorkarea);
                var unit = BLL.UnitService.GetUnit(this.CurrUser.UnitId);
                if (unit != null && unit.UnitType == "2")
                {
                    this.ddlUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                    this.ddlUnit.SelectedValue = this.CurrUser.UnitId;
                    this.ddlWorkarea.Items.AddRange(BLL.WorkAreaService.GetWorkAreaListByUnit(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                }
                else
                {
                    Funs.PleaseSelect(ddlUnit);
                    this.ddlUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId));
                }

                this.Flag = "0";
            }
        }


        /// <summary>
        /// GridView绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTrustCheck_DataBound(object sender, EventArgs e)
        {
            if (this.gvTrustCheck.BottomPagerRow == null)
            {
                return;
            }

            ((Web.Controls.GridNavgator)this.gvTrustCheck.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvTrustCheck;
        }

        /// <summary>
        /// 绑定参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["unitId"] = this.ddlUnit.SelectedValue;
            e.InputParameters["workAreaId"] = this.ddlWorkarea.SelectedValue;
            if (this.CurrUser.ProjectId == null)
            {
                e.InputParameters["projectId"] = this.drpProject.SelectedValue;
            }
            else
            {
                e.InputParameters["projectId"] = this.CurrUser.ProjectId;
            }
            e.InputParameters["flag"] = Flag;
          
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
            this.Flag = "1";
            this.gvTrustCheck.PageIndex = 0;
            this.gvTrustCheck.DataBind();
            CalcFooter();
        }

        /// <summary>
        /// 导出探伤综合分析表信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, ImageClickEventArgs e)
        {
            this.gvTrustCheck.PageSize = 1000;
            this.gvTrustCheck.PageIndex = 0;
            this.gvTrustCheck.DataBind();
            this.gvTrustCheck.BottomPagerRow.Visible = false;//导出到Excel表后，隐藏分页部分
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

            Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("委托检测数据一览表" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/ms-excel";
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            this.gvTrustCheck.RenderControl(oHtmlTextWriter);
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


        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlWorkarea.Items.Clear();
            Funs.PleaseSelect(ddlWorkarea);
            if (this.CurrUser.ProjectId == null)   //总部
            {
                this.ddlWorkarea.Items.AddRange(BLL.WorkAreaService.GetWorkAreaListByUnit(this.drpProject.SelectedValue, this.ddlUnit.SelectedValue));
            }
            else   //项目
            {
                this.ddlWorkarea.Items.AddRange(BLL.WorkAreaService.GetWorkAreaListByUnit(this.CurrUser.ProjectId, this.ddlUnit.SelectedValue));
            }

            this.gvTrustCheck.PageIndex = 0;
            this.gvTrustCheck.DataBind();
        }


        /// <summary>
        /// 计算合计数量
        /// </summary>
        private void CalcFooter()
        {
            string unitId = null;
            string workAreaId = null;
           
            if (this.ddlUnit.SelectedValue != "0")
            {
                unitId = this.ddlUnit.SelectedValue;
            }
            if (this.ddlWorkarea.SelectedValue != "0")
            {
                workAreaId = this.ddlWorkarea.SelectedValue;
            }

            var qq = Funs.DB.SpTrustCheckReport(unitId, workAreaId, this.CurrUser.ProjectId);
            var q = qq.ToList();
          
            this.gvTrustCheck.Columns[5].FooterStyle.HorizontalAlign = HorizontalAlign.Left;
            this.gvTrustCheck.Columns[5].FooterText = q.Sum(x => x.trust_Audit_total).ToString();
            this.gvTrustCheck.Columns[6].FooterStyle.HorizontalAlign = HorizontalAlign.Left;
            this.gvTrustCheck.Columns[6].FooterText = q.Sum(x => x.trust_NoAudit_total).ToString();
            this.gvTrustCheck.Columns[7].FooterStyle.HorizontalAlign = HorizontalAlign.Left;
            this.gvTrustCheck.Columns[7].FooterText = q.Sum(x => x.check_Audit_total).ToString();

            this.gvTrustCheck.Columns[8].FooterStyle.HorizontalAlign = HorizontalAlign.Left;
            this.gvTrustCheck.Columns[8].FooterText = q.Sum(x => x.check_NoAudit_total).ToString();
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