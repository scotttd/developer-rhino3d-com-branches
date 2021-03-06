---
title: Surface from Control Points
description: Demonstrates how to create a surface from a grid of control points.
author: dale@mcneel.com
apis: ['C/C++']
languages: ['C/C++']
platforms: ['Windows']
categories: ['Adding Objects']
origin: http://wiki.mcneel.com/developer/sdksamples/rhinosrfcontrolptgrid
order: 1
keywords: ['rhino']
layout: code-sample-cpp
---

```cpp
CRhinoCommand::result CCommandTest::RunCommand( const CRhinoCommandContext& context )
{
  // Degree 3 surface
  int degree[2];
  degree[0] = 3;
  degree[1] = 3;

  // Four columns of four points
  int point_count[2];
  point_count[0] = 4;
  point_count[1] = 4;

  ON_3dPointArray point_array( point_count[0] * point_count[1] );

  point_array.Append( ON_3dPoint(0,0,0) );
  point_array.Append( ON_3dPoint(0,3.33,0) );
  point_array.Append( ON_3dPoint(0,6.67,0) );
  point_array.Append( ON_3dPoint(0,10,0) );

  point_array.Append( ON_3dPoint(6.68,0,-0.0296) );
  point_array.Append( ON_3dPoint(6.68,3.33,-0.0296) );
  point_array.Append( ON_3dPoint(6.68,6.67,-0.0296) );
  point_array.Append( ON_3dPoint(6.68,10,-0.0296) );

  point_array.Append( ON_3dPoint(13.3,0,2.77) );
  point_array.Append( ON_3dPoint(13.3,3.33,2.77) );
  point_array.Append( ON_3dPoint(13.3,6.67,2.77) );
  point_array.Append( ON_3dPoint(13.3,10,2.77) );

  point_array.Append( ON_3dPoint(17.9,0,7.58) );
  point_array.Append( ON_3dPoint(17.9,3.33,7.58) );
  point_array.Append( ON_3dPoint(17.9,6.67,7.58) );
  point_array.Append( ON_3dPoint(17.9,10,7.58) );

  ON_NurbsSurface srf;
  if( RhinoSrfControlPtGrid( point_count, degree, point_array, &srf) )
  {
    context.m_doc.AddSurfaceObject( srf );
    context.m_doc.Redraw();
  }

  return success;
}
```
