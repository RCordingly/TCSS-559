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
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Page.IsPostBack == false)
        {
            mathTrivia();
        }
    }

    protected void Timer1_Tick(object sender, EventArgs e)
    {
        
    }

    protected void rfid_search_submit(object sender, EventArgs e)
    {
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

            //string result = doGetRequest("http://localhost:62312/api/pet/id/0");
            JavaScriptSerializer js = new JavaScriptSerializer();
            var pet = js.Deserialize<dynamic>(result);

            SearchResults.Visible = true;

            UpdateTextBox(PetName, pet, "petName");
            UpdateTextBox(PetBreed, pet, "petBredd");
            rfid_message.Visible = false;
        }
    }

    private void UpdateTextBox(TextBox box, dynamic pet, string field)
    {
        if (pet[field] != null)
        {
            box.Text = pet[field];
            box.Visible = true;
        } else
        {
            box.Visible = false;
        }
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

    void mathTrivia()
    {
        HttpWebRequest serviceRequest = (HttpWebRequest)WebRequest.Create("https://numbersapi.p.mashape.com/random/trivia?fragment=true&json=true&max=20&min=10");
        serviceRequest.Method = "GET";
        serviceRequest.ContentLength = 0;
        serviceRequest.ContentType = "text/plain; charset=utf-8";
        serviceRequest.Accept = "text/plain";
        serviceRequest.Headers.Add("X-RapidAPI-Key", "gZi9wXlPr0mshb5QAyNa6Fih9ywpp1yRBpdjsnkGyjBpRededz");
        HttpWebResponse serviceResponse = (HttpWebResponse)serviceRequest.GetResponse();
        Stream receiveStream = serviceResponse.GetResponseStream();
        Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
        StreamReader readStream = new StreamReader(receiveStream, encode, true);
        string responseFromServer = readStream.ReadToEnd();
        JavaScriptSerializer js = new JavaScriptSerializer();
        var obj = js.Deserialize<dynamic>(responseFromServer);
        //trivia_canswer.Text = obj["number"].ToString();
        //trivia_question.Text = obj["text"];
    }

    protected void UpdatePanel1_Load(object sender, EventArgs e)
    {
        
    }
}
 