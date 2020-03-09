using BLL;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Web.DataIn
{
    public partial class DataInTable : PPage
    {
        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {
                var dataInTemp = from x in Funs.DB.Sys_DataInTemp where x.ProjectId == this.CurrUser.ProjectId && x.UserId == this.CurrUser.UserId select x;
                var errData = from x in dataInTemp where x.ToopValue != null select x;
                this.lbCout.Text = "总数为【" + dataInTemp.Count().ToString() + "】条记录；错误记录数【" + errData.Count().ToString() + "】";
            }
        }

        #region 导入数据到临时表
        /// <summary>
        /// 导入数据到临时表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnImport_Click(object sender, EventArgs e)
        {
            imgbtnImport.Enabled = false;
            try
            {                
                if (this.FileExcel.HasFile == false)
                {
                    Response.Write("<script>alert('请您选择Excel文件')</script> ");
                    return;
                }
                string IsXls = Path.GetExtension(FileExcel.FileName).ToString().Trim().ToLower();
                if (IsXls != ".xls")
                {
                    Response.Write("<script>alert('只可以选择Excel文件')</script>");
                    return;
                }

                string rootPath = Server.MapPath("~/");
                string initFullPath = rootPath + Const.ExcelUrl;
                if (!Directory.Exists(initFullPath))
                {
                    Directory.CreateDirectory(initFullPath);
                }
                string fileName =  Funs.GetNewFileName() + IsXls;
                string filePath = initFullPath + fileName;
                this.FileExcel.PostedFile.SaveAs(filePath);
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>ShowDataInTableProgressBar('" + fileName + "','0');</script>");       ///进度条0 导入，1保存          
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('" + ex.Message + "')", true);
            }
            imgbtnImport.Enabled = true;
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
            e.InputParameters["projectId"] = this.CurrUser.ProjectId;
            e.InputParameters["userId"] = this.CurrUser.UserId;
            e.InputParameters["isRowNo"] = this.ckSorIndex.Checked;
        }
        #endregion

        #region GV 行点击事件
        /// <summary>
        /// GV事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvJointInfo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "TempClick")
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>ShowDataInTableEdit('" + e.CommandArgument.ToString() + "');</script>");
            }
            if (e.CommandName == "TempDelete")
            {
                BLL.DataInTableService.DeleteDataInTempByDataInTempID(e.CommandArgument.ToString());
                this.gvJointInfo.PageIndex = 0;
                this.gvJointInfo.DataBind();
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('删除成功！')", true);
            }
        }
        #endregion

        #region GV 绑定
        /// <summary>
        ///  绑定GV
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvJointInfo_DataBound(object sender, EventArgs e)
        {
            if (this.gvJointInfo.BottomPagerRow == null)
            {
                return;
            }
            ((Web.Controls.GridNavgator)this.gvJointInfo.BottomPagerRow.FindControl("GridNavgator1")).GridView = this.gvJointInfo;
            int rowsCount = this.gvJointInfo.Rows.Count;           
            for (int i = 0; i < rowsCount; i++)
            {
                LinkButton Value4 = ((LinkButton)(this.gvJointInfo.Rows[i].FindControl("Value4")));
                if (!string.IsNullOrEmpty(Value4.ToolTip))
                {                    
                    this.gvJointInfo.Rows[i].BackColor = System.Drawing.Color.LightCoral;
                }
            }

            var dataInTemp = from x in Funs.DB.Sys_DataInTemp where x.ProjectId == this.CurrUser.ProjectId && x.UserId == this.CurrUser.UserId select x;
            var errData = from x in dataInTemp where x.ToopValue != null select x;
            this.lbCout.Text = "总数为【" + dataInTemp.Count().ToString() + "】条记录；错误记录数【" + errData.Count().ToString()+"】";
        }
        #endregion

        #region 数据导入说明
        /// <summary>
        /// 数据导入说明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void DataHelp_Click(object sender, EventArgs e)
        {
            this.TemplateUpload(Const.DataInHelpUrl);
        }

        #region  模板下载方法
        /// <summary>
        /// 模板下载方法
        /// </summary>
        /// <param name="initTemplatePath"></param>
        protected void TemplateUpload(string initTemplatePath)
        {
            string rootPath = Server.MapPath("~/");
            string uploadfilepath = rootPath + initTemplatePath;

            string filePath = initTemplatePath;
            string fileName = Path.GetFileName(filePath);
            FileInfo info = new FileInfo(uploadfilepath);
            if (info.Exists)
            {
                long fileSize = info.Length;
                Response.Clear();
                Response.ContentType = "application/x-zip-compressed";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
                Response.AddHeader("Content-Length", fileSize.ToString());
                Response.TransmitFile(uploadfilepath, 0, fileSize);
                Response.Flush();
                Response.Close();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('模板不存在，请联系管理员！')", true);
            }
        }
        #endregion        
        #endregion

        #region 下载模版
        /// <summary>
        /// 下载模版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnUpload_Click(object sender, ImageClickEventArgs e)
        {            
            string rootPath = Server.MapPath("~/");
            string filePath = Const.DataInTemplateUrl;
            string uploadfilepath = rootPath + filePath;           
            string fileName = Path.GetFileName(filePath);
            FileInfo info = new FileInfo(uploadfilepath);
            long fileSize = info.Length;
            Response.Clear();
            Response.ContentType = "application/x-zip-compressed";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + System.Web.HttpUtility.UrlEncode(fileName, System.Text.Encoding.UTF8));
            Response.AddHeader("Content-Length", fileSize.ToString().Trim());
            Response.TransmitFile(uploadfilepath, 0, fileSize);
            Response.Flush();
            Response.Close();
        }
        #endregion

        #region 删除所有当前记录
        /// <summary>
        /// 删除所有当前记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAllDelete_Click(object sender, ImageClickEventArgs e)
        {
            ////先删除临时表中 该人员以前导入的数据
            BLL.DataInTableService.DeleteDataInTempByProjectIdUserId(this.CurrUser.ProjectId, this.CurrUser.UserId);
            this.gvJointInfo.PageIndex = 0;
            this.gvJointInfo.DataBind();
            this.lbCout.Text = string.Empty;
            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('删除成功！')", true);
        }
        #endregion

        /// <summary>
        /// 返回事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgDetail_Click(object sender, ImageClickEventArgs e)
        {
            this.gvJointInfo.PageIndex = 0;
            this.gvJointInfo.DataBind();
        }

        #region 保存审核事件
        /// <summary>
        /// 保存审核事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>ShowDataInTableProgressBar('" + string.Empty + "','1');</script>");       ///进度条0 导入，1保存
        }
        #endregion

        #region 选择排序方式
        /// <summary>
        ///  选择排序方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ckSorIndex_CheckedChanged(object sender, EventArgs e)
        {
            this.gvJointInfo.PageIndex = 0;
            this.gvJointInfo.DataBind();
        }
        #endregion
    }
}