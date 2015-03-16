using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using DotNetBay.Core;
using DotNetBay.Core.Execution;
using DotNetBay.Model;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<Auction> auctions = new ObservableCollection<Auction>();
        private readonly AuctionService auctionService;

        public ObservableCollection<Auction> Auctions
        {
            get { return auctions; }
            set { auctions = value; OnPropertyChanged(); }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            var app = Application.Current as App;

            app.AuctionRunner.Auctioneer.AuctionEnded += AuctioneerOnAuctionEnded;
            app.AuctionRunner.Auctioneer.AuctionStarted += AuctioneerOnAuctionStarted;
            app.AuctionRunner.Auctioneer.BidAccepted += AuctioneerOnBidAccepted;
            app.AuctionRunner.Auctioneer.BidDeclined += AuctioneerOnBidDeclined;

            var repository = ((App) Application.Current).MainRepository;
            auctionService = new AuctionService(repository, new SimpleMemberService(repository));

            foreach (var auction in auctionService.GetAll())
            {
                auctions.Add(auction);
            }
        }

        private void AuctioneerOnBidDeclined(object sender, ProcessedBidEventArgs e)
        {
            var allAuctions = auctionService.GetAll();
            Auctions = new ObservableCollection<Auction>(allAuctions);
        }

        private void AuctioneerOnBidAccepted(object sender, ProcessedBidEventArgs e)
        {
            var allAuctions = auctionService.GetAll();
            Auctions = new ObservableCollection<Auction>(allAuctions);
        }

        private void AuctioneerOnAuctionStarted(object sender, AuctionEventArgs e)
        {
            var allAuctions = auctionService.GetAll();
            Auctions = new ObservableCollection<Auction>(allAuctions);
        }

        private void AuctioneerOnAuctionEnded(object sender, AuctionEventArgs e)
        {
            var allAuctions = auctionService.GetAll();
            Auctions = new ObservableCollection<Auction>(allAuctions);
        }

        private void SellButton_Click(object sender, RoutedEventArgs e)
        {
            var sellView = new SellView();
            sellView.ShowDialog();

            var allAuctionsFromService = auctionService.GetAll();
            Auctions = new ObservableCollection<Auction>(allAuctionsFromService);
        }

        private void PlaceBid_Click(object sender, RoutedEventArgs e)
        {
            var selectedAuction = AuctionGrid.SelectedItem;

            if (selectedAuction.GetType() == typeof(Auction))
            {
                var bidView = new BidView((Auction) selectedAuction);
                bidView.ShowDialog();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
