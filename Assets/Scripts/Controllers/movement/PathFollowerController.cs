using UnityEngine;

public class PathFollowerController : MonoBehaviour, ISlowable {
	[SerializeField] private float speed = 0.4f;
	[SerializeField] private GameObject pathContainer;
	[SerializeField] private int repetitions = 1;

	private Transform[] _points;
	private int _pathIndex = 0;
	private float _currentSpeed;
	private IMovable _movementController;

	public bool endReached => repetitions == 0 && _pathIndex == _points.Length;

    void Awake() {
        _movementController = GetComponent<IMovable>();

		if (pathContainer)
			SetPath(pathContainer);
		
		_currentSpeed = speed;
	}

	void FixedUpdate() {
		if (_points == null || (repetitions == 0 && _pathIndex == _points.Length))
			return;

		if (repetitions > 0 && _pathIndex == _points.Length) {
			 _pathIndex = 0;
			 repetitions -= 1;
		}

		Vector3 dir = _points[_pathIndex].position - transform.position;

		_movementController.move(dir, _currentSpeed);

		if (Vector3.Distance(transform.position, _points[_pathIndex].position) <= 1f)
			_pathIndex++;
	}

	public void SetPath(GameObject _pathContainer) {
		pathContainer = _pathContainer;
		_points = new Transform[pathContainer.transform.childCount];
		for (int i = 0; i < _points.Length; i++)
			_points[i] = pathContainer.transform.GetChild(i);
	}

	public void TakeSlowEffect(float _slowFactor, float _slowDuration) {
		if(_currentSpeed == speed) {
			_currentSpeed *= _slowFactor;
			Invoke("ResetSpeed", _slowDuration);
		}
	}

	private void ResetSpeed() {
		_currentSpeed = speed;
	}

	public float Speed => speed;
}
