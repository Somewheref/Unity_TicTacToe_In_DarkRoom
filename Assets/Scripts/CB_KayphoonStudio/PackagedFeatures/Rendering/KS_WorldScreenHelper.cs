using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KayphoonStudio.PackagedFeatures.Rendering
{
    public class KS_WorldScreenHelper
    {
        public static Vector2 GetScreenSize()
        {
            return new Vector2(Screen.width, Screen.height);
        }

        /// <summary>
        /// Retrns the ratio of the world position to the screen size
        /// e.g. (0.1, 0.1) means the world position is at 10% of the screen size (from the bottom left corner
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="currentPos"></param>
        /// <returns></returns>
        public static Vector2 GetWorldScreenRatio(Camera camera, Vector3 currentPos, bool reverseY = false)
        {
            Vector3 screenPos = camera.WorldToScreenPoint(currentPos);
            return new Vector2(screenPos.x / Screen.width, reverseY ? 1 - screenPos.y / Screen.height : screenPos.y / Screen.height);
        }


        /// <summary>
        /// Returns the world position on the canvas
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="currentPos"></param>
        /// <returns></returns>
        public static Vector2 GetWorldCanvasPosition_ScreenOverlay(Camera camera, Vector3 currentPos)
        {
            Vector3 screenPos = camera.WorldToScreenPoint(currentPos);
            return screenPos;
        }


        /// <summary>
        /// Returns the world position on the canvas
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="currentPos"></param>
        /// <param name="canvas"></param>
        /// <returns></returns>
        public static Vector2 GetWorldCanvasPosition_CameraOverlay(Camera camera, Vector3 currentPos, Canvas canvas)
        {
            Vector3 screenPos = camera.WorldToScreenPoint(currentPos);
            Vector2 canvasLocalPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPos, canvas.worldCamera, out canvasLocalPos);
            return canvasLocalPos;
        }


        /// <summary>
        /// Returns the canvas position, from a world position (ScreenSpace - Camera)
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="currentPos"></param>
        /// <param name="canvas"></param>
        /// <returns></returns>
        public static Vector2 GetCanvasPositionFromWorldPosition(Camera camera, Vector3 worldPosition, Canvas canvas)
        {
            RectTransform canvasRectTransform = canvas.GetComponent<RectTransform>();

            // Convert the world position to a viewport position
            Vector3 viewportPosition = camera.WorldToViewportPoint(worldPosition);
            
            // Convert the viewport position to a canvas position
            Vector2 canvasPosition = new(
                (viewportPosition.x * canvasRectTransform.sizeDelta.x) - (canvasRectTransform.sizeDelta.x * 0.5f),
                (viewportPosition.y * canvasRectTransform.sizeDelta.y) - (canvasRectTransform.sizeDelta.y * 0.5f)
            );
            
            return canvasPosition;
        }
    }
}