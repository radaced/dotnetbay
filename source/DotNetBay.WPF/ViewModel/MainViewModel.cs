using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Windows.Input;
using DotNetBay.Core;
using DotNetBay.Model;
using DotNetBay.WPF.View;

namespace DotNetBay.WPF.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private ObservableCollection<AuctionViewModel> auctions = new ObservableCollection<AuctionViewModel>();
        private readonly AuctionService auctionService;
        private readonly SimpleMemberService simpleMemberService;

        public ObservableCollection<AuctionViewModel> Auctions
        {
            get { return auctions; }
            set { auctions = value; OnPropertyChanged(); }
        }
        public RelayCommand<object> AddAuctionCommand { get; private set; }

        public MainViewModel(AuctionService auctionService, SimpleMemberService simpleMemberService)
        {
            this.auctionService = auctionService;
            this.simpleMemberService = simpleMemberService;

            AddAuctionCommand = new RelayCommand<object>(AddAuctionAction);

            var allAuctions = auctionService.GetAll();
            foreach (var auction in allAuctions)
            {
                Auctions.Add(new AuctionViewModel(auction, simpleMemberService, auctionService));
            }
        }

        private void AddAuctionAction(object o)
        {
            var view = new SellView();
            view.ShowDialog();
        }
    }
}
