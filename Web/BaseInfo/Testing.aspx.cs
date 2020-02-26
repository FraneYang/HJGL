using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.BaseInfo
{
    public partial class Testing : PPage
    {

        #region 定义项
        /// <summary>
        /// 探伤类型主键
        /// 
        /// </summary>
        public string TestingID
        {
            get
            {
                return (string)ViewState["TestingId"];
            }
            set
            {
                ViewState["TestingId"] = value;
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
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.TestingMenuId);

                Funs.PleaseSelect(drpSearch);
                this.drpSearch.Items.AddRange(BLL.TestingService.SearchItem());

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
        public void TextIsReadOnly(bool readOnly)
        {
            this.txtTestingCode.Enabled=!readOnly;
            this.txtTestingType.Enabled = !readOnly;
            this.txtDef.Enabled = !readOnly;
            this.ddlSysType.Enabled = !readOnly;
            this.txtSafeRange.Enabled = !readOnly;
            this.txtInjuryLevel.Enabled = !readOnly;
            this.txtRemark.Enabled =!readOnly;
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
            this.txtTestingCode.Text = string.Empty;
            this.txtTestingType.Text = string.Empty;
            this.ddlSysType.SelectedValue = "0";
            this.txtSafeRange.Text = string.Empty;
            this.txtInjuryLevel.Text = string.Empty;
            this.txtDef.Text = string.Empty;
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
                TestingID = string.Empty;
                this.txtTestingCode.Text = string.Empty;
                this.txtTestingType.Text = string.Empty;
                this.txtDef.Text = string.Empty;
                this.ddlSysType.SelectedValue = "0";
                this.txtSafeRange.Text = string.Empty;
                this.txtInjuryLevel.Text = string.Empty;
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
                Model.BS_NDTType testing = new Model.BS_NDTType();

                testing.NDT_Code = this.txtTestingCode.Text.Trim();
                testing.NDT_Name = this.txtTestingType.Text.Trim();
                testing.SysType = this.ddlSysType.SelectedValue.Trim();
                testing.NDT_Description = this.txtDef.Text.Trim();
                if (!string.IsNullOrEmpty(this.txtSafeRange.Text.Trim()))
                {
                    testing.NDT_SecuritySpace = Convert.ToDecimal(this.txtSafeRange.Text.Trim());
                }
                else
                {
                    testing.NDT_SecuritySpace = null;
                }              
                testing.NDT_Harm = this.txtInjuryLevel.Text.Trim();
                testing.NDT_Remark = this.txtRemark.Text.Trim();

                if (OperateState == BLL.Const.Add)
                {
                    if (BLL.TestingService.IsExitTestingCode(this.txtTestingCode.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此探伤类型代号已经存在！')", true);
                        return;
                    }
                    BLL.TestingService.AddTesting(testing);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "添加探伤类型");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！')", true);
                }
                if (OperateState == BLL.Const.Modify)
                {
                    string testingCode = BLL.TestingService.GetTestingByTestingId(TestingID).NDT_Code;
                    if (testingCode != this.txtTestingCode.Text.Trim())
                    {
                        if (BLL.TestingService.IsExitTestingCode(this.txtTestingCode.Text.Trim()))
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此探伤比例代号已经存在！')", true);
                            return;
                        }
                    }
                    testing.NDT_ID = TestingID;
                    BLL.TestingService.UpdateTesting(testing);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改探伤类型");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！')", true);
                }
                this.gvTesting.DataBind();
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
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            this.gvTesting.PageIndex = 0;
            this.gvTesting.DataBind();
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

        #region 判断是否可删除
        /// <summary>
        /// 判断是否可删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete()
        {
            string content = "";
            if (BLL.PW_IsoInfoService.GetIsoInfoByNDTID(TestingID) > 0)
            {
                content = "管线中已经使用了该探伤类型，不能删除！";
            }

            if (BLL.PW_JointInfoService.GetJointInfoByRepairID1(TestingID) > 0)
            {
                content = "焊口中已经使用该探伤类型，不能删除！";
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

        #region 绑定GridView
        /// <summary>
        /// 绑定GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTesting_DataBound(object sender, EventArgs e)
        {
            if (this.gvTesting.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvTesting.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvTesting;
        }
        #endregion

        #region GridView点击事件
        /// <summary>
        /// GridView点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvTesting_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            TestingID = e.CommandArgument.ToString();
            if (e.CommandName=="click")
            {
                Model.BS_NDTType testing = BLL.TestingService.GetTestingByTestingId(TestingID);
                this.txtTestingCode.Text = testing.NDT_Code;
                this.txtTestingType.Text = testing.NDT_Name;
                //this.ddlSysType.SelectedValue = testing.SysType;
                this.txtDef.Text = testing.NDT_Description;
                this.txtSafeRange.Text =Convert.ToString(testing.NDT_SecuritySpace);
                this.txtInjuryLevel.Text = testing.NDT_Harm;
                this.txtRemark.Text = testing.NDT_Remark;
            }
            if (e.CommandName=="Del")
            {
                if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    if (judgementDelete())
                    {
                        BLL.TestingService.DeleteTesting(TestingID);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除探伤类型");
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('删除成功')", true);
                        this.gvTesting.DataBind();
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
    }
}