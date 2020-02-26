<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestPackageManageAudit.aspx.cs" Inherits="Web.TestPackageManage.TestPackageManageAudit" %>

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
        function ReportSearch() {
            document.getElementById("imgReportSearch").click();
        }       

        function CheckDropDownList(source, args) {
            if (args.Value == "0")
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        function TestPackageReportPrint(reportId, replaceParameter, varValue) {
            var result = window.showModalDialog("../ReportPrint/ExReportPrint.aspx?reportId=" + reportId + "&replaceParameter=" + replaceParameter + "&varValue=" + escape(varValue), "", "status=no;dialogWidth=1024px;dialogHeight=640px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != "") {
                document.getElementById("ImageButton1").click();
            }
        }     

        function ShowJointInfoView(iSOID) {
            var iWidth = 800;
            var iHeight = 480;
            var iTop = (window.screen.availHeight - 30 - iHeight) / 2;
            var iLeft = (window.screen.availWidth - 10 - iWidth) / 2;
            window.open("ShowJointInfoView.aspx?iSOID=" + iSOID, "", "width=" + iWidth + ",height=" + iHeight + ",top=" + iTop + ",left=" + iLeft + ",status=no,toolbar=no,menubar=no,location=no,scrollbars=yes,resizable=no");
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
                            &nbsp;试压包审核
                        </td>
                        <td align="right" valign="middle" style="width: 75%; height: 30px;">
                            <asp:ImageButton ID="btnAudit" runat="server" ImageUrl="~/Images/Audit.gif" 
                                ValidationGroup="Save" onclick="btnAudit_Click"/>
                            <asp:ImageButton ID="btnCancelAudit" runat="server" 
                                ImageUrl="~/Images/CancelAudit.gif" Visible="false" 
                                onclick="btnCancelAudit_Click"/>
                            <asp:DropDownList ID="drpSelectPrint" runat="server" Width="150px" Height="21px" ToolTip="选择打印"></asp:DropDownList>  
                            <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/Print1.gif" 
                                ToolTip="打印" onclick="btnPrint_Click" /><asp:ImageButton ID="ImageButton1"
                                    runat="server" Width="0" />                            
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
                                 &nbsp;<asp:ImageButton ID="imgReportSearch" runat="server" ImageUrl="~/Images/Search.gif" Style="cursor: hand"
                                                        ToolTip="查询" onclick="imgReportSearch_Click" ImageAlign="AbsMiddle"/>
                                    </td>
                                </tr>
                                <tr >
                                    <td align="left" >
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
                        <td  valign="top" style="width: 75%">
                            <table id="Table3" runat="server" width="100%" cellpadding="0" cellspacing="0">                               
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
                                                </td>  
                                                <td align="right" height="25px"  width="15%">
                                                    <asp:Label ID="Label5" runat="server" Text="装置名称"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px"  width="20%">
                                                    &nbsp;<asp:DropDownList ID="drpInstallationId" runat="server" Width="80%" CssClass="textboxStyle"></asp:DropDownList>
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
                                                </td>
                                               <td align="right" height="25px">
                                                    <asp:Label ID="Label25" runat="server" Text="审核人"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:DropDownList ID="drpPTP_Auditer" runat="server" Width="80%" Height="22px">
                                                    </asp:DropDownList>
                                                       <asp:CustomValidator ID="CustomValidator5" runat="server" Display="Dynamic" ErrorMessage="请选择审核人！"
                                                        ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpPTP_Auditer" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
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
                                                    <asp:Label ID="Label28" runat="server" Text="审核日期"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<input id="txtPTP_AduditDate" runat="server" readonly="readonly" class="Wdate" style="width: 75%;
                                                        cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPTP_AduditDate"
                                                        Display="Dynamic" ErrorMessage="请输入审核日期" ForeColor="Red" SetFocusOnError="True"
                                                        ValidationGroup="Save">*</asp:RequiredFieldValidator>    
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
                                                <td width="80%" align="left" height="25px" >
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
                                                <td  align="left" height="25px" >
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
                                                <td width="80%" align="left" height="25px" >
                                                    &nbsp;<asp:TextBox ID="txtPTP_PurgingMedium" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                              <td align="right" height="25px">
                                                    <asp:Label ID="Label4" runat="server" Text="修改人"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:DropDownList ID="drpPTP_Modifier" runat="server" Width="80%" Height="22px">
                                                    </asp:DropDownList>
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
                                                <td width="80%" align="left" height="25px" >
                                                    &nbsp;<asp:TextBox ID="txtPTP_CleaningMedium" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                              <td align="right" height="25px">
                                                    <asp:Label ID="Label11" runat="server" Text="修改时间"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtPTP_ModifyDate" runat="server" CssClass="textboxStyle" 
                                                        Width="80%"></asp:TextBox>                                                    
                                                </td>
                                                 
                                                 <td align="right" height="25px">
                                                    <asp:Label ID="Label29" runat="server" Text="严密性试验压力"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtPTP_TightnessTest" runat="server" CssClass="textboxStyle" 
                                                        Width="80%"></asp:TextBox>
                                                </td>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label37" runat="server" Text="建档时间"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtPTP_TableDate" runat="server" CssClass="textboxStyle" 
                                                        Width="80%"></asp:TextBox> 
                                                 </td>
                                            </tr>
                                            <tr>
                                             <td align="right" height="25px">
                                                    <asp:Label ID="Label32" runat="server" Text="建档人"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:DropDownList ID="drpPTP_Tabler" runat="server" Width="80%" Height="22px">
                                                    </asp:DropDownList>
                                                </td>
                                               
                                                 <td align="right" height="25px">
                                                    <asp:Label ID="Label34" runat="server" Text="备注"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px" colspan="3">
                                                   &nbsp;<asp:TextBox ID="txtPTP_Remark" runat="server" Width="92%" CssClass="textboxStyle"></asp:TextBox>                                               
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table id="Table6" width="100%" cellpadding="0" cellspacing="0" runat="server">                                
                                <tr>
                                    <td colspan="12" width="100%">
                                    <div id="div2" style="overflow: auto;overflow-x:hidden" runat="server">
                                        <asp:GridView ID="gvTestPackage" runat="server" AllowSorting="True" PageSize="500"
                                            AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%" OnDataBound="gvTestPackage_DataBound"
                                            AlternatingRowStyle-CssClass="GridBgColr" OnRowCommand="gvTestPackage_RowCommand">
                                            <AlternatingRowStyle CssClass="GridBgColr" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                </asp:TemplateField>                                                
                                                <asp:TemplateField HeaderText="管线编号" HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                      <asp:LinkButton ID="ISO_IsoNo" runat="server" CommandArgument='<%# Bind("ISO_ID") %>'
                                                        CssClass="ItemLink" Text='<%# Bind("ISO_IsoNo") %>' ToolTip="查看焊口信息"
                                                        CommandName="lbLink"></asp:LinkButton>
                                                         <asp:HiddenField ID="hdISO_ID" runat="server" Value='<%# Bind("ISO_ID") %>' />
                                                    </ItemTemplate>  
                                                </asp:TemplateField>  
                                                  <asp:TemplateField HeaderText="总焊口" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="IsoInfoCount" runat="server" Text='<%# Bind("IsoInfoCount") %>'></asp:Label>
                                                    </ItemTemplate>  
                                                </asp:TemplateField>                                                           
                                                
                                                 <asp:TemplateField HeaderText="完成总焊口" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="IsoInfoCountT" runat="server" Text='<%# Bind("IsoInfoCountT") %>'></asp:Label>
                                                    </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="合格数" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CountS" runat="server" Text='<%# Bind("CountS") %>'></asp:Label>
                                                    </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="不合格数" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="CountU" runat="server" Text='<%# Bind("CountU") %>'></asp:Label>
                                                    </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="应检测比例" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="NDTR_Name" runat="server" Text='<%# Bind("NDTR_Name") %>'></asp:Label>
                                                        <asp:HiddenField ID="NDTR_Rate" runat="server" Value='<%# Bind("NDTR_Rate") %>' />
                                                    </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="实际检测比例" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Ratio" runat="server" Text='<%# Bind("Ratio") %>'></asp:Label>
                                                        <asp:HiddenField ID="RatioC" runat="server" Value='<%# Bind("RatioC") %>' />
                                                    </ItemTemplate>  
                                                </asp:TemplateField>           
                                            </Columns>
                                            <HeaderStyle CssClass="GridBgColr" />
                                            <RowStyle CssClass="GridRow" />
                                        </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                                <tr style="height:25px">
                                        <td style="width:4%" align="left">
                                            <asp:Label ID="lblv" runat="server" BackColor="Cyan" style="width:100%">&nbsp;&nbsp;</asp:Label>
                                        </td>
                                        <td style="width:8%" align="left">
                                            <asp:Label ID="Label31" runat="server" style="width:100%">未焊完</asp:Label>
                                        </td>
                                        <td style="width:4%" align="left">
                                            <asp:Label ID="lab1" runat="server" style="width:100%">0</asp:Label>
                                        </td>
                                         
                                        <td style="width:4%" align="left">
                                        <asp:Label ID="Label35" runat="server" BackColor="Yellow" style="width:100%">&nbsp;&nbsp;</asp:Label>
                                        </td>
                                        <td style="width:20%" align="left">
                                        <asp:Label ID="Label36" runat="server" style="width:100%">已焊完，未达检测比例</asp:Label>
                                        </td>
                                        <td style="width:4%" align="left">
                                        <asp:Label ID="lab2" runat="server" style="width:100%">0</asp:Label>
                                        </td>     
                                        <td style="width:4%" align="left">
                                        <asp:Label ID="Label38" runat="server" BackColor="Green" style="width:100%">&nbsp;&nbsp;</asp:Label>
                                        </td>
                                        <td style="width:30%" align="left">
                                        <asp:Label ID="Label39" runat="server" style="width:100%">已焊完，已达检测比例，但有不合格</asp:Label>
                                        </td>
                                        <td style="width:4%" align="left">
                                        <asp:Label ID="lab3" runat="server" style="width:100%">0</asp:Label>
                                        </td>     
                                        <td style="width:4%" align="left">
                                        <asp:Label ID="Label41" runat="server"  BackColor="Purple" style="width:100%">&nbsp;&nbsp;</asp:Label>
                                        </td>
                                        <td style="width:8%" align="left">
                                        <asp:Label ID="Label42" runat="server" style="width:100%">已通过</asp:Label>
                                        </td>
                                        <td style="width:4%" align="left">
                                         <asp:Label ID="lab4" runat="server" style="width:100%" >0</asp:Label>
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
