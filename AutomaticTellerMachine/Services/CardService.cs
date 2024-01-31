using AutomaticTellerMachine.DataTransportLayer;
using AutomaticTellerMachine.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutomaticTellerMachine.Services
{
    public class CardService
    {
        private readonly FileService fileService;
        public List<Card> Cards { get; set; }

        public CardService(FileService fileService)
        {
            this.fileService = fileService;
            InitCards();
        }

        private void InitCards()
        {
            Cards = new List<Card>();
            var cardsStringList = fileService.GetCards();
            foreach (var card in cardsStringList)
            {
                CreateCard(card);
            }
        }

        private void CreateCard(string cardString)
        {
            var cardProps = cardString.Split(";");
            Cards.Add(new Card() { Number= cardProps[0], Pin = cardProps[1], Balance = decimal.Parse(cardProps[2]), Provider = cardProps[3] });
        }

        public void SaveCards()
        {
            List<string> cardsToSave = new List<string>();
            foreach (var card in Cards)
            {
                cardsToSave.Add(card.Number+";"+card.Pin+";"+card.Balance+";"+card.Provider);
            }

            fileService.SaveCards(cardsToSave);
        }
    }
}
