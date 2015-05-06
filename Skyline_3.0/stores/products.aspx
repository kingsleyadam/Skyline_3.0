<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="products.aspx.cs" Inherits="Skyline_3._0.stores.products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updatePanelSearch" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel runat="server" ID="pnlProductsFilter" CssClass="panel panel-default search">
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
                <asp:UpdateProgress ID="updateProgressSearch" runat="server">
                    <ProgressTemplate>
                        <div class="products-modal"></div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlProductsGrid" CssClass="panel panel-default no-margin products">
                <asp:Panel ID="pnlProductHeader" runat="server" CssClass="panel-heading">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="btn-group">
                                <asp:LinkButton ID="btnSortBy" runat="server" CssClass="btn btn-plain dropdown-toggle" data-toggle="dropdown" aria-expanded="false">Sort By: <strong>Most Relevant</strong> <span class="caret"></span></asp:LinkButton><ul class="dropdown-menu" role="menu" aria-labelledby="btnSortBy">
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
                        </div>
                        <div class="col-md-2 col-md-offset-8">
                        </div>
                    </div>

                </asp:Panel>
                <div class="panel-body">
                    <asp:ListView ID="lvProducts" runat="server" GroupItemCount="4" OnItemDataBound="lvProducts_ItemDataBound" ClientIDMode="AutoID">
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
                            <div class="col-md-3 col-sm-6 col-xs-12">
                                <asp:Panel ID="pnlThumnnail" runat="server" class="thumbnail">
                                    <asp:LinkButton ID="btnSelectImage" runat="server" CommandName="ProductSelected" CommandArgument='<%# Eval("ProductID")%>' OnClick="ProductSelected">
                                        <asp:Image ID="imgProduct" ImageUrl='<%# Eval("imgThumb") %>' runat="server" CssClass="img-rounded" />
                                    </asp:LinkButton>
                                    <div class="caption">
                                        <h4>
                                            <asp:LinkButton ID="btnSelect" runat="server" Text='<%# Eval("Name") %>' CommandName="ProductSelected" CommandArgument='<%# Eval("ProductID")%>' OnClick="ProductSelected" />
                                        </h4>
                                        <p>
                                            <div id="divProductNum" runat="server">
                                                <asp:Label ID="lblProductNumLabel" runat="server">Product #: </asp:Label><asp:Label ID="lblProductNum" runat="server" Text='<%# Eval("ProductNum") %>' />
                                            </div>

                                            <div id="divImageCount" runat="server">
                                                <asp:Label ID="lblImageCountLabel" runat="server">Number Of Images: </asp:Label><asp:Label ID="lblImageCount" runat="server" Text='<%# Eval("ImageCount") %>' />
                                            </div>

                                            <div id="divAvailable" runat="server">
                                                <asp:Label ID="lblAvailableLabel" runat="server" Text="Label">Availability: </asp:Label><asp:Label ID="lblAvailable" runat="server" Text='<%# Eval("Availability") %>' />
                                            </div>

                                            <asp:Panel runat="server" ID="pnlPrice">
                                                <asp:Label ID="lblPriceLabel" runat="server" Text="Price: " />
                                                <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price", "{0:c}") %>' />
                                            </asp:Panel>
                                        </p>
                                        <asp:Panel runat="server" ID="pnlAdd2Order" CssClass="input-group" DefaultButton="btnAdd2Cart">
                                            <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" Text="1"></asp:TextBox><span class="input-group-btn"><asp:LinkButton ID="btnAdd2Cart" runat="server" CssClass="btn btn-default" CommandName="Add2Cart" Text="Add To Order" Width="130px" OnClick="Add2Cart" CommandArgument='<%# Eval("ProductID")%>' />
                                            </span>
                                        </asp:Panel>
                                        <asp:HiddenField ID="hdnProductID" runat="server" Value='<%# Eval("ProductID") %>' />
                                        <asp:HiddenField ID="hdnPrice" runat="server" Value='<%# Eval("Price") %>' />
                                        <asp:HiddenField ID="hdnIsBestSeller" runat="server" Value='<%# Eval("IsBestSeller") %>' />
                                        <asp:HiddenField ID="hdnImgThumb" runat="server" Value='<%# Eval("imgThumb") %>' />
                                        <asp:HiddenField ID="hdnImgURL" runat="server" Value='<%# Eval("imgURL") %>' />
                                        <asp:HiddenField ID="hdnImgOrig" runat="server" Value='<%# Eval("imgOrig") %>' />
                                        <asp:HiddenField ID="hdnDescription" runat="server" Value='<%# Eval("Description") %>' />
                                        <asp:HiddenField ID="hdnBestSeller" runat="server" Value='<%# Eval("IsBestSeller") %>' />
                                        <asp:HiddenField ID="hdnSoldOut" runat="server" Value='<%# Eval("IsSoldOut") %>' />
                                    </div>
                                </asp:Panel>

                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </div>
                <asp:Panel ID="pnlPageSelect" runat="server" CssClass="panel-footer">
                    <div class="row">
                        <div class="col-md-4 col-sm-4 col-xs-12 page-btn text-center">
                            <asp:LinkButton ID="lnkFirstPage" runat="server" CommandName="ChangePage" CommandArgument="FirstPage" Text="First" OnClick="ChangePage" />
                            <asp:LinkButton ID="lnkPrevPage" runat="server" CommandName="ChangePage" CommandArgument="PreviousPage" OnClick="ChangePage" CssClass="page-btn-primary"><span aria-hidden="true">&larr;</span> Previous</asp:LinkButton>
                        </div>
                        <div class="col-md-4 col-sm-4 col-xs-12 page-row text-center">
                            <asp:Label ID="lblPageInfo1" runat="server" />
                            <div class="page-btn dropup">
                                <asp:LinkButton ID="lnkPageDropDown" CssClass=" dropdown-toggle" runat="server" data-toggle="dropdown" aria-expanded="false">1 <span class="caret"></span></asp:LinkButton><ul class="dropdown-menu" role="menu" aria-labelledby="lnkPageDropDown">
                                    <asp:Repeater ID="repPages" runat="server">
                                        <ItemTemplate>
                                            <li id="liPage" runat="server" role="presentation">
                                                <asp:LinkButton ID="lnkPage" role="menuitem" TabIndex="-1" runat="server" OnClick="SelectPage" CommandArgument='<%# Eval("Page") %>' Text='<%# Eval("Page") %>'></asp:LinkButton></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>
                            <asp:Label ID="lblPageInfo2" runat="server" />
                        </div>
                        <div class="col-md-4 col-md-offset-0 col-sm-4 col-sm-offset-0 col-xs-12 col-xs-offset-0 page-btn page-row text-center">
                            <asp:LinkButton ID="lnkNextPage" runat="server" CommandName="ChangePage" CommandArgument="NextPage" OnClick="ChangePage" CssClass="page-btn-primary">Next <span aria-hidden="true">&rarr;</span></asp:LinkButton><asp:LinkButton ID="lnkLastPage" runat="server" Text="Last" CommandName="ChangePage" CommandArgument="LastPage" OnClick="ChangePage" />
                        </div>
                    </div>
                </asp:Panel>
                <asp:UpdateProgress ID="updateProgressProducts" runat="server">
                    <ProgressTemplate>
                        <div class="products-modal"></div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </asp:Panel>


            <asp:Panel ID="pnlProductInfo" runat="server" Visible="false" CssClass="panel panel-default products">
                <div class="panel-heading">
                    <asp:Label ID="lblProductInfo" runat="server" Text="Product Information" />
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="form-group">
                                <asp:HyperLink ID="lnkProductImage" runat="server" Target="_blank">
                                    <asp:Image ID="imgProductImg" runat="server" CssClass="img-rounded" />
                                </asp:HyperLink>
                            </div>
                            <asp:Panel ID="pnlPrice" runat="server">
                                <h3>
                                    <asp:Label ID="lblProductInfoPrice" runat="server" />
                                    <asp:HiddenField ID="hdnProductInfoPrice" runat="server" />
                                </h3>
                            </asp:Panel>

                            <div class="form-group">
                                <asp:Panel runat="server" ID="pnlProductInfoAdd2Order" CssClass="input-group" DefaultButton="btnAdd2CartFromInfo">
                                    <asp:TextBox ID="txtQuantityFromInfo" runat="server" CssClass="form-control" Text="1"></asp:TextBox><span class="input-group-btn"><asp:LinkButton ID="btnAdd2CartFromInfo" runat="server" CssClass="btn btn-default" CommandName="Add2CartFromInfo" Text="Add To Order" Width="130px" OnClick="Add2Cart" />
                                    </span>
                                </asp:Panel>
                            </div>
                        </div>
                        <div class="col-md-8">
                            <asp:Panel runat="server" ID="pnlSoldOut" class="alert alert-danger" role="alert">
                                <asp:Label ID="lblSoldOut" runat="server" Text="Sorry, this item is currently sold out." />
                            </asp:Panel>
                            <h3>
                                <asp:Label ID="lblProductName" runat="server" />
                            </h3>
                            <h4>
                                <asp:Label ID="lblProductNumLabel" runat="server" Text="Product #:" />
                                <asp:Label ID="lblProductNum" runat="server" /></h4>
                            <h4>
                                <asp:Label ID="lblProductDescription" runat="server" /></h4>

                            <div class="row">
                                <asp:Repeater ID="repProductImages" runat="server">
                                    <ItemTemplate>
                                        <div class="col-xs-6 col-md-3">
                                            <asp:HyperLink ID="lnkFullImage" runat="server" NavigateUrl='<%# Eval("imgFull") %>' CssClass="thumbnail product-info-thumbnail" Target="_blank">
                                                <asp:Image ID="imgOtherImages" runat="server" ImageUrl='<%# Eval("imgThumb") %>' />
                                            </asp:HyperLink>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <asp:Panel ID="pnlBestSeller" runat="server" CssClass="well well-sm well-bestseller">
                                <asp:Label ID="lblProductInfoBestSeller" runat="server" Text="This item is one of our best sellers." />
                            </asp:Panel>
                        </div>
                    </div>
                </div>
                <div class="panel-footer">
                    <asp:LinkButton ID="btnBack" runat="server" CssClass="btn btn-plain" OnClick="btnBack_Click" Text="Back To All Items" />
                </div>
                <asp:UpdateProgress ID="updateProgressProductInfo" runat="server">
                    <ProgressTemplate>
                        <div class="products-modal"></div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScriptContent" runat="server">
</asp:Content>
