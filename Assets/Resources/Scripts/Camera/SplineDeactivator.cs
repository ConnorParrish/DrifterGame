using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SplineDeactivator : MonoBehaviour
{
    GameObject PlayerHUD;

    void Start()
    {
        PlayerHUD = GameObject.Find("General UI Canvas");
    }

    private void DeactivateSpline()
    {
        GameObject rootHolder = Camera.main.GetComponent<SplineController>().SplineRootHolder.gameObject;

        SplineInterpolator _sInterp = Camera.main.GetComponent<SplineInterpolator>();
        _sInterp.mState = "Once";
        _sInterp.ended = true;
        _sInterp.mCurrentIdx++;

        rootHolder.transform.GetChild(0).gameObject.SetActive(true);

        for (int i = 0; i < rootHolder.transform.GetChild(0).childCount; i++)
        {
            rootHolder.transform.GetChild(0).GetChild(i).gameObject.SetActive(true);
        }

        Player.Instance.WorldInteraction.stateBools.canMove = true;
    }

    public void DeactivatePanhandling()
    {
        Player.Instance.PanhandlingScript.enabled = false;
        DeactivateSpline();
        transform.GetChild(0).gameObject.SetActive(false);

    }

    public void DeactivateMerchant()
    {
        GameObject _merchantUI = GameObject.Find("Merchant Inventory"); // TODO this needs to change for having more merchants (buttons within each UI canvas?)
        _merchantUI.SetActive(false);
        Inventory _merchantInv = _merchantUI.transform.GetChild(0).GetComponent<Inventory>();

        if (true)// _merchantInv.buyer) //removing items if its a buyer
        {
            _merchantInv.AddMoney(-_merchantInv.Money);
            int _slotAmount = _merchantInv.items.Count - 1;

            for (int i = 0; i < _slotAmount; i++)
            {
                if (_merchantInv.items[i].Stackable)
                    _merchantInv.ChangeItemAmount(i, -_merchantInv.slots[i].transform.GetChild(0).GetComponent<ItemData>().amount);
                else
                    if (_merchantInv.slots[i].transform.childCount != 0)
                    _merchantInv.RemoveItem(i);

            }
        }

        transform.GetChild(1).gameObject.SetActive(false);

        //Time.timeScale = 1f;
        PlayerHUD.SetActive(true);

        DeactivateSpline();
    }
}
