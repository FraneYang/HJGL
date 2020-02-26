<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeldingProcedurePrint.aspx.cs"
    Inherits="Web.WeldingManage.WeldingProcedurePrint" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <link href="../Styles/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.autocomplete.js" type="text/javascript"></script>
    <script src="../Scripts/inputAutocomplete.js" type="text/javascript"></script>
    <script src="../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function CheckDropDownList(source, args) {
            if (args.Value == "0")
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        $().ready(function () {
            var name = "txtSignPerson";
            var url = "../Ajax.aspx?operate=GetUsersByUserName";
            var backName = "hdSignPerson";
            inputSearch(name, url, backName);
        })

        function GetProjectHeadConfirm() {
            var name = "txtProjectHeadConfirm";
            var backName = "hdProjectHeadConfirm";
            var url = "../Ajax.aspx?operate=GetUsersByUserName";
            inputSearch(name, url, backName);
        }

        function del(filePath) {
            document.getElementById("hdAttachUrl").value = filePath;
            document.getElementById("imgbtnAttachUrl").click();
        }

        function pagesetup_null() {
            try {
                var RegWsh = new ActiveXObject("WScript.Shell")
                hkey_key = "header"
                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "")
                hkey_key = "footer"
                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "")
            } catch (e) { }
        }
        //设置网页打印的页眉页脚为默认值
        function pagesetup_default() {
            try {
                var RegWsh = new ActiveXObject("WScript.Shell")
                hkey_key = "header"
                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "&w&b页码，&p/&P")
                hkey_key = "footer"
                RegWsh.RegWrite(hkey_root + hkey_path + hkey_key, "&u&b&d")
            } catch (e) { }
        }
        function printpr() //预览函数
        {
            pagesetup_null(); //预览之前去掉页眉，页脚
            document.getElementById("div1").style.display = "none";

            var WebBrowser = '<OBJECT ID="WebBrowser1" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></OBJECT>';
            document.body.insertAdjacentHTML('beforeEnd', WebBrowser); //在body标签内加入html（WebBrowser activeX控件）
            WebBrowser1.ExecWB(7, 1); //打印预览
            WebBrowser1.outerHTML = ""; //从代码中清除插入的html代码
            pagesetup_default(); //预览结束后页眉页脚恢复默认值
            document.getElementById("div1").style.display = "block";
        }
        function print() //打印函数
        {
            pagesetup_null(); //打印之前去掉页眉，页脚
            document.getElementById("div1").style.display = "none";

            var WebBrowser = '<OBJECT ID="WebBrowser1" WIDTH=0 HEIGHT=0 CLASSID="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"></OBJECT>';
            document.body.insertAdjacentHTML('beforeEnd', WebBrowser); //在body标签内加入html（WebBrowser activeX控件）
            WebBrowser1.ExecWB(6, 6); //打印
            WebBrowser1.outerHTML = ""; //从代码中清除插入的html代码
            pagesetup_default(); //打印结束后页眉页脚恢复默认值
            document.getElementById("div1").style.display = "block";
        }
    </script>
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
        .style1
        {
            width: 19%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" runat="server" width="95%" cellpadding="0" cellspacing="0" align="center">
        <tr>
            <td style="width: 100%; background: url('../Images/bg-1.gif'); border-width: 0px;">
                <div id="div1">
                    <table id="tabbtn" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                        cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" valign="middle" style="width: 50%; font-size: 11pt; font-weight: bold;
                                border-width: 0px;">
                                <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                                &nbsp;PQR
                            </td>
                            <td align="right" valign="middle" style="width: 50%; height: 30px; border-width: 0px;">
                                <img src="../Images/PageSetup.gif" runat="server" id="Img2" onclick="document.all.WebBrowser.ExecWB(8,1)"
                                    alt="页面设置" style="cursor: pointer" />
                                <img src="../Images/PrintSetup.gif" runat="server" id="btnPrint" onclick="document.all.WebBrowser.ExecWB(6,1)"
                                    alt="打印设置" style="cursor: pointer" />
                                <img src="../Images/PrintPreview.gif" runat="server" id="Img1" onclick="printpr()"
                                    alt="打印预览" style="cursor: pointer" />
                                <img src="../Images/Print.gif" runat="server" id="Img3" onclick="print()" alt="打印"
                                    style="cursor: pointer" />
                            </td>
                            <td align="right" valign="middle" style="width: 50%; height: 30px; border-width: 0px;">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td align="center" height="36px" >
                <asp:Label ID="Label1" runat="server" Text="焊接工艺PQR" Font-Bold="True" Font-Size="20px"></asp:Label>               
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="0" cellspacing="0" class="table" 
                     style="border-collapse:collapse;" border="1">
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
    <object id="WebBrowser" width="0" height="0" classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2">
    </object>
    </form>
</body>
</html>
