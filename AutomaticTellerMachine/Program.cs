using AutomaticTellerMachine.DataTransportLayer;
using AutomaticTellerMachine.Helpers;
using AutomaticTellerMachine.Services;
using System;

namespace AutomaticTellerMachine
{
    public class Program
    {
        static void Main(string[] args)
        {
            FileService fileService = new FileService();
            DisplayHelper displayHelper = new DisplayHelper();
            CardService cardService = new CardService(fileService);
            ConfigService configService = new ConfigService(fileService);

            ATM atm = new ATM(cardService,displayHelper,configService);
            atm.Start();
            displayHelper.ThankYou();

        }
    }
}
