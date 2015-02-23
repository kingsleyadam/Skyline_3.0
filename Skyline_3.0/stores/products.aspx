<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="products.aspx.cs" Inherits="Skyline_3._0.stores.products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default no-margin">
        <div class="panel-heading">Skyline Products</div>
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
                    <div class="col-md-3">
                        <div class="thumbnail">
                            <asp:LinkButton ID="btnSelectImage" runat="server" CommandName="ProductSelected" CommandArgument='<%# Eval("ProductID")%>'>
                                <asp:Image ID="imgProduct" ImageUrl='<%# Eval("imgURL") %>' runat="server" CssClass="img-rounded" />
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
        <div class="panel-footer">
            <asp:Panel ID="pnlPageSelect" runat="server">
                <nav>
                    <ul class="pagination pagination-sm no-margin">
                        <asp:PlaceHolder ID="phFirstPage" runat="server">
                            <li>
                                <asp:LinkButton ID="lnkFirstPage" runat="server" CommandName="ChangePage" CommandArgument="FirstPage" Text="First" OnClick="ChangePage" /></li>
                        </asp:PlaceHolder>

                        <asp:PlaceHolder ID="phPrevPage" runat="server">
                            <li>
                                <asp:LinkButton ID="lnkPrevPage" runat="server" CommandName="ChangePage" CommandArgument="PreviousPage" Text="Previous" OnClick="ChangePage" /></li>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="phPagePrevGrp" runat="server">
                            <li>
                                <asp:LinkButton ID="lnkPagePrevGrp" runat="server" CommandName="ChangeGrp" CommandArgument="PrevGrp" Text="..." OnClick="ChangePage" /></li>
                        </asp:PlaceHolder>
                        <asp:Repeater ID="repPager" runat="server">
                            <ItemTemplate>
                                <li id="liPageNum" runat="server">
                                    <asp:LinkButton ID="lnkPageNum" runat="server" Text='<%# Eval("Page")%>' CommandArgument='<%# Eval("Page")%>' CommandName="ChangePage" OnClick="ChangePage" />
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:PlaceHolder ID="phPageNextGrp" runat="server">
                            <li>
                                <asp:LinkButton ID="lnkPageNextGrp" runat="server" Text="..." CommandName="ChangeGrp" CommandArgument="NextGrp" OnClick="ChangePage" /></li>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="phNextPage" runat="server">
                            <li>
                                <asp:LinkButton ID="lnkNextPage" runat="server" Text="Next" CommandName="ChangePage" CommandArgument="NextPage" OnClick="ChangePage" /></li>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder ID="phLastPage" runat="server">
                            <li>
                                <asp:LinkButton ID="lnkLastPage" runat="server" Text="Last" CommandName="ChangePage" CommandArgument="LastPage" OnClick="ChangePage" /></li>
                        </asp:PlaceHolder>
                    </ul>
                </nav>
            </asp:Panel>

        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScriptContent" runat="server">
</asp:Content>
