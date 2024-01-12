
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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

      mdl.Entity<Asset>().HasData(new Asset(1, com, "Siemens",   "WS100", eur, 2500, DateTime.Today.AddDays(-1100)));
      mdl.Entity<Asset>().HasData(new Asset(2, com, "Asus",    "Zenbook", eur,  745,  DateTime.Today.AddDays(-50)));
      mdl.Entity<Asset>().HasData(new Asset(3, pho, "Nokia",       "4.2", eur,  180,  DateTime.Today.AddDays(-1000)));

      mdl.Entity<Asset>().HasData(new Asset(4, pho, "Xiaomi", "Redmi 5", afr, 320, DateTime.Today.AddDays(-1200)));
      mdl.Entity<Asset>().HasData(new Asset(5, pho, "LG",         "Max", afr, 545, DateTime.Today.AddDays(-700)));
      mdl.Entity<Asset>().HasData(new Asset(6, pho, "Vivo", "V-10Micro", afr, 125, DateTime.Today.AddDays(-1050)));

      mdl.Entity<Asset>().HasData(new Asset(7, tab, "Samsung", "Pad 5", asi, 855, DateTime.Today.AddDays(-960)));
      mdl.Entity<Asset>().HasData(new Asset(8, tab, "Asus",  "Matepad", asi, 395, DateTime.Today.AddDays(-500)));
      mdl.Entity<Asset>().HasData(new Asset(9, pho, "Huawei",  "Ultra", asi, 680, DateTime.Today.AddDays(-200)));
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
      if      (Location == AssetLocation.EUROPE)    (newLocation, localPrice, currency) = ("Europe", Price, "EUR");
      else if (Location == AssetLocation.AFRICA)    (newLocation, localPrice, currency) = ("Africa", Price * 20.4, "ZAR");
      else                                          (newLocation, localPrice, currency) = ("Asia", Price * 7.82, "CNY");

      string newType;
      if      (Type == AssetType.COMPUTER)    newType = "Computer";
      else if (Type == AssetType.PHONE)       newType = "Phone";
      else                                    newType = "Tablet";

      return $"{Id,4}  {newType,-12}{Brand,-12}{Model,-15}{newLocation,-15}{DateOfPurchase.ToShortDateString(),-12}{localPrice,12:0.00}{currency,4}";
    }
  }

}
