namespace LevelUpDev.Domain.Enums;

/// <summary>
/// Badge rarity levels.
/// </summary>
public enum BadgeRarity
{
    Common = 1,
    Rare = 2,
    Epic = 3,
    Legendary = 4,
    Mythic = 5,
    Special = 6
}

/// <summary>
/// Achievement types for the badge system.
/// </summary>
public enum AchievementType
{
    // Streak Achievements
    FirstSteps,
    OnFire,          // 7-day streak
    Lightning,       // 30-day streak
    Unstoppable,     // 100-day streak

    // Quest Achievements
    QuestMaster,     // Complete any Quest
    DsaSage,         // Complete all 35 DSA levels
    Polyglot,        // Complete all 4 Quests

    // Leaderboard Achievements
    CommunityChampion,

    // Social Achievements
    Mentor,          // Help 10 members

    // Challenge Achievements
    Sharpshooter     // 100% daily challenge completion for a month
}
