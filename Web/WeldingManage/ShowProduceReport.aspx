<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowProduceReport.aspx.cs"
    Inherits="Web.WeldingManage.ShowProduceReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>工艺评定报告</title>
    <base target="_self" />
    <link href="../Styles/Style.css" type="text/css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <style type="text/css">
        tabel
        {
            border-collapse: collapse;
        }
        td
        {
            border: solid #000 1px;
            border-collapse: collapse;
        }
    </style>
</head>
<body style="overflow-x: hidden">
    <form id="form1" runat="server">
    <table id="Table1" runat="server" width="98%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 100%; background: url('../Images/bg-1.gif')">
                <table id="tabbtn" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                    cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td align="left" valign="middle">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;<asp:Label runat="server" ID="lblTitle">工艺评定PQR</asp:Label>
                        </td>
                        <td align="right" valign="middle" style="border: 0px none #FFFFFF; height: 30px;">
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" ValidationGroup="Save"
                                OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="Table8" width="98%" cellpadding="0" cellspacing="0" class="table" 
                     style="border-collapse:collapse;" border="1" runat="server">
         <tr>
            <td align="center" height="23px">
                <asp:Label ID="Label65" runat="server" Text="工艺图片内容"></asp:Label>
             </td>
            <td align="left" colspan="4">
                <asp:DropDownList ID="drpImage" runat="server" Height="22px" Width="100%" 
                    AutoPostBack="True" onselectedindexchanged="drpImage_SelectedIndexChanged">
                </asp:DropDownList>
                  </td>
            <td align="left" colspan="4" valign="top" width="15%">
                <asp:Label ID="Label7" runat="server" Text="简图：（接头形式、坡口形式与尺寸、焊层、焊道布置及顺序）"></asp:Label>
                </td>
        </tr>
        <tr>
            <td align="center" height="23px">
                <asp:Label ID="Label2" runat="server" Text="预焊接工艺规程编号"></asp:Label>
            </td>
            <td align="left" colspan="4">
                <asp:TextBox ID="txtProcedureCode" runat="server" Width="90%" CssClass="textboxnoneborder"
                    Height="16px" ReadOnly="True"></asp:TextBox>
            </td>
            <td align="center"  valign="middle" width="15%" colspan="4" rowspan="4"
                height="100px">
                <asp:Image ID="imgURL" runat="server" />
            </td>
        </tr>
        <tr>
            <td align="center" height="23px">
                <asp:Label ID="Label3" runat="server" Text="日期"></asp:Label>
            </td>
            <td align="left" colspan="4">
                <input id="txtProcedureDate" runat="server" readonly="readonly" class="Wdate" style="width: 90%;
                    cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
            </td>
        </tr>
        <tr>
            <td align="center" height="23px">
                <asp:Label ID="Label5" runat="server" Text="焊接接头"></asp:Label>
            </td>
            <td align="left" colspan="4">
                <asp:TextBox ID="txtWeldedJoints" runat="server" Width="90%" CssClass="textboxnoneborder"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" height="23px">
                <asp:Label ID="Label6" runat="server" Text="坡口形式"></asp:Label>
            </td>
            <td align="left" colspan="4">
                <asp:TextBox ID="txtGrooveForm" runat="server" Width="90%" CssClass="textboxnoneborder"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" height="23px" colspan="9">
                &nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label8" runat="server" Font-Bold="True" Text="母材"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" height="23px">
                <asp:Label ID="Label34" runat="server" Text="材料代号"></asp:Label>
            </td>
            <td align="left" colspan="4">
                <asp:TextBox ID="txtMaterialCode" runat="server" Width="90%" CssClass="textboxnoneborder"
                    Height="16px"></asp:TextBox>
            </td>
            <td width="15%">
                <asp:Label ID="Label35" runat="server" Text="材料标准"></asp:Label>
            </td>
            <td align="left" colspan="3">
                <asp:TextBox ID="txtMaterialStandard" runat="server" CssClass="textboxnoneborder"
                    Width="90%" Height="16px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" height="23px">
                <asp:Label ID="Label36" runat="server" Text="厚度适用范围"></asp:Label>
            </td>
            <td align="left" colspan="4">
                <asp:TextBox ID="txtThicknessRange" runat="server" CssClass="textboxnoneborder" Width="90%"></asp:TextBox>
            </td>
            <td width="15%">
                &nbsp;
            </td>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left" height="23px" colspan="9">
                &nbsp;&nbsp;
                <asp:Label ID="Label9" runat="server" Font-Bold="True" Text="填充金属："></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" height="23px">
                <asp:Label ID="Label10" runat="server" Text="焊材类别"></asp:Label>
            </td>
            <td align="left" colspan="4">
                <asp:TextBox ID="txtMaterialType" runat="server" Width="90%" CssClass="textboxnoneborder"></asp:TextBox>
            </td>
            <td width="15%">
                <asp:Label ID="Label11" runat="server" Text="焊材标准"></asp:Label>
            </td>
            <td align="left" colspan="3">
                <asp:TextBox ID="txtMaterialSpecification" runat="server" CssClass="textboxnoneborder"
                    Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" height="23px">
                <asp:Label ID="Label37" runat="server" Text="焊材型号"></asp:Label>
            </td>
            <td align="left" colspan="4">
                <asp:TextBox ID="txtMaterialModel" runat="server" Width="90%" CssClass="textboxnoneborder"></asp:TextBox>
            </td>
            <td width="15%">
                &nbsp;
            </td>
            <td align="left" colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td align="left" height="23px" colspan="5">
                &nbsp;&nbsp;
                <asp:Label ID="Label16" runat="server" Font-Bold="True" Text="焊接位置："></asp:Label>
            </td>
            <td align="left" height="23px" colspan="4">
                <asp:Label ID="Label17" runat="server" Font-Bold="True" Text="焊后热处理:"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" height="23px">
                <asp:Label ID="Label18" runat="server" Text="对接焊缝的位置"></asp:Label>
            </td>
            <td align="left" colspan="4">
                <asp:TextBox ID="txtWeldingPosition" runat="server" Width="90%" CssClass="textboxnoneborder"></asp:TextBox>
            </td>
            <td width="15%">
                <asp:Label ID="Label19" runat="server" Text="保温温度（℃）"></asp:Label>
            </td>
            <td align="left" colspan="3">
                <asp:TextBox ID="txtHotTemperatures" runat="server" Width="90%" CssClass="textboxnoneborder"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" height="23px">
                &nbsp;
            </td>
            <td align="left" colspan="4">
                &nbsp;
            </td>
            <td width="15%">
                <asp:Label ID="Label62" runat="server" Text="保温时间(h)"></asp:Label>
            </td>
            <td align="left" colspan="3">
                <input id="txtHoldingDate" runat="server" readonly="readonly" class="Wdate" style="width: 90%;
                    cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
            </td>
        </tr>
        <tr>
            <td align="left" height="23px" colspan="5">
                &nbsp;&nbsp;
                <asp:Label ID="Label24" runat="server" Font-Bold="True" Text="预热："></asp:Label>
            </td>
            <td align="left" height="23px" colspan="4">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="保护气体："></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" height="23px">
                <asp:Label ID="Label26" runat="server" Text="预热温度（℃）"></asp:Label>
            </td>
            <td align="left" colspan="4">
                <asp:TextBox ID="txtPreheatingTemperature" runat="server" Width="90%" CssClass="textboxnoneborder"></asp:TextBox>
            </td>
            <td width="15%">
                <asp:Label ID="Label38" runat="server" Text="成分"></asp:Label>
            </td>
            <td align="left" colspan="3">
                <asp:TextBox ID="txtGasComponent" runat="server" Width="90%" CssClass="textboxnoneborder"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" height="23px">
                &nbsp;
                <asp:Label ID="Label55" runat="server" Text="加热方式"></asp:Label>
            </td>
            <td align="left" colspan="4">
                <asp:TextBox ID="txtHeatingMode" runat="server" Width="90%" CssClass="textboxnoneborder"></asp:TextBox>
            </td>
            <td width="15%">
                <asp:Label ID="Label39" runat="server" Text="流量（L/min）"></asp:Label>
            </td>
            <td align="left" colspan="3">
                <asp:TextBox ID="txtGasFlow" runat="server" Width="90%" CssClass="textboxnoneborder"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" height="23px" colspan="9">
                &nbsp;&nbsp;
                <asp:Label ID="Label40" runat="server" Font-Bold="True" Text="电特性:"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" height="23px">
                <asp:Label ID="Label41" runat="server" Text="钨极直径（mm）"></asp:Label>
            </td>
            <td align="left" colspan="4">
                <asp:TextBox ID="txtPolarDiameter" runat="server" Width="90%" CssClass="textboxnoneborder"></asp:TextBox>
            </td>
            <td width="15%">
                <asp:Label ID="Label42" runat="server" Text="喷嘴直径（mm）"></asp:Label>
            </td>
            <td align="left" colspan="3">
                <asp:TextBox ID="txtNozzleDiameter" runat="server" Width="90%" CssClass="textboxnoneborder"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" height="23px" colspan="9">
                <asp:Label ID="Label43" runat="server" Text="（按所焊位置和厚度，分别列出电流和电压范围，记入下表）"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" height="23px" rowspan="2">
                <asp:Label ID="Label44" runat="server" Text="焊道/焊层"></asp:Label>
            </td>
            <td align="center" rowspan="2">
                <asp:Label ID="Label45" runat="server" Text="焊接方法"></asp:Label>
            </td>
            <td align="center" colspan="2">
                <asp:Label ID="Label51" runat="server" Text="填充金属"></asp:Label>
            </td>
            <td align="center" colspan="2">
                <asp:Label ID="Label54" runat="server" Text="焊接电流"></asp:Label>
            </td>
            <td align="center" rowspan="2">
                <asp:Label ID="Label48" runat="server" Text="焊接电压（V）"></asp:Label>
            </td>
            <td align="center" rowspan="2">
                <asp:Label ID="Label49" runat="server" Text="焊接速度（cm/min）"></asp:Label>
            </td>
            <td align="center" rowspan="2">
                <asp:Label ID="Label50" runat="server" Text="线能力（KJ/cm）"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="Label52" runat="server" Text="牌号"></asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="Label53" runat="server" Text="直径"></asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="Label46" runat="server" Text="极性"></asp:Label>
            </td>
            <td width="15%" align="center">
                <asp:Label ID="Label47" runat="server" Text="电流（A）"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" height="23px">
                <asp:TextBox ID="txtWeldLayer" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
            </td>
            <td align="center" width="8%">
                <asp:TextBox ID="txtWeldMethod" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
            </td>
            <td align="center" width="9%">
                <asp:TextBox ID="txtCardNum" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
            </td>
            <td align="center" width="9%">
                <asp:TextBox ID="txtDiameter" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
            </td>
            <td align="center" width="9%">
                <asp:TextBox ID="txtPolarity" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
            </td>
            <td align="center" width="15%">
                <asp:TextBox ID="txtElectricCurrent" runat="server" CssClass="textboxnoneborder"
                    Width="95%"></asp:TextBox>
            </td>
            <td align="center" width="11%">
                <asp:TextBox ID="txtVoltage" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
            </td>
            <td align="center" width="12%">
                <asp:TextBox ID="txtSpeed" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
            </td>
            <td align="center" width="12%">
                <asp:TextBox ID="txtLineCapacity" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" height="23px" colspan="9">
                &nbsp;&nbsp;
                <asp:Label ID="Label56" runat="server" Font-Bold="True" Text="无损检验："></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" height="23px">
                <asp:Label ID="Label57" runat="server" Text="RT"></asp:Label>
            </td>
            <td align="left" colspan="4">
                <asp:TextBox ID="txtTestingRT" runat="server" CssClass="textboxnoneborder" Width="90%"></asp:TextBox>
            </td>
            <td align="center">
                <asp:Label ID="Label58" runat="server" Text="UT"></asp:Label>
            </td>
            <td align="left" colspan="3">
                <asp:TextBox ID="txtTestingUT" runat="server" CssClass="textboxnoneborder" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" height="23px">
                <asp:Label ID="Label59" runat="server" Text="MT"></asp:Label>
            </td>
            <td align="left" colspan="4">
                <asp:TextBox ID="txtTestingMT" runat="server" CssClass="textboxnoneborder" Width="90%"></asp:TextBox>
            </td>
            <td align="center">
                <asp:Label ID="Label60" runat="server" Text="PT"></asp:Label>
            </td>
            <td align="left" colspan="3">
                <asp:TextBox ID="txtTestingPT" runat="server" CssClass="textboxnoneborder" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" height="23px">
                <asp:Label ID="Label61" runat="server" Text="其他"></asp:Label>
            </td>
            <td align="left" colspan="8">
                <asp:TextBox ID="txtTestingOther" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" height="23px">
                <asp:Label ID="Label63" runat="server" Font-Bold="True" Text="技术措施"></asp:Label>
            </td>
            <td align="left" colspan="8">
                <asp:TextBox ID="txtTechnicalMeasures" runat="server" CssClass="textboxnoneborder"
                    Width="95%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" height="23px">
                <asp:Label ID="Label64" runat="server" Font-Bold="True" Text="其他说明"></asp:Label>
            </td>
            <td align="left" colspan="8">
                <asp:TextBox ID="txtDescription" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
