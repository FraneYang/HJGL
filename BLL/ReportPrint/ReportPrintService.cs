using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Data.Linq;
using System.Web.Security;
using System.Web.UI.WebControls;
using Model;
using BLL;
using System.Collections.Generic;

namespace BLL
{
    public class ReportPrintService
    {
        /// <summary>
        /// 打印报表列表
        /// </summary>
        /// <returns></returns>
        public static ListItem[] PrintReport()
        {
            ListItem[] lis = new ListItem[17];
            lis[0] = new ListItem("管道焊接工作记录", BLL.Const.JointInfoReportId);
            lis[1] = new ListItem("管道焊口日报表", BLL.Const.JointReportDayReportId);
            lis[2] = new ListItem("管道点口日报表", BLL.Const.PointReportDayReportId);
            lis[3] = new ListItem("无损检测委托单", BLL.Const.TrustReportId);
            lis[4] = new ListItem("无损检测结果通知单", BLL.Const.CheckReportId);
            lis[5] = new ListItem("硬度检测日委托单", BLL.Const.HardCheckReportId);
            lis[6] = new ListItem("合格焊工登记表", BLL.Const.WelderRecordReportId);
            lis[7] = new ListItem("无损检测委托单(2)", BLL.Const.TrustReport2Id);
            lis[8] = new ListItem("无损检测委托单(第三方)", BLL.Const.TrustReport3Id);
            lis[9] = new ListItem("无损检测委托单(神化)", BLL.Const.TrustReport4Id);
            lis[10] = new ListItem("管道焊接接头热处理报告（一）", BLL.Const.HotHandle1ReportId);
            lis[11] = new ListItem("管道焊接接头热处理报告（二）", BLL.Const.HotHandle2ReportId);
            lis[12] = new ListItem("管道对接焊接接头报检/检查记录", BLL.Const.WeldJointCheckReportId);
            lis[13] = new ListItem("射线结果确认表", BLL.Const.RTCheckResultReportId);
            lis[14] = new ListItem("管道焊接接头射线检测比例确认表（一）", BLL.Const.WeldJointRTCheck1ReportId);
            lis[15] = new ListItem("管道焊接接头射线检测比例确认表（二）", BLL.Const.WeldJointRTCheck2ReportId);
            lis[16] = new ListItem("管道系统压力试验记录", BLL.Const.TestPackageManageReportId);
            


            return lis;
        }

        public static ListItem[] HotHandleSelectPrint()
        {
            ListItem[] lis = new ListItem[2];
            lis[0] = new ListItem("管道焊接接头热处理报告（一）", BLL.Const.HotHandle1ReportId);
            lis[1] = new ListItem("管道焊接接头热处理报告（二）", BLL.Const.HotHandle2ReportId);
            return lis;
        }

        public static ListItem[] NDTCheckSelectPrint()
        {
            ListItem[] lis = new ListItem[2];
            lis[0] = new ListItem("无损检测结果通知单", BLL.Const.CheckReportId);
            lis[1] = new ListItem("管道对接焊接接头报检/检查记录", BLL.Const.WeldJointCheckReportId);
            return lis;
        }

        public static ListItem[] TestPackageSelectPrint()
        {
            ListItem[] lis = new ListItem[3];
            lis[0] = new ListItem("射线结果确认表", BLL.Const.RTCheckResultReportId);
            lis[1] = new ListItem("管道焊接接头射线检测比例确认表（一）", BLL.Const.WeldJointRTCheck1ReportId);
            lis[2] = new ListItem("管道焊接接头射线检测比例确认表（二）", BLL.Const.WeldJointRTCheck2ReportId);
            return lis;
        }
    }
}
