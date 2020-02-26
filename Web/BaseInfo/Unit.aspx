<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Unit.aspx.cs" Inherits="Web.BaseInfo.Unit" %>

<%@ Register Src="~/Controls/GridNavgator.ascx" TagName="GridNavgator" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>单位</title>
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
                            &nbsp;单位信息
                        </td>
                        <td align="right" valign="middle" style="width: 55%; height: 30px;">
                            <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/addbutton.gif" OnClick="btnAdd_Click" />
                            <asp:ImageButton ID="btnModify" runat="server" ImageUrl="~/Images/modybutton.gif"
                                OnClick="btnModify_Click" />
                            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="~/Images/savebutton.gif" OnClick="btnSave_Click"
                                ValidationGroup="Save" />
                            <asp:ImageButton ID="btncancel" runat="server" ImageUrl="~/Images/cancel.gif" OnClick="btncancel_Click" />&nbsp;
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
                            <asp:Label ID="Label3" runat="server" Text="单位类型"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                            &nbsp;
                            <asp:DropDownList ID="ddlUnitType" runat="server" Height="22px" Width="80%" Enabled="False">
                                <%--<asp:ListItem Value="1">业主</asp:ListItem>
                                <asp:ListItem Value="2">监理</asp:ListItem>
                                <asp:ListItem Value="3">施工分包方</asp:ListItem>
                                <asp:ListItem Value="4">其它</asp:ListItem>
                                <asp:ListItem Value="5">总包</asp:ListItem>--%>
                            </asp:DropDownList>
                        </td>
                        <td align="right" style="width: 10%">
                            <asp:Label ID="Label1" runat="server" Text="单位代码"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                            &nbsp;
                            <asp:TextBox ID="txtUnitCode" runat="server" Width="80%" CssClass="textboxStyle"
                                ValidationGroup="Save" Enabled="False">
                            </asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUnitCode"
                                Display="Dynamic" ErrorMessage="请输入单位代码" ForeColor="Red" SetFocusOnError="True"
                                ValidationGroup="Save">*</asp:RequiredFieldValidator>
                        </td>
                        <td align="right" style="width: 10%">
                            <asp:Label ID="Label2" runat="server" Text="单位名称"></asp:Label>
                        </td>
                        <td align="left" style="width: 30%">
                            &nbsp;
                            <asp:TextBox ID="txtUnitName" runat="server" Width="80%" CssClass="textboxStyle"
                                ValidationGroup="Save" Enabled="False">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr style="height: 32px;">
                        <td align="right" style="width: 10%">
                            <%--<asp:Label ID="Label4" runat="server" Text="所属项目"></asp:Label>--%>
                            <asp:Label ID="Label12" runat="server" Text="工期(月)"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                            &nbsp;
                            <asp:TextBox ID="txtDuration" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                        </td>
                        <td align="right" style="width: 10%">
                            <asp:Label ID="Label7" runat="server" Text="电话"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                            &nbsp;
                            <asp:TextBox ID="txtTelephone" runat="server" Width="80%" CssClass="textboxStyle"
                                ValidationGroup="Save" Enabled="False">
                            </asp:TextBox>
                        </td>
                        <td align="right" style="width: 10%">
                            <asp:Label ID="Label5" runat="server" Text="法人代表"></asp:Label>
                        </td>
                        <td align="left" style="width: 30%">
                            &nbsp;
                            <asp:TextBox ID="txtCorporate" runat="server" Width="80%" CssClass="textboxStyle"
                                ValidationGroup="Save" Enabled="False">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr style="height: 32px">
                        <td align="right" style="width: 10%">
                            <asp:Label ID="Label9" runat="server" Text="工程范围"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                            &nbsp;
                            <asp:TextBox ID="txtProjectRange" runat="server" Width="80%" CssClass="textboxStyle"
                                ValidationGroup="Save" Enabled="False">
                            </asp:TextBox>
                        </td>
                        <td align="right" style="width: 10%">
                           <asp:Label ID="Label8" runat="server" Text="传真"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                           &nbsp;
                            <asp:TextBox ID="txtFax" runat="server" Width="80%" CssClass="textboxStyle" ValidationGroup="Save"
                                Enabled="False">
                            </asp:TextBox>
                        </td>
                        <td align="right" style="width: 10%">
                                <asp:Label ID="Label6" runat="server" Text="通讯地址"></asp:Label>
                        </td>
                        <td align="left" style="width: 30%">
                           &nbsp;
                            <asp:TextBox ID="txtAddress" runat="server" Width="80%" CssClass="textboxStyle" ValidationGroup="Save"
                                Enabled="False">
                            </asp:TextBox>
                        </td>
                    </tr>
                    <tr style="height: 32px;">
                        <td align="right" style="width: 10%">
                         <asp:Label ID="Label4" runat="server" Text="入场时间"></asp:Label>
                        </td>
                        <td align="left">
                           &nbsp;
                            <input id="txtInTime" runat="server" readonly="readonly" class="Wdate" style="width: 80%;
                                cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
                        </td>
                        <td align="right" style="width: 10%">
                           <asp:Label ID="Label13" runat="server" Text="出场时间"></asp:Label>
                        </td>
                        <td align="left" style="width: 20%">
                              &nbsp;
                            <input id="txtOutTime" runat="server" readonly="readonly" class="Wdate" style="width: 80%;
                                cursor: hand" onfocus="WdatePicker({dateFmt:'yyyy-MM-dd',skin:'whyGreen'})" />
                        </td>
                         <td align="right" style="width:10%">
                         <asp:Label ID="Label10" runat="server" Text="排列序号"></asp:Label>
                      </td>
                      <td align="left" style="width:20%">&nbsp;
                            <asp:TextBox ID="txtSortIndex" runat="server" Width="80%" CssClass="textboxStyle"></asp:TextBox>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" 
                              ControlToValidate="txtSortIndex" Display="Dynamic" 
                              ErrorMessage="&quot;[排列序号]只能够为正整数！&quot;" ForeColor="Red" 
                              ValidationExpression="^0$|^[1-9][0-9]*$" ValidationGroup="Save">*</asp:RegularExpressionValidator>
                      </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="Table3" runat="server" cellpadding="0" cellspacing="0" style="background: url('../Images/bg-1.gif')"
                    width="100%">
                    <tr>
                        <td align="left" valign="middle" style="width: 100%">
                            <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image1" runat="server" />
                            &nbsp;
                            <asp:DropDownList ID="ddlSearch" runat="server" Height="22px">
                            </asp:DropDownList>
                            <asp:TextBox ID="txtSearch" runat="server">
                            </asp:TextBox>
                            <asp:ImageButton ID="btnSearch" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/search.png"
                                OnClick="btnSearch_Click" />&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <div id="div2" runat="server" style="overflow: auto;">
        <table id="Table8" width="1400px" cellpadding="0" cellspacing="0" runat="server">
            <tr>
                <td>
                    <asp:GridView ID="UnitGridView" runat="server" DataSourceID="ObjectDataSource1" AllowPaging="True"
                        AllowSorting="True" AutoGenerateColumns="False" HorizontalAlign="Justify" PageSize="12"
                        Width="100%" OnRowCommand="UnitGridView_RowCommand" OnDataBound="UnitGridView_DataBound">
                        <AlternatingRowStyle CssClass="GridBgColr" />
                        <Columns>
                            <asp:BoundField DataField="UnitCode" HeaderText="单位代码">
                                <HeaderStyle Width="5%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="单位名称">
                                <ItemTemplate>
                                    <asp:LinkButton ID="UnitName" runat="server" CommandArgument='<%# Bind("UnitId") %>'
                                        CommandName="UpdateUnit" CssClass="ItemLink" Text='<%# Bind("UnitName") %>'></asp:LinkButton>
                                    <asp:HiddenField ID="hdUnitCode" runat="server" Value='<%# Bind("UnitCode") %>' />
                                </ItemTemplate>
                                <HeaderStyle Width="15%" />
                            </asp:TemplateField>
                            <%--<asp:BoundField DataField="UnitName" HeaderText="单位名称">
                                            <HeaderStyle Width="10%" />
                                        </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="单位类型">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitType" runat="server" Text='<%# ConvertStr(Eval("UnitType")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Width="5%" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="所属项目" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProject" runat="server" Text='<%# Eval("ProjectName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                            <asp:BoundField DataField="Corporate" HeaderText="法人代表">
                                <HeaderStyle Width="5%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Address" HeaderText="通讯地址">
                                <HeaderStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Telephone" HeaderText="电话">
                                <HeaderStyle Width="7%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Fax" HeaderText="传真">
                                <HeaderStyle Width="7%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ProjectRange" HeaderText="工程范围">
                                <HeaderStyle Width="10%" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Duration" HeaderText="工期(月)">
                                <HeaderStyle Width="5%" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="删除">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/Images/DeleteBtn.gif"
                                        CommandArgument='<%# Bind("UnitId") %>' CommandName="UnitDelete" OnClientClick="return confirm(&quot;确定删除此单位吗？&quot;);"
                                        ToolTip="删除" />
                                </ItemTemplate>
                                <HeaderStyle Width="4%" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="GridBgColr" />
                        <PagerStyle HorizontalAlign="Left" />
                        <RowStyle CssClass="GridRow" />
                        <PagerTemplate>
                            <uc1:GridNavgator ID="GridNavgator1" runat="server" />
                        </PagerTemplate>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" OnSelecting="ObjectDataSource1_Selecting"
                        SelectCountMethod="GetListCount" SelectMethod="GetListData" TypeName="BLL.UnitService">
                        <SelectParameters>
                            <asp:Parameter Name="searchItem" />
                            <asp:Parameter Name="searchValue" />
                            <asp:Parameter Name="projectId" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
            </tr>
        </table>
    </div>
    <asp:ValidationSummary ID="ValidationSummary1" Style="z-index: 101; left: 8px; position: absolute;
        top: 8px" runat="server" HeaderText="请注意！" ShowMessageBox="True" ShowSummary="False"
        ValidationGroup="Save" />
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    var height = parent.document.getElementById("center").offsetHeight;
    $("#div2").height(height - 220);
</script>
