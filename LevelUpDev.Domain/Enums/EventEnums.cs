namespace LevelUpDev.Domain.Enums;

/// <summary>
/// Days of the week for events.
/// </summary>
public enum EventDay
{
    Monday,    // Week kickoff - New community goal
    Wednesday, // Squad Battle Day
    Friday,    // Random Challenge Friday
    Saturday,  // Contest Prep - Mock contest
    Sunday     // Weekly Recap & Awards
}

/// <summary>
/// Types of weekly events.
/// </summary>
public enum WeeklyEventType
{
    WeekKickoff,
    SquadBattle,
    RandomChallenge,
    ContestPrep,
    WeeklyRecap
}

/// <summary>
/// Challenge difficulty levels.
/// </summary>
public enum ChallengeDifficulty
{
    Easy = 1,
    Medium = 2,
    Hard = 3
}
