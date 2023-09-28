namespace Binance.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_mig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TradeBotRanges",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CoinName = c.String(),
                        UpSellPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LowSellPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellStopLossPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellTakeProfitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UpBuyPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LowBuyPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BuyStopLossPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BuyTakeProfitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsActiveBuy = c.Boolean(nullable: false),
                        IsActiveSell = c.Boolean(nullable: false),
                        BuyAvailable = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SellAvailable = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TradingDatas",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CoinName = c.String(),
                        OpenDate = c.DateTime(nullable: false),
                        Available = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Position = c.String(),
                        Leverage = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        OpenPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ClosePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PositionValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PositionMargin = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PercentageinTrade = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CloseDate = c.DateTime(nullable: false),
                        DaysinPosition = c.Int(nullable: false),
                        GrossProfig_Loss = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GrossProfit_Loss_Percentage = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvailableAfterPosition = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Net_Profit_Loss = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Net_Portfo_Profit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TradingDatas");
            DropTable("dbo.TradeBotRanges");
        }
    }
}
