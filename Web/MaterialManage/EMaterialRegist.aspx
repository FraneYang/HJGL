<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EMaterialRegist.aspx.cs"
    Inherits="Web.MaterialManage.EMaterialRegist" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>材料到货登记及验收记录</title>
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
                        <td align="left" valign="middle" style="width: 25%; font-size: 11pt; font-weight: bold">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;材料到货登记及验收记录
                        </td>
                        <td align="right" valign="middle" style="width: 75%; height: 30px;">
                            <asp:ImageButton ID="btnAddButton" runat="server" ImageUrl="~/Images/addbutton.gif"
                                OnClick="btnAddButton_Click" />                           
                        </td>
                    </tr>
                </table>
            </td>
        </tr>        
    </table>
    <div id="div1" runat="server" style="overflow: auto;">
        <table id="Table8" width="100%" cellpadding="0" cellspacing="0" runat="server">
            <tr>
                <td style="width: 100%">
                    <asp:GridView ID="gvEMaterialRegist" runat="server" AllowPaging="True" AllowSorting="True"
                        PageSize="12" DataSourceID="ObjectDataSource1" AutoGenerateColumns="False" HorizontalAlign="Justify"
                        Width="100%" OnDataBound="gvEMaterialRegist_DataBound" OnRowCommand="gvEMaterialRegist_RowCommand">
                        <AlternatingRowStyle CssClass="GridBgColr" />
                        <Columns>
                            <asp:TemplateField HeaderText="运单或车号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnEMaterialRegistCode" runat="server" CommandArgument='<%# Bind("EMaterialRegistId") %>'
                                        CommandName="click" CssClass="ItemLink" Text='<%# Bind("EMaterialRegistCode") %>'
                                        ToolTip="修改"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="15%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ProjectName" HeaderText="工程名称">
                                <ItemStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UnitName" HeaderText="供货单位">
                            <ItemStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DeliveryMan" HeaderText="送（提）货人">
                            <ItemStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EMaterialRegistDate" DataFormatString="{0:d}" 
                                HeaderText="登记日期">
                                <ItemStyle Width="15%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CompileDate" DataFormatString="{0:d}" HeaderText="编制日期">
                                <ItemStyle Width="15%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="删除">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="del" ToolTip="删除"
                                        ImageUrl="~/Images/DeleteBtn.gif" CommandArgument='<%# Bind("EMaterialRegistId") %>'
                                        OnClientClick="return confirm(&quot;确定要删除此条信息吗？&quot;);" />
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="GridBgColr" />
                        <RowStyle CssClass="GridRow" />
                        <PagerTemplate>
                            <uc1:GridNavgator ID="GridNavgator1" runat="server" />
                        </PagerTemplate>
                        <PagerStyle HorizontalAlign="Left" />
                    </asp:GridView>
                    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" TypeName="BLL.EMaterialRegistService"
                        SelectCountMethod="GetListCount" SelectMethod="GetListData" EnablePaging="true"
                        EnableCaching="false" OnSelecting="ObjectDataSource1_Selecting">
                        <SelectParameters>
                            <asp:Parameter Name="projectId" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
