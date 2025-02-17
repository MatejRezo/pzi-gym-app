namespace SportiverseApi.Models.Exercise
{
    public class PlanExercises
    {
        public int ID { get; set; }
        public int PlanID { get; set; }

        public Exercise Exercise { get; set; }
    }
}
