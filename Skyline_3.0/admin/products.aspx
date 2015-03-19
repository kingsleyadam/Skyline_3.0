<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="products.aspx.cs" Inherits="Skyline_3._0.admin.products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel runat="server" ID="pnlProductsFilter" CssClass="panel panel-default">
        <div class="panel-body">
            <div class="row line-break filter-results">
                <div class="col-md-6 col-xs-6">
                    <asp:Label ID="lblFilterResults" runat="server" Text="Filter Results" />
                </div>
                <div class="col-md-6 col-xs-6 text-right">
                    <asp:LinkButton ID="lnkClear" runat="server" OnClick="lnkClear_Click">Clear Results</asp:LinkButton>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-3 col-md-4 col-sm-5">
                    <div class="filter-results-data">
                        <div class="btn-group btn-fullwidth">
                            <asp:LinkButton ID="lnkCategories" CssClass="btn btn-default btn-fullwidth dropdown-toggle" runat="server" data-toggle="dropdown" aria-expanded="false">Category: New Products <span class="caret"></span></asp:LinkButton><ul class="dropdown-menu" role="menu" aria-labelledby="lnkCategories">
                                <li role="presentation">
                                    <asp:LinkButton ID="lnkAllProducts" runat="server" role="menuitem" TabIndex="-1" Text="All Products" CommandArgument="1" OnClick="CategorySelected" /></li>
                                <li role="presentation">
                                    <asp:LinkButton ID="lnkNewProducts" runat="server" role="menuitem" TabIndex="-1" Text="New Products" CommandArgument="-1" OnClick="CategorySelected" /></li>
                                <li role="presentation">
                                    <asp:LinkButton ID="lnkBestSellers" runat="server" role="menuitem" TabIndex="-1" Text="Best Sellers" CommandArgument="-2" OnClick="CategorySelected" /></li>
                                <li role="presentation" class="divider"></li>
                                <asp:Repeater ID="repCategories" runat="server">
                                    <ItemTemplate>
                                        <li id="liCategory" runat="server" role="presentation">
                                            <asp:LinkButton ID="lnkMenuItem" role="menuitem" TabIndex="-1" runat="server" OnClick="CategorySelected" CommandArgument='<%# Eval("CategoryID") %>' Text='<%# Eval("Name") %>'></asp:LinkButton></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </div>
                    </div>
                </div>
                <div class="col-lg-5 col-lg-offset-4 col-md-5 col-md-offset-3 col-sm-7">
                    <div class="filter-results-data">
                        <div class="input-group">

                            <div class="input-group-btn">
                                <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-default" Text="Search: All Fields" OnClick="btnSearch_Click" />
                                <asp:LinkButton ID="lnkSearchIn" CssClass="btn btn-default dropdown-toggle" runat="server" data-toggle="dropdown" aria-expanded="false"><span class="caret"></span></asp:LinkButton><ul class="dropdown-menu" role="menu" aria-labelledby="lnkSearchIn">
                                    <li role="presentation">
                                        <asp:LinkButton ID="lnkSearchInAllFields" runat="server" role="menuitem" TabIndex="-1" Text="All Fields" CommandArgument="AllFields" OnClick="SearchInSelected" /></li>
                                    <li role="presentation" class="divider"></li>
                                    <li role="presentation">
                                        <asp:LinkButton ID="lnkSearchInName" runat="server" role="menuitem" TabIndex="-1" Text="Name" CommandArgument="Name" OnClick="SearchInSelected" /></li>
                                    <li role="presentation">
                                        <asp:LinkButton ID="lnkSearchInDescr" runat="server" role="menuitem" TabIndex="-1" Text="Description" CommandArgument="Description" OnClick="SearchInSelected" /></li>
                                    <li role="presentation">
                                        <asp:LinkButton ID="lnkSearchInProductNum" runat="server" role="menuitem" TabIndex="-1" Text="Product Number" CommandArgument="ProductNum" OnClick="SearchInSelected" /></li>
                                </ul>
                            </div>
                            <asp:TextBox ID="txtSearch" runat="server" placeholder="Search for..." CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>

    <%--<!--Gridview of Products--!>--%>

    <asp:Panel ID="pnlProducts" runat="server" CssClass="panel panel-default no-margin">
        <div class="panel-heading">Products</div>
        <div class="panel-body">
            <asp:Label ID="lblHelpText" runat="server" Text="Below are the list of products, click on a products row to select the product. Once selected you can adjust product details as well as add/remove images and categories."></asp:Label>
        </div>
        <div class="table-responsive">
            <asp:GridView ID="grdProducts" runat="server" AllowPaging="True"
                AutoGenerateColumns="False" DataKeyNames="ProductID" GridLines="None" Width="100%"
                EnableModelValidation="True" CssClass="table table-striped table-condensed table-hover table-hover-paged no-margin " OnPageIndexChanging="grdProducts_PageIndexChanging" OnPreRender="grdProducts_PreRender" OnRowDataBound="grdProducts_RowDataBound">
                <Columns>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false"
                                CommandName="Delete" OnClientClick='return confirm("Are you sure you want to delete this product?");' Text="Delete"></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="25px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="ProductID" HeaderText="ProductID"
                        InsertVisible="False" ReadOnly="True" SortExpression="ProductID"
                        Visible="False" />
                    <asp:TemplateField HeaderText="ProductNum" SortExpression="ProductNum">
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("ProductNum") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name" SortExpression="Name">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Description" SortExpression="Description">
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Price" SortExpression="Price">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("Price", "{0:N2}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity" SortExpression="Quantity">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CheckBoxField DataField="IsSoldOut" HeaderText="Sold Out"
                        SortExpression="IsSoldOut" />
                    <asp:CheckBoxField DataField="IsBestSeller" HeaderText="Best Seller"
                        SortExpression="IsBestSeller" />
                    <asp:BoundField DataField="imgURL" HeaderText="imgURL" SortExpression="imgURL"
                        Visible="False"></asp:BoundField>
                    <asp:BoundField DataField="DateAdded" HeaderText="DateAdded"
                        SortExpression="DateAdded" Visible="False" />
                    <asp:BoundField DataField="CategoryName" HeaderText="CategoryName"
                        SortExpression="CategoryName" Visible="False" />
                    <asp:BoundField DataField="CategoryName" HeaderText="CategoryName"
                        SortExpression="CategoryName" Visible="False" />
                    <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                        <ItemTemplate>
                            <asp:Button runat="server" ID="SelectButton" CommandName="Select" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" CssClass="table-pager" />
                <SelectedRowStyle CssClass="table-select-row" />
                <EmptyDataTemplate>
                    <div class="alert alert-danger noMargin">There are no products found for your search.</div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </asp:Panel>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScriptContent" runat="server">
</asp:Content>
