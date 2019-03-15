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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" OnLoad="UpdatePanel1_Load">
            <ContentTemplate>
                <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="10000">
                </asp:Timer>
                <asp:Label ID="year" runat="server"></asp:Label>
                &nbsp;&nbsp;
                <asp:Label ID="text" runat="server"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
        <p>
        A super amazing tool to find stuff about pets.
        </p>
        <p>
            ______________________________________________________________________________________________________________________________________
        </p>
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
                    Search Results...
                </div>

                <p>Pet Name: <asp:TextBox ID="PetName" runat="server" Visible="False"></asp:TextBox></p>
                <p>Pet Breed: <asp:TextBox ID="PetBreed" runat="server" Visible="False"></asp:TextBox></p>
                
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
</body>
</html>
