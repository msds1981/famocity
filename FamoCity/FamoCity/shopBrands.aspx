<%@ Page Title="" Language="C#" MasterPageFile="~/MasterShpCms.Master" AutoEventWireup="true"
    CodeBehind="shopBrands.aspx.cs" Inherits="FamoCity.shopBrands" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            ActiveSubMnu('mnuBrands', 'submnuBrands');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="content">
        <div id="page-heading">
            <h1>
                عرض الماركات</h1>
        </div>
        <table border="0" width="100%" cellpadding="0" cellspacing="0" id="content-table">
            <tr>
                <th rowspan="3" class="sized">
                    <img src="images/shared/side_shadowright.jpg" width="20" height="300" alt="" />
                </th>
                <th class="topleft">
                </th>
                <td id="tbl-border-top">
                    &nbsp;
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
                                <asp:GridView ID="GridView1" runat="server" CssClass="product-table" 
                                    AutoGenerateColumns="False">
                                    <AlternatingRowStyle CssClass="alternate-row" />
                                    
                                    <Columns>
                                        <asp:BoundField DataField="brand_id" HeaderText="44444444">
                                        <HeaderStyle CssClass="table-header-check" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="title" HeaderText="الماركة">
                                        <HeaderStyle CssClass="table-header-repeat line-left" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="description" HeaderText="الوصف">
                                        <HeaderStyle CssClass="table-header-repeat line-left" />
                                        </asp:BoundField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <a href="" title="Edit" class="icon-1 info-tooltip"></a>
                                                <a href="" title="Delete" class="icon-2 info-tooltip"></a>
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
                    &nbsp;
                </td>
                <th class="sized bottomright">
                </th>
            </tr>
        </table>
        <div class="clear">
        </div>
    </div>
</asp:Content>
