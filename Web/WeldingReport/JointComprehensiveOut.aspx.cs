using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.Text;

namespace Web.WeldingReport
{
    public partial class JointComprehensiveOut : PPage
    {
        /// <summary>
     /// 传参
     /// </summary>
        public string values
        {
            get
            {
                return (string)ViewState["values"];
            }
            set
            {
                ViewState["values"] = value;
            }
        }
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.values = Request.Params["values"];
            }
        }
        /// <summary>
        /// 绑定GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvJointCompre_DataBound(object sender, EventArgs e)
        {
            if (this.gvJointCompre.BottomPagerRow==null)
            {
                return;    
            }
            ((Web.Controls.GridNavgator)this.gvJointCompre.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvJointCompre;
        }
        /// <summary>
        /// 绑定参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["values"] = this.values;            
        }

        #region 导出
        /// <summary>
        /// 导出焊口综合信息表信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, ImageClickEventArgs e)
        {
            if (this.gvJointCompre.Rows.Count > 0)
            {
                this.gvJointCompre.PageSize = this.gvJointCompre.Rows.Count;//BLL.JointComprehensiveService.count
                this.gvJointCompre.PageIndex = 0;
                this.gvJointCompre.DataBind();
                this.gvJointCompre.BottomPagerRow.Visible = false;//导出到Excel表后，隐藏分页部分

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "GB2312";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.Write("<meta http-equiv=Content-Type content=text/html;charset=UTF-8>");

                DateTime dt = DateTime.Now;
                string filename = "焊口信息表" + dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();

                Response.AppendHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8) + ".xls");
                Response.ContentType = "application/ms-excel";
                this.EnableViewState = false;
                System.IO.StringWriter oStringWriter = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter oHtmlTextWriter = new System.Web.UI.HtmlTextWriter(oStringWriter);
                this.gvJointCompre.RenderControl(oHtmlTextWriter);
                Response.Write(oStringWriter.ToString());
                Response.Flush();
                Response.End();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('列表没有数据！')", true);
            }
        }
       
        /// <summary>
        /// 重载VerifyRenderingInServerForm方法，否则运行的时候会出现如下错误提示：“类型“GridView”的控件“GridView1”必须放在具有 runat=server 的窗体标记内”
        /// </summary>
        /// <param name="control"></param>
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
        #endregion

        #region 导出按钮
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOut_Click(object sender, ImageClickEventArgs e)
        {
            Response.ClearContent();
            DateTime dt = DateTime.Now;
            string filename = "焊口信息表" + dt.Year.ToString() + dt.Month.ToString() + dt.Day.ToString() + dt.Hour.ToString() + dt.Minute.ToString() + dt.Second.ToString();
            Response.AddHeader("content-disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(filename, Encoding.UTF8) + ".xls");
            Response.ContentType = "application/excel";
            Response.ContentEncoding = System.Text.Encoding.UTF8;

            this.gvJointCompre.PageSize = this.gvJointCompre.Rows.Count;//BLL.JointComprehensiveService.count
            this.gvJointCompre.PageIndex = 0;
            this.gvJointCompre.DataBind();
            this.gvJointCompre.BottomPagerRow.Visible = false;//导出到Excel表后，隐藏分页部分

            Response.Write(GetGridTableHtml(this.gvJointCompre));
            Response.End();
        }

        /// <summary>
        /// 导出方法
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private string GetGridTableHtml(GridView grid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<meta http-equiv=\"content-type\" content=\"application/excel; charset=UTF-8\"/><html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
            sb.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            sb.Append("<tr>");
            foreach (var column in grid.Columns)
            {
                sb.AppendFormat("<td>{0}</td>", ((BoundField)column).HeaderText);
            }
            sb.Append("</tr>");
            foreach (var row in grid.Rows)
            {
                sb.Append("<tr>");
                foreach (var cell in ((TableRow)row).Cells)
                {
                    string html =  ((TableCell)cell).Text.ToString();
                    ////if (column.ColumnID == "tfNumber")
                    ////{
                    ////    html = (row.FindControl("lblNumber") as AspNet.Label).Text;
                    ////}               
                    sb.AppendFormat("<td x:str>{0}</td>", html);
                }

                sb.Append("</tr>");
            }

            sb.Append("</table>");
            return sb.ToString();
        }
        #endregion

        protected void btnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("JointComprehensive.aspx?values=" + this.values);
        }
    }
}