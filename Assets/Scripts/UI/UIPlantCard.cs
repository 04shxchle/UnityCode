using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unity.VisualScripting;


/// <summary>
/// ��Ƭ������״̬
/// </summary>
public enum CardState
    {
    //��������CD
    CanPlace,
    //��������CD
    NotCD,
    //û��������CD
    NotSun,
    //��û��
    NotAll
}

public class UIPlantCard : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler
{
    //����ͼƬ��img���
    private Image maskImg;

    //�����imag���
    private Image image;

    //��Ҫ����������Text
    private Text wantSunText;

    //��Ҫ����������ֲ
    public int WantSunNum;

    //��ȴʱ�䣺������Է���һ��ֲ��
    public float CDTime;

    //��ǰʱ�䣺������ȴʱ��ļ���
    private float currTimeForCd;

    //�Ƿ�1���Է���ֲ���CD
    private bool canPlace;

    //�Ƿ���Ҫ����
    private bool wantPlace;

    //����������ֲ��
    private PlantBase plant;

    //�������е�ֲ��,����͸����
    private PlantBase plantInGrid;

    //��ǰ��Ƭ����Ӧ��ֲ������
    public PlantType CardPlantType;

    private CardState cardState=CardState.NotAll;

    public CardState CardState { get => cardState; set
        {
            //���Ҫ�޸ĵ�ֵ�͵�ǰһ����������������Ҫ�����κ��߼�
            if (cardState == value)
            {
                return;
            }
          
            switch(value)
            {
                case CardState.CanPlace:
                    //CDû�����֣�������������
                    maskImg.fillAmount= 0;
                    image.color=Color.white;
                    break;
                case CardState.NotCD:
                    //CD�����֣�������������
                    image.color = Color.white;
                    if (cardState == CardState.NotAll) return;
                    CDEnter();
                    break;
                case CardState.NotSun:
                    //CDû�����֣������ǻ谵��
                    maskImg.fillAmount = 0;
                    image.color = new Color(0.75f,0.75f,0.75f);
                    break;
                case CardState.NotAll:
                    image.color = new Color(0.75f, 0.75f, 0.75f);
                    if (cardState == CardState.NotCD) return;
                    CDEnter();
                    break;
            }
            cardState = value;
        }
    }
    public bool CanPlace { get => canPlace;
        set
        {
            canPlace = value;
            CheckState();
        }
    
    }

    public bool WantPlace { get => wantPlace;
        set
        {
            
                wantPlace = value;
                if (WantPlace)
                {
                    GameObject prefab = PlantManager.Instance.GetPlantForType(CardPlantType);
                    plant = GameObject.Instantiate<GameObject>(prefab, Vector3.zero, Quaternion.identity, PlantManager.Instance.transform).GetComponent<PlantBase>();
                    plant.InitForCreate(false);
                }
                else
                {
                    if (plant != null)
                    {
                        Destroy(plant.gameObject);
                        plant = null;
                    }

                }

            
        }
    }



    void Start()
    {
        if (transform.Find("Mask") != null)
            maskImg = transform.Find("Mask").GetComponent<Image>();
        if (transform.Find("Text") != null)
        {
            wantSunText = transform.Find("Text").GetComponent<Text>();
            wantSunText.text = WantSunNum.ToString();
        }
        image = GetComponent<Image>();
        CanPlace = true;
        PlayerManager.Instance.AddSunNumUpdateActionListener(CheckState);
       
    }
    private void Update()
    {
        //���ȥ��Ҫ����ֲ�����Ҫ���õ�ֲ�ﲻΪ��
        if(WantPlace&&plant!=null)
        {
            //��ֲ��������
            Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Grid grid = GridManager.Instance.GetGridByWorldPos(mousePoint);

            plant.transform.position = new Vector3(mousePoint.x, mousePoint.y,0);
            
            //����Ҿ�������ȽϽ���������û��ֲ���Ҫ�������ϳ���һ��͸����ֲ��
            if(grid.HavePlant==false&&Vector2.Distance(mousePoint,grid.Position)<1.5)
            {
                if(plantInGrid==null)
                {
                    plantInGrid = GameObject.Instantiate<GameObject>(plant.gameObject, grid.Position, Quaternion.identity, PlantManager.Instance.transform).GetComponent<PlantBase>();
                    plantInGrid.InitForCreate(true);

                }
                else
                {
                    plantInGrid.transform.position= grid.Position;
                }

                //��������꣬����ֲ��
                if (Input.GetMouseButtonDown(0))
                {
                    plant.InitForPlace(grid);
                    plant = null;
                    Destroy(plantInGrid.gameObject);
                    plantInGrid = null;
                    WantPlace = false;
                    CanPlace = false;

                    //��ֲ�ɹ���Ҫ�����������
                    PlayerManager.Instance.SunNum -= WantSunNum;
                }
            }
            else
            {
                if(plantInGrid!=null)
                {
                    Destroy(plantInGrid.gameObject);
                    plantInGrid = null;
                }
               
            }
            //����Ҽ���ȡ������
            if(Input.GetMouseButtonDown(1))
            {
                if(plant!=null) Destroy(plant.gameObject);
                if (plant != null)
                    Destroy(plantInGrid.gameObject);
                plant = null;
                plantInGrid = null;
                WantPlace = false;
            }
        }
    }


    /// <summary>
    /// ״̬���
    /// </summary>
    private void CheckState()
    {
        //��������CD
        if (canPlace && PlayerManager.Instance.SunNum >= WantSunNum)
        {
            CardState = CardState.CanPlace;
        }
        //û��CD������
        else if (!canPlace && PlayerManager.Instance.SunNum >= WantSunNum)
        {
            CardState = CardState.NotCD;
        }
        //��CDû������
        else if (canPlace && PlayerManager.Instance.SunNum < WantSunNum)
        {
            CardState = CardState.NotSun;
        }
        //��û��
        else
        {
            CardState = CardState.NotAll;
        }
        //Debug.Log("Planting pea shooter CD");
    }


    /// <summary>
    /// ����CD
    /// </summary>
    private void CDEnter()
    {
        maskImg.fillAmount = 1;
        //���ֺ󣬿�ʼ������ȴ
        StartCoroutine(CalCD());
    }
    /// <summary>
    /// ������ȴʱ��
    /// </summary>
    IEnumerator CalCD()
    {
        float calCD = (1 / CDTime) * 0.1f;
        currTimeForCd = CDTime;
        while (currTimeForCd >= 0)
        {
            yield return new WaitForSeconds(0.1f);
            maskImg.fillAmount -= calCD;
            currTimeForCd -= 0.1f;
        }

        //���Է�����

        CanPlace = true;

    }
    //�������
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!CanPlace) return;
        transform.localScale = new Vector2(1.05f, 1.05f);
        //Debug.Log("Planting pea shooter����");
    }

    //����Ƴ�

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!CanPlace) return;
        transform.localScale = new Vector2(1f, 1f);
    }
    

    // �����ʱ��Ч��������ֲ��
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!CanPlace) return;
        if (!WantPlace)
        {
            WantPlace = true;
        }
        // �����־
       // Debug.Log("Planting pea shooter");
    }
}
