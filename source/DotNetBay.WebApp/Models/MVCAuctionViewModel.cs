using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DotNetBay.WebApp.Models
{
    public class MVCAuctionViewModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Range(1, 10000)]
        [Display(Name = @"Start Price")]
        public double StartPrice { get; set; }

        [Required]
        [Display(Name = @"Start Date Time UTC")]
        public DateTime StartDateTimeUtc { get; set; }

        [Required]
        [Display(Name = @"End Date Time UTC")]
        public DateTime EndDateTimeUtc { get; set; }

        public HttpPostedFileBase Image { get; set; }
    }
}