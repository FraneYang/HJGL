<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ElectrodeRecoveryPrint.aspx.cs"
    Inherits="Web.MaterialManage.ElectrodeRecoveryPrint" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>焊条发放回收记录打印</title>
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
                                &nbsp;焊条发放回收记录
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
                    cellspacing="0" style="font-size:14px;">
                    <tr>
                        <td width="25%">
                        </td>
                        <td align="center" width="45%">
                            <asp:Label ID="Label1" runat="server" Text="焊条发放回收记录" Font-Bold="True" Font-Size="Larger"></asp:Label>
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
            <td style="border-width: 0px;">
                <table id="Table2" runat="server" width="100%" cellpadding="0" cellspacing="0" border="1"
                    bordercolor="#bcd2e7" bordercolordark="#bcd2e7" bordercolorlight="#bcd2e7">
                    <tr>
                        <td>
                            <asp:GridView ID="gvElectrodeRecovery" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                HorizontalAlign="Justify" Width="100%" DataSourceID="ObjectDataSource1" OnDataBound="gvElectrodeRecovery_DataBound" UseAccessibleHeader="false">
                                <HeaderStyle HorizontalAlign="Center" Font-Size="14px" Height="30px" />
                                <Columns>
                                    <asp:TemplateField HeaderText="序号" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="月" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label2" runat="server" Text='<%# ConvertIntMonth(Eval("ElectrodeRecoveryDate").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                          <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="日" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Text='<%# ConvertIntDay(Eval("ElectrodeRecoveryDate").ToString()) %>'></asp:Label>
                                        </ItemTemplate>
                                          <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="型号" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("ElectrodeRecoveryModel") %>'></asp:Label>
                                        </ItemTemplate>
                                          <ItemStyle Width="6%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="焊条牌号" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("ElectrodeGrade") %>'></asp:Label>
                                        </ItemTemplate>
                                          <ItemStyle Width="7%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="批号" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("BatchNumber") %>'></asp:Label>
                                        </ItemTemplate>
                                          <ItemStyle Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="入库自编号" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("InLibCode") %>'></asp:Label>
                                        </ItemTemplate>
                                          <ItemStyle Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="规格mm" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("Specifications") %>'></asp:Label>
                                        </ItemTemplate>
                                          <ItemStyle Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="焊工代号" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("WelderCode") %>'></asp:Label>
                                        </ItemTemplate>
                                          <ItemStyle Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="使用部位" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label9" runat="server" Text='<%# Bind("UseSite") %>'></asp:Label>
                                        </ItemTemplate>
                                          <ItemStyle Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="焊件材质" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label10" runat="server" Text='<%# Bind("WeldingMaterial") %>'></asp:Label>
                                        </ItemTemplate>
                                      <ItemStyle Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="领用数量(根)" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label11" runat="server" Text='<%# Bind("RecipientsCount") %>'></asp:Label>
                                        </ItemTemplate>
                                          <ItemStyle Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="回收数量（根）" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label12" runat="server" Text='<%# Bind("RecoveryCount") %>'></asp:Label>
                                        </ItemTemplate>
                                          <ItemStyle Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="发放人" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label ID="Label13" runat="server" Text='<%# Bind("GrantMan") %>'></asp:Label>
                                        </ItemTemplate>
                                          <ItemStyle Width="8%" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerTemplate>
                                    <uc1:GridNavgator ID="GridNavgator1" runat="server" />
                                </PagerTemplate>
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetListDataPrint"
                                TypeName="BLL.ElectrodeRecoveryService" OnSelecting="ObjectDataSource1_Selecting">
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
