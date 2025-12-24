namespace LevelUpDev.Domain.Enums;

/// <summary>
/// Types of quests available in the platform.
/// </summary>
public enum QuestType
{
    DSA = 1,
    Database = 2,
    SystemDesign = 3,
    Maths = 4
}

/// <summary>
/// Theme zones for DSA Quest (35 levels).
/// </summary>
public enum DsaThemeZone
{
    LinearShoal = 1,        // Levels 1-5
    SequenceValley = 2,     // Levels 6-10
    AssociationSlope = 3,   // Levels 11-15
    SortingPlateau = 4,     // Levels 16-20
    RecursionMaze = 5,      // Levels 21-25
    GraphTheoryPeaks = 6,   // Levels 26-28
    TreeShapedForest = 7,   // Levels 29-32
    StrategySummit = 8      // Levels 33-35
}

/// <summary>
/// Theme zones for Database Quest (5 levels).
/// </summary>
public enum DatabaseThemeZone
{
    SqlBasic = 1,
    FilteringAggregation = 2,
    GroupingJoin = 3,
    WindowFunctions = 4,
    SqlAdvanced = 5
}

/// <summary>
/// Theme zones for System Design Quest (5 levels).
/// </summary>
public enum SystemDesignThemeZone
{
    CacheSystem = 1,
    DataFlowProcessing = 2,
    DataStructureDesign = 3,
    BusinessSystemSimulation = 4,
    ComprehensiveDataOperation = 5
}

/// <summary>
/// Theme zones for Maths Quest (7 levels).
/// </summary>
public enum MathsThemeZone
{
    ArithmeticReasoning = 1,
    DivisibilityModular = 2,
    CombinationPermutation = 3,
    NumberTheory = 4,
    GeometricConfiguration = 5,
    BitOperation = 6,
    BitmaskState = 7
}
