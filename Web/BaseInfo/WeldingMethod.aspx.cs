using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.BaseInfo
{
    public partial class WeldingMethod : PPage
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string WME_ID
        {
            get
            {
                return (string)ViewState["WME_ID"];
            }
            set
            {
                ViewState["WME_ID"] = value;
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

        #region 页面加载
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
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.WeldingMethodMenuId);

                Funs.PleaseSelect(drpSearch);
                drpSearch.Items.AddRange(BLL.WeldingMethodService.SearchItem());

                this.btnSave.Enabled = false;
                this.btncancel.Enabled = false;
                this.TextIsReadOnly(false);
            }
        }
         #endregion

        #region 文本框是否可编辑、按钮是否可用、文本清空
        /// <summary>
        /// 文本框是否可编辑
        /// </summary>
        /// <param name="readOnly"></param>
        private void TextIsReadOnly(bool readOnly)
        {
            this.txtWMECode.Enabled = !readOnly;
            this.txtWMEName.Enabled = !readOnly;
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
            this.txtWMECode.Text = string.Empty;
            this.txtWMEName.Text = string.Empty;
            this.txtRemark.Text = string.Empty;
        }
        #endregion

        #region 添加
        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnAdd) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                WME_ID = string.Empty;
                this.txtWMECode.Text = string.Empty;
                this.txtWMEName.Text = string.Empty;
                this.txtRemark.Text = string.Empty;

                this.OperateState = BLL.Const.Add;
                this.TextIsReadOnly(false);
                this.IsButtonEnable(false);
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
        /// 修改按钮
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
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ButtonList.Contains(BLL.Const.BtnSave) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                Model.BS_WeldMethod weldMethod = new Model.BS_WeldMethod();

                weldMethod.WME_Code = this.txtWMECode.Text.Trim();
                weldMethod.WME_Name = this.txtWMEName.Text.Trim();
                weldMethod.WME_Remark = this.txtRemark.Text.Trim();

                if (OperateState == BLL.Const.Add)
                {
                    if (BLL.WeldingMethodService.IsExitWMECode(this.txtWMECode.Text.Trim()))
                    {
                         ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此焊接代码已经存在！')", true);
                        return;
                    }
                    BLL.WeldingMethodService.AddWeldMethod(weldMethod);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "添加焊接方法");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！')", true);
                }
                if (OperateState ==BLL.Const.Modify)
                {
                    string wme_code = BLL.WeldingMethodService.GetWeldMethodByWMEID(WME_ID).WME_Code;

                    if (wme_code!=this.txtWMECode.Text.Trim())
                    {
                        if (BLL.WeldingMethodService.IsExitWMECode(this.txtWMECode.Text.Trim()))
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此焊接代码已经存在！')", true);
                            return;
                        }
                    }
                    weldMethod.WME_ID = WME_ID;
                    BLL.WeldingMethodService.UpdateWeldMethod(weldMethod);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改焊接方法");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！')", true);
                }
                this.gvWeldMethod.DataBind();
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
            this.IsButtonEnable(true);
            this.TextIsReadOnly(true);
        }
        #endregion

        #region 搜索
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            this.gvWeldMethod.PageIndex = 0;
            this.gvWeldMethod.DataBind();
        }
        #endregion

        #region GridView绑定
        /// <summary>
        /// GridView绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvWeldMethod_DataBound(object sender, EventArgs e)
        {
            if (this.gvWeldMethod.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvWeldMethod.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvWeldMethod;
        }
        #endregion

        #region GridView点击事件
        /// <summary>
        /// GridView 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvWeldMethod_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            WME_ID = e.CommandArgument.ToString();
            if (e.CommandName=="click")
            {
                Model.BS_WeldMethod weldMethod = BLL.WeldingMethodService.GetWeldMethodByWMEID(WME_ID);
                this.txtWMECode.Text = weldMethod.WME_Code;
                this.txtWMEName.Text = weldMethod.WME_Name;
                this.txtRemark.Text = weldMethod.WME_Remark;
            }
            if (e.CommandName=="Del")
            {
                if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    if (judgementDelete())
                    {
                        BLL.WeldingMethodService.DeleteWeldMethod(WME_ID);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除焊接方法");
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('删除成功')", true);
                        this.gvWeldMethod.DataBind();
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
        private bool judgementDelete()
        {
            string content = "";
            if (BLL.PW_JointInfoService.GetJointInfoByFloorWeld(WME_ID)>0)
            {
                content = "焊口中已经使用了该焊接方法，不能删除！";
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

        #region 绑定参数
        /// <summary>
        /// 绑定参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["searchItem"] = this.drpSearch.SelectedValue;
            e.InputParameters["searchValue"] = this.txtSearch.Text;;
        }
        #endregion
    }
}