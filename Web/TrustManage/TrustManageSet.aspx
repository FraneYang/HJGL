<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrustManageSet.aspx.cs" Inherits="Web.TrustManage.TrustManageSet" %>

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
                        <td align="left" valign="middle" style="width: 30%; font-size: 11pt; font-weight: bold">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;无损委托单生成
                        </td>
                        <td align="right" valign="middle" style="width: 70%; height: 35px;">                           
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/Generate.gif" ValidationGroup="Save"
                                OnClick="btnSave_Click" Style="height: 20px" />
                            <asp:ImageButton ID="btnReturn" runat="server" ImageUrl="~/Images/Return.gif" 
                            OnClientClick="window.close()" />&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table5" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                    <tr>
                        <td width="20%" align="center" height="35px">
                            <asp:Label ID="Label1" runat="server" Text="委托单号"></asp:Label>
                        </td>
                        <td width="30%" align="left" height="35px">
                            &nbsp;<asp:TextBox ID="txtCH_TrustCode" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCH_TrustCode"
                            Display="Dynamic" ErrorMessage="请输入委托单号" ForeColor="Red" SetFocusOnError="True"
                            ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                                               
                        <td width="20%" align="center" height="35px">
                            <asp:Label ID="Label6" runat="server" Text="探伤比例"></asp:Label>
                        </td>
                        <td width="30%" align="left" height="35px">
                             &nbsp;<asp:DropDownList ID="drpCH_NDTRate" runat="server" Width="80%" Height="22px">
                        </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator3" runat="server" Display="Dynamic" ErrorMessage="请选择探伤比例！"
                            ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpCH_NDTRate" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                        </td>
                    </tr>                                           
                    <tr>
                    <td  align="center" height="35px">
                        <asp:Label ID="Label7" runat="server" Text="委托单位"></asp:Label>
                    </td>
                    <td align="left" height="35px">
                        &nbsp;<asp:DropDownList ID="drpCH_TrustUnit" runat="server" Width="80%" Height="22px">
                         </asp:DropDownList>
                        <asp:CustomValidator ID="CustomValidator1" runat="server" Display="Dynamic" ErrorMessage="请选择委托单位！"
                         ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpCH_TrustUnit" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                    </td>
                    <td align="center" height="35px">
                        <asp:Label ID="Label12" runat="server" Text="检测方法"></asp:Label>
                    </td>
                    <td align="left" height="35px">
                        &nbsp;<asp:DropDownList ID="drpCH_NDTMethod" runat="server" Width="80%" Height="22px">
                        </asp:DropDownList> 
                        <asp:CustomValidator ID="CustomValidator4" runat="server" Display="Dynamic" ErrorMessage="请选择检测方法！"
                        ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpCH_NDTMethod" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                    </td>                                          
                    </tr>
                    <tr>
                    <td align="center" height="35px">
                        <asp:Label ID="Label2" runat="server" Text="委托日期"></asp:Label>
                    </td>
                    <td align="left" height="35px">
                        &nbsp;<input id="txtCH_TrustDate" runat="server" readonly="readonly" class="Wdate" style="width:80%;
                            cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCH_TrustDate"
                        Display="Dynamic" ErrorMessage="请输入委托日期" ForeColor="Red" SetFocusOnError="True"
                        ValidationGroup="Save">*</asp:RequiredFieldValidator>                                                  
                    </td>
                    <td  align="center" height="35px">
                        <asp:Label ID="Label21" runat="server" Text="检测单位"></asp:Label>
                    </td>
                    <td  align="left" height="35px">
                        &nbsp;<asp:DropDownList ID="drpCH_CheckUnit" runat="server" Width="80%" Height="22px">
                        </asp:DropDownList> 
                        <asp:CustomValidator ID="CustomValidator2" runat="server" Display="Dynamic" ErrorMessage="请选择检测单位！"
                        ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpCH_CheckUnit" ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                    </td>
                    </tr>
                    <tr>
                    <td align="center" height="35px">
                        <asp:Label ID="Label19" runat="server" Text="制单日期"></asp:Label>
                    </td>
                    <td  align="left" height="35px">
                        &nbsp;<input id="txtCH_TableDate" runat="server" class="Wdate" style="width: 80%;
                        cursor: hand" 
                        onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" /></td>
                    <td align="center" height="35px">
                        <asp:Label ID="Label22" runat="server" Text="备注"></asp:Label>
                    </td>
                    <td  align="left" height="35px">
                        &nbsp;<asp:TextBox ID="txtCH_Remark" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>                                                    
                    </td>
                    </tr>                                         
                </table>
            </td>
        </tr>
    </table>
     <asp:ValidationSummary ID="ValidationSummary1" Style="z-index: 101; left: 8px; position: absolute;
                                top: 8px" runat="server" HeaderText="请注意！" ShowMessageBox="True" ShowSummary="False"
                                ValidationGroup="Save" />
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">   
</script>
