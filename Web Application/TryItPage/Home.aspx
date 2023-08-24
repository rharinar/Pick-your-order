<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="TryItPage.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>
        Pick Me Up
    </h1>
    <h2>
        Description:
    </h2>
    <p>
        This application enables the user to make orders at their favourite restaurant and allows the restaurant owners to alter their menu as they need. 
    </p>
    <a href="http://webstrar183.fulton.asu.edu/index_revanth.html">Service Directory</a>
    <h2>
        Functionalities offered (Remote Components):
    </h2>
    <h3>
        User Registration & Authentication Service
    </h3>
    <p>
        This service is developed from end to end and will be further used in developing the Member and Staff pages in assignment-9
        <br /> This service collects all the user related information such as Input - Required Username (string) Password (string) Optional username (string) password (string) firstName (string) lastName (string) It allows new users to register by providing all the details. It also allows the service to authenticate users.
    </p>
    <h3>
        Find Nearest Store:
    </h3>
    <p>
        This service takes zipcode and store name as inputs and gives the address of the nearest store address as a string. First, it fetches the latitude and longitude using the zipcode and <a href="http://api.zippopotam.us/">Zippopotam API</a>. This latitude and longitude is given as input to <a href="https://api.foursquare.com/v3/places/nearby?ll=">foursquare API</a> and return the first nearest store address as a string.
    </p>
    <h3>
        Menu Listing Service: 1)Get Menu By Restaurant Name API Test Link 
    </h3>
    <p>
        1)Get Menu By Restaurant: This service checks for a restaurant matching the exact name, collects all the menu items and then uses spoonacular API to get the nutritional information of the dish for customers to make healthy choices.
    </p>
    <h3>
        Menu Listing Service: 2)Get All available items filtered by type API Test Link
    </h3>
    <p>
        2)Get All available items filtered by type: This service gets all the items matching a particular type like Entree to from all the restaurants to make it easier for the user to choose from and order at multiple restaurants. 
    </p>
    <h3>
        Menu Listing Service: 3)Get All available items less than a price API Test Link
    </h3>
    <p>
        3)Get All available items less than a price: This service would be helpful when dealing with a budget and looking for items which are less than a particular price.
    </p>
    <h2>
        Local Components developed:
    </h2>
    <h3>
        Global.asax file
    </h3>
    <p>
        We developed a logging component which log each incoming web request in the Application_BeginRequest method.
    </p>
    <h3>
        DLL Component
    </h3>
    <p>
        We developed a DLL which provided functions to generate salt(which would be unique to user and stored on server), hash password(hashed password is stored on server as well) and verify the password(which would be done on the server by fetching the hashed password and salt from credentials file.) 
    </p>
    <h2>
        How end users can sign up for these services?
    </h2>
    <p>
        If a member, he can sign in or sign up using the Member page. If staff, he can sign in using Staff Page and registration can only done by Administrator by adding their credentials on staff_credentials.xml in sever.
    </p>
    <p>
        The member page, and their page to display services, the staff page and their page to display services are In Progress and will be developed in Assignment-9.
    </p>
    <h2>
        How can you test the services?
    </h2>
    <p>
        The user can test the services by registering in the Member signup page or using the TA credentials for staff login and access them.  
    </p>
    <h3>
        Local Components: Global.asax and XML File manifulation to add elements to Log.xml file
    </h3>
    <p>
        Use this <a href="http://webstrar183.fulton.asu.edu/page4/tryIt">Link</a> to access the try it component for Global.asax file which would display the logs of each request made to the application. For display purposes, I am displaying log contents in TryIt Page.
    </p>
    <h3>
        Local Components: DLL
    </h3>
    <p>
        Use this <a href="http://webstrar183.fulton.asu.edu/page4/tryIt">Link</a> to access the try it component for Hashing. 
        <br />Step 1: Click the Generate Salt button to generate salt(This salt is generated for each user and stored in users.xml on server while registration).
        <br />Step 2: Provide the salt and password and click Hash password Button.(This hashed password and salt is stored on the server after registration).
        <br />Step 3: Provide the salt, password and hashed password and click verify button.(During login, the user name provided by user is used to get his respective hashed password and salt from users.xml. This is used to verify the password.)
    </p>
    <h3>
        Remote Component: User registration and Authentication Service <a href="http://webstrar183.fulton.asu.edu/page4/tryIt">Try It for Remote Components</a>
    </h3>
    <p>
        Login API - String username, String password, Registration API - String username, String password, String FirstName, String LastName 
        <br /> Inputs - Login: username: hello, password: hello
    </p>
    <h3>
        Remote Component: Menu Listing Service
    </h3>
    <p>
        1)Get Menu By Restaurant Name - String Restaurant Name (Sample Inputs: Italiano, Sushi_Spot, Thai_Palace, BBQ_Pit, The_Sushi_Bar, The_Pasta_House, The_Steakhouse) 
        <br />2)Get All available items filtered by type - String Item Type (Sample Inputs: Appetizer, Entree, Dessert) 
        <br />3)Get All available items less than a price - Int maximum price(Sample Inputs: 5, 10, 15, 20)
    </p>
    <h3>
        Remote Component: Find Nearest Store Service
    </h3>
    <p>
        Int ZipCode, String Store Name 
        <br />Sample Inputs - Zipcode=85281, Storename=McDonalds
    </p>

</body>
</html>
