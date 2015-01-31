using System;
using System.Collections.Generic;

namespace DotNetBay.Model
{
    public class Auction
    {
        public long Id { get; set; }

        public double StartPrice { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }

        public double CurrentPrice { get; set; }

        /// <summary>
        /// Always work with UTC DateTime values to avoid wrong data when serializing the values
        /// </summary>
        public DateTime StartDateTimeUtc { get; set; }

        /// <summary>
        /// Always work with UTC DateTime values to avoid wrong data when serializing the values
        /// </summary>
        public DateTime EndDateTimeUtc { get; set; }

        public Member Seller { get; set; }

        public Member Winner { get; set; }

        public List<Bid> Bids { get; set; }

        public Auction()
        {
            this.Bids = new List<Bid>();
        }

        public bool HasStarted
        {
            get { return this.StartDateTimeUtc <= DateTime.UtcNow; }
        }

        public bool IsFinished
        {
            get { return this.EndDateTimeUtc <= DateTime.UtcNow; }
        }

        public bool IsSuccessful
        {
            get { return this.Winner != null; }
        }
    }
}