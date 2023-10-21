﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO;

/// <summary>
/// Engineer Entity represents a student with all its prop
/// </summary>
/// <param name="Id">Personal unique ID of engineer (as in national id card)</param>
/// <param name="Name">Name of the engineer</param>
/// <param name="Email">o Email address of the engineer</param>
/// <param name="Level">The level of the engineer</param>
/// <param name="cost">Hourly cost of the engineer</param>
public record Engineer
(
    int Id,
    string Name,
    string? Email = null,
    EngineerExperience Level = EngineerExperience.Junior,
    double? Cost = 0
);
