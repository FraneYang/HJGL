<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Components.aspx.cs" Inherits="Web.BaseInfo.Components" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>安装组件</title>
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="Table1" runat="server" width="100%" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 100%; background: url('../Images/bg-1.gif')">
                    <table id="tabbtn" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                        cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" valign="middle" style="width: 45%; font-size: 11pt; font-weight: bold">
                                <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                                &nbsp;安装组件
                            </td>
                            <td align="right" valign="middle" style="width: 55%; height: 30px;">
                                <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/addbutton.gif" 
                                    onclick="btnAdd_Click" />
                                <asp:ImageButton ID="btnModify" runat="server" ImageUrl="~/Images/modybutton.gif"
                                    Style="height: 20px" onclick="btnModify_Click" />
                                <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" 
                                    ValidationGroup="Save" onclick="btnSave_Click" />
                                <asp:ImageButton ID="btncancel" runat="server" ImageUrl="~/Images/cancel.gif" 
                                    onclick="btncancel_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td align="right" width="20%" class="style1" height="32px">
                                &nbsp;
                                <asp:Label ID="Label1" runat="server" Text="组件代号"></asp:Label>
                            </td>
                            <td width="30%" align="left" class="style1">
                                &nbsp;
                                <asp:TextBox ID="txtComCode" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtComCode"
                                    Display="Dynamic" ErrorMessage="&quot;请输入组件代号！&quot;" ForeColor="Red" SetFocusOnError="True"
                                    ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            </td>
                            <td width="20%" align="right" class="style1">
                                <asp:Label ID="Label2" runat="server" Text="组件名称"></asp:Label>
                            </td>
                            <td align="left" class="style1" width="30%">
                                &nbsp;
                                <asp:TextBox ID="txtComName" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtComName"
                                    Display="Dynamic" ErrorMessage="&quot;请输入组件名称！&quot;" ForeColor="Red" SetFocusOnError="True"
                                    ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" height="32px">
                                &nbsp;
                                <asp:Label ID="Label4" runat="server" Text="备注"></asp:Label>
                            </td>
                            <td colspan="3" align="left">
                                &nbsp;
                                <asp:TextBox ID="txtRemark" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="Table3" runat="server" style="background: url('../Images/bg-1.gif')" width="100%"
                        cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="left" valign="middle" style="width: 100%">
                                <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image1" runat="server" />
                                &nbsp;
                                <asp:DropDownList ID="drpSearch" runat="server" Height="22px">
                                </asp:DropDownList>
                                <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
                                <asp:ImageButton ID="btnSearch" runat="server" ImageAlign="AbsMiddle" 
                                    ImageUrl="~/Images/search.png" onclick="btnSearch_Click" />&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvComponent" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" HorizontalAlign="Justify"
                        PageSize="12" Width="100%" 
                        onrowcommand="gvComponent_RowCommand" ondatabound="gvComponent_DataBound" 
                        DataSourceID="ObjectDataSource1">
                        <AlternatingRowStyle CssClass="GridBgColr" />
                        <Columns>
                            <asp:BoundField DataField="COM_Code" HeaderText="组件代号">
                                <HeaderStyle Width="20%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="组件名称">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbTestingId" runat="server" CommandArgument='<%# Bind("COM_ID") %>'
                                        CssClass="ItemLink" Text='<%# Bind("COM_Name") %>' CommandName="click"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="20%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="COM_Remark" HeaderText="备注">
                                <HeaderStyle Width="55%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="删除">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton_1" runat="server" CommandArgument='<%# Bind("COM_ID") %>'
                                        CommandName="Del" ImageUrl="~/Images/DeleteBtn.gif" OnClientClick="return confirm(&quot;确定要删除此组件吗？&quot;);" />
                                </ItemTemplate>
                                <HeaderStyle Width="5%" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="GridBgColr" />
                        <PagerStyle HorizontalAlign="Left" />
                        <RowStyle CssClass="GridRow" />
                        <PagerTemplate>
                            <uc1:GridNavgator ID="GridNavgator1" runat="server" />
                        </PagerTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" SelectCountMethod="GetListCount"
                        SelectMethod="GetListData" TypeName="BLL.ComponentsService" 
                        onselecting="ObjectDataSource1_Selecting">
                        <SelectParameters>
                            <asp:Parameter Name="searchItem" />
                            <asp:Parameter Name="searchValue" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
        <asp:ValidationSummary ID="ValidationSummary1" Style="z-index: 101; left: 7px; position: absolute;
            top: -5px" runat="server" HeaderText="请注意！" ShowMessageBox="True" ShowSummary="False"
            ValidationGroup="Save" />
    </div>
    </form>
</body>
</html>
