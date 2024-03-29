﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 使用 LoadScene 必要的引用程式

public class PlayerController : MonoBehaviour {
    private Rigidbody2D _rigid2D;
    private Animator _animator;
    private float _jumpForce = 680.0f;
    private float _walkForce = 30.0f;
    private float _maxWalkSpeed = 2.0f;
    private float _threshold = 0.2f;

    // Start is called before the first frame update
    void Start() {
        this._rigid2D = GetComponent<Rigidbody2D>();
        this._animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        // 跳躍
        if (Input.GetKeyDown(KeyCode.Space) && this._rigid2D.velocity.y == 0) {
            this._animator.SetTrigger("JumpTrigger");
            this._rigid2D.AddForce(transform.up * this._jumpForce);
        }

        if (Input.GetMouseButtonDown(0) && this._rigid2D.velocity.y == 0) {
            this._animator.SetTrigger("JumpTrigger");
            this._rigid2D.AddForce(transform.up * this._jumpForce);
        }

        // 左右移動
        int key = 0;
        if (Input.GetKey(KeyCode.RightArrow)) key = 1;
        if (Input.GetKey(KeyCode.LeftArrow)) key = -1;
        // if (Input.acceleration.x > this._threshold) key = 1;
        // if (Input.acceleration.x < this._threshold) key = 1;

        // 遊戲角色的速度
        float speedX = Mathf.Abs(this._rigid2D.velocity.x);

        // 速度限制
        if (speedX < this._maxWalkSpeed) {
            this._rigid2D.AddForce(transform.right * this._walkForce * key);
        }

        // 依照行進方向翻轉
        if (key != 0) {
            transform.localScale = new Vector3(key, 1, 1);
        }

        // 依遊戲角色的速度改變動畫的速度
        if (this._rigid2D.velocity.y == 0) {
            this._animator.speed = speedX / this._maxWalkSpeed;
        }
        // 將跳躍期間的動畫速度設為 1.0
        else {
            this._animator.speed = 1.0f;
        }

        // 跑出畫面時就回到初始畫面
        if (transform.position.y < -10) {
            SceneManager.LoadScene("GameScene");
        }
    }

    // 抵達終點
    private void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("終點");
        SceneManager.LoadScene("ClearScene");
    }
}