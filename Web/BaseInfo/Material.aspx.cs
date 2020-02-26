using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.BaseInfo
{
    public partial class Material :PPage
    {
        #region 定义项
        /// <summary>
        /// 材质主键
        /// </summary>
        public string STE_ID
        {
            get 
            {
                return (string)ViewState["STE_ID"];
            }
            set
            {
                ViewState["STE_ID"] = value;
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
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.MaterialMenuId);

                Funs.PleaseSelect(drpSearch);
                this.drpSearch.Items.AddRange(BLL.MaterialService.SearchItem());

                Funs.PleaseSelect(ddlSteType);
                this.ddlSteType.Items.AddRange(BLL.MaterialService.GetSteTypeList());

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
        public void TextIsReadOnly(bool readOnly)
        {
            this.txtSte_Code.Enabled = !readOnly;
            this.txtSte_Name.Enabled = !readOnly;
            this.ddlSteType.Enabled = !readOnly;
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
            this.txtSte_Code.Text = string.Empty;
            this.txtSte_Name.Text = string.Empty;
            this.txtRemark.Text = string.Empty;
            this.ddlSteType.SelectedValue = "0";
        }
        #endregion

        #region 添加按钮
        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnAdd) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                STE_ID = string.Empty;
                this.txtSte_Code.Text = string.Empty;
                this.txtSte_Name.Text = string.Empty;
                this.ddlSteType.SelectedValue = "0";
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

        #region 修改按钮
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

        #region 保存按钮
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ButtonList.Contains(BLL.Const.BtnSave) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                Model.BS_Steel steel = new Model.BS_Steel();

                steel.STE_Code = this.txtSte_Code.Text.Trim();
                steel.STE_Name = this.txtSte_Name.Text.Trim();
                steel.STE_SteelType = Convert.ToInt32(this.ddlSteType.SelectedValue.ToString());
                steel.STE_Remark = this.txtRemark.Text.Trim();
                if (OperateState == BLL.Const.Add)
                {
                    if (BLL.MaterialService.IsExitSteelCode(this.txtSte_Code.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此材质代号已经存在！')", true);
                        return;
                    }
                    BLL.MaterialService.AddSteel(steel);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "添加材质");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！')", true);
                }
                if (OperateState == BLL.Const.Modify)
                {
                    var ste=  BLL.MaterialService.GetSteelBySteID(STE_ID);
                    if (ste != null)
                    {
                        string ste_code = ste.STE_Code;
                        if (ste_code != this.txtSte_Code.Text.Trim())
                        {
                            if (BLL.MaterialService.IsExitSteelCode(this.txtSte_Code.Text.Trim()))
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此材质代号已经存在！')", true);
                                return;
                            }
                        }
                        steel.STE_ID = STE_ID;
                        BLL.MaterialService.UpdateSteel(steel);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "修改材质");
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！')", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请增加此记录！')", true);
                        return;
                    }
                }
                this.gvSteel.DataBind();
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

        #region 取消按钮
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

        #region GridView绑定
        /// <summary>
        /// 绑定GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvSteel_DataBound(object sender, EventArgs e)
        {
            if (this.gvSteel.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvSteel.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvSteel;
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
            e.InputParameters["searchValue"] = this.txtSearch.Text.Trim();
        }
        #endregion

        #region GridView点击事件
        /// <summary>
        /// GridView点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvSteel_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            STE_ID = e.CommandArgument.ToString();
            if (e.CommandName=="click")
            {
                Model.BS_Steel steel = BLL.MaterialService.GetSteelBySteID(STE_ID);
                this.txtSte_Code.Text = steel.STE_Code;
                this.txtSte_Name.Text = steel.STE_Name;
                this.ddlSteType.SelectedValue =Convert.ToString(steel.STE_SteelType);
                this.txtRemark.Text = steel.STE_Remark;
            }
            if (e.CommandName=="Del")
            {
                if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    if (judgementDelete())
                    {
                        BLL.MaterialService.DeleteSteel(STE_ID);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除材质");
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('删除成功')", true);
                        this.gvSteel.DataBind();
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

        #region 查找
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            this.gvSteel.PageIndex = 0;
            this.gvSteel.DataBind();
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
            if (BLL.PW_IsoInfoService.GetIsoInfoBySteId(STE_ID)>0)
            {
                content = "管线中已经使用了该材质，不能删除！";
            }
            if (BLL.PW_JointInfoService.GetJointInfoBySTEID(STE_ID)>0)
            {
                content = "焊口中已经使用了该材质，不能删除！";
            }
            if (BLL.PW_JointInfoService.GetJointInfoBySTEID2(STE_ID)>0)
            {
                content = "焊口中已经使用了该材质，不能删除！";
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

        #region 转换字符串类型
        /// <summary>
        /// 转换钢材类型为字符串型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected string GetSteelType(object type)
        {
            if (type != null)
            {
                return (from x in BLL.MaterialService.GetSteTypeList() where x.Value == type.ToString() select x.Text).FirstOrDefault();                
            }
            return "";
        }
        #endregion
    }
}