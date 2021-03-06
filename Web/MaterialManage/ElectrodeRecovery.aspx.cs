﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.MaterialManage
{
    public partial class ElectrodeRecovery : PPage
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string ElectrodeRecoveryId
        {
            get
            {
                return (string)ViewState["ElectrodeRecoveryId"];
            }
            set
            {
                ViewState["ElectrodeRecoveryId"] = value;
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
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.ElectrodeRecoveryMenuId);
            }
        }
        /// <summary>
        /// 分页绑定GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvElectrodeRecovery_DataBound(object sender, EventArgs e)
        {
            if (this.gvElectrodeRecovery.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvElectrodeRecovery.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvElectrodeRecovery;
        }
        /// <summary>
        /// GridView点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvElectrodeRecovery_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string electrodeRecoveryId = e.CommandArgument.ToString();
            if (e.CommandName == "click") //点击编号
            {
                Response.Redirect("ElectrodeRecoveryEdit.aspx?electrodeRecoveryId=" + electrodeRecoveryId + "");
            }
            if (e.CommandName == "del") //点击删除
            {
                if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    if (judgementDelete())
                    {
                        BLL.ElectrodeRecoveryService.DeleteElectrodeRecoveryItem(this.CurrUser.ProjectId, electrodeRecoveryId);
                        BLL.ElectrodeRecoveryService.DeleteElectrodeRecovery(electrodeRecoveryId);
                        this.gvElectrodeRecovery.DataBind();
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
        /// 添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ButtonList.Contains(BLL.Const.BtnAdd) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                Response.Redirect("ElectrodeRecoveryEdit.aspx");
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
        /// <summary>
        /// 确定打印
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
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "", "window.open('ElectrodeRecoveryPrint.aspx?startDate=" + this.txtStartDate.Value + "&endDate=" + this.txtEndDate.Value + "')", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有权限，请与管理员联系！')", true);
            }
        }
    }
}