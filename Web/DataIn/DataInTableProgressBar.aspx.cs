using BLL;
using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web.UI;

namespace Web.DataIn
{
    public partial class DataInTableProgressBar : PPage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string type = Request.Params["type"];    ///进度条0 导入，1保存
            string fileName = Request.Params["fileName"];
            if (type == "0")
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    string name = Server.MapPath("~/") + BLL.Const.ExcelUrl + fileName;
                    this.ImportXlsToData(name);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('导入文件不存在！')", true);
                    return;
                }
            }
            else
            {
                this.AuditData();
            }
        }

        #region 进度条
        /// <summary>
        /// 进度条开始
        /// </summary>
        private void beginProgress()
        {
            //根据ProgressBar.htm显示进度条界面
            string templateFileName = Path.Combine(Server.MapPath("."), "DataInTableProgressBar.aspx");
            StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("GB2312"));
            string html = reader.ReadToEnd();
            reader.Close();
            Response.Write(html);
            Response.Flush();
        }

        /// <summary>
        /// 进度条设置
        /// </summary>
        /// <param name="percent"></param>
        private void setProgress(int percent)
        {
            string jsBlock = "<script>SetPorgressBar('" + percent.ToString() + "'); </script>";
            Response.Write(jsBlock);
            Response.Flush();
        }

        /// <summary>
        /// 进度条完成
        /// </summary>
        /// <param name="result"></param>
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
                AddDatasetToSQL(ds.Tables[0]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!string.IsNullOrEmpty(fileName) && System.IO.File.Exists(fileName))
                {
                    File.Delete(fileName);//删除上传的XLS文件
                }
            }
        }
        #endregion

        #region 将Dataset的数据导入数据库
        /// <summary>
        /// 将Dataset的数据导入数据库
        /// </summary>
        /// <param name="pds">数据集</param>
        /// <param name="Cols">数据集列数</param>
        /// <returns></returns>
        private void AddDatasetToSQL(DataTable pds)
        {
            this.beginProgress();
            int ir = pds.Rows.Count;
            if (pds != null && ir > 0)
            {
                ////先删除临时表中 该人员以前导入的数据
                //  BLL.DataInTableService.DeleteDataInTempByProjectIdUserId(projectId, this.CurrUser.UserId);
                for (int i = 0; i < ir; i++)
                {
                    if (i % (ir / 100 + 1) == 0 && i < ir && i > 0)
                    {
                        setProgress(i / (ir / 100 + 1));
                        //此处用线程休眠代替实际的操作，如加载数据等
                        //System.Threading.Thread.Sleep(50);
                    }

                    if (!string.IsNullOrEmpty(pds.Rows[i][3].ToString()))
                    {
                        Model.Sys_DataInTemp newDataInTemp = new Model.Sys_DataInTemp();
                        newDataInTemp.TempId = SQLHelper.GetNewID(typeof(Model.Sys_DataInTemp));
                        newDataInTemp.ProjectId = this.CurrUser.ProjectId;
                        newDataInTemp.UserId = this.CurrUser.UserId;
                        newDataInTemp.Time = DateTime.Now;
                        newDataInTemp.Value1 = pds.Rows[i][0].ToString().Trim();
                        newDataInTemp.Value2 = pds.Rows[i][1].ToString().Trim();
                        newDataInTemp.Value3 = pds.Rows[i][2].ToString().Trim();
                        newDataInTemp.Value4 = pds.Rows[i][3].ToString().Trim();
                        newDataInTemp.Value5 = pds.Rows[i][4].ToString().Trim();
                        newDataInTemp.Value6 = pds.Rows[i][5].ToString().Trim();
                        newDataInTemp.Value7 = pds.Rows[i][6].ToString().Trim();
                        newDataInTemp.Value8 = pds.Rows[i][7].ToString().Trim();
                        newDataInTemp.Value9 = pds.Rows[i][8].ToString().Trim();
                        newDataInTemp.Value10 = pds.Rows[i][9].ToString().Trim();
                        newDataInTemp.Value11 = pds.Rows[i][10].ToString().Trim();
                        newDataInTemp.Value12 = pds.Rows[i][11].ToString().Trim();
                        newDataInTemp.Value13 = pds.Rows[i][12].ToString().Trim();
                        newDataInTemp.Value14 = pds.Rows[i][13].ToString().Trim();
                        newDataInTemp.Value15 = pds.Rows[i][14].ToString().Trim();
                        newDataInTemp.Value16 = pds.Rows[i][15].ToString().Trim();
                        newDataInTemp.Value17 = pds.Rows[i][16].ToString().Trim();
                        newDataInTemp.Value18 = pds.Rows[i][17].ToString().Trim();
                        newDataInTemp.Value19 = pds.Rows[i][18].ToString().Trim();
                        newDataInTemp.Value20 = pds.Rows[i][19].ToString().Trim();
                        newDataInTemp.Value21 = pds.Rows[i][20].ToString().Trim();
                        newDataInTemp.Value22 = pds.Rows[i][21].ToString().Trim();
                        newDataInTemp.Value23 = pds.Rows[i][22].ToString().Trim();
                        newDataInTemp.Value24 = pds.Rows[i][23].ToString().Trim();
                        newDataInTemp.Value25 = pds.Rows[i][24].ToString().Trim();
                        newDataInTemp.Value26 = pds.Rows[i][25].ToString().Trim();
                        newDataInTemp.Value27 = pds.Rows[i][26].ToString().Trim();
                        newDataInTemp.Value28 = pds.Rows[i][27].ToString().Trim();
                        newDataInTemp.Value29 = pds.Rows[i][28].ToString().Trim();
                        newDataInTemp.Value30 = pds.Rows[i][29].ToString().Trim();
                        newDataInTemp.Value31 = pds.Rows[i][30].ToString().Trim();
                        newDataInTemp.Value32 = pds.Rows[i][31].ToString().Trim();
                        newDataInTemp.Value33 = pds.Rows[i][32].ToString().Trim();
                        newDataInTemp.Value34 = pds.Rows[i][33].ToString().Trim();
                        newDataInTemp.Value35 = pds.Rows[i][34].ToString().Trim();
                        newDataInTemp.Value36 = pds.Rows[i][35].ToString().Trim();
                        newDataInTemp.Value37 = pds.Rows[i][36].ToString().Trim();
                        newDataInTemp.Value38 = pds.Rows[i][37].ToString().Trim();
                        newDataInTemp.Value39 = pds.Rows[i][38].ToString().Trim();
                        newDataInTemp.Value40 = pds.Rows[i][39].ToString().Trim();
                        newDataInTemp.Value41 = pds.Rows[i][40].ToString().Trim();
                        newDataInTemp.Value42 = pds.Rows[i][41].ToString().Trim();
                        newDataInTemp.Value43 = pds.Rows[i][42].ToString().Trim();
                        newDataInTemp.Value44 = pds.Rows[i][43].ToString().Trim();
                        newDataInTemp.Value45 = pds.Rows[i][44].ToString().Trim();
                        newDataInTemp.Value46 = pds.Rows[i][45].ToString().Trim();
                        newDataInTemp.Value47 = pds.Rows[i][46].ToString().Trim();
                        newDataInTemp.Value48 = pds.Rows[i][47].ToString().Trim();
                        newDataInTemp.Value49 = pds.Rows[i][48].ToString().Trim();
                        newDataInTemp.Value50 = pds.Rows[i][49].ToString().Trim();
                        newDataInTemp.Value51 = pds.Rows[i][50].ToString().Trim();
                        newDataInTemp.Value52 = pds.Rows[i][51].ToString().Trim();
                        newDataInTemp.Value53 = pds.Rows[i][52].ToString().Trim();
                        newDataInTemp.Value54 = pds.Rows[i][53].ToString().Trim();
                        newDataInTemp.Value55 = pds.Rows[i][54].ToString().Trim();
                        newDataInTemp.RowNo = i + 2;
                        DataInTableService.AddDataInTemp(newDataInTemp);
                    }
                }

                finishProgress("数据已导入临时表，请点击保存按钮进行数据保存审核操作！");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('导入数据为空！')", true);
                return;
            }
        }
        #endregion

        #region 保存审核方法
        /// <summary>
        /// 保存审核方法
        /// </summary>
        private void AuditData()
        {
            string projectId = this.CurrUser.ProjectId;
            var units = from x in Funs.DB.Base_Unit where x.ProjectId == projectId select x;
            var welders = from x in Funs.DB.BS_Welder where x.ProjectId == projectId select x;
            var workAreas = from x in Funs.DB.Base_WorkArea where x.ProjectId == projectId select x;            
            var steels = from x in Funs.DB.BS_Steel select x;
            var rates = from x in Funs.DB.BS_NDTRate select x;
            var types = from x in Funs.DB.BS_JointType select x;
            var methods = from x in Funs.DB.BS_WeldMethod select x;
            var materials = from x in Funs.DB.BS_WeldMaterial select x;
            var services = from x in Funs.DB.BS_Service select x;
            var slopeTypes = from x in Funs.DB.BS_SlopeType select x;
            var isoClasss = from x in Funs.DB.BS_IsoClass select x;
            var components = from x in Funs.DB.BS_Component select x;
            //// 探伤类型
            var tests = from x in Funs.DB.BS_NDTType select x;
            var dataInTemp = from x in Funs.DB.Sys_DataInTemp where x.ProjectId == projectId && x.UserId == this.CurrUser.UserId select x;
            this.beginProgress();
            int rowsCount = dataInTemp.Count();
            int i = 0;
            int okCount = 0;
            foreach (var tempData in dataInTemp)
            {
                if (i % (rowsCount / 100 + 1) == 0 && i < rowsCount && i > 0)
                {
                    setProgress(i / (rowsCount / 100 + 1));
                    //此处用线程休眠代替实际的操作，如加载数据等
                    //System.Threading.Thread.Sleep(50);
                }

                i++;
                if (tempData != null)
                {
                    string errInfo = string.Empty;
                    //var isExitValue = Funs.DB.View_JointInfoAndIsoInfo.FirstOrDefault(x => x.ProjectId == projectId && x.UnitCode == tempData.Value1
                    //    && x.WorkAreaCode == tempData.Value2 && x.ISO_IsoNo == tempData.Value3 && x.JOT_JointNo == tempData.Value4);
                    //if (isExitValue == null || isUpdate)
                    
                    int? installationId = null;
                    string unitId = string.Empty;
                    string workAreaId = string.Empty;

                    Model.PW_IsoInfo isoInfo = new Model.PW_IsoInfo(); ///管线
                    Model.PW_JointInfo jointInfo = new Model.PW_JointInfo();  ///焊口
                    #region 管线-焊口
                    if (!string.IsNullOrEmpty(tempData.Value1))
                    {
                        var unit = units.FirstOrDefault(x => x.UnitCode == tempData.Value1);
                        if (unit == null)
                        {
                            errInfo += "单位代码[" + tempData.Value1 + "]不存在；";
                        }
                        else
                        {
                            isoInfo.BSU_ID = unit.UnitId;
                            unitId= unit.UnitId;
                        }
                    }
                    else
                    {
                        errInfo += "单位代码为必填项；";
                    }
                    if (!string.IsNullOrEmpty(tempData.Value2))
                    {
                        var workArea = workAreas.FirstOrDefault(x => x.WorkAreaCode == tempData.Value2 && x.UnitId == unitId);
                        if (workArea == null)
                        {
                            errInfo += "工区编号[" + tempData.Value2 + "]不存在或不在该单位下；";
                        }
                        else
                        {
                            isoInfo.BAW_ID = workArea.WorkAreaId;                           
                            installationId = workArea.InstallationId;
                            workAreaId = workArea.WorkAreaId;
                            if (!installationId.HasValue)
                            {
                                errInfo += "工区编号[" + tempData.Value2 + "]不存在装置；";
                            }
                        }
                    }
                    else
                    {
                        errInfo += "工区编号为必填项；";
                    }
                    if (string.IsNullOrEmpty(tempData.Value3))
                    {
                        errInfo += "管线代号此项为必填项！";
                    }
                    else
                    {
                        isoInfo.ISO_IsoNo = tempData.Value3;
                    }
                    if (string.IsNullOrEmpty(tempData.Value4))
                    {
                        errInfo += "焊口代号此项为必填项！";
                    }
                    else
                    {
                        jointInfo.JOT_JointNo = tempData.Value4;
                    }
                    if (!string.IsNullOrEmpty(tempData.Value5))
                    {
                        var steel = steels.FirstOrDefault(x => x.STE_Code == tempData.Value5);
                        if (steel == null)
                        {
                            errInfo += "材质1[" + tempData.Value5 + "]不存在；";
                        }
                        else
                        {
                            isoInfo.STE_ID = steel.STE_ID;
                            jointInfo.STE_ID = steel.STE_ID;
                        }
                    }
                    else
                    {
                        errInfo += "材质1为必填项；";
                    }
                    if (!string.IsNullOrEmpty(tempData.Value6))
                    {
                        var steel = steels.FirstOrDefault(x => x.STE_Code == tempData.Value6);
                        if (steel == null)
                        {
                            errInfo += "材质2[" + tempData.Value6 + "]不存在；";
                        }
                        else
                        {
                            jointInfo.STE_ID2 = steel.STE_ID;
                        }
                    }
                    if (!string.IsNullOrEmpty(tempData.Value7))
                    {
                        var rate = rates.FirstOrDefault(x => x.NDTR_Code == tempData.Value7);
                        if (rate == null)
                        {
                            errInfo += "探伤比例[" + tempData.Value7 + "]不存在；";
                        }
                        else
                        {
                            isoInfo.NDTR_ID = rate.NDTR_ID;
                        }
                    }
                    else
                    {
                        errInfo += "探伤比例为必填项；";
                    }
                    if (!string.IsNullOrEmpty(tempData.Value8))
                    {
                        var type = types.FirstOrDefault(x => x.JOTY_Code == tempData.Value8);
                        if (type == null)
                        {
                            errInfo += "焊缝类型代号[" + tempData.Value8 + "]不存在；";
                        }
                        else
                        {
                            jointInfo.JOTY_ID = type.JOTY_ID;
                        }
                    }
                    else
                    {
                        errInfo += "焊缝类型代号为必填项；";
                    }
                    if (!string.IsNullOrEmpty(tempData.Value9))
                    {
                        if (tempData.Value9 != "安装" && tempData.Value9 != "预制")
                        {
                            errInfo += "焊接区域[" + tempData.Value9 + "]不存在；";
                        }
                        else
                        {
                            if (tempData.Value9 == "安装")
                            {
                                jointInfo.WLO_Code = "F";
                            }
                            else
                            {
                                jointInfo.WLO_Code = "S";
                            }
                        }
                    }
                    else
                    {
                        errInfo += "焊接区域为必填项；";
                    }
                    if (!string.IsNullOrEmpty(tempData.Value10))
                    {
                        if (tempData.Value10 != "固定" && tempData.Value10 != "活动")
                        {
                            errInfo += "焊口属性[" + tempData.Value10 + "]不存在；";
                        }
                        else
                        {
                            jointInfo.JOT_JointAttribute = tempData.Value10;
                        }
                    }
                    else
                    {
                        errInfo += "焊口属性为必填项；";
                    }
                    if (!string.IsNullOrEmpty(tempData.Value11))
                    {
                        try
                        {
                            decimal doneDin = Convert.ToDecimal(tempData.Value11);
                            jointInfo.JOT_Size = doneDin;
                            isoInfo.ISO_Specification = doneDin.ToString();
                        }
                        catch (Exception)
                        {
                            errInfo += "达因数[" + tempData.Value11 + "]错误；";
                        }
                    }
                    else
                    {
                        errInfo += "达因数为必填项；";
                    }
                    if (string.IsNullOrEmpty(tempData.Value12))
                    {
                        errInfo += "规格此项为必填项！";
                    }
                    else
                    {
                        jointInfo.JOT_JointDesc = tempData.Value12;
                    }
                    if (string.IsNullOrEmpty(tempData.Value13))
                    {
                        errInfo += "壁厚此项为必填项！";
                    }
                    else
                    {
                        jointInfo.JOT_Sch = tempData.Value13;
                    }
                    if (!string.IsNullOrEmpty(tempData.Value14))
                    {
                        var method = methods.FirstOrDefault(x => x.WME_Code == tempData.Value14);
                        if (method == null)
                        {
                            errInfo += "焊接方法代号[" + tempData.Value14 + "]不存在；";
                        }
                        else
                        {
                            jointInfo.WME_ID = method.WME_ID;
                        }
                    }
                    else
                    {
                        errInfo += "焊接方法代号为必填项；";
                    }
                    if (!string.IsNullOrEmpty(tempData.Value15) && tempData.Value15.Trim().Length > 0)
                    {
                        try
                        {
                            decimal testPress = Convert.ToDecimal(tempData.Value15.Trim());
                            isoInfo.ISO_TestPress = testPress;
                        }
                        catch (Exception)
                        {
                            errInfo += "试验压力[" + tempData.Value15 + "]错误；";
                        }
                    }
                    if (!string.IsNullOrEmpty(tempData.Value16) && tempData.Value16.Trim().Length > 0)
                    {
                        var material = materials.FirstOrDefault(x => x.WMT_MatCode == tempData.Value16 && x.WMT_MatType == "2");
                        if (material == null)
                        {
                            errInfo += "焊条代号[" + tempData.Value16 + "]不存在；";
                        }
                        else
                        {
                            jointInfo.JOT_WeldMat = material.WMT_ID;
                        }
                    }
                    if (!string.IsNullOrEmpty(tempData.Value17))
                    {
                        var material = materials.FirstOrDefault(x => x.WMT_MatCode == tempData.Value17 && x.WMT_MatType == "1");
                        if (material == null)
                        {
                            errInfo += "焊丝代号[" + tempData.Value17 + "]不存在；";
                        }
                        else
                        {
                            jointInfo.JOT_WeldSilk = material.WMT_ID;
                        }
                    }
                    if (!string.IsNullOrEmpty(tempData.Value18))
                    {
                        var service = services.FirstOrDefault(x => x.SER_Code == tempData.Value18);
                        if (service == null)
                        {
                            errInfo += "介质代号[" + tempData.Value18 + "]不存在；";
                        }
                        else
                        {
                            isoInfo.SER_ID = service.SER_ID;
                        }
                    }
                    isoInfo.ISO_IsoNumber = tempData.Value19;
                    if (!string.IsNullOrEmpty(tempData.Value20))
                    {
                        try
                        {
                            decimal testPress = Convert.ToDecimal(tempData.Value20);
                            isoInfo.ISO_DesignPress = testPress;
                        }
                        catch (Exception)
                        {
                            errInfo += "设计压力[" + tempData.Value20 + "]错误；";
                        }
                    }
                    if (!string.IsNullOrEmpty(tempData.Value21))
                    {
                        try
                        {
                            decimal testPress = Convert.ToDecimal(tempData.Value21);
                            isoInfo.ISO_DesignTemperature = testPress;
                        }
                        catch (Exception)
                        {
                            errInfo += "设计温度[" + tempData.Value21 + "]错误；";
                        }
                    }
                    if (!string.IsNullOrEmpty(tempData.Value22))
                    {
                        var slopeType = slopeTypes.FirstOrDefault(x => x.JST_Code == tempData.Value22);
                        if (slopeType == null)
                        {
                            errInfo += "坡口代号[" + tempData.Value22 + "]不存在；";
                        }
                        else
                        {
                            jointInfo.JST_ID = slopeType.JST_ID;
                        }
                    }
                    else
                    {
                        errInfo += "坡口代号必填项；";
                    }
                    if (!string.IsNullOrEmpty(tempData.Value23))
                    {
                        var isoClass = isoClasss.FirstOrDefault(x => x.ISC_IsoCode == tempData.Value23);
                        if (isoClass == null)
                        {
                            errInfo += "管线等级代号[" + tempData.Value23 + "]不存在；";
                        }
                        else
                        {
                            isoInfo.ISC_ID = isoClass.ISC_ID;
                        }
                    }
                    if (!string.IsNullOrEmpty(tempData.Value24))
                    {
                        var component = components.FirstOrDefault(x => x.COM_Code == tempData.Value24);
                        if (component == null)
                        {
                            errInfo += "组件一代号[" + tempData.Value24 + "]不存在；";
                        }
                        else
                        {
                            jointInfo.JOT_Component1 = component.COM_ID;
                        }
                    }
                    if (!string.IsNullOrEmpty(tempData.Value25))
                    {
                        var component = components.FirstOrDefault(x => x.COM_Code == tempData.Value25);
                        if (component == null)
                        {
                            errInfo += "组件二代号[" + tempData.Value25 + "]不存在；";
                        }
                        else
                        {
                            jointInfo.JOT_Component2 = component.COM_ID;
                        }
                    }
                    jointInfo.JOT_HeartNo1 = tempData.Value26;
                    jointInfo.JOT_HeartNo2 = tempData.Value27;
                    jointInfo.JOT_BelongPipe = tempData.Value28;
                    if (!string.IsNullOrEmpty(tempData.Value29))
                    {
                        try
                        {
                            decimal testPress = Convert.ToDecimal(tempData.Value29);
                            jointInfo.JOT_PrepareTemp = testPress;
                        }
                        catch (Exception)
                        {
                            errInfo += "预热温度[" + tempData.Value29 + "]错误；";
                        }
                    }
                    if (!string.IsNullOrEmpty(tempData.Value30))
                    {
                        if (tempData.Value30 != "是" && tempData.Value30 != "否")
                        {
                            errInfo += "是否热处理[" + tempData.Value30 + "]错误；";
                        }
                        else
                        {
                            if (tempData.Value30 == "是")
                            {
                                jointInfo.IS_Proess = "1";
                            }
                            else
                            {
                                jointInfo.IS_Proess = "0";
                            }
                        }
                    }
                    jointInfo.JOT_HotRpt = tempData.Value31;
                    if (!string.IsNullOrEmpty(tempData.Value32))
                    {
                        if (tempData.Value32 != "1G" && tempData.Value32 != "2G" && tempData.Value32 != "3G"
                            && tempData.Value32 != "4G" && tempData.Value32 != "5G" && tempData.Value32 != "6G")
                        {
                            errInfo += "焊接位置[" + tempData.Value32 + "]不存在；";
                        }
                        else
                        {
                            jointInfo.JOT_Location = tempData.Value32;
                        }
                    }
                    else
                    {
                        errInfo += "焊接位置必填项；";
                    }
                    if (!string.IsNullOrEmpty(tempData.Value33))
                    {
                        try
                        {
                            decimal testPress = Convert.ToDecimal(tempData.Value33);
                            jointInfo.JOT_Dia = testPress;
                        }
                        catch (Exception)
                        {
                            errInfo += "外径[" + tempData.Value33 + "]错误；";
                        }
                    }
                    if (!string.IsNullOrEmpty(tempData.Value34))
                    {
                        try
                        {
                            decimal testPress = Convert.ToDecimal(tempData.Value34);
                            isoInfo.ISO_HardnessRate = Convert.ToDecimal(testPress) / Convert.ToDecimal(1.0 * 100);
                        }
                        catch (Exception)
                        {
                            errInfo += "硬度检测比例[" + tempData.Value34 + "]错误；";
                        }
                    }
                    string NDT_ID = string.Empty;
                    if (!string.IsNullOrEmpty(tempData.Value35))
                    {
                        var test = tests.FirstOrDefault(x => x.NDT_Code == tempData.Value35);
                        if (test == null)
                        {
                            errInfo += "探伤类型代号[" + tempData.Value35 + "]不存在；";
                        }
                        else
                        {
                            isoInfo.NDT_ID = test.NDT_ID;
                            NDT_ID= test.NDT_ID;
                        }
                    }
                    if (!string.IsNullOrEmpty(tempData.Value36))
                    {
                        if (tempData.Value36 == "Ⅰ" || tempData.Value36 == "Ⅱ" || tempData.Value36 == "Ⅲ" || tempData.Value36 == "Ⅳ" || tempData.Value36 == "Ⅴ")
                        {
                            isoInfo.ISO_NDTClass = tempData.Value36;
                        }
                        else
                        {
                            errInfo += "合格等级从（Ⅰ、Ⅱ、Ⅲ、Ⅳ、Ⅴ）中选填；";
                        }
                    }
                    

                    if (tempData.Value37 == "是")
                    {
                        isoInfo.IsBig =true;
                    }
                    else
                    {
                        isoInfo.IsBig = false; 
                    }
                    isoInfo.PipeNumber = tempData.Value38;
                    isoInfo.ISO_CwpNo= tempData.Value39;
                    #endregion
                    
                    #region 日报
                    Model.BO_WeldReportMain newWeldReportMain = new Model.BO_WeldReportMain();
                    if (!string.IsNullOrEmpty(tempData.Value40) && !string.IsNullOrEmpty(tempData.Value41))
                    {
                        newWeldReportMain.JOT_DailyReportNo = tempData.Value40;
                        newWeldReportMain.JOT_WeldDate = Funs.GetNewDateTimeOrNow(tempData.Value41);
                        newWeldReportMain.ProjectId = projectId;
                        newWeldReportMain.InstallationId = installationId;
                        newWeldReportMain.BSU_ID = unitId;
                        var getFloorWelder = welders.FirstOrDefault(x => x.WED_Code == tempData.Value42);
                        if (getFloorWelder == null)
                        {
                            errInfo += "打底焊工[" + tempData.Value42 + "]不存在；";
                        }
                        else
                        {
                            jointInfo.JOT_FloorWelder = getFloorWelder.WED_ID;
                        }

                        var getCellWelder = welders.FirstOrDefault(x => x.WED_Code == tempData.Value43);
                        if (getCellWelder == null)
                        {
                            errInfo += "盖面焊工[" + tempData.Value43 + "]不存在；";
                        }
                        else
                        {
                            jointInfo.JOT_CellWelder = getCellWelder.WED_ID;
                        }
                    }
                    #endregion

                    #region 点口
                    Model.BO_Point newPoint = new Model.BO_Point();
                    if (!string.IsNullOrEmpty(tempData.Value44) && !string.IsNullOrEmpty(tempData.Value45))
                    {
                        newPoint.PW_PointNo = tempData.Value44;
                        newPoint.PW_PointDate = Funs.GetNewDateTimeOrNow(tempData.Value45);
                        newPoint.ProjectId = projectId;
                        newPoint.InstallationId = installationId;
                        newPoint.BSU_ID = unitId;
                        if (!newPoint.PW_PointDate.HasValue || newWeldReportMain.JOT_WeldDate > newPoint.PW_PointDate)
                        {
                            errInfo += "点口日期不能早于焊接日期；";
                        }
                    }
                    #endregion

                    #region 委托单
                    Model.CH_Trust newTrust = new Model.CH_Trust();
                    if (!string.IsNullOrEmpty(tempData.Value46) && !string.IsNullOrEmpty(tempData.Value47))
                    {
                        newTrust.CH_TrustCode = tempData.Value46;
                        newTrust.CH_TrustDate = Funs.GetNewDateTimeOrNow(tempData.Value47);
                        newTrust.ProjectId = projectId;
                        newTrust.InstallationId = installationId;
                        newTrust.CH_TrustUnit = unitId;
                        if (!newPoint.PW_PointDate.HasValue || newPoint.PW_PointDate > newTrust.CH_TrustDate)
                        {
                            errInfo += "委托日期不能早于点口日期；";
                        }
                        if (string.IsNullOrEmpty(NDT_ID))
                        {
                            errInfo += "探伤类型不能为空；";
                        }
                    }
                    #endregion

                    #region 检查单
                    Model.CH_Check newCheck = new Model.CH_Check();
                    Model.CH_CheckItem newCheckItem = new Model.CH_CheckItem();
                    if (!string.IsNullOrEmpty(tempData.Value48) && !string.IsNullOrEmpty(tempData.Value49))
                    {
                        newCheck.CHT_CheckCode = tempData.Value48;
                        newCheck.CHT_CheckDate = Funs.GetNewDateTimeOrNow(tempData.Value49);
                        newCheck.ProjectId = projectId;
                        newCheck.InstallationId = installationId;
                        newCheck.UnitId = unitId;
                        if (!newTrust.CH_TrustDate.HasValue || newTrust.CH_TrustDate > newCheck.CHT_CheckDate)
                        {
                            errInfo += "检查日期不能早于委托日期；";
                        }

                        newCheckItem.CHT_FilmDate = Funs.GetNewDateTime(tempData.Value50);
                        newCheckItem.CHT_ReportDate = Funs.GetNewDateTime(tempData.Value51);
                        newCheckItem.CHT_TotalFilm = Funs.GetNewInt(tempData.Value52);
                        newCheckItem.CHT_PassFilm = Funs.GetNewInt(tempData.Value53);
                        if (newCheckItem.CHT_TotalFilm.HasValue  && newCheckItem.CHT_TotalFilm == newCheckItem.CHT_PassFilm)
                        {
                            newCheckItem.CHT_CheckResult = "合格";
                        }
                        else
                        {
                            newCheckItem.CHT_CheckResult = "不合格";
                        }
                        newCheckItem.CHT_CheckNo = tempData.Value54;
                        newCheckItem.CHT_FilmSpecifications = tempData.Value55;
                    }
                    #endregion

                    if (string.IsNullOrEmpty(errInfo)) ////所有信息正确的话 这插入管线焊口
                    {
                        #region 插入管线 焊口信息
                        isoInfo.ProjectId = projectId;
                        jointInfo.ProjectId = projectId;

                        var isExitISOValue = Funs.DB.View_JointInfoAndIsoInfo.FirstOrDefault(x => x.ProjectId == projectId && x.UnitCode == tempData.Value1
                                                && x.WorkAreaCode == tempData.Value2 && x.ISO_IsoNo == tempData.Value3);
                        if (isExitISOValue != null) ///管线已存在
                        {
                            isoInfo.ISO_ID = isExitISOValue.ISO_ID;
                            PW_IsoInfoService.UpdateIsoInfo(isoInfo);
                        }
                        else
                        {
                            isoInfo.ISO_ID = SQLHelper.GetNewID(typeof(Model.PW_IsoInfo));
                            Funs.DB.PW_IsoInfo.InsertOnSubmit(isoInfo);
                            Funs.DB.SubmitChanges();
                        }

                        jointInfo.ISO_ID = isoInfo.ISO_ID;
                        var isExitJotNoValue = Funs.DB.View_JointInfoAndIsoInfo.FirstOrDefault(x => x.ProjectId == projectId && x.UnitCode == tempData.Value1
                                     && x.WorkAreaCode == tempData.Value2 && x.ISO_IsoNo == tempData.Value3 && x.JOT_JointNo == tempData.Value4);
                        if (isExitJotNoValue == null)
                        {
                            jointInfo.JOT_ID = SQLHelper.GetNewID();
                            Funs.DB.PW_JointInfo.InsertOnSubmit(jointInfo);
                            Funs.DB.SubmitChanges();
                        }
                        else
                        {
                            jointInfo.JOT_ID = isExitJotNoValue.JOT_ID;
                            PW_JointInfoService.UpdateJointInfo(jointInfo);
                        }
                        #endregion

                        var getUpdateJot = Funs.DB.PW_JointInfo.FirstOrDefault(e => e.JOT_ID == jointInfo.JOT_ID);
                        if (getUpdateJot != null)
                        {
                            getUpdateJot.JOT_JointStatus = "100";
                            getUpdateJot.JOT_TrustFlag = "00";
                            getUpdateJot.JOT_CheckFlag = "00";
                            getUpdateJot.JOT_FloorWelder = jointInfo.JOT_FloorWelder;
                            getUpdateJot.JOT_CellWelder = jointInfo.JOT_CellWelder;

                            #region 日报
                            if (!string.IsNullOrEmpty(newWeldReportMain.JOT_DailyReportNo))
                            {
                                var getWeldReportMain = Funs.DB.BO_WeldReportMain.FirstOrDefault(x => x.ProjectId == projectId && x.InstallationId == installationId && x.BSU_ID == unitId && x.JOT_DailyReportNo == newWeldReportMain.JOT_DailyReportNo);
                                if (getWeldReportMain != null)
                                {
                                    newWeldReportMain.DReportID = getWeldReportMain.DReportID;
                                    newWeldReportMain.CHT_Tabler = getWeldReportMain.CHT_Tabler;
                                    newWeldReportMain.CHT_TableDate = getWeldReportMain.CHT_TableDate;
                                    Funs.DB.SubmitChanges();
                                }
                                else
                                {
                                    newWeldReportMain.DReportID = SQLHelper.GetNewID();
                                    newWeldReportMain.CHT_Tabler = this.CurrUser.UserName;
                                    newWeldReportMain.CHT_TableDate = newWeldReportMain.JOT_WeldDate;
                                    Funs.DB.BO_WeldReportMain.InsertOnSubmit(newWeldReportMain);
                                    Funs.DB.SubmitChanges();
                                }

                                getUpdateJot.DReportID = newWeldReportMain.DReportID;
                            }
                            #endregion

                            #region 点口单
                            if (!string.IsNullOrEmpty( getUpdateJot.DReportID) && !string.IsNullOrEmpty(newPoint.PW_PointNo))
                            {
                                var getPoint = Funs.DB.BO_Point.FirstOrDefault(x => x.ProjectId == projectId && x.InstallationId == installationId && x.BSU_ID == unitId && x.PW_PointNo == newPoint.PW_PointNo);
                                if (getPoint != null)
                                {
                                    newPoint.PW_PointID = getPoint.PW_PointID;
                                    newPoint.PW_Tabler = getPoint.PW_Tabler;
                                    newPoint.PW_TablerDate = getPoint.PW_TablerDate;
                                    Funs.DB.SubmitChanges();
                                }
                                else
                                {
                                    newPoint.PW_PointID = SQLHelper.GetNewID();
                                    newPoint.PW_Tabler = this.CurrUser.UserId;
                                    newPoint.PW_TablerDate = newPoint.PW_PointDate;
                                    Funs.DB.BO_Point.InsertOnSubmit(newPoint);
                                    Funs.DB.SubmitChanges();
                                }

                                getUpdateJot.PW_PointID = newPoint.PW_PointID;
                                getUpdateJot.JOT_JointStatus = "101";
                            }
                            #endregion

                            #region 委托单
                            string newTrustItemID = string.Empty;
                            if (!string.IsNullOrEmpty(getUpdateJot.PW_PointID) && !string.IsNullOrEmpty(newTrust.CH_TrustCode))
                            {
                                var getTrust = Funs.DB.CH_Trust.FirstOrDefault(x => x.ProjectId == projectId && x.InstallationId == installationId && x.CH_TrustUnit == unitId && x.CH_TrustCode == newTrust.CH_TrustCode);
                                if (getTrust != null)
                                {
                                    newTrust.CH_TrustID = getTrust.CH_TrustID;
                                    newTrust.CH_TrustType = getTrust.CH_TrustType;
                                    newTrust.CH_TrustMan = getTrust.CH_TrustMan;
                                    newTrust.CH_Tabler = getTrust.CH_Tabler;
                                    newTrust.CH_TableDate = getTrust.CH_TableDate;
                                    newTrust.CH_AuditMan = getTrust.CH_AuditMan;
                                    newTrust.CH_AuditDate = getTrust.CH_AuditDate;
                                    newTrust.CH_WorkNo = getTrust.CH_WorkNo;
                                    newTrust.CH_SlopeType = getTrust.CH_SlopeType;
                                    newTrust.CH_WeldMethod = getTrust.CH_WeldMethod;
                                    newTrust.CH_NDTRate = getTrust.CH_NDTRate;
                                    if (!string.IsNullOrEmpty(getTrust.CH_NDTMethod))
                                    {
                                        newTrust.CH_NDTMethod = getTrust.CH_NDTMethod;
                                    }
                                    else
                                    {
                                        newTrust.CH_NDTMethod = isoInfo.NDT_ID;
                                    }
                                    newTrust.CH_AcceptGrade = getTrust.CH_AcceptGrade;
                                    newTrust.CH_CheckUnit = getTrust.CH_CheckUnit;
                                    newTrust.CH_RequestDate = getTrust.CH_RequestDate;
                                    Funs.DB.SubmitChanges();
                                }
                                else
                                {
                                    newTrust.CH_TrustID = SQLHelper.GetNewID();
                                    newTrust.CH_TrustType = "1";
                                    newTrust.CH_TrustMan = this.CurrUser.UserId;
                                    newTrust.CH_Tabler = this.CurrUser.UserId;
                                    newTrust.CH_TableDate = newTrust.CH_TrustDate;
                                    newTrust.CH_AuditMan = this.CurrUser.UserId;
                                    newTrust.CH_AuditDate = newTrust.CH_TrustDate;                                    
                                    newTrust.CH_SlopeType = getUpdateJot.JST_ID;
                                    newTrust.CH_WeldMethod = getUpdateJot.WME_ID;
                                    newTrust.CH_NDTRate = isoInfo.NDTR_ID;
                                    newTrust.CH_NDTMethod = isoInfo.NDT_ID;
                                    var getgrade = TrustManageEditService.GetAcceptGradeList().FirstOrDefault(x => x.Text == isoInfo.ISO_NDTClass);
                                    if (getgrade != null)
                                    {
                                        newTrust.CH_AcceptGrade = getgrade.Value;
                                    }
                                    var getTrustUnit = Funs.DB.CH_Trust.FirstOrDefault(x => x.ProjectId == projectId && x.InstallationId == installationId && x.CH_TrustUnit == unitId);
                                    if (getTrustUnit != null)
                                    {
                                        newTrust.CH_CheckUnit = getTrustUnit.CH_CheckUnit;
                                    }                                    
                                    newTrust.CH_RequestDate = newTrust.CH_TrustDate;

                                    Funs.DB.CH_Trust.InsertOnSubmit(newTrust);
                                    Funs.DB.SubmitChanges();
                                }

                                var getTrustItem = Funs.DB.CH_TrustItem.FirstOrDefault(x => x.CH_TrustID == newTrust.CH_TrustID && x.JOT_ID == getUpdateJot.JOT_ID);
                                if (getTrustItem == null)
                                {
                                    Model.CH_TrustItem newTrustItem = new Model.CH_TrustItem
                                    {
                                        CH_TrustItemID = SQLHelper.GetNewID(),
                                        CH_TrustID = newTrust.CH_TrustID,
                                        JOT_ID = getUpdateJot.JOT_ID,
                                    };
                                    newTrustItemID = newTrustItem.CH_TrustItemID;
                                    Funs.DB.CH_TrustItem.InsertOnSubmit(newTrustItem);
                                    Funs.DB.SubmitChanges();
                                }
                                else
                                {
                                    newTrustItemID = getTrustItem.CH_TrustItemID;
                                }

                                getUpdateJot.JOT_TrustFlag = "01";
                                if (newTrust.CH_AuditDate.HasValue)
                                {
                                    getUpdateJot.JOT_TrustFlag = "02";
                                }                               
                            }
                            #endregion

                            #region 检测单
                            if (!string.IsNullOrEmpty(newTrust.CH_TrustID) && !string.IsNullOrEmpty(newTrustItemID) && !string.IsNullOrEmpty(newCheck.CHT_CheckCode))
                            {
                                var getCheck = Funs.DB.CH_Check.FirstOrDefault(x => x.ProjectId == projectId && x.InstallationId == installationId && x.UnitId == unitId && x.CHT_CheckCode == newCheck.CHT_CheckCode);
                                if (getCheck != null)
                                {
                                    newCheck.CHT_CheckID = getCheck.CHT_CheckID;
                                    newCheck.CH_TrustID = getCheck.CH_TrustID;
                                    newCheck.CHT_CheckType = getCheck.CHT_CheckType;
                                    newCheck.CHT_CheckMan = getCheck.CHT_CheckMan;
                                    newCheck.CHT_Tabler = getCheck.CHT_Tabler;
                                    newCheck.CHT_TableDate = getCheck.CHT_TableDate;
                                    newCheck.CHT_AuditMan = getCheck.CHT_AuditMan;
                                    newCheck.CHT_AuditDate = getCheck.CHT_AuditDate;
                                    Funs.DB.SubmitChanges();
                                }
                                else
                                {
                                    newCheck.CHT_CheckID = SQLHelper.GetNewID();
                                    newCheck.CH_TrustID = newTrust.CH_TrustID;
                                    newCheck.CHT_CheckType = "C1";
                                    newCheck.CHT_Tabler = this.CurrUser.UserId;
                                    newCheck.CHT_TableDate = newCheck.CHT_CheckDate;
                                    newCheck.CHT_AuditMan = this.CurrUser.UserId;
                                    newCheck.CHT_AuditDate = newCheck.CHT_CheckDate;

                                    Funs.DB.CH_Check.InsertOnSubmit(newCheck);
                                    Funs.DB.SubmitChanges();
                                }

                                newCheckItem.CHT_CheckID = newCheck.CHT_CheckID;
                                newCheckItem.JOT_ID = getUpdateJot.JOT_ID;
                                newCheckItem.CH_TrustItemID = newTrustItemID;                               
                                if (!string.IsNullOrEmpty(newTrust.CH_NDTMethod))
                                {
                                    newCheckItem.CHT_CheckMethod = newTrust.CH_NDTMethod;
                                }
                                else
                                {
                                    newCheckItem.CHT_CheckMethod = isoInfo.NDT_ID;
                                }
                                var getCheckItem = Funs.DB.CH_CheckItem.FirstOrDefault(x => x.CHT_CheckID == newCheck.CHT_CheckID && x.CH_TrustItemID== newTrustItemID && x.JOT_ID == getUpdateJot.JOT_ID);
                                if (getCheckItem == null)
                                {
                                    newCheckItem.CHT_CheckItemID = SQLHelper.GetNewID();
                                    newCheckItem.CHT_AuditTime = newCheck.CHT_CheckDate;
                                    Funs.DB.CH_CheckItem.InsertOnSubmit(newCheckItem);
                                    Funs.DB.SubmitChanges();
                                }
                                else
                                {
                                    newCheckItem.CHT_CheckItemID = getCheckItem.CHT_CheckItemID;
                                    newCheckItem.CHT_AuditTime = getCheckItem.CHT_AuditTime;
                                    Funs.DB.SubmitChanges();
                                }

                                getUpdateJot.JOT_CheckFlag = "01";
                                if (newCheck.CHT_AuditDate.HasValue)
                                {
                                    getUpdateJot.JOT_CheckFlag = "02";
                                }
                            }
                            #endregion

                            #region 试压包
                            if (!string.IsNullOrEmpty(isoInfo.ISO_CwpNo))
                            {
                                string ptp_ID = SQLHelper.GetNewID();
                                var getTestPackage = Funs.DB.TP_TestPackage.FirstOrDefault(x => x.ProjectId == projectId && x.BSU_ID == unitId && x.InstallationId == installationId && x.PTP_TestPackageNo == isoInfo.ISO_CwpNo);
                                if (getTestPackage == null)
                                {
                                    Model.TP_TestPackage newTestPackage = new Model.TP_TestPackage
                                    {
                                        PTP_ID = ptp_ID,
                                        ProjectId = projectId,
                                        InstallationId = installationId,
                                        BSU_ID = unitId,
                                        PTP_TestPackageNo = isoInfo.ISO_CwpNo,
                                        PTP_TestPackageCode = isoInfo.ISO_CwpNo,
                                        PTP_Modifier = this.CurrUser.UserId,
                                        PTP_Tabler = this.CurrUser.UserId,
                                        PTP_TableDate = DateTime.Now.Date,
                                    };
                                    Funs.DB.TP_TestPackage.InsertOnSubmit(newTestPackage);
                                    Funs.DB.SubmitChanges();
                                }
                                else
                                {
                                    ptp_ID = getTestPackage.PTP_ID;
                                }

                                var getIsoList = Funs.DB.TP_IsoList.FirstOrDefault(x => x.PTP_ID == ptp_ID && x.ISO_ID == getUpdateJot.ISO_ID);
                                if (getIsoList == null)
                                {
                                    Model.TP_IsoList newIsoList = new Model.TP_IsoList
                                    {
                                        PT_ID = SQLHelper.GetNewID(),
                                        PTP_ID = ptp_ID,
                                        ISO_ID= getUpdateJot.ISO_ID,
                                    };

                                    Funs.DB.TP_IsoList.InsertOnSubmit(newIsoList);
                                    Funs.DB.SubmitChanges();
                                }
                             }
                            #endregion

                            Funs.DB.SubmitChanges();
                            DataInTableService.DeleteDataInTempByDataInTempID(tempData.TempId);
                            okCount++;
                        }
                    }

                    if (!string.IsNullOrEmpty(errInfo))
                    {
                        tempData.ToopValue = errInfo;                       
                        DataInTableService.UpdateDataInTemp(tempData);
                    }
                }
            }

            finishProgress("保存操作已完成，成功保存" + okCount.ToString() + "条数据到管线焊口表！");
        }
        #endregion
    }
}