namespace StatTrackerGlobal.Shared
{
    public class VolleyballPlayerValidator
    {
        public static bool Validate(string firstName, string lastName, string team, int jerseyNumber, string height, string position)
        {
            if (string.IsNullOrWhiteSpace(firstName) ||
                string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(team) ||
                jerseyNumber <= 0 ||
                jerseyNumber >= 100 ||
                string.IsNullOrWhiteSpace(height) ||
                string.IsNullOrWhiteSpace(position))
            {
                return false;
            }
            return true;
        }
    }
}
