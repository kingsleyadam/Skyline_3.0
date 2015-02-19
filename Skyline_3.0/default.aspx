<%@ Page Title="" Language="C#" MasterPageFile="~/Web.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Skyline_3._0._default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-8">
            <div class="no-border">
                <asp:DataList ID="dlHomeLeft" runat="server" CssClass="table table-condensed no-margin" DataKeyField="GlossID" Width="100%">
                    <ItemTemplate>
                        <asp:Label ID="TextLabel" runat="server" Text='<%# Eval("Text") %>' />
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
        <div class="col-md-4">
            <div class="no-border">
                <asp:DataList ID="dlHomeRight" runat="server" CssClass="table table-condensed no-margin" DataKeyField="GlossID" Width="100%">
                    <ItemTemplate>
                        <asp:Label ID="TextLabel" runat="server" Text='<%# Eval("Text") %>' />
                    </ItemTemplate>
                </asp:DataList>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="JavaScriptContent" runat="server">
</asp:Content>
