<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeldReport.aspx.cs" Inherits="Web.WeldingManage.WeldReport" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ValidateGroupControl.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function ShowSearch(unitId, installId) {
            var result = window.showModalDialog("ShowReportSearch.aspx?rnd=" + (new Date()).getTime() + "&unitId=" + unitId + "&installId=" + installId, "", "status=no;dialogWidth=900px;dialogHeight=600px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != null && result != "") {
                document.getElementById("hdSelectList").value = result;
                document.getElementById("imgBtnGetSearch").click();
            }
        }

        function ReportSearch() {
            document.getElementById("imgReportSearch").click();
        }

        function GetSize() {
            document.getElementById("imgSize").click();
        }

        function GetPersonAll1(unitId) {
            var result = window.showModalDialog("ShowPerson.aspx?unitId=" + unitId + "&rnd=" + (new Date()).getTime(), "", "status=no;dialogWidth=750px;dialogHeight=478px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != null && result != "") {
                var val = result.split(",");
                document.getElementById("hdAll1").value = val[0];
                document.getElementById("imgBtnAll1").click();
            }
        }

        function GetPersonAll2_C(unitId) {
            var result = window.showModalDialog("ShowPerson.aspx?unitId=" + unitId + "&rnd=" + (new Date()).getTime(), "", "status=no;dialogWidth=750px;dialogHeight=478px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != null && result != "") {
                var val = result.split(",");
                document.getElementById("hdAll1").value = val[0];
                document.getElementById("imgBtnAll2_C").click();
            }
        }

        function GetPersonAll2_F(unitId) {
            var result = window.showModalDialog("ShowPerson.aspx?unitId=" + unitId + "&rnd=" + (new Date()).getTime(), "", "status=no;dialogWidth=750px;dialogHeight=478px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != null && result != "") {
                var val = result.split(",");
                document.getElementById("hdAll1").value = val[0];
                document.getElementById("imgBtnAll2_F").click();
            }
        }

        function GetPerson3(unitId, JOT_CellWelderClientID, hdJOT_CellWelderClientID, JOT_FloorWelderClientID, hdJOT_FloorWelderClientID) {
            var result = window.showModalDialog("ShowPerson.aspx?unitId=" + unitId + "&rnd=" + (new Date()).getTime(), "", "status=no;dialogWidth=750px;dialogHeight=478px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != null && result != "") {
                var val = result.split(",");
                document.getElementById(hdJOT_CellWelderClientID).value = val[0];
                document.getElementById(JOT_CellWelderClientID).value = val[1];
                document.getElementById(hdJOT_FloorWelderClientID).value = val[0];
                document.getElementById(JOT_FloorWelderClientID).value = val[1];
                document.getElementById("imgBtnAll3").click();
            }
        }

        function GetPerson1(unitId, JOT_CellWelderClientID, hdJOT_CellWelderClientID) {
            var result = window.showModalDialog("ShowPerson.aspx?unitId=" + unitId + "&rnd=" + (new Date()).getTime(), "", "status=no;dialogWidth=750px;dialogHeight=478px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != null && result != "") {
                var val = result.split(",");
                document.getElementById(hdJOT_CellWelderClientID).value = val[0];
                document.getElementById(JOT_CellWelderClientID).value = val[1];
                document.getElementById("imgBtnAll3").click();
            }
        }

        function GetPerson2(unitId, JOT_FloorWelderClientID, hdJOT_FloorWelderClientID) {
            var result = window.showModalDialog("ShowPerson.aspx?unitId=" + unitId + "&rnd=" + (new Date()).getTime(), "", "status=no;dialogWidth=750px;dialogHeight=478px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != null && result != "") {
                var val = result.split(",");
                document.getElementById(hdJOT_FloorWelderClientID).value = val[0];
                document.getElementById(JOT_FloorWelderClientID).value = val[1];
                document.getElementById("imgBtnAll3").click();
            }
        }

        function CheckDropDownList(source, args) {
            if (args.Value == "0")
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        function keypress() {
            var keyASCII = event.keyCode;
            if ((keyASCII >= 48 && keyASCII <= 57)) {

            }
            else {
                event.keyCode = 0;
            }

        }

        function JointInfoPrint(reportId, replaceParameter, varValue) {
            var result = window.showModalDialog("../ReportPrint/ExReportPrint.aspx?reportId=" + reportId + "&replaceParameter=" + replaceParameter + "&varValue=" + escape(varValue), "", "status=no;dialogWidth=900px;dialogHeight=600px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != "") {
                document.getElementById("ImageButton1").click();
            }
        }

        function ShowExport(purchasePlanId) {
            var iWidth = 1024;
            var iHeight = 630;
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;
            window.open("ShowReportExport.aspx","", "width=" + iWidth + ",height=" + iHeight + ",top=" + iTop + ",left=" + iLeft + ",status=no,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=yes");
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
                            &nbsp;焊接日常管理
                        </td>
                        <td align="right" valign="middle" style="width: 75%; height: 30px;">
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/addbutton.gif" OnClick="btnAdd_Click" />
                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/deletebutton.gif"
                                OnClick="btnDelete_Click" OnClientClick="return confirm(&quot;确定要删除此条信息吗？&quot;);" />
                            <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/Print1.gif" ToolTip="打印"
                                OnClick="btnPrint_Click" /><asp:ImageButton ID="ImageButton1" runat="server" Width="0" />
                            <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/Images/Export.gif" ToolTip="导出"
                                OnClick="btnExport_Click" />
                                <asp:ImageButton ID="imgbtnIn" runat="server" ImageUrl="~/Images/Import.gif" OnClick="imgbtnIn_Click" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table2" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                    <tr>
                        <td style="width: 25%">
                            <table cellspacing="0" cellpadding="0" width="100%">
                                <tr>
                                    <td valign="middle" style="border-bottom: 1px solid Black;">
                                        月份
                                        <input id="txtReportDate" runat="server" class="Wdate" style="width: 90px; height: 20px;
                                            cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM',skin:'whyGreen'})" />
                                        &nbsp;<asp:ImageButton ID="imgReportSearch" runat="server" ImageUrl="~/Images/search.png"
                                            Style="vertical-align: text-bottom; cursor: hand" ToolTip="查询" OnClick="imgReportSearch_Click" />
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
                                                    &nbsp;焊接日报
                                                </td>
                                                <td align="right" style="width: 80%; height: 30px;">
                                                    <input id="hdSelectList" type="hidden" runat="server" />
                                                    <input id="hdNewTemplates" type="hidden" runat="server" />
                                                    <input id="hdAll1" type="hidden" runat="server" />
                                                    <input id="hdAll2" type="hidden" runat="server" />
                                                    <asp:ImageButton ID="imgBtnGetSearch" runat="server" Width="0" OnClick="imgBtnSearch_Click"
                                                        Style="height: 1px" />
                                                    <asp:ImageButton ID="imgBtnAll1" runat="server" Width="0" OnClick="imgBtnAll1_Click"
                                                        Style="height: 1px" />
                                                    <asp:ImageButton ID="imgBtnAll2_C" runat="server" Width="0" OnClick="imgBtnAll2_C_Click"
                                                        Style="height: 1px" />
                                                    <asp:ImageButton ID="imgBtnAll2_F" runat="server" Width="0" OnClick="imgBtnAll2_F_Click"
                                                        Style="height: 1px" />
                                                    <asp:ImageButton ID="imgBtnAll3" runat="server" Width="0" OnClick="imgBtnAll3_Click"
                                                        Style="height: 1px" />
                                                    <asp:ImageButton ID="imgSize" runat="server" Width="0" OnClick="imgSize_Click" Style="height: 1px" />
                                                    <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/Images/Find.gif" Style="cursor: hand"
                                                        ToolTip="查找" OnClick="imgSearch_Click" />
                                                    <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" ValidationGroup="Save"
                                                        OnClick="btnSave_Click" Style="height: 20px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" id="AddItem" runat="server">
                                        <table id="Table5" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                                            <tr>
                                                <td width="10%" align="right" height="35px">
                                                    <asp:Label ID="Label1" runat="server" Text="日报告号"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="35px">
                                                    &nbsp;
                                                    <asp:TextBox ID="txtDReportID" runat="server" Width="80%" CssClass="textboxStyle" ReadOnly="true"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDReportID"
                                                        Display="Dynamic" ErrorMessage="请输入日报告号" ForeColor="Red" SetFocusOnError="True"
                                                        ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td width="10%" align="right" height="35px">
                                                    <asp:Label ID="Label3" runat="server" Text="单位"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="35px">
                                                    &nbsp;
                                                    <asp:DropDownList ID="drpUnit" runat="server" Width="80%" Height="22px" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CustomValidator2" runat="server" Display="Dynamic" ErrorMessage="请选择单位！"
                                                        ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpUnit" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                                                </td>
                                                <td width="10%" align="right" height="35px">
                                                    <asp:Label ID="Label6" runat="server" Text="装置"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="35px">
                                                    &nbsp;
                                                    <asp:DropDownList ID="drpInstall" runat="server" Width="80%" Height="22px">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CustomValidator3" runat="server" Display="Dynamic" ErrorMessage="请选择装置！"
                                                        ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpInstall" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10%" align="right" height="35px">
                                                    <asp:Label ID="Label2" runat="server" Text="制单人"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="35px">
                                                    &nbsp;
                                                    <asp:DropDownList ID="drpCHT_Tabler" runat="server" Width="80%" Height="22px">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CustomValidator1" runat="server" Display="Dynamic" ErrorMessage="请选择制单人！"
                                                        ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpCHT_Tabler" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                                                </td>
                                                <td width="10%" align="right" height="35px">
                                                    <asp:Label ID="Label5" runat="server" Text="制单日期"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="35px">
                                                    &nbsp;
                                                    <asp:TextBox ID="txtCHT_TableDate" Style="width: 80%; cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})"
                                                       runat="server" CssClass="textboxStyle"></asp:TextBox>
                                                </td>
                                                <td width="10%" align="right" height="35px">
                                                    <asp:Label ID="Label4" runat="server" Text="焊接日期"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="35px">
                                                    &nbsp;
                                                   <asp:TextBox ID="txtJOT_WeldDate" Style="width: 80%; cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})"
                                                      runat="server" CssClass="textboxStyle" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10%" align="right" height="35px">
                                                    <asp:Label ID="Label9" runat="server" Text="备注"></asp:Label>
                                                </td>
                                                <td colspan="5" width="20%" align="left" height="35px">
                                                    &nbsp;
                                                    <asp:TextBox ID="txtJOT_Remark" runat="server" Width="90%" CssClass="textboxStyle"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table id="Table6" width="100%" cellpadding="0" cellspacing="0" runat="server">
                                <tr style="background: url('../Images/bg-1.gif')">
                                    <td colspan="5" align="left" valign="middle" style="width: 100%">
                                        <asp:CheckBox runat="server" ID="ckAll" Text="是否默认批量填充焊工" />&nbsp;&nbsp;
                                        <asp:CheckBox runat="server" ID="ckBoth" Text="是否默认打底、盖面焊工一致" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" width="100%">
                                        <div id="div2" style="overflow: auto; overflow-x: hidden" runat="server">
                                            <asp:GridView ID="gvWeldReportDetail" runat="server" AllowSorting="True" PageSize="200"
                                                AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%" OnRowCommand="gvWeldReportDetail_RowCommand"
                                                 AlternatingRowStyle-CssClass="GridBgColr"
                                                OnRowDataBound="gvWeldReportDetail_DataBound">
                                                <AlternatingRowStyle CssClass="GridBgColr" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="序号">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="工作区">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtBAW_ID" runat="server" Text='<%# GetBAW_ID(Eval("ISO_ID")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="管线编号">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtISO_ID" runat="server" Text='<%# GetISO_IsoNo(Eval("ISO_ID")) %>'></asp:Label>
                                                            <asp:HiddenField ID="hdISO_ID" runat="server" Value='<%# Bind("ISO_ID") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="13%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="焊口代号">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtJOT_JointNo" runat="server" CssClass="textboxnoneborder" Text='<%# Bind("JOT_JointNo") %>'
                                                                Width="90%"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="9%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="探伤比例">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtNDTR_ID" runat="server" CssClass="textboxnoneborder" Text='<%# GetNDTR(Eval("ISO_ID")) %>'
                                                                Width="90%"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="6%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="盖面焊工">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtJOT_CellWelder" runat="server" Text='<%# GetPersonNameByJOT_CellWelder(Eval("JOT_CellWelder")) %>'
                                                                CssClass="textboxnoneborder" Width="98%" ReadOnly="true"></asp:TextBox>
                                                            <asp:HiddenField ID="hdJOT_CellWelder" runat="server" Value='<%# Bind("JOT_CellWelder") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle Width="6%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="打底焊工">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtJOT_FloorWelder" runat="server" Text='<%# GetPersonNameByJOT_FloorWelder(Eval("JOT_FloorWelder")) %>'
                                                                CssClass="textboxnoneborder" Width="98%" ReadOnly="true"></asp:TextBox>
                                                            <asp:HiddenField ID="hdJOT_FloorWelder" runat="server" Value='<%# Bind("JOT_FloorWelder") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle Width="7%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="焊接区域">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drpWLO_Code" runat="server" Height="22" Width="90%" CssClass="textboxnoneborder"
                                                                SelectedValue='<%# Bind("WLO_Code") %>'>
                                                                <asp:ListItem Value="F">安装</asp:ListItem>
                                                                <asp:ListItem Value="S">预制</asp:ListItem>
                                                                 <asp:ListItem Value="">未知</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="7%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="焊口属性">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drpJOT_JointAttribute" runat="server" Height="22" Width="90%"
                                                                CssClass="textboxnoneborder" SelectedValue='<%# Bind("JOT_JointAttribute") %>'>
                                                                <asp:ListItem Value="活动">活动</asp:ListItem>
                                                                <asp:ListItem Value="固定">固定</asp:ListItem>
                                                                 <asp:ListItem Value="">未知</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="7%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="焊接位置">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="drpJOT_Location" runat="server" Height="22" Width="90%" CssClass="textboxnoneborder">
                                                                <asp:ListItem Value="1G">1G</asp:ListItem>
                                                                <asp:ListItem Value="2G">2G</asp:ListItem>
                                                                <asp:ListItem Value="3G">3G</asp:ListItem>
                                                                <asp:ListItem Value="4G">4G</asp:ListItem>
                                                                <asp:ListItem Value="5G">5G</asp:ListItem>
                                                                <asp:ListItem Value="6G">6G</asp:ListItem>
                                                                <asp:ListItem Value="">未知</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="6%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="实际寸径">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtJOT_Size" runat="server" Text='<%# Bind("JOT_Size","{0:F2}") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="8%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="完成达因">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtJOT_DoneDin" runat="server" CssClass="textboxnoneborder" Text='<%# JOT_DoneDin(Eval("JOT_ID")) %>'
                                                                Width="90%" onkeypress="keypress()"></asp:TextBox>
                                                            <asp:HiddenField runat="server" ID="hdJOT_ID" Value='<%# Bind("JOT_ID") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="8%" />
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
                                <tr style="height: 32px">
                                    <td style="width: 70%">
                                    </td>
                                    <td align="center" style="width: 11%">
                                        总计：
                                    </td>
                                    <td align="center" style="width: 5%">
                                        <asp:Label ID="lblSize" runat="server" Width="80%"></asp:Label>&nbsp
                                    </td>
                                    <td align="left" style="width: 5%">
                                        &nbsp<asp:Label ID="lblDine" runat="server" Width="80%"></asp:Label>
                                    </td>
                                    <td style="width: 9%">
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
    $("#div2").height(height - 270);

</script>
