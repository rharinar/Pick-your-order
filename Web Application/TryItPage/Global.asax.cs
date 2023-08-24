using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml;
using HashingLibrary;

namespace TryItPage
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        // Log each incoming web request
        void Application_BeginRequest(object sender, EventArgs e)
        {
            // Append the log entry for the current request to the log file
            AppendLogEntry();
        }


        // This function appends a log entry to an XML file
        // The log entry includes information about the current HTTP request, such as timestamp, URL, method, user agent, referrer, IP address, and status code
        // If the status code is 400 or higher, an "error" element is included with a message and stack trace
        // The log file is opened in append mode, and the log entry is written using an XmlWriter object with UTF-8 encoding and indentation enabled.
        private void AppendLogEntry()
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = Encoding.UTF8;
            settings.Indent = true;
            // Open the log file in append mode
            string logFilePath = Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, "log.xml");
            using (FileStream stream = new FileStream(logFilePath, FileMode.Append))
            {
                // Create a new XmlWriter object that writes to the stream
                using (XmlWriter writer = XmlWriter.Create(stream, settings))
                {
                    // Write the log entry for the current request
                    writer.WriteStartElement("request");
                    writer.WriteElementString("timestamp", DateTime.Now.ToString());
                    writer.WriteElementString("url", HttpContext.Current.Request.Url.ToString());
                    writer.WriteElementString("method", HttpContext.Current.Request.HttpMethod);
                    writer.WriteElementString("userAgent", HttpContext.Current.Request.UserAgent);
                    writer.WriteElementString("referrer", HttpContext.Current.Request.UrlReferrer?.ToString() ?? "");
                    writer.WriteElementString("ipAddress", HttpContext.Current.Request.UserHostAddress);
                    writer.WriteElementString("statusCode", HttpContext.Current.Response.StatusCode.ToString());
                    if (HttpContext.Current.Response.StatusCode >= 400)
                    {
                        writer.WriteStartElement("error");
                        writer.WriteElementString("message", HttpContext.Current.Server.GetLastError()?.Message ?? "");
                        writer.WriteElementString("stackTrace", HttpContext.Current.Server.GetLastError()?.StackTrace ?? "");
                        writer.WriteEndElement(); // end error
                    }
                    writer.WriteEndElement(); // end request
                }
            }
        }



    }
}