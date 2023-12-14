using System;

namespace BusinessLogicLayer
{
    
    [Serializable]
    
    public class Student : Person
    {
        public int GroupNumber { get; set; }
        public int CourseLevel { get; set; }
        public bool IsTeacher { get; set; }
        public bool IsAstronaut { get; set; }
        public bool CanSing { get; set; }

        public Student(int studentId, string fullName, DateTime dateOfBirth, int groupNumber, int courseLevel)
            : base(studentId, fullName, dateOfBirth)
        {
            GroupNumber = groupNumber;
            CourseLevel = courseLevel;
        }
        public Student() : base(0, "", DateTime.Now)
        {
            // Initialize any default values if needed
        }
       

        public override string ToString()
        {
            return $"Student ID: {Id}, Full Name: {FullName}, Date of Birth: {DateOfBirth:dd-MM-yyyy}, Group Number: {GroupNumber}, Course Level: {CourseLevel}";
        }

        public int CompareTo(Student? other)
        {
            if (other == null)
            {
                return 1;
            }
            return Id.CompareTo(other.Id);
        }
    }
}