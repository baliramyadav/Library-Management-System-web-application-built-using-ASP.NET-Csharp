<%@ Page Title="Add_publisher" Language="C#" MasterPageFile="~/Admin/AdminSite.Master" AutoEventWireup="true" CodeBehind="Add_publisher.aspx.cs" Inherits="LMS_ProjectTraining.Admin.Add_publisher" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row">      
            <div class="col-3 border">
                <div class="row">
                    <div class="col-12">
                        <h4>Add Publisher</h4>
                        <div class="form-group">
                            <asp:TextBox ID="txtpublisherID" CssClass="form-control" placeholder="Publisher ID" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" runat="server" ErrorMessage="enter valid id" ValidationGroup="btn_Save" ControlToValidate="txtpublisherID" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <asp:TextBox ID="txtpublisherName" CssClass="form-control" runat="server" placeholder="Publisher Name"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ForeColor="Red" runat="server" ErrorMessage="enter valid name" Display="Dynamic" ValidationGroup="btn_Save"  ControlToValidate="txtpublisherName"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                        <asp:Button ID="btnAdd" CssClass="btn btn-success" ValidationGroup="btn_Save" runat="server" Text="Add" OnClick="btnAdd_Click" />
                        <asp:Button ID="btnupdate" CssClass="btn btn-info" runat="server" Text="Update" OnClick="btnupdate_Click" />
                        <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                        </div>
                    </div>
                </div>

            </div>
            <div class="col-9 border">
                <div class="table table-responsive border">
                    <h4>show all publisher List:</h4>
                    <asp:Repeater ID="RptPublisher" runat="server" OnItemCommand="RptPublisher_ItemCommand">
                    <HeaderTemplate>
                        <table class="table table-bordered table-hover">
                            <thead class="alert-info">
                                <tr>
                                    <th><span>Publisehr ID</span> </th>
                                    <th><span>Publisher Name</span> </th>
                                    <th>&nbsp;</th>
                                </tr>
                            </thead>
                            <tbody>
                      </HeaderTemplate>
                    <ItemTemplate>
                       <tr> 
                           <td><%#Eval("publisher_id") %> </td>
                           <td><%#Eval("publisher_name") %> </td>
                           <td style="width:18%">
                               <asp:LinkButton ID="lnkEdit" class="table-link text-primary" runat="server" CommandArgument='<%#Eval("publisher_id") %>' CommandName="edit" ToolTip="edit record">
                                   <span class="fa-stack">
                                       <i class="fa fa-square  fa-stack-2x"> </i>
                                       <i class="fa fa-pencil fa-stack-1x fa-inverse"></i>

                                   </span>
                               </asp:LinkButton>

                               <asp:LinkButton ID="lnkDelete" class="table-link text-danger" runat="server" CommandArgument='<%#Eval("publisher_id") %>' CommandName="delete" Text="Delete" ToolTip="Delete record" OnClientClick="return confirm('Do you want to delete this row?');">
                                   <span class="fa-stack">
                                       <i class="fa fa-square  fa-stack-2x"> </i>
                                       <i class=" fa fa-trash fa-stack-1x fa-inverse"></i>

                                   </span>
                               </asp:LinkButton>
                           </td>
                       </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                        </table>
                    </FooterTemplate>  
                </asp:Repeater>
                </div>
            </div>
        </div> 
    </div>
</asp:Content>
