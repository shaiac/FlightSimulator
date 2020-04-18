﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    //Interface for the Client
    public interface IClient
    {
        void connect(string ip, int port);
        void write(string command);
        string read();
        void disconnect();
    }
}
