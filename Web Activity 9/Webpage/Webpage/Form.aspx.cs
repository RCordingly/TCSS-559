using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class Form : System.Web.UI.Page
{
    static dynamic loadedPet = null;

    private string[] petFields = { "petName", "petType", "petBreed", "petAge", "petVaccinations", "ownerName",
        "ownerEmail", "ownerPhone", "ownerAddress", "ownerZip" };

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void openSearch(object sender, EventArgs e)
    {
        CreatePanel.Visible = false;
        SearchPanel.Visible = true;
        UserPanel.Visible = false;
    }

    protected void openCreate(object sender, EventArgs e)
    {
        createMessage.Visible = false;
        SearchPanel.Visible = false;
        CreatePanel.Visible = true;
        WidgetPanel.Visible = false;
        UserPanel.Visible = false;
    }

    protected void openUser(object sender, EventArgs e)
    {
        createMessage.Visible = false;
        SearchPanel.Visible = false;
        CreatePanel.Visible = false;
        WidgetPanel.Visible = false;
        UserPanel.Visible = true;
        UserLabel.Visible = false;
    }

    protected void submitUser(object sender, EventArgs e)
    {
        string email = ownerUsername.Text;
        string password = ownerPassword.Text;
        string url = database_textbox.Text;
        if (newPassword.Visible)
        {
            dynamic user = new {};
            user.username = email;
            user.petIds = new int[] { };
            string newPass = newPassword.Text;
            string result = doDataRequest("http://" + url + "/api/owners", user, "POST", new string[] { "password", password, "newPassword", newPass });

            if (!result.Equals("ERROR"))
            {
                UserLabel.Visible = true;
                UserLabel.Text = "Password Updated";
            }
            else
            {
                UserLabel.Visible = true;
                UserLabel.Text = "Error updating password.";
            }

        } else if (repeatPassword.Visible)
        {
            string repeatPass = repeatPassword.Text;
            dynamic user = new { };
            user.username = email;
            user.petIds = new int[] { };
            string result = doDataRequest("http://" + url + "/api/owners", user, "POST", new string[] { "password", password, "newPassword", repeatPass });

            if (!result.Equals("ERROR"))
            {
                UserLabel.Visible = true;
                UserLabel.Text = "Account created.";
            }
            else
            {
                UserLabel.Visible = true;
                UserLabel.Text = "Error creating account. Account may already exist.";
            }

        } else
        {
            string result = doBasicRequest("http://" + url + "/api/owners/validate", "POST", new string[] { "username", email, "password", password });
            if (!result.Equals("ERROR"))
            {
                UserLabel.Visible = true;
                UserLabel.Text = "Account logged in!";
            } else
            {
                UserLabel.Visible = true;
                UserLabel.Text = "Error validating email and password.";
            }
        }
    }

    protected void userModeToggle(object sender, EventArgs e)
    {
        if (ModeToggle.Text.Equals("Sign Up.."))
        {
            repeatPassword.Visible = true;
            newPassword.Visible = false;
            ModeToggle.Text = "Change Password..";
        }
        else if (ModeToggle.Text.Equals("Change Password.."))
        {
            newPassword.Visible = true;
            repeatPassword.Visible = false;
            ModeToggle.Text = "Log In..";
        }
        else if (ModeToggle.Text.Equals("Log In..")) {

            newPassword.Visible = false;
            repeatPassword.Visible = false;
            ModeToggle.Text = "Sign Up..";
        }

    }

        protected void rfidSearchSubmit(object sender, EventArgs e)
    {
        updateMessage.Visible = true;
        rfid_message.Visible = false;
        string rfid = rfid_textbox.Text;
        string url = database_textbox.Text;

        string result = doBasicRequest("http://" + url + "/api/pet/id/" + rfid, "GET");
        if (result.Equals("ERROR"))
        {
            WidgetPanel.Visible = false;
            rfid_message.Text = "Unable to find RFID.";
            rfid_message.Visible = true;
        }
        else
        {

            JavaScriptSerializer js = new JavaScriptSerializer();
            var pet = js.Deserialize<dynamic>(result);

            loadedPet = pet;

            WidgetPanel.Visible = true;
            updateWidgets(sender, e);

            foreach (string s in petFields)
            {
                UpdateTextBox(SearchResults.FindControl(s) as TextBox, pet, s);
            }

            rfid_message.Visible = false;
        }
    }

    private void UpdateTextBox(TextBox box, dynamic pet, string field)
    {
        if (pet[field] != null)
        {
            box.Text = pet[field].ToString();
        }

        box.Visible = true;
    }

    private string doBasicRequest(string url, string method, string[] headers)
    {
        HttpWebRequest serviceRequest = (HttpWebRequest)WebRequest.Create(url);
        serviceRequest.Method = method;

        for (var i = 0; i < headers.Length; i += 2)
        {
            serviceRequest.Headers.Add(headers[i], headers[i + 1]);
        }

        try
        {
            HttpWebResponse serviceResponse = (HttpWebResponse)serviceRequest.GetResponse();
            Stream receiveStream = serviceResponse.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader readStream = new StreamReader(receiveStream, encode, true);
            return readStream.ReadToEnd();

        }
        catch (System.Net.WebException e)
        {
            return "ERROR";
        }
    }

    private string doBasicRequest(string url, string method)
    {
        return doBasicRequest(url, method, new string[] { });
    }

    private string doDataRequest(string url, dynamic pet, string method, string[] headers)
    {
        HttpWebRequest serviceRequest = (HttpWebRequest)WebRequest.Create(url);
        serviceRequest.Method = method;
        serviceRequest.ContentType = "application/json";

        for (var i = 0; i < headers.Length; i += 2)
        {
            serviceRequest.Headers.Add(headers[i], headers[i + 1]);
        }

        JavaScriptSerializer js = new JavaScriptSerializer();
        string serializedObject = js.Serialize(pet);
        serviceRequest.ContentLength = serializedObject.Length;

        using (var writer = new StreamWriter(serviceRequest.GetRequestStream()))
        {
            writer.Write(serializedObject);
        }

        try
        {
            HttpWebResponse serviceResponse = (HttpWebResponse)serviceRequest.GetResponse();
            Stream receiveStream = serviceResponse.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader readStream = new StreamReader(receiveStream, encode, true);
            return readStream.ReadToEnd();

        }
        catch (System.Net.WebException e)
        {
            return "ERROR";
        }
    }

    private string doDataRequest(string url, dynamic pet, string method)
    {
        return doDataRequest(url, pet, method, new string[] { });
    }

    protected void updateWidgets(object sender, EventArgs e)
    {
        //Label
        if (loadedPet["petName"] != null)
        {
            petNameLabel.Text = "About " + loadedPet["petName"] + ":";
        } else
        {
            petNameLabel.Text = "About Your Pet:";
        }

        //Update Image
        if (loadedPet["petBreed"] != null || !loadedPet["petBreed"].Equals("")) {
            string url = "https://faroo-faroo-web-search.p.rapidapi.com/api?q=" + loadedPet["petBreed"];
            string[] headers = { "X-RapidAPI-Key", "gZi9wXlPr0mshb5QAyNa6Fih9ywpp1yRBpdjsnkGyjBpRededz" };
            string result = doBasicRequest(url, "GET", headers);
            if (!result.Equals("ERROR"))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                var data = js.Deserialize<dynamic>(result);

                if (data["results"].Length > 0)
                {
                    var imageUrl = data["results"][0]["iurl"];
                    if (imageUrl != null)
                    {
                        PetImage.Visible = true;
                        PetImage.ImageUrl = imageUrl;
                    }
                }
            }
        }


    }

    protected void deletePet(object sender, EventArgs e)
    {
        string rfid = loadedPet["id"].ToString();
        string url = database_textbox.Text;

        string result = doBasicRequest("http://" + url + "/api/pet/id/" + rfid, "DELETE");

        if (!result.Equals("ERROR"))
        {
            SearchResults.Visible = false;
        }

        updateMessage.Text = "Error deleting pet.";
        updateMessage.Visible = true;
    }

    protected void putPetChanges(object sender, EventArgs e)
    {

        foreach (string s in petFields)
        {
            TextBox textBox = SearchResults.FindControl(s) as TextBox;
            loadedPet[s] = textBox.Text;
        }

        string petId = loadedPet["id"].ToString();
        string url = database_textbox.Text;
        string result = doDataRequest("http://" + url + "/api/pet/id/" + petId, loadedPet, "PUT");

        if (!result.Equals("ERROR"))
        {
            updateMessage.Text = "Update successful!";
            updateMessage.Visible = true;
            UpdatePetButton.Visible = true;
            updateWidgets(sender, e);
        }
        else
        {
            updateMessage.Text = "Error updating.";
            updateMessage.Visible = true;
        }
    }

    protected void createPet(object sender, EventArgs e)
    {
        string petId = rfid_textbox_create.Text;
        string pet = "{\"id\":" + petId + ",";

        foreach (string s in petFields)
        {
            TextBox textBox = SearchResults.FindControl(s + "Create") as TextBox;
            pet += "\"" + s + "\":\"" + textBox.Text + "\",";
        }

        pet = pet.Substring(0, pet.Length - 1) + "}";
        JavaScriptSerializer js = new JavaScriptSerializer();
        dynamic pet2 = js.DeserializeObject(pet);

        string url = database_create_textbox.Text;
        string result = doDataRequest("http://" + url + "/api/pet", pet2, "POST");

        if (!result.Equals("ERROR"))
        {
            createMessage.Text = "Creation successful!";
            createMessage.Visible = true;
            UpdatePetButton.Visible = true;
        }
        else
        {
            createMessage.Text = "Error creating pet.";
            createMessage.Visible = true;
        }
    }

    protected void editToggle(object sender, EventArgs e)
    {
        SearchResults.Visible = !SearchResults.Visible;
    }

}