using UnityEngine;
using UnityEngine.AI;

public class WorkerNavigation : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    [SerializeField] private bool onTheWayToDelivery = false;
    [SerializeField] private GameObject pickingUpGood;

    [SerializeField] private bool storing = false;

    private PickGoods pickGoods;
    [SerializeField] private bool picking = false;

    [SerializeField] private GameObject goodToPick;
    [SerializeField] private bool putPickAway = false;

    [SerializeField] private SpreadGoodsParent CollectorZoneSpreadGoodsParent;
    [SerializeField] private bool busy;

    [SerializeField] private float _storingRange = 9.5f;
    private Animator _animator;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        pickGoods = GetComponent<PickGoods>();
        _animator = transform.GetChild(5).GetComponent<Animator>();
    }

    private void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            _animator.Play("Idle");
        }

        if (onTheWayToDelivery)
        {
            //_animator.Play("Idle");
            SetBusy(true);
            if (Vector3.Distance(transform.position, navMeshAgent.destination)<5)
            {
                //pickingUpGood.transform.parent = gameObject.transform;

                pickingUpGood.transform.parent = transform.GetChild(5).GetChild(0);
                
                storing = true;
                onTheWayToDelivery = false;
            }
        }
        if (storing)
        {
            //if (gameObject.transform.childCount > 8) // childcount > 8 weil das Forklift Model 8 Childs hat.
            //if(transform.GetChild(5).transform.childCount>3)
            if (transform.GetChild(5).GetChild(0).transform.childCount > 0)
            {
                //pickingUpGood.transform.position = new Vector3(0, .25f, 5.75f);
                //pickingUpGood.transform.localPosition = new Vector3(0, .25f, 5.75f);
                pickingUpGood.transform.localPosition = new Vector3(0, -2f, 2f);
                pickingUpGood.transform.localRotation = Quaternion.identity;


                //pickingUpGood.transform.LookAt(transform);          // => ändern

                //transform.GetChild(6).transform.LookAt(transform);


                pickingUpGood.GetComponent<NavMeshObstacle>().enabled = false;
                var slotNr = PalletRackParentScript.Instance.GetFreeRack().GetFirstFreeSlot();
                navMeshAgent.destination = PalletRackParentScript.Instance.GetFreeRack().gameObject.transform.GetChild(slotNr).transform.position;

                if (slotNr > 2)
                {
                    if (Vector3.Distance(transform.position, navMeshAgent.destination) < 5)
                        _animator.Play("Lift");
                }

                if (slotNr > 2)
                {
                    if (Vector3.Distance(pickingUpGood.transform.position, PalletRackParentScript.Instance.GetFreeRack().transform.GetChild(slotNr).transform.position )< _storingRange) // diese abfrage schlägt fehlt
                    {
                        pickingUpGood.GetComponent<NavMeshObstacle>().enabled = true;
                        pickingUpGood = null;
                        PalletRackParentScript.Instance.GetFreeRack().SetSlot(transform.GetChild(5).transform.GetChild(0).GetChild(0).gameObject);
                        WorkerParent.Instance.SetStoredGoods(WorkerParent.Instance.GetStoredGoods() + 1);

                        _animator.Play("LiftDown");
                        print(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
                            
                        SetBusy(false);
                        storing = false;
                    }

                } else if (Vector3.Distance(transform.position, navMeshAgent.destination) <5)
                {
                    pickingUpGood.GetComponent<NavMeshObstacle>().enabled = true;
                    pickingUpGood = null;
                    PalletRackParentScript.Instance.GetFreeRack().SetSlot(transform.GetChild(5).transform.GetChild(0).GetChild(0).gameObject);
                    WorkerParent.Instance.SetStoredGoods(WorkerParent.Instance.GetStoredGoods() + 1);

                    //_animator.Play("Idle");
                    SetBusy(false);
                    storing = false;
                }
            }
        }
        if (picking)
        {
            SetBusy(true);
            if (pickGoods.GetFirstPick() != null)
            {
                goodToPick = pickGoods.GetFirstPick();  
                pickingUpGood = goodToPick;
                // 114 und 115 in eine Zeile ?

                navMeshAgent.destination = goodToPick.transform.position;

                // Hier abfrage einbauen, ob der pickSlot > 2 ist.
                // returned wird das Gameobject. => von da aus auf den slot schließen.
                //  pickingUpGood.transform.parent.parent; => PalletRack
                var slotNr = 0;
                for (int i=0; i< pickingUpGood.transform.parent.parent.childCount; i++)
                {
                    // Hier werden alle PalletRack_Prefab childs überprüft, ob sie ElternObjekt vom Pick sind.
                    if(pickingUpGood.transform.parent.parent.GetChild(i)== pickingUpGood.transform.parent)
                    {
                        slotNr = i;
                    }
                }
                if (slotNr > 2)
                {
                    if (Vector3.Distance(transform.position, navMeshAgent.destination) < 5)
                        _animator.Play("Lift");
                }

                if (Vector3.Distance(transform.position, navMeshAgent.destination) < 3)
                {
                    transform.LookAt(goodToPick.transform); // austauschen durch transform.LookAt(Rack)?
                    goodToPick.transform.SetParent(gameObject.transform);
                    pickGoods.ClearFirstPickableSlot();
                    WorkerParent.Instance.SetStoredGoods(WorkerParent.Instance.GetStoredGoods() - 1);
                    putPickAway = true;
                    picking = false;
                }
            }
        }
        if (putPickAway)    
        {
            pickingUpGood.transform.localPosition = new Vector3(0, .25f, 5.75f);
            pickingUpGood.transform.LookAt(transform);
            pickingUpGood.GetComponent<NavMeshObstacle>().enabled = false;

            CollectorZoneSpreadGoodsParent.GetFreeZone().CalculateMaxPallets();
            var whereToPut = CollectorZoneSpreadGoodsParent.GetFreeZone().spreadingArea; // nextFree Position?
            var whereToPutVector = CollectorZoneSpreadGoodsParent.GetFreeZone().NextPosition();
            navMeshAgent.destination = whereToPutVector;

            if (Vector3.Distance(transform.position, navMeshAgent.destination) < 5)
            {
                // Hier das NavMeshObstacle aktivieren
                pickingUpGood.GetComponent<NavMeshObstacle>().enabled = true;
                pickingUpGood = null;
            
                goodToPick.transform.SetParent(whereToPut.transform.parent);
                whereToPut.parent.GetComponent<SpreadGoods>().SpreadParent();
                
                navMeshAgent.destination = Vector3.zero;
                
                goodToPick.transform.rotation = new Quaternion();
                goodToPick = null;
                SetBusy(false);
                putPickAway = false;
            }
        }
    }

    public bool GetBusy()
    {
        return busy;
    }
    public void SetBusy(bool newBool)
    {
        busy = newBool;
    }
    public SpreadGoodsParent GetCollectorZoneSpreadGoodsParent()
    {
        return CollectorZoneSpreadGoodsParent;
    }
    public void SetCollectorZoneSpreadGoodsParent(SpreadGoodsParent collectorZoneSpreadGoodsParent)
    {
        CollectorZoneSpreadGoodsParent = collectorZoneSpreadGoodsParent;
    }

    public void GetSpawnedGood()
    {   
        if (SpawnGoods.Instance.SpawnedGoods.Count>0)
        {
            SetBusy(true);
            print("Getting spawnedGood");
            // Wichtige Abfrage, warum auch immer. Kann ich nicht erklären.
            if (navMeshAgent != null)
            {
                // Peek => Pop | Alt
                // navMeshAgent.destination = SpawnGoods.Instance.SpawnedGoods.Peek().transform.position;
                // Neu
                pickingUpGood = SpawnGoods.Instance.SpawnedGoods.Peek();
                navMeshAgent.destination = SpawnGoods.Instance.SpawnedGoods.Pop().transform.position;

                onTheWayToDelivery = true;
            }
        }
    }
    
    public void PickStoredGood()
    {
        //SetBusy(true);

        // => Zugriff auf MaxPallets der IssueZone oder ScaleZone

        if (pickGoods != null)
            {
            //if (pickGoods.GetFirstObject() != null)
            if (pickGoods.GetFirstPick() != null)
            {
                    if (navMeshAgent != null)
                    {
                        picking = true;
                    }
                }
            }
    }
}
