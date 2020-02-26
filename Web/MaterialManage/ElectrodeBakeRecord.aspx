<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ElectrodeBakeRecord.aspx.cs"
    Inherits="Web.MaterialManage.ElectrodeBakeRecord" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>焊条烘烤记录</title>
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">    
     function JointInfoPrint(reportId, replaceParameter, varValue) { 
            var result = window.showModalDialog("../ReportPrint/ExReportPrint.aspx?reportId=" + reportId + "&replaceParameter=" + replaceParameter + "&varValue=" + escape(varValue), "", "status=no;dialogWidth=1200px;dialogHeight=600px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != "") {
                document.getElementById("ImageButton1").click();
            }
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
                        <td align="left" valign="middle" style="width: 25%; font-size: 11pt; font-weight: bold">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;焊条烘烤记录
                        </td>
                        <td align="right" valign="middle" style="width: 75%; height: 30px;">
                            <asp:ImageButton ID="btnAddButton" runat="server" ImageUrl="~/Images/addbutton.gif"
                                OnClick="btnAddButton_Click" />
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/Search.gif" OnClick="btnPrint_Click" /><asp:ImageButton ID="ImageButton1" runat="server" Width="0" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divPrint" runat="server" visible="false">
                    <table id="TableS" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                        <tr style="height: 32px">
                            <td align="right" width="20%">
                                <asp:Label ID="Label5" runat="server" Text="日期："></asp:Label>
                            </td>
                            <td align="left" width="50%">
                                <input id="txtStartDate" runat="server" readonly="readonly" class="Wdate" style="width: 40%;
                                    cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
                                <asp:Label ID="Label6" runat="server" Text="至"></asp:Label>
                                &nbsp;<input id="txtEndDate" runat="server" readonly="readonly" class="Wdate" style="width: 40%;
                                    cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
                            </td>
                            <td align="left" style="height: 32px" width="30%">
                                <asp:ImageButton ID="imgbtnConfirm" runat="server" ImageUrl="~/Images/confirm.gif"
                                    OnClick="imgbtnConfirm_Click" />
                                <asp:ImageButton ID="imgbtnCancal" runat="server" ImageUrl="~/Images/cancel.gif"
                                    OnClick="imgbtnCancal_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <div id="div1" runat="server" style="overflow: auto;">
        <table id="Table8" width="100%" cellpadding="0" cellspacing="0" runat="server">
            <tr>
                <td style="width: 100%">
                    <asp:GridView ID="gvElectrodeBake" runat="server" AllowPaging="True" AllowSorting="True"
                        PageSize="12" DataSourceID="ObjectDataSource1" AutoGenerateColumns="False" HorizontalAlign="Justify"
                        Width="100%" OnDataBound="gvElectrodeBake_DataBound" OnRowCommand="gvElectrodeBake_RowCommand">
                        <AlternatingRowStyle CssClass="GridBgColr" />
                        <Columns>
                            <asp:TemplateField HeaderText="编号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnElectrodeCode" runat="server" CommandArgument='<%# Bind("ElectrodeID") %>'
                                        CommandName="click" CssClass="ItemLink" Text='<%# Bind("ElectrodeCode") %>' ToolTip="修改"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="20%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ProjectName" HeaderText="工程名称">
                                <ItemStyle Width="30%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ElectrodeDate" DataFormatString="{0:d}" HeaderText="烘烤日期">
                                <ItemStyle Width="20%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CompileDate" DataFormatString="{0:d}" HeaderText="编制日期">
                                <ItemStyle Width="20%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="删除">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="del" ToolTip="删除"
                                        ImageUrl="~/Images/DeleteBtn.gif" CommandArgument='<%# Bind("ElectrodeID") %>'
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
                    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" TypeName="BLL.ElectrodeBakeService"
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
