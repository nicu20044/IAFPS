namespace IAFPS_Back.Models;

public class Node
{
    public IList<Edge> Edges { get; private set; } = [];
    public IList<Places> Places { get; private set; } = [];

    public void AddEdge(Edge edge) => Edges.Add(edge);
    public void AddPlace(Places place) => Places.Add(place);
}
