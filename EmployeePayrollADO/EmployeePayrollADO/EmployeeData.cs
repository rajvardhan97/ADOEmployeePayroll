using System;
namespace EmployeePayrollADO
{
    public class EmployeeData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Salary { get; set; }
        public DateTime StartDate { get; set; }
        public char Gender { get; set; }
        public double PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public double BasicPay { get; set; }
        public double Deductions { get; set; }
        public double IncomeTax { get; set; }
        public double TaxablePay { get; set; }
        public double NetPay { get; set; }
    }
}