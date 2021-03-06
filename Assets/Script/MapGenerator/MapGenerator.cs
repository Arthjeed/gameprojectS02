﻿using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using System;
using Photon.Pun;

public class MapGenerator : MonoBehaviour {

	public int width;
	public int height;

	public string seed;
	public bool useRandomSeed;

	[Range(0,100)]
	public int randomFillPercent;

	//GameObject[] spheres = new GameObject[5];
	//GameObject[] capsules = new GameObject[5];

	public int[,] map;

	public NavMeshSurface surface;

	public bool useExistingMesh;

	public GameObject enemySpawner;

	public GameObject spaceShip;

	private NavMeshData navMeshData;
	private NavMeshDataInstance navMeshDataInstance;
    private PhotonView PV;

	private List<Room> savedListRoom = new List<Room>();

	public string mapTo2D;

	//public LineRenderer lineRenderer;

	private List<GameObject> listEnemySpawner = new List<GameObject>();

    private float timer = 0;
	

    void Start() {
        PV = GetComponent<PhotonView>();
		// if (PV.IsMine) {
            GenerateMap();
			/*if (!useExistingMesh)
            	surface.BuildNavMesh();*/
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.M)) {
			GenerateMap();
			savedListRoom = new List<Room>();
			//navMeshDataInstance.Remove();
			//surface.BuildNavMesh();
		}

        timer += Time.deltaTime;
        if (timer >= 20)
        {
            timer = 0;
            for (int i = 0; i < listEnemySpawner.Count; i++)
            {
                listEnemySpawner[i].GetComponent<SpawnerManagement>().enemyMinLevel += 1;
                listEnemySpawner[i].GetComponent<SpawnerManagement>().enemyMaxLevel += 1;
            }
        }
    }

	void SpawnPlayer(int[,] map) {	
		//int a = 0;	
		foreach (Coord tiles in savedListRoom[0].tiles)
		{
			//lineRenderer.SetPosition(a, new Vector2(tiles.tileX * 30 - 1500, tiles.tileY * 30 - 1500));
			//lineRenderer.positionCount++;
			if (Physics2D.OverlapArea(new Vector2(tiles.tileX * 30 - 1500 - 60, tiles.tileY * 30 - 1500 + 80),
			 new Vector2(tiles.tileX * 30 - 1500 + 60, tiles.tileY * 30 - 1500 - 80)) == null)
			{
				//lineRenderer.SetPosition(0, new Vector2(tiles.tileX * 30 - 1500 - 60, tiles.tileY * 30 - 1500 + 80));
				//lineRenderer.SetPosition(1, new Vector2(tiles.tileX * 30 - 1500 + 60,  tiles.tileY * 30 - 1500 - 80));
				spaceShip.transform.position = new Vector2(tiles.tileX * 30 - 1500, tiles.tileY * 30 - 1500);
			}
		}
	}

	void SpawnSpawner(int[,] map) {
		foreach (Room room in savedListRoom)
		{
			List<Vector2> listPossibleSpawner = new List<Vector2>();
			foreach (Coord tiles in room.tiles)
			{
				if (Physics2D.OverlapArea(new Vector2(tiles.tileX * 30 - 1500 - 100, tiles.tileY * 30 - 1500 + 100),
			 	new Vector2(tiles.tileX * 30 - 1500 + 100, tiles.tileY * 30 - 1500 - 100)) == null)
				{
					//lineRenderer.SetPosition(0, new Vector2(tiles.tileX * 30 - 1500 - 100, tiles.tileY * 30 - 1500 + 100));
					//lineRenderer.SetPosition(1, new Vector2(tiles.tileX * 30 - 1500 + 100,  tiles.tileY * 30 - 1500 - 100));

					listPossibleSpawner.Add(new Vector2(tiles.tileX * 30 - 1500, tiles.tileY * 30 - 1500));
					//GameObject spawner = Instantiate(enemySpawner, new Vector2(tiles.tileX * 30 - 1500, tiles.tileY * 30 - 1500), Quaternion.identity);
					//listEnemySpawner.Add(spawner);
				}
			}
			if (listPossibleSpawner.Count > 0)
			{
				GameObject spawner = Instantiate(enemySpawner, new Vector2(listPossibleSpawner[listPossibleSpawner.Count / 2].x, listPossibleSpawner[listPossibleSpawner.Count / 2].y), Quaternion.identity);
				listEnemySpawner.Add(spawner);
			}
			if (listPossibleSpawner.Count > 1000)
			{
				GameObject spawner = Instantiate(enemySpawner, new Vector2(listPossibleSpawner[0].x, listPossibleSpawner[0].y), Quaternion.identity);
				listEnemySpawner.Add(spawner);
			}
		}
	}

	

	/*void Spawn(int[,] map) {

	}*/
	/*void Spawn(int[,] map) {

		int nodeX = map.GetLength(0);
		int nodeY = map.GetLength(1);
		int mapX;
		int mapY;
		
		for (int i = 0; i < 5; i++) {
			Destroy(spheres[i]);
			Destroy(capsules[i]);
			spheres[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			do {
				mapX = UnityEngine.Random.Range(2, nodeX - 2);
				mapY = UnityEngine.Random.Range(2, nodeY - 2);
			}
			while (map[mapX, mapY] == 1);
			spheres[i].transform.position = new Vector3(mapX - nodeX / 2, mapY - nodeY / 2, 0);
			capsules[i] = GameObject.CreatePrimitive(PrimitiveType.Capsule);
			do {
				mapX = UnityEngine.Random.Range(2, nodeX - 2);
				mapY = UnityEngine.Random.Range(2, nodeY - 2);
			}
			while (map[mapX, mapY] == 1 || (map[mapX - 1, mapY] == 0 && map[mapX + 1, mapY] == 0 && map[mapX, mapY - 1] == 0 && map[mapX, mapY + 1] == 0));
			capsules[i].transform.position = new Vector3(mapX - nodeX / 2 + 0.5f, mapY - nodeY / 2 + 0.5f, 0);
			if (map[mapX + 1, mapY] == 1 || map[mapX - 1, mapY] == 1)
				capsules[i].transform.Rotate(0.0f, 0.0f, 90.0f, Space.Self);
		}
	}*/

	void GenerateMap() {
		if (!useExistingMesh) {
			map = new int[width,height];
			RandomFillMap();

			for (int i = 0; i < 5; i ++) {
				SmoothMap();
			}

			ProcessMap ();
		}
			int borderSize = 1;
			int[,] borderedMap = new int[width + borderSize * 2,height + borderSize * 2];

		if (!useExistingMesh){
			for (int x = 0; x < borderedMap.GetLength(0); x ++) {
				for (int y = 0; y < borderedMap.GetLength(1); y ++) {
					if (x >= borderSize && x < width + borderSize && y >= borderSize && y < height + borderSize) {
						borderedMap[x,y] = map[x-borderSize,y-borderSize];
					}
					else {
						borderedMap[x,y] =1;
					}
				}
			}
		} 	else {
				for (int x = 0; x < width + borderSize * 2; x++){
					for (int y = 0; y < height + borderSize * 2; y++)
					{
						borderedMap[x,y] = mapTo2D[x*(width+borderSize*2) + y] - '0';
					}
			}
		}
			/*string arrayString ="";
			for (int x = 0; x < borderedMap.GetLength(0); x ++) {
				for (int y = 0; y < borderedMap.GetLength(1); y ++) {
					arrayString += string.Format("{0}", borderedMap[x,y]);
				}
				//arrayString += System.Environment.NewLine + System.Environment.NewLine;
			}
			Debug.Log(arrayString);
		*/
		
		MeshGenerator meshGen = GetComponent<MeshGenerator>();
		
		meshGen.GenerateMesh(borderedMap, 1, useExistingMesh);
		SpawnPlayer(borderedMap);
		Debug.Log(borderedMap.GetLength(0));
		Debug.Log(borderedMap.GetLength(1));
		SpawnSpawner(borderedMap);
	}

	void ProcessMap() {
		List<List<Coord>> wallRegions = GetRegions (1);
		int wallThresholdSize = 100;

		foreach (List<Coord> wallRegion in wallRegions) {
			if (wallRegion.Count < wallThresholdSize) {
				foreach (Coord tile in wallRegion) {
					map[tile.tileX,tile.tileY] = 0;
				}
			}
		}

		List<List<Coord>> roomRegions = GetRegions (0);
		int roomThresholdSize = 100;
		List<Room> survivingRooms = new List<Room> ();
		
		foreach (List<Coord> roomRegion in roomRegions) {
			if (roomRegion.Count < roomThresholdSize) {
				foreach (Coord tile in roomRegion) {
					map[tile.tileX,tile.tileY] = 1;
				}
			}
			else {
				survivingRooms.Add(new Room(roomRegion, map));
				savedListRoom.Add(new Room(roomRegion, map)); //
			}
		}
		survivingRooms.Sort ();
		survivingRooms [0].isMainRoom = true;
		survivingRooms [0].isAccessibleFromMainRoom = true;

		savedListRoom.Sort(); //

		ConnectClosestRooms (survivingRooms);
	}

	void ConnectClosestRooms(List<Room> allRooms, bool forceAccessibilityFromMainRoom = false) {

		List<Room> roomListA = new List<Room> ();
		List<Room> roomListB = new List<Room> ();

		if (forceAccessibilityFromMainRoom) {
			foreach (Room room in allRooms) {
				if (room.isAccessibleFromMainRoom) {
					roomListB.Add (room);
				} else {
					roomListA.Add (room);
				}
			}
		} else {
			roomListA = allRooms;
			roomListB = allRooms;
		}

		int bestDistance = 0;
		Coord bestTileA = new Coord ();
		Coord bestTileB = new Coord ();
		Room bestRoomA = new Room ();
		Room bestRoomB = new Room ();
		bool possibleConnectionFound = false;

		foreach (Room roomA in roomListA) {
			if (!forceAccessibilityFromMainRoom) {
				possibleConnectionFound = false;
				if (roomA.connectedRooms.Count > 0) {
					continue;
				}
			}

			foreach (Room roomB in roomListB) {
				if (roomA == roomB || roomA.IsConnected(roomB)) {
					continue;
				}
			
				for (int tileIndexA = 0; tileIndexA < roomA.edgeTiles.Count; tileIndexA ++) {
					for (int tileIndexB = 0; tileIndexB < roomB.edgeTiles.Count; tileIndexB ++) {
						Coord tileA = roomA.edgeTiles[tileIndexA];
						Coord tileB = roomB.edgeTiles[tileIndexB];
						int distanceBetweenRooms = (int)(Mathf.Pow (tileA.tileX-tileB.tileX,2) + Mathf.Pow (tileA.tileY-tileB.tileY,2));

						if (distanceBetweenRooms < bestDistance || !possibleConnectionFound) {
							bestDistance = distanceBetweenRooms;
							possibleConnectionFound = true;
							bestTileA = tileA;
							bestTileB = tileB;
							bestRoomA = roomA;
							bestRoomB = roomB;
						}
					}
				}
			}
			if (possibleConnectionFound && !forceAccessibilityFromMainRoom) {
				CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
			}
		}

		if (possibleConnectionFound && forceAccessibilityFromMainRoom) {
			CreatePassage(bestRoomA, bestRoomB, bestTileA, bestTileB);
			ConnectClosestRooms(allRooms, true);
		}

		if (!forceAccessibilityFromMainRoom) {
			ConnectClosestRooms(allRooms, true);
		}
	}

	void CreatePassage(Room roomA, Room roomB, Coord tileA, Coord tileB) {
		Room.ConnectRooms (roomA, roomB);
		//Debug.DrawLine (CoordToWorldPoint (tileA), CoordToWorldPoint (tileB), Color.green, 100);

		List<Coord> line = GetLine (tileA, tileB);
		foreach (Coord c in line) {
			DrawCircle(c,5);
		}
	}

	void DrawCircle(Coord c, int r) {
		for (int x = -r; x <= r; x++) {
			for (int y = -r; y <= r; y++) {
				if (x*x + y*y <= r*r) {
					int drawX = c.tileX + x;
					int drawY = c.tileY + y;
					if (IsInMapRange(drawX, drawY)) {
						map[drawX,drawY] = 0;
					}
				}
			}
		}
	}

	List<Coord> GetLine(Coord from, Coord to) {
		List<Coord> line = new List<Coord> ();

		int x = from.tileX;
		int y = from.tileY;

		int dx = to.tileX - from.tileX;
		int dy = to.tileY - from.tileY;

		bool inverted = false;
		int step = Math.Sign (dx);
		int gradientStep = Math.Sign (dy);

		int longest = Mathf.Abs (dx);
		int shortest = Mathf.Abs (dy);

		if (longest < shortest) {
			inverted = true;
			longest = Mathf.Abs(dy);
			shortest = Mathf.Abs(dx);

			step = Math.Sign (dy);
			gradientStep = Math.Sign (dx);
		}

		int gradientAccumulation = longest / 2;
		for (int i =0; i < longest; i ++) {
			line.Add(new Coord(x,y));

			if (inverted) {
				y += step;
			}
			else {
				x += step;
			}

			gradientAccumulation += shortest;
			if (gradientAccumulation >= longest) {
				if (inverted) {
					x += gradientStep;
				}
				else {
					y += gradientStep;
				}
				gradientAccumulation -= longest;
			}
		}

		return line;
	}

	Vector3 CoordToWorldPoint(Coord tile) {
		return new Vector3 (-width / 2 + .5f + tile.tileX, 2, -height / 2 + .5f + tile.tileY);
	}

	List<List<Coord>> GetRegions(int tileType) {
		List<List<Coord>> regions = new List<List<Coord>> ();
		int[,] mapFlags = new int[width,height];

		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
				if (mapFlags[x,y] == 0 && map[x,y] == tileType) {
					List<Coord> newRegion = GetRegionTiles(x,y);
					regions.Add(newRegion);

					foreach (Coord tile in newRegion) {
						mapFlags[tile.tileX, tile.tileY] = 1;
					}
				}
			}
		}

		return regions;
	}

	List<Coord> GetRegionTiles(int startX, int startY) {
		List<Coord> tiles = new List<Coord> ();
		int[,] mapFlags = new int[width,height];
		int tileType = map [startX, startY];

		Queue<Coord> queue = new Queue<Coord> ();
		queue.Enqueue (new Coord (startX, startY));
		mapFlags [startX, startY] = 1;

		while (queue.Count > 0) {
			Coord tile = queue.Dequeue();
			tiles.Add(tile);

			for (int x = tile.tileX - 1; x <= tile.tileX + 1; x++) {
				for (int y = tile.tileY - 1; y <= tile.tileY + 1; y++) {
					if (IsInMapRange(x,y) && (y == tile.tileY || x == tile.tileX)) {
						if (mapFlags[x,y] == 0 && map[x,y] == tileType) {
							mapFlags[x,y] = 1;
							queue.Enqueue(new Coord(x,y));
						}
					}
				}
			}
		}
		return tiles;
	}

	bool IsInMapRange(int x, int y) {
		return x >= 0 && x < width && y >= 0 && y < height;
	}


	void RandomFillMap() {
	
		if (useRandomSeed) {
			seed = Time.time.ToString();
            //Debug.Log(seed.GetHashCode());
		}

		System.Random pseudoRandom = new System.Random(seed.GetHashCode());

		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
				if (x == 0 || x == width-1 || y == 0 || y == height -1) {
					map[x,y] = 1;
				}
				else {
					map[x,y] = (pseudoRandom.Next(0,100) < randomFillPercent)? 1: 0;
				}
			}
		}
	}

	void SmoothMap() {
		for (int x = 0; x < width; x ++) {
			for (int y = 0; y < height; y ++) {
				int neighbourWallTiles = GetSurroundingWallCount(x,y);

				if (neighbourWallTiles > 4)
					map[x,y] = 1;
				else if (neighbourWallTiles < 4)
					map[x,y] = 0;

			}
		}
	}

	int GetSurroundingWallCount(int gridX, int gridY) {
		int wallCount = 0;
		for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX ++) {
			for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY ++) {
				if (IsInMapRange(neighbourX,neighbourY)) {
					if (neighbourX != gridX || neighbourY != gridY) {
						wallCount += map[neighbourX,neighbourY];
					}
				}
				else {
					wallCount ++;
				}
			}
		}

		return wallCount;
	}

	struct Coord {
		public int tileX;
		public int tileY;

		public Coord(int x, int y) {
			tileX = x;
			tileY = y;
		}
	}


	class Room : IComparable<Room> {
		public List<Coord> tiles;
		public List<Coord> edgeTiles;
		public List<Room> connectedRooms;
		public int roomSize;
		public bool isAccessibleFromMainRoom;
		public bool isMainRoom;

		public Room() {
		}

		public Room(List<Coord> roomTiles, int[,] map) {
			tiles = roomTiles;
			roomSize = tiles.Count;
			connectedRooms = new List<Room>();

			edgeTiles = new List<Coord>();
			foreach (Coord tile in tiles) {
				for (int x = tile.tileX-1; x <= tile.tileX+1; x++) {
					for (int y = tile.tileY-1; y <= tile.tileY+1; y++) {
						if (x == tile.tileX || y == tile.tileY) {
							if (map[x,y] == 1) {
								edgeTiles.Add(tile);
							}
						}
					}
				}
			}
		}

		public void SetAccessibleFromMainRoom() {
			if (!isAccessibleFromMainRoom) {
				isAccessibleFromMainRoom = true;
				foreach (Room connectedRoom in connectedRooms) {
					connectedRoom.SetAccessibleFromMainRoom();
				}
			}
		}

		public static void ConnectRooms(Room roomA, Room roomB) {
			if (roomA.isAccessibleFromMainRoom) {
				roomB.SetAccessibleFromMainRoom ();
			} else if (roomB.isAccessibleFromMainRoom) {
				roomA.SetAccessibleFromMainRoom();
			}
			roomA.connectedRooms.Add (roomB);
			roomB.connectedRooms.Add (roomA);
		}

		public bool IsConnected(Room otherRoom) {
			return connectedRooms.Contains(otherRoom);
		}

		public int CompareTo(Room otherRoom) {
			return otherRoom.roomSize.CompareTo (roomSize);
		}
	}

}