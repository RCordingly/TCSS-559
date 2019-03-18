using System;
using System.Collections.Generic;
using System.Dynamic;
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
    /**
     * The currently loaded pet.
     */
    static dynamic loadedPet = null;

    /**
     * All fields of a pet.
     */
    private string[] petFields = { "petName", "petType", "petBreed", "petAge", "petVaccinations", "ownerName",
        "ownerEmail", "ownerPhone", "ownerAddress", "ownerZip" };

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /**
     * Open the search section of the webpage and close all others.
     */
    protected void openSearch(object sender, EventArgs e)
    {
        CreatePanel.Visible = false;
        SearchPanel.Visible = true;
        UserPanel.Visible = false;
    }

    /**
     * Open the create section of the webpage and close all others.
     */
    protected void openCreate(object sender, EventArgs e)
    {
        createMessage.Visible = false;
        SearchPanel.Visible = false;
        CreatePanel.Visible = true;
        //WidgetPanel.Visible = false;
        UserPanel.Visible = false;
    }

    /**
     *  Open the user section of the webpage and close all others.
     */
    protected void openUser(object sender, EventArgs e)
    {
        createMessage.Visible = false;
        SearchPanel.Visible = false;
        CreatePanel.Visible = false;
        //WidgetPanel.Visible = false;
        UserPanel.Visible = true;
        UserLabel.Visible = false;
    }

    /**
     * Submit a user.
     * The web request activated dependeds on the content of the page. 
     * This method may change a users password, create a new account, or log a user in.
     */
    protected void submitUser(object sender, EventArgs e)
    {
        UserLabel.Visible = true;
        UserLabel.Text = "Loading...";

        string email = ownerUsername.Text;
        string password = ownerPassword.Text;
        string url = database_textbox.Text;
        if (newPassword.Visible)
        {
            dynamic user = new { username = email, password = "", petIds = new int[] { } };
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

            if (!repeatPass.Equals(password))
            {
                UserLabel.Visible = true;
                UserLabel.Text = "Passwords do not match.";
            }
            else
            {

                dynamic user = new { username = email, password = "", petIds = new int[] { } };
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
            }

        } else
        {
            string result = doBasicRequest("http://" + url + "/api/owners/validate", "POST", new string[] { "username", email, "password", password});
            if (!result.Equals("ERROR"))
            {
                UserLabel.Visible = true;
                UserLabel.Text = "Account logged in!";

                result = doBasicRequest("http://" + url + "/api/owners/getPets", "POST",  new string[] { "username", email }, "Pets");

                if (!result.Equals("ERROR"))
                {
                    ownerPets.Visible = true;
                    result = result.Replace(",", "<br/>");
                    PetList.Text = result;
                }

            } else
            {
                UserLabel.Visible = true;
                UserLabel.Text = "Error validating email or password.";
            }
        }
    }

    protected void ownerAddPet(object sender, EventArgs e)
    {
        string email = ownerUsername.Text;
        string password = ownerPassword.Text;
        string url = database_textbox.Text;
        string pet = PetInput.Text;
        string result = doBasicRequest("http://" + url + "/api/owners/addPet", "POST", new string[] { "username", email , "pet", pet});

        if (!result.Equals("ERROR"))
        {
            result = doBasicRequest("http://" + url + "/api/owners/getPets", "POST", new string[] { "username", email }, "Pets");

            if (result != null && !result.Equals("ERROR"))
            {
                ownerPets.Visible = true;
                result = result.Replace(",", "<br/>");
                PetList.Text = result;
            }
        }
    }

    /**
        * Change the content of the user page when the mode is pressed.
        */
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

    /**
     * Start a search processes. Activated when search button is clicked
     */
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

    /**
     * Update the content in a text box.
     */
    private void UpdateTextBox(TextBox box, dynamic pet, string field)
    {
        box.Text = "";
        if (pet[field] != null)
        {
            box.Text = pet[field].ToString();
        } 
        box.Visible = true;
    }

    /**
     * Execute a web request without any concent in the body.
     */
    private string doBasicRequest(string url, string method)
    {
        return doBasicRequest(url, method, new string[] { });
    }
    private string doBasicRequest(string url, string method, string[] headers)
    {
        HttpWebRequest serviceRequest = (HttpWebRequest)WebRequest.Create(url);
        serviceRequest.Method = method;
        serviceRequest.Accept = "application/json";
        serviceRequest.ContentLength = 0;

        for (var i = 0; i < headers.Length; i++)
        {
            serviceRequest.Headers.Add(headers[i], headers[i + 1]);
            i++;
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
    private string doBasicRequest(string url, string method, string[] headers, string getHeader)
    {
        HttpWebRequest serviceRequest = (HttpWebRequest)WebRequest.Create(url);
        serviceRequest.Method = method;
        serviceRequest.Accept = "application/json";
        serviceRequest.ContentLength = 0;

        for (var i = 0; i < headers.Length; i++)
        {
            serviceRequest.Headers.Add(headers[i], headers[i + 1]);
            i++;
        }

        try
        {
            HttpWebResponse serviceResponse = (HttpWebResponse)serviceRequest.GetResponse();
            
            return serviceResponse.Headers[getHeader];
            

            //    Stream receiveStream = serviceResponse.GetResponseStream();
            //Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            //StreamReader readStream = new StreamReader(receiveStream, encode, true);
            //return readStream.ReadToEnd();
        }
        catch (System.Net.WebException e)
        {
            return "ERROR";
        }
    }

    /**
     * Execute a web request and put a dynamic in the body of the request. 
     */
    private string doDataRequest(string url, dynamic pet, string method)
    {
        return doDataRequest(url, pet, method, new string[] { });
    }
    private string doDataRequest(string url, dynamic pet, string method, string[] headers)
    {
        return doDataRequest(url, pet, method, headers, "application/json");
    }
    private string doDataRequest(string url, dynamic pet, string method, string[] headers, string contentType)
    {
        HttpWebRequest serviceRequest = (HttpWebRequest)WebRequest.Create(url);
        serviceRequest.Method = method;
        serviceRequest.ContentType = contentType;

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


    /**
     * Controls the content in the widget area of the webpage.
     */
    protected void updateWidgets(object sender, EventArgs e)
    {
        //Labels
        if (loadedPet["petName"] != null)
        {
            petNameLabel.Text = "About " + loadedPet["petName"] + ":";
        } else
        {
            petNameLabel.Text = "About Your Pet:";
        }

        if (loadedPet["petBreed"] != null)
        {
            BreedLabel.Text = "Breed: " + loadedPet["petBreed"] + ".";
            BreedLabel.Visible = true;
        }
        else
        {
            BreedLabel.Visible = false;
        }

        if (loadedPet["petAge"] != null)
        {
            AgeLabel.Text = "Age: " + loadedPet["petAge"] + " years old.";
            AgeLabel.Visible = true;
        }
        else
        {
            AgeLabel.Visible = false;
        }

        if (loadedPet["ownerPhone"] != null)
        {
            SendSMSButton.Visible = true;
        } else
        {
            SendSMSButton.Visible = false;
        }

        if (loadedPet["ownerEmail"] != null)
        {
            sendEmailButton.Visible = true;
        }
        else
        {
            sendEmailButton.Visible = false;
        }


        //Recommended Vaccinations
        if (loadedPet["petAge"] != null && loadedPet["petType"] != null)
        {
            string url = database_textbox.Text;
            string[] headers = { "age", loadedPet["petAge"], "type", loadedPet["petType"]};
            string result = doBasicRequest("http://" + url + "/api/pet/vax", "POST", headers);
            if (result.Equals("ERROR"))
            {
                VaxLabel.Visible = false;
            } else
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                var data = js.Deserialize<dynamic>(result);
                VaxLabel.Text = "Vaccination Recommendation: " + data["VaxStats"]["message"];
                VaxLabel.Visible = true;
            }
        }

        //Update Image
        if (loadedPet["petBreed"] != null && !loadedPet["petBreed"].Equals(""))
        {
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
        } else
        {
            PetImage.Visible = false;
        }

        //Map Image
        if (loadedPet["ownerZip"] != null && !loadedPet["ownerZip"].Equals(""))
        {
            string zip = loadedPet["ownerZip"];
            string results = GetLatLong(zip);
            if (!results.Equals(""))
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                var data = js.Deserialize<dynamic>(results);
                string lat = data["lat"].ToString();
                string lng = data["lng"].ToString();
                MapImage.ImageUrl = "https://static-maps.yandex.ru/1.x/?lang=en_US&ll=" + lng + "," + lat + "&spn=0.1,0.1&l=map";
                MapImage.Visible = true;
            } else
            {
                MapImage.Visible = false;
                Console.WriteLine("Error loading map.");
            }
            
        } else
        {
            Console.WriteLine("Error reading properties.");
            MapImage.Visible = false;
        }
    }

    /**
     * Checks to make sure a dynamic has a specific property.
     */
    public static bool propertyExists(dynamic settings, string name)
    {
        if (settings is ExpandoObject)
            return ((IDictionary<string, object>)settings).ContainsKey(name);

        return settings.GetType().GetProperty(name) != null;
    }

    /**
     * Removes a pet from the database using DELETE.
     */
    protected void deletePet(object sender, EventArgs e)
    {
        string rfid = loadedPet["id"].ToString();
        string url = database_textbox.Text;

        string result = doBasicRequest("http://" + url + "/api/pet/id/" + rfid, "DELETE");

        if (!result.Equals("ERROR"))
        {
            SearchResults.Visible = false;
            WidgetPanel.Visible = false;
        }

        updateMessage.Text = "Error deleting pet.";
        updateMessage.Visible = true;
    }

    /**
     * Executes a PUT request to update the values of a pet in the database.
     */
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

    /**
     * Executes a POST request to create a new pet in the database.
     */
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

    /**
     * Toggles the visibility of the search area.
     */
    protected void editToggle(object sender, EventArgs e)
    {
        SearchResults.Visible = !SearchResults.Visible;
    }

    /**
    *  Run the first web request. Get Lat and Long from supplied IP address.
    */
    private string GetLatLong(string someZip)
    {
    //Construct the HTTP Request

    
        HttpWebRequest serviceRequest = (HttpWebRequest)WebRequest.Create("https://www.zipcodeapi.com/rest/85X8oCWvGax6PFBUY8ScmlyV4weaJR4asI0O6Gt3oYNAsDM7w2Tw94rQlUpKZ0Yu/info.json/" + someZip + "/degrees");
        serviceRequest.Method = "GET";
        serviceRequest.ContentType = "text/xml; charset=utf-8";
        serviceRequest.ContentLength = 0;

        //Send the HTTP request and analyze the response.
        HttpWebResponse serviceResponse = (HttpWebResponse)serviceRequest.GetResponse();

        //Check to make sure the request was successful.
        if (serviceResponse.StatusCode == HttpStatusCode.OK)
        {
            //Gets the stream associated with the response.
            Stream receiveStream = serviceResponse.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");

            //Pipes the stream to a higher level stream reader with the required encoding format.
            StreamReader readStream = new StreamReader(receiveStream, encode, true);

            //Return the results of the web service.
            return readStream.ReadToEnd();
        }
        else
        {
            //Return nothing in the event of an error.
            return "";
        }
    }

    protected void sendMessage(object sender, EventArgs e)
    {
        dynamic data = new { phone = loadedPet["ownerPhone"], message = "Your pet has been found!", key = "2623bedaa26dcb9f3f95e9a57ccc85e73c3fa2e0UM15lZohGmPHbgqTpd5a4rn0t" };
        string result = doDataRequest("http://textbelt.com/text", data, "POST");
    }

    protected void sendEmail(object sender, EventArgs e)
    {
        System.Diagnostics.Process.Start("mailto:" + loadedPet["ownerEmail"]);
    }
}