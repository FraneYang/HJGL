<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrustManageEdit.aspx.cs" Inherits="Web.TrustManage.TrustManageEdit" %>

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
        function ShowSearch(unitId, installationId) {
            var result = window.showModalDialog("ShowTrustSearch.aspx?rnd=" + (new Date()).getTime() + "&unitId=" + unitId + "&installationId=" + installationId, "", "status=no;dialogWidth=750px;dialogHeight=478px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != null && result != "") {
                document.getElementById("hdSelectList").value = result;
                document.getElementById("imgBtnGetSearch").click();
            }
        }

        function ReportSearch() {
            document.getElementById("imgReportSearch").click();
        }       

        function CheckDropDownList(source, args) {
            if (args.Value == "0")
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        function ShowToPoint(toPoint) {
            var result = window.showModalDialog("../WeldingManage/ShowPointItem.aspx?rnd=" + (new Date()).getTime() + "&toPoint=" + toPoint, "", "status=no;dialogWidth=900px;dialogHeight=540px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != null && result != "") {
                location.href = result;
            }
        }

        function TrustInfoPrint(reportId, replaceParameter, varValue) {
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
                            &nbsp;无损委托单录入
                        </td>
                        <td align="right" valign="middle" style="width: 75%; height: 30px;">
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/addbutton.gif" OnClick="btnAdd_Click" />
                            <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/Print1.gif" 
                                ToolTip="打印" onclick="btnPrint_Click" /><asp:ImageButton ID="ImageButton1"
                                    runat="server" Width="0" />
                            <asp:ImageButton ID="btnDelete" runat="server" 
                                ImageUrl="~/Images/deletebutton.gif" onclick="btnDelete_Click" OnClientClick="return confirm(&quot;确定要删除此条信息吗？&quot;);"/>
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
                                <tr >
                                     <td  style="border-bottom: 1px solid Black;">
                                    <asp:DropDownList ID="drpSearch" runat="server" Width="25%" Height="22px" CssClass="textboxStyle"
                                            AutoPostBack="true" onselectedindexchanged="drpSearch_SelectedIndexChanged">
                                       <asp:ListItem>按月份</asp:ListItem>
                                       <asp:ListItem>按单号</asp:ListItem>
                                    </asp:DropDownList>
                                    
                                    <input id="txtReportDate" runat="server" class="Wdate" 
                                       style="width: 35%; height:20px; cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM',skin:'whyGreen'})" />
                                     
                                     <asp:TextBox ID="txtSearchCode" runat="server" style="width: 38%; height:20px;" CssClass="textboxStyle"></asp:TextBox>
                                 &nbsp;<asp:ImageButton ID="imgReportSearch" runat="server" ImageUrl="~/Images/search.png" Style="cursor: hand"
                                                        ToolTip="查询" onclick="imgReportSearch_Click" ImageAlign="AbsMiddle"/>
                                    </td>
                                </tr>
                                <tr >
                                    <td align="left" >
                                        <div id="div1" style="width: 100%; overflow: auto;" runat="server">
                                            <font face="宋体">
                                                <asp:TreeView ID="tvControlItem" ForeColor="Black" runat="server" ExpandDepth="1"
                                                    ShowCheckBoxes="None" Height="360px" Width="100%" ShowLines="True" OnSelectedNodeChanged="tvControlItem_SelectedNodeChanged"
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
                        <td  valign="top" style="width: 75%">
                            <table id="Table3" runat="server" width="100%" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td colspan="3" style="width: 100%; background: url('../Images/bg-1.gif')">
                                        <table id="Table4" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                                            cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td align="left" valign="middle" style="width: 20%; font-size: 11pt; font-weight: bold">
                                                    <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image1" runat="server" />
                                                    &nbsp;无损委托
                                                </td>
                                                <td align="right" style="width: 80%; height: 30px;">   
                                                    <input id="hdSelectList" type="hidden" runat="server" />                                                                                                      
                                                    <asp:ImageButton ID="imgBtnGetSearch" runat="server" Width="0" OnClick="imgBtnSearch_Click"
                                                        Style="height: 1px" />                                                                                               
                                                    <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/Images/Find.gif" Style="cursor: hand"
                                                        ToolTip="查询" OnClick="imgSearch_Click" />
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
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label1" runat="server" Text="委托单号" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtCH_TrustCode" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCH_TrustCode"
                                                        Display="Dynamic" ErrorMessage="请输入委托单号" ForeColor="Red" SetFocusOnError="True"
                                                        ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label7" runat="server" Text="委托单位" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    &nbsp;<asp:DropDownList 
                                                        ID="drpCH_TrustUnit" runat="server" Width="80%" Height="22px" AutoPostBack="true"
                                                        onselectedindexchanged="drpCH_TrustUnit_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CustomValidator1" runat="server" Display="Dynamic" ErrorMessage="请选择委托单位！"
                                                        ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpCH_TrustUnit" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                                                </td>
                                                 <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label5" runat="server" Text="装置名称" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    &nbsp;<asp:DropDownList ID="drpInstallationId" runat="server" Width="80%" CssClass="textboxStyle"></asp:DropDownList>
                                                     <asp:CustomValidator ID="CustomValidator5" runat="server" Display="Dynamic" ErrorMessage="请选择探伤比例！"
                                                        ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpInstallationId" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                                                </td>
                                            </tr>                                            
                                            <tr>
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label2" runat="server" Text="委托日期" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    &nbsp;<input id="txtCH_TrustDate" runat="server" readonly="readonly" class="Wdate" style="width: 75%;
                                                        cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCH_TrustDate"
                                                        Display="Dynamic" ErrorMessage="请输入委托日期" ForeColor="Red" SetFocusOnError="True"
                                                        ValidationGroup="Save">*</asp:RequiredFieldValidator>                                                  
                                                </td>
                                               
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label4" runat="server" Text="压力"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtCH_Press" runat="server" Width="80%" CssClass="textboxStyle" ></asp:TextBox>                                                     
                                                </td>
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label6" runat="server" Text="探伤比例" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    &nbsp;<asp:DropDownList ID="drpCH_NDTRate" runat="server" Width="80%" Height="22px">
                                                    </asp:DropDownList>
                                                     <asp:CustomValidator ID="CustomValidator3" runat="server" Display="Dynamic" ErrorMessage="请选择探伤比例！"
                                                        ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpCH_NDTRate" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label10" runat="server" Text="委托类型"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    &nbsp;<asp:Label ID="txtCH_TrustType" runat="server" Width="80%" CssClass="textboxStyle" Text="委托"></asp:Label>
                                                    
                                                </td>
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label11" runat="server" Text="工号"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtCH_WorkNo" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                                                    
                                                </td>
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label12" runat="server" Text="检测方法" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    &nbsp;<asp:DropDownList ID="drpCH_NDTMethod" runat="server" Width="80%" Height="22px">
                                                    </asp:DropDownList> 
                                                    <asp:CustomValidator ID="CustomValidator4" runat="server" Display="Dynamic" ErrorMessage="请选择检测方法！"
                                                        ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpCH_NDTMethod" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label13" runat="server" Text="委托人"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    &nbsp;<asp:DropDownList ID="drpCH_TrustMan" runat="server" Width="80%" Height="22px">
                                                    </asp:DropDownList> 
                                                </td>
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label14" runat="server" Text="检件名称"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtCH_ItemName" runat="server" Width="80%" 
                                                        CssClass="textboxStyle"></asp:TextBox>
                                                </td>
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label18" runat="server" Text="合格等级"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    &nbsp;<asp:DropDownList 
                                                        ID="drpCH_AcceptGrade" runat="server" Width="80%" AutoPostBack="true" Height="22px"
                                                        CssClass="textboxStyle" 
                                                        onselectedindexchanged="drpCH_AcceptGrade_SelectedIndexChanged"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label16" runat="server" Text="制单人"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    &nbsp;<asp:DropDownList ID="drpCH_Tabler" runat="server" Width="80%" Height="22px">
                                                    </asp:DropDownList> 
                                                </td>
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label17" runat="server" Text="坡口类型"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    &nbsp;<asp:DropDownList ID="drpCH_SlopeType" runat="server" Width="80%" Height="22px"
                                                        CssClass="textboxStyle"></asp:DropDownList>
                                                </td>
                                                 <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label15" runat="server" Text="检测标准"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtCH_NDTCriteria" runat="server" Width="80%" 
                                                        CssClass="textboxStyle"></asp:TextBox>
                                                &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label9" runat="server" Text="焊接方法"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    &nbsp;<asp:DropDownList ID="drpCH_WeldMethod" runat="server" Width="80%" Height="22px">
                                                    </asp:DropDownList>                                                    
                                                </td>
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label20" runat="server" Text="介质温度"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtCH_ServiceTemp" runat="server" Width="80%" 
                                                        CssClass="textboxStyle"></asp:TextBox>
                                                </td>
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label21" runat="server" Text="检测单位" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    &nbsp;<asp:DropDownList 
                                                        ID="drpCH_CheckUnit" runat="server" Width="80%" Height="22px">
                                                    </asp:DropDownList> 
                                                    <asp:CustomValidator ID="CustomValidator2" runat="server" Display="Dynamic" ErrorMessage="请选择检测单位！"
                                                        ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpCH_CheckUnit" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                 <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label3" runat="server" Text="需检日期" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td width="20%" align="left" height="25px">
                                                    &nbsp;<input id="txtCH_RequestDate" runat="server" readonly="readonly" class="Wdate" style="width: 75%;
                                                        cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCH_RequestDate"
                                                        Display="Dynamic" ErrorMessage="请输入需检日期" ForeColor="Red" SetFocusOnError="True"
                                                        ValidationGroup="Save">*</asp:RequiredFieldValidator>                                                  
                                                </td>
                                                <td width="10%" align="right" height="25px">
                                                    <asp:Label ID="Label22" runat="server" Text="备注"></asp:Label>
                                                </td>
                                                <td width="25%" align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtCH_Remark" runat="server" Width="90%" CssClass="textboxStyle"></asp:TextBox>                                                    
                                                </td>
                                                <td width="10%" align="right" height="25px">
                                                     <asp:Label ID="Label8" runat="server" Text="对应点口单"></asp:Label>
                                                </td>
                                                <td width="25%" align="left" height="25px">
                                                  &nbsp;<asp:LinkButton ID="lbtnToPoint" runat="server"  Font-Bold="true" ForeColor="Blue"
                                                       onclick="lbtnToPoint_Click"></asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table id="Table6" width="100%" cellpadding="0" cellspacing="0" runat="server">                                
                                <tr>
                                    <td colspan="5" width="100%">
                                    <div id="div2" style="overflow: auto;overflow-x:hidden" runat="server">
                                        <asp:GridView ID="gvTrustItem" runat="server" AllowSorting="True" PageSize="500"
                                            AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%" OnRowCommand="gvTrustItem_RowCommand"
                                            AlternatingRowStyle-CssClass="GridBgColr">
                                            <AlternatingRowStyle CssClass="GridBgColr" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                </asp:TemplateField>                                                
                                                <asp:TemplateField HeaderText="管线编号" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ISO_IsoNo" runat="server" Text='<%# Bind("ISO_IsoNo") %>'></asp:Label>
                                                    </ItemTemplate>  
                                                </asp:TemplateField>                 
                                                <asp:TemplateField HeaderText="焊口编号" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbJOT_JointNo" runat="server" Text='<%# Bind("JOT_JointNo") %>' ></asp:Label>
                                                         <asp:HiddenField ID="hdJOT_ID" runat="server" Value='<%# Bind("JOT_ID") %>' />
                                                    </ItemTemplate> 
                                                </asp:TemplateField>                                                                                       
                                                <asp:TemplateField HeaderText="备注">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="CH_Remark" runat="server" CssClass="textboxnoneborder" Text='<%# Bind("CH_Remark") %>'
                                                            Width="90%"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderText="外径">
                                                    <ItemTemplate>
                                                        <asp:Label ID="JOT_Dia" runat="server" Text='<%# Bind("JOT_Dia") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="8%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="壁厚">
                                                    <ItemTemplate>
                                                        <asp:Label ID="JOT_Sch" runat="server" Text='<%# Bind("JOT_Sch") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="8%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="焊接区域">
                                                    <ItemTemplate>
                                                        <asp:Label ID="WLO_Code" runat="server" Text='<%# Bind("WLO_Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="8%" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="焊接方法" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="WME_Name" runat="server" Text='<%# Bind("WME_Name") %>'></asp:Label>
                                                    </ItemTemplate> 
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
                                <tr style="height:32px">
                    <td style="width:72%">
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
    $("#div2").height(height - 325);
</script>
