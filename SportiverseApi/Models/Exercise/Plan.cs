namespace SportiverseApi.Models.Exercise
{
    public class Plan
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public String Description { get; set; } = string.Empty;
        public List<PlanExercises> Exercises { get; set; }
    }
}
