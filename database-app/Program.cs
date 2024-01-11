
using AssetTracker;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using System.Drawing;
using System.Numerics;


const ConsoleColor RED = ConsoleColor.Red;
const ConsoleColor GRAY = ConsoleColor.Gray;
const ConsoleColor YELLOW = ConsoleColor.Yellow;

var db = new AssetTrackerDbContext(@"Data Source=..\..\..\..\asset-tracker.sqlite3");


//  main menu

while (true)
{
  Print(GRAY, "(L) List all assets \n" +
              "(S) Search assets   \n" +
              "(U) Update asset    \n" +
              "(A) Add new asset   \n" +
              "(D) Delete asset    \n" +
              "(X) Exit            \n\n" +
              "Your choice: ");

  char choice = GetChoice("lsuadx");

  if      (choice == 'l')    ListAssets(ShowSummary:true);
  else if (choice == 's')    Search();
  else if (choice == 'a')    Add();
  else if (choice == 'u')    Update();
  else if (choice == 'd')    Delete();
  else if (choice == 'x')    return;
}


//  list all assets

void ListAssets(bool ShowSummary=false)
{
  if (!db.Assets.Any())
  {
    PrintLine(GRAY, "\nNo assets");
    return;
  }

  PrintLine(GRAY, "\n   #  Type        Brand       Model          Office         Date        Price\n" +
                  "----  ----------  ----------  -------------  -------------  ----------  ----------------");
  foreach (var Asset in db.Assets.OrderBy(x => x.Type).ThenBy(x => x.DateOfPurchase))
  {
    PrintLine(GRAY, Asset.ToString());
  }

  if (ShowSummary)
  {
    var ncomp = db.Assets.Where(x => x.Type == Asset.AssetType.COMPUTER).Count();
    var nphone = db.Assets.Where(x => x.Type == Asset.AssetType.PHONE).Count();
    var ntab = db.Assets.Where(x => x.Type == Asset.AssetType.TABLET).Count();
    var value = db.Assets.Select(x => x.Price).Sum();
    var summary = $"{ncomp} computers, {nphone} phones, {ntab} tablets, total value of EUR {value}";
    PrintLine(GRAY, $"\n{summary}\n");
  }
}


//  search

void Search()
{
  Console.WriteLine();

  string[] keywords;
  do
  {
    Print(GRAY, "Enter keywords: ");
    keywords = Console.ReadLine().Trim().ToLower().Split();

  } while(keywords.Count() == 1 && keywords[0] == "");

  foreach (var Asset in db.Assets)
  {
    if (keywords.Any(kw => Asset.ToString().ToLower().Contains(kw)))    PrintLine(GRAY, Asset.ToString());
  }

}


//  add asset

void Add()
{
  Print(GRAY, "Add new (C) Computer (P) Phone (T) Tablet: ");
  var choice = GetChoice("cpt");
  Asset.AssetType type;
  if (choice == 'c') type = Asset.AssetType.COMPUTER;
  else if (choice == 'p') type = Asset.AssetType.PHONE;
  else type = Asset.AssetType.TABLET;

  var brand = GetLine("Brand");
  var model = GetLine("Model");

  Print(GRAY, "Choose office (E) Europe (S) South Africa (A) Asia: ");
  choice = GetChoice("esa");
  Asset.AssetLocation location;
  if (choice == 'e') location = Asset.AssetLocation.EUROPE;
  else if (choice == 's') location = Asset.AssetLocation.AFRICA;
  else location = Asset.AssetLocation.ASIA;

  var price = Convert.ToInt32(GetLine("Price"));

  db.Assets.Add(new Asset(0, type, brand, model, location, price, DateTime.Now));
  db.SaveChanges();
}

string GetLine(string value)
{
  string line;
  do
  {
    Print(GRAY, $"{value}: ");
    line = Console.ReadLine().Trim();
  } while (line == "");

  return line;
}


void Update()
{
  PrintLine(GRAY, "Update asset");
}


//  delete assets

void Delete()
{
  ListAssets();

  var ids = new List<int>();

  Print(GRAY, "Enter IDs of assets to delete: ");
  foreach (var input in Console.ReadLine().Trim().Split())
  {
    if (int.TryParse(input, out int id))    ids.Add(id);
  }

  var query = db.Assets.Where(x => ids.Contains(x.Id));
  db.Assets.RemoveRange(query);
  db.SaveChanges();
}


//  helper functions

void Print(ConsoleColor col, string str)
{
  Console.ForegroundColor = col;
  Console.Write(str);
}


void PrintLine(ConsoleColor col, string str) => Print(col, str + '\n');


char GetChoice(string choices)
{
  char c;
  do
  {
    c = Console.ReadKey(true).KeyChar;
  } while (!choices.Contains(c));
  Console.WriteLine(c);

  return c;
}

