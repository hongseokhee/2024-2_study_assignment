using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Rendering;

public class GameManager : MonoBehaviour
{
    UIManager MyUIManager;

    public GameObject BallPrefab;   // prefab of Ball

    // Constants for SetupBalls
    public static Vector3 StartPosition = new Vector3(0, 0, -6.35f);
    public static Quaternion StartRotation = Quaternion.Euler(0, 90, 90);
    const float BallRadius = 0.286f;
    const float RowSpacing = 0.02f;

    GameObject PlayerBall;
    GameObject CamObj;

    const float CamSpeed = 3f;

    const float MinPower = 15f;
    const float PowerCoef = 1f;

    void Awake()
    {
        // PlayerBall, CamObj, MyUIManager를 얻어온다. ok
        // ---------- TODO ---------- 
        PlayerBall = GameObject.Find("PlayerBall");
        CamObj = GameObject.Find("Main Camera");
        MyUIManager = FindObjectOfType<UIManager>();
        // -------------------- 
    }

    void Start()
    {
        SetupBalls();
    }

    // Update is called once per frame
    void Update()
    {
        // 좌클릭시 raycast하여 클릭 위치로 ShootBallTo 한다. 
        // ---------- TODO ---------- 
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                Vector3 targetposition = hit.point;
                ShootBallTo(targetposition);
            }
        }
        // -------------------- 
    }

    void LateUpdate()
    {
        CamMove();
    }

    void SetupBalls()
    {
        // 15개의 공을 삼각형 형태로 배치한다.
        // 가장 앞쪽 공의 위치는 StartPosition이며, 공의 Rotation은 StartRotation이다.
        // 각 공은 RowSpacing만큼의 간격을 가진다.
        // 각 공의 이름은 {index}이며, 아래 함수로 index에 맞는 Material을 적용시킨다.
        // Obj.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Materials/ball_1"); ok
        // ---------- TODO ---------- 
        int rowchange = 1;
        for (int i = 1; i <= 15; i++) {
            if (i == 2 || i == 4 || i == 7 || i == 11) {
                StartPosition.z -= BallRadius * 2 + RowSpacing;
                StartPosition.x = -BallRadius * rowchange;
                rowchange++;
            }
            GameObject ball = Instantiate(BallPrefab, StartPosition, StartRotation);
            ball.name = $"ball_{i}";
            ball.GetComponent<MeshRenderer>().material = Resources.Load<Material>($"Materials/ball_{i}");
            StartPosition.x += BallRadius * 2;
        }
        // -------------------- 
    }
    void CamMove()
    {
        // CamObj는 PlayerBall을 CamSpeed의 속도로 따라간다. ok
        // ---------- TODO ---------- 
        if (CamObj && PlayerBall) {
            Vector3 lerf_pos = new Vector3(PlayerBall.transform.position.x, CamObj.transform.position.y, PlayerBall.transform.position.z);
            CamObj.transform.position = Vector3.Lerp(CamObj.transform.position, lerf_pos, CamSpeed * Time.deltaTime);
        }
        // -------------------- 
    }

    float CalcPower(Vector3 displacement)
    {
        return MinPower + displacement.magnitude * PowerCoef;
    }

    void ShootBallTo(Vector3 targetPos)
    {
        // targetPos의 위치로 공을 발사한다.
        // 힘은 CalcPower 함수로 계산하고, y축 방향 힘은 0으로 한다.
        // ForceMode.Impulse를 사용한다.
        // ---------- TODO ---------- 
        Rigidbody rb = PlayerBall.GetComponent<Rigidbody>();
        Vector3 convert_pos = new Vector3(targetPos.x, 0, targetPos.z).normalized;
        rb.AddForce(CalcPower(convert_pos) * convert_pos, ForceMode.Impulse);
        // -------------------- 
    }
    
    // When ball falls
    public void Fall(string ballName)
    {
        // "{ballName} falls"을 1초간 띄운다. ok
        // ---------- TODO ---------- 
        UIManager myuimanager = MyUIManager.GetComponent<UIManager>();
        myuimanager.DisplayText($"{ballName} falls", 1);
        Debug.Log($"{ballName} falls");
        // -------------------- 
    }
}
