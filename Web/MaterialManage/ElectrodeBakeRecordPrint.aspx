<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ElectrodeBakeRecordPrint.aspx.cs"
    Inherits="Web.MaterialManage.ElectrodeBakeRecordPrint" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>焊条烘烤记录打印</title>
    <script type="text/javascript" language="javascript">
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
        .style1
        {
            height: 21px;
        }
       tabel
        {
            border-collapse: collapse;
        }
       
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" runat="server" width="95%" cellpadding="0" cellspacing="0" align="center" border="1">
        <tr>
            <td style="width: 100%; background: url('../Images/bg-1.gif'); border-width: 0px;">
                <div id="div1">
                    <table id="tabbtn" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                        cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" valign="middle" style="width: 50%; font-size: 11pt; font-weight: bold;
                                border-width: 0px;">
                                <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                                &nbsp;焊条烘烤记录
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
            <td style="border-width: 0px;">
                <table id="Table5" runat="server" width="100%" height="80px" border="1" cellpadding="0"
                    cellspacing="0" style="font-size: 14px;">
                    <tr>
                        <td width="25%">
                        </td>
                        <td align="center" width="45%">
                            <asp:Label ID="Label1" runat="server" Text="焊丝烘烤记录" Font-Bold="True" Font-Size="Larger"></asp:Label>
                        </td>
                        <td width="30%">
                            <asp:Label ID="Label2" runat="server" Text="工程名称："></asp:Label>
                            <asp:Label ID="lblProjectName" runat="server" Text="lblProjectName"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="border-width: 1px;">
                <table id="Table2" runat="server" width="100%" cellpadding="0" cellspacing="0" border="1" style=" border:#000">
                    <tr>
                        <td style=" border:1; border-color:#000;">
                            <asp:GridView ID="gvElectrodeBake" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                HorizontalAlign="Justify" Width="100%" DataSourceID="ObjectDataSource1" OnDataBound="gvElectrodeBake_DataBound"
                                UseAccessibleHeader="False" BorderColor="Black" BorderStyle="Solid" 
                                BorderWidth="1px">
                                <HeaderStyle HorizontalAlign="Center" Font-Size="14px" Height="30px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="烘烤日期" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <table width="100%" cellpadding="0" cellspacing="0" border="1" 
                                                style="height:30px;">
                                                <tr>
                                                    <td align="center" colspan="2"  style=" border:1; border-color:#000;">
                                                        <asp:Label ID="Label14" runat="server" Text="烘烤日期"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width:50%; border-color:#000;" align="center">
                                                        <asp:Label ID="Label15" runat="server" Text="月"></asp:Label>
                                                    </td>
                                                    <td style="width:50%; border-color:#000;" align="center">
                                                        <asp:Label ID="Label16" runat="server" Text="日"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%" cellpadding="0" cellspacing="0" border="1" 
                                                style=" height:22px;">
                                                <tr>
                                                    <td style="width:50%; border:1; border-color:#000000; height:22px;">
                                                        <asp:Label ID="Label2" runat="server" Text='<%# ConvertIntMonth(Eval("ElectrodeDate").ToString()) %>'></asp:Label>
                                                    </td>
                                                    <td style="width:50%;">
                                                        <asp:Label ID="Label3" runat="server" Text='<%# ConvertIntDay(Eval("ElectrodeDate").ToString()) %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Width="8%" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="型号" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("ElectrodeModel") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="6%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="牌号" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("CardCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="6%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="批号" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("BatchCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="6%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="入库自编号" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("InLibCode") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="6%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="规格mm" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("Specifications") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="6%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="数量kg" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("ElectrodeCount") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="烘箱送电" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <table align="center" width="100%" border="1" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center" colspan="3" style="border-color:#000000;">
                                                        <asp:Label ID="Label17" runat="server" Text="烘箱送电"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2" style="border-color:#000000;">
                                                        <asp:Label ID="Label18" runat="server" Text="时间"></asp:Label>
                                                    </td>
                                                    <td align="center" rowspan="2" width="50%" style="border-color:#000000;">
                                                        <asp:Label ID="Label21" runat="server" Text="温度"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" width="25%" style="border-color:#000000";>
                                                        <asp:Label ID="Label19" runat="server" Text="月"></asp:Label>
                                                    </td>
                                                    <td align="center" width="25%" style="border-color:#000000";>
                                                        <asp:Label ID="Label20" runat="server" Text="日"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table border="1" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td width="25%" align="center" height="100%" style="border-color:#000000; height:22px;">
                                                        <asp:Label ID="Label9" runat="server" Text='<%# Bind("OvenElectricHours") %>'></asp:Label>
                                                    </td>
                                                    <td width="25%" align="center" style="border-color:#000000";>
                                                        <asp:Label ID="Label22" runat="server" Text='<%# Bind("OvenElectricMinute") %>'></asp:Label>
                                                    </td>
                                                    <td width="50%" align="center" style="border-color:#000000";>
                                                        <asp:Label ID="Label23" runat="server" Text='<%# Bind("OvenElectricTemperature") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="恒温" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <table border="1" cellpadding="0" cellspacing="0" width="100%" style=" height:30px">
                                                <tr>
                                                    <td align="center" colspan="5" style="border-color:#000000;">
                                                        <asp:Label ID="Label24" runat="server" Text="恒温"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" rowspan="2" width="20%" style="border-color:#000000;">
                                                        <asp:Label ID="Label25" runat="server" Text="温度"></asp:Label>
                                                    </td>
                                                    <td align="center" colspan="2" style="border-color:#000000;">
                                                        <asp:Label ID="Label26" runat="server" Text="开始时间"></asp:Label>
                                                    </td>
                                                    <td align="center" colspan="2" style="border-color:#000000;">
                                                        <asp:Label ID="Label27" runat="server" Text="结束时间"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" width="20%" style="border-color:#000000;">
                                                        <asp:Label ID="Label28" runat="server" Text="时"></asp:Label>
                                                    </td>
                                                    <td align="center" width="20%" style="border-color:#000000;">
                                                        <asp:Label ID="Label29" runat="server" Text="分"></asp:Label>
                                                    </td>
                                                    <td align="center" width="20%" style="border-color:#000000;">
                                                        <asp:Label ID="Label30" runat="server" Text="时"></asp:Label>
                                                    </td>
                                                    <td align="center" width="20%" style="border-color:#000000;">
                                                        <asp:Label ID="Label31" runat="server" Text="分"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%" border="1" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td align="center" height="22px" width="20%" style="border-color:#000000;">
                                                        <asp:Label ID="Label32" runat="server" Text='<%# Bind("ConstantTemperature") %>'></asp:Label>
                                                    </td>
                                                    <td align="center" width="20%" style="border-color:#000000;">
                                                        <asp:Label ID="Label33" runat="server" Text='<%# Bind("ConstantStartHours") %>'></asp:Label>
                                                    </td>
                                                    <td align="center" width="20%" style="border-color:#000000;">
                                                        <asp:Label ID="Label34" runat="server" Text='<%# Bind("ConstantStartMinute") %>'></asp:Label>
                                                    </td>
                                                    <td align="center" width="20%" style="border-color:#000000;">
                                                        <asp:Label ID="Label35" runat="server" Text='<% Bind("ConstantEndHours") %>'></asp:Label>
                                                        <asp:Label ID="Label45" runat="server" Text='<%# Bind("ConstantEndMinute") %>'></asp:Label>
                                                    </td>
                                                    <td align="center" width="20%" style="border-color:#000000;">
                                                        <asp:Label ID="Label36" runat="server" Text='<%# Bind("ConstantEndMinute") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="移入保温箱" ItemStyle-HorizontalAlign="Center">
                                        <HeaderTemplate>
                                            <table border="1" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="center" class="style1" colspan="3" style="border-color:#000000;">
                                                        <asp:Label ID="Label37" runat="server" Text="移入保温箱"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2" style="border-color:#000000;">
                                                        <asp:Label ID="Label38" runat="server" Text="时间"></asp:Label>
                                                    </td>
                                                    <td align="center" rowspan="2" width="50%" style="border-color:#000000;">
                                                        <asp:Label ID="Label39" runat="server" Text="温度"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" width="25%" style="border-color:#000000;">
                                                        <asp:Label ID="Label40" runat="server" Text="时"></asp:Label>
                                                    </td>
                                                    <td align="center" width="25%" style="border-color:#000000;">
                                                        <asp:Label ID="Label41" runat="server" Text="分"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table border="1" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td align="center" width="25%" height="22px" style="border-color:#000000;">
                                                        <asp:Label ID="Label42" runat="server" Text='<%# Bind("MoveInBoxHours") %>'></asp:Label>
                                                    </td>
                                                    <td align="center" width="25%" style="border-color:#000000;">
                                                        <asp:Label ID="Label43" runat="server" Text='<%# Bind("MoveInBoxMinute") %>'></asp:Label>
                                                    </td>
                                                    <td align="center" width="50%" style="border-color:#000000;">
                                                        <asp:Label ID="Label44" runat="server" Text='<%# Bind("MoveInTemperature") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Width="15%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="烘烤次数" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label12" runat="server" Text='<%# Bind("BakeNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="6%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="烘烤负责人" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label13" runat="server" Text='<%# Bind("BakeHead") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="6%" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerTemplate>
                                    <uc1:GridNavgator ID="GridNavgator1" runat="server" />
                                </PagerTemplate>
                                <RowStyle BorderColor="Black" BorderStyle="Solid" BorderWidth="1px" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetListDataPrint"
                                TypeName="BLL.ElectrodeBakeService" OnSelecting="ObjectDataSource1_Selecting">
                                <SelectParameters>
                                    <asp:Parameter Name="startDate" Type="DateTime" />
                                    <asp:Parameter Name="endDate" Type="DateTime" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
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
