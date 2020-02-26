<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ElectrodeRecoveryEdit.aspx.cs"
    Inherits="Web.MaterialManage.ElectrodeRecoveryEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>焊条发放回收记录编辑</title>
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
                            &nbsp;焊条发放回收记录
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
                            <asp:Label ID="Label1" runat="server" Text="焊条发放回收记录" Font-Bold="True" Font-Size="16pt"></asp:Label>
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
                            <asp:Label ID="Label3" runat="server" Text="发放回收日期"></asp:Label>
                        </td>
                        <td align="left" width="35%">
                            <input id="txtElectrodeRecoveryDate" runat="server" class="Wdate" style="width: 80%;
                                cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" /><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtElectrodeRecoveryDate"
                                    Display="Dynamic" ErrorMessage="&quot;请选择发放回收日期！&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
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
                            <asp:GridView ID="gvElectrodeCovery" runat="server" AllowSorting="True" PageSize="12"
                                AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%" 
                                OnRowCommand="gvElectrodeCovery_RowCommand" 
                                DataSourceID="ObjectDataSource1" ondatabound="gvElectrodeCovery_DataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="序号">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="材料名称" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="20px">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="drpWME_ID" runat="server" Width="90%" CssClass="textboxnoneborder">
                                            </asp:DropDownList>
                                            <asp:HiddenField ID="hdWMEID" runat="server" Value='<%# Bind("WMT_ID") %>' />
                                        </ItemTemplate>                                      
                                        <ItemStyle  Width="10%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="型号" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="20px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtModel" runat="server" Text='<%# Bind("ElectrodeRecoveryModel") %>'
                                                BorderStyle="None" Width="90%"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle  Width="8%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="焊条牌号" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="20px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtElectrodeGrade" runat="server" Text='<%# Bind("ElectrodeGrade") %>'
                                                BorderStyle="None" Width="90%"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="8%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="批号">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBatchNumber" runat="server" Text='<%# Bind("BatchNumber") %>'
                                                BorderStyle="None" Width="90%"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="入库自编号">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtInLibCode" runat="server" Text='<%# Bind("InLibCode") %>' BorderStyle="None"
                                                Width="90%"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="规格mm">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSpecifications" runat="server" Text='<%# Bind("Specifications") %>'
                                                BorderStyle="None" Width="90%"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="焊工代号">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtWelderCode" runat="server" Text='<%# Bind("WelderCode") %>' BorderStyle="None"
                                                Width="90%"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="使用部位">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUseSite" runat="server" Text='<%#Bind("UseSite") %>' Width="90% "
                                                BorderStyle="None"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="焊件材质">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtWeldingMaterial" runat="server" Text='<%#Bind("WeldingMaterial") %>'
                                                BorderStyle="None" Width="90%"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="8%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="领用数量(根)">
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td onkeypress="keypress()">
                                                        <asp:TextBox ID="txtRecipientsCount" runat="server" Text='<%#Bind("RecipientsCount") %>'
                                                            BorderStyle="None"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Width="6%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="回收数量(根)">
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td onkeypress="keypress()">
                                                        <asp:TextBox ID="txtRecoveryCount" runat="server" Text='<%#Bind("RecoveryCount") %>'
                                                            BorderStyle="None"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Width="6%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="发放人">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGrantMan" runat="server" Text='<%#Bind("GrantMan") %>' BorderStyle="None"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="删除">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="del" ToolTip="删除"
                                                ImageUrl="~/Images/DeleteBtn.gif" CommandArgument='<%# Bind("ElectrodeRecoveryItemID") %>' />
                                        </ItemTemplate>
                                         <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="GridBgColr" />
                                <RowStyle CssClass="GridRow" />
                                <PagerStyle HorizontalAlign="Left" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetElectrodeRecoveryItemList"
                                TypeName="BLL.ElectrodeRecoveryService">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="ElectrodeRecoveryId" QueryStringField="ElectrodeRecoveryId"
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
