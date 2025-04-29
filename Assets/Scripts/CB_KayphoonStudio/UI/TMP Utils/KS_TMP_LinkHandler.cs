using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using KayphoonStudio;

namespace KayphoonStudio.TMP
{
    public class KS_TMP_LinkHandler : MonoBehaviour, IPointerClickHandler
    {
        public TMP_Text textMeshPro;
        [Tooltip("The script that has the interface to receive link id and do stuff.")]
        public KS_TMP_LinkEventReceiver stringParameterInterface;
        public bool LogLinkID = true;

        private void Reset()
        {
            textMeshPro = GetComponent<TMP_Text>();
            stringParameterInterface = GetComponent<KS_TMP_LinkEventReceiver>();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // Check if the click is over a link
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(textMeshPro, Input.mousePosition, null);
            if (linkIndex != -1)
            {
                // Get the link info
                TMP_LinkInfo linkInfo = textMeshPro.textInfo.linkInfo[linkIndex];

                // Get the link ID
                string linkId = linkInfo.GetLinkID();
                HandleLinkClick(linkId);
            }
        }

        private void HandleLinkClick(string linkId)
        {
            // Log the link ID or handle it as needed
            if (LogLinkID)
            {
                KS_Logger.Log("Link ID: \"" + linkId + "\"", this);
            }

            if (stringParameterInterface != null)
            {
                stringParameterInterface.ReceiveString(linkId);
            }
            else
            {
                KS_Logger.LogWarning("No KS_StringParameterInterface found on " + gameObject.name, this);
            }
        }
    }

}
