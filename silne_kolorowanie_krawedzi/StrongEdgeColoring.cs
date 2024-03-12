
namespace ASD
{
    using ASD.Graphs;
    using System.Collections.Generic;

    public class Lab03 : System.MarshalByRefObject
    {
        // Część I
        // Funkcja zwracajaca kwadrat danego grafu.
        // Kwadratem grafu nazywamy graf o takim samym zbiorze wierzchołków jak graf pierwotny, w którym wierzchołki
        // połączone sa krawędzią jeśli w grafie pierwotnym były polączone krawędzia bądź ścieżką złożoną z 2 krawędzi
        // (ale pętli, czyli krawędzi o początku i końcu w tym samym wierzchołku, nie dodajemy!).
        public Graph Square(Graph graph)
        {
            // // Dziala ale wolno
            // Graph square = new Graph(graph.VertexCount, graph.Representation);
            // foreach (var edge in graph.DFS().SearchAll())
            // {
            //     square.AddEdge(edge.To, edge.From);
            //     foreach (var secondEdge in graph.DFS().SearchAll())
            //     {
            //         if (edge.From == secondEdge.To && secondEdge.From != edge.To && 
            //             !square.HasEdge(secondEdge.From, edge.To))
            //             square.AddEdge(secondEdge.From, edge.To);
            //         if (edge.To == secondEdge.From && secondEdge.To != edge.From &&
            //             !square.HasEdge(secondEdge.To, edge.From))
            //             square.AddEdge(secondEdge.To, edge.From);
            //         if (edge.To == secondEdge.To && edge.From != secondEdge.From && 
            //             !square.HasEdge(edge.From, secondEdge.From))
            //             square.AddEdge(edge.From, secondEdge.From);
            //     }
            // }
            
            // DUZO SZYBIEJ !!!
            // Teraz sposob po wierzcholkach
            Graph square = new Graph(graph.VertexCount, graph.Representation);
            for (int i = 0; i < graph.VertexCount; i++)
            {
                foreach (var n in graph.OutNeighbors(i))
                {
                    foreach (var n2 in graph.OutNeighbors(n))
                    {
                        if (n2 != i && !square.HasEdge(i, n2))
                            square.AddEdge(i, n2);
                    }

                    if (!square.HasEdge(i, n))
                        square.AddEdge(i, n);
                }
            }
            
            return square;
        }

        // Część II
        // Funkcja zwracająca Graf krawędziowy danego grafu.
        // Wierzchołki grafu krawędziwego odpowiadają krawędziom grafu pierwotnego, wierzcholki grafu krawędziwego
        // połączone sa krawędzią jeśli w grafie pierwotnym z krawędzi odpowiadającej pierwszemu z nich można przejść
        // na krawędź odpowiadającą drugiemu z nich przez wspólny wierzchołek.

        // Tablicę names tworzymy i wypełniamy według następującej zasady.
        // Każdemu wierzchołkowi grafu krawędziowego odpowiada element tablicy names (o indeksie równym numerowi wierzchołka)
        // zawierający informację z jakiej krawędzi grafu pierwotnego wierzchołek ten powstał.
        // Np.dla wierzchołka powstałego z krawedzi <0,1> do tablicy zapisujemy krotke (0, 1) - przyda się w dalszych etapach
        public Graph LineGraph(Graph graph, out (int x, int y)[] names)
        {
            names = null;
            return null;
        }

        // Część III
        // Funkcja znajdujaca poprawne kolorowanie wierzchołków danego grafu nieskierowanego.
        // Kolorowanie wierzchołków jest poprawne, gdy każde dwa sąsiadujące wierzchołki mają różne kolory
        // Funkcja ma szukać kolorowania według następujacego algorytmu zachłannego:

        // Dla wszystkich wierzchołków v (od 0 do n-1)
        // pokoloruj wierzcholek v kolorem o najmniejszym możliwym numerze(czyli takim, na który nie są pomalowani jego sąsiedzi)
        // Kolory numerujemy począwszy od 0.

        // UWAGA: Podany opis wyznacza kolorowanie jednoznacznie, jakiekolwiek inne kolorowanie, nawet jeśli spełnia formalnie
        // definicję kolorowania poprawnego, na potrzeby tego zadania będzie uznane za błędne.

        // Funkcja zwraca liczbę użytych kolorów (czyli najwyższy numer użytego koloru + 1),
        // a w tablicy colors zapamiętuje kolory poszczególnych wierzchołkow.
        public int VertexColoring(Graph graph, out int[] colors)
        {
            colors = null;
            return 0;
        }

        // Funkcja znajduje silne kolorowanie krawędzi danego grafu.
        // Silne kolorowanie krawędzi grafu jest poprawne gdy każde dwie krawędzie, które są ze sobą sąsiednie
        // (czyli można przejść z jednej na drugą przez wspólny wierzchołek)
        // albo są połączone inną krawędzią(czyli można przejść z jednej na drugą przez ową inną krawędź), mają różne kolory.

        // Należy zwrocić nowy graf, który będzie miał strukturę identyczną jak zadany graf,
        // ale w wagach krawędzi zostaną zapisane przydzielone kolory.

        // Wskazówka - to bardzo proste.Należy wykorzystać wszystkie poprzednie funkcje.
        // Zastanowić się co możemy powiedzieć o kolorowaniu wierzchołków kwadratu grafu krawędziowego?
        // Jak się to ma do silnego kolorowania krawędzi grafu pierwotnego?
        public int StrongEdgeColoring(Graph graph, out Graph<int> coloredGraph)
        {
            coloredGraph = null;
            return 0;
        }

    }

}
