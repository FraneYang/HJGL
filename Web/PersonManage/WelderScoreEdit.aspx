<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WelderScoreEdit.aspx.cs"
    Inherits="Web.PersonManage.WelderScoreEdit" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>焊工业绩信息</title>
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
            function keypress() {
            var keyASCII = event.keyCode;
            if ((keyASCII >= 48 && keyASCII <= 57) || keyASCII == 46) {

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
                        <td align="left" valign="middle" style="width: 40%; font-size: 11pt; font-weight: bold">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;<asp:Label ID="lblWelderScore" runat="server" Text="lblWelderScore"></asp:Label>
                            &nbsp;
                        </td>
                        <td align="right" valign="middle" style="width: 60%; height: 30px;">
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/addbutton.gif" OnClick="btnAdd_Click" />
                            <asp:ImageButton ID="btnModify" runat="server" ImageUrl="~/Images/modybutton.gif"
                                OnClick="btnModify_Click" />
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" OnClick="btnSave_Click"
                                ValidationGroup="Save" />
                            <asp:ImageButton ID="btncancel" runat="server" ImageUrl="~/Images/cancel.gif" OnClick="btncancel_Click" />&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td height="32px" width="10%" align="right">
                            &nbsp;
                            <asp:Label ID="Label1" runat="server" Text="所在项目"></asp:Label>
                        </td>
                        <td align="left" width="40%">
                            <asp:TextBox ID="txtProjectName" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                        </td>
                        <td align="right" width="10%">
                            <asp:Label ID="Label2" runat="server" Text="所在单位"></asp:Label>
                        </td>
                        <td align="left" width="40%">
                            <asp:TextBox ID="txtUnitName" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="32px" align="right">
                            <asp:Label ID="Label3" runat="server" Text="总焊口数"></asp:Label>
                        </td>
                        <td align="left" onkeypress="keypress()">
                            <asp:TextBox ID="txtTotalJot" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label4" runat="server" Text="合格焊口数"></asp:Label>
                        </td>
                        <td align="left" onkeypress="keypress()">
                            <asp:TextBox ID="txtQualifiedJot" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="32px" align="right">
                            <asp:Label ID="Label5" runat="server" Text="施焊范围"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtWeldRange" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="Label6" runat="server" Text="备注"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtRemark" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvWelderScore" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" HorizontalAlign="Justify"
                    PageSize="12" Width="100%" OnDataBound="gvWelderScore_DataBound" OnRowCommand="gvWelderScore_RowCommand">
                    <AlternatingRowStyle CssClass="GridBgColr" />
                    <Columns>
                        <asp:BoundField DataField="ProjectName" HeaderText="所属项目">
                            <HeaderStyle Width="20%" />
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UnitName" HeaderText="所属单位">
                            <ItemStyle Width="20%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="姓名">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbTestingId" runat="server" CommandArgument='<%# Bind("WelderScoreId") %>'
                                    CssClass="ItemLink" Text='<%# Bind("WED_Name") %>' CommandName="click"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="10%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="TotalJot" HeaderText="总焊口数">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="QualifiedJot" HeaderText="合格焊口数">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="WeldRange" HeaderText="施焊范围">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Remark" HeaderText="备注">
                            <ItemStyle Width="15%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="删除">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton_1" runat="server" CommandArgument='<%# Bind("WelderScoreId") %>'
                                    CommandName="Del" ImageUrl="~/Images/DeleteBtn.gif" OnClientClick="return confirm(&quot;确定要删除此信息吗？&quot;);" />
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
                    SelectMethod="GetListData" TypeName="BLL.WelderScoreService" OnSelecting="ObjectDataSource1_Selecting">
                    <SelectParameters>
                        <asp:Parameter Name="wed_id" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
