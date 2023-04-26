namespace Api.Utils
{
    public class Utilities
    {
        public Utilities() { }

        public string WantedMonts (int month)
        {
            return month switch
            {
                5 => "[2021-05] ",
                6 => "[2021-06] ",
                _ => "[2021-07] ",
            };
        }

        public string AscOrDesc (bool descending)
        {
            if (descending)
            {
                return "DESC";
            }
            return "ASC";
        }
    }
}
