using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Json.Serialization;

public class BudgetSheet
{
    public float CurrentBalance { get; private set; } = 0.00f;
    public string Name { get; private set; }
    public string FilePath { get; private set; }
    public List<Transaction> Transactions { get; private set; }

    public static string FileExtensionName { get; private set; } = ".bds";

    [JsonConstructor]
    public BudgetSheet(string name, string filePath, float currentBalance, List<Transaction> transactions)
    {
        Name = name;
        FilePath = filePath;
        CurrentBalance = currentBalance;
        Transactions = transactions?? new List<Transaction>();
    }

    public BudgetSheet(string name, string path)
    {
        Name = name;
        FilePath = $"{path}\\{Name}{FileExtensionName}";
        Transactions = new List<Transaction>();
    }

    public void Create()
    {
        // Ensure the file stream is closed before writing
        using (var fileStream = File.Create(FilePath))
        {
            // Stream is created, now we can write to it!

        }

        WriteToFile();
    }

    public static BudgetSheet? ReadFormat(string path)
    {
        if (Path.GetExtension(path) != FileExtensionName) return null;

        string fileJsonFormat = File.ReadAllText(path);

        try
        {
            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                IncludeFields = true,

            };

            var budgetSheet = JsonSerializer.Deserialize<BudgetSheet>(fileJsonFormat, jsonSerializerOptions);
            if (budgetSheet != null)
            {
#if DEBUG
                Application.LogInfo("Json successfully serialized!", ELogVerbosity.Info, true);
#endif
                return budgetSheet;
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Deserialization error: {ex.Message}");
            Console.ResetColor();
        }

        return null;
    }

    public void WriteToFile()
    {
        JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions() 
        { 
            WriteIndented = true, 
            IncludeFields = true 
        };

        string budgetSheetJson = JsonSerializer.Serialize(this, jsonSerializerOptions);

        File.WriteAllText(FilePath, budgetSheetJson);
    }

    public void CreateTransaction(ref Transaction transactionObject)
    {
        Transactions.Add(transactionObject);
        UpdateTransactions(ref transactionObject, false);
    }

    public Transaction? DeleteTransaction(string guid)
    {
        for (int i = 0; i < Transactions.Count; i++)
        {
            Transaction transaction = Transactions[i];
            if (transaction.ID.Equals(guid))
            {
                Transactions.Remove(transaction);
                UpdateTransactions(ref transaction, true);
                return transaction;
            }
        }

        return null;
    }

    public void ClearTransactions()
    {
        Transactions.Clear();
        CurrentBalance = 0;
        WriteToFile();
    }

    private void UpdateTransactions(ref Transaction transactionObject, bool bIsReversed = false)
    {
        switch (transactionObject.Type)
        {
            case (ETransactionType.Income):
                {
                    CurrentBalance = (bIsReversed == false? CurrentBalance +transactionObject.Amount 
                        : CurrentBalance -transactionObject.Amount);
                    break;
                }

            case (ETransactionType.Expense):
                {
                    CurrentBalance = (bIsReversed == false? CurrentBalance -transactionObject.Amount 
                        : CurrentBalance +transactionObject.Amount);
                    break;
                }
        }

        WriteToFile();
    }
}
