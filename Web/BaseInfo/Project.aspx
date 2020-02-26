<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Project.aspx.cs" Inherits="Web.BaseInfo.Project" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
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
                            &nbsp;项目信息
                        </td>
                        <td align="right" valign="middle" style="width: 55%; height: 30px;">
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/addbutton.gif" OnClick="btnAdd_Click" />
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" OnClick="btnSave_Click"
                                ValidationGroup="Save" />
                            <asp:ImageButton ID="btncancel" runat="server" ImageUrl="~/Images/Return.gif" OnClick="btncancel_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divSearch" runat="server" visible="false">
                    <table id="Table2" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                        <tr style="height: 32px">
                            <td align="right" style="width: 10%">
                                <asp:Label ID="Label1" runat="server" Text="项目编号"></asp:Label>&nbsp;
                            </td>
                            <td width="25%" align="left">
                                <asp:TextBox ID="txtProjectCode" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtProjectCode"
                                    Display="Dynamic" ErrorMessage="&quot;请输入项目编号&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            </td>
                            <td width="10%" align="right">
                                <asp:Label ID="Label2" runat="server" Text="项目名称"></asp:Label>&nbsp;
                            </td>
                            <td width="25%" align="left">
                                <asp:TextBox ID="txtProjectName" runat="server" CssClass="textboxStyle" Width="90%"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtProjectName"
                                    Display="Dynamic" ErrorMessage="&quot;请输入项目名称&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            </td>
                           <td width="10%" align="right">
                                <asp:Label ID="Label7" runat="server" Text="开工时间"></asp:Label>&nbsp;
                            </td>
                            <td  width="20%" align="left">
                                <asp:TextBox ID="txtStartDate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})"
                                    runat="server" Width="90%" CssClass="textboxStyle"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtStartDate"
                                    Display="Dynamic" ErrorMessage="&quot;请输入开始时间&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                      
                        <tr style="height: 32px">
                            <td align="right">
                                <asp:Label ID="Label9" runat="server" Text="项目地址"></asp:Label>&nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtProjectAddress" runat="server" Width="90%" 
                                    CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            <td align="right">
                                <asp:Label ID="Label10" runat="server" Text="备注"></asp:Label>&nbsp;
                            </td>
                            <td align="left" colspan="3">
                                <asp:TextBox ID="txtRemark" runat="server" Width="95%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="ProjectGridView" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" HorizontalAlign="Justify" PageSize="13" Width="100%"
                    DataSourceID="ObjectDataSource1" OnDataBound="ProjectGridView_DataBound" OnRowCommand="ProjectGridView_RowCommand">
                    <AlternatingRowStyle CssClass="GridBgColr" />
                    <Columns>
                        <asp:BoundField DataField="ProjectCode" HeaderText="项目编号">
                            <HeaderStyle Width="8%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="项目名称">
                            <ItemTemplate>
                                <asp:LinkButton ID="ProjectName" runat="server" CommandArgument='<%# Bind("ProjectId") %>'
                                    CommandName="ProjectName" CssClass="ItemLink" 
                                    Text='<%# Bind("ProjectName") %>' ToolTip="修改"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="15%" />
                        </asp:TemplateField>
                       
                        <asp:TemplateField HeaderText="开工时间">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("StartDate","{0:yyyy-MM-dd}") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="8%" />
                        </asp:TemplateField>

                         <asp:BoundField DataField="ProjectAddress" HeaderText="项目地址">
                            <HeaderStyle Width="30%" />
                        </asp:BoundField>

                        <asp:BoundField DataField="Remark" HeaderText="备注">
                            <HeaderStyle Width="34%" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="删除">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument='<%# Bind("ProjectId") %>'
                                    CommandName="ProjectDelete" ImageUrl="~/Images/DeleteBtn.gif" OnClientClick="return confirm(&quot;确定要删除此项目信息吗？&quot;);" />
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
                    SelectCountMethod="getListCount" SelectMethod="getListData" 
                    TypeName="BLL.ProjectService">
                    <SelectParameters>
                        <asp:Parameter Name="projectId" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" Style="z-index: 101; left: 8px; position: absolute;
        top: 8px" runat="server" HeaderText="请注意！" ShowMessageBox="True" ShowSummary="False"
        ValidationGroup="Save" />
    </form>
</body>
</html>
