﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Request
{
    public record GridNodeDto(GridNodeTypeDto Type, int Weight);

    public class GridNodeDtoValidator : AbstractValidator<GridNodeDto>
    {
        public GridNodeDtoValidator()
        {
            RuleFor(node => node.Weight)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Grid node weight can not be negative.");
        }
    }
}
