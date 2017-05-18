using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cereal64.VisObj64.Data.OpenGL;
using System.Collections.ObjectModel;

namespace TrackShack.Data
{
    public class ElementSelectionGroup
    {
        public ReadOnlyCollection<VO64GraphicsElement> Elements;
        private List<IVO64Vertex> Vertices;

        public float MinX { get; set; }
        public float MaxX { get; set; }
        public float MinY { get; set; }
        public float MaxY { get; set; }
        public float MinZ { get; set; }
        public float MaxZ { get; set; }

        public ElementSelectionGroup(List<VO64GraphicsElement> elements)
        {
            Elements = elements.AsReadOnly();
            LoadVertices();
        }

        public ElementSelectionGroup(VO64GraphicsCollection collection)
        {

            Elements = collection.GetAllElements().AsReadOnly();
            LoadVertices();
        }

        public ElementSelectionGroup(List<VO64GraphicsCollection> collections)
        {
            List<VO64GraphicsElement> elements = new List<VO64GraphicsElement>();
            foreach (VO64GraphicsCollection coll in collections)
                elements = elements.Union(coll.GetAllElements()).ToList();
            Elements = elements.AsReadOnly();
            LoadVertices();
        }

        public void CalculateMinMaxes()
        {
            //Analyze the vertices
            MinX = float.MaxValue;
            MinY = float.MaxValue;
            MinZ = float.MaxValue;
            MaxX = float.MinValue;
            MaxY = float.MinValue;
            MaxZ = float.MinValue;
            foreach (IVO64Vertex vertex in Vertices)
            {
                if (vertex.X < MinX)
                    MinX = vertex.X;
                if (vertex.X > MaxX)
                    MaxX = vertex.X;
                if (vertex.Y < MinY)
                    MinY = vertex.Y;
                if (vertex.Y > MaxY)
                    MaxY = vertex.Y;
                if (vertex.Z < MinZ)
                    MinZ = vertex.Z;
                if (vertex.Z > MaxZ)
                    MaxZ = vertex.Z;
            }
        }

        private void LoadVertices()
        {
            Vertices = new List<IVO64Vertex>();
            foreach (VO64GraphicsElement el in Elements)
            {
                Vertices = Vertices.Union(el.Vertices).ToList();
            }
            CalculateMinMaxes();
        }
    }
}
