<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleList.aspx.cs" Inherits="Web.SysManage.RoleList" %>
<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
     <link href="../Styles/Style.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
       <table id="Table1" runat="server"  width="100%" cellpadding="0" cellspacing="0">
          <tr>
            <td style="width:100%; background:url('../Images/bg-1.gif')">
              <table id="tabbtn" runat="server" width="100%"  style="background:url('../Images/bg-1.gif')" cellpadding="0" cellspacing="0">
                 <tr>
                    <td align="left" valign="middle" style="width:45%; font-size:11pt; font-weight:bold">
                       <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;角色信息
                    </td>
                      <td align="right" valign="middle" style="width:55%; height:30px;">
                         <asp:ImageButton ID="btnAdd"  runat="server" ImageUrl="~/Images/addbutton.gif" 
                          onclick="btnAdd_Click" />
                         <asp:ImageButton ID="btnModify"  runat="server" 
                            ImageUrl="~/Images/modybutton.gif" onclick="btnModify_Click" />
                         <asp:ImageButton ID="btnSave"  runat="server" ImageUrl="~/Images/savebutton.gif" 
                            onclick="btnSave_Click" ValidationGroup="Save" />
                         <asp:ImageButton ID="btncancel"  runat="server" ImageUrl="~/Images/cancel.gif" 
                            onclick="btncancel_Click" />&nbsp;
                      </td>
                 </tr>
              </table>
            </td>
          </tr>
          <tr>
             <td>
                <table id="Table2" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                   <tr style="height:32px">
                      <td align="right" style="width:10%">
                         <asp:Label ID="Label1" runat="server" Text="角色名称"></asp:Label>&nbsp;
                      </td>
                      <td align="left" style="width:23%">
                         <asp:TextBox ID="txtRoleName" runat="server" Width="80%" CssClass="textboxStyle" ValidationGroup="Save" MaxLength="25"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="TDDisplaynameValidator1" runat="server" ControlToValidate="txtRoleName" ValidationGroup="Save"
                                            Display="Dynamic" ErrorMessage="&quot;请输入角色名称！&quot;" ForeColor="Red" >*</asp:RequiredFieldValidator>
                      </td>
                       <td align="right" style="width:10%">
                         <asp:Label ID="Label2" runat="server" Text="角色描述"></asp:Label>&nbsp;
                      </td>
                      <td align="left" style="width:23%">
                         <asp:TextBox ID="txtDef" runat="server" Width="80%" CssClass="textboxStyle" MaxLength="50"></asp:TextBox>
                      </td>
                       <td align="right" style="width:14%">
                         <asp:Label ID="Label3" runat="server" Text="排列序号" Visible="false"></asp:Label>&nbsp;
                      </td>
                      <td align="left" style="width:20%">
                            <asp:TextBox ID="txtSortIndex" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                              ControlToValidate="txtSortIndex" Display="Dynamic" 
                              ErrorMessage="&quot;[排列序号]只能够为正整数！&quot;" ForeColor="Red" 
                              ValidationExpression="^0$|^[1-9][0-9]*$" ValidationGroup="Save">*</asp:RegularExpressionValidator>
                      </td>
                   </tr>
                </table>
             </td>
          </tr>
          
          <tr>
             <td>
                 <table id="Table8" width="100%" cellpadding="0" cellspacing="0" runat="server">
                         <tr>
                            <td style="width:100%">
                                <asp:GridView ID="RoleGridView" runat="server" AllowPaging="True" AllowSorting="True"
                                    PageSize="12" DataSourceID="ObjectDataSource1"
                                    AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%" 
                                    onrowcommand="RoleGridView_RowCommand" ondatabound="RoleGridView_DataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="序号" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="25px">
                                            <ItemTemplate>
                                                <%# RoleGridView.PageIndex * RoleGridView.PageSize + Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="角色名称" HeaderStyle-Width="30%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="RoleName" runat="server" CommandName="RoleNameClick" CommandArgument= '<%# Bind("RoleId") %>'
                                                    Text='<%# Bind("RoleName") %>' CssClass="ItemLink">
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:BoundField HeaderText="排列序号" HeaderStyle-Width="10%" DataField="SortIndex" ItemStyle-Height="25px" />

                                        <asp:BoundField HeaderText="角色描述" HeaderStyle-Width="50%" DataField="Def" ItemStyle-Height="25px" />

                                         <asp:TemplateField HeaderText="删除"  HeaderStyle-Width="5%" >
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="RoleDelete" 
                                                    ToolTip="删除" ImageUrl="~/Images/DeleteBtn.gif" 
                                                    CommandArgument='<%# Bind("RoleId") %>' 
                                                    onclientclick="return confirm(&quot;确定要删除此角色吗？&quot;);" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                   </Columns>
                                    <AlternatingRowStyle CssClass="GridBgColr"/>
                                    <HeaderStyle CssClass="GridBgColr"/>
                                    <RowStyle CssClass="GridRow" />      
                                     <PagerTemplate>
                                        <uc1:GridNavgator ID="GridNavgator1"  runat="server"  />
                                    </PagerTemplate>
                                    <PagerStyle HorizontalAlign="Left" />                            
                                </asp:GridView>
                                <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" TypeName="BLL.RoleService" 
                                     SelectCountMethod="getListCount" SelectMethod="getListData"  EnablePaging="true"
                                     EnableCaching="false" onselecting="ObjectDataSource1_Selecting">
                                     <SelectParameters>
                                        <asp:Parameter Name="projectId" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                         </tr>
                    </table>
             </td>
          </tr>
          </table>
          <asp:validationsummary id="ValidationSummary1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px"
				runat="server" ValidationGroup="Save" HeaderText="请注意！" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary>
    </form>
</body>
</html>
