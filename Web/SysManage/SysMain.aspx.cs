using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using BLL;

namespace Web.SysManage
{
    public partial class SysMain : PPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.ButtonNoEnbled();
                DataSet ds = this.GetMenu();
                DataView dv = ds.Tables["Sys_Menu"].DefaultView;

                foreach (DataRowView drv in dv)
                {
                    if (drv["MenuId"].ToString() == Const.RoleMenuId)
                    {
                        btnRole.Enabled = true;
                    }

                    if (drv["MenuId"].ToString() == Const.UserMenuId)
                    {
                        btnUser.Enabled = true;
                    }

                    if (drv["MenuId"].ToString() == Const.UpdatePasswordMenuId)
                    {
                        btnUpdatePassword.Enabled = true;
                    }

                    if (drv["MenuId"].ToString() == Const.RolePowerMenuId)
                    {
                        btnRolePower.Enabled = true;
                    }

                    if (drv["MenuId"].ToString() == Const.DataBakMenuId)
                    {
                        btnDataBak.Enabled = true;
                    }

                    if (drv["MenuId"].ToString() == Const.LogMenuId)
                    {
                        btnLog.Enabled = true;
                    }

                    if (drv["MenuId"].ToString() == Const.DepartMenuId)
                    {
                        btnDepart.Enabled = true;
                    }
                }
            }
        }

        #region 菜单列表
        /// <summary>
        /// /取得菜单列表
        /// </summary>
        /// <returns>菜单</returns>
        public DataSet GetMenu()
        {
            SqlParameter[] parameters = { new SqlParameter("@UserName", SqlDbType.NVarChar, 50),
                                          new SqlParameter("@projectId",SqlDbType.NVarChar,50)};
            parameters[0].Value = this.CurrUser.Account;
            if (this.CurrUser.ProjectId != null)
            {
                parameters[1].Value = this.CurrUser.ProjectId;
            }
            else
            {
                parameters[1].Value = "gly";
            }

            DataSet menu = SQLHelper.RunProcedure("sp_Sys_GetMenuByUserId", parameters, "Sys_Menu");
            return menu;
        }
        #endregion

        /// <summary>
        /// 按钮禁用
        /// </summary>
        private void ButtonNoEnbled()
        {
            btnRole.Enabled = false;
            btnUser.Enabled = false;
            btnUpdatePassword.Enabled = false;
            btnRolePower.Enabled = false;
            btnDataBak.Enabled = false;
            btnLog.Enabled = false;
            btnDepart.Enabled = false;
        }

        protected void btnRole_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("RoleList.aspx");
        }

        protected void btnUser_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("UserList.aspx");
        }

        protected void btnUpdatePassword_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("UpdatePassword.aspx");
        }

        protected void btnRolePower_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("RolePower.aspx");
        }

        protected void btnDataBak_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("DataBak.aspx");
        }

        protected void btnLog_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("LogList.aspx");
        }

        protected void btnDepart_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("DepartList.aspx");
        }
    }
}