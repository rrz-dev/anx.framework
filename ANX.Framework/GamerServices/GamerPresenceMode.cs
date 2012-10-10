#region Using Statements
using System;
using ANX.Framework.NonXNA.Development;

#endregion // Using Statements

// This file is part of the ANX.Framework created by the
// "ANX.Framework developer group" and released under the Ms-PL license.
// For details see: http://anxframework.codeplex.com/license

namespace ANX.Framework.GamerServices
{
    [PercentageComplete(100)]
    [Developer("Glatzemann")]
    [TestState(TestStateAttribute.TestState.Tested)]
    public enum GamerPresenceMode
    {
        None,
        SinglePlayer,
        Multiplayer,
        LocalCoOp,
        LocalVersus,
        OnlineCoOp,
        OnlineVersus,
        VersusComputer,
        Stage,
        Level,
        CoOpStage,
        CoOpLevel,
        ArcadeMode,
        CampaignMode,
        ChallengeMode,
        ExplorationMode,
        PracticeMode,
        PuzzleMode,
        ScenarioMode,
        StoryMode,
        SurvivalMode,
        TutorialMode,
        DifficultyEasy,
        DifficultyMedium,
        DifficultyHard,
        DifficultyExtreme,
        Score,
        VersusScore,
        Winning,
        Losing,
        ScoreIsTied,
        Outnumbered,
        OnARoll,
        InCombat,
        BattlingBoss,
        TimeAttack,
        TryingForRecord,
        FreePlay,
        WastingTime,
        StuckOnAHardBit,
        NearlyFinished,
        LookingForGames,
        WaitingForPlayers,
        WaitingInLobby,
        SettingUpMatch,
        PlayingWithFriends,
        AtMenu,
        StartingGame,
        Paused,
        GameOver,
        WonTheGame,
        ConfiguringSettings,
        CustomizingPlayer,
        EditingLevel,
        InGameStore,
        WatchingCutscene,
        WatchingCredits,
        PlayingMinigame,
        FoundSecret,
        CornflowerBlue
    }
}
