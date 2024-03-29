﻿
using AssetTracker;


const ConsoleColor RED = ConsoleColor.Red;
const ConsoleColor GRAY = ConsoleColor.Gray;
const ConsoleColor YELLOW = ConsoleColor.Yellow;

var db = new AssetTrackerDbContext(@"Data Source=..\..\..\..\asset-tracker.sqlite3");



//  Main menu

while (true)
{
  Print(GRAY, "(L) List all assets \n" +
              "(S) Search assets   \n" +
              "(A) Add new asset   \n" +
              "(T) Transfer assets \n" +
              "(D) Delete assets   \n" +
              "(X) Exit            \n\n" +
              "Your choice: ");

  char choice = GetChoice("lsatdx");

  if      (choice == 'l')    ListAssets(ShowSummary:true);
  else if (choice == 's')    Search();
  else if (choice == 'a')    Add();
  else if (choice == 't')    Transfer();
  else if (choice == 'd')    Delete();
  else if (choice == 'x')    return;
}


//  List all assets

void ListAssets(bool ShowSummary=false)
{
  if (!db.Assets.Any())
  {
    PrintLine(GRAY, "\nNo assets");
    return;
  }

  PrintLine(GRAY, "\n   #  Type        Brand       Model          Office         Date        Price\n" +
                  "----  ----------  ----------  -------------  -------------  ----------  ----------------");
  foreach (var ast in db.Assets.OrderBy(x => x.Type).ThenBy(x => x.DateOfPurchase))
  {
    ConsoleColor col;

    //  Highlight red when item is 3 years old, yellow if less than 90 days away

    var days = (ast.DateOfPurchase.AddYears(3)-DateTime.Today).Days;
    if      (days <= 0)    col = ConsoleColor.Red;
    else if (days < 90)    col = ConsoleColor.Yellow;
    else                   col = ConsoleColor.Gray;

    PrintLine(col, ast.ToString());
  }

  if (ShowSummary)
  {
    //  Count assets and total value

    var ncomp = db.Assets.Where(x => x.Type == Asset.AssetType.COMPUTER).Count();
    var nphone = db.Assets.Where(x => x.Type == Asset.AssetType.PHONE).Count();
    var ntab = db.Assets.Where(x => x.Type == Asset.AssetType.TABLET).Count();
    var value = db.Assets.Select(x => x.Price).Sum();
    var summary = $"{ncomp} computers, {nphone} phones, {ntab} tablets, total value of EUR {value}";
    PrintLine(GRAY, $"\n{summary}\n");
  }
}



//  Search assets

void Search()
{
  Console.WriteLine();

  //  Split keywords into string array

  string[] keywords;
  do
  {
    Print(GRAY, "Enter keywords: ");
    keywords = Console.ReadLine().Trim().ToLower().Split();

  } while(keywords.Count() == 1 && keywords[0] == "");

  //  Show results where any keyword is present in any column

  foreach (var Asset in db.Assets)
  {
    if (keywords.Any(kw => Asset.ToString().ToLower().Contains(kw)))    PrintLine(GRAY, Asset.ToString());
  }

}



//  Add new asset

void Add()
{
  Print(GRAY, "Add new (C) Computer (P) Phone (T) Tablet: ");
  var choice = GetChoice("cpt");
  Asset.AssetType type;
  if      (choice == 'c')    type = Asset.AssetType.COMPUTER;
  else if (choice == 'p')    type = Asset.AssetType.PHONE;
  else                       type = Asset.AssetType.TABLET;

  var brand = GetLine("Brand");
  var model = GetLine("Model");

  Print(GRAY, "Choose office (E) Europe (S) South Africa (A) Asia: ");
  choice = GetChoice("esa");
  Asset.AssetLocation location;
  if      (choice == 'e')    location = Asset.AssetLocation.EUROPE;
  else if (choice == 's')    location = Asset.AssetLocation.AFRICA;
  else                       location = Asset.AssetLocation.ASIA;

  var price = Convert.ToInt32(GetLine("Price (EUR)"));

  db.Assets.Add(new Asset(0, type, brand, model, location, price, DateTime.Now));
  db.SaveChanges();
}





//  Transfer assets to another office

void Transfer()
{
  ListAssets();

  //  Create a list of IDs to transfer

  var ids = new List<int>();
  Print(GRAY, "Enter IDs of assets to transfer: ");
  foreach (var input in Console.ReadLine().Trim().Split())
  {
    if (int.TryParse(input, out int id))    ids.Add(id);
  }

  Print(GRAY, "Transfer assets to new office (E) Europe (S) South Africa (A) Asia: ");
  var choice = GetChoice("esa");

  Asset.AssetLocation newLoc;
  if      (choice == 'e')    newLoc = Asset.AssetLocation.EUROPE;
  else if (choice == 's')    newLoc = Asset.AssetLocation.AFRICA;
  else                       newLoc = Asset.AssetLocation.ASIA;

  //  Query for all assets with matching IDs

  var query = db.Assets.Where(x => ids.Contains(x.Id));
  foreach (var ast in query)    ast.Location = newLoc;

  db.SaveChanges();
}



//  Delete assets

void Delete()
{
  ListAssets();

  //  Create a list of asset IDs

  var ids = new List<int>();
  Print(GRAY, "Enter IDs of assets to delete: ");
  foreach (var input in Console.ReadLine().Trim().Split())
  {
    if (int.TryParse(input, out int id))    ids.Add(id);
  }

  //  Query for all assets with matching IDs

  var query = db.Assets.Where(x => ids.Contains(x.Id));
  db.Assets.RemoveRange(query);
  db.SaveChanges();
}



//  Helper functions

void PrintLine(ConsoleColor col, string str)  =>  Print(col, str + '\n');
void Print(ConsoleColor col, string str)
{
  Console.ForegroundColor = col;
  Console.Write(str);
}

//  Prompt and read in a line of text

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

//  Wait for user to press one of the given keys

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
