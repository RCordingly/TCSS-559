<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            font-size: x-large;
            color: #FFFFCC;
            background-color: #0066FF;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="auto-style1">
            <strong>PET ID</strong>
        </div>
        <asp:UpdatePanel ID="BasePanel" runat="server">
            <ContentTemplate>
                
            </ContentTemplate>
        </asp:UpdatePanel>
        <p>
        A super amazing tool to find stuff about pets.
        </p>
        <p>
            ______________________________________________________________________________________________________________________________________
        </p>
         <asp:UpdatePanel ID="SearchPanel" runat="server" Visible="True">
            <ContentTemplate>
                <div class="auto-style1">
                    Pet Look Up
                </div>
                <p>
                    Search for a pet by RFID.
                </p>
                <!--<p>
                    <asp:Label ID="trivia_question" runat="server"></asp:Label>
                </p>-->
                <p>
                    Database URL:
                    <asp:TextBox ID="database_textbox" Text="localhost:62312" runat="server"></asp:TextBox>
                </p>
                <p>
                    RFID:
                    <asp:TextBox ID="rfid_textbox" runat="server"></asp:TextBox>
                    <asp:Button ID="rfid_submit" runat="server" Text="Search" OnClick="rfid_search_submit" />
                </p>
                <p>
                    <asp:Label ID="trivia_checkanswer" runat="server"></asp:Label>
                </p>
                <p>
                    <asp:Label ID="rfid_message" runat="server" Visible="False" ></asp:Label>
                </p>
                <p>
                    ______________________________________________________________________________________________________________________________________
                </p>

                <asp:UpdatePanel ID="SearchResults" runat="server" Visible="False">
                    <ContentTemplate>

                        <div class="auto-style1">
                            Pet Details:
                        </div>

                        <p>Name: <asp:TextBox ID="petName" runat="server" Visible="False"></asp:TextBox></p>
                        <p>Type: <asp:TextBox ID="petType" runat="server" Visible="False"></asp:TextBox></p>
                        <p>Breed: <asp:TextBox ID="petBreed" runat="server" Visible="False"></asp:TextBox></p>
                        <p>Age: <asp:TextBox ID="petAge" runat="server" Visible="False"></asp:TextBox></p>
                        <p>Vaccinations: <asp:TextBox ID="petVaccinations" runat="server" Visible="False"></asp:TextBox></p>

                        <div class="auto-style1">
                            Owner Details:
                        </div>

                        <p>Name: <asp:TextBox ID="ownerName" runat="server" Visible="False"></asp:TextBox></p>
                        <p>Email: <asp:TextBox ID="ownerEmail" runat="server" Visible="False"></asp:TextBox></p>
                        <p>Phone: <asp:TextBox ID="ownerPhone" runat="server" Visible="False"></asp:TextBox></p>
                        <p>Address: <asp:TextBox ID="ownerAddress" runat="server" Visible="False"></asp:TextBox></p>
                        <p>Zip: <asp:TextBox ID="ownerZip" runat="server" Visible="False"></asp:TextBox></p>

                        <br/>

                        <p>
                             <asp:Button ID="UpdatePetButton" runat="server" Text="Save Changes.." OnClick="postPetChanges" Visible="True" />
                        <p/>

                         <p>
                            <asp:Label ID="updateMessage" runat="server" Visible="False" ></asp:Label>
                        </p>
                
                    </ContentTemplate>
                </asp:UpdatePanel>
                </ContentTemplate>
        </asp:UpdatePanel>

    </form>
</body>
</html>
