using UnityEngine;

public class ArrowPointer : MonoBehaviour
{
    public Vector3 targetPos;

    private Vector3 targetPositionScreenPoint;
    private bool isOffScreen;
    private Vector3 cappedTargetScreenPosition;
    private float borderSizeX;
    private float borderSizeY;

    private Camera _camera;
    
    [SerializeField] private Transform arrowTransform;

    [SerializeField] private SpriteRenderer spriteRenderer;



    private void Start()
    {
        _camera = GameController.instance.character.GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        borderSizeX = 150f;
        borderSizeY = 100f;
        targetPositionScreenPoint = _camera.WorldToScreenPoint(targetPos);

        isOffScreen = targetPositionScreenPoint.x <= -50 || targetPositionScreenPoint.x >= Screen.width + 50 || targetPositionScreenPoint.y <= -25 || targetPositionScreenPoint.y >= Screen.height + 25;

        if(isOffScreen )
        {
            spriteRenderer.enabled = true;
            cappedTargetScreenPosition = targetPositionScreenPoint;
            if(cappedTargetScreenPosition.x <= borderSizeX)
                cappedTargetScreenPosition.x = borderSizeX;

            if (cappedTargetScreenPosition.x >= Screen.width - borderSizeX)
                cappedTargetScreenPosition.x = Screen.width - borderSizeX;

            if (cappedTargetScreenPosition.y <= borderSizeY)
                cappedTargetScreenPosition.y = borderSizeY;

            if(cappedTargetScreenPosition.y >= Screen.height - borderSizeY)
                cappedTargetScreenPosition.y = Screen.height - borderSizeY;

            arrowTransform.position = _camera.ScreenToWorldPoint(cappedTargetScreenPosition);
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }
}
