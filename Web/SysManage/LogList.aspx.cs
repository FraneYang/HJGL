using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.SysManage
{
    public partial class LogList : PPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {
                BLL.Funs.PleaseSelect(this.drpUser);
                if (!string.IsNullOrEmpty(this.CurrUser.ProjectId))
                {
                    this.drpUser.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));
                }

                this.SelectId.Visible = false;
                if (this.CurrUser.Account == BLL.Const.AdminId || this.CurrUser.Account == BLL.Const.GLY)
                {
                    this.btnErr.Visible = true;
                }
            }
        }

        /// <summary>
        /// 参数设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["userId"] = this.drpUser.SelectedValue;
            e.InputParameters["startTime"] = this.txtBegin.Value.Trim();
            e.InputParameters["endTime"] = this.txtEnd.Value.Trim();
            e.InputParameters["projectId"] = this.CurrUser.ProjectId;
        }

        /// <summary>
        /// 点击查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            this.SelectId.Visible = true;
        }

        /// <summary>
        ///  点击取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            this.SelectId.Visible = false;
        }

        /// <summary>
        ///  点击确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSuer_Click(object sender, ImageClickEventArgs e)
        {
            this.LogGridView.PageIndex = 0;
            this.LogGridView.DataBind();
            this.SelectId.Visible = false;
        }

        /// <summary>
        /// grid数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LogGridView_DataBound(object sender, EventArgs e)
        {
            if (this.LogGridView.BottomPagerRow == null)
            {
                return;
            }

            ((Web.Controls.GridNavgator)this.LogGridView.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.LogGridView;
        }

        protected void btnErr_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ErrLogInfo.aspx");
        }
    }
}