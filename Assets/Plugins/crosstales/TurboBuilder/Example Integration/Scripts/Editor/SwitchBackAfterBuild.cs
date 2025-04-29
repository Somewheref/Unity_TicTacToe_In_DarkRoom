#if false || CT_DEVELOP
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Crosstales.TPB.Example
{
   /// <summary>Switch back to a defined BuildTarget after building.</summary>
   [InitializeOnLoad]
   public abstract class SwitchBackAfterBuild
   {
      #if UNITY_EDITOR_WIN
         private const BuildTarget target = BuildTarget.StandaloneWindows64; //change to the desired BuildTarget after a build
      #elif UNITY_EDITOR_LINUX
         private const BuildTarget target = BuildTarget.StandaloneLinux64; //change to the desired BuildTarget after a build
      #else
         private const BuildTarget target = BuildTarget.StandaloneOSX; //change to the desired BuildTarget after a build
      #endif

      #region Constructor

      static SwitchBackAfterBuild()
      {
         Builder.OnBuildingComplete += onBuildingComplete;
         Builder.OnBuildAllComplete += onBuildAllComplete;
      }

      #endregion


      #region Callbacks

      private static void onBuildingComplete(bool success)
      {
         switchBack();
      }

      private static void onBuildAllComplete(bool success)
      {
         switchBack();
      }

      #endregion


      #region Private methods

      private static void switchBack()
      {
         Debug.Log($"Switching back to default target.");

#if CT_TPS
         Crosstales.TPS.Switcher.Switch(target);
#else
         BuildTargetGroup group = BuildPipeline.GetBuildTargetGroup(target);
         EditorUserBuildSettings.SwitchActiveBuildTarget(group, target);
#endif
      }

      #endregion
   }
}
#endif
#endif
// © 2020-2024 crosstales LLC (https://www.crosstales.com)