using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace Web.WeldingManage
{
    public partial class ProcedureImageList :PPage
    {
        /// <summary>
        /// 图片主键
        /// </summary>
        public string ImageId
        {
            get
            {
                return (string)ViewState["ImageId"];
            }
            set
            {
                ViewState["ImageId"] = value;
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
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.ImageMenuId);
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
                Response.Redirect("ProcedureImageEdit.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }

       /// <summary>
       /// 绑定Gridview
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        protected void gvPictureList_DataBound(object sender, EventArgs e)
        {
            if (this.gvPictureList.BottomPagerRow == null)
            {
                return;
            }

            ((Web.Controls.GridNavgator)this.gvPictureList.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvPictureList;

            int rowsCount = this.gvPictureList.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                string imageId = ((ImageButton)(this.gvPictureList.Rows[i].FindControl("imgbtnScanUrl"))).CommandArgument;
                Model.PW_ProcedureImageManage procedureImage = BLL.ProcedureImageService.GetImageById(imageId);
                if (!string.IsNullOrEmpty(procedureImage.AttachUrl))
                {
                    string url = "../" + procedureImage.AttachUrl.Replace('\\', '/');
                    ((ImageButton)(this.gvPictureList.Rows[i].FindControl("imgbtnScanUrl"))).ToolTip = "查看图片";
                    ((ImageButton)(this.gvPictureList.Rows[i].FindControl("imgbtnScanUrl"))).Attributes["onclick"] = "window.open('" + url + "')";
                }
                else
                {
                    ((ImageButton)(this.gvPictureList.Rows[i].FindControl("imgbtnScanUrl"))).ToolTip = "无图片";
                }
            }
        }
        /// <summary>
        /// Gridview行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvPictureList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ImageId = e.CommandArgument.ToString();
            if (e.CommandName == "click")
            {
                if (ButtonList.Contains(BLL.Const.BtnModify) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    Response.Redirect("ProcedureImageEdit.aspx?ImageId=" + ImageId);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
                }
            }
            if (e.CommandName == "del")
            {
                if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
                {
                    Model.PW_ProcedureImageManage procedureImage = BLL.ProcedureImageService.GetImageById(ImageId);
                    string rootPath = Server.MapPath("~/");
                    string urlFullPath = rootPath + procedureImage.AttachUrl;
                    if (File.Exists(urlFullPath))
                    {
                        File.Delete(urlFullPath);
                    }

                    BLL.ProcedureImageService.DeleteProcedureImage(ImageId);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "删除图片记录！");
                    this.gvPictureList.DataBind();
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('删除成功！')", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
                }
            }
        }
    }
}