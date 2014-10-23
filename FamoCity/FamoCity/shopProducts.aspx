<%@ Page Title="" Language="C#" MasterPageFile="~/MasterShpCms.Master" AutoEventWireup="true"
    CodeBehind="shopProducts.aspx.cs" Inherits="FamoCity.shopProducts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            ActiveSubMnu('mnuProd', 'submnuProds');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function deleteP(pid) {
            var ans = confirm('Are you sure you want to delete?');
            if (ans) {
                $.ajax({
                    type: "POST",
                    url: 'shopProducts.aspx/delete',
                    data: "{'id':'" + pid + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d > 0) {


                        }
                    },
                    error: AjaxFailed
                });
            }
        }

        function AjaxFailed(result) {
            alert(result.status + ' ' + result.statusText);
        }

    </script>
    <div id="content">
        <div id="page-heading">
            <h1>
                عرض المنتجات</h1>
        </div>
        <table border="0" width="100%" cellpadding="0" cellspacing="0" id="content-table">
            <tr>
                <th rowspan="3" class="sized">
                    <img src="images/shared/side_shadowright.jpg" width="20" height="300" alt="" />
                </th>
                <th class="topleft">
                </th>
                <td id="tbl-border-top">
                </td>
                <th class="topright">
                </th>
                <th rowspan="3" class="sized">
                    <img src="images/shared/side_shadowleft.jpg" width="20" height="300" alt="" />
                </th>
            </tr>
            <tr>
                <td id="tbl-border-left">
                </td>
                <td>
                    <!--  start content-table-inner ...................................................................... START -->
                    <div id="content-table-inner">
                        <!--  start table-content  -->
                        <div id="table-content">
                            <!--  start product-table ..................................................................................... -->
                            <div id="mainform">
                                
                                <asp:GridView ID="GridView1" runat="server" CssClass="product-table" AutoGenerateColumns="False">
                                    <AlternatingRowStyle CssClass="alternate-row" />
                                    <Columns>
                                        <asp:BoundField DataField="prod_id" HeaderText="ID">
                                            <HeaderStyle CssClass="table-header-check" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="name" HeaderText="المنتج">
                                            <HeaderStyle CssClass="table-header-repeat line-left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="description" HeaderText="الوصف">
                                            <HeaderStyle CssClass="table-header-repeat line-left" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLinkEdit" CssClass="icon-1 info-tooltip" runat="server" NavigateUrl='<%# string.Format("Shop/edit/Product/{{id}}", Eval("id")) %>'></asp:HyperLink>
                                                <asp:HyperLink ID="HyperLinkDelete" CssClass="icon-2 info-tooltip" runat="server"
                                                    NavigateUrl='<%# string.Format("javascript:deleteP({0});", Eval("prod_id")) %>'></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="table-header-options line-left" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <!--  end product-table................................... -->
                            </div>
                        </div>
                        <!--  end content-table  -->
                        <!--  start actions-box ............................................... -->
                        <div id="actions-box">
                        </div>
                        <!-- end actions-box........... -->
                        <!--  start paging..................................................... -->
                        <!--  end paging................ -->
                        <div class="clear">
                        </div>
                    </div>
                    <!--  end content-table-inner ............................................END  -->
                </td>
                <td id="tbl-border-right">
                </td>
            </tr>
            <tr>
                <th class="sized bottomleft">
                </th>
                <td id="tbl-border-bottom">
                    &nbsp; &nbsp;
                </td>
                <th class="sized bottomright">
                </th>
            </tr>
        </table>
        <div class="clear">
        </div>
    </div>
</asp:Content>
