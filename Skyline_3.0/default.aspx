<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Skyline_3._0._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-8">
            <asp:DataList ID="dlHomeLeft" runat="server" CssClass="table-condensed"
                DataKeyField="GlossID" DataSourceID="admGetGlossary"
                Width="100%">
                <ItemTemplate>
                    <asp:Label ID="TextLabel" runat="server" Text='<%# Eval("Text") %>' />
                </ItemTemplate>
            </asp:DataList>
        </div>
        <div class="col-md-4">
            <asp:DataList ID="dlHomeRight" runat="server" CssClass="table-condensed"
                DataKeyField="GlossID" DataSourceID="admGetGlossaryR"
                Width="100%">
                <ItemTemplate>
                    <asp:Label ID="TextLabel" runat="server" Text='<%# Eval("Text") %>' />
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>

    <asp:SqlDataSource ID="admGetGlossary" runat="server"
        ConnectionString="<%$ ConnectionStrings:skylinebigredConnectionString %>"
        SelectCommand="admGetGlossary" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter DefaultValue="1" Name="PageID" Type="Int32" />
            <asp:Parameter DefaultValue="L" Name="Location" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="admGetGlossaryR" runat="server"
        ConnectionString="<%$ ConnectionStrings:skylinebigredConnectionString %>"
        SelectCommand="admGetGlossary" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:Parameter DefaultValue="1" Name="PageID" Type="Int32" />
            <asp:Parameter DefaultValue="R" Name="Location" Type="String" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScriptContent" runat="server">
</asp:Content>
