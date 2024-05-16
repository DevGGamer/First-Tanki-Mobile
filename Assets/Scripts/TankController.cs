using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float RotateSpeed;
    [SerializeField] private float TurretSpeed = 5f;
    [SerializeField] private Transform turret;

    private CharacterController cc;
    private Animator anim;

    private bool isMobile;
    [SerializeField] private Joystick joyMove;
    [SerializeField] private Joystick joyRotate;
    [SerializeField] private GameObject ShootButton;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        #if UNITY_STANDALONE
            isMobile = false;
        #endif

        #if UNITY_ANDROID || UNITY_IOS
            isMobile = true;
        #endif

        if (!isMobile)
        {
            joyMove.gameObject.SetActive(false);
            joyRotate.gameObject.SetActive(false);
            ShootButton.SetActive(false);
        }
    }

    private void Update()
    {
        float moveZ = isMobile ? joyMove.Vertical : Input.GetAxis("Vertical");
        float moveX = isMobile ? joyMove.Horizontal : Input.GetAxis("Horizontal");
        float RotZ = isMobile ? joyRotate.Horizontal : Input.GetAxis("Mouse X");



        transform.Rotate(0, RotateSpeed * moveX * Time.deltaTime, 0);
        turret.Rotate(0,0, TurretSpeed * RotZ * Time.deltaTime);
        cc.SimpleMove(transform.forward*MoveSpeed* moveZ);
        
        anim.SetFloat("MoveX", moveX);
        anim.SetFloat("MoveY", moveZ);
        
        if (Input.GetMouseButtonDown(0) && !isMobile)
            GetComponent<Shooting>().Shoot();
    }
}
