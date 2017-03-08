using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
namespace E2tima.Network
{
    public class UDPClient : IDisposable
    {


        public delegate void Avaliable();
        public event Avaliable DataAvaliable;

        protected internal static int SendPort { get; set; }
        protected internal static int ReceivePort { get; set; }

        protected UdpClient UDP = null;
        protected Thread Tick = null;
        public UDPClient()
        {
            ReceivePort = 15001;
            SendPort = 15000;
            UDP = new UdpClient(ReceivePort);
            Tick = new Thread(Process);
            Tick.IsBackground = true;
            DataAvaliable += () => { };
            Tick.Start();

        }

        void Process()
        {
            while (true)
            {
                if (UDP.Available > 0)
                {
                    DataAvaliable();
                }
            }

            throw new ThreadStateException();
        }

        public string ReceiveFrom(ref IPEndPoint ip) => Encoding.Default.GetString(UDP.Receive(ref ip));

        public void SendTo(string ip, string message)
        {
            UdpClient udp = new UdpClient();


            IPAddress ipaddress = IPAddress.Parse(ip);
            IPEndPoint ipendpoint = new IPEndPoint(ipaddress, SendPort);

            byte[] data = Encoding.Default.GetBytes(message);
            udp.Send(data, data.Length, ipendpoint);

            udp.Close();
        }

        void IDisposable.Dispose()
        {
            Tick.Abort();
            Tick = null;
            UDP.Close();
        }
    }
}
