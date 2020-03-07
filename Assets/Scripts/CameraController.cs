using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    private GameObject _player;

    // Start is called before the first frame update
    void Start() {
        this._player = GameObject.Find("cat");
    }

    // Update is called once per frame
    void Update() {
        // 確認遊戲角色的 Y 座標
        Vector3 playerPos = this._player.transform.position;
        // 調整攝影機的 Y 座標
        transform.position = new Vector3(transform.position.x, playerPos.y, transform.position.z);
    }
}