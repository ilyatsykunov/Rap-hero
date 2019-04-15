public class Tile{

    public Tile(int pos)
    {
        this.pos = pos;
    }

    public int pos;
    public bool next;
    public bool prev;
}









/*
TileController tc;
public List<GameObject> tiles;
public Material black;
public Material white;
public Material grey;

public GameObject activeTile;

// Use this for initialization
void Start () {
    tc = gameObject.transform.parent.GetComponent<TileController>();
    Transform[] tTiles = gameObject.GetComponentsInChildren<Transform>();
    for(int i = 1; i < gameObject.transform.childCount + 1; i++)
    {
        tTiles[i].name = i.ToString();
        tiles.Add(tTiles[i].gameObject);
    }
    SwapTiles();
}

// Update is called once per frame
void Update () {
    if(gameObject.transform.position.z <= tc.activePos && gameObject.transform.position.z > tc.activePos - 0.1f)
    {
        tc.activeTile = activeTile;
    }

    if(gameObject.transform.position.z > tc.bottom)
    {
        gameObject.transform.Translate(Vector3.back * Time.deltaTime * tc.speed);
    }
    if(gameObject.transform.position.z <= tc.bottom)
    {
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, tc.top);
        tc.StopSound();
        tc.RemoveTile();
        SwapTiles();
    }
}
public void SwapTiles()
{
    int tile = Random.Range(0, tiles.Count);
    for(int i = 0; i < tiles.Count; i++)
    {
        if(i == tile)
        {
            tiles[tile].GetComponent<MeshRenderer>().material = black;
            activeTile = tiles[tile];
            tc.AddTile(tile+1);
        }
        else
        {
            tiles[i].GetComponent<MeshRenderer>().material = white;
        }
    }
}
void Click()
{
    activeTile.GetComponent<MeshRenderer>().material = grey;
}
*/
