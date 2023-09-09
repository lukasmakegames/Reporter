using UnityEngine;

public class Shoot : MonoBehaviour
{
    public RectTransform objective;
    public Animator animator;

    public GameObject shootSoundPrefab;
    public GameObject reloadSoundPrefab;

    private RectTransform rectTransform;
    private Canvas canvas;

    private float shootTimer;
    private float reloadTimer;
    private float reloadTime = 1.962f;
    private float shootTime = 0.06f;

    private int bullets = 5;


    public void Start()
    {
        Cursor.visible = false;
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        shootTimer = shootTime;
        reloadTimer = reloadTime;
    }


    public void Update()
    {
        shootTimer += Time.deltaTime;
        reloadTimer += Time.deltaTime;

        Vector2 movePos;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            Input.mousePosition, canvas.worldCamera,
            out movePos);

        Vector3 mousePos = canvas.transform.TransformPoint(movePos);

        //Move the Object/Panel
        rectTransform.position = mousePos;

        if (Input.GetMouseButtonDown(0) && shootTimer > shootTime && reloadTimer > reloadTime)
        {
            // Calculate the distance from the center of the target.
            Vector2 targetCenter = new Vector2(objective.position.x, objective.position.y);
            Vector2 hitPoint = new Vector2(rectTransform.position.x, rectTransform.position.y);
            float distance = Vector2.Distance(targetCenter, hitPoint);

            // Display the distance in the UI Text.
            if (distance < 1.0f)
            {
                if (distance < 0.2f)
                {
                    Debug.Log("500 points!");
                    ScoreManager.Instance.HitTarget(hitPoint, 500, Color.red);
                }
                else if (distance < 0.4f)
                {
                    Debug.Log("200 points!");
                    ScoreManager.Instance.HitTarget(hitPoint, 200, Color.yellow);

                }
                else if (distance < 0.5f)
                {
                    Debug.Log("80 points!");
                    ScoreManager.Instance.HitTarget(hitPoint, 80, Color.blue);

                }
                else
                {
                    Debug.Log("10 points!");
                    ScoreManager.Instance.HitTarget(hitPoint, 10, Color.green);

                }
            }
            animator.SetTrigger("Shoot");
            shootTimer = 0;

            Instantiate(shootSoundPrefab, hitPoint, Quaternion.identity);

            bullets--;
            if (bullets <= 0)
            {
                bullets = 5;
                reloadTimer = 0;
                Instantiate(reloadSoundPrefab, hitPoint, Quaternion.identity);
            }
        }
    }
}