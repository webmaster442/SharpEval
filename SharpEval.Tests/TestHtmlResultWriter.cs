namespace SharpEval.Tests;

[TestFixture]
public class TestHtmlResultWriter
{
    private HtmlResultWriter _sut;
    private ISvgImage _imageMock;

    [SetUp]
    public void Setup()
    {
        _sut = new HtmlResultWriter();
        _imageMock = Substitute.For<ISvgImage>();
        _imageMock.Data.Returns("<svg></svg>");
    }

    [Test]
    public void ResultLine()
    {
        _sut.Result("5");
        string result = _sut.ToString();
        Assert.That(result, Is.EqualTo("<i>5</i>\r\n"));
    }

    [Test]
    public void ResultImage()
    {
        _sut.Result(_imageMock);
        string result = _sut.ToString();
        Assert.That(result, Is.EqualTo("<figure><svg></svg></figure>"));
    }

    [Test]
    public void ResultTable()
    {
        ITableRow row = Substitute.For<ITableRow>();
        row.ColumnCount.Returns(1);
        row.Columns.Returns(new[] { "column" });
        _sut.Result(new ITableRow[] { row });

        string result = _sut.ToString();
        Assert.That(result, Is.EqualTo("<table>\r\n<tr>\r\n<td>column</td>\r\n</tr>\r\n</table>\r\n"));

    }

    [Test]
    public void Echo()
    {
        _sut.Echo(AngleSystem.Deg, "3+32");
        string result = _sut.ToString();
        Assert.That(result, Is.EqualTo("<b>(Deg) 3+32:</b>\r\n"));
    }



    [Test]
    public void Error()
    {
        _sut.Error("error");
        string result = _sut.ToString();
        Assert.That(result, Is.EqualTo("<span style=\"color: red; font-weight: bold;\">error</span>\r\n"));
    }

    [Test]
    public void ErrorWithTrace()
    {
        _sut.Error("error", "trace");
        string result = _sut.ToString();
        Assert.That(result, Is.EqualTo("<span style=\"color: red; font-weight: bold;\">error</span>\r\n<code><pre>trace</pre></code>\r\n"));
    }
}
