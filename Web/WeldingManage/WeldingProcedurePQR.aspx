<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeldingProcedurePQR.aspx.cs"
    Inherits="Web.WeldingManage.WeldingProcedurePQR" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PQR</title>
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function AddPQR() {
            var iWidth = 950;
            var iHeight = 760;
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;
            window.open("WeldingProcedurePQREdit.aspx", "", "width=" + iWidth + ",height=" + iHeight + ",top=" + iTop + ",left=" + iLeft + ",status=no,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no");
        }

//        function ProcedureCheck(cbox) { //CheckBox单选
//            var obj = document.getElementsByTagName("input");
//            for (var i = 0; i < obj.length; i++) {
//                if (obj[i].type == "checkbox") {
//                    obj[i].checked = false;
//                }
//            }
//            var procedureId = cbox.id;
//            document.getElementById(procedureId).checked = true;
//        }       
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
                            &nbsp;PQR
                        </td>
                        <td align="right" valign="middle" style="width: 75%; height: 30px;">
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/addbutton.gif" OnClick="btnAdd_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="div1" runat="server" style="overflow: auto;">
        <table id="Table8" width="2000px" cellpadding="0" cellspacing="0" runat="server">
            <tr>
                <td width="100%">
                    <asp:GridView ID="gvPQR" runat="server" AllowPaging="True" AllowSorting="True" PageSize="12"
                        AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%" OnDataBound="gvPQR_DataBound"
                        DataSourceID="ObjectDataSource1" OnRowCommand="gvPQR_RowCommand" OnRowDataBound="gvPQR_RowDataBound">
                        <AlternatingRowStyle CssClass="GridBgColr" />
                        <Columns>
                            <asp:TemplateField HeaderText="焊接工艺评定编号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbTestingId" runat="server" CommandArgument='<%# Bind("WeldingProcedureId") %>'
                                        CssClass="ItemLink" Text='<%# Bind("WeldingProcedureId") %>' CommandName="click"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="12%" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="类型">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# ConvertType(Eval("WType")) %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="7%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="Material" HeaderText="材质">
                                <ItemStyle Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Specification" HeaderText="规格（mm）">
                                <ItemStyle Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Welding" HeaderText="焊材">
                                <ItemStyle Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="WRange" HeaderText="厚度适用范围">
                                <ItemStyle Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MaterialGroup" HeaderText="材料类组">
                                <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="JointsForm" HeaderText="试件接头形式">
                                <ItemStyle Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TubeDiameter" HeaderText="管外径Φmm">
                                <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SpecimenThickness" HeaderText="试件壁厚mm">
                                <ItemStyle Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="WeldMethod" HeaderText="焊接方法">
                                <ItemStyle Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="WeldPositionCode" HeaderText="焊接位置代号">
                                <ItemStyle Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="WeldPreheating" HeaderText="焊前预热">
                                <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PWHT" HeaderText="焊后热处理">
                                <ItemStyle Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Remark" HeaderText="备注">
                                <ItemStyle Width="9%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="删除">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton_1" runat="server" CommandArgument='<%# Bind("WeldingProcedureId") %>'
                                        CommandName="Del" ImageUrl="~/Images/DeleteBtn.gif" OnClientClick="return confirm(&quot;确定要删除此条记录吗？&quot;);" />
                                </ItemTemplate>
                                <HeaderStyle Width="3%" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="GridBgColr" />
                        <RowStyle CssClass="GridRow" />
                        <PagerTemplate>
                            <uc1:GridNavgator ID="GridNavgator1" runat="server" />
                        </PagerTemplate>
                        <PagerStyle HorizontalAlign="Left" />
                    </asp:GridView>
                    <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" TypeName="BLL.WeldingProcedureService"
                        SelectCountMethod="GetPQRListCount" SelectMethod="GetPQRListData" EnablePaging="true"
                        EnableCaching="false"></asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    var width = parent.document.getElementById("center").offsetWidth;
    if (width > 2000) {
        $("#Table8").width(width);
    }
    var height = parent.document.getElementById("center").offsetHeight;
    var table1Height = $("#Table1").height();
    var hei = height - table1Height - 5;
    $("#div1").height(hei);
</script>
