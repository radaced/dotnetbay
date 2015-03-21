using System;
using System.IO;
using System.Windows;
using DotNetBay.Core;
using DotNetBay.Model;
using Microsoft.Win32;

namespace DotNetBay.WPF.ViewModel
{
    class SellViewModel : ViewModelBase
    {
        private readonly SimpleMemberService simpleMemberService;
        private readonly AuctionService auctionService;
        private readonly Auction newAuction;

        public Auction NewAuction { get { return newAuction; } }

        private string pathToAuctionImage;
        public String PathToAuctionImage
        {
            get { return pathToAuctionImage; }
            set { Set(() => PathToAuctionImage, ref pathToAuctionImage, value); }
        }
        public RelayCommand<Window> AddNewAuctionCommand { get; private set; }
        public RelayCommand<Window> CancelCommand { get; private set; }
        public RelayCommand<String> FileChooserCommand { get; private set; } 

        public SellViewModel(AuctionService auctionService, SimpleMemberService simpleMemberService)
        {
            this.auctionService = auctionService;
            this.simpleMemberService = simpleMemberService;

            newAuction = new Auction()
            {
                Seller = simpleMemberService.GetCurrentMember(),
                StartDateTimeUtc = DateTime.UtcNow.AddMinutes(5),
                EndDateTimeUtc = DateTime.UtcNow.AddDays(7)
            };

            AddNewAuctionCommand = new RelayCommand<Window>(AddNewAuctionAction);
            CancelCommand = new RelayCommand<Window>(CancelAction);
            FileChooserCommand = new RelayCommand<String>(FileChooserAction);
        }

        private bool ValidateInput()
        {
            var valid = true;
            valid &= newAuction.StartPrice > 0;
            valid &= newAuction.StartDateTimeUtc > DateTime.UtcNow;
            valid &= newAuction.EndDateTimeUtc > newAuction.StartDateTimeUtc;
            valid &= newAuction.Description.Length >= 0;
            valid &= newAuction.Title.Length > 0;

            return valid;
        }

        private void FileChooserAction(String s)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.DefaultExt = ".jpg";

            if (openFileDialog.ShowDialog() == true)
            {
                if (openFileDialog.CheckFileExists)
                {
                    PathToAuctionImage = openFileDialog.FileName;
                    newAuction.Image = File.ReadAllBytes(PathToAuctionImage);
                }
            }
        }

        private void AddNewAuctionAction(Window window)
        {
            if (ValidateInput())
            {
                auctionService.Save(newAuction);
                window.Close();
            }
        }

        private void CancelAction(Window window)
        {
            window.Close();
        }
    }
}
