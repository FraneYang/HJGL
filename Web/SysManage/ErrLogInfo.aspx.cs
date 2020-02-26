using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.SysManage
{
    public partial class ErrLogInfo : PPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.txtEnd.Value = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now);
                this.txtBegin.Value = string.Format("{0:yyyy-MM-dd}", System.DateTime.Now.AddDays(-7));
            }
        }

        /// <summary>
        /// 参数设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
       {
            e.InputParameters["startTime"] = this.txtBegin.Value.Trim();
            e.InputParameters["endTime"] = this.txtEnd.Value.Trim();
        }

        /// <summary>
        /// 点击查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            this.gvErrLog.PageIndex = 0;
            this.gvErrLog.DataBind();
        }
        
        /// <summary>
        /// grid数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvErrLog_DataBound(object sender, EventArgs e)
        {
            if (this.gvErrLog.BottomPagerRow == null)
            {
                return;
            }

            ((Web.Controls.GridNavgator)this.gvErrLog.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvErrLog;
        }
        

        protected void btnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("LogList.aspx");
        }

        protected void btnDel_Click(object sender, ImageClickEventArgs e)
        {
            var sysErr = from x in BLL.Funs.DB.Sys_ErrLogInfo select x;
            if (sysErr.Count() > 0)
            {
                if (!String.IsNullOrEmpty(this.txtBegin.Value))
                {
                    sysErr = sysErr.Where(y => y.ErrTime >= Convert.ToDateTime(this.txtBegin.Value));
                }
                if (!String.IsNullOrEmpty(this.txtEnd.Value))
                {
                    sysErr = sysErr.Where(y => y.ErrTime <= Convert.ToDateTime(this.txtEnd.Value));
                }
                if (sysErr.Count() > 0)
                {
                    BLL.Funs.DB.Sys_ErrLogInfo.DeleteAllOnSubmit(sysErr);
                    BLL.Funs.DB.SubmitChanges();
                    this.gvErrLog.DataBind();
                }
            }
        }

        protected void gvErrLog_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "click")
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>ShowErrLogItem('" + e.CommandArgument.ToString() + "','" + string.Empty + "','" + string.Empty + "');</script>");
            }

            if (e.CommandName == "Del")
            {
                var sysErr = BLL.Funs.DB.Sys_ErrLogInfo.FirstOrDefault(x => x.ErrLogId == e.CommandArgument.ToString());
                if (sysErr != null)
                {
                    BLL.Funs.DB.Sys_ErrLogInfo.DeleteOnSubmit(sysErr); 
                }
                
                BLL.Funs.DB.SubmitChanges();

                this.gvErrLog.DataBind();
            }
        }

        protected void btnExport_Click(object sender, ImageClickEventArgs e)
        {
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>ShowErrLogItem('" + string.Empty + "','" + this.txtBegin.Value + "','" + this.txtEnd.Value + "');</script>");
        }
    }
}