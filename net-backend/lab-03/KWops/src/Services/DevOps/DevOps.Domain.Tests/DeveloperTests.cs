using Domain;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;

namespace DevOps.Domain.Tests
{
    public class DeveloperTests : TestBase
    {
        private string _id;
        private string _firstName;
        private string _lastName;
        private Percentage _rating;

        [SetUp]
        public void BeforeEachTest()
        {
            _id = Random.NextString();
            _firstName = Random.NextString();
            _lastName = Random.NextString();
            _rating = Random.NextDouble();
        }

        [Test]
        public void CreateNew_ValidInput_ShouldInitializeFieldsCorrectly()
        {
            //Act
            Developer developer = Developer.CreateNew(_id, _firstName, _lastName, _rating);

            //Assert
            Assert.That(developer.Id, Is.EqualTo(_id));
            Assert.That(developer.FirstName, Is.EqualTo(_firstName));
            Assert.That(developer.LastName, Is.EqualTo(_lastName));
            Assert.That(developer.Rating, Is.EqualTo(_rating));
        }

        [TestCase(null)]
        [TestCase("")]
        public void CreateNew_EmptyId_ShouldThrowContractException(string emptyId)
        {
            Assert.That(() => Developer.CreateNew(emptyId, _firstName, _lastName, _rating), Throws.InstanceOf<ContractException>());
        }

        [TestCase(null)]
        [TestCase("")]
        public void CreateNew_EmptyFirstName_ShouldThrowContractException(string emptyFirstName)
        {
            Assert.That(() => Developer.CreateNew(_id, emptyFirstName, _lastName, _rating), Throws.InstanceOf<ContractException>());
        }

        [TestCase(null)]
        [TestCase("")]
        public void CreateNew_EmptyLastName_ShouldThrowContractException(string emptyLastName)
        {
            Assert.That(() => Developer.CreateNew(_id, _firstName, emptyLastName, _rating), Throws.InstanceOf<ContractException>());
        }
    }
}
