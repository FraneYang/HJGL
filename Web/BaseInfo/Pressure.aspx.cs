using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.BaseInfo
{
    public partial class Pressure : PPage
    {
        #region 定义项
        /// <summary>
        /// 主键
        /// </summary>
        public string TPT_ID
        {
            get
            {
                return (string)ViewState["TPT_ID"];
            }
            set
            {
                ViewState["TPT_ID"] = value;
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
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.PressureMenuId);

                Funs.PleaseSelect(drpSearch);
                this.drpSearch.Items.AddRange(BLL.PressureService.SearchItem());

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
            this.txtTPTCode.Enabled = !readOnly;
            this.txtTPTName.Enabled = !readOnly;
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
            this.txtTPTCode.Text = string.Empty;
            this.txtTPTName.Text = string.Empty;
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
                TPT_ID = string.Empty;
                this.txtTPTCode.Text = string.Empty;
                this.txtTPTName.Text = string.Empty;
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
        ///修改按钮
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
                Model.BS_PackageTestType packageTestType = new Model.BS_PackageTestType();

                packageTestType.TPT_Code = this.txtTPTCode.Text.Trim();
                packageTestType.TPT_TypeName = this.txtTPTName.Text.Trim();
                packageTestType.TPT_Remark = this.txtRemark.Text.Trim();

                if (OperateState == BLL.Const.Add)
                {
                    if (BLL.PressureService.IsExitTPTCode(this.txtTPTCode.Text.Trim()))
                    {
                         ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此试压代号已经存在！')", true);
                        return;
                    }
                    BLL.PressureService.AddPackageTestType(packageTestType);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "添加试压类型");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！')", true);
                }
                if (OperateState==BLL.Const.Modify)
                {
                    string tpt_code = BLL.PressureService.GetPackageTestTypeByTPTID(TPT_ID).TPT_Code;
                    if (tpt_code != this.txtTPTCode.Text.Trim())
                    {
                        if (BLL.PressureService.IsExitTPTCode(this.txtTPTCode.Text.Trim()))
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此试压代号已经存在！')", true);
                            return;
                        }
                    }
                    packageTestType.TPT_ID = TPT_ID;
                    BLL.PressureService.UpdatePackageTestType(packageTestType);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改试压类型");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！')", true);
                }
                this.gvPackageTestType.DataBind();
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
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            this.gvPackageTestType.PageIndex = 0;
            this.gvPackageTestType.DataBind();
        }
        #endregion

        #region 绑定GridView
        /// <summary>
        ///GridView绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPackageTestType_DataBound(object sender, EventArgs e)
        {
            if (this.gvPackageTestType.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvPackageTestType.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvPackageTestType;
        }
        #endregion

        #region GridView点击事件
        /// <summary>
        /// GridView点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPackageTestType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            TPT_ID = e.CommandArgument.ToString();
            if (e.CommandName=="click")
            {
                Model.BS_PackageTestType packageTestType = BLL.PressureService.GetPackageTestTypeByTPTID(TPT_ID);
                this.txtTPTCode.Text = packageTestType.TPT_Code;
                this.txtTPTName.Text = packageTestType.TPT_TypeName;
                this.txtRemark.Text = packageTestType.TPT_Remark;
            }
            if (e.CommandName=="Del")
            {
                if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    if (judgementDelete())
                    {
                        BLL.PressureService.DeletePackageTestType(TPT_ID);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除试压类型");
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('删除成功')", true);
                        this.gvPackageTestType.DataBind();
                    }
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