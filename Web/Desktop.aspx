<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Desktop.aspx.cs" Inherits="Web.Desktop" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>显示区</title>
    <link href="Styles/Style.css" type="text/css" rel="stylesheet" />
    <script src="Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript">
        function start(name, name1, name2) {
            var speed = 50;
            name2.innerHTML = name1.innerHTML;
            function Marquee() {
                if (name2.offsetTop - name.scrollTop <= 0) {
                    name.scrollTop -= name1.offsetHeight;
                }
                else {
                    name.scrollTop++;
                }
            }
            var MyMar = setInterval(Marquee, speed);
            name.onmouseover = function () { clearInterval(MyMar); }
            name.onmouseout = function () { MyMar = setInterval(Marquee, speed); }
        }
    </script>
    </head>
<body style="background:url('/Images/bg_r.jpg')">
  <form runat="server">
    <table border="0" cellpadding="1" cellspacing="1" align="center" width="80%" style="height:100%">
        <tr>
			<td>
              <table cellpadding="1" cellspacing="1" style="width:340px;">
                  <tr style="width:340px; height:28px; background-image:url('Images/DeskTop.jpg')">
                     <td colspan="2" align="left" style="width:340px; font-weight:bold; font-size:11pt";>
                     &nbsp;&nbsp;&nbsp;&nbsp;半年内未焊焊工
                     <asp:LinkButton ID="LinkShow10" runat="server" style="margin-left:110px"  onclick="Show10_Click">显示</asp:LinkButton>
                     <asp:LinkButton ID="LinkShow11"  runat="server" style="margin-left:110px" onclick="Show10_Click" Visible="false">隐藏</asp:LinkButton>
                     </td>
                  </tr>
                  <tr style="width:340px; height:160px">
                     <td align="left" style="width:340px; font-size:11pt; padding-left:5px;">                      
                         <div id="divNotice" style="overflow: auto;">
                             <asp:DataList ID="dlReceiveFileManager2" runat="server">
                                <ItemTemplate>
                              <asp:LinkButton ID="lbtnAuditingManage" Height="24px" runat="server" CommandArgument='<%# Bind("WED_ID") %>'
                                        Text='<%# Bind("WED_Name") %>' CssClass="ItemLink"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:DataList>
                         </div>
                     </td>
                  </tr>
              </table>
            </td>
            <td>
                <table cellpadding="1" cellspacing="1" style="width:340px;">
                  <tr  style="width:340px; height:28px; background-image:url('Images/DeskTop.jpg')">
                     <td colspan="2" align="left" style="width:340px; font-weight:bold; font-size:11pt";>
                        &nbsp;&nbsp;&nbsp;&nbsp;焊工资质过期提醒
                        <asp:LinkButton ID="LinkShow20" runat="server" style="margin-left:100px"  onclick="Show20_Click">显示</asp:LinkButton>
                     <asp:LinkButton ID="LinkShow21"  runat="server" style="margin-left:100px" onclick="Show20_Click" Visible="false">隐藏</asp:LinkButton>
                         </td>
                  </tr>
                  <tr style="width:340px; height:160px">
                     <td align="left" style="width:340px; font-size:11pt; padding-left:5px;">                        
                         <div id="divAuditingManage" style="overflow: auto;">
                            <asp:DataList ID="dlAuditingManage2" runat="server">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnAuditingManage" Height="24px" runat="server" CommandArgument='<%# Bind("WED_ID") %>'
                                        Text='<%# Bind("WED_Name") %>' CssClass="ItemLink"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:DataList>
                         </div>
                     </td>
                  </tr>
              </table>
            </td>
            <td>
                <table cellpadding="1" cellspacing="1" style="width:340px;">
                  <tr  style="width:340px; height:28px; background-image:url('Images/DeskTop.jpg')">
                     <td colspan="2" align="left" style="width:340px; font-weight:bold; font-size:11pt";>
                        &nbsp;&nbsp;&nbsp;&nbsp;点口未委托焊口
                        <asp:LinkButton ID="LinkShow30" runat="server" style="margin-left:110px"  onclick="Show30_Click">显示</asp:LinkButton>
                        <asp:LinkButton ID="LinkShow31"  runat="server" style="margin-left:110px" onclick="Show30_Click" Visible="false">隐藏</asp:LinkButton>
                         </td>
                  </tr>
                  <tr style="width:340px; height:160px">
                     <td align="left" style="width:340px; font-size:11pt; padding-left:5px;">                         
                         <div id="divPointNoTrust" style="overflow: auto;">
                            <asp:DataList ID="dlPointNoTrust2" runat="server">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnPointNoTrust" Height="24px" runat="server" 
                                        Text='<%# Bind("PointNoTrust") %>' CssClass="ItemLink"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:DataList>
                         </div>
                     </td>
                  </tr>
              </table>
            </td>
		</tr>
        <tr>
			 <td>
                <table cellpadding="1" cellspacing="1" style="width:340px;">
                  <tr  style="width:340px; height:28px; background-image:url('Images/DeskTop.jpg')">
                     <td colspan="2" align="left" style="width:340px; font-weight:bold; font-size:11pt";>
                         &nbsp;&nbsp;&nbsp;&nbsp;需要进行返修的检测单
                         <asp:LinkButton ID="LinkShow40" runat="server" style="margin-left:70px"  onclick="Show40_Click">显示</asp:LinkButton>
                        <asp:LinkButton ID="LinkShow41"  runat="server" style="margin-left:70px" onclick="Show40_Click" Visible="false">隐藏</asp:LinkButton>
                         </td>
                  </tr>
                  <tr style="width:340px; height:160px">
                     <td align="left" style="width:340px; font-size:11pt; padding-left:5px;">                        
                         <div id="divTrustNoAudit" style="overflow: auto;">
                            <asp:DataList ID="dlTrustNoAudit2" runat="server">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnTrustNoAudit" Height="24px" runat="server" 
                                        Text='<%# Bind("RepairCheck") %>' CssClass="ItemLink"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:DataList>
                         </div>
                     </td>
                  </tr>
              </table>
            </td>
            <td>
                <table cellpadding="1" cellspacing="1" style="width:340px;">
                  <tr  style="width:340px; height:28px; background-image:url('Images/DeskTop.jpg')">
                     <td colspan="2" align="left" style="width:340px; font-weight:bold; font-size:11pt";>
                         &nbsp;&nbsp;&nbsp;&nbsp;未检测或未全部检测的委托
                         <asp:LinkButton ID="LinkShow50" runat="server" style="margin-left:40px"  onclick="Show50_Click">显示</asp:LinkButton>
                        <asp:LinkButton ID="LinkShow51"  runat="server" style="margin-left:40px" onclick="Show50_Click" Visible="false">隐藏</asp:LinkButton>
                         
                         </td>
                  </tr>
                  <tr style="width:340px; height:160px">
                     <td align="left" style="width:340px; font-size:11pt; padding-left:5px;">                        
                         <div id="divTrustNoCheck" style="overflow: auto;">
                            <asp:DataList ID="dlTrustNoCheck2" runat="server">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnTrustNoCheck" Height="24px" runat="server" 
                                        Text='<%# Bind("TrustNoCheck") %>' CssClass="ItemLink"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:DataList>
                         </div>
                     </td>
                  </tr>
              </table>
            </td>
        <td>
                <table cellpadding="1" cellspacing="1" style="width:340px;">
                  <tr  style="width:340px; height:28px; background-image:url('Images/DeskTop.jpg')">
                     <td colspan="2" align="left" style="width:340px; font-weight:bold; font-size:11pt";>
                         &nbsp;&nbsp;&nbsp;&nbsp;未审核的检测
                          <asp:LinkButton ID="LinkShow60" runat="server" style="margin-left:130px"  onclick="Show60_Click">显示</asp:LinkButton>
                        <asp:LinkButton ID="LinkShow61"  runat="server" style="margin-left:130px" onclick="Show60_Click" Visible="false">隐藏</asp:LinkButton>
                         </td>
                  </tr>
                  <tr style="width:340px; height:160px">
                     <td align="left" style="width:340px; font-size:11pt; padding-left:5px;">                        
                         <div id="divCheckNoAudit" style="overflow: auto;">
                            <asp:DataList ID="dlCheckNoAudit2" runat="server">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnCheckNoAudit" Height="24px" runat="server" 
                                        Text='<%# Bind("CheckNoAudit") %>' CssClass="ItemLink"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:DataList>
                         </div>
                     </td>
                  </tr>
              </table>
            </td>
		</tr>
        <tr>
           <td colspan="3" style="height:2px; width:100%; background-color:Blue"></td>
        </tr>
	</table>
   </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    var height = 720;
    $("#divNotice").height(height / 2 - 135);
    $("#divAuditingManage").height(height / 2 - 135);
    $("#divPointNoTrust").height(height / 2 - 135);
    $("#divTrustNoAudit").height(height / 2 - 135);
    $("#divTrustNoCheck").height(height / 2 - 135); 
    $("#divCheckNoAudit").height(height / 2 - 135);
</script>

