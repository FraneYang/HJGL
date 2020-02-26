<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="Web.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>诺必达管道焊接管理系统</title>
    <link href="Styles/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="ext/resources/css/ext-all.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="ext/adapter/ext/ext-base.js"></script>
    <script type="text/javascript" src="ext/ext-all.js"></script>
   
    <style type="text/css"> 
     *{font-size:12px!important;} 
    </style>

    <script type="text/javascript">
        function setTime() {
            var objTime = new Date();
            var year = objTime.getFullYear();
            var month = objTime.getMonth() + 1;
            var day = objTime.getDate();
           
            //alert(day);
            document.getElementById("lblTime").innerText = year + "年" + month + "月" + day + "日 ";
        }
        window.setInterval(setTime, 1000);
    </script>
    <script type="text/javascript">
    function openWord(){
       window.open("Doc/help.doc");
     }

     if ((typeof Range !== "undefined") && !Range.prototype.createContextualFragment) {
            Range.prototype.createContextualFragment = function (html) {
                var frag = document.createDocumentFragment(),
                        div = document.createElement("div");
                frag.appendChild(div);
                div.outerHTML = html;
                return frag;
            };
        }



    Ext.onReady(function(){
       Ext.BLANK_IMAGE_URL="ext/resources/images/default/s.gif";
       var Tree = Ext.tree;
       var tree = new Tree.TreePanel({
            el:'west_content',
            useArrows:true,
            autoHeight:true,
            split:true,
            lines:true,
            autoScroll:true,
            animate:true,
            enableDD:true,
            border:false,
            containerScroll: true, 
            loader: new Tree.TreeLoader({
                dataUrl:'ext_tree_json.aspx' //生成 ext 2.0 所需要的树型格式
                })
        });

        // set the root node
//        var root = new Tree.AsyncTreeNode({
//            text: '管理员',
//            draggable:false,
//            id:'0' // 0 为根目录
//        });
//        tree.setRootNode(root);
//        // render the tree
//        tree.render();
//        root.expand();
        
       var viewport = new Ext.Viewport({
            layout:'border',
            items:[ {
                region:'west',
                id:'west',
                //el:'panelWest',
                title:'菜单导航',
                split:true,
                width: 240,
                minSize: 200,
                maxSize: 360,
                collapsible: true,
                margins:'60 5 2 2',
                cmargins:'60 5 2 2',
                layout:'fit',
                layoutConfig:{ activeontop:true},
                defaults: { bodyStyle: 'margin:0;padding:0;'},
                //iconCls:'nav',
                items:
                    new Ext.TabPanel({
                        border:false,
                        activeTab:0,
                        tabPosition:'bottom',
                        items:[{
                                contentEl:'west_content',
                                title:'树状菜单',
                                autoScroll:true,
                                bodyStyle:'padding:5px;'
                               },
                               {
                                layout:'accordion',layoutConfig:{animate:true }, 
                                title:'菜单管理',
                                autoScroll:true,
                                border:false,
                                items: [<%=  GetMenuString() %>]                                  
                               }]
                     })
            },{
                region:'center',
                id:'center',
                deferredRender:false,
                margins:'60 0 2 0',
                html:'<iframe id="center-iframe" src="desktop.aspx" width="100%" height=100% name="main"  frameborder="0" scrolling="auto" style="border:0px none; background-color:#FFFFFF; "  ></iframe>',
                autoScroll:true 
            },
            {
                region:'south',
                margins:'0 0 0 2',
                border:false,
                html:'<div class="menu south">Copyright © 2010 hfnbdgs.com Inc. All rights reserved.p合肥诺必达 版权所有</div>'
               }
            ]
        });
        
        setTimeout(function(){
        Ext.get('loading').remove();
        Ext.get('loading-mask').fadeOut({remove:true});
        }, 250)
    });
    </script>
    <base target="_self" />
</head>
<body>
    <form id="form1" runat="server">
     <div id="loading-mask" style=""></div>
      <div id="loading">
        <div class="loading-indicator"><img src="ext/resources/extanim32.gif" alt="" width="32" height="32" style="margin-right:8px;" align="absmiddle"/>Loading...</div>
      </div>
  <div id="header"><h1><%= unitSet%></h1></div>
  <div class="menu">
                <span style="float: left">欢迎&nbsp;&nbsp;<b><asp:Label ID="lblUserName" runat="server"></asp:Label></b>&nbsp;&nbsp;今天是<asp:Label runat="server" ID="lblTime"></asp:Label>&nbsp;&nbsp;
                </span>
               <span style="float: right">
                    <img id="IMG1" onmouseover="this.style['cursor']='hand'" runat="server" alt="" onclick="var bConfirmed=confirm('您确定要注销吗?');if(bConfirmed) { window.open('login.aspx','_top');}"
                        src="images/zxan.gif"/>
                    <img id="IMG2" onmouseover="this.style['cursor']='hand'" runat="server" alt="" src="images/help.gif"
                        onclick="openWord()"/>
                    <img onmouseover="this.style['cursor']='hand'" onclick="var bConfirmed=confirm('您确定要退出系统吗?');if(bConfirmed) { window.close()}"
                        alt="" src="Images/004.gif" border="0"/>
                </span>
            </div>
  <div id="west">
    
  </div>
  <div id="center">
    
  </div>
  <div id="west_content" style="height:300px; ">
      <asp:TreeView ID="TreeView1" runat="server" ExpandDepth="0" 
          ImageSet="XPFileExplorer" ShowLines="True" NodeStyle-NodeSpacing="2">
          <LeafNodeStyle HorizontalPadding="2px" 
              ImageUrl="~/Ext/resources/images/default/tree/ICON1.GIF" />
          <LevelStyles>
              <asp:TreeNodeStyle Font-Underline="False" ForeColor="#003366" />
          </LevelStyles>
          <NodeStyle NodeSpacing="2px" />
      </asp:TreeView>
  </div>
    </form>
</body>
</html>
