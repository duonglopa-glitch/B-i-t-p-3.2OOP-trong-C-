using System;

namespace OOP_Practice
{
    public class Person
    {
        // C# 7.3: private set
        public string Id { get; private set; }
        public string FullName { get; set; }
        public int BirthYear { get; set; }

        public Person(string id, string fullName, int birthYear)
        {
            Id = id;
            FullName = fullName;
            BirthYear = birthYear;
        }

        public int GetAge(int currentYear) => currentYear - BirthYear;
    }

    public class Employee : Person
    {
        public decimal BaseSalary { get; set; }

        public Employee(string id, string fullName, int birthYear, decimal baseSalary)
            : base(id, fullName, birthYear)
        {
            BaseSalary = baseSalary;
        }

        public virtual decimal CalculateIncome() => BaseSalary;
    }

    public sealed class Manager : Employee
    {
        public decimal ResponsibilityAllowance { get; set; }

        public Manager(string id, string fullName, int birthYear, decimal baseSalary, decimal allowance)
            : base(id, fullName, birthYear, baseSalary)
        {
            ResponsibilityAllowance = allowance;
        }

        public override decimal CalculateIncome() => BaseSalary + ResponsibilityAllowance;
    }
}