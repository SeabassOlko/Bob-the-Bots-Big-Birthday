using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

public class InputManager : Singleton<InputManager>
{
    [SerializeField] private ControlsScheme inputAction;
    [SerializeField] private PlayerController controller;
    //[SerializeField] private EnemyAI enemyController;

    protected override void Awake()
    {
        base.Awake();
        inputAction = new ControlsScheme();
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        inputAction.Enable();
        SubscribeToActions();
    }

    private void OnDisable()
    {
        UnsubscribeFromActions();
        inputAction.Disable();
    }

    private void SubscribeToActions()
    {
        if (controller != null)
        {
            inputAction.Controls.Move.performed += controller.MoveStarted;
            inputAction.Controls.Move.canceled += controller.MoveCanceled;
            //inputAction.Controls.Punch.performed += controller.Punch;
            //inputAction.Controls.Kick.performed += controller.Kick;
            //inputAction.Controls.Action.performed += controller.Die;
            //inputAction.Controls.Jump.performed += controller.Jump;
            //inputAction.Controls.DropWeapon.performed += controller.DropWeapon;
            //inputAction.Controls.ChangeWeapon.performed += controller.ChangeWeapon;
        }

        //if (enemyController != null)
        //{
        //    inputAction.Controls.EnemyAnim1.performed += enemyController.OnEnemyAnim1;
        //    inputAction.Controls.EnemyAnim2.performed += enemyController.OnEnemyAnim2;
        //    inputAction.Controls.EnemyAnim3.performed += enemyController.OnEnemyAnim3;
        //}
    }

    private void UnsubscribeFromActions()
    {
        if (controller != null)
        {
            inputAction.Controls.Move.performed -= controller.MoveStarted;
            inputAction.Controls.Move.canceled -= controller.MoveCanceled;
            //inputAction.Controls.Punch.performed -= controller.Punch;
            //inputAction.Controls.Kick.performed -= controller.Kick;
            //inputAction.Controls.Action.performed -= controller.Die;
            //inputAction.Controls.Jump.performed -= controller.Jump;
            //inputAction.Controls.DropWeapon.performed -= controller.DropWeapon;
            //inputAction.Controls.ChangeWeapon.performed -= controller.ChangeWeapon;
        }

        //if (enemyController != null)
        //{
        //    inputAction.Controls.EnemyAnim1.performed -= enemyController.OnEnemyAnim1;
        //    inputAction.Controls.EnemyAnim2.performed -= enemyController.OnEnemyAnim2;
        //    inputAction.Controls.EnemyAnim3.performed -= enemyController.OnEnemyAnim3;
        //}
    }

    public void UpdatePlayerController(PlayerController newController)
    {
        UnsubscribeFromActions();
        controller = newController;
        SubscribeToActions();
    }

    //public void UpdateEnemyController(EnemyAI newEnemyController)
    //{
    //    if (enemyController != null)
    //    {
    //        inputAction.Controls.EnemyAnim1.performed -= enemyController.OnEnemyAnim1;
    //        inputAction.Controls.EnemyAnim2.performed -= enemyController.OnEnemyAnim2;
    //        inputAction.Controls.EnemyAnim3.performed -= enemyController.OnEnemyAnim3;
    //    }

    //    enemyController = newEnemyController;

    //    if (enemyController != null)
    //    {
    //        inputAction.Controls.EnemyAnim1.performed += enemyController.OnEnemyAnim1;
    //        inputAction.Controls.EnemyAnim2.performed += enemyController.OnEnemyAnim2;
    //        inputAction.Controls.EnemyAnim3.performed += enemyController.OnEnemyAnim3;
    //    }
    //}
}
