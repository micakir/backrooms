using UnityEngine;
using System.Collections.Generic;

public class ChunkManager : MonoBehaviour
{
    public Transform player;

    // Acá van todas las habitaciones
    public GameObject[] roomPrefabs;

    // Tamaño REAL de una habitación
    public Vector2 roomSize = new Vector2(16, 20);

    // 2 = 5x5 habitaciones
    public int renderDistance = 2;

    private Dictionary<Vector2Int, GameObject> loadedRooms =
        new Dictionary<Vector2Int, GameObject>();

    private Vector2Int currentChunk;

    void Start()
    {
        currentChunk = GetPlayerChunk();
        UpdateChunks();
    }

    void Update()
    {
        Vector2Int newChunk = GetPlayerChunk();

        if (newChunk != currentChunk)
        {
            currentChunk = newChunk;
            UpdateChunks();
        }
    }

    Vector2Int GetPlayerChunk()
    {
        return new Vector2Int(
            Mathf.FloorToInt(player.position.x / roomSize.x),
            Mathf.FloorToInt(player.position.z / roomSize.y)
        );
    }

    void UpdateChunks()
    {
        HashSet<Vector2Int> neededChunks = new HashSet<Vector2Int>();

        for (int x = -renderDistance; x <= renderDistance; x++)
        {
            for (int z = -renderDistance; z <= renderDistance; z++)
            {
                Vector2Int coord = currentChunk + new Vector2Int(x, z);

                neededChunks.Add(coord);

                if (!loadedRooms.ContainsKey(coord))
                {
                    SpawnRoom(coord);
                }
            }
        }

        List<Vector2Int> toRemove = new List<Vector2Int>();

        foreach (var room in loadedRooms)
        {
            if (!neededChunks.Contains(room.Key))
            {
                Destroy(room.Value);
                toRemove.Add(room.Key);
            }
        }

        foreach (var coord in toRemove)
        {
            loadedRooms.Remove(coord);
        }
    }

    void SpawnRoom(Vector2Int coord)
    {
        // Seed basada en la posición
        int seed = coord.x * 73856093 ^ coord.y * 19349663;

        System.Random rng = new System.Random(seed);

        int index = rng.Next(roomPrefabs.Length);

        Vector3 pos = new Vector3(
        coord.x * roomSize.x,
        0,
        coord.y * roomSize.y
);

        GameObject room =
            Instantiate(roomPrefabs[index], pos, Quaternion.identity);

        room.name = "Room (" + coord.x + "," + coord.y + ")";

        loadedRooms.Add(coord, room);
    }
}