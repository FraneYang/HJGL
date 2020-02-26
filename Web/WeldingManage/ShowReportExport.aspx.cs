using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.WeldingManage
{
    public partial class ShowReportExport : PPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ddlUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId));              
                if (!string.IsNullOrEmpty(this.CurrUser.UnitId))
                {
                    this.ddlUnit.SelectedValue = this.CurrUser.UnitId;
                }

                Funs.PleaseSelect(this.ddlWorkarea);
                this.ddlWorkarea.Items.AddRange(BLL.WorkAreaService.GetWorkAreaListByUnit(this.CurrUser.ProjectId, this.ddlUnit.SelectedValue));

                this.txtdate1.Value = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                this.txtdate2.Value = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
            }
        }

        /// <summary>
        /// 绑定数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["projectId"] = this.CurrUser.ProjectId;
            e.InputParameters["unitId"] = this.ddlUnit.SelectedValue;
            e.InputParameters["workareaId"] = this.ddlWorkarea.SelectedValue;
            e.InputParameters["iso_IsoNo"] = this.txtIsoNo.Text.Trim();
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
        }


        /// <summary>
        /// 在控件被绑定后激发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPW_JointInfo_DataBound(object sender, EventArgs e)
        {
            if (this.gvPW_JointInfo.BottomPagerRow == null)
            {
                return;
            }

            ((Web.Controls.GridNavgator)this.gvPW_JointInfo.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvPW_JointInfo;
        }

        /// <summary>
        /// 单位联动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ddlWorkarea.Items.Clear();
            Funs.PleaseSelect(this.ddlWorkarea);
            this.ddlWorkarea.Items.AddRange(BLL.WorkAreaService.GetWorkAreaListByUnit(this.CurrUser.ProjectId, this.ddlUnit.SelectedValue));
        }

        /// <summary>
        /// 统计查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        protected void btnFind_Click(object sender, ImageClickEventArgs e)
        {
            this.gvPW_JointInfo.PageIndex = 0;
            this.gvPW_JointInfo.DataBind();
        }

        /// <summary>
        /// 导出按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, ImageClickEventArgs e)
        {
            this.gvPW_JointInfo.PageSize = 10000;
            this.gvPW_JointInfo.PageIndex = 0;
            this.gvPW_JointInfo.DataBind();
            this.gvPW_JointInfo.BottomPagerRow.Visible = false;//导出到Excel表后，隐藏分页部分
            
            DateTime dt = DateTime.Now;
            string filename = dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "GB2312";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write("<meta http-equiv=Content-Type content=text/html;charset=UTF-8>");

            Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode("管道焊接工作记录" + filename, System.Text.Encoding.UTF8) + ".xls");
            Response.ContentType = "application/ms-excel";
            this.EnableViewState = false;
            System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
            this.gvPW_JointInfo.RenderControl(oHtmlTextWriter);
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
    }
}