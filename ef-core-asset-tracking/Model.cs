
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Reflection;
using System.ComponentModel;

return;


namespace AssetTracker
{

  public class AssetTrackerDbContext(string ConnStr = @"Data Source=..\asset-tracker.sqlite3") : DbContext
  {
    private string ConnStr { get; } = ConnStr;

    public DbSet<Asset> Assets { get; set; }

    override protected void OnConfiguring(DbContextOptionsBuilder opts)
    {
      opts.UseSqlite(ConnStr);
    }

    protected override void OnModelCreating(ModelBuilder mdl)
    {
      var com = Asset.AssetType.COMPUTER;
      var pho = Asset.AssetType.PHONE;
      var tab = Asset.AssetType.TABLET;
      var eur = Asset.AssetLocation.EUROPE;
      var afr = Asset.AssetLocation.AFRICA;
      var asi = Asset.AssetLocation.ASIA;

      mdl.Entity<Asset>().HasData(new Asset(1, com, "Siemens", "WS100", eur, 2500, new DateTime(2024, 1, 2)));
      mdl.Entity<Asset>().HasData(new Asset(2, pho, "Nokia",   "6.1",   afr,  500, new DateTime(2024, 1, 3)));
      mdl.Entity<Asset>().HasData(new Asset(3, tab, "Samsung", "ST-14", asi,  400, new DateTime(2024, 1, 4)));
    }
  }



  public class Asset(int Id, Asset.AssetType Type, string Brand, string Model, Asset.AssetLocation Location, int Price, DateTime DateOfPurchase)
  {
    public enum AssetType { COMPUTER, PHONE, TABLET }
    public enum AssetLocation { EUROPE, ASIA, AFRICA }

    public int Id { get; set; } = Id;

    [MaxLength(20)] public string Brand { get; set; } = Brand;
    [MaxLength(20)] public string Model { get; set; } = Model;
    public AssetLocation Location { get; set; } = Location;
    public AssetType Type { get; set; } = Type;
    public int Price { get; set; } = Price;
    public DateTime DateOfPurchase { get; set; } = DateOfPurchase;

    override public string ToString()
    {
      //  prices are stored in euros
      //  european office uses eur
      //  african office uses south african rand
      //  asian office uses chinese yuan

      double localPrice;
      string currency, newLocation;
      if (Location == AssetLocation.EUROPE) (newLocation, localPrice, currency) = ("Europe", Price, "EUR");
      else if (Location == AssetLocation.AFRICA) (newLocation, localPrice, currency) = ("Africa", Price * 20.4, "ZAR");
      else (newLocation, localPrice, currency) = ("Asia", Price * 7.82, "CNY");

      string newType;
      if (Type == AssetType.COMPUTER) newType = "Computer";
      else if (Type == AssetType.PHONE) newType = "Phone";
      else newType = "Tablet";

      return $"{Id,4}  {newType,-12}{Brand,-12}{Model,-15}{newLocation,-15}{DateOfPurchase.ToShortDateString(),-12}{localPrice,12:0.00}{currency,4}";
    }
  }

}
