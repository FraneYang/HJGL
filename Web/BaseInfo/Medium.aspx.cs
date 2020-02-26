using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.BaseInfo
{
    public partial class Medium : PPage
    {
        #region 定义项

        /// <summary>
        /// 主键
        /// </summary>
        public string SER_ID
        {
            get
            {
                return (string)ViewState["SER_ID"];
            }
            set
            {
                ViewState["SER_ID"] = value;
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
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.MediumMenuId);

                Funs.PleaseSelect(drpSearch);
                this.drpSearch.Items.AddRange(BLL.MediumService.SearchItem());

                this.btnSave.Enabled = false;
                this.btncancel.Enabled = false;
                this.TextIsReadOnly(false);
            }
        }
        #endregion

        #region 文本框是否可编辑、按钮是否可用、清空文本框
        /// <summary>
        /// 文本框是否可编辑
        /// </summary>
        /// <param name="readOnly"></param>
        private void TextIsReadOnly(bool readOnly)
        {
            this.txtSerCode.Enabled = !readOnly;
            this.txtSerName.Enabled = !readOnly;
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
            this.txtSerCode.Text = string.Empty;
            this.txtSerName.Text = string.Empty;
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
                SER_ID = string.Empty;
                this.txtSerCode.Text = string.Empty;
                this.txtSerName.Text = string.Empty;
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
                this.gvService.DataBind();
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
                Model.BS_Service service = new Model.BS_Service();

                service.SER_Code = this.txtSerCode.Text.Trim();
                service.SER_Name = this.txtSerName.Text.Trim();
                service.SER_Remark = this.txtRemark.Text.Trim();

                if (OperateState == BLL.Const.Add)
                {
                    if (BLL.MediumService.IsExitSERCode(this.txtSerCode.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此介质代号已经存在！')", true);
                        return;
                    }
                    BLL.MediumService.AddService(service);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "添加介质");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！')", true);

                }
                if (OperateState == BLL.Const.Modify)
                {
                    string ser_code = BLL.MediumService.GetServiceBySERID(SER_ID).SER_Code;
                    if (ser_code != this.txtSerCode.Text.Trim())
                    {
                        if (BLL.MediumService.IsExitSERCode(this.txtSerCode.Text.Trim()))
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此介质代号已经存在！')", true);
                            return;
                        }
                    }
                    service.SER_ID = SER_ID;
                    BLL.MediumService.UpdateService(service);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改介质");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！')", true);
                }
                this.gvService.DataBind();
                this.TextIsReadOnly(true);
                this.IsButtonEnable(true);
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
            this.gvService.PageIndex = 0;
            this.gvService.DataBind();
        }
        #endregion

        #region 绑定GridView
        /// <summary>
        /// 绑定GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvService_DataBound(object sender, EventArgs e)
        {
            if (this.gvService.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvService.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvService;
        }
        #endregion

        #region GridView点击事件
        /// <summary>
        /// GridView点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvService_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SER_ID = e.CommandArgument.ToString();
            if (e.CommandName == "click")
            {
                Model.BS_Service service = BLL.MediumService.GetServiceBySERID(SER_ID);
                this.txtSerCode.Text = service.SER_Code;
                this.txtSerName.Text = service.SER_Name;
                this.txtRemark.Text = service.SER_Remark;
            }
            if (e.CommandName == "Del")
            {
                if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    if (judgementDelete())
                    {
                        BLL.MediumService.DeleteService(SER_ID);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除介质");
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('删除成功')", true);
                        this.gvService.DataBind();
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
            if (BLL.PW_IsoInfoService.GetIsoInfoBySerID(SER_ID)>0)
            {
                content = "管线中已经使用了该介质，不能删除！";
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
            e.InputParameters["searchValue"] = this.txtSearch.Text;
        }
        #endregion
    }
}