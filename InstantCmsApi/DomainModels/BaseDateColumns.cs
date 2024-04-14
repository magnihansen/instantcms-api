﻿using System;
namespace InstantCmsApi.DomainModels;

public abstract class BaseDateColumns
{
    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; }

    public DateTime UpdatedDate { get; set; }

    public string UpdatedBy { get; set; }
}
