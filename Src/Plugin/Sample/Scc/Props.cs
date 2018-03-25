// DO NOT EDIT!!!
// This file was autogenerated by the PropGen utility from the props.xml schema file on 2008-05-15T00:09:06.

using JetBrains.Omea.OpenAPI;

namespace JetBrains.Omea.SamplePlugins.SccPlugin
{

    /// <summary>
    /// Definitions for the resource types and property types used by the plugin.
    /// </summary>
    internal class Props
    {
        internal const string RepositoryResource = "jetbrains.scc.Repository";
        internal const string ChangeSetResource = "jetbrains.scc.ChangeSet";
        internal const string FolderResource = "jetbrains.scc.Folder";
        internal const string FileChangeResource = "jetbrains.scc.FileChange";
        internal const string UserToRepositoryMapResource = "jetbrains.scc.UserToRepositoryMap";
        internal const string LinkRegexResource = "jetbrains.scc.LinkRegex";

        private static PropId<string> _propRepositoryType;
        private static PropId<int> _propChangeSetNumber;
        private static PropId<string> _propP4Client;
        private static PropId<IResource> _propAffectsFolder;
        private static PropId<string> _propChangeType;
        private static PropId<int> _propRevision;
        private static PropId<string> _propDiff;
        private static PropId<IResource> _propChange;
        private static PropId<bool> _propBinary;
        private static PropId<IResource> _propChangeSetRepository;
        private static PropId<string> _propP4IgnoreChanges;
        private static PropId<string> _propP4WebUrl;
        private static PropId<string> _propP4ServerPort;
        private static PropId<string> _propPathsToWatch;
        private static PropId<int> _propLastRevision;
        private static PropId<IResource> _propUserRepository;
        private static PropId<IResource> _propUserContact;
        private static PropId<string> _propUserId;
        private static PropId<string> _propRepositoryUrl;
        private static PropId<string> _propRepositoryRoot;
        private static PropId<string> _propUserName;
        private static PropId<string> _propPassword;
        private static PropId<string> _propRegexMatch;
        private static PropId<string> _propRegexReplace;
        private static PropId<string> _propLastError;
        private static PropId<bool> _propShowSubfolderContents;

        internal static PropId<string> RepositoryType { get { return _propRepositoryType; } }
        internal static PropId<int> ChangeSetNumber { get { return _propChangeSetNumber; } }
        internal static PropId<string> P4Client { get { return _propP4Client; } }
        internal static PropId<IResource> AffectsFolder { get { return _propAffectsFolder; } }
        internal static PropId<string> ChangeType { get { return _propChangeType; } }
        internal static PropId<int> Revision { get { return _propRevision; } }
        internal static PropId<string> Diff { get { return _propDiff; } }

        /// <summary>
        /// Links a ChangeSet to individual FileChange resources contained in it.
        /// </summary>
        internal static PropId<IResource> Change { get { return _propChange; } }
        internal static PropId<bool> Binary { get { return _propBinary; } }
        internal static PropId<IResource> ChangeSetRepository { get { return _propChangeSetRepository; } }
        internal static PropId<string> P4IgnoreChanges { get { return _propP4IgnoreChanges; } }
        internal static PropId<string> P4WebUrl { get { return _propP4WebUrl; } }
        internal static PropId<string> P4ServerPort { get { return _propP4ServerPort; } }
        internal static PropId<string> PathsToWatch { get { return _propPathsToWatch; } }
        internal static PropId<int> LastRevision { get { return _propLastRevision; } }
        internal static PropId<IResource> UserRepository { get { return _propUserRepository; } }
        internal static PropId<IResource> UserContact { get { return _propUserContact; } }
        internal static PropId<string> UserId { get { return _propUserId; } }
        internal static PropId<string> RepositoryUrl { get { return _propRepositoryUrl; } }
        internal static PropId<string> RepositoryRoot { get { return _propRepositoryRoot; } }
        internal static PropId<string> UserName { get { return _propUserName; } }
        internal static PropId<string> Password { get { return _propPassword; } }
        internal static PropId<string> RegexMatch { get { return _propRegexMatch; } }
        internal static PropId<string> RegexReplace { get { return _propRegexReplace; } }
        internal static PropId<string> LastError { get { return _propLastError; } }
        internal static PropId<bool> ShowSubfolderContents { get { return _propShowSubfolderContents; } }

