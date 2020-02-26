<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SysMain.aspx.cs" Inherits="Web.SysManage.SysMain" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Style.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <table id="Table1" runat="server"  width="100%" cellpadding="0" cellspacing="0">
           <tr>
            <td style="width:100%; background:url('../Images/bg-1.gif')">
              <table id="tabbtn" runat="server" width="100%"  style="background:url('../Images/bg-1.gif')" cellpadding="0" cellspacing="0">
                 <tr>
                    <td align="left" valign="middle" style="width:45%; font-size:11pt; font-weight:bold">
                       <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;系统设置
                    </td>
                 </tr>
              </table>
            </td>
          </tr>
          <tr>
             <td>
                 <table id="Table2" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                    <tr>
                        <td style="width:10%"> </td>
                        <td style="width:80%">
                           <table id="Table3" runat="server" width="100%"> 
                               <tr>
                                 <td>
                                    <asp:ImageButton ID="btnRole"  runat="server" 
                                         ImageUrl="~/Images/BtnImg/Role.jpg" onclick="btnRole_Click"/>
                                 </td>
                                 <td>
                                    <asp:ImageButton ID="btnUser"  runat="server" 
                                         ImageUrl="~/Images/BtnImg/UserInfo.jpg" onclick="btnUser_Click"/>
                                 </td>
                                 <td>
                                    <asp:ImageButton ID="btnUpdatePassword"  runat="server" 
                                         ImageUrl="~/Images/BtnImg/UpdatePassword.jpg" 
                                         onclick="btnUpdatePassword_Click"/>
                                 </td>
                                 <td>
                                   
                                 </td>
                                 <td></td>
                              </tr>
                              <tr>
                                 <td>
                                    <asp:ImageButton ID="btnRolePower"  runat="server" 
                                         ImageUrl="~/Images/BtnImg/RolePower.jpg" onclick="btnRolePower_Click"/>
                                 </td>
                                 <td>
                                    <asp:ImageButton ID="btnDataBak"  runat="server" 
                                         ImageUrl="~/Images/BtnImg/DataBak.jpg" onclick="btnDataBak_Click"/>
                                 </td>
                                
                                 <td>
                                    <asp:ImageButton ID="btnLog"  runat="server" ImageUrl="~/Images/BtnImg/Log.jpg" 
                                         onclick="btnLog_Click"/>
                                 </td>
                                 <td>
                                   
                                 </td>
                              </tr>
                              <tr>
                                 <td>
                                    <asp:ImageButton ID="btnDepart"  runat="server" 
                                         ImageUrl="~/Images/BtnImg/Depart.jpg" onclick="btnDepart_Click"/>
                                 </td>
                                 <td>
                                     &nbsp;</td>
                                
                                 <td>
                                     &nbsp;</td>
                                 <td>
                                   
                                 </td>
                              </tr>
                              </table>
                         </td>
                        <td style="width:10%"> </td>
                    </tr>
                 </table>
             </td>
          </tr>
        </table>
    </form>
</body>
</html>
