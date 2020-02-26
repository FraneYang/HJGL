using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using System.IO;

namespace Web.WeldingManage
{
    public partial class ProcedureImageEdit :PPage
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
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ImageUrl;

        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                 string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.ImageMenuId);
                Funs.PleaseSelect(this.drpWeldingMethod);
                this.drpWeldingMethod.Items.AddRange(BLL.WeldingMethodService.GetWeldMethodNameList());
                Funs.PleaseSelect(this.drpJOTYID);
                this.drpJOTYID.Items.AddRange(BLL.WeldService.GetJointTypeNameList());
                    Funs.PleaseSelect(this.drpJSTID);
                this.drpJSTID.Items.AddRange(BLL.GrooveService.GetSlopeTypeNameList());
          
                string imageId = Request.Params["imageId"];
                if (!string.IsNullOrEmpty(imageId))
                {
                    Model.PW_ProcedureImageManage procedureImage = BLL.ProcedureImageService.GetImageById(imageId);
                    this.txtImageContent.Text = procedureImage.ImageContent;
                    if (!string.IsNullOrEmpty(procedureImage.WME_ID))
                    {
                        this.drpWeldingMethod.SelectedValue = procedureImage.WME_ID;
                    }
                    if (procedureImage.Thickness!=null)
                    {
                        this.txtThikness.Text = Convert.ToString(procedureImage.Thickness);
                    }
                    if (!string.IsNullOrEmpty(procedureImage.JOTY_ID))
                    {
                        this.drpJOTYID.SelectedValue = procedureImage.JOTY_ID;
                    }
                    if (!string.IsNullOrEmpty(procedureImage.JST_ID))
                    {
                        this.drpJSTID.SelectedValue = procedureImage.JST_ID;
                    }

                    ImageId = imageId;
                    string url = string.Empty;
                    if (!String.IsNullOrEmpty(procedureImage.AttachUrl))
                    {
                        url = "../" + procedureImage.AttachUrl.Replace('\\', '/');
                        string[] subUrl = url.Split('/');
                        string fileName = subUrl[subUrl.Count() - 1];
                        this.lblAttachUrl.Text = fileName.Substring(fileName.IndexOf("~") + 1);
                        this.lblAttachUrl.Style.Add("cursor", "hand");
                        this.lblAttachUrl.ToolTip = "查看图片";
                        this.lblAttachUrl.Attributes["onClick"] = "window.open('" + url + "')";
                    }
                    else
                    {
                        this.lblAttachUrl.Text = "没有图片或图片已被删除！";
                    }
                }
            }
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnSave) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                Model.PW_ProcedureImageManage procedureImage = new Model.PW_ProcedureImageManage();
                procedureImage.ImageContent = this.txtImageContent.Text.Trim();
                if (this.drpWeldingMethod.SelectedValue!="0")
                {
                    procedureImage.WME_ID = this.drpWeldingMethod.SelectedValue;
                }
                if (!string.IsNullOrEmpty(this.txtThikness.Text.Trim()))
                {
                    procedureImage.Thickness = Convert.ToDecimal(this.txtThikness.Text.Trim());
                }
                if (this.drpJOTYID.SelectedValue!="0")
                {
                    procedureImage.JOTY_ID = this.drpJOTYID.SelectedValue;
                }
                if (this.drpJSTID.SelectedValue!="0")
                {
                    procedureImage.JST_ID = this.drpJSTID.SelectedValue;
                }
                if (this.fuAttachUrl.HasFile)
                {
                    string rootPath = Server.MapPath("~/");
                    string initFullPath = rootPath + initPath;
                    if (!Directory.Exists(initFullPath))
                    {
                        Directory.CreateDirectory(initFullPath);
                    }
                    string filePath = this.fuAttachUrl.PostedFile.FileName;
                    int count = this.fuAttachUrl.PostedFile.ContentLength;
                    string fileName = Funs.GetNewFileName() + "~" + Path.GetFileName(filePath);
                    string savePath = initPath + fileName;
                    string fullPath = initFullPath + fileName;
                    if (!File.Exists(fullPath))
                    {
                        byte[] buffer = new byte[count];
                        Stream stream = this.fuAttachUrl.PostedFile.InputStream;
                        stream.Read(buffer, 0, count);
                        MemoryStream memoryStream = new MemoryStream(buffer);
                        FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write);
                        memoryStream.WriteTo(fs);
                        memoryStream.Flush();
                        memoryStream.Close();
                        fs.Flush();
                        fs.Close();
                        memoryStream = null;
                        fs = null;
                        procedureImage.AttachUrl = savePath;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('文件名已经存在')", true);
                    }
                }


                if (!string.IsNullOrEmpty(ImageId))
                {
                    procedureImage.ImageId = ImageId;
                    Model.PW_ProcedureImageManage procedureImage1 = BLL.ProcedureImageService.GetImageById(ImageId);
                    if (!string.IsNullOrEmpty(procedureImage1.AttachUrl))
                    {
                        if (string.IsNullOrEmpty(procedureImage.AttachUrl))
                        {
                            procedureImage.AttachUrl = procedureImage1.AttachUrl;
                        }
                        else
                        {
                            string rootPath = Server.MapPath("~/");
                            string urlFullPath = rootPath + procedureImage1.AttachUrl;
                            if (File.Exists(urlFullPath))
                            {
                                File.Delete(urlFullPath);
                            }
                        }
                    }
                    BLL.ProcedureImageService.UpdateProcedureImage(procedureImage);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改图片信息");
                }
                else
                {
                    procedureImage.ImageId = SQLHelper.GetNewID(typeof(Model.PW_ProcedureImageManage));
                    BLL.ProcedureImageService.AddProcedureImage(procedureImage);
                    BLL.LogService.AddLog(this.CurrUser.UserId, "添加图片信息");
                }
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！');location.href='ProcedureImageList.aspx'", true);
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
        protected void btnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("ProcedureImageList.aspx");
        }

    }
}