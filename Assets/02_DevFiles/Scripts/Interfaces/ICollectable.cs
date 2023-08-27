using UnityEngine;
public interface ICollectable
{
    Vector3 GetPose();
    void Collected();
    Collider col { get; }
}
