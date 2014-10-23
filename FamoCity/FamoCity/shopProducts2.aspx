<%@ Page Title="" Language="C#" MasterPageFile="~/MasterShpCms2.master" AutoEventWireup="true"
    CodeBehind="shopProducts2.aspx.cs" Inherits="FamoCity.shopProducts2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderChield" runat="server">
    <script type="text/javascript">
        function deleteP(pid) {
            var ans = confirm('هل انت متأكد من حذف المنتج ؟');
            if (ans) {
                $.ajax({
                    type: "POST",
                    url: '/shopProductAdd2.aspx/deleteP',
                    data: "{'pid':'" + pid + "'}",
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
    <div class="cms_page_title">
        <span>عرض المنتجات</span>
    </div>
    <div id="inside_left_content_cms">
        <div class="cms_cp_title">
            <div class="cpp_title">
                <a href="#">تعديل و حذف المنتجات </a><span>
                    <img src="/images/cpp_home.png" /></span></div>
        </div>
        <form id="form" runat="server">
        <div class="grid_menu">
            <asp:GridView runat="server" Width="100%" ID="gridView" AutoGenerateColumns="False"
                CssClass="grid_menu_tab">
                <Columns>
                    <asp:TemplateField HeaderText="تعديل">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLinkDelete" runat="server" NavigateUrl='<%# string.Format("javascript:deleteP({0});", Eval("prod_id")) %>'><img src="/images/cms_remove.png" /></asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cms_remove_title" />
                        <ItemStyle CssClass="cms_remove" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="description" HeaderText="الوصف">
                        <HeaderStyle CssClass="cms_col_title" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="اسم المنتج">
                        <ItemTemplate>
                            <a href='/Shop/edit/Prod/<%#Eval("prod_id") %>'>
                                <%#Eval("name") %></a>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cms_col_title" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="prod_id" HeaderText="الرقم">
                        <HeaderStyle CssClass="cms_col_title" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        </form>
    </div>
</asp:Content>
