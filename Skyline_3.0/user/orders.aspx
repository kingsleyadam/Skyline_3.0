<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="orders.aspx.cs" Inherits="Skyline_3._0.user.orders" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default no-margin">
        <div class="panel-heading">Website Orders</div>
        <asp:Panel ID="pnlSiteOrders" runat="server" CssClass="panel-body">
            <div class="form-group no-margin">
                <p class="help-block">Below is a list of the orders placed on the website. Click on an order to view more details.</p>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlNoOrders" runat="server" CssClass="panel-body">
            <asp:Label ID="lblNoOrders" runat="server" Text="You do not have any previous website orders."></asp:Label>
        </asp:Panel>
        <div class="table-responsive">
            <asp:GridView ID="grdOrder" runat="server" DataKeyNames="OrderID" CssClass="table table-striped table-condensed table-hover no-margin" OnPreRender="grdOrder_PreRender" AutoGenerateColumns="false" GridLines="None" OnRowDataBound="grdOrder_RowDataBound" OnRowCommand="grdOrder_RowCommand">
                <Columns>
                    <asp:BoundField DataField="OrderNum" HeaderText="Order Number" ItemStyle-Width="120px" />
                    <asp:BoundField DataField="Company" HeaderText="Company" ItemStyle-Width="200px" />
                    <asp:BoundField DataField="FullName" HeaderText="Name" Visible="False" />
                    <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-Width="200px" />
                    <asp:TemplateField ItemStyle-Width="50px">
                        <HeaderTemplate>
                            <asp:Label ID="lblOrderDateHeader" runat="server" Text="Date"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblOrderDate" runat="server" Text='<%# Bind("Date", "{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="OrderStatus" HeaderText="Order Status" ItemStyle-Width="120px" HeaderStyle-CssClass="textAlignCenter" ItemStyle-CssClass="textAlignCenter" />
                    <asp:BoundField DataField="ProductCount" HeaderText="Total Products" ItemStyle-Width="120px" HeaderStyle-CssClass="textAlignRight" ItemStyle-CssClass="textAlignRight" />
                    <asp:TemplateField HeaderStyle-CssClass="textAlignRight" ItemStyle-CssClass="textAlignRight" ItemStyle-Width="70px">
                        <HeaderTemplate>
                            <asp:Label ID="lblTotalOrderPriceHeader" runat="server" Text="Price"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblTotalOrderPrice" runat="server" Text='<%# Bind("TotalOrderPrice", "{0:C2}")%>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                        <ItemTemplate>
                            <asp:Button ID="btnOrderInfo" runat="server" CommandName="OrderInfo" CommandArgument='<%# Eval("OrderID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScriptContent" runat="server">
</asp:Content>
