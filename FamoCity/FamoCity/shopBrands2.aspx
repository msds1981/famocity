<%@ Page Title="" Language="C#" MasterPageFile="~/MasterShpCms2.master" AutoEventWireup="true"
    CodeBehind="shopBrands2.aspx.cs" Inherits="FamoCity.shopBrands2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderChield" runat="server">
    <script type="text/javascript">
        function deleteB(bid) {
            var ans = confirm('هل انت متأكد من حذف هذه الماركة ؟');
            if (ans) {
                $.ajax({
                    type: "POST",
                    url: '/shopBrands2.aspx/delete',
                    data: "{'id':'" + bid + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        if (msg.d > 0) {
                            document.location("/shop/edit/brands");
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
        <span>عرض الماركات</span>
    </div>
    <div id="inside_left_content_cms">
        <div class="cms_cp_title">
            <div class="cpp_title">
                <a href="#">تعديل و حذف الماركات </a><span>
                    <img src="/images/cpp_home.png" /></span></div>
        </div>
        <form id="form" runat="server">
        <div class="grid_menu">
            <asp:GridView runat="server" Width="100%" ID="gridView" AutoGenerateColumns="False"
                CssClass="grid_menu_tab">
                <Columns>
                    <asp:TemplateField HeaderText="تعديل">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLinkDelete" runat="server" NavigateUrl='<%# string.Format("javascript:deleteB({0});", Eval("brand_id")) %>'><img src="/images/cms_remove.png" /></asp:HyperLink>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cms_remove_title" />
                        <ItemStyle CssClass="cms_remove delcln" />
                        
                    </asp:TemplateField>
                    <asp:BoundField DataField="description" HeaderText="الوصف">
                        <HeaderStyle CssClass="cms_col_title" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="اسم الماركة">
                        <ItemTemplate>
                            <a href='/Shop/edit/Brand/<%#Eval("brand_id") %>'>
                                <%#Eval("title") %></a>
                        </ItemTemplate>
                        <HeaderStyle CssClass="cms_col_title" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="brand_id" HeaderText="الرقم">
                        <HeaderStyle CssClass="cms_col_title" />
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
        </form>
    </div>
</asp:Content>
