<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditPipeline.aspx.cs" Inherits="Web.WeldingManage.EditPipeline" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title></title>
    <base target="_self" />
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Controls/My97DatePicker/WdatePicker.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function CheckDropDownList(source, args) {
            if (args.Value == "0")
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        function WindowClose(result) {
            window.returnValue = result;
            window.close();
        }

        function keypress() {
            var keyASCII = event.keyCode;
            if ((keyASCII >= 48 && keyASCII <= 57) || keyASCII == 46) {

            }
            else {
                event.keyCode = 0;
            }

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
                            <td align="left" valign="middle" style="width: 70%; font-size: 11pt; font-weight: bold">
                                <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                                &nbsp;<asp:Label runat="server" ID="lblTitle"></asp:Label>
                            </td>
                            <td align="right" valign="middle" style="width: 80%; height: 30px;">
                                <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" OnClick="btnSave_Click" ValidationGroup="Save"/>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
            <td>
                <table id="Table2" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                    <tr style="height: 32px">
                        <td align="right" style="width: 10%">
                            <asp:Label ID="Label3" runat="server" Text="管线代号" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                            &nbsp;
                          <asp:TextBox ID="txtISO_IsoNo" runat="server" Width="80%" CssClass="textboxStyle"
                                ValidationGroup="Save"  >
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtISO_IsoNo"
                                Display="Dynamic" ErrorMessage="请输入管线代号" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="right" style="width: 10%">
                            <asp:Label ID="Label1" runat="server" Text="单位"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                            &nbsp;
                            <asp:TextBox ID="txtUnitName" runat="server" Width="80%" CssClass="textboxStyle"
                                 ReadOnly="true">
                            </asp:TextBox>
                        </td>
                        <td align="right" style="width: 10%">
                            <asp:Label ID="Label2" runat="server" Text="介质" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" style="width: 30%">
                            &nbsp;<asp:DropDownList ID="drpSER" runat="server" Height="22" Width="80%"
                                CssClass="textboxStyle" >
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator2" runat="server" Display="Dynamic" ErrorMessage="&quot;请选择介质&quot;"
                                ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpSER"
                                ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                        </td>
                    </tr>
                    <tr style="height: 32px;">
                        <td align="right" style="width: 10%">
                            <%--<asp:Label ID="Label4" runat="server" Text="所属项目"></asp:Label>--%>
                            <asp:Label ID="Label12" runat="server" Text="探伤比例" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                              &nbsp;<asp:DropDownList ID="drpNDTRate" runat="server" Height="22" Width="80%"
                                CssClass="textboxStyle">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator1" runat="server" Display="Dynamic" ErrorMessage="&quot;请选择探伤比例&quot;"
                                ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpNDTRate"
                                ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                        </td>
                        <td align="right" style="width: 10%">
                            <asp:Label ID="Label7" runat="server" Text="探伤类型"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                            &nbsp;
                                    <asp:DropDownList ID="drpNDTName" runat="server" Height="22" Width="80%"
                                CssClass="textboxStyle">
                            </asp:DropDownList>
                        </td>
                        <td align="right" style="width: 10%">
                            <asp:Label ID="Label5" runat="server" Text="施工区域"></asp:Label>
                        </td>
                        <td align="left" style="width: 30%">
                            &nbsp;
                     <asp:TextBox ID="txtWorkArea" runat="server" Width="80%" CssClass="textboxStyle"
                                 ReadOnly="true">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr style="height: 32px">
                        <td align="right" style="width: 10%">
                            <asp:Label ID="Label9" runat="server" Text="系统号"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                            &nbsp;
                            <asp:TextBox ID="txtISO_SysNo" runat="server" Width="80%" CssClass="textboxStyle"
                                ValidationGroup="Save" >
                            </asp:TextBox>
                        </td>
                        <td align="right" style="width: 10%">
                           <asp:Label ID="Label8" runat="server" Text="分系统号"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                           &nbsp;
                            <asp:TextBox ID="txtISO_SubSysNo" runat="server" Width="80%" CssClass="textboxStyle" ValidationGroup="Save"
                                >
                            </asp:TextBox>
                        </td>
                        <td align="right" style="width: 10%">
                                <asp:Label ID="Label6" runat="server" Text="工作包号"></asp:Label>
                        </td>
                        <td align="left" style="width: 30%">
                           &nbsp;
                            <asp:TextBox ID="txtISO_CwpNo" runat="server" Width="80%" CssClass="textboxStyle" ValidationGroup="Save"
                                >
                            </asp:TextBox>
                        </td>
                    </tr>
                       <tr style="height: 32px">
                        <td align="right" style="width: 10%">
                            <asp:Label ID="Label10" runat="server" Text="单线图号"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                            &nbsp;
                            <asp:TextBox ID="txtISO_IsoNumber" runat="server" Width="80%" CssClass="textboxStyle"
                                ValidationGroup="Save">
                            </asp:TextBox>
                        </td>
                        <td align="right" style="width: 10%">
                           <asp:Label ID="Label11" runat="server" Text="图纸版次"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                           &nbsp;
                            <asp:TextBox ID="txtISO_Rev" runat="server" Width="80%" CssClass="textboxStyle" ValidationGroup="Save"
                                >
                            </asp:TextBox>
                        </td>
                        <td align="right" style="width: 10%">
                                <asp:Label ID="Label14" runat="server" Text="页数"></asp:Label>
                        </td>
                        <td align="left" style="width: 30%">
                           &nbsp;
                            <asp:TextBox ID="txtISO_Sheet" runat="server" Width="80%" CssClass="textboxStyle" ValidationGroup="Save"
                                >
                            </asp:TextBox>
                        </td>
                    </tr>
                       <tr style="height: 32px">
                        <td align="right" style="width: 10%">
                            <asp:Label ID="Label15" runat="server" Text="总管段数"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%" onkeypress="keypress()">
                            &nbsp;
                            <asp:TextBox ID="txtISO_PipeQty" runat="server" Width="80%" CssClass="textboxStyle"
                                ValidationGroup="Save">
                            </asp:TextBox>
                        </td>
                        <td align="right" style="width: 10%">
                           <asp:Label ID="Label16" runat="server" Text="涂漆类别"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                           &nbsp;
                            <asp:TextBox ID="txtISO_Paint" runat="server" Width="80%" CssClass="textboxStyle" ValidationGroup="Save"
                                >
                            </asp:TextBox>
                        </td>
                        <td align="right" style="width: 10%">
                                <asp:Label ID="Label17" runat="server" Text="绝热类别"></asp:Label>
                        </td>
                        <td align="left" style="width: 30%">
                           &nbsp;
                            <asp:TextBox ID="txtISO_Insulator" runat="server" Width="80%" CssClass="textboxStyle" ValidationGroup="Save"
                                >
                            </asp:TextBox>
                        </td>
                    </tr>
                       <tr style="height: 32px">
                        <td align="right" style="width: 10%">
                            <asp:Label ID="Label18" runat="server" Text="材质" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                            &nbsp;
                         <asp:DropDownList ID="drpSTEName" runat="server" Height="22" Width="80%"
                                CssClass="textboxStyle">
                            </asp:DropDownList>
                            <asp:CustomValidator ID="CustomValidator5" runat="server" Display="Dynamic" ErrorMessage="&quot;请选择材质&quot;"
                                ForeColor="Red" ValidationGroup="Save" ControlToValidate="drpSTEName"
                                ClientValidationFunction="CheckDropDownList">*</asp:CustomValidator>
                        </td>
                        <td align="right" style="width: 10%">
                           <asp:Label ID="Label19" runat="server" Text="执行标准"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                           &nbsp;
                            <asp:TextBox ID="txtISO_Executive" runat="server" Width="80%" CssClass="textboxStyle" ValidationGroup="Save"
                                >
                            </asp:TextBox>
                        </td>
                        <td align="right" style="width: 10%">
                                <asp:Label ID="Label20" runat="server" Text="规格"></asp:Label>
                        </td>
                        <td align="left" style="width: 30%">
                           &nbsp;
                            <asp:TextBox ID="txtISO_Specification" runat="server" Width="80%" CssClass="textboxStyle" ValidationGroup="Save"
                                >
                            </asp:TextBox>
                        </td>
                    </tr>
                      <tr style="height: 32px">
                        <td align="right" style="width: 10%">
                            <asp:Label ID="Label21" runat="server" Text="修改人"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                            &nbsp;
                            <asp:TextBox ID="txtISO_Modifier" runat="server" Width="80%" CssClass="textboxStyle"
                                ValidationGroup="Save">
                            </asp:TextBox>
                        </td>
                        <td align="right" style="width: 10%">
                           <asp:Label ID="Label22" runat="server" Text="修改日期"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                           &nbsp;
                             <input id="txtISO_ModifyDate" runat="server" readonly="readonly" class="Wdate" style="width: 80%;
                                cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
                        </td>
                        <td align="right" style="width: 10%">
                                <asp:Label ID="Label23" runat="server" Text="建档人"></asp:Label>
                        </td>
                        <td align="left" style="width: 30%">
                           &nbsp;
                            <asp:TextBox ID="txtISO_Creator" runat="server" Width="80%" CssClass="textboxStyle" ValidationGroup="Save"
                                >
                            </asp:TextBox>
                        </td>
                    </tr>
                      <tr style="height: 32px">
                        <td align="right" style="width: 10%">
                            <asp:Label ID="Label24" runat="server" Text="建档日期"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                            &nbsp;
                            <input id="txtISO_CreateDate" runat="server" readonly="readonly" class="Wdate" style="width: 80%;
                                cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
                        </td>
                        <td align="right" style="width: 10%">
                           <asp:Label ID="Label25" runat="server" Text="设计压力"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%" onkeypress="keypress()">
                           &nbsp;
                            <asp:TextBox ID="txtISO_DesignPress" runat="server" Width="80%" CssClass="textboxStyle" ValidationGroup="Save"
                                >
                            </asp:TextBox>
                        </td>
                        <td align="right" style="width: 10%">
                                <asp:Label ID="Label26" runat="server" Text="设计温度"></asp:Label>
                        </td>
                        <td align="left" style="width: 30%" onkeypress="keypress()">
                           &nbsp;
                            <asp:TextBox ID="txtISO_DesignTemperature" runat="server" Width="80%" CssClass="textboxStyle" ValidationGroup="Save"
                                >
                            </asp:TextBox>
                        </td>
                    </tr>
                      <tr style="height: 32px">
                        <td align="right" style="width: 10%">
                            <asp:Label ID="Label27" runat="server" Text="试验压力"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%" onkeypress="keypress()">
                            &nbsp;
                            <asp:TextBox ID="txtISO_TestPress" runat="server" Width="80%" CssClass="textboxStyle"
                                ValidationGroup="Save">
                            </asp:TextBox>
                        </td>
                        <td align="right" style="width: 10%">
                           <asp:Label ID="Label28" runat="server" Text="试验温度"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%" onkeypress="keypress()">
                           &nbsp;
                            <asp:TextBox ID="txtISO_TestTemperature" runat="server" Width="80%" CssClass="textboxStyle" ValidationGroup="Save"
                                >
                            </asp:TextBox>
                        </td>
                        <td align="right" style="width: 10%">
                                <asp:Label ID="Label29" runat="server" Text="合格等级"></asp:Label>
                        </td>
                        <td align="left" style="width: 30%">
                           &nbsp;
                         <asp:DropDownList ID="drpNDTClass" runat="server" Height="22" Width="80%"
                                CssClass="textboxStyle">
                                 <asp:ListItem Value="0">- 请选择 -</asp:ListItem>
                                    <asp:ListItem Value="Ⅰ">Ⅰ</asp:ListItem>
                                <asp:ListItem Value="Ⅱ">Ⅱ</asp:ListItem>
                                 <asp:ListItem Value="Ⅲ">Ⅲ</asp:ListItem>
                                <asp:ListItem Value="Ⅳ">Ⅳ</asp:ListItem>
                                 <asp:ListItem Value="Ⅴ">Ⅴ</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                     <tr style="height: 32px">
                        <td align="right" style="width: 10%">
                            <asp:Label ID="Label30" runat="server" Text="渗透比例"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                            &nbsp;
                               <asp:TextBox ID="txtISO_PTRate" runat="server" Width="80%" CssClass="textboxStyle" ValidationGroup="Save"
                                >
                            </asp:TextBox>
                        </td>
                        <td align="right" style="width: 10%">
                           <asp:Label ID="Label31" runat="server" Text="渗透等级"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                           &nbsp;
                            <asp:TextBox ID="txtISO_PTClass" runat="server" Width="80%" CssClass="textboxStyle" ValidationGroup="Save"
                                >
                            </asp:TextBox>
                        </td>
                        <td align="right" style="width: 10%">
                                <asp:Label ID="Label32" runat="server" Text="是否酸洗"></asp:Label>
                        </td>
                        <td align="left" style="width: 30%">
                           &nbsp;
                            <asp:DropDownList ID="drpISO_IfPickling" runat="server" Height="22" Width="80%"
                                CssClass="textboxStyle">
                                 <asp:ListItem Value="0">- 请选择 -</asp:ListItem>
                                    <asp:ListItem Value="true">是</asp:ListItem>
                                <asp:ListItem Value="false">否</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                     <tr style="height: 32px">
                        <td align="right" style="width: 10%">
                            <asp:Label ID="Label33" runat="server" Text="是否抛光"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                            &nbsp;
                              <asp:DropDownList ID="drpISO_IfChasing" runat="server" Height="22" Width="80%"
                                CssClass="textboxStyle">
                                 <asp:ListItem Value="0">- 请选择 -</asp:ListItem>
                                    <asp:ListItem Value="true">是</asp:ListItem>
                                <asp:ListItem Value="false">否</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                             <td align="right" style="width: 10%">
                           <asp:Label ID="Label4" runat="server" Text="管道等级"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                           &nbsp;
                                   <asp:DropDownList ID="drpIDName" runat="server" Height="22" Width="80%"
                                CssClass="textboxStyle">
                            </asp:DropDownList>
                        </td>
                        <td align="right" style="width: 10%">
                           <asp:Label ID="Label34" runat="server" Text="备注"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                           &nbsp;
                            <asp:TextBox ID="txtISO_Remark" runat="server" Width="80%" CssClass="textboxStyle" ValidationGroup="Save"
                                >
                            </asp:TextBox>
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
