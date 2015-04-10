using System;
using System.Windows;
using DotNetBay.Core;
using DotNetBay.Model;

namespace DotNetBay.WPF.ViewModel
{
    class BidViewModel : ViewModelBase
    {
        private Auction auction;
        private readonly SimpleMemberService simpleMemberService;
        private readonly AuctionService auctionService;

        public Auction Auction { get { return auction; } }
        public Double Bid { get; set; }
        public RelayCommand<Window> PlaceBidCommand { get; private set; }
        public RelayCommand<Window> CancelCommand { get; private set; }

        public BidViewModel(Auction auction, AuctionService auctionService, SimpleMemberService simpleMemberService)
        {
            this.auction = auction;
            this.auctionService = auctionService;
            this.simpleMemberService = simpleMemberService;

            PlaceBidCommand = new RelayCommand<Window>(PlaceBidAction);
            CancelCommand = new RelayCommand<Window>(CancelAction);
        }

        private bool ValidateInput()
        {
            var valid = true;
            valid &= Bid >= 0;
            valid &= Bid > Auction.CurrentPrice;

            return valid;
        }

        private void PlaceBidAction(Window window)
        {
            if (ValidateInput())
            {
                auctionService.PlaceBid(auction, Bid);
                window.Close();
            }
        }

        private void CancelAction(Window window)
        {
            window.Close();
        }
    }
}
