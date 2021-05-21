﻿using Domain.Entities;
using Domain.Pathfinding.Implementation;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Domain.UnitTests.Pathfinding.Implementation
{
    public class DijkstraTest
    {
        public static PathfindingTestData SHORTEST_PATH_HORIZONTAL = new(
            (
                new[]
                {
                    "S 1 1",
                    "W 9 1",
                    "F 1 1"
                },
                GridType.Horizontal,
                new Position[] { new(0, 0), new(0, 1), new(0, 2), new(1, 2), new(2, 2), new(2, 1), new(2, 0) }
            ),
            (
                new[]
                {
                    "W 1 1 1",
                    "W 1 W 1",
                    "S 1 W F",
                    "1 1 9 1"
                },
                GridType.Horizontal,
                new Position[] { new(2, 0), new(2, 1), new(1, 1), new(0, 1), new(0, 2), new(0, 3), new(1, 3), new(2, 3) }
            )
        );

        public static PathfindingTestData SHORTEST_PATH_DIAGONAL = new(
            (
                new[]
                {
                    "S 1 1",
                    "W 9 1",
                    "F 1 1"
                },
                GridType.Diagonal,
                new Position[] { new(0, 0), new(0, 1), new(1, 2), new(2, 1), new(2, 0) }
            ),
            (
                new[]
                {
                    "W 1 1 1",
                    "W 1 W 1",
                    "S W W F",
                    "W 9 1 1"
                },
                GridType.Diagonal,
                new Position[] { new(2, 0), new(1, 1), new(0, 2), new(1, 3), new(2, 3) }
            )
        );

        public static PathfindingTestData BLOCKED_PATH = new(
            (
                new[]
                {
                    "S 1 1",
                    "W W 1",
                    "F W 1"
                },
                GridType.Horizontal,
                new Position[] { new(0, 0), new(0, 1), new(0, 2), new(1, 2), new(2, 2) }
            ),
            (
                new[]
                {
                    "W W W W",
                    "W S 1 W",
                    "W 1 1 W",
                    "W W W F"
                },
                GridType.Horizontal,
                new Position[] { new(1, 1), new(1, 2), new(2, 1), new(2, 2) }
            )
        );

        [Theory]
        [MemberData(nameof(SHORTEST_PATH_HORIZONTAL))]
        public void BreadthFirstSearch_ShouldFindShortestPath_Horizonatal(Grid grid, Position[] expectedShortestPath)
        {
            var sut = new Dijkstra(grid);
            var pathfindingResult = sut.ShortestPath();

            Assert.Equal(expectedShortestPath, pathfindingResult.ShortestPath.Select(node => node.Position));
        }

        [Theory]
        [MemberData(nameof(SHORTEST_PATH_DIAGONAL))]
        public void BreadthFirstSearch_ShouldFindShortestPath_Diagonal(Grid grid, Position[] expectedShortestPath)
        {
            var sut = new Dijkstra(grid);
            var pathfindingResult = sut.ShortestPath();

            Assert.Equal(expectedShortestPath, pathfindingResult.ShortestPath.Select(node => node.Position));
        }

        [Theory]
        [MemberData(nameof(BLOCKED_PATH))]
        public void BreadthFirstSearch_ShouldVisitAllUnblockedPositions(Grid grid, Position[] expectedVisitedPositions)
        {
            var sut = new Dijkstra(grid);
            var visitedPositions = sut.ShortestPath().VisitedNodes.Select(node => node.Position);

            Assert.Equal(expectedVisitedPositions.Length, visitedPositions.Count());
            Assert.All(expectedVisitedPositions, pos => Assert.Contains(pos, visitedPositions));
        }
    }
}
