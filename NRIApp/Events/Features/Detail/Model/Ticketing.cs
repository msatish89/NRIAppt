using System;
using System.Collections.Generic;
using System.Text;

namespace NRIApp.Events.Features.Detail.Model
{

    public class EVENT_TICKETING_DETAILS
    {
        public List<Ticketing_Data> ROW_DATA { get; set; }
    }
    public class Ticketing_Data
    {
        public string result { get; set; }
        public string title { get; set; }
        public string tickettypeid { get; set; }
        public string displayorder { get; set; }
        public double originalprice { get; set; }

        public string originalpricetxt { get; set; }
        public string tickettypedesc { get; set; }
        public double servicecharge { get; set; }
        public double chargeperticket { get; set; }

        public string servicechargetxt { get; set; }

        public bool isticketavail { get; set; }
        public string ticketavailtxt{ get; set; }
        public string ticketavailtxtclr { get; set; }
        public bool isticketcloseavail { get; set; }

        public string ticketclosetxt { get; set; }
        public int ticketssold { get; set; }
        public int qtyavailable { get; set; }
        public int minperorder { get; set; }
        public int maxperorder { get; set; }
        public int customersctype { get; set; }
        public int sulorganiser { get; set; }
        public string tickettype { get; set; }
        public string couponstatus { get; set; }
        public string isticketdeliveryoption { get; set; }
        public string tickettrend { get; set; }

       
        public string ticketcount { get; set; }
        public string salesstatus { get; set; }
        public string hideticketnotsale { get; set; }
        public string sellingdate { get; set; }
        public string seatcolor { get; set; }
        public string isseated { get; set; }
        public string soldoutdesc { get; set; }
        public string currency { get; set; }
        public string rate { get; set; }
        public int rowwiseselecttickets { get; set; }
        public bool isdropdownavail { get; set; }

        public string totalservicecharge { get; set; }

        public bool couponavail { get; set; }


    }

    public class Sendpaymentchoosedetails
    {
        public string couponcode { get; set; }
        public string email { get; set; }

        public string parameters { get; set; }
        public string quantity { get; set; }
        public string tickettypeid { get; set; }
        public string displayorder { get; set; }

        public string objectid { get; set; }
    }

    public class Ticket_Calculation
    {
        public List<Ticket_Calculation_Data> ROW_TICKETLINEITEM { get; set; }
    }

    public class Ticket_Calculation_Data
    {
        public string Result { get; set; }
        public string lineitemtypeid { get; set; }
        public string lineitemqty { get; set; }
        public string lineitemunitprice { get; set; }
        public double? lineitemservicecharge { get; set; }

        public string userpid { get; set; }
        public string objectid { get; set; }
        public string lineitemtitle { get; set; }
        public string lineitemid { get; set; }

        public string lineitemtotal { get; set; }

        public string lineitemdesc { get; set; }
        public string lineitemsulekhamargin { get; set; }

        public string lineitempurchasetotal { get; set; }

        public string totalqty { get; set; }
        public string totalservicecharge { get; set; }
        public string totalamount { get; set; }
        public string lineitemtotalamount { get; set; }
        public string lineitemunittaxamount { get; set; }
        public string lineitemtotaltaxamount { get; set; }
        public string totaltaxamount { get; set; }
        public string ticketcharge { get; set; }
        public string ticketchargelineitemtotal { get; set; }
        public string ticketchargetotal { get; set; }
        public string chargepertickettype { get; set; }
        public string ticketchargelineitempay { get; set; }
        public string ticketchargetotalpay { get; set; }
        public string lineitemdealid { get; set; }
        public string lineitemdealtype { get; set; }
        public string lineitemdealqty { get; set; }
        public string lineitemdealpercentage { get; set; }
        public string lineitemdealamount { get; set; }
        public string lineitemdealtotalamount { get; set; }
        public string totaldealamount { get; set; }

        public string couponstatus { get; set; }
        public string coupontype { get; set; }
        public string ticketcoupon { get; set; }

        public string coupondiscounttype { get; set; }
        public string coupondiscountamount { get; set; }
        public string coupondiscountpercentage { get; set; }
        public string couponoffertotalamount { get; set; }
    }

    public class Checkout_Result
    {
        public string result { get; set; }
        public string errorinfo { get; set; }
        public string id { get; set; }
    }


    public class Order_Summary
    {
        public string title { get; set; }
        public string quantitytxt { get; set; }

        public string quantity { get; set; }

        public string totalamounttxt { get; set; }
        public string totalamount { get; set; }

        public string ticketfee { get; set; }

        public string totalservicecharge { get; set; }

        public bool couponavail { get; set; }

        public string couondiscount { get; set; }
    }

    public class Ticketing_final_submit_paraams
    {
        public string firstname { get; set; }
        public string email { get; set; }
        public string countryode { get; set; }
        public string homephone { get; set; }

        public string workphone { get; set; }
        public string chksms { get; set; }

        public string chkwhatsapp { get; set; }
        public string city { get; set; }
        public string state { get; set; }

        public string statecode { get; set; }
        public string country { get; set; }

        public string zipcode { get; set; }

        public string cardnumber { get; set; }
        public string cardname { get; set; }

        public string expirymonth { get; set; }

