using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection.Emit;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;

namespace TryItPage
{
    public partial class tryIt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        // This function is an event handler for the login button. 
        // It creates a new instance of the WebstrarUserService client, 
        // calls the LoginUser method with the inputted userName and password, 
        // and displays the status of the login attempt on Label1. 
        // @param sender The object that raised the event
        // @param e The event arguments
        protected void login(object sender, EventArgs e)
        {
            webstrarService.Service1Client userService = new webstrarService.Service1Client();
            string status = userService.LoginUser(loginUserNameTextBox.Text, loginPasswordTextBox.Text);
            Label1.Text = "Registration Status is displayed here: " + status;
        }

        // This function is an event handler for the register button. 
        // It creates a new instance of the WebstrarUserService client, 
        // calls the RegisterUser method with the inputted registration details (username, password, first name, and last name), 
        // and displays the status of the registration attempt on Label2. 
        // @param sender The object that raised the event
        // @param e The event arguments
        protected void register(object sender, EventArgs e)
        {
            webstrarService.Service1Client userService = new webstrarService.Service1Client();
            string status = userService.RegisterUser(regUserNameTextBox.Text, regPasswordTextBox.Text, firstNameTextBox.Text, lastNameTextBox.Text);
            Label2.Text = "Register Status is displayed here: " + status;

        }

        // generateSalt:
        // This function generates a new random salt value using the HashingLibrary.Hashing.GenerateSalt() method.
        // The generated salt is displayed in the saltTextBox.
        protected void generateSalt(object sender, EventArgs e)
        {
            string userSalt = HashingLibrary.Hashing.GenerateSalt();
            saltTextBox.Text = "<strong>Salt is displayed here:" + userSalt + " </strong>";
        }

        // hashPassword:
        // This function hashes a password string using the HashingLibrary.Hashing.HashPassword() method.
        // The password and salt values are read from hashPasswordPasswordTextBox and hashPasswordSaltTextBox, respectively.
        // The hashed password is displayed in the hashedPasswordTextBox.
        protected void hashPassword(object sender, EventArgs e)
        {
            string hashedPassword = HashingLibrary.Hashing.HashPassword(hashPasswordPasswordTextBox.Text,hashPasswordSaltTextBox.Text);
            hashedPasswordTextBox.Text = "<strong>Hashed Password is displayed here: "+hashedPassword+"</strong>";
        }

        // verifyPassword:
        // This function verifies a password string against a previously hashed password using the HashingLibrary.Hashing.VerifyPassword() method.
        // The password, salt, and hashed password values are read from verifyPasswordTextBox, verifySaltTextBox, and verifyhashedPasswordTextBox, respectively.
        // The verification status (true or false) is displayed in the verificationTextBox.
        protected void verifyPassword(object sender, EventArgs e)
        {
            bool verificationStatus = HashingLibrary.Hashing.VerifyPassword(verifyPasswordTextBox.Text, verifySaltTextBox.Text, verifyhashedPasswordTextBox.Text);
            verificationTextBox.Text = "<strong>Verification Status is displayed here: "+verificationStatus+"</strong>";
        }

        // displayLogs:
        // This function reads the contents of the log.xml file located in the application's physical path using a StreamReader object.
        // The file contents are then displayed in the logsTextBox.
        protected void displayLogs(object sender, EventArgs e)
        {
            string _userFile = Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, "log.xml");

            string xmlString;

