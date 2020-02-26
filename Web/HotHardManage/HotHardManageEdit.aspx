<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotHardManageEdit.aspx.cs" Inherits="Web.HotHardManage.HotHardManageEdit" %>

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
            var result = window.showModalDialog("ShowHotHardSearch.aspx?rnd=" + (new Date()).getTime() + "&unitId=" + unitId + "&installationId=" + installationId, "", "status=no;dialogWidth=750px;dialogHeight=478px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
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

        function HotHardInfoPrint(reportId, replaceParameter, varValue) {
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
                            &nbsp;硬度委托录入
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
                                    月份<input id="txtReportDate" runat="server" class="Wdate" 
                              style="width: 25%; height:20px; cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM',skin:'whyGreen'})" />
                                 &nbsp;<asp:ImageButton ID="imgReportSearch" runat="server" ImageUrl="~/Images/search.png" Style="cursor: hand"
                                                        ToolTip="查询" onclick="imgReportSearClick" ImageAlign="AbsMiddle"/>
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
                                                    &nbsp;硬度委托
                                                </td>
                                                <td align="right" style="width: 80%; height: 30px;">   
                                                    <input id="hdSelectList" type="hidden" runat="server" />                                                                                                      
                                                    <asp:ImageButton ID="imgBtnGetSearch" runat="server" Width="0" OnClick="imgBtnSearClick"
                                                        Style="height: 1px" />                                                                                               
                                                    <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/Images/Find.gif" Style="cursor: hand"
                                                        ToolTip="查询" OnClick="imgSearClick" />
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
                                                <td width="15%" align="right" height="25px">
                                                    <asp:Label ID="Label7" runat="server" Text="委托单位" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td width="27%" align="left" height="25px">
                                                    &nbsp;<asp:DropDownList ID="drpHotHardUnit" runat="server" Width="80%" Height="22px" AutoPostBack="true"
                                                        onselectedindexchanged="drpHotHardUnit_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CustomValidator1" runat="server" Display="Dynamic" ErrorMessage="请选择委托单位！"
                                                        ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpHotHardUnit" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                                                </td>
                                                <td width="15%" align="right" height="25px">
                                                    <asp:Label ID="Label5" runat="server" Text="装置名称" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td width="15%" align="left" height="25px">
                                                    &nbsp;<asp:DropDownList ID="drpInstallationId" runat="server" Width="80%" CssClass="textboxStyle" Height="22px"></asp:DropDownList>
                                                     <asp:CustomValidator ID="CustomValidator5" runat="server" Display="Dynamic" ErrorMessage="请选择装置名称！"
                                                        ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpInstallationId" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                                                </td>
                                                <td width="12%" align="right" height="25px">
                                                    <asp:Label ID="Label1" runat="server" Text="委托单号" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td width="15%" align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtHotHardCode" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtHotHardCode"
                                                        Display="Dynamic" ErrorMessage="请输入委托单号" ForeColor="Red" SetFocusOnError="True"
                                                        ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </td>
                                              
                                            </tr>                                            
                                            <tr>                                            
                                                <td  align="right" height="25px">
                                                    <asp:Label ID="Label21" runat="server" Text="检测单位" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:DropDownList 
                                                        ID="drpCheckUnit" runat="server" Width="80%" Height="22px">
                                                    </asp:DropDownList> 
                                                    <asp:CustomValidator ID="CustomValidator2" runat="server" Display="Dynamic" ErrorMessage="请选择检测单位！"
                                                        ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpCheckUnit" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                                                </td>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label13" runat="server" Text="委托人" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td align="left" height="25px">
                                                    &nbsp;<asp:DropDownList ID="drpHotHardMan" runat="server" Width="80%" Height="22px">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CustomValidator3" runat="server" Display="Dynamic" ErrorMessage="请选择委托人！"
                                                        ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpHotHardMan" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator> 
                                                </td>  
                                                <td  align="right" height="25px">
                                                    <asp:Label ID="Label2" runat="server" Text="委托日期" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<input id="txtHotHardDate" runat="server" readonly="readonly" class="Wdate" style="width: 75%;
                                                        cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
                                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtHotHardDate"
                                                        Display="Dynamic" ErrorMessage="请输入委托日期" ForeColor="Red" SetFocusOnError="True"
                                                        ValidationGroup="Save">*</asp:RequiredFieldValidator>                                                  
                                                </td>  
                                            </tr>
                                            <tr>
                                                 <td  align="right" height="25px">
                                                    <asp:Label ID="Label12" runat="server" Text="检测方法" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtNDTMethod" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtNDTMethod"
                                                        Display="Dynamic" ErrorMessage="请输入检测方法" ForeColor="Red" SetFocusOnError="True"
                                                        ValidationGroup="Save">*</asp:RequiredFieldValidator> 
                                                </td>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label6" runat="server" Text="检测比例"></asp:Label>
                                                </td>
                                                <td align="left" height="25px">
                                                  &nbsp;<asp:TextBox ID="txtNDTRate" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                                                </td>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label4" runat="server" Text="执行标准"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                   &nbsp;<asp:TextBox ID="txtStandards" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                                                </td>
                                                                                                                                  
                                            </tr>
                                            <tr>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label8" runat="server" Text="报检/检查记录编号"></asp:Label>
                                                </td>
                                                <td align="left" height="25px">
                                                  &nbsp;<asp:TextBox ID="txtInspectionNum" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                                                </td>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label9" runat="server" Text="外观检查合格焊口数"></asp:Label>
                                                </td>
                                                <td align="left" height="25px">
                                                   &nbsp;<asp:TextBox ID="txtCheckNum" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                                                </td>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label10" runat="server" Text="委托检测焊口数"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtTestWeldNum" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>                                           
                                                </td>                                                                                     
                                            </tr>
                                            <tr>                                              
                                                <td align="right" height="25px">
                                                <asp:Label ID="lb" runat="server" Text="检测时机"></asp:Label>
                                                </td>
                                                <td align="left" height="25px" colspan="3">
                                                    &nbsp;<asp:CheckBox  ID="ckDetectionTime0" runat="server" Text="工厂化预制焊口" 
                                                        oncheckedchanged="ckDetectionTime0_CheckedChanged" AutoPostBack="True"/>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="ckDetectionTime1" runat="server" Text="安装施工焊口" 
                                                    oncheckedchanged="ckDetectionTime1_CheckedChanged" AutoPostBack="True"/>&nbsp;                                                 
                                                </td> 
                                                 <td align="right" height="25px">
                                                    <asp:Label ID="Label3" runat="server" Text="接收人"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtSendee" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
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
                                        <asp:GridView ID="gvHotHardItem" runat="server" AllowSorting="True" PageSize="500"
                                            AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%" OnRowCommand="gvHotHardItem_RowCommand"
                                            AlternatingRowStyle-CssClass="GridBgColr">
                                            <AlternatingRowStyle CssClass="GridBgColr" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                </asp:TemplateField> 
                                                 <asp:TemplateField HeaderText="焊工代号" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CellWelderCode" runat="server" Text='<%# Bind("CellWelderCode") %>'></asp:Label>
                                                    </ItemTemplate>  
                                                </asp:TemplateField>                                                
                                                <asp:TemplateField HeaderText="管道编号" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
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
                                                <asp:TemplateField HeaderText="规格(mm)" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="JOT_JointDesc" runat="server" Text='<%# Bind("JOT_JointDesc") %>'></asp:Label>
                                                    </ItemTemplate>  
                                                </asp:TemplateField> 
                                                 <asp:TemplateField HeaderText="材质" HeaderStyle-Width="12%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="STE_Name" runat="server" Text='<%# Bind("STE_Name") %>'></asp:Label>
                                                    </ItemTemplate>  
                                                </asp:TemplateField> 
                                                <asp:TemplateField HeaderText="单线图号">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ISO_IsoNumber" runat="server" CssClass="textboxnoneborder" Text='<%# Bind("ISO_IsoNumber") %>'
                                                            Width="90%"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="8%" />
                                                </asp:TemplateField>                                                                                      
                                                <asp:TemplateField HeaderText="备注">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="Remark" runat="server" CssClass="textboxnoneborder" Text='<%# Bind("Remark") %>'
                                                            Width="90%"></asp:TextBox>
                                                    </ItemTemplate>
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
    $("#div2").height(height - 261);
</script>
