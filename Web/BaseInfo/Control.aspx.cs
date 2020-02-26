using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.BaseInfo
{
    public partial class Control : PPage
    {
        #region 定义项

        /// <summary>
        /// 主键
        /// </summary>
        public string BST_ID
        {
            get
            {
                return (string)ViewState["BST_ID"];
            }
            set
            {
                ViewState["BST_ID"] = value;
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
                ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.ControlMenuId);

                Funs.PleaseSelect(drpSearch);
                this.drpSearch.Items.AddRange(BLL.ControlService.SearchItem());

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
        /// <param name="p"></param>
        private void TextIsReadOnly(bool readOnly)
        {
            //this.txtcode.Enabled = !readOnly;
            //this.txtDia.Enabled = !readOnly;
            this.txtDN.Enabled = !readOnly;
            this.txtInch.Enabled = !readOnly;
            //this.txtSch5s.Enabled = !readOnly;
            //this.txtSch10s.Enabled = !readOnly;
            //this.txtSch10.Enabled = !readOnly;
            //this.txtSch20.Enabled = !readOnly;
            //this.txtSch30.Enabled = !readOnly;
            //this.txtSch40s.Enabled = !readOnly;
            //this.txtSTD.Enabled = !readOnly;
            //this.txtSch40.Enabled = !readOnly;
            //this.txtSch60.Enabled = !readOnly;
            //this.txtSch80s.Enabled = !readOnly;
            //this.txtXS.Enabled = !readOnly;
            //this.txtSch80.Enabled = !readOnly;
            //this.txtSch100.Enabled = !readOnly;
            //this.txtSch120.Enabled = !readOnly;
            //this.txtSch140.Enabled = !readOnly;
            //this.txtSch160.Enabled = !readOnly;
            //this.txtXXS.Enabled = !readOnly;
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
            //this.txtcode.Text = string.Empty;
            //this.txtDia.Text = string.Empty;
            this.txtDN.Text = string.Empty;
            this.txtInch.Text = string.Empty;
            //this.txtSch5s.Text = string.Empty;
            //this.txtSch10s.Text = string.Empty;
            //this.txtSch10.Text = string.Empty;
            //this.txtSch20.Text = string.Empty;
            //this.txtSch30.Text = string.Empty;
            //this.txtSch40s.Text = string.Empty;
            //this.txtSTD.Text = string.Empty;
            //this.txtSch40.Text = string.Empty;
            //this.txtSch60.Text = string.Empty;
            //this.txtSch80s.Text = string.Empty;
            //this.txtXS.Text = string.Empty;
            //this.txtSch80.Text = string.Empty;
            //this.txtSch100.Text = string.Empty;
            //this.txtSch120.Text = string.Empty;
            //this.txtSch140.Text = string.Empty;
            //this.txtSch160.Text = string.Empty;
            //this.txtXXS.Text = string.Empty;
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
                BST_ID = string.Empty;
                //this.txtcode.Text = string.Empty;
                //this.txtDia.Text = string.Empty;
                this.txtDN.Text = string.Empty;
                this.txtInch.Text = string.Empty;
                //this.txtSch5s.Text = string.Empty;
                //this.txtSch10s.Text = string.Empty;
                //this.txtSch10.Text = string.Empty;
                //this.txtSch20.Text = string.Empty;
                //this.txtSch30.Text = string.Empty;
                //this.txtSch40s.Text = string.Empty;
                //this.txtSTD.Text = string.Empty;
                //this.txtSch40.Text = string.Empty;
                //this.txtSch60.Text = string.Empty;
                //this.txtSch80s.Text = string.Empty;
                //this.txtXS.Text = string.Empty;
                //this.txtSch80.Text = string.Empty;
                //this.txtSch100.Text = string.Empty;
                //this.txtSch120.Text = string.Empty;
                //this.txtSch140.Text = string.Empty;
                //this.txtSch160.Text = string.Empty;
                //this.txtXXS.Text = string.Empty;
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
                Model.BS_SchTab schTab = new Model.BS_SchTab();
                //schTab.BST_Code = this.txtcode.Text.Trim();
                //if (!string.IsNullOrEmpty(this.txtDia.Text.Trim()))
                //{
                //    schTab.BST_Dia =Convert.ToDecimal(this.txtDia.Text.Trim());
                //}
                //else
                //{
                //    schTab.BST_Dia = 0;
                //}              
                schTab.BST_DN = this.txtDN.Text.Trim();
                if (!string.IsNullOrEmpty(this.txtInch.Text.Trim()))
                {
                    schTab.BST_Inch =Convert.ToDecimal(this.txtInch.Text.Trim());
                }
                else
                {
                    schTab.BST_Inch = 0;
                }
                //if (!string.IsNullOrEmpty(this.txtSch5s.Text.Trim()))
                //{
                //    schTab.BST_Sch5s = Convert.ToDecimal(this.txtSch5s.Text.Trim());
                //}
                //else
                //{
                //    schTab.BST_Sch5s = 0;
                //}
                //if (!string.IsNullOrEmpty(this.txtSch10s.Text.Trim()))
                //{
                //    schTab.BST_Sch10s = Convert.ToDecimal(this.txtSch10s.Text.Trim());
                //}
                //else
                //{
                //    schTab.BST_Sch10s = 0;
                //}
                //if (!string.IsNullOrEmpty(this.txtSch10.Text.Trim()))
                //{
                //    schTab.BST_Sch10 = Convert.ToDecimal(this.txtSch10.Text.Trim());
                //}
                //else
                //{
                //    schTab.BST_Sch10 = 0;
                //}
                //if (!string.IsNullOrEmpty(this.txtSch20.Text.Trim()))
                //{
                //    schTab.BST_Sch20 = Convert.ToDecimal(this.txtSch20.Text.Trim());
                //}
                //else
                //{
                //    schTab.BST_Sch20 = 0;
                //}
                //if (!string.IsNullOrEmpty(this.txtSch30.Text.Trim()))
                //{
                //    schTab.BST_Sch30 = Convert.ToDecimal(this.txtSch30.Text.Trim());
                //}
                //else
                //{
                //    schTab.BST_Sch30 = 0; 
                //}
                //if (!string.IsNullOrEmpty(this.txtSch40s.Text.Trim()))
                //{
                //    schTab.BST_Sch40s = Convert.ToDecimal(this.txtSch40s.Text.Trim());
                //}
                //else
                //{
                //    schTab.BST_Sch40s = 0;
                //}
                //if (!string.IsNullOrEmpty(this.txtSTD.Text.Trim()))
                //{
                //    schTab.BST_STD = Convert.ToDecimal(this.txtSTD.Text.Trim());
                //}
                //else
                //{
                //    schTab.BST_STD = 0;
                //}
                //if (!string.IsNullOrEmpty(this.txtSch40.Text.Trim()))
                //{
                //    schTab.BST_Sch40 = Convert.ToDecimal(this.txtSch40.Text.Trim());
                //}
                //else
                //{
                //    schTab.BST_Sch40 = 0;
                //}
                //if (!string.IsNullOrEmpty(this.txtSch60.Text.Trim()))
                //{
                //    schTab.BST_Sch60 =Convert.ToDecimal(this.txtSch60.Text.Trim());
                //}
                //else
                //{
                //    schTab.BST_Sch60 = 0;
                //}
                //if (!string.IsNullOrEmpty(this.txtSch80s.Text.Trim()))
                //{
                //    schTab.BST_Sch80s = Convert.ToDecimal(this.txtSch80s.Text.Trim());
                //}
                //else
                //{
                //    schTab.BST_Sch80s = 0;
                //}
                //if (!string.IsNullOrEmpty(this.txtXS.Text.Trim()))
                //{
                //    schTab.BST_XS = Convert.ToDecimal(this.txtXS.Text.Trim());
                //}
                //else
                //{
                //    schTab.BST_XS = 0;
                //}
                //if (!string.IsNullOrEmpty(this.txtSch80.Text.Trim()))
                //{
                //    schTab.BST_Sch80 = Convert.ToDecimal(this.txtSch80.Text.Trim());
                //}
                //else
                //{
                //    schTab.BST_Sch80 = 0;
                //}
                //if (!string.IsNullOrEmpty(this.txtSch100.Text.Trim()))
                //{
                //    schTab.BST_Sch100 = Convert.ToDecimal(this.txtSch100.Text.Trim());
                //}
                //else
                //{
                //    schTab.BST_Sch100 = 0;
                //}
                //if (!string.IsNullOrEmpty(this.txtSch120.Text.Trim()))
                //{
                //    schTab.BST_Sch120 = Convert.ToDecimal(this.txtSch120.Text.Trim());
                //}
                //else
                //{
                //    schTab.BST_Sch120 = 0;
                //}
                //if (!string.IsNullOrEmpty(this.txtSch140.Text.Trim()))
                //{
                //    schTab.BST_Sch140 = Convert.ToDecimal(this.txtSch140.Text.Trim());
                //}
                //else
                //{
                //    schTab.BST_Sch140 = 0;
                //}
                //if (!string.IsNullOrEmpty(this.txtSch160.Text.Trim()))
                //{
                //    schTab.BST_Sch160 = Convert.ToDecimal(this.txtSch160.Text.Trim());
                //}
                //else
                //{
                //    schTab.BST_Sch160 = 0;
                //}
                //if (!string.IsNullOrEmpty(this.txtXXS.Text.Trim()))
                //{
                //    schTab.BST_XXS = Convert.ToDecimal(this.txtXXS.Text.Trim());
                //}
                //else
                //{
                //    schTab.BST_XXS = 0;
                //}
                schTab.BST_Remark = this.txtRemark.Text.Trim();

                if (OperateState==BLL.Const.Add)
                {
                    if (BLL.ControlService.IsExitBSTCode(this.txtDN.Text.Trim()))
                    {
                         ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此公称直径已经存在！')", true);
                        return;
                    }
                    BLL.ControlService.AddSchTab(schTab);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "添加公称直径寸径对照");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！')", true);
                }
                if (OperateState==BLL.Const.Modify)
                {
                    string bst_code = BLL.ControlService.GetSchTabByBSTID(BST_ID).BST_Code;
                    if (bst_code != this.txtDN.Text.Trim())
                    {
                        if (BLL.ControlService.IsExitBSTCode(this.txtDN.Text.Trim()))
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此公称直径已经存在！')", true);
                            return;
                        }
                    }
                    schTab.BST_ID = BST_ID;
                    BLL.ControlService.UpdateSchTab(schTab);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改公称直径寸径对照");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！')", true);
                }
                this.gvControl.DataBind();
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
        /// 取消按钮
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
            this.gvControl.PageIndex = 0;
            this.gvControl.DataBind();
        }
        #endregion

        #region GridView绑定
        /// <summary>
        /// GridView绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvControl_DataBound(object sender, EventArgs e)
        {
            if (this.gvControl.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvControl.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvControl;
        }
        #endregion

        #region GridView点击事件
        /// <summary>
        /// GridView点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvControl_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            BST_ID = e.CommandArgument.ToString();
            if (e.CommandName == "click")
            {
                Model.BS_SchTab schTab = BLL.ControlService.GetSchTabByBSTID(BST_ID);
                //this.txtcode.Text = schTab.BST_Code;
                //this.txtDia.Text =string.Format("{0:N}",schTab.BST_Dia);
                this.txtDN.Text = schTab.BST_DN;
                this.txtInch.Text =string.Format("{0:N}",schTab.BST_Inch);
                //this.txtSch5s.Text = string.Format("{0:N}",schTab.BST_Sch5s);
                //this.txtSch10s.Text = string.Format("{0:N}",schTab.BST_Sch10s);
                //this.txtSch10.Text = string.Format("{0:N}",schTab.BST_Sch10);
                //this.txtSch20.Text = string.Format("{0:N}",schTab.BST_Sch20);
                //this.txtSch30.Text = string.Format("{0:N}",schTab.BST_Sch30);
                //this.txtSch40s.Text = string.Format("{0:N}",schTab.BST_Sch40s);
                //this.txtSTD.Text = string.Format("{0:N}",schTab.BST_STD);
                //this.txtSch40.Text = string.Format("{0:N}",schTab.BST_Sch40);
                //this.txtSch60.Text = string.Format("{0:N}",schTab.BST_Sch60);
                //this.txtSch80s.Text = string.Format("{0:N}",schTab.BST_Sch80s);
                //this.txtXS.Text = string.Format("{0:N}",schTab.BST_XS);
                //this.txtSch80.Text = string.Format("{0:N}",schTab.BST_Sch80);
                //this.txtSch100.Text = string.Format("{0:N}",schTab.BST_Sch100);
                //this.txtSch120.Text = string.Format("{0:N}",schTab.BST_Sch120);
                //this.txtSch140.Text = string.Format("{0:N}",schTab.BST_Sch140);
                //this.txtSch160.Text = string.Format("{0:N}",schTab.BST_Sch160);
                //this.txtXXS.Text =string.Format("{0:N}",schTab.BST_XXS);
                this.txtRemark.Text = schTab.BST_Remark;
            }
            if (e.CommandName == "Del")
            {
                if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    if (judgementDelete())
                    {
                        BLL.ControlService.DeleteSchTab(BST_ID);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除直径寸径对照");
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('删除成功')", true);
                        this.gvControl.DataBind();
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
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["searchItem"] = this.drpSearch.SelectedValue;
            e.InputParameters["searchValue"] = this.txtSearch.Text;
        }
        #endregion
    }
}