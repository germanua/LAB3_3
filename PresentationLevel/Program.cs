using BusinessLogicLayer;

namespace PresentationLevel;

public class Program
{
    private static List<Student> studentList;

    static void Main(string[] args)
    {
        InitializeStudentList();

        bool exit = false;

        while (!exit)
        {
            Menu.DisplayMenu();
            string choice = Menu.GetUserInput("Enter your choice: ");

            switch (choice)
            {
                case "1":
                    Menu.AddStudent(studentList);
                    break;
                case "2":
                    Menu.RemoveStudent(studentList);
                    break;
                case "3":
                    Menu.UpdateStudent(studentList);
                    break;
                case "4":
                    Menu.FindStudent(studentList);
                    break;
                case "5":
                    Menu.ShowStudentList(studentList);
                    break;
                case "6":
                    Menu.SaveData(studentList);
                    break;
                case "7":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }
    }

    private static void InitializeStudentList()
    {
        LoadStudentsFromJson();
        if (studentList == null)
        {
            studentList = new List<Student>();
        }
    }

    private static void LoadStudentsFromJson()
    {
        JsonSerializer<Student> jsonSerializer = new JsonSerializer<Student>("students.json");
        studentList = jsonSerializer.Load();
    }
}