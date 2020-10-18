using NzbDrone.Core.Notifications;

namespace Prowlarr.Api.V1.Notifications
{
    public class NotificationResource : ProviderResource
    {
        public string Link { get; set; }
        public bool OnHealthIssue { get; set; }
        public bool SupportsOnHealthIssue { get; set; }
        public bool IncludeHealthWarnings { get; set; }
        public string TestCommand { get; set; }
    }

    public class NotificationResourceMapper : ProviderResourceMapper<NotificationResource, NotificationDefinition>
    {
        public override NotificationResource ToResource(NotificationDefinition definition)
        {
            if (definition == null)
            {
                return default(NotificationResource);
            }

            var resource = base.ToResource(definition);

            resource.OnHealthIssue = definition.OnHealthIssue;
            resource.SupportsOnHealthIssue = definition.SupportsOnHealthIssue;
            resource.IncludeHealthWarnings = definition.IncludeHealthWarnings;

            return resource;
        }

        public override NotificationDefinition ToModel(NotificationResource resource)
        {
            if (resource == null)
            {
                return default(NotificationDefinition);
            }

            var definition = base.ToModel(resource);

            definition.OnHealthIssue = resource.OnHealthIssue;
            definition.SupportsOnHealthIssue = resource.SupportsOnHealthIssue;
            definition.IncludeHealthWarnings = resource.IncludeHealthWarnings;

            return definition;
        }
    }
}
