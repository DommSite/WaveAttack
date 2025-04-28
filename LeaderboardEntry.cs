namespace WaveAttack
{
    public class LeaderboardEntry
    {
        public string Name { get; set; }
        public int Score { get; set; }

        public LeaderboardEntry(string name, int score)
        {
            Name = name;
            Score = score;
        }
    }
}