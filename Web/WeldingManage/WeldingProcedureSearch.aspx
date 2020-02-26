<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeldingProcedureSearch.aspx.cs"
    Inherits="Web.WeldingManage.WeldingProcedureSearch" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function ShowProduce(weldingProduceId) {
            var result = window.showModalDialog("ShowProduceReport.aspx?weldingProduceId=" + weldingProduceId + "&rnd=" + (new Date()).getTime(), "", "status=no;dialogWidth=1024px;dialogHeight=700px;menu=no;resizeable=no;scroll=yes;center=yes;edge=raise;location=no");
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
                            &nbsp;焊接工艺评定
                        </td>
                        <td align="right" valign="middle" style="width: 75%; height: 30px;">
                            <asp:ImageButton ID="imgbtnShowSearch" runat="server" ImageUrl="~/Images/Search.gif"
                                OnClick="imgbtnShowSearch_Click" />
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/addbutton.gif" OnClick="btnAdd_Click" />
                            <asp:ImageButton ID="btnModify" runat="server" ImageUrl="~/Images/modybutton.gif"
                                Style="height: 20px" OnClick="btnModify_Click" />
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" ValidationGroup="Save"
                                OnClick="btnSave_Click" Style="height: 20px" />
                            <asp:ImageButton ID="btncancel" runat="server" ImageUrl="~/Images/cancel.gif" OnClick="btncancel_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divEdit" runat="server">
                    <table id="Table2" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                        <tr style="height: 32px">
                            <td align="right" style="width: 8%">
                                <asp:Label ID="Label3" runat="server" Text="编号"></asp:Label>&nbsp;
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtWeldingProcedureId" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 8%">
                                <asp:Label ID="Label4" runat="server" Text="类型"></asp:Label>&nbsp;
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtWtype" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 8%">
                                <asp:Label ID="Label5" runat="server" Text="材质"></asp:Label>&nbsp;
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:DropDownList ID="drpSte_Name" runat="server" Height="22px" Width="80%" CssClass="textboxStyle"
                                    AutoPostBack="True" OnSelectedIndexChanged="drpSte_Name_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="height: 32px">
                            <td align="right" style="width: 8%">
                                <asp:Label ID="Label1" runat="server" Text="规格（mm）"></asp:Label>&nbsp;
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtSpecification" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 8%">
                                <asp:Label ID="Label7" runat="server" Text="焊材"></asp:Label>&nbsp;
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtWelding" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 10%">
                                <asp:Label ID="Label8" runat="server" Text="厚度适用范围"></asp:Label>&nbsp;
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtWRange" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 32px">
                            <td align="right" style="width: 8%">
                                <asp:Label ID="Label15" runat="server" Text="材料类别"></asp:Label>
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtMaterialType" runat="server" Width="80%" 
                                    CssClass="textboxStyle" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 8%">
                                <asp:Label ID="Label29" runat="server" Text="材料组别"></asp:Label>
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtMaterialGroup" runat="server" Width="80%" 
                                    CssClass="textboxStyle" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 10%">
                                <asp:Label ID="Label16" runat="server" Text="管外径Φmm"></asp:Label>
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtTubeDiameter" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 32px">
                            <td align="right" style="width: 8%">
                                <asp:Label ID="lablel" runat="server" Text="试件接头形式"></asp:Label>
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtJointsForm" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 8%">
                                <asp:Label ID="Label17" runat="server" Text="试件壁厚(mm)"></asp:Label>
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtSpecimenThickness" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 10%">
                                <asp:Label ID="Label18" runat="server" Text="焊接方法"></asp:Label>
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtWeldMethod" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 32px">
                            <td align="right" style="width: 8%">
                                <asp:Label ID="Label19" runat="server" Text="焊接位置代号"></asp:Label>
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtWeldPositionCode" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 8%">
                                <asp:Label ID="Label20" runat="server" Text="焊前预热℃"></asp:Label>
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtWeldPreheating" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 10%">
                                <asp:Label ID="Label21" runat="server" Text="焊后热处理"></asp:Label>
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtPWHT" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 32px">
                            <td align="right" style="width: 8%">
                                <asp:Label ID="Label6" runat="server" Text="备注"></asp:Label>
                                &nbsp;
                            </td>
                            <td align="left" colspan="5">
                                <asp:TextBox ID="txtRemark" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divSearch" runat="server">
                    <table id="Table3" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                        <tr style="height: 32px">
                            <td align="right" style="width: 8%">
                                <asp:Label ID="Label9" runat="server" Text="编号"></asp:Label>
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtWeldingProcedureIdS" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 8%">
                                <asp:Label ID="Label10" runat="server" Text="类型"></asp:Label>&nbsp;
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtSType" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 8%">
                                <asp:Label ID="Label11" runat="server" Text="材质"></asp:Label>&nbsp;
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:DropDownList ID="drpSte_NameS" runat="server" Height="22px" Width="80%" CssClass="textboxStyle">
                                </asp:DropDownList>
                            </td>
                            <td align="center" style="width: 10%">
                                <asp:ImageButton ID="btnConfirm" runat="server" ImageUrl="~/Images/Confirm.gif" OnClick="imgbtnConfirm_Click"
                                    Style="height: 20px" />
                            </td>
                        </tr>
                        <tr style="height: 32px">
                            <td align="right" style="width: 8%">
                                <asp:Label ID="Label12" runat="server" Text="规格（mm）"></asp:Label>
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtSpecificationS" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 8%">
                                <asp:Label ID="Label13" runat="server" Text="焊材"></asp:Label>&nbsp;
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtWeldingS" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 10%">
                                <asp:Label ID="Label14" runat="server" Text="厚度适用范围"></asp:Label>&nbsp;
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtWRangeS" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 10%">
                                <asp:ImageButton ID="imgbtncancel" runat="server" ImageUrl="~/Images/cancel.gif"
                                    OnClick="imgbtncancel_Click" />
                            </td>
                        </tr>
                        <tr style="height: 32px">
                            <td align="right" style="width: 8%">
                                <asp:Label ID="Label23" runat="server" Text="管外径Φmm"></asp:Label>
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtTubeDiameterS" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 8%">
                                <asp:Label ID="lablel0" runat="server" Text="试件接头形式"></asp:Label>
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtJointsFormS" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 10%">
                                <asp:Label ID="Label24" runat="server" Text="试件壁厚(mm)"></asp:Label>
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtSpecimenThicknessS" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 10%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr style="height: 32px">
                            <td align="right" style="width: 8%">
                                <asp:Label ID="Label25" runat="server" Text="焊接方法"></asp:Label>
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtWeldMethodS" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 8%">
                                <asp:Label ID="Label26" runat="server" Text="焊接位置代号"></asp:Label>
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtWeldPositionCodeS" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 10%">
                                <asp:Label ID="Label27" runat="server" Text="焊前预热℃"></asp:Label>
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtWeldPreheatingS" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 10%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr style="height: 32px">
                            <td align="right" style="width: 8%">
                                <asp:Label ID="Label28" runat="server" Text="焊后热处理"></asp:Label>
                            </td>
                            <td align="left" style="width: 22%">
                                <asp:TextBox ID="txtPWHTS" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            <td align="right" style="width: 8%">
                                &nbsp;</td>
                            <td align="left" style="width: 22%">
                                &nbsp;</td>
                            <td align="right" style="width: 10%">
                                &nbsp;</td>
                            <td align="left" style="width: 22%">
                                &nbsp;
                                <asp:TextBox ID="txtProcedureId" runat="server" Visible="False" Width="50%"></asp:TextBox>
                            </td>
                            <td align="center" style="width: 10%">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <div id="div1" runat="server" style="overflow: auto;">
        <table id="Table8" width="2000px" cellpadding="0" cellspacing="0" runat="server">
            <tr>
                <td width="100%">
                    <asp:GridView ID="gvPerson" runat="server" AllowPaging="True" AllowSorting="True"
                        PageSize="12" AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%"
                        OnDataBound="gvPerson_DataBound" DataSourceID="ObjectDataSource1" OnRowCommand="gvPerson_RowCommand">
                        <AlternatingRowStyle CssClass="GridBgColr" />
                        <Columns>
                            <asp:TemplateField HeaderText="焊接工艺评定编号">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbTestingId" runat="server" CommandArgument='<%# Bind("WeldingProcedureId") %>'
                                        CssClass="ItemLink" Text='<%# Bind("WeldingProcedureCode") %>' CommandName="click"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="12%" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="WType" HeaderText="类型">
                                <ItemStyle Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="STE_Name" HeaderText="材质">
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
                            <asp:BoundField DataField="MaterialType" HeaderText="材料类别">
                                <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="MaterialGroups" HeaderText="材料组别">
                                <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="JointsForm" HeaderText="试件接头形式">
                                <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TubeDiameter" HeaderText="管外径Φmm">
                                <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SpecimenThickness" HeaderText="试件壁厚mm">
                                <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="WeldMethod" HeaderText="焊接方法">
                                <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="WeldPositionCode" HeaderText="焊接位置代号">
                                <ItemStyle Width="6%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="WeldPreheating" HeaderText="焊前预热">
                                <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PWHT" HeaderText="焊后热处理">
                                <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Remark" HeaderText="备注">
                                <ItemStyle Width="5%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="操作">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnsee" runat="server" CommandArgument='<%# Bind("WeldingProcedureId") %>'
                                        CommandName="produce" ToolTip="预焊接工艺规程" ImageUrl="~/Images/go_see.gif" />
                                </ItemTemplate>
                                <HeaderStyle Width="3%" />
                            </asp:TemplateField>                          
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
                        SelectCountMethod="getListCount" SelectMethod="getListData" EnablePaging="true"
                        EnableCaching="false" OnSelecting="ObjectDataSource1_Selecting">
                        <SelectParameters>
                            <asp:Parameter Name="weldingProcedureId" />
                            <asp:Parameter Name="wType" />
                            <asp:Parameter Name="ste_Id" />
                            <asp:Parameter Name="specification" />
                            <asp:Parameter Name="welding" />
                            <asp:Parameter Name="wRange" />
                            <asp:Parameter Name="jointsForm" />
                            <asp:Parameter Name="tubeDiameter" />
                            <asp:Parameter Name="specimenThickness" />
                            <asp:Parameter Name="weldMethod" />
                            <asp:Parameter Name="weldPositionCode" />
                            <asp:Parameter Name="weldPreheating" />
                            <asp:Parameter Name="pWHT" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
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
