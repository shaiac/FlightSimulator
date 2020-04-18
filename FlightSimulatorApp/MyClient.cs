using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.ComponentModel;
using System.Threading;

namespace FlightSimulatorApp
{
    public class MyClient : IClient, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        TcpClient client;
        Stream stm;
        String exception;
        bool is_con;

        public MyClient()
        {
            this.exception = "";
            is_con = false;
        }

        public string Exception
        {
            get { return this.exception; }
            set
            {
                this.exception = value;
                NotifyPropertyChanged("Exception");
            }
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        //Connecting to the server, TCP connection setting the read time out to 10 sec.
        public void connect(string ip, int port)
        {
            client = new TcpClient();
            try
            {
                Console.WriteLine("Connecting...");
                client.Connect(ip, port);
                Console.WriteLine("Connected");
                stm = client.GetStream();
                client.GetStream().ReadTimeout = 10000;
            }
            catch
            {
                throw new Exception();
            }
            is_con = true;
        }

        //Writing to the server.
        public void write(string command)
        {
            if (is_con)
            {
                byte[] bytes = Encoding.ASCII.GetBytes(command);
                try
                {
                    stm.Write(bytes, 0, bytes.Length);
                }
                catch
                {
                    Console.WriteLine("Cannot Write to the server, the server is off");
                    is_con = false;
                }
            }
        }
        //Reading from the server.
        public string read()
        {
            if (is_con)
            {
                string str = "";
                byte[] bytes = new byte[100];
                try
                {
                    stm.Read(bytes, 0, 100);
                    str = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                }
                catch
                {
                    Console.WriteLine("Cannot Read from the server, the server is off");
                    is_con = false;
                }
                return str;
            }
            return "";
        }
        //Closing the connection
        public void disconnect()
        {
            client.Close();
        }
    }
}
