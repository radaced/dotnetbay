using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetBay.Core;
using DotNetBay.Core.Execution;
using DotNetBay.Data.EF;
using DotNetBay.Interfaces;
using DotNetBay.Model;
using DotNetBay.WebApp.Models;

namespace DotNetBay.WebApp.Controllers
{
    public class AuctionsController : Controller
    {
        private readonly IMainRepository mainRepository;
        private readonly IAuctionService auctionService; 

        public AuctionsController()
        {
            mainRepository = new EFMainRepository();
            auctionService = new AuctionService(mainRepository, new SimpleMemberService(mainRepository));
        }

        // GET: Auctions
        public ActionResult Index()
        {
            return View(auctionService.GetAll().ToList());
        }

        // GET: Auctions/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Auctions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Auctions/Create
        [HttpPost]
        public ActionResult Create(MVCAuctionViewModel auction)
        {
            if (ModelState.IsValid)
            {
                var members = new SimpleMemberService(mainRepository);
                var newAuction = new Auction()
                {
                    Title = auction.Title,
                    Description = auction.Description,
                    StartDateTimeUtc = auction.StartDateTimeUtc,
                    EndDateTimeUtc = auction.EndDateTimeUtc,
                    StartPrice = auction.StartPrice,
                    Seller = members.GetCurrentMember()
                };

                if (auction.Image != null)
                {
                    var fileContent = new byte[auction.Image.InputStream.Length];
                    auction.Image.InputStream.Read(fileContent, 0, fileContent.Length);
                    newAuction.Image = fileContent;
                }

                auctionService.Save(newAuction);
            }
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Image(int auctionId)
        {
            var auction = mainRepository.GetAuctions().FirstOrDefault(a => a.Id == auctionId);

            if (auction == null)
            {
                return HttpNotFound();
            }

            if (auction.Image != null)
            {
                return new FileContentResult(auction.Image, "image/jpg");
            }

            return new EmptyResult();
        }
    }
}
