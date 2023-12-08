namespace BusinessLogicLayer
{
    [Serializable]
    public class Student : IComparable<Student>
    {
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public int YearOfBirth { get; set; }
        public int GroupNumber { get; set; }
        public int CourseLevel { get; set; } 
        
        [NonSerialized]
        private string someTransientData; // Example of a non-serialized field

        // Parameterless constructor for serialization
        public Student()
        {
        }

        public Student(int studentid, string fullName, int yearOfBirth, int groupNumber, int courseLevel)
        {
            StudentId = studentid;
            FullName = fullName;
            YearOfBirth = yearOfBirth;
            GroupNumber = groupNumber;
            CourseLevel = courseLevel;
        }

        public void TransferToNextCourse()
        {
            CourseLevel++; 
        }

        public int CalculateAge()
        {
            int currentYear = DateTime.Now.Year;
            int age = currentYear - YearOfBirth;
            return age;
        }

        public override string ToString()
        {
            return $"Student ID: {StudentId}, Full Name: {FullName}, Year of Birth: {YearOfBirth}, Group Number: {GroupNumber}, Course Level: {CourseLevel}";
        }

        public int CompareTo(Student? other)
        {
            if (other == null)
            {
                return 1;
            }
            return StudentId.CompareTo(other.StudentId);
        }
    }
}
