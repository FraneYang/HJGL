<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EMaterialRegistEdit.aspx.cs"
    Inherits="Web.MaterialManage.EMaterialRegistEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>材料到货登记及验收记录编辑</title>
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
                            &nbsp;材料到货登记及验收记录
                        </td>
                        <td align="right" valign="middle" style="width: 50%; height: 30px;">
                            <asp:ImageButton ID="btnAddItem" runat="server" ImageUrl="~/Images/addbutton.gif"
                                OnClick="btnAddItem_Click" />
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" OnClick="btnSave_Click"
                                ValidationGroup="Save" />
                            <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/Print.gif" OnClick="btnPrint_Click" />
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
                            <asp:Label ID="Label1" runat="server" Text="材料到货登记及验收记录" Font-Bold="True" Font-Size="16pt"></asp:Label>
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
                            <asp:Label ID="Label32" runat="server" Text="供货单位"></asp:Label>
                        </td>
                        <td align="left" width="35%">
                            <asp:TextBox ID="txtUnitName1" runat="server" CssClass="textboxStyle" 
                                Width="80%"></asp:TextBox>
                        </td>
                        <td align="center" width="15%">
                            <asp:Label ID="Label31" runat="server" Text="运单或车号"></asp:Label>
                        </td>
                        <td align="left" width="35%">
                            <asp:TextBox ID="txtEMaterialRegistCode" runat="server" CssClass="textboxStyle"
                                Width="80%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEMaterialRegistCode"
                                Display="Dynamic" ErrorMessage="&quot;请输入运单或车号!&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" height="30px">
                            <asp:Label ID="Label33" runat="server" Text="送（提）货人"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtDeliveryMan" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:Label ID="Label4" runat="server" Text="登记日期"></asp:Label>
                        </td>
                        <td align="left">
                            <input id="txtEMaterialRegistDate" runat="server" readonly="readonly" class="Wdate"
                                style="width: 80%; cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
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
                            <asp:GridView ID="gvEmaterialRegist" runat="server" AllowSorting="True" PageSize="12"
                                AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%"
                                OnRowCommand="gvEmaterialRegist_RowCommand" 
                                DataSourceID="ObjectDataSource1" ondatabound="gvEmaterialRegist_DataBound">
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
                                        <HeaderStyle Width="14%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Height="20px" Width="9%"></ItemStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="规格">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSpecificationsModel" runat="server" Text='<%# Bind("SpecificationsModel") %>'
                                                BorderStyle="None" Width="90%"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="13%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="型号">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtModels" runat="server" Text='<%# Bind("Models") %>'
                                                BorderStyle="None" Width="90%"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="13%" />
                                    </asp:TemplateField>                                  
                                    <asp:TemplateField HeaderText="单位">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtUnitName2" runat="server" Text='<%# Bind("UnitName") %>'
                                                            BorderStyle="None" Width="90%"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="数量">
                                        <ItemTemplate>
                                            <table width="100%">
                                                <tr>
                                                    <td onkeypress="keypress()">
                                                        <asp:TextBox ID="txtMaterialCount" runat="server" Text='<%# Bind("MaterialCount") %>'
                                                            BorderStyle="None" Width="90%"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                        <ItemStyle Width="6%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="随货资料及编号">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtItemCode" runat="server" Text='<%#Bind("ItemCode") %>' Width="90% "
                                                BorderStyle="None"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="14%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="验收记录">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtTestrecords" runat="server" Text='<%#Bind("Testrecords") %>'
                                                BorderStyle="None" Width="90%"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="删除">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="del" ToolTip="删除"
                                                ImageUrl="~/Images/DeleteBtn.gif" CommandArgument='<%# Bind("EMaterialRegistItemId") %>' />
                                        </ItemTemplate>
                                        <ItemStyle Width="5%" />
                                    </asp:TemplateField>
                                </Columns>
                                <HeaderStyle CssClass="GridBgColr" />
                                <RowStyle CssClass="GridRow" />
                                <PagerStyle HorizontalAlign="Left" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetEMaterialRegistItemList"
                                TypeName="BLL.EMaterialRegistService">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="EMaterialRegistId" QueryStringField="EMaterialRegistId"
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
