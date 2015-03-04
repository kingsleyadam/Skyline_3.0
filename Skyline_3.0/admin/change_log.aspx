<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="change_log.aspx.cs" Inherits="Skyline_3._0.admin.change_log" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="panel panel-default no-margin">
        <div class="panel-heading">Site Information</div>
        <div class="panel-body">
            <div class="row line-break">
                <div class="col-xs-12 form-horizontal">
                    <div class="col-md-1 col-sm-2">
                        <asp:Label ID="lblVersionSelectHeader" runat="server" Text="Version: " CssClass="control-label" AssociatedControlID="ddAllVersions" />
                    </div>
                    <div class="col-md-11 col-sm-10">
                        <div class="form-inline form-group-nested" role="group">
                            <asp:DropDownList ID="ddAllVersions" runat="server" AutoPostBack="true" DataValueField="IterationID" DataTextField="Name" CssClass="form-control" OnSelectedIndexChanged="ddAllVersions_SelectedIndexChanged"></asp:DropDownList>
                            <asp:LinkButton ID="btnMakeCurrent" runat="server" CssClass="btn btn-plain">Make Current</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row top-margin-row form-group line-break">
                <div class="col-md-2">
                    <div class="form-group">
                        <strong>
                            <asp:Label ID="lblVersionHeader" runat="server" Text="Version: " CssClass="control-label" /></strong>
                        <asp:Label ID="lblVersion" runat="server" CssClass="control-label" />
                    </div>

                    <div class="form-group">
                        <strong>
                            <asp:Label ID="lblFrameworkHeader" runat="server" Text="Framework: " CssClass="control-label" /></strong>
                        <asp:Label ID="lblFramework" runat="server" CssClass="control-label" />
                    </div>

                    <div class="form-group">
                        <strong>
                            <asp:Label ID="lblLanguageHeader" runat="server" Text="Language: " CssClass="control-label" /></strong>
                        <asp:Label ID="lblLanguage" runat="server" CssClass="control-label" />
                    </div>

                    <div class="form-group">
                        <strong>
                            <asp:Label ID="lblDateAppliedHeader" runat="server" Text="Date Applied: " CssClass="control-label" /></strong>
                        <asp:Label ID="lblDateApplied" runat="server" CssClass="control-label" />
                    </div>
                    <div class="form-group">
                        <strong>
                            <asp:Label ID="lblRepoHeader" runat="server" Text="Repository: " CssClass="control-label" /></strong>
                        <asp:Label ID="lblRepo" runat="server" CssClass="control-label" />
                    </div>
                </div>
                <div class="col-md-10">
                    <div class="table-responsive table-responsive-nested">
                        <asp:GridView ID="grdChangeLog" runat="server" AutoGenerateColumns="False" DataKeyNames="ChangeLogID" CssClass="table table-hover no-margin" BorderStyle="None" GridLines="None" OnPreRender="grdChangeLog_PreRender" OnRowDataBound="grdChangeLog_RowDataBound">
                            <Columns>
                                <asp:BoundField DataField="IterationID" HeaderText="IterationID"
                                    InsertVisible="False" ReadOnly="True" SortExpression="IterationID"
                                    Visible="False" />
                                <asp:BoundField DataField="ChangeLogID" HeaderText="ChangeLogID"
                                    InsertVisible="False" ReadOnly="True" SortExpression="ChangeLogID"
                                    Visible="False" />
                                <asp:BoundField DataField="ChangeTypeID" HeaderText="ChangeTypeID"
                                    SortExpression="ChangeTypeID" Visible="False" />
                                <asp:BoundField DataField="ChangeType" HeaderText="Change Type"
                                    SortExpression="ChangeType">
                                    <ItemStyle Width="120px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Description" HeaderText="Description"
                                    SortExpression="Description" />
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

            <div class="btn-group" role="group">
                <asp:LinkButton ID="btnAddNewVersion" runat="server" CssClass="btn btn-plain">Add New Version</asp:LinkButton>
                <asp:LinkButton ID="btnAddToChangeLog" runat="server" CssClass="btn btn-plain">Add to Change Log</asp:LinkButton>
            </div>

        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScriptContent" runat="server">
</asp:Content>
