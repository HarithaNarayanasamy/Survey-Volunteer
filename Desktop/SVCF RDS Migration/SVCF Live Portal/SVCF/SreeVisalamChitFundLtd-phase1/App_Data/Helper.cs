using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;
using System.Net;
using System.Net.NetworkInformation;

namespace SreeVisalamChitFundLtd_phase1
{
    public static class Helper
    {
        public static List<T> DataTableToList<T>(this DataTable table) where T : class, new()
        {
            try
            {
                List<T> list = new List<T>();

                foreach (var row in table.AsEnumerable())
                {
                    T obj = new T();
                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                        }
                        catch (Exception err)
                        {
                            continue;
                        }
                    }
                    list.Add(obj);
                }
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

       
    }


    public static class NetworkUtils
    {
        // http://www.codeproject.com/KB/IP/host_info_within_network.aspx
        [System.Runtime.InteropServices.DllImport("iphlpapi.dll", ExactSpelling = true)]
        static extern int SendARP(int DestIP, int SrcIP, byte[] pMacAddr, ref int PhyAddrLen);

        /// <summary>
        /// Gets the MAC address (<see cref="PhysicalAddress"/>) associated with the specified IP.
        /// </summary>
        /// <param name="ipAddress">The remote IP address.</param>
        /// <returns>The remote machine's MAC address.</returns>
        public static PhysicalAddress GetMacAddress(IPAddress ipAddress)
        {
            const int MacAddressLength = 6;
            int length = MacAddressLength;
            var macBytes = new byte[MacAddressLength];
            SendARP(BitConverter.ToInt32(ipAddress.GetAddressBytes(), 0), 0, macBytes, ref length);
            return new PhysicalAddress(macBytes);
        }
    }







}
