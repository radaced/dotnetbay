using System;
using System.Data.Entity;
using System.Linq;
using DotNetBay.Interfaces;
using DotNetBay.Model;

namespace DotNetBay.Data.EF
{
    public class EFMainRepository : IMainRepository
    {
        private readonly MainDbContext dbContext;

        public Database Database {
            get { return dbContext.Database; }
        }

        public EFMainRepository()
        {
            dbContext = new MainDbContext();
        }

        public IQueryable<Auction> GetAuctions()
        {
            return dbContext.Auctions.Include(a => a.Bids);
        }

        public IQueryable<Member> GetMembers()
        {
            return dbContext.Members.Include(m => m.Auctions);
        }

        public Auction Add(Auction auction)
        {
            dbContext.Auctions.Add(auction);
            return auction;
        }

        public Auction Update(Auction auction)
        {
            return auction;
        }

        public Bid Add(Bid bid)
        {
            dbContext.Bids.Add(bid);
            return bid;
        }

        public Bid GetBidByTransactionId(Guid transactionId)
        {
            return dbContext.Bids.FirstOrDefault(bid => bid.TransactionId == transactionId);
        }

        public Member Add(Member member)
        {
            dbContext.Members.Add(member);
            return member;
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }
    }
}
