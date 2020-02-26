using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using System.IO;
using BLL;

namespace Web.DataIn
{
    public partial class DataIn : PPage
    {
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
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 导入模版文件原始的虚拟路径
        /// </summary>
        private string initTemplatePath = Const.DataInTemplateUrl;

        /// <summary>
        /// 焊口集合
        /// </summary>
        public static List<Model.PW_JointInfo> jointInfos = new List<Model.PW_JointInfo>();

        /// <summary>
        /// 管线集合
        /// </summary>
        public static List<Model.PW_IsoInfo> isoInfos = new List<Model.PW_IsoInfo>();

        /// <summary>
        /// 错误集合
        /// </summary>
        public static List<Model.ErrorInfo> errorInfos = new List<Model.ErrorInfo>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.DataInMenuId);
                this.hdfileName.Value = string.Empty;
                this.hdCheckResult.Value = string.Empty;

                Funs.PleaseSelect(this.drpWorkArea);
                if (BLL.UnitService.GetUnit(this.CurrUser.UnitId) != null && BLL.UnitService.GetUnit(this.CurrUser.UnitId).UnitType == "2")
                {
                    this.drpWorkArea.Items.AddRange(BLL.WorkAreaService.GetWorkAreaListByUnit(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                }

                else
                {
                    this.drpWorkArea.Items.AddRange(BLL.WorkAreaService.GetWorkAreaList(this.CurrUser.ProjectId));
                }

                if (jointInfos != null)
                {
                    jointInfos.Clear();
                }
                if (isoInfos != null)
                {
                    isoInfos.Clear();
                }
                if (errorInfos != null)
                {
                    errorInfos.Clear();
                }
                this.div1.Visible = false;
                this.Table4.Visible = false;
            }
        }

