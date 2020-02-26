<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdatePassword.aspx.cs" Inherits="Web.SysManage.UpdatePassword" %>

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
                            &nbsp;修改密码
                    </td>
                      <td align="right" valign="middle" style="width:55%; height:30px;">
                         <asp:ImageButton ID="btnSave"  runat="server" ImageUrl="~/Images/savebutton.gif" 
                            onclick="btnSave_Click" ValidationGroup="Save" />
                         <asp:ImageButton ID="btncancel"  runat="server" ImageUrl="~/Images/cancel.gif" 
                            onclick="btncancel_Click" />&nbsp;
                      </td>
                 </tr>
              </table>
            </td>
          </tr>
          <tr>
             <td>
                <table id="Table2" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                   <tr style="height:32px">
                      <td align="right" style="width:10%">
                         <asp:Label ID="Label4" runat="server" Text="人员姓名"></asp:Label>&nbsp;
                      </td>
                      <td align="left" style="width:30%">
                         <asp:DropDownList ID="drpName" runat="server" Height="22px" Width="150px">
                        </asp:DropDownList>
                      </td>
                      <td style="width:60%"></td>
                   </tr>
                   <tr id="trEnabled" style="height:32px">
                      <td align="right" style="width:10%">
                         <asp:Label ID="Label1" runat="server" Text="旧密码"></asp:Label>&nbsp;
                      </td>
                      <td align="left" style="width:30%">
                         <asp:TextBox ID="txtOldPwd" runat="server" Width="180px" CssClass="textboxStyle" 
                              ValidationGroup="Save" TextMode="Password"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="TDDisplaynameValidator1" runat="server" ControlToValidate="txtOldPwd" ValidationGroup="Save"
                                            Display="Dynamic" ErrorMessage="&quot;请输入旧密码！&quot;" ForeColor="Red" >*</asp:RequiredFieldValidator>
                      </td>
                      <td style="width:60%"></td>
                   </tr>

                    <tr style="height:32px">
                      <td align="right" style="width:10%">
                         <asp:Label ID="Label3" runat="server" Text="新密码"></asp:Label>&nbsp;
                      </td>
                      <td align="left" style="width:30%">
                         <asp:TextBox ID="txtNewPwd" runat="server" Width="180px" CssClass="textboxStyle" 
                              TextMode="Password"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                              ControlToValidate="txtNewPwd" Display="Dynamic" 
                              ErrorMessage="&quot;请输入新密码！&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                      </td>
                       
                      <td style="width:60%">
                      </td>
                   </tr>

                    <tr style="height:32px">
                      <td align="right" style="width:10%">
                         <asp:Label ID="Label2" runat="server" Text="确认密码"></asp:Label>&nbsp;
                      </td>
                      <td align="left" style="width:30%">
                         <asp:TextBox ID="txtConfirm" runat="server" Width="180px" 
                              CssClass="textboxStyle" TextMode="Password"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                              ControlToValidate="txtConfirm" Display="Dynamic" 
                              ErrorMessage="&quot;请输入确认密码！&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                      </td>
                       
                      <td style="width:60%">
                      </td>
                   </tr>
                </table>
             </td>
          </tr>
        </table>

        <asp:validationsummary id="ValidationSummary1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px"
				runat="server" ValidationGroup="Save" HeaderText="请注意！" ShowSummary="False" ShowMessageBox="True"></asp:validationsummary>
    </form>
</body>
</html>
