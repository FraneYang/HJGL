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

namespace Web.WeldingManage
{
    public partial class WeldReportDataIn : PPage
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
        private string initTemplatePath = Const.WeldReportDataInTemplateUrl;

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

                if (errorInfos != null)
                {
                    errorInfos.Clear();
                }
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
            //imgbtnIn.Enabled = false;
            if (errorInfos.Count <= 0)
            {
                //if (this.FileExcel.HasFile == false)
                //{
                //    Response.Write("<script>alert('请您选择Excel文件')</script> ");
                //    return;
                //}
                //string IsXls = Path.GetExtension(FileExcel.FileName).ToString().Trim().ToLower();
                //if (IsXls != ".xls")
                //{
                //    Response.Write("<script>alert('只可以选择Excel文件')</script>");
                //    return;
                //}
                //if (errorInfos != null)
                //{
                //    errorInfos.Clear();
                //}
                string rootPath = Server.MapPath("~/");
                string initFullPath = rootPath + initPath;
                if (!Directory.Exists(initFullPath))
                {
                    Directory.CreateDirectory(initFullPath);
                }

                //this.hdfileName.Value = BLL.Funs.GetNewFileName() + IsXls;
                string filePath = initFullPath + this.hdfileName.Value;
                //FileExcel.PostedFile.SaveAs(filePath);
                ImportXlsToData2(filePath);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请先将错误数据修正，再重新导入保存！')", true);
            }
            //imgbtnIn.Enabled = true;
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

