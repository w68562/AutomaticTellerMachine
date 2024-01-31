using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AutomaticTellerMachine.DataTransportLayer
{
    public class FileService
    {
        private const string CONFIG_PATH = "config.txt";
        private const string CARDS_PATH = "cards.txt";
        public List<string> GetConfig()
        {
            List<string> acceptedProviders = new List<string>();

            try
            {
                using (StreamReader reader = new StreamReader(CONFIG_PATH))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        acceptedProviders.Add(line);
                    }
                }
            }catch (Exception)
            {
                Console.WriteLine("Błąd odczytu pliku konfiguracji");
            }
            return acceptedProviders;
        }

        public void SaveConfig(List<string> config)
        {
            try
            {
                using(StreamWriter writer = new StreamWriter(CONFIG_PATH))
                {
                    foreach (string line in config)
                    {
                        writer.WriteLine(line);
                    }
                }
            }catch(Exception)
            {
                Console.WriteLine("Błąd zapisu konfiguracji.");
            }
        }

        public List<string> GetCards()
        {
            List<string> cards = new List<string>();
            try
            {
                using (StreamReader reader = new StreamReader(CARDS_PATH))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        cards.Add(line);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Błąd odczytu pliku konfiguracji");
            }

            return cards;
        }

        public void SaveCards(List<string> cards)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(CARDS_PATH))
                {
                    foreach (string line in cards)
                    {
                        writer.WriteLine(line);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Błąd zapisu konfiguracji.");
            }
        }
    }
}
