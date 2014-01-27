using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Connectivity;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using System.Diagnostics;

namespace Tivomote.Common
{
    class TivoRemote
    {
        public string Hostname { get; set; }

        public TivoRemote(string hostname)
        {
            Hostname = hostname;
        }

        public async void setCommand(string command)
        {
            try
            {

                StreamSocket socket = new StreamSocket();
                HostName hostname = new HostName(Hostname);

                await socket.ConnectAsync(hostname, "31339", SocketProtectionLevel.PlainSocket);

                DataWriter writer = new DataWriter(socket.OutputStream);

                string stringToSend = String.Format("IRCODE {0}\r", command);
                writer.WriteString(stringToSend);

                await writer.StoreAsync();

                writer.DetachStream();
                writer.Dispose();

                socket.Dispose();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

            }
        }

    }
}
