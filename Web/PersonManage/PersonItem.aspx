<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonItem.aspx.cs" Inherits="Web.PersonManage.PersonItem" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script src="../Scripts/ValidateGroupControl.js" type="text/javascript"></script>
   <%-- <script type="text/javascript">
        function ValidateGroupControl() {
            var validationGroups = "Save";
            return confirm("确定要执行保存操作吗？");
        }
    </script>--%>
</head>
<body>
    <form id="form1" runat="server">
    <table id="Table1" runat="server" width="100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 100%; background: url('../Images/bg-1.gif')">
                <table id="tabbtn" runat="server" width="100%" style="background: url('../Images/bg-1.gif')"
                    cellpadding="0" cellspacing="0">
                    <tr>
                        <td align="left" valign="middle" style="width: 20%; font-size: 11pt; font-weight: bold">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;<asp:Label ID="WelderTitle" runat="server"></asp:Label>
                        </td>
                        <td align="right" valign="middle" style="width: 80%; height: 30px;">
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" OnClick="btnSave_Click"
                                ValidationGroup="Save" />&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table9" width="100%" cellpadding="0" cellspacing="0" runat="server">
                    <tr style="background: url('../Images/bg-1.gif')">
                        <td style="width: 20%; font-size: 11pt; font-weight: bold;" align="left">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image1" runat="server" />
                            &nbsp;材质
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <div id="div1" runat="server" style="overflow: auto; height: 300px;">
                                <table id="Table8" width="1005px" cellpadding="0" cellspacing="0" runat="server">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvBS_Steel" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                HorizontalAlign="Justify" Width="100%" OnDataBound="gvBS_Steel_DataBound" 
                                                DataKeyNames="STE_ID" >
                                                <AlternatingRowStyle CssClass="GridBgColr" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="25px">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ckbBS_Steel" runat="server" AutoPostBack="True" 
                                                                          OnCheckedChanged="ckbBS_Steel_CheckedChanged"/>
                                                            <asp:Label ID="lblBS_Steel" runat="server" Text='<%# Bind("STE_ID") %>' Visible="False"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="6%" />
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="ckbAll" runat="server" Text="全选" AutoPostBack="True" OnCheckedChanged="ckbAll_CheckedChanged" />
                                                        </HeaderTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Height="25px"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="STE_Name" HeaderText="名称" HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-Height="25px">
                                                        <HeaderStyle Width="11%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="STE_Code" HeaderText="代码" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-Height="25px">
                                                        <HeaderStyle Width="11%" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="钢材类型">
                                                        <ItemTemplate>
                                                            <asp:Label ID="labSteelType" runat="server" Text='<%# ConvertSteelType(Eval("STE_SteelType")) %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="10%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="焊接壁厚(最小)">
                                                        <ItemTemplate>
                                                             <asp:TextBox ID="txtThicknessMin" runat="server" Width="80%" CssClass="textboxnoneborder"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server"
                                                              ControlToValidate="txtThicknessMin" Display="Dynamic" ErrorMessage="&quot;壁厚最多可以保留4位小数！&quot;"
                                                              ForeColor="Red" ValidationExpression="^0$|^[1-9]\d*$|^[1-9]\d*\.\d{1,4}$|^0\.\d{1,4}$"
                                                              ValidationGroup="Save">*</asp:RegularExpressionValidator>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="9%" />
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="焊接壁厚(最大)">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtThicknessMax" runat="server" Width="80%" CssClass="textboxnoneborder"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtThicknessMax"
                                                             Display="Dynamic" ErrorMessage="&quot;壁厚最多可以保留4位小数！&quot;" ForeColor="Red" ValidationExpression="^0$|^[1-9]\d*$|^[1-9]\d*\.\d{1,4}$|^0\.\d{1,4}$"
                                                             ValidationGroup="Save">*</asp:RegularExpressionValidator>
                                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="&quot;壁厚最大值必须大于最小值！&quot;"
                                                             ControlToCompare="txtThicknessMin" ControlToValidate="txtThicknessMax" Type="Double" Display="Dynamic"
                                                             ForeColor="Red" Operator="GreaterThan" ValidationGroup="Save">*</asp:CompareValidator>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="9%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="焊接尺寸(最小)">
                                                        <ItemTemplate>
                                                             <asp:TextBox ID="txtSizesMin" runat="server" Width="80%" CssClass="textboxnoneborder"></asp:TextBox>
                                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtSizesMin"
                                                                  Display="Dynamic" ErrorMessage="&quot;大小最多可以保留4位小数！&quot;" ForeColor="Red" ValidationExpression="^0$|^[1-9]\d*$|^[1-9]\d*\.\d{1,4}$|^0\.\d{1,4}$"
                                                                  ValidationGroup="Save">*</asp:RegularExpressionValidator>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="9%" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="焊接尺寸(最大)">
                                                        <ItemTemplate>
                                                              <asp:TextBox ID="txtSizesMax" runat="server" Width="80%" CssClass="textboxnoneborder"></asp:TextBox>
                                                              <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtSizesMax"
                                                                   Display="Dynamic" ErrorMessage="&quot;大小最多可以保留4位小数！&quot;" ForeColor="Red" ValidationExpression="^0$|^[1-9]\d*$|^[1-9]\d*\.\d{1,4}$|^0\.\d{1,4}$"
                                                                   ValidationGroup="Save">*</asp:RegularExpressionValidator>
                                                              <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="&quot;大小最大值必须大于最小值！&quot;"
                                                                   ControlToCompare="txtSizesMin" ControlToValidate="txtSizesMax" Type="Double" Display="Dynamic"
                                                                   ForeColor="Red" Operator="GreaterThan" ValidationGroup="Save">*</asp:CompareValidator>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="9%" />
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="STE_Remark" HeaderText="备注" HeaderStyle-Width="40%" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-Height="25px">
                                                        <HeaderStyle Width="26%" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <HeaderStyle CssClass="GridBgColr" />
                                                <RowStyle CssClass="GridRow" />
                                                <PagerTemplate>
                                                    <uc1:GridNavgator ID="GridNavgator1" runat="server" />
                                                </PagerTemplate>
                                                <PagerStyle HorizontalAlign="Left" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                    <tr style="background: url('../Images/bg-1.gif')">
                        <td align="left" style="width: 20%; font-size: 11pt; font-weight: bold;">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image2" runat="server" />
                            &nbsp;焊接方法
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <div id="div2" runat="server" style="overflow: auto; height: 180px;">
                                <table id="Table3" width="1005px" cellpadding="0" cellspacing="0" runat="server">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvWeldMethod" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                                HorizontalAlign="Justify" Width="100%" OnDataBound="gvWeldMethod_DataBound"
                                                DataKeyNames="WME_ID">
                                                <AlternatingRowStyle CssClass="GridBgColr" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="选择" ItemStyle-HorizontalAlign="Center" ItemStyle-Height="25px">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="ckbBS_WeldMethod" runat="server" />
                                                            <asp:Label ID="lblBS_WeldMethod" runat="server" Text='<%# Bind("WME_ID") %>' Visible="False"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%" />
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="ckbWeldMethodAll" runat="server" Text="全选" AutoPostBack="True"
                                                                OnCheckedChanged="ckbWeldMethodAll_CheckedChanged" />
                                                        </HeaderTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Height="25px"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="WME_Name" HeaderText="焊接方法" HeaderStyle-Width="30%" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-Height="25px">
                                                        <HeaderStyle Width="30%" />
                                                        <ItemStyle HorizontalAlign="Center" Height="25px"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="WME_Code" HeaderText="代码" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-Height="25px">
                                                        <HeaderStyle Width="20%" />
                                                        <ItemStyle HorizontalAlign="Center" Height="25px"></ItemStyle>
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="WME_Remark" HeaderText="备注" HeaderStyle-Width="40%" ItemStyle-HorizontalAlign="Center"
                                                        ItemStyle-Height="25px">
                                                        <HeaderStyle Width="40%" />
                                                        <ItemStyle HorizontalAlign="Center" Height="25px"></ItemStyle>
                                                    </asp:BoundField>
                                                </Columns>
                                                <HeaderStyle CssClass="GridBgColr" />
                                                <RowStyle CssClass="GridRow" />
                                                <PagerTemplate>
                                                    <uc1:GridNavgator ID="GridNavgator1" runat="server" />
                                                </PagerTemplate>
                                                <PagerStyle HorizontalAlign="Left" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
      <asp:ValidationSummary ID="ValidationSummary1" Style="z-index: 101; left: 8px; position: absolute;
        top: 8px; " runat="server" HeaderText="请注意！" ShowMessageBox="True"
        ShowSummary="False" ValidationGroup="Save" />
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    var height = document.getElementById("div1").offsetHeight;
    if (height > 300) {
        $("#div1").height(height);
    }

    var weldMethodheight = document.getElementById("div2").offsetHeight;
    if (weldMethodheight > 180) {
        $("#div2").height(weldMethodheight);
    }
</script>
