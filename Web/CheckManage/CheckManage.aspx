<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckManage.aspx.cs" Inherits="Web.CheckManage.CheckManage" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>检测单录入</title>
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ValidateGroupControl.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function ShowSearch(unitId, installationId, ch_TrustID) {
            var result = window.showModalDialog("ShowTrust.aspx?rnd=" + (new Date()).getTime() + "&unitId=" + unitId + "&InstallationId=" + installationId + "&ch_TrustID=" + ch_TrustID, "", "status=no;dialogWidth=960px;dialogHeight=640px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != null && result != "") {
                document.getElementById("hdSelectList").value = result;
                document.getElementById("imgBtnGetSearch").click();
            }
        }
        function CheckSearch() {
            document.getElementById("imgReportSearch").click();
        }

        function CheckDropDownList(source, args) {
            if (args.Value == "0")
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        function GetFilmDate() {//拍片日期
            document.getElementById("imgBtnAll2").click();
        }

        function GetReportDate() { //报告日期
            document.getElementById("imgBtnReportDate").click();
        }

        ///判断是否合格
        function GetCheckRestult() {
            document.getElementById("imgCheckResult").click();
        }

        function keypress() { //只能录入数字型
            var keyASCII = event.keyCode;
            if ((keyASCII >= 48 && keyASCII <= 57)) {

            }
            else {
                event.keyCode = 0;
            }
        }
    </script>

     <script type="text/javascript" language="javascript">
         function ShowToTrust(toTrust) {
             var result = window.showModalDialog("../TrustManage/ShowTrustItem.aspx?rnd=" + (new Date()).getTime() + "&toTrust=" + toTrust, "", "status=no;dialogWidth=900px;dialogHeight=540px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
             if (result != null && result != "") {
                 location.href = result;
             }
         }

         function CheckReportPrint(reportId, replaceParameter, varValue) {
             var result = window.showModalDialog("../ReportPrint/ExReportPrint.aspx?reportId=" + reportId + "&replaceParameter=" + replaceParameter + "&varValue=" + escape(varValue), "", "status=no;dialogWidth=1024px;dialogHeight=640px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
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
                            &nbsp;检测单录入
                        </td>
                        <td align="right" valign="middle" style="width: 75%; height: 30px;">
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/addbutton.gif" 
                                OnClick="btnAdd_Click" style="height: 20px" />
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
                        <td style="width: 25%" valign="top">
                            <table cellspacing="0" cellpadding="0" width="100%">
                                <tr>
                                    <td  style="border-bottom: 1px solid Black;">
                                    <asp:DropDownList ID="drpSearch" runat="server" Width="25%" Height="22px" CssClass="textboxStyle"
                                            AutoPostBack="true" onselectedindexchanged="drpSearch_SelectedIndexChanged">
                                       <asp:ListItem>按月份</asp:ListItem>
                                       <asp:ListItem>按单号</asp:ListItem>
                                    </asp:DropDownList>
                                    
                                    <input id="txtCheckTime" runat="server" class="Wdate" 
                                       style="width: 35%; height:20px; cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM',skin:'whyGreen'})" />
                                     
                                     <asp:TextBox ID="txtSearchCode" runat="server" style="width: 38%; height:20px;" CssClass="textboxStyle"></asp:TextBox>
                                        &nbsp;<asp:ImageButton ID="imgReportSearch" runat="server" ImageUrl="~/Images/search.png"
                                            Style="cursor: hand" ToolTip="查询" OnClick="imgReportSearch_Click" ImageAlign="AbsMiddle" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <div id="div1" style="width: 100%; overflow: auto;" runat="server">
                                            <font face="宋体">
                                                <asp:TreeView ID="tvControlItem" ForeColor="Black" runat="server" ExpandDepth="1"
                                                    ShowCheckBoxes="None" Height="438px" Width="100%" ShowLines="True" OnSelectedNodeChanged="tvControlItem_SelectedNodeChanged"
                                                    NodeIndent="15" CssClass="tree">
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
                            <table id="Table3" runat="server" width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="3" style="width: 100%; background: url('../Images/bg-1.gif')">
                                        <table id="Table4" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                                            cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="left" valign="middle" style="width: 20%; font-size: 11pt; font-weight: bold">
                                                    <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image1" runat="server" />
                                                    &nbsp;检测管理
                                                </td>
                                                <td align="right" style="width: 80%; height: 30px;">
                                                    <input id="hdSelectList" type="hidden" runat="server" />
                                                    <input id="hdAll2" type="hidden" runat="server" />
                                                   <asp:ImageButton ID="imgBtnAll2" runat="server" Width="0" OnClick="imgBtnAll2_Click"
                                                        Style="height: 1px" />
                                                   <asp:ImageButton ID="imgBtnReportDate" runat="server" Width="0" OnClick="imgBtnReportDate_Click"
                                                        Style="height: 1px" />
                                                    <asp:ImageButton ID="imgBtnGetSearch" runat="server" Width="0" OnClick="imgBtnSearch_Click"
                                                        Style="height: 1px" />
                                                    <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/Images/Search.gif" Style="cursor: hand"
                                                        ToolTip="查询" OnClick="imgSearch_Click" />
                                                    <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" ValidationGroup="Save"
                                                        OnClick="btnSave_Click" Style="height: 20px" />
                                                         <asp:ImageButton ID="imgCheckResult" runat="server" Width="0" OnClick="imgCheckResult_Click"
                                                        Style="height: 1px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" id="AddItem" runat="server">
                                        <table id="Table5" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                                            <tr>
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label1" runat="server" Text="探伤单号" Font-Bold="true"></asp:Label>&nbsp;
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    <asp:TextBox ID="txtCheckCode" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCheckCode"
                                                        Display="Dynamic" ErrorMessage="请输入探伤单号" ForeColor="Red" SetFocusOnError="True"
                                                        ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label3" runat="server" Text="单位" Font-Bold="true"></asp:Label>&nbsp;
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    <asp:DropDownList ID="ddlUnit" runat="server" Width="80%" AutoPostBack="true"
                                                        CssClass="textboxStyle" onselectedindexchanged="ddlUnit_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="ddlUnit"
                                                        Display="Dynamic" ErrorMessage="&quot;请选择单位！&quot;" ForeColor="Red" ValidationGroup="Save"
                                                        ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                                                </td>
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label29" runat="server" Text="装置" Font-Bold="true"></asp:Label>&nbsp;
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    <asp:DropDownList ID="ddlInstallationId" runat="server" CssClass="textboxStyle" 
                                                        Width="80%">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CustomValidator2" runat="server" 
                                                        ClientValidationFunction="CheckDropDownList" 
                                                        ControlToValidate="ddlInstallationId" Display="Dynamic" 
                                                        ErrorMessage="&quot;请选择装置！&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:CustomValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10%" align="right" height="25px"></font>
                                                    <asp:Label ID="Label23" runat="server" Text="单据类型"></asp:Label>&nbsp;
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    <asp:TextBox ID="txtCheckType" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                                                </td>
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label24" runat="server" Text="制单人"></asp:Label>&nbsp;
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    <asp:DropDownList ID="ddlTabler" runat="server" CssClass="textboxStyle" Width="80%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label25" runat="server" Text="制单日期"></asp:Label>&nbsp;
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    <input id="txtTableDate" runat="server" class="Wdate" style="width: 80%; cursor: hand"
                                                        onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" /></td>
                                            </tr>
                                            <tr>
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label4" runat="server" Text="探伤日期" Font-Bold="true"></asp:Label>&nbsp;
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    <input id="txtCheckDate" runat="server" class="Wdate" style="width: 80%; cursor: hand"
                                                        onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" /><asp:RequiredFieldValidator
                                                            ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCheckDate"
                                                            Display="Dynamic" ErrorMessage="&quot;请输入探伤日期&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label26" runat="server" Text="审核人"></asp:Label>&nbsp;
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    <asp:DropDownList ID="ddlAuditMan" runat="server" CssClass="textboxStyle" Width="80%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label27" runat="server" Text="审核日期"></asp:Label>&nbsp;
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    <input id="txtAuditDate" runat="server" class="Wdate" style="width: 80%; cursor: hand"
                                                        onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" /></td>
                                            </tr>
                                            <tr>
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label28" runat="server" Text="检测人"></asp:Label>&nbsp;
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    <asp:DropDownList ID="ddlCheckMan" runat="server" CssClass="textboxStyle" Width="80%">
                                                    </asp:DropDownList>
                                                </td>
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label22" runat="server" Text="备注"></asp:Label>&nbsp;
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    <asp:TextBox ID="txtRemark" runat="server" Width="95%" CssClass="textboxStyle"></asp:TextBox>
                                                </td>
                                                <td width="10%" align="right" height="25px">
                                                     <asp:Label ID="Label2" runat="server" Text="对应委托单"></asp:Label>&nbsp;
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                  <asp:LinkButton ID="lbtnToTrust" runat="server"  Font-Bold="true" ForeColor="Blue"
                                                       onclick="lbtnToTrust_Click"></asp:LinkButton>
                                                </td>
                                            </tr>
                                            </table>
                                    </td>
                                </tr>
                            </table>
                            <table id="Table6" width="100%" cellpadding="0" cellspacing="0" runat="server">
                                <tr style="background: url('../Images/bg-1.gif')">
                                    <td colspan="5" align="left" valign="middle" style="width: 100%; height: 25px">
                                        <asp:CheckBox runat="server" ID="ckAllFilmDate" Text="是否批量填充拍片日期" />&nbsp;
                                        <asp:CheckBox runat="server" ID="ckAllReportDate" Text="是否批量填充报告日期" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5" width="100%">
                                        <div id="div2" style="width: 100%; overflow: auto;" runat="server">
                                            <asp:GridView ID="gvCheckItem" runat="server" AllowSorting="True" PageSize="500"
                                                AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%" OnRowCommand="gvCheckItem_RowCommand"
                                                AlternatingRowStyle-CssClass="GridBgColr" OnRowDataBound="gvCheckItem_DataBound">
                                                <AlternatingRowStyle CssClass="GridBgColr" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="序号">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="4%" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="管线编号" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="ISO_IsoNo" runat="server" Text='<%# Bind("ISO_IsoNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="15%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="焊口编号" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbJOT_JointNo" runat="server" Text='<%# Bind("JOT_JointNo") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdJOT_ID" runat="server" Value='<%# Bind("JOT_ID") %>' />
                                                            <asp:HiddenField ID="hdTrustItemId" runat="server" Value='<%# Bind("CH_TrustItemID") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="6%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="探伤方法">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbNDT_Name" runat="server" Text='<%# Bind("NDT_Name") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdNDTMethod" runat="server" Value='<%# Bind("NDT_ID") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="拍片日期">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="CHT_FilmDate" runat="server" Text='<%# Bind("CHT_FilmDate") %>'
                                                                Width="98%"  class="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="12%" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="报告日期">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="CHT_ReportDate" runat="server" Text='<%# Bind("CHT_ReportDate") %>'
                                                                Width="98%"  class="Wdate" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})"></asp:TextBox>                      
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="12%" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="返修位置">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="CHT_RepairLocation" runat="server" CssClass="textboxnoneborder"
                                                                Text='<%# Bind("CHT_RepairLocation") %>' Width="98%"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="8%" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="拍片总数">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="CHT_TotalFilm" runat="server" CssClass="textboxnoneborder" Text='<%# Bind("CHT_TotalFilm") %>'
                                                                Width="98%" onkeypress="keypress()"></asp:TextBox>
                                                             <asp:HiddenField ID="hdJOT_Dia" runat="server" Value='<%# Bind("JOT_Dia") %>' />
                                                             <asp:HiddenField ID="hdJOT_Component1" runat="server" Value='<%# Bind("JOT_Component1") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="6%" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="合格片数">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="CHT_PassFilm" runat="server" CssClass="textboxnoneborder" Text='<%# Bind("CHT_PassFilm") %>'
                                                                Width="98%" onkeypress="keypress()"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="6%" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="探伤结果">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCheckResult" runat="server" CssClass="textboxnoneborder" Text='<%# Bind("CHT_CheckResult") %>'
                                                                Width="90%" ReadOnly="true"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="8%" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="探伤报告编号">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="CHT_CheckNo" runat="server" CssClass="textboxnoneborder" Text='<%# Bind("CHT_CheckNo") %>'
                                                                Width="98%"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="8%" />
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="拍片规格">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="CHT_FilmSpecifications" runat="server" CssClass="textboxnoneborder" Text='<%# Bind("CHT_FilmSpecifications") %>'
                                                                Width="98%"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="5%" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="删除">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="del" ToolTip="删除"
                                                                ImageUrl="~/Images/DeleteBtn.gif" CommandArgument='<%# Bind("JOT_ID") %>' />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="5%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="GridBgColr" />
                                                <RowStyle CssClass="GridRow" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                                <tr style="height: 32px">
                                    <td style="width: 72%">
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
