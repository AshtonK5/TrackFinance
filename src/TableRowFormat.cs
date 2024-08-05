
using System.Text;

public class TableRowFormat : IFormatable
{
    public string[] RowInfo { get; private set; }
    public int Indent = 2;

    public TableRowFormat(string[] row)
    {
        RowInfo = row;

    }

    public StringBuilder Format()
    {
        var builder = new StringBuilder();

        int index = 0;
        foreach (string element in RowInfo)
        {
            if (index == 0 || index == RowInfo.Length)
            {
                builder.Append($"{new string(' ', Indent)}|");
            }

            builder.Append($" {(element != string.Empty? element : "N/A")} |");
            index++;
        }

        return builder;
    }
}
