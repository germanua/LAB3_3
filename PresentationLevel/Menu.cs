using System.Globalization;
using System.Text.RegularExpressions;
using BusinessLogicLayer;

namespace PresentationLevel;

public static class Menu
{
    public static void DisplayMenu()
    {
        Console.WriteLine("Choose an operation:");
        Console.WriteLine("1. Add Student");
        Console.WriteLine("2. Remove Student");
        Console.WriteLine("3. Update Student");
        Console.WriteLine("4. Find Student");
        Console.WriteLine("5. Show Student List");
        Console.WriteLine("6. Save Data");
        Console.WriteLine("7. Calculate 3rd-year students born in summer");
        Console.WriteLine("8. Exit");
    }
    

    public static void ShowStudentList(List<Student> students)
    {
        int pageSize = 10;
        int currentPage = 1;

        do
        {
            Console.Clear();
            Console.WriteLine($"Student List - Page {currentPage}");
            DisplayStudentPage(students, pageSize, currentPage);

            Console.WriteLine("[L] Previous Page   [R] Next Page   [M] Back to Menu");

            char choice = GetUserInput("Enter your choice: ")[0];

            switch (choice)
            {
                case 'l':
                    if (currentPage > 1)
                        currentPage--;
                    break;
                case 'r':
                    int lastPage = (int)Math.Ceiling((double)students.Count / pageSize);
                    if (currentPage < lastPage)
                        currentPage++;
                    break;
                case 'm':
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        } while (true);
    }

    private static void DisplayStudentPage(List<Student> students, int pageSize, int currentPage)
    {
        int startIndex = (currentPage - 1) * pageSize;
        int endIndex = Math.Min(startIndex + pageSize - 1, students.Count - 1);

        for (int i = startIndex; i <= endIndex; i++)
        {
            Console.WriteLine($"{i + 1}. {students[i]}");
        }
    }

    public static void AddStudent(List<Student> studentList)
    {
        Console.Write("Enter Student ID: ");
        int id = GetValidStudentId();

        Console.Write("Enter Full Name: ");
        string name = GetValidFullName();

        Console.Write("Enter Date of Birth (dd-mm-yyyy): ");
        DateTime dateOfBirth = GetValidDateOfBirth();

        Console.Write("Enter Group Number: ");
        int groupNumber = GetValidGroupNumber();

        Console.Write("Enter Course Level: ");
        int courseLevel = GetValidCourseLevel();

        Console.Write("Is the student a teacher? (true/false): ");
        bool isTeacher = bool.Parse(Console.ReadLine());

        Console.Write("Is the student an astronaut? (true/false): ");
        bool isAstronaut = bool.Parse(Console.ReadLine());

        Console.Write("Can the student sing? (true/false): ");
        bool canSing = bool.Parse(Console.ReadLine());

        Student newStudent = new Student(id, name, dateOfBirth, groupNumber, courseLevel)
        {
            IsTeacher = isTeacher,
            IsAstronaut = isAstronaut,
            CanSing = canSing
        };

        studentList.Add(newStudent);

        Console.WriteLine("Student added successfully!");
    }
    private static DateTime GetValidDateOfBirth()
    {
        while (true)
        {
            Console.Write("Enter Date of Birth (dd-mm-yyyy): ");
            string input = Console.ReadLine();

            if (DateTime.TryParseExact(input, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateOfBirth))
            {
                return dateOfBirth;
            }

            Console.WriteLine("Invalid input. Please enter a valid Date of Birth in dd-mm-yyyy format.");
        }
    }

    public static void RemoveStudent(List<Student> studentList)
    {
        Console.Write("Enter Student ID to remove: ");
        int id = GetValidStudentId();

        Student studentToRemove = studentList.FirstOrDefault(s => s.Id == id);

        if (studentToRemove != null)
        {
            studentList.Remove(studentToRemove);
            Console.WriteLine("Student removed successfully!");
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }

    public static void UpdateStudent(List<Student> studentList)
    {
        Console.Write("Enter Student ID to update: ");
        int id = GetValidStudentId();

        Student studentToUpdate = studentList.FirstOrDefault(s => s.Id == id);

        if (studentToUpdate != null)
        {
            Console.Write("Enter Full Name: ");
            string name = GetValidFullName();

            Console.Write("Enter Date of Birth (dd-mm-yyyy): ");
            DateTime dateOfBirth = GetValidDateOfBirth();

            Console.Write("Enter Group Number: ");
            int groupNumber = GetValidGroupNumber();

            Console.Write("Enter Course Level: ");
            int courseLevel = GetValidCourseLevel();

            studentList.Remove(studentToUpdate);

            Student updatedStudent = new Student(id, name, dateOfBirth, groupNumber, courseLevel)
            {
                IsTeacher = studentToUpdate.IsTeacher,
                IsAstronaut = studentToUpdate.IsAstronaut,
                CanSing = studentToUpdate.CanSing
            };
            studentList.Add(updatedStudent);

            Console.WriteLine("Student updated successfully!");
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }


    public static void FindStudent(List<Student> studentList)
    {
        Console.Write("Enter Student ID to find: ");
        int id = GetValidStudentId();

        Student studentToFind = studentList.FirstOrDefault(s => s.Id == id);

        if (studentToFind != null)
        {
            Console.WriteLine($"Found Student: {studentToFind}");
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }

    public static void CalculateSummerBirthdays(List<Student> students)
    {
        int currentYear = DateTime.Now.Year;
        int thirdYear = currentYear - 2;

        var summerBirthdays = students
            .Where(student => student.CourseLevel == 3 && student.DateOfBirth.Month >= 6 && student.DateOfBirth.Month <= 8)
            .Count();

        Console.WriteLine($"Number of 3rd-year students born in summer: {summerBirthdays}");
    }

    private static int GetValidStudentId()
    {
        while (true)
        {
            Console.Write("Enter Student ID (5 to 8 digits): ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int id) && id >= 10000 && id <= 99999999)
            {
                return id;
            }

            Console.WriteLine("Invalid input. Please enter a valid Student ID.");
        }
    }

    private static string GetValidFullName()
    {
        while (true)
        {
            Console.Write("Enter Full Name (letters and space between name and surname): ");
            string input = Console.ReadLine();

            if (Regex.IsMatch(input, "^[a-zA-Z]+ [a-zA-Z]+$"))
            {
                return input;
            }

            Console.WriteLine("Invalid input. Please enter a valid Full Name.");
        }
    }

    private static int GetValidYearOfBirth()
    {
        while (true)
        {
            Console.Write("Enter Year of Birth: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int yearOfBirth))
            {
                return yearOfBirth;
            }

            Console.WriteLine("Invalid input. Please enter a valid Year of Birth.");
        }
    }

    private static int GetValidGroupNumber()
    {
        while (true)
        {
            Console.Write("Enter Group Number (2 to 3 digits): ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int groupNumber) && groupNumber >= 10 && groupNumber <= 999)
            {
                return groupNumber;
            }

            Console.WriteLine("Invalid input. Please enter a valid Group Number.");
        }
    }

    private static int GetValidCourseLevel()
    {
        while (true)
        {
            Console.Write("Enter Course Level (1 to 5): ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int courseLevel) && courseLevel >= 1 && courseLevel <= 5)
            {
                return courseLevel;
            }

            Console.WriteLine("Invalid input. Please enter a valid Course Level.");
        }
    }

    public static string GetUserInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }

    public static string GetFileType()
    {
        return GetUserInput("Enter file type (json/xml/custom/binary): ");
    }

    public static void SaveData(List<Student> studentList)
    {
        string defaultFilePath = "students";
        string fileType = GetFileType();
        string extension = GetFileExtension(fileType);
        string filePath = $"{defaultFilePath}{extension}";

        Console.WriteLine("Saving data...");

        switch (fileType.ToLower())
        {
            case "json":
                SaveToJson(filePath, studentList);
                break;
            case "xml":
                SaveToXml(filePath, studentList);
                break;
            case "custom":
                SaveToCustom(filePath, studentList, extension);
                break;
            case "binary":
                SaveToBinary(filePath, studentList, extension);
                break;
            default:
                Console.WriteLine("Invalid file type.");
                return;
        }

        Console.WriteLine($"Data saved successfully to {filePath}!");
    }

    private static string GetFileExtension(string fileType)
    {
        switch (fileType.ToLower())
        {
            case "json":
                return ".json";
            case "xml":
                return ".xml";
            case "custom":
                return ".txt";
            case "binary":
                return ".bin";
            default:
                return "";
        }
    }

    private static void SaveToJson(string filePath, List<Student> studentList)
    {
        var jsonSerializer = new JsonSerializer<Student>(filePath);
        jsonSerializer.Save(studentList);
    }

    private static void SaveToXml(string filePath, List<Student> studentList)
    {
        var xmlSerializer = new XmlSerializer<Student>(filePath);
        xmlSerializer.Save(studentList);
    }

    private static void SaveToCustom(string filePath, List<Student> studentList, string extension)
    {
        var customSerializer = new CustomSerializer<Student>(filePath);
        customSerializer.Save(studentList);
    }

    private static void SaveToBinary(string filePath, List<Student> studentList, string extension)
    {
        var binarySerializer = new BinarySerializer<Student>(filePath);
        binarySerializer.Save(studentList);
    }

}