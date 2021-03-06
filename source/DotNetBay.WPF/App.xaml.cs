﻿using System;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Windows;
using DotNetBay.Core;
using DotNetBay.Core.Execution;
using DotNetBay.Data.EF;
using DotNetBay.Data.EF.Migrations;
using DotNetBay.Data.FileStorage;
using DotNetBay.Interfaces;
using DotNetBay.Model;

namespace DotNetBay.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public readonly IAuctionRunner AuctionRunner;
        public readonly IMainRepository MainRepository;

        public App()
        {
            var ensureDLLIsCopied = SqlProviderServices.Instance;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MainDbContext, Configuration>());

            MainRepository = new EFMainRepository();
            MainRepository.SaveChanges();
            //MainRepository = new FileSystemMainRepository("data.json");   *ohne EF
            AuctionRunner = new AuctionRunner(MainRepository);

            AuctionRunner.Start();
            AddTestAuctions();
        }

        private void AddTestAuctions()
        {
            var memberService = new SimpleMemberService(MainRepository);
            var service = new AuctionService(MainRepository, memberService);

            if (!service.GetAll().Any())
            {
                var me = memberService.GetCurrentMember(); 
                service.Save(new Auction
                {
                    Title = "My First Auction", 
                    StartDateTimeUtc = DateTime.UtcNow.AddSeconds(10), 
                    EndDateTimeUtc = DateTime.UtcNow.AddDays(14), 
                    StartPrice = 72, 
                    Seller = me
                });
            }
        }
    }
}
