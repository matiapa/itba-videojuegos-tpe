using UnityEngine;
public class BuildController : MonoBehaviour, IBuildable
{
    [SerializeField] private int _cost;
    public int Cost => _cost;
    public void Build(Transform transform)
    {
        Instantiate(this.gameObject, transform.position, transform.rotation);
        EventManager.instance.CoinChange(-Cost);
    }
}
