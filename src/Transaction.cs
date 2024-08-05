
public enum ETransactionType
{
	Income,
	Expense
}


public struct Transaction
{

	/// <summary>
	/// Type of transaction
	/// </summary>
	public ETransactionType Type;

	/// <summary>
	/// Transaction amount (USD)
	/// </summary>
	public double Amount;


	public string Name;


	public string Category;


	public string Account;


	public DateTime Date;


	public string ID;


	public Transaction(ETransactionType type, double amt, string name, string category, string? account)
	{
		Type = type;
		Amount = amt;
		Name = name;
		Category = category;
		Account = ((account != null)? account : string.Empty);
		Date = DateTime.Now;
#if DEBUG
		Application.LogInfo("Transaction Created: ", ELogVerbosity.Success, false);
		Application.LogInfo(Date.ToString(), ELogVerbosity.Info, false);
#endif
		ID = Guid.NewGuid().ToString("B");
	}
}
