using System;

namespace AppPosht.Models
{
    public class Piple
    {
        public Piple(string adress, string fullName, string number, DateTime datePay, double sum)
        {
            Adress = adress ?? throw new ArgumentNullException(nameof(adress));
            FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
            Number = number ?? throw new ArgumentNullException(nameof(number));
            DatePay = datePay;
            Sum = sum;
        }

        public string Adress { get; protected set; }
        public string FullName { get; protected set; }
        public string Number { get; protected set; }
        public DateTime DatePay { get; protected set; }
        public double Sum { get; protected set; }
    }
}