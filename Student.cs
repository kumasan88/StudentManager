using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Assignment_2
{
    class Student
    {
        public int StudentID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Major { get; set; }
        public string PhoneNumber { get; set; }
        public double GPA { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Student()
        {

        }

        public Student(int studentID, string firstName, string lastName, string major, string phoneNumber, int gpa, DateTime dateOfBirth)
        {
            StudentID = studentID;
            FirstName = firstName;
            LastName = lastName;
            Major = major;
            PhoneNumber = phoneNumber;
            GPA = gpa;
            DateOfBirth = dateOfBirth;
        }

        public string printStudent() //display for single student
        {
            string result = "";
            result += "\n**********************\n";
            result += "Student ID: " + StudentID;
            result += "\nFirst Name:" + FirstName;
            result += "\nLast Name:" + LastName;
            result += "\nMajor: " + Major;
            result += "\nPhone Number: " + PhoneNumber;
            result += "\nGPA: " + GPA;
            result += "\nDate of Birth: " + DateOfBirth.Date.ToString("MM-dd-yyyy");
            result += "\n**********************\n";

            return result;
        }

        public string printStudentRow() //to display in a row looking like table
        {
            return $" {StudentID, 8}   {FirstName, 14}  {LastName, 14}   {Major, 6}    {PhoneNumber, 14}    {GPA, 8}   {DateOfBirth.Date.ToString("MM/dd/yyyy"), 12}";
        }

        public override string ToString() //display in text file
        {
            return $"{StudentID},{FirstName},{LastName},{Major},{PhoneNumber},{GPA},{DateOfBirth.Date.ToString("MM/dd/yyyy")}";
        }
    }
}
