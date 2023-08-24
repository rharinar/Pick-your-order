using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WcfService1.Models;

namespace WcfService1
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebGet(UriTemplate = "wsHash?wsdlUrl={wsdlUrl}")] // Add this HTTP GET attribute/directive, use default format
        Dictionary<string, List<Tuple<string, string>>> wsHash(string wsdlUrl);

        [OperationContract]
        [WebGet(UriTemplate = "nearestStore?zipCode={zipCode}&StoreName={StoreName}")] // Add this HTTP GET attribute/directive, use default format
        string nearestStore(int zipCode, string StoreName);

        [OperationContract]
        [WebGet(UriTemplate = "getRestaurantMenu?restaurantName={restaurantName}")] // Add this HTTP GET attribute/directive, use default format
        List<MenuItem> getRestaurantMenu(string restaurantName);

        [OperationContract]
        [WebGet(UriTemplate = "getItemByType?itemType={itemType}")] // Add this HTTP GET attribute/directive, use default format
        List<Tuple<string, MenuItem>> getItemsByType(string itemType);

        [OperationContract]
        [WebGet(UriTemplate = "getItemsLessThanPrice?maxPrice={maxPrice}")] // Add this HTTP GET attribute/directive, use default format
        List<Tuple<string, MenuItem>> getItemsLessThanPrice(Decimal maxPrice);

        [OperationContract]
        [WebGet(UriTemplate = "AbsValue?x ={x}")] // define input format
        int AbsValue(int x);

        [OperationContract]
        [WebGet(UriTemplate = "add2?x ={x}&y ={y}")] // define input format
        int addition(int x, int y);

        // TODO: Add your service operations here
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
