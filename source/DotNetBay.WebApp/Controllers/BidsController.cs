using System;
using System.Linq;
using System.Web.Mvc;
using DotNetBay.Core;
using DotNetBay.Data.EF;
using DotNetBay.Interfaces;
using DotNetBay.WebApp.Models;

namespace DotNetBay.WebApp.Controllers
{
    public class BidsController : Controller
    {
        private readonly IMainRepository mainRepository;
        private readonly IAuctionService auctionService;

        public BidsController() 
        {
            mainRepository = new EFMainRepository();
            auctionService = new AuctionService(mainRepository, new SimpleMemberService(mainRepository));
        }

        // GET: Bids/Create
        public ActionResult Create(int auctionId)
        {
            var auction = auctionService.GetAll().FirstOrDefault(a => a.Id == auctionId);

            if (auction == null)
            {
                return HttpNotFound();
            }
            
            var bidVM = new MVCBidViewModel()
            {
                AuctionId = auctionId,
                Title = auction.Title,
                Description = auction.Description,
                StartPrice = auction.StartPrice,
                CurrentPrice = auction.CurrentPrice,
                Bid = Math.Max(auction.StartPrice, auction.CurrentPrice) + 1
            };

            return View(bidVM);
        }

        // POST: Bids/Create
        [HttpPost]
        public ActionResult Create(MVCBidViewModel bid)
        {
            if (ModelState.IsValid)
            {
                var newBidder = new SimpleMemberService(mainRepository).GetCurrentMember();
                var auction = auctionService.GetAll().FirstOrDefault(a => a.Id == bid.AuctionId);

                auctionService.PlaceBid(newBidder, auction, bid.Bid);
            }

            return RedirectToAction("Index", "Auctions");
        }
    }
}
