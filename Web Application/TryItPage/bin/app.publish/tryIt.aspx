<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="tryIt.aspx.cs" Inherits="TryItPage.tryIt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>TryIt Page</title>
</head>

<body>
    <form id="frm" runat="server">
        <div>
            <asp:Label runat="server" Text="<strong>TryIt Page</strong>"></asp:Label>
        </div>
        <div>
            <asp:Label runat="server" Text="<strong>Ws Hash Service</strong>"></asp:Label>
        </div>
        <div>
            <asp:Label runat="server" Text="<strong>Description:</strong> This service takes a WSDL Url as input 
                and generates a hash of all the operations enlisted in the WSDL file. It returns dictionary of operation names, input types and output type."></asp:Label>
        </div>
        <div>
            <asp:Label runat="server" Text="<strong>String Wsdl Url</strong>"></asp:Label>

            <asp:TextBox runat="server" ID="wsdlUrlText"></asp:TextBox>
            <asp:Button runat="server" Text="Calculate Hash" OnClick="generateHash"></asp:Button>
        </div>
        <div>
            <asp:Label ID="HashTextBox" runat="server" Text="<strong>Hash is displayed here: </strong>"></asp:Label>
        </div>
        <div>
            <asp:Label runat="server" Text="<strong>Find the nearest Store</strong>"></asp:Label>
        </div>
        <div>
            <asp:Label runat="server" Text="<strong>Description:</strong> This servcie takes Zip Code(Integer) and 
                Store Name(string) as input and returns the nearest store address matching the given constraints.
                If no store found nearby, it would show no store found in 20 miles.
                This API uses Zippotatum service to get the latitude and longitude from Zipcode. 
                This latitude and longitude is used as input to FourSquare API which fetches the list of nearest stores matching store name sorted by distance."></asp:Label>
        </div>
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
        <div>
            <asp:Label runat="server" Text="<strong>Login Service</strong>"></asp:Label>
        </div>
        <div>
            <asp:Label runat="server" Text="<strong>Description:</strong> Login Service"></asp:Label>
        </div>
        <div>
            <asp:Label runat="server" Text="String Username"></asp:Label>
            <asp:TextBox runat="server" ID="userNameTextBox"></asp:TextBox>
            <asp:Label runat="server" Text="String Password"></asp:Label>
            <asp:TextBox runat="server" ID="passwordTextBox"></asp:TextBox>
            <asp:Button runat="server" Text="Login" OnClick="login"></asp:Button>
        </div>
        <div>
            <asp:Label ID="Label1" runat="server" Text="<strong>Registration Status is displayed here: </strong>"></asp:Label>
        </div>
         <div>
            <asp:Label runat="server" Text="<strong>Register Service</strong>"></asp:Label>
        </div>
        <div>
            <asp:Label runat="server" Text="<strong>Description:</strong> Register Service"></asp:Label>
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
    </form>
</body>
</html>
