using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Collections.ObjectModel;
using System.Threading;

namespace RemoteControl
{
    class Port : ObservableCollection<string>
    {
        #region private fields
        private string selectedport;
        private uint selectedbaud;
        private Parity selectedparity;
        private StopBits stopbits = StopBits.One;
        private Handshake handshake = Handshake.None;
        private string data;
        private char[] datasend = { (char)0 };
        private SerialPort serialPort = new SerialPort();
        public bool serialportstarted { get; set; }

        private ObservableCollection<uint> baud = new ObservableCollection<uint>();
        #endregion

        public string Data
        {
            get { return data; }
            set 
            {
                // TO DO: hexconverter
                char[] arr = value.ToArray<char>();
                switch (arr[0])
                {
                    case 'A':
                        datasend[0] = (char) 160;
                        break;
                    case 'B':
                        datasend[0] = (char)176;
                        break;
                    case 'C':
                        datasend[0] = (char)192;
                        break;
                    case 'D':
                        datasend[0] = (char)208;
                        break;
                    case 'E':
                        datasend[0] = (char)224;
                        break;
                    case 'F':
                        datasend[0] = (char)240;
                        break;
                    case '0':
                        datasend[0] = (char)0;
                        break;
                    case '1':
                        datasend[0] = (char)16;
                        break;
                    case '2':
                        datasend[0] = (char)32;
                        break;
                    case '3':
                        datasend[0] = (char)48;
                        break;
                    case '4':
                        datasend[0]  = (char)64;
                        break;
                    case '5':
                        datasend[0] = (char)80;
                        break;
                    case '6':
                        datasend[0] = (char)96;
                        break;
                    case '7':
                        datasend[0] = (char)112;
                        break;
                    case '8':
                        datasend[0] = (char)128;
                        break;
                    case '9':
                        datasend[0] = (char)144;
                        break;
                }
                switch (arr[1])
                {
                    case 'A':
                        datasend[0] += (char)10;
                        break;
                    case 'B':
                        datasend[0] += (char)11;
                        break;
                    case 'C':
                        datasend[0] += (char)12;
                        break;
                    case 'D':
                        datasend[0] += (char)13;
                        break;
                    case 'E':
                        datasend[0] += (char)14;
                        break;
                    case 'F':
                        datasend[0] += (char)15;
                        break;
                    case '0':
                        datasend[0] += (char)0;
                        break;
                    case '1':
                        datasend[0] += (char)1;
                        break;
                    case '2':
                        datasend[0] = (char)2;
                        break;
                    case '3':
                        datasend[0] += (char)3;
                        break;
                    case '4':
                        datasend[0] += (char)4;
                        break;
                    case '5':
                        datasend[0] += (char)5;
                        break;
                    case '6':
                        datasend[0] += (char)6;
                        break;
                    case '7':
                        datasend[0] += (char)7;
                        break;
                    case '8':
                        datasend[0] += (char)8;
                        break;
                    case '9':
                        datasend[0] += (char)9;
                        break;
                }
                data = value;
            }
        }
        public void Write()
        {
            while(serialportstarted == true)
            {
                //char[] arr = data.ToArray<char>();
                //serialPort.Write(arr,0,2);
                serialPort.Write(datasend, 0, 1);
            }
        }
        public bool Start()
        {
            serialPort.PortName = selectedport;
            serialPort.DataBits = 8;
            serialPort.BaudRate = (int)selectedbaud;
            serialPort.Parity = selectedparity;
            serialPort.StopBits = stopbits;
            serialPort.Handshake = handshake;
            serialPort.Open();
            return true;
        }

        public bool Stop()
        {
            serialPort.Close();
            return false;
        }

        public ObservableCollection<uint> Baud
        {
            get { return baud; }
        }
        public uint addBaudrate
        {
            set { baud.Add(value); }
        }

        public Parity Selectedparity
        {
            get { return Selectedparity; }
            set { selectedparity = value; }
        }

        public string Selectedport
        {
            get { return selectedport; }
            set { selectedport = value; }
        }

        public uint Selectedbaud
        {
            get { return Selectedbaud; }
            set { selectedbaud = value; }
        }


    }
}
