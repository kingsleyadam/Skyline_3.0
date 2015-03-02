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

        <asp:Panel runat="server" ID="pnlBodyStep1" Visible="false">
            <div class="panel-body order-body">
                <div class="row line-break">
                    <div class="col-xs-12">
                        <asp:Label ID="lblOrderInfoStep1" runat="server" Text="Below are the list of items within your order. You can adjust each items quantiy, remove an item, or continue to submit the order by clicking the 'Next Step' button below."></asp:Label>
                    </div>
                </div>
            </div>
            <div class="table-responsive">
                <asp:GridView ID="grdOrderItems" runat="server" CssClass="table table-striped table-condensed no-margin" AutoGenerateColumns="false" GridLines="None" DataKeyNames="ProductID" OnPreRender="grdOrderItems_PreRender" OnRowCommand="grdOrderItems_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Name" ItemStyle-Width="400px" HeaderStyle-Width="400px">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkImage" runat="server" CommandName="ProductSelected" CommandArgument='<%# Eval("ProductID") %>'>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>' />
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Image ID="imgProduct" runat="server" CssClass="img-rounded" ImageUrl='<%# Eval("Thumbnail") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="200px">
                            <ItemTemplate>
                                <div class="form-group form-inline no-margin">
                                    <div class="input-group">
                                        <asp:TextBox runat="server" ID="txtQuantity" Width="50px" Text='<%# Eval("Quantity") %>' CssClass="form-control"></asp:TextBox><div class="input-group-btn">
                                            <asp:LinkButton runat="server" ID="btnUpdate" Text="Update" CommandName="Update" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-default"></asp:LinkButton><asp:LinkButton runat="server" ID="btnRemove" CommandName="Delete" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-default"><span class="glyphicon glyphicon-trash"></span></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="UnitPrice" HeaderText="Price Per Unit"
                            HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right"
                            DataFormatString="{0:C}" ItemStyle-Width="100px"></asp:BoundField>
                        <asp:BoundField DataField="TotalPrice" HeaderText="Total Price"
                            HeaderStyle-CssClass="text-right" ItemStyle-CssClass="text-right" ItemStyle-Width="110px"
                            DataFormatString="{0:C}"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </asp:Panel>

        <asp:Panel runat="server" ID="pnlBodyStep2">
            <div class="panel-body order-body">
                <div class="row line-break">
                    <div class="col-xs-12">
                        <asp:Label ID="lblOrderInfoStep2" runat="server" Text="Please fill in your contact information by using the form below. Once you've inserted everything you can review your order before submitting by click the 'Review Order' button at the bottom."></asp:Label>
                    </div>
                </div>
                <div class="form-horizontal">
                    <div class="form-group">
                        <asp:Label ID="lblCompanyName" runat="server" Text="Company Name" CssClass="col-md-2 col-sm-3 control-label" AssociatedControlID="txtCompanyName" />
                        <div class="col-md-10 col-sm-9">
                            <asp:TextBox ID="txtCompanyName" runat="server" CssClass="form-control" placeholder="Company Name"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lblFirstName" runat="server" Text="First Name" CssClass="col-md-2 col-sm-3 control-label" AssociatedControlID="txtFirstName" />
                        <div class="col-md-10 col-sm-9">
                            <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control" placeholder="First Name"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lblLastName" runat="server" Text="Last Name" CssClass="col-md-2 col-sm-3 control-label" AssociatedControlID="txtLastName" />
                        <div class="col-md-10 col-sm-9">
                            <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control" placeholder="Last Name"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lblPhoneNumber" runat="server" Text="Phone Number" CssClass="col-md-2 col-sm-3 control-label" AssociatedControlID="txtPhoneNumber" />
                        <div class="col-md-10 col-sm-9">
                            <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control" placeholder="Phone Number (e.g. XXX-XXX-XXXX)"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lblEmailAddress" runat="server" Text="Email Address" CssClass="col-md-2 col-sm-3 control-label" AssociatedControlID="txtEmail" />
                        <div class="col-md-10 col-sm-9">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email Address"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lblAddress1" runat="server" Text="Address 1" CssClass="col-md-2 col-sm-3 control-label" AssociatedControlID="txtAddress1" />
                        <div class="col-md-10 col-sm-9">
                            <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control" placeholder="Address 1"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lblAddress2" runat="server" Text="Address 2" CssClass="col-md-2 col-sm-3 control-label" AssociatedControlID="txtAddress2" />
                        <div class="col-md-10 col-sm-9">
                            <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" placeholder="Address 2"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lblCity" runat="server" Text="City" CssClass="col-md-2 col-sm-3 control-label" AssociatedControlID="txtCity" />
                        <div class="col-md-10 col-sm-9">
                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" placeholder="City"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lblState" runat="server" Text="State" CssClass="col-md-2 col-sm-3 control-label" AssociatedControlID="txtState" />
                        <div class="col-md-10 col-sm-9">
                            <asp:TextBox ID="txtState" runat="server" CssClass="form-control" placeholder="State"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lblZipCode" runat="server" Text="Zip Code" CssClass="col-md-2 col-sm-3 control-label" AssociatedControlID="txtZipCode" />
                        <div class="col-md-10 col-sm-9">
                            <asp:TextBox ID="txtZipCode" runat="server" CssClass="form-control" placeholder="Zip Code"></asp:TextBox>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lblCountry" runat="server" Text="Country" CssClass="col-md-2 col-sm-3 control-label" AssociatedControlID="txtCountry" />
                        <div class="col-md-10 col-sm-9">
                            <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control" placeholder="Country" Text="United States" disabled=""></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>

        <asp:Panel runat="server" ID="pnlBodyStep3">
            <div class="panel-body order-body">
                <div class="row line-break">
                    <div class="col-xs-12">
                        <asp:Label ID="lblOrderInfoStep3" runat="server" Text="Please review all of the order information below. If everything is correct click 'Submit Order' button at the bottom."></asp:Label>
                    </div>
                </div>


                <div class="form-group">
                    <address>
                        <strong>
                            <asp:Label ID="lblCompanyNameStep3" runat="server" /></strong><br />
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
        </asp:Panel>

        <asp:Panel ID="pnlFooter" runat="server" CssClass="panel-footer order-footer" Visible="false">
            <div class="row">
                <div class="col-xs-6">
                    <h3><small>
                        <asp:Label ID="lblTotalHeader" runat="server" Text="Total: " /></small><asp:Label ID="lblTotal" runat="server" /></h3>
                </div>

                <asp:Panel runat="server" ID="pnlFooterStep1" CssClass="col-xs-6 text-right">
                    <asp:LinkButton ID="lnkStep1" runat="server" CssClass="btn btn-primary" CommandArgument="2" OnClick="ChangeStep_Click">Next Step</asp:LinkButton>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnlFooterStep2" CssClass="col-xs-6 text-right">
                    <div class="btn-group" role="group">
                        <asp:LinkButton ID="lnkBackStep2" runat="server" CssClass="btn btn-default" CommandArgument="1" OnClick="ChangeStep_Click">Back</asp:LinkButton>
                        <asp:LinkButton ID="lnkStep2" runat="server" CssClass="btn btn-primary" CommandArgument="3" OnClick="ChangeStep_Click">Review Order</asp:LinkButton>
                    </div>
                </asp:Panel>

                <asp:Panel ID="pnlFooterStep3" runat="server" CssClass="col-xs-6 text-right">
                    <div class="btn-group" role="group">
                        <asp:LinkButton ID="lnkBackStep3" runat="server" CssClass="btn btn-default" CommandArgument="2" OnClick="ChangeStep_Click">Back</asp:LinkButton>
                        <asp:LinkButton ID="lnkStep3" runat="server" CssClass="btn btn-primary">Submit Order</asp:LinkButton>
                    </div>
                </asp:Panel>
            </div>


        </asp:Panel>
    </asp:Panel>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScriptContent" runat="server">
</asp:Content>
