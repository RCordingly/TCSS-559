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
            <strong>&nbsp;&nbsp;PET ID</strong>
        </div>
        <asp:UpdatePanel ID="BasePanel" runat="server">
            <ContentTemplate>
                <p>
                &nbsp;&nbsp;&nbsp;A super amazing tool to find stuff about pets.
                </p>
                <p>
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="SearchButton" runat="server" Text="Search" OnClick="openSearch" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="CreateButton" runat="server" Text="Create" OnClick="openCreate" />
                </p>
                <p>
                    ______________________________________________________________________________________________________________________________________
                </p>

                <!-- CREATE PANEL -->

                <asp:UpdatePanel ID="CreatePanel" runat="server" Visible="False">
                    <ContentTemplate>
                        <div class="auto-style1">
                            &nbsp;&nbsp;Pet Creation
                        </div>
                        <p>
                            &nbsp;&nbsp;&nbsp;Add a new pet to the database.
                        </p>
                        <p>
                            &nbsp;&nbsp;&nbsp;Database URL:
                            <asp:TextBox ID="database_create_textbox" Text="localhost:62312" runat="server"></asp:TextBox>
                        </p>
                        <p>
                            &nbsp;&nbsp;&nbsp;RFID:
                            <asp:TextBox ID="rfid_textbox_create" runat="server"></asp:TextBox>
                        </p>
                        <p>
                            <asp:Label ID="create_message" runat="server" Visible="False" ></asp:Label>
                        </p>
                        <div class="auto-style1">
                            &nbsp;&nbsp;&nbsp;Pet Details:
                        </div>
                        <p>Name: <asp:TextBox ID="petNameCreate" runat="server"></asp:TextBox></p>
                        <p>Type: <asp:TextBox ID="petTypeCreate" runat="server"></asp:TextBox></p>
                        <p>Breed: <asp:TextBox ID="petBreedCreate" runat="server"></asp:TextBox></p>
                        <p>Age: <asp:TextBox ID="petAgeCreate" runat="server"></asp:TextBox></p>
                        <p>Vaccinations: <asp:TextBox ID="petVaccinationsCreate" runat="server"></asp:TextBox></p>
                        <div class="auto-style1">
                            &nbsp;&nbsp;&nbsp;Owner Details:
                        </div>
                        <p>Name: <asp:TextBox ID="ownerNameCreate" runat="server"></asp:TextBox></p>
                        <p>Email: <asp:TextBox ID="ownerEmailCreate" runat="server"></asp:TextBox></p>
                        <p>Phone: <asp:TextBox ID="ownerPhoneCreate" runat="server"></asp:TextBox></p>
                        <p>Address: <asp:TextBox ID="ownerAddressCreate" runat="server"></asp:TextBox></p>
                        <p>Zip: <asp:TextBox ID="ownerZipCreate" runat="server"></asp:TextBox></p>
                        <p>
                            &nbsp;&nbsp;<asp:Button ID="CreatePetButton" runat="server" Text="Create Pet" OnClick="createPet"/>
                        <p/>
                        <p>
                            <asp:Label ID="createMessage" runat="server" Visible="False" ></asp:Label>
                        </p>
                        <p>
                            ______________________________________________________________________________________________________________________________________
                        </p>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                <!-- SEARCH PANEL -->

                 <asp:UpdatePanel ID="SearchPanel" runat="server" Visible="False">
                    <ContentTemplate>
                        <div class="auto-style1">
                            &nbsp;&nbsp;Pet Look Up
                        </div>
                        <p>
                            &nbsp;&nbsp;&nbsp;Search for a pet by RFID.
                        </p>
                        <p>
                            &nbsp;&nbsp;&nbsp;Database URL:
                            <asp:TextBox ID="database_textbox" Text="localhost:62312" runat="server"></asp:TextBox>
                        </p>
                        <p>
                            &nbsp;&nbsp;&nbsp;RFID:
                            <asp:TextBox ID="rfid_textbox" runat="server"></asp:TextBox>
                            <asp:Button ID="rfid_submit" runat="server" Text="Search" OnClick="rfidSearchSubmit" />
                        </p>
                        <p>
                            <asp:Label ID="rfid_message" runat="server" Visible="False" ></asp:Label>
                        </p>
                        <p>
                            ______________________________________________________________________________________________________________________________________
                        </p>

                        <!-- SEARCH RESULTS PANEL -->

                        <asp:UpdatePanel ID="SearchResults" runat="server" Visible="False">
                            <ContentTemplate>
                                <div class="auto-style1">
                                    &nbsp;&nbsp;&nbsp;Pet Details:
                                </div>
                                <p>Name: <asp:TextBox ID="petName" runat="server" Visible="False"></asp:TextBox></p>
                                <p>Type: <asp:TextBox ID="petType" runat="server" Visible="False"></asp:TextBox></p>
                                <p>Breed: <asp:TextBox ID="petBreed" runat="server" Visible="False"></asp:TextBox></p>
                                <p>Age: <asp:TextBox ID="petAge" runat="server" Visible="False"></asp:TextBox></p>
                                <p>Vaccinations: <asp:TextBox ID="petVaccinations" runat="server" Visible="False"></asp:TextBox></p>
                                <div class="auto-style1">
                                    &nbsp;&nbsp;&nbsp;Owner Details:
                                </div>
                                <p>Name: <asp:TextBox ID="ownerName" runat="server" Visible="False"></asp:TextBox></p>
                                <p>Email: <asp:TextBox ID="ownerEmail" runat="server" Visible="False"></asp:TextBox></p>
                                <p>Phone: <asp:TextBox ID="ownerPhone" runat="server" Visible="False"></asp:TextBox></p>
                                <p>Address: <asp:TextBox ID="ownerAddress" runat="server" Visible="False"></asp:TextBox></p>
                                <p>Zip: <asp:TextBox ID="ownerZip" runat="server" Visible="False"></asp:TextBox></p>
                                <br/>
                                <p>
                                    &nbsp;&nbsp;
                                    <asp:Button ID="UpdatePetButton" runat="server" Text="Save Changes.." OnClick="putPetChanges" Visible="True" />
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="DeletePetButton" runat="server" Text="Delete Pet" OnClick="deletePet" Visible="True" />
                                <p/>
                                <p>
                                    <asp:Label ID="updateMessage" runat="server" Visible="False" ></asp:Label>
                                </p>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </ContentTemplate>
                </asp:UpdatePanel>
                </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
