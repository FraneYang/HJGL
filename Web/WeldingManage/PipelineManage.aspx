<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PipelineManage.aspx.cs" Inherits="Web.WeldingManage.PipelineManage" %>
<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Style.css" type="text/css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function ShowColumn() {
            var result = window.showModalDialog("ShowColumn.aspx?rnd=" + (new Date()).getTime(), "", "status=no;dialogWidth=450px;dialogHeight=320px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != null && result != "") {
                document.getElementById("hdColumn").value = result;
                document.getElementById("imgBtnGetColumn").click();
            }
        }

        function ShowSearch() {
            var result = window.showModalDialog("ShowSearch.aspx?rnd=" + (new Date()).getTime(), "", "status=no;dialogWidth=450px;dialogHeight=320px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != null && result != "") {
                document.getElementById("hdSearch").value = result;
                document.getElementById("imgBtnGetSearch").click();
            }
        }

        function ShowAddPipeline(unitId, workAreaId) {
            var result = window.showModalDialog("EditPipeline.aspx?unitId=" + unitId + "&workAreaId=" + workAreaId, "", "status=no;dialogWidth=960px;dialogHeight=480px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != "") {
                document.getElementById("imgDetail").click();
            }
        }

        function ShowModifyPipeline(iso_id) {
            var result = window.showModalDialog("EditPipeline.aspx?iSO_ID=" + iso_id, "", "status=no;dialogWidth=960px;dialogHeight=480px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != "") {
                document.getElementById("imgDetail").click();
            }
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
                        <td align="left" valign="middle" style="width: 65%; font-size: 11pt; font-weight: bold">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            管线管理&nbsp;
                        </td>
                        <td align="right" valign="middle" style="width: 35%; height: 30px;">
                            &nbsp;筛选显示列  <asp:Image ID="imgColumn" runat="server" ImageUrl="~/Images/go_see.gif" Style="cursor: hand"
                                ToolTip="显示列列表" onclick="ShowColumn();" />
                            <asp:ImageButton ID="imgBtnGetColumn" runat="server" Width="0" OnClick="imgBtnColumn_Click"
                                Style="height: 1px" />
                                <input id="hdColumn" type="hidden" runat="server" />
                                &nbsp;&nbsp;
                                <asp:Image ID="imgSearch" runat="server" ImageUrl="~/Images/Search.gif" Style="cursor: hand"
                                ToolTip="查询" onclick="ShowSearch();" />
                                    <asp:ImageButton ID="imgBtnGetSearch" runat="server" Width="0" OnClick="imgBtnSearch_Click"
                                Style="height: 1px" />
                                <input id="hdSearch" type="hidden" runat="server" />
                                <input id="hdISO_IsoNo" type="hidden" runat="server" />
                                <input id="hdSER_ID" type="hidden" runat="server" />
                                <input id="hdNDT_ID" type="hidden" runat="server" />
                                <input id="hdISO_IsoNumber" type="hidden" runat="server" />
                                <input id="hdSTE_ID" type="hidden" runat="server" />
                                <input id="hdISO_Specification" type="hidden" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top" style="width: 25%">
                <table cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td align="left">
                            <div id="div1" style="width: 100%; overflow: auto;" runat="server">
                                <font face="宋体">
                                    <asp:TreeView ID="tvControlItem" ForeColor="Black" runat="server" ExpandDepth="1"
                                        ShowCheckBoxes="None" Height="428px" Width="100%" ShowLines="True" OnSelectedNodeChanged="tvControlItem_SelectedNodeChanged"
                                        NodeIndent="15" CssClass="tree" 
                                    >
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
             <table id="Table8" width="100%" cellpadding="0" cellspacing="0" runat="server">
                    <tr style="border-width: 0px">
                        <td align="left" background="../Images/bg-1.gif">
                           <%-- &nbsp;显示记录数：
                            <asp:DropDownList ID="drpPageSize" runat="server" Height="22" Width="75px" AutoPostBack="true"
                                OnSelectedIndexChanged="drpPageSize_SelectedIndexChanged">
                                <asp:ListItem Value="12">- 请选择 -</asp:ListItem>
                                <asp:ListItem Value="2">2条</asp:ListItem>
                                <asp:ListItem Value="3">3条</asp:ListItem>
                                <asp:ListItem Value="4">4条</asp:ListItem>
                            </asp:DropDownList>--%>
                             
                        </td>
                        <td valign="middle" align="right" background="../Images/bg-1.gif" style="border-width: 0px;">
                            <font face="宋体" style="font-weight: bold; font-size: 14px;">
                                <asp:ImageButton ID="btn_AddDetail" runat="server" ImageUrl="~/Images/addbutton.gif"
                                    OnClick="btn_AddDetail_Click" ToolTip="增加管线信息" />
                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/deletebutton.gif"
                                ToolTip="批量删除管线信息" OnClick="btnDelete_Click" OnClientClick="return confirm(&quot;将会删除所选管线下所有焊口，确定要删除这些管线信息吗？&quot;);"/>
                                <asp:ImageButton ID="imgDetail" runat="server" Width="0" OnClick="imgDetail_Click" />
                                  <input id="hdUnitId" type="hidden" runat="server" />
                            </font>
                        </td>
                    </tr>
                </table>
                    <table id="Table2" width="100%" cellpadding="0" cellspacing="0" runat="server">
                        <tr>
                            <td width="100%" colspan="2">
                                <div id="divControlItemDetailDisplay" style="overflow:auto" runat="server">
                                    <table id="Table1" width="2500px" cellpadding="0" cellspacing="0" runat="server">
                                        <tr>
                                            <td width="100%">
                                                <asp:GridView ID="gvIsoInfo" runat="server" ShowFooter="true"  AllowPaging="True" AllowSorting="True"
                                                    PageSize="10" AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%"
                                                    OnDataBound="gvIsoInfo_DataBound" DataSourceID="ObjectDataSource1" OnRowCommand="gvIsoInfo_RowCommand">
                                                    <AlternatingRowStyle CssClass="GridBgColr" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="25px">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="ckbAll" runat="server" AutoPostBack="True" OnCheckedChanged="ckbAll_CheckedChanged"
                                                                    Text="全选" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="ckbISO_ID" runat="server" />
                                                                <asp:HiddenField ID="hdISO_ID" runat="server" Value='<%# Bind("ISO_ID") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="50px" />
                                                            <ItemStyle HorizontalAlign="Center" Height="25px"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="管线代号">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtnISO_ID" runat="server" CommandArgument='<%# Bind("ISO_ID") %>'
                                                                    ToolTip="管线代号" CommandName="click" CssClass="ItemLink" Text='<%# Bind("ISO_IsoNo") %>'></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="150px" />
                                                            <FooterTemplate>
                                                                合计:
                                                            </FooterTemplate>
                                                            <FooterStyle Width="150px" Height="25px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="ISO_TotalDin" DataFormatString="{0:N2}" HeaderText="总达因数">
                                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ISO_JointQty" HeaderText="总焊口量">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="UnitName" HeaderText="单位">
                                                            <ItemStyle Width="150px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="SERName" HeaderText="介质">
                                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NDTR_Name" HeaderText="探伤比例">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="NDTName" HeaderText="探伤类型">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="BAWName" HeaderText="施工区域">
                                                            <ItemStyle Width="100px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ISO_SysNo" HeaderText="系统号">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ISO_SubSysNo" HeaderText="分系统号">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ISO_CwpNo" HeaderText="工作包号">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ISO_IsoNumber" HeaderText="单线图号">
                                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ISO_Rev" HeaderText="图纸版次">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ISO_Sheet" HeaderText="页数">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ISO_PipeQty" HeaderText="总管段数">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ISO_Paint" HeaderText="涂漆类别">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ISO_Insulator" HeaderText="绝热类别">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="STEName" HeaderText="材质">
                                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ISO_Executive" HeaderText="执行标准">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <%--  <asp:BoundField DataField="ISO_Specification" HeaderText="规格">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>--%>
                                                        <asp:BoundField DataField="ISO_Modifier" HeaderText="修改人">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ISO_ModifyDate" HeaderText="修改日期" DataFormatString="{0:d}">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ISO_Creator" HeaderText="建档人">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ISO_CreateDate" HeaderText="建档日期" DataFormatString="{0:d}">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ISO_DesignPress" DataFormatString="{0:N2}" HeaderText="设计压力">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ISO_DesignTemperature" DataFormatString="{0:N2}" HeaderText="设计温度">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ISO_TestPress" DataFormatString="{0:N2}" HeaderText="试验压力">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ISO_TestTemperature" DataFormatString="{0:N2}" HeaderText="试验温度">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ISO_NDTClass" HeaderText="合格等级">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ISO_PTRate" HeaderText="渗透比例">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="IDName" HeaderText="管道等级">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ISO_PTClass" HeaderText="渗透等级">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ISO_IfPickling" HeaderText="是否酸洗">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ISO_IfChasing" HeaderText="是否抛光">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="PTP_TestPackageNo" HeaderText="试压包编号">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="ISO_Remark" HeaderText="备注">
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:BoundField>
                                                        <asp:TemplateField HeaderText="删除">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="del" ToolTip="删除"
                                                                    ImageUrl="~/Images/DeleteBtn.gif" CommandArgument='<%# Bind("ISO_ID") %>' OnClientClick="return confirm(&quot;确定要删除此条管线信息吗？&quot;);" />
                                                            </ItemTemplate>
                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="GridBgColr" />
                                                    <RowStyle CssClass="GridRow" />
                                                    <PagerTemplate>
                                                        <uc1:GridNavgator ID="GridNavgator1" runat="server" />
                                                    </PagerTemplate>
                                                    <PagerStyle HorizontalAlign="Left" />
                                                </asp:GridView>
                                                <asp:ObjectDataSource runat="server" ID="ObjectDataSource1" TypeName="BLL.PW_IsoInfoService"
                                                    SelectCountMethod="getListCount" SelectMethod="getListData" EnablePaging="true"
                                                    EnableCaching="false" OnSelecting="ObjectDataSource1_Selecting">
                                                    <SelectParameters>
                                                        <asp:Parameter Name="projectId" />
                                                        <asp:Parameter Name="iSO_IsoNo" />
                                                        <asp:Parameter Name="sER_ID" />
                                                        <asp:Parameter Name="nDT_ID" />
                                                        <asp:Parameter Name="iSO_IsoNumber" />
                                                        <asp:Parameter Name="sTE_ID" />
                                                        <asp:Parameter Name="iSO_Specification" />
                                                        <asp:Parameter Name="workAreaId" />
                                                    </SelectParameters>
                                                </asp:ObjectDataSource>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    var height = parent.document.getElementById("center").offsetHeight;
    $("#div1").height(height - 55);
    $("#div1").width(275);
    $("#divControlItemDetailDisplay").height(height - 75);
    var width = parent.document.getElementById("center").offsetWidth;
    $("#divControlItemDetailDisplay").width(width - 295);
</script>
