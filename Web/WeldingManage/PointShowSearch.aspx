<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PointShowSearch.aspx.cs"
    Inherits="Web.WeldingManage.PointShowSearch" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>点口查找管线</title>
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
            <td colspan="3" style="background: url('../Images/bg-1.gif')" class="style1">
                <table id="tabbtn" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" valign="middle" style="width: 20%; font-size: 11pt; font-weight: bold">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            查找管线及焊口&nbsp;
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
            <td colspan="3">
                &nbsp;&nbsp;施工区域&nbsp;
                <asp:DropDownList ID="drpWorkArea" runat="server" Width="33%" Height="22px">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 管线代号
                <asp:TextBox ID="txtISO_ID" runat="server" Width="30%" CssClass="textboxStyle"></asp:TextBox>&nbsp;
                <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/Images/Search.gif" OnClick="imgBtnSearch_Click"
                    ToolTip="查询" />
            </td>
        </tr>
        <tr style="height: 32px">
           <td align="left" colspan="3" style="border-bottom: 1px solid Black;">&nbsp;&nbsp;&nbsp;已点口数：
              <asp:Label runat="server" ID="lblPointNum"></asp:Label>&nbsp;比例：
              <asp:Label runat="server" ID="lblPonitRate"></asp:Label>&nbsp;&nbsp;&nbsp;固定口：
              <asp:Label runat="server" ID="lblGdk"></asp:Label>&nbsp;固定口比例：
              <asp:Label runat="server" ID="lblGdkRate"></asp:Label>&nbsp;&nbsp;&nbsp;活动口：
              <asp:Label runat="server" ID="lblHdk"></asp:Label>&nbsp;活动口比例：
              <asp:Label runat="server" ID="lblHdkRate"></asp:Label>
           </td>
        </tr>
        <tr>
            <td valign="top" style="width: 25%">
                <table cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td align="left">
                            <div id="div1" style="width: 260px; height: 480px; overflow: auto;" runat="server">
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
            <td rowspan="2" valign="top" style="width: 75%">
                <asp:GridView ID="gvPW_JointInfo" runat="server" AllowPaging="True" AllowSorting="True"
                    PageSize="15" DataSourceID="ObjectDataSource1" AutoGenerateColumns="False" HorizontalAlign="Justify"
                    OnPageIndexChanging="gvPW_JointInfo_PageIndexChanging" Width="100%" 
                    OnDataBound="gvPW_JointInfo_DataBound">
                    <AlternatingRowStyle CssClass="GridBgColr" />
                    <Columns>
                        <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="25px">
                            <HeaderTemplate>
                                <asp:CheckBox ID="ckbAll" runat="server" AutoPostBack="True" OnCheckedChanged="ckbAll_CheckedChanged"
                                    Text="全选" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="ckbJOT_ID" runat="server" />
                                <asp:Label ID="lblJOT_ID" runat="server" Text='<%# Bind("JOT_ID") %>' Visible="False"></asp:Label>
                                <asp:HiddenField ID="hdJOT_ID" runat="server" Value='<%# Bind("JOT_ID") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="8%" />
                            <ItemStyle HorizontalAlign="Center" Height="25px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="JOT_JointNo" HeaderText="焊口代号">
                            <ItemStyle Width="12%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="管线编号">
                            <ItemTemplate>
                                <asp:Label ID="lblISO_IsoNo" runat="server" Text='<%# ConvertIsoNo(Eval("ISO_ID")) %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="35%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="焊接日期">                          
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text ='<%# ConertDReaportDate(Eval("DReportID")) %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="15%" />
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="焊工号" DataField="JOT_CellWelder" >
                        <HeaderStyle Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField HeaderText="焊缝类型" DataField="JointType" >
                        <HeaderStyle Width="15%" />
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="GridBgColr" />
                    <RowStyle CssClass="GridRow" />
                    <PagerStyle HorizontalAlign="Left" />
                    <PagerTemplate>
                        <uc1:GridNavgator ID="GridNavgator1" runat="server" />
                    </PagerTemplate>
                </asp:GridView>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="getListData3"
                    TypeName="BLL.PW_JointInfoService" EnablePaging="True" OnSelecting="ObjectDataSource1_Selecting"
                    SelectCountMethod="GetListCount3">
                    <SelectParameters>
                        <asp:Parameter Name="iso_id" />
                        <asp:Parameter Name="dreportId" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
