<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Control.aspx.cs" Inherits="Web.BaseInfo.Control" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>直径寸径对照</title>
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script language="javascript"> 
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
    <table id="Table1" runat="server" width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 100%; background: url('../Images/bg-1.gif')">
                <table id="tabbtn" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" valign="middle" style="width: 45%; font-size: 11pt; font-weight: bold">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;直径寸径对照
                        </td>
                        <td align="right" valign="middle" style="width: 55%; height: 30px;">
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/addbutton.gif" OnClick="btnAdd_Click" />
                            <asp:ImageButton ID="btnModify" runat="server" ImageUrl="~/Images/modybutton.gif"
                                Style="height: 20px" OnClick="btnModify_Click" />
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" ValidationGroup="Save"
                                OnClick="btnSave_Click" />
                            <asp:ImageButton ID="btncancel" runat="server" ImageUrl="~/Images/cancel.gif" OnClick="btncancel_Click"
                                Style="height: 20px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr height="32px">
                        <%--<td align="right" height="32px" width="10%">
                            <asp:Label ID="Label1" runat="server" Text="外径代号"></asp:Label>
                        </td>
                        <td align="left" width="15%">
                            <asp:TextBox ID="txtcode" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtcode"
                                Display="Dynamic" ErrorMessage="&quot;请输入外径代号&quot;" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="right" width="10%">
                            <asp:Label ID="Label2" runat="server" Text="外径值"></asp:Label>
                        </td>
                        <td align="left" width="15%">
                            <asp:TextBox ID="txtDia" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDia"
                                Display="Dynamic" ErrorMessage="&quot;请输入正确的【外径值】！&quot;" ForeColor="Red"
                                ValidationExpression="^[-+]?\d+(\.\d+)?$" ValidationGroup="Save">*</asp:RegularExpressionValidator>
                        </td>--%>
                        <td align="right" width="10%">
                            <asp:Label ID="Label3" runat="server" Text="公称直径"></asp:Label>
                        </td>
                        <td align="left" width="15%">
                            <asp:TextBox ID="txtDN" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDN"
                                Display="Dynamic" ErrorMessage="&quot;请输入公称直径&quot;" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDN"
                                Display="Dynamic" ErrorMessage="&quot;请输入正确的【公称直径】！&quot;" ForeColor="Red"
                                ValidationExpression="^[-+]?\d+(\.\d+)?$" ValidationGroup="Save">*</asp:RegularExpressionValidator>
                        </td>
                        <td align="right" width="10%">
                            <asp:Label ID="Label4" runat="server" Text="寸径值"></asp:Label>
                        </td>
                        <td align="left" width="15%">
                            <asp:TextBox ID="txtInch" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtInch"
                                Display="Dynamic" ErrorMessage="&quot;请输入寸径值&quot;" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtInch"
                                Display="Dynamic" ErrorMessage="&quot;请输入正确的【寸径值】！&quot;" ForeColor="Red"
                                ValidationExpression="^[-+]?\d+(\.\d+)?$" ValidationGroup="Save">*</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                  <%--  <tr>
                        <td align="right" height="32px">
                            <asp:Label ID="Label5" runat="server" Text="Sch5s"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSch5s" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtSch5s"
                                Display="Dynamic" ErrorMessage="&quot;请输入正确的【Sch5s】！&quot;" ForeColor="Red"
                                ValidationExpression="^[-+]?\d+(\.\d+)?$" ValidationGroup="Save">*</asp:RegularExpressionValidator>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label6" runat="server" Text="Sch10s"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSch10s" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtSch10s"
                                Display="Dynamic" ErrorMessage="&quot;请输入正确的【Sch10s】！&quot;" ForeColor="Red"
                                ValidationExpression="^[-+]?\d+(\.\d+)?$" ValidationGroup="Save">*</asp:RegularExpressionValidator>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label7" runat="server" Text="Sch10"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSch10" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtSch10"
                                Display="Dynamic" ErrorMessage="&quot;请输入正确的【Sch10】！&quot;" ForeColor="Red"
                                ValidationExpression="^[-+]?\d+(\.\d+)?$" ValidationGroup="Save">*</asp:RegularExpressionValidator>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label8" runat="server" Text="Sch20"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSch20" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtSch20"
                                Display="Dynamic" ErrorMessage="&quot;请输入正确的【Sch20】！&quot;" ForeColor="Red"
                                ValidationExpression="^[-+]?\d+(\.\d+)?$" ValidationGroup="Save">*</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="32px">
                            <asp:Label ID="Label9" runat="server" Text="Sch30"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSch30" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtSch30"
                                Display="Dynamic" ErrorMessage="&quot;请输入正确的【Sch30】！&quot;" ForeColor="Red"
                                ValidationExpression="^[-+]?\d+(\.\d+)?$" ValidationGroup="Save">*</asp:RegularExpressionValidator>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label10" runat="server" Text="Sch40s"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSch40s" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtSch40s"
                                Display="Dynamic" ErrorMessage="&quot;请输入正确的【Sch40s】！&quot;" ForeColor="Red"
                                ValidationExpression="^[-+]?\d+(\.\d+)?$" ValidationGroup="Save">*</asp:RegularExpressionValidator>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label11" runat="server" Text="STD"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSTD" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label12" runat="server" Text="Sch40"></asp:Label>
                        </td>
                        <td align="left" onkeypress="keypress()">
                            <asp:TextBox ID="txtSch40" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ControlToValidate="txtSch40"
                                Display="Dynamic" ErrorMessage="&quot;请输入正确的【Sch40】！&quot;" ForeColor="Red"
                                ValidationExpression="^[-+]?\d+(\.\d+)?$" ValidationGroup="Save">*</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="32px">
                            <asp:Label ID="Label13" runat="server" Text="Sch60"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSch60" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="txtSch60"
                                Display="Dynamic" ErrorMessage="&quot;请输入正确的【Sch60】！&quot;" ForeColor="Red"
                                ValidationExpression="^[-+]?\d+(\.\d+)?$" ValidationGroup="Save">*</asp:RegularExpressionValidator>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label14" runat="server" Text="Sch80s"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSch80s" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtSch80s"
                                Display="Dynamic" ErrorMessage="&quot;请输入正确的【Sch80s】！&quot;" ForeColor="Red"
                                ValidationExpression="^[-+]?\d+(\.\d+)?$" ValidationGroup="Save">*</asp:RegularExpressionValidator>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label15" runat="server" Text="XS"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtXS" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator13" runat="server" ControlToValidate="txtXS"
                                Display="Dynamic" ErrorMessage="&quot;请输入正确的【XS】！&quot;" ForeColor="Red"
                                ValidationExpression="^[-+]?\d+(\.\d+)?$" ValidationGroup="Save">*</asp:RegularExpressionValidator>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label16" runat="server" Text="Sch80"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSch80" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator14" runat="server" ControlToValidate="txtSch80"
                                Display="Dynamic" ErrorMessage="&quot;请输入正确的【Sch80】！&quot;" ForeColor="Red"
                                ValidationExpression="^[-+]?\d+(\.\d+)?$" ValidationGroup="Save">*</asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" height="32px">
                            <asp:Label ID="Label17" runat="server" Text="Sch100"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSch100" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" ControlToValidate="txtSch100"
                                Display="Dynamic" ErrorMessage="&quot;请输入正确的【Sch100】！&quot;" ForeColor="Red"
                                ValidationExpression="^[-+]?\d+(\.\d+)?$" ValidationGroup="Save">*</asp:RegularExpressionValidator>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label18" runat="server" Text="Sch120"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSch120" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" ControlToValidate="txtSch120"
                                Display="Dynamic" ErrorMessage="&quot;请输入正确的【Sch120】！&quot;" ForeColor="Red"
                                ValidationExpression="^[-+]?\d+(\.\d+)?$" ValidationGroup="Save">*</asp:RegularExpressionValidator>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label19" runat="server" Text="Sch140"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSch140" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator17" runat="server" ControlToValidate="txtSch140"
                                Display="Dynamic" ErrorMessage="&quot;请输入正确的【Sch140】！&quot;" ForeColor="Red"
                                ValidationExpression="^[-+]?\d+(\.\d+)?$" ValidationGroup="Save">*</asp:RegularExpressionValidator>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label20" runat="server" Text="Sch160"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtSch160" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator18" runat="server" ControlToValidate="txtSch160"
                                Display="Dynamic" ErrorMessage="&quot;请输入正确的【Sch160】！&quot;" ForeColor="Red"
                                ValidationExpression="^[-+]?\d+(\.\d+)?$" ValidationGroup="Save">*</asp:RegularExpressionValidator>
                        </td>
                    </tr>--%>
                    <tr height="32px">
                        <%--<td align="right" height="32px">
                            <asp:Label ID="Label21" runat="server" Text="XXS"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtXXS" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator19" runat="server" ControlToValidate="txtXXS"
                                Display="Dynamic" ErrorMessage="&quot;请输入正确的【XXS】！&quot;" ForeColor="Red"
                                ValidationExpression="^[-+]?\d+(\.\d+)?$" ValidationGroup="Save">*</asp:RegularExpressionValidator>
                        </td>--%>
                        <td align="right">
                            <asp:Label ID="Label22" runat="server" Text="备注"></asp:Label>
                        </td>
                        <td align="left" colspan="3">
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
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
    </table>
    <%--<div id="div1" runat="server" style="overflow: auto;">--%>
        <table id="Table8" width="100%" cellpadding="0" cellspacing="0" runat="server">
            <tr>
                <td>
                    <asp:GridView ID="gvControl" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" HorizontalAlign="Justify"
                        PageSize="12" Width="100%" OnDataBound="gvControl_DataBound" OnRowCommand="gvControl_RowCommand">
                        <AlternatingRowStyle CssClass="GridBgColr" />
                        <Columns>
                           <%-- <asp:TemplateField HeaderText="外径代号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbTestingId" runat="server" CommandArgument='<%# Bind("BST_ID") %>'
                                        CssClass="ItemLink" Text='<%# Bind("BST_Code") %>' CommandName="click"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="5%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="外径值">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# string.Format("{0:N}",Eval("BST_Dia")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="4%" />
                            </asp:TemplateField>--%>
                            <asp:BoundField DataField="BST_DN" HeaderText="公称直径">
                                <HeaderStyle Width="15%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="寸径值">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# string.Format("{0:N}",Eval("BST_Inch")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="15%" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Sch5s">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# string.Format("{0:N}",Eval("BST_Sch5s")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sch10s">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# string.Format("{0:N}",Eval("BST_Sch10s")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sch10">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# string.Format("{0:N}",Eval("BST_Sch10")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sch20">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# string.Format("{0:N}",Eval("BST_Sch20")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sch30">
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# string.Format("{0:N}",Eval("BST_Sch30")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sch40s">
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Text='<%# string.Format("{0:N}",Eval("BST_Sch40s")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="STD">
                                <ItemTemplate>
                                    <asp:Label ID="Label9" runat="server" Text='<%# string.Format("{0:N}",Eval("BST_STD")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sch40">
                                <ItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Text='<%# string.Format("{0:N}",Eval("BST_Sch40")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sch60">
                                <ItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Text='<%# string.Format("{0:N}",Eval("BST_Sch60")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sch80s">
                                <ItemTemplate>
                                    <asp:Label ID="Label12" runat="server" Text='<%# string.Format("{0:N}",Eval("BST_Sch80s")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="XS">
                                <ItemTemplate>
                                    <asp:Label ID="Label13" runat="server" Text='<%# string.Format("{0:N}",Eval("BST_XS")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sch80">
                                <ItemTemplate>
                                    <asp:Label ID="Label14" runat="server" Text='<%# string.Format("{0:N}",Eval("BST_Sch80")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sch100">
                                <ItemTemplate>
                                    <asp:Label ID="Label15" runat="server" Text='<%# string.Format("{0:N}",Eval("BST_Sch100")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sch120">
                                <ItemTemplate>
                                    <asp:Label ID="Label16" runat="server" Text='<%# string.Format("{0:N}",Eval("BST_Sch120")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sch140">
                                <ItemTemplate>
                                    <asp:Label ID="Label17" runat="server" Text='<%# string.Format("{0:N}",Eval("BST_Sch140")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sch160">
                                <ItemTemplate>
                                    <asp:Label ID="Label18" runat="server" Text='<%# string.Format("{0:N}",Eval("BST_Sch160")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="4%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="XXS">
                                <ItemTemplate>
                                    <asp:Label ID="Label19" runat="server" Text='<%# string.Format("{0:N}",Eval("BST_XXS")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="4%" />
                            </asp:TemplateField>--%>
                            <asp:BoundField DataField="BST_Remark" HeaderText="备注">
                                <HeaderStyle Width="60%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="删除">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton_1" runat="server" CommandArgument='<%# Bind("BST_ID") %>'
                                        CommandName="Del" ImageUrl="~/Images/DeleteBtn.gif" OnClientClick="return confirm(&quot;确定要删除此外径寸径对照吗？&quot;);" />
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
                        SelectMethod="GetListData" TypeName="BLL.ControlService" OnSelecting="ObjectDataSource1_Selecting">
                        <SelectParameters>
                            <asp:Parameter Name="searchItem" />
                            <asp:Parameter Name="searchValue" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
   <%-- </div>--%>
    <asp:ValidationSummary ID="ValidationSummary1" Style="z-index: 101; left: 7px; position: absolute;
        top: -5px" runat="server" HeaderText="请注意！" ShowMessageBox="True" ShowSummary="False"
        ValidationGroup="Save" />
</body>
</html>
</form>
<script language="javascript" type="text/javascript">
    var width = parent.document.getElementById("center").offsetWidth;
    if (width > 1500) {
        $("#Table8").width(width);
    }
    var height = parent.document.getElementById("center").offsetHeight;
    var table1Height = $("#Table1").height();
    var hei = height - table1Height - 5;
    $("#div1").height(hei);
</script>
