using System;
using System.Net;
using System.Net.Sockets;
using System.Text;


namespace SocketClient
{
    public class Program
    {
        static void Main(string[] args)
        {
            Start();
        }

        public static void Start()
        {
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("192.168.10.175"), 9050);

            byte[] data = new byte[1024];
            try
            {
                server.Connect(ipEndPoint);
            }
            catch(SocketException e)
            {
                Console.WriteLine("Couldn't connect to the server");
                Console.WriteLine(e.ToString());
            }

            int recievedBytes = server.Receive(data);

            //displays the welcome message from the server to the console
            string convertedDataFromServer = Encoding.UTF8.GetString(data, 0, recievedBytes);
            Console.WriteLine(convertedDataFromServer);

            string clientInputMessage;

            while (true)
            {
                clientInputMessage = Console.ReadLine();

                if (clientInputMessage == "exit")
                    break;

                //set the position of the cursor at the previous line so input message doesnt stay on the console window
                Console.SetCursorPosition(0, Console.CursorTop - 1);
                Console.WriteLine($"You: {clientInputMessage}");

                server.Send(Encoding.UTF8.GetBytes(clientInputMessage));

                data = new byte[1024];
                recievedBytes = server.Receive(data);
                convertedDataFromServer = Encoding.UTF8.GetString(data, 0, recievedBytes);

                Console.WriteLine("Server: " + convertedDataFromServer);
            }

            //disable the socket
            Console.WriteLine("You are dissconected from the server");
            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }
    }
}
