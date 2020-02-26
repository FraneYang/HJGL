using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using BLL;

namespace Web.WeldingManage
{
    public partial class WeldReport : PPage
    {
        /// <summary>
        /// 焊接日报主键
        /// </summary>
        public string DReportID
        {
            get
            {
                return (string)ViewState["DReportID"];
            }
            set
            {
                ViewState["DReportID"] = value;
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
        /// 文本框是否可编辑
        /// </summary>
        /// <param name="readOnly"></param>
        private void TextIsReadOnly(bool readOnly)
        {
            this.txtDReportID.Enabled = !readOnly;
            this.drpUnit.Enabled = !readOnly;
            this.txtJOT_WeldDate.Enabled = !readOnly;
            this.drpCHT_Tabler.Enabled = !readOnly;
            this.txtCHT_TableDate.Enabled = !readOnly;
            this.txtJOT_Remark.Enabled = !readOnly;
        }

        /// <summary>
        /// 按钮是否可用
        /// </summary>
        /// <param name="enabled"></param>
        private void ButtonIsEnabled(bool enabled)
        {
            this.btnSave.Enabled = enabled;
            this.imgSearch.Enabled = enabled;
        }

        /// <summary>
        /// 页面加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {
                this.imgbtnIn.Visible = false;
                Model.Sys_Set dayReportDataIn = Funs.DB.Sys_Set.FirstOrDefault(x => x.SetId == 6);
                if (dayReportDataIn!= null && dayReportDataIn.IsAuto == true)
                {
                    this.imgbtnIn.Visible = true;
                }
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.WeldReportMenuId);
                this.TextIsReadOnly(true);
                this.ButtonIsEnabled(false);
                this.txtReportDate.Value = string.Format("{0:yyyy-MM}", DateTime.Now);
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ReportSearch();</script>");
            }
        }

        /// <summary>
        /// 每次执行Select()和SelectCount前都会引发一次该事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ObjectDataSource1_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
        {
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnAdd) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                this.TextIsReadOnly(false);
                this.ButtonIsEnabled(true);

