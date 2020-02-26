<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Consumables.aspx.cs" Inherits="Web.BaseInfo.Consumables" %>
<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>焊接耗材</title>
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
                            &nbsp;焊接耗材
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
                            <asp:Label ID="Label1" runat="server" Text="焊材代号"></asp:Label>
                        </td>
                        <td width="20%" align="left" height="32px">
                            &nbsp;
                            <asp:TextBox ID="txtConsumablesCode" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                ControlToValidate="txtConsumablesCode" Display="Dynamic" 
                                ErrorMessage="&quot;请输入焊材代号&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td width="10%" align="right" height="32px">
                            <asp:Label ID="Label2" runat="server" Text="焊材名称"></asp:Label>
                        </td>
                        <td width="20%" align="left" height="32px">
                            &nbsp;
                            <asp:TextBox ID="txtConsumablesName" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                ControlToValidate="txtConsumablesName" Display="Dynamic" 
                                ErrorMessage="&quot;请输入焊材名称&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                         <td width="10%" align="right" height="32px">
                            <asp:Label ID="Label5" runat="server" Text="焊材类型"></asp:Label>
                        </td>
                        <td width="20%" align="left" height="32px">
                            &nbsp;
                            <asp:DropDownList ID="drpConsumablesType" runat="server" Width="80%" CssClass="textboxStyle"></asp:DropDownList>                           
                             <asp:CustomValidator ID="CustomValidator2" runat="server" ControlToValidate="drpConsumablesType"
                                Display="Dynamic" ErrorMessage="&quot;请选择焊材类型&quot;" ForeColor="Red" ClientValidationFunction="CheckDropDownList"
                                ValidationGroup="Save">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr>
                        
                        <td align="right">
                            <asp:Label ID="Label4" runat="server" Text="备注"></asp:Label>
                        </td>
                        <td colspan="4" align="left" height="32px">
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
                <asp:GridView ID="gvConsumablesType" runat="server" AllowPaging="True" AllowSorting="True"
                    AutoGenerateColumns="False" DataSourceID="ObjectDataSource1" HorizontalAlign="Justify"
                    PageSize="12" Width="100%" ondatabound="gvConsumablesType_DataBound" 
                    onrowcommand="gvConsumablesType_RowCommand">
                    <AlternatingRowStyle CssClass="GridBgColr" />
                    <Columns>                      
                        <asp:TemplateField HeaderText="焊材代号">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbConsumables" runat="server" CommandArgument='<%# Bind("WMT_ID") %>'
                                    CssClass="ItemLink" Text='<%# Bind("WMT_MatCode") %>' 
                                    CommandName="ConsumablesLink"></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="15%" />
                        </asp:TemplateField>   
                        <asp:BoundField DataField="WMT_MatName" HeaderText="焊材名称" >
                            <HeaderStyle Width="15%" />
                        </asp:BoundField>                       
                          <asp:TemplateField HeaderText="焊材类型">
                         <ItemTemplate >
                                <asp:Label ID="labConsumablesType" runat="server" Text='<%# ConsumablesTypeName(Eval("WMT_MatType")) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="15%" />
                       </asp:TemplateField>
                        <asp:BoundField DataField="WMT_Remark" HeaderText="备注" >
                            <HeaderStyle Width="25%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="删除">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument='<%# Bind("WMT_ID") %>'
                                    CommandName="DeleteConsumables" ImageUrl="~/Images/DeleteBtn.gif" OnClientClick="return confirm(&quot;确定要删除此焊接耗材信息吗？&quot;);" />
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
                    SelectCountMethod="getListCount" SelectMethod="getListData" TypeName="BLL.ConsumablesService">
                    <SelectParameters>
                        <asp:Parameter Name="searchItem" />
                        <asp:Parameter Name="searchValue" /> 
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
