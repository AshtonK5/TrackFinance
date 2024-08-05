
using System.Text;

public struct TableTitleLayout
{
    public string Title;
    public string SubTitle;

    public TableTitleLayout(string title, string? subTitle = null)
    {
        Title = title;
        SubTitle = (subTitle != null? subTitle : string.Empty);

    }
}

public class TableLayoutBuilder
{

    private string[]? m_Headers;

    private List<TableRowFormat> m_Rows;

    private StringBuilder m_TableLayoutText;

    private TableTitleLayout? m_TitleLayout;

    public int Columns { get; private set; } = 0;

    public int Rows { get; private set; } = 0;

    //public TableLayoutBuilder CreateColumn()
    //{

    //    return this;
    //}

    public TableLayoutBuilder()
    {
        m_TableLayoutText = new StringBuilder();
        m_Rows = new List<TableRowFormat>();

    }

    public TableLayoutBuilder SetupTitleBarInfo(string title, string? subTitle = null)
    {
        var titleLayout = new TableTitleLayout(
            title, subTitle);

        m_TitleLayout = titleLayout;
        return this;
    }

    private TableLayoutBuilder CreateTitleBar()
    {
        if (m_TitleLayout != null)
        {

        }

        return this;
    }

    public TableLayoutBuilder CreateHeaders(string[] headers)
    {
        m_Headers = headers;
        Columns = headers.Length;
        return this;
    }

    private StringBuilder FormatAllHeadings()
    {
        StringBuilder builder = new StringBuilder();
        if (m_Headers != null)
        {
            builder.AppendLine($"  {new string('_', 111)}");
            builder.Append("  |");

            foreach (string header in m_Headers)
            {
                builder.Append($"{header} |");
            }

            builder.AppendLine($"\n  |{new string('-', 111)}");
        }

        return builder;
    }

    private StringBuilder FormatAllRows()
    {
        StringBuilder builder = new StringBuilder();
        if (m_Rows != null)
        {
            foreach (TableRowFormat row in m_Rows)
            {
                builder.AppendLine(row.Format().ToString());
            }
        }
        return builder;
    }

    private int GetMaxRowWidth()
    {
        int MaxWidth = 0;
        foreach (TableRowFormat row in m_Rows)
        {
            foreach (var rowInfo in row.RowInfo)
            {
                if (rowInfo.Length > MaxWidth)
                {
                    MaxWidth = rowInfo.Length;
                }
            }
        }

        return MaxWidth;
    }

    public TableLayoutBuilder CreateRow(string[] rowInfo)
    {
        m_Rows.Add(new TableRowFormat(rowInfo));
        Rows++;
        return this;
    }

    private void CreateBottomSeperator()
    {
       m_TableLayoutText.AppendLine($"  |{new string('_', 111)}|");
    }

    public void Build()
    {
        StringBuilder rowsFormat = FormatAllRows();
        StringBuilder headingsFormat = FormatAllHeadings();

        m_TableLayoutText.Append(headingsFormat);
        m_TableLayoutText.Append(rowsFormat);
        CreateBottomSeperator();
        CreateTitleBar();

        Console.WriteLine($"Columns: {Columns}  Rows: {Rows}");
    }

    public void LogTableLayout() => Application.LogInfo(m_TableLayoutText.ToString(), ELogVerbosity.Info, true);
}

