namespace LevelUpDev.Domain.Enums;

/// <summary>
/// User profile themes unlocked by quest completion.
/// </summary>
public enum ProfileTheme
{
    Default,
    LinearShoal,
    SequenceValley,
    ForestWalker,
    SpeedSolver,
    GraphMaster,
    StrategySummit
}

/// <summary>
/// User titles earned through achievements.
/// </summary>
public enum UserTitle
{
    Newcomer,
    ForestWalker,      // 🌲 Forest Walker
    SpeedSolver,       // ⚡ Speed Solver
    StreakWarrior,     // 🔥 Streak Warrior
    QuestMaster,       // 🏆 Quest Master
    DsaSage,           // 🧠 DSA Sage
    CommunityChampion, // 👑 Community Champion
    Mentor             // 🤝 Mentor
}

/// <summary>
/// User roles in the platform.
/// </summary>
public enum UserRole
{
    Member = 1,
    Moderator = 2,
    Admin = 3
}
