namespace SampleModels.Models
{
    public class Tick
    {
        public static readonly Tick Shared = new Tick();

        private Tick() { }
    }
}
