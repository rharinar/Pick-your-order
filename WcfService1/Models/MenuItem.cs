using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WcfService1.Models
{
    /// <summary>
    /// This class represents a MenuItem in a restaurant
    /// Name: A string representing the name of the menu item.
    /// Description: A string representing the description of the menu item.
    /// Price: A decimal representing the price of the menu item.
    /// ItemType: A string representing the type of the menu item (e.g., appetizer, entree, dessert).
    /// NutritionalInformation: A string representing the nutritional information of the menu item.
    /// </summary>
    public class MenuItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ItemType { get; set; }

        public string NutritionalInformation { get; set; }
    }
    /// <summary>
    /// This class represents a restaurant
    /// Name: A string representing the name of the restaurant.
    /// Address: A string representing the address of the restaurant.
    /// Menu: A list of MenuItem objects representing the menu of the restaurant.
    /// </summary>
    public class Restaurant
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public List<MenuItem> Menu { get; set; }
    }
    /// <summary>
    /// This class represents a repository of restaurants
    //Fields
    //restaurants: A private list of Restaurant objects representing the collection of restaurants.
    //Methods
    //RestaurantRepository(): A constructor that initializes the restaurants field with some sample data.
    //GetAllRestaurants(): A method that returns all the restaurants in the repository.
    //GetRestaurantByName(string name): A method that returns the restaurant with the specified name, or null if it doesn't exist.
    //GetRestaurantsByItemType(string itemType) : A method that returns all the restaurants that serve the specified type of menu item.
    /// </summary>
    public class RestaurantRepository
    {
        private readonly List<Restaurant> restaurants;

        public RestaurantRepository()
        {
            restaurants = new List<Restaurant>()
        {
            new Restaurant()
            {
                Name = "Italiano",
                Address = "123 Oak St.",
                Menu = new List<MenuItem>()
                {
                    new MenuItem() { Name = "Spaghetti Bolognese", Description = "Spaghetti with meat sauce", Price = 12.99M, ItemType = "Entree" },
                    new MenuItem() { Name = "Lasagna", Description = "Layers of pasta, meat, and cheese", Price = 14.99M, ItemType = "Entree" },
                    new MenuItem() { Name = "Tiramisu", Description = "A classic Italian dessert with coffee and cream", Price = 6.99M, ItemType = "Dessert" }
                }
            },
            new Restaurant()
            {
                Name = "Sushi_Spot",
                Address = "456 Main St.",
                Menu = new List<MenuItem>()
                {
                    new MenuItem() { Name = "California Roll", Description = "Crab, avocado, and cucumber roll", Price = 8.99M, ItemType = "Appetizer" },
                    new MenuItem() { Name = "Rainbow Roll", Description = "Assorted fish and avocado on top of a California roll", Price = 12.99M, ItemType = "Entree" },
                    new MenuItem() { Name = "Mochi Ice Cream", Description = "Japanese rice cake filled with ice cream", Price = 5.99M, ItemType = "Dessert" }
                }
            },
            new Restaurant()
            {
                Name = "Thai_Palace",
                Address = "789 Maple Ave.",
                Menu = new List<MenuItem>()
                {
                    new MenuItem() { Name = "Pad Thai", Description = "Stir-fried noodles with meat, eggs, and peanuts", Price = 10.99M, ItemType = "Entree" },
                    new MenuItem() { Name = "Green Curry", Description = "Spicy curry with meat and vegetables", Price = 12.99M, ItemType = "Entree" },
                    new MenuItem() { Name = "Mango Sticky Rice", Description = "Sweet sticky rice with fresh mango", Price = 6.99M, ItemType = "Dessert" }
                }
            },
            new Restaurant()
            {
                Name = "BBQ_Pit",
                Address = "321 Cedar St.",
                Menu = new List<MenuItem>()
                {
                    new MenuItem() { Name = "Pulled Pork Sandwich", Description = "Slow-cooked pork with BBQ sauce on a bun", Price = 9.99M, ItemType = "Entree" },
                    new MenuItem() { Name = "Brisket Plate", Description = "Smoked beef brisket with sides", Price = 15.99M, ItemType = "Entree" },
                    new MenuItem() { Name = "Sweet Potato Pie", Description = "A Southern classic dessert", Price = 5.99M, ItemType = "Dessert" }
                }
            },
            new Restaurant()
            {
                Name = "The_Sushi_Bar",
                Address = "789 Cherry Lane",
                Menu = new List<MenuItem>()
                {
                    new MenuItem() { Name = "California Roll", Description = "Crab and avocado wrapped in sushi rice", Price = 8.99M, ItemType = "Appetizer" },
                    new MenuItem() { Name = "Tuna Sashimi", Description = "Thinly sliced raw tuna", Price = 12.99M, ItemType = "Appetizer" },
                    new MenuItem() { Name = "Spicy Tuna Roll", Description = "Tuna and spicy mayo wrapped in sushi rice", Price = 10.99M, ItemType = "Entree" },
                    new MenuItem() { Name = "Dragon Roll", Description = "Shrimp tempura and avocado wrapped in sushi rice with eel sauce", Price = 13.99M, ItemType = "Entree" },
                    new MenuItem() { Name = "Green Tea Ice Cream", Description = "Traditional Japanese ice cream made with green tea", Price = 4.99M, ItemType = "Dessert" }
                }
            },
            new Restaurant()
            {
                Name = "The_Pasta_House",
                Address = "321 Olive Ave",
                Menu = new List<MenuItem>()
                {
                    new MenuItem() { Name = "Caprese Salad", Description = "Tomatoes and mozzarella with basil and balsamic glaze", Price = 7.99M, ItemType = "Appetizer" },
                    new MenuItem() { Name = "Fettuccine Alfredo", Description = "Creamy alfredo sauce with fettuccine pasta", Price = 12.99M, ItemType = "Entree" },
                    new MenuItem() { Name = "Spaghetti Bolognese", Description = "Spaghetti with meat sauce", Price = 11.99M, ItemType = "Entree" },
                    new MenuItem() { Name = "Tiramisu", Description = "Classic Italian dessert made with mascarpone cheese and ladyfingers", Price = 6.99M, ItemType = "Dessert" }
                }
            },
            new Restaurant()
            {
                Name = "The_Steakhouse",
                Address = "555 Walnut St",
                Menu = new List<MenuItem>()
                {
                    new MenuItem() { Name = "Shrimp Cocktail", Description = "Chilled shrimp with cocktail sauce", Price = 9.99M, ItemType = "Appetizer" },
                    new MenuItem() { Name = "Filet Mignon", Description = "8 oz. tenderloin steak", Price = 24.99M, ItemType = "Entree" },
                    new MenuItem() { Name = "Ribeye Steak", Description = "12 oz. steak with marbling for flavor", Price = 28.99M, ItemType = "Entree" },
                    new MenuItem() { Name = "Chocolate Cake", Description = "Rich chocolate cake with chocolate ganache", Price = 7.99M, ItemType = "Dessert" }
                }
            }
        };
        }

        public List<Restaurant> GetAllRestaurants()
        {
            return restaurants;
        }
    }
}