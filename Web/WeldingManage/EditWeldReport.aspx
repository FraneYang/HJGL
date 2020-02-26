<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditWeldReport.aspx.cs"
    Inherits="Web.WeldingManage.EditWeldReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ValidateGroupControl.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function ShowSearch(unitId) {
            var result = window.showModalDialog("ShowReportSearch.aspx?rnd=" + (new Date()).getTime() + "&unitId=" + unitId, "", "status=no;dialogWidth=750px;dialogHeight=478px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != null && result != "") {
                document.getElementById("hdSelectList").value = result;
                document.getElementById("imgBtnGetSearch").click();
            }
        }

        function GetPersonAll1() {
            document.getElementById("imgBtnSave").click();
            var result = window.showModalDialog("ShowPerson.aspx?rnd=" + (new Date()).getTime(), "", "status=no;dialogWidth=750px;dialogHeight=478px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != null && result != "") {
                var val = result.split(",");
                document.getElementById("hdAll1").value = val[0];
                document.getElementById("imgBtnAll1").click();
            }
        }

        function GetPersonAll2_C() {
            document.getElementById("imgBtnSave").click();
            var result = window.showModalDialog("ShowPerson.aspx?rnd=" + (new Date()).getTime(), "", "status=no;dialogWidth=750px;dialogHeight=478px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != null && result != "") {
                var val = result.split(",");
                document.getElementById("hdAll1").value = val[0];
                document.getElementById("imgBtnAll2_C").click();
            }
        }

        function GetPersonAll2_F() {
            document.getElementById("imgBtnSave").click();
            var result = window.showModalDialog("ShowPerson.aspx?rnd=" + (new Date()).getTime(), "", "status=no;dialogWidth=750px;dialogHeight=478px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != null && result != "") {
                var val = result.split(",");
                document.getElementById("hdAll1").value = val[0];
                document.getElementById("imgBtnAll2_F").click();
            }
        }

        function GetPerson3(JOT_CellWelderClientID, hdJOT_CellWelderClientID, JOT_FloorWelderClientID, hdJOT_FloorWelderClientID) {
            document.getElementById("imgBtnSave").click();
            var result = window.showModalDialog("ShowPerson.aspx?rnd=" + (new Date()).getTime(), "", "status=no;dialogWidth=750px;dialogHeight=478px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != null && result != "") {
                var val = result.split(",");
                document.getElementById(hdJOT_CellWelderClientID).value = val[0];
                document.getElementById(JOT_CellWelderClientID).value = val[1];
                document.getElementById(hdJOT_FloorWelderClientID).value = val[0];
                document.getElementById(JOT_FloorWelderClientID).value = val[1];
                document.getElementById("imgBtnAll3").click();
            }
        }

        function GetPerson1(JOT_CellWelderClientID, hdJOT_CellWelderClientID) {
            document.getElementById("imgBtnSave").click();
            var result = window.showModalDialog("ShowPerson.aspx?rnd=" + (new Date()).getTime(), "", "status=no;dialogWidth=750px;dialogHeight=478px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != null && result != "") {
                var val = result.split(",");
                document.getElementById(hdJOT_CellWelderClientID).value = val[0];
                document.getElementById(JOT_CellWelderClientID).value = val[1];
                document.getElementById("imgBtnAll3").click();
            }
        }

        function GetPerson2(JOT_FloorWelderClientID, hdJOT_FloorWelderClientID) {
            document.getElementById("imgBtnSave").click();
            var result = window.showModalDialog("ShowPerson.aspx?rnd=" + (new Date()).getTime(), "", "status=no;dialogWidth=750px;dialogHeight=478px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" runat="server" width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="3" style="width: 100%; background: url('../Images/bg-1.gif')">
                <table id="tabbtn" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" valign="middle" style="width: 20%; font-size: 11pt; font-weight: bold">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;焊接日报
                        </td>
                        <td align="right" style="width: 80%; height: 30px;">
                        
                            <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/Images/Search.gif" Style="cursor: hand"
                                ToolTip="查询" OnClick="imgSearch_Click" />
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" ValidationGroup="Save"
                                OnClick="btnSave_Click" Style="height: 20px" />
                            <asp:ImageButton ID="btnReturn" runat="server" ImageUrl="~/Images/Return.gif" OnClick="btnReturn_Click" />
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
                                    <asp:ImageButton ID="imgBtnSave" runat="server" Width="0" OnClick="imgBtnSave_Click"
                                Style="height: 1px" />
                                          <asp:ImageButton ID="imgBtnAll3" runat="server" Width="0" OnClick="imgBtnAll3_Click"
                                Style="height: 1px" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3" id="AddItem" runat="server">
                <table id="Table2" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                    <tr>
                        <td width="10%" align="right" height="35px">
                            <asp:Label ID="Label1" runat="server" Text="日报告号"></asp:Label>
                        </td>
                        <td width="20%" align="left" height="35px">
                            &nbsp;
                            <asp:TextBox ID="txtDReportID" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDReportID"
                                Display="Dynamic" ErrorMessage="请输入日报告号" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td width="10%" align="right" height="35px">
                            <asp:Label ID="Label3" runat="server" Text="单位"></asp:Label>
                        </td>
                        <td width="20%" align="left" height="35px">
                            &nbsp;
                            <asp:DropDownList ID="drpUnit" runat="server" Width="80%" Height="22px">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator2" runat="server" Display="Dynamic" ErrorMessage="请选择单位！"
                                ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpUnit" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                        </td>
                        <td width="10%" align="right" height="35px">
                            <asp:Label ID="Label4" runat="server" Text="焊接日期"></asp:Label>
                        </td>
                        <td width="20%" align="left" height="35px">
                            &nbsp;
                            <input id="txtJOT_WeldDate" runat="server" readonly="readonly" class="Wdate" style="width: 80%;
                                cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
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
                            <input id="txtCHT_TableDate" runat="server" readonly="readonly" class="Wdate" style="width: 80%;
                                cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
                        </td>
                        <td width="10%" align="right" height="35px">
                            <asp:Label ID="Label6" runat="server" Text="备注"></asp:Label>
                        </td>
                        <td width="20%" align="left" height="35px">
                            &nbsp;
                            <asp:TextBox ID="txtJOT_Remark" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="Table3" width="100%" cellpadding="0" cellspacing="0" runat="server">
     <tr style="background: url('../Images/bg-1.gif')">
                    <td align="left" valign="middle" style="width:100%">
                    <asp:CheckBox runat="server" ID="ckAll" Text="是否默认批量填充焊工" />&nbsp;&nbsp;
                         <asp:CheckBox runat="server" ID="ckBoth" Text="是否默认打底、盖面焊工一致" />
                    </td>
                 </tr>
        <tr>
            <td width="100%">
                <asp:GridView ID="gvWeldReportDetail" runat="server" AllowSorting="True" PageSize="500"
                    AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%" OnRowCommand="gvWeldReportDetail_RowCommand"
                    OnRowCreated="gvWeldReportDetail_RowCreated" AlternatingRowStyle-CssClass="GridBgColr"
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
                                <asp:Label ID="txtBAW_ID" runat="server" Text='<%# GetBAW_ID(Eval("ISO_ID")) %>'
                                   ></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="15%" />
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="管线编号">
                            <ItemTemplate>
                                <asp:Label ID="txtISO_ID" runat="server" Text='<%# GetISO_IsoNo(Eval("ISO_ID")) %>'
                                   ></asp:Label>
                                    <asp:HiddenField ID="hdISO_ID" runat="server" Value='<%# Bind("ISO_ID") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="15%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="焊口代号">
                            <ItemTemplate>
                                <asp:TextBox ID="txtJOT_JointNo" runat="server" CssClass="textboxnoneborder" Text='<%# Bind("JOT_JointNo") %>'
                                    Width="90%"></asp:TextBox>
                            </ItemTemplate>
                            <ItemStyle Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="探伤比例">
                            <ItemTemplate>
                                <asp:Label ID="txtNDTR_ID" runat="server" CssClass="textboxnoneborder" Text='<%# GetNDTR(Eval("ISO_ID")) %>'
                                    Width="90%"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="10%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="盖面焊工">
                            <ItemTemplate>
                                <asp:TextBox ID="txtJOT_CellWelder" runat="server" Text='<%# GetPersonNameByJOT_CellWelder(Eval("JOT_CellWelder")) %>'
                                    CssClass="textboxnoneborder" Width="98%" ReadOnly="true"></asp:TextBox>
                                <asp:HiddenField ID="hdJOT_CellWelder" runat="server" Value='<%# Bind("JOT_CellWelder") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle Width="7%" />
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
                                            </asp:DropDownList>
                                        </ItemTemplate>
                            <ItemStyle Width="7%" />
                        </asp:TemplateField>
                                 <asp:TemplateField HeaderText="焊口属性">
                          <ItemTemplate>
                                            <asp:DropDownList ID="drpJOT_JointAttribute" runat="server" Height="22" Width="90%" CssClass="textboxnoneborder"
                                                SelectedValue='<%# Bind("JOT_JointAttribute") %>'>
                                                <asp:ListItem Value="活动">活动</asp:ListItem>
                                                <asp:ListItem Value="固定">固定</asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                            <ItemStyle Width="7%" />
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="实际寸径">
                            <ItemTemplate>
                                <asp:Label ID="txtJOT_Size" runat="server" Text='<%# Bind("JOT_Size") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="7%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="完成达因">
                            <ItemTemplate>
                                <asp:TextBox ID="txtJOT_DoneDin" runat="server" CssClass="textboxnoneborder" Text='<%# JOT_DoneDin(Eval("JOT_ID")) %>'
                                    Width="90%" onkeypress="keypress()"></asp:TextBox>
                                    <asp:HiddenField runat="server" ID="hdJOT_ID" Value='<%# Bind("JOT_ID") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="7%" />
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
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"></asp:ObjectDataSource>
            </td>
        </tr>
    </table>
     <asp:ValidationSummary ID="ValidationSummary1" Style="z-index: 101; left: 8px; position: absolute;
        top: 8px" runat="server" HeaderText="请注意！" ShowMessageBox="True" ShowSummary="False"
        ValidationGroup="Save" />
    </form>
</body>
</html>
