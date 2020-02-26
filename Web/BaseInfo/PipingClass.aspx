<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PipingClass.aspx.cs" Inherits="Web.BaseInfo.PipingClass" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>管道等级</title>
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            height: 32px;
        }
    </style>
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
                                &nbsp;管道等级
                            </td>
                            <td align="right" valign="middle" style="width: 55%; height: 30px;">
                                <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/addbutton.gif" 
                                    onclick="btnAdd_Click" />
                                <asp:ImageButton ID="btnModify" runat="server" 
                                    ImageUrl="~/Images/modybutton.gif" onclick="btnModify_Click" />
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
                            <td align="right" width="10%" class="style1">
                                &nbsp;
                                <asp:Label ID="Label1" runat="server" Text="等级代号"></asp:Label>
                            </td>
                            <td width="20%" align="left" class="style1">
                                &nbsp;
                                <asp:TextBox ID="txtPipingClassCode" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPipingClassCode"
                                    Display="Dynamic" ErrorMessage="&quot;请输入等级代号！&quot;" ForeColor="Red" SetFocusOnError="True"
                                    ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            </td>
                            <td width="10%" align="right" class="style1">
                                <asp:Label ID="Label2" runat="server" Text="等级名称"></asp:Label>
                            </td>
                            <td align="left" class="style1">
                                &nbsp;
                                <asp:TextBox ID="txtPipingClassName" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPipingClassName"
                                    Display="Dynamic" ErrorMessage="&quot;请输入等级名称！&quot;" ForeColor="Red" SetFocusOnError="True"
                                    ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            </td>
                            <td width="10%" align="right" class="style1">
                                <asp:Label ID="Label3" runat="server" Text="管道等级"></asp:Label>
                            </td>
                            <td width="20%" align="left" class="style1">
                                &nbsp;
                                <asp:TextBox ID="txtPipingClassGrade" runat="server" CssClass="textboxStyle"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPipingClassGrade"
                                    Display="Dynamic" ErrorMessage="&quot;请输入管道等级！&quot;" ForeColor="Red" SetFocusOnError="True"
                                    ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                &nbsp;
                                <asp:Label ID="Label4" runat="server" Text="备注"></asp:Label>
                            </td>
                            <td colspan="5" align="left">
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
                                <asp:ImageButton ID="btnSearch" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/search.png"
                                    OnClick="btnSearch_Click" />&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvPipingClass" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" HorizontalAlign="Justify"
                        PageSize="12" Width="100%" onrowcommand="gvPipingClass_RowCommand" 
                        ondatabound="gvPipingClass_DataBound">
                        <AlternatingRowStyle CssClass="GridBgColr" />
                        <Columns>
                            <asp:BoundField DataField="ISC_IsoCode" HeaderText="等级代号">
                                <HeaderStyle Width="15%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="等级名称">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbPipingClassID" runat="server" CommandArgument='<%# Bind("ISC_ID") %>'
                                        CssClass="ItemLink" Text='<%# Bind("ISC_IsoName") %>' CommandName="click"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="25%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ISC_IsoClass" HeaderText="管道等级">
                                <HeaderStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ISC_Remark" HeaderText="备注">
                                <HeaderStyle Width="40%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="删除">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton_1" runat="server" CommandArgument='<%# Bind("ISC_ID") %>'
                                        CommandName="Del" ImageUrl="~/Images/DeleteBtn.gif" OnClientClick="return confirm(&quot;确定要删除此装置/单元信息吗？&quot;);" />
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
                        SelectMethod="GetListData" TypeName="BLL.PipingClassService" 
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
