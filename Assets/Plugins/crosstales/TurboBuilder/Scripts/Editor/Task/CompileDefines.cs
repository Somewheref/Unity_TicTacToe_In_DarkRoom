﻿#if UNITY_EDITOR
using UnityEditor;

namespace Crosstales.TPB.EditorTask
{
   /// <summary>Adds the given define symbols to PlayerSettings define symbols.</summary>
   [InitializeOnLoad]
   public class CompileDefines : Crosstales.Common.EditorTask.BaseCompileDefines
   {
      private const string symbol = "CT_TPB";

      static CompileDefines()
      {
         if (Crosstales.TPB.Util.Config.COMPILE_DEFINES)
         {
            addSymbolsToAllTargets(symbol);
         }
         else
         {
            removeSymbolsFromAllTargets(symbol);
         }
      }
   }
}
#endif
// © 2018-2024 crosstales LLC (https://www.crosstales.com)