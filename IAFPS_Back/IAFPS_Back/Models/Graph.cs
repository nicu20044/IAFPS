namespace IAFPS_Back.Models;

public class Graph
{
    private readonly List<Node> _nodes = [];
    private readonly List<Edge> _edges = [];

    public Node GetStartNode => _nodes[0];
    public void AddNode(Node node) => _nodes.Add(node);
    public void AddEdge(Edge edge) => _edges.Add(edge);
    public void AddEdges(IEnumerable<Edge> edges) => _edges.AddRange(edges);
    public void AddNodes(IEnumerable<Node> nodes) => _nodes.AddRange(nodes);
    public List<Edge> GetEdges() => _edges;


    public decimal[][] GetAdjacencyMatrix()
    {
        int n = _nodes.Count;
        decimal[][] matrix = new decimal[n][];

        for (int i = 0; i < n; i++)
        {
            matrix[i] = new decimal[n];

            for (int j = 0; j < n; j++)
            {
                matrix[i][j] = _edges.FirstOrDefault(e => e.Start == _nodes[i] && e.End == _nodes[j])?.GetWeight() ?? 0;
            }
        }

        return matrix;
    }
}
