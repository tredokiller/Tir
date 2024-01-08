using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float baseMoveSpeed = 5f;
    public float rotateSpeed = 2f;
    public float ascentSpeed = 5f;
    public float sprintMultiplier = 2f;
    public float accelerationTime = 1.5f; // Время для достижения максимальной скорости

    private float currentMoveSpeed;
    private Vector3 velocity; // Используется для SmoothDamp

    void Update()
    {
        // Передвижение камеры
        MoveCamera();

        // Вращение камеры
        RotateCamera();
    }

    void MoveCamera()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float sprintMultiplier = Input.GetKey(KeyCode.LeftShift) ? this.sprintMultiplier : 1f;

        Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

        // Рассчитываем новую скорость с использованием SmoothDamp
        currentMoveSpeed = Mathf.SmoothDamp(currentMoveSpeed, baseMoveSpeed * sprintMultiplier, ref velocity.y, accelerationTime);

        transform.Translate(moveDirection * currentMoveSpeed * Time.deltaTime, Space.Self);

        float ascentInput = Input.GetKey(KeyCode.Q) ? -1f : Input.GetKey(KeyCode.E) ? 1f : 0f;

        Vector3 ascentDirection = new Vector3(0f, ascentInput, 0f).normalized;
        transform.Translate(ascentDirection * ascentSpeed * Time.deltaTime, Space.World);
    }

    void RotateCamera()
    {
        if (Input.GetMouseButton(2))
        {
            float mouseX = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up, mouseX * rotateSpeed);
        }
    }
}

