using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Authorization;
using System;
using System.Linq;

namespace ElLib.Net7.Service
{
    public class ModeratorAreaAuthorization : IControllerModelConvention
    {
        private readonly string area;
        private readonly string policy;
        public ModeratorAreaAuthorization(string area, string policy)
        {
            this.area = area;
            this.policy = policy;
        }
        public void Apply(ControllerModel controller)
        {
            if (controller.Attributes.Any(a =>
            a is AreaAttribute && (a as AreaAttribute).RouteValue.Equals(area, StringComparison.OrdinalIgnoreCase)))
            {
                controller.Filters.Add(new AuthorizeFilter(policy));
            }
        }
    }
}
