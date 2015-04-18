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
            <div class="row">
                <div class="col-md-10 col-sm-9">
                    <h5><asp:Label ID="lblHelpText" runat="server" Text="Below are the list of products, click on a products row to select the product. Once selected you can adjust product details as well as add/remove images and categories."></asp:Label></h5>
                </div>
                <div class="col-md-2 col-sm-3">
                    <h4><asp:LinkButton ID="lbtnAddNew" runat="server" OnClick="lbtnAddNew_Click" CssClass="btn btn-plain btn-fullwidth">New Product</asp:LinkButton></h4>
                </div>
            </div>
            
        </div>
        <div class="table-responsive">
            <asp:GridView ID="grdProducts" runat="server" AllowPaging="True"
                AutoGenerateColumns="False" DataKeyNames="ProductID" GridLines="None" Width="100%"
                EnableModelValidation="True" CssClass="table table-striped table-condensed table-hover no-margin" OnPageIndexChanging="grdProducts_PageIndexChanging" OnPreRender="grdProducts_PreRender" OnRowDataBound="grdProducts_RowDataBound" OnSelectedIndexChanged="grdProducts_SelectedIndexChanged" OnDataBound="grdProducts_DataBound" OnRowDeleting="grdProducts_RowDeleting">
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
                            <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
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
                    <div class="alert alert-danger no-margin">There are no products found for your search.</div>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>
    </asp:Panel>

    <%--Specific Product Information--%>

    <asp:Panel runat="server" ID="pnlProductInfo" CssClass="panel panel-default" Visible="false">
        <div class="panel-heading">
            <asp:Label ID="lblProductInformation" runat="server" Text="Product Information: "></asp:Label>
            <asp:Label ID="lblProductName" runat="server" />
        </div>
        <div class="panel-body">
            <div class="form-horizontal">
                <div class="form-group">
                    <asp:Label ID="lblProductNameHeader" runat="server" Text="Name" CssClass="col-sm-2 control-label" AssociatedControlID="txtProductName"></asp:Label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtProductName" runat="server" placeholder="Product Name" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqProductName" runat="server" ErrorMessage="Name Required" ControlToValidate="txtProductName" Display="None" ValidationGroup="UpdateInfo" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblProductNumHeader" runat="server" Text="Number" CssClass="col-sm-2 control-label" AssociatedControlID="txtProductNum"></asp:Label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtProductNum" runat="server" placeholder="Product Number" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqProductNum" runat="server" ErrorMessage="Number Required" ControlToValidate="txtProductNum" Display="None" ValidationGroup="UpdateInfo" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblDescriptionHeader" runat="server" Text="Description" CssClass="col-sm-2 control-label" AssociatedControlID="txtDescription"></asp:Label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtDescription" runat="server" placeholder="Product Description" CssClass="form-control" TextMode="MultiLine" Height="100px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqProductDescription" runat="server" ErrorMessage="Description Required" ControlToValidate="txtDescription" Display="None" ValidationGroup="UpdateInfo" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblPriceHeader" runat="server" Text="Price" CssClass="col-sm-2 control-label" AssociatedControlID="txtPrice"></asp:Label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtPrice" runat="server" placeholder="Product Price" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblQuantityHeader" runat="server" Text="Quantity" CssClass="col-sm-2 control-label" AssociatedControlID="txtQuantity"></asp:Label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtQuantity" runat="server" placeholder="Product Quantity" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <div class="checkbox checkbox-inline">
                            <asp:CheckBox ID="chkSoldOut" runat="server" Text="Is Sold Out?" />
                        </div>
                        <div class="checkbox checkbox-inline">
                            <asp:CheckBox ID="chkBestSeller" runat="server" Text="Is Best Seller?" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <asp:ValidationSummary ID="valSummary" CssClass="valSummary" ValidationGroup="UpdateInfo" runat="server" />
                </div>
                <asp:Panel ID="pnlUpdateSuccess" runat="server" CssClass="col-xs-12" Visible="false">
                    <div class="alert alert-success no-margin">
                        <asp:Label ID="lblSuccess" runat="server"><strong>Success!</strong> The project has been successfully added/updated.</asp:Label>
                    </div>
                </asp:Panel>
            </div>
        </div>

        <div class="panel-footer">
            <div class="form-horizontal">
                <div class="form-group no-margin">
                    <div class="col-sm-offset-2 col-sm-10">
                        <div class="btn-group">
                            <asp:LinkButton ID="lbtnUpdate" runat="server" CommandName="Update" CssClass="btn btn-plain" OnClick="lbtnUpdate_Click" ValidationGroup="UpdateInfo">Update</asp:LinkButton>
                            <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="btn btn-plain" OnClick="lbtnCancel_Click">Cancel</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>


    <asp:Panel runat="server" ID="pnlProductImages" CssClass="panel panel-default" Visible="false">
        <div class="panel-heading">
            <asp:Label ID="lblImageHeader" runat="server" Text="Images"></asp:Label>
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-xs-12">
                    <asp:Label ID="lblImageInfo" runat="server" Text="Tap or click an image to delete it." CssClass="help-text"></asp:Label>
                </div>
            </div>
            <div class="row">
                <asp:Repeater ID="repImages" runat="server" OnItemDataBound="repImages_ItemDataBound" OnItemCommand="repImages_ItemCommand">
                    <ItemTemplate>
                        <div class="col-xs-12 col-sm-3 col-md-3 col-lg-2">
                            <asp:HiddenField ID="hdnProductID" runat="server" Visible="false" Value='<%# Eval("ProductID") %>' />
                            <asp:HiddenField ID="hdnImageID" runat="server" Visible="false" Value='<%# Eval("ImageID") %>' />
                            <asp:HiddenField ID="hdnImageName" runat="server" Visible="false" Value='<%# Eval("imgName") %>' />
                            <asp:HiddenField ID="hdnIsDefault" runat="server" Visible="false" Value='<%# Eval("IsDefault") %>' />
                            <div class="thumbnail">
                                <asp:LinkButton ID="lnkImage" runat="server" CommandName="Delete" OnClientClick="Confirm()">
                                    <div class="image">
                                        <asp:Image ID="imgProductImage" runat="server" ImageUrl='<%# Eval("imgThumb") %>' />
                                        <asp:Panel ID="pnlOverlay" runat="server" CssClass="overlay"><span class="glyphicon glyphicon-remove"></span></asp:Panel>
                                    </div>
                                </asp:LinkButton>
                                <div class="caption">
                                    <p class="no-margin">
                                        <asp:LinkButton ID="lbtnUploadImage" runat="server" CssClass="btn btn-plain btn-fullwidth" Visible="false">Upload Image</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnMakeDefault" runat="server" CssClass="btn btn-plain btn-fullwidth" CommandName="MakeDefault">Make Default</asp:LinkButton>
                                    </p>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>

                <asp:FileUpload ID="fileUploadImage" runat="server" type="file" EnableTheming="true" CssClass="hide-object" OnChange="UploadFileNow()" />
            </div>
            <asp:Panel ID="pnlImageUploadStatus" runat="server" CssClass="row" Visible="false">
                <div class="col-xs-12">
                    <div class="alert alert-danger no-margin">
                        <asp:Label ID="lblImageUploadMessage" runat="server"><strong>Error!</strong></asp:Label>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </asp:Panel>

    <asp:Panel runat="server" ID="pnlProductCategories" CssClass="panel panel-default no-margin" Visible="false">
        <div class="panel-heading">
            <asp:Label ID="lblCategoryHeader" runat="server" Text="Categories"></asp:Label>
        </div>
        <div class="panel-body">
            <div class="list-group no-margin">
                <asp:Repeater ID="repProductCategories" runat="server" OnItemDataBound="repProductCategories_ItemDataBound" OnItemCommand="repProductCategories_ItemCommand">
                    <ItemTemplate>
                        <asp:HiddenField ID="hdnAssigned" runat="server" Value='<%# Eval("Assigned") %>' Visible="false" />
                        <asp:HiddenField ID="hdnProductID" runat="server" Value='<%# Eval("ProductID") %>' Visible="false" />
                        <asp:LinkButton ID="lbtnCategory" runat="server" CssClass="list-group-item" Text='<%# Eval("Name") %>' CommandArgument='<%# Eval("CategoryID") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </asp:Panel>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScriptContent" runat="server">
    <script type="text/javascript">
        function OpenFileUpload() {
            var ctlFileUpload = document.getElementById('<%=fileUploadImage.ClientID %>');
            ctlFileUpload.click();
        }
        function UploadFileNow() {
            var value = document.getElementById('<%=fileUploadImage.ClientID %>').valueOf;
            if (value != '') {
                $("#form1").submit();
            }
        };
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Are you sure you want to delete that photo?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <script type="text/javascript">

    </script>
</asp:Content>
