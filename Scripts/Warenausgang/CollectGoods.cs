using UnityEngine;

public class CollectGoods : MonoBehaviour
{
    public static CollectGoods Instance;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this; 
        }
    }

    public Transform issueGoodsHere;
    public void CollectAll()
    {
        if(issueGoodsHere.transform.childCount > 0)
        {
            for(int i=0; i< issueGoodsHere.transform.childCount; i++)
            {
                Destroy(issueGoodsHere.transform.GetChild(i).gameObject);
                ChangeScore.Instance.RiseScore();
            }
        }
        else
        {
            ChangeScore.Instance.ModifyScore(-20);
            Debug.Log("Keine Güter abgeholt, der  Lieferant war da für nichts und wir müssen jetzt Strafe bezahlen!");
        }
    }
    public void CollectAllExceptVisual() //  CollectAll musste auf das Parent Object verlegt werden, damit die Skalierung nicht verzerrt wird.
    {
        if (issueGoodsHere.transform.parent.childCount > 1)
        {
            for (int i = 1; i < issueGoodsHere.transform.parent.childCount; i++)
            {
                Destroy(issueGoodsHere.transform.parent.GetChild(i).gameObject);
                ChangeScore.Instance.RiseScore();
            }
        }
        else
        {
            ChangeScore.Instance.ModifyScore(-20);
            Debug.Log("Keine Güter abgeholt, der  Lieferant war da für nichts und wir müssen jetzt Strafe bezahlen!");
        }
    }

    public void CollectAllInAllChildren()
    {
        //print("eins");
        for(int i=0; i<issueGoodsHere.childCount; i++)
        {
            // If abfrage ob überhaupt eine der beiden inhalt hat und danach die else klausel
            //print("zwei");
            if (issueGoodsHere.transform.GetChild(i).childCount > 1)
            {
                //print("drei");
                for (int j=0; j < issueGoodsHere.transform.GetChild(i).childCount-1; j++)
                {
                    //print("vier");
                    ChangeScore.Instance.RiseScore();
                    Destroy(issueGoodsHere.transform.GetChild(i).GetChild(j+1).gameObject);
                }
            }
            else
            {
                ChangeScore.Instance.ModifyScore(-20);
                Debug.Log("Keine Güter abgeholt, der  Lieferant war da für nichts und wir müssen jetzt Strafe bezahlen!");
            }
        }
    }

}
