
using ef_core_asset_tracking;


using (var db = new AssetTrackerDbContext(@"Data Source=..\..\..\asset-tracker.sqlite3"))
{
  var n = db.Assets.Count();
  Console.WriteLine($"{n} assets");
};
