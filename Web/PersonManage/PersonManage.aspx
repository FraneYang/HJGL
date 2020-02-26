<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonManage.aspx.cs" Inherits="Web.PersonManage.PersonManage" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Style.css" type="text/css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function CheckDropDownList(source, args) {
            if (args.Value == "0")
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        function ShowPersonItem(wED_ID) {
            var iWidth = 1024;
            var iHeight = 660;
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;
            window.open("PersonItem.aspx?wED_ID=" + wED_ID, "", "width=" + iWidth + ",height=" + iHeight + ",top=" + iTop + ",left=" + iLeft + ",status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no");
        }

        function ShowPersonSave(WED_ID) {
            var iWidth = 800;
            var iHeight = 350;
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;
            window.open("PersonSave.aspx?WED_ID=" + WED_ID, "", "width=" + iWidth + ",height=" + iHeight + ",top=" + iTop + ",left=" + iLeft + ",status=no,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no");
        }

        function WelderRecordPrint(reportId, replaceParameter, varValue) {
            var result = window.showModalDialog("../ReportPrint/ExReportPrint.aspx?reportId=" + reportId + "&replaceParameter=" + replaceParameter + "&varValue=" + escape(varValue), "", "status=no;dialogWidth=1024px;dialogHeight=640px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != "") {
                document.getElementById("ImageButton1").click();
            }
        }

        function WelderScoreEdit(WED_ID) { //编辑焊工业绩信息
            var iWidth = 900;
            var iHeight = 600;
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;
            window.open("WelderScoreEdit.aspx?WED_ID=" + WED_ID, "", "width=" + iWidth + ",height=" + iHeight + ",top=" + iTop + ",left=" + iLeft + ",status=no,toolbar=no,menubar=no,location=no,scrollbars=no,resizable=no");          
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
                            &nbsp;人员管理
                        </td>
                        <td align="right" valign="middle" style="width: 55%; height: 30px;">
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/addbutton.gif" OnClick="btnAdd_Click" />
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/Search.gif" OnClick="btnSearch_Click" />
                            <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/Print1.gif" ToolTip="打印"
                                OnClick="btnPrint_Click" /><asp:ImageButton ID="ImageButton1" runat="server" Width="0" />
                            <asp:ImageButton ID="btncancel" runat="server" ImageUrl="~/Images/cancel.gif" OnClick="btncancel_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divSearch" runat="server" visible="false">
                    <table id="TableS" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                        <tr style="height: 32px">
                            <td align="right" width="10%">
                                <asp:Label ID="Label5" runat="server" Text="单位"></asp:Label>
                            </td>
                            <td align="left" width="20%">
                                <asp:DropDownList ID="drpUnitS" runat="server" Height="22px" Width="80%">
                                </asp:DropDownList>
                            </td>
                            <td align="right" width="10%">
                                <asp:Label ID="Label6" runat="server" Text="班组"></asp:Label>
                            </td>
                            <td width="20%" align="left">
                                <asp:DropDownList ID="drpEducationS" runat="server" Height="22px" Width="80%">
                                </asp:DropDownList>
                            </td>
                            <td align="right" width="10%">
                                <asp:Label ID="Label7" runat="server" Text="焊工代号"></asp:Label>
                            </td>
                            <td width="20%" align="left">
                                <asp:TextBox ID="txtCodeS" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            <td align="left" style="height: 32px" width="10%">
                                <asp:ImageButton ID="imgbtnConfirm" runat="server" ImageUrl="~/Images/confirm.gif"
                                    OnClick="imgbtnConfirm_Click" />
                            </td>
                        </tr>
                        <tr style="height: 32px">
                            <td align="right">
                                <asp:Label ID="Label8" runat="server" Text="焊工姓名"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtNameS" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            <td align="right">
                                <asp:Label ID="Label15" runat="server" Text="上岗证号"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtWorkCodeS" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            <td align="right">
                                <asp:Label ID="Label16" runat="server" Text="焊工等级"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtClassS" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
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
                    AutoGenerateColumns="False" HorizontalAlign="Justify" PageSize="14" Width="100%"
                    DataSourceID="ObjectDataSource1" OnDataBound="gvTeamGroup_DataBound" OnRowCommand="gvTeamGroup_RowCommand">
                    <AlternatingRowStyle CssClass="GridBgColr" />
                    <Columns>
                        <asp:BoundField DataField="UnitName" HeaderText="单位">
                            <HeaderStyle Width="15%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="EDU_Name" HeaderText="班组">
                            <HeaderStyle Width="6%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="WED_Code" HeaderText="焊工代号">
                            <HeaderStyle Width="6%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="焊工姓名">
                            <ItemTemplate>
                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Bind("WED_ID") %>'
                                    CommandName="click" CssClass="ItemLink" Text='<%# Bind("WED_Name") %>'></asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="6%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="WED_Sex" HeaderText="性别">
                            <HeaderStyle Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="WED_Birthday" HeaderText="出生日期">
                            <HeaderStyle Width="8%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="WED_WorkCode" HeaderText="上岗证号">
                            <HeaderStyle Width="6%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="WED_Class" HeaderText="焊工等级">
                            <HeaderStyle Width="6%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="WED_IfOnGuardName" HeaderText="是否在岗">
                            <HeaderStyle Width="6%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="焊工资质" HeaderStyle-Width="8%">
                            <ItemTemplate>
                                <asp:LinkButton ID="lkBS_Steel" runat="server" CommandName="BS_SteelClick" CommandArgument='<%# Bind("WED_ID") %>'
                                    Text="资质条件" CssClass="ItemLink">
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="8%"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="焊工业绩">
                            <ItemTemplate>
                                <asp:LinkButton ID="lk_BS_WelderScore" runat="server" CommandName="BS_WelderScore"
                                    CommandArgument='<%# Bind("WED_ID") %>' Text="焊工业绩" CssClass="ItemLink">
                                </asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle Width="7%"></HeaderStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="删除">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument='<%# Bind("WED_ID") %>'
                                    CommandName="Del" ImageUrl="~/Images/DeleteBtn.gif" OnClientClick="return confirm(&quot;确定要删除此人员信息吗？&quot;)" />
                            </ItemTemplate>
                            <HeaderStyle Width="5%" />
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
    <asp:ValidationSummary ID="ValidationSummary1" Style="z-index: 101; left: 8px; position: absolute;
        top: 8px" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="Save" />
    </form>
</body>
</html>
