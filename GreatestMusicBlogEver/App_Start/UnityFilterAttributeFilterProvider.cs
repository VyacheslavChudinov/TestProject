using System.Collections.Generic;
using System.Web.Mvc;
using Microsoft.Practices.Unity;

namespace Service
{
    public class UnityFilterAttributeFilterProvider : FilterAttributeFilterProvider
    {
        private readonly IUnityContainer container;

        public UnityFilterAttributeFilterProvider(IUnityContainer container)
        {
            this.container = container;
        }

        protected override IEnumerable<FilterAttribute> GetControllerAttributes(
                    ControllerContext controllerContext,
                    ActionDescriptor actionDescriptor)
        {

            var attributes = base.GetControllerAttributes(controllerContext,
                                                          actionDescriptor);
            foreach (var attribute in attributes)
            {
                container.BuildUp(attribute.GetType(), attribute);
            }

            return attributes;
        }

        protected override IEnumerable<FilterAttribute> GetActionAttributes(
                    ControllerContext controllerContext,
                    ActionDescriptor actionDescriptor)
        {

            var attributes = base.GetActionAttributes(controllerContext,
                                                      actionDescriptor);
            foreach (var attribute in attributes)
            {
                container.BuildUp(attribute.GetType(), attribute);
            }

            return attributes;
        }
    }
}