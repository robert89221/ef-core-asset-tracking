
using AssetTracker;
using System.Drawing;


const ConsoleColor RED = ConsoleColor.Red;
const ConsoleColor GRAY = ConsoleColor.Gray;
const ConsoleColor YELLOW = ConsoleColor.Yellow;

var db = new AssetTrackerDbContext(@"Data Source=..\..\..\..\asset-tracker.sqlite3");


//  main menu

while (true)
{
  var ncomp = db.Assets.Where(x => x.Type == Asset.AssetType.COMPUTER).Count();
  var nphone = db.Assets.Where(x => x.Type == Asset.AssetType.PHONE).Count();
  var ntab = db.Assets.Where(x => x.Type == Asset.AssetType.TABLET).Count();
  var value = db.Assets.Select(x => x.Price).Sum();
  var summary = $"{ncomp} computers, {nphone} phones, {ntab} tablets, total value of EUR {value} \n\n";

  Print(GRAY, summary +
              "(L) List all assets \n" +
              "(S) Search assets   \n" +
              "(U) Update asset    \n" +
              "(A) Add new asset   \n" +
              "(D) Delete asset    \n" +
              "(X) Exit            \n\n");

  char choice = GetChoice("lsuadx");

  if      (choice == 'l')    ListAssets();
  else if (choice == 's')    Search();
  else if (choice == 'a')    Add();
  else if (choice == 'u')    Update();
  else if (choice == 'd')    Delete();
  else if (choice == 'x')    return;
}


void ListAssets()
{
  PrintLine(GRAY, "List assets");
}


void Search()
{
  PrintLine(GRAY, "Search assets");
}


void Add()
{
  PrintLine(GRAY, "Add asset");
}


void Update()
{
  PrintLine(GRAY, "Update asset");
}


void Delete()
{
  PrintLine(GRAY, "Delete asset");
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
    Print(GRAY, "Your choice: ");
    c = Console.ReadKey().KeyChar;
    Console.WriteLine();

  } while (!choices.Contains(c));

  return c;
}

