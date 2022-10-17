using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;

namespace HumanResources.Domain.Tests
{
    public class EmployeeNumberTests : TestBase
    {
        [Test]
        public void Constructor_FromStartDateAndSequence_ShouldInitializeProperly()
        {
            //Arrange
            DateTime startDate = Random.NextDateTimeInFuture();
            int sequence = Random.NextPositive();

            //Act
            var number = new EmployeeNumber(startDate, sequence);

            //Assert
            Assert.That(number.Year, Is.EqualTo(startDate.Year));
            Assert.That(number.Month, Is.EqualTo(startDate.Month));
            Assert.That(number.Day, Is.EqualTo(startDate.Day));
            Assert.That(number.Sequence, Is.EqualTo(sequence));
        }

        [Test]
        public void Constructor_FromStartDateAndSequence_InvalidSequence_ShouldThrowContractException()
        {
            //Arrange
            DateTime startDate = Random.NextDateTimeInFuture();
            int invalidSequence = Random.NextZeroOrNegative();

            //Act + Assert
            Assert.That(() => new EmployeeNumber(startDate, invalidSequence), Throws.InstanceOf<ContractException>());
        }

        [TestCase("19990101001", 1999, 1, 1, 1)]
        [TestCase("20001231123", 2000, 12, 31, 123)]
        public void Constructor_FromString_ShouldInitializeProperly(string input, int expectedYear, int expectedMonth, int expectedDay, int expectedSequence)
        {
            //Act
            var number = new EmployeeNumber(input);

            //Assert
            Assert.That(number.Year, Is.EqualTo(expectedYear));
            Assert.That(number.Month, Is.EqualTo(expectedMonth));
            Assert.That(number.Day, Is.EqualTo(expectedDay));
            Assert.That(number.Sequence, Is.EqualTo(expectedSequence));
        }

        [TestCase("")]
        [TestCase("1")]
        [TestCase("abcdefghikj")]
        [TestCase("00001231001")]
        [TestCase("19991331001")]
        [TestCase("19990031001")]
        [TestCase("19991200001")]
        [TestCase("19991232001")]
        [TestCase("19991231000")]
        public void Constructor_FromString_InvalidInput_ShouldThrowContractException(string input)
        {
            Assert.That(() => new EmployeeNumber(input), Throws.InstanceOf<ContractException>());
        }

        [TestCaseSource(nameof(ToStringCases))]
        public void ToString_ShouldCorrectlyConvert(DateTime startDate, int sequence, string expected)
        {
            EmployeeNumber number = new EmployeeNumber(startDate, sequence);
            string result = number.ToString();
            Assert.That(result, Is.EqualTo(expected));
        }

        [TestCaseSource(nameof(ToStringCases))]
        public void ImplicitConvertToString_ShouldCorrectlyConvert(DateTime startDate, int sequence, string expected)
        {
            EmployeeNumber number = new EmployeeNumber(startDate, sequence);
            string result = number;
            Assert.That(result, Is.EqualTo(expected));
        }

        private static object[] ToStringCases =
        {
            new object[] { new DateTime(2000,12,29), 987, "20001229987" },
            new object[] { new DateTime(1,2,3), 4, "00010203004" } //with padded zero's
        };
    }
}