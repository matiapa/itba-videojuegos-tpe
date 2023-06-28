using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BuildingSystem : MonoBehaviour
{
    public GameObject[] turretsPreviews;
    public GameObject[] turrets;
    private int index;
    private GameObject pendingTurret;
    private RaycastHit hit;
    private Vector3 actualPos;
    private AudioSource _audioSource;

    [SerializeField] private AudioClip _notEnoughCoinsClip;

    static public BuildingSystem instance;

    public int[] TurretsCost => turrets.Select(
        turretObject => turretObject.GetComponent<Turret>().GetComponent<BuildController>().Cost).ToArray();

    private void Awake() {
        if (instance != null) Destroy(this);
        instance = this;
    }
    private void Start() {
        _audioSource = GetComponent<AudioSource>();
        actualPos = Vector3.zero + new Vector3(0,-10,0);
    }

    private void FixedUpdate() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            IBuildHolder buildHolder = hit.transform.gameObject.GetComponent<IBuildHolder>();
            if (buildHolder != null)
            {
                actualPos = hit.transform.gameObject.transform.position;
            }
            else
            {
                actualPos = Vector3.zero + new Vector3(0,-10,0);
            }
        }
 
    }

    private void Update() {
        if (pendingTurret != null)
            pendingTurret.transform.position = actualPos;

        if (Input.GetButtonUp("Click"))
            OnMouseUp();

        if (Input.GetButtonUp("Tower 1"))
            SelectTurret(0);

        if (Input.GetButtonUp("Tower 2"))
            SelectTurret(1);

        if (Input.GetButtonUp("Tower 3"))
            SelectTurret(2);
    }

    private void OnMouseUp() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out hit, 1000))
            return;

        IBuildHolder buildHolder = hit.transform.gameObject.GetComponent<IBuildHolder>();
        if (buildHolder == null)
            return;
        
        float distanceThreshold = 2.0f; // Umbral de distancia
        if (Vector3.Distance(hit.transform.gameObject.transform.position, actualPos) > distanceThreshold)
            return;

        int cost = turrets[index].gameObject.GetComponent<BuildController>().Cost;
        if (GameManager.instance.Coins - cost >= 0) {
            Destroy(pendingTurret);
            CmdBuild cmdBuild = new CmdBuild(buildHolder, turrets[index]);
            CommandQueue.instance.AddEventToQueue(cmdBuild);
            pendingTurret = null;
        }
        else {
            _audioSource.PlayOneShot(_notEnoughCoinsClip);
        }
    }

    private void SelectTurret(int index) {
        this.index = index;
        if(pendingTurret != null)
            Destroy(pendingTurret);
        pendingTurret = Instantiate(turretsPreviews[index], actualPos, transform.rotation);
    }
    
}
