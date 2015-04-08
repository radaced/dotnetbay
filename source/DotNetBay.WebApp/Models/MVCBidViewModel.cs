using System.ComponentModel.DataAnnotations;

namespace DotNetBay.WebApp.Models
{
    public class MVCBidViewModel
    {
        public long AuctionId { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Start Price")]
        public double StartPrice { get; set; }

        [Display(Name = "Current Price")]
        public double CurrentPrice { get; set; }

        [Required]
        [Display(Name = "Your Bid")]
        public double Bid { get; set; }
    }
}