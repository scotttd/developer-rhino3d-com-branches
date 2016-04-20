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
public override string EnglishName { get { return "cs_CustomUndoCommand"; } }

double MyFavoriteNumber { get; set; }

protected override Rhino.Commands.Result RunCommand(RhinoDoc doc, Rhino.Commands.RunMode mode)
{
  // Rhino automatically sets up an undo record when a command is run,
  // but... the undo record is not saved if nothing changes in the
  // document (objects added/deleted, layers changed,...)
  //
  // If we have a command that doesn't change things in the document,
  // but we want to have our own custom undo called then we need to do
  // a little extra work

  double d = MyFavoriteNumber;
  if (Rhino.Input.RhinoGet.GetNumber("Favorite number", true, ref d) == Rhino.Commands.Result.Success)
  {
    double current_value = MyFavoriteNumber;
    doc.AddCustomUndoEvent("Favorite Number", OnUndoFavoriteNumber, current_value);
    MyFavoriteNumber = d;
  }
  return Rhino.Commands.Result.Success;
}

// event handler for custom undo
void OnUndoFavoriteNumber(object sender, Rhino.Commands.CustomUndoEventArgs e)
{
  // !!!!!!!!!!
  // NEVER change any setting in the Rhino document or application.  Rhino
  // handles ALL changes to the application and document and you will break
  // the Undo/Redo commands if you make any changes to the application or
  // document. This is meant only for your own private plug-in data
  // !!!!!!!!!!

  // This function can be called either by undo or redo
  // In order to get redo to work, add another custom undo event with the
  // current value.  If you don't want redo to work, just skip adding
  // a custom undo event here
  double current_value = MyFavoriteNumber;
  e.Document.AddCustomUndoEvent("Favorite Number", OnUndoFavoriteNumber, current_value);

  double old_value = (double)e.Tag;
  RhinoApp.WriteLine("Going back to your favorite = {0}", old_value);
  MyFavoriteNumber = old_value;
}
}
