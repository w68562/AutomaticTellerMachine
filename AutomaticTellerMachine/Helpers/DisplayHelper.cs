using System;
using System.Collections.Generic;
using System.Text;

namespace AutomaticTellerMachine.Helpers
{
    public class DisplayHelper
    {
        public void WelcomeMessage() => Console.WriteLine("BANKOMAT");
        public void ThankYou() => Console.WriteLine("Dziękujemy za skorzystanie z naszego bankomatu");
        public void Options()
        {
            Console.WriteLine("Wybierz jedną opcje:");
            Console.WriteLine("1. Wypłata");
            Console.WriteLine("2. Opcje techniczne");
        }

        public void TechOptions()
        {
            Console.WriteLine("Wybierz jedną opcje:");
            Console.WriteLine("1. Dodaj dostawcę");
            Console.WriteLine("2. Usuń dostawcę");
        }

        public void RemoveProviderOptions(List<string> providers)
        {
            Console.WriteLine("Wybierz dostawcę do usunięcia");
            DisplayList(providers);
        }

        private void DisplayList(List<string> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                Console.WriteLine(i + 1 + ": " + list[i]);
            }
        }

        public void SelectCard(List<string> cardNumbers)
        {
            Console.WriteLine("Wybierz kartę");
            DisplayList(cardNumbers);
        }

        public void Pin()
        {
            Console.WriteLine("Podaj pin");
        }

        public void AmoutToWithdraw()
        {
            Console.WriteLine("Podaj kwotę do wypłaty");
        }

        public void WithdrawComplete()
        {
            Console.WriteLine("Wypłata zakończona");
        }

        public void NotEnough()
        {
            Console.WriteLine("Błędna liczba lub niewystaraczająca ilość środków na koncie");
        }

        public void WrongPin()
        {
            Console.WriteLine("Błędny pin");
        }

        public void UnsupportedProvider(string provider)
        {
            Console.WriteLine($"Karta dostawcy {provider} nie jest obsługiwana w tym bankomacie");
        }
    }
}
