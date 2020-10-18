using System.Collections.Generic;
using System.Text.RegularExpressions;
using FluentValidation;
using NzbDrone.Common.Extensions;
using NzbDrone.Core.Annotations;
using NzbDrone.Core.Languages;
using NzbDrone.Core.Parser.Model;
using NzbDrone.Core.Validation;

namespace NzbDrone.Core.Indexers.IPTorrents
{
    public class IPTorrentsSettingsValidator : AbstractValidator<IPTorrentsSettings>
    {
        public IPTorrentsSettingsValidator()
        {
            RuleFor(c => c.BaseUrl).ValidRootUrl();

            RuleFor(c => c.BaseUrl).Matches(@"(?:/|t\.)rss\?.+$");

            RuleFor(c => c.BaseUrl).Matches(@"(?:/|t\.)rss\?.+;download(?:;|$)")
                .WithMessage("Use Direct Download Url (;download)")
                .When(v => v.BaseUrl.IsNotNullOrWhiteSpace() && Regex.IsMatch(v.BaseUrl, @"(?:/|t\.)rss\?.+$"));
        }
    }

    public class IPTorrentsSettings : ITorrentIndexerSettings
    {
        private static readonly IPTorrentsSettingsValidator Validator = new IPTorrentsSettingsValidator();

        public IPTorrentsSettings()
        {
            BaseUrl = string.Empty;
            MinimumSeeders = IndexerDefaults.MINIMUM_SEEDERS;
            MultiLanguages = new List<int>();
            RequiredFlags = new List<int>();
        }

        [FieldDefinition(0, Label = "Feed URL", HelpText = "The full RSS feed url generated by IPTorrents, using only the categories you selected (HD, SD, x264, etc ...)")]
        public string BaseUrl { get; set; }

        [FieldDefinition(1, Type = FieldType.Select, SelectOptions = typeof(LanguageFieldConverter), Label = "Multi Languages", HelpText = "What languages are normally in a multi release on this indexer?", Advanced = true)]
        public IEnumerable<int> MultiLanguages { get; set; }

        [FieldDefinition(2, Type = FieldType.Number, Label = "Minimum Seeders", HelpText = "Minimum number of seeders required.", Advanced = true)]
        public int MinimumSeeders { get; set; }

        [FieldDefinition(3, Type = FieldType.TagSelect, SelectOptions = typeof(IndexerFlags), Label = "Required Flags", HelpText = "What indexer flags are required?", HelpLink = "https://github.com/Prowlarr/Prowlarr/wiki/Indexer-Flags#1-required-flags", Advanced = true)]
        public IEnumerable<int> RequiredFlags { get; set; }

        public NzbDroneValidationResult Validate()
        {
            return new NzbDroneValidationResult(Validator.Validate(this));
        }
    }
}
