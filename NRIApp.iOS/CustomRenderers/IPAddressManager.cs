using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using Xamarin.Forms;
using Foundation;
using NRIApp.Techjobs.Features.LeadForm.Interfaces;
using UIKit;

[assembly: Dependency(typeof(NRIApp.iOS.CustomRenderers.IPAddressManager))]
namespace NRIApp.iOS.CustomRenderers
{
   public class IPAddressManager : IIPAddressManager
    {
        public string GetIPAddress()
        {
            String ipAddress = "";

            foreach (var netInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (netInterface.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                    netInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    foreach (var addrInfo in netInterface.GetIPProperties().UnicastAddresses)
                    {
                        if (addrInfo.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            ipAddress = addrInfo.Address.ToString();

                        }
                    }
                }
            }

            return ipAddress;
        }
    }
}