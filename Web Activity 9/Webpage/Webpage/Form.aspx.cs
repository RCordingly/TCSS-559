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

public partial class Form : System.Web.UI.Page
{
    static dynamic loadedPet = null;

    private string[] petFields = { "petName", "petType", "petBreed", "petAge", "petVaccinations", "ownerName",
        "ownerEmail", "ownerPhone", "ownerAddress", "ownerZip" };

    protected void Page_Load(object sender, EventArgs e)
    {

        
    }

    

    protected void rfid_search_submit(object sender, EventArgs e)
    {
        updateMessage.Visible = true;
        string rfid = rfid_textbox.Text;
        string url = database_textbox.Text;

        string result = doGetRequest("http://" + url + "/api/pet/id/" + rfid);
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

    private string doGetRequest(string url)
    {
        HttpWebRequest serviceRequest = (HttpWebRequest)WebRequest.Create(url);
        serviceRequest.Method = "GET";
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

    private string doPutRequest(string url, dynamic pet)
    {
        HttpWebRequest serviceRequest = (HttpWebRequest)WebRequest.Create(url);
        serviceRequest.Method = "PUT";
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

   

    protected void showPetUpdateButton(object sender, EventArgs e)
    {
        UpdatePetButton.Visible = true;
        updateMessage.Visible = false;
    }

    protected void postPetChanges(object sender, EventArgs e)
    {

       foreach (string s in petFields) {
            TextBox textBox = SearchResults.FindControl(s) as TextBox;
            loadedPet[s] = textBox.Text;
        }

        string petId = loadedPet["id"].ToString();
        string url = database_textbox.Text;
        string result = doPutRequest("http://" + url + "/api/pet/id/" + petId, loadedPet);

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


}
 