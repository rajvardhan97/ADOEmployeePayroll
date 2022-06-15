using System;
namespace EmployeePayrollADO
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Employee employeeData = new Employee();
            employeeData.SetConnection();
            employeeData.Close();
        }
    }
}