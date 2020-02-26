<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ErrLogInfoItem.aspx.cs" Inherits="Web.SysManage.ErrLogInfoItem" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../Styles/Style.css" type="text/css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">      
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
                        <td align="left" valign="middle" style="width: 45%; font-size: 11pt; font-weight: bold">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;错误信息详细查看
                        </td>
                        <td align="right" valign="middle" style="width: 55%; height: 30px;">                                                  
                            <asp:ImageButton ID="btnReturn" runat="server" ImageUrl="~/Images/Return.gif" OnClientClick="window.close()"  />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
               <table id="Table2" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                        <tr >
                         <td align="left" class="tdd">
                            <asp:TextBox ID="txtErr" runat="server" CssClass="textboxnoneborder" Width="98%"
                            TextMode="MultiLine" style="word-wrap:break-word;height:420px;word-break:break-all;"></asp:TextBox>                           
                        </td>
                      </tr>
                    </table>
              </td>
           </tr>
         </table>    
    </form>
</body>
</html>
