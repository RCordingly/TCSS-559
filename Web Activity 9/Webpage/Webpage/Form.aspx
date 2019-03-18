<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Form.aspx.cs" Inherits="Form" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        body, html {
	        padding: 0px;
	        margin: 0px;
	        background-color: hsla(0,0%,95%,1.00);
        }

        .banner {
            font-size: x-large;
            color: #FFFFCC;
            background-color: #0066FF;
            margin-left: auto;
            margin-right: auto;
            text-align: center;
        }
        .divider {
            font-size: xx-small;
            background-color: #808080;
            margin-left: auto;
            margin-right: auto;
            text-align: center;
        }
        .img{
            height: 200px;
            float: right;
        }

        #header {
	        font-family: 'Noto Sans', sans-serif;
	        position: fixed;
	        left: 0px;
	        top: 0px;
	        width: 100%;
	        padding-top: 5px;
	        padding-bottom: 5px;
	        min-width: 320px;
	        border-bottom: thin solid hsla(0,0%,45%,1.00);
	        -webkit-box-shadow: 0px 4px 20px hsla(0,0%,63%,1.00);
	        box-shadow: 0px 4px 20px hsla(0,0%,63%,1.00);
	        background-image: -webkit-linear-gradient(270deg,rgba(255,255,255,1.00) 0%,rgba(217,217,217,1.00) 100%);
	        background-image: -moz-linear-gradient(270deg,rgba(255,255,255,1.00) 0%,rgba(217,217,217,1.00) 100%);
	        background-image: -o-linear-gradient(270deg,rgba(255,255,255,1.00) 0%,rgba(217,217,217,1.00) 100%);
	        background-image: linear-gradient(180deg,rgba(255,255,255,1.00) 0%,rgba(217,217,217,1.00) 100%);
        }
        #nav {
	        font-family: 'Noto Sans', sans-serif;
	        font-size: 20px;
	        line-height: 35px;
	        display: inline;
	        position: relative;
	        float: right;
	        width: 25%;
	        min-width: 340px;
	        max-width: 500px;
	        margin-top: -20px;
	        right: 2%;
        }

        #nav  ul li:before	{
	      content: "";
          position: absolute;
          z-index: -1;
          left: 50%;
          right: 50%;
          bottom: 0;
          background: #2098d1;
          height: 4px;
        }

        #nav ul li:hover:before, #nav  ul li:focus:before, #nav  ul li:active:before {
          left: 0;
          right: 0;
        }

        #nav ul {
	        list-style-type: none;	
        }
        #nav ul li {
	        float: right;
	        padding-left: 15px;
	        text-align: center;
	
		     display: inline-block;
             vertical-align: middle;
             -webkit-transform: translateZ(0);
             transform: translateZ(0);
             box-shadow: 0 0 1px rgba(0, 0, 0, 0);
             -webkit-backface-visibility: hidden;
             backface-visibility: hidden;
             -moz-osx-font-smoothing: grayscale;
             position: relative;
             overflow: hidden;
        }
        #nav ul li a {
	        text-decoration: none;
	        color: black;	
        }
        #nav ul li a:hover {
	        color: black;
	        text-shadow: 0px 0px 10px white;
        }

        p {
	        font-size: 16px;
	        display: block;
	        font-family: 'Noto Sans', sans-serif;
	        width: 96%;
	        padding-left: 2%;
	        padding-right: 2%;	
        }

        p a {
	        text-decoration: none;
	        color: green;	
        }

        p a:hover {
	        text-decoration: underline;	
        }

        h1 {
	        font-family: 'Noto Sans', sans-serif;
	        padding: 0px;
	        margin: 0px;
	        font-size: 18px;
        }

        h2 {
	        font-family: 'Noto Sans', sans-serif;
	        width: 100%;
	        height: auto;
	        margin-left: auto;
	        margin-right: auto;
	        text-align: center;
	        font-size: 20px;
	        border-bottom: thin solid hsla(0,0%,91%,1.00);
	        float: right;
        }

        h1 a{
	        padding-top: 10px;
	        padding-bottom: 10px;
	        padding-left: 20px;
	        padding-right: 20px;
	        text-decoration: none;
	        color: black;
	        background-image: -webkit-linear-gradient(270deg,rgba(199,199,199,1.00) 0%,rgba(180,180,180,1.00) 100%);
	        background-image: -moz-linear-gradient(270deg,rgba(199,199,199,1.00) 0%,rgba(180,180,180,1.00) 100%);
	        background-image: -o-linear-gradient(270deg,rgba(199,199,199,1.00) 0%,rgba(180,180,180,1.00) 100%);
	        background-image: linear-gradient(180deg,rgba(199,199,199,1.00) 0%,rgba(180,180,180,1.00) 100%);
	        border: thin solid hsla(0,0%,54%,1.00);
	        -webkit-box-shadow: 4px 4px 15px hsla(0,0%,77%,1.00);
	        box-shadow: 4px 4px 15px hsla(0,0%,77%,1.00);
        }

        h1 a:hover {
	        color: green;
	        background-image: -webkit-linear-gradient(270deg,rgba(224,224,224,1.00) 0%,rgba(203,203,203,1.00) 100%);
	        background-image: -moz-linear-gradient(270deg,rgba(224,224,224,1.00) 0%,rgba(203,203,203,1.00) 100%);
	        background-image: -o-linear-gradient(270deg,rgba(224,224,224,1.00) 0%,rgba(203,203,203,1.00) 100%);
	        background-image: linear-gradient(180deg,rgba(224,224,224,1.00) 0%,rgba(203,203,203,1.00) 100%);
	        border: thin solid hsla(0,0%,54%,1.00);
	        -webkit-box-shadow: 4px 4px 15px hsla(0,0%,77%,1.00);
	        box-shadow: 4px 4px 15px hsla(0,0%,77%,1.00);
	        -webkit-box-shadow: 5px 5px 10px hsla(0,0%,100%,1.00), -5px 5px 10px hsla(0,0%,100%,1.00), 5px -5px 10px hsla(0,0%,100%,1.00), -5px -5px 10px hsla(0,0%,100%,1.00);
	        box-shadow: 5px 5px 10px hsla(0,0%,100%,1.00), -5px 5px 10px hsla(0,0%,100%,1.00), 5px -5px 10px hsla(0,0%,100%,1.00), -5px -5px 10px hsla(0,0%,100%,1.00);		
        }

        h3 {
	        font-family: 'Noto Sans', sans-serif;
	        width: 96%;
	        height: auto;
	        margin-left: auto;
	        margin-right: auto;
	        text-align: left;
	        font-size: 20px;
	        float: right;
	        padding-left: 2%;
	        padding-right: 2%;
	        margin-top: 10px;
	        margin-bottom: 10px;
        }

        h3 a{
	        text-decoration: none;
	        color: black;	
        }

        h3 a:hover {
	        color: green;
        }


        h4 {
	        font-weight: normal;
	        margin-top: 0px;
	        font-size: 14px;
	        display: block;
	        font-family: 'Noto Sans', sans-serif;
	        width: 96%;
	        padding-left: 2%;
	        padding-right: 2%;
	        color: hsla(0,1%,21%,1.00);
	        float: left;
        }

        h4 a{
	        color: green;
	        text-decoration: none;
        }

        h4 a:hover {
	        text-decoration: underline;	
        }

        #title {
	        font-family: 'Noto Sans', sans-serif;
	        font-size: 20px;
	        position: relative;
	        width: 140px;
	        height: auto;
	        left: 3%;
	        float: left;
	        line-height: 35px;
        }
        #title a {
	        text-decoration: none;
	        color: black;
        }
        #title a:hover {
	        color: green;
	        text-shadow: 0px 0px 10px green;
        }

        #main {
            margin: 0px;
            padding: 0px;

        }

        #content {
	        width: 95%;
	        font-family: 'Noto Sans', sans-serif;
	        max-width: 1000px;
	        margin-left: auto;
	        margin-right: auto;
	        position: relative;
	        top: 60px;
	        height: auto;
	        padding-top: 5px;
	        padding-bottom: 10px;
	        min-width: 320px;
	        margin-bottom: 20px;
        }

        .module {
	        margin-top: 20px;
	        position: relative;
	        float: left;
	        width: 96%;
	        margin-left: 2%;
	        margin-right: 2%;
	        height: auto;
	        background-image: -webkit-linear-gradient(270deg,rgba(224,224,224,1.00) 0%,rgba(203,203,203,1.00) 100%);
	        background-image: -moz-linear-gradient(270deg,rgba(224,224,224,1.00) 0%,rgba(203,203,203,1.00) 100%);
	        background-image: -o-linear-gradient(270deg,rgba(224,224,224,1.00) 0%,rgba(203,203,203,1.00) 100%);
	        background-image: linear-gradient(180deg,rgba(224,224,224,1.00) 0%,rgba(203,203,203,1.00) 100%);
	        border: thin solid hsla(0,0%,54%,1.00);
	        -webkit-box-shadow: 4px 4px 15px hsla(0,0%,77%,1.00);
	        box-shadow: 4px 4px 15px hsla(0,0%,77%,1.00);
            padding: 20px;
        }


    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <!-- BASE PANEL -->

            <asp:UpdatePanel ID="BasePanel" runat="server">
                <ContentTemplate>

                    <div id="content">

                        <p>
                            
                        </p>

                        <!-- USER PANEL -->
                        <asp:UpdatePanel ID="UserPanel" runat="server" Visible="False">
                            <ContentTemplate>
                                <div class="module"> 
                                    <h3>
                                        Login / Sign Up
                                    </h3>
                                    <h4>
                                        Log in to view your pet's RFIDs.
                                    </h4>
                                    <p>
                                        <p>Email: <asp:TextBox ID="ownerUsername" runat="server"></asp:TextBox></p>
                                        <p>Password: <asp:TextBox ID="ownerPassword" runat="server"></asp:TextBox></p>
                                        <p><asp:TextBox ID="newPassword" Text="New Password" runat="server" Visible ="False"></asp:TextBox></p>
                                        <p><asp:TextBox ID="repeatPassword" Text="Repeat Password" runat="server" Visible ="False"></asp:TextBox></p>

                                        <p>
                                            <asp:Button ID="UserSubmit" runat="server" Text="Submit" OnClick="submitUser" />
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="ModeToggle" runat="server" Text="Sign Up.." OnClick="userModeToggle" />
                                        </p>

                                        <p>
                                            <asp:Label ID="UserLabel" runat="server" Visible="False" ></asp:Label>
                                        </p>
                                    </p>
                                </div>

                                <!-- USER Pets -->

                                <asp:UpdatePanel ID="ownerPets" runat="server" Visible="False">
                                    <ContentTemplate>
                                        <h2>
                                            &nbsp;
                                        </h2>

                                        <div class="module"> 
                                            <h3>
                                                Your Pet RFIDs:
                                            </h3>
                                            <h4>
                                                The pets assigned to your account.
                                            </h4>
                                            <p>
                                                <asp:Label ID="PetList" runat="server" ></asp:Label>
                                            </p>
                                            <br/>
                                            <h3>
                                                Add or Remove RFIDs:
                                            </h3>
                                            <h4>
                                                Add a new pet to your account or remove an existing one.
                                            </h4>
                                            <p>RFID: <asp:TextBox ID="PetInput" runat="server"></asp:TextBox></p>
                                            <p>
                                                <asp:Button ID="AddPet" runat="server" Text="Submit" OnClick="ownerAddPet" Visible="True" />
                                            </p>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <!-- CREATE PANEL -->

                        <asp:UpdatePanel ID="CreatePanel" runat="server" Visible="False">
                            <ContentTemplate>
                                <div class="module"> 
                                    <h3>
                                        Pet Creation
                                    </h3>
                                    <h4>
                                        Add a new pet to the database.
                                    </h4>
                                    <p>
                                        Database URL:
                                        <asp:TextBox ID="database_create_textbox" Text="localhost:62312" runat="server"></asp:TextBox>
                                    </p>
                                    <p>
                                        RFID:
                                        <asp:TextBox ID="rfid_textbox_create" runat="server"></asp:TextBox>
                                    </p>
                                    <p>
                                        <asp:Label ID="create_message" runat="server" Visible="False" ></asp:Label>
                                    </p>
                                    <h3>
                                        Pet Details:
                                    </h3>
                                    <h4>
                                        Enter information about your pet.
                                    </h4>
                                    <p>Name: <asp:TextBox ID="petNameCreate" runat="server"></asp:TextBox></p>
                                    <p>Type: <asp:TextBox ID="petTypeCreate" runat="server"></asp:TextBox></p>
                                    <p>Breed: <asp:TextBox ID="petBreedCreate" runat="server"></asp:TextBox></p>
                                    <p>Age: <asp:TextBox ID="petAgeCreate" runat="server"></asp:TextBox></p>
                                    <p>Vaccinations: <asp:TextBox ID="petVaccinationsCreate" runat="server"></asp:TextBox></p>
                                    <h3>
                                        Owner Details:
                                    </h3>
                                    <h4>
                                        Enter owner contact information.
                                    </h4>
                                    <p>Name: <asp:TextBox ID="ownerNameCreate" runat="server"></asp:TextBox></p>
                                    <p>Email: <asp:TextBox ID="ownerEmailCreate" runat="server"></asp:TextBox></p>
                                    <p>Phone: <asp:TextBox ID="ownerPhoneCreate" runat="server"></asp:TextBox></p>
                                    <p>Address: <asp:TextBox ID="ownerAddressCreate" runat="server"></asp:TextBox></p>
                                    <p>Zip: <asp:TextBox ID="ownerZipCreate" runat="server"></asp:TextBox></p>
                                    <p>
                                        <asp:Button ID="CreatePetButton" runat="server" Text="Create Pet" OnClick="createPet"/>
                                    <p/>
                                    <p>
                                        <asp:Label ID="createMessage" runat="server" Visible="False" ></asp:Label>
                                    </p>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        <!-- SEARCH PANEL -->

                         <asp:UpdatePanel ID="SearchPanel" runat="server" Visible="True">
                            <ContentTemplate>
                                <div class="module"> 
                                    <h3>
                                        Pet Look Up
                                    </h3>
                                    <h4>
                                        Search for a pet by RFID.
                                    </h4>
                                    <p>
                                        Database URL:
                                        <asp:TextBox ID="database_textbox" Text="localhost:62312" runat="server"></asp:TextBox>
                                    </p>
                                    <p>RFID:
                                        <asp:TextBox ID="rfid_textbox" runat="server"></asp:TextBox>
                                        <asp:Button ID="rfid_submit" runat="server" Text="Search" OnClick="rfidSearchSubmit" />
                                    </p>
                                    <p>
                                        <asp:Label ID="rfid_message" runat="server" Visible="False" ></asp:Label>
                                    </p>
                                </div>

                                <!-- WIDGET PANEL -->

                                <asp:UpdatePanel ID="WidgetPanel" runat="server" Visible="False">
                                    <ContentTemplate>
                                        <h2>
                                            &nbsp;
                                        </h2>

                                        <div class="module"> 
                                            <h3>
                                                <asp:Label ID="petNameLabel" runat="server" Visible="True" ></asp:Label>
                                            </h3>
                                            <p>
                                                <asp:Image CssClass="img" runat ="server" ID="PetImage" Visible="False"/>
                                            </p>
                                            <p>
                                                <asp:Image CssClass="img" runat ="server" ID="MapImage" Visible="False"/>
                                            </p>
                                            <p>
                                                <asp:Label ID="VaxLabel" runat="server" Visible="False" ></asp:Label>
                                            </p>
                                            <p>
                                                <asp:Button ID="EditToggle" runat="server" Text="Edit..." OnClick="editToggle" Visible="True" />
                                            </p>
                                        </div>

                                        <!-- SEARCH EDIT PANEL -->

                                        <asp:UpdatePanel ID="SearchResults" runat="server" Visible="False">
                                            <ContentTemplate>

                                                <h2>
                                                    &nbsp;
                                                </h2>

                                                <div class="module"> 
                                                    <h3>
                                                        Pet Details:
                                                    </h3>
                                                    <h4>
                                                        Information about the pet.
                                                    </h4>
                                                    <p>Name: <asp:TextBox ID="petName" runat="server" Visible="False"></asp:TextBox></p>
                                                    <p>Type: <asp:TextBox ID="petType" runat="server" Visible="False"></asp:TextBox></p>
                                                    <p>Breed: <asp:TextBox ID="petBreed" runat="server" Visible="False"></asp:TextBox></p>
                                                    <p>Age: <asp:TextBox ID="petAge" runat="server" Visible="False"></asp:TextBox></p>
                                                    <p>Vaccinations: <asp:TextBox ID="petVaccinations" runat="server" Visible="False"></asp:TextBox></p>
                                                    <h3>
                                                        Owner Details:
                                                    </h3>
                                                    <h4>
                                                        Pet owner contact information.
                                                    </h4>
                                                    <p>Name: <asp:TextBox ID="ownerName" runat="server" Visible="False"></asp:TextBox></p>
                                                    <p>Email: <asp:TextBox ID="ownerEmail" runat="server" Visible="False"></asp:TextBox></p>
                                                    <p>Phone: <asp:TextBox ID="ownerPhone" runat="server" Visible="False"></asp:TextBox></p>
                                                    <p>Address: <asp:TextBox ID="ownerAddress" runat="server" Visible="False"></asp:TextBox></p>
                                                    <p>Zip: <asp:TextBox ID="ownerZip" runat="server" Visible="False"></asp:TextBox></p>
                                                    <br/>
                                                    <p>
                                                        <asp:Button ID="UpdatePetButton" runat="server" Text="Save Changes.." OnClick="putPetChanges" Visible="True" />
                                                        &nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="DeletePetButton" runat="server" Text="Delete Pet" OnClick="deletePet" Visible="True" />
                                                    <p/>
                                                    <p>
                                                        <asp:Label ID="updateMessage" runat="server" Visible="False" ></asp:Label>
                                                    </p>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>  
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                </ContentTemplate>
                        </asp:UpdatePanel>

                        <p>
                            <h2>
                                &nbsp;
                            </h2>
                            <br/>
                        </p>

                    </div>

                    <div id="header">
                        <div id="title">
	                      <a">PET ID</a> 
                        </div>
                      <div id="nav">
                            <ul>
                                <li><asp:LinkButton id="LinkButton2" runat="server" OnClick="openUser">Login / Signup</asp:LinkButton></li>
                                <li><asp:LinkButton id="LinkButton1" runat="server" OnClick="openCreate">Create</asp:LinkButton></li>
                                <li><asp:LinkButton id="LinkButton0" runat="server" OnClick="openSearch">Search</asp:LinkButton></li>
                            </ul>
                        </div>
                    </div>


                    </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        

    </form>
</body>
</html>
