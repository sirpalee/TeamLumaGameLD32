﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VendingMachine : MonoBehaviour {

    private uint m_numPiesLeft;
    private GameObject m_instancedUI;

    public uint InitialPieCountMin = 15;
    public uint InitialPieCountMax = 25;
    public float canBuyFromDistance = 3.0f;

    // Use this for initialization
    void Start ()
    {
        m_numPiesLeft = (uint)Random.Range((int)InitialPieCountMin, (int)InitialPieCountMax);
    }

    // Update is called once per frame
    void Update ()
    {

    }

    void OnMouseEnter()
    {
        if ((Vector3.Distance(transform.position, PlayerController.Instance.gameObject.transform.position)
         < canBuyFromDistance))
        {
            CreateText();
        }
    }

    void OnMouseExit()
    {
        DeleteText();
    }

    void OnMouseUp()
    {
        if (m_numPiesLeft > 0)
        {
            if (PlayerInventory.Instance.TryPickingUp("vending"))
                --m_numPiesLeft;
        }
    }

    void CreateText()
    {
        if (m_instancedUI == null)
        {
            m_instancedUI = (GameObject)Object.Instantiate(Resources.Load("Texts/BuyPieMessage"), transform.position, transform.rotation);
            RectTransform parentRect = m_instancedUI.GetComponent<RectTransform>();
            parentRect.localPosition = parentRect.localPosition + new Vector3(0.0f, 0.0f, -0.5f);
            m_instancedUI.transform.rotation = Quaternion.identity;
        }
    }
    
    void DeleteText()
    {
        if (m_instancedUI != null)
        {
            Object.Destroy(m_instancedUI);
        }
    }
}
