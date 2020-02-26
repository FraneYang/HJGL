using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.SysManage
{
    public partial class ButtonToMenu : PPage
    {
        #region 主键
        /// <summary>
        /// 主键
        /// </summary>
        public string ButtonToMenuId
        {
            get
            {
                return (string)ViewState["ButtonToMenuId"];
            }
            set
            {
                ViewState["ButtonToMenuId"] = value;
            }
        }
        #endregion

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

        #region 加载页面
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Funs.PleaseSelect(drpMainMenu);
                drpMainMenu.Items.AddRange(BLL.SysMenuService.GetSupMenuNameList());
                Funs.PleaseSelect(ddlMenuName);

                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.ButtonToMenuMenuId);
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
            if (ButtonList.Contains(BLL.Const.BtnSave) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                Model.ButtonToMenu buttonToMenu = new Model.ButtonToMenu();
                BLL.ButtonPowerService.DeleteButtonPowerByBtn(ddlMenuName.SelectedValue);
                BLL.ButtonToMenuService.DeleteButtonToMenu(ddlMenuName.SelectedValue);

                int sortIndex = 0;
                foreach (ListItem item in cblButtonName.Items)
                {
                    if (item.Selected)
                    {
                        buttonToMenu.MenuId = this.ddlMenuName.SelectedValue.ToString();
                        buttonToMenu.ButtonName = item.Value;
                        buttonToMenu.SortIndex = sortIndex++;
                        BLL.ButtonToMenuService.AddButtonToMenu(buttonToMenu);
                    }
                }
                BLL.LogService.AddLog(this.CurrUser.UserId, "添加按钮权限");
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！')", true);
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
            foreach (ListItem item in cblButtonName.Items)
            {
                item.Selected = false;
            }
            this.ddlMenuName.SelectedValue = "0";
        }
        #endregion

        #region 下拉菜单触发事件
        protected void drpMainMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlMenuName.Items.Clear();
            Funs.PleaseSelect(ddlMenuName);
            ddlMenuName.Items.AddRange(BLL.SysMenuService.GetMenuNameList(drpMainMenu.SelectedValue));
        }
       
        /// <summary>
        /// 下拉菜单触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlMenuName_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ddlmenu = this.ddlMenuName.SelectedValue;
            if (ddlmenu != "0")
            {
                Model.ButtonToMenu buttonToMenu = BLL.ButtonToMenuService.GetButtonToMenuByMenuId(ddlmenu);

                if (buttonToMenu != null)
                {
                    foreach (ListItem item in cblButtonName.Items)
                    {
                        if (BLL.ButtonToMenuService.GetButtonToMenuByButtonName(buttonToMenu.MenuId,item.Value) != null)
                        {
                            item.Selected = true;
                        }
                        else
                        {
                            item.Selected = false;
                        }
                    }
                }
                else
                {
                    foreach (ListItem item in cblButtonName.Items)
                    {
                        item.Selected = false;
                    }
                }
            }
        }
        #endregion
    }
}