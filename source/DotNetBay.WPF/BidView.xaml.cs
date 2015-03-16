using System;
using System.Windows;
using DotNetBay.Core;
using DotNetBay.Model;
using Xceed.Wpf.DataGrid;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for BidView.xaml
    /// </summary>
    public partial class BidView
    {
        private Auction auction;
        private readonly SimpleMemberService simpleMemberService;
        private readonly AuctionService auctionService;

        public Auction Auction { get { return auction; } }
        public Double Bid { get; set; }

        public BidView() {
            InitializeComponent();
            DataContext = this;

            var app = Application.Current as App;
            
            if (app == null) throw new NullReferenceException("App is null");
            simpleMemberService = new SimpleMemberService(app.MainRepository);
            auctionService = new AuctionService(app.MainRepository, simpleMemberService);
        }

        public BidView(Auction data):this()
        {
            auction = data;
        }

        private void PlaceBid_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                auctionService.PlaceBid(simpleMemberService.GetCurrentMember(), Auction, Bid);
                Close();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool ValidateInput()
        {
            var valid = true;
            valid &= Bid >= 0;
            valid &= Bid > Auction.CurrentPrice;

            return valid;
        }
    }
}
