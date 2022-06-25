using Moq;

namespace DemoApp.Integration.Test.Tests;
public class MathUnitTest
{
    [Theory]
    [InlineData(1, 3, 4)]
    [InlineData(4, 7, 14)]
    [InlineData(14, 17, 31)]
    public void Add_TwoNumbers_Theory(int a, int b, int expected)
    {
        // Arrange

        // Act
        var result = new Calc().Add(a, b);
        // Assert 

        Assert.Equal(expected, result);
    }


    [Fact]
    public void Add_TwoNumbers_With_MoQ()
    {
        // Arrange
        var mathMock = new Mock<ICalc>();
        mathMock.Setup(x => x.Add(2, 3)).Returns(5);

        // Act
        var math = new Math(mathMock.Object);

        // Assert 
        Assert.Equal(5, math.Add(2, 3));
    }

    [Fact]
    public void Verify_Method_Has_Been_Called()
    {
        // Arrange
        var mathMock = new Mock<ICalc>();
        var msg = "hello";
        // Act
        var math = new Math(mathMock.Object);
        math.RunOutData(msg);

        // Assert 
        mathMock.Verify(x => x.OutData(msg), Times.Once);
    }
}

class Math
{
    private readonly ICalc _calc;

    public Math(ICalc calc)
    {
        _calc = calc;
    }

    public int Add(int a, int b) => _calc.Add(a, b);

    public void RunOutData(string msg) => _calc.OutData(msg);
}

public interface ICalc
{
    int Add(int a, int b);
    void OutData(string msg);
}

class Calc : ICalc
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public void OutData(string msg)
    {
        Console.WriteLine($"Method Eexecuted for {msg}");
    }
}