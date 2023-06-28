using System.Collections.Generic;
using UnityEngine;

public class CommandQueue : MonoBehaviour {
    static public CommandQueue instance;

    private List<ICommand> _events = new List<ICommand>();
    private Queue<ICommand> _eventQueue = new Queue<ICommand>();

    public List<ICommand> Events => _events;
    public Queue<ICommand> EventQueue => _eventQueue;
    

    private void Awake() {
        if (instance != null) Destroy(this);
        instance = this;
    }

    private void Update() {
        foreach (var command in _events) {
            command.Execute();
        }

        foreach (var command in _eventQueue) {
            command.Execute();
        }

        _events.Clear();
        _eventQueue.Clear();
    }

    public void AddEvent(ICommand command) => _events.Add(command);
    public void AddEventToQueue(ICommand command) => _eventQueue.Enqueue(command);
}
