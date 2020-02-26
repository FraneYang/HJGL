<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeamGroup.aspx.cs" Inherits="Web.PersonManage.TeamGroup" %>

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
                            &nbsp;班组管理
                        </td>
                        <td align="right" valign="middle" style="width: 55%; height: 30px;">
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/addbutton.gif" OnClick="btnAdd_Click" />
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/Search.gif" 
                                OnClick="btnSearch_Click" />
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" ValidationGroup="Save"
                                OnClick="btnSave_Click" />
                            <asp:ImageButton ID="btncancel" runat="server" ImageUrl="~/Images/cancel.gif" OnClick="btncancel_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divEdit" runat="server" visible="false">
                    <table id="Table2" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                        <tr style="height: 32px">
                            <td align="right">
                                <asp:Label ID="Label3" runat="server" Text="所属单位"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="drpUnit" runat="server" Height="22px" Width="80%">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator2" runat="server" Display="Dynamic" ErrorMessage="&quot;请选择所属单位！&quot;"
                                ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpUnit" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                            </td>
                            <td align="right" width="10%">
                                <asp:Label ID="Label1" runat="server" Text="班组编号"></asp:Label>
                            </td>
                            <td width="40%" align="left">
                                <asp:TextBox ID="txtTeamGroupCode" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            
                        </tr>
                        <tr style="height: 32px">
                            <td align="right" width="10%">
                                <asp:Label ID="Label2" runat="server" Text="班组名称"></asp:Label>
                            </td>
                            <td width="40%" align="left">
                                <asp:TextBox ID="txtTeamGroupName" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTeamGroupName"
                                    Display="Dynamic" ErrorMessage="&quot;请输入班组名称&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            </td>
                            <td align="right">
                                <asp:Label ID="Label4" runat="server" Text="负责人"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPersonName" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divSearch" runat="server" visible="false">
                    <table style="width: 100%;">
                        <tr style="height: 32px">
                            <td align="right" width="20%">
                                <asp:Label ID="Label5" runat="server" Text="班组编号"></asp:Label>
                            </td>
                            <td align="left" width="40%">
                                <asp:TextBox ID="txtCode" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="height: 32px">
                                <asp:Label ID="Label6" runat="server" Text="班组名称"></asp:Label>
                            </td>
                            <td align="left" style="height: 32px">
                                <asp:TextBox ID="txtName" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                            </td>
                             <td>
                                &nbsp;
                            </td>                           
                        </tr>
                        <tr>
                            <td align="right" style="height: 32px">
                                <asp:Label ID="Label7" runat="server" Text="所属单位"></asp:Label>
                            </td>
                            <td align="left" style="height: 32px">
                                <asp:DropDownList ID="drpUnitId" runat="server" Height="22px" Width="90%">
                                </asp:DropDownList>
                            </td>
                             <td align="left" style="height: 32px">
                                <asp:ImageButton ID="imgbtnConfirm" runat="server" ImageUrl="~/Images/confirm.gif"
                                    OnClick="imgbtnConfirm_Click" />
                            </td>                            
                        </tr>
                         <tr style="height: 32px">
                            <td align="right" width="20%">
                                <asp:Label ID="Label8" runat="server" Text="负责人"></asp:Label>
                            </td>
                            <td align="left" width="40%">
                                <asp:TextBox ID="txtPerson" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                            </td>
                           <td align="left" style="height: 32px">
                                <asp:ImageButton ID="imgbtnCancal" runat="server" ImageUrl="~/Images/cancel.gif"
                                    OnClick="imgbtnCancal_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvTeamGroup" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" HorizontalAlign="Justify" PageSize="12" Width="100%"
                    DataSourceID="ObjectDataSource1" OnDataBound="gvTeamGroup_DataBound" OnRowCommand="gvTeamGroup_RowCommand">
                    <AlternatingRowStyle CssClass="GridBgColr" />
                    <Columns>
                        <asp:BoundField DataField="EDU_Code" HeaderText="班组编号">
                            <HeaderStyle Width="10%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="班组名称">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Bind("EDU_ID") %>'
                                    CommandName="click" CssClass="ItemLink" Text='<%# Bind("EDU_Name") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="20%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="UnitName" HeaderText="所属单位">
                            <HeaderStyle Width="20%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EDU_Main" HeaderText="负责人" />
                        <asp:TemplateField HeaderText="删除">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument='<%# Bind("EDU_ID") %>'
                                    CommandName="Del" ImageUrl="~/Images/DeleteBtn.gif" OnClientClick="return confirm(&quot;确定要删除此班组信息吗？&quot;)" />
                            </ItemTemplate>
                            <HeaderStyle Width="10%" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="GridBgColr" />
                    <PagerStyle HorizontalAlign="Left" />
                    <RowStyle CssClass="GridRow" />
                    <PagerTemplate>
                        <uc1:GridNavgator runat="server" ID="GridNavgator1" />
                    </PagerTemplate>
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="getListData"
                    TypeName="BLL.TeamGroupService" EnablePaging="True" OnSelecting="ObjectDataSource1_Selecting"
                    SelectCountMethod="GetListCount">
                    <SelectParameters>
                        <asp:Parameter Name="EDU_Code" />
                        <asp:Parameter Name="EDU_Name" />
                        <asp:Parameter Name="EDU_Unit" />
                        <asp:Parameter Name="EDU_Main" />
                        <asp:Parameter Name="project" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" Style="z-index: 101; left: 8px; position: absolute;
        top: 8px" runat="server" ShowMessageBox="True" ShowSummary="False" 
        ValidationGroup="Save" />
    </form>
</body>
</html>
