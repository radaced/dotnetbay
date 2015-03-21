using System;
using DotNetBay.Core;
using DotNetBay.Model;
using DotNetBay.WPF.View;

namespace DotNetBay.WPF.ViewModel
{
    class AuctionViewModel : ViewModelBase
    {
        private Auction auction;
        private double currentPrice;
        private double bidCount;
        private string currentWinner;
        private DateTime? closedDateLocal;
        private string winner;
        private string status;
        private bool isRunning;

        public RelayCommand<object> NewBidCommand { get; private set; }

        public Auction Auction { get { return auction; } }

        public string Title { get { return auction.Title; } }

        public byte[] Image { get { return auction.Image; } }

        public double StartPrice { get { return auction.StartPrice; } }

        public string Status
        {
            get { return status; } 
            set { Set(() => Status, ref status, value); }
        }

        public string Description { get; set; }

        public double CurrentPrice
        {
            get { return currentPrice; }
            set
            {
                if (currentPrice != value)
                {
                    currentPrice = value;
                    RaisePropertyChanged(() => CurrentPrice);
                }
            }
        }

        public double BidCount
        {
            get { return bidCount; }
            set
            {
                if (bidCount != value)
                {
                    bidCount = value;
                    RaisePropertyChanged(() => BidCount);
                }
            }
        }

        public string CurrentWinner
        {
            get { return currentWinner; }
            set { Set(() => CurrentWinner, ref currentWinner, value); }
        }

        public DateTime StartDateTimeLocal { get { return auction.StartDateTimeUtc.ToLocalTime(); } }

        public DateTime EndDateTimeLocal { get { return auction.EndDateTimeUtc.ToLocalTime(); } }

        public DateTime? CloseDateTimeLocal
        {
            get { return closedDateLocal != null ? closedDateLocal.Value.ToLocalTime() : (DateTime?)null; }
            set { closedDateLocal = value; }
        }

        public Member Seller { get { return auction.Seller; } }

        public String Winner
        {
            get { return winner; }
            set { Set(() => Winner, ref winner, value); }
        }

        public bool IsRunning
        {
            get { return isRunning; }
            set
            {
                if (isRunning != value)
                {
                    isRunning = value;
                    RaisePropertyChanged(() => IsRunning);
                }
            }
        }

        public AuctionViewModel(Auction auction)
        {
            this.auction = auction;

            NewBidCommand = new RelayCommand<object>(NewBidAction);

            Update(auction);
        }

        private void NewBidAction(object o)
        {
            var view = new BidView(this.auction);
            view.ShowDialog();
        }

        public void Update(Auction auction)
        {
            this.auction = auction;

            Status = this.auction.IsClosed ? "Closed" : "Valid";
            CurrentPrice = this.auction.CurrentPrice;
            BidCount = this.auction.Bids.Count;
            IsRunning = this.auction.IsRunning;

            if (this.auction.ActiveBid != null)
                CurrentWinner = this.auction.ActiveBid.Bidder.DisplayName;

            if (this.auction.CloseDateTimeUtc > DateTime.MinValue)
                CloseDateTimeLocal = this.auction.CloseDateTimeUtc.ToLocalTime();

            if (this.auction.Winner != null)
                Winner = this.auction.Winner.DisplayName;
        }
    }
}
