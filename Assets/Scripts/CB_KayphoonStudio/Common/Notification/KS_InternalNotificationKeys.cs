using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KayphoonStudio
{
    public static class KS_InternalNotificationKeys
    {

        // Game State (below are not yet implemented in KS codebase)
        public const string OnGameFirstOpen = nameof(OnGameFirstOpen);  // not implemented
        public const string OnGameStart = nameof(OnGameStart);
        public const string OnGameEnd = nameof(OnGameEnd);
        public const string OnGamePause = nameof(OnGamePause);
        public const string OnGameResume = nameof(OnGameResume);


        // Common
        public const string OnSceneSwitched = nameof(OnSceneSwitched);
        public const string OnLanguageChanged = nameof(OnLanguageChanged);

        // Ranking
        public const string RankingDataReceived = nameof(RankingDataReceived);
        public const string RankingDataFailed = nameof(RankingDataFailed);
        public const string UpdatePlayerNameSuccess = nameof(UpdatePlayerNameSuccess);
        public const string UpdatePlayerNameFailed = nameof(UpdatePlayerNameFailed);
        public const string UpdatePlayerNameRejected = nameof(UpdatePlayerNameRejected);


        // AD
        public const string AD_Banner_OnLoaded = nameof(AD_Banner_OnLoaded);
        public const string AD_Banner_OnLoadError = nameof(AD_Banner_OnLoadError);
        public const string AD_Banner_OnClick = nameof(AD_Banner_OnClick);
        public const string AD_Banner_OnShown = nameof(AD_Banner_OnShown);
        public const string AD_Banner_OnHidden = nameof(AD_Banner_OnHidden);
        public const string AD_Interstitial_OnShown = nameof(AD_Interstitial_OnShown);
        public const string AD_Interstitial_OnClicked = nameof(AD_Interstitial_OnClicked);
        public const string AD_Interstitial_OnCompleted = nameof(AD_Interstitial_OnCompleted);
        public const string AD_Interstitial_OnSkipped = nameof(AD_Interstitial_OnSkipped);
        public const string AD_Interstitial_OnFinishedAnyways = nameof(AD_Interstitial_OnFinishedAnyways);
        public const string AD_Rewarded_OnShown = nameof(AD_Rewarded_OnShown);
        public const string AD_Rewarded_OnClicked = nameof(AD_Rewarded_OnClicked);
        public const string AD_Rewarded_OnCompleted = nameof(AD_Rewarded_OnCompleted);
        public const string AD_Rewarded_OnSkipped = nameof(AD_Rewarded_OnSkipped);
        public const string AD_Rewarded_OnFailed = nameof(AD_Rewarded_OnFailed);
        public const string AD_Rewarded_OnFinishedAnyways = nameof(AD_Rewarded_OnFinishedAnyways);
    }
}