            // Read the file contents into a string
            using (StreamReader reader = new StreamReader(_userFile))
            {
                xmlString = reader.ReadToEnd();
            }
            logsTextBox.Text = "<strong>Logs are displayed here: "+xmlString+"</strong>";
        }

        /**
        - This function fetches the address of the nearest store based on the provided ZIP code and store name.
        - It takes in the ZIP code and store name as input from the zipCodeTextBox and storeNameTextBox TextBoxes, respectively.
        - The function first parses the ZIP code to an integer value.
        - It then creates a new RestClient object with the base URL of the web service.
        - It creates a new GET request with the ZIP code and store name as query parameters.
        - The response of the request is stored in a RestResponse object.
        - The address of the nearest store returned by the web service is displayed in the Address TextBox.
        - If an exception occurs during the process, the error message is written to the console.
        - @param sender The object that raised the event
        - @param e The event arguments
        */
        protected void fetchAddress(object sender, EventArgs e)
        {
            // Get the ZIP code and store name from the zipCodeTextBox and storeNameTextBox TextBoxes, respectively
            int zipCode = Int32.Parse(zipCodeTextBox.Text);
            string storeName = storeNameTextBox.Text;
            try
            {
                // Create a new RestClient object with the base URL of the web service
                var client = new RestClient("http://webstrar183.fulton.asu.edu/page3/Service1.svc/nearestStore?zipCode=" + zipCode + "&StoreName=" + storeName);

                // Create a new GET request with the ZIP code and store name as query parameters
                var request = new RestRequest("", Method.Get);
                RestResponse response;
                // Execute the request and store the response
                response = client.Execute(request);
                // Display the address of the nearest store in the Address TextBox
                Address.Text = "Nearest address is displayed here: " + Regex.Unescape(response.Content);
            }
            catch (Exception ex)
            {
                // Write the error message to the console
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// This function is called when the "Get Menu by Restaurant Name" button is clicked on the user interface.
        /// It retrieves the restaurant name entered by the user in the restaurantNameTextBox control and sends a GET request to the web service
        /// hosted at "http://webstrar183.fulton.asu.edu/page3/Service1.svc/getRestaurantMenu?restaurantName=" + restaurantName,
        /// where "restaurantName" is the query parameter specifying the name of the restaurant to get the menu for.
        /// The response received from the web service is then displayed in the Label3 control on the user interface.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void getMenuByRestaurantName(object sender, EventArgs e)
        {
            string restaurantName = restaurantNameTextBox.Text;
            try
            {
                // Create a new RestClient object with the base URL of the web service
                var client = new RestClient("http://webstrar183.fulton.asu.edu/page3/Service1.svc/getRestaurantMenu?restaurantName=" + restaurantName);
                
                // Create a new GET request with the ZIP code and store name as query parameters
                var request = new RestRequest("", Method.Get);
                RestResponse response;
                // Execute the request and store the response
                response = client.Execute(request);
                Label3.Text = "Restaurant Menu is displayed here: " + response.Content;
            }
            catch (Exception ex)
            {
                // Write the error message to the console
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// This function is called when the "Get Menu by Item Type" button is clicked on the user interface.
        /// It retrieves the item type entered by the user in the itemTypeTextBox control and sends a GET request to the web service
        /// hosted at "http://webstrar183.fulton.asu.edu/page3/Service1.svc/getItemByType?itemType=" + itemType,
        /// where "itemType" is the query parameter specifying the type of the items to get.
        /// The response received from the web service is then displayed in the Label4 control on the user interface.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void getMenuByItemType(object sender, EventArgs e)
        {
            string itemType = itemTypeTextBox.Text;
            try
            {
                // Create a new RestClient object with the base URL of the web service
                var client = new RestClient("http://webstrar183.fulton.asu.edu/page3/Service1.svc/getItemByType?itemType=" + itemType);
                // Create a new GET request with the ZIP code and store name as query parameters
                var request = new RestRequest("", Method.Get);
                RestResponse response;
                // Execute the request and store the response
                response = client.Execute(request);
                Label4.Text = "List of items is displayed here: " + response.Content.ToString();
            }
            catch (Exception ex)
            {
                // Write the error message to the console
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// This function is called when the "Get Menu Less Than Price" button is clicked on the user interface.
        /// It retrieves the maximum price entered by the user in the maxPriceTextBox control and sends a GET request to the web service
        /// hosted at "http://webstrar183.fulton.asu.edu/page3/Service1.svc/getItemsLessThanPrice?maxPrice=" + maxPrice,
        /// where "maxPrice" is the query parameter specifying the maximum price of the items to get.
        /// The response received from the web service is then displayed in the Label5 control on the user interface.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void getMenuLessThanPrice(object sender, EventArgs e)
        {
            int maxPrice = Int32.Parse(maxPriceTextBox.Text);
            try
            {
                // Create a new RestClient object with the base URL of the web service
                var client = new RestClient("http://webstrar183.fulton.asu.edu/page3/Service1.svc/getItemsLessThanPrice?maxPrice=" + maxPrice);
                
                // Create a new GET request with the ZIP code and store name as query parameters
                var request = new RestRequest("", Method.Get);
                RestResponse response;
                // Execute the request and store the response
                response = client.Execute(request);
                Label5.Text = "List of items is displayed here: " + response.Content;
            }
            catch (Exception ex)
            {
                // Write the error message to the console
                Console.WriteLine(ex.Message);
            }
        }
    }
}