        internal static void Register( IPlugin ownerPlugin )
        {
            IResourceStore store = Core.ResourceStore;
            _propRepositoryType = store.PropTypes.Register( "jetbrains.scc.RepositoryType", PropDataTypes.String,
                PropTypeFlags.Internal );
            _propChangeSetNumber = store.PropTypes.Register( "jetbrains.scc.ChangeSetNumber", PropDataTypes.Int );
            store.PropTypes.RegisterDisplayName( _propChangeSetNumber, "Change Set Number" );

            _propP4Client = store.PropTypes.Register( "jetbrains.scc.Client", PropDataTypes.String );

            _propAffectsFolder = store.PropTypes.Register( "jetbrains.scc.AffectsFolder", PropDataTypes.Link,
                PropTypeFlags.Internal | PropTypeFlags.CountUnread );
            _propChangeType = store.PropTypes.Register( "jetbrains.scc.ChangeType", PropDataTypes.String );
            store.PropTypes.RegisterDisplayName( _propChangeType, "Change Type" );

            _propRevision = store.PropTypes.Register( "jetbrains.scc.Revision", PropDataTypes.Int );

            _propDiff = store.PropTypes.Register( "jetbrains.scc.Diff", PropDataTypes.LongString,
                PropTypeFlags.Internal );
            _propChange = store.PropTypes.Register( "jetbrains.scc.Change", PropDataTypes.Link,
                PropTypeFlags.Internal | PropTypeFlags.DirectedLink );
            _propBinary = store.PropTypes.Register( "jetbrains.scc.Binary", PropDataTypes.Bool,
                PropTypeFlags.Internal );
            _propChangeSetRepository = store.PropTypes.Register( "jetbrains.scc.ChangeSetRepository", PropDataTypes.Link,
                PropTypeFlags.Internal );
            _propP4IgnoreChanges = store.PropTypes.Register( "jetbrains.scc.P4IgnoreChanges", PropDataTypes.LongString,
                PropTypeFlags.Internal );
            _propP4WebUrl = store.PropTypes.Register( "jetbrains.scc.P4WebUrl", PropDataTypes.LongString,
                PropTypeFlags.Internal );
            _propP4ServerPort = store.PropTypes.Register( "jetbrains.scc.P4ServerPort", PropDataTypes.LongString,
                PropTypeFlags.Internal );
            _propPathsToWatch = store.PropTypes.Register( "jetbrains.scc.PathsToWatch", PropDataTypes.LongString,
                PropTypeFlags.Internal );
            _propLastRevision = store.PropTypes.Register( "jetbrains.scc.LastRevision", PropDataTypes.Int );
            store.PropTypes.RegisterDisplayName( _propLastRevision, "Last Revision" );

            _propUserRepository = store.PropTypes.Register( "jetbrains.scc.UserRepository", PropDataTypes.Link,
                PropTypeFlags.Internal );
            _propUserContact = store.PropTypes.Register( "jetbrains.scc.UserContact", PropDataTypes.Link,
                PropTypeFlags.Internal );
            _propUserId = store.PropTypes.Register( "jetbrains.scc.UserId", PropDataTypes.String,
                PropTypeFlags.Internal );
            _propRepositoryUrl = store.PropTypes.Register( "jetbrains.scc.RepositoryUrl", PropDataTypes.String,
                PropTypeFlags.Internal );
            _propRepositoryRoot = store.PropTypes.Register( "jetbrains.scc.RepositoryRoot", PropDataTypes.String,
                PropTypeFlags.Internal );
            _propUserName = store.PropTypes.Register( "jetbrains.scc.UserName", PropDataTypes.String,
                PropTypeFlags.Internal );
            _propPassword = store.PropTypes.Register( "jetbrains.scc.Password", PropDataTypes.String,
                PropTypeFlags.Internal );
            _propRegexMatch = store.PropTypes.Register( "jetbrains.scc.RegexMatch", PropDataTypes.String,
                PropTypeFlags.Internal );
            _propRegexReplace = store.PropTypes.Register( "jetbrains.scc.RegexReplace", PropDataTypes.String,
                PropTypeFlags.Internal );
            _propLastError = store.PropTypes.Register( "jetbrains.scc.LastError", PropDataTypes.String,
                PropTypeFlags.Internal );
            _propShowSubfolderContents = store.PropTypes.Register( "jetbrains.scc.ShowSubfolderContents", PropDataTypes.Bool,
                PropTypeFlags.Internal );

            store.ResourceTypes.Register( RepositoryResource, "Repository", "Name",
                ResourceTypeFlags.ResourceContainer | ResourceTypeFlags.NoIndex, ownerPlugin );
            store.ResourceTypes.Register( ChangeSetResource, "Changeset", "Subject",
                ResourceTypeFlags.CanBeUnread, ownerPlugin );
            store.ResourceTypes.Register( FolderResource, "Folder", "Name",
                ResourceTypeFlags.ResourceContainer | ResourceTypeFlags.NoIndex, ownerPlugin );
            store.ResourceTypes.Register( FileChangeResource, "", "",
                ResourceTypeFlags.Internal | ResourceTypeFlags.NoIndex, ownerPlugin );
            store.ResourceTypes.Register( UserToRepositoryMapResource, "", "",
                ResourceTypeFlags.Internal | ResourceTypeFlags.NoIndex, ownerPlugin );
            store.ResourceTypes.Register( LinkRegexResource, "", "",
                ResourceTypeFlags.Internal | ResourceTypeFlags.NoIndex, ownerPlugin );

            store.RegisterLinkRestriction( UserToRepositoryMapResource, _propUserRepository, RepositoryResource, 1, 1 );
            store.RegisterLinkRestriction( UserToRepositoryMapResource, _propUserContact, "Contact", 1, 1 );
        }
    }

