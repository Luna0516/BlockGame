using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public bool isDropReady = false;

    public GameObject currentBlock = null;
    public GameObject keepBlock = null;

    public float moveSpeed = 2.0f;

    public Vector3 moveVec;

    public List<GameObject> blockList = new List<GameObject>();

    PlayerInputActions inputActions;

    public System.Action onDropBlock;

    private void Awake()
    {
        inputActions = new PlayerInputActions();

        onDropBlock += DropBlock;

        CreateNextBlock();
    }

    private void Update()
    {
        transform.position += Time.deltaTime * moveVec * moveSpeed;

        if(currentBlock != null)
        {
            currentBlock.transform.position = transform.position;
        }
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMove;
        inputActions.Player.Move.canceled += OnMove;
        inputActions.Player.Drop.performed += OnDrop;
        inputActions.Player.Manu.performed += OnManu;
    }

    private void OnDisable()
    {
        inputActions.Player.Manu.performed -= OnManu;
        inputActions.Player.Drop.performed -= OnDrop;
        inputActions.Player.Move.canceled -= OnMove;
        inputActions.Player.Move.performed -= OnMove;
        inputActions.Player.Disable();
    }

    private void CreateNextBlock()
    {
        if (currentBlock == null)
        {
            currentBlock = PoolManager.Inst.GetObject(Shape.Circle, transform.position);
            Block block = currentBlock.GetComponent<Block>();
            onDropBlock += () =>
            {
                block.Active();
            };
        }
    }

    private void DropBlock()
    {
        currentBlock = null;
        CreateNextBlock();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();

        moveVec.x = input.x;
    }

    private void OnDrop(InputAction.CallbackContext context)
    {
        if (isDropReady)
        {
            onDropBlock?.Invoke();
        }
    }

    private void OnManu(InputAction.CallbackContext context)
    {
        if(GameManager.Inst.GameState == GameState.Play)
        {
            Debug.Log("게임 메뉴창 열기");
        }
    }
}
