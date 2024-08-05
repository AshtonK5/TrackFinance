
using System.CommandLine;
using System.Globalization;
using Spectre.Console;

public class ShowCommand : Command
{
    public ShowCommand() : base("show", "Shows your current balance sheet info")
    {
        this.SetHandler(() =>
        {
        if (Application.CurrentBudgetSheet != null)
        {
                // Create a panel (box) around the title text
                var titlePanel = new Panel(new Markup($"[bold gray]Current Balance[/]\n[green]{Application.CurrentBudgetSheet.CurrentBalance.ToString("C", CultureInfo.GetCultureInfo("en-US"))}[/]"))
                {
                    Border = BoxBorder.Square,
                    Padding = new Padding(1, 1, 1, 1),
                    BorderStyle = new Style(decoration: Decoration.Dim),
                    Expand = true,
                };

                // Render the title panel to the console
                AnsiConsole.Write(titlePanel);

                // Create a new table layout
                Table currentTableLayout = new Table();
                currentTableLayout.ShowRowSeparators = true;
                currentTableLayout.Expand = true;
                currentTableLayout.BorderStyle = new Style(decoration: Decoration.Dim);
                currentTableLayout.AddColumns(new string[]
                {
                    "[gray]Transaction[/]",
                    "[gray]Type[/]",
                    "[gray]Amount[/]",
                    "[gray]Account[/]",
                    "[gray]Category[/]",
                    "[gray]Date[/]",
                    "[gray]Guid[/]"
                });

                // Add transactions to current table
                foreach (Transaction transaction in Application.CurrentBudgetSheet.Transactions)
                {
                    currentTableLayout.AddRow(
                        transaction.Name,
                        transaction.Type    .ToString(),
                        transaction.Amount  .ToString("C", CultureInfo.GetCultureInfo("en-US")),
                        string.IsNullOrEmpty(transaction.Account)  == false? transaction.Account  : "N/A",
                        string.IsNullOrEmpty(transaction.Category) == false? transaction.Category : "N/A",
                        transaction.Date    .ToString(),
                        transaction.ID
                    );
                }

                AnsiConsole.Write(currentTableLayout);
                //TableLayoutBuilder tableLayoutBuilder = new TableLayoutBuilder();
                //tableLayoutBuilder
                //    .SetupTitleBarInfo("Current Balance", $"${Application.CurrentBudgetSheet.CurrentBalance}")
                //    .CreateHeaders(new string[]
                //    {
                //        "Transaction",
                //        "Type",
                //        "Amount",
                //        "Account",
                //        "Category",
                //        "Date",
                //        "Guid"

                //    });
                //foreach (Transaction transaction in Application.CurrentBudgetSheet.Transactions)
                //{
                //    tableLayoutBuilder.CreateRow(new string[]
                //    {
                //        transaction.Name,
                //        transaction.Type        .ToString(),
                //        transaction.Amount      .ToString("C", CultureInfo.GetCultureInfo("en-US")),
                //        transaction.Account     .ToString(),
                //        transaction.Category    .ToString(),
                //        transaction.Date        .ToString(),
                //        transaction.ID          .ToString()
                //    });
                //}

                //tableLayoutBuilder.Build();
                //tableLayoutBuilder.LogTableLayout();
            }
            else
            {
                throw new BudgetSheetNullException();
            }
        });
    }
}
