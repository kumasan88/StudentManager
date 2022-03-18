using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;

namespace Assignment_2
{
    class StudentManager
    {
        private List<Student> students;
        public StudentManager()
        { //constructor
            students = new List<Student>();
        }
        public void Start()
        {//start function created to organize and execute all the functions
            LoadStudents(); //start by reading all the data from file. loading all the saved students
            bool isStart = true; //bool set to true
            while (isStart) //repeats the menu if user answers true which is determined by y or n
            { // 5 choices for the menu
                int choose = Menu(); //input chosen from menu
                switch (choose)
                {
                    case 1: //displays all students
                        Clear();
                        SortAllStudents();
                        break;
                    case 2: //search for student
                        Clear();
                        SearchStudent();
                        break;
                    case 3: //add new student
                        Clear();
                        AddStudent();
                        break;
                    case 4: //update student data
                        Clear();
                        UpdateStudent();
                        break;
                    default: //exits
                        return;
                }
                Write("\nWould you like to go back to the menu? (Y/N) ");
                isStart = YesNo(); // if input from user is Y or y = true, goes back to menu 
                Clear();
            }
            ReadLine();
        }
        public void AddStudent()
        { //adding new student data from this function
            bool isAddAgain = true;
            while (isAddAgain) //in while loop for adding students. user inputs n or N it exits the loop.
            {
                Student student = new Student();
                student.StudentID = StudentManager.IDGenerator(); //student id generated each time when student is added
                Write("Please enter the Student's first name: ");
                student.FirstName = GetCharacters();

                Write("Please enter the Student's last name: ");
                student.LastName = GetCharacters();

                Write("Please enter the major of the student: ");
                student.Major = ReadLine();

                Write("Please enter Student's phone number: ");
                student.PhoneNumber = Phone();

                Write("Please enter Student's GPA: ");
                student.GPA = GPA();

                Write("Please enter Student's Date of Birth: ");
                student.DateOfBirth = DOB();
                WriteLine("\nStudent is added\n");
                
                students.Add(student); //adding student data to list
                StreamWriter studentOutputFile = new StreamWriter("students.txt", true);
                studentOutputFile.WriteLine(student);//writing to students text file, data will be saved even when closing application.
                studentOutputFile.Close(); // close file

                Write("Would you like to enter new student data? (Y/N) ");
                isAddAgain = YesNo();
                Clear();
                
            }
        }
        string GetCharacters()
        { //only letters
            string input = ReadLine();
            while (!Regex.IsMatch(input, @"^[a-zA-Z ]+$")) //limit only lowercase and uppercase letters for input.
            {
                Write("Only letter. Please Try Again: ");
                input = ReadLine();
            }
            return input;
        }
        string Phone()
        {//phone number pattern
            string input = ReadLine();
            while (!Regex.IsMatch(input, @"^\d{3}-\d{3}-\d{4}$")) //123-123-1234 pattern only
            {
                Write("Please enter in this format: 123-123-1234. Please Try Again: ");
                input = ReadLine();
            }
            return input;
        }
        double GPA()
        {
            double input = GetRangeDouble(0,4); //gpa can be entered from min 0 to max 4. 
            return input;
        }
        DateTime DOB()
        { //for user to enter correct date format.
            DateTime input;

            while (!DateTime.TryParse(ReadLine(), out input)) //when it's not dateformat
            {
                Write("Invalid Format! Please enter the date of birth in MM/DD/YYYY. For example: 01/01/1111:  ");
            }
            return input;
        }
        public static int IDGenerator()
        {
            StreamReader generatorInputFile = new StreamReader("generator.txt"); //reading generator file
            int id;
            if (!Int32.TryParse(generatorInputFile.ReadLine(), out id))
            {
                id = 10000; // id initialized to 10000
            }
            generatorInputFile.Close(); //close reader file
            StreamWriter generatorOutputFile = new StreamWriter("generator.txt"); //write file
            generatorOutputFile.WriteLine(id + 1); //everytime when it is written in file, it adds 1 to id
            generatorOutputFile.Close();
            return id; //returns id starting from 10000
        }
        public int Menu()
        {// menu function for user to select
            WriteLine(" *****************************************************************\n");
            WriteLine("                         Student Management\n");
            WriteLine(" *****************************************************************\n");
            WriteLine("                       1. Display all students\n");
            WriteLine("                       2. Search student\n");
            WriteLine("                       3. Add student\n");
            WriteLine("                       4. Update student\n");
            WriteLine("                       5. Exit\n");
            WriteLine(" *****************************************************************\n");
            Write("              Please choose what you would like to do: ");
            return GetRangeInt(1, 5); //for the user to enter only 1 or 5 from the menu.
        }
        public int UpdateMenu()
        {
            WriteLine(" *****************************************************************\n");
            WriteLine("                         Update Student\n");
            WriteLine(" *****************************************************************\n");
            WriteLine("                       1. First Name\n");
            WriteLine("                       2. Last Name\n");
            WriteLine("                       3. Major\n");
            WriteLine("                       4. Phone Number\n");
            WriteLine("                       5. GPA\n");
            WriteLine("                       6. Date of Birth\n");
            WriteLine("                       7. Exit\n");
            WriteLine(" *****************************************************************\n");
            Write("        Please choose which data you would like to update: ");
            return GetRangeInt(1, 7);
        }
        public int SearchMenu()
        {
            WriteLine(" *****************************************************************\n");
            WriteLine("                         Search Student By\n");
            WriteLine(" *****************************************************************\n");
            WriteLine("                       1. Student ID\n");
            WriteLine("                       2. Major\n");
            WriteLine("                       3. GPA\n");
            WriteLine("                       4. Exit\n");
            WriteLine(" *****************************************************************\n");
            Write("        Please choose which method you want to search by: ");
            return GetRangeInt(1, 4);
        }
        public int SortMenu()
        {
            WriteLine(" *****************************************************************\n");
            WriteLine("                         Sort Students By\n");
            WriteLine(" *****************************************************************\n");
            WriteLine("                         1. First Name\n");
            WriteLine("                         2. Last Name\n");
            WriteLine("                         3. GPA\n");
            WriteLine("                         4. Student ID\n");
            WriteLine(" *****************************************************************\n");
            Write("         Please choose which method you want to sort by: ");
            return GetRangeInt(1, 4);
        }
        public int FilterGPAMenu()
        {
            WriteLine(" *****************************************************************\n");
            WriteLine("                          Filter GPA By\n");
            WriteLine(" *****************************************************************\n");
            WriteLine("                       1. Higher than\n");
            WriteLine("                       2. Lower than\n");
            WriteLine("                       3. Exact\n");
            WriteLine(" *****************************************************************\n");
            Write("        Please choose which method you want to filter GPA by: ");
            return GetRangeInt(1, 3);
        }
        public bool YesNo()
        { //boolean function used to only accept Y or N as an answer. as well as lowercase letter y and n
            string rtn = "";
            do
            {
                try
                {
                    rtn = ReadLine();
                    if (rtn != "y" && rtn != "Y" && rtn != "n" && rtn != "N") //if condition used to only accept y,Y,n,N values
                    {
                        throw new FormatException("Invalid Entry:");
                    }
                }
                catch (FormatException e)
                {// if other input, ask for input again.
                    rtn = "";
                    Write($"{e.Message} Please enter either Y or N! ");
                }

            } while (rtn == "");
            return rtn == "Y" || rtn == "y"; //y or Y set to true.
        }
        public double GetRangeDouble(double min, double max)
        {//function for the user to only enter min to max double number and only positive numbers or else gives error msg and prompts the user to enter the correct value
            double num = -1; //num set to run while loop
            do
            {
                try
                {
                    num = Convert.ToDouble(ReadLine());
                    if (num < min || num > max) //when other numbers entered that's not in the range of the min and man, throws it to FormatException error.
                    {
                        throw new FormatException("Invalid Entry!"); //when FormatException error occurs, throws it to do what is inside catch
                    }
                }
                catch (FormatException e) //when letters or entered wrong number  
                {
                    num = -1;  //forces to run while loop
                    Write($"{e.Message} Please enter either {min} or {max}! ");
                }
            } while (num == -1); //while loop runs when there is error.
            return num;
        }
        public int GetRangeInt(int min, int max)
        {//function for the user to only enter min to max number and only positive numbers or else gives error msg and prompts the user to enter the correct value
            int num = -1; //num set to run while loop
            do
            {
                try
                {
                    num = Convert.ToInt32(ReadLine());
                    if (num < min || num > max) //when other numbers entered that's not in the range of the min and man, throws it to FormatException error.
                    {
                        throw new FormatException(); //when FormatException error occurs, throws it to do what is inside catch
                    }
                }
                catch (FormatException) //when letters or entered wrong number  
                {
                    num = -1;  //forces to run while loop
                    Write($"Invalid Entry! Please enter either {min} or {max}! ");
                }
            } while (num == -1); //while loop runs when there is error.
            return num;
        }
        public void UpdateStudent()
        {
            bool isAddAgain = true;
            Student student = SearchStudentById(); //student is selected by id
            if (student == null) //when student doesn't exist
            {
                return;
            }
            do
            {
                int type = UpdateMenu(); //chooses type of data to update
                UpdateStudentByType(student, type);
                
                WriteLine("\nWould you like to change another data of this student? (Y/N) ");
                isAddAgain = YesNo();
                Clear();
            }while (isAddAgain);

            WriteLine("Would you like to save all the data? (Y/N) ");
            if (YesNo()) //for user to save data so it writes to file.
            {
                SaveStudents();
            }
        }
        private void UpdateStudentByType(Student student, int type)
        {
            if(type != 7)  // 7 exits
            {
                string[] types = { "First Name", "Last Name", "Major", "Phone Number", "GPA", "Date of Birth" }; //array of string types created to ask question when user selects certain type of data
                Write($"Please enter {types[type - 1]}: "); //type -1 added because minimum user input would be 1 and array index starts from 0.
                switch (type)
                {
                    case 1:
                        student.FirstName = GetCharacters();
                        break;
                    case 2:
                        student.LastName = GetCharacters();
                        break;
                    case 3:
                        student.Major = ReadLine();
                        break;
                    case 4:
                        student.PhoneNumber = Phone();
                        break;
                    case 5:
                        student.GPA = GPA();
                        break;
                    case 6:
                        student.DateOfBirth = DOB();
                        break;
                    case 7:
                        break;

                }
            }
        }
        public Student SearchStudentById()
        { //searches only by student ID
            Write("Please enter the ID of the student you would like to update: ");
            Student student = null;
            int id;
            Int32.TryParse(ReadLine(), out id);
            student = students.Find(std => std.StudentID == id); //find student that has the same value as the id that was entered by user
            DisplayStudent(student);
            return student;
        }
        public List<Student> SearchStudent()
        {
            List<Student> students = null;
            do
            {
                Clear();
                int target = SearchMenu(); // target determined from what user entered from search menu
                Clear();
                students = SearchStudents(target); //filter student function called with user's choice
                DisplayAllStudents(students); //displays filtered students

                Write("Would you like to search for another student? (Y/N) ");

            } while (YesNo()); //able to keep searching for student until user inputs n
            
            return students;
        }
        public List<Student> SearchStudents(int type)
        {
            List<Student> students = null;
            switch (type)
            {
                case 1: //search by student id
                    Write("Enter student's ID: ");
                    int id; 
                    Int32.TryParse(ReadLine(), out id); 
                    students = this.students.FindAll(std => std.StudentID == id); //finds student with same id as the input
                    break;
                case 2: //search by major
                    Write("Enter student's Major: ");
                    string major = ReadLine();
                    students = this.students.FindAll(std => std.Major == major); //finds all students with same major as the input
                    break;
                case 3: //search by gpa
                    int option = FilterGPAMenu();
                    Clear();
                    Write("Enter the GPA:");
                    double gpa;
                    Double.TryParse(ReadLine(), out gpa);
                    switch (option)
                    {
                        case 1:
                            students = this.students.FindAll(std => std.GPA > gpa); //displays higher than gpa input
                            break;
                        case 2:
                            students = this.students.FindAll(std => std.GPA < gpa); //displays lower than the gpa input
                            break;
                        case 3:
                            students = this.students.FindAll(std => std.GPA == gpa); //displays students that has the same gpa as the input
                            break;
                    }
                    break;
                case 4:
                    break;
            }

            return students;
        }
        public void DisplayStudent(Student student)
        { //used to display single student in searchstudentbyid function
            if (student == null) //when student doesn't exist 
            {
                WriteLine("Student does not exist.");
            }
            else //display student when they exist
            {
                WriteLine(student.printStudent());
            }
        }
        public void SortAllStudents()
        {
            bool isContinue = false;   
            do
            {
                Clear();
                DisplayAllStudents(students);
                Write("Would you like sort the students? (Y/N): ");
                if(YesNo()) //if yes, prompt user 3 options of how they want to sort students
                {
                    Clear();
                    isContinue = true;
                    int target = SortMenu();
                    SortStudents(target); //sortstudents function runs according to chosen target
                } else
                {
                    isContinue = false;
                }
            } while (isContinue); //while isContinue is true
        }
        public void DisplayAllStudents(List<Student> students)
        { //displaying all the students in list
            WriteLine("-------------------------------------------------------------------------------------------------");
            WriteLine("  StudentID       FirstName       LastName     Major        Phone#            GPA        DOB   ");
            WriteLine("-------------------------------------------------------------------------------------------------");
            if(students.Count != 0) // to display no data when student isn't found.
            {
                students.ForEach(student => WriteLine(student.printStudentRow())); //displaying each student by row
            } else
            {
                WriteLine("                                           No Data");
            }
            WriteLine("-------------------------------------------------------------------------------------------------");
        }
        public void SortStudents(int target)
        {
            if(target == 1) // if user select 1, sort by firstname
            {
                students.Sort((s1, s2) => s1.FirstName.CompareTo(s2.FirstName));
            } 
            else if(target == 2) //if user select 2, sort by lastname
            {
                students.Sort((s1, s2) => s1.LastName.CompareTo(s2.LastName));
            } 
            else if (target == 3)
            { //user select 3, sort by GPA
                students.Sort((s1, s2) => s2.GPA.CompareTo(s1.GPA));
            }
            else
            { //4. sort by student id
                students.Sort((s1, s2) => s1.StudentID.CompareTo(s2.StudentID));
            }
        }
        public void LoadStudents()
        {// to read all the data from file when application starts, list gets populated with student data
            StreamReader studentInputFile = new StreamReader("students.txt");
            while (studentInputFile.Peek() != -1) //reads all the data in students.txt file
            {
                Student student = new Student();
                string[] cols = studentInputFile.ReadLine().Split(','); //string array created. split by , reads each line in file
                student.StudentID = Convert.ToInt32(cols[0]); //enter each data split by , into student
                student.FirstName = cols[1];
                student.LastName = cols[2];
                student.Major = cols[3];
                student.PhoneNumber = cols[4];
                student.GPA = Convert.ToDouble(cols[5]);
                DateTime dateTime;
                DateTime.TryParse(cols[6], out dateTime);
                student.DateOfBirth = dateTime;

                students.Add(student); //add to student list
            }
            studentInputFile.Close(); 
        }
        public void SaveStudents()
        {
            StreamWriter studentOutputFile = new StreamWriter("students.txt", false);
            studentOutputFile.Write("");
            studentOutputFile.Close(); //delete all data from file and close
            studentOutputFile = new StreamWriter("students.txt", true); //write file that will save all the data even after closing application
            students.ForEach(student => //writes each student data in the file
            {
                studentOutputFile.WriteLine(student);//writing to students text file, data will be saved even when closing application.
            });
            //write existing data plus updated data to text file and close.
            studentOutputFile.Close(); 
        }

    }
}
