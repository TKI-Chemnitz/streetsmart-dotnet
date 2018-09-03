﻿/*
 * Street Smart .NET integration
 * Copyright (c) 2016 - 2018, CycloMedia, All rights reserved.
 * 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3.0 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 * 
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library.
 */

using System;
using System.Collections.Generic;

using StreetSmart.Common.Interfaces.Data;
using StreetSmart.Common.Interfaces.GeoJson;

namespace StreetSmart.Common.Data.GeoJson
{
  internal class Polygon : List<IList<ICoordinate>>, IPolygon
  {
    public Polygon(Dictionary<string, object> polygon)
    {
      string type = polygon?["type"]?.ToString() ?? string.Empty;
      IList<object> coordinates = polygon?["coordinates"] as IList<object> ?? new List<object>();

      try
      {
        Type = (MeasurementGeometryType) Enum.Parse(typeof(MeasurementGeometryType), type);
      }
      catch (ArgumentException)
      {
        Type = MeasurementGeometryType.Unknown;
      }

      foreach (var ring in coordinates)
      {
        List<ICoordinate> coordinateList = new List<ICoordinate>();

        foreach (var coordinate in ring as IList<object> ?? new List<object>())
        {
          coordinateList.Add(new Coordinate(coordinate as IList<object>));
        }

        Add(coordinateList);
      }
    }

    public MeasurementGeometryType Type { get; }
  }
}
