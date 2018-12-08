using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MapMenu : UIBasic2 {
    public Transform itemBox;
    public GridLayoutGroup grid;
    public ScrollRect scrollRect;
    public RectTransform maskRect;
    public RectTransform contentRect;
    public Transform tmpBox;//用于把item 移除出去的一个临时存放的地方

    private List<MonsterPorItem> mineStrongholdList;
    private List<MonsterPorItem> otherStrongholdList;


    public List<MapUIItem_icon_lvBoard_Name> bussniessItem;
    public List<MapUIItem_icon_lvBoard_Name> playerItem;

    public override void InitValue()
    {
        base.InitValue();
    }

    public override void OnDispawn()
    {
        int count = bussniessItem.Count;
        for(int i = 0 ; i < count; i ++ )
        {
            AndaDataManager.Instance.RecieveItem(bussniessItem[i]);
        }
        bussniessItem.Clear();
        count = playerItem.Count;
        for(int i = 0; i < count; i++)
        {
            AndaDataManager.Instance.RecieveItem(playerItem[i]);
        }
        playerItem.Clear();

        base.OnDispawn();
    }

    public void BuildMapItem(List<PlayerStrongholdAttribute> players, List<BusinessStrongholdAttribute> bussiness)
    {
        //构建玩家据点
        int count = players.Count;
        for(int i = 0 ; i <count; i ++)
        {
            PlayerStrongholdAttribute p = players[i];
            MapUIItem_icon_lvBoard_Name _item = AndaDataManager.Instance.InstantiateMenu<MapUIItem_icon_lvBoard_Name>(ONAME.MapUIItemShboard_PlayerSH);
            _item.transform.SetInto(itemBox);
             Sprite imgPor = AndaDataManager.Instance.GetStrongholdPorSprite(p.statueID.ToString());
            Sprite levelBoard = AndaDataManager.Instance.GetStrongholdLevelBoardSprite(p.strongholdLevel);
            _item.SetInfo(p.strongholdIndex,imgPor,levelBoard,p.strongholdNickName);
            if(playerItem == null) playerItem =new List<MapUIItem_icon_lvBoard_Name>();
            playerItem.Add(_item);
        }

        count = bussiness.Count;
        for(int i = 0 ; i < count; i++)
        {
            BusinessStrongholdAttribute b = bussiness[i];
            MapUIItem_icon_lvBoard_Name _item = AndaDataManager.Instance.InstantiateMenu<MapUIItem_icon_lvBoard_Name>(ONAME.MapUIItemShboard_PlayerSH);
            _item.transform.SetInto(itemBox);
            Sprite levelBoard = AndaDataManager.Instance.objdataManager.GetBussinessStrongholdLevelSprite(b.strongholdLevel);
            _item.SetInfo(b.strongholdIndex,null, levelBoard,b.strongholdNickName);
            if(bussniessItem == null)bussniessItem = new List<MapUIItem_icon_lvBoard_Name>();
            bussniessItem.Add(_item);
        }
    }

   

    public void BuildMapItemForMineAndAnotherStronghold(List<PlayerStrongholdAttribute> otherList, List<PlayerStrongholdAttribute> mineList)
    {
        int count = otherList.Count + mineList.Count;
        for(int i = 0 ; i < count ; i ++)
        {
            PlayerStrongholdAttribute _psa = new PlayerStrongholdAttribute();
            if (i < otherList.Count)
            {
                _psa = otherList[i];
            }
            else
            {
                _psa = mineList[i];
            }
           


            PlayerStrongholdConfigAttribute strongholdBaseAttribution = MonsterGameData.GetStrongBasedAttribute();

            int limit = strongholdBaseAttribution.playerStrongholdGrowUpExp[_psa.strongholdLevel];//(_psa.strongholdLevel);//(pma.monsterMaxPower , monsterBaseConfig);

            float per = (float)_psa.strongholdGloryValue / limit;

            MonsterPorItem monsterPor = AndaDataManager.Instance.InstantiateMenu<MonsterPorItem>("ShporMonsterPorItem");

            monsterPor.transform.SetUIInto(transform);

            Color color = AndaGameExtension.GetLevelColor(_psa.strongholdLevel);

            monsterPor.SetStrongholdInfo(_psa.medalLevel, _psa.statueID, per, color);
            //map camera world to screen
            Vector2 screenPose = ARMonsterSceneDataManager.Instance.MapCamera.WorldToScreenPoint(_psa.strongholdInMapPosition);
            //screen to world
            Vector3 p = ARMonsterSceneDataManager.Instance.UICamera.ScreenToWorldPoint(screenPose);

            monsterPor.transform.position = p;

        }

       

    }



    public void SetToGrid(Transform rect)
    {
        rect.SetUIInto(grid.transform);
    }

    /// <summary>
    /// Sets to out.
    /// </summary>
    public void SetToOutGrid(Transform rect)
    {
        rect.gameObject.SetTargetActiveOnce(false);
        rect.SetUIInto(tmpBox);
    }
     

    public void ShowItem(RectTransform rect)
    {
        //获取一点Item脚本，然后加载
        rect.GetComponent<StrongholdInformation_MapItem>().ShowInfo();
        //居中
        CenterOnItem(rect,scrollRect,maskRect,contentRect);
    }

   
}
