using UnityEngine;

namespace My.Game
{
    public class TestInitializer : MonoBehaviour
    {
        private void Awake()
        {
            if (FindObjectOfType<World>() == null)
            {
                GameObject worldObject = new GameObject("World");
                worldObject.AddComponent<World>();
            }

            if (FindObjectOfType<Game>() == null)
            {
                GameObject gameObject = new GameObject("Game");
                gameObject.AddComponent<Game>();
            }
        }
    }
}
