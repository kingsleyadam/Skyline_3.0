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
                            <asp:LinkButton ID="btnMakeCurrent" runat="server" CssClass="btn btn-plain" OnClientClick = "Confirm()" OnClick="btnMakeCurrent_Click">Make Current</asp:LinkButton>
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
                        <asp:HyperLink ID="lnkRepo" runat="server" Target="_blank" />
                        <asp:Label ID="lblRepo" runat="server" CssClass="control-label" />
                    </div>
                    <div class="form-group">
                        <asp:LinkButton ID="lnkEditIteration" runat="server" OnClick="lnkEditIteration_Click">Edit</asp:LinkButton>
                    </div>
                </div>
                <div class="col-md-10">
                    <div class="table-responsive table-responsive-nested">
                        <asp:GridView ID="grdChangeLog" runat="server" AutoGenerateColumns="False" DataKeyNames="ChangeLogID" CssClass="table table-hover no-margin" BorderStyle="None" GridLines="None" OnPreRender="grdChangeLog_PreRender" OnRowDataBound="grdChangeLog_RowDataBound" OnSelectedIndexChanged="grdChangeLog_SelectedIndexChanged">
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
                            <EmptyDataTemplate>
                                <asp:Label ID="lblNoData" runat="server" Text="There are no changes for this version."></asp:Label>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <asp:Panel ID="pnlChangeLogID" runat="server" CssClass="form-horizontal line-break form-group" Visible="false">
                <div class="form-group">
                    <asp:Label ID="lblChangeType" runat="server" Text="Change Type" CssClass="col-sm-2 control-label" AssociatedControlID="ddChangeType" />
                    <div class="col-sm-10">
                        <asp:DropDownList ID="ddChangeType" runat="server" CssClass="form-control" DataTextField="ChangeType" DataValueField="ChangeTypeID" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblChangeTypeDescription" runat="server" Text="Description" CssClass="col-sm-2 control-label" AssociatedControlID="txtDescription"></asp:Label>
                    <div class="col-sm-10">

                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" Rows="5" TextMode="MultiLine" placeholder="Change Description" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <div class="btn-group">
                            <asp:LinkButton ID="lnkUpdate" runat="server" CssClass="btn btn-primary" CommandName="Update" OnClick="lnkUpdate_Click">Update</asp:LinkButton>
                            <asp:LinkButton ID="lnkCancel" runat="server" CssClass="btn btn-plain" OnClick="lnkCancel_Click">Cancel</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <asp:Panel ID="pnlAddUpdateInteration" runat="server" CssClass="form-horizontal line-break form-group" Visible="false">
                <div class="form-group">
                    <asp:Label ID="lblAddVersionNumber" runat="server" Text="Version Number" CssClass="col-sm-2 control-label" AssociatedControlID="txtVersionNumber" />
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtVersionNumber" runat="server" CssClass="form-control" placeholder="Version #" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblAddFramework" runat="server" Text="Framework" CssClass="col-sm-2 control-label" AssociatedControlID="txtFramework"></asp:Label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtFramework" runat="server" CssClass="form-control" placeholder=".NET Framework Version" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblAddLanguage" runat="server" Text="Design Language" CssClass="col-sm-2 control-label" AssociatedControlID="txtLanguage"></asp:Label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtLanguage" runat="server" CssClass="form-control" placeholder="Programming Language" />
                    </div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblAddRepository" runat="server" Text="Repository" CssClass="col-sm-2 control-label" AssociatedControlID="txtRepository"></asp:Label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txtRepository" runat="server" CssClass="form-control" placeholder="Repository Link" />
                    </div>
                </div>
                <div class="form-group">
                    <div class="col-sm-offset-2 col-sm-10">
                        <div class="btn-group">
                            <asp:LinkButton ID="lnkUpdateIteration" runat="server" CssClass="btn btn-primary" CommandName="Update" OnClick="lnkUpdateIteration_Click">Update</asp:LinkButton>
                            <asp:LinkButton ID="lnkCancelIteration" runat="server" CssClass="btn btn-plain" OnClick="lnkCancelIteration_Click">Cancel</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <div class="btn-group" role="group">
                <asp:LinkButton ID="btnAddNewVersion" runat="server" CssClass="btn btn-plain" OnClick="btnAddNewVersion_Click">Add New Version</asp:LinkButton>
                <asp:LinkButton ID="btnAddToChangeLog" runat="server" CssClass="btn btn-plain" OnClick="btnAddToChangeLog_Click">Add to Change Log</asp:LinkButton>
            </div>

        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScriptContent" runat="server">
    <script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to update the applied on date?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</asp:Content>
