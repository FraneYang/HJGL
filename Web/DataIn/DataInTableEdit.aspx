<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataInTableEdit.aspx.cs"
    Inherits="Web.DataIn.DataInTableEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>编辑导入信息</title>
    <base target="_self" />
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script language="JavaScript" type="text/javascript"> 
　　        function WindowClose(result) {
            window.returnValue = result;
            alert("保存成功！");
            window.close();
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
                            &nbsp;编辑导入信息
                        </td>
                        <td align="right" valign="middle" style="width: 55%; height: 35px;">
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" ValidationGroup="Save"
                                OnClick="btnSave_Click" />&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table2" runat="server" width="100%" cellpadding="1" cellspacing="1">
                    <tr style="height: 32px">
                        <td align="right" style="width: 10%;">
                            <asp:Label ID="Label1" runat="server" Text="单位代码" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" style="width: 15%;">
                            <asp:TextBox ID="txtValue1" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtValue1"
                                Display="Dynamic" ErrorMessage="请输入单位代码" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="right" style="width: 10%;">
                            <asp:Label ID="Label2" runat="server" Text="工区编号" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" style="width: 15%;">
                            <asp:TextBox ID="txtValue2" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtValue2"
                                Display="Dynamic" ErrorMessage="请输入工区编号" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="right" style="width: 10%;">
                            <asp:Label ID="Label5" runat="server" Text="管线代号" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" style="width: 15%;">
                            <asp:TextBox ID="txtValue3" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtValue3"
                                Display="Dynamic" ErrorMessage="请输入管线代号" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="right" style="width: 10%;">
                            <asp:Label ID="Label7" runat="server" Text="焊口代号" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" style="width: 15%;">
                            <asp:TextBox ID="txtValue4" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtValue4"
                                Display="Dynamic" ErrorMessage="请输入焊口代号" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>  
                    <tr style="height: 32px">
                        <td align="right" >
                            <asp:Label ID="Label3" runat="server" Text="材质1代号" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue5" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtValue5"
                                Display="Dynamic" ErrorMessage="请输入材质1代号" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="right" >
                            <asp:Label ID="Label4" runat="server" Text="材质2代号"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue6" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="50"></asp:TextBox>                           
                        </td>
                        <td align="right" >
                            <asp:Label ID="Label6" runat="server" Text="探伤比例代号" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue7" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtValue7"
                                Display="Dynamic" ErrorMessage="请输入探伤比例代号" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="right" >
                            <asp:Label ID="Label8" runat="server" Text="焊缝类型代号" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue8" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtValue8"
                                Display="Dynamic" ErrorMessage="请输入焊缝类型代号" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr style="height: 32px">
                        <td align="right" >
                            <asp:Label ID="Label9" runat="server" Text="焊接区域" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue9" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="2"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtValue9"
                                Display="Dynamic" ErrorMessage="请输入焊接区域" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="right" >
                            <asp:Label ID="Label10" runat="server" Text="焊口属性"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue10" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="2"></asp:TextBox>                           
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtValue10"
                                Display="Dynamic" ErrorMessage="请输入焊口属性" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="right" >
                            <asp:Label ID="Label11" runat="server" Text="达因数" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue11" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtValue11"
                                Display="Dynamic" ErrorMessage="请输入达因数" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                    ControlToValidate="txtValue11" Display="Dynamic" ErrorMessage="&quot;达因数最多4位小数的数字！&quot;"
                                    ForeColor="Red" ValidationExpression="^0$|^[1-9]\d*$|^[1-9]\d*\.\d{1,4}$|^0\.\d{1,4}$"
                                    ValidationGroup="Save">*</asp:RegularExpressionValidator>
                        </td>
                        <td align="right" >
                            <asp:Label ID="Label12" runat="server" Text="规格（mm)" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue12" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="100"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtValue12"
                                Display="Dynamic" ErrorMessage="请输入规格（mm)" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                    </tr>    
                    <tr style="height: 32px">
                         <td align="right" >
                            <asp:Label ID="Label15" runat="server" Text="壁厚" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue13" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="50"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtValue13"
                                Display="Dynamic" ErrorMessage="请输入壁厚" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>                            
                        </td>
                        <td align="right" >
                            <asp:Label ID="Label13" runat="server" Text="焊接方法代码" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue14" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="20"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtValue14"
                                Display="Dynamic" ErrorMessage="请输入焊接方法代码" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="right" >
                            <asp:Label ID="Label14" runat="server" Text="试验压力"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue15" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="20"></asp:TextBox>                                                      
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                    ControlToValidate="txtValue15" Display="Dynamic" ErrorMessage="&quot;试验压力最多4位小数的数字！&quot;"
                                    ForeColor="Red" ValidationExpression="^0$|^[1-9]\d*$|^[1-9]\d*\.\d{1,4}$|^0\.\d{1,4}$"
                                    ValidationGroup="Save">*</asp:RegularExpressionValidator>
                        </td>
                        <td align="right" >
                            <asp:Label ID="Label16" runat="server" Text="焊条代号"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue16" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="20"></asp:TextBox>                           
                        </td>
                    </tr>  
                    <tr style="height: 32px">   
                         <td align="right" >
                            <asp:Label ID="Label20" runat="server" Text="焊丝代号"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue17" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="20"></asp:TextBox>                           
                        </td>
                         <td align="right" >
                            <asp:Label ID="Label17" runat="server" Text="介质代号" ></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue18" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="50"></asp:TextBox>                                                  
                        </td>
                        <td align="right" >
                            <asp:Label ID="Label18" runat="server" Text="单线图号"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue19" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="50"></asp:TextBox>                          
                        </td>
                        <td align="right" >
                            <asp:Label ID="Label19" runat="server" Text="设计压力"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue20" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="20"></asp:TextBox>                                                      
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                    ControlToValidate="txtValue20" Display="Dynamic" ErrorMessage="&quot;设计压力最多4位小数的数字！&quot;"
                                    ForeColor="Red" ValidationExpression="^0$|^[1-9]\d*$|^[1-9]\d*\.\d{1,4}$|^0\.\d{1,4}$"
                                    ValidationGroup="Save">*</asp:RegularExpressionValidator>
                        </td>
                    </tr>   
                    <tr style="height: 32px">  
                        <td align="right" >
                            <asp:Label ID="Label24" runat="server" Text="设计温度"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue21" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="20"></asp:TextBox>                                                      
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                    ControlToValidate="txtValue21" Display="Dynamic" ErrorMessage="&quot;设计温度最多4位小数的数字！&quot;"
                                    ForeColor="Red" ValidationExpression="^0$|^[1-9]\d*$|^[1-9]\d*\.\d{1,4}$|^0\.\d{1,4}$"
                                    ValidationGroup="Save">*</asp:RegularExpressionValidator>
                        </td> 
                         <td align="right" >
                            <asp:Label ID="Label21" runat="server" Text="坡口代号" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue22" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="20"></asp:TextBox>                           
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtValue22"
                                Display="Dynamic" ErrorMessage="请输入坡口代号" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                         <td align="right" >
                            <asp:Label ID="Label22" runat="server" Text="管线等级代号" ></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue23" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="20"></asp:TextBox>                                                  
                        </td>
                        <td align="right" >
                            <asp:Label ID="Label23" runat="server" Text="组件一代号"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue24" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="20"></asp:TextBox>                          
                        </td>
                    </tr> 
                    <tr style="height: 32px">  
                        <td align="right" >
                            <asp:Label ID="Label25" runat="server" Text="组件二代号"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue25" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="20"></asp:TextBox>                          
                        </td>
                        <td align="right" >
                            <asp:Label ID="Label26" runat="server" Text="炉批号一"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue26" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="50"></asp:TextBox>                          
                        </td>
                        <td align="right" >
                            <asp:Label ID="Label27" runat="server" Text="炉批号二"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue27" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="50"></asp:TextBox>                          
                        </td>
                        <td align="right" >
                            <asp:Label ID="Label28" runat="server" Text="所属管段"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue28" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="50"></asp:TextBox>                          
                        </td>
                    </tr>                                       
                    <tr style="height: 32px">  
                        <td align="right" >
                            <asp:Label ID="Label29" runat="server" Text="预热温度"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue29" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="20"></asp:TextBox>                          
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server"
                                    ControlToValidate="txtValue29" Display="Dynamic" ErrorMessage="&quot;预热温度最多4位小数的数字！&quot;"
                                    ForeColor="Red" ValidationExpression="^0$|^[1-9]\d*$|^[1-9]\d*\.\d{1,4}$|^0\.\d{1,4}$"
                                    ValidationGroup="Save">*</asp:RegularExpressionValidator>
                        </td>
                        <td align="right" >
                            <asp:Label ID="Label30" runat="server" Text="是否需热处理"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue30" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="1"></asp:TextBox>                          
                        </td>
                        <td align="right" >
                            <asp:Label ID="Label31" runat="server" Text="热处理编号"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue31" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="50"></asp:TextBox>                          
                        </td>
                        <td align="right" >
                            <asp:Label ID="Label32" runat="server" Text="焊接位置" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue32" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="2"></asp:TextBox>                          
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtValue32"
                                Display="Dynamic" ErrorMessage="请输入焊接位置" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                    </tr> 
                    <tr style="height: 32px">  
                        <td align="right" >
                            <asp:Label ID="Label33" runat="server" Text="外径"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue33" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="20"></asp:TextBox>                          
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server"
                                    ControlToValidate="txtValue33" Display="Dynamic" ErrorMessage="&quot;外径最多4位小数的数字！&quot;"
                                    ForeColor="Red" ValidationExpression="^0$|^[1-9]\d*$|^[1-9]\d*\.\d{1,4}$|^0\.\d{1,4}$"
                                    ValidationGroup="Save">*</asp:RegularExpressionValidator>
                        </td>
                        <td align="right" >
                            <asp:Label ID="Label34" runat="server" Text="硬度检测比例"></asp:Label>
                        </td>
                        <td align="left" >
                            <asp:TextBox ID="txtValue34" runat="server" Width="90%" CssClass="textboxStyle" MaxLength="20"></asp:TextBox>                          
                        </td>
                        <td align="right" colspan="2">
                            <asp:CheckBox ID="ckAll" runat="server" Checked="true" Text="是否批量修改" Font-Bold="true" ToolTip="批量更新修改数据，若修改焊口号则要取消批量修改" />
                        </td>
                    </tr>
                    <tr style="height:96px">
                         <td align="right"> 
                             <asp:Label ID="Label35" runat="server" Text="错误信息"></asp:Label>
                         </td>
                         <td colspan="7" align="left">
                            <asp:TextBox  ID="lbCout" runat="server" Height="96px" TextMode="MultiLine" ReadOnly="true" Width="98%" ForeColor="Red"></asp:TextBox>
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
