using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Configuration;
using System.Xml;
using System.Xml.Serialization;
using HashingLibrary;

namespace Reg_Auth_Service1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        // Size of the salt used for hashing the password
        private const int SaltSize = 16;
        
        String _userFile = Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, "users.xml");
        String _userFileTemp = Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, "users_temp.xml");
        
        /// <summary>
        /// This method check if the email exists and validates the password
        /// Implementation of the LoginUser method from the IService1 interface
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        string IService1.LoginUser(string email, string password)
        {
            try
            {
                User user = CheckIfemailExists(email);
                if (user == null)
                {
                    return "No account with this email exists";
                }
                if (HashingLibrary.Hashing.VerifyPassword(password, user.Salt, user.Password))
                {
                    return "Login Successful";
                }
                return "Incorrect Password";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// This method registers a user by first checking if the email exists followed by copying all the details 
        /// to the XML file by storing just the hashed password
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        string IService1.RegisterUser(string email, string password, string firstName, string lastName)
        {
            try
            {
                // Check if the user with the given email already exists
                User temp = CheckIfemailExists(email);
                if (temp != null)
                {
                    return "An account with this email address already exists";
                }
                // Generate a salt and hash the password
                string userSalt = HashingLibrary.Hashing.GenerateSalt();
                string passwdEncrypted = HashingLibrary.Hashing.HashPassword(password, userSalt);
                // Create a new User object
                User newUser = new User { Email = email, Password = passwdEncrypted, FirstName = firstName, LastName = lastName, Salt = userSalt };

                // Read the XML user file
                using (WebClient client = new WebClient())
                {
                    XmlTextReader reader = new XmlTextReader(client.OpenRead(_userFile));

                    using (WebClient client1 = new WebClient())
                    {
                        XmlTextWriter writer = new XmlTextWriter(_userFileTemp, Encoding.UTF8);
                        writer.Formatting = Formatting.Indented;

                        // Find the "Users" element in the XML file
                        while (reader.Read())
                        {
                            // Find the "Users" element in the XML file
                            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Users")
                            {
                                // Write the "Users" element to the new XML file
                                writer.WriteStartElement(reader.Name);

                                // Copy the existing child elements of the "Users" element to the new XML file
                                while (reader.Read())
                                {
                                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "User")
                                    {
                                        string line = reader.ReadInnerXml();
                                        User user = User.FromXml(line);
                                        writer.WriteStartElement("User");
                                        writer.WriteElementString("Email", user.Email);
                                        writer.WriteElementString("Password", user.Password);
                                        writer.WriteElementString("FirstName", user.FirstName);
                                        writer.WriteElementString("LastName", user.LastName);
                                        writer.WriteElementString("Salt", user.Salt);
                                        writer.WriteEndElement(); // User
                                    }
                                    //writer.WriteNode(reader, true);
                                }

                                // Write the new "User" element to the new XML file
                                writer.WriteStartElement("User");
                                writer.WriteElementString("Email", newUser.Email);
                                writer.WriteElementString("Password", newUser.Password);
                                writer.WriteElementString("FirstName", newUser.FirstName);
                                writer.WriteElementString("LastName", newUser.LastName);
                                writer.WriteElementString("Salt", newUser.Salt);
                                writer.WriteEndElement(); // User

                                writer.WriteEndElement(); // Users
                            }
                        }

                        reader.Close();

                        writer.Close();
                        // Replace the original XML file with the new one
                        File.Delete(_userFile);
                        File.Move(_userFileTemp, _userFile);
                        return "Registration Successful";

                    }

                }
                
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // This method generates a random salt of the specified size and returns it as a Base64-encoded string.
        //private string GenerateSalt()
        //{
        //    using (var rng = new RNGCryptoServiceProvider())
        //    {
        //        byte[] salt = new byte[SaltSize];
        //        rng.GetBytes(salt);
        //        return Convert.ToBase64String(salt);
        //    }
        //}
        ///// <summary>
        ///// The function first converts the salt string from Base64 encoding to a byte array using the Convert.FromBase64String() method.
        ///// It then creates a new instance of the SHA256Managed class to compute the hash of the salted password.
        ///// The password string is converted to a byte array using the Encoding.UTF8.GetBytes() method.
        ///// A new byte array is created with a length equal to the sum of the lengths of the password and salt byte arrays.This array will hold the salted password.
        ///// The Buffer.BlockCopy() method is used to copy the password and salt byte arrays into the salted password array.
        ///// The ComputeHash() method of the SHA256Managed instance is called with the salted password byte array as its input, and it returns a hashed byte array.
        ///// Finally, the hashed byte array is converted to a Base64-encoded string using the Convert.ToBase64String() method and returned by the function.
        ///// </summary>
        ///// <param name="password"></param>
        ///// <param name="salt"></param>
        ///// <returns></returns>
        //private string HashPassword(string password, string salt)
        //{
        //    byte[] saltBytes = Convert.FromBase64String(salt);
        //    using (var sha = new SHA256Managed())
        //    {
        //        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        //        byte[] saltedPassword = new byte[passwordBytes.Length + saltBytes.Length];
        //        Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
        //        Buffer.BlockCopy(saltBytes, 0, saltedPassword, passwordBytes.Length, saltBytes.Length);
        //        byte[] hash = sha.ComputeHash(saltedPassword);
        //        return Convert.ToBase64String(hash);
        //    }
        //}
        /// <summary>
        /// This is a function that takes three input parameters: a password string, a salt string, and a hashed password string.
        /// It returns a boolean value indicating whether the provided password matches the hashed password.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <param name="hashedPassword"></param>
        /// <returns></returns>
        //private bool VerifyPassword(string password, string salt, string hashedPassword)
        //{
        //    string hashedPasswordToVerify = HashPassword(password, salt);
        //    return hashedPasswordToVerify == hashedPassword;
        //}

        /// <summary>
        /// This function takes an email string as input and returns a User object if the email exists in the user file, or null if it does not exist.
        /// If an exception occurs, it returns a User object with an empty email and logs the exception.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private User CheckIfemailExists(string email)
        {
            string error = "";
            try
            {
                // Initialize a null User object to store the user with the given email, if found.
                User ans = null;
                using (StreamReader sr = new StreamReader(_userFile))
                {
                    // Load the user file as an XmlDocument.
                    XmlDocument doc = new XmlDocument();
                    doc.Load(sr);
                    XmlNodeList userNodes = doc.SelectNodes("//Users/User");
                    // Select all user nodes in the document.

                    // Iterate through the user nodes and check if any have the specified email.
                    foreach (XmlNode userNode in userNodes)
                    {
                        string line = userNode.InnerXml;
                        User user = User.FromXml(line);
                        if (user.Email == email)
                        {
                            ans = user;
                            break;
                        }
                    }
                }
                // Return the User object with the specified email, if found.
                return ans;
            }
            catch(Exception ex)
            {
                // If an exception occurs, log the error and return a User object with an empty email.
                // The email will be checked to determine if the function was successful.
                return new User { Email = error };
            }
            
        }
    }

    // This is a class representing a user of the system
    public class User
    {
        // Properties of a user
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Salt { get; set; }

        // Method to create a User object from an XML string
        public static User FromXml(string xml)
        {
            // Wrap the XML string in a <User> tag
            xml = "<User>" + xml + "</User>";
            
            // Create a StringReader to read the XML string
            StringReader stringReader = new StringReader(xml);
            
            // Create an XmlSerializer for the User class
            XmlSerializer serializer = new XmlSerializer(typeof(User));

            // Deserialize the XML string into a User object and return it
            return (User)serializer.Deserialize(stringReader);
        }
    }
}
