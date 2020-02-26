using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;
using System.IO;
using BLL;

namespace Web.SysManage
{
    public partial class UserList : PPage
    {
        /// <summary>
        /// 人员主键
        /// </summary>
        public string UserId
        {
            get
            {
                return (string)ViewState["UserId"];
            }
            set
            {
                ViewState["UserId"] = value;
            }
        }

        /// <summary>
        /// 操作状态：增加、修改、删除
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

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {
                this.btnSave.Enabled = false;
                this.btncancel.Enabled = false;
                this.TextIsReadOnly(true);
                Funs.PleaseSelect(this.drpSearch);
                Funs.PleaseSelect(this.drpRole);
                Funs.PleaseSelect(this.drpUnit);
                this.drpSearch.Items.AddRange(BLL.UserService.SearchList());
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.UserMenuId);
                if (!string.IsNullOrEmpty(this.CurrUser.ProjectId))   //项目用户界面
                {
                    this.drpRole.Items.AddRange(BLL.RoleService.GetRoleList(this.CurrUser.ProjectId));
                    this.drpUnit.Items.AddRange(BLL.UnitService.GetUnitNameList(this.CurrUser.ProjectId));
                }
                else   //总部用户界面
                {
                    this.trProject.Visible = false;
                    this.lbRole.Visible = false;
                    this.drpRole.Visible = false;
                    this.gvUser.Columns[2].Visible = false;
                    this.gvUser.Columns[3].Visible = false;
                    this.gvUser.Columns[4].Visible = false;
                }
            }
        }

        /// <summary>
        /// 绑定参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["searchItem"] = this.drpSearch.SelectedValue;
            e.InputParameters["searchValue"] = this.txtSearch.Text.ToString();
            e.InputParameters["projectId"] = this.CurrUser.ProjectId;
        }




        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnAdd) || this.CurrUser.Account == BLL.Const.AdminId || this.CurrUser.Account==BLL.Const.GLY || this.CurrUser.Account==BLL.Const.GLY)
            {
                UserId = string.Empty;
                ResetInterface();

                this.OperateState = BLL.Const.Add;
                this.ButtonIsEnabled(false);
                this.TextIsReadOnly(false);
                this.txtAccount.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }

        /// <summary>
        /// 重置界面
        /// </summary>
        private void ResetInterface()
        {
            this.txtAccount.Text = string.Empty;
            this.drpRole.SelectedIndex = 0;
            this.txtUserName.Text = string.Empty;
            this.txtUserCode.Text = string.Empty;
            this.drpIsPost.SelectedIndex = 0;
            this.drpUnit.SelectedIndex = 0;
            this.txtRemark.Text = string.Empty;
        }

        /// <summary>
        /// 修改按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnModify_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnModify) || this.CurrUser.Account == BLL.Const.AdminId || this.CurrUser.Account==BLL.Const.GLY)
            {
                this.OperateState = BLL.Const.Modify;
                this.ButtonIsEnabled(false);
                this.TextIsReadOnly(false);
                this.txtAccount.Focus();
                //this.drpRole.Enabled = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnSave) || this.CurrUser.Account == BLL.Const.AdminId || this.CurrUser.Account==BLL.Const.GLY)
            {
                Model.Sys_User user = new Model.Sys_User();
                user.UserCode = this.txtUserCode.Text.Trim();
                user.Account = this.txtAccount.Text.Trim();
                user.UserName = this.txtUserName.Text.Trim();
                if (this.drpUnit.SelectedValue != "0")
                {
                    user.UnitId = this.drpUnit.SelectedValue.ToString();
                }
                else
                {
                    user.UnitId = null;
                }
                if (this.drpRole.SelectedValue != "0")
                {
                    user.RoleId = this.drpRole.SelectedValue; 
                }
                else
                {
                    user.RoleId = null;
                }
                user.IsPost = Convert.ToBoolean(this.drpIsPost.SelectedValue);
                user.ProjectId = this.CurrUser.ProjectId;
                user.Remark = this.txtRemark.Text.Trim();
                Model.Sys_User user1 = BLL.UserService.GetUserByAccount(this.txtAccount.Text.Trim(), this.CurrUser.ProjectId);
                if (OperateState == BLL.Const.Add)
                {
                    if (user1 != null)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('添加失败，此账户已存在！')", true);
                        return;
                    }
                    user.Password = UserService.EncryptionPassword("123");//默认密码“123”
                    BLL.UserService.AddUser(user);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "添加用户信息");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('添加成功！')", true);
                }
                if (OperateState == BLL.Const.Modify)
                {
                    var userInfo = BLL.UserService.GetUserName(UserId);
                    if (userInfo.Account != this.txtAccount.Text.Trim())
                    {
                        if (user1 != null)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('修改失败，此账户已存在！')", true);
                            return;
                        }
                    }
                    user.UserId = UserId;
                    user.Password = userInfo.Password;
                    BLL.UserService.UpdateUser(user);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改用户信息");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('修改成功！')", true);
                }
                this.gvUser.DataBind();
                this.ButtonIsEnabled(true);
                this.TextIsReadOnly(true);
                ResetInterface();
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }

        /// <summary>
        /// 取消按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btncancel_Click(object sender, ImageClickEventArgs e)
        {
            ResetInterface();
            this.ButtonIsEnabled(true);
            this.TextIsReadOnly(true);
        }
        /// <summary>
        /// 文本框是否可编辑
        /// </summary>
        /// <param name="readOnly"></param>
        private void TextIsReadOnly(bool readOnly)
        {
            this.txtAccount.ReadOnly = readOnly;
            this.txtUserName.ReadOnly = readOnly;
            this.txtUserCode.ReadOnly = readOnly;
            this.drpRole.Enabled = !readOnly;
            this.drpIsPost.Enabled = !readOnly;
            this.drpUnit.Enabled = !readOnly;
            this.txtRemark.ReadOnly = readOnly;
        }

        /// <summary>
        /// 按钮是否可用
        /// </summary>
        /// <param name="enabled"></param>
        public void ButtonIsEnabled(bool enabled)
        {
            this.btnAdd.Enabled = enabled;
            this.btnModify.Enabled = enabled;
            this.btnSave.Enabled = !enabled;
            this.btncancel.Enabled = !enabled;
        }

        /// <summary>
        /// 查询按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            this.gvUser.PageIndex = 0;
            this.gvUser.DataBind();
        }

        /// <summary>
        /// 绑定GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvUser_DataBound(object sender, EventArgs e)
        {
            if (this.gvUser.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvUser.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvUser;
        }

        /// <summary>
        /// GridView点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvUser_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            UserId = e.CommandArgument.ToString();
            if (e.CommandName == "click")
            {
                Model.Sys_User user = BLL.UserService.GetUserName(UserId);
                this.txtUserCode.Text = user.UserCode;
                this.txtUserName.Text = user.UserName;
                this.drpUnit.SelectedValue = user.UnitId;
                this.txtAccount.Text = user.Account;
                this.drpIsPost.SelectedValue = user.IsPost.ToString();
                if (user.RoleId != null)
                {
                    this.drpRole.SelectedValue = user.RoleId;
                }
                this.txtRemark.Text = user.Remark;
            }
            if (e.CommandName == "Del")
            {
                if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId || this.CurrUser.Account==BLL.Const.GLY)
                {
                    if (judgementDelete())
                    {
                        BLL.UserService.DeleteUser(UserId);
                        this.gvUser.DataBind();
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除用户信息");
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
                }
            }
        }

        /// <summary>
        /// 把是否在岗的布尔值转换为字符串类型
        /// </summary>
        /// <param name="isPost"></param>
        /// <returns></returns>
        protected string ConvertIsPost(object isPost)
        {
            if (isPost != null)
            {
                if ((bool)isPost)
                {
                    return "是";
                }
            }
            return "否";
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

        protected void imgbtnOut_Click(object sender, ImageClickEventArgs e)
        {
            ToFiles();
        }

        private void ToFiles()
        {
            string strFileName = DateTime.Now.ToString("yyyyMMdd-hhmmss");
            System.Web.HttpContext HC = System.Web.HttpContext.Current;
            HC.Response.Clear();
            HC.Response.Buffer = true;
            HC.Response.ContentEncoding = System.Text.Encoding.UTF8;//设置输出流为简体中文

            //---导出为Excel文件
            HC.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8) + ".xls");
            HC.Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。

            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            this.gvUser.PageSize = 500;
            this.gvUser.Columns[7].Visible = false;
            this.gvUser.DataBind();
            this.gvUser.RenderControl(htw);
            HC.Response.Write(sw.ToString());
            HC.Response.End();
        }

        /// <summary>
        /// 重载VerifyRenderingInServerForm方法，否则运行的时候会出现如下错误提示：“类型“GridView”的控件“GridView1”必须放在具有 runat=server 的窗体标记内”
        /// </summary>
        /// <param name="control"></param>
        public override void VerifyRenderingInServerForm(Control control)
        {
        }
    }
}