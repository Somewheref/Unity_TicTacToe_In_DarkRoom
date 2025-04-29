
using UnityEngine;

namespace KayphoonStudio.PublicEvents
{
    public class KS_Event_ApplicationQuit : MonoBehaviour
    {
        public void QuitApplication()
        {
            Application.Quit();
            KS_Logger.Log("Application quit");
        }
    }

}
