<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EWeldRHRecordEdit.aspx.cs"
    Inherits="Web.MaterialManage.EWeldRHRecordEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>焊材库温湿度记录</title>
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
                            &nbsp;焊材库温湿度记录
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
                        </td>
                        <td style="width: 50%; height: 50px; vertical-align: middle; border-color: #bcd2e7">
                            <asp:Label ID="Label1" runat="server" Text="焊材库温湿度记录表" Font-Bold="True" Font-Size="16pt"></asp:Label>
                        </td>
                        <td align="left" style="width: 25%; height: 50px; vertical-align: top; border-color: #bcd2e7">
                            <br />
                            <asp:Label ID="Label2" runat="server" Text="工程名称："></asp:Label>
                            <asp:Label ID="lblProjectName" runat="server" Text="lblProjectName"></asp:Label>
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
                        <td align="center" height="30px" width="15%">
                            <asp:Label ID="Label31" runat="server" Text="编号"></asp:Label>
                        </td>
                        <td align="left" width="35%">
                            <asp:TextBox ID="txtEWeldRHRecordCode" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEWeldRHRecordCode"
                                Display="Dynamic" ErrorMessage="&quot;请输入编号!&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
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
                            <asp:GridView ID="gvEWeldRHRecord" runat="server" AllowSorting="True" PageSize="12"
                                AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%" OnRowDataBound="gvEWeldRHRecord_RowDataBound"
                                OnRowCommand="gvEWeldRHRecord_RowCommand" DataSourceID="ObjectDataSource1">
                                <Columns>
                                    <asp:TemplateField HeaderText="月">
                                        <ItemTemplate>
                                            <table border="0" width="100%">
                                                <tr>
                                                    <td align="center">
                                                        <asp:TextBox ID="txtEWeldRHRecordMonth" runat="server" Text='<%# Bind("EWeldRHRecordMonth") %>'
                                                            BorderStyle="None" Width="90%"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
ControlToValidate="txtEWeldRHRecordMonth" ErrorMessage="请输入正确的月份" ValidationExpression="^(0?[[1-9]|1[0-2])$" Display="Dynamic" ForeColor="Red" ></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Width="13%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="日">
                                        <ItemTemplate>
                                            <table width="100%" border="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtEWeldRHRecordDay" runat="server" Text='<%# Bind("EWeldRHRecordDay") %>'
                                                            BorderStyle="None" Width="90%"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
ControlToValidate="txtEWeldRHRecordDay" ValidationExpression="[0-2]\d|3[0-1]|[1-9]" ErrorMessage="请输入正确的日期" Display="Dynamic" ForeColor="Red" ></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Width="13%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="时">
                                        <ItemTemplate>
                                            <table width="100%" border="0">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtEWeldRHRecordHours" runat="server" Text='<%#Bind("EWeldRHRecordHours") %>'
                                                            Width="90% " BorderStyle="None"></asp:TextBox>
  <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" 
ControlToValidate="txtEWeldRHRecordHours" ErrorMessage="请输入正确的时间" ValidationExpression="[0-1]\d|2[0-4]|[0-9]" Display="Dynamic" ForeColor="Red" ></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Width="13%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="室温（℃）">
                                        <ItemTemplate>
                                            <table width="100%" border="0">
                                                <tr>
                                                    <td onkeypress="keypress()">
                                                        <asp:TextBox ID="txtRoomTemperature" runat="server" Text='<%# Bind("RoomTemperature") %>'
                                                            BorderStyle="None" Width="90%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Width="13%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="湿度（%）">
                                        <ItemTemplate>
                                            <table width="100%" border="0">
                                                <tr>
                                                    <td onkeypress="keypress()">
                                                        <asp:TextBox ID="txtHumidity" runat="server" Text='<%#Bind("Humidity") %>' BorderStyle="None"
                                                            Width="90%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Width="13%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="记录人（保管员）">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRecordMan" runat="server" Text='<%#Bind("RecordMan") %>' BorderStyle="None"
                                                Width="90%"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="13%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="备注">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemark" runat="server" Text='<%#Bind("Remark") %>' BorderStyle="None"
                                                Width="90%"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="17%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="删除">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="del" ToolTip="删除"
                                                ImageUrl="~/Images/DeleteBtn.gif" CommandArgument='<%# Bind("EWeldRHRecordItemId") %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="GridBgColr" />
                                <RowStyle CssClass="GridRow" />
                                <PagerStyle HorizontalAlign="Left" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetEWeldRHRecordItemList"
                                TypeName="BLL.EWeldRHRecordService">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="EWeldRHRecordId" QueryStringField="EWeldRHRecordId"
                                        Type="String" />
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
