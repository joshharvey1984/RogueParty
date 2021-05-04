using UnityEngine;

namespace RogueParty.Core.UI {
    public class CircleDraw : MonoBehaviour {
        float thetaScale = 0.01f;
        int size;
        LineRenderer lineRenderer;

        public void Draw(Vector2 mousePosition, float radius) {
            var sizeValue = (2.0f * Mathf.PI) / thetaScale;
            size = (int)sizeValue;
            size++;
            lineRenderer = gameObject.GetComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.positionCount = size;
            var theta = 0f;
            for (var i = 0; i < size; i++) {
                theta += (2.0f * Mathf.PI * thetaScale);
                var x = radius * Mathf.Cos(theta);
                var y = radius * Mathf.Sin(theta);
                var position = mousePosition;
                x += position.x;
                y += position.y;
                var pos = new Vector3(x, y, 0);
                lineRenderer.SetPosition(i, pos);
            }
        }
    }
}