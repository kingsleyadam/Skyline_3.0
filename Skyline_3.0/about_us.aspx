<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="about_us.aspx.cs" Inherits="Skyline_3._0.about_us" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="no-border">
        <asp:DataList ID="dlAboutUs" runat="server" CssClass="table table-condensed no-margin" DataKeyField="GlossID" Width="100%">
            <ItemTemplate>
                <asp:Label ID="txtText" runat="server" Text='<%# Eval("Text") %>' />
            </ItemTemplate>
        </asp:DataList>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScriptContent" runat="server">
</asp:Content>
