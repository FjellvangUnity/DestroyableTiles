using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;
using System;

public class GroundTile : Tile {

    [SerializeField]
    private Sprite[] groundSprites;

    [SerializeField]
    private Sprite preview;

    public Tile.ColliderType m_defaultColliderTyper = Tile.ColliderType.Sprite;


    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        // Called everytime you place a new tile, on that pos.
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                Vector3Int nPos = new Vector3Int(position.x + x, position.y + y, position.z);

                if (HasWater(tilemap, nPos))
                {
                    tilemap.RefreshTile(nPos);
                }
            }
        }
    }
    static internal int[][] NeighborArray()
    {
        int[][] neighbors = new int[][]
        {
            new int[] {-1,1}, new int[] {0,1},  new int[]  {1,1 },
            new int[] {-1,0},                   new int[]  {1,0 },
            new int[] {-1,-1},new int[] {0,-1}, new int[]  {1,-1 }
        };
        return neighbors;
    }
    
    bool HasWater(ITilemap tileMap, Vector3Int position)
    {
        // returns true if the tile has water.
        // because it compares to this u know håmie
        return tileMap.GetTile(position) == this;
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        //int north = HasWater(tilemap, new Vector3Int(position.x, position.y + 1, position.z)) ? 1 : 0;
        //int south = HasWater(tilemap, new Vector3Int(position.x, position.y - 1, position.z)) ? 1 : 0;
        //int east = HasWater(tilemap, new Vector3Int(position.x + 1, position.y, position.z)) ? 1 : 0;
        //int west = HasWater(tilemap, new Vector3Int(position.x - 1, position.y, position.z)) ? 1 : 0;
        //int northeast = (HasWater(tilemap, new Vector3Int(position.x + 1, position.y + 1, position.z)) ? 1 : 0) & (north & east);
        //int northwest = (HasWater(tilemap, new Vector3Int(position.x - 1, position.y + 1, position.z)) ? 1 : 0) & (north & west);
        //int southeast = (HasWater(tilemap, new Vector3Int(position.x + 1, position.y - 1, position.z)) ? 1 : 0) & (south & east);
        //int southwest = (HasWater(tilemap, new Vector3Int(position.x - 1, position.y - 1, position.z)) ? 1 : 0) & (south & west);

        //int index = 1 * northwest + 2 * north + 4 * northeast + 8 * west + 16 * east + 32 * southwest + 64 * south + 128 * southeast;
        int north = Convert.ToByte(HasWater(tilemap, new Vector3Int(position.x, position.y + 1, position.z)));
        int south = Convert.ToByte(HasWater(tilemap, new Vector3Int(position.x, position.y - 1, position.z)));
        int east = Convert.ToByte(HasWater(tilemap, new Vector3Int(position.x + 1, position.y, position.z)));
        int west = Convert.ToByte(HasWater(tilemap, new Vector3Int(position.x - 1, position.y, position.z)));
        int northeast = (Convert.ToByte(HasWater(tilemap, new Vector3Int(position.x + 1, position.y + 1, position.z)))) & (north & east);
        int northwest = (Convert.ToByte(HasWater(tilemap, new Vector3Int(position.x - 1, position.y + 1, position.z)))) & (north & west);
        int southeast = (Convert.ToByte(HasWater(tilemap, new Vector3Int(position.x + 1, position.y - 1, position.z)))) & (south & east);
        int southwest = (Convert.ToByte(HasWater(tilemap, new Vector3Int(position.x - 1, position.y - 1, position.z)))) & (south & west);

        int index = 0;
        index |= (northwest | (north << 1) | (northeast << 2) | (west << 3) | (east << 4) | (southwest << 5) | (south << 6) | (southeast << 7));
        //int index = (int)Flag(position, tilemap);
        int spriteIndex = GetIndex(index);

        tileData.colliderType = m_defaultColliderTyper;

        try
        {
            tileData.sprite = groundSprites[spriteIndex];
        }
        catch (System.IndexOutOfRangeException)
        {

            Debug.LogWarning("ERROR:" + spriteIndex);
            throw;
        }
    }
    byte Flag(Vector3Int position, ITilemap tilemap)
    {
        //todo get new name
        byte flag = 0;
        byte option = 1;
        var neighbors = NeighborArray();
        for (byte i = 0; i < neighbors.GetLength(0); i++)
        {
            var x = neighbors[i];
            if(HasWater(tilemap, new Vector3Int(position.x + x[0], position.y + x[1], position.z)))
            {
                flag |= (byte)(option << i);
            }
        }
        return flag;
    }

    private static int GetIndex(int index)
    {
        int spriteIndex = 0;
        switch (index)
        {
            case 0:
                spriteIndex = 0;
                break;
            case 2:
                spriteIndex = 1;
                break;
            case 8:
                spriteIndex = 2;
                break;
            case 10:
                spriteIndex = 3;
                break;
            case 11:
                spriteIndex = 4;
                break;
            case 16:
                spriteIndex = 5;
                break;
            case 18:
                spriteIndex = 6;
                break;
            case 22:
                spriteIndex = 7;
                break;
            case 24:
                spriteIndex = 8;
                break;
            case 26:
                spriteIndex = 9;
                break;
            case 27:
                spriteIndex = 10;
                break;
            case 30:
                spriteIndex = 11;
                break;
            case 31:
                spriteIndex = 12;
                break;
            case 64:
                spriteIndex = 13;
                break;
            case 66:
                spriteIndex = 14;
                break;
            case 72:
                spriteIndex = 15;
                break;
            case 74:
                spriteIndex = 16;
                break;
            case 75:
                spriteIndex = 17;
                break;
            case 80:
                spriteIndex = 18;
                break;
            case 82:
                spriteIndex = 19;
                break;
            case 86:
                spriteIndex = 20;
                break;
            case 88:
                spriteIndex = 21;
                break;
            case 90:
                spriteIndex = 22;
                break;
            case 91:
                spriteIndex = 23;
                break;
            case 94:
                spriteIndex = 24;
                break;
            case 95:
                spriteIndex = 25;
                break;
            case 104:
                spriteIndex = 26;
                break;
            case 106:
                spriteIndex = 27;
                break;
            case 107:
                spriteIndex = 28;
                break;
            case 120:
                spriteIndex = 29;
                break;
            case 122:
                spriteIndex = 30;
                break;
            case 123:
                spriteIndex = 31;
                break;
            case 126:
                spriteIndex = 32;
                break;
            case 127:
                spriteIndex = 33;
                break;
            case 208:
                spriteIndex = 34;
                break;
            case 210:
                spriteIndex = 35;
                break;
            case 214:
                spriteIndex = 36;
                break;
            case 216:
                spriteIndex = 37;
                break;
            case 218:
                spriteIndex = 38;
                break;
            case 219:
                spriteIndex = 39;
                break;
            case 222:
                spriteIndex = 40;
                break;
            case 223:
                spriteIndex = 41;
                break;
            case 248:
                spriteIndex = 42;
                break;
            case 250:
                spriteIndex = 43;
                break;
            case 251:
                spriteIndex = 44;
                break;
            case 254:
                spriteIndex = 45;
                break;
            case 255:
                spriteIndex = 46;
                break;
            default:
                Debug.Log("ERROR: " + index);
                break;
        }

        return spriteIndex;
    }


#if UNITY_EDITOR
    [MenuItem("Assets/Create/Tiles/GroundTile")]
    public static void CreateWaterTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Groundtile", "New Groundtile", "asset", "SaveGroundtile", "Assets");
        if (path == null)
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<GroundTile>(), path);

    }

#endif
}
