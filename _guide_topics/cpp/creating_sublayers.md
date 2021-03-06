---
title: Creating Sublayers
description: This brief guide demonstrates how to create sublayers of a parent layer using C/C++.
author: dale@mcneel.com
apis: ['C/C++']
languages: ['C/C++']
platforms: ['Windows']
categories: ['Fundamentals']
origin: http://wiki.mcneel.com/developer/sdksamples/childlayer
order: 1
keywords: ['rhino', 'layers']
layout: toc-guide-page
---

# Creating Sublayers

{{ page.description }}

## Problem

You would like to create a "sublayer" (or a "child layer") of a parent layer.

## Solution

All layers have a layer id field, or `ON_Layer::m_layer_id`, that uniquely identifies that layer.  Also, layers maintain a parent id field, or `ON_Layer::m_parent_layer_id`, that identifies the layer's parent.  If a layer's parent id is a `null` UUID, then the layer does not have a parent and, thus, is considered a root layer.

## Sample

The following sample demonstrates how to add a parent layer then then add a child layer to that parent.

```cpp
CRhinoCommand::result CCommandTest::RunCommand( const CRhinoCommandContext& context )
{
  CRhinoLayerTable& layer_table = context.m_doc.m_layer_table;

  // Define parent layer
  ON_Layer parent_layer;
  parent_layer.m_name = L"Parent";

  // Add parent layer
  int parent_layer_index = layer_table.AddLayer( parent_layer );
  if( parent_layer_index >= 0 )
  {
    // Get the layer we just added
    const CRhinoLayer& layer = layer_table[parent_layer_index];

    // Define child layer
    ON_Layer child_layer;
    child_layer.m_name = L"Child";
    // Assign parent layer's id as child's parent id
    child_layer.m_parent_layer_id = layer.m_layer_id;

    // Add child layer
    layer_table.AddLayer( child_layer );
  }

  return CRhinoCommand::success;
}
```
