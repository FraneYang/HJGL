﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.BaseInfo
{
    public partial class Groove : PPage
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string JST_ID
        {
            get
            {
                return (string)ViewState["JST_ID"];
            }
            set
            {
                ViewState["JST_ID"] = value;
            }
        }

        /// <summary>
        /// 操作状态:增加、修改、删除
        /// </summary>
        public string OperateState
        {
            get
            {
                return (string)ViewState["State"];
            }
            set
            {
                ViewState["State"] = value;
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

        #endregion

        #region 加载页面
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
                ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.GrooveMenuId);

                Funs.PleaseSelect(drpSearch);
                this.drpSearch.Items.AddRange(BLL.GrooveService.SearchItem());

                this.btnSave.Enabled = false;
                this.btncancel.Enabled = false;
                this.TextIsReadOnly(false);
            }
        }
        #endregion

        #region 文本框是否可编辑、按钮是否可用、清空文本框
        private void TextIsReadOnly(bool readOnly)
        {
            this.txtJSTCode.Enabled = !readOnly;
            this.txtJSTName.Enabled = !readOnly;
            this.txtRemark.Enabled = !readOnly;
        }

        /// <summary>
        /// 按钮是否可用
        /// </summary>
        /// <param name="enabled"></param>
        public void IsButtonEnable(bool enabled)
        {
            this.btnAdd.Enabled = enabled;
            this.btnModify.Enabled = enabled;
            this.btnSave.Enabled = !enabled;
            this.btncancel.Enabled = !enabled;
        }

        /// <summary>
        /// 清空文本框
        /// </summary>
        private void EnterEmpty()
        {
            this.txtJSTCode.Text = string.Empty;
            this.txtJSTName.Text = string.Empty;
            this.txtRemark.Text = string.Empty;
        }

        #endregion

        #region 绑定参数
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["searchItem"] = this.drpSearch.SelectedValue;
            e.InputParameters["searchValue"] = this.txtSearch.Text;
        }
        #endregion

        #region 添加
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnAdd) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                JST_ID = string.Empty;
                this.txtJSTCode.Text = string.Empty;
                this.txtJSTName.Text = string.Empty;
                this.txtRemark.Text = string.Empty;

                OperateState = BLL.Const.Add;
                this.IsButtonEnable(false);
                this.TextIsReadOnly(false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
                return;
            }
        }
        #endregion

        #region 修改
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnModify_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ButtonList.Contains(BLL.Const.BtnModify) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                this.TextIsReadOnly(false);
                this.IsButtonEnable(false);
                this.OperateState = BLL.Const.Modify;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
                return;
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ButtonList.Contains(BLL.Const.BtnSave) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                Model.BS_SlopeType slopeType = new Model.BS_SlopeType();
                slopeType.JST_Code = this.txtJSTCode.Text.Trim();
                slopeType.JST_Name = this.txtJSTName.Text.Trim();
                slopeType.JST_Remark = this.txtRemark.Text.Trim();

                if (OperateState == BLL.Const.Add)
                {
                    if (BLL.GrooveService.IsExitJSTCode(this.txtJSTCode.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此坡口代号已经存在！')", true);
                        return;
                    }
                    BLL.GrooveService.AddSlopeType(slopeType);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "添加坡口类型");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！')", true);
                }
                if (OperateState == BLL.Const.Modify)
                {
                    string jst_code = BLL.GrooveService.GetSlopeTypeByJSTID(JST_ID).JST_Code;
                    if (jst_code != this.txtJSTCode.Text.Trim())
                    {
                        if (BLL.GrooveService.IsExitJSTCode(this.txtJSTCode.Text.Trim()))
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此坡口代号已经存在！')", true);
                            return;
                        }
                    }
                    slopeType.JST_ID = JST_ID;
                    BLL.GrooveService.UpdateSlopeType(slopeType);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改坡口类型");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！')", true);
                }
                this.gvGroove.DataBind();
                this.TextIsReadOnly(true);
                this.IsButtonEnable(true);
                this.EnterEmpty();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
                return;
            }
        }
        #endregion

        #region 取消
        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btncancel_Click(object sender, ImageClickEventArgs e)
        {
            this.TextIsReadOnly(true);
            this.IsButtonEnable(true);
        }
        #endregion

        #region 查找
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            this.gvGroove.PageIndex = 0;
            this.gvGroove.DataBind();
        }
        #endregion

        #region GridView绑定
        protected void gvGroove_DataBound(object sender, EventArgs e)
        {
            if (this.gvGroove.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvGroove.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvGroove;
        }
        #endregion

        #region GridView点击事件
        /// <summary>
        /// GridView点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvGroove_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            JST_ID = e.CommandArgument.ToString();
            if (e.CommandName == "click")
            {
                Model.BS_SlopeType slopeType = BLL.GrooveService.GetSlopeTypeByJSTID(JST_ID);
                this.txtJSTCode.Text = slopeType.JST_Code;
                this.txtJSTName.Text = slopeType.JST_Name;
                this.txtRemark.Text = slopeType.JST_Remark;
            }
            if (e.CommandName == "Del")
            {
                if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    if (judgementDelete())
                    {
                        BLL.GrooveService.DeleteSlopeType(JST_ID);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除坡口类型");
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('删除成功')", true);
                        this.gvGroove.DataBind();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('对不起，您没有这个权限，请联系管理员！')", true);
                    return;
                }
            }
        }
        #endregion

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete()
        {
            string content = "";
            if (BLL.PW_JointInfoService.GetJointInfoByFloorWeld(JST_ID)>0)
            {
                content = "焊口中已经使用了该破口类型,不能删除！";
            }
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
        #endregion
    }
}