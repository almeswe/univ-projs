using System;
using System.Linq;
using System.Diagnostics;

using System.Collections.Generic;

namespace Tracer.Core
{
    public sealed class CallTree
    {
        public Dictionary<int, List<CallNode>> Roots;

        public CallTree() =>
            this.Roots = new Dictionary<int, List<CallNode>>();
        
        public void AddNode(CallNode node)
        {
            int id = node.Note.ThreadId;
            if (!this.Roots.ContainsKey(id))
                this.Roots[id] = new List<CallNode>(){ node };
            else
            {
                CallNode currentNode = null;
                var frames = this.GetFrames(node);
                foreach (var frame in frames)
                    foreach (var rootNode in this.Roots[id])
                        if (frame.GetMethod().Name == rootNode.MethodName)
                            currentNode = rootNode;
                if (currentNode == null)
                    this.Roots[id].Add(node);
                else
                {
                    var nodesToCheck = new List<CallNode>() { currentNode };
                    foreach (var frame in frames)
                    {
                        foreach (var nodeToCheck in nodesToCheck)
                        {
                            if (frame.GetMethod().Name == nodeToCheck.MethodName)
                            {
                                currentNode = nodeToCheck;
                                nodesToCheck = nodeToCheck.Nodes;
                                goto continueChecking;
                            }
                        }
                        currentNode.Nodes.Add(node);
                        return;
                    continueChecking:;
                    }
                    currentNode.Nodes.Add(node);
                }
            }
        }

        private List<StackFrame> GetFrames(CallNode node)
        {
            var frames = new List<StackFrame>();
            frames.AddRange(node.Trace.GetFrames());
            frames = frames.Skip(2).ToList();
            frames.Reverse();
            return frames;
        }

        public TimeSpan GetThreadSummary(int threadId)
        {
            if (!this.Roots.ContainsKey(threadId))
                throw new ArgumentException("Speicified thread id is not registered.");
            var summary = TimeSpan.Zero;
            foreach (var rootNode in this.Roots[threadId])
                summary += rootNode.Note.ExecutionTime;
            return summary;
        }

        public void PrintTree()
        {
            string indentation = "  ";
            foreach (var key in this.Roots.Keys)
            {
                Console.WriteLine($"Thread id: [{key}] " +
                    $"[{this.GetThreadSummary(key).TotalMilliseconds} ms]");
                foreach (var rootNode in this.Roots[key])
                    this.PrintNode(rootNode, indentation);
            }
        }

        private void PrintNode(CallNode node, string indentation)
        {
            Console.WriteLine($"{indentation}Method [{node.MethodName}] " +
                $"[{node.Note.ExecutionTime.Milliseconds} ms]");
            indentation += "  ";
            foreach (var subNode in node.Nodes)
                this.PrintNode(subNode, indentation);
        }
    }
}