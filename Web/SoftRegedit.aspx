<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SoftRegedit.aspx.cs" Inherits="Web.SoftRegedit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Styles/Style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <table  width="100%" cellpadding="0" cellspacing="0">
      <tr style="height:240px"></tr>
      <tr>
         <td align="center">
             <table width="40%" cellpadding="0" cellspacing="0" border="1">
                  <tr>
                    <td style="width:100%">
                      <table id="tabbtn" width="100%"  style="background:url('Images/bg-1.gif')" cellpadding="0" cellspacing="0">
                         <tr>
                            <td align="left" valign="middle" style="width:45%; font-size:11pt; font-weight:bold">
                               <asp:Image ImageUrl="Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                                    &nbsp;软件注册信息
                            </td>
                              <td align="right" valign="middle" style="width:55%; height:30px;">
                                 <asp:ImageButton ID="btnSave"  runat="server" ImageUrl="~/Images/RegeditBtn.jpg" 
                                     ValidationGroup="Save" onclick="btnSave_Click" />
                                 <asp:ImageButton ID="btncancel"  runat="server" ImageUrl="~/Images/Return.gif" onclick="btncancel_Click" 
                                    />
                              </td>
                         </tr>
                      </table>
                    </td>
                  </tr>
                  <tr id="NoRegedit" runat="server">
                     <td align="center" style="width:100%">
                        <table id="Table2" width="100%" cellpadding="1" cellspacing="1" class="table">
                           <tr style="height:15px"></tr>
                           <tr style="height:32px">
                              <td align="right" style="width:20%">
                                 <asp:Label ID="Label1" runat="server" Text="序列号："></asp:Label>&nbsp;
                              </td>
                              <td align="left" style="width:80%">
                                 <asp:TextBox ID="txtSerialId" runat="server" Width="80%" 
                                      CssClass="textboxStyle"></asp:TextBox>
                              </td>
                           </tr>
                           <tr style="height:10px"></tr>
                            <tr style="height:32px">
                              <td align="right" style="width:20%">
                                 <asp:Label ID="Label4" runat="server" Text="注册码："></asp:Label>&nbsp;
                                 </td>
                              <td align="left" style="width:80%">
                              <asp:TextBox ID="txtRegisterCode" runat="server" Width="80%" 
                                      CssClass="textboxStyle"></asp:TextBox>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                      ControlToValidate="txtRegisterCode" Display="Dynamic" 
                                      ErrorMessage="&quot;请输入注册码！&quot;" ForeColor="Red" 
                                      ValidationGroup="Save">*</asp:RequiredFieldValidator>
                              </td>
                           </tr>
                            <tr style="height:10px"></tr>
                           <tr style="height:30px">
                              <td colspan="2" align="center" style="font-size: 12pt; font-weight: bold; color: #0000FF">此软件未注册，请及时注册！</td>
                           </tr>
                           <tr style="height:20px"></tr>
                        </table>
                     </td>
                  </tr>
                   <tr id="Regedited" runat="server">
                     <td align="center" style="width:100%">
                        <table id="Table1" width="100%" cellpadding="1" cellspacing="1" class="table">
                           <tr style="height:15px"></tr>
                           <tr style="height:32px">
                              <td align="right" style="width:20%">
                              </td>
                              <td align="left" 
                                   style="width:80%; font-size: large; font-weight: bold; color: #0000FF;">
                                 此软件已注册
                              </td>
                           </tr>
                           <tr style="height:10px"></tr>
                            <tr style="height:32px">
                            <td style="width:20%; font-size: large; font-weight: bold; color: #0000FF;">
                              <asp:Label  runat="server" Text="注册码："></asp:Label>
                            </td>
                              <td align="left" style="width:80%; font-size: large; font-weight: bold;">
                                 <asp:Label ID="txtRegeditedCode" runat="server" Width="80%"></asp:Label>
                                 </td>
                           </tr>
                           <tr style="height:30px"></tr>
                        </table>
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
