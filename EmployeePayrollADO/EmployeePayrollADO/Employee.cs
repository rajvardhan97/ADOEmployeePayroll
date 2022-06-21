using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace EmployeePayrollADO
{
    public class Employee
    {
        public static string connection = @"Data Source = RAJVARDHAN;Initial Catalog = Payroll_Service;Integrated Security=SSPI";
        SqlConnection sqlConnection = new SqlConnection(connection);

        

        public void SetConnection()
        {
            if(sqlConnection != null && sqlConnection.State.Equals(ConnectionState.Closed))
            {
                try
                {
                    sqlConnection.Open();
                }
                catch (Exception)
                {
                    throw new CustomException(CustomException.ExceptionType.Connection_Failed, "Connection Failed");
                }
            }
        }

        public void CloseConnection()
        {
            if(sqlConnection != null && !sqlConnection.State.Equals(ConnectionState.Open))
            {
                try
                {
                    sqlConnection.Close();
                }
                catch(Exception)
                {
                    throw new CustomException(CustomException.ExceptionType.Connection_Failed, "Connection Failed");
                }
            }
        }

        EmployeeData employeeData = new EmployeeData();
        public void GetSqlData()
        {
            sqlConnection.Open();
            string Spname = "dbo.ViewDetails";
            using (sqlConnection)
            {
                SqlCommand sqlCommand = new SqlCommand(Spname, sqlConnection);
                        sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                if (sqlDataReader.HasRows)
                {
                    while (sqlDataReader.Read())
                    {
                        employeeData.ID = sqlDataReader.GetInt32(1);
                        employeeData.Name = sqlDataReader["Name"].ToString();
                        employeeData.Salary = Convert.ToDouble(sqlDataReader["Salary"]);
                        employeeData.StartDate = Convert.ToDateTime(sqlDataReader["StartDate"]);
                        employeeData.Gender = Convert.ToChar(sqlDataReader["Gender"]);
                        employeeData.PhoneNumber = Convert.ToDouble(sqlDataReader["PhoneNumber"]);
                        employeeData.Address = sqlDataReader["Address"].ToString();
                        employeeData.Department = sqlDataReader["Department"].ToString();
                        employeeData.BasicPay = Convert.ToDouble(sqlDataReader["BasicPay"]);
                        employeeData.Deductions = Convert.ToDouble(sqlDataReader["Deductions"]);
                        employeeData.IncomeTax = Convert.ToDouble(sqlDataReader["IncomeTax"]);
                        employeeData.TaxablePay = Convert.ToDouble(sqlDataReader["TaxablePay"]);
                        employeeData.NetPay = Convert.ToDouble(sqlDataReader["NetPay"]);

                        Console.WriteLine("ID: " + employeeData.ID + "\n" +
                            "Name: " + employeeData.Name + "\n" +
                            "Salary: " + employeeData.Salary + "\n" +
                            "Start Date: " + employeeData.StartDate + "\n" +
                            "Gender: " + employeeData.Gender + "\n" +
                            "Phone Number: " + employeeData.PhoneNumber + "\n" +
                            "Address: " + employeeData.Address + "\n" +
                            "Department: " + employeeData.Department + "\n" +
                            "Basic Pay: " + employeeData.BasicPay + "\n" +
                            "Deductions: " + employeeData.Deductions + "\n" +
                            "Income Tax: " + employeeData.IncomeTax + "\n" +
                            "Taxable Pay: " + employeeData.TaxablePay + "\n" +
                            "Net Pay: " + employeeData.NetPay);
                    }
                    sqlDataReader.Close();
                }
                sqlConnection.Close();
            }
        }

        public int UpdateSalary()
        {
            sqlConnection.Open();
            string query = "update employee_payroll set BasicPay=3000000 where Name= 'Terrrisa'";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            int result = sqlCommand.ExecuteNonQuery();
            if (result != 0)
            {
                Console.WriteLine("Updated");
            }
            else
            {
                Console.WriteLine("Not Updated");
            }
            sqlConnection.Close();
            GetSqlData();
            return result;
        }

        public int UpdateSalarySP(EmployeeData employeeData)
        {
            int result = 0;
            try
            {
                using (sqlConnection)
                {
                    SqlCommand sqlCommand = new SqlCommand("dbo.UpdateDetails", this.sqlConnection);
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@BasicPay", 3000000);
                    sqlCommand.Parameters.AddWithValue("@Name", "Terrisa");
                    sqlCommand.Parameters.AddWithValue("@Id", 4);

                    sqlConnection.Open();
                    result = sqlCommand.ExecuteNonQuery();

                    if (result != 0)
                    {
                        Console.WriteLine("Updated");
                        return result;
                    }
                    else
                    {
                        Console.WriteLine("Not Updated");
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                throw new CustomException(CustomException.ExceptionType.No_data_found, "Cannot Update");
            }
        }

        public string DataBasedOnDateRange()
        {
            string nameList = "";
            try
            {
                using (sqlConnection)
                {
                    string query = @"select * from employee_payroll where StartDate BETWEEN '2022-01-12' and GetDate()";
                    SqlCommand command = new SqlCommand(query, this.sqlConnection);
                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = command.ExecuteReader();
                    if (sqlDataReader.HasRows)
                    {
                        while (sqlDataReader.Read())
                        {
                            DisplayEmployeeDetails(sqlDataReader);
                            nameList += sqlDataReader["Name"].ToString();
                        }
                    }
                    sqlDataReader.Close();
                }

            }
            catch (Exception)
            {
                throw new CustomException(CustomException.ExceptionType.No_data_found, "No Data Found");
            }
            sqlConnection.Close();
            return nameList;

        }
        public void DisplayEmployeeDetails(SqlDataReader sqlDataReader)
        {
            employeeData.ID = sqlDataReader.GetInt32(0);
            employeeData.Name = sqlDataReader["Name"].ToString();
            employeeData.Salary = Convert.ToDouble(sqlDataReader["Salary"]);
            employeeData.StartDate = Convert.ToDateTime(sqlDataReader["StartDate"]);
            employeeData.Gender = Convert.ToChar(sqlDataReader["Gender"]);
            employeeData.PhoneNumber = Convert.ToDouble(sqlDataReader["PhoneNumber"]);
            employeeData.Address = sqlDataReader["Address"].ToString();
            employeeData.Department = sqlDataReader["Department"].ToString();
            employeeData.BasicPay = Convert.ToDouble(sqlDataReader["BasicPay"]);
            employeeData.Deductions = Convert.ToDouble(sqlDataReader["Deduction"]);
            employeeData.IncomeTax = Convert.ToDouble(sqlDataReader["IncomeTax"]);
            employeeData.TaxablePay = Convert.ToDouble(sqlDataReader["TaxablePay"]);
            employeeData.NetPay = Convert.ToDouble(sqlDataReader["NetPay"]);

            Console.WriteLine("ID" + employeeData.ID + "\n" +
                "Name" + employeeData.Name + "\n" +
                "Salary" + employeeData.Salary + "\n" +
                "Start Date" + employeeData.StartDate + "\n" +
                "Gender" + employeeData.Gender + "\n" +
                "Phone Number" + employeeData.PhoneNumber + "\n" +
                "Address" + employeeData.Address + "\n" +
                "Department" + employeeData.Department + "\n" +
                "Basic Pay" + employeeData.BasicPay + "\n" +
                "Deductions" + employeeData.Deductions + "\n" +
                "Income Tax" + employeeData.IncomeTax + "\n" +
                "Taxable Pay" + employeeData.TaxablePay + "\n" +
                "Net Pay" + employeeData.NetPay);
        }

        public int RemoveEmployee()
        {
            try
            {
                using (sqlConnection)
                {
                    SqlCommand command = new SqlCommand("ado.DeleteDetails", sqlConnection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ID", employeeData.ID);
                    sqlConnection.Open();
                    int result = command.ExecuteNonQuery();

                    if (result != 0)
                    {
                        Console.WriteLine("Contact is deleted");
                        return result;
                    }
                    else
                        return result;
                    sqlConnection.Close();
                }
            }
            catch (Exception)
            {
                throw new CustomException(CustomException.ExceptionType.No_data_found, "No Data Found");
            }
        }
    }
}
