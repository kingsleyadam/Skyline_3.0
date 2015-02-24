<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="products.aspx.cs" Inherits="Skyline_3._0.stores.products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default">
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
                            <asp:LinkButton ID="lnkCategories" CssClass="btn btn-default btn-fullwidth dropdown-toggle" runat="server" data-toggle="dropdown" aria-expanded="false">Category: All Products <span class="caret"></span></asp:LinkButton>
                            <ul class="dropdown-menu" role="menu" aria-labelledby="lnkCategories">
                                <li role="presentation">
                                    <asp:LinkButton ID="lnkNewProducts" runat="server" role="menuitem" TabIndex="-1" Text="New Products" CommandArgument="-1" OnClick="CategorySelected" /></li>
                                <li role="presentation">
                                    <asp:LinkButton ID="lnkBestSellers" runat="server" role="menuitem" TabIndex="-1" Text="Best Sellers" CommandArgument="-2" OnClick="CategorySelected" /></li>
                                <li role="presentation">
                                    <asp:LinkButton ID="lnkAllProducts" runat="server" role="menuitem" TabIndex="-1" Text="All Products" CommandArgument="1" OnClick="CategorySelected" /></li>
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
                                <asp:LinkButton ID="lnkSearchIn" CssClass="btn btn-default dropdown-toggle" runat="server" data-toggle="dropdown" aria-expanded="false"><span class="caret"></span></asp:LinkButton>
                                <ul class="dropdown-menu" role="menu" aria-labelledby="lnkSearchIn">
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
    </div>
    <div class="panel panel-default no-margin">
        <asp:Panel ID="pnlProductHeader" runat="server" CssClass="panel-heading">
            <div class="btn-group">
                <asp:LinkButton ID="btnSortBy" runat="server" CssClass="btn btn-plain dropdown-toggle" data-toggle="dropdown" aria-expanded="false">Sort By: <strong>Most Relevant</strong> <span class="caret"></span></asp:LinkButton>
                <ul class="dropdown-menu" role="menu" aria-labelledby="btnSortBy">
                    <li role="presentation">
                        <asp:LinkButton ID="btnSortMostRelevant" runat="server" role="menuitem" TabIndex="-1" Text="Most Relevant" CommandArgument="Default" OnClick="SortSelected" /></li>
                    <li role="presentation">
                        <asp:LinkButton ID="btnNameASC" runat="server" role="menuitem" TabIndex="-1" Text="Name Ascending" CommandArgument="Name ASC" OnClick="SortSelected" /></li>
                    <li role="presentation">
                        <asp:LinkButton ID="btnNameDSC" runat="server" role="menuitem" TabIndex="-1" Text="Name Descending" CommandArgument="Name DESC" OnClick="SortSelected" /></li>
                    <li role="presentation">
                        <asp:LinkButton ID="btnPriceASC" runat="server" role="menuitem" TabIndex="-1" Text="Price Lowest To Highest" CommandArgument="Price ASC" OnClick="SortSelected" /></li>
                    <li role="presentation">
                        <asp:LinkButton ID="btnPriceDSC" runat="server" role="menuitem" TabIndex="-1" Text="Price Highest To Lowest" CommandArgument="Price DESC" OnClick="SortSelected" /></li>
                    <li role="presentation">
                        <asp:LinkButton ID="btnDateAddedDSC" runat="server" role="menuitem" TabIndex="-1" Text="Date Added Newest To Oldest" CommandArgument="DateAdded DESC" OnClick="SortSelected" /></li>
                    <li role="presentation">
                        <asp:LinkButton ID="btnDateAddedASC" runat="server" role="menuitem" TabIndex="-1" Text="Date Added Oldest To Newest" CommandArgument="DateAdded ASC" OnClick="SortSelected" /></li>
                </ul>
            </div>
        </asp:Panel>
        <div class="panel-body">
            <asp:ListView ID="lvProducts" runat="server" GroupItemCount="4">
                <LayoutTemplate>
                    <div id="groupPlaceholderContainer" runat="server">
                        <div id="groupPlaceholder" runat="server">
                        </div>
                    </div>
                </LayoutTemplate>
                <GroupTemplate>
                    <div id="itemPlaceholderContainer" runat="server" class="row">
                        <td id="itemPlaceholder" runat="server"></td>
                    </div>
                </GroupTemplate>
                <EmptyDataTemplate>
                    <div class="form-group no-margin">There are no products found for this query.</div>
                </EmptyDataTemplate>
                <EmptyItemTemplate>
                    <td runat="server" />
                </EmptyItemTemplate>
                <ItemTemplate>
                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                        <div class="thumbnail">
                            <asp:LinkButton ID="btnSelectImage" runat="server" CommandName="ProductSelected" CommandArgument='<%# Eval("ProductID")%>'>
                                <asp:Image ID="imgProduct" ImageUrl='<%# Eval("imgThumb") %>' runat="server" CssClass="img-rounded" />
                            </asp:LinkButton>
                            <div class="caption">
                                <h4>
                                    <asp:LinkButton ID="btnSelect" runat="server" Text='<%# Eval("Name") %>' CommandName="ProductSelected" CommandArgument='<%# Eval("ProductID")%>' />
                                </h4>
                                <p>
                                    <div id="divProductNum" runat="server">
                                        <asp:Label ID="lblProductNum" runat="server">Product #:</asp:Label>
                                        <asp:Label ID="ProductNumLabel" runat="server" Text='<%# Eval("ProductNum") %>' />

                                    </div>

                                    <div id="divImageCount" runat="server">
                                        <asp:Label ID="lblImageCountLabel" runat="server">Number Of Images:</asp:Label>
                                        <asp:Label ID="lblImageCount" runat="server" Text='<%# Eval("ImageCount") %>' />
                                    </div>

                                    <div id="divAvailable" runat="server">
                                        <asp:Label ID="lblAvailableLabel" runat="server" Text="Label">Availability:</asp:Label>
                                        <asp:Label ID="lblAvailable" runat="server" Text='<%# Eval("Availability") %>' />
                                    </div>

                                    <div id="divPrice" runat="server">
                                        <asp:Label ID="lblPriceLabel" runat="server" Text="Price:" />
                                        <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price", "{0:c}") %>' />
                                    </div>
                                </p>

                                <div class="input-group">
                                    <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" Text="1"></asp:TextBox>
                                    <span class="input-group-btn">
                                        <asp:LinkButton ID="btnAdd2Cart" runat="server" CssClass="btn btn-default" CommandName="Add2Cart" Text="Add To Order" Width="130px" CommandArgument='<%# Eval("ProductID")%>' />
                                    </span>
                                </div>
                                <asp:Label ID="lblAddItemStatus" runat="server" Visible="false" Font-Size="12px" />
                            </div>
                        </div>

                    </div>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <asp:Panel ID="pnlPageSelect" runat="server" CssClass="panel-footer">
            <div class="row">
                <div class="col-md-1 col-md-offset-1 col-sm-1 col-xs-6 text-center page-btn small page-row">
                    <asp:LinkButton ID="lnkFirstPage" runat="server" CommandName="ChangePage" CommandArgument="FirstPage" Text="First" OnClick="ChangePage" />
                </div>
                <div class="col-md-2 col-sm-3 col-xs-6 text-center page-btn page-row">
                    <asp:LinkButton ID="lnkPrevPage" runat="server" CommandName="ChangePage" CommandArgument="PreviousPage" OnClick="ChangePage" CssClass="page-btn-primary"><span aria-hidden="true">&larr;</span> Previous</asp:LinkButton>
                </div>
                <div class="col-md-4 col-sm-3 col-xs-12 text-center page-info page-row">
                    <asp:Label ID="lblPageInfo" runat="server" />
                </div>
                <div class="col-md-2 col-sm-3 col-xs-6 text-center page-btn page-row">
                    <asp:LinkButton ID="lnkNextPage" runat="server" CommandName="ChangePage" CommandArgument="NextPage" OnClick="ChangePage" CssClass="page-btn-primary">Next <span aria-hidden="true">&rarr;</span></asp:LinkButton>
                </div>
                <div class="col-md-1 col-sm-1 col-xs-6 text-center page-btn small page-row">
                    <asp:LinkButton ID="lnkLastPage" runat="server" Text="Last" CommandName="ChangePage" CommandArgument="LastPage" OnClick="ChangePage" />
                </div>
            </div>
        </asp:Panel>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScriptContent" runat="server">
</asp:Content>
