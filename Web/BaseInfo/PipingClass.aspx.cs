using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.BaseInfo
{
    public partial class PipingClass : PPage
    {

        #region 定义项
        /// <summary>
        /// 管道等级主键
        /// </summary>
        public string PipingClassID
        {
            get
            {
                return (string)ViewState["PipingClassID"];
            }
            set
            {
                ViewState["PipingClassID"] = value;
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
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.PipingMenuId);

                Funs.PleaseSelect(this.drpSearch);
                this.drpSearch.Items.AddRange(BLL.PipingClassService.SearchItem());

                this.btnSave.Enabled = false;
                this.btncancel.Enabled = false;
                this.TextIsReadOnly(false);
            }
        }
        #endregion

        #region 文本框、按钮是否可用、文本框置空
        /// <summary>
        /// 文本框是否可编辑
        /// </summary>
        /// <param name="readOnly"></param>
        public void TextIsReadOnly(bool readOnly)
        {   
            this.txtPipingClassCode.Enabled = !readOnly;
            this.txtPipingClassName.Enabled = !readOnly;
            this.txtPipingClassGrade.Enabled = !readOnly;
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
        /// 清空内容
        /// </summary>
        private void EnterEmpty()
        {
            this.txtPipingClassCode.Text = string.Empty;
            this.txtPipingClassName.Text = string.Empty;
            this.txtPipingClassGrade.Text = string.Empty;
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
                PipingClassID = string.Empty;
                this.txtPipingClassCode.Text = string.Empty;
                this.txtPipingClassName.Text = string.Empty;
                this.txtPipingClassGrade.Text = string.Empty;
                this.txtRemark.Text = string.Empty;

                this.OperateState = BLL.Const.Add;

                TextIsReadOnly(false);
                IsButtonEnable(false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
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
            if (this.ButtonList.Contains(BLL.Const.BtnModify)||this.CurrUser.Account==BLL.Const.AdminId)
            {
                this.TextIsReadOnly(false);
                this.IsButtonEnable(false);
                this.OperateState = BLL.Const.Modify;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
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
                Model.BS_IsoClass pipingClass = new Model.BS_IsoClass();

                pipingClass.ISC_IsoCode = this.txtPipingClassCode.Text.Trim();
                pipingClass.ISC_IsoName = this.txtPipingClassName.Text.Trim();
                pipingClass.ISC_IsoClass = this.txtPipingClassGrade.Text.Trim();
                pipingClass.ISC_Remark = this.txtRemark.Text.Trim();

                if (OperateState == BLL.Const.Add)
                {
                    if (BLL.PipingClassService.IsExistPipingClassCode(this.txtPipingClassCode.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此管道等级代号已经存在！')", true);
                        return;
                    }

                    BLL.PipingClassService.AddPipingClass(pipingClass);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "添加管道等级");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！')", true);
                }
                if (OperateState == BLL.Const.Modify)
                {
                    string pipingClassCode = BLL.PipingClassService.GetPipingClassByPipingClassId(PipingClassID).ISC_IsoCode;
                    if (pipingClassCode != this.txtPipingClassCode.Text.Trim())
                    {
                        if (BLL.PipingClassService.IsExistPipingClassCode(this.txtPipingClassCode.Text.Trim()))
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此管道等级代号已经存在！')", true);
                            return;
                        }
                    }
                    pipingClass.ISC_ID = this.PipingClassID;
                    BLL.PipingClassService.UpdatePipingClass(pipingClass);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改管道等级");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！')", true);
                }

                this.gvPipingClass.DataBind();
                this.TextIsReadOnly(true);
                this.IsButtonEnable(true);
                this.EnterEmpty();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
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

        #region 绑定GridView
        /// <summary>
        /// 绑定GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPipingClass_DataBound(object sender, EventArgs e)
        {
            if (this.gvPipingClass.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvPipingClass.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvPipingClass;
        }
        #endregion

        #region GridView点击事件
        /// <summary>
        /// GridView点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPipingClass_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            PipingClassID = e.CommandArgument.ToString();
            if (e.CommandName == "click")
            {
                Model.BS_IsoClass pipingClass = BLL.PipingClassService.GetPipingClassByPipingClassId(PipingClassID);
                this.txtPipingClassCode.Text = pipingClass.ISC_IsoCode;
                this.txtPipingClassName.Text = pipingClass.ISC_IsoName;
                this.txtPipingClassGrade.Text = pipingClass.ISC_IsoClass;
                this.txtRemark.Text = pipingClass.ISC_Remark;
            }
            if (e.CommandName == "Del")
            {
                if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account== BLL.Const.AdminId)
                {
                    if (judgementDelete())
                    {
                        BLL.PipingClassService.DeletePipingClass(PipingClassID);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除管道等级");
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('删除成功')", true);
                        this.gvPipingClass.DataBind();
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
            if (BLL.PW_IsoInfoService.GetIsoInfoByISCID(PipingClassID)>0)
            {
                content = "管线中已经使用该管道等级，不能删除！";
            }
            if (content=="")
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

        #region 查找
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            this.gvPipingClass.PageIndex = 0;
            this.gvPipingClass.DataBind();
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