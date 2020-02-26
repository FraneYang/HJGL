using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Threading;
using System.IO;
using System.Linq;
using BLL;
using System.Data.OleDb;
using System.Collections.Generic;

namespace Web.DataIn
{
    public partial class ProgressBarUpdateIn : PPage
    {
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        /// <summary>
        /// 焊口集合
        /// </summary>
        private List<Model.PW_JointInfo> jointInfos = new List<Model.PW_JointInfo>();

        /// <summary>
        /// 更新焊口集合
        /// </summary>
       // private List<Model.PW_JointInfo> jointInfosUpdate = new List<Model.PW_JointInfo>();

        /// <summary>
        /// 管线集合
        /// </summary>
        private List<Model.PW_IsoInfo> isoInfos = new List<Model.PW_IsoInfo>();

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["jointInfos"] = null;
            Session["isoInfos"] = null;
            //Session["jointInfosUpdate"] = null;
            string rootPath = Server.MapPath("~/");
            string fileName = rootPath + initPath + Request.Params["fileName"];
            this.hdWorkArea.Value = Request.Params["workAreaId"];
            ImportXlsToData(fileName);

        }

        #region 进度条
        private void beginProgress()
        {
            //根据ProgressBar.htm显示进度条界面
            string templateFileName = Path.Combine(Server.MapPath("."), "ProgressBarUpdateIn.aspx");
            StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("GB2312"));
            string html = reader.ReadToEnd();
            reader.Close();
            Response.Write(html);
            Response.Flush();
        }

        private void setProgress(int percent)
        {
            string jsBlock = "<script>SetPorgressBar('" + percent.ToString() + "'); </script>";
            Response.Write(jsBlock);
            Response.Flush();
        }

        private void finishProgress(string result)
        {
            string jsBlock = "<script>SetCompleted('" + result + "');</script>";
            Response.Write(jsBlock);
            Response.Flush();
        }
        #endregion

        #region Excel提取数据
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

                AddDatasetToSQL(ds.Tables[0], 34);
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
            beginProgress();
            //int a = 0;
            string projectId = this.CurrUser.ProjectId;
            int ic, ir;
            jointInfos.Clear();
            isoInfos.Clear();
            ic = pds.Columns.Count;
            if (ic < Cols)
            {
                throw new Exception("导入Excel格式错误！Excel只有" + ic.ToString().Trim() + "列");
            }

            ir = pds.Rows.Count;
            if (pds != null && ir > 0)
            {
                var units = from x in Funs.DB.Base_Unit where x.ProjectId == projectId select x;
                //var workAreas = from x in Funs.DB.Base_WorkArea where x.ProjectId == projectId select x;
                var steels = from x in Funs.DB.BS_Steel select x;
                var rates = from x in Funs.DB.BS_NDTRate select x;
                var types = from x in Funs.DB.BS_JointType select x;
                var methods = from x in Funs.DB.BS_WeldMethod select x;
                var materials = from x in Funs.DB.BS_WeldMaterial select x;
                var services = from x in Funs.DB.BS_Service select x;
                var slopeTypes = from x in Funs.DB.BS_SlopeType select x;
                var isoClasss = from x in Funs.DB.BS_IsoClass select x;
                var components = from x in Funs.DB.BS_Component select x;

                for (int i = 0; i < ir; i++)
                {
                    if (i % (ir / 100 + 1) == 0 && i < ir && i > 0)
                    {
                        setProgress(i / (ir / 100 + 1));

                        //此处用线程休眠代替实际的操作，如加载数据等
                        //System.Threading.Thread.Sleep(50);
                    }

                    Model.PW_JointInfo jointInfo = new Model.PW_JointInfo();
                    Model.PW_IsoInfo isoInfo = new Model.PW_IsoInfo();
                    string row0 = pds.Rows[i][0].ToString().Trim();
                    string row2 = pds.Rows[i][2].ToString().Trim();
                    string row3 = pds.Rows[i][3].ToString().Trim();
                    string row4 = pds.Rows[i][4].ToString();
                    string row5 = pds.Rows[i][5].ToString();
                    string row6 = pds.Rows[i][6].ToString();
                    string row7 = pds.Rows[i][7].ToString();
                    string row8 = pds.Rows[i][8].ToString();
                    string row9 = pds.Rows[i][9].ToString();
                    string row10 = pds.Rows[i][10].ToString();
                    string row11 = pds.Rows[i][11].ToString();
                    string row12 = pds.Rows[i][12].ToString();
                    string row13 = pds.Rows[i][13].ToString();
                    string row14 = pds.Rows[i][14].ToString();
                    string row15 = pds.Rows[i][15].ToString();
                    string row16 = pds.Rows[i][16].ToString();
                    string row17 = pds.Rows[i][17].ToString();
                    string row18 = pds.Rows[i][18].ToString();
                    string row19 = pds.Rows[i][19].ToString();
                    string row20 = pds.Rows[i][20].ToString();
                    string row21 = pds.Rows[i][21].ToString();
                    string row22 = pds.Rows[i][22].ToString();
                    string row23 = pds.Rows[i][23].ToString();
                    string row24 = pds.Rows[i][24].ToString();
                    string row25 = pds.Rows[i][25].ToString();
                    string row26 = pds.Rows[i][26].ToString();
                    string row27 = pds.Rows[i][27].ToString();
                    string row28 = pds.Rows[i][28].ToString();
                    string row29 = pds.Rows[i][29].ToString();
                    string row30 = pds.Rows[i][30].ToString();
                    string row31 = pds.Rows[i][31].ToString();
                    string row32 = pds.Rows[i][32].ToString();
                    string row33 = pds.Rows[i][33].ToString();

                    //var oldViewInfos = from x in Funs.DB.View_JointInfoAndIsoInfo
                    //               where x.ProjectId == projectId
                    //               select x;
                    //Model.View_JointInfoAndIsoInfo oldViewInfo = new Model.View_JointInfoAndIsoInfo();
                    //oldViewInfo = oldViewInfos.Where(x => x.BAW_ID == this.hdWorkArea.Value
                    //                                   && x.ISO_IsoNo == pds.Rows[i][2].ToString().Trim()
                    //                                   && x.JOT_JointNo == pds.Rows[i][3].ToString().Trim()).FirstOrDefault();
                    //if (oldViewInfo == null)
                    //{
                        isoInfo.BAW_ID = this.hdWorkArea.Value;
                        isoInfo.BSU_ID = units.Where(x => x.UnitCode == row0).FirstOrDefault().UnitId;
                        isoInfo.ISO_IsoNo = row2;
                        if (!string.IsNullOrEmpty(row4))
                        {
                            isoInfo.STE_ID = steels.Where(x => x.STE_Code == row4.Trim()).FirstOrDefault().STE_ID;
                        }
                        if (!string.IsNullOrEmpty(row6))
                        {
                            isoInfo.NDTR_ID = rates.Where(x => x.NDTR_Code == row6.Trim()).FirstOrDefault().NDTR_ID;
                        }
                        if (!string.IsNullOrEmpty(row11))
                        {
                            isoInfo.ISO_Specification = row11.Trim();
                        }
                        if (!string.IsNullOrEmpty(row14))
                        {
                            isoInfo.ISO_TestPress = Convert.ToDecimal(row14.Trim());
                        }
                        if (!string.IsNullOrEmpty(row17))
                        {
                            isoInfo.SER_ID = services.Where(x => x.SER_Code == row17.Trim()).FirstOrDefault().SER_ID;
                        }
                        if (!string.IsNullOrEmpty(row18))
                        {
                            isoInfo.ISO_IsoNumber = row18.Trim();
                        }
                        if (!string.IsNullOrEmpty(row19))
                        {
                            isoInfo.ISO_DesignPress = Convert.ToDecimal(row19.Trim());
                        }
                        if (!string.IsNullOrEmpty(row20))
                        {
                            isoInfo.ISO_DesignTemperature = Convert.ToDecimal(row20.Trim());
                        }
                        if (!string.IsNullOrEmpty(row22))
                        {
                            isoInfo.ISC_ID = isoClasss.Where(x => x.ISC_IsoCode == row22.Trim()).FirstOrDefault().ISC_ID;
                        }
                        if (!string.IsNullOrEmpty(row33))
                        {
                            isoInfo.ISO_HardnessRate = Convert.ToDecimal(row33.Trim()) / Convert.ToDecimal(1.0 * 100);
                        }
                        if (isoInfos.Where(e => e.ISO_IsoNo == row2).FirstOrDefault() == null)
                        {
                            isoInfos.Add(isoInfo);
                        }


                        jointInfo.ISO_ID = row2;
                        jointInfo.JOT_JointNo = row3;
                        if (!string.IsNullOrEmpty(row4))
                        {
                            jointInfo.STE_ID = isoInfo.STE_ID;
                        }
                        if (!string.IsNullOrEmpty(row5))
                        {
                            jointInfo.STE_ID2 = steels.Where(x => x.STE_Code == row5.Trim()).FirstOrDefault().STE_ID;
                        }
                        if (!string.IsNullOrEmpty(row7))
                        {
                            jointInfo.JOTY_ID = types.Where(x => x.JOTY_Code == row7.Trim()).FirstOrDefault().JOTY_ID;
                        }
                        if (!string.IsNullOrEmpty(row8))
                        {
                            if (row8.Trim() == "安装")
                            {
                                jointInfo.WLO_Code = "F";
                            }
                            else
                            {
                                jointInfo.WLO_Code = "S";
                            }
                        }
                        if (!string.IsNullOrEmpty(row9))
                        {
                            jointInfo.JOT_JointAttribute = row9.Trim();
                        }
                        if (!string.IsNullOrEmpty(row10))
                        {
                            jointInfo.JOT_Size = Convert.ToDecimal(row10.Trim());
                        }
                        if (!string.IsNullOrEmpty(row11))
                        {
                            jointInfo.JOT_JointDesc = row11.Trim();
                        }
                        if (!string.IsNullOrEmpty(row12))
                        {
                            jointInfo.JOT_Sch = row12.Trim();
                        }
                        if (!string.IsNullOrEmpty(row13))
                        {
                            jointInfo.WME_ID = methods.Where(x => x.WME_Code == row13.Trim()).FirstOrDefault().WME_ID;
                        }
                        if (!string.IsNullOrEmpty(row15))
                        {
                            jointInfo.JOT_WeldMat = materials.Where(x => x.WMT_MatCode == row15.Trim() && x.WMT_MatType == "2").FirstOrDefault().WMT_ID;
                        }
                        if (!string.IsNullOrEmpty(row16))
                        {
                            jointInfo.JOT_WeldSilk = materials.Where(x => x.WMT_MatCode == row16.Trim() && x.WMT_MatType == "1").FirstOrDefault().WMT_ID;
                        }
                        if (!string.IsNullOrEmpty(row21))
                        {
                            jointInfo.JST_ID = slopeTypes.Where(x => x.JST_Code == row21.Trim()).FirstOrDefault().JST_ID;
                        }
                        if (!string.IsNullOrEmpty(row23))
                        {
                            jointInfo.JOT_Component1 = components.Where(x => x.COM_Code == row23.Trim()).FirstOrDefault().COM_ID;
                        }
                        if (!string.IsNullOrEmpty(row24))
                        {
                            jointInfo.JOT_Component2 = components.Where(x => x.COM_Code == row24.Trim()).FirstOrDefault().COM_ID;
                        }
                        if (!string.IsNullOrEmpty(row25))
                        {
                            jointInfo.JOT_HeartNo1 = row25.Trim();
                        }
                        if (!string.IsNullOrEmpty(row26))
                        {
                            jointInfo.JOT_HeartNo2 = row26.Trim();
                        }
                        if (!string.IsNullOrEmpty(row27))
                        {
                            jointInfo.JOT_BelongPipe = row27.Trim();
                        }
                        if (!string.IsNullOrEmpty(row28))
                        {
                            jointInfo.JOT_PrepareTemp = Convert.ToDecimal(row28.Trim());
                        }

                        if (!string.IsNullOrEmpty(row29))
                        {
                            if (row29.Trim() == "是")
                            {
                                jointInfo.IS_Proess = "1";
                            }
                            else
                            {
                                jointInfo.IS_Proess = "0";
                            }
                        }

                        if (!string.IsNullOrEmpty(row30))
                        {
                            jointInfo.JOT_HotRpt = row30.Trim();
                        }
                        if (!string.IsNullOrEmpty(row31))
                        {
                            jointInfo.JOT_Location = row31.Trim();
                        }
                        if (!string.IsNullOrEmpty(row32))
                        {
                            jointInfo.JOT_Dia = Convert.ToDecimal(row32.Trim());
                        }
                        if (jointInfos.Where(e => e.ISO_ID == row2 && e.JOT_JointNo == row3).FirstOrDefault() == null)
                        {
                            jointInfos.Add(jointInfo);
                        }
                    //}

                    //else if (oldViewInfo != null && string.IsNullOrEmpty(oldViewInfo.DReportID))
                    //{
                    //    jointInfo.JOT_ID = oldViewInfo.JOT_ID;
                    //    jointInfo.ISO_ID = row2;
                    //    jointInfo.JOT_JointNo = row3;
                    //    if (!string.IsNullOrEmpty(row4))
                    //    {
                    //        jointInfo.STE_ID = steels.Where(x => x.STE_Code == row4.Trim()).FirstOrDefault().STE_ID;
                    //    }
                    //    if (!string.IsNullOrEmpty(row5))
                    //    {
                    //        jointInfo.STE_ID2 = steels.Where(x => x.STE_Code == row5.Trim()).FirstOrDefault().STE_ID;
                    //    }
                    //    if (!string.IsNullOrEmpty(row7))
                    //    {
                    //        jointInfo.JOTY_ID = types.Where(x => x.JOTY_Code == row7.Trim()).FirstOrDefault().JOTY_ID;
                    //    }
                    //    if (!string.IsNullOrEmpty(row8))
                    //    {
                    //        if (row8.Trim() == "安装")
                    //        {
                    //            jointInfo.WLO_Code = "F";
                    //        }
                    //        else
                    //        {
                    //            jointInfo.WLO_Code = "S";
                    //        }
                    //    }
                    //    if (!string.IsNullOrEmpty(row9))
                    //    {
                    //        jointInfo.JOT_JointAttribute = row9.Trim();
                    //    }
                    //    if (!string.IsNullOrEmpty(row10))
                    //    {
                    //        jointInfo.JOT_Size = Convert.ToDecimal(row10.Trim());
                    //    }
                    //    if (!string.IsNullOrEmpty(row11))
                    //    {
                    //        jointInfo.JOT_JointDesc = row11.Trim();
                    //    }
                    //    if (!string.IsNullOrEmpty(row12))
                    //    {
                    //        jointInfo.JOT_Sch = row12.Trim();
                    //    }
                    //    if (!string.IsNullOrEmpty(row13))
                    //    {
                    //        jointInfo.WME_ID = methods.Where(x => x.WME_Code == row13.Trim()).FirstOrDefault().WME_ID;
                    //    }
                    //    if (!string.IsNullOrEmpty(row15))
                    //    {
                    //        jointInfo.JOT_WeldMat = materials.Where(x => x.WMT_MatCode == row15.Trim()).FirstOrDefault().WMT_ID;
                    //    }
                    //    if (!string.IsNullOrEmpty(row16))
                    //    {
                    //        jointInfo.JOT_WeldSilk = materials.Where(x => x.WMT_MatCode == row16.Trim()).FirstOrDefault().WMT_ID;
                    //    }
                    //    if (!string.IsNullOrEmpty(row21))
                    //    {
                    //        jointInfo.JST_ID = slopeTypes.Where(x => x.JST_Code == row21.Trim()).FirstOrDefault().JST_ID;
                    //    }
                    //    if (!string.IsNullOrEmpty(row23))
                    //    {
                    //        jointInfo.JOT_Component1 = components.Where(x => x.COM_Code == row23.Trim()).FirstOrDefault().COM_ID;
                    //    }
                    //    if (!string.IsNullOrEmpty(row24))
                    //    {
                    //        jointInfo.JOT_Component2 = components.Where(x => x.COM_Code == row24.Trim()).FirstOrDefault().COM_ID;
                    //    }
                    //    if (!string.IsNullOrEmpty(row25))
                    //    {
                    //        jointInfo.JOT_HeartNo1 = row25.Trim();
                    //    }
                    //    if (!string.IsNullOrEmpty(row26))
                    //    {
                    //        jointInfo.JOT_HeartNo2 = row26.Trim();
                    //    }
                    //    if (!string.IsNullOrEmpty(row27))
                    //    {
                    //        jointInfo.JOT_BelongPipe = row27.Trim();
                    //    }
                    //    if (!string.IsNullOrEmpty(row28))
                    //    {
                    //        jointInfo.JOT_PrepareTemp = Convert.ToDecimal(row28.Trim());
                    //    }

                    //    if (!string.IsNullOrEmpty(row29))
                    //    {
                    //        if (row29.Trim() == "是")
                    //        {
                    //            jointInfo.IS_Proess = "1";
                    //        }
                    //        else
                    //        {
                    //            jointInfo.IS_Proess = "0";
                    //        }
                    //    }

                    //    if (!string.IsNullOrEmpty(row30))
                    //    {
                    //        jointInfo.JOT_HotRpt = row30.Trim();
                    //    }
                    //    if (!string.IsNullOrEmpty(row31))
                    //    {
                    //        jointInfo.JOT_Location = row31.Trim();
                    //    }
                    //    if (!string.IsNullOrEmpty(row32))
                    //    {
                    //        jointInfo.JOT_Dia = Convert.ToDecimal(row32.Trim());
                    //    }
                        
                    //    jointInfosUpdate.Add(jointInfo);
                    //}
                }
                Session["jointInfos"] = jointInfos;
                Session["isoInfos"] = isoInfos;
                //Session["jointInfosUpdate"] = jointInfosUpdate;
                finishProgress("OK");
            }
            else
            {
                throw new Exception("导入数据为空！");
            }
            return true;
        }
    }
}