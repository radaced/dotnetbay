using System.Collections.ObjectModel;
using System.Linq;
using DotNetBay.Core;
using DotNetBay.Core.Execution;
using DotNetBay.Model;
using DotNetBay.WPF.View;

namespace DotNetBay.WPF.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private ObservableCollection<AuctionViewModel> auctions = new ObservableCollection<AuctionViewModel>();
        private readonly AuctionService auctionService;
        private readonly SimpleMemberService simpleMemberService;
        private readonly IAuctioneer auctioneer;

        public ObservableCollection<AuctionViewModel> Auctions
        {
            get { return auctions; }
            set { auctions = value; }
        }
        public RelayCommand<object> AddAuctionCommand { get; private set; }

        public MainViewModel(IAuctioneer auctioneer, AuctionService auctionService, SimpleMemberService simpleMemberService)
        {
            this.auctionService = auctionService;
            this.simpleMemberService = simpleMemberService;
            this.auctioneer = auctioneer;

            auctioneer.AuctionEnded += (sender, args) => ApplyChanges(args.Auction);
            auctioneer.AuctionStarted += (sender, args) => ApplyChanges(args.Auction);
            auctioneer.BidAccepted += (sender, args) => ApplyChanges(args.Auction);
            auctioneer.BidDeclined += (sender, args) => ApplyChanges(args.Auction);

            AddAuctionCommand = new RelayCommand<object>(AddAuctionAction);

            var allAuctions = auctionService.GetAll();
            foreach (var auction in allAuctions)
            {
                Auctions.Add(new AuctionViewModel(auction));
            }
        }

        private void ApplyChanges(Auction auction)
        {
            var auctionVM = auctions.FirstOrDefault(vm => vm.Auction == auction);

            if (auctionVM != null)
            {
                auctionVM.Update(auction);
            }
        }

        private void AddAuctionAction(object o)
        {
            var view = new SellView();
            view.ShowDialog();

            var allAuctions = auctionService.GetAll();
            
            auctions.Clear();

            foreach (var auction in allAuctions)
            {
                var auctionVM = new AuctionViewModel(auction);
                auctions.Add(auctionVM);
            }
        }
    }
}
