using System;
using ASD.Graphs;
using ASD;
using System.Collections.Generic;

namespace ASD
{

    public class Lab03GraphFunctions : System.MarshalByRefObject
    {

        // Część 1
        // Wyznaczanie odwrotności grafu
        //   0.5 pkt
        // Odwrotność grafu to graf skierowany o wszystkich krawędziach przeciwnie skierowanych niż w grafie pierwotnym
        // Parametry:
        //   g - graf wejściowy
        // Wynik:
        //   odwrotność grafu
        // Uwagi:
        //   1) Graf wejściowy pozostaje niezmieniony
        //   2) Graf wynikowy musi być w takiej samej reprezentacji jak wejściowy
        public DiGraph Lab03Reverse(DiGraph g)
        {
            DiGraph gRes = new DiGraph(g.VertexCount, g.Representation);
            foreach (var edge in g.DFS().SearchAll())
                gRes.AddEdge(edge.To, edge.From);

            return gRes;
        }

        // Część 2
        // Badanie czy graf jest dwudzielny
        //   0.5 pkt
        // Graf dwudzielny to graf nieskierowany, którego wierzchołki można podzielić na dwa rozłączne zbiory
        // takie, że dla każdej krawędzi jej końce należą do róźnych zbiorów
        // Parametry:
        //   g - badany graf
        //   vert - tablica opisująca podział zbioru wierzchołków na podzbiory w następujący sposób
        //          vert[i] == 1 oznacza, że wierzchołek i należy do pierwszego podzbioru
        //          vert[i] == 2 oznacza, że wierzchołek i należy do drugiego podzbioru
        // Wynik:
        //   true jeśli graf jest dwudzielny, false jeśli graf nie jest dwudzielny (w tym przypadku parametr vert ma mieć wartość null)
        // Uwagi:
        //   1) Graf wejściowy pozostaje niezmieniony
        //   2) Podział wierzchołków może nie być jednoznaczny - znaleźć dowolny
        //   3) Pamiętać, że każdy z wierzchołków musi być przyporządkowany do któregoś ze zbiorów
        //   4) Metoda ma mieć taki sam rząd złożoności jak zwykłe przeszukiwanie (za większą będą kary!)
        public bool Lab03IsBipartite(Graph g, out int[] vert)
        {
            vert = new int[g.VertexCount];
            foreach (var edge in g.DFS().SearchAll())
            {
                if (vert[edge.From] == 0 && vert[edge.To] == 0)
                {
                    vert[edge.From] = 1;
                    vert[edge.To] = 2;
                }
                else if (vert[edge.From] == 0)
                    vert[edge.From] = 3 - vert[edge.To];
                else if (vert[edge.To] == 0)
                    vert[edge.To] = 3 - vert[edge.From];
                else if (vert[edge.From] == vert[edge.To])
                {
                    vert = null;
                    return false;
                }
            }

            for(int i = 0; i < g.VertexCount; i++)
                if (vert[i] == 0)
                    vert[i] = 1;

            return true;
        }

        // Część 3
        // Wyznaczanie minimalnego drzewa rozpinającego algorytmem Kruskala
        //   1 pkt
        // Schemat algorytmu Kruskala
        //   1) wrzucić wszystkie krawędzie do "wspólnego worka"
        //   2) wyciągać z "worka" krawędzie w kolejności wzrastających wag
        //      - jeśli krawędź można dodać do drzewa to dodawać, jeśli nie można to ignorować
        //      - punkt 2 powtarzać aż do skonstruowania drzewa (lub wyczerpania krawędzi)
        // Parametry:
        //   g - graf wejściowy
        //   mstw - waga skonstruowanego drzewa (lasu)
        // Wynik:
        //   skonstruowane minimalne drzewo rozpinające (albo las)
        // Uwagi:
        //   1) Graf wejściowy pozostaje niezmieniony
        //   2) Wykorzystać klasę UnionFind z biblioteki Graph
        //   3) Jeśli graf g jest niespójny to metoda wyznacza las rozpinający
        //   4) Graf wynikowy (drzewo) musi być w takiej samej reprezentacji jak wejściowy
        public Graph<int> Lab03Kruskal(Graph<int> g, out int mstw)
        {
            var edges = new PriorityQueue<int, Edge<int>>();
            foreach (var edge in g.DFS().SearchAll())
                edges.Insert(edge, edge.Weight);
            Graph<int> res = new Graph<int>(g.VertexCount, g.Representation);
            UnionFind uf = new UnionFind(g.VertexCount);
            mstw = 0;
            while (edges.Count > 0)
            {
                Edge<int> e = edges.Extract();
                int aSet = uf.Find(e.From);
                int bSet = uf.Find(e.To);
                if (aSet != bSet)
                {
                    uf.Union(aSet, bSet);
                    res.AddEdge(e.From, e.To, e.Weight);
                    mstw += e.Weight;
                    if (res.EdgeCount >= res.VertexCount - 1)
                        break;
                }
            }
            
            return res;
        }

        // Część 4
        // Badanie czy graf nieskierowany jest acykliczny
        //   0.5 pkt
        // Parametry:
        //   g - badany graf
        // Wynik:
        //   true jeśli graf jest acykliczny, false jeśli graf nie jest acykliczny
        // Uwagi:
        //   1) Graf wejściowy pozostaje niezmieniony
        //   2) Najpierw pomysleć jaki, prosty do sprawdzenia, warunek spełnia acykliczny graf nieskierowany
        //      Zakodowanie tego sprawdzenia nie powinno zająć więcej niż kilka linii!
        //      Zadanie jest bardzo łatwe (jeśli wydaje się trudne - poszukać prostszego sposobu, a nie walczyć z trudnym!)
        public bool Lab03IsUndirectedAcyclic(Graph g)
        {
            UnionFind forest = new UnionFind(g.VertexCount);
            Graph checkEdges = new Graph(g.VertexCount, g.Representation);
            foreach (var edge in g.DFS().SearchAll())
            {
                if (checkEdges.HasEdge(edge.From, edge.To))
                    continue;
                checkEdges.AddEdge(edge.From, edge.To);
                int aSet = forest.Find(edge.From);
                int bSet = forest.Find(edge.To);
                if (aSet != bSet)
                    forest.Union(aSet, bSet);
                else
                    return false;
            }
            
            return true;
        }

    }

}
