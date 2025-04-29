using System.Collections;
using System.Collections.Generic;
using KayphoonStudio.SimpleHelper;
using UnityEngine;

// shorthand for KS_InternalSavingKeys
using KSISKeys = KayphoonStudio.KS_InternalSavingKeys;

namespace KayphoonStudio
{
    public static class KS_SaveValuesCommon
    {
        // here are a list of values that are commonly used in all games
        public static bool MusicOn => KS_SimpleSaver.Load<bool>(KSISKeys.MusicOn, true);
        public static bool SfxOn => KS_SimpleSaver.Load<bool>(KSISKeys.SFXOn, true);
        public static int PersonalBest => KS_SimpleSaver.Load<int>(KSISKeys.PersonalBest, 0);
        public static int AdSchemaID => KS_SimpleSaver.Load<int>(KSISKeys.AdSchemaID, 2);
    }
}
