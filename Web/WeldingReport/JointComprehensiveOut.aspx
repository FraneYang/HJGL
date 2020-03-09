<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JointComprehensiveOut.aspx.cs"
    Inherits="Web.WeldingReport.JointComprehensiveOut" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">    
    <title>焊口综合信息</title>
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
     <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" runat="server" width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 100%; background: url('../Images/bg-1.gif')">
                <table id="tabbtn" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" valign="middle" style="width: 80%; font-size: 11pt; font-weight: bold">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;焊口综合信息
                        </td>
                        <td align="right">
                         <asp:ImageButton ID="btnConfirm" runat="server" ImageUrl="~/Images/confirm.gif"  
                                OnClick="btnOut_Click" />
                             <asp:ImageButton ID="btnReturn" runat="server" ImageUrl="~/Images/Return.gif"  
                                OnClick="btnReturn_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>      
    </table>
    <div id="div1" runat="server" style="overflow: auto;height:500px">
        <table id="Table2" width="4500px" cellpadding="0" cellspacing="0" runat="server" >
            <tr>
                <td width="100%">
                    <asp:GridView ID="gvJointCompre" runat="server" AllowPaging="True" AllowSorting="True"
                        PageSize="20" AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%"
                        DataSourceID="ObjectDataSource1" OnDataBound="gvJointCompre_DataBound">
                        <AlternatingRowStyle CssClass="GridBgColr" />
                        <Columns>  
                             <asp:BoundField DataField="UnitCode" HeaderText="单位代码" />
                            <asp:BoundField DataField="WorkAreaCode" HeaderText="工区编码" />
                            <asp:BoundField DataField="ISO_IsoNo" HeaderText="管线代号" />
                            <asp:BoundField DataField="JOT_JointNo" HeaderText="焊口代号" />
                            <asp:BoundField DataField="STE_Code" HeaderText="材质1代号" />
                            <asp:BoundField DataField="STE_Code2" HeaderText="材质2代号" />
                            <asp:BoundField DataField="NDTR_Code" HeaderText="探伤比例代号" />
                            <asp:BoundField DataField="JOTY_Code" HeaderText="焊缝类型代号" />
                            <asp:BoundField DataField="WLO_CodeName" HeaderText="焊接区域*" />
                            <asp:BoundField DataField="JOT_JointAttribute" HeaderText="焊口属性" />
                            <asp:BoundField DataField="JOT_Size" HeaderText="达因数" />
                            <asp:BoundField DataField="JOT_JointDesc" HeaderText="规格" />
                            <asp:BoundField DataField="JOT_Sch" HeaderText="壁厚" />
                            <asp:BoundField DataField="WME_Code" HeaderText="焊接方法代码" />
                             <asp:BoundField DataField="ISO_TestPress" HeaderText="试验压力" />
                            <asp:BoundField DataField="WMT_MatCode" HeaderText="焊条代号" />
                            <asp:BoundField DataField="WMT_MatCode2" HeaderText="焊丝代号" />
                            <asp:BoundField DataField="SER_Code" HeaderText="介质代号" />
                            <asp:BoundField DataField="ISO_IsoNumber" HeaderText="单线图号" />
                            <asp:BoundField DataField="ISO_DesignPress" HeaderText="设计压力" />
                            <asp:BoundField DataField="ISO_DesignTemperature" HeaderText="设计温度" />
                            <asp:BoundField DataField="JST_Code" HeaderText="坡口代号" />
                            <asp:BoundField DataField="ISC_IsoCode" HeaderText="管线等级代号" />
                            <asp:BoundField DataField="COM_Code" HeaderText="组件一代号" />
                            <asp:BoundField DataField="COM_Code2" HeaderText="组件二代号" />
                            <asp:BoundField DataField="JOT_HeartNo1" HeaderText="炉批号一" />
                            <asp:BoundField DataField="JOT_HeartNo2" HeaderText="炉批号二" />
                             <asp:BoundField DataField="JOT_BelongPipe" HeaderText="所属管段" />
                            <asp:BoundField DataField="JOT_PrepareTemp" HeaderText="预热温度" />
                            <asp:BoundField DataField="IS_ProessName" HeaderText="热处理*" />
                            <asp:BoundField DataField="JOT_HotRpt" HeaderText="热处理编号" />
                            <asp:BoundField DataField="JOT_Location" HeaderText="焊接位置" />
                            <asp:BoundField DataField="JOT_Dia" HeaderText="外径" />
                            <asp:BoundField DataField="ISO_HardnessRateName" HeaderText="硬度检测比例*" />
                            <asp:BoundField DataField="NDT_Code" HeaderText="探伤类型代号" />
                            <asp:BoundField DataField="ISO_NDTClass" HeaderText="合格等级" />
                            <asp:BoundField DataField="IsBigName" HeaderText="大管*" />
                            <asp:BoundField DataField="PipeNumber" HeaderText="管道编号" />
                            <asp:BoundField DataField="ISO_CwpNo" HeaderText="试压包编号" />
                            <asp:BoundField DataField="JOT_DailyReportNo" HeaderText="日报编号" />
                            <asp:BoundField DataField="JOT_WeldDate" HeaderText="焊接日期" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="WED_Code" HeaderText="打底焊工代号" />
                            <asp:BoundField DataField="WED_Code2" HeaderText="盖面焊工代号" />
                            <asp:BoundField DataField="PW_PointNo" HeaderText="点口单号" />
                            <asp:BoundField DataField="PW_PointDate" HeaderText="点口日期" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="CH_TrustCode" HeaderText="无损委托单号" />
                            <asp:BoundField DataField="CH_TrustDate" HeaderText="无损委托日期" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="CHT_CheckCode" HeaderText="探伤单号" />
                            <asp:BoundField DataField="CHT_CheckDate" HeaderText="探伤日期"  DataFormatString="{0:d}" />
                            <asp:BoundField DataField="CHT_FilmDate" HeaderText="拍片日期" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="CHT_ReportDate" HeaderText="报告日期" DataFormatString="{0:d}" />
                            <asp:BoundField DataField="CHT_TotalFilm" HeaderText="拍片总数" />
                            <asp:BoundField DataField="CHT_PassFilm" HeaderText="合格片数" />
                            <asp:BoundField DataField="CHT_CheckNo" HeaderText="报告编号" />
                            <asp:BoundField DataField="CHT_FilmSpecifications" HeaderText="拍片规格" />
                        </Columns>
                        <HeaderStyle CssClass="GridBgColr" />
                        <RowStyle CssClass="GridRow" />
                        <PagerTemplate>
                            <uc1:GridNavgator ID="GridNavgator1" runat="server" />
                        </PagerTemplate>
                        <PagerStyle HorizontalAlign="Left" />
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetListData"
                        TypeName="BLL.JointComprehensiveOutService" SelectCountMethod="GetListCount" OnSelecting="ObjectDataSource1_Selecting"
                        EnablePaging="True" EnableCaching="false">
                        <SelectParameters>
                            <asp:Parameter Name="values" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    //var width = parent.parent.document.getElementById("center").offsetWidth;
    //if (width > 2000) {
    //    $("#Table2").width(width);
    //}
    //var height =  parent.parent.document.getElementById("center").offsetHeight;
    //var table1Height = $("#Table1").height();
    //var hei = height - table1Height - 5;
    //$("#div1").height(hei);
</script>
