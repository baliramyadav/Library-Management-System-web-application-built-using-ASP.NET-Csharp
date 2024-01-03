<%@ Page Title="User Home Page" Language="C#" MasterPageFile="~/UserScreen/User.Master" AutoEventWireup="true" CodeBehind="UserHome.aspx.cs" Inherits="LMS_ProjectTraining.UserScreen.UserHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Welcome to the User Home</h3>
    <div class="row">
        <div class="col-lg-4 mb-4">
            <!--  card 1-->
            <div class="card h-100 border-start-lg border-start-primary">
                <div class="card-body">
                    <div class="small text-muted">Current Issue Books</div>
                    <div class="h3"> <asp:Label ID="lblIssuebook" runat="server" Text="Label"></asp:Label></div>
                    <a class="text-arrow-icon small" href="uReport.aspx">
                        View All
                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-right"><line x1="5" y1="12" x2="19" y2="12"></line><polyline points="12 5 19 12 12 19"></polyline></svg>
                    </a>
                </div>
            </div>
        </div>
        <div class="col-lg-4 mb-4">
            <!-- Billing card 2-->
            <div class="card h-100 border-start-lg border-start-secondary">
                <div class="card-body">
                    <div class="small text-muted">Total Books </div>
                    <div class="h3"><asp:Label ID="lblTotalbooks" runat="server" Text="Label"></asp:Label></div>
                    <a class="text-arrow-icon small text-secondary" href="uViewBook.aspx">
                        View All Books
                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-right"><line x1="5" y1="12" x2="19" y2="12"></line><polyline points="12 5 19 12 12 19"></polyline></svg>
                    </a>
                </div>
            </div>
        </div>
        <div class="col-lg-4 mb-4">
            <!-- Billing card 3-->
            <div class="card h-100 border-start-lg border-start-success">
                <div class="card-body">
                    <div class="small text-muted">Fine Amount</div>
                    <div class="h3 d-flex align-items-center"> Rs. <asp:Label ID="lblamount" runat="server" Text="Label"></asp:Label></div>
                    <a class="text-arrow-icon small text-success" href="uPayment.aspx">
                        Total Fine Rs.
                        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="feather feather-arrow-right"><line x1="5" y1="12" x2="19" y2="12"></line><polyline points="12 5 19 12 12 19"></polyline></svg>
                    </a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
