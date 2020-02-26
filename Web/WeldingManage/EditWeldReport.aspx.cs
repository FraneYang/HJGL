using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.WeldingManage
{
    public partial class EditWeldReport : PPage
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
        /// 焊口集合
        /// </summary>
        private static List<Model.PW_JointInfo> jointInfos = new List<Model.PW_JointInfo>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {
                jointInfos = new List<Model.PW_JointInfo>();
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.WeldReportMenuId);
                Funs.PleaseSelect(this.drpUnit);
                this.drpUnit.Items.AddRange(BLL.UnitService.GetSubUnitNameList(this.CurrUser.ProjectId));
                Funs.PleaseSelect(this.drpCHT_Tabler);
                this.drpCHT_Tabler.Items.AddRange(BLL.UserService.GetUserList(this.CurrUser.ProjectId));
                DReportID = Request.Params["DReportID"];
                this.gvWeldReportDetail.Visible = false;
                if (!string.IsNullOrEmpty(DReportID))
                {
                    Model.BO_WeldReportMain report = BLL.WeldReportService.GetWeldReportByDReportID(DReportID);
                    this.txtDReportID.Text = report.JOT_DailyReportNo;
                    if (!string.IsNullOrEmpty(report.BSU_ID))
                    {
                        this.drpUnit.SelectedValue = report.BSU_ID;
                    }
                    if (report.JOT_WeldDate != null)
                    {
                        this.txtJOT_WeldDate.Value = string.Format("{0:yyyy-MM-dd}", report.JOT_WeldDate);
                    }
                    if (!string.IsNullOrEmpty(report.CHT_Tabler))
                    {
                        this.drpCHT_Tabler.SelectedValue = BLL.UserService.GetUsersByUserName(report.CHT_Tabler, this.CurrUser.ProjectId).First().UserId;
                    }
                    if (report.CHT_TableDate != null)
                    {
                        this.txtCHT_TableDate.Value = string.Format("{0:yyyy-MM-dd}", report.CHT_TableDate);
                    }
                    this.txtJOT_Remark.Text = report.JOT_Remark;
                    jointInfos = BLL.PW_JointInfoService.GetJointInfosByDReportID(DReportID);
                    jointInfos = (from x in jointInfos join y in Funs.DB.PW_IsoInfo on x.ISO_ID equals y.ISO_ID orderby y.ISO_IsoNo, x.JOT_JointNo select x).ToList();
                    if (jointInfos.Count > 0)
                    {
                        this.gvWeldReportDetail.Visible = true;
                        this.gvWeldReportDetail.DataSource = jointInfos;
                        this.gvWeldReportDetail.DataBind();
                    }
                }
                else
                {
                    this.ckAll.Checked = true;
                    this.ckBoth.Checked = true;
                    this.drpCHT_Tabler.SelectedValue = this.CurrUser.UserId;
                    this.txtJOT_WeldDate.Value = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                    this.txtCHT_TableDate.Value = string.Format("{0:yyyy-MM-dd}", DateTime.Now);
                }
            }
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void imgBtnSearch_Click(object sender, ImageClickEventArgs e)
        {
            string selectList = this.hdSelectList.Value;
            if (!string.IsNullOrEmpty(selectList))
            {
                List<string> infos = selectList.Split(',').ToList();
                List<string> removes = new List<string>();
                foreach (var i in infos)
                {
                    foreach (var j in jointInfos)
                    {
                        if (j.JOT_ID == i)
                        {
                            removes.Add(i);
                        }
                    }
                }
                if (removes.Count > 0)
                {
                    foreach (var item in removes)
                    {
                        infos.Remove(item);
                    }
                }
                foreach (var item in infos)
                {
                    Model.PW_JointInfo info = BLL.PW_JointInfoService.GetJointInfoByJotID(item);
                    jointInfos.Add(info);
                }
            }
            jointInfos = (from x in jointInfos join y in Funs.DB.PW_IsoInfo on x.ISO_ID equals y.ISO_ID orderby y.ISO_IsoNo, x.JOT_JointNo select x).ToList();
            if (jointInfos.Count > 0)
            {
                this.gvWeldReportDetail.Visible = true;
                this.gvWeldReportDetail.DataSource = jointInfos;
                this.gvWeldReportDetail.DataBind();
            }
        }

        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (ButtonList.Contains(BLL.Const.BtnSave) || this.CurrUser.Account == BLL.Const.AdminId)
            {
                if (this.gvWeldReportDetail.Rows.Count > 0)
                {
                    Model.BO_WeldReportMain report = new Model.BO_WeldReportMain();
                    report.ProjectId = this.CurrUser.ProjectId;
                    if (this.drpUnit.SelectedValue != "0")
                    {
                        report.BSU_ID = this.drpUnit.SelectedValue;
                    }
                    if (!string.IsNullOrEmpty(this.txtJOT_WeldDate.Value))
                    {
                        report.JOT_WeldDate = Convert.ToDateTime(this.txtJOT_WeldDate.Value);
                    }
                    if (!string.IsNullOrEmpty(this.txtDReportID.Text.Trim()))
                    {
                        report.JOT_DailyReportNo = this.txtDReportID.Text.Trim();
                    }
                    if (this.drpCHT_Tabler.SelectedValue != "0")
                    {
                        report.CHT_Tabler = this.drpCHT_Tabler.SelectedItem.Text;
                    }
                    if (!string.IsNullOrEmpty(this.txtJOT_WeldDate.Value))
                    {
                        report.CHT_TableDate = Convert.ToDateTime(this.txtCHT_TableDate.Value);
                    }
                    if (!string.IsNullOrEmpty(this.txtJOT_Remark.Text.Trim()))
                    {
                        report.JOT_Remark = this.txtJOT_Remark.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(DReportID))
                    {
                        report.DReportID = DReportID;
                        BLL.WeldReportService.UpdateWeldReport(report);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "修改焊接日报信息");
                    }
                    else
                    {
                        report.DReportID = SQLHelper.GetNewID(typeof(Model.BO_WeldReportMain));
                        BLL.WeldReportService.AddWeldReport(report);
                        BLL.LogService.AddLog(this.CurrUser.UserId, "添加焊接日报信息");
                    }
                    SaveList();
                    foreach (var item in jointInfos)
                    {
                        item.DReportID = report.DReportID;
                        item.JOT_JointStatus = "100";
                        BLL.PW_JointInfoService.UpdateJointInfoByDReport(item);
                   }                                                                                        
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('保存成功！');window.location.href='WeldReport.aspx'", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('焊口信息不能为空！')", true);
                    return;
                }
            }
        }

        protected void imgBtnSave_Click(object sender, ImageClickEventArgs e)
        {
            SaveList();
        }

        protected void SaveList()
        {
            jointInfos.Clear();
            for (int i = 0; i < this.gvWeldReportDetail.Rows.Count; i++)
            {
                TextBox txtJOT_JointNo = (TextBox)(this.gvWeldReportDetail.Rows[i].FindControl("txtJOT_JointNo"));
                HiddenField hdISO_ID = (HiddenField)(this.gvWeldReportDetail.Rows[i].FindControl("hdISO_ID"));
                HiddenField hdJOT_CellWelder = (HiddenField)(this.gvWeldReportDetail.Rows[i].FindControl("hdJOT_CellWelder"));
                HiddenField hdJOT_FloorWelder = (HiddenField)(this.gvWeldReportDetail.Rows[i].FindControl("hdJOT_FloorWelder"));
                DropDownList drpWLO_Code = (DropDownList)(this.gvWeldReportDetail.Rows[i].FindControl("drpWLO_Code"));
                DropDownList drpJOT_JointAttribute = (DropDownList)(this.gvWeldReportDetail.Rows[i].FindControl("drpJOT_JointAttribute"));
                TextBox txtJOT_DoneDin = (TextBox)(this.gvWeldReportDetail.Rows[i].FindControl("txtJOT_DoneDin"));
                Label txtJOT_Size = (Label)(this.gvWeldReportDetail.Rows[i].FindControl("txtJOT_Size"));
                HiddenField hdJOT_ID = (HiddenField)(this.gvWeldReportDetail.Rows[i].FindControl("hdJOT_ID"));
                Model.PW_JointInfo info = new Model.PW_JointInfo();
                info.JOT_ID = hdJOT_ID.Value;
                info.ISO_ID = hdISO_ID.Value;
                info.JOT_JointNo = txtJOT_JointNo.Text.Trim();
                if (!string.IsNullOrEmpty(hdJOT_CellWelder.Value))
                {
                    info.JOT_CellWelder = hdJOT_CellWelder.Value;
                }
                if (!string.IsNullOrEmpty(hdJOT_FloorWelder.Value))
                {
                    info.JOT_FloorWelder = hdJOT_FloorWelder.Value;
                }
                info.WLO_Code = drpWLO_Code.SelectedValue;
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

        protected void btnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("WeldReport.aspx");
        }

        /// <summary>
        /// 当 GridView 内生成事件时激发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvWeldReportDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string JOT_ID = e.CommandArgument.ToString();
            if (e.CommandName == "del")
            {
                foreach (Model.PW_JointInfo info in jointInfos)
                {
                    if (info.JOT_ID == JOT_ID)
                    {
                        jointInfos.Remove(info);
                        break;
                    }
                }
            }
            this.gvWeldReportDetail.DataSourceID = null;
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
                HiddenField hdJOT_CellWelder = (HiddenField)(this.gvWeldReportDetail.Rows[i].FindControl("hdJOT_CellWelder"));
                HiddenField hdJOT_FloorWelder = (HiddenField)(this.gvWeldReportDetail.Rows[i].FindControl("hdJOT_FloorWelder"));
                if (this.ckAll.Checked == true && this.ckBoth.Checked == true)
                {
                    ((TextBox)(this.gvWeldReportDetail.Rows[i].FindControl("txtJOT_CellWelder"))).Attributes["onfocus"] = "GetPersonAll1()";
                    ((TextBox)(this.gvWeldReportDetail.Rows[i].FindControl("txtJOT_FloorWelder"))).Attributes["onfocus"] = "GetPersonAll1()";
                }
                else if (this.ckAll.Checked == true && this.ckBoth.Checked == false)
                {
                    ((TextBox)(this.gvWeldReportDetail.Rows[i].FindControl("txtJOT_CellWelder"))).Attributes["onfocus"] = "GetPersonAll2_C()";
                    ((TextBox)(this.gvWeldReportDetail.Rows[i].FindControl("txtJOT_FloorWelder"))).Attributes["onfocus"] = "GetPersonAll2_F()";
                }
                else if (this.ckAll.Checked == false && this.ckBoth.Checked == true)
                {
                    ((TextBox)(this.gvWeldReportDetail.Rows[i].FindControl("txtJOT_CellWelder"))).Attributes["onfocus"] = "GetPerson3('" + txtJOT_CellWelder.ClientID + "','" + hdJOT_CellWelder.ClientID + "','" + txtJOT_FloorWelder.ClientID + "','" + hdJOT_FloorWelder.ClientID + "')";
                    ((TextBox)(this.gvWeldReportDetail.Rows[i].FindControl("txtJOT_FloorWelder"))).Attributes["onfocus"] = "GetPerson3('" + txtJOT_CellWelder.ClientID + "','" + hdJOT_CellWelder.ClientID + "','" + txtJOT_FloorWelder.ClientID + "','" + hdJOT_FloorWelder.ClientID + "')";
                }
                else
                {
                    ((TextBox)(this.gvWeldReportDetail.Rows[i].FindControl("txtJOT_CellWelder"))).Attributes["onfocus"] = "GetPerson1('" + txtJOT_CellWelder.ClientID + "','" + hdJOT_CellWelder.ClientID + "')";
                    ((TextBox)(this.gvWeldReportDetail.Rows[i].FindControl("txtJOT_FloorWelder"))).Attributes["onfocus"] = "GetPerson2('" + txtJOT_FloorWelder.ClientID + "','" + hdJOT_FloorWelder.ClientID + "')";
                }
            }
        }

        /// <summary>
        /// 在创建行时激发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvWeldReportDetail_RowCreated(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    DropDownList drpCheckType = ((DropDownList)(e.Row.FindControl("drpCheckType")));
            //    DropDownList drpUnitId = ((DropDownList)(e.Row.FindControl("drpUnitId")));
            //    DropDownList drpHandleResult = ((DropDownList)(e.Row.FindControl("drpHandleResult")));
            //    drpUnitId.Items.AddRange(BLL.UnitService.GetSubUnitList());
            //}
        }

        protected string GetBAW_ID(object ISO_ID)
        {
            if (ISO_ID != null)
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
            if (ISO_ID != null)
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
            if (ISO_ID != null)
            {
                return BLL.DetectionService.GetNDTRateByNDTRID(BLL.PW_IsoInfoService.GetIsoInfoByIsoInfoId(ISO_ID.ToString()).NDTR_ID).NDTR_Name;
            }
            else
            {
                return "";
            }
        }

        protected string GetPersonNameByJOT_CellWelder(object JOT_CellWelder)
        {
            if (JOT_CellWelder != null)
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
            if (JOT_FloorWelder != null)
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
                    return info.JOT_Size.ToString();
                }
                else
                {
                    return info.JOT_DoneDin.ToString();
                }
            }
            else
            {
                return "";
            }
        }

        protected void imgSearch_Click(object sender, ImageClickEventArgs e)
        {
            if (this.drpUnit.SelectedValue == "0")
            {
                ScriptManager.RegisterStartupScript(this, typeof(string), "_alert", "alert('请选择单位！')", true);
                return;
            }
            else
            {
                ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script type='text/javascript'>ShowSearch('" + this.drpUnit.SelectedValue + "');</script>");
            }
        }

        protected void imgBtnAll1_Click(object sender, ImageClickEventArgs e)
        {
            string id = this.hdAll1.Value;
            if (!string.IsNullOrEmpty(id))
            {
                foreach (var item in jointInfos)
                {
                    item.JOT_CellWelder = id;
                    item.JOT_FloorWelder = id;
                }
                this.gvWeldReportDetail.DataSource = jointInfos;
                this.gvWeldReportDetail.DataBind();
            }
        }

        protected void imgBtnAll2_C_Click(object sender, ImageClickEventArgs e)
        {
            string id = this.hdAll1.Value;
            if (!string.IsNullOrEmpty(id))
            {
                foreach (var item in jointInfos)
                {
                    item.JOT_CellWelder = id;
                }
                this.gvWeldReportDetail.DataSource = jointInfos;
                this.gvWeldReportDetail.DataBind();
            }
        }

        protected void imgBtnAll2_F_Click(object sender, ImageClickEventArgs e)
        {
            string id = this.hdAll1.Value;
            if (!string.IsNullOrEmpty(id))
            {
                foreach (var item in jointInfos)
                {
                    item.JOT_FloorWelder = id;
                }
                this.gvWeldReportDetail.DataSource = jointInfos;
                this.gvWeldReportDetail.DataBind();
            }
        }

        protected void imgBtnAll3_Click(object sender, ImageClickEventArgs e)
        {
            SaveList();
            this.gvWeldReportDetail.DataSource = jointInfos;
            this.gvWeldReportDetail.DataBind();
        }
    }
}