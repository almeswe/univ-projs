﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace Faker.Analyzer
{
    public sealed class FakerAnalyzer
    {
        public GraphNet _net;

        private static Type[] DefaultTypes = new Type[]
        {
            typeof(char),
            typeof(bool),
            typeof(sbyte),
            typeof(short),
            typeof(int),
            typeof(long),
            typeof(byte),
            typeof(ushort),
            typeof(uint),
            typeof(ulong),
            typeof(float),
            typeof(double),
            typeof(string)
        };

        public void Analyze<T>()
        {
            this._net = new GraphNet(typeof(T));
            this.Analyze(typeof(T), this._net.Root);
        }

        private void Analyze(Type type, Graph graph)
        {
            var nodes = new List<Graph>();
            foreach (var memberType in this.GetTypes(type))
                nodes.Add(this._net.AddGraphEdge(graph, memberType));
            foreach (var node in nodes)
                this.Analyze(node.Type, node);
        }

        private Type[] GetTypes(Type type)
        {
            var types = new List<Type>();
            if (DefaultTypes.Contains(type))
                return types.ToArray();
            types.AddRange(type.GetProperties()
                .Where(p => p.CanWrite)
                .Select(p => p.PropertyType));
            types.AddRange(type.GetFields()
                .Where(f => f.IsPublic)
                .Select(f => f.FieldType));
            return types.ToArray();
        }
    }
}
