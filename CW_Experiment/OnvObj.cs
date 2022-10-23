using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.IO;
using CodeWalker;
using CodeWalker.GameFiles;
using SharpDX;

namespace ONV_Exporter
{
    public class OnvObj
    {
        public List<NavMeshVertex> Vertices;
        public List<ushort> Indices;
        public List<OnvEdge> Edges;
        public List<OnvPoly> Polys;
        public OnvNavTree SectorTree;
        public Vector3 Size;

        public OnvObj()
        {
            //...
        }

        void PrintSectorData(OnvNavTree st, string indent, StreamWriter sw)
        {
            sw.Write("{0}\tSectorData ", indent);

            if (st.SectorData == null)
            {
                sw.Write("null\n");
                return;
            }

            ushort[] spIndices = st.SectorPolyIndices != null ? st.SectorPolyIndices : new ushort[0];
            NavMeshPoint[] bounds = st.SectorBounds != null ? st.SectorBounds : new NavMeshPoint[0];

            sw.WriteLine("\n{0}\t{{\n{0}\t\tPolyIndices {1}", indent, spIndices.Length);

            if (spIndices.Length > 0)
            {
                sw.WriteLine("{0}\t\t{{", indent);
                for (int i = 0; i < spIndices.Length; i += 15)
                {
                    sw.Write("{0}\t\t\t", indent);
                    for (int j = i; j < i + 15 && j < spIndices.Length; j++)
                    {
                        sw.Write("{0} ", spIndices[j]);
                    }
                    sw.Write('\n');
                }
                sw.WriteLine("{0}\t\t}}", indent);
            }

            sw.WriteLine("{0}\t\tBounds {1}", indent, bounds.Length);
            if (bounds.Length > 0)
            {
                sw.WriteLine("{0}\t\t{{", indent);
                foreach (NavMeshPoint pt in bounds)
                {
                    sw.WriteLine("{0}\t\t\t{1} {2} {3} {4}", indent, pt.X, pt.Y, pt.Z, pt.Angle);
                }
                sw.WriteLine("{0}\t\t}}", indent);
            }

            sw.WriteLine("{0}\t}}", indent);
        }

        void PrintSectorTree(OnvNavTree st, int depth, string name, StreamWriter sw)
        {
            string indent = String.Concat(Enumerable.Repeat("\t", depth));

            if (st == null)
            {
                sw.WriteLine("{0}{1} null", indent, name);
                return;
            }

            Vector3 AABBMin = st.AABBMin;
            Vector3 AABBMax = st.AABBMax;

            sw.WriteLine("{0}{1}\n{0}{{", indent, name);
            sw.WriteLine("{0}\tAABBMin {1} {2} {3}", indent, AABBMin.X, AABBMin.Y, AABBMin.Z);
            sw.WriteLine("{0}\tAABBMax {1} {2} {3}", indent, AABBMax.X, AABBMax.Y, AABBMax.Z);
            PrintSectorData(st, indent, sw);
            PrintSectorTree(st.SubTree0, depth + 1, "SubTree0", sw);
            PrintSectorTree(st.SubTree1, depth + 1, "SubTree1", sw);
            PrintSectorTree(st.SubTree2, depth + 1, "SubTree2", sw);
            PrintSectorTree(st.SubTree3, depth + 1, "SubTree3", sw);
            sw.WriteLine("{0}}}", indent);
        }

