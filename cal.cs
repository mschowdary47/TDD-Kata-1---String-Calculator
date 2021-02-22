[TestFixture]
public class StringCalculatorTests
{
    private StringCalculator _underTest;

    [SetUp]
    public void SetUp()
    {
        _underTest = new StringCalculator();
    }

    private int Act_CalculateNumbers(string numbers)
    {
        var calculatedResult = _underTest.Add(numbers);

        return calculatedResult;
    }

    [Test]
    [TestCase("", ExpectedResult = 0)]
    [TestCase(null, ExpectedResult = 0)]
    public int Returns_0_When_Null_Or_EmptyString_Input(string numbers)
    {
        return Act_CalculateNumbers(numbers);
    }

    [Test]
    [TestCase("1", ExpectedResult = 1)]
    [TestCase("100", ExpectedResult = 100)]
    public int Returns_SameNumber_When_ValidNumber_Input(string numbers)
    {
        return Act_CalculateNumbers(numbers);
    }

    [Test]
    [TestCase("1,2", ExpectedResult = 3)]
    [TestCase("11,12", ExpectedResult = 23)]
    public int Returns_SumOf_TwoNumbers_When_Two_ValidNumbers_Input(string numbers)
    {
        return Act_CalculateNumbers(numbers);
    }

    [Test]
    [TestCase("1,2,3,4", ExpectedResult = 10)]
    [TestCase("11,12,13,14", ExpectedResult = 50)]
    public int Returns_SumOf_Numbers_When_Multiple_ValidNumbers_Input(string numbers)
    {
        return Act_CalculateNumbers(numbers);
    }

    [Test]
    [TestCase("1\n2", ExpectedResult = 3)]
    [TestCase("1\n14", ExpectedResult = 15)]
    [TestCase("1\n1,4", ExpectedResult = 6)]
    [TestCase("1\n5,4", ExpectedResult = 10)]
    public int Returns_CorectSum_When_NewLine_Delimiter_Used(string numbers)
    {
        return Act_CalculateNumbers(numbers);
    }

    [Test]
    [TestCase("//$\n1", ExpectedResult = 1)]
    [TestCase("//$\n1$2", ExpectedResult = 3)]
    [TestCase("//$\n1$2,3", ExpectedResult = 6)]
    [TestCase("//$\n1$2,3\n4", ExpectedResult = 10)]
    [TestCase("//$\n1$2,3\n4$5", ExpectedResult = 15)]
    public int Returns_CorectSum_When_Custom_Delimiter_Used(string numbers)
    {
        return Act_CalculateNumbers(numbers);
    }

    [Test]
    [TestCase("-1", "-1")]
    [TestCase("1,-1", "-1")]
    [TestCase("1\n-1", "-1")]
    [TestCase("//$\n-1", "-1")]
    [TestCase("//$\n1$-2", "-2")]
    [TestCase("//$\n1$-2,3", "-2")]
    [TestCase("//$\n1$-2,-3\n4", "-2,-3")]
    [TestCase("//$\n1$2,3\n4$-5", "-5")]
    public void Throws_Correct_Exception_When_NegativeNumber_Input(string numbers, string negativeNumbers)
    {
        //Act
        var exception = Should.Throw<FormatException>(() => Act_CalculateNumbers(numbers));

        //Assert
        exception.Message.ShouldBe($"negatives not allowed '{negativeNumbers}'");
    }

    [Test]
    [TestCase("1001", ExpectedResult = 0)]
    [TestCase("1,1001", ExpectedResult = 1)]
    [TestCase("1\n1001", ExpectedResult = 1)]
    [TestCase("//$\n1,1001", ExpectedResult = 1)]
    [TestCase("//$\n1$1001", ExpectedResult = 1)]
    [TestCase("//$\n1$2,1001", ExpectedResult = 3)]
    public int Returns_CorrectSum_When_Ignoring_Numbers_Greater_Than_1000(string numbers)
    {
        return Act_CalculateNumbers(numbers);
    }

    [Test]
    [TestCase("//[$$][££]\n1££1", ExpectedResult = 2)]
		[TestCase("//[$$][££]\n1$$1££1", ExpectedResult = 3)]
		[TestCase("//[$$][££]\n1$$1,1££1", ExpectedResult = 4)]
		[TestCase("//[$$$][£££]\n1$$$1,1\n1£££1", ExpectedResult = 5)]
    public int Returns_CorrectSum_With_Custom_Delimiters_Of_Any_Length(string numbers)
    {
        return Act_CalculateNumbers(numbers);
    }
}