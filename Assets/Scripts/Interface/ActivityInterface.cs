using UnityEngine;

public interface ActivityInterface
{
    KeyCode key { get; }
    string actionText { get; }
    void OnActivity();
}
