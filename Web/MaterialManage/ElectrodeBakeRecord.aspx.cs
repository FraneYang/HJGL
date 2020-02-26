using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.MaterialManage
{
    public partial class ElectrodeBakeRecord : PPage
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string ElectrodeID
        {
            get
            {
                return (string)ViewState["ElectrodeID"];
            }
            set
            {
                ViewState["ElectrodeID"] = value;
            }
        }

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
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.ElectrodeBakeMenuId);
            }
        }

        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ButtonList.Contains(BLL.Const.BtnAdd) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                Response.Redirect("ElectrodeBakeRecordEdit.aspx");
            }
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
        /// GridView分页绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvElectrodeBake_DataBound(object sender, EventArgs e)
        {
            if (this.gvElectrodeBake.BottomPagerRow==null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvElectrodeBake.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvElectrodeBake;
        }

        /// <summary>
        /// GridView点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvElectrodeBake_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string electrodeId = e.CommandArgument.ToString();
            if (e.CommandName=="click") //点击编号
            {
                Response.Redirect("ElectrodeBakeRecordEdit.aspx?electrodeId=" + electrodeId + "");
            }
            if (e.CommandName=="del") //点击删除
            {
                if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    if (judgementDelete())
                    {
                        BLL.ElectrodeBakeService.DeleteElectrodeBakeItem(electrodeId);
                        BLL.ElectrodeBakeService.DeleteElectrodeBake(electrodeId);
                        this.gvElectrodeBake.DataBind();
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除焊丝烘烤记录！");
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
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
        /// <summary>
        /// 打印按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrint_Click(object sender, ImageClickEventArgs e)
        {
            this.divPrint.Visible = true;
            this.txtStartDate.Value = string.Empty;
            this.txtEndDate.Value = string.Empty;
        }

        /// <summary>
        /// 打印确定按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnConfirm_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnPrint) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                if (string.IsNullOrEmpty(this.txtStartDate.Value) || string.IsNullOrEmpty(this.txtEndDate.Value))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('时间不能为空！')", true);
                    return;
                }
                else if (Convert.ToDateTime(this.txtStartDate.Value) > Convert.ToDateTime(this.txtEndDate.Value))
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('开始时间不能大于结束时间！')", true);
                    return;
                }
                else
                {
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "window.open('ElectrodeBakeRecordPrint.aspx?startDate=" + this.txtStartDate.Value + "&endDate=" + this.txtEndDate.Value + "')", true);
                    string date1 = string.Empty;
                    string date2 = string.Empty;
                    date1 = this.txtStartDate.Value;
                    date2 = this.txtEndDate.Value;

                    //ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>JointInfoPrint('" + ElectrodeID+ "','" + date1 + "','" + date2 + "');</script>");
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有权限，请与管理员联系！')", true);
            }
        }

        /// <summary>
        /// 取消打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnCancal_Click(object sender, ImageClickEventArgs e)
        {
            this.txtStartDate.Value = string.Empty;
            this.txtEndDate.Value = string.Empty;
            this.divPrint.Visible = false;
        }
    }
}