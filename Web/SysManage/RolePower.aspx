<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RolePower.aspx.cs" Inherits="Web.SysManage.RolePower" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Styles/Style.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript">
        function OnTreeNodeChecked() //单击父节点子结点选中的代码
        {
            var ele = event.srcElement;
            if (ele.type == 'checkbox') {
                if (ele.checked) {
                    selparent(ele);
                }

                var childrenDivID = ele.id.replace('CheckBox', 'Nodes');
                var div = document.getElementById(childrenDivID);

                if (div == null) return;
                var checkBoxs = div.getElementsByTagName('INPUT');

                for (var i = 0; i < checkBoxs.length; i++) {
                    if (checkBoxs[i].type == 'checkbox')
                        checkBoxs[i].checked = ele.checked;
                }
            }
        }

        function selparent(obj) {
            var p = obj.parentNode.parentNode.parentNode.parentNode.parentNode;
            var pCheckNodeID = p.id.replace("Nodes", "CheckBox");
            var checkNode = false;
            if (pCheckNodeID != "") {
                checkNode = document.getElementById(pCheckNodeID);
            }
            if (checkNode) {
                checkNode.checked = true;
                selparent(checkNode);
            }
        }
</script>
    <style type="text/css">
        .style1
        {
            height: 450px;
        }
        .style2
        {
            width: 0px;
            height: 450px;
        }
    </style>
</head>

<body>
   <form id="form1" runat="server">
     <table cellspacing="0" cellpadding="0" width="100%" 
             style="border-bottom-style: solid; border-bottom-color:Silver">
            <tr>
               <td colspan="3" style="width:100%; background:url('../Images/bg-1.gif')">
                <table id="tabbtn" runat="server" width="100%"  style="background:url('../Images/bg-1.gif')" cellpadding="0" cellspacing="0">
                 <tr>
                    <td align="left" valign="middle" style="width:55%; font-size:11pt; font-weight:bold">
                       <asp:Image ImageUrl="~/Images/lv-1.gif" ImageAlign="AbsMiddle" ID="image15" runat="server" />
                         角色授权&nbsp;
                         <asp:Label ID="lblText" runat="server">
                         </asp:Label>
                    </td>
                      <td align="right" valign="middle" style="width:45%; height:30px;">
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
				<td valign="top" align="left" class="style1">
                    <div id="Div1" style="height:450px; width:175px; overflow:auto;" runat="server">
					<font face="宋体" >
                        <asp:TreeView ID="RoleTree" ForeColor="Black" runat="server"  ExpandDepth="1" 
                            ShowLines="True" onselectednodechanged="PostTree_SelectedNodeChanged">
                        </asp:TreeView>
			        </font>
                    </div>
				</td>
                <td style="background-color:Silver" class="style2"></td>
                <td valign="top" align="left" class="style1">
                    <div id="Div2" style="height:420px; width:600px; overflow:auto;" runat="server">
					<font face="宋体" >
                        <asp:TreeView ID="MenuTree"  onclick="OnTreeNodeChecked()"  ForeColor="Black" runat="server"  ExpandDepth="1" 
                            ShowCheckBoxes="All" ShowLines="True">
                        </asp:TreeView>
			        </font>
                    </div>
                </td>
			</tr>
        </table>
    </form>
</body>
</html>
