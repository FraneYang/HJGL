using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace Web.ReportPrint
{
    public partial class PrintDesigner : PPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Funs.PleaseSelect(this.drpPrintReport);
                this.drpPrintReport.Items.AddRange(BLL.ReportPrintService.PrintReport());

            }
        }

        protected void BtnReportDesigner_Click(object sender, EventArgs e)
        {
            if (this.drpPrintReport.SelectedValue.Trim() == BLL.Const.JointInfoReportId)
            {
                BLL.LogService.AddLog(this.CurrUser.UserId, "设计管道焊接工作记录报表");
                Response.Redirect("ExPrintSet.aspx?reportId=" + BLL.Const.JointInfoReportId + "&reportName=管道焊接工作记录报表");
            }

            if (this.drpPrintReport.SelectedValue.Trim() == BLL.Const.JointReportDayReportId)
            {
                BLL.LogService.AddLog(this.CurrUser.UserId, "设计管道焊口日报表");
                Response.Redirect("ExPrintSet.aspx?reportId=" + BLL.Const.JointReportDayReportId + "&reportName=管道焊口日报表");
            }

            if (this.drpPrintReport.SelectedValue.Trim() == BLL.Const.PointReportDayReportId)
            {
                BLL.LogService.AddLog(this.CurrUser.UserId, "设计管道点口日报表");
                Response.Redirect("ExPrintSet.aspx?reportId=" + BLL.Const.PointReportDayReportId + "&reportName=管道点口日报表");
            }

            if (this.drpPrintReport.SelectedValue.Trim() == BLL.Const.TrustReportId)
            {
                BLL.LogService.AddLog(this.CurrUser.UserId, "设计无损检测委托单报表");
                Response.Redirect("ExPrintSet.aspx?reportId=" + BLL.Const.TrustReportId + "&reportName=无损检测委托单");
            }

            if (this.drpPrintReport.SelectedValue.Trim() == BLL.Const.CheckReportId)
            {
                BLL.LogService.AddLog(this.CurrUser.UserId, "设计无损检测结果通知单报表");
                Response.Redirect("ExPrintSet.aspx?reportId=" + BLL.Const.CheckReportId + "&reportName=无损检测结果通知单");
            }

            if (this.drpPrintReport.SelectedValue.Trim() == BLL.Const.HardCheckReportId)
            {
                BLL.LogService.AddLog(this.CurrUser.UserId, "设计硬度检测日委托单表");
                Response.Redirect("ExPrintSet.aspx?reportId=" + BLL.Const.HardCheckReportId + "&reportName=硬度检测日委托单");
            }

            if (this.drpPrintReport.SelectedValue.Trim() == BLL.Const.WelderRecordReportId)
            {
                BLL.LogService.AddLog(this.CurrUser.UserId, "设计合格焊工登记表");
                Response.Redirect("ExPrintSet.aspx?reportId=" + BLL.Const.WelderRecordReportId + "&reportName=合格焊工登记表");
            }

            if (this.drpPrintReport.SelectedValue.Trim() == BLL.Const.TrustReport2Id)
            {
                BLL.LogService.AddLog(this.CurrUser.UserId, "设计无损检测委托单(2)报表");
                Response.Redirect("ExPrintSet.aspx?reportId=" + BLL.Const.TrustReport2Id + "&reportName=无损检测委托单(2)报表");
            }

            if (this.drpPrintReport.SelectedValue.Trim() == BLL.Const.TrustReport3Id)
            {
                BLL.LogService.AddLog(this.CurrUser.UserId, "设计无损检测委托单(第三方)报表");
                Response.Redirect("ExPrintSet.aspx?reportId=" + BLL.Const.TrustReport3Id + "&reportName=无损检测委托单(第三方)报表");
            }

            if (this.drpPrintReport.SelectedValue.Trim() == BLL.Const.TrustReport4Id)
            {
                BLL.LogService.AddLog(this.CurrUser.UserId, "设计无损检测委托单(神化)报表");
                Response.Redirect("ExPrintSet.aspx?reportId=" + BLL.Const.TrustReport4Id + "&reportName=无损检测委托单(神化)报表");
            }

            if (this.drpPrintReport.SelectedValue.Trim() == BLL.Const.HotHandle1ReportId)
            {
                BLL.LogService.AddLog(this.CurrUser.UserId, "设计管道焊接接头热处理报告（一）");
                Response.Redirect("ExPrintSet.aspx?reportId=" + BLL.Const.HotHandle1ReportId + "&reportName=管道焊接接头热处理报告（一）表");
            }

            if (this.drpPrintReport.SelectedValue.Trim() == BLL.Const.HotHandle2ReportId)
            {
                BLL.LogService.AddLog(this.CurrUser.UserId, "设计管道焊接接头热处理报告（二）");
                Response.Redirect("ExPrintSet.aspx?reportId=" + BLL.Const.HotHandle2ReportId + "&reportName=管道焊接接头热处理报告（二）表");
            }


            if (this.drpPrintReport.SelectedValue.Trim() == BLL.Const.WeldJointCheckReportId)
            {
                BLL.LogService.AddLog(this.CurrUser.UserId, "设计管道对接焊接接头报检/检查记录");
                Response.Redirect("ExPrintSet.aspx?reportId=" + BLL.Const.WeldJointCheckReportId + "&reportName=管道对接焊接接头报检/检查记录表");
            }

            if (this.drpPrintReport.SelectedValue.Trim() == BLL.Const.RTCheckResultReportId)
            {
                BLL.LogService.AddLog(this.CurrUser.UserId, "设计射线结果确认报表");
                Response.Redirect("ExPrintSet.aspx?reportId=" + BLL.Const.RTCheckResultReportId + "&reportName=射线结果确认报表");
            }

            if (this.drpPrintReport.SelectedValue.Trim() == BLL.Const.WeldJointRTCheck1ReportId)
            {
                BLL.LogService.AddLog(this.CurrUser.UserId, "设计管道焊接接头射线检测比例确认表（一）");
                Response.Redirect("ExPrintSet.aspx?reportId=" + BLL.Const.WeldJointRTCheck1ReportId + "&reportName=管道焊接接头射线检测比例确认表（一）");
            }

            if (this.drpPrintReport.SelectedValue.Trim() == BLL.Const.WeldJointRTCheck2ReportId)
            {
                BLL.LogService.AddLog(this.CurrUser.UserId, "设计管道焊接接头射线检测比例确认表（二）");
                Response.Redirect("ExPrintSet.aspx?reportId=" + BLL.Const.WeldJointRTCheck2ReportId + "&reportName=管道焊接接头射线检测比例确认表（二）");
            }
        }
    }
}