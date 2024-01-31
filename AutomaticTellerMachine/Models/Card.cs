using System;
using System.Collections.Generic;
using System.Text;

namespace AutomaticTellerMachine.Models
{
    public class Card
    {
        public string Number { get; set; }
        public string Pin { get; set; }
        public decimal Balance { get; set; }
        public string Provider { get; set; }

        public bool IsPinCorrect(string pin)
        {
            return Pin == pin;
        }

        public bool CanWithdraw(decimal amount)
        {
            return Balance >= amount;
        }
    }
}
