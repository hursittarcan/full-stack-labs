using Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;

namespace HumanResources.Domain.Tests
{
    public class EmployeeFactoryTests : TestBase
    {
        private Employee.Factory _factory;
        private string _lastName;
        private string _firstName;
        private DateTime _startDate;
        private int _sequence;

        [SetUp]
        public void BeforeEachTest()
        {
            _factory = new Employee.Factory();

            _lastName = Random.NextString();
            _firstName = Random.NextString();
            _startDate = Random.NextDateTimeInFuture();
            _sequence = Random.Next(1, 1000);
        }

        [Test]
        public void CreateNew_ValidInput_ShouldInitializeFieldsCorrectly()
        {
            //Act
            IEmployee employee = _factory.CreateNew(_lastName, _firstName, _startDate, _sequence);

            //Assert
            Assert.That(employee.Number, Is.EqualTo(new EmployeeNumber(_startDate, _sequence)));
            Assert.That(employee.FirstName, Is.EqualTo(_firstName));
            Assert.That(employee.LastName, Is.EqualTo(_lastName));
            Assert.That(employee.StartDate, Is.EqualTo(_startDate));
            Assert.That(employee.EndDate, Is.Null);
        }

        [Test]
        public void CreateNew_StartDateMoreThanAYearAgo_ShouldThrowContractException()
        {
            DateTime invalidStartDate = DateTime.Now.AddYears(-1);
            Assert.That(() => _factory.CreateNew(_lastName, _firstName, invalidStartDate, _sequence), Throws.InstanceOf<ContractException>());
        }

        [TestCase(null)]
        [TestCase("a")]
        public void CreateNew_InvalidFirstName_ShouldThrowContractException(string invalidFirstName)
        {
            Assert.That(() => _factory.CreateNew(_lastName, invalidFirstName, _startDate, _sequence), Throws.InstanceOf<ContractException>());
        }

        [TestCase(null)]
        [TestCase("a")]
        public void CreateNew_InvalidLastName_ShouldThrowContractException(string invalidLastName)
        {
            Assert.That(() => _factory.CreateNew(invalidLastName, _firstName, _startDate, _sequence), Throws.InstanceOf<ContractException>());
        }
    }
}