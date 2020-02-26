using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.MaterialManage
{
    public partial class EWeldRHRecord : PPage
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string EWeldRHRecordId
        {
            get
            {
                return (string)ViewState["EWeldRHRecordId"];
            }
            set
            {
                ViewState["EWeldRHRecordId"] = value;
            }
        }
        /// <summary>
        /// 按钮权限
        /// </summary>
        public string[] ButtonList
        {
            get {
                return (string[])ViewState["ButtonList"];
            }
            set
            {
                ViewState["ButtonList"] = value;
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
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.EWeldRHRecordMenuId);
            }
        }

        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddButton_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnAdd) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                Response.Redirect("EWeldRHRecordEdit.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "alert", "alert('您没有该权限，请与管理员联系！')", true);
            }
        }

        /// <summary>
        /// 打印按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrint_Click(object sender, ImageClickEventArgs e)
        {
            this.divPrint.Visible = true;
        }

        /// <summary>
        /// 确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnConfirm_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnPrint) || this.CurrUser.Account == BLL.Const.AdminId)
            {                
                if (Convert.ToInt32(this.drpMonth1.SelectedValue) >  Convert.ToInt32(this.drpMonth2.SelectedValue))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('开始月份不能大于结束月份！')", true);
                }
                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "", "window.open('EWeldRHRecordPrint.aspx?startDate=" + this.drpMonth1.SelectedValue + "&endDate=" + this.drpMonth2.SelectedValue + "')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有权限，请与管理员联系！')", true);
            }
        }
        /// <summary>
        ///取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnCancal_Click(object sender, ImageClickEventArgs e)
        {
            this.divPrint.Visible = false;
        }

        /// <summary>
        /// 绑定参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["projectId"] = this.CurrUser.ProjectId;
        }

        /// <summary>
        /// 绑定GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvEWeldRHRecord_DataBound(object sender, EventArgs e)
        {
            if (this.gvEWeldRHRecord.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvEWeldRHRecord.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvEWeldRHRecord;
        }
        /// <summary>
        /// GridView点击行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvEWeldRHRecord_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string eWeldRHRecordId = e.CommandArgument.ToString();
            if (e.CommandName == "click")
            {
                Response.Redirect("EWeldRHRecordEdit.aspx?eWeldRHRecordId=" + eWeldRHRecordId);
            }
            if (e.CommandName == "del")
            {
                if (this.ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    if (judgementDelete())
                    {
                        BLL.EWeldRHRecordService.DeleteEWeldRHRecordItem(eWeldRHRecordId);
                        BLL.EWeldRHRecordService.DeleteEWeldRHRecord(eWeldRHRecordId);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除焊材库温湿度记录");
                        this.gvEWeldRHRecord.DataBind();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有权限，请与管理员联系！')", true);
                }
            }
        }

        /// <summary>
        /// 判断是否可删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete()
        {
            string content = "";
            if (content == "")
            {
                return true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('" + content + "')", true);
                return false;
            }
        }
    }
}