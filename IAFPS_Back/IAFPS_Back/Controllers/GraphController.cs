using IAFPS_Back.Models;
using Microsoft.AspNetCore.Mvc;

namespace IAFPS_Back.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GraphController : Controller
{
    private readonly Graph _graph = new();

    [HttpGet()]
    public ActionResult<decimal[][]> Index()
    {
        Node n1 = new Node();
        Node n2 = new Node();
        Node n3 = new Node();
        Node n4 = new Node();

        Edge e1 = new Edge(n1, n2, 4, 500, 50);
        Edge e2 = new Edge(n2, n1, 5, 600, 60);
        Edge e3 = new Edge(n1, n3, 3, 600, 50);
        Edge e4 = new Edge(n3, n1, 3, 600, 50);
        Edge e5 = new Edge(n1, n4, 5, 1500, 70);
        Edge e6 = new Edge(n4, n1, 5, 2500, 70);

        _graph.AddEdges([e1, e2, e3, e4, e5, e6]);
        _graph.AddNodes([n1, n2, n3, n4]);

        return _graph.GetAdjacencyMatrix();
    }

    [HttpGet("/getcurrentstate")]
    public ActionResult<Edge> GetCurrentState()
    {
        return _graph.GetStartNode.Edges[0];
    }

    // [HttpGet]
    // public ActionResult<IList<Tuple<int, int, string>>> GetInt()
    // {
    // return new List<Tuple<int, int, string>>
    // {
    // new(1, 1, "str1"),
    // new(2, 2, "str2")
    // };
    // }
}