        // TODO: Write generic text generation from Object to filestream
        public void Export(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine("Version 1 1");
                sw.WriteLine("Sizes {0} {1} {2}", Size.X, Size.Y, Size.Z);
                sw.WriteLine("Flags 3");
                //Vertices
                sw.WriteLine("Vertices {0}\n{{", Vertices.Count);
                foreach (NavMeshVertex v in Vertices)
                {
                    sw.WriteLine("\t{0} {1} {2}", v.X, v.Y, v.Z);
                }
                //Indices
                sw.WriteLine("}}\nIndices {0}\n{{", Indices.Count);
                for (int i = 0; i < Indices.Count; i += 15)
                {
                    sw.Write('\t');
                    for (int j = i; j < i + 15 && j < Indices.Count; j++)
                    {
                        sw.Write("{0} ", Indices[j]);
                    }
                    sw.Write('\n');
                }
                //Edges
                sw.WriteLine("}}\nEdges {0}\n{{", Edges.Count);
                foreach (OnvEdge e in Edges)
                {
                    sw.WriteLine("\t{0}, 0, {1}, {2}, {3}, 0", e.AreaID1, e.PolyID1, e.AreaID2, e.PolyID2);
                }
                //Polygons
                uint totalPolyEdges = 0;
                sw.WriteLine("}}\nPolys {0}\n{{", Polys.Count);
                foreach (OnvPoly p in Polys)
                {
                    sw.WriteLine("\t{0} {1} {2} 0 0", totalPolyEdges, p.EdgeCount, p.Flags);
                    totalPolyEdges += p.EdgeCount;
                }
                sw.WriteLine("}");
                //SectorTree
                PrintSectorTree(SectorTree, 0, "SectorTree", sw);

                sw.WriteLine("Portals 0\nSectorID {0}", Edges[0].AreaID1);
            }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class OnvEdge
        {
            int _PolyID1, _PolyID2, _AreaID1, _AreaID2;
            public int PolyID1 { get { return _PolyID1; } set { _PolyID1 = LimitRange(value); } }
            public int PolyID2 { get { return _PolyID2; } set { _PolyID2 = LimitRange(value); } }
            public int AreaID1 { get { return _AreaID1; } set { _AreaID1 = LimitRange(value); } }
            public int AreaID2 { get { return _AreaID2; } set { _AreaID2 = LimitRange(value); } }

            public OnvEdge() { }

            public OnvEdge(int PolyID1, int PolyID2, int AreaID1, int AreaID2)
            {
                this.PolyID1 = PolyID1;
                this.PolyID2 = PolyID2;
                this.AreaID1 = AreaID1;
                this.AreaID2 = AreaID2;
            }

            public static OnvEdge YnvToOnvEdge(YnvEdge e)
            {
                return new OnvEdge((int)e.PolyID1, (int)e.PolyID2, (int)e.AreaID1, (int)e.AreaID2);
            }

            static int LimitRange(int value)
            {
                return value > 16382 ? -1 : value;
            }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class OnvPoly
        {
            public uint Flags;
            public uint EdgeCount;

            public OnvPoly() { }

            public OnvPoly(uint Flags, uint EdgeCount)
            {
                this.Flags = Flags;
                this.EdgeCount = EdgeCount;
            }

            public static OnvPoly YnvToOnvPoly(YnvPoly p)
            {
                return new OnvPoly(p.RawData.PolyFlags0, (uint)p.Edges.Length);
            }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        public class OnvNavTree
        {
            public Vector3 AABBMin;
            public Vector3 AABBMax;

            public NavMeshSectorData SectorData;
            public ushort[] SectorPolyIndices;
            public NavMeshPoint[] SectorBounds;

            public OnvNavTree SubTree0;
            public OnvNavTree SubTree1;
            public OnvNavTree SubTree2;
            public OnvNavTree SubTree3;

            public OnvNavTree() { }
            public OnvNavTree(NavMeshSector tree) 
            {
                AABBMin = (Vector3)tree.AABBMin;
                AABBMax = (Vector3)tree.AABBMax;
                SectorData = tree.Data;
                if (SectorData != null)
                {
                    SectorPolyIndices = SectorData.PolyIDs;
                    SectorBounds = SectorData.Points;
                }

                SubTree0 = tree.SubTree1 == null ? null : new OnvNavTree(tree.SubTree1);
                SubTree1 = tree.SubTree2 == null ? null : new OnvNavTree(tree.SubTree2);
                SubTree2 = tree.SubTree3 == null ? null : new OnvNavTree(tree.SubTree3);
                SubTree3 = tree.SubTree4 == null ? null : new OnvNavTree(tree.SubTree4);
            }
        }
    }
}