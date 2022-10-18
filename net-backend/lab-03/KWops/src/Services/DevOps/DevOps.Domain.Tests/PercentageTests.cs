using Test;

namespace DevOps.Domain.Tests
{
    public class PercentageTests : TestBase
    {
        [Test]
        public void Constructor_ValidInput_ShouldNotThrowAnException()
        {
            double value = Random.NextDouble();
            var percentage = new Percentage(value);
        }

        [TestCase(-0.1)]
        [TestCase(1.1)]
        public void Constructor_InputOutOfRange_ShouldThrowContractException(double invalidInput)
        {
            Assert.That(() => new Percentage(invalidInput), Throws.InstanceOf<ContractException>());
        }

        [TestCaseSource(nameof(_toStringCases))]
        public void ToString_ShouldConvertCorrectly(double value, string expected)
        {
            var percentage = new Percentage(value);
            Assert.That(percentage.ToString(), Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(_toStringCases))]
        public void ImplicitConvertToString_ShouldCorrectlyConvert(double value, string expected)
        {
            var percentage = new Percentage(value);
            string result = percentage;
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ImplicitConvertToDouble_ShouldCorrectlyConvert()
        {
            double value = Random.NextDouble();
            var percentage = new Percentage(value);
            double result = percentage;
            Assert.That(result, Is.EqualTo(value));
        }

        [Test]
        public void ImplicitConvertFromDouble_ShouldCorrectlyConvert()
        {
            double value = Random.NextDouble();
            Percentage percentage = value;
            Percentage samePercentage = new Percentage(value);
            Assert.That(percentage, Is.EqualTo(samePercentage));
        }

        private static object[] _toStringCases =
        {
            new object[] { 0.5, "50%" },
            new object[] { 1.0, "100%" },
            new object[] { 0.40123, "40,12%" },
            new object[] { 0.21987, "21,99%" },
            new object[] { 0.005, "0,5%" }
        };
    }
}