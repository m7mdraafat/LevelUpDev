namespace LevelUpDev.Domain.Enums;

/// <summary>
/// Types of leaderboards.
/// </summary>
public enum LeaderboardType
{
    QuestChampions,    // Quest level completion - Real-time
    StreakKings,       // Current streak days - Daily
    SpeedDemons,       // Problems solved this week - Weekly
    HardCrushers,      // Hard problems solved - Monthly
    ContestWarriors,   // Contest rating - After contests
    RisingStars        // Most improved (% gain) - Weekly
}

/// <summary>
/// Refresh frequency for leaderboards.
/// </summary>
public enum LeaderboardRefreshRate
{
    RealTime,
    Daily,
    Weekly,
    Monthly,
    AfterContests
}
