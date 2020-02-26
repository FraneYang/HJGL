using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.PersonManage
{
    public partial class WelderScoreEdit :PPage
    {
        #region  定义变量
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId
        {
            get
            {
                return (string)ViewState["RoleId"];
            }
            set
            {
                ViewState["RoleId"] = value;
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
        /// 人员id
        /// </summary>
        public string WED_ID
        {
            get
            {
                return (string)ViewState["WED_ID"];
            }
            set
            {
                ViewState["WED_ID"] = value;
            }
        }

        /// <summary>
        ///焊工业绩主键
        /// </summary>
        public string WelderScoreId
        {
            get
            {
                return (string)ViewState["WelderScoreId"];
            }
            set
            {
                ViewState["WelderScoreId"] = value;
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
        /// 文本框是否可编辑
        /// </summary>
        /// <param name="readOnly"></param>
        private void TextIsReadOnly(bool readOnly)
        {
            this.txtProjectName.Enabled = !readOnly;
            this.txtUnitName.Enabled = !readOnly;
            this.txtTotalJot.Enabled = !readOnly;
            this.txtQualifiedJot.Enabled = !readOnly;
            this.txtWeldRange.Enabled = !readOnly;
            this.txtRemark.Enabled = !readOnly;
        }

        /// <summary>
        /// 清空文本框
        /// </summary>
        private void EnterEmpty()
        {
            this.txtProjectName.Text = string.Empty;
            this.txtUnitName.Text = string.Empty;
            this.txtTotalJot.Text = string.Empty;
            this.txtQualifiedJot.Text = string.Empty;
            this.txtWeldRange.Text = string.Empty;
            this.txtRemark.Text = string.Empty;
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
            //this.btncancel.Enabled = !enabled;
        }

        #endregion

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
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.PersonManageMenuId);

                WED_ID = Request.Params["WED_ID"];

                if (!string.IsNullOrEmpty(WED_ID))
                {
                    var person = BLL.PersonManageService.GetWelderByWenId(WED_ID);
                    if (person != null)
                    {
                        this.lblWelderScore.Text = person.WED_Name + "的焊工业绩";
                    }
                }
                this.btnSave.Enabled = false;
                this.TextIsReadOnly(false);
            }
        }
       
        /// <summary>
        ///保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnSave) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                Model.BS_WelderScore welderScore = new Model.BS_WelderScore();

                welderScore.WED_ID = this.WED_ID;
                welderScore.ProjectName = this.txtProjectName.Text.Trim();
                welderScore.UnitName = this.txtUnitName.Text.Trim();
                welderScore.TotalJot = Convert.ToInt32(this.txtTotalJot.Text.Trim());
                welderScore.QualifiedJot = Convert.ToInt32(this.txtQualifiedJot.Text.Trim());
                welderScore.WeldRange = this.txtWeldRange.Text.Trim();
                welderScore.Remark = this.txtRemark.Text.Trim();

                if (OperateState == BLL.Const.Add)
                {
                    welderScore.WelderScoreId = SQLHelper.GetNewID(typeof(Model.BS_WelderScore));
                    BLL.WelderScoreService.AddWelderScore(welderScore);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "添加焊工业绩信息！"); 
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功')", true);
                }
                if (OperateState == BLL.Const.Modify)
                {
                    welderScore.WelderScoreId = WelderScoreId;
                    BLL.WelderScoreService.UpdateWelderScore(welderScore);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改焊工业绩信息！");
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功')", true);
                }
               
                this.gvWelderScore.DataBind();
                this.TextIsReadOnly(true);
                this.IsButtonEnable(true);
                EnterEmpty();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }

        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnAdd) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                WelderScoreId = string.Empty;
                EnterEmpty();
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

        /// <summary>
        /// 返回按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btncancel_Click(object sender, ImageClickEventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "window.opener.location=window.opener.location;OnClientClick=window.close();", true);
        }
        
        /// <summary>
        /// 绑定GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvWelderScore_DataBound(object sender, EventArgs e)
        {
            if (this.gvWelderScore.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvWelderScore.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvWelderScore;
        }

        /// <summary>
        /// GridView点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvWelderScore_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            WelderScoreId = e.CommandArgument.ToString();
            if (e.CommandName == "click")
            {
                Model.BS_WelderScore welderScore = BLL.WelderScoreService.GetWelderScoreByWelderScoreId(WelderScoreId);
                this.txtProjectName.Text = welderScore.ProjectName;
                this.txtUnitName.Text = welderScore.UnitName;
                this.txtTotalJot.Text = Convert.ToString(welderScore.TotalJot);
                this.txtQualifiedJot.Text = Convert.ToString(welderScore.QualifiedJot);
                this.txtWeldRange.Text = welderScore.WeldRange;
                this.txtRemark.Text = welderScore.Remark;
            }
            if (e.CommandName == "Del")
            {
                if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    if (judgementDelete())
                    {
                        BLL.WelderScoreService.DeleteWelderScore(WelderScoreId);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除焊工业绩");
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('删除成功')", true);
                        this.gvWelderScore.DataBind();
                        EnterEmpty();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('对不起，您没有这个权限，请联系管理员！')", true);
                    return;
                }
            }
        }
        /// <summary>
        /// 判断是否可以删除
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

        /// <summary>
        /// GridView绑定参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
            e.InputParameters["wed_id"] = this.WED_ID;
        }
    }
}