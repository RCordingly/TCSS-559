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
        SearchPanel.Visible = true;
        CreatePanel.Visible = false;
    }

    protected void openCreate(object sender, EventArgs e)
    {
        createMessage.Visible = false;
        SearchPanel.Visible = false;
        CreatePanel.Visible = true;
    }

    protected void rfidSearchSubmit(object sender, EventArgs e)
    {
        updateMessage.Visible = true;
        string rfid = rfid_textbox.Text;
        string url = database_textbox.Text;

        string result = doBasicRequest("http://" + url + "/api/pet/id/" + rfid, "GET");
        if (result.Equals("ERROR"))
        {
            SearchResults.Visible = false;
            rfid_message.Text = "Unable to find RFID.";
            rfid_message.Visible = true;
        }
        else
        {
            
            JavaScriptSerializer js = new JavaScriptSerializer();
            var pet = js.Deserialize<dynamic>(result);

            loadedPet = pet;

            SearchResults.Visible = true;

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

    private string doBasicRequest(string url, string method)
    {
        HttpWebRequest serviceRequest = (HttpWebRequest)WebRequest.Create(url);
        serviceRequest.Method = method;
        try
        {
            HttpWebResponse serviceResponse = (HttpWebResponse)serviceRequest.GetResponse();
            Stream receiveStream = serviceResponse.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader readStream = new StreamReader(receiveStream, encode, true);
            return readStream.ReadToEnd();

        } catch (System.Net.WebException e)
        {
            return "ERROR";
        }
    }

    private string doDataRequest(string url, dynamic pet, string method)
    {
        HttpWebRequest serviceRequest = (HttpWebRequest)WebRequest.Create(url);
        serviceRequest.Method = method;
        serviceRequest.ContentType = "application/json";

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

       foreach (string s in petFields) {
            TextBox textBox = SearchResults.FindControl(s) as TextBox;
            loadedPet[s] = textBox.Text;
        }

        string petId = loadedPet["id"].ToString();
        string url = database_textbox.Text;
        string result = doDataRequest("http://" + url + "/api/pet/id/" + petId, loadedPet, "PUT");

        if (!result.Equals("ERROR")) {
            updateMessage.Text = "Update successful!";
            updateMessage.Visible = true;
            UpdatePetButton.Visible = true;
        } else
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
}
 