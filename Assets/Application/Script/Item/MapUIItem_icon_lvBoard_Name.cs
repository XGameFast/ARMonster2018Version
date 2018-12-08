using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapUIItem_icon_lvBoard_Name : AndaObjectBasic {

    public Image porImg;
    public Image levelboard;
    public Text shName;

    public int dataIndex;
    private System.Action<int> clickCallBack;
    public void SetInfo(int _index,Sprite icon,Sprite _lvBoard,string _shName)
    {
        dataIndex =_index;
        UpdatePorImage(icon);
        levelboard.sprite = _lvBoard;
        shName.text = _shName;
    }

    public void RegisterClickCallBack(System.Action<int> callback)
    {
        clickCallBack = callback;
    }

    public void UpdatePorImage(Sprite sp)
    {
        porImg.sprite = sp;
    }

    public void ClickItem()
    {
        if(clickCallBack!=null)
        {
            clickCallBack(dataIndex);
        }
    }
}
