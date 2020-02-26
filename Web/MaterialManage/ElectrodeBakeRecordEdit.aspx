<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ElectrodeBakeRecordEdit.aspx.cs"
    Inherits="Web.MaterialManage.ElectrodeBakeRecordEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>焊条烘烤记录编辑</title>
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function keypress() {
            var keyASCII = event.keyCode;
            if ((keyASCII >= 48 && keyASCII <= 57)) {

            }
            else {
                event.keyCode = 0;
            }

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" runat="server" width="100%" cellpadding="0" cellspacing="0" align="center">
        <tr>
            <td>
                <table id="tabbtn" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" valign="middle" style="width: 50%; font-size: 11pt; font-weight: bold">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;焊条烘烤记录
                        </td>
                        <td align="right" valign="middle" style="width: 50%; height: 30px;">
                            <asp:ImageButton ID="btnAddItem" runat="server" ImageUrl="~/Images/addbutton.gif"
                                OnClick="btnAddItem_Click" />
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" OnClick="btnSave_Click"
                                ValidationGroup="Save" />
                            <asp:ImageButton ID="btnReturn" runat="server" ImageUrl="~/Images/Return.gif" OnClick="btnReturn_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table2" runat="server" width="100%" border="1" cellpadding="0" cellspacing="0"
                    bordercolor="#bcd2e7" bordercolordark="#bcd2e7" bordercolorlight="#bcd2e7">
                    <tr>
                        <td align="left" style="width: 25%; height: 50px; vertical-align: middle; border-color: #bcd2e7">
                            &nbsp;
                            <asp:Label ID="Label31" runat="server" Text="编号："></asp:Label>
                            <asp:TextBox ID="txtEletrodeCode" runat="server" CssClass="textboxStyle" Width="70%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEletrodeCode"
                                Display="Dynamic" ErrorMessage="&quot;请输入编号!&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td style="width: 50%; height: 50px; vertical-align: middle; border-color: #bcd2e7">
                            <asp:Label ID="Label1" runat="server" Text="焊 条 烘 烤 记 录" Font-Bold="True" Font-Size="16pt"></asp:Label>
                        </td>
                        <td align="left" style="width: 25%; height: 50px; vertical-align: top; border-color: #bcd2e7">
                            <br />
                            <asp:Label ID="Label2" runat="server" Text="工程名称："></asp:Label>
                            <asp:Label ID="lblProjectName" runat="server" Text="lblProjectName"></asp:Label>
                            <br />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table3" runat="server" width="100%" border="1" cellpadding="0" cellspacing="0"
                    bordercolor="#bcd2e7" bordercolordark="#bcd2e7" bordercolorlight="#bcd2e7">
                    <tr>
                        <td align="center" width="15%" height="30px">
                            <asp:Label ID="Label3" runat="server" Text="烘烤日期"></asp:Label>
                        </td>
                        <td align="left" width="35%">
                            <input id="txtElectrodeDate" runat="server" class="Wdate" style="width: 80%; cursor: hand"
                                onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" /><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtElectrodeDate"
                                    Display="Dynamic" ErrorMessage="&quot;请选择烘烤日期！&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="center" width="15%">
                            <asp:Label ID="Label4" runat="server" Text="编制日期"></asp:Label>
                        </td>
                        <td align="left" width="35%">
                            <input id="txtCompileDate" runat="server" readonly="readonly" class="Wdate" style="width: 80%;
                                cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table4" runat="server" width="100%" border="1" cellpadding="0" cellspacing="0"
                    bordercolor="#bcd2e7" bordercolordark="#bcd2e7" bordercolorlight="#bcd2e7">
                    <tr>
                        <td>
                            <asp:GridView ID="gvElectrodeBake" runat="server" AllowSorting="True" PageSize="12"
                                AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%" OnRowDataBound="gvElectrodeBake_RowDataBound"
                                OnRowCommand="gvElectrodeBake_RowCommand" DataSourceID="ObjectDataSource1">
                                <Columns>
                                 <asp:TemplateField HeaderText="型号" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Height="20px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtModel" runat="server" Text='<%# Bind("ElectrodeModel") %>' BorderStyle="None"
                                                Width="90%"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="牌号" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Height="20px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCardCode" runat="server" Text='<%# Bind("CardCode") %>' BorderStyle="None"
                                                Width="90%"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="批号">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBatchCode" runat="server" Text='<%# Bind("BatchCode") %>' BorderStyle="None"
                                                Width="90%"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="入库自编号">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtInLibCode" runat="server" Text='<%# Bind("InLibCode") %>' BorderStyle="None"
                                                Width="90%"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="规格mm">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSpecifications" runat="server" Text='<%# Bind("Specifications") %>'
                                                BorderStyle="None" Width="90%"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="数量kg">
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td onkeypress="keypress()">
                                                        <asp:TextBox ID="txtElectrodeCount" runat="server" Text='<%# Bind("ElectrodeCount") %>'
                                                            BorderStyle="None" Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="烘箱送电">
                                        <HeaderTemplate>
                                            <table style="width: 100%; border-color: #000000;" border="1" cellpadding="0" cellspacing="0">
                                                <tr style="border-color: #000000;">
                                                    <td colspan="3" style="border-color: #000000;">
                                                        <asp:Label ID="Label10" runat="server" Text="烘箱送电"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="border-color: #000000;">
                                                    <td colspan="2" style="width: 50%; border-color: #000000;">
                                                        <asp:Label ID="Label11" runat="server" Text="时间"></asp:Label>
                                                    </td>
                                                    <td rowspan="2" style="width: 50%; border-color: #000000;">
                                                        <asp:Label ID="Label9" runat="server" Text="温度°C"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="border-color: #000000;">
                                                    <td style="border-color: #000000;">
                                                        <asp:Label ID="Label8" runat="server" Text="时"></asp:Label>
                                                    </td>
                                                    <td style="border-color: #000000;">
                                                        <asp:Label ID="Label12" runat="server" Text="分"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td style="width: 24%;" onkeypress="keypress()">
                                                        <asp:TextBox ID="txtOvenElectricHours" runat="server" Text='<%# Bind("OvenElectricHours") %>'
                                                            BorderStyle="None" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 24%" onkeypress="keypress()">
                                                        <asp:TextBox ID="txtOvenElectricMinute" runat="server" Text='<%# Bind("OvenElectricMinute") %>'
                                                            BorderStyle="None" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td style="width: 49%" onkeypress="keypress()">
                                                        <asp:TextBox ID="txtOvenElectricTemperature" runat="server" Text='<%# Bind("OvenElectricTemperature") %>'
                                                            BorderStyle="None" Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="恒温">
                                        <HeaderTemplate>
                                            <table style="width: 100%; border-color: #000000;" border="1" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td colspan="5" style="border-color: #000000;">
                                                        <asp:Label ID="Label13" runat="server" Text="恒温"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="border-color: #000000;">
                                                    <td rowspan="2" width="20%" style="border-color: #000000;">
                                                        <asp:Label ID="Label14" runat="server" Text="温度"></asp:Label>
                                                    </td>
                                                    <td colspan="2" width="40%" style="border-color: #000000;">
                                                        <asp:Label ID="Label15" runat="server" Text="开始时间"></asp:Label>
                                                    </td>
                                                    <td colspan="2" width="40%" style="border-color: #000000;">
                                                        <asp:Label ID="Label16" runat="server" Text="结束时间"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="border-color: #000000;">
                                                    <td width="20%" style="border-color: #000000;">
                                                        <asp:Label ID="Label17" runat="server" Text="时"></asp:Label>
                                                    </td>
                                                    <td width="20%" style="border-color: #000000;">
                                                        <asp:Label ID="Label18" runat="server" Text="分"></asp:Label>
                                                    </td>
                                                    <td width="20%" style="border-color: #000000;">
                                                        <asp:Label ID="Label19" runat="server" Text="时"></asp:Label>
                                                    </td>
                                                    <td width="20%" style="border-color: #000000;">
                                                        <asp:Label ID="Label20" runat="server" Text="分"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%;" border="1" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="20%" onkeypress="keypress()">
                                                        <asp:TextBox ID="txtConstantTemperature" runat="server" Text='<%# Bind("ConstantTemperature") %>'
                                                            BorderStyle="None" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td width="20%" onkeypress="keypress()">
                                                        <asp:TextBox ID="txtConstantStartHours" runat="server" Text='<%# Bind("ConstantStartHours") %>'
                                                            BorderStyle="None" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td width="20%" onkeypress="keypress()">
                                                        <asp:TextBox ID="txtConstantStartMinute" runat="server" Text='<%# Bind("ConstantStartMinute") %>'
                                                            BorderStyle="None" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td width="20%" onkeypress="keypress()">
                                                        <asp:TextBox ID="txtConstantEndHours" runat="server" Text='<%# Bind("ConstantEndHours") %>'
                                                            BorderStyle="None" Width="100%"></asp:TextBox>
                                                    </td>
                                                    <td width="20%" onkeypress="keypress()">
                                                        <asp:TextBox ID="txtConstantEndMinute" runat="server" Text='<%# Bind("ConstantEndMinute") %>'
                                                            BorderStyle="None" Width="100%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="移入保温箱">
                                        <HeaderTemplate>
                                            <table style="width: 100%; border-color: #000000;" border="1" cellpadding="0" cellspacing="0">
                                                <tr style="border-color: #000000;">
                                                    <td style="border-color: #000000;" colspan="3">
                                                        <asp:Label ID="Label26" runat="server" Text="移入保温箱"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="border-color: #000000;">
                                                    <td colspan="2" width="50%" style="border-color: #000000;">
                                                        <asp:Label ID="Label27" runat="server" Text="时间"></asp:Label>
                                                    </td>
                                                    <td rowspan="2" width="50%" style="border-color: #000000;">
                                                        <asp:Label ID="Label28" runat="server" Text="温度°C"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr style="border-color: #000000;">
                                                    <td width="25%" style="border-color: #000000;">
                                                        <asp:Label ID="Label29" runat="server" Text="时"></asp:Label>
                                                    </td>
                                                    <td width="25%" style="border-color: #000000;">
                                                        <asp:Label ID="Label30" runat="server" Text="分"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%; height: 100%;" border="1" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td width="25%" onkeypress="keypress()" >
                                                        <asp:TextBox ID="txtMoveInBoxHours" runat="server" Text='<%# Bind("MoveInBoxHours") %>'
                                                            BorderStyle="None" Width="90%"></asp:TextBox>
                                                    </td>
                                                    <td width="25%" onkeypress="keypress()">
                                                        <asp:TextBox ID="txtMoveInBoxMinute" runat="server" Text='<%# Bind("MoveInBoxMinute") %>'
                                                            BorderStyle="None" Width="90%"></asp:TextBox>
                                                    </td>
                                                    <td width="50%" onkeypress="keypress()">
                                                        <asp:TextBox ID="txtMoveInTemperature" runat="server" Text='<%# Bind("MoveInTemperature") %>'
                                                            BorderStyle="None" Width="90%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="烘烤次数">
                                        <ItemTemplate>
                                            <table style="width: 100%; height: 100%">
                                                <tr>
                                                    <td onkeypress="keypress()">
                                                        <asp:TextBox ID="txtBakeNumber" runat="server" Text='<%#Bind("BakeNumber") %>' Width="90% "
                                                            BorderStyle="None"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="烘烤负责人">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBakeHead" runat="server" Text='<%#Bind("BakeHead") %>' BorderStyle="None"
                                                Width="90%"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="删除">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="del" ToolTip="删除"
                                                ImageUrl="~/Images/DeleteBtn.gif" CommandArgument='<%# Bind("ElectrodeItemID") %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="GridBgColr" />
                                <RowStyle CssClass="GridRow" />
                                <PagerStyle HorizontalAlign="Left" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetElectrodeItemList"
                                TypeName="BLL.ElectrodeBakeService">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="ElectrodeID" QueryStringField="ElectrodeID" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="请注意！" ShowMessageBox="true"
        ShowSummary="false" ValidationGroup="Save" Style="z-index: 101; left: 8px; position: absolute;
        top: 8px" />
    </form>
</body>
</html>
