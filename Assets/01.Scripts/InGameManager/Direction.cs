﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction : MonoBehaviour
{
    public InGameManager InGameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("node"))
        {
            collision.gameObject.SetActive(false);
            InGameManager.dangerNum++;
        }
    }
}