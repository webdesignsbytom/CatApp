namespace CatApp.Model.Game
{
    public class CatModel
    {
        public string Name { get; set; }
        public int Happiness { get; set; } = 1; // Out of 5

        public CatModel(string name)
        {
            Name = name;
        }

        public int IncreaseHappiness()
        {
            Happiness++;
            if (Happiness > 5) Happiness = 5; // Ensure happiness does not exceed 5
            return Happiness;
        }

        public int DecreaseHappiness() 
        {
            Happiness--;
            if (Happiness <= 0) Happiness = 0;
            return Happiness;
        }
    }
}
