<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowPerson.aspx.cs" Inherits="Web.WeldingManage.ShowPerson" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self" />
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript"> 
　　        function HazardTemplateClose(result) {
            window.returnValue = result;
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" runat="server" width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="3" style="width: 100%; background: url('../Images/bg-1.gif')">
                <table id="tabbtn" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" valign="middle" style="width: 20%; font-size: 11pt; font-weight: bold">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;选择人员
                        </td>
                        <td align="right" valign="middle" style="width: 80%; height: 30px;">
                            
                            <asp:ImageButton ID="imgbtnConfirm" runat="server" ImageUrl="~/Images/confirm.gif"
                                OnClick="imgbtnConfirm_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
         <tr style="height:32px">
        <td colspan="3">
          &nbsp;&nbsp;焊工代号
                            <asp:TextBox ID="txtCodeS" runat="server" Width="33%" 
                CssClass="textboxStyle" Height="16px"></asp:TextBox>&nbsp;
                            焊工姓名 <asp:TextBox ID="txtNameS" runat="server" Width="33%" CssClass="textboxStyle"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                            <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/Images/Search.gif" OnClick="imgBtnSearch_Click"
                                ToolTip="查询" />
        </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gvHazardTemplate" runat="server" AllowPaging="true" AllowSorting="True" PageSize="12" OnDataBound="gvHazardTemplate_DataBound"
                    AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%" DataSourceID="ObjectDataSource1">
                    <AlternatingRowStyle CssClass="GridBgColr" />
                    <Columns>
                        <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="25px">
                            <ItemTemplate>
                                <asp:CheckBox ID="ckbHazardTemplate" runat="server" AutoPostBack="True" OnCheckedChanged="ckbHazardTemplate_CheckedChanged" />
                                <asp:Label ID="lblWED_ID" runat="server" Text='<%# Bind("WED_ID") %>' Visible="False"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="10%" />
                            <ItemStyle HorizontalAlign="Center" Height="25px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="EDU_Name" HeaderText="班组">
                            <HeaderStyle Width="30%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="焊工代号">
                            <ItemTemplate>
                                <asp:Label ID="lblWED_Code" runat="server" Text='<%# Bind("WED_Code") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="30%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="焊工姓名">
                            <ItemTemplate>
                                <asp:Label ID="lblWED_Name" runat="server" Text='<%# Bind("WED_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="30%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                    <HeaderStyle CssClass="GridBgColr" />
                    <RowStyle CssClass="GridRow" />
                    <PagerStyle HorizontalAlign="Left" />
                    <PagerTemplate>
                        <uc1:GridNavgator ID="GridNavgator1" runat="server" />
                    </PagerTemplate>
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="getListData"
                    TypeName="BLL.PersonManageService" EnablePaging="True" OnSelecting="ObjectDataSource1_Selecting"
                    SelectCountMethod="GetListCount">
                    <SelectParameters>
                        <asp:Parameter Name="drpUnitS" />
                        <asp:Parameter Name="drpEducationS" />
                        <asp:Parameter Name="txtCodeS" />
                        <asp:Parameter Name="txtNameS" />
                        <asp:Parameter Name="txtWorkCodeS" />
                        <asp:Parameter Name="txtClassS" />
                        <asp:Parameter Name="project" />
                        <asp:Parameter Name="IfOnGuard" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