        public string expiryyear { get; set; }
        public string  cvv { get; set; }
        public string  tablerows { get; set; }
        public string tablerowsseats { get; set; }
        public string seatingenabled { get; set; }
        public string  seatsprocess { get; set; }
        public string currencycode { get; set; }
        public string currencyvalue { get; set; }
        public string timezone { get; set; }
        public string ticketingid { get; set; }
        public string paythrough { get; set; }
        public string seattype { get; set; }
        public string checkoutid { get; set; }
        public string emailchk { get; set; }
        public string meetingid { get; set; }
        public string scheduled { get; set; }

        public string ticketingtypeid { get; set; }


        public string firstname_reg { get; set; }
        public string lastname_reg { get; set; }

        public string dateofbirth { get; set; }

        public string gender { get; set; }

        public string areyoutrained { get; set; }

        public string howtoyouknow { get; set; }

        public string others { get; set; }

        public string registration { get; set; }

        public string sagcountry { get; set; }

        public string couponcode { get; set; }

    }

    public class Ticketing_final_submit_sagg_paraams
    {
        public string firstname_reg { get; set; }
        public string firstname { get; set; }

        public string lastname_reg { get; set; }

        public string dateofbirth { get; set; }

        public string gender { get; set; }
        public string email { get; set; }
        public string countryode { get; set; }
        public string homephone { get; set; }

        public string workphone { get; set; }

        public string areyoutrained { get; set; }

        public string howtoyouknow { get; set; }

        public string others { get; set; }
        public string chksms { get; set; }

        public string chkwhatsapp { get; set; }
        public string city { get; set; }
        public string state { get; set; }

        public string statecode { get; set; }
        public string country { get; set; }

        public string zipcode { get; set; }

        public string cardnumber { get; set; }
        public string cardname { get; set; }

        public string expirymonth { get; set; }

        public string expiryyear { get; set; }
        public string cvv { get; set; }
        public string tablerows { get; set; }
        public string tablerowsseats { get; set; }
        public string seatingenabled { get; set; }
        public string seatsprocess { get; set; }
        public string currencycode { get; set; }
        public string currencyvalue { get; set; }
        public string timezone { get; set; }
        public string ticketingid { get; set; }
        public string paythrough { get; set; }
        public string seattype { get; set; }
        public string checkoutid { get; set; }
        public string emailchk { get; set; }
        public string meetingid { get; set; }
        public string scheduled { get; set; }

        public string ticketingtypeid { get; set; }

        public string registration { get; set; }


    }
    public class paymentresult
    {
        public string name { get; set; }

        public string value { get; set; }
        public string errormsg { get; set; }
        public string errormsgdetails { get; set; }
        public string orderguid { get; set; }
        public string ordernumber { get; set; }
        public string ticketcount { get; set; }
        public string stage { get; set; }
    }

    public class Checkout_Details
    {
        public List<Checkout_Details_Data> ROW_DATA { get; set; }
    }

    public class Checkout_Details_Data
    {
        public string checkoutstatus { get; set; }
        public string contentid { get; set; }
        public string objecttype { get; set; }
        public string title { get; set; }
        public string titleurl { get; set; }

        public string shortdesc { get; set; }
        public string pagetitle { get; set; }
        public string keywords { get; set; }
        public string description { get; set; }

        public string pagedesc { get; set; }

        public string heading { get; set; }
        public string activepathpattern { get; set; }

        public string cityurl { get; set; }

        public string venue { get; set; }
        public string venueaddress { get; set; }
        public string venuecity { get; set; }
        public string city { get; set; }
        public string venuezip { get; set; }
        public string statecode { get; set; }
        public string statename { get; set; }
        public string venuecountry { get; set; }
        public string metro { get; set; }
        public string metrourl { get; set; }
        public string lat { get; set; }
        public string long1 { get; set; }
        public string state { get; set; }
        public string nma { get; set; }
        public string iseventactive { get; set; }
        public string soldout { get; set; }
        public string orgname { get; set; }
        public string orgphone { get; set; }
        public string paymentthrough { get; set; }
        public string soldoutdesc { get; set; }

        public string hostedby { get; set; }
        public string orgid { get; set; }
        public string evtvenue { get; set; }
        public string evteventdate { get; set; }
        public string phone { get; set; }
        public string type { get; set; }
        public string customdate { get; set; }
        public string imageurl { get; set; }
        public string artisthtml { get; set; }
        public string eventtype { get; set; }
        public string coupon { get; set; }
        public string loggedtime { get; set; }
        public string eventdatemon { get; set; }
        public string eventdatedd { get; set; }
        public string eventdateyear { get; set; }
        public string url { get; set; }
        public string title_replaced { get; set; }
        public string ticketdealstatus { get; set; }
        public string eventdatetime { get; set; }
        public string termcondition { get; set; }


        public string artistid { get; set; }
        public string orgurl { get; set; }
        public string artistlist { get; set; }
        public string venuemasterurl { get; set; }
      
        public string cityname { get; set; }
     
        public string seattype { get; set; }
        public string postedtype { get; set; }
        public string timezone { get; set; }
        public string orderform { get; set; }
        public string rate { get; set; }
        public string currency { get; set; }
        public string currencycode { get; set; }
        public string meetingid { get; set; }
        public string scheduled { get; set; }
    }

    //prc_get_city_ticketedeventlist_mobile @lat='40.80405',@long='-73.952833',@distance='100',@pageno='1'
}
