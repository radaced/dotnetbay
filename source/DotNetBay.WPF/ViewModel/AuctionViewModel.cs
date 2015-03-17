using System;
using DotNetBay.Core;
using DotNetBay.Model;
using DotNetBay.WPF.View;

namespace DotNetBay.WPF.ViewModel
{
    class AuctionViewModel
    {
        private SimpleMemberService memberService;
        private AuctionService auctionService;
        private Auction auction;

        public RelayCommand<Auction> NewBidCommand { get; private set; } 

        public Auction Auction { get; set; }
        public long Id { get; private set; }
        public double StartPrice { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double CurrentPrice { get; set; }
        public byte[] Image { get; set; }

        private DateTime startDateTimeUtc;
        public DateTime StartDateTimeUtc { get { return startDateTimeUtc; } set { startDateTimeUtc = value; } }

        private DateTime endDateTimeUtc;
        public DateTime EndDateTimeUtc { get { return endDateTimeUtc; } set { endDateTimeUtc = value; } }

        private DateTime closeDateTimeUtc;
        public DateTime CloseDateTimeUtc { get { return closeDateTimeUtc; } set { closeDateTimeUtc = value; } }

        public Member Seller { get; set; }
        public Member Winner { get; set; }
        public Bid ActiveBid { get; set; }
        public bool IsClosed { get; set; }
        public bool IsRunning { get; set; }

        public AuctionViewModel(Auction auction, SimpleMemberService memberService, AuctionService auctionService)
        {
            this.memberService = memberService;
            this.auctionService = auctionService;
        
            NewBidCommand = new RelayCommand<Auction>(NewBidAction);

            if (auction != null)
            {
                Id = auction.Id;
                StartPrice = auction.StartPrice;
                Title = auction.Title;
                Description = auction.Description;
                Image = auction.Image;
                
                CurrentPrice = auction.CurrentPrice;
                StartDateTimeUtc = auction.StartDateTimeUtc;
                EndDateTimeUtc = auction.EndDateTimeUtc;
                CloseDateTimeUtc = auction.CloseDateTimeUtc;
                Seller = auction.Seller;
                Winner = auction.Winner;

                ActiveBid = auction.ActiveBid;
                IsClosed = auction.IsClosed;
                IsRunning = auction.IsRunning;
            }
            else
            {
                StartDateTimeUtc = DateTime.Today;
                EndDateTimeUtc = DateTime.Today;
            }
        }

        private void NewBidAction(Auction a)
        {
            var view = new BidView(a);
            view.ShowDialog();
        }
    }
}
