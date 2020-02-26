using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.BaseInfo
{
    public partial class Detection : PPage
    {
        #region 定义项
        /// <summary>
        /// 探伤比例主键
        /// </summary>
        public string NDTR_ID
        {
            get
            {
                return (string)ViewState["NDTR_ID"];
            }
            set
            {
                ViewState["NDTR_ID"] = value;
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
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.DetectionMenuId);

                Funs.PleaseSelect(drpSearch);
                this.drpSearch.Items.AddRange(BLL.DetectionService.SearchItem());

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
            this.txtNDTRateCode.Enabled = !readOnly;
            this.txtNDTRateName.Enabled = !readOnly;
            this.txtNDRate.Enabled = !readOnly;
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
            this.txtNDTRateCode.Text = string.Empty;
            this.txtNDTRateName.Text = string.Empty;
            this.txtNDRate.Text = string.Empty;
            this.txtRemark.Text = string.Empty;
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
                NDTR_ID = string.Empty;
                this.txtNDTRateCode.Text = string.Empty;
                this.txtNDTRateName.Text = string.Empty;
                this.txtNDRate.Text = string.Empty;
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
                Model.BS_NDTRate ndtrate = new Model.BS_NDTRate();
                ndtrate.NDTR_Code = this.txtNDTRateCode.Text.Trim();
                ndtrate.NDTR_Name = this.txtNDTRateName.Text.Trim();
                ndtrate.NDTR_Rate = this.txtNDRate.Text.Trim();
                ndtrate.NDTR_Remark = this.txtRemark.Text.Trim();

                if (OperateState == BLL.Const.Add)
                {
                    if (BLL.DetectionService.IsExitNDTRateCode(this.txtNDTRateCode.Text.Trim()))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此探伤比例代号已经存在！')", true);
                        return;
                    }
                    BLL.DetectionService.AddNDTRate(ndtrate);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "添加探伤比例");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！')", true);
                }
                if (OperateState == BLL.Const.Modify)
                {
                    string ndtratecode = BLL.DetectionService.GetNDTRateByNDTRID(NDTR_ID).NDTR_Code;
                    if (ndtratecode != this.txtNDTRateCode.Text.Trim())
                    {
                        if (BLL.DetectionService.IsExitNDTRateCode(this.txtNDTRateCode.Text.Trim()))
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此探伤比例代号已经存在！')", true);
                            return;
                        }
                    }
                    ndtrate.NDTR_ID = NDTR_ID;
                    BLL.DetectionService.UpdateNDTRate(ndtrate);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改探伤比例");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！')", true);

                }
                this.gvNDTRate.DataBind();
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

        #region 搜索
        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            this.gvNDTRate.PageIndex = 0;
            this.gvNDTRate.DataBind();
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

            if (BLL.PW_IsoInfoService.GetIsoInfoByNDTRID(NDTR_ID)>0)
            {
                content = "管线中已经使用了该探伤比例，不能删除！";
            }
            var trust = from x in Funs.DB.CH_Trust where x.CH_NDTRate == NDTR_ID select x;
            if (trust.Count() > 0)
            {
                content = "委托单中已经使用了该探伤比例，不能删除！";
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

        #region GridView绑定事件
        /// <summary>
        /// 绑定GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvNDTRate_DataBound(object sender, EventArgs e)
        {
            if (this.gvNDTRate.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvNDTRate.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvNDTRate;
        }
        #endregion

        #region GridView点击事件
        /// <summary>
        /// GridView点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvNDTRate_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            NDTR_ID = e.CommandArgument.ToString();
            if (e.CommandName == "click")
            {
                Model.BS_NDTRate ndtrate = BLL.DetectionService.GetNDTRateByNDTRID(NDTR_ID);
                this.txtNDTRateCode.Text = ndtrate.NDTR_Code;
                this.txtNDTRateName.Text = ndtrate.NDTR_Name;
                this.txtNDRate.Text = ndtrate.NDTR_Rate;
                this.txtRemark.Text = ndtrate.NDTR_Remark;
            }
            if (e.CommandName == "Del")
            {
                if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    if (judgementDelete())
                    {
                        BLL.DetectionService.DeleteNDTRate(NDTR_ID);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除探伤比例");
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('删除成功')", true);
                        this.gvNDTRate.DataBind();
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