using System.Reflection;

namespace Aplikace.Models
{
    public class Lid
    {
        public int LidId { get; set; }
        public string Name { get; set; } = "";
        public int Size { get; set; }

        public int PotId { get; set; }
        public Pot Pot { get; set; }
    }
}
