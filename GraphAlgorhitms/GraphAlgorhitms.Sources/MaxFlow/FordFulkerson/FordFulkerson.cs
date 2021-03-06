﻿using System;
using System.Collections.Generic;
using System.Linq;
using GraphAlgorhitms.Graphonium.Models;

namespace GraphAlgorhitms.Sources.MaxFlow.FordFulkerson
{
    public class FordFulkerson
    {
        private Graph _workGraph;

        private readonly Stack<Tuple<int, int>> _wayStack = new Stack<Tuple<int, int>>();
        private readonly List<Tuple<int, int>> _unavailableEdges = new List<Tuple<int, int>>();

        private readonly Func<Vertex, IEnumerable<Edge>> _availableEdgesFrom = vertex =>
        {
            return vertex.OutgoingEdges.Where(e => ((FordFulkersonEdge)e).Flow < e.Weight)
            .Union(vertex.IncomingEdges.Where(e => ((FordFulkersonEdge)e).Flow > 0));
        };

        public int GetMaxFlow(Graph initGraph)
        {
            _workGraph = CastToFordFulkerson(initGraph);

            var startVertex = _workGraph.Vertexes.First();
            var endVertex = _workGraph.Vertexes.Last();

            do
            {
                _wayStack.Clear();
                _unavailableEdges.Clear();
            } while (TryFindWay(startVertex, endVertex));

            var result = startVertex.Edges.Sum(e => ((FordFulkersonEdge)e).Flow);

            return result;
        }

        private bool TryFindWay(Vertex originVertex, Vertex destinationVertex)
        {
            if (originVertex == destinationVertex)
            {
                ApplyWayChanges();
                return true;
            }

            var found = false;
            IEnumerable<Edge> availableEdges;
            do
            {
                availableEdges = _availableEdgesFrom(originVertex)
                    .Where(e => !_unavailableEdges.Any(t => t.Item1 == e.VertexBegin.Number && t.Item2 == e.VertexEnd.Number));
                var availableEdge = availableEdges.FirstOrDefault();
                if (availableEdge != null)
                {
                    Vertex nextVertex;
                    if (availableEdge.VertexBegin == originVertex)
                    {
                        nextVertex = availableEdge.VertexEnd;
                    }
                    else
                    {
                        nextVertex = availableEdge.VertexBegin;
                    }
                    _wayStack.Push(new Tuple<int, int>(availableEdge.VertexBegin.Number, availableEdge.VertexEnd.Number));
                    _unavailableEdges.Add(new Tuple<int, int>(availableEdge.VertexBegin.Number,
                        availableEdge.VertexEnd.Number));
                    _unavailableEdges.Add(new Tuple<int, int>(availableEdge.VertexEnd.Number,
                        availableEdge.VertexBegin.Number));

                    found |= TryFindWay(nextVertex, destinationVertex);
                }
                else
                {
                    GoBack();
                    return false;
                }
            }
            while (!found && availableEdges.Any());

            if (!availableEdges.Any() && _wayStack.Any())
            {
                GoBack();
            }

            return found;
        }

        private void GoBack()
        {
            var lastStep = _wayStack.Peek();
            var availableTuple = _unavailableEdges.Single(t => t.Item1 == lastStep.Item2 && t.Item2 == lastStep.Item1);
            _unavailableEdges.Remove(availableTuple);
            _wayStack.Pop();
        }

        private void ApplyWayChanges()
        {
            var delta = CalculateDelta(_wayStack.ToList());
            var endpointVertex = _wayStack.Peek().Item2;
            while (_wayStack.Any())
            {
                var previuosStep = _wayStack.Pop();
                var directOrder = previuosStep.Item2 == endpointVertex;

                var from = previuosStep.Item1;
                var to = previuosStep.Item2;

                var edge = _workGraph.Edges.Single(e => e.VertexBegin.Number == from && e.VertexEnd.Number == to);
                ((FordFulkersonEdge)edge).Flow += directOrder ? delta : -delta;

                endpointVertex = directOrder ? previuosStep.Item1 : previuosStep.Item2;
            }
        }

        private int CalculateDelta(List<Tuple<int, int>> list)
        {
            var lowestDelta = int.MaxValue;
            var endpointVertex = list.First().Item2;
            while (list.Any())
            {
                var previuosStep = list.First();
                list.Remove(previuosStep);
                var directOrder = previuosStep.Item2 == endpointVertex;

                var from = previuosStep.Item1;
                var to = previuosStep.Item2;

                var edge = _workGraph.Edges.Single(e => e.VertexBegin.Number == from && e.VertexEnd.Number == to);
                var delta = directOrder ? edge.Weight - ((FordFulkersonEdge)edge).Flow : ((FordFulkersonEdge)edge).Flow;

                lowestDelta = Math.Min(delta, lowestDelta);

                endpointVertex = directOrder ? previuosStep.Item1 : previuosStep.Item2;
            }

            return lowestDelta;
        }

        private Graph CastToFordFulkerson(Graph graph)
        {
            var result = new Graph()
            {
                Edges = new List<Edge>(graph.Edges.Select(e =>
                {
                    var newEdge = new FordFulkersonEdge()
                    {
                        Flow = 0,
                        VertexBegin = e.VertexBegin,
                        VertexEnd = e.VertexEnd,
                        Weight = e.Weight
                    };

                    e.VertexBegin.Edges.Remove(e);
                    e.VertexEnd.Edges.Remove(e);
                    e.VertexBegin.Edges.Add(newEdge);
                    e.VertexEnd.Edges.Add(newEdge);

                    return newEdge;
                })),
                Vertexes = graph.Vertexes
            };

            return result;
        }
    }
}
