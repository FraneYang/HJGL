<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" EnableEventValidation="false" Inherits="Web.SysManage.UserList" %>
<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
                            &nbsp;用户信息
                        </td>
                        <td align="right" valign="middle" style="width: 55%; height: 30px;">
                            <asp:ImageButton ID="imgbtnOut" runat="server" ImageUrl="~/Images/Export.gif" 
                    OnClick="imgbtnOut_Click" />
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/addbutton.gif" OnClick="btnAdd_Click" />
                            <asp:ImageButton ID="btnModify" runat="server" ImageUrl="~/Images/modybutton.gif"
                                OnClick="btnModify_Click" />
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" ValidationGroup="Save"
                                OnClick="btnSave_Click" />
                            <asp:ImageButton ID="btncancel" runat="server" ImageUrl="~/Images/cancel.gif" OnClick="btncancel_Click" />&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table2" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                    <tr>
                        <td align="right" style="height: 32px" width="10%">
                            <asp:Label ID="Label1" runat="server" Text="帐号"></asp:Label>
                        </td>
                        <td align="left" style="height: 32px" width="23%">
                            <asp:TextBox ID="txtAccount" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtAccount"
                                Display="Dynamic" ErrorMessage="请输入帐号" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="right" style="height: 32px" width="10%">
                            <asp:Label ID="Label2" runat="server" Text="姓名"></asp:Label>
                        </td>
                        <td align="left" style="height: 32px" width="23%">
                            <asp:TextBox ID="txtUserName" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtUserName"
                                Display="Dynamic" ErrorMessage="请输入姓名" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="right" style="height: 32px" width="10%">
                            <asp:Label ID="lbRole" runat="server" Text="所属角色"></asp:Label>
                        </td>
                        <td align="left" style="height: 32px" width="24%">
                        <asp:DropDownList ID="drpRole" runat="server" Height="22" Width="81%" 
                                CssClass="textboxStyle">
                            </asp:DropDownList>
                             <asp:CustomValidator ID="CustomValidator3" runat="server" Display="Dynamic" ErrorMessage="&quot;请选择所属角色！&quot;"
                                ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpRole" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr runat="server" id="trProject">
                        <td align="right" style="height: 32px" width="10%">
                        <asp:Label ID="Label4" runat="server" Text="用户编号"></asp:Label>    
                        </td>
                        <td align="left" style="height: 32px" width="23%">
                            <asp:TextBox ID="txtUserCode" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtUserCode"
                                Display="Dynamic" ErrorMessage="请输入用户编号" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>                       
                       <td align="right" style="height: 32px" width="10%">
                            <asp:Label ID="Label5" runat="server" Text="单位"></asp:Label>
                        </td>
                        <td align="left" style="height: 32px" width="23%">
                            <asp:DropDownList ID="drpUnit" runat="server" Height="22" Width="81%" 
                                CssClass="textboxStyle">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" Display="Dynamic" ErrorMessage="&quot;请选择单位！&quot;"
                                ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpUnit" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                        </td>
                        <td align="right" style="height: 32px" width="10%">
                            <asp:Label ID="Label7" runat="server" Text="是否在岗"></asp:Label>
                        </td>
                        <td align="left" style="height: 32px" width="23%">
                            <asp:DropDownList ID="drpIsPost" runat="server" Height="22px" Width="81%" CssClass="textboxStyle">
                                     <asp:ListItem Value="True" Selected="True">是</asp:ListItem>
                                    <asp:ListItem Value="False">否</asp:ListItem>
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator2" runat="server" Display="Dynamic" ErrorMessage="&quot;请选择是否在岗！&quot;"
                                ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpIsPost" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr runat="server" id="tr1">
                        <td align="right" style="height: 32px" width="10%">
                        <asp:Label ID="Label3" runat="server" Text="备注"></asp:Label>    
                        </td>
                        <td align="left" colspan="5" style="height: 32px" width="23%">
                            <asp:TextBox ID="txtRemark" runat="server" Width="95%" CssClass="textboxStyle"></asp:TextBox>
                        </td>                       
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table3" runat="server" cellpadding="0" cellspacing="0" style="background: url('../Images/bg-1.gif')"
                    width="100%">
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
                <asp:GridView ID="gvUser" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                    HorizontalAlign="Justify" PageSize="10" Width="100%" OnDataBound="gvUser_DataBound"
                    OnRowCommand="gvUser_RowCommand" DataSourceID="ObjectDataSource1">
                    <AlternatingRowStyle CssClass="GridBgColr" />
                    <Columns>
                        <asp:BoundField DataField="Account" HeaderText="帐号">
                            <HeaderStyle Width="10%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="姓名">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnUserName" runat="server" CommandName="click" CssClass="ItemLink"
                                    Text='<%# Bind("UserName") %>' CommandArgument='<%# Bind("UserId") %>' ToolTip="修改用户信息"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="UnitName" HeaderText="单位">
                            <HeaderStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UserCode" HeaderText="编号">
                            <HeaderStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Role" HeaderText="所属角色">
                            <HeaderStyle Width="10%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="是否在岗">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# ConvertIsPost(Eval("IsPost")) %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Remark" HeaderText="备注">
                            <HeaderStyle Width="20%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="删除">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/DeleteBtn.gif"
                                    CommandArgument='<%# Bind("UserId") %>' CommandName="Del" OnClientClick="return confirm(&quot;确定要删除此用户信息吗？&quot;)"
                                    ToolTip="删除" />
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="GridBgColr" />
                    <PagerStyle HorizontalAlign="Left" />
                    <RowStyle CssClass="GridRow" />
                    <PagerTemplate>
                        <uc1:GridNavgator runat="server" ID="GridNavgator1" />
                    </PagerTemplate>
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetListData"
                    TypeName="BLL.UserService" EnablePaging="True" OnSelecting="ObjectDataSource1_Selecting"
                    SelectCountMethod="GetListCount">
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
            top: -5px" runat="server" HeaderText="请注意！" ShowMessageBox="True" ShowSummary="False"
            ValidationGroup="Save" />
    </form>
</body>
</html>
