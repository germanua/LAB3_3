using System;

namespace BusinessLogicLayer
{
    [Serializable]
    public class Person
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }

        public Person(int id, string fullName, DateTime dateOfBirth)
        {
            Id = id;
            FullName = fullName;
            DateOfBirth = dateOfBirth;
        }
    }
}