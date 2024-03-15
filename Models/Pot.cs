namespace Aplikace.Models
{
    public class Pot
    {
        public int PotId { get; set; }
        public string Name { get; set; } = "";
        public int Size { get; set; }

        public int LidId { get; set; }
        public Lid Lid { get; set; }
    }
}
