<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="glossary.aspx.cs" Inherits="Skyline_3._0.admin.glossary" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default no-margin">
        <div class="panel-heading">Website Text</div>
        <div class="panel-body">
            <div class="row form-group line-break">
                <div class="col-xs-12 form-horizontal">
                    <div class="col-md-1 col-sm-2">
                        <asp:Label ID="lblPageSelectionHeader" runat="server" Text="Page: " CssClass="control-label" AssociatedControlID="ddPage" />
                    </div>
                    <div class="col-md-11 col-sm-10">
                        <div class="form-inline form-group-nested" role="group">
                            <asp:DropDownList ID="ddPage" runat="server" AutoPostBack="true" DataValueField="PageID" DataTextField="PageName" CssClass="form-control" OnSelectedIndexChanged="ddPage_SelectedIndexChanged"></asp:DropDownList>
                            <asp:LinkButton ID="btnAddText" runat="server" CssClass="btn btn-plain" OnClick="btnAddText_Click">Add New Text</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

            <asp:Panel runat="server" ID="pnlAddEditGlossaryText" class="row line-break" Visible="false" DefaultButton="btnUpdate">
                <div class="col-xs-12 form-horizontal">
                    <div class="form-group">
                        <asp:Label ID="lblPage" runat="server" CssClass="col-sm-2 control-label" Text="Page"></asp:Label>
                        <div class="col-sm-10">
                            <asp:DropDownList ID="ddPageSelection" runat="server" DataValueField="PageID" DataTextField="PageName" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                            <asp:TextBox ID="txtGlossaryText" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblDispOrder" runat="server" Text="Display Order" CssClass="col-sm-2 control-label" AssociatedControlID="txtDispOrder" />
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtDispOrder" runat="server" CssClass="form-control" placeholder="Display Order"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblLocation" runat="server" Text="Location" CssClass="col-sm-2 control-label" AssociatedControlID="txtLocation" />
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtLocation" runat="server" CssClass="form-control" placeholder="Location"></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-sm-offset-2 col-sm-10 btn-group">
                            <asp:LinkButton ID="btnUpdate" runat="server" CssClass="btn btn-primary" type="submit" OnClick="btnUpdate_Click" CommandName="Update">Update</asp:LinkButton>
                            <asp:LinkButton ID="btnCancel" runat="server" CssClass="btn btn-plain" OnClick="btnCancel_Click">Cancel</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <div class="table-responsive table-responsive-nested">
            <asp:GridView ID="grdGlossary" runat="server" AutoGenerateColumns="False"
                DataKeyNames="GlossID" GridLines="None" CssClass="table table-hover no-margin" OnPreRender="grdGlossary_PreRender" OnRowDataBound="grdGlossary_RowDataBound" OnSelectedIndexChanged="grdGlossary_SelectedIndexChanged" OnRowDeleting="grdGlossary_RowDeleting">
                <Columns>
                    <asp:TemplateField ShowHeader="False" ItemStyle-CssClass="no-pointer">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkDeleteButton" runat="server" CausesValidation="false"
                                CommandName="Delete" OnClientClick='return confirm("Are you sure you want to delete this website text?");' Text="Delete"></asp:LinkButton>
                        </ItemTemplate>
                        <HeaderStyle Width="25px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="GlossID" HeaderText="GlossID" InsertVisible="False"
                        ReadOnly="True" SortExpression="GlossID" Visible="False" />
                    <asp:BoundField DataField="PageID" HeaderText="PageID" SortExpression="PageID"
                        Visible="False" />
                    <asp:BoundField DataField="Text" HeaderText="Text" SortExpression="Text" />
                    <asp:BoundField DataField="DispOrder" HeaderText="Display Order"
                        SortExpression="DispOrder">
                        <HeaderStyle Width="120px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Location" HeaderText="Location"
                        SortExpression="Location">
                        <HeaderStyle Width="25px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                        <ItemTemplate>
                            <asp:Button runat="server" ID="SelectButton" CommandName="Select" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <SelectedRowStyle CssClass="table-select-row" />
            </asp:GridView>
        </div>
        </div>

        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScriptContent" runat="server">
    <script src="../ckeditor/ckeditor.js"></script>

    <script type="text/javascript">
        CKEDITOR.replace(document.getElementById('<%=txtGlossaryText.ClientID %>'));
    </script>
</asp:Content>
