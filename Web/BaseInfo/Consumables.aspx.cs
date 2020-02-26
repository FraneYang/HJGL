using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.BaseInfo
{
    public partial class Consumables : PPage
    {
        #region 定义项
        /// <summary>
        /// 焊接耗材主键
        /// </summary>
        public string ConsumablesId
        {
            get
            {
                return (string)ViewState["ConsumablesId"];
            }
            set
            {
                ViewState["ConsumablesId"] = value;
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
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.ConsumablesMenuId);

                Funs.PleaseSelect(this.drpSearch);
                this.drpSearch.Items.AddRange(BLL.ConsumablesService.SearchList());

                Funs.PleaseSelect(this.drpConsumablesType);
                this.drpConsumablesType.Items.AddRange(BLL.ConsumablesService.ConsumablesTypeList());              

                this.btnSave.Enabled = false;
                this.btncancel.Enabled = false;
                this.TextIsReadOnly(true);
            }
        }
        #endregion 

        #region 绑定GridView
        /// <summary>
        /// 绑定GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvConsumablesType_DataBound(object sender, EventArgs e)
        {
            if (this.gvConsumablesType.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvConsumablesType.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvConsumablesType;
        }
        #endregion 

        #region GridView基本操作
        /// <summary>
        /// GridView基本操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvConsumablesType_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ConsumablesId = e.CommandArgument.ToString();
            if (e.CommandName == "ConsumablesLink")
            {
                Model.BS_WeldMaterial consumables = BLL.ConsumablesService.getConsumablesByConsumablesId(ConsumablesId);

                this.txtConsumablesCode.Text = consumables.WMT_MatCode;
                this.txtConsumablesName.Text = consumables.WMT_MatName;
                if (!String.IsNullOrEmpty(consumables.WMT_MatType))
                {
                    this.drpConsumablesType.SelectedValue = consumables.WMT_MatType;
                }

                this.txtDef.Text = consumables.WMT_Remark;
            }

            if (e.CommandName == "DeleteConsumables")
            {
                if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    if (judgementDelete())
                    {
                        BLL.ConsumablesService.DeleteConsumables(ConsumablesId);
                        this.gvConsumablesType.DataBind();
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除焊接耗材");
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
                }
            }
        }
        #endregion

        #region 判断是否可以删除
        /// <summary>
        /// 判断是否可以删除
        /// </summary>
        /// <returns></returns>
        private bool judgementDelete()
        {
            string content = "";
            if (BLL.PW_JointInfoService.GetJointInfoByWeldSilk(ConsumablesId)>0 ||BLL.PW_JointInfoService.GetJointInfoByWeldSilk(ConsumablesId)>0)
            {
                content = "焊口中已经使用了该焊材，不能删除！";
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

        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            this.gvConsumablesType.PageIndex = 0;
            this.gvConsumablesType.DataBind();
        }
        #endregion

        #region 添加增加按钮
        /// <summary>
        /// 添加装置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnAdd) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                this.EnterEmpty();
                this.OperateState = BLL.Const.Add;
                this.ButtonIsEnabled(false);
                this.TextIsReadOnly(false);
                this.txtConsumablesCode.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnModify_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnModify) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                this.OperateState = BLL.Const.Modify;
                this.ButtonIsEnabled(false);
                this.TextIsReadOnly(false);
                this.txtConsumablesCode.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }
        #endregion

        #region 保存事件
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnSave) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                Model.BS_WeldMaterial consumables = new Model.BS_WeldMaterial();

                consumables.WMT_MatCode = this.txtConsumablesCode.Text.Trim();
                consumables.WMT_MatName = this.txtConsumablesName.Text.Trim();
                if (this.drpConsumablesType.SelectedValue != "0")
                {
                    consumables.WMT_MatType = this.drpConsumablesType.SelectedValue;
                }
                consumables.WMT_Remark = this.txtDef.Text.Trim();

                if (OperateState == BLL.Const.Add)
                {
                    if (BLL.ConsumablesService.IsExistConsumablesCode(this.txtConsumablesCode.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此焊接耗材代号已存在！')", true);
                        return;
                    }

                    BLL.ConsumablesService.AddConsumables(consumables);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "增加焊接耗材");
                }
                if (OperateState == BLL.Const.Modify)
                {
                    string ConsumablesCode = BLL.ConsumablesService.getConsumablesByConsumablesId(this.ConsumablesId).WMT_MatCode;
                    if (ConsumablesCode != this.txtConsumablesCode.Text.Trim())
                    {
                        if (BLL.ConsumablesService.IsExistConsumablesCode(this.txtConsumablesCode.Text.Trim()))
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此焊接耗材代号已存在！')", true);
                            return;
                        }
                    }

                    consumables.WMT_ID = this.ConsumablesId;
                    BLL.ConsumablesService.updateConsumables(consumables);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改焊接耗材");
                }


                this.gvConsumablesType.DataBind();
                this.ButtonIsEnabled(true);
                this.TextIsReadOnly(true);
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
            this.ButtonIsEnabled(true);
            this.TextIsReadOnly(true);
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
            e.InputParameters["searchValue"] = this.txtSearch.Text.ToString();
        }
        #endregion

        #region 文本框是否可编辑、按钮是否可用、清空文本框
        /// <summary>
        /// 文本框是否可编辑
        /// </summary>
        /// <param name="readOnly"></param>
        public void TextIsReadOnly(bool readOnly)
        {
            this.txtConsumablesCode.ReadOnly = readOnly;
            this.txtConsumablesName.ReadOnly = readOnly;
            this.drpConsumablesType.Enabled = !readOnly;
            this.txtDef.ReadOnly = readOnly;
        }

        /// <summary>
        /// 按钮是否可选
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
        ///  输入清空
        /// </summary>
        protected void EnterEmpty()
        {
            ConsumablesId = string.Empty;
            this.drpConsumablesType.SelectedValue = "0";
            this.txtConsumablesCode.Text = string.Empty; 
            this.txtConsumablesName.Text = string.Empty;
            this.txtDef.Text = string.Empty;
        }
        #endregion

        #region GV绑定下拉项
        /// <summary>
        ///  焊材类型
        /// </summary>
        /// <param name="applyTypeId"></param>
        /// <returns></returns>
        protected string ConsumablesTypeName(object ConsumablesType)
        {
            if (ConsumablesType == null)
            {
                return null;
            }
            else
            {
                return BLL.ConsumablesService.ConsumablesTypeList().FirstOrDefault(x => x.Value == ConsumablesType.ToString()).Text;
            }
        }
        #endregion
    }
}