        /// <summary>
        /// 把Excel导入数据库中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnIn_Click(object sender, ImageClickEventArgs e)
        {
            imgbtnIn.Enabled = false;
            if (errorInfos.Count <= 0)
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>ShowProgressBarInsert('" + this.hdfileName.Value + "','" + this.drpWorkArea.SelectedValue + "');</script>");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请先将错误数据修正，再重新导入保存！')", true);
            }
            imgbtnIn.Enabled = true;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            btnSave.Enabled = false;
            if (errorInfos.Count <= 0)
            {
                //foreach (var item in isoInfos)
                //{
                //    bool isExistIso = BLL.PW_IsoInfoService.IsExistIsoInfoCode(item.ISO_IsoNo, this.drpWorkArea.SelectedValue.Trim());
                //    if (isExistIso == false)
                //    {
                //        item.ISO_ID = SQLHelper.GetNewID(typeof(Model.PW_IsoInfo));
                //        item.ProjectId = this.CurrUser.ProjectId;
                //        item.ISO_Specification = (from x in jointInfos where x.ISO_ID == item.ISO_IsoNo orderby x.JOT_Size descending select x.JOT_JointDesc).FirstOrDefault();
                //        BLL.PW_IsoInfoService.AddIsoInfo(item);
                //    }
                //}
                //foreach (var j in jointInfos)
                //{
                //    var q = from x in BLL.Funs.DB.PW_IsoInfo where x.ISO_IsoNo == j.ISO_ID && x.BAW_ID == this.drpWorkArea.SelectedValue.Trim() select x;
                //    if (q.Count() > 0)
                //    {
                //        j.ISO_ID = q.First().ISO_ID;
                //        j.ProjectId = this.CurrUser.ProjectId;
                //        j.JOT_JointStatus = "100"; //正常口
                //        BLL.PW_JointInfoService.AddJointInfo(j);
                //    }
                //}
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>ShowProgressBarSave('" + this.drpWorkArea.SelectedValue + "');</script>");
                string rootPath = Server.MapPath("~/");
                string initFullPath = rootPath + initPath;
                string filePath = initFullPath + this.hdfileName.Value;
                if (filePath != string.Empty && System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);//删除上传的XLS文件
                }

                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('导入成功！')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请先将错误数据修正，再重新导入保存！')", true);
            }
            btnSave.Enabled = true;
        }

        /// <summary>
        /// 把布尔值转换为字符串类型
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        protected string ConvertString(object b)
        {
            if (b != null)
            {
                if ((bool)b)
                {
                    return "是";
                }
                else
                {
                    return "否";
                }
            }
            else
            {
                return "";
            }
        }



        /// <summary>
        /// 下载模版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgbtnUpload_Click(object sender, ImageClickEventArgs e)
        {
            string rootPath = Server.MapPath("~/");
            string uploadfilepath = rootPath + initTemplatePath;

            string filePath = initTemplatePath;
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

        protected void imgbtnOut_Click(object sender, ImageClickEventArgs e)
        {
            ToFiles();
        }

        private void ToFiles()
        {
            string strFileName = DateTime.Now.ToString("yyyyMMdd-hhmmss");
            System.Web.HttpContext HC = System.Web.HttpContext.Current;
            HC.Response.Clear();
            HC.Response.Buffer = true;
            HC.Response.ContentEncoding = System.Text.Encoding.UTF8;//设置输出流为简体中文

            //---导出为Excel文件
            HC.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8) + ".xls");
            HC.Response.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。

            System.IO.StringWriter sw = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
            this.Table9.RenderControl(htw);
            HC.Response.Write(sw.ToString());
            HC.Response.End();
        }

        /// <summary>
        /// 重载VerifyRenderingInServerForm方法，否则运行的时候会出现如下错误提示：“类型“GridView”的控件“GridView1”必须放在具有 runat=server 的窗体标记内”
        /// </summary>
        /// <param name="control"></param>
        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected string ConvertSTE1(object STE_ID)
        {
            if (STE_ID != null)
            {
                return BLL.MaterialService.GetSteelBySteID(STE_ID.ToString()).STE_Code;
            }
            else
            {
                return "";
            }
        }

        protected string ConvertSTE2(object STE_ID2)
        {
            if (STE_ID2 != null)
            {
                return BLL.MaterialService.GetSteelBySteID(STE_ID2.ToString()).STE_Code;
            }
            else
            {
                return "";
            }
        }

        protected string ConvertJoty(object JOTY_ID)
        {
            if (JOTY_ID != null)
            {
                return BLL.WeldService.GetJointTypeByJotID(JOTY_ID.ToString()).JOTY_Name;
            }
            else
            {
                return "";
            }
        }

        protected string ConvertWLO(object WLO_Code)
        {
            if (WLO_Code != null)
            {
                if (WLO_Code.ToString() == "F")
                {
                    return "安装";
                }
                else
                {
                    return "预制";
                }
            }
            else
            {
                return "";
            }
        }

        protected string ConvertWME(object WME_ID)
        {
            if (WME_ID != null)
            {
                return BLL.WeldingMethodService.GetWeldMethodByWMEID(WME_ID.ToString()).WME_Code;
            }
            else
            {
                return "";
            }
        }

        protected string ConvertWeldMat(object JOT_WeldMat)
        {
            if (JOT_WeldMat != null)
            {
                return BLL.ConsumablesService.getConsumablesByConsumablesId(JOT_WeldMat.ToString()).WMT_MatCode;
            }
            else
            {
                return "";
            }
        }

        protected string ConvertWeldSilk(object JOT_WeldSilk)
        {
            if (JOT_WeldSilk != null)
            {
                return BLL.ConsumablesService.getConsumablesByConsumablesId(JOT_WeldSilk.ToString()).WMT_MatCode;
            }
            else
            {
                return "";
            }
        }

        protected string ConvertJST(object JST_ID)
        {
            if (JST_ID != null)
            {
                return BLL.GrooveService.GetSlopeTypeByJSTID(JST_ID.ToString()).JST_Code;
            }
            else
            {
                return "";
            }
        }

        protected string ConvertComponent1(object JOT_Component1)
        {
            if (JOT_Component1 != null)
            {
                return BLL.ComponentsService.GetComponentByComID(JOT_Component1.ToString()).COM_Code;
            }
            else
            {
                return "";
            }
        }

        protected string ConvertComponent2(object JOT_Component2)
        {
            if (JOT_Component2 != null)
            {
                return BLL.ComponentsService.GetComponentByComID(JOT_Component2.ToString()).COM_Code;
            }
            else
            {
                return "";
            }
        }

        protected void imgbtnAudit_Click(object sender, ImageClickEventArgs e)
        {
            imgbtnAudit.Enabled = false;
            try
            {
                if (this.drpWorkArea.SelectedValue == "0")
                {
                    Response.Write("<script>alert('请您选择施工区域')</script> ");
                    return;
                }
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
                if (jointInfos != null)
                {
                    jointInfos.Clear();
                }
                if (isoInfos != null)
                {
                    isoInfos.Clear();
                }
                if (errorInfos != null)
                {
                    errorInfos.Clear();
                }
                string rootPath = Server.MapPath("~/");
                string initFullPath = rootPath + initPath;
                if (!Directory.Exists(initFullPath))
                {
                    Directory.CreateDirectory(initFullPath);
                }

                this.hdfileName.Value = BLL.Funs.GetNewFileName() + IsXls;
                string filePath = initFullPath + this.hdfileName.Value;
                FileExcel.PostedFile.SaveAs(filePath);
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>ShowProgressBarAudit('" + this.hdfileName.Value + "','" + this.drpWorkArea.SelectedValue + "');</script>");
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('审核完成,请点击导入！')", true);
                //ImportXlsToData(filePath);//将XLS文件的数据导入数据库   

                //if (filePath != string.Empty && System.IO.File.Exists(filePath))
                //{
                //    System.IO.File.Delete(filePath);//删除上传的XLS文件
                //}
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('" + ex.Message + "')", true);
            }
            imgbtnAudit.Enabled = true;
        }

        /// <summary>
        /// 审核数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnCheck_Click(object sender, ImageClickEventArgs e)
        {
            errorInfos.Clear();
            if (!string.IsNullOrEmpty(this.hdCheckResult.Value.Trim()))
            {
                string result = this.hdCheckResult.Value.Trim();
                List<string> errorInfoList = result.Split('|').ToList();
                foreach (var item in errorInfoList)
                {
                    string[] errors = item.Split(',');
                    Model.ErrorInfo errorInfo = new Model.ErrorInfo();
                    errorInfo.Row = Convert.ToInt32(errors[0]);
                    errorInfo.Column = errors[1];
                    errorInfo.Reason = errors[2];
                    errorInfos.Add(errorInfo);
                }
                if (errorInfos.Count > 0)
                {
                    this.div1.Visible = false;
                    this.Table4.Visible = true;
                    this.gvErrorInfo.DataSource = errorInfos;
                    this.gvErrorInfo.DataBind();
                }
            }
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnInsert_Click(object sender, ImageClickEventArgs e)
        {
            if (Session["jointInfos"] != null)
            {
                jointInfos = Session["jointInfos"] as List<Model.PW_JointInfo>;
            }
            if (Session["isoInfos"] != null)
            {
                isoInfos = Session["isoInfos"] as List<Model.PW_IsoInfo>;
            }
            if (jointInfos.Count > 0)
            {
                this.div1.Visible = true;
                this.Table4.Visible = false;
                this.gvJointInfo.DataSource = jointInfos;
                this.gvJointInfo.DataBind();
            }
        }

        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (this.drpWorkArea.SelectedValue != "0")
            {
                int count = (from x in Funs.DB.PW_IsoInfo
                             join y in Funs.DB.PW_JointInfo on x.ISO_ID equals y.ISO_ID
                             where x.BAW_ID == this.drpWorkArea.SelectedValue
                             select y).Count();
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('该区域共有" + count + "个焊口！')", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请先选择区域！')", true);
            }
        }

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
    }
}