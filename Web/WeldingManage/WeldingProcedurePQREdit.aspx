<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeldingProcedurePQREdit.aspx.cs" Inherits="Web.WeldingManage.WeldingProcedurePQREdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self" />
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
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
<body>
    <form id="form1" runat="server">
   <table id="Table1" runat="server" width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 100%; background: url('../Images/bg-1.gif')">
                <table id="tabbtn" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                    cellpadding="0" cellspacing="0" border="0">
                    <tr>
                        <td align="left" valign="middle" 
                            style="border: 0px none #FFFFFF; font-size: 11pt; font-weight: bold">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;<asp:Label runat="server" ID="lblTitle">PQR编辑</asp:Label>
                        </td>
                        <td align="right" valign="middle" 
                            style="border: 0px none #FFFFFF; height: 30px;">
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" ValidationGroup="Save"
                                OnClick="btnSave_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" height="36px" 
                style="border-style: 0; border-width: 0px; border-color: #FFFFFF;">
                <asp:Label ID="Label1" runat="server" Text="焊接作业指导书（WWI）" Font-Bold="True" Font-Size="20px"></asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Text="编号：WWI-"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" border="1">
                    <tr>
                        <td align="center" height="23px">
                            <asp:Label ID="Label3" runat="server" Text="项目名称"></asp:Label>
                        </td>
                        <td colspan="5" align="left">
                            <asp:TextBox ID="txtProjectName" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
                        </td>
                        <td colspan="7" align="center">
                            <asp:Label ID="Label4" runat="server" Text="焊接接头简图：（坡口形式与尺寸、焊层焊道布置及顺序）"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 23px;">
                        <td align="center" height="23px">
                            <asp:Label ID="Label5" runat="server" Text="编制"></asp:Label>
                        </td>
                        <td colspan="2" align="left" valign="middle">
                            <asp:TextBox ID="txtCompile" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:Label ID="Label6" runat="server" Text="审批"></asp:Label>
                        </td>
                        <td colspan="2" align="left">
                            <asp:TextBox ID="txtApproval" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
                        </td>
                        <td colspan="7" rowspan="8">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center" height="23px">
                            <asp:Label ID="Label7" runat="server" Text="工艺评定报告编号"></asp:Label>
                        </td>
                        <td colspan="4" align="left">
                            <asp:TextBox ID="txtProduceCode" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" height="23px">
                            <asp:Label ID="Label8" runat="server" Text="母材材质"></asp:Label>
                        </td>
                        <td colspan="2" align="left">
                            <asp:TextBox ID="txtMaterialGroup" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:Label ID="Label9" runat="server" Text="焊接位置"></asp:Label>
                        </td>
                        <td colspan="2" align="left">
                            <asp:TextBox ID="txtWeldPosition" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" height="23px">
                            <asp:Label ID="Label10" runat="server" Text="接头形式"></asp:Label>
                        </td>
                        <td align="left" colspan="2">
                            <asp:TextBox ID="txtJointsForm" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:Label ID="Label11" runat="server" Text="母材壁厚(mm)"></asp:Label>
                        </td>
                        <td colspan="2" align="left">
                            <asp:TextBox ID="txtSpecimenThickness" runat="server" CssClass="textboxnoneborder"
                                Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="3" align="center">
                            <asp:Label ID="Label12" runat="server" Text="焊接材料"></asp:Label>
                        </td>
                        <td height="23px" align="center">
                            <asp:Label ID="Label13" runat="server" Text="型号"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="Label14" runat="server" Text="规格(mm)"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="Label15" runat="server" Text="烘烤温度（℃）"></asp:Label>
                        </td>
                        <td colspan="2" align="center">
                            <asp:Label ID="Label16" runat="server" Text="恒温时间 (h)"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="23px" align="center">
                            <asp:TextBox ID="txtMaterialModel1" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtSpecification1" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtBakingTemperature1" runat="server" CssClass="textboxnoneborder"
                                Width="95%"></asp:TextBox>
                        </td>
                        <td colspan="2" align="center">
                            <asp:TextBox ID="txtSoakTime1" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="23px" align="center">
                            <asp:TextBox ID="txtMaterialModel2" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtSpecification2" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtBakingTemperature2" runat="server" CssClass="textboxnoneborder"
                                Width="95%"></asp:TextBox>
                        </td>
                        <td class="style1" colspan="2" align="center">
                            <asp:TextBox ID="txtSoakTime2" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center" height="23px">
                            <asp:Label ID="Label17" runat="server" Text="适应管线号"></asp:Label>
                        </td>
                        <td colspan="4" align="left">
                            <asp:TextBox ID="txtAdaptlineNo" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" height="23px">
                            <asp:Label ID="Label18" runat="server" Text="钨极直径(mm)"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="Label19" runat="server" Text="喷嘴孔径(mm)"></asp:Label>
                        </td>
                        <td colspan="2" align="center">
                            <asp:Label ID="Label20" runat="server" Text="保护气体"></asp:Label>
                        </td>
                        <td rowspan="2" align="center">
                            <asp:Label ID="Label21" runat="server" Text="层—道"></asp:Label>
                        </td>
                        <td align="center" rowspan="2">
                            <asp:Label ID="Label22" runat="server" Text="焊接方法"></asp:Label>
                        </td>
                        <td rowspan="2" align="center">
                            <asp:Label ID="Label23" runat="server" Text="机械化程度"></asp:Label>
                        </td>
                        <td rowspan="2" align="center">
                            <asp:Label ID="Label24" runat="server" Text="焊材直径（mm）"></asp:Label>
                        </td>
                        <td colspan="3" align="center">
                            <asp:Label ID="Label25" runat="server" Text="焊接电源"></asp:Label>
                        </td>
                        <td align="center" rowspan="2">
                            <asp:Label ID="Label26" runat="server" Text="焊接速度（cm/min）"></asp:Label>
                        </td>
                        <td align="center" rowspan="2">
                            <asp:Label ID="Label27" runat="server" Text="线能量(KJ/cm)"></asp:Label>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="23px" align="center">
                            <asp:TextBox ID="txtTungstenLiameter1" runat="server" CssClass="textboxnoneborder"
                                Width="95%"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtNozzlediameter1" runat="server" CssClass="textboxnoneborder"
                                Width="95%"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:Label ID="Label31" runat="server" Text="成分(%)"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="Label32" runat="server" Text="流量(L/min)"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="Label28" runat="server" Text="电流(A)"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="Label29" runat="server" Text="电压(V)"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="Label30" runat="server" Text="种类极性"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td height="23px" align="center">
                            <asp:TextBox ID="txtTungstenLiameter2" runat="server" CssClass="textboxnoneborder"
                                Width="95%"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtNozzlediameter2" runat="server" CssClass="textboxnoneborder"
                                Width="95%"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtGasComposition" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtGasFlow" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtLayerTogether" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtWME_ID" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtMechanization" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtWireDiameter" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtElectricity" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtVoltage" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtKindsPolarity" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtWeldingRate" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtHeatInput" runat="server" CssClass="textboxnoneborder" Width="95%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="23px" align="center">
                            <asp:Label ID="Label33" runat="server" Text="预热温度（℃）"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="Label34" runat="server" Text="层间温度（℃）"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="Label35" runat="server" Text="后热温度（℃）"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="Label36" runat="server" Text="加热方法"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="23px" width="8%" align="center">
                            <asp:TextBox ID="txtPreheatTemperature" runat="server" Width="95%" CssClass="textboxnoneborder"></asp:TextBox>
                        </td>
                        <td width="8%" align="center">
                            <asp:TextBox ID="txtInterpassTemperature" runat="server" Width="95%" CssClass="textboxnoneborder"></asp:TextBox>
                        </td>
                        <td width="8%" align="center">
                            <asp:TextBox ID="txtPostheatTemperature" runat="server" Width="95%" CssClass="textboxnoneborder"></asp:TextBox>
                        </td>
                        <td width="9%" align="center">
                            <asp:TextBox ID="txtHeatingMeans" runat="server" Width="95%" CssClass="textboxnoneborder"></asp:TextBox>
                        </td>
                        <td width="9%">
                            &nbsp;
                        </td>
                        <td width="9%">
                            &nbsp;
                        </td>
                        <td width="7%">
                            &nbsp;
                        </td>
                        <td width="7%">
                            &nbsp;
                        </td>
                        <td width="7%">
                            &nbsp;
                        </td>
                        <td width="7%">
                            &nbsp;
                        </td>
                        <td width="7%">
                            &nbsp;
                        </td>
                        <td width="7%">
                            &nbsp;
                        </td>
                        <td width="7%">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" height="23px" align="center">
                            <asp:Label ID="Label37" runat="server" Text="无损检测合格级别"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="23px" align="center">
                            <asp:Label ID="Label38" runat="server" Text="RT"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="Label39" runat="server" Text="UT"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="Label40" runat="server" Text="MT"></asp:Label>
                        </td>
                        <td align="center">
                            <asp:Label ID="Label41" runat="server" Text="PT"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="23px" align="center">
                            <asp:TextBox ID="txtCheckRT" runat="server" Width="95%" CssClass="textboxnoneborder"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtCheckUT" runat="server" Width="95%" CssClass="textboxnoneborder"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtCheckMT" runat="server" Width="95%" CssClass="textboxnoneborder"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="txtCheckPT" runat="server" Width="95%" CssClass="textboxnoneborder"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" height="23px" align="center">
                            <asp:Label ID="Label42" runat="server" Text="其他："></asp:Label>
                            <asp:TextBox ID="txtOther" runat="server" Width="55%" CssClass="textboxnoneborder"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" height="23px" align="center">
                            <asp:Label ID="Label43" runat="server" Text="焊后热处理参数"></asp:Label>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" height="23px" align="center">
                            <asp:Label ID="Label44" runat="server" Text="加热方式"></asp:Label>
                        </td>
                        <td colspan="2" align="left">
                            <asp:TextBox ID="txtHotMethod" runat="server" Width="95%" CssClass="textboxnoneborder"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" height="23px" align="center">
                            <asp:Label ID="Label45" runat="server" Text="升温速度（℃/hr)"></asp:Label>
                        </td>
                        <td colspan="2" align="left">
                            <asp:TextBox ID="txtUpTemperatureSpeed" runat="server" Width="95%" CssClass="textboxnoneborder"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" height="23px" align="center">
                            <asp:Label ID="Label46" runat="server" Text="热处理温度(℃)"></asp:Label>
                        </td>
                        <td colspan="2" align="left">
                            <asp:TextBox ID="txtHotDisposeT" runat="server" Width="95%" CssClass="textboxnoneborder"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" height="23px" align="center">
                            <asp:Label ID="Label47" runat="server" Text="保温时间（hr）"></asp:Label>
                        </td>
                        <td colspan="2" align="left">
                            <asp:TextBox ID="txtCoolingRate" runat="server" Width="95%" CssClass="textboxnoneborder"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" height="23px" align="center">
                            <asp:Label ID="Label48" runat="server" Text="降温速度（℃/hr）"></asp:Label>
                        </td>
                        <td colspan="2" align="left">
                            <asp:TextBox ID="txtSoakingTime0" runat="server" Width="95%" CssClass="textboxnoneborder"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td height="23px" align="center">
                            <asp:Label ID="Label49" runat="server" Text="技术措施"></asp:Label>
                        </td>
                        <td colspan="6" align="left">
                            <asp:TextBox ID="txtTechnicalMeasure" runat="server" Width="95%" CssClass="textboxnoneborder"></asp:TextBox>
                        </td>
                        <td colspan="2" align="center">
                            <asp:Label ID="Label50" runat="server" Text="其他说明"></asp:Label>
                        </td>
                        <td colspan="4" align="left">
                            <asp:TextBox ID="txtOtherDescription" runat="server" Width="95%" CssClass="textboxnoneborder"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
