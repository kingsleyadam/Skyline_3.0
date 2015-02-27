<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="order.aspx.cs" Inherits="Skyline_3._0.stores.order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel runat="server" ID="pnlProductsGrid" class="panel panel-default no-margin">
        <div class="panel-heading">
            <asp:Label ID="lblOrderHeader" runat="server" Text="Order Information"></asp:Label>
        </div>
        <asp:Panel runat="server" ID="pnlNoItems" class="panel-body" Visible="false">
            <asp:Label ID="lblNoItems" runat="server" Text="You currently have no items added to your order. Please go to the products page to add items."></asp:Label>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlOrderInfo" class="panel-body" Visible="false">
            <asp:Label ID="lblOrderInfo" runat="server" Text="Below are the list of items within your order. You can adjust each items quantiy, remove an item, or continue to submit the order by clicking the 'Next' button below."></asp:Label>
        </asp:Panel>
        <asp:GridView ID="grdOrderItems" runat="server" CssClass="table table-condensed no-margin" AutoGenerateColumns="false" GridLines="None" DataKeyNames="ProductID" OnPreRender="grdOrderItems_PreRender" OnRowDataBound="grdOrderItems_RowDataBound" OnRowCommand="grdOrderItems_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkImage" runat="server" CommandName="ProductSelected" CommandArgument='<%# Eval("ProductID") %>'>
                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>' />
                        </asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField ItemStyle-Width="70px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Image ID="imgProduct" runat="server" CssClass="img-rounded" ImageUrl='<%# Eval("Thumbnail") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="160px" FooterStyle-Width="160px">
                    <ItemTemplate>
                        <div class="form-group form-inline no-margin">
                            <div class="input-group">
                                <asp:TextBox runat="server" ID="txtQuantity" Width="50px" Text='<%# Eval("Quantity") %>' CssClass="form-control"></asp:TextBox><div class="input-group-btn">
                                    <asp:LinkButton runat="server" ID="btnUpdate" Text="Update" CommandName="Update" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-default"></asp:LinkButton><asp:LinkButton runat="server" ID="btnRemove" CommandName="Delete" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-default"><span class="glyphicon glyphicon-trash"></span></asp:LinkButton></div></div></div></ItemTemplate></asp:TemplateField><asp:BoundField DataField="UnitPrice" HeaderText="Price Per Unit"
                    HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right"
                    DataFormatString="{0:C}" ItemStyle-Width="120px"></asp:BoundField>
                <asp:BoundField DataField="TotalPrice" HeaderText="Total Price"
                    HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right" ItemStyle-Width="120px"
                    DataFormatString="{0:C}"></asp:BoundField>
            </Columns>
        </asp:GridView>
    </asp:Panel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScriptContent" runat="server">
</asp:Content>
