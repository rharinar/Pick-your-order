using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService1.Models
{
    /* 
        - The NearestStoreClasses class is nested and contains several classes that correspond to different parts of the JSON response. These nested classes are as follows:
        - Category: includes information about a store's category, such as its ID, name, and icon.
        - Geocodes: contain information on a store's latitude and longitude coordinates.
        - Icon: includes information about a store's icon, such as a prefix and suffix.
        - Location: includes address, address extended, census block, country, cross street, dma, formatted address, locality, postcode, and region information.
        - Main: contains information on the latitude and longitude coordinates of a store.
        - RelatedPlaces: an empty class that may be used in future updates to represent related places to a store 
        - Results: an array of Result objects.
        - Result: a single store's ID, categories, chains, distance, geocodes, link, location, name, related places, and timezone are all included.
        - Roof: contains latitude and longitude coordinates for a store, which are used in conjunction with Main to create a bounding box for the store's location.
        - Root: a collection of Result objects that are used to deserialize the entire JSON response.
        - Place: contains information about a specific location, such as its name, longitude, state, state abbreviation, and latitude.
        - ZippoLatLon: a location's zip/postal code, country, country abbreviation, and a list of places with their respective information.*/
    public class NearestStoreClasses
    {
        public class Category
        {
            public int id { get; set; }
            public string name { get; set; }
            public Icon icon { get; set; }
        }

        public class Geocodes
        {
            public Main main { get; set; }
            public Roof roof { get; set; }
        }

        public class Icon
        {
            public string prefix { get; set; }
            public string suffix { get; set; }
        }

        public class Location
        {
            public string address { get; set; }
            public string address_extended { get; set; }
            public string census_block { get; set; }
            public string country { get; set; }
            public string cross_street { get; set; }
            public string dma { get; set; }
            public string formatted_address { get; set; }
            public string locality { get; set; }
            public string postcode { get; set; }
            public string region { get; set; }
        }

        public class Main
        {
            public double latitude { get; set; }
            public double longitude { get; set; }
        }

        public class RelatedPlaces
        {
        }

        public class Results
        {
            public Result[] results;
        }

        public class Result
        {
            public string fsq_id { get; set; }
            public List<Category> categories { get; set; }
            public List<object> chains { get; set; }
            public int distance { get; set; }
            public Geocodes geocodes { get; set; }
            public string link { get; set; }
            public Location location { get; set; }
            public string name { get; set; }
            public RelatedPlaces related_places { get; set; }
            public string timezone { get; set; }
        }

        public class Roof
        {
            public double latitude { get; set; }
            public double longitude { get; set; }
        }

        public class Root
        {
            public List<Result> results { get; set; }
        }

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Place
        {
            [JsonProperty("place name")]
            public string placename { get; set; }
            public string longitude { get; set; }
            public string state { get; set; }

            [JsonProperty("state abbreviation")]
            public string stateabbreviation { get; set; }
            public string latitude { get; set; }
        }

        public class ZippoLatLon
        {
            [JsonProperty("post code")]
            public string postcode { get; set; }
            public string country { get; set; }

            [JsonProperty("country abbreviation")]
            public string countryabbreviation { get; set; }
            public List<Place> places { get; set; }
        }



    }
}