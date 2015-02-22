<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="orders_info.aspx.cs" Inherits="Skyline_3._0.user.orders_info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default no-margin">
        <div class="panel-heading">Order Information</div>
        <asp:Panel ID="pnlNoOrders" runat="server" CssClass="panel-body" Visible="false">
            <asp:Label ID="lblNoOrders" runat="server" Text="We could not find any information for this order."></asp:Label>
        </asp:Panel>
        <asp:Panel ID="pnlSiteOrders" runat="server" CssClass="panel-body no-bottom-padding">
            <div class="form-group line-break">
                <h1><small>
                    <asp:Label ID="lblOrderNumLabel" runat="server" Text="Your order number is: " /></small>
                    <asp:Label ID="lblOrderNum" runat="server" Text="Label"></asp:Label>
                </h1>
            </div>
            <div class="form-group line-break">
                <div class="form-group">
                    <asp:Label ID="lblContactInfoHeader" runat="server" Text="Contact Information"></asp:Label>
                </div>
                <div class="form-group">
                    <address>
                        <strong>
                            <asp:Label ID="lblCompanyName" runat="server" /></strong><br />
                        <asp:Label ID="lblStreetAddress" runat="server" /><br />
                        <asp:Label ID="lblCountryInfo" runat="server" /><br />
                        <asp:Label ID="lblPhone" runat="server" />
                    </address>

                    <address>
                        <strong>
                            <asp:Label ID="lblFullName" runat="server" /></strong><br />
                        <asp:Label ID="lblEmail" runat="server" />
                    </address>

                </div>
            </div>
            <div class="form-group">
                <asp:Label ID="lblItemsInOrder" runat="server" Text="Products in Order"></asp:Label>
            </div>
        </asp:Panel>
        <div class="table-responsive">
            <asp:GridView ID="grdOrderItems" runat="server" DataKeyNames="ProductID" CssClass="table table-striped table-condensed no-margin" OnPreRender="grdOrder_PreRender" AutoGenerateColumns="false" GridLines="None" ShowFooter="true" OnRowDataBound="grdOrderItems_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="ProductNum" HeaderText="Product Number" ItemStyle-Width="120px" />
                    <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Width="200px" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" ItemStyle-Width="120px" ItemStyle-CssClass="text-right" HeaderStyle-CssClass="text-right" />
                    <asp:TemplateField HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right" ItemStyle-Width="70px">
                        <HeaderTemplate>
                            <asp:Label ID="lblTotalPriceHeader" runat="server" Text="Price"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTotalPrice" runat="server" Text='<%# Bind("TotalPrice", "{0:C2}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScriptContent" runat="server">
</asp:Content>
