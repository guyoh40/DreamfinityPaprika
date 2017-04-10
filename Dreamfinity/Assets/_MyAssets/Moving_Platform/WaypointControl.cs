using UnityEngine;
using System.Collections;
<<<<<<< HEAD
=======
using UnityEditor;

[InitializeOnLoad]
[ExecuteInEditMode]
>>>>>>> f86e85029d666cfb1c0332a0196ff61731d06bb1

public class WaypointControl : MonoBehaviour {

    [Header("Waypoint Options")]
    public bool StopPoint = false;
    public float StopTime;


    SphereCollider colTrigger;
    LineRenderer m_lineRenderer;
    GameObject m_parentObject;
    GameObject m_nextPoint;


    [Header("Way Point Info")]

    public int m_chiledIndex;
    public int m_childCount;
    public bool m_toggle = true;
    public bool m_drawPoints;



    /*For initializing in the editor*/
    bool m_editorInit = true; 

    void EditorInit()
    {
        m_parentObject = transform.parent.gameObject;
        m_lineRenderer = GetComponent<LineRenderer>();
        colTrigger = GetComponent<SphereCollider>();

<<<<<<< HEAD
=======
        //allows this to run in the scene view instead of the game view
        runInEditMode = true;

>>>>>>> f86e85029d666cfb1c0332a0196ff61731d06bb1
        m_editorInit = true;
    }

    private void Start()
    {
        m_parentObject = transform.parent.gameObject;
        m_lineRenderer = GetComponent<LineRenderer>();

<<<<<<< HEAD
        
        m_toggle = false;
    }


    

=======
        //allows this to run in the scene view instead of the game view
        runInEditMode = true;

        m_toggle = true;
    }
    


>>>>>>> f86e85029d666cfb1c0332a0196ff61731d06bb1
    private void Update()
    {
        if(!m_editorInit)
        {
            EditorInit();
        }

<<<<<<< HEAD
        if(Application.isPlaying)
        {
            m_toggle = false;
        }
        else
        {
            m_toggle = true;
        }
        
=======
        m_toggle = true;
>>>>>>> f86e85029d666cfb1c0332a0196ff61731d06bb1

        m_childCount = m_parentObject.transform.childCount;

        //index number this object is within the parent object
        m_chiledIndex = transform.GetSiblingIndex();

        // finding the next chiled in the parent object
        if(m_chiledIndex == m_parentObject.transform.childCount -1)
        {
            m_nextPoint = transform.parent.transform.GetChild(0).gameObject;
        }
        else if(m_chiledIndex < m_parentObject.transform.childCount -1)
        { 
            m_nextPoint = m_parentObject.transform.GetChild(m_chiledIndex + 1).gameObject;
        }
        

        //By default there should only need to be 2 points
        m_lineRenderer.positionCount = 2;

        //setting the points for the line renderer
        m_lineRenderer.SetPosition(0, this.transform.position);
        m_lineRenderer.SetPosition(1, m_nextPoint.transform.position);

        RenderToggle(m_toggle);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.transform.tag == "Enemy" && other.gameObject.GetComponent<EnemyControl>().m_waypointParentObj == this.transform.parent)
        {
            Debug.Log("PathComplete");
        }
       
    }

    public void RenderToggle(bool toggle)
    {
        GetComponent<MeshRenderer>().enabled = toggle;
        m_lineRenderer.enabled = toggle;
       
    }
}
