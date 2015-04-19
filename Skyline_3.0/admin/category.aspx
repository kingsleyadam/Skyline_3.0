<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="category.aspx.cs" Inherits="Skyline_3._0.admin.category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="pnlCategories" runat="server" CssClass="panel panel-default no-margin">
        <div class="panel-heading">Categories</div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-10 col-sm-9">
                    <h5><asp:Label ID="lblHelpText" runat="server" Text="Below are the list of categories, click on a category row to edit it."></asp:Label></h5>
                </div>
                <div class="col-md-2 col-sm-3">
                    <h4><asp:LinkButton ID="lbtnAddNew" runat="server" OnClick="lbtnAddNew_Click" CssClass="btn btn-plain btn-fullwidth">New Category</asp:LinkButton></h4>
                </div>
            </div>
        </div>
        <div class="table-responsive">
            <asp:GridView ID="grdCategories" runat="server" DataKeyNames="CategoryID" AllowPaging="false" AutoGenerateColumns="false" GridLines="None" CssClass="table table-striped table-hover no-margin" OnPreRender="grdCategories_PreRender" OnRowDataBound="grdCategories_RowDataBound" OnRowEditing="grdCategories_RowEditing" OnRowCancelingEdit="grdCategories_RowCancelingEdit" OnRowUpdating="grdCategories_RowUpdating" OnRowDeleting="grdCategories_RowDeleting">
                <Columns>
                    <asp:TemplateField ItemStyle-Width="105px">
                        <EditItemTemplate>
                            <div class="btn-group">
                                <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-plain btn-small" CommandName="Update">Update</asp:LinkButton>
                                <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="btn btn-plain btn-small" CommandName="Cancel"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></asp:LinkButton>
                            </div>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="false"
                                CommandName="Delete" OnClientClick="Confirm()" Text="Delete"></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="25px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="CategoryID" HeaderText="Category ID" SortExpression="CategoryID" Visible="False" ReadOnly="true"></asp:BoundField>
                    <asp:TemplateField ShowHeader="true" SortExpression="Name">
                        <HeaderTemplate>
                            <asp:Label ID="lblNameHeader" runat="server" Text="Name"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtName" runat="server" Text='<%# Eval("Name") %>' CssClass="form-control form-control-small"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Abbrev" HeaderText="Abbreviation" ReadOnly="True" SortExpression="Abbrev" ItemStyle-CssClass="text-right" HeaderStyle-CssClass="text-right" />
                    <asp:BoundField DataField="NumItems" HeaderText="Number of Products" ReadOnly="True" SortExpression="NumItems" ItemStyle-Width="150px" ItemStyle-CssClass="text-right" HeaderStyle-CssClass="text-right" />
                    <asp:TemplateField ShowHeader="true" SortExpression="IsActive" ItemStyle-Width="100px" ItemStyle-CssClass="text-right" HeaderStyle-CssClass="text-right">
                        <HeaderTemplate>
                            <asp:Label ID="lblIsActiveHeader" runat="server" Text="Is Active?"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%# Eval("IsActive") %>' Enabled="false" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%# Eval("IsActive") %>' Enabled="true" />
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                        <ItemTemplate>
                            <asp:Button runat="server" ID="EditButton" CommandName="Edit" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <SelectedRowStyle CssClass="table-select-row" />
            </asp:GridView>
        </div>
    </asp:Panel>

    <asp:Panel runat="server" ID="pnlNewCategory" CssClass="panel panel-default" Visible="false">
        <div class="panel-heading">
            <asp:Label ID="lblNewCategoryHeader" runat="server" Text="Add Category"></asp:Label>
        </div>
        <div class="panel-body">
            <div class="form-horizontal">
                <div class="form-group">
                    <asp:Label ID="lblCategoryNameHeader" runat="server" Text="Name" CssClass="col-sm-2 control-label" AssociatedControlID="txtCategoryName"></asp:Label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtCategoryName" runat="server" placeholder="Category Name" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqCategoryName" runat="server" ErrorMessage="Name Required" ControlToValidate="txtCategoryName" Display="None" ValidationGroup="AddCategory" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblAbbrevHeader" runat="server" Text="Abbreviation" CssClass="col-sm-2 control-label" AssociatedControlID="txtAbbrev"></asp:Label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtAbbrev" runat="server" placeholder="Abbreviation" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="reqAbbrev" runat="server" ErrorMessage="Abbreviation Required" ControlToValidate="txtAbbrev" Display="None" ValidationGroup="AddCategory" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <asp:ValidationSummary ID="valSummary" CssClass="valSummary" ValidationGroup="AddCategory" runat="server" />
                </div>
            </div>
        </div>

        <div class="panel-footer">
            <div class="form-horizontal">
                <div class="form-group no-margin">
                    <div class="col-sm-offset-2 col-sm-10">
                        <div class="btn-group">
                            <asp:LinkButton ID="lbtnAddCategory" runat="server" CommandName="Add" CssClass="btn btn-plain" ValidationGroup="AddCategory" OnClick="lbtnAddCategory_Click">Add Category</asp:LinkButton>
                            <asp:LinkButton ID="lbtnCancel" runat="server" CssClass="btn btn-plain" OnClick="lbtnCancel_Click">Cancel</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScriptContent" runat="server">
</asp:Content>
