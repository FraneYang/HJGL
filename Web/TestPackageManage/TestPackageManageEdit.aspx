<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPackageManageEdit.aspx.cs" Inherits="Web.TestPackageManage.TestPackageManageEdit" %>

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
            var result = window.showModalDialog("ShowTestPackageSearch.aspx?rnd=" + (new Date()).getTime() + "&unitId=" + unitId + "&installationId=" + installationId, "", "status=no;dialogWidth=900px;dialogHeight=478px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
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
                            &nbsp;试压包录入
                        </td>
                        <td align="right" valign="middle" style="width: 75%; height: 30px;">
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/addbutton.gif" OnClick="btnAdd_Click" />
                            <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/Print1.gif" 
                                ToolTip="打印"  /><asp:ImageButton ID="ImageButton1"
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
                                                    &nbsp;试压包录入
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
                                                 <td align="right" height="25px"  width="10%">
                                                    <asp:Label ID="Label7" runat="server" Text="单位"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px"  width="20%">
                                                    &nbsp;<asp:DropDownList ID="drpBSU_ID" runat="server" Width="80%" Height="22px">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CustomValidator1" runat="server" Display="Dynamic" ErrorMessage="请选择单位！"
                                                       ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpBSU_ID" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                                                </td>  
                                                <td align="right" height="25px"  width="15%">
                                                    <asp:Label ID="Label5" runat="server" Text="装置名称"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px"  width="20%">
                                                    &nbsp;<asp:DropDownList ID="drpInstallationId" runat="server" Width="80%" CssClass="textboxStyle"></asp:DropDownList>
                                                     <asp:CustomValidator ID="CustomValidator5" runat="server" Display="Dynamic" ErrorMessage="请选择装置名称！"
                                                        ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpInstallationId" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                                                </td>
                                               <td align="right" height="25px"  width="15%">
                                                    <asp:Label ID="Label8" runat="server" Text="严密性试验时间"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px" width="20%">
                                                    &nbsp;<asp:TextBox ID="txtPTP_TightnessTestTime" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>                                                    
                                                </td>
                                            </tr>                                            
                                            <tr>
                                             <td align="right" height="25px">
                                                    <asp:Label ID="Label1" runat="server" Text="试压编号"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtPTP_TestPackageNo" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPTP_TestPackageNo"
                                                        Display="Dynamic" ErrorMessage="请输入试压编号" ForeColor="Red" SetFocusOnError="True"
                                                        ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label4" runat="server" Text="修改人"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:DropDownList ID="drpPTP_Modifier" runat="server" Width="80%" Height="22px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label6" runat="server" Text="泄露性试验介质"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtPTP_LeakageTestService" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label10" runat="server" Text="系统名称"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtPTP_TestPackageName" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                                                </td>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label11" runat="server" Text="修改时间"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<input id="txtPTP_ModifyDate" runat="server" readonly="readonly" class="Wdate" style="width: 75%;
                                                        cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
                                                    
                                                </td>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label12" runat="server" Text="泄露性试验压力"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtPTP_LeakageTestPressure" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label13" runat="server" Text="试验压力"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtPTP_TestHeat" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox> 
                                                </td>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label14" runat="server" Text="试压包号"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtPTP_TestPackageCode" runat="server" Width="80%" 
                                                        CssClass="textboxStyle"></asp:TextBox>
                                                </td>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label33" runat="server" Text="严密性试验温度"></asp:Label>
                                                </td>
                                                <td align="left" height="25px" >
                                                    &nbsp;<asp:TextBox ID="txtPTP_TightnessTestTemp" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                                                </td>
                                               
                                            </tr>
                                            <tr>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label16" runat="server" Text="试验介质"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtPTP_TestService" runat="server" CssClass="textboxStyle" 
                                                        Width="80%"></asp:TextBox>
                                                </td>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label17" runat="server" Text="试验环境温度"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtPTP_TestAmbientTemp" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                                                </td>
                                                 <td align="right" height="25px">
                                                    <asp:Label ID="Label15" runat="server" Text="真空试验介质"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtPTP_VacuumTestService" runat="server" Width="80%" 
                                                        CssClass="textboxStyle"></asp:TextBox>
                                                &nbsp;</td>
                                                
                                            </tr>
                                            <tr>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label9" runat="server" Text="试验类型"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:DropDownList ID="drpPTP_TestType" runat="server" Width="80%" Height="22px">
                                                    </asp:DropDownList>                                                    
                                                </td>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label20" runat="server" Text="试验介质温度"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtPTP_TestMediumTemp" runat="server" Width="80%" 
                                                        CssClass="textboxStyle"></asp:TextBox>
                                                </td>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label18" runat="server" Text="真空试验压力"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtPTP_VacuumTestPressure" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label27" runat="server" Text="允许渗水量"></asp:Label>
                                                </td>
                                                <td align="left" height="25px" >
                                                    &nbsp;<asp:TextBox ID="txtPTP_AllowSeepage" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>                                                    
                                                </td>
                                                 <td align="right" height="25px">
                                                    <asp:Label ID="Label2" runat="server" Text="耐压试验压力"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtPTP_TestPressure" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                                                 </td>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label21" runat="server" Text="操作介质"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtPTP_OperationMedium" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                                                </td>
                                                
                                             </tr>
                                             <tr>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label30" runat="server" Text="实际渗水量"></asp:Label>
                                                </td>
                                                <td align="left" height="25px" >
                                                    &nbsp;<asp:TextBox ID="txtPTP_FactSeepage" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>                                                    
                                                </td>
                                                 <td align="right" height="25px">
                                                    <asp:Label ID="Label23" runat="server" Text="耐压试验温度"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtPTP_TestPressureTemp" runat="server" CssClass="textboxStyle" 
                                                        Width="80%"></asp:TextBox>
                                                </td>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label22" runat="server" Text="吹扫介质"></asp:Label>
                                                </td>
                                                <td align="left" height="25px" >
                                                    &nbsp;<asp:TextBox ID="txtPTP_PurgingMedium" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                 <td align="right" height="25px">
                                                    <asp:Label ID="Label25" runat="server" Text="建档人"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:DropDownList ID="drpPTP_Tabler" runat="server" Width="80%" Height="22px">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CustomValidator2" runat="server" Display="Dynamic" ErrorMessage="请选择建档人！"
                                                        ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpPTP_Tabler" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                                                    
                                                </td>
                                                 <td align="right" height="25px">
                                                    <asp:Label ID="Label26" runat="server" Text="耐压试验时间"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtPTP_TestPressureTime" runat="server" CssClass="textboxStyle" 
                                                        Width="80%"></asp:TextBox>
                                                 </td>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label24" runat="server" Text="清扫介质"></asp:Label>
                                                </td>
                                                <td align="left" height="25px" >
                                                    &nbsp;<asp:TextBox ID="txtPTP_CleaningMedium" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                 <td align="right" height="25px">
                                                    <asp:Label ID="Label28" runat="server" Text="建档时间"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<input id="txtPTP_TableDate" runat="server" readonly="readonly" class="Wdate" style="width: 75%;
                                                        cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPTP_TableDate"
                                                        Display="Dynamic" ErrorMessage="请输入建档时间" ForeColor="Red" SetFocusOnError="True"
                                                        ValidationGroup="Save">*</asp:RequiredFieldValidator>   
                                                 </td>
                                                 <td align="right" height="25px">
                                                    <asp:Label ID="Label29" runat="server" Text="严密性试验压力"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtPTP_TightnessTest" runat="server" CssClass="textboxStyle" 
                                                        Width="80%"></asp:TextBox>
                                                </td>
                                                 <td align="right" height="25px">
                                                    <asp:Label ID="Label34" runat="server" Text="备注"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                   &nbsp;<asp:TextBox ID="txtPTP_Remark" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>                                               
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
                                        <asp:GridView ID="gvTestPackage" runat="server" AllowSorting="True" PageSize="500"
                                            AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%" OnRowCommand="gvTestPackage_RowCommand"
                                            AlternatingRowStyle-CssClass="GridBgColr">
                                            <AlternatingRowStyle CssClass="GridBgColr" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                </asp:TemplateField>                                                
                                                <asp:TemplateField HeaderText="管线编号" HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ISO_IsoNo" runat="server" Text='<%# Bind("ISO_IsoNo") %>'></asp:Label>
                                                         <asp:HiddenField ID="hdISO_ID" runat="server" Value='<%# Bind("ISO_ID") %>' />
                                                    </ItemTemplate>  
                                                </asp:TemplateField>                                                           
                                                <asp:BoundField DataField="ISO_DesignPress" HeaderText="设计压力">
                                                    <ItemStyle Width="10%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ISO_DesignTemperature" HeaderText="设计温度">
                                                    <ItemStyle Width="10%" />
                                                </asp:BoundField>                                                
                                               <asp:TemplateField HeaderText="介质">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSER_ID" runat="server" Text='<%# ConvertSER_ID(Eval("SER_ID")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="删除">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="del" ToolTip="删除"
                                                            ImageUrl="~/Images/DeleteBtn.gif" CommandArgument='<%# Bind("ISO_ID") %>' />
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
    $("#div2").height(height - 400);
</script>
