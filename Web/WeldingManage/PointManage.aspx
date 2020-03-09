<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PointManage.aspx.cs"
    Inherits="Web.WeldingManage.PointManage" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>点口管理</title>
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ValidateGroupControl.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function ShowSearch(unitId, installationId) {
            var result = window.showModalDialog("PointShowSearch.aspx?rnd=" + (new Date()).getTime() + "&unitId=" + unitId + "&InstallationId=" + installationId, "", "status=no;dialogWidth=960px;dialogHeight=600px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != null && result != "") {
                document.getElementById("hdSelectList").value = result;
                document.getElementById("imgBtnGetSearch").click();
            }
        }

        function PointSearch() {
            document.getElementById("imgReportSearch").click();
        }

//        function GetPerson1(JOT_CellWelderClientID, hdJOT_CellWelderClientID) {
//            document.getElementById("imgBtnSave").click();
//            var result = window.showModalDialog("ShowPerson.aspx?rnd=" + (new Date()).getTime(), "", "status=no;dialogWidth=750px;dialogHeight=478px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
//            if (result != null && result != "") {
//                var val = result.split(",");
//                document.getElementById(hdJOT_CellWelderClientID).value = val[0];
//                document.getElementById(JOT_CellWelderClientID).value = val[1];
//                document.getElementById("imgBtnAll3").click();
//            }
//        }

        function CheckDropDownList(source, args) {
            if (args.Value == "0")
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        function ShowTrustManageSet(pointID) {
            var iWidth = 600;
            var iHeight = 260;
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;
            window.open("../TrustManage/TrustManageSet.aspx?pointID=" + pointID, "", "width=" + iWidth + ",height=" + iHeight + ",top=" + iTop + ",left=" + iLeft + ",status=no,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no");
        }

        function PointInfoPrint(reportId, replaceParameter, varValue) {
            var result = window.showModalDialog("../ReportPrint/ExReportPrint.aspx?reportId=" + reportId + "&replaceParameter=" + replaceParameter + "&varValue=" + escape(varValue), "", "status=no;dialogWidth=900px;dialogHeight=600px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
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
                            &nbsp;点口管理
                        </td>
                        <td align="right" valign="middle" style="width: 75%; height: 30px;">
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/addbutton.gif" OnClick="btnAdd_Click" />
                            <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/Print1.gif" ToolTip="打印"
                                OnClick="btnPrint_Click" /><asp:ImageButton ID="ImageButton1" runat="server" Width="0" />
                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/deletebutton.gif"
                                OnClick="btnDelete_Click" OnClientClick="return confirm(&quot;确定要删除此条信息吗？&quot;);" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table2" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                    <tr>
                        <td style="width: 25%"  valign="top">
                            <table cellspacing="0" cellpadding="0" width="100%">
                                <tr>
                                   <td  style="border-bottom: 1px solid Black;">
                                    <asp:DropDownList ID="drpSearch" runat="server" Width="25%" Height="22px" CssClass="textboxStyle"
                                            AutoPostBack="true" onselectedindexchanged="drpSearch_SelectedIndexChanged">
                                       <asp:ListItem>按月份</asp:ListItem>
                                       <asp:ListItem>按单号</asp:ListItem>
                                    </asp:DropDownList>
                                    
                                    <input id="txtPointManageDate" runat="server" class="Wdate" 
                                       style="width: 35%; height:20px; cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM',skin:'whyGreen'})" />
                                     
                                     <asp:TextBox ID="txtSearchCode" runat="server" style="width: 38%; height:20px;" CssClass="textboxStyle"></asp:TextBox>
                                        &nbsp;<asp:ImageButton ID="imgReportSearch" runat="server" ImageUrl="~/Images/search.png"
                                            Style="cursor: hand" ToolTip="查询" OnClick="imgReportSearch_Click"  ImageAlign="AbsMiddle"  />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <div id="div1" style="width: 100%; overflow: auto;" runat="server">
                                            <font face="宋体">
                                                <asp:TreeView ID="tvControlItem" ForeColor="Black" runat="server" ExpandDepth="1"
                                                    ShowCheckBoxes="None" Height="428px" Width="100%" ShowLines="True" OnSelectedNodeChanged="tvControlItem_SelectedNodeChanged"
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
                            <table id="Table3" runat="server" width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="3" style="width: 100%; background: url('../Images/bg-1.gif')">
                                        <table id="Table4" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                                            cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="left" valign="middle" style="width: 20%; font-size: 11pt; font-weight: bold">
                                                    <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image1" runat="server" />
                                                    &nbsp;点口管理
                                                </td>
                                                <td align="right" style="width: 80%; height: 30px;">
                                                    <input id="hdSelectList" type="hidden" runat="server" />
                                                    <%--<input id="hdNewTemplates" type="hidden" runat="server" />--%>
                                                    <input id="hdAll1" type="hidden" runat="server" />
                                                    <%--<input id="hdAll2" type="hidden" runat="server" />--%>
                                                    <asp:ImageButton ID="imgBtnGetSearch" runat="server" Width="0" OnClick="imgBtnSearch_Click"
                                                        Style="height: 1px" />
                                                    <%--<asp:ImageButton ID="imgBtnAll1" runat="server" Width="0" OnClick="imgBtnAll1_Click"
                                                        Style="height: 1px" />--%>
                                                    <%-- <asp:ImageButton ID="imgBtnAll2_C" runat="server" Width="0" OnClick="imgBtnAll2_C_Click"
                                                        Style="height: 1px" />
                                                    <asp:ImageButton ID="imgBtnAll2_F" runat="server" Width="0" OnClick="imgBtnAll2_F_Click"
                                                        Style="height: 1px" />--%>
                                                    <asp:ImageButton ID="imgBtnSave" runat="server" Width="0" OnClick="imgBtnSave_Click"
                                                        Style="height: 1px" />
                                                     <asp:ImageButton ID="imgBtnAll3" runat="server" Width="0" OnClick="imgBtnAll3_Click"
                                                        Style="height: 1px" />
                                                    <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/Images/Find.gif" Style="cursor: hand"
                                                        ToolTip="查询" OnClick="imgSearch_Click" />
                                                    <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" ValidationGroup="Save"
                                                        OnClick="btnSave_Click" Style="height: 20px" />
                                                    <asp:ImageButton ID="btnGenerate" runat="server" ImageUrl="~/Images/Generate.gif" 
                                                        OnClick="btnGenerate_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" id="AddItem" runat="server">
                                        <table id="Table5" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                                            <tr>
                                                <td align="right" height="35px" class="style1" width="10%">
                                                    <asp:Label ID="Label1" runat="server" Text="点口编号" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="35px">
                                                    <asp:TextBox ID="txtPointID" runat="server" Width="80%" CssClass="textboxStyle" ReadOnly="true"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPointID"
                                                        Display="Dynamic" ErrorMessage="请输入点口编号！" ForeColor="Red" SetFocusOnError="True"
                                                        ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td width="10%" align="right" height="35px">
                                                    <asp:Label ID="Label3" runat="server" Text="单位" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="35px">
                                                    <asp:DropDownList ID="drpUnit" runat="server" Width="80%" Height="22px" 
                                                        CssClass="textboxStyle" onselectedindexchanged="drpUnit_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CustomValidator2" runat="server" Display="Dynamic" ErrorMessage="&quot;请选择单位！&quot;"
                                                        ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpUnit" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                                                </td>
                                                <td width="10%" align="right" height="35px">
                                                    <asp:Label ID="Label7" runat="server" Text="装置" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="35px">
                                                    <asp:DropDownList ID="ddlInstallationId" runat="server" CssClass="textboxStyle" Width="80%">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CustomValidator3" runat="server" ClientValidationFunction="CheckDropDownList"
                                                        ControlToValidate="ddlInstallationId" Display="Dynamic" ErrorMessage="&quot;请选择装置！&quot;"
                                                        ForeColor="Red" ValidationGroup="Save">*</asp:CustomValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" height="35px" class="style1">
                                                    <asp:Label ID="Label4" runat="server" Text="点口日期"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="35px">
                                                   <asp:TextBox ID="txtPointDate" Style="width: 80%; cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})"
                               runat="server" CssClass="textboxStyle" AutoPostBack="true" ontextchanged="txtPointDate_TextChanged"></asp:TextBox>
                                                </td>
                                                <td width="10%" align="right" height="35px">
                                                    <asp:Label ID="Label2" runat="server" Text="制单人" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="35px">
                                                    <asp:DropDownList ID="drpTabler" runat="server" Width="80%" Height="22px" CssClass="textboxStyle">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CustomValidator1" runat="server" Display="Dynamic" ErrorMessage="请选择制单人！"
                                                        ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpTabler" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                                                </td>
                                                <td width="10%" align="right" height="35px">
                                                    <asp:Label ID="Label5" runat="server" Text="制单日期"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="35px">
                                                  <asp:TextBox ID="txtTableDate" Style="width: 80%; cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})"
                                                        runat="server" CssClass="textboxStyle"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" height="35px" class="style1">
                                                    <asp:Label ID="Label6" runat="server" Text="备注"></asp:Label>
                                                </td>
                                                <td align="left" height="35px" colspan="5">
                                                    &nbsp;<asp:TextBox ID="txtRemark" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table id="Table6" width="100%" cellpadding="0" cellspacing="0" runat="server">                                
                                <tr>
                                    <td width="100%">
                                        <div id="div2" style="overflow-x:hidden; overflow: auto;" runat="server">
                                            <asp:GridView ID="gvPoint" runat="server" AllowSorting="True" PageSize="100" AutoGenerateColumns="False"
                                                HorizontalAlign="Justify" Width="100%" AlternatingRowStyle-CssClass="GridBgColr"
                                                OnRowCommand="gvPoint_RowCommand" OnRowDataBound="gvPoint_RowDataBound">
                                                <AlternatingRowStyle CssClass="GridBgColr" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="序号">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="施工区域">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtBAW_ID" CssClass="textboxnoneborder" runat="server" Text='<%# GetBAW_ID(Eval("ISO_ID")) %>'></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="管线编号">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtISO_ID" runat="server" Text='<%# GetISO_IsoNo(Eval("ISO_ID")) %>'></asp:Label>
                                                            <asp:HiddenField ID="hdISO_ID" runat="server" Value='<%# Bind("ISO_ID") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="30%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="焊口代号">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtJOT_JointNo" runat="server" CssClass="textboxnoneborder" Text='<%# Bind("JOT_JointNo") %>'
                                                                Width="90%"></asp:TextBox>
                                                            <asp:HiddenField runat="server" ID="hdJOT_ID" Value='<%# Bind("JOT_ID") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>                                                   
                                                    <asp:TemplateField HeaderText="焊工">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtJOT_CellWelder" runat="server" Text='<%# GetPersonNameByJOT_CellWelder(Eval("JOT_CellWelder")) %>'
                                                                CssClass="textboxnoneborder" Width="98%" ReadOnly="true"></asp:TextBox>
                                                            <asp:HiddenField ID="hdJOT_CellWelder" runat="server" Value='<%# Bind("JOT_CellWelder") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle Width="10%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="删除">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="del" ToolTip="删除"
                                                                ImageUrl="~/Images/DeleteBtn.gif" CommandArgument='<%# Bind("JOT_ID") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="GridBgColr" />
                                                <RowStyle CssClass="GridRow" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <asp:ValidationSummary ID="ValidationSummary1" Style="z-index: 101; left: 8px; position: absolute;
                                top: 8px" runat="server" HeaderText="请注意！" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="Save" />
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
    $("#div2").height(height - 230);
</script>
