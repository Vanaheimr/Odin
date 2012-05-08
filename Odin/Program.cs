/*
 * Copyright (c) 2010-2012, Achim 'ahzf' Friedland <achim@graph-database.org>
 * This file is part of Odin <http://www.github.com/Vanaheimr/Odin>
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#region Usings

using System;
using System.Linq;
using System.Collections.Generic;

using de.ahzf.Illias.Commons;
using de.ahzf.Blueprints;
using de.ahzf.Blueprints.PropertyGraphs;
using de.ahzf.Balder;

#endregion

namespace de.ahzf.Vanaheimr.Odin
{

    public class Program
    {

        private const String _isPerson = "isPerson";
        private const String _likes    = "likes";
        private const String _Age      = "Age";
        
        public static void Main(String[] args)
        {

            var _graph = GraphFactory.CreateGenericPropertyGraph2("My first graph");


            // Some graph vertex events...
            _graph.OnVertexAdding += (graph, vertex, vote) =>
                Console.WriteLine("I like all vertices!");

            _graph.OnVertexAdding += (graph, vertex, vote) =>
            {
                if (vertex.Id == "Daniel")
                {
                    Console.WriteLine("Sorry " + vertex.Id + ", please check your karma!");
                    vote.Veto();
                }
            };


            // Add vertices and edges to the graph
            var Alice  = _graph.AddVertex("Alice",  _isPerson, v => v.SetProperty(_Age, 21));
            var Bob    = _graph.AddVertex("Bob",    _isPerson, v => v.AddEdge_chainable(_likes, Alice, e => e.SetProperty("intensity", 1.0)).
                                                                      SetProperty(_Age, 23));
            var Carol  = _graph.AddVertex("Carol",  _isPerson, v => v.AddEdge_chainable(_likes, Alice).AddEdge_chainable(_likes, Bob).
                                                                      SetProperty(_Age, 20));
            var Daniel = _graph.AddVertex("Daniel", _isPerson, v => v.SetProperty(_Age, 30));

            var e1     = _graph.AddEdge(Alice, _likes, Bob);

            Console.WriteLine("Number of vertices added: "    + _graph.NumberOfVertices());
            Console.WriteLine("Number of edges added: "       + _graph.NumberOfEdges());
            Console.WriteLine("Number of multi edges added: " + _graph.NumberOfMultiEdges());
            Console.WriteLine("Number of hyper edges added: " + _graph.NumberOfHyperEdges());


            // Who likes someone...
            _graph.EdgesByLabel("likes").
                   OutV().
                   Ids().
                   Distinct().
                   ForEach(Name => Console.WriteLine(Name));


            // Who is liked by someone... long version ;)
            foreach (var Name in _graph.Edges(e => e.Label == _likes).
                                        InV(v => v.Label == _isPerson).
                                        Prop("Id").
                                        Distinct())
                Console.WriteLine(Name);


            // Add some property events on Carol
            Carol.OnPropertyChanging += (sender, Key, oldValue, newValue, vote) =>
                Console.WriteLine(sender.GetProperty("Id") + "'s " + Key + "' property changing: '" + oldValue + "' -> '" + newValue + "'");

            Carol.OnPropertyChanged += (sender, Key, oldValue, newValue) =>
                Console.WriteLine(sender.GetProperty("Id") + "'s " + Key + " property changed: '" + oldValue + "' -> '" + newValue + "'");


            // Let's do some dynamic magics...
            var _DynamicCarol = Carol.AsDynamic();
            Console.WriteLine("How old is Carol today? " + _DynamicCarol.Age);
            _DynamicCarol.Age += 1;
            Console.WriteLine("How old is Alice in a year? " + _DynamicCarol.Age);

            _DynamicCarol.Likes = (Action<IEnumerable<String>>) ((likes) => Console.WriteLine("Carol likes: " + likes.Aggregate((a,b) => a + ", " + b)));
            _DynamicCarol.Likes(Carol.OutEdges(_likes).InV().Ids().Distinct());


        }

    }

}
