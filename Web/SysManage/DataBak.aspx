<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataBak.aspx.cs" Inherits="Web.SysManage.DataBak" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <table id="Table1" runat="server"  width="100%" cellpadding="0" cellspacing="0">
          <tr>
            <td style="width:100%; background:url('../Images/bg-1.gif')">
              <table id="tabbtn" runat="server" width="100%"  style="background:url('../Images/bg-1.gif')" cellpadding="0" cellspacing="0">
                 <tr>
                    <td align="left" valign="middle" style="width:95%; font-size:11pt; font-weight:bold">
                       <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;数据库备份
                    </td>
                 </tr>
              </table>
            </td>
          </tr>
          <tr>
             <td style="height:10px"></td>
          </tr>
          <tr>
            <td>&nbsp;&nbsp;
                <asp:Button ID="BtnDataBak" runat="server" Text="数据库备份" 
                    onclick="BtnDataBak_Click" />&nbsp;&nbsp;
                <asp:HyperLink ID="HyperLink1" runat="server" >HyperLink</asp:HyperLink>
            </td>
          </tr>
        </table>  
    
    </form>
</body>
</html>
