using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using SerialLogger;
using PacketFormat;

namespace RdrDoorClient
{
    class RdrClient
    {
        UdpClient UdpCli;

        public RdrClient(string SrvIpAddress)
        {
            try
            {

                UdpCli = new UdpClient(33233);
                IPAddress RemoteMac = IPAddress.Parse(SrvIpAddress);
                UdpCli.Connect(RemoteMac, 33211);
            }
            catch (Exception ex)
            {
                applog.logexcep("RdrDoorClient: RdrClient()",ex.Message);
            }
        }

        public bool ChangeServerIp(string SrvIpAddress)
        {
            try
            {
                if (UdpCli != null)
                {
                    //UdpCli.Close();
                    IPAddress RemoteMac = IPAddress.Parse(SrvIpAddress);
                    UdpCli.Connect(RemoteMac, 33211);
                    return true;
                }

                return false;
            }
            catch(Exception ex)
            {
                
                applog.logexcep("RdrDoorClient: ChangeServerIp()", ex.Message);
                return false;
            }


        }

        public bool Send(RdrMsgFormat p)
        {
            try
            {
                byte[] byteData = p.GetPacketBytes();

                UdpCli.Send(byteData, byteData.Length);
                return true;
            }
            catch (Exception ex)
            {
                applog.logexcep("RdrDoorClient: Send()",ex.Message);
                return false;
            }
        }
    }
}
