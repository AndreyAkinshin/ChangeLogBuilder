using IniParser;
using IniParser.Model;
using JetBrains.Annotations;

namespace ChangeLogBuilder
{
    public class Config
    {
        private static readonly FileIniDataParser Parser = new FileIniDataParser();

        public const string MainConfigFileName = "config.ini";

        private const string CredentialsSectionKey = "Credentials";
        private const string ProductHeaderKey = "ProductHeader";
        private const string TokenKey = "Token";

        private const string ChangeLogSectionKey = "ChangeLog";
        private const string RepoOwnerKey = "RepoOwner";
        private const string RepoNameKey = "RepoName";
        private const string CurrentMilestoneKey = "CurrentMilestone";
        private const string PreviousMilestoneKey = "PreviousMilestone";
        private const string LastCommitKey = "LastCommit";

        private const string Unknown = "?";

        private readonly IniData data;

        [PublicAPI] public string ProductHeader => data[CredentialsSectionKey][ProductHeaderKey];
        [PublicAPI] public string Token => data[CredentialsSectionKey][TokenKey];

        [PublicAPI] public string RepoOwner => data[ChangeLogSectionKey][RepoOwnerKey];
        [PublicAPI] public string RepoName => data[ChangeLogSectionKey][RepoNameKey];
        [PublicAPI] public string CurrentMilestone => data[ChangeLogSectionKey][CurrentMilestoneKey];
        [PublicAPI] public string PreviousMilestone => data[ChangeLogSectionKey][PreviousMilestoneKey];
        [PublicAPI] public string LastCommit => data[ChangeLogSectionKey][LastCommitKey];

        public void Deconstruct(out string repoOwner, out string repoName, out string currentMilestone,
            out string previousMilestone, out string lastCommit)
        {
            repoOwner = RepoOwner;
            repoName = RepoName;
            currentMilestone = CurrentMilestone;
            previousMilestone = PreviousMilestone;
            lastCommit = LastCommit;
        }

        private Config(IniData data) => this.data = data;

        public static Config CreateBlank()
        {
            var data = new IniData();
            data[CredentialsSectionKey][ProductHeaderKey] = Unknown;
            data[CredentialsSectionKey][TokenKey] = Unknown;
            data[ChangeLogSectionKey][RepoOwnerKey] = Unknown;
            data[ChangeLogSectionKey][RepoNameKey] = Unknown;
            data[ChangeLogSectionKey][CurrentMilestoneKey] = Unknown;
            data[ChangeLogSectionKey][PreviousMilestoneKey] = Unknown;
            data[ChangeLogSectionKey][LastCommitKey] = Unknown;
            return new Config(data);
        }

        public static Config ReadFile(string filePath) => new Config(Parser.ReadFile(filePath));

        public void WriteFile(string filePath) => Parser.WriteFile(filePath, data);
    }
}