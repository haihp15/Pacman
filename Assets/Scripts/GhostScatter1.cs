using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GhostScatter1 : GhostBehavior1
{
    private void OnDisable()
    {
        ghost.chase.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node1 node = other.GetComponent<Node1>();


        if (node != null && this.enabled && !this.ghost.frightened.enabled)
        {

            int index = Random.Range(0, node.availableDirections.Count);

            if (node.availableDirections[index] == -this.ghost.movement.direction && node.availableDirections.Count > 1)
            {
                index++;

                if (index >= node.availableDirections.Count)
                {
                    index = 0;
                }
            }

            ghost.movement.SetDirection(node.availableDirections[index]);
        }
    }

}
