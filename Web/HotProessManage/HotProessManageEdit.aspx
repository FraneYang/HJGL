<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HotProessManageEdit.aspx.cs" Inherits="Web.HotProessManage.HotProessManageEdit" %>

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
            var result = window.showModalDialog("ShowHotProessSearch.aspx?rnd=" + (new Date()).getTime() + "&unitId=" + unitId + "&installationId=" + installationId, "", "status=no;dialogWidth=900px;dialogHeight=478px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != null && result != "") {
                document.getElementById("hdSelectList").value = result;
                document.getElementById("imgBtnGetSearch").click();
            }
        }

        function CommonPrint(reportId, replaceParameter, varValue) {
            var result = window.showModalDialog("../ReportPrint/ExReportPrint.aspx?reportId=" + reportId + "&replaceParameter=" + replaceParameter + "&varValue=" + escape(varValue), "", "status=no;dialogWidth=900px;dialogHeight=600px;menu=no;resizeable=no;scroll=no;center=yes;edge=raise;location=no");
            if (result != "") {
                document.getElementById("ImageButton1").click();
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
                            &nbsp;热处理录入
                        </td>
                        <td align="right" valign="middle" style="width: 75%; height: 30px;">
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/addbutton.gif" OnClick="btnAdd_Click" />
                            <asp:ImageButton ID="btnDelete" runat="server" 
                                ImageUrl="~/Images/deletebutton.gif" onclick="btnDelete_Click" OnClientClick="return confirm(&quot;确定要删除此条信息吗？&quot;);"/>
                             <asp:DropDownList ID="drpSelectPrint" runat="server" Width="150px" Height="21px" ToolTip="选择打印"></asp:DropDownList>  
                             <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/Print1.gif" 
                                ToolTip="打印" onclick="btnPrint_Click"  />
                                <asp:ImageButton ID="ImageButton1"  runat="server" Width="0" />&nbsp;
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
                                                    &nbsp;热处理录入
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
                                                 <td align="right" height="25px"  width="20%">
                                                    <asp:Label ID="Label7" runat="server" Text="单位"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px"  width="30%">
                                                    &nbsp;<asp:DropDownList 
                                                        ID="drpUnit" runat="server" Width="80%" Height="22px" 
                                                        onselectedindexchanged="drpUnit_SelectedIndexChanged" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:CustomValidator ID="CustomValidator1" runat="server" Display="Dynamic" ErrorMessage="请选择单位！"
                                                       ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpUnit" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                                                </td>  
                                                <td align="right" height="25px"  width="20%">
                                                    <asp:Label ID="Label5" runat="server" Text="装置名称"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px"  width="30%">
                                                    &nbsp;<asp:DropDownList ID="drpInstallationId" runat="server" Width="80%" CssClass="textboxStyle"></asp:DropDownList>
                                                     <asp:CustomValidator ID="CustomValidator5" runat="server" Display="Dynamic" ErrorMessage="请选择装置名称！"
                                                        ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpInstallationId" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                                                </td>
                                               
                                            </tr>                                            
                                            <tr>
                                             <td align="right" height="25px">
                                                    <asp:Label ID="Label1" runat="server" Text="热处理报告号"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtHotProessNo" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtHotProessNo"
                                                        Display="Dynamic" ErrorMessage="请输入热处理报告号" ForeColor="Red" SetFocusOnError="True"
                                                        ValidationGroup="Save">*</asp:RequiredFieldValidator>
                                                </td>
                                                <td align="right" height="25px"  >
                                                    <asp:Label ID="Label8" runat="server" Text="热处理日期"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px" >
                                                    &nbsp;<asp:TextBox CssClass="textboxStyle" id="txtProessDate" runat="server"  style="width: 80%;
                                                        cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" ></asp:TextBox>
                                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtProessDate"
                                                        Display="Dynamic" ErrorMessage="请输入热处理日期" ForeColor="Red" SetFocusOnError="True"
                                                        ValidationGroup="Save">*</asp:RequiredFieldValidator>                                                     
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label4" runat="server" Text="热处理方法"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtProessMethod" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                                                </td>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label6" runat="server" Text="热处理设备"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtProessEquipment" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                               <td align="right" height="25px">
                                                    <asp:Label ID="Label10" runat="server" Text="制单人"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:DropDownList ID="drpTabler" runat="server" Width="80%" Height="22px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td align="right" height="25px">
                                                    <asp:Label ID="Label2" runat="server" Text="备注"></asp:Label>
                                                </td>
                                                <td  align="left" height="25px">
                                                    &nbsp;<asp:TextBox ID="txtRemark" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>
                                                </td>
                                              </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table id="Table6" width="100%" cellpadding="0" cellspacing="0" runat="server">                                
                                <tr style="background: url('../Images/bg-1.gif');height:25px">
                                   <td align="left"  width="100%" valign="middle">
                                       <asp:Label ID="Label3" runat="server" Text="测点数量" Height="18px" valign="middle"></asp:Label>&nbsp;                                  
                                        <asp:DropDownList ID="drpPointCount" runat="server" CssClass="textboxStyle" Width="10%">
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td  width="100%">
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
                                                    <ItemStyle Width="5%" HorizontalAlign="Center" />
                                                </asp:TemplateField> 
                                                 <asp:TemplateField HeaderText="管线编号" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ISO_IsoNo" runat="server" Text='<%# Bind("ISO_IsoNo") %>' Width="90%" CssClass="textboxnoneborder"></asp:Label>                                                        
                                                    </ItemTemplate>  
                                                </asp:TemplateField>                                              
                                                <asp:TemplateField HeaderText="焊口编号" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="JOT_JointNo" runat="server" Text='<%# Bind("JOT_JointNo") %>'></asp:Label>
                                                         <asp:HiddenField ID="hdJOT_ID" runat="server" Value='<%# Bind("JOT_ID") %>' />
                                                    </ItemTemplate>  
                                                </asp:TemplateField>    
                                                  <asp:TemplateField HeaderText="材质" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="STE_Name" runat="server" Text='<%# Bind("STE_Name") %>' Width="90%" CssClass="textboxnoneborder"></asp:Label>                                                        
                                                    </ItemTemplate>  
                                                </asp:TemplateField>
                                              <asp:TemplateField HeaderText="规格" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ISO_Specification" runat="server" Text='<%# Bind("ISO_Specification") %>' Width="90%" CssClass="textboxnoneborder"></asp:Label>                                                        
                                                    </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="测温点编号" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PointCount" runat="server" Text='<%# Bind("PointCount") %>' Width="90%" CssClass="textboxnoneborder"></asp:Label>                                                        
                                                    </ItemTemplate>  
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="热处理温度℃(要求)" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="RequiredT" runat="server" Text='<%# Bind("RequiredT") %>' Width="90%" CssClass="textboxnoneborder"></asp:TextBox>                                                        
                                                    </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="热处理温度℃(实际)" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="ActualT" runat="server" Text='<%# Bind("ActualT") %>' Width="90%" CssClass="textboxnoneborder"></asp:TextBox>                                                        
                                                    </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="恒温时间h（要求）" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="RequestTime" runat="server" Text='<%# Bind("RequestTime") %>' Width="90%" CssClass="textboxnoneborder"></asp:TextBox>                                                        
                                                    </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="恒温时间h（实际）" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="ActualTime" runat="server" Text='<%# Bind("ActualTime") %>' Width="90%" CssClass="textboxnoneborder"></asp:TextBox>                                                        
                                                    </ItemTemplate>  
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="记录曲线图编号" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="RecordChartNo" runat="server" Text='<%# Bind("RecordChartNo") %>' Width="90%" CssClass="textboxnoneborder"></asp:TextBox>                                                        
                                                    </ItemTemplate>  
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="硬度检验报告编号" HeaderStyle-Width="6%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="HardnessReportNo" runat="server" Text='<%# Bind("HardnessReportNo") %>' Width="90%" CssClass="textboxnoneborder"></asp:TextBox>                                                        
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
    $("#div2").height(height - 270);
</script>
