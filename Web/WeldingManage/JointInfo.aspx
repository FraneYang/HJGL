<%@ Page Language="C#" AutoEventWireup="true"   EnableEventValidation = "false"  CodeBehind="JointInfo.aspx.cs" Inherits="Web.WeldingManage.JointInfo" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>焊口信息初始化</title>
    <link href="../Styles/Style.css" type="text/css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function ShowColumn() {
            var result = window.showModalDialog("JointInfoShowColumn.aspx?rnd=" + (new Date()).getTime(), "", "status=no;dialogWidth=450px;dialogHeight=320px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != null && result != "") {
                document.getElementById("hdColumn").value = result;
                document.getElementById("imgBtnGetColumn").click();
            }
        }

        function ShowSearch() {
            var result = window.showModalDialog("JointInfoSearch.aspx?rnd=" + (new Date()).getTime(), "", "status=no;dialogWidth=450px;dialogHeight=350px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != null && result != "") {
                document.getElementById("hdSearch").value = result;
                document.getElementById("imgBtnGetSearch").click();
            }
        }

        function ShowAddJoint(iso_id, workArea) { ///添加
            var result = window.showModalDialog("JointInfoEdit.aspx?iso_id=" + iso_id + "&workArea=" + workArea, "", "status=no;dialogWidth=1000px;dialogHeight=550px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != "") {
                document.getElementById("imgDetail").click();
            }
        }

        function ShowModifyJoint(iso_id, workArea, jot_id) { ///修改
            var result = window.showModalDialog("JointInfoEdit.aspx?iso_id=" + iso_id + "&workArea=" + workArea + "&jot_id=" + jot_id, "", "status=no;dialogWidth=1000px;dialogHeight=550px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != "") {
                document.getElementById("imgDetail").click();
            }
        }

        function JointInfoPrint(reportId, replaceParameter, varValue) { ///添加
            var result = window.showModalDialog("../ReportPrint/ExReportPrint.aspx?reportId=" + reportId + "&replaceParameter=" + replaceParameter + "&varValue=" + escape(varValue), "", "status=no;dialogWidth=1200px;dialogHeight=640px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != "") {
                document.getElementById("ImageButton1").click();
            }
        }

        function ShowBatchAddJoint(iso_id, workArea) { ///批量增加焊口信息
            var result = window.showModalDialog("JointInfoBatchEdit.aspx?iso_id=" + iso_id + "&workArea=" + workArea, "", "status=no;dialogWidth=750px;dialogHeight=360px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != "") {
                document.getElementById("imgDetail").click();
            }
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" style="height: 100%">
        <tr>
            <td colspan="3"  style="width: 100%; background: url('../Images/bg-1.gif')">
                <table id="tabbtn" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" valign="middle" style="width: 65%; font-size: 11pt; font-weight: bold">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            焊口信息初始化&nbsp;
                        </td>
                        <td align="right" valign="middle" style="width: 35%; height: 30px;">
                            <asp:ImageButton ID="imgBtnGetSearch" runat="server" Width="0" OnClick="imgBtnSearch_Click"
                                Height="1px" />
                            <input id="hdSearch" type="hidden" runat="server" />
                            <input id="hdJointNo" type="hidden" runat="server" />
                            <input id="hdISO_ID" type="hidden" runat="server" />
                            <input id="hdWLO_Code" type="hidden" runat="server" />
                            <input id="hdJointDesc" type="hidden" runat="server" />
                            <input id="hdJOTY_ID" type="hidden" runat="server" />
                            <input id="hdWME_ID" type="hidden" runat="server" />
                              <input id="hdDReportID" type="hidden" runat="server" />
                            <input id="hdPW_PointID" type="hidden" runat="server" />
                           <asp:ImageButton ID="imgDetail"
                                    runat="server" Width="0" onclick="imgDetail_Click" />
                            <input id="hdISOID" type="hidden" runat="server" />
                            &nbsp;
                            <asp:Image ID="imgSearch" runat="server" ImageUrl="~/Images/Search.gif" Style="cursor: hand; height: 20px;"
                                ToolTip="查询" onclick="ShowSearch();" />
                            
                             <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/Print1.gif" 
                                ToolTip="打印" onclick="btnPrint_Click" /><asp:ImageButton ID="ImageButton1"
                                    runat="server" Width="0" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td valign="top" style="width: 25%">
                <table id="tabletv" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td valign="middle" style="border-bottom: 1px solid Black;">
                            管线号
                            <asp:TextBox ID="txtIsoNo" runat="server" Width="150px" CssClass="textboxStyle"></asp:TextBox>
                            &nbsp;<asp:ImageButton ID="imgReportSearch" runat="server" ImageUrl="~/Images/search.png"
                                Style="vertical-align: text-bottom; cursor: hand" ToolTip="查询" OnClick="imgReportSearch_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <div id="div1" style="width: 100%; overflow: auto;" runat="server">
                                <font face="宋体">
                                    <asp:TreeView ID="tvControlItem" ForeColor="Black" runat="server" ExpandDepth="1"
                                        ShowCheckBoxes="None" Height="428px" Width="100%" ShowLines="True" NodeIndent="15"
                                        CssClass="tree" OnSelectedNodeChanged="tvControlItem_SelectedNodeChanged" OnTreeNodeExpanded="tvControlItem_TreeNodeExpanded">
                                        <SelectedNodeStyle BackColor="#99CCFF" />
                                    </asp:TreeView>
                                </font>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 1px; background-color: Silver">
            </td>
            <td valign="top" style="width: 75%">
               <table id="Table8" width="100%" cellpadding="0" cellspacing="0" runat="server"  background="../Images/bg-1.gif">
                    <tr style="border-width: 0px">
                       <%-- <td align="left">
                            &nbsp;显示记录数：
                            <asp:DropDownList ID="drpPageSize" runat="server" Height="22" Width="75px" AutoPostBack="true"
                                OnSelectedIndexChanged="drpPageSize_SelectedIndexChanged">
                                <asp:ListItem Value="12">- 请选择 -</asp:ListItem>
                                <asp:ListItem Value="2">2条</asp:ListItem>
                                <asp:ListItem Value="3">3条</asp:ListItem>
                                <asp:ListItem Value="4">4条</asp:ListItem>
                            </asp:DropDownList>
                             
                        </td>--%>
                        <td align="left">&nbsp;筛选显示列
                            <asp:Image ID="imgColumn" runat="server" ImageUrl="~/Images/go_see.gif" Style="cursor: hand"
                                ToolTip="显示列列表" onclick="ShowColumn();" />
                            <asp:ImageButton ID="imgBtnGetColumn" runat="server" Width="0" OnClick="imgBtnColumn_Click" />
                            <input id="hdColumn" type="hidden" runat="server" />
                        </td>
                        <td valign="middle" align="right"  style="border-width: 0px;">
                         <asp:ImageButton ID="btn_AddDetail" runat="server" ImageUrl="~/Images/addbutton.gif"
                                ToolTip="增加焊口信息" OnClick="btn_AddDetail_Click" />  
                            <asp:ImageButton ID="btn_BatchAddDetail" runat="server" ImageUrl="~/Images/batchaddbutton.gif"
                                ToolTip="批量增加焊口信息" onclick="btn_BatchAddDetail_Click"  />
                             <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/deletebutton.gif"
                                ToolTip="批量删除焊口信息" OnClick="btnDelete_Click" OnClientClick="return confirm(&quot;确定要删除这些焊口信息吗？&quot;);"/>
                             <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/Images/Export.gif"
                                ToolTip="导出焊口信息" OnClick="btnExport_Click" />  
                        </td>
                    </tr>
                </table>
                <table id="Table2" width="100%" cellpadding="0" cellspacing="0" runat="server">
                    <tr>
                        <td width="100%">
                            <div id="divControlItemDetailDisplay" style="overflow:auto" runat="server">
                                <table id="Table3" width="3810px" cellpadding="0" cellspacing="0" runat="server">
                                    <tr>
                                        <td width="100%">
                                            <asp:GridView ID="gvJointInfo" runat="server" AllowPaging="True" AllowSorting="True"
                                                PageSize="150" AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%"
                                                DataSourceID="ObjectDataSource1" OnDataBound="gvJointInfo_DataBound" OnRowCommand="gvJointInfo_RowCommand">
                                                <AlternatingRowStyle CssClass="GridBgColr" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="25px">
                                                       <HeaderTemplate>
                                                            <asp:CheckBox ID="ckbAll" runat="server" AutoPostBack="True" OnCheckedChanged="ckbAll_CheckedChanged"
                                                                Text="全选" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ckbJOT_ID" runat="server"/>                                                            
                                                            <asp:HiddenField ID="hdJOT_ID" runat="server" Value='<%# Bind("JOT_ID") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="50px" />
                                                        <ItemStyle HorizontalAlign="Center" Height="25px"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="焊口代号">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtJotNo" runat="server" CommandArgument='<%# Bind("JOT_ID") %>'
                                                                ToolTip="焊口代号" CommandName="click" CssClass="ItemLink" Text='<%# Bind("JOT_JointNo") %>'></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="100px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="是否焊接">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label1" runat="server" Text='<%# ConvertString(Eval("Is_hj")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="80px" />
                                                    </asp:TemplateField>
                                                   <%-- <asp:TemplateField HeaderText="点口扩透否">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label2" runat="server" Text='<%# ConvertString(Eval("If_dk")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="80px" />
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="焊口状态">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label5" runat="server" Text='<%# ConverStringJointStatus(Eval("JointStatus")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="80px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="委托情况">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label3" runat="server" Text='<%# ConvertStringTrustFlag(Eval("JOT_TrustFlag")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="120px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="探伤情况">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label4" runat="server" Text='<%# ConvertStringCheckFlag(Eval("JOT_CheckFlag")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="120px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="施工区域" DataField="WorkAreaCode">
                                                        <HeaderStyle Width="150px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JOT_WeldDate" HeaderText="焊接日期" DataFormatString="{0:d}">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JOT_DailyReportNo" HeaderText="焊接日报告号">
                                                        <HeaderStyle Width="100px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="STE_Name1" HeaderText="材质">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Component1" HeaderText="组件1号">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="WED_Name1" HeaderText="盖面焊工">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="WED_Name2" HeaderText="打底焊工">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Component2" HeaderText="组件2号">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JOT_JointDesc" HeaderText="焊口规格">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JOT_Dia" HeaderText="外径">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JOT_Size" HeaderText="尺寸">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JOT_Sch" HeaderText="壁厚">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JOT_FactSch" HeaderText="实际壁厚">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JST_Name" HeaderText="坡口类型">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JOTY_Name" HeaderText="焊缝类型">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="WME_Name" HeaderText="焊接方法">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="WeldSilk" HeaderText="焊丝代号">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="WeldMat" HeaderText="焊条代号">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="WLO_Name" HeaderText="焊接区域">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JOT_DoneDin" HeaderText="完成达因">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JOT_PrepareTemp" HeaderText="预热温度">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JOT_JointAttribute" HeaderText="焊口属性">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JOT_CellTemp" HeaderText="层间温度">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JOT_LastTemp" HeaderText="后热温度">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JOT_HeartNo1" HeaderText="炉批号1">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JOT_HeartNo2" HeaderText="炉批号2">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="点口日期" DataField="PointDate" DataFormatString="{0:d}">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                     <asp:BoundField DataField="PointNo" HeaderText="点口报告号">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="CH_TrustCode" HeaderText="委托编号">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField HeaderText="委托日期" DataField="CH_TrustDate" DataFormatString="{0:d}">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JOT_FaceCheckResult" HeaderText="外检结果">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JOT_FaceCheckDate" HeaderText="外检日期" DataFormatString="{0:d}">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JOT_FaceChecker" HeaderText="外检人员">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="IS_Proess" HeaderText="是否需热处理">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                   <%-- <asp:BoundField DataField="JOT_PHWTDate" HeaderText="PHWT日期" DataFormatString="{0:d}">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JOT_PHWTReportNo" HeaderText="PHWT报告号">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JOT_PHWTResult" HeaderText="PHET结果">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>--%>
                                                    <%--<asp:BoundField DataField="JOT_BecauseJointNo" HeaderText="源于焊口">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JOT_RepairFlag" HeaderText="返修标志">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>--%>
                                                    <asp:BoundField DataField="JOT_BelongPipe" HeaderText="所属管段">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JOT_Electricity" HeaderText="焊接电流">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JOT_Voltage" HeaderText="焊接电压">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JOT_ProessDate" HeaderText="热处理日期" DataFormatString="{0:d}">
                                                        <HeaderStyle Width="80px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="JOT_HotRpt" HeaderText="热处理报告号">
                                                        <HeaderStyle Width="100px" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="JOT_Remark" HeaderText="备注" ItemStyle-Width="150px"/>                                                  
                                                    <asp:TemplateField HeaderText="删除">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="del" ToolTip="删除"
                                                                ImageUrl="~/Images/DeleteBtn.gif" CommandArgument='<%# Bind("JOT_ID") %>' OnClientClick="return confirm(&quot;确定要删除此焊口信息吗？&quot;);" />
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
                                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetListData"
                                                TypeName="BLL.PW_JointInfoService" EnablePaging="True" SelectCountMethod="GetListCount"
                                                EnableCaching="false" OnSelecting="ObjectDataSource1_Selecting">
                                                <SelectParameters>
                                                    <asp:Parameter Name="projectId" />
                                                    <asp:Parameter Name="workAreaId" />
                                                    <asp:Parameter Name="jointNo" />
                                                    <asp:Parameter Name="iso_id" />
                                                    <asp:Parameter Name="wlo_Code" />
                                                    <asp:Parameter Name="jointDesc" />
                                                    <asp:Parameter Name="joty_id" />
                                                    <asp:Parameter Name="wme_id" />
                                                    <asp:Parameter Name="DReportID" />
                                                    <asp:Parameter Name="PW_PointID" />
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
    $("#div1").height(height - 88);
    $("#div1").width(275);
    $("#divControlItemDetailDisplay").height(height - 75);
    var width = parent.document.getElementById("center").offsetWidth;
    $("#divControlItemDetailDisplay").width(width - 295);
</script>
