using Xunit;
using NumberService;

public class NumberServiceTests
{
    private readonly NumberService.NumberService _numberService;

    public NumberServiceTests()
    {
        _numberService = new NumberService.NumberService();
    }

    [Fact]
    public void IsPrime_InputIs1_ReturnFalse() { 
        var result = _numberService.IsPrime(1);
        Assert.False(result, "1 should not be prime");
     }

    [Fact]
    public void CalculateAverage_InputIs1And2_Return1Point5() { 
        var result = _numberService.CalculateAverage(new int[] { 1, 2 });
        Assert.Equal(1.5, result);
     }

    [Fact]
    public void IsEven_InputIs2_ReturnTrue() { 
        var result = _numberService.IsEven(2);
        Assert.True(result, "2 should be even");
     }

    [Fact]
    public void IsDivisibleByThree_InputIs3_ReturnTrue() { 
        var result = _numberService.IsDivisibleByThree(3);
        Assert.True(result, "3 should be divisible by 3");
     }
}