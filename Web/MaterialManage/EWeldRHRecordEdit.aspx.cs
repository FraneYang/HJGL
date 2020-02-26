using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.MaterialManage
{
    public partial class EWeldRHRecordEdit : PPage
    {
        /// <summary>
        /// 主键
        /// </summary>
        public string EWeldRHRecordId
        {
            get
            {
                return (string)ViewState["EWeldRHRecordId"];
            }
            set
            {
                ViewState["EWeldRHRecordId"] = value;
            }
        }

        /// <summary>
        /// 按钮权限
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
        /// 焊材库温湿度记录明细集合
        /// </summary>
        private static List<Model.EWeldRHRecordItem> eWeldRHRecordItems = new List<Model.EWeldRHRecordItem>();

        /// <summary>
        /// 加载页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack && this.CurrUser != null)
            {
                string roleId = BLL.UserService.GetRoleIdByUserId(this.CurrUser.UserId);
                this.ButtonList = BLL.ButtonPowerService.GetButtonPowerList(roleId, BLL.Const.EWeldRHRecordMenuId);

                this.lblProjectName.Text = BLL.ProjectService.GetProjectByProjectId(this.CurrUser.ProjectId).ProjectName;
                this.txtCompileDate.Value = string.Format("{0:yyyy-MM-dd}", DateTime.Now);

                EWeldRHRecordId = Request.Params["eWeldRHRecordId"];
                if (!string.IsNullOrEmpty(EWeldRHRecordId))
                {
                    Model.EWeldRHRecord eWeldRHRecord = BLL.EWeldRHRecordService.GetEWeldRHRecordByID(EWeldRHRecordId);
                    this.txtEWeldRHRecordCode.Text = eWeldRHRecord.EWeldRHRecordCode;
                    this.txtCompileDate.Value = Convert.ToString(string.Format("{0:yyyy-MM-dd}", eWeldRHRecord.CompileDate));
                    this.lblProjectName.Text = BLL.ProjectService.GetProjectByProjectId(eWeldRHRecord.ProjectId).ProjectName;

                    eWeldRHRecordItems = BLL.EWeldRHRecordService.GetEWeldRHRecordItemByID(EWeldRHRecordId);
                }
            }
        }

        /// <summary>
        /// GridView行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvEWeldRHRecord_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string eWeldRHRecordItemId = e.CommandArgument.ToString();
            if (e.CommandName=="del")
            {
                foreach (Model.EWeldRHRecordItem item in eWeldRHRecordItems)
                {
                    if (item.EWeldRHRecordItemId==eWeldRHRecordItemId)
                    {
                        eWeldRHRecordItems.Remove(item);
                        break;
                    }
                }
            }
            this.gvEWeldRHRecord.DataSourceID = null;
            this.gvEWeldRHRecord.DataSource = eWeldRHRecordItems;
            this.gvEWeldRHRecord.DataBind();
        }

        /// <summary>
        /// 绑定GridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void gvEWeldRHRecord_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtEWeldRHRecordMonth = (TextBox)e.Row.FindControl("txtEWeldRHRecordMonth");
                TextBox txtEWeldRHRecordDay = (TextBox)e.Row.FindControl("txtEWeldRHRecordDay");
                TextBox txtEWeldRHRecordHours = (TextBox)e.Row.FindControl("txtEWeldRHRecordHours");
                TextBox txtRoomTemperature = (TextBox)e.Row.FindControl("txtRoomTemperature");
                TextBox txtHumidity = (TextBox)e.Row.FindControl("txtHumidity");
                TextBox txtRecordMan = (TextBox)e.Row.FindControl("txtRecordMan");
                TextBox txtRemark = (TextBox)e.Row.FindControl("txtRemark");

                if (this.gvEWeldRHRecord.Rows.Count != 0)
                {
                    if (!string.IsNullOrEmpty(eWeldRHRecordItems[e.Row.RowIndex].EWeldRHRecordItemId))
                    {
                        txtEWeldRHRecordMonth.Text = Convert.ToString(eWeldRHRecordItems[e.Row.RowIndex].EWeldRHRecordMonth);
                        txtEWeldRHRecordDay.Text = Convert.ToString(eWeldRHRecordItems[e.Row.RowIndex].EWeldRHRecordDay);
                        txtEWeldRHRecordHours.Text = Convert.ToString(eWeldRHRecordItems[e.Row.RowIndex].EWeldRHRecordHours);
                        txtRoomTemperature.Text = Convert.ToString(eWeldRHRecordItems[e.Row.RowIndex].RoomTemperature);
                        txtHumidity.Text = Convert.ToString(eWeldRHRecordItems[e.Row.RowIndex].Humidity);
                        txtRecordMan.Text = eWeldRHRecordItems[e.Row.RowIndex].RecordMan;
                        txtRemark.Text = eWeldRHRecordItems[e.Row.RowIndex].Remark;
                    }
                }
            }
        }
        /// <summary>
        /// 增加行按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAddItem_Click(object sender, ImageClickEventArgs e)
        {
            Model.EWeldRHRecordItem item = new Model.EWeldRHRecordItem();
            jerqueSaveList();
            eWeldRHRecordItems.Add(item);
            this.gvEWeldRHRecord.DataSourceID = null;
            this.gvEWeldRHRecord.DataSource = eWeldRHRecordItems;
            this.gvEWeldRHRecord.DataBind();
        }
        /// <summary>
        /// 检查并保存焊材库温湿度记录
        /// </summary>
        private void jerqueSaveList()
        {
            eWeldRHRecordItems.Clear();
            int rowsCount = this.gvEWeldRHRecord.Rows.Count;
            for (int i = 0; i < rowsCount; i++)
            {
                Model.EWeldRHRecordItem item = new Model.EWeldRHRecordItem();
                string eWeldRHRecordMonth = ((TextBox)(this.gvEWeldRHRecord.Rows[i].FindControl("txtEWeldRHRecordMonth"))).Text;//月
                string eWeldRHRecordDay = ((TextBox)(this.gvEWeldRHRecord.Rows[i].FindControl("txtEWeldRHRecordDay"))).Text;//日
                string eWeldRHRecordHours = ((TextBox)(this.gvEWeldRHRecord.Rows[i].FindControl("txtEWeldRHRecordHours"))).Text;//时
                string roomTemperature = ((TextBox)(this.gvEWeldRHRecord.Rows[i].FindControl("txtRoomTemperature"))).Text;//室温
                string humidity = ((TextBox)(this.gvEWeldRHRecord.Rows[i].FindControl("txtHumidity"))).Text;//湿度
                string recordMan = ((TextBox)(this.gvEWeldRHRecord.Rows[i].FindControl("txtRecordMan"))).Text;//记录人（保管员）
                string remark = ((TextBox)(this.gvEWeldRHRecord.Rows[i].FindControl("txtRemark"))).Text;//备注

                if (!string.IsNullOrEmpty(eWeldRHRecordMonth))
                {
                    item.EWeldRHRecordMonth = Convert.ToInt32(eWeldRHRecordMonth);
                }
                if (!string.IsNullOrEmpty(eWeldRHRecordDay))
                {
                    item.EWeldRHRecordDay = Convert.ToInt32(eWeldRHRecordDay);
                }
                if (!string.IsNullOrEmpty(eWeldRHRecordHours))
                {
                    item.EWeldRHRecordHours = Convert.ToInt32(eWeldRHRecordHours);
                }
                if (!string.IsNullOrEmpty(roomTemperature))
                {
                    item.RoomTemperature = Convert.ToDecimal(roomTemperature);
                }
                if (!string.IsNullOrEmpty(humidity))
                {
                    item.Humidity = Convert.ToDecimal(humidity);
                }               
                item.RecordMan = recordMan;
                item.Remark = remark;

                eWeldRHRecordItems.Add(item);
            }
        }
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ButtonList.Contains(BLL.Const.BtnSave)||this.CurrUser.Account==BLL.Const.AdminId)
            {
                Model.EWeldRHRecord eWeldRHRecord = new Model.EWeldRHRecord();
                eWeldRHRecord.EWeldRHRecordCode = this.txtEWeldRHRecordCode.Text.Trim();
                eWeldRHRecord.EWeldRHRecordDate = DateTime.Now;
                eWeldRHRecord.UnitId = null;
                eWeldRHRecord.CompileMan = this.CurrUser.UserId;
                if (!string.IsNullOrEmpty(this.txtCompileDate.Value))
                {
                     eWeldRHRecord.CompileDate = Convert.ToDateTime(this.txtCompileDate.Value);
                }
                eWeldRHRecord.ProjectId = this.CurrUser.ProjectId;

                if (!string.IsNullOrEmpty(EWeldRHRecordId))
                {
                    eWeldRHRecord.EWeldRHRecordId = EWeldRHRecordId;
                    BLL.EWeldRHRecordService.UpdateEWeldRHRecord(eWeldRHRecord);
                    BLL.EWeldRHRecordService.DeleteEWeldRHRecordItem(EWeldRHRecordId);
                    jerqueSaveList();
                    foreach (var item in eWeldRHRecordItems)
                    {
                        item.EWeldRHRecordId = eWeldRHRecord.EWeldRHRecordId;
                        BLL.EWeldRHRecordService.AddEWeldRHRecordItem(item);
                    }
                    BLL.LogService.AddLog(this.CurrUser.UserId, "修改焊材库温湿度记录！");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('保存成功！');window.location.href='EWeldRHRecord.aspx'", true);
                }
                else
                {
                    eWeldRHRecord.EWeldRHRecordId = SQLHelper.GetNewID(typeof(Model.EWeldRHRecord));
                    BLL.EWeldRHRecordService.AddEWeldRHRecord(eWeldRHRecord);
                    jerqueSaveList();
                    foreach (var item in eWeldRHRecordItems)
                    {
                        item.EWeldRHRecordId = eWeldRHRecord.EWeldRHRecordId;
                        BLL.EWeldRHRecordService.AddEWeldRHRecordItem(item);
                    }
                    BLL.LogService.AddLog(this.CurrUser.UserId, "添加焊材库温湿度记录");
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "", "alert('保存成功！');window.location.href='EWeldRHRecord.aspx'", true);
                }
            }
        }
        /// <summary>
        /// 返回按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReturn_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("EWeldRHRecord.aspx");
        }
    }
}