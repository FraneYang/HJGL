<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WeldReportDataIn.aspx.cs" Inherits="Web.WeldingManage.WeldReportDataIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function ShowProgressBarAudit(fileName, workAreaId) {
            var result = window.showModalDialog("ProgressBar.aspx?fileName=" + fileName + "&workAreaId=" + workAreaId, "", "status=no;dialogWidth=520px;dialogHeight=150px;menu=no;resizeable=no;scroll=yes;scrollbars=yes;center=yes;edge=raise;location=no");
            if (result != null && result != "") {
                document.getElementById("hdCheckResult").value = result;
                document.getElementById("imgBtnCheck").click();
            }
        }

        function ShowProgressBarInsert(fileName, workAreaId) {
            var result = window.showModalDialog("ProgressBarIn.aspx?fileName=" + fileName + "&workAreaId=" + workAreaId, "", "status=no;dialogWidth=520px;dialogHeight=150px;menu=no;resizeable=no;scroll=yes;scrollbars=yes;center=yes;edge=raise;location=no");
            if (result != null && result != "") {
                document.getElementById("imgBtnInsert").click();
            }
        }

        function ShowProgressBarSave(workAreaId) {
            var result = window.showModalDialog("ProgressBarSave.aspx?workAreaId=" + workAreaId, "", "status=no;dialogWidth=520px;dialogHeight=150px;menu=no;resizeable=no;scroll=yes;scrollbars=yes;center=yes;edge=raise;location=no");
            //            if (result != null && result != "") {
            //                document.getElementById("imgBtnInsert").click();
            //            }
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
                        <td align="left" valign="middle" style="width: 25%; font-size: 11pt; font-weight: bold">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                            &nbsp;焊接日报导入
                            <input id="hdCheckResult" type="hidden" runat="server" />
                            <input id="hdfileName" type="hidden" runat="server" />
                        </td>
                        <td align="right" valign="middle" style="width: 75%; height: 30px;">
                            <asp:ImageButton ID="imgbtnUpload" runat="server" ImageUrl="~/Images/Template.gif"
                                    OnClick="imgbtnUpload_Click" />
                                    <asp:ImageButton ID="btnReturn" runat="server" 
                                ImageUrl="~/Images/return.gif" onclick="btnReturn_Click" 
                                    />
                                    &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divSearch" runat="server">
                    <table id="Table2" runat="server" width="100%" cellpadding="1" cellspacing="1" class="table">
                        <tr style="height: 32px">
                            <td align="center" style="width: 35%">
                                施工区域&nbsp;
                                <asp:DropDownList ID="drpWorkArea" runat="server" Width="60%" Height="22px">
                                </asp:DropDownList>
                                      <asp:ImageButton ID="imgSearch" runat="server" ImageUrl="~/Images/JointCount.gif" Style="cursor: hand"
                                                        ToolTip="查找" OnClick="imgSearch_Click" />
                            </td>
                            <td align="right" style="width: 35%">
                                <asp:Label ID="Label6" runat="server" Text="选择要导入的文件"></asp:Label>&nbsp;
                                <asp:FileUpload ID="FileExcel" runat="server" Width="70%" />
                            </td>
                            <td colspan="2" align="center" style="width: 22%">
                             <asp:ImageButton ID="imgbtnAudit" runat="server" ImageUrl="~/Images/Audit.gif" 
                                    onclick="imgbtnAudit_Click" />
                                <asp:ImageButton ID="imgbtnIn" runat="server" ImageUrl="~/Images/Import.gif" OnClick="imgbtnIn_Click" />
                                <%--<asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" OnClick="btnSave_Click"
                                ValidationGroup="Save" />--%>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <table id="Table4" width="100%" cellpadding="0" cellspacing="0" runat="server">
        <tr>
            <td align="right" valign="middle" style="width: 75%; height: 30px;">
                <asp:ImageButton ID="imgbtnOut" runat="server" ImageUrl="~/Images/Export.gif" ValidationGroup="Save"
                    OnClick="imgbtnOut_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table9" width="100%" cellpadding="0" cellspacing="0" runat="server">
                    <tr>
                        <td>
                            <div id="div2" style="width: 100%; overflow: auto; height: 480px;" runat="server">
                                <asp:GridView ID="gvErrorInfo" runat="server" AllowSorting="True" PageSize="12" AutoGenerateColumns="False"
                                    HorizontalAlign="Justify" Width="100%">
                                    <AlternatingRowStyle CssClass="GridBgColr" />
                                    <Columns>
                                       <%-- <asp:BoundField DataField="Number" HeaderText="序号">
                                            <ItemStyle Width="10%" />
                                        </asp:BoundField>--%>
                                         <asp:TemplateField HeaderText="序号">
                                            <ItemTemplate>
                                                <%#gvErrorInfo.PageIndex + Container.DataItemIndex + 1%>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        <asp:BoundField DataField="Row" HeaderText="错误行号">
                                            <ItemStyle Width="20%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Column" HeaderText="错误列">
                                            <ItemStyle Width="30%" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Reason" HeaderText="错误类型">
                                            <ItemStyle Width="40%" />
                                        </asp:BoundField>
                                    </Columns>
                                    <HeaderStyle CssClass="GridBgColr" />
                                    <RowStyle CssClass="GridRow" />
                                    <PagerStyle HorizontalAlign="Left" />
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="Table5" style="width: 100%; font-size: 15px; color: Blue;" runat="server"
        border="0" cellpadding="1" cellspacing="0">
        <tr style="height: 28px">          
            <td style="text-align: left;">
               &nbsp;&nbsp; <%--<asp:LinkButton ID="lkAchievements" runat="server" 
                    Text="数据导入说明" CssClass="ItemLink" onclick="DataHelp_Click">
                </asp:LinkButton>--%>
            </td>
        </tr>       
    </table>
    </form>
</body>
</html>
