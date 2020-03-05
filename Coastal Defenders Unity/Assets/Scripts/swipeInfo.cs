using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class swipeInfo : MonoBehaviour, IDragHandler, IEndDragHandler
{
    //public GameObject[] resources = new GameObject[5];
    private Vector3 panelLocation;
    public float panelDist;
    public float percentThreshold = .2f;
    public float easing = .5f;
    // Start is called before the first frame update
    private void Start()
    {
        //resources[1].transform.localPosition = new Vector3(resources[0].transform.localPosition.x + panelDist, resources[1].transform.localPosition.y);
        //resources[2].transform.localPosition = new Vector3(resources[1].transform.localPosition.x + panelDist, resources[2].transform.localPosition.y);
        //resources[3].transform.localPosition = new Vector3(resources[2].transform.localPosition.x + panelDist, resources[3].transform.localPosition.y);
        //resources[4].transform.localPosition = new Vector3(resources[3].transform.localPosition.x + panelDist, resources[4].transform.localPosition.y);
        panelLocation = transform.localPosition;
    }

    // Update is called once per frame
    private void Update()
    {
    }

   public void OnDrag(PointerEventData data)
   {
        Debug.Log(data.pressPosition.x - data.position.x);
        float difference = data.pressPosition.x - data.position.x;
        transform.localPosition = panelLocation - new Vector3(difference * 5f, 0);
   }
    
    public void OnEndDrag(PointerEventData data)
    {
        float percentage = (data.pressPosition.x - data.position.x) / Screen.height;
        if(Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector3 newLocation = panelLocation;
            if(percentage > 0)
            {
                newLocation += new Vector3(-575, 0, 0);
            }else if(percentage < 0)
            {
                newLocation += new Vector3(575, 0, 0);
            }
            StartCoroutine(SmoothMove(transform.localPosition, newLocation, easing));
            transform.localPosition = newLocation;
            panelLocation = newLocation;
        }
        else
        {
            StartCoroutine(SmoothMove(transform.localPosition, panelLocation, easing));
        }
        //panelLocation = transform.localPosition;
    }

    IEnumerator SmoothMove(Vector3 startpos, Vector3 endPos, float seconds)
    {
        float t = 0f;
        while(t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.localPosition = Vector3.Lerp(startpos, endPos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }
}