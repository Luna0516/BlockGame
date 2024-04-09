using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    bool active = false;

    public bool isDropReady = false;

    private bool isPause = false;
    public bool IsPause
    {
        get => isPause;
        set
        {
            if(isPause != value)
            {
                isPause = value;

                InputActive(!isPause);
            }
        }
    }

    private int score;
    public int Score
    {
        get => score;
        set
        {
            if(score != value)
            {
                score = value;
                
                if (onChangeScore != null)
                {
                    onChangeScore.Invoke(score);
                }
            }
        }
    }
 
    public float moveSpeed = 2.0f;
    float moveRange = 2.9f;

    float minX;
    float maxX;

    public Block currentBlock = null;
    public Block nextBlock = null;

    public Vector3 moveVec;

    PlayerInputActions inputActions;

    public System.Action onDropBlock;
    public System.Action<Block> onSetNextBlock;
    public System.Action<int> onChangeScore;

    private void Awake()
    {
        inputActions = new PlayerInputActions();

        if (GameManager.Inst.GameState == GameState.Play)
        {
            StartCoroutine(GameReady());
        }

        GameManager.Inst.onGameOver = null;
        GameManager.Inst.onGameOver += GameOver;
    }

    private void Start()
    {
        minX = transform.position.x - moveRange;
        maxX = transform.position.x + moveRange;

        if(onSetNextBlock != null)
        {
            onSetNextBlock.Invoke(nextBlock);
        }
    }

    private void Update()
    {
        transform.position += Time.deltaTime * moveVec * moveSpeed;

        if(currentBlock != null)
        {
            currentBlock.transform.position = transform.position;
        }

        if (transform.position.x < minX)
        {
            transform.position = new Vector3(maxX, transform.position.y);
        }
        else if (transform.position.x > maxX)
        {
            transform.position = new Vector3(minX, transform.position.y);
        }
    }

    private void OnEnable()
    {
        InputActive(true);
    }

    private void OnDisable()
    {
        InputActive(false);

        if (currentBlock != null)
            currentBlock.gameObject.SetActive(false);
        if (nextBlock != null)
            nextBlock.gameObject.SetActive(false);
        
        currentBlock = null;
        nextBlock = null;

        active = false;
        IsPause = false;
        isDropReady = false;

        onDropBlock = null;
        onSetNextBlock = null;
        onChangeScore = null;

        score = 0;
    }

    private Block CreateNextBlock()
    {
        Block block = null;

        int value = Random.Range(0, 3);

        switch (value)
        {
            case 0:
                block = PoolManager.Inst.GetBlock(Shape.Circle, transform.position);
                break;
            case 1:
                block = PoolManager.Inst.GetBlock(Shape.Square, transform.position);
                break;
            case 2:
                block = PoolManager.Inst.GetBlock(Shape.Triangle, transform.position);
                break;
        }

        return block;
    }

    private void GameOver()
    {
        InputActive(false);

        DataManager.Inst.RankUpdate(score);
        DataManager.Inst.SaveData();

        DataManager.Inst.onDataSet.Invoke();
    }

    void InputActive(bool _active)
    {
        if(active == _active) {  return; }

        active = _active;

        if (active)
        {
            inputActions.Player.Enable();
            inputActions.Player.Move.performed += OnMove;
            inputActions.Player.Move.canceled += OnMove;
            inputActions.Player.Drop.performed += OnDrop;
            inputActions.Player.Manu.performed += OnManu;
        }
        else
        {
            inputActions.Player.Manu.performed -= OnManu;
            inputActions.Player.Drop.performed -= OnDrop;
            inputActions.Player.Move.canceled -= OnMove;
            inputActions.Player.Move.performed -= OnMove;
            inputActions.Player.Disable();
        }
    }

    private void SetNextBlock()
    {
        currentBlock = nextBlock;
        currentBlock.gameObject.SetActive(true);

        nextBlock = CreateNextBlock();
        onSetNextBlock.Invoke(nextBlock);
    }

    IEnumerator GameReady()
    {
        currentBlock = CreateNextBlock();

        nextBlock = CreateNextBlock();

        yield return new WaitForSeconds(1.0f);

        currentBlock.gameObject.SetActive(true);
        isDropReady = true;
    }

    IEnumerator Drop()
    {
        currentBlock.Active();
        currentBlock = null;

        yield return new WaitForSeconds(0.5f);

        SetNextBlock();

        isDropReady = true;
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
            isDropReady = false;
            StartCoroutine(Drop());
        }
    }

    private void OnManu(InputAction.CallbackContext context)
    {
        if(GameManager.Inst.GameState == GameState.Play)
        {
            IsPause = !IsPause;
            GameManager.Inst.onGamePause.Invoke(IsPause);
        }
    }
}
