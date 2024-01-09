
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Runtime.CompilerServices;


namespace ef_core_asset_tracking
{

  internal class AssetTrackerDbContext(string ConnStr = @"Data Source=.\asset-tracker.sqlite3"):DbContext
  {
    private string ConnStr { get; } = ConnStr;

    public DbSet<Asset> Assets { get; set; }

    override protected void OnConfiguring(DbContextOptionsBuilder opts)
    {
      opts.UseSqlite(ConnStr);
    }

    //protected override void OnModelCreating(ModelBuilder mdl) { }
  }



  internal class Asset(Asset.AssetType Type, string Brand, string Model, Asset.AssetLocation Location, int Price, DateTime DateOfPurchase)
  {
    public enum AssetType { COMPUTER, PHONE, TABLET }
    public enum AssetLocation { EUROPE, ASIA, AFRICA }

    public int Id { get; set; }

    [MaxLength(50)] public string Brand { get; set; } = Brand;
    [MaxLength(50)] public string Model { get; set; } = Model;
    public AssetLocation Location { get; set; } = Location;
    public AssetType Type { get; set; } = Type;
    public int Price { get; set; } = Price;
    public DateTime DateOfPurchase { get; set; } = DateOfPurchase;
  }

}
