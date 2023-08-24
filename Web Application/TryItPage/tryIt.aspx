<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tryIt.aspx.cs" Inherits="TryItPage.tryIt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TryIt Page for Local Components</title>
</head>

<body>
    <form id="frm" runat="server">
        <h1>TryIt Page for Local Components</h1>
        <h2>Hashing Component</h2>
        <p>
            <strong>Description:</strong>This DLL is used to hash a password and will be used to verify a user as well.
        </p>
        <div>
            <asp:Label runat="server" Text="<strong>Sample Salt</strong>"></asp:Label>
            <asp:Button runat="server" Text="Generate Salt" OnClick="generateSalt"></asp:Button>
        </div>
        <div>
            <asp:Label ID="saltTextBox" runat="server" Text="<strong>Salt is displayed here: </strong>"></asp:Label>
        </div>
        <h3>Use the above generated salt to hash the password.</h3>
        <div>
            <asp:Label runat="server" Text="String Password"></asp:Label>
            <asp:TextBox runat="server" ID="hashPasswordPasswordTextBox"></asp:TextBox>
            <asp:Label runat="server" Text="String Salt"></asp:Label>
            <asp:TextBox runat="server" ID="hashPasswordSaltTextBox"></asp:TextBox>
            <asp:Button runat="server" Text="Hash Password" OnClick="hashPassword"></asp:Button>
        </div>
        <div>
            <asp:Label ID="hashedPasswordTextBox" runat="server" Text="<strong>Hashed Password is displayed here: </strong>"></asp:Label>
        </div>
        <h3>Verify Password</h3>
        <p>
            <strong>Description:</strong>Provide the salt(which will be hidden to user), password and hashedPassword(which will be hidden to user) to verify user.
        </p>
        <div>
            <asp:Label runat="server" Text="String password"></asp:Label>
            <asp:TextBox runat="server" ID="verifyPasswordTextBox"></asp:TextBox>
            <asp:Label runat="server" Text="String Salt"></asp:Label>
            <asp:TextBox runat="server" ID="verifySaltTextBox"></asp:TextBox>
            <asp:Label runat="server" Text="String Hashed Password"></asp:Label>
            <asp:TextBox runat="server" ID="verifyhashedPasswordTextBox"></asp:TextBox>
            <asp:Button runat="server" Text="Verify" OnClick="verifyPassword"></asp:Button>
        </div>
        <div>
            <asp:Label ID="verificationTextBox" runat="server" Text="<strong>Verification Status is displayed here: </strong>"></asp:Label>
        </div>
        <h2>Logging Component using Global.asax</h2>
        <p>
            <strong>Description:</strong>This is to test the event handler. On every request made, a log is written into log.xml and is displayed here.
        </p>
        <div>
            <asp:Button runat="server" Text="Display Logs" OnClick="displayLogs"></asp:Button>
        </div>
        <div>
            <asp:Label ID="logsTextBox" runat="server" Text="<strong>Logs are displayed here: </strong>"></asp:Label>
        </div>
        <h1>Try It for Remote Components(Member Page)</h1>
        <h2>
            <strong>Login Service</strong>
        </h2>
        <div>
            <asp:Label runat="server" Text="<strong>Description:</strong> This service takes the username and password as input, checks if the username exists,
                verifies by decrypting the password stored in the XML file and returns appropriate response."></asp:Label>
        </div>
        <div>
            <asp:Label runat="server" Text="String Username"></asp:Label>
            <asp:TextBox runat="server" ID="loginUserNameTextBox"></asp:TextBox>
            <asp:Label runat="server" Text="String Password"></asp:Label>
            <asp:TextBox runat="server" ID="loginPasswordTextBox"></asp:TextBox>
            <asp:Button runat="server" Text="Login" OnClick="login"></asp:Button>
        </div>
        <div>
            <asp:Label ID="Label1" runat="server" Text="<strong>Login Status is displayed here: </strong>"></asp:Label>
        </div>
        <h2>
            <strong>Register Service</strong>
        </h2>
        <div>
            <asp:Label runat="server" Text="<strong>Description:</strong>This service takes the username, password, last name and first name of the user, checks if the username is available
                stores the new user with his details and encrypted password in an XML file."></asp:Label>
        </div>
        <div>
            <asp:Label runat="server" Text="String Username"></asp:Label>
            <asp:TextBox runat="server" ID="regUserNameTextBox"></asp:TextBox>
            <asp:Label runat="server" Text="String Password"></asp:Label>
            <asp:TextBox runat="server" ID="regPasswordTextBox"></asp:TextBox>
            <asp:Label runat="server" Text="String First Name"></asp:Label>
            <asp:TextBox runat="server" ID="firstNameTextBox"></asp:TextBox>
            <asp:Label runat="server" Text="String Last Name"></asp:Label>
            <asp:TextBox runat="server" ID="lastNameTextBox"></asp:TextBox>
            <asp:Button runat="server" Text="Register" OnClick="register"></asp:Button>
        </div>
        <div>
            <asp:Label ID="Label2" runat="server" Text="<strong>Register Status is displayed here: </strong>"></asp:Label>
        </div>
        <h2>
            <strong>Find the nearest Store</strong>
        </h2>
        <p>
            <strong>Description:</strong> This servcie takes Zip Code(Integer) and 
                Store Name(string) as input and returns the nearest store address matching the given constraints.
                If no store found nearby, it would show no store found in 20 miles.
                This API uses Zippotatum service to get the latitude and longitude from Zipcode. 
                This latitude and longitude is used as input to FourSquare API which fetches the list of nearest stores matching store name sorted by distance.
        </p>
        <div>
            <asp:Label runat="server" Text="Integer ZipCode"></asp:Label>
            <asp:TextBox runat="server" ID="zipCodeTextBox"></asp:TextBox>
            <asp:Label runat="server" Text="String Store Name"></asp:Label>
            <asp:TextBox runat="server" ID="storeNameTextBox"></asp:TextBox>
            <asp:Button runat="server" Text="Fetch Addresses" OnClick="fetchAddress"></asp:Button>
        </div>
        <div>
            <asp:Label ID="Address" runat="server" Text="<strong>Nearest address is displayed here: </strong>"></asp:Label>
        </div>
        <h2>
            <strong>Menu Listing Service: Get Menu By Restaurant Name</strong>
        </h2>
        <p>
            <strong>Description:</strong>This service fetches all the items in the menu of a restaurant given its name.
                Sample Restaurant Names are Italiano, Sushi_Spot, Thai_Palace, BBQ_Pit, The_Sushi_Bar, The_Pasta_House, The_Steakhouse
        </p>
        <div>
            <asp:Label runat="server" Text="String Restaurant Name"></asp:Label>
            <asp:TextBox runat="server" ID="restaurantNameTextBox"></asp:TextBox>
            <asp:Button runat="server" Text="Get Menu" OnClick="getMenuByRestaurantName"></asp:Button>
        </div>
        <div>
            <asp:Label ID="Label3" runat="server" Text="<strong>Restaurant Menu is displayed here: </strong>"></asp:Label>
        </div>
        <h2>
            <strong>Menu Listing Service: Get All available items filtered by type</strong>
        </h2>
        <p>
            <strong>Description:</strong>This service fetches all the items from 
                all restaurants belonging to a particular type. Sample item types are Appetizer, Entree, Dessert
        </p>
        <div>
            <asp:Label runat="server" Text="String Item Type"></asp:Label>
            <asp:TextBox runat="server" ID="itemTypeTextBox"></asp:TextBox>
            <asp:Button runat="server" Text="Get Menu By Type" OnClick="getMenuByItemType"></asp:Button>
        </div>
        <div>
            <asp:Label ID="Label4" runat="server" Text="<strong>List of items is displayed here: </strong>"></asp:Label>
        </div>
        <h2>
            <strong>Menu Listing Service: Get All available items less than a price</strong>
        </h2>
        <p>
            <strong>Description:</strong>This service fetches all the items from 
                all restaurants less than particular price. Sample inputs are 5, 10, 15, 20
        </p>
        <div>
            <asp:Label runat="server" Text="Integer Maximum price"></asp:Label>
            <asp:TextBox runat="server" ID="maxPriceTextBox"></asp:TextBox>
            <asp:Button runat="server" Text="Get Menu Less Than Price" OnClick="getMenuLessThanPrice" Width="30px"></asp:Button>
        </div>
        <div>
            <asp:Label ID="Label5" runat="server" Text="<strong>List of items is displayed here: </strong>"></asp:Label>
        </div>
    </form>
</body>
</html>
