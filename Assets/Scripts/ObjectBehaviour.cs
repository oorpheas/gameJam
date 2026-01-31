using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
      [SerializeField] private float _scaleFactor = 5f;

      private float _zCoord;
      private bool _isDraging = true;

      private Vector3 _offset;
      private Vector3 _startScale;
      private Vector3 _newScale;s

      private SpriteRenderer _sR;
      private static int _topSortingOrder = 0;


      void Awake() {
            _sR = GetComponent<SpriteRenderer>();
      }

      private void Start() {
            // pega a escala inicial e define a porcentagem de destaque ao segurar
            _startScale = transform.localScale;
            _newScale.x = _startScale.x + _startScale.x * (_scaleFactor / 100);
            _newScale.y = _startScale.y + _startScale.y * (_scaleFactor / 100);
            _newScale.z = _startScale.z + _startScale.z * (_scaleFactor / 100);
      }

      void OnMouseDown() {
            // coloca o ultimo objeto clicado em destaque
            _sR.sortingOrder = ++_topSortingOrder;

            // aplica o destaque
            Highlight(_isDraging);

            // pega a profundidade (distância da câmera)
            _zCoord = Camera.main.WorldToScreenPoint(transform.position).z;

            // converte posição do mouse pra mundo, na mesma profundidade
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, _zCoord)
            );

            _offset = transform.position - mousePoint;
      }

      void OnMouseDrag() {
            // atualiza posição do mouse no mundo mantendo a profundidade
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, _zCoord)
            );

            // limita na area definida acima
            Vector3 targetPos = mousePoint + _offset;

            targetPos.x = Mathf.Clamp(targetPos.x, 
                  InteractionLimits.minBounds.x, InteractionLimits.maxBounds.x);
            targetPos.y = Mathf.Clamp(targetPos.y, 
                  InteractionLimits.minBounds.y, InteractionLimits.maxBounds.y);

            transform.position = targetPos;
      }

      void Highlight(bool toggle) {
            // destaca o objeto clicado
            transform.localScale = toggle
                ? new Vector3(_newScale.x, _newScale.y, _newScale.z)
                : _startScale;
      }
      void OnMouseUp() {
            // remove o destaque
            Highlight(!_isDraging);
      }
}