                jointInfos = new List<Model.PW_JointInfo>();
                drpUnit.Items.Clear();
                drpInstall.Items.Clear();
                drpCHT_Tabler.Items.Clear();
                Funs.PleaseSelect(this.drpCHT_Tabler);
                this.drpCHT_Tabler.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));

                Funs.PleaseSelect(this.drpUnit);
                var unit = BLL.UnitService.GetUnit(this.CurrUser.UnitId);
                if (unit == null || unit.UnitType == "1")
                {
                    this.drpUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId));
                }
                else
                {
                    this.drpUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                }

                this.txtJOT_Remark.Text = string.Empty;
                this.gvWeldReportDetail.Visible = false;
                this.ckAll.Checked = true;
                this.ckBoth.Checked = true;
                this.drpCHT_Tabler.SelectedValue = this.CurrUser.UserId;
                this.txtJOT_WeldDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                this.txtCHT_TableDate.Text = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                this.txtDReportID.Text = string.Empty;

                if (BLL.SysSetService.IsAuto(1, this.CurrUser.ProjectId) != true)
                {
                    this.txtDReportID.ReadOnly = false;
                    //string date = this.txtJOT_WeldDate.Text.Replace("-", "");
                    //this.txtDReportID.Text = BLL.SQLHelper.RunProcNewId("SpGetNewCode", "dbo.BO_WeldReportMain", "JOT_DailyReportNo", date + "-");
                }
                else
                {
                    this.txtDReportID.ReadOnly = true;
                }
                DReportID = null;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }

        protected string GetUnitName(object BSU_ID)
        {
            if (BSU_ID != null)
            {
                return BLL.UnitService.GetUnit(BSU_ID.ToString()).UnitName;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 焊口集合
        /// </summary>
        private List<Model.PW_JointInfo> jointInfos = new List<Model.PW_JointInfo>();

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            string selectList = this.hdSelectList.Value;
            List<string> infos = selectList.Split(',').ToList();
            jointInfos.Clear();
            if (DReportID == null)
            {
                if (!string.IsNullOrEmpty(selectList))
                {
                    foreach (var item in infos)
                    {
                        Model.PW_JointInfo info = BLL.PW_JointInfoService.GetJointInfoByJotID(item);
                        jointInfos.Add(info);
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(selectList))
                {
                    Get_jointInfos("single", null);
                    foreach (var jot in infos)
                    {
                        Model.PW_JointInfo info = BLL.PW_JointInfoService.GetJointInfoByJotID(jot);

                        if (jointInfos.Where(y => y.JOT_ID == jot).Count() == 0)
                        {
                            jointInfos.Add(info);
                        }
                    }
                }
            }

            if (BLL.SysSetService.IsAuto(1, this.CurrUser.ProjectId) == true)
            {
                string isoId = jointInfos.First().ISO_ID;
                string bawId = BLL.PW_IsoInfoService.GetIsoInfoByIsoInfoId(isoId).BAW_ID;
                string bawCode = BLL.WorkAreaService.getWorkAreaByWorkAreaId(bawId).WorkAreaCode;
                this.txtDReportID.Text = bawCode;
            }

            jointInfos = (from x in jointInfos join y in Funs.DB.PW_IsoInfo on x.ISO_ID equals y.ISO_ID orderby y.ISO_IsoNo, x.JOT_JointNo select x).ToList();

            if (jointInfos.Count > 0)
            {
                this.gvWeldReportDetail.Visible = true;
                this.gvWeldReportDetail.DataSource = jointInfos;
                this.gvWeldReportDetail.DataBind();
            }
            string size = (from x in jointInfos select x.JOT_Size ?? 0).Sum().ToString("F");
            this.lblSize.Text = size;
            this.lblDine.Text = size;
        }
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
                if (this.gvWeldReportDetail.Rows.Count > 0)
                {

                    Get_jointInfos("single", null);
                    foreach (var item in jointInfos)
                    {
                        if (item.JOT_CellWelder == null || item.JOT_FloorWelder == null)
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('焊工信息不能为空！')", true);
                            return;
                        }
                    }

                    if (string.IsNullOrEmpty(this.txtJOT_WeldDate.Text))
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('焊接日期不能为空！')", true);
                        return;
                    }
                    Model.BO_WeldReportMain report = new Model.BO_WeldReportMain();
                    report.ProjectId = this.CurrUser.ProjectId;
                    if (this.drpUnit.SelectedValue != "0")
                    {
                        report.BSU_ID = this.drpUnit.SelectedValue;
                    }
                    if (!string.IsNullOrEmpty(this.txtJOT_WeldDate.Text))
                    {
                        report.JOT_WeldDate = Convert.ToDateTime(this.txtJOT_WeldDate.Text);
                    }

                    if (this.drpCHT_Tabler.SelectedValue != "0")
                    {
                        report.CHT_Tabler = this.drpCHT_Tabler.SelectedItem.Text;
                    }
                    if (!string.IsNullOrEmpty(this.txtJOT_WeldDate.Text))
                    {
                        report.CHT_TableDate = Convert.ToDateTime(this.txtCHT_TableDate.Text);
                    }
                    if (!string.IsNullOrEmpty(this.txtJOT_Remark.Text.Trim()))
                    {
                        report.JOT_Remark = this.txtJOT_Remark.Text.Trim();
                    }
                    if (this.drpInstall.SelectedValue != "0")
                    {
                        report.InstallationId = Convert.ToInt32(this.drpInstall.SelectedValue);
                    }

                    if (!string.IsNullOrEmpty(DReportID))       // 修改
                    {
                        if (BLL.SysSetService.IsAuto(1, this.CurrUser.ProjectId) == true)
                        {
                            if (!string.IsNullOrEmpty(this.txtDReportID.Text.Trim()))
                            {
                                var d = BLL.WeldReportService.GetWeldReportByDReportID(DReportID);
                                report.JOT_DailyReportNo = d.JOT_DailyReportNo;
                            }
                        }
                        else
                        {
                            report.JOT_DailyReportNo = this.txtDReportID.Text.Trim();
                        }
                        report.DReportID = DReportID;

                        BLL.WeldReportService.UpdateWeldReport(report);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "修改焊接日报信息");
                    }
                    else       // 保存
                    {
                        if (BLL.SysSetService.IsAuto(1, this.CurrUser.ProjectId) == true)
                        {
                            string isoId = jointInfos.First().ISO_ID;
                            string bawId = BLL.PW_IsoInfoService.GetIsoInfoByIsoInfoId(isoId).BAW_ID;
                            string bawCode = BLL.WorkAreaService.getWorkAreaByWorkAreaId(bawId).WorkAreaCode;
                            string welder = BLL.PersonManageService.GetWelderByWenId(jointInfos.First().JOT_FloorWelder).WED_Code;
                            string perfix = bawCode + "-" + welder + "-";

                            string reportNo = BLL.SQLHelper.RunProcNewId("SpGetNewCode", "dbo.BO_WeldReportMain", "JOT_DailyReportNo", this.CurrUser.ProjectId, perfix);
                            report.JOT_DailyReportNo = reportNo;
                        }
                        else
                        {
                            if (!BLL.WeldReportService.IsExistDailyReportNO(this.txtDReportID.Text))
                            {
                                report.JOT_DailyReportNo = this.txtDReportID.Text.Trim();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('日报编号已存在，请重新录入！')", true);
                                return;
                            }
                        }

                        report.DReportID = SQLHelper.GetNewID(typeof(Model.BO_WeldReportMain));
                        BLL.WeldReportService.AddWeldReport(report);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "添加焊接日报信息");
                    }

                    foreach (var item in jointInfos)
                    {
                        item.DReportID = report.DReportID;
                        item.JOT_JointStatus = "100";
                        BLL.PW_JointInfoService.UpdateJointInfoByDReport(item);

                        //更新焊口号 修改固定焊口号后 +G
                        BLL.PW_JointInfoService.UpdateJointNoAddG(item.JOT_ID, item.JOT_JointAttribute, Const.Add);
                    }

                    jointInfos.Clear();
                    this.gvWeldReportDetail.DataSource = null;
                    this.gvWeldReportDetail.DataBind();
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！');", true);
                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ReportSearch();</script>");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('焊口信息不能为空！')", true);
                    return;
                }
            }
        }
        #endregion

        /// <summary>
        /// 重新绑定集合
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="weldId"></param>
        protected void Get_jointInfos(string flag, string weldId)
        {
            jointInfos.Clear();
            for (int i = 0; i < this.gvWeldReportDetail.Rows.Count; i++)
            {
                TextBox txtJOT_JointNo = (TextBox)(this.gvWeldReportDetail.Rows[i].FindControl("txtJOT_JointNo"));
                HiddenField hdISO_ID = (HiddenField)(this.gvWeldReportDetail.Rows[i].FindControl("hdISO_ID"));
                HiddenField hdJOT_CellWelder = (HiddenField)(this.gvWeldReportDetail.Rows[i].FindControl("hdJOT_CellWelder"));
                HiddenField hdJOT_FloorWelder = (HiddenField)(this.gvWeldReportDetail.Rows[i].FindControl("hdJOT_FloorWelder"));
                DropDownList drpWLO_Code = (DropDownList)(this.gvWeldReportDetail.Rows[i].FindControl("drpWLO_Code"));
                DropDownList drpJOT_Location = (DropDownList)(this.gvWeldReportDetail.Rows[i].FindControl("drpJOT_Location"));
                DropDownList drpJOT_JointAttribute = (DropDownList)(this.gvWeldReportDetail.Rows[i].FindControl("drpJOT_JointAttribute"));
                TextBox txtJOT_DoneDin = (TextBox)(this.gvWeldReportDetail.Rows[i].FindControl("txtJOT_DoneDin"));
                Label txtJOT_Size = (Label)(this.gvWeldReportDetail.Rows[i].FindControl("txtJOT_Size"));
                HiddenField hdJOT_ID = (HiddenField)(this.gvWeldReportDetail.Rows[i].FindControl("hdJOT_ID"));

                Model.PW_JointInfo info = new Model.PW_JointInfo();
                info.JOT_ID = hdJOT_ID.Value;
                info.ISO_ID = hdISO_ID.Value;
                info.JOT_JointNo = txtJOT_JointNo.Text.Trim();

                if (flag == "all")
                {
                    if (String.IsNullOrEmpty(hdJOT_CellWelder.Value))
                    {
                        info.JOT_CellWelder = weldId;
                    }
                    else
                    {
                        info.JOT_CellWelder = hdJOT_CellWelder.Value;
                    }

                    if (String.IsNullOrEmpty(hdJOT_FloorWelder.Value))
                    {
                        info.JOT_FloorWelder = weldId;
                    }
                    else
                    {
                        info.JOT_FloorWelder = hdJOT_FloorWelder.Value;
                    }
                }

                if (flag == "cell")
                {
                    if (String.IsNullOrEmpty(hdJOT_CellWelder.Value))
                    {
                        info.JOT_CellWelder = weldId;
                    }
                    else
                    {
                        info.JOT_CellWelder = hdJOT_CellWelder.Value;
                    }

                    if (!String.IsNullOrEmpty(hdJOT_FloorWelder.Value))
                    {
                        info.JOT_FloorWelder = hdJOT_FloorWelder.Value;
                    }
                }

                if (flag == "floor")
                {
                    if (String.IsNullOrEmpty(hdJOT_FloorWelder.Value))
                    {
                        info.JOT_FloorWelder = weldId;
                    }
                    else
                    {
                        info.JOT_FloorWelder = hdJOT_FloorWelder.Value;
                    }

                    if (!String.IsNullOrEmpty(hdJOT_CellWelder.Value))
                    {
                        info.JOT_CellWelder = hdJOT_CellWelder.Value;
                    }
                }

                if (flag == "single")
                {
                    if (!String.IsNullOrEmpty(hdJOT_CellWelder.Value))
                    {
                        info.JOT_CellWelder = hdJOT_CellWelder.Value;
                    }
                    if (!string.IsNullOrEmpty(hdJOT_FloorWelder.Value))
                    {
                        info.JOT_FloorWelder = hdJOT_FloorWelder.Value;
                    }
                }

                if (!String.IsNullOrEmpty(drpWLO_Code.SelectedValue))
                {
                    info.WLO_Code = drpWLO_Code.SelectedValue;
                }
                if (!String.IsNullOrEmpty(drpJOT_Location.SelectedValue))
                {
                    info.JOT_Location = drpJOT_Location.SelectedValue;
                }
                if (!string.IsNullOrEmpty(txtJOT_Size.Text.Trim()))
                {
                    info.JOT_Size = Convert.ToDecimal(txtJOT_Size.Text.Trim());
                }
                else
                {
                    info.JOT_Size = 0;
                }
                info.JOT_JointAttribute = drpJOT_JointAttribute.SelectedValue.Trim();
                if (!string.IsNullOrEmpty(txtJOT_DoneDin.Text.Trim()))
                {
                    info.JOT_DoneDin = Convert.ToDecimal(txtJOT_DoneDin.Text.Trim());
                }
                jointInfos.Add(info);
            }
        }

        /// <summary>
        /// 当 GridView 内生成事件时激发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvWeldReportDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Get_jointInfos("single", null);
            string JOT_ID = e.CommandArgument.ToString();
            if (e.CommandName == "del")
            {
                foreach (Model.PW_JointInfo info in jointInfos)
                {
                    if (info.JOT_ID == JOT_ID)
                    {

                        Model.PW_JointInfo i = BLL.PW_JointInfoService.GetJointInfoByJotID(JOT_ID);
                        if (String.IsNullOrEmpty(i.PW_PointID))
                        {
                            jointInfos.Remove(info);
                            i.DReportID = null;
                            i.JOT_CellWelder = null;
                            i.JOT_FloorWelder = null;
                            i.JOT_DoneDin = null;
                            BLL.PW_JointInfoService.UpdateJointInfoByDReport(i);
                            ////更新焊口号 删除固定焊口号后 +G
                            BLL.PW_JointInfoService.UpdateJointNoAddG(i.JOT_ID, i.JOT_JointAttribute, Const.Delete);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('已点口，不能删除！');", true);
                        }
                        break;
                    }
                }
            }

            this.gvWeldReportDetail.DataSource = jointInfos;
            this.gvWeldReportDetail.DataBind();
        }

        /// <summary>
        /// 在控件被数据绑定后激发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvWeldReportDetail_DataBound(object sender, EventArgs e)
        {
            int rowsCount = this.gvWeldReportDetail.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                TextBox txtJOT_CellWelder = (TextBox)(this.gvWeldReportDetail.Rows[i].FindControl("txtJOT_CellWelder"));
                TextBox txtJOT_FloorWelder = (TextBox)(this.gvWeldReportDetail.Rows[i].FindControl("txtJOT_FloorWelder"));
                DropDownList drpJOT_Location = (DropDownList)(this.gvWeldReportDetail.Rows[i].FindControl("drpJOT_Location"));
                HiddenField hdJOT_CellWelder = (HiddenField)(this.gvWeldReportDetail.Rows[i].FindControl("hdJOT_CellWelder"));
                HiddenField hdJOT_ID = (HiddenField)(this.gvWeldReportDetail.Rows[i].FindControl("hdJOT_ID"));
                HiddenField hdJOT_FloorWelder = (HiddenField)(this.gvWeldReportDetail.Rows[i].FindControl("hdJOT_FloorWelder"));
                ((TextBox)(this.gvWeldReportDetail.Rows[i].FindControl("txtJOT_DoneDin"))).Attributes["onblur"] = "GetSize()";
                if (this.ckAll.Checked == true && this.ckBoth.Checked == true)
                {
                    ((TextBox)(this.gvWeldReportDetail.Rows[i].FindControl("txtJOT_CellWelder"))).Attributes["onfocus"] = "GetPersonAll1('" + this.drpUnit.SelectedValue.Trim() + "')";
                    ((TextBox)(this.gvWeldReportDetail.Rows[i].FindControl("txtJOT_FloorWelder"))).Attributes["onfocus"] = "GetPersonAll1('" + this.drpUnit.SelectedValue.Trim() + "')";
                }
                else if (this.ckAll.Checked == true && this.ckBoth.Checked == false)
                {
                    ((TextBox)(this.gvWeldReportDetail.Rows[i].FindControl("txtJOT_CellWelder"))).Attributes["onfocus"] = "GetPersonAll2_C('" + this.drpUnit.SelectedValue.Trim() + "')";
                    ((TextBox)(this.gvWeldReportDetail.Rows[i].FindControl("txtJOT_FloorWelder"))).Attributes["onfocus"] = "GetPersonAll2_F('" + this.drpUnit.SelectedValue.Trim() + "')";
                }
                else if (this.ckAll.Checked == false && this.ckBoth.Checked == true)
                {
                    ((TextBox)(this.gvWeldReportDetail.Rows[i].FindControl("txtJOT_CellWelder"))).Attributes["onfocus"] = "GetPerson3('" + this.drpUnit.SelectedValue.Trim() + "','" + txtJOT_CellWelder.ClientID + "','" + hdJOT_CellWelder.ClientID + "','" + txtJOT_FloorWelder.ClientID + "','" + hdJOT_FloorWelder.ClientID + "')";
                    ((TextBox)(this.gvWeldReportDetail.Rows[i].FindControl("txtJOT_FloorWelder"))).Attributes["onfocus"] = "GetPerson3('" + this.drpUnit.SelectedValue.Trim() + "','" + txtJOT_CellWelder.ClientID + "','" + hdJOT_CellWelder.ClientID + "','" + txtJOT_FloorWelder.ClientID + "','" + hdJOT_FloorWelder.ClientID + "')";
                }
                else
                {
                    ((TextBox)(this.gvWeldReportDetail.Rows[i].FindControl("txtJOT_CellWelder"))).Attributes["onfocus"] = "GetPerson1('" + this.drpUnit.SelectedValue.Trim() + "','" + txtJOT_CellWelder.ClientID + "','" + hdJOT_CellWelder.ClientID + "')";
                    ((TextBox)(this.gvWeldReportDetail.Rows[i].FindControl("txtJOT_FloorWelder"))).Attributes["onfocus"] = "GetPerson2('" + this.drpUnit.SelectedValue.Trim() + "','" + txtJOT_FloorWelder.ClientID + "','" + hdJOT_FloorWelder.ClientID + "')";
                }
                Model.PW_JointInfo info = BLL.PW_JointInfoService.GetJointInfoByJotID(hdJOT_ID.Value.Trim());
                if (string.IsNullOrEmpty(info.JOT_Location))
                {
                    drpJOT_Location.SelectedValue = "1G";
                }
                else
                {
                    drpJOT_Location.SelectedValue = info.JOT_Location;
                }
            }
        }

        protected string GetBAW_ID(object ISO_ID)
        {
            if (ISO_ID != null && ISO_ID.ToString() != "")
            {
                return BLL.WorkAreaService.getWorkAreaByWorkAreaId(BLL.PW_IsoInfoService.GetIsoInfoByIsoInfoId(ISO_ID.ToString()).BAW_ID).WorkAreaCode;
            }
            else
            {
                return "";
            }
        }

        protected string GetISO_IsoNo(object ISO_ID)
        {
            if (ISO_ID != null && ISO_ID.ToString() != "")
            {
                return BLL.PW_IsoInfoService.GetIsoInfoByIsoInfoId(ISO_ID.ToString()).ISO_IsoNo;
            }
            else
            {
                return "";
            }
        }

        protected string GetNDTR(object ISO_ID)
        {
            string ndtr = string.Empty;
            if (ISO_ID != null && ISO_ID.ToString() != "")
            {
                var iso = BLL.PW_IsoInfoService.GetIsoInfoByIsoInfoId(ISO_ID.ToString());
                if (!String.IsNullOrEmpty(iso.NDTR_ID))
                {
                    ndtr = BLL.DetectionService.GetNDTRateByNDTRID(iso.NDTR_ID).NDTR_Name;
                }
            }
            return ndtr;
        }

        protected string GetPersonNameByJOT_CellWelder(object JOT_CellWelder)
        {
            if (JOT_CellWelder != null && JOT_CellWelder.ToString() != "")
            {
                return BLL.PersonManageService.GetBSWelderByTeamWEDID(JOT_CellWelder.ToString()).WED_Code;
            }
            else
            {
                return "";
            }
        }

        protected string GetPersonNameByJOT_FloorWelder(object JOT_FloorWelder)
        {
            if (JOT_FloorWelder != null && JOT_FloorWelder.ToString() != "")
            {
                return BLL.PersonManageService.GetBSWelderByTeamWEDID(JOT_FloorWelder.ToString()).WED_Code;
            }
            else
            {
                return "";
            }
        }

        protected string JOT_DoneDin(object JOT_ID)
        {
            if (JOT_ID != null)
            {
                Model.PW_JointInfo info = (from x in jointInfos where x.JOT_ID == JOT_ID.ToString() select x).First();
                if (info.JOT_DoneDin == null || info.JOT_DoneDin == 0)
                {
                    if (info.JOT_Size != null)
                    {
                        return info.JOT_Size.Value.ToString("F");
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return info.JOT_DoneDin.Value.ToString("F");
                }
            }
            else
            {
                return "";
            }
        }

        protected void imgSize_Click(object sender, ImageClickEventArgs e)
        {
            Get_jointInfos("single", null);

            this.lblSize.Text = (from x in jointInfos select x.JOT_Size ?? 0).Sum().ToString("F");
            this.lblDine.Text = (from x in jointInfos select x.JOT_DoneDin ?? 0).Sum().ToString("F");
        }

        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (!string.IsNullOrEmpty(DReportID))
            {
                List<Model.PW_JointInfo> infos = BLL.PW_JointInfoService.GetJointInfosByDReportID(DReportID);
                bool isPoint = false;
                foreach (var p in infos)
                {
                    if (!String.IsNullOrEmpty(p.PW_PointID))
                    {
                        isPoint = true;
                        break;
                    }
                }

                if (isPoint == false)
                {
                    if (this.drpUnit.SelectedValue == "0" || this.drpInstall.SelectedValue == "0")
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择单位及装置！')", true);
                        return;
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ShowSearch('" + this.drpUnit.SelectedValue + "','" + this.drpInstall.SelectedValue + "');</script>");
                    }
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此日报已有点口数据，不能修改！');", true);
                }
            }

            else
            {
                if (this.drpUnit.SelectedValue == "0" || this.drpInstall.SelectedValue == "0")
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择单位及装置！')", true);
                    return;
                }
                else
                {
                    ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ShowSearch('" + this.drpUnit.SelectedValue + "','" + this.drpInstall.SelectedValue + "');</script>");
                }
            }
        }

        protected void imgBtnAll1_Click(object sender, ImageClickEventArgs e)
        {
            string id = this.hdAll1.Value;
            if (!string.IsNullOrEmpty(id))
            {
                Get_jointInfos("all", id);
                this.gvWeldReportDetail.DataSource = jointInfos;
                this.gvWeldReportDetail.DataBind();
            }
        }

        protected void imgBtnAll2_C_Click(object sender, ImageClickEventArgs e)
        {
            string id = this.hdAll1.Value;
            if (!string.IsNullOrEmpty(id))
            {
                Get_jointInfos("cell", id);
                this.gvWeldReportDetail.DataSource = jointInfos;
                this.gvWeldReportDetail.DataBind();
            }
        }

        protected void imgBtnAll2_F_Click(object sender, ImageClickEventArgs e)
        {
            string id = this.hdAll1.Value;
            if (!string.IsNullOrEmpty(id))
            {
                Get_jointInfos("floor", id);
                this.gvWeldReportDetail.DataSource = jointInfos;
                this.gvWeldReportDetail.DataBind();
            }
        }

        protected void imgBtnAll3_Click(object sender, ImageClickEventArgs e)
        {
            Get_jointInfos("single", null);
            this.gvWeldReportDetail.DataSource = jointInfos;
            this.gvWeldReportDetail.DataBind();
        }

        /// <summary>
        /// 日报集合
        /// </summary>
        private List<Model.BO_WeldReportMain> weldReportMains = new List<Model.BO_WeldReportMain>();

        protected void imgReportSearch_Click(object sender, ImageClickEventArgs e)
        {
            weldReportMains.Clear();
            if (!string.IsNullOrEmpty(this.txtReportDate.Value.Trim()))
            {
                DateTime startTime = Convert.ToDateTime(this.txtReportDate.Value.Trim() + "-01");
                DateTime endTime = startTime.AddMonths(1);
                weldReportMains = (from x in Funs.DB.BO_WeldReportMain
                                   where x.JOT_WeldDate >= startTime && x.JOT_WeldDate < endTime && x.ProjectId == this.CurrUser.ProjectId
                                   select x).ToList();
                this.tvControlItem.Nodes.Clear();
                List<Model.Base_Unit> units = null;
                var getunit = BLL.UnitService.GetUnit(this.CurrUser.UnitId);
                if (getunit == null || getunit.UnitType == "1")
                {
                    units = BLL.UnitService.GetWeldReportUnitsByUnitType("2", this.CurrUser.ProjectId);
                }
                else
                {
                    units = BLL.UnitService.GetUnits(this.CurrUser.UnitId);
                }

                if (units != null)
                {
                    foreach (var unit in units)
                    {
                        TreeNode rootNode = new TreeNode();//定义根节点
                        rootNode.Text = unit.UnitName;
                        rootNode.Value = unit.UnitId;
                        rootNode.Expanded = true;
                        this.tvControlItem.Nodes.Add(rootNode);
                        this.GetNodes(rootNode.ChildNodes, rootNode.Value, rootNode, startTime, endTime);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请先增加施工单位！')", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择日报月份！')", true);
                return;
            }
        }

        #region  遍历节点方法
        /// <summary>
        /// 遍历节点方法
        /// </summary>
        /// <param name="nodes">节点集合</param>
        /// <param name="parentId">父节点</param>
        private void GetNodes(TreeNodeCollection nodes, string parentId, TreeNode node, DateTime startTime, DateTime endTime)
        {
            if (weldReportMains.Count() > 0)
            {
                if (node.Depth == 0)
                {
                    var instaRe = (from x in weldReportMains where x.BSU_ID == parentId select x.InstallationId).Distinct();
                    foreach (var q in instaRe)
                    {
                        TreeNode newNode = new TreeNode();

                        newNode.Value = q.ToString();
                        var install = BLL.InstallationService.GetInstallationByInstallationId(q.ToString());
                        if (install != null)
                        {
                            newNode.Text = install.InstallationName;
                        }
                        nodes.Add(newNode);
                    }
                }
                if (node.Depth == 1)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = this.txtReportDate.Value.Trim();
                    newNode.Value = this.txtReportDate.Value.Trim();
                    nodes.Add(newNode);
                }


                for (int i = 0; i < nodes.Count; i++)
                {
                    GetNodes(nodes[i].ChildNodes, nodes[i].Value, nodes[i], startTime, endTime);
                }
            }
        }

        private void getNodes(TreeNodeCollection nodes, string parentId, TreeNode node, DateTime startTime, DateTime endTime)
        {
            if (node.Depth == 2)
            {
                var days = (from x in weldReportMains
                            where x.InstallationId.ToString() == node.Parent.Value
                            && x.BSU_ID == node.Parent.Parent.Value
                            orderby x.JOT_WeldDate
                            select x.JOT_WeldDate).Distinct();
                foreach (var item in days)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = string.Format("{0:yyyy-MM-dd}", item);
                    newNode.Value = item.ToString();
                    nodes.Add(newNode);
                }
            }
            if (node.Depth == 3)
            {
                var dReports = from x in weldReportMains
                               where x.InstallationId.ToString() == node.Parent.Parent.Value
                               && x.BSU_ID == node.Parent.Parent.Parent.Value
                               && x.JOT_WeldDate == Convert.ToDateTime(parentId)
                               orderby x.JOT_DailyReportNo
                               select x;
                foreach (var item in dReports)
                {
                    TreeNode newNode = new TreeNode();
                    newNode.Text = item.JOT_DailyReportNo;
                    newNode.Value = item.DReportID;
                    nodes.Add(newNode);
                }
            }
        }
        #endregion

        protected void tvControlItem_SelectedNodeChanged(object sender, EventArgs e)
        {
            this.tvControlItem.SelectedNode.Expanded = true;
            if (this.tvControlItem.SelectedNode != null)
            {
                DateTime startTime = Convert.ToDateTime(this.txtReportDate.Value.Trim() + "-01");
                DateTime endTime = startTime.AddMonths(1);
                weldReportMains = (from x in Funs.DB.BO_WeldReportMain
                                   where x.JOT_WeldDate >= startTime && x.JOT_WeldDate < endTime && x.ProjectId == this.CurrUser.ProjectId
                                   select x).ToList();

                this.getNodes(this.tvControlItem.SelectedNode.ChildNodes, this.tvControlItem.SelectedValue, this.tvControlItem.SelectedNode, startTime, endTime);
                if (this.tvControlItem.SelectedNode.Depth == 4)
                {
                    this.tvControlItem.SelectedNodeStyle.ForeColor = System.Drawing.Color.DarkRed;
                    this.TextIsReadOnly(false);
                    this.ButtonIsEnabled(true);

                    DReportID = this.tvControlItem.SelectedValue;
                    jointInfos.Clear();
                    drpUnit.Items.Clear();
                    drpInstall.Items.Clear();
                    drpCHT_Tabler.Items.Clear();

                    Funs.PleaseSelect(this.drpUnit);
                    Funs.PleaseSelect(this.drpInstall);

                    var getunit = BLL.UnitService.GetUnit(this.CurrUser.UnitId);
                    if (getunit == null || getunit.UnitType == "1")
                    {
                        this.drpUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId));
                        this.drpInstall.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId));
                    }
                    else
                    {
                        this.drpUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                        this.drpInstall.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId, this.CurrUser.UnitId));
                    }

                    Funs.PleaseSelect(this.drpCHT_Tabler);
                    this.drpCHT_Tabler.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));

                    if (!string.IsNullOrEmpty(DReportID))
                    {
                        Model.BO_WeldReportMain report = BLL.WeldReportService.GetWeldReportByDReportID(DReportID);
                        if (report != null)
                        {
                            this.txtDReportID.Text = report.JOT_DailyReportNo;
                            if (!string.IsNullOrEmpty(report.BSU_ID))
                            {
                                this.drpUnit.SelectedValue = report.BSU_ID;
                            }
                            if (report.InstallationId != null)
                            {
                                this.drpInstall.SelectedValue = report.InstallationId.ToString();
                            }
                            if (report.JOT_WeldDate != null)
                            {
                                this.txtJOT_WeldDate.Text = string.Format("{0:yyyy-MM-dd}", report.JOT_WeldDate);
                            }
                            if (!string.IsNullOrEmpty(report.CHT_Tabler))
                            {
                                this.drpCHT_Tabler.SelectedValue = BLL.UserService.GetUsersByUserName(report.CHT_Tabler, this.CurrUser.ProjectId).First().UserId;
                            }
                            if (report.CHT_TableDate != null)
                            {
                                this.txtCHT_TableDate.Text = string.Format("{0:yyyy-MM-dd}", report.CHT_TableDate);
                            }
                            this.txtJOT_Remark.Text = report.JOT_Remark;
                            jointInfos = BLL.PW_JointInfoService.GetJointInfosByDReportID(DReportID);
                            jointInfos = (from x in jointInfos
                                          join y in Funs.DB.PW_IsoInfo on x.ISO_ID equals y.ISO_ID
                                          orderby y.ISO_IsoNo, x.JOT_JointNo
                                          select x).ToList();
                            if (jointInfos.Count > 0)
                            {
                                this.gvWeldReportDetail.Visible = true;
                                this.gvWeldReportDetail.DataSource = jointInfos;
                                this.gvWeldReportDetail.DataBind();
                                this.lblSize.Text = (from x in jointInfos select x.JOT_Size ?? 0).Sum().ToString("F");
                                this.lblDine.Text = (from x in jointInfos select x.JOT_DoneDin ?? 0).Sum().ToString("F");
                            }
                            else
                            {
                                this.gvWeldReportDetail.Visible = false;
                            }
                        }
                    }
                }
            }
            else
            {
                DReportID = null;
                this.gvWeldReportDetail.Visible = false;
            }
        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnDelete) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                if (!string.IsNullOrEmpty(DReportID))
                {
                    List<Model.PW_JointInfo> infos = BLL.PW_JointInfoService.GetJointInfosByDReportID(DReportID);
                    bool isPoint = false;
                    foreach (var p in infos)
                    {
                        if (!String.IsNullOrEmpty(p.PW_PointID))
                        {
                            isPoint = true;
                            break;
                        }
                    }

                    if (isPoint == false)
                    {
                        foreach (var item in infos)
                        {
                            item.DReportID = null;
                            item.JOT_CellWelder = null;
                            item.JOT_FloorWelder = null;
                            item.JOT_DoneDin = null;
                            BLL.PW_JointInfoService.UpdateJointInfoByDReport(item);
                        }
                        BLL.WeldReportService.DeleteWeldReportByDReportID(DReportID);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "删除焊接日报");
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('删除成功！');", true);
                        ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ReportSearch();</script>");
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('此日报已有点口数据，不能删除！');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择要删除的日报记录！')", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('您没有这个权限，请与管理员联系！')", true);
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPrint_Click(object sender, ImageClickEventArgs e)
        {
            if (this.tvControlItem.SelectedNode != null && this.tvControlItem.SelectedNode.Depth == 4)
            {
                string reportId = this.tvControlItem.SelectedNode.Value;
                string installation = this.tvControlItem.SelectedNode.Parent.Parent.Parent.Text;
                string unitname = this.tvControlItem.SelectedNode.Parent.Parent.Parent.Parent.Text;
                string projectName = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.ProjectId).ProjectName;
                string varValue = String.Empty;
                varValue = installation.Replace("/", ",") + "|" + unitname + "|" + projectName.Replace("/", ",");

                ClientScript.RegisterStartupScript(ClientScript.GetType(), "", "<script type='text/javascript'>JointInfoPrint('" + BLL.Const.JointReportDayReportId + "','" + reportId + "','" + varValue + "');</script>");
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择日报告！')", true);
                return;
            }
        }

        /// <summary>
        /// 根据单位获取其下的装置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void drpUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpInstall.Items.Clear();
            Funs.PleaseSelect(this.drpInstall);
            this.drpInstall.Items.AddRange(BLL.InstallationService.GetInstallationList(this.CurrUser.ProjectId, this.drpUnit.SelectedValue));
        }

        /// <summary>
        /// 导出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExport_Click(object sender, ImageClickEventArgs e)
        {
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ShowExport();</script>");
        }

        protected void imgbtnIn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("WeldReportDataIn.aspx");
        }
    }
}