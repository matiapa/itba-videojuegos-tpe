using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BuildingSystem : MonoBehaviour
{
    public GameObject[] entitiesPreviews;
    public GameObject[] entities;
    private int index;
    private GameObject pendingEntity;
    private RaycastHit hit;
    private Vector3 actualPos;
    private AudioSource _audioSource;

    [SerializeField] private AudioClip _notEnoughCoinsClip;

    static public BuildingSystem instance;

    public int[] EntitiesCost => entities.Select(
        entity => entity.GetComponent<BuildController>().Cost
    ).ToArray();

    private void Awake() {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
    private void Start() {
        _audioSource = GetComponent<AudioSource>();
        actualPos = Vector3.zero + new Vector3(0,-10,0);
    }

    private void FixedUpdate() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 2000)) {
            IBuildHolder buildHolder = hit.transform.gameObject.GetComponent<IBuildHolder>();
            GameObject objectToBuild = entities[index];

            if (buildHolder != null && buildHolder.Allows(objectToBuild))
                actualPos = hit.transform.gameObject.transform.position;
            else
                actualPos = Vector3.zero + new Vector3(0,-10,0);
        }
 
    }

    private void Update() {
        if (pendingEntity != null)
            pendingEntity.transform.position = actualPos;
            
        if (Input.GetButtonUp("Click"))
            OnMouseUp();

        if (Input.GetButtonUp("Basic Tower"))
            SelectEntity(0);

        if (Input.GetButtonUp("Ice Tower"))
            SelectEntity(1);

        if (Input.GetButtonUp("Poison Tower"))
            SelectEntity(2);

        if (Input.GetButtonUp("Missile Tower"))
            SelectEntity(3);

        if (Input.GetButtonUp("Bomb"))
            SelectEntity(4);
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

        int cost = entities[index].gameObject.GetComponent<BuildController>().Cost;
        if (GameManager.instance.Coins - cost >= 0) {
            Destroy(pendingEntity);
            CmdBuild cmdBuild = new CmdBuild(buildHolder, entities[index]);
            CommandQueue.instance.AddEventToQueue(cmdBuild);
            pendingEntity = null;
        }
        else {
            _audioSource.PlayOneShot(_notEnoughCoinsClip);
        }
    }

    private void SelectEntity(int index) {
        this.index = index;

        if(pendingEntity != null)
            Destroy(pendingEntity);

        pendingEntity = Instantiate(entitiesPreviews[index], actualPos, transform.rotation);
    }
    
}