    internal partial class Folder: BusinessObject
    {
        internal class FolderResourceType: ResourceTypeId<Folder>
        {
            private FolderResourceType(): base(Props.FolderResource)
            {
            }

            public override Folder CreateBusinessObject(IResource res)
            {
                return new Folder(res);
            }

            internal static FolderResourceType Instance = new FolderResourceType();
        }

        public static FolderResourceType ResourceType = FolderResourceType.Instance;

        public static Folder Create()
        {
            return new Folder();
        }

        protected Folder(): base(Props.FolderResource)
        {
        }

        protected Folder(IResource res): base(res)
        {
        }

        public string Name
        {
            get { return GetProp(Core.PropIds.Name); }
            set { SetProp(Core.PropIds.Name, value); }
        }

        public IResource Parent
        {
            get { return GetProp(Core.PropIds.Parent); }
            set { SetProp(Core.PropIds.Parent, value); }
        }

    }

    internal partial class FileChange: BusinessObject
    {
        internal class FileChangeResourceType: ResourceTypeId<FileChange>
        {
            private FileChangeResourceType(): base(Props.FileChangeResource)
            {
            }

            public override FileChange CreateBusinessObject(IResource res)
            {
                return new FileChange(res);
            }

            internal static FileChangeResourceType Instance = new FileChangeResourceType();
        }

        public static FileChangeResourceType ResourceType = FileChangeResourceType.Instance;

        public static FileChange Create()
        {
            return new FileChange();
        }

        protected FileChange(): base(Props.FileChangeResource)
        {
        }

        protected FileChange(IResource res): base(res)
        {
        }

        public string Name
        {
            get { return GetProp(Core.PropIds.Name); }
            set { SetProp(Core.PropIds.Name, value); }
        }

        public IResource AffectsFolder
        {
            get { return GetProp(Props.AffectsFolder); }
            set { SetProp(Props.AffectsFolder, value); }
        }

        public string ChangeType
        {
            get { return GetProp(Props.ChangeType); }
            set { SetProp(Props.ChangeType, value); }
        }

        public int Revision
        {
            get { return GetProp(Props.Revision); }
            set { SetProp(Props.Revision, value); }
        }

        public string Diff
        {
            get { return GetProp(Props.Diff); }
            set { SetProp(Props.Diff, value); }
        }

        public bool Binary
        {
            get { return GetProp(Props.Binary); }
            set { SetProp(Props.Binary, value); }
        }

        public IResource Change
        {
            get { return GetProp(Props.Change); }
            set { SetProp(Props.Change, value); }
        }

    }

    internal partial class UserToRepositoryMap: BusinessObject
    {
        internal class UserToRepositoryMapResourceType: ResourceTypeId<UserToRepositoryMap>
        {
            private UserToRepositoryMapResourceType(): base(Props.UserToRepositoryMapResource)
            {
            }

            public override UserToRepositoryMap CreateBusinessObject(IResource res)
            {
                return new UserToRepositoryMap(res);
            }

            internal static UserToRepositoryMapResourceType Instance = new UserToRepositoryMapResourceType();
        }

        public static UserToRepositoryMapResourceType ResourceType = UserToRepositoryMapResourceType.Instance;

        public static UserToRepositoryMap Create()
        {
            return new UserToRepositoryMap();
        }

        protected UserToRepositoryMap(): base(Props.UserToRepositoryMapResource)
        {
        }

        protected UserToRepositoryMap(IResource res): base(res)
        {
        }

        public string UserId
        {
            get { return GetProp(Props.UserId); }
            set { SetProp(Props.UserId, value); }
        }

        public IResource UserRepository
        {
            get { return GetProp(Props.UserRepository); }
            set { SetProp(Props.UserRepository, value); }
        }

        public IResource UserContact
        {
            get { return GetProp(Props.UserContact); }
            set { SetProp(Props.UserContact, value); }
        }

    }

    internal partial class LinkRegex: BusinessObject
    {
        internal class LinkRegexResourceType: ResourceTypeId<LinkRegex>
        {
            private LinkRegexResourceType(): base(Props.LinkRegexResource)
            {
            }

            public override LinkRegex CreateBusinessObject(IResource res)
            {
                return new LinkRegex(res);
            }

            internal static LinkRegexResourceType Instance = new LinkRegexResourceType();
        }

        public static LinkRegexResourceType ResourceType = LinkRegexResourceType.Instance;

        public static LinkRegex Create()
        {
            return new LinkRegex();
        }

        protected LinkRegex(): base(Props.LinkRegexResource)
        {
        }

        protected LinkRegex(IResource res): base(res)
        {
        }

        public string RegexMatch
        {
            get { return GetProp(Props.RegexMatch); }
            set { SetProp(Props.RegexMatch, value); }
        }

        public string RegexReplace
        {
            get { return GetProp(Props.RegexReplace); }
            set { SetProp(Props.RegexReplace, value); }
        }

    }

}
