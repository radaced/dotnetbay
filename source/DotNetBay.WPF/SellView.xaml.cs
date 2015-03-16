using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DotNetBay.Core;
using DotNetBay.Model;
using Microsoft.Win32;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for SellView.xaml
    /// </summary>
    public partial class SellView : Window, INotifyPropertyChanged
    {
        #region Fields
        private readonly AuctionService auctionService;
        private readonly Auction newAuction;

        public Auction NewAuction { get { return newAuction; } }
        public String PathToAuctionImage { get; set; }
        #endregion

        public SellView()
        {
            InitializeComponent();
            DataContext = this;

            var app = Application.Current as App;
            if (app == null) throw new NullReferenceException("App is null");
            var simpleMemberService = new SimpleMemberService(app.MainRepository);
            auctionService = new AuctionService(app.MainRepository, simpleMemberService);

            newAuction = new Auction()
            {
                Seller = simpleMemberService.GetCurrentMember(),
                StartDateTimeUtc = DateTime.UtcNow,
                EndDateTimeUtc = DateTime.UtcNow.AddDays(7)
            };
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddAuction_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                auctionService.Save(newAuction);
                Close();
            }
        }

        private void FileChooser_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".jpg";

            if (openFileDialog.ShowDialog() == true)
            {
                if (openFileDialog.CheckFileExists)
                {
                    PathToAuctionImage = openFileDialog.FileName;
                    newAuction.Image = File.ReadAllBytes(PathToAuctionImage);
                    OnPropertyChanged("PathToAuctionImage");
                }
            }
        }

        private bool ValidateInput()
        {
            var valid = true;
            valid &= newAuction.StartPrice >= 0;
            valid &= newAuction.StartDateTimeUtc > DateTime.UtcNow;
            valid &= newAuction.EndDateTimeUtc > newAuction.StartDateTimeUtc;
            valid &= newAuction.Description.Length >= 0;
            valid &= newAuction.Title.Length >= 0;

            return valid;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
