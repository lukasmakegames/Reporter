using UnityEngine;

public class Shoot : MonoBehaviour
{
    public RectTransform objective;

    private RectTransform rectTransform;
    private Canvas canvas;

    public void Start()
    {
        Cursor.visible = false;
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }


    public void Update()
    {
        Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition, canvas.worldCamera,
            out movePos);

        Vector3 mousePos = canvas.transform.TransformPoint(movePos);

        //Move the Object/Panel
        rectTransform.position = mousePos;

        if (Input.GetMouseButtonDown(0))
        {
            // Calculate the distance from the center of the target.
            Vector2 targetCenter = new Vector2(objective.position.x, objective.position.y);
            Vector2 hitPoint = new Vector2(rectTransform.position.x, rectTransform.position.y);
            float distance = Vector2.Distance(targetCenter, hitPoint);

            // Display the distance in the UI Text.
            if (distance<1.0f){
                if(distance<0.2f){
                    Debug.Log("500 points!");
                }else if(distance<0.4f){
                    Debug.Log("200 points!");
                }else if(distance<0.5f){
                    Debug.Log("80 points!");
                }else{
                    Debug.Log("10 points!");
                }
            }
        }
    }
}