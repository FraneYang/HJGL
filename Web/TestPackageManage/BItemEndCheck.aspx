<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BItemEndCheck.aspx.cs" Inherits="Web.TestPackageManage.BItemEndCheck" %>

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
                            &nbsp;B项尾工录入
                        </td>
                        <td align="right" valign="middle" style="width: 75%; height: 30px;">
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/addbutton.gif" OnClick="btnAdd_Click" />
                            <asp:ImageButton ID="ImageButton1"
                                    runat="server" Width="0" />
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" ValidationGroup="Save"
                                   OnClick="btnSave_Click" Style="height: 20px" />
                            <asp:ImageButton ID="btnCanel" runat="server" ImageUrl="~/Images/cancel.gif" 
                                   OnClick="btnCanel_Click" Style="height: 20px" />
                            <asp:ImageButton ID="btnPrint" runat="server" ImageUrl="~/Images/Print1.gif" 
                                ToolTip="打印"  />&nbsp;
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
                                    <td colspan="3" id="AddItem" runat="server">
                                        <table id="Table5" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                                            <tr>
                                                 <td align="right" height="32px"  width="10%">
                                                    <asp:Label ID="Label7" runat="server" Text="检查人"></asp:Label>
                                                </td>
                                                <td  align="left" height="32px"  width="20%">
                                                    &nbsp;<asp:TextBox ID="txtEIC_CheckMan" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                                                </td>  
                                                <td align="right" height="32px"  width="15%">
                                                    <asp:Label ID="Label5" runat="server" Text="检查日期"></asp:Label>
                                                </td>
                                                <td  align="left" height="32px"  width="20%">
                                                    &nbsp;<asp:TextBox ID="txtEIC_CheckDate" runat="server" Width="80%" CssClass="textboxStyle" 
                                                        onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" ></asp:TextBox>                                                 
                                                </td>
                                            </tr>                                            
                                            <tr>
                                             <td align="right" height="32px">
                                                    <asp:Label ID="Label1" runat="server" Text="处理人"></asp:Label>
                                                </td>
                                                <td  align="left" height="32px">
                                                    &nbsp;<asp:TextBox ID="txtEIC_DealMan" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                                                </td>
                                                <td align="right" height="32px">
                                                    <asp:Label ID="Label4" runat="server" Text="处理日期"></asp:Label>
                                                </td>
                                                <td  align="left" height="32px">
                                                    &nbsp;<asp:TextBox ID="txtEIC_DealDate" runat="server" Width="80%" CssClass="textboxStyle" 
                                                        onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" ></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                               <td align="right" height="32px"  width="15%">
                                                    <asp:Label ID="Label8" runat="server" Text="B项内容"></asp:Label>
                                                </td>
                                                <td  align="left" height="32px" width="20%" colspan="3">
                                                    &nbsp;<asp:TextBox ID="txtEIC_Remark" runat="server" CssClass="textboxStyle" Width="80%"></asp:TextBox>                                                    
                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEIC_Remark"
                                                        Display="Dynamic" ErrorMessage="请输入B项内容" ForeColor="Red" SetFocusOnError="True"
                                                        ValidationGroup="Save">*</asp:RequiredFieldValidator>
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
                                        <asp:GridView ID="gvBItemEndCheck" runat="server" AllowSorting="True" PageSize="500"
                                            AutoGenerateColumns="False" HorizontalAlign="Justify" Width="100%" OnRowCommand="gvBItemEndCheck_RowCommand"
                                            AlternatingRowStyle-CssClass="GridBgColr">
                                            <AlternatingRowStyle CssClass="GridBgColr" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="序号">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" HorizontalAlign="Center" />
                                                </asp:TemplateField>                                                
                                               <asp:TemplateField HeaderText="B项内容">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbRemark" runat="server" CommandArgument='<%# Bind("EIC_ID") %>'
                                                        CssClass="ItemLink" Text='<%# Bind("EIC_Remark") %>' 
                                                        CommandName="RemarkLink"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="30%" />
                                                 </asp:TemplateField>                                                          
                                                <asp:BoundField DataField="EIC_CheckMan" HeaderText="检查人">
                                                    <ItemStyle Width="12%" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="EIC_CheckDate" HeaderText="检查日期"  DataFormatString="{0:yyyy-MM-dd}">
                                                  <ItemStyle Width="12%" />
                                                  </asp:BoundField>
                                                <asp:BoundField DataField="EIC_DealMan" HeaderText="处理人">
                                                    <ItemStyle Width="12%" />
                                                </asp:BoundField>                                                
                                                <asp:BoundField DataField="EIC_DealDate" HeaderText="处理日期" DataFormatString="{0:yyyy-MM-dd}">
                                                    <ItemStyle Width="12%" />
                                                </asp:BoundField> 
                                                <asp:TemplateField HeaderText="删除">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="del" ToolTip="删除"
                                                            ImageUrl="~/Images/DeleteBtn.gif" CommandArgument='<%# Bind("EIC_ID") %>' OnClientClick="return confirm(&quot;确定要删除此条信息吗？&quot;);"/>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="10%" />
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
    $("#div2").height(height - 200);
</script>
