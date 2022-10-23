using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CodeWalker;
using CodeWalker.GameFiles;
using SharpDX;

namespace ONV_Exporter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initializing...");

            OnvObj Onv = new OnvObj();

            byte[] b = File.ReadAllBytes(args[0]);
            YnvFile Ynv = new YnvFile();

            Ynv.Load(b);
            NavMesh Nav = Ynv.Nav;

            Onv.Indices = Ynv.Indices;
            Onv.Edges = Ynv.Edges.ConvertAll(
                new Converter<YnvEdge, OnvObj.OnvEdge>(OnvObj.OnvEdge.YnvToOnvEdge)
            );
            Onv.Polys = Ynv.Polys.ConvertAll(
                new Converter<YnvPoly, OnvObj.OnvPoly>(OnvObj.OnvPoly.YnvToOnvPoly)
            );

            NavMeshSector SectorTree = Nav.SectorTree;
            Onv.SectorTree = new OnvObj.OnvNavTree(SectorTree);
            Onv.Size = Nav.AABBSize;
            Onv.Vertices = Nav.Vertices.GetFullList();


            Onv.Export(args[1]);

            /*if (Ynv.AllPortals != null)
            {
                foreach (var portal in Ynv.AllPortals)
                {
                    Console.WriteLine(portal.RawData);
                }
            }*/

            Console.WriteLine("Success!");

        }
    }
}
