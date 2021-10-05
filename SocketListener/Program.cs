using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketListener
{
    public class Program
    {
        static void Main(string[] args)
        {
            Start();
        }

        public static void Start()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse("192.168.10.175"), 9050);
             
            //searching for a connection on the other end
            socket.Bind(localEndPoint);
            socket.Listen(10);

            Console.WriteLine("Waiting for a user to join the chat...");

            Socket client = socket.Accept();

            IPEndPoint clientEndPoint = (IPEndPoint)client.RemoteEndPoint;
            Console.WriteLine($"Sucessfully connected with {clientEndPoint.Address} at port {clientEndPoint.Port}");

            byte[] data = new byte[1024];

            string welcomeMessage = "Welcome to the test server chat room!";
            data = Encoding.UTF8.GetBytes(welcomeMessage);

            //send the message to the other node
            client.Send(data, data.Length, SocketFlags.None);

            string serverInputMessage;
            int bytesRecieved;

            while (true)
            {
                data = new byte[1024];
                bytesRecieved = client.Receive(data);

                if (bytesRecieved == 0)
                    break;

                Console.WriteLine("Client: " + Encoding.UTF8.GetString(data));

                serverInputMessage = Console.ReadLine();

                //cursor is set at previous line, so input message doesnt stay on the console
                Console.SetCursorPosition(0, Console.CursorTop - 1);

                Console.WriteLine($"You: {serverInputMessage}");
                client.Send(Encoding.UTF8.GetBytes(serverInputMessage));
            }

            Console.WriteLine($"Disconnected from {clientEndPoint.Address}");
            client.Close();
            socket.Close();
        }
    }
}
