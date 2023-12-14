
namespace BusinessLogicLayer
{
    [Serializable]
    public class Astronaut
    {
        public int AstronautId { get; set; }
        public string FullName { get; set; }

        public override string ToString()
        {
            return $"Astronaut ID: {AstronautId}, Full Name: {FullName}";
        }
    }
}