        protected void imgbtnAudit_Click(object sender, ImageClickEventArgs e)
        {
            //imgbtnAudit.Enabled = false;
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
                ImportXlsToData(filePath);

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('" + ex.Message + "')", true);
            }
            //imgbtnAudit.Enabled = true;
        }

        #region 读Excel提取数据
        /// <summary>
        /// 从Excel提取数据--》Dataset
        /// </summary>
        /// <param name="filename">Excel文件路径名</param>
        private void ImportXlsToData(string fileName)
        {
            try
            {
                string oleDBConnString = String.Empty;
                oleDBConnString = "Provider=Microsoft.Jet.OLEDB.4.0;";
                oleDBConnString += "Data Source=";
                oleDBConnString += fileName;
                oleDBConnString += ";Extended Properties=Excel 8.0;";
                OleDbConnection oleDBConn = null;
                OleDbDataAdapter oleAdMaster = null;
                DataTable m_tableName = new DataTable();
                DataSet ds = new DataSet();

                oleDBConn = new OleDbConnection(oleDBConnString);
                oleDBConn.Open();
                m_tableName = oleDBConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (m_tableName != null && m_tableName.Rows.Count > 0)
                {

                    m_tableName.TableName = m_tableName.Rows[0]["TABLE_NAME"].ToString().Trim();

                }
                string sqlMaster;
                sqlMaster = " SELECT *  FROM [" + m_tableName.TableName + "]";
                oleAdMaster = new OleDbDataAdapter(sqlMaster, oleDBConn);
                oleAdMaster.SelectCommand.CommandTimeout = 600;
                oleAdMaster.Fill(ds, "m_tableName");
                oleAdMaster.Dispose();
                oleDBConn.Close();
                oleDBConn.Dispose();

                AddDatasetToSQL(ds.Tables[0], 16);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// 将Dataset的数据导入数据库
        /// </summary>
        /// <param name="pds">数据集</param>
        /// <param name="Cols">数据集列数</param>
        /// <returns></returns>
        private bool AddDatasetToSQL(DataTable pds, int Cols)
        {
            string projectId = this.CurrUser.ProjectId;
            string result = string.Empty;
            int ic, ir;
            ic = pds.Columns.Count;
            if (ic < Cols)
            {
                throw new Exception("导入Excel格式错误！Excel只有" + ic.ToString().Trim() + "列");
            }

            ir = pds.Rows.Count;
            if (pds != null && ir > 0)
            {
                var oldViewInfos = from x in Funs.DB.View_JointInfoAndIsoInfo
                                   where x.ProjectId == projectId
                                   select x;

                var units = from x in Funs.DB.Base_Unit where x.ProjectId == projectId select x;
                var installations = from x in Funs.DB.Base_Installation where x.ProjectId == projectId select x;
                var workAreas = from x in Funs.DB.Base_WorkArea where x.ProjectId == projectId select x;
                var steels = from x in Funs.DB.BS_Steel select x;
                var types = from x in Funs.DB.BS_JointType select x;
                var methods = from x in Funs.DB.BS_WeldMethod select x;
                var teamGroups = from x in Funs.DB.HS_Education where x.ProjectId == this.CurrUser.ProjectId select x;
                var welders = from x in Funs.DB.BS_Welder select x;

                for (int i = 0; i < ir; i++)
                {
                    Model.View_JointInfoAndIsoInfo oldViewInfo = new Model.View_JointInfoAndIsoInfo();
                    oldViewInfo = oldViewInfos.Where(x => x.BAW_ID == this.drpWorkArea.SelectedValue
                                                       && x.ISO_IsoNo == pds.Rows[i][2].ToString().Trim()
                                                       && x.JOT_JointNo == pds.Rows[i][3].ToString().Trim()).FirstOrDefault();

                    if (oldViewInfo != null)
                    {
                        Model.PW_JointInfo jointInfo = BLL.PW_JointInfoService.GetJointInfoByJotID(oldViewInfo.JOT_ID);
                        if (string.IsNullOrEmpty(jointInfo.DReportID))
                        {
                            string row0 = pds.Rows[i][0].ToString();
                            var unit = units.Where(x => x.UnitCode == row0.Trim()).FirstOrDefault();
                            if (!string.IsNullOrEmpty(row0))
                            {
                                if (units.Where(x => x.UnitCode == row0.Trim()).FirstOrDefault() == null)
                                {
                                    result += (i + 2).ToString() + "," + "单位代号" + "," + "[" + row0 + "]不存在！" + "|";
                                }
                            }
                            else
                            {
                                result += (i + 2).ToString() + "," + "单位代号" + "," + "此项为必填项！" + "|";
                            }

                            string row1 = pds.Rows[i][1].ToString();
                            if (!string.IsNullOrEmpty(row1))
                            {
                                var installation = installations.Where(x => x.InstallationCode == row1.Trim()).FirstOrDefault();
                                if (installation == null)
                                {
                                    result += (i + 2).ToString() + "," + "装置代号" + "," + "[" + row1 + "]不存在！" + "|";
                                }
                            }
                            else
                            {
                                result += (i + 2).ToString() + "," + "装置代号" + "," + "此项为必填项！" + "|";
                            }

                            if (!string.IsNullOrEmpty(row0))
                            {

                                var area = workAreas.Where(x => x.UnitId == unit.UnitId && x.WorkAreaId == this.drpWorkArea.SelectedValue).FirstOrDefault();
                                if (area == null)
                                {
                                    result += (i + 2).ToString() + "," + "区域" + "," + "该单位没有此区域号！" + "|";
                                }
                            }

                            if (string.IsNullOrEmpty(pds.Rows[i][2].ToString()))
                            {
                                result += (i + 2).ToString() + "," + "管线代号" + "," + "此项为必填项！" + "|";
                            }

                            if (string.IsNullOrEmpty(pds.Rows[i][3].ToString()))
                            {
                                result += (i + 2).ToString() + "," + "焊口代号" + "," + "此项为必填项！" + "|";
                            }

                            string row4 = pds.Rows[i][4].ToString();
                            if (!string.IsNullOrEmpty(row4))
                            {
                                if (steels.Where(x => x.STE_Code == row4.Trim()).FirstOrDefault() == null)
                                {
                                    result += (i + 2).ToString() + "," + "材质1代号" + "," + "[" + row4 + "]错误！" + "|";
                                }
                            }

                            string row5 = pds.Rows[i][5].ToString();
                            if (!string.IsNullOrEmpty(row5))
                            {
                                if (types.Where(x => x.JOTY_Code == row5.Trim()).FirstOrDefault() == null)
                                {
                                    result += (i + 2).ToString() + "," + "焊缝类型" + "," + "[" + row5 + "]错误！" + "|";
                                }
                            }

                            string row6 = pds.Rows[i][6].ToString();
                            if (!string.IsNullOrEmpty(row6))
                            {
                                if (row6.Trim() != "安装" && row6.Trim() != "预制")
                                {
                                    result += (i + 2).ToString() + "," + "焊接区域" + "," + "[" + row6 + "]错误！" + "|";
                                }
                            }
                            else
                            {
                                result += (i + 2).ToString() + "," + "焊接区域" + "," + "此项为必填项！" + "|";
                            }

                            string row7 = pds.Rows[i][7].ToString();
                            if (!string.IsNullOrEmpty(row7))
                            {
                                if (row7.Trim() != "固定" && row7.Trim() != "活动")
                                {
                                    result += (i + 2).ToString() + "," + "焊口属性" + "," + "[" + row7 + "]错误！" + "|";
                                }
                            }
                            else
                            {
                                result += (i + 2).ToString() + "," + "焊口属性" + "," + "此项为必填项！" + "|";
                            }

                            string row8 = pds.Rows[i][8].ToString();
                            if (!string.IsNullOrEmpty(row8))
                            {
                                try
                                {
                                    decimal doneDin = Convert.ToDecimal(row8.Trim());
                                }
                                catch (Exception)
                                {
                                    result += (i + 2).ToString() + "," + "达因数" + "," + "[" + row8 + "]错误！" + "|";
                                }
                            }

                            string row11 = pds.Rows[i][11].ToString();
                            if (!string.IsNullOrEmpty(row11))
                            {
                                if (methods.Where(x => x.WME_Code == row11.Trim()).FirstOrDefault() == null)
                                {
                                    result += (i + 2).ToString() + "," + "焊接方法" + "," + "[" + row11 + "]错误！" + "|";
                                }
                            }

                            string row12 = pds.Rows[i][12].ToString();
                            if (!string.IsNullOrEmpty(row12) && !string.IsNullOrEmpty(row0))
                            {
                                var teamGroup = teamGroups.Where(x => x.EDU_Unit == unit.UnitId && x.EDU_Code == row12.Trim()).FirstOrDefault();
                                if (teamGroup == null)
                                {
                                    result += (i + 2).ToString() + "," + "班组代号" + "," + "该单位没有此班组代号！" + "|";
                                }
                            }

                            string row13 = pds.Rows[i][13].ToString();
                            if (!string.IsNullOrEmpty(row13) && !string.IsNullOrEmpty(row0))
                            {
                                if (!string.IsNullOrEmpty(row12))
                                {
                                    var teamGroup = teamGroups.Where(x => x.EDU_Unit == unit.UnitId && x.EDU_Code == row12.Trim()).FirstOrDefault();
                                    if (teamGroup != null)
                                    {
                                        var welder = welders.Where(x => x.WED_Unit == unit.UnitId && x.EDU_ID == teamGroup.EDU_ID && x.WED_Code == row13.Trim()).FirstOrDefault();
                                        if (welder == null)
                                        {
                                            result += (i + 2).ToString() + "," + "盖面焊工代号" + "," + "该单位没有此焊工代号！" + "|";
                                        }
                                    }
                                }
                                else
                                {
                                    var welder = welders.Where(x => x.WED_Unit == unit.UnitId && x.WED_Code == row13.Trim()).FirstOrDefault();
                                    if (welder == null)
                                    {
                                        result += (i + 2).ToString() + "," + "盖面焊工代号" + "," + "该单位没有此焊工代号！" + "|";
                                    }
                                }
                            }
                            else
                            {
                                result += (i + 2).ToString() + "," + "盖面焊工代号" + "," + "此项为必填项！" + "|";
                            }

                            string row14 = pds.Rows[i][14].ToString();
                            if (!string.IsNullOrEmpty(row14) && !string.IsNullOrEmpty(row0))
                            {
                                if (!string.IsNullOrEmpty(row12))
                                {
                                    var teamGroup = teamGroups.Where(x => x.EDU_Unit == unit.UnitId && x.EDU_Code == row12.Trim()).FirstOrDefault();
                                    if (teamGroup != null)
                                    {
                                        var welder = welders.Where(x => x.WED_Unit == unit.UnitId && x.EDU_ID == teamGroup.EDU_ID && x.WED_Code == row14.Trim()).FirstOrDefault();
                                        if (welder == null)
                                        {
                                            result += (i + 2).ToString() + "," + "打底焊工代号" + "," + "该单位没有此焊工代号！" + "|";
                                        }
                                    }
                                }
                                else
                                {
                                    var welder = welders.Where(x => x.WED_Unit == unit.UnitId && x.WED_Code == row14.Trim()).FirstOrDefault();
                                    if (welder == null)
                                    {
                                        result += (i + 2).ToString() + "," + "打底焊工代号" + "," + "该单位没有此焊工代号！" + "|";
                                    }
                                }
                            }
                            else
                            {
                                result += (i + 2).ToString() + "," + "打底焊工代号" + "," + "此项为必填项！" + "|";
                            }

                            string row15 = pds.Rows[i][15].ToString().Trim();
                            if (!string.IsNullOrEmpty(row15))
                            {
                                if (row15 != "1G" && row15 != "2G" && row15 != "3G" && row15 != "4G" && row15 != "5G" && row15 != "6G")
                                {
                                    result += (i + 2).ToString() + "," + "焊接位置" + "," + "[" + row15 + "]错误！" + "|";
                                }
                            }
                            else
                            {
                                result += (i + 2).ToString() + "," + "焊接位置" + "," + "此项为必填项！" + "|";
                            }

                            string row16 = pds.Rows[i][16].ToString();
                            if (!string.IsNullOrEmpty(row16))
                            {
                                try
                                {
                                    DateTime date = Convert.ToDateTime(row16.Trim());
                                }
                                catch (Exception)
                                {
                                    result += (i + 2).ToString() + "," + "焊接日期" + "," + "[" + row16 + "]错误！" + "|";
                                }
                            }
                            else
                            {
                                result += (i + 2).ToString() + "," + "焊接日期" + "," + "此项为必填项！" + "|";
                            }
                        }
                        else
                        {
                            result += (i + 2).ToString() + "," + "焊口代号" + "," + "该焊口日报已存在！" + "|";
                        }
                    }
                    else
                    {
                        result += (i + 2).ToString() + "," + "焊口代号" + "," + "该焊口不存在！" + "|";
                    }
                    //a++;
                }

                if (!string.IsNullOrEmpty(result))
                {
                    result = result.Substring(0, result.LastIndexOf("|"));
                }
                errorInfos.Clear();
                if (!string.IsNullOrEmpty(result))
                {
                    string results = result;
                    List<string> errorInfoList = results.Split('|').ToList();
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
                        this.Table4.Visible = true;
                        this.gvErrorInfo.DataSource = errorInfos;
                        this.gvErrorInfo.DataBind();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('审核完成,请点击导入！')", true);
                }
            }
            else
            {
                throw new Exception("导入数据为空！");
            }
            return true;
        }

        #region 读Excel提取数据
        /// <summary>
        /// 从Excel提取数据--》Dataset
        /// </summary>
        /// <param name="filename">Excel文件路径名</param>
        private void ImportXlsToData2(string fileName)
        {
            try
            {
                string oleDBConnString = String.Empty;
                oleDBConnString = "Provider=Microsoft.Jet.OLEDB.4.0;";
                oleDBConnString += "Data Source=";
                oleDBConnString += fileName;
                oleDBConnString += ";Extended Properties=Excel 8.0;";
                OleDbConnection oleDBConn = null;
                OleDbDataAdapter oleAdMaster = null;
                DataTable m_tableName = new DataTable();
                DataSet ds = new DataSet();

                oleDBConn = new OleDbConnection(oleDBConnString);
                oleDBConn.Open();
                m_tableName = oleDBConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (m_tableName != null && m_tableName.Rows.Count > 0)
                {

                    m_tableName.TableName = m_tableName.Rows[0]["TABLE_NAME"].ToString().Trim();

                }
                string sqlMaster;
                sqlMaster = " SELECT *  FROM [" + m_tableName.TableName + "]";
                oleAdMaster = new OleDbDataAdapter(sqlMaster, oleDBConn);
                oleAdMaster.SelectCommand.CommandTimeout = 600;
                oleAdMaster.Fill(ds, "m_tableName");
                oleAdMaster.Dispose();
                oleDBConn.Close();
                oleDBConn.Dispose();

                AddDatasetToSQL2(ds.Tables[0], 16);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        /// <summary>
        /// 将Dataset的数据导入数据库
        /// </summary>
        /// <param name="pds">数据集</param>
        /// <param name="Cols">数据集列数</param>
        /// <returns></returns>
        private bool AddDatasetToSQL2(DataTable pds, int Cols)
        {
            string projectId = this.CurrUser.ProjectId;
            string result = string.Empty;
            string dReportID = string.Empty;
            string dateStr = string.Empty;
            int ic, ir;
            ic = pds.Columns.Count;
            if (ic < Cols)
            {
                throw new Exception("导入Excel格式错误！Excel只有" + ic.ToString().Trim() + "列");
            }

            ir = pds.Rows.Count;
            if (pds != null && ir > 0)
            {
                var oldViewInfos = from x in Funs.DB.View_JointInfoAndIsoInfo
                                   where x.ProjectId == projectId
                                   select x;

                var units = from x in Funs.DB.Base_Unit where x.ProjectId == projectId select x;
                var workAreas = from x in Funs.DB.Base_WorkArea where x.ProjectId == projectId select x;
                var steels = from x in Funs.DB.BS_Steel select x;
                var types = from x in Funs.DB.BS_JointType select x;
                var methods = from x in Funs.DB.BS_WeldMethod select x;
                var teamGroups = from x in Funs.DB.HS_Education where x.ProjectId == this.CurrUser.ProjectId select x;
                var welders = from x in Funs.DB.BS_Welder select x;
                Model.View_JointInfoAndIsoInfo oldViewInfo1 = new Model.View_JointInfoAndIsoInfo();
                oldViewInfo1 = oldViewInfos.Where(x => x.BAW_ID == this.drpWorkArea.SelectedValue
                                          && x.ISO_IsoNo == pds.Rows[0][2].ToString().Trim()
                                          && x.JOT_JointNo == pds.Rows[0][3].ToString().Trim()).FirstOrDefault();
                //Model.BO_WeldReportMain report = new Model.BO_WeldReportMain();
                //string isoId = oldViewInfo1.ISO_ID;
                //string bawId = BLL.PW_IsoInfoService.GetIsoInfoByIsoInfoId(isoId).BAW_ID;
                //string bawCode = BLL.WorkAreaService.getWorkAreaByWorkAreaId(bawId).WorkAreaCode;
                //string welder = pds.Rows[0][13].ToString().Trim();
                //string perfix = bawCode + "-" + welder + "-";
                //string reportNo = BLL.SQLHelper.RunProcNewId("SpGetNewCode", "dbo.BO_WeldReportMain", "JOT_DailyReportNo", this.CurrUser.ProjectId, perfix);
                //report.DReportID = SQLHelper.GetNewID(typeof(Model.BO_WeldReportMain));
                //report.ProjectId = this.CurrUser.ProjectId;
                //report.InstallationId = Funs.DB.Base_Installation.First(e => e.InstallationCode == pds.Rows[0][1].ToString().Trim()).InstallationId;
                //report.BSU_ID = Funs.DB.Base_Unit.First(e => e.UnitCode == pds.Rows[0][0].ToString().Trim()).UnitId;
                //report.JOT_WeldDate = Convert.ToDateTime(pds.Rows[0][16].ToString().Trim());
                //report.JOT_DailyReportNo = reportNo;
                //report.CHT_Tabler = this.CurrUser.UserName;
                //report.CHT_TableDate = DateTime.Now;
                //BLL.WeldReportService.AddWeldReport(report);
                //dReportID = report.DReportID;
                for (int i = 0; i < ir; i++)
                {
                    Model.View_JointInfoAndIsoInfo oldViewInfo = new Model.View_JointInfoAndIsoInfo();
                    oldViewInfo = oldViewInfos.Where(x => x.BAW_ID == this.drpWorkArea.SelectedValue
                                                       && x.ISO_IsoNo == pds.Rows[i][2].ToString().Trim()
                                                       && x.JOT_JointNo == pds.Rows[i][3].ToString().Trim()).FirstOrDefault();

                    if (oldViewInfo != null)
                    {
                        if (dateStr != pds.Rows[i][16].ToString().Trim())
                        {
                            Model.BO_WeldReportMain report = new Model.BO_WeldReportMain();
                            string isoId = oldViewInfo1.ISO_ID;
                            string bawId = BLL.PW_IsoInfoService.GetIsoInfoByIsoInfoId(isoId).BAW_ID;
                            string bawCode = BLL.WorkAreaService.getWorkAreaByWorkAreaId(bawId).WorkAreaCode;
                            string welder = pds.Rows[i][13].ToString().Trim();
                            string perfix = bawCode + "-" + welder + "-";
                            string reportNo = BLL.SQLHelper.RunProcNewId("SpGetNewCode", "dbo.BO_WeldReportMain", "JOT_DailyReportNo", this.CurrUser.ProjectId, perfix);
                            report.DReportID = SQLHelper.GetNewID(typeof(Model.BO_WeldReportMain));
                            report.ProjectId = this.CurrUser.ProjectId;
                            report.InstallationId = Funs.DB.Base_Installation.First(e => e.InstallationCode == pds.Rows[i][1].ToString().Trim()).InstallationId;
                            report.BSU_ID = Funs.DB.Base_Unit.First(e => e.UnitCode == pds.Rows[i][0].ToString().Trim()).UnitId;
                            report.JOT_WeldDate = Convert.ToDateTime(pds.Rows[i][16].ToString().Trim());
                            report.JOT_DailyReportNo = reportNo;
                            report.CHT_Tabler = this.CurrUser.UserName;
                            report.CHT_TableDate = DateTime.Now;
                            BLL.WeldReportService.AddWeldReport(report);
                            dReportID = report.DReportID;
                            dateStr = pds.Rows[i][16].ToString().Trim();
                        }

                        Model.PW_JointInfo jointInfo = BLL.PW_JointInfoService.GetJointInfoByJotID(oldViewInfo.JOT_ID);
                        jointInfo.WLO_Code = pds.Rows[i][6].ToString().Trim() == "安装" ? "F" : "S";
                        jointInfo.JOT_JointAttribute = pds.Rows[i][7].ToString().Trim();
                        if (!string.IsNullOrEmpty(pds.Rows[i][8].ToString().Trim()))
                        {
                            jointInfo.JOT_DoneDin = Convert.ToDecimal(pds.Rows[i][8].ToString().Trim());
                        }
                        if (!string.IsNullOrEmpty(pds.Rows[i][9].ToString().Trim()))
                        {
                            jointInfo.JOT_JointDesc = pds.Rows[i][9].ToString().Trim();
                        }
                        if (!string.IsNullOrEmpty(pds.Rows[i][10].ToString().Trim()))
                        {
                            jointInfo.JOT_Sch = pds.Rows[i][10].ToString().Trim();
                        }
                        if (!string.IsNullOrEmpty(pds.Rows[i][11].ToString().Trim()))
                        {
                            jointInfo.WME_ID = methods.First(e => e.WME_Code == pds.Rows[i][11].ToString().Trim()).WME_ID;
                        }
                        jointInfo.DReportID = dReportID;
                        var unit = units.Where(x => x.UnitCode == pds.Rows[i][0].ToString().Trim()).FirstOrDefault();
                        if (string.IsNullOrEmpty(pds.Rows[i][12].ToString().Trim()))
                        {
                            jointInfo.JOT_CellWelder = welders.First(e => e.WED_Unit == unit.UnitId && e.WED_Code == pds.Rows[i][13].ToString().Trim()).WED_ID;
                            jointInfo.JOT_FloorWelder = welders.First(e => e.WED_Unit == unit.UnitId && e.WED_Code == pds.Rows[i][14].ToString().Trim()).WED_ID;
                        }
                        else
                        {
                            var teamGroup = teamGroups.Where(x => x.EDU_Unit == unit.UnitId && x.EDU_Code == pds.Rows[i][12].ToString().Trim()).FirstOrDefault();
                            jointInfo.JOT_CellWelder = welders.First(e => e.WED_Unit == unit.UnitId && e.EDU_ID == teamGroup.EDU_ID && e.WED_Code == pds.Rows[i][13].ToString().Trim()).WED_ID;
                            jointInfo.JOT_FloorWelder = welders.First(e => e.WED_Unit == unit.UnitId && e.EDU_ID == teamGroup.EDU_ID && e.WED_Code == pds.Rows[i][14].ToString().Trim()).WED_ID;
                        }
                        jointInfo.JOT_Location = pds.Rows[i][15].ToString().Trim();
                        BLL.PW_JointInfoService.UpdateJointInfo(jointInfo);
                    }
                }
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('导入成功！')", true);
            }
            else
            {
                throw new Exception("导入数据为空！");
            }
            return true;
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

        protected void btnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("WeldReport.aspx");
        }
        #endregion
    }
}