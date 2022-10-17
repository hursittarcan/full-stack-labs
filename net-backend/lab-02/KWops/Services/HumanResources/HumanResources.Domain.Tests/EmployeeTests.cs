using Domain;
using HumanResources.Domain.Tests.Builders;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test;

namespace HumanResources.Domain.Tests
{
    public class EmployeeTests : TestBase
    {
        [Test]
        public void Dismiss_WithoutNotice_ShouldSetEndDateOnToday()
        {
            //Arrange
            Employee employee = new EmployeeBuilder().WithEndDate(null).Build();

            //Act
            employee.Dismiss(withNotice: false);

            //Assert
            Assert.That(employee.EndDate, Is.EqualTo(DateTime.Now).Within(10).Seconds);
        }

        [Test]
        public void Dismiss_WithoutNotice_EmployeeAlreadyHasEndDate_ShouldSetEndDateOnToday()
        {
            //Arrange
            Employee employee = new EmployeeBuilder().WithEndDate(DateTime.Now.AddDays(5)).Build();

            //Act
            employee.Dismiss(withNotice: false);

            //Assert
            Assert.That(employee.EndDate, Is.EqualTo(DateTime.Now).Within(10).Seconds);
        }

        [Test]
        public void Dismiss_WithNotice_EmployeeAlreadyHasEndDate_ShouldThrowContractException()
        {
            //Arrange
            Employee employee = new EmployeeBuilder().WithEndDate(DateTime.Now.AddDays(5)).Build();

            //Act + Assert
            Assert.That(() => employee.Dismiss(withNotice: true), Throws.InstanceOf<ContractException>());
        }

        [Test]
        public void Dismiss_WithNotice_LessThan3MonthsInService_ShouldSetEndDateInOneWeek()
        {
            //Arrange

            DateTime lessThan3MonthsAgo = DateTime.Now.AddDays(-28);
            Employee employee = new EmployeeBuilder()
                .WithStartDate(lessThan3MonthsAgo)
                .WithEndDate(null)
                .Build();

            //Act
            employee.Dismiss(withNotice: true);

            //Assert
            DateTime over1Week = DateTime.Now.AddDays(7);
            Assert.That(employee.EndDate, Is.EqualTo(over1Week).Within(10).Seconds);
        }

        [Test]
        public void Dismiss_WithNotice_LessThan12MonthsInService_ShouldSetEndDateIn2Weeks()
        {
            //Arrange
            DateTime lessThan12MonthsAgo = DateTime.Now.AddMonths(-10);
            Employee employee = new EmployeeBuilder()
                .WithStartDate(lessThan12MonthsAgo)
                .WithEndDate(null)
                .Build();

            //Act
            employee.Dismiss(withNotice: true);

            //Assert
            DateTime over2Weeks = DateTime.Now.AddDays(14);
            Assert.That(employee.EndDate, Is.EqualTo(over2Weeks).Within(10).Seconds);
        }

        [Test]
        public void Dismiss_WithNotice_MoreThan12MonthsInService_ShouldSetEndDateIn4Weeks()
        {
            //Arrange
            DateTime moreThan12MonthsAgo = DateTime.Now.AddYears(-1);
            Employee employee = new EmployeeBuilder()
                .WithStartDate(moreThan12MonthsAgo)
                .WithEndDate(null)
                .Build();

            //Act
            employee.Dismiss(withNotice: true);

            //Assert
            DateTime over4Weeks = DateTime.Now.AddDays(28);
            Assert.That(employee.EndDate, Is.EqualTo(over4Weeks).Within(10).Seconds);
        }
    }
}
