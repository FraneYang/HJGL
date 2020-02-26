<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowTrust.aspx.cs" Inherits="Web.CheckManage.ShowTrust" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <base target="_self" />
    <link href="../Styles/Style.css" type="text/css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript"> 
　　        function ShowWorkStageClose(result) {
            window.returnValue = result;
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table cellspacing="0" cellpadding="0" width="100%" style="height: 100%">
        <tr>
            <td colspan="3" style="width: 100%; background: url('../Images/bg-1.gif')">
                <table id="tabbtn" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" valign="middle" style="width: 20%; font-size: 11pt; font-weight: bold">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            查找委托单号&nbsp;
                        </td>
                        <td align="right" valign="middle" style="width: 80%; height: 30px;">
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/sure.gif" ValidationGroup="Save"
                                OnClick="btnSave_Click" Style="height: 20px" />
                            <input id="hdSearch" type="hidden" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr style="height: 32px">
            <td colspan="3" style="border-bottom: 1px solid Black;">
                &nbsp;&nbsp;施工区域&nbsp;
                <asp:DropDownList ID="drpWorkArea" runat="server" Width="33%" Height="22px">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 委托单号
                <asp:TextBox ID="txtTrust_ID" runat="server" Width="30%" CssClass="textboxStyle"></asp:TextBox>&nbsp;
                <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/Images/Search.gif" OnClick="imgBtnSearch_Click"
                    ToolTip="查询" />
            </td>
        </tr>
        <tr>
            <td valign="top" style="width: 25%">
                <table cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td align="left">
                            <div id="div1" style="width: 220px; height: 580px; overflow: auto;" runat="server">
                                <font face="宋体">
                                    <asp:TreeView ID="tvControlItem" ForeColor="Black" runat="server" ExpandDepth="1"
                                        ShowCheckBoxes="None" Width="100%" ShowLines="True" OnSelectedNodeChanged="tvControlItem_SelectedNodeChanged"
                                        NodeIndent="15" CssClass="tree">
                                    </asp:TreeView>
                                </font>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td rowspan="2" style="width: 1px; background-color: Silver">
            </td>
            <td rowspan="2" valign="top" style="width: 735px">
                 <asp:GridView ID="gvPW_JointInfo" runat="server" AllowPaging="True" AllowSorting="True"
                    PageSize="12" DataSourceID="ObjectDataSource1" AutoGenerateColumns="False" HorizontalAlign="Justify"
                    OnPageIndexChanging="gvPW_JointInfo_PageIndexChanging" Width="100%" OnDataBound="gvPW_JointInfo_DataBound">
                    <AlternatingRowStyle CssClass="GridBgColr" />
                    <Columns>
                        <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="25px">
                            <ItemStyle HorizontalAlign="Center" Height="25px"></ItemStyle>
                            <ItemTemplate>
                                <asp:CheckBox ID="ckbJOT_ID" runat="server" />
                                <asp:Label ID="lblJOT_ID" runat="server" Text='<%# Bind("JOT_ID") %>' Visible="False"></asp:Label>
                                <asp:HiddenField ID="hdJOT_ID" runat="server" Value='<%# Bind("JOT_ID") %>' />
                            </ItemTemplate>
                            <HeaderTemplate>
                                <asp:CheckBox ID="ckbAll" runat="server" AutoPostBack="True" OnCheckedChanged="ckbAll_CheckedChanged"
                                    Text="全选" />
                            </HeaderTemplate>
                            <ItemStyle Width="10%" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="委托单号" DataField="CH_TrustCode">
                            <HeaderStyle Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="委托单位" DataField="UnitName">
                            <HeaderStyle Width="35%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="管线编号">
                            <ItemTemplate>
                                <asp:Label ID="lblISO_IsoNo" runat="server" Text='<%# Eval("ISO_IsoNo") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="20%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="JOT_JointNo" HeaderText="焊口代号">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="探伤方法" DataField="NDT_Name">
                            <HeaderStyle Width="10%" />
                        </asp:BoundField>
                       <%-- <asp:TemplateField HeaderText="需检日期">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# string.Format("{0:yyyy-MM-dd}",Eval("CH_RequestDate")).ToString() %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="15%" />
                        </asp:TemplateField>--%>
                    </Columns>
                    <HeaderStyle CssClass="GridBgColr" />
                    <RowStyle CssClass="GridRow" />
                    <PagerStyle HorizontalAlign="Left" />
                    <PagerTemplate>
                        <uc1:GridNavgator ID="GridNavgator1" runat="server" />
                    </PagerTemplate>
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetListDataTrust"
                    TypeName="BLL.CheckItemManageService" EnablePaging="True" OnSelecting="ObjectDataSource1_Selecting"
                    SelectCountMethod="GetListCount2">
                    <SelectParameters>
                        <asp:Parameter Name="trustId" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
