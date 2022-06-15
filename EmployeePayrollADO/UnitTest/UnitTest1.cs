using NUnit.Framework;

namespace EmployeePayrollADO
{
    public class Tests
    {
        Employee employee;
        [SetUp]
        public void Setup()
        {
            employee = new Employee();
        }

        [Test]
        public void UpdateQuery()
        {
            int expected = 1;
            int actual = employee.UpdateSalary();
            Assert.AreEqual(actual, expected);
        }

        [Test]
        public void UpdateQuery_UsingStoredProcedure()
        {
            EmployeeData employeeData = new EmployeeData();
            int expected = 1;
            employeeData.ID = 4;
            employeeData.Name = "Terrisa";
            employeeData.Basic_Pay = 30000000;
            int actual = employee.UpdateSalary(employeeData);
            Assert.AreEqual(actual, expected);
        }
    }
}