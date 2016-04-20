using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Rhino;
using Rhino.Collections;
using Rhino.Commands;
using Rhino.DocObjects;
using Rhino.Display;
using Rhino.Geometry;
using Rhino.Geometry.Collections;
using Rhino.Geometry.Intersect;
using Rhino.Geometry.Morphs;
using Rhino.Input;
using Rhino.Input.Custom;
using Rhino.UI;
using Rhino.UI.Gumball;
using System.Runtime.InteropServices;

class TestCompileWrapperClass
{
public static Rhino.Commands.Result AddMaterial(Rhino.RhinoDoc doc)
{
  // materials are stored in the document's material table
  int index = doc.Materials.Add();
  Rhino.DocObjects.Material mat = doc.Materials[index];
  mat.DiffuseColor = System.Drawing.Color.Chocolate;
  mat.SpecularColor = System.Drawing.Color.CadetBlue;
  mat.CommitChanges();

  // set up object attributes to say they use a specific material
  Rhino.Geometry.Sphere sp = new Rhino.Geometry.Sphere(Rhino.Geometry.Plane.WorldXY, 5);
  Rhino.DocObjects.ObjectAttributes attr = new Rhino.DocObjects.ObjectAttributes();
  attr.MaterialIndex = index;
  attr.MaterialSource = Rhino.DocObjects.ObjectMaterialSource.MaterialFromObject;
  doc.Objects.AddSphere(sp, attr);

  // add a sphere without the material attributes set
  sp.Center = new Rhino.Geometry.Point3d(10, 10, 0);
  doc.Objects.AddSphere(sp);

  doc.Views.Redraw();
  return Rhino.Commands.Result.Success;
}
}
