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
    public partial class ProgressBar : PPage
    {
        /// <summary>
        /// 上传预设的虚拟路径
        /// </summary>
        private string initPath = Const.ExcelUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            string rootPath = Server.MapPath("~/");
            string fileName = rootPath + initPath + Request.Params["fileName"];
            this.hdWorkArea.Value = Request.Params["workAreaId"];
            ImportXlsToData(fileName);
            //if (fileName != string.Empty && System.IO.File.Exists(fileName))
            //{
            //    System.IO.File.Delete(fileName);//删除上传的XLS文件
            //}
        }

        #region 进度条
        private void beginProgress()
        {
            //根据ProgressBar.htm显示进度条界面
            string templateFileName = Path.Combine(Server.MapPath("."), "ProgressBar.aspx");
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
                var workAreas = from x in Funs.DB.Base_WorkArea where x.ProjectId == projectId select x;
                var steels = from x in Funs.DB.BS_Steel select x;
                var rates = from x in Funs.DB.BS_NDTRate select x;
                var types = from x in Funs.DB.BS_JointType select x;
                var methods = from x in Funs.DB.BS_WeldMethod  select x;
                var materials = from x in Funs.DB.BS_WeldMaterial select x;
                var services = from x in Funs.DB.BS_Service select x;
                var slopeTypes = from x in Funs.DB.BS_SlopeType  select x;
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

                    Model.View_JointInfoAndIsoInfo oldViewInfo = new Model.View_JointInfoAndIsoInfo();
                    oldViewInfo = oldViewInfos.Where(x => x.BAW_ID == this.hdWorkArea.Value
                                                       && x.ISO_IsoNo == pds.Rows[i][2].ToString().Trim()
                                                       && x.JOT_JointNo == pds.Rows[i][3].ToString().Trim()).FirstOrDefault();

                    if (oldViewInfo == null)
                    {
                        string row0 = pds.Rows[i][0].ToString();
                        if (!string.IsNullOrEmpty(row0))
                        {
                            if (units.Where(x => x.UnitCode == row0.Trim()).FirstOrDefault() == null)
                            {
                                result += (i + 2).ToString() + "," + "单位代码" + "," + "[" + row0 + "]不存在！" + "|";
                            }
                        }
                        else
                        {
                            result += (i + 2).ToString() + "," + "单位代码" + "," + "此项为必填项！" + "|";
                        }

                        string row1 = pds.Rows[i][1].ToString();
                        if (!string.IsNullOrEmpty(row1))
                        {
                            var workArea = workAreas.Where(x => x.WorkAreaCode == row1.Trim()).FirstOrDefault();
                            if (workArea == null)
                            {
                                result += (i + 2).ToString() + "," + "装置工区编号" + "," + "[" + row1 + "]错误！" + "|";
                            }
                            else
                            {
                                if (workArea.WorkAreaId != this.hdWorkArea.Value)
                                {
                                    result += (i + 2).ToString() + "," + "装置工区编号" + "," + "[" + row1 + "]错误！" + "|";
                                }
                            }
                        }
                        else
                        {
                            result += (i + 2).ToString() + "," + "装置工区编号" + "," + "此项为必填项！" + "|";
                        }

                        if (!string.IsNullOrEmpty(row0))
                        {
                            var unit = units.Where(x => x.UnitCode == row0.Trim()).FirstOrDefault();
                            var area = workAreas.Where(x => x.UnitId == unit.UnitId && x.WorkAreaCode == row1.Trim()).FirstOrDefault();
                            if (area == null)
                            {
                                result += (i + 2).ToString() + "," + "装置工区编号" + "," + "该单位没有此工区号！" + "|";
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
                                result += (i + 2).ToString() + "," + "材质1" + "," + "[" + row4 + "]错误！" + "|";
                            }
                        }

                        string row5 = pds.Rows[i][5].ToString();
                        if (!string.IsNullOrEmpty(row5))
                        {
                            if (steels.Where(x => x.STE_Code == row5.Trim()).FirstOrDefault() == null)
                            {
                                result += (i + 2).ToString() + "," + "材质2" + "," + "[" + row5 + "]错误！" + "|";
                            }
                        }

                        string row6 = pds.Rows[i][6].ToString();
                        if (!string.IsNullOrEmpty(row6))
                        {
                            if (rates.Where(x => x.NDTR_Code == row6.Trim()).FirstOrDefault() == null)
                            {
                                result += (i + 2).ToString() + "," + "探伤比例" + "," + "[" + row6 + "]错误！" + "|";
                            }
                        }

                        string row7 = pds.Rows[i][7].ToString();
                        if (!string.IsNullOrEmpty(row7))
                        {
                            if (types.Where(x => x.JOTY_Code == row7.Trim()).FirstOrDefault() == null)
                            {
                                result += (i + 2).ToString() + "," + "焊缝类型" + "," + "[" + row7 + "]错误！" + "|";
                            }
                        }

                        string row8 = pds.Rows[i][8].ToString();
                        if (!string.IsNullOrEmpty(row8))
                        {
                            if (row8.Trim() != "安装" && row8.Trim() != "预制")
                            {
                                result += (i + 2).ToString() + "," + "焊接区域" + "," + "[" + row8 + "]错误！" + "|";
                            }
                        }
                        else
                        {
                            result += (i + 2).ToString() + "," + "焊接区域" + "," + "此项为必填项！" + "|";
                        }

                        string row9 = pds.Rows[i][9].ToString();
                        if (!string.IsNullOrEmpty(row9))
                        {
                            if (row9.Trim() != "固定" && row9.Trim() != "活动")
                            {
                                result += (i + 2).ToString() + "," + "焊口属性" + "," + "[" + row9 + "]错误！" + "|";
                            }
                        }
                        else
                        {
                            result += (i + 2).ToString() + "," + "焊口属性" + "," + "此项为必填项！" + "|";
                        }

                        string row10 = pds.Rows[i][10].ToString();
                        if (!string.IsNullOrEmpty(row10))
                        {
                            try
                            {
                                decimal doneDin = Convert.ToDecimal(row10.Trim());
                            }
                            catch (Exception)
                            {
                                result += (i + 2).ToString() + "," + "达因数" + "," + "[" + row10 + "]错误！" + "|";
                            }
                        }
                        else
                        {
                            result += (i + 2).ToString() + "," + "达因数" + "," + "此项为必填项！" + "|";
                        }

                        string row13 = pds.Rows[i][13].ToString();
                        if (!string.IsNullOrEmpty(row13))
                        {
                            if (methods.Where(x => x.WME_Code == row13.Trim()).FirstOrDefault() == null)
                            {
                                result += (i + 2).ToString() + "," + "焊接方法" + "," + "[" + row13 + "]错误！" + "|";
                            }
                        }

                        string row14 = pds.Rows[i][14].ToString();
                        if (!string.IsNullOrEmpty(row14))
                        {
                            try
                            {
                                decimal testPress = Convert.ToDecimal(row14.Trim());
                            }
                            catch (Exception)
                            {
                                result += (i + 2).ToString() + "," + "试验压力" + "," + "[" + row14 + "]错误！" + "|";
                            }
                        }

                        string row15 = pds.Rows[i][15].ToString();
                        if (!string.IsNullOrEmpty(row15))
                        {
                            if (materials.Where(x => x.WMT_MatCode == row15.Trim() && x.WMT_MatType == "2").FirstOrDefault() == null)
                            {
                                result += (i + 2).ToString() + "," + "焊条代号" + "," + "[" + row15 + "]错误！" + "|";
                            }
                        }

                        string row16 = pds.Rows[i][16].ToString();
                        if (!string.IsNullOrEmpty(row16))
                        {
                            if (materials.Count() == 0 || (materials.Where(x => x.WMT_MatCode == row16.Trim() && x.WMT_MatType == "1").FirstOrDefault() == null))
                            {
                                result += (i + 2).ToString() + "," + "焊丝代号" + "," + "[" + row16 + "]错误！" + "|";
                            }
                        }

                        string row17 = pds.Rows[i][17].ToString();
                        if (!string.IsNullOrEmpty(row17))
                        {
                            if (services.Where(x => x.SER_Code == row17.Trim()).FirstOrDefault() == null)
                            {
                                result += (i + 2).ToString() + "," + "介质" + "," + "[" + row17 + "]错误！" + "|";
                            }
                        }

                        string row19 = pds.Rows[i][19].ToString();
                        if (!string.IsNullOrEmpty(row19))
                        {
                            try
                            {
                                decimal degPress = Convert.ToDecimal(row19.Trim());
                            }
                            catch (Exception)
                            {
                                result += (i + 2).ToString() + "," + "设计压力" + "," + "[" + row19 + "]错误！" + "|";
                            }
                        }

                        string row20 = pds.Rows[i][20].ToString();
                        if (!string.IsNullOrEmpty(row20))
                        {
                            try
                            {
                                decimal deg = Convert.ToDecimal(row20.Trim());
                            }
                            catch (Exception)
                            {
                                result += (i + 2).ToString() + "," + "设计温度" + "," + "[" + row20 + "]错误！" + "|";
                            }
                        }

                        string row21 = pds.Rows[i][21].ToString();
                        if (!string.IsNullOrEmpty(row21))
                        {
                            if (slopeTypes.Where(x => x.JST_Code == row21.Trim()).FirstOrDefault() == null)
                            {
                                result += (i + 2).ToString() + "," + "坡口类型" + "," + "[" + row21 + "]错误！" + "|";
                            }
                        }

                        string row22 = pds.Rows[i][22].ToString();
                        if (!string.IsNullOrEmpty(row22))
                        {
                            if (isoClasss.Where(x => x.ISC_IsoCode == row22.Trim()).FirstOrDefault() == null)
                            {
                                result += (i + 2).ToString() + "," + "管线等级" + "," + "[" + row22 + "]错误！" + "|";
                            }
                        }

                        string row23 = pds.Rows[i][23].ToString();
                        if (!string.IsNullOrEmpty(row23))
                        {
                            if (components.Where(x => x.COM_Code == row23.Trim()).FirstOrDefault() == null)
                            {
                                result += (i + 2).ToString() + "," + "组件一号" + "," + "[" + row23 + "]错误！" + "|";
                            }
                        }

                        string row24 = pds.Rows[i][24].ToString();
                        if (!string.IsNullOrEmpty(row24))
                        {
                            if (components.Where(x => x.COM_Code == row24.Trim()).FirstOrDefault() == null)
                            {
                                result += (i + 2).ToString() + "," + "组件二号" + "," + "[" + row24 + "]错误！" + "|";
                            }
                        }

                        string row28 = pds.Rows[i][28].ToString();
                        if (!string.IsNullOrEmpty(row28))
                        {
                            try
                            {
                                decimal prepareTemp = Convert.ToDecimal(row28.Trim());
                            }
                            catch (Exception)
                            {
                                result += (i + 2).ToString() + "," + "预热温度" + "," + "[" + row28 + "]错误！" + "|";
                            }
                        }

                        string row29 = pds.Rows[i][29].ToString();
                        if (!string.IsNullOrEmpty(row29))
                        {
                            if (row29.Trim() != "是" && row29.Trim() != "否")
                            {
                                result += (i + 2).ToString() + "," + "是否热处理" + "," + "[" + row29 + "]错误！" + "|";
                            }
                        }
                        

                        string row31 = pds.Rows[i][31].ToString();
                        if (!string.IsNullOrEmpty(row31))
                        {
                            string l = row31.Trim();
                            if (l != "1G" && l != "2G" && l != "3G" && l != "4G" && l != "5G" && l != "6G")
                            {
                                result += (i + 2).ToString() + "," + "焊接位置" + "," + "[" + row31 + "]错误！" + "|";
                            }
                        }

                        string row32 = pds.Rows[i][32].ToString();
                        if (!string.IsNullOrEmpty(row32))
                        {
                            try
                            {
                                decimal testPress = Convert.ToDecimal(row32.Trim());
                            }
                            catch (Exception)
                            {
                                result += (i + 2).ToString() + "," + "外径" + "," + "[" + row32 + "]错误！" + "|";
                            }
                        }

                        string row33 = pds.Rows[i][33].ToString();
                        if (!string.IsNullOrEmpty(row33))
                        {
                            try
                            {
                                decimal hardnessRate = Convert.ToDecimal(row33.Trim());
                            }
                            catch (Exception)
                            {
                                result += (i + 2).ToString() + "," + "硬度检测比例" + "," + "[" + row33 + "]错误！" + "|";
                            }
                        }
                    }
                    else
                    {
                        result += (i + 2).ToString() + "," + "焊口代号" + "," + "该焊口已存在！" + "|";
                    }
                    //a++;
                }

                if (!string.IsNullOrEmpty(result))
                {
                    result = result.Substring(0, result.LastIndexOf("|"));
                }
                finishProgress(result);
            }
            else
            {
                throw new Exception("导入数据为空！");
            }
            return true;
        }
    }
}