

namespace BusinessLogicLayer
{
    [Serializable]
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string FullName { get; set; }

        public override string ToString()
        {
            return $"Teacher ID: {TeacherId}, Full Name: {FullName}";
        }
    }
}