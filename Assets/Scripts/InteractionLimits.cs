using UnityEngine;

public class InteractionLimits : MonoBehaviour
{
      public static Vector2 minBounds;
      public static Vector2 maxBounds;

      private SpriteRenderer _sR;

      void Start() {

            _sR = GetComponent<SpriteRenderer>();

            float z = Camera.main.WorldToScreenPoint(transform.position).z;

            Vector3 bottomLeft = Camera.main.ViewportToWorldPoint( // define o ponto inferior esquerdo da camera
                new Vector3(0.3f, 0.2f, z) // estamos diminuindo para nao pegar a area toda
            );

            Vector3 topRight = Camera.main.ViewportToWorldPoint( // define o ponto superior direito da camera
                new Vector3(1f, 1f, z)
            );

            minBounds = bottomLeft;
            maxBounds = topRight;

            StretchSpriteToArea(_sR, bottomLeft, topRight);
      }

      // essa função serve para ajustar uma sprite para ela formar a "mesa"
      void StretchSpriteToArea(SpriteRenderer spriteRenderer, Vector3 areaBottomLeft, Vector3 areaTopRight) {
            float areaWidth = areaTopRight.x - areaBottomLeft.x;
            float areaHeight = areaTopRight.y - areaBottomLeft.y;

            Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

            float scaleX = areaWidth / spriteSize.x;
            float scaleY = areaHeight / spriteSize.y;

            spriteRenderer.transform.localScale = new Vector3(scaleX, scaleY, 1f);

            // Centraliza na área
            spriteRenderer.transform.position = new Vector3(
                (areaBottomLeft.x + areaTopRight.x) * 0.5f,
                (areaBottomLeft.y + areaTopRight.y) * 0.5f,
                spriteRenderer.transform.position.z
            );
      }

      void OnDrawGizmos() { // desenha a area para controle
            if (!Camera.main)
                  return;

            // Profundidade do objeto em relação à câmera
            float z = Camera.main.WorldToScreenPoint(transform.position).z;

            // Limites responsivos (Viewport)
            Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(
                new Vector3(0.3f, 0.2f, z)
            );

            Vector3 topRight = Camera.main.ViewportToWorldPoint(
                new Vector3(1f, 1f, z)
            );

            Vector3 topLeft = new Vector3(bottomLeft.x, topRight.y, bottomLeft.z);
            Vector3 bottomRight = new Vector3(topRight.x, bottomLeft.y, bottomLeft.z);

            // Cor do Gizmo
            Gizmos.color = Color.green;

            // Desenha o retângulo
            Gizmos.DrawLine(bottomLeft, bottomRight);
            Gizmos.DrawLine(bottomRight, topRight);
            Gizmos.DrawLine(topRight, topLeft);
            Gizmos.DrawLine(topLeft, bottomLeft);

            // Limites responsivos (Viewport)
            Vector3 bottomLeft2 = Camera.main.ViewportToWorldPoint(
                new Vector3(0f, 0.2f, z)
            );

            Vector3 topRight2 = Camera.main.ViewportToWorldPoint(
                new Vector3(0.3f, 1f, z)
            );

            Vector3 topLeft2 = new Vector3(bottomLeft2.x, topRight2.y, bottomLeft2.z);
            Vector3 bottomRight2 = new Vector3(topRight2.x, bottomLeft2.y, bottomLeft2.z);

            // Cor do Gizmo
            Gizmos.color = Color.blue;

            // Desenha o retângulo
            Gizmos.DrawLine(bottomLeft2, bottomRight2);
            Gizmos.DrawLine(bottomRight2, topRight2);
            Gizmos.DrawLine(topRight2, topLeft2);
            Gizmos.DrawLine(topLeft2, bottomLeft2);
      }
}
