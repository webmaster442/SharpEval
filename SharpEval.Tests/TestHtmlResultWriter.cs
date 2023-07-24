namespace SharpEval.Tests;

[TestFixture]
public class TestHtmlResultWriter
{
    private HtmlResultWriter _sut;
    private Mock<ISvgImage> _imageMock;

    [SetUp]
    public void Setup()
    {
        _sut = new HtmlResultWriter();
        _imageMock = new Mock<ISvgImage>(MockBehavior.Strict);
        _imageMock.SetupGet(x => x.Data).Returns("<svg></svg>");
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
        _sut.Result(_imageMock.Object);
        string result = _sut.ToString();
        Assert.That(result, Is.EqualTo("<figure><svg></svg></figure>"));
    }

    [Test]
    public void ResultTable()
    {
        Mock<ITableRow> row = new Mock<ITableRow>(MockBehavior.Strict);
        row.SetupGet(x => x.ColumnCount).Returns(1);
        row.SetupGet(x => x.Columns).Returns(new[] { "column" });
        _sut.Result(new ITableRow[] { row.Object });

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
