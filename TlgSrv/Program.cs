using System;
using Telegram;


namespace TlgSrv
{
    internal class Program
    {
        protected const string Tocken = "1279990267:AAFME7qmFYV74GbhA3gXTcKMnoEDuw5gx8g";
        static void Main(string[] args)
        {
            TlgBase tlgBase = new TlgBase(Tocken);
            tlgBase.Start();


            Console.ReadKey();
        }




    }
}
