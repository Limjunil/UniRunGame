using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingPlatformController : ScrollingObjController
{
    private bool isStart = false;
    
    public override void Start()
    {
        base.Start();

        isStart = true;

    }   // Start()

    public override void Update()
    {
        base.Update();
    }   // Update()

    protected override void InitObjsPosition()
    {
        base.InitObjsPosition();

        // { 생성한 오브젝트의 위치를 설정한다.
        Vector2 posOffset = Vector2.zero;

        float xPos =
            objPrefabSize.x * (scrollingObjCount - 1) * (-1) * 0.5f;

        float yPos = prefabYPos;

        for (int i = 0; i < scrollingObjCount; i++)
        {

            scrollingPool[i].SetLocalPos(xPos, yPos, 0f);

            // 랜덤한 오프셋을 받아와서 x, y 포지션에 더하는 로직
            posOffset = GetRandomPosOffset();

            if(isStart == true && i == 1)
            {
                xPos = xPos + objPrefabSize.x;
                isStart = false;
            }
            else
            {
                xPos = xPos + objPrefabSize.x + posOffset.x;
            }
            yPos = prefabYPos + posOffset.y;


        }   // loop : 생성한 오브젝트를 가로로 왼쪽부터 차례대로 정렬하는 루프

        // } 생성한 오브젝트의 위치를 설정한다.
    }   // InitObjsPosition()

    protected override void RepositionFirstObj()
    {
        base.RepositionFirstObj();

        // 스크롤링 풀의 첫번째 오브젝트를 마지막으로 리포지셔닝하는 로직
        float lastScrObjCurrentXPos =
            scrollingPool[scrollingObjCount - 1].transform.localPosition.x;

        if (lastScrObjCurrentXPos <= objPrefabSize.x * 0.5f)
        {
            Vector2 posOffset = Vector2.zero;
            posOffset = GetRandomPosOffset();

            float lastScrObjInitXPos =
                Mathf.Floor(scrollingObjCount * 0.5f) * objPrefabSize.x
                + (objPrefabSize.x * 0.5f);

            scrollingPool[0].SetLocalPos(
                lastScrObjInitXPos + posOffset.x, prefabYPos + posOffset.y, 0f);
            scrollingPool.Add(scrollingPool[0]);
            scrollingPool.RemoveAt(0);

        }   // if : 스크롤링 오브젝트의 마지막 오브젝트가 화면 상의 절반 정도 Draw 되는 때
    }   // RepositionFirstObj()


    //! 랜덤한 포지션 오프셋을 리턴하는 함수
    private Vector2 GetRandomPosOffset()
    {
        Vector2 offset = Vector2.zero;
        offset.x = Random.Range(50f, 300f);

        offset.y = Random.Range(-80f, 30f);

        return offset;
    }   // GetRandomPosOffset()
}
