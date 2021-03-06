﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore.Core
{
    /// <summary>
    /// Represents a customer of the store.
    /// </summary>
    public class Customer
    {
        public string Name { get; private set; }

        private List<Rental> _rentals = new List<Rental>();

        public Customer(string name)
        {
            Name = name;
        }

        public void AddRental(Rental arg)
        {
            _rentals.Add(arg);
        }

        public string Statement()
        {
            string result = "Rental Record for " + Name + "\n";
            result = _rentals.Aggregate(result, (current, each) => current + ("\t" + each.Movie.Title + "\t" + each.GetCharge() + "\n"));

            // Add footer lines.
            result += "Amount owed is " + GetTotalCharge() + "\n";
            result += "You earned " + GetTotalFrequentRenterPoints() + " frequent renter points";
            return result;
        }

        // 1: Added new feature - an html version of Statement
        public string HtmlStatement()
        {
            string result = "<H1>Rentals for <em>" + Name + "</em></h1><p>\n";
            result = _rentals.Aggregate(result, (current, each) => current + ("\t" + each.Movie.Title + "\t" + each.GetCharge() + "<br>\n"));

            // Add footer lines.
            result += "<P> You owe <em>" + GetTotalCharge() + "</em><p>\n";
            result += "On this rental you earned <em>" + GetTotalFrequentRenterPoints() + "</em> frequent renter points<p>";
            return result;
        }

        private double GetTotalCharge()
        {
            return _rentals.Sum(each => each.GetCharge());
        }
        
        private int GetTotalFrequentRenterPoints()
        {
            return _rentals.Sum(each => each.GetFrequentRenterPoints());
        }
    }
}
