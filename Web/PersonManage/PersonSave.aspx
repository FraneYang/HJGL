<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonSave.aspx.cs" Inherits="Web.PersonManage.PersonSave" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="../Styles/Style.css" type="text/css" rel="stylesheet" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
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
                        <td align="left" valign="middle" style="width: 45%; font-size: 11pt; font-weight: bold">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;人员管理
                        </td>
                        <td align="right" valign="middle" style="width: 55%; height: 30px;">                                                  
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" ValidationGroup="Save"
                                OnClick="btnSave_Click" />
                            <asp:ImageButton ID="btncancel" runat="server" ImageUrl="~/Images/cancel.gif" OnClientClick="window.close()"  />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divEdit" runat="server" >
                    <table id="Table2" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                        <tr style="height: 32px">
                            <td align="right" width="8%">
                                <asp:Label ID="Label3" runat="server" Text="单位"></asp:Label>&nbsp;
                            </td>
                            <td align="left" width="17%">
                                <asp:DropDownList ID="drpUnit" runat="server" Height="22px" Width="80%">
                                </asp:DropDownList>
                                <asp:CustomValidator ID="CustomValidator2" runat="server" Display="Dynamic" ErrorMessage="&quot;请选择单位！&quot;"
                                ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpUnit" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                            </td>
                            <td align="right" width="8%">
                                <asp:Label ID="Label1" runat="server" Text="班组"></asp:Label>&nbsp;
                            </td>
                            <td width="17%" align="left">
                               <asp:DropDownList ID="drpEducation" runat="server" Height="22px" Width="80%">
                                </asp:DropDownList>                               
                            </td>
                            </tr>
                            <tr style="height: 32px">
                            
                            <td align="right" width="8%">
                                <asp:Label ID="Label2" runat="server" Text="焊工姓名"></asp:Label>&nbsp;
                            </td>
                            <td align="left" width="17%">
                                <asp:TextBox ID="txtName" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName"
                                    Display="Dynamic" ErrorMessage="&quot;请输入焊工姓名&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>
                            </td>
                             <td align="right" width="8%">
                                <asp:Label ID="Label9" runat="server" Text="焊工代号"></asp:Label>&nbsp;
                            </td>
                            <td width="17%" align="left">
                                <asp:TextBox ID="txtCode" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>  
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCode"
                                    Display="Dynamic" ErrorMessage="&quot;请输入焊工代号&quot;" ForeColor="Red" ValidationGroup="Save">*</asp:RequiredFieldValidator>                             
                            </td>
                            </tr>
                            
                        
                        <tr style="height: 32px">
                             <td align="right">
                                <asp:Label ID="Label10" runat="server" Text="出生日期"></asp:Label>&nbsp;
                            </td>
                            
                            <td align="left">
                                <asp:TextBox ID="txtBirthday" runat="server" Width="80%" CssClass="textboxStyle" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})"></asp:TextBox>
                                </td>
                          <td align="right">
                                <asp:Label ID="Label4" runat="server" Text="性别"></asp:Label>&nbsp;
                            </td>
                            <td align="left">
                            <asp:RadioButtonList ID="drpSex" runat="server"  RepeatDirection="Horizontal" Width="50%">
                                <asp:ListItem Selected="True" Value="1">男</asp:ListItem>
                                <asp:ListItem  Value="2">女</asp:ListItem>
                            </asp:RadioButtonList>
                            </td>
                            </tr>
                        <tr style="height: 32px">
                             <td align="right" >
                                <asp:Label ID="Label11" runat="server" Text="上岗证号"></asp:Label>&nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtWorkCode" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                           <td align="right" >
                                <asp:Label ID="Label5" runat="server" Text="有效期限"></asp:Label>&nbsp;
                            </td>
                             <td align="left">
                                <asp:TextBox ID="txtLimitDate" runat="server" Width="80%" CssClass="textboxStyle" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})"></asp:TextBox>
                             </td>
                        </tr>
                        <tr style="height: 32px"> 
                             <td align="right">
                                <asp:Label ID="Label12" runat="server" Text="焊工等级"></asp:Label>&nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtClass" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            </td>
                            <td align="right">
                                <asp:Label ID="Label13" runat="server" Text="是否在岗"></asp:Label>&nbsp;
                            </td>
                            <td align="left">
                                <asp:CheckBox ID="drpIfOnGuard" runat="server" />
                            </td>
                        </tr>
                         <tr style="height: 32px">
                              <td align="right">
                                <asp:Label ID="Label7" runat="server" Text="身份证号"></asp:Label>&nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtIdentityCard" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtIdentityCard"
                                Display="Dynamic" ErrorMessage="&quot;身份证号码只能为15位或18位！&quot;" ForeColor="Red"
                                ValidationExpression="\d{17}[\d|X|x]|\d{15}" ValidationGroup="Save">*</asp:RegularExpressionValidator>
                            </td>
                            <td align="right" >
                                <asp:Label ID="Label6" runat="server" Text="备注"></asp:Label>&nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtRemark" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>                               
                            </td>
                        </tr>
                        <tr style="height: 32px"> <td align="right" >
                                <asp:Label ID="Label8" runat="server" Text="合格项目代号"></asp:Label>&nbsp;
                            </td>
                            <td align="left" colspan="3">
                                <asp:TextBox ID="txtSE_EquipmentID" runat="server" Width="92%" TextMode="MultiLine" Rows="3" CssClass="textboxStyle"></asp:TextBox>                               
                            </td>
                        </tr>
                    </table>
                </div>
              </td>
           </tr>
         </table>
    <asp:ValidationSummary ID="ValidationSummary1" Style="z-index: 101; left: 8px; position: absolute;
        top: 8px" runat="server" ShowMessageBox="True" ShowSummary="False" 
        ValidationGroup="Save" />
    </form>
</body>
</html>
