using UnityEngine.InputSystem;
using UnityEngine;

public class CinemachineSwitcher : MonoBehaviour
{
    [SerializeField]
    private InputAction action;
    private Animator animator;

    private void Awake(){
        animator = GetComponent<Animator>();
    }
    private void Enable(){
        action.Enable();
    }
    private void OnDisable(){
        action.Disable();
    }
    private void SwitchState(){
        
    }

    // Start is called before the first frame update
    void Start()
    {
        action.performed += _ => SwitchState();
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
