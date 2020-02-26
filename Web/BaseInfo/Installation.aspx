<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Installation.aspx.cs" Inherits="Web.BaseInfo.Installation" %>
<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>装置</title>
     <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" language="javascript">
         function CheckDropDownList(source, args) {
             if (args.Value == "0")
                 args.IsValid = false;
             else
                 args.IsValid = true;
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
                            &nbsp;装置设置
                        </td>
                        <td align="right" valign="middle" style="width: 55%; height: 30px;">
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/addbutton.gif" OnClick="btnAdd_Click" />
                            <asp:ImageButton ID="btnModify" runat="server" ImageUrl="~/Images/modybutton.gif"
                                OnClick="btnModify_Click" />
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" OnClick="btnSave_Click"
                                ValidationGroup="Save" />
                            <asp:ImageButton ID="btncancel" runat="server" ImageUrl="~/Images/cancel.gif" OnClick="btncancel_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table2" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                    <tr>
                     <td width="10%" align="right" height="32px">
                            <asp:Label ID="Label1" runat="server" Text="装置代码"></asp:Label>
                        </td>
                        <td width="40%" align="left" height="32px">
                            &nbsp;
                            <asp:TextBox ID="txtInstallationCode" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtInstallationCode" Display="Dynamic" 
                                ErrorMessage="&quot;请输入装置代码&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td width="10%" align="right" height="32px">
                            <asp:Label ID="Label2" runat="server" Text="装置名称"></asp:Label>
                        </td>
                        <td width="40%" align="left" height="32px">
                            &nbsp;
                            <asp:TextBox ID="txtInstallationName" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtInstallationName" Display="Dynamic" 
                                ErrorMessage="&quot;请输入装置名称&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label4" runat="server" Text="装置描述"></asp:Label>
                        </td>
                        <td colspan="3" align="left" height="32px">
                            &nbsp;
                            <asp:TextBox ID="txtDef" runat="server" Width="90%" CssClass="textboxStyle"></asp:TextBox>
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
                <asp:GridView ID="gvWorkArea" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" HorizontalAlign="Justify"
                    PageSize="12" Width="100%" ondatabound="gvWorkArea_DataBound" 
                    onrowcommand="gvWorkArea_RowCommand">
                    <AlternatingRowStyle CssClass="GridBgColr" />
                    <Columns>
                      <asp:BoundField DataField="InstallationCode" HeaderText="装置编号" >
                        <HeaderStyle Width="25%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="装置名称">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbWorkName" runat="server" CommandArgument='<%# Bind("InstallationId") %>'
                                    CssClass="ItemLink" Text='<%# Bind("InstallationName") %>' 
                                    CommandName="InstallationName"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="25%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Def" HeaderText="装置描述" >
                        <HeaderStyle Width="45%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="删除">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument='<%# Bind("InstallationId") %>'
                                    CommandName="DeleteInstallation" ImageUrl="~/Images/DeleteBtn.gif" OnClientClick="return confirm(&quot;确定要删除此装置/单元信息吗？&quot;);" />
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
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" OnSelecting="ObjectDataSource1_Selecting"
                    SelectCountMethod="getListCount" SelectMethod="getListData" TypeName="BLL.InstallationService">
                    <SelectParameters>
                        <asp:Parameter Name="searchItem" />
                        <asp:Parameter Name="searchValue" /> 
                        <asp:Parameter Name="projectId" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" Style="z-index: 101; left: 7px; position: absolute;
            top: -5px" runat="server" HeaderText="请注意！" 
        ShowMessageBox="True" ShowSummary="False" ValidationGroup="Save" />
    </form>
</body>
</html>
