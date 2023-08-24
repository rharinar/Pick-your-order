using Newtonsoft.Json;
using Org.OpenAPITools.Model;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Services.Description;
using WcfService1.Models;
using static WcfService1.Models.NearestStoreClasses;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        private static readonly string _spoonacularApiKey = "16dd616c92594b9caa7dec9213b39181";

        /// <summary>
        /// Calculates the hash value of the WSDL XML file located at the specified URL.
        /// </summary>
        /// <param name="wsdlUrl">The URL of the WSDL XML file.</param>
        /// <returns>A dictionary containing the operation names and their input/output parameters.</returns>
        public Dictionary<string, List<Tuple<string, string>>> wsHash(string wsdlUrl)
        {
            // Download the WSDL XML file from the specified URL.
            string wsdlXml = new WebClient().DownloadString(wsdlUrl);
            // Read the XML into a ServiceDescription object.
            TextReader xr = new StringReader(wsdlXml);
            ServiceDescription serviceDesc = ServiceDescription.Read(xr);
            // Create a dictionary to hold the operation names and their input/output parameters.
            Dictionary<string, List<Tuple<string, string>>> operationParamsDict = new Dictionary<string, List<Tuple<string, string>>>();
            // Create a dictionary to hold message names and their corresponding part element names.
            Dictionary<string, string> messagePairs = new Dictionary<string, string>();
            // Iterate through the port types and their operations to extract the input/output parameters.
            foreach (Message message in serviceDesc.Messages)
            {
                messagePairs.Add(message.Name, message.Parts[0].Element.Name);
            }
            foreach (PortType portType in serviceDesc.PortTypes)

            {
                foreach (Operation operation in portType.Operations)
                {
                    string operationName = operation.Name;

                    foreach (var message in operation.Messages)
                    {
                        // Check if the message is an input message.
                        if (message is OperationInput)
                        {
                            OperationInput tempMessage = (OperationInput)message;
                            // Check if the operation is already in the dictionary.
                            if (operationParamsDict.ContainsKey(operationName))
                            {
                                operationParamsDict[operationName].Add(Tuple.Create("Input", messagePairs[tempMessage.Message.Name]));
                            }
                            else
                            {
                                operationParamsDict.Add(operationName, new List<Tuple<string, string>> { Tuple.Create("Input", messagePairs[tempMessage.Message.Name]) });
                            }
                        }
                        // Check if the message is an output message.
                        if (message is OperationOutput)
                        {
                            OperationOutput tempMessage = (OperationOutput)message;

                            // Check if the operation is already in the dictionary.
                            if (operationParamsDict.ContainsKey(operationName))
                            {
                                operationParamsDict[operationName].Add(Tuple.Create("Output", messagePairs[tempMessage.Message.Name]));
                            }
                            else
                            {
                                operationParamsDict.Add(operationName, new List<Tuple<string, string>> { Tuple.Create("Output", messagePairs[tempMessage.Message.Name]) });
                            }
                        }
                    }
                }
            }
            // Return the dictionary of operation names and their input/output parameters.
            return operationParamsDict;
        }
        public int AbsValue(int x)
        {
            if (x >= 0) return (x);
            else return (-x);
        }
        public int addition(int x, int y)
        {
            return (x + y);
        }

        public string nearestStore(int zipCode, string StoreName)
        {
            // Declare variables for latitude and longitude
            string latitude = "", longitude = "";
            try
            {
                // Create a new RestClient object with the base URL of the zippopotam.us API and send a GET request with the provided ZIP code
                var client1 = new RestClient("http://api.zippopotam.us/us/" + zipCode);
                var request1 = new RestRequest("", Method.Get);
                request1.AddHeader("accept", "application/json");
                RestResponse res = client1.Execute(request1);
                // Deserialize the response into a ZippoLatLon object and extract the latitude and longitude coordinates
                ZippoLatLon resp = JsonConvert.DeserializeObject<ZippoLatLon>(res.Content);
                latitude = resp.places[0].latitude.ToString();
                longitude = resp.places[0].longitude.ToString();
            }
            catch (Exception ex)
            {
                // Print the exception to the console
                Console.Write(ex.ToString());
            }
            RestResponse response;
            // Create a new RestClient object with the base URL of the Foursquare API and send a GET request with the extracted coordinates and store name
            var client = new RestClient("https://api.foursquare.com/v3/places/nearby?ll=" + latitude + "," + longitude + "&query=" + StoreName);
            var request = new RestRequest("", Method.Get);
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", "fsq3zOibl2GfyR5liId2j17rhSIPfym7vp4ITrUM3NPqKV4=");
            response = client.Execute(request);
            // Deserialize the response into a Results object
            Models.NearestStoreClasses.Results fourSquareResults = JsonConvert.DeserializeObject<Models.NearestStoreClasses.Results>(response.Content);
            if (fourSquareResults.results.Length == 0)
            {
                // If there are no nearby stores within 20 miles, return the string "no stores within 20 miles"
                return "no stores within 20 miles";
            }
            // If there is a nearby store, return the location object of the first result as a JSON string
            Location answer = fourSquareResults.results[0].location;
            return JsonConvert.SerializeObject(answer);
        }


        /// <summary>
        /// This function is used to convert the nutrional information response object to nutrional information string.
        /// </summary>
        /// <param name="nutrition"></param>
        /// <returns></returns>
        public static string createNutritionString(GuessNutritionByDishName200Response nutrition)
        {
            string ans = "";
            ans += "Calories: " + nutrition.Calories.Value.ToString() + " " + nutrition.Calories.Unit + ",";
            ans += "Carbs: " + nutrition.Carbs.Value.ToString() + " " + nutrition.Carbs.Unit + ",";
            ans += "Fat: " + nutrition.Fat.Value.ToString() + " " + nutrition.Fat.Unit + ",";
            ans += "Protein: " + nutrition.Protein.Value.ToString() + " " + nutrition.Protein.Unit;

            return ans;
        }

        /// <summary>
        /// This API is used to fetch the menu of a restaurant given its name
        /// For each item on the menu, it makes a request to a 3rd party API Spoonacular to fetch the nutritional information
        /// Attach the nutritional information along with the menu
        /// </summary>
        /// <param name="name"> Restaurant Name</param>
        /// <returns></returns>

        public List<MenuItem> getRestaurantMenu(string name)
        {
            List<Restaurant> restaurants = new RestaurantRepository().GetAllRestaurants();
            foreach (Restaurant restaurant in restaurants)
            {
                if (restaurant.Name == name)
                {
                    foreach (MenuItem item in restaurant.Menu)
                    {
                        try
                        {
                            // Create a new RestClient object with the base URL of the zippopotam.us API and send a GET request with the provided ZIP code
                            var client1 = new RestClient($"https://api.spoonacular.com/recipes/guessNutrition?title={item.Name}&apiKey={_spoonacularApiKey}");
                            var request1 = new RestRequest("", Method.Get);
                            request1.AddHeader("accept", "application/json");
                            RestResponse res = client1.Execute(request1);
                            if (res.Content.Contains("error") || res.Content.Contains("failure"))
                            {
                                item.NutritionalInformation = "No nutritional Information available";
                            }
                            else if (res.StatusCode == HttpStatusCode.OK)
                            {
                                GuessNutritionByDishName200Response nutrition = JsonConvert.DeserializeObject<GuessNutritionByDishName200Response>(res.Content);
                                item.NutritionalInformation = createNutritionString(nutrition);
                            }
                        }
                        catch (Exception ex)
                        {
                            // Print the exception to the console
                            Console.Write(ex.ToString());
                        }
                    }
                    return restaurant.Menu;
                }
            }
            return null;
        }

        /// <summary>
        /// This API is used to fetch all the items from all restaurants which match a particular type like entree, dessert or main course
        /// It also appends the nutritional information of each item as well.
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public List<Tuple<string, MenuItem>> getItemsByType(string itemType)
        {
            List<Restaurant> restaurants = new RestaurantRepository().GetAllRestaurants();
            List<Tuple<string, MenuItem>> results = new List<Tuple<string, MenuItem>>();
            foreach (Restaurant restaurant in restaurants)
            {
                foreach (MenuItem item in restaurant.Menu)
                {
                    if (item.ItemType == itemType)
                    {
                        try
                        {
                            // Create a new RestClient object with the base URL of the zippopotam.us API and send a GET request with the provided ZIP code
                            var client1 = new RestClient($"https://api.spoonacular.com/recipes/guessNutrition?title={item.Name}&apiKey={_spoonacularApiKey}");
                            var request1 = new RestRequest("", Method.Get);
                            request1.AddHeader("accept", "application/json");
                            RestResponse res = client1.Execute(request1);
                            if (res.Content.Contains("error") || res.Content.Contains("failure"))
                            {
                                item.NutritionalInformation = "No nutritional Information available";
                            }
                            else if (res.StatusCode == HttpStatusCode.OK)
                            {
                                GuessNutritionByDishName200Response nutrition = JsonConvert.DeserializeObject<GuessNutritionByDishName200Response>(res.Content);
                                item.NutritionalInformation = createNutritionString(nutrition);
                            }
                        }
                        catch (Exception ex)
                        {
                            // Print the exception to the console
                            Console.Write(ex.ToString());
                        }
                        results.Add(Tuple.Create(restaurant.Name, item));
                    }
                }
            }
            return results;
        }

        /// <summary>
        /// This API is used fetch all the items below a certain price making it easier for users to plan their meals according to their budget.
        /// It also appends all the nutritional information for each item as well.
        /// </summary>
        /// <param name="maximumPrice"></param>
        /// <returns></returns>
        public List<Tuple<string, MenuItem>> getItemsLessThanPrice(Decimal maximumPrice)
        {
            List<Restaurant> restaurants = new RestaurantRepository().GetAllRestaurants();
            List<Tuple<string, MenuItem>> results = new List<Tuple<string, MenuItem>>();
            foreach (Restaurant restaurant in restaurants)
            {
                foreach (MenuItem item in restaurant.Menu)
                {
                    if (item.Price <= maximumPrice)
                    {
                        try
                        {
                            // Create a new RestClient object with the base URL of the zippopotam.us API and send a GET request with the provided ZIP code
                            var client1 = new RestClient($"https://api.spoonacular.com/recipes/guessNutrition?title={item.Name}&apiKey={_spoonacularApiKey}");
                            var request1 = new RestRequest("", Method.Get);
                            request1.AddHeader("accept", "application/json");
                            RestResponse res = client1.Execute(request1);
                            if (res.Content.Contains("error") || res.Content.Contains("failure"))
                            {
                                item.NutritionalInformation = "No nutritional Information available";
                            }
                            else if (res.StatusCode == HttpStatusCode.OK)
                            {
                                GuessNutritionByDishName200Response nutrition = JsonConvert.DeserializeObject<GuessNutritionByDishName200Response>(res.Content);
                                item.NutritionalInformation = createNutritionString(nutrition);
                            }
                        }
                        catch (Exception ex)
                        {
                            // Print the exception to the console
                            Console.Write(ex.ToString());
                        }
                        results.Add(Tuple.Create(restaurant.Name, item));
                    }
                }
            }
            results.Sort((x, y) => x.Item2.Price.CompareTo(y.Item2.Price));
            return results;
        }
    }
}
