using System.Windows;
using DotNetBay.Core;
using DotNetBay.WPF.ViewModel;

namespace DotNetBay.WPF.View
{
    /// <summary>
    /// Interaction logic for SellView.xaml
    /// </summary>
    public partial class SellView
    {
        public SellView()
        {
            InitializeComponent();

            var app = Application.Current as App;

            var simpleMemberService = new SimpleMemberService(app.MainRepository);
            var auctionService = new AuctionService(app.MainRepository, simpleMemberService);

            DataContext = new SellViewModel(auctionService, simpleMemberService);
        }
    }
}
