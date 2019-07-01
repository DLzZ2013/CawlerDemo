<%@ Page Title="" Language="C#" MasterPageFile="~/Frame.Master" AutoEventWireup="true" CodeBehind="news.aspx.cs" Inherits="Crawler.Web.news" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <table>
        <tbody></tbody>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="script" runat="server">
    <script>
        $("body").ready(function () {
            invokeDefault("news.aspx/GetBaiduNews");
        });
        function successCallback(data) {
            data = data.d;
            var model = JSON.parse(data);
            var html = "<tbody>";
            for (var i = 0; i < model.length; i++) {
                html += "<tr><td>" + model[i].NewsTitle + "</td></tr>";
            }
            html += "</tbody>";
            $("tbody").css("display", "none");
            $("table").append(html);
        }
    </script>
</asp:Content>
