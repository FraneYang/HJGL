<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowJointInfoView.aspx.cs" Inherits="Web.TestPackageManage.ShowJointInfoView" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>焊口信息初始化</title>
    <link href="../Styles/Style.css" type="text/css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
      

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" style="height: 100%">
           <tr>
            <td style="width: 100%; background: url('../Images/bg-1.gif')">
                <table id="tabbtn" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" valign="middle" style="width: 100%; font-size: 11pt; font-weight: bold">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;焊口信息查看
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
            <tr>
                <td width="100%">
                    <table id="Table3" width="100%" cellpadding="0" cellspacing="0" runat="server">
                            <tr>
                                <td width="100%">
                                    <asp:GridView ID="gvJointInfo" runat="server" AllowPaging="True" AllowSorting="True"
                                        PageSize="12" AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%"
                                        DataSourceID="ObjectDataSource1" OnDataBound="gvJointInfo_DataBound" >
                                        <AlternatingRowStyle CssClass="GridBgColr" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="序号" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center"
                                                ItemStyle-Height="25px">
                                                <ItemTemplate>
                                                    <%# gvJointInfo.PageIndex * gvJointInfo.PageSize + Container.DataItemIndex + 1%>
                                                </ItemTemplate>
                                                <HeaderStyle Width="5%"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Height="25px"></ItemStyle>
                                            </asp:TemplateField>
                                                <asp:BoundField DataField="JOT_JointNo" HeaderText="焊口代号">
                                                <HeaderStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:TemplateField HeaderText="是否焊接">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# ConvertString(Eval("Is_hj")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="焊口状态">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# ConverStringJointStatus(Eval("JointStatus")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" />
                                            </asp:TemplateField>
                                                <asp:TemplateField HeaderText="委托情况">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# ConvertStringTrustFlag(Eval("JOT_TrustFlag")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="探伤情况">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# ConvertStringCheckFlag(Eval("JOT_CheckFlag")) %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="JOT_JointAttribute" HeaderText="焊口属性">
                                                <HeaderStyle Width="10%"/>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="JOT_WeldDate" HeaderText="焊接日期" DataFormatString="{0:yyyy-MM-dd}">
                                                <HeaderStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="WED_Name1" HeaderText="盖面焊工">
                                                <HeaderStyle Width="10%" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="WED_Name2" HeaderText="打底焊工">
                                                <HeaderStyle Width="10%" />
                                            </asp:BoundField>
                                        </Columns>
                                        <HeaderStyle CssClass="GridBgColr" />
                                        <RowStyle CssClass="GridRow" />
                                        <PagerTemplate>
                                            <uc1:GridNavgator ID="GridNavgator1" runat="server" />
                                        </PagerTemplate>
                                        <PagerStyle HorizontalAlign="Left" />
                                    </asp:GridView>
                                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetListData"
                                        TypeName="BLL.ShowJointInfoViewService" EnablePaging="True" SelectCountMethod="GetListCount"
                                        EnableCaching="false" OnSelecting="ObjectDataSource1_Selecting">
                                        <SelectParameters>
                                            <asp:Parameter Name="iso_id" />
                                            <asp:Parameter Name="projectId" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                        </table>
                </td>
            </tr>
    </table>
    </form>
</body>
</html>
