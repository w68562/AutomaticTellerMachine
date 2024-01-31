using AutomaticTellerMachine.Helpers;
using AutomaticTellerMachine.Services;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutomaticTellerMachine
{
    public class ATM
    {
        private readonly ConfigService configService;
        private readonly DisplayHelper displayHelper;
        private readonly CardService cardService;

        public ATM(CardService cardService, DisplayHelper displayHelper, ConfigService configService)
        {
            this.cardService = cardService;
            this.displayHelper = displayHelper;
            this.configService = configService;
        }

        public void Start()
        {
            displayHelper.WelcomeMessage();
            displayHelper.Options();
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    WithdrawFlow();
                    break;
                case "2":
                    OptionsFlow();
                    break;
                default:
                    Console.WriteLine("Wybrano błędną opcję");
                    Start();
                    break;
            }
        }

        private void WithdrawFlow()
        {
            var cardNumbers = cardService.Cards.Select(x => x.Number).ToList();
            displayHelper.SelectCard(cardNumbers);
            var option = Console.ReadLine();
            var isOptionInt = int.TryParse(option, out int intOption);
            if (!isOptionInt)
            {
                Console.WriteLine("Wybrano błędną opcję");
                Start();
            }
            var index = intOption - 1;
            try
            {
                var selectedCard = cardService.Cards[index];
                if (!configService.IsProviderAccepted(selectedCard.Provider))
                {
                    displayHelper.UnsupportedProvider(selectedCard.Provider);
                    Start();
                }

                displayHelper.Pin();
                var pin = Console.ReadLine();
                if (selectedCard.IsPinCorrect(pin))
                {
                    displayHelper.AmoutToWithdraw();
                
                    var amount = Console.ReadLine();
                    var isDecimal = decimal.TryParse(amount, out decimal decimalOption);
                    if (isDecimal && selectedCard.CanWithdraw(decimalOption))
                    {
                        displayHelper.WithdrawComplete();
                        selectedCard.Balance -= decimalOption;
                        cardService.SaveCards();
                    }
                    else
                    {
                        displayHelper.NotEnough();
                        Start();
                    }
                }
                else
                {
                    displayHelper.WrongPin();
                    Start();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Wybrano błędną opcję");
                Start();
            }
        }

        private void OptionsFlow()
        {
            displayHelper.TechOptions();
            var option = Console.ReadLine();
            switch (option)
            {
                case "1":
                    var provider = Console.ReadLine();
                     var wasAdded = configService.UpdateConfig(provider);
                    if (wasAdded)
                    {
                        Console.WriteLine("Dostawca został dodany");
                    }
                    else
                    {
                        Console.WriteLine("Dostawca nie został dodany - istniał wcześniej");
                    }
                    Start();
                    break;
                case "2":
                    RemoveProviderFlow();
                    Start();
                    break;
                default:
                    Console.WriteLine("Wybrano błędną opcję");
                    Start();
                    break;
            }
        }

        private void RemoveProviderFlow()
        {
            var providers = configService.GetProviders();
            displayHelper.RemoveProviderOptions(providers);
            var option = Console.ReadLine();
            var isOptionInt = int.TryParse(option, out int intOption);
            if (!isOptionInt)
            {
                Console.WriteLine("Wybrano błędną opcję");
                Start();
            }
            var index = intOption - 1;
            try
            {
                configService.DeleteProvider(providers[index]);
            }catch(Exception ex)
            {
                Console.WriteLine("Wybrano błędną opcję");
                Start();
            }
        }
    }
}
