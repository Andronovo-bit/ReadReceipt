
public class Receipt
{
    public Item[] items { get; set; }
}

public class Item
{
    public string locale { get; set; }
    public string description { get; set; }
    public Boundingpoly boundingPoly { get; set; }
}

public class Boundingpoly
{
    public Vertex[] vertices { get; set; }
}

public class Vertex
{
    public int x { get; set; }
    public int y { get; set; }